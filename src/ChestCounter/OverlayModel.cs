namespace ChestCounter;

/// <summary>Builds what the overlays (window and OBS page) show from the saved matches and the match in progress.</summary>
public static class OverlayModel
{
    /// <summary>Caption of the right counter between two matches (also used to keep the overlay width stable).</summary>
    public static string LastMatchCaption => L.T("dernière partie", "last match");

    /// <param name="live">Figures of the match in progress, when the replay could be read (null otherwise).</param>
    /// <param name="inProgress">A match is being recorded (even if its figures are not readable yet).</param>
    public static OverlayView Build(TrackerData d, MatchRecord? live, bool inProgress)
    {
        var liveChests = live?.ChestsOpened ?? 0;
        var liveTreasures = live?.TreasureFinds.Count ?? 0;

        var total = d.Matches.Sum(m => m.ChestsOpened) + liveChests;
        var session = d.Matches.Where(ReportWriter.IsThisSession).ToList();
        var rows = new List<OverlayRow>();

        // Session: chests, then small grey "n parties · moy. x" (finished matches of this session).
        string? sessionDetail = null;
        if (session.Count > 0)
        {
            var avg = (double)session.Sum(m => m.ChestsOpened) / session.Count;
            var avgText = Math.Round(avg, MidpointRounding.AwayFromZero).ToString("0", L.Culture);
            sessionDetail = $"{L.Count(session.Count, "partie", "parties", "match", "matches")} · {L.T("moy.", "avg.")} {avgText}";
        }
        rows.Add(new OverlayRow("Session", L.Count(session.Sum(m => m.ChestsOpened) + liveChests, "coffre", "chest"), Detail: sessionDetail));

        var last = d.Matches.OrderByDescending(m => m.MatchStartUtc ?? m.AnalyzedAtUtc).FirstOrDefault();
        if (last != null)
        {
            rows.Add(new OverlayRow(L.T("Dernière", "Last"), L.Count(last.ChestsOpened, "coffre", "chest")));
        }

        var treasures = d.Matches.Sum(m => m.TreasureFinds.Count) + liveTreasures;
        rows.Add(new OverlayRow(L.T("Trésors", "Treasures"), $"★ {L.N(treasures)}", Treasure: true));

        // Right counter: the match in progress when readable, "…" while it cannot be read yet, else the last match.
        if (live != null)
        {
            return new OverlayView(L.N(total), L.N(live.ChestsOpened), L.T("partie en cours", "current match"), true, L.T("● EN DIRECT", "● LIVE"), rows);
        }
        if (inProgress)
        {
            return new OverlayView(L.N(total), "…", L.T("en cours", "in progress"), true, L.T("● PARTIE EN COURS", "● MATCH IN PROGRESS"), rows);
        }
        return new OverlayView(L.N(total), last != null ? L.N(last.ChestsOpened) : null, LastMatchCaption, false, null, rows);
    }
}
