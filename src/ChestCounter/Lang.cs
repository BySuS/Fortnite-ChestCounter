using System.Globalization;

namespace ChestCounter;

/// <summary>
/// User interface language (French or English), chosen on first launch and saved in config.json.
/// Texts are written inline as <c>L.T("français", "English")</c>. Names stored in data.json (chest kinds,
/// treasure items) are kept in French for compatibility and translated when displayed (<see cref="Name"/>).
/// </summary>
public static class L
{
    public static bool En { get; private set; }

    public static CultureInfo Culture { get; private set; } = CultureInfo.GetCultureInfo("fr-FR");

    public static string Code => En ? "en" : "fr";

    public static void Set(string? code)
    {
        En = Normalize(code) == "en";
        Culture = CultureInfo.GetCultureInfo(En ? "en-GB" : "fr-FR");
    }

    /// <summary>"fr", "en" or null when the answer is not recognized.</summary>
    public static string? Normalize(string? code) => code?.Trim().ToLowerInvariant() switch
    {
        "fr" or "fra" or "fre" or "french" or "français" or "francais" => "fr",
        "en" or "eng" or "english" or "anglais" => "en",
        _ => null,
    };

    /// <summary>Language of Windows: French if Windows is in French, English otherwise.</summary>
    public static string SystemDefault => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "fr" ? "fr" : "en";

    public static string T(string fr, string en) => En ? en : fr;

    public static string N(int n) => n.ToString("N0", Culture);

    /// <summary>"3 coffres" / "3 chests" (regular plurals: + "s").</summary>
    public static string Count(int n, string fr, string en) => Count(n, fr, fr + "s", en, en + "s");

    /// <summary>French: singular for 0 and 1. English: singular for 1 only.</summary>
    public static string Count(int n, string frOne, string frMany, string enOne, string enMany) =>
        En ? $"{N(n)} {(n == 1 ? enOne : enMany)}" : $"{N(n)} {(n > 1 ? frMany : frOne)}";

    /// <summary>"12e" / "12th".</summary>
    public static string Ordinal(int n)
    {
        if (!En)
        {
            return n == 1 ? "1er" : $"{n}e";
        }
        var suffix = (n % 100) is 11 or 12 or 13 ? "th" : (n % 10) switch { 1 => "st", 2 => "nd", 3 => "rd", _ => "th" };
        return $"{n}{suffix}";
    }

    /// <summary>Short date + time of a match ("05/11 22:54" / "5 Nov 22:54").</summary>
    public static string DateTimeShort(DateTime d) => d.ToString(En ? "d MMM HH:mm" : "dd/MM HH:mm", Culture);

    // French names stored in data.json -> English display names.
    private static readonly Dictionary<string, string> FrToEn = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Coffre"] = "Chest",
        ["Coffre rare"] = "Rare chest",
        ["Coffre enterré"] = "Buried chest",
        ["Coffre d'extraction / reliques"] = "Extraction / relic chest",
        ["Coffre des affiches d'esprit"] = "Sprite poster chest",
        ["Autre coffre"] = "Other chest",
        ["Canon à butin"] = "Loot cannon",
        ["Caisse de munitions"] = "Ammo box",
        ["Caisse de nourriture"] = "Food box",
        ["Autre conteneur"] = "Other container",
        ["Trésor"] = "Treasure",
        ["Trésor (type inconnu)"] = "Treasure gizmo",
        ["Gizmo de trésor"] = "Treasure gizmo",
        ["Feu d'artifice doré"] = "Golden firework",
        ["Extracteur portable"] = "Portable extractor",
        ["Localisateur d'esprit"] = "Relic locator",
        ["Localisateur chanceux"] = "Lucky locator",
        ["Accélérateur d'extraction"] = "Extraction accelerator",
        ["inconnu"] = "unknown",
    };

    /// <summary>Display name of a chest kind or treasure item stored in French.</summary>
    public static string Name(string? stored)
    {
        if (string.IsNullOrEmpty(stored))
        {
            return stored ?? "";
        }
        return En && FrToEn.TryGetValue(stored, out var en) ? en : stored;
    }
}
