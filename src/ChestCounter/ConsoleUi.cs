namespace ChestCounter;

/// <summary>
/// Small interactive console helpers: screens that replace each other, arrow-key menus and a progress bar.
/// Everything falls back to plain text when the console is redirected (no keyboard / no cursor).
/// </summary>
public static class ConsoleUi
{
    /// <summary>Where keys come from: the console, or the queue fed by the keyboard thread once it runs.</summary>
    public static Func<ConsoleKeyInfo> ReadKey { get; set; } = () => Console.ReadKey(intercept: true);

    /// <summary>True when keys can be read and the cursor moved.</summary>
    public static bool Interactive { get; } = !Console.IsInputRedirected && !Console.IsOutputRedirected;

    private static int Width
    {
        get
        {
            try { return Math.Max(40, Console.WindowWidth); } catch { return 80; }
        }
    }

    /// <summary>Clears the window and draws the title bar.</summary>
    public static void Screen(string title, string? step = null)
    {
        if (Interactive)
        {
            try { Console.Clear(); } catch { /* not a real console */ }
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"  {title}");
        Console.ResetColor();
        if (step != null)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"   {step}");
            Console.ResetColor();
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  " + new string('─', Math.Min(Width - 4, 72)));
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>Window title (ignored when there is no console window, e.g. output redirected).</summary>
    public static void SetTitle(string title)
    {
        try { Console.Title = title; } catch { }
    }

    public static void Text(string text, ConsoleColor? color = null)
    {
        if (color is { } c) Console.ForegroundColor = c;
        foreach (var line in text.Split('\n'))
        {
            Console.WriteLine("  " + line);
        }
        if (color != null) Console.ResetColor();
    }

    public static void Hint(string text) => Text(text, ConsoleColor.DarkGray);

    /// <summary>
    /// Arrow-key menu (↑ ↓ then Enter; digits also work). Returns the chosen index.
    /// Without a keyboard, returns <paramref name="selected"/>.
    /// </summary>
    public static int Menu(IReadOnlyList<string> options, int selected = 0, string? help = null)
    {
        if (!Interactive)
        {
            return selected;
        }

        Console.WriteLine();
        var top = Console.CursorTop;
        var cursorVisible = true;
        try { cursorVisible = Console.CursorVisible; Console.CursorVisible = false; } catch { }
        try
        {
            while (true)
            {
                Console.SetCursorPosition(0, top);
                for (var i = 0; i < options.Count; i++)
                {
                    var line = $"  {(i == selected ? "›" : " ")} {options[i]}";
                    if (i == selected)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(line);
                    Console.ResetColor();
                    Console.WriteLine(new string(' ', Math.Max(0, Width - line.Length - 1)));
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("  " + (help ?? L.T("↑ ↓ pour choisir, Entrée pour valider", "↑ ↓ to choose, Enter to confirm", "↑ ↓ para elegir, Intro para confirmar")));
                Console.ResetColor();

                var key = ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow: selected = (selected + options.Count - 1) % options.Count; break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.Tab: selected = (selected + 1) % options.Count; break;
                    case ConsoleKey.Home: selected = 0; break;
                    case ConsoleKey.End: selected = options.Count - 1; break;
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        Console.WriteLine();
                        return selected;
                    default:
                        if (key.KeyChar is >= '1' and <= '9' && key.KeyChar - '1' < options.Count)
                        {
                            selected = key.KeyChar - '1';
                        }
                        break;
                }
            }
        }
        finally
        {
            try { Console.CursorVisible = cursorVisible; } catch { }
        }
    }

    /// <summary>Free text answer (Enter alone keeps <paramref name="defaultValue"/>).</summary>
    public static string Input(string prompt, string defaultValue = "")
    {
        if (!Interactive)
        {
            return defaultValue;
        }
        Console.Write($"  {prompt}");
        var answer = (Console.ReadLine() ?? "").Trim().Trim('"');
        return answer.Length == 0 ? defaultValue : answer;
    }

    public static void WaitEnter(string text)
    {
        if (!Interactive)
        {
            return;
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"  {text}");
        Console.ResetColor();
        while (ReadKey().Key != ConsoleKey.Enter) { }
        Console.WriteLine();
    }
}

/// <summary>One line progress bar, redrawn in place: "  Analyse  [██████░░░░]  12 / 31  file".</summary>
public sealed class ProgressBar
{
    private readonly string _label;
    private readonly int _total;
    private int _lastLength;

    public ProgressBar(string label, int total)
    {
        _label = label;
        _total = Math.Max(1, total);
    }

    public void Update(int done, string? detail = null)
    {
        const int barWidth = 28;
        var filled = (int)Math.Round(barWidth * Math.Clamp(done, 0, _total) / (double)_total);
        var bar = new string('█', filled) + new string('░', barWidth - filled);
        var count = $"{done} / {_total}";
        if (!ConsoleUi.Interactive)
        {
            return; // no redraw possible: the summary line at the end is enough
        }

        int width;
        try { width = Console.WindowWidth; } catch { width = 80; }
        var text = $"  {_label}  {bar}  {count}";
        if (!string.IsNullOrEmpty(detail))
        {
            var room = width - text.Length - 4;
            if (room > 8)
            {
                text += "  " + (detail.Length > room ? "…" + detail[^(room - 1)..] : detail);
            }
        }
        Console.Write("\r");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"  {_label}  ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(bar);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(text[(4 + _label.Length + bar.Length)..]);
        Console.ResetColor();
        if (text.Length < _lastLength)
        {
            Console.Write(new string(' ', _lastLength - text.Length));
        }
        _lastLength = text.Length;
    }

    /// <summary>Erases the bar so the summary can be written on the same line.</summary>
    public void Clear()
    {
        if (!ConsoleUi.Interactive || _lastLength == 0)
        {
            return;
        }
        Console.Write("\r" + new string(' ', _lastLength) + "\r");
        _lastLength = 0;
    }
}
