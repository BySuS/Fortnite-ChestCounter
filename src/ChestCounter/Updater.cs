using System.Security.Cryptography;
using System.Text.Json;

namespace ChestCounter;

public sealed record UpdateInfo(Version Version, string Tag, string AssetUrl, long Size, string? Sha256, string PageUrl);

/// <summary>
/// Checks the latest GitHub release and replaces ChestCounter.exe with it. The running exe is renamed (Windows allows
/// that), the new one takes its place and is started; the old file is deleted at the next launch.
/// This is the only network access of the tool, and it can be turned off in the settings.
/// </summary>
public static class Updater
{
    private static string ApiUrl => Environment.GetEnvironmentVariable("CHESTCOUNTER_UPDATE_API")
        ?? $"https://api.github.com/repos/{AppInfo.Repo}/releases/latest";

    private static HttpClient NewClient(TimeSpan timeout)
    {
        var http = new HttpClient { Timeout = timeout };
        http.DefaultRequestHeaders.UserAgent.ParseAdd($"ChestCounter/{AppInfo.VersionText}");
        http.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
        return http;
    }

    /// <summary>The newer release, or null (up to date, offline, repository not reachable…).</summary>
    public static async Task<UpdateInfo?> CheckAsync()
    {
        try
        {
            using var http = NewClient(TimeSpan.FromSeconds(10));
            using var response = await http.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var root = doc.RootElement;
            if (root.TryGetProperty("draft", out var draft) && draft.GetBoolean()
                || root.TryGetProperty("prerelease", out var pre) && pre.GetBoolean())
            {
                return null;
            }
            var tag = root.GetProperty("tag_name").GetString() ?? "";
            if (!Version.TryParse(tag.TrimStart('v', 'V'), out var version) || Normalize(version) <= Normalize(AppInfo.Version))
            {
                return null;
            }
            foreach (var asset in root.GetProperty("assets").EnumerateArray())
            {
                if (!string.Equals(asset.GetProperty("name").GetString(), "ChestCounter.exe", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                string? sha = null;
                if (asset.TryGetProperty("digest", out var digest) && digest.GetString() is { } d && d.StartsWith("sha256:", StringComparison.OrdinalIgnoreCase))
                {
                    sha = d[7..];
                }
                return new UpdateInfo(version, tag, asset.GetProperty("browser_download_url").GetString()!, asset.GetProperty("size").GetInt64(), sha,
                    root.TryGetProperty("html_url", out var page) ? page.GetString() ?? AppInfo.RepoUrl : AppInfo.RepoUrl);
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    private static Version Normalize(Version v) => new(v.Major, v.Minor, Math.Max(0, v.Build));

    /// <summary>Downloads and puts the new exe in place. Returns null when done, else the reason it failed.</summary>
    public static string? Install(UpdateInfo update)
    {
        var exe = AppInfo.ExePath;
        if (exe == null)
        {
            return L.T("mise à jour possible seulement depuis ChestCounter.exe", "update only possible from ChestCounter.exe");
        }
        var download = exe + ".download";
        var old = exe + ".old";
        try
        {
            using (var http = NewClient(TimeSpan.FromMinutes(5)))
            using (var source = http.GetStreamAsync(update.AssetUrl).GetAwaiter().GetResult())
            using (var target = File.Create(download))
            {
                source.CopyTo(target);
            }

            var info = new FileInfo(download);
            if (info.Length != update.Size)
            {
                throw new IOException(L.T("téléchargement incomplet", "incomplete download"));
            }
            using (var fs = File.OpenRead(download))
            {
                if (fs.ReadByte() != 'M' || fs.ReadByte() != 'Z')
                {
                    throw new IOException(L.T("le fichier téléchargé n'est pas un exe", "the downloaded file is not an exe"));
                }
                if (update.Sha256 != null)
                {
                    fs.Position = 0;
                    var hash = Convert.ToHexString(SHA256.HashData(fs));
                    if (!hash.Equals(update.Sha256, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new IOException(L.T("empreinte SHA-256 incorrecte", "wrong SHA-256 checksum"));
                    }
                }
            }

            // The standby process may run the current exe: stop it (it is restarted from the new exe).
            if (OperatingSystem.IsWindows())
            {
                FortniteWatch.StopStandby();
            }
            if (File.Exists(old))
            {
                try { File.Delete(old); } catch { old = exe + $".{DateTime.Now:yyyyMMddHHmmss}.old"; }
            }
            File.Move(exe, old);
            File.Move(download, exe);
            return null;
        }
        catch (Exception ex)
        {
            try { File.Delete(download); } catch { }
            return ex.Message;
        }
    }

    /// <summary>Deletes the files left by the previous update.</summary>
    public static void CleanupOld()
    {
        var exe = AppInfo.ExePath;
        var dir = exe != null ? Path.GetDirectoryName(exe) : null;
        if (dir == null)
        {
            return;
        }
        foreach (var f in Directory.EnumerateFiles(dir, "ChestCounter.exe.*"))
        {
            if (f.EndsWith(".old", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".download", StringComparison.OrdinalIgnoreCase))
            {
                try { File.Delete(f); } catch { /* still in use: next time */ }
            }
        }
    }
}
