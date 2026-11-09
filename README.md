<div align="center">

**English** · [Français](README.fr.md) · [Español](README.es.md)

# Fortnite Chest Counter

[![version](https://img.shields.io/github/v/release/LED-N/Fortnite-ChestCounter?style=flat-square&label=version&color=e8b04b)](../../releases/latest) [![downloads](https://img.shields.io/github/downloads/LED-N/Fortnite-ChestCounter/total?style=flat-square&label=downloads&color=e8b04b)](../../releases) ![Windows 10 | 11](https://img.shields.io/badge/Windows-10%20%7C%2011-3a7bd5?style=flat-square) [![license](https://img.shields.io/github/license/LED-N/Fortnite-ChestCounter?style=flat-square&label=license&color=555)](LICENSE)

**Counts every chest you open in Fortnite, match after match, with an in-game overlay.**

<img src="docs/overlay-en.png" alt="Overlay between two matches" width="300">&nbsp;&nbsp;<img src="docs/overlay-match-en.png" alt="Overlay during a match" width="300">

[**⬇ Download ChestCounter.exe**](../../releases/latest) · Windows 10 / 11 · free · no install

</div>

---

Chest Counter reads the replays Fortnite saves after each match and keeps a running count of the chests you opened, plus the **treasure hunt treasures** your chests gave you. An always-on-top overlay shows your totals while you play, and a detailed report keeps your whole history.

> It counts the chests **you opened**. It does not show where chests are on the map.

## How it works (and why it is safe)

Fortnite can record a **replay file** of every match on your PC. Chest Counter reads these files once the match is over, and nothing else.

- ✅ **Only reads replay files**: the `.replay` files Fortnite writes to `%LOCALAPPDATA%\FortniteGame\Saved\Demos`. They are opened read-only and never modified.
- ✅ **No game modification**: nothing in the Fortnite installation folder is touched, no file is added or patched.
- ✅ **No access to the game's memory**: the tool never reads, writes or injects anything into the Fortnite process. It does not interact with the running game at all.
- ✅ **No data sent anywhere**: no login, no Epic account access, no telemetry. The only network access is the check for a new version on GitHub, which can be turned off in the settings.
- ✅ **Lightweight**: runs at low priority (and in "efficiency mode" on Windows 11), sleeps between matches, and the overlay is only redrawn when a number changes.

> [!IMPORTANT]
> **Replays must be turned on in Fortnite**: *Settings → Game → Replays → Record Replays: **On***. Without replays, the tool has nothing to read.

Because Fortnite only writes the replay's decryption key when the match ends, chests are counted **a few seconds after each match**, not during it. While you play, the overlay shows **● MATCH IN PROGRESS**.

## Getting started

1. Download **[`ChestCounter.exe`](../../releases/latest)**. That single file is all you need.
2. Turn on replays in Fortnite (see above).
3. Run `ChestCounter.exe`.
   > Windows may show *"Windows protected your PC"* because the exe is not signed. Click *More info → Run anyway*.
4. A short setup (arrow keys + Enter) asks for your **language** (English / French / Spanish), checks your **replays folder**, offers to count the **replays already on your PC**, and offers to **open the counter automatically when Fortnite starts**.

<p align="center"><img src="docs/setup-en.png" alt="First launch setup" width="760"></p>

If you count your existing replays, a progress bar runs in the window while the report opens in your browser and fills in live:

<p align="center"><img src="docs/progress-en.png" alt="Analysis progress bar" width="760"></p>

5. Keep the window open (you can minimize it) while you play. A few seconds after each match, its chests are added:

<p align="center"><img src="docs/console-en.png" alt="Main window" width="820"></p>

> [!TIP]
> Fortnite only keeps a limited number of unsaved replays and deletes the oldest ones, and a match played while the counter is closed is not counted. Turn on **Open with Fortnite** (offered during the setup, or in the settings with **C**) so you never have to think about it.

## Open with Fortnite

When this option is on, a tiny invisible process starts with Windows and checks every few seconds whether Fortnite is running. It reads nothing from the game: it only looks at the list of running programs. When Fortnite starts, it opens the counter (minimized, the game keeps the screen). When Fortnite closes, the counter counts your last match and closes itself 30 seconds later; press any key in its window to keep it open. Turn it off at any time in the settings (**C**): the invisible process stops and is removed from Windows startup.

## The overlay

- **Big numbers**: your **total** chests (gold) and the chests of your **last match**.
- **Session**: chests since the tool was started, with the number of matches and the average.
- **Last**: chests of your last match. **Treasures**: treasures found so far.
- During a match, the badge **● MATCH IN PROGRESS** appears and the right counter shows "…" until the match ends.

To see it over the game, set Fortnite to **Windowed Fullscreen** (*Settings → Video → Window Mode*): in exclusive fullscreen, Windows cannot draw anything on top of the game. Too big, too small or too bright? Its **size** (70 to 175%) and **opacity** (100 to 30%) can be changed with **C**, and you see the change right away. Streaming? `overlay.html` is the same overlay with a transparent background, for an **OBS** browser source.

## Shortcuts

| Shortcut | What it does |
|---|---|
| **Ctrl+Shift+O** | Show / hide the overlay. Works anywhere, even in game. |
| **Ctrl+Shift+L** | Lock / unlock the overlay. Works anywhere. **Locked**: the mouse goes through it and it cannot move (best in game). **Unlocked**: hold the left mouse button to move it. |
| Right-click on the overlay | Hide it. |
| Padlock on the overlay | Lock it. |
| **R** | Open the report. |
| **O** / **L** | Same as Ctrl+Shift+O / Ctrl+Shift+L. |
| **P** | Switch profile (several accounts on this PC). |
| **C** | Settings: language, open with Fortnite, updates, overlay at startup, overlay size and opacity, report at startup, replays folder. |
| **U** | Check for a new version, and install it when there is one. |
| **S** / **H** / **Q** | Summary / help / quit. |

The single-letter keys work in the tool's window. If another program already uses Ctrl+Shift+O or Ctrl+Shift+L, the tool tells you and the O and L keys still work.

## The report

`report.html` opens in your browser and refreshes by itself: total, average, best match, chests per day, breakdown by chest type, treasures and every match.

<p align="center"><img src="docs/report-en.png" alt="Report" width="820"></p>

## Treasures

- **★★ Golden chest (certain).** When a chest gives a treasure hunt prize (cosmetic, music track, loading screen, vehicle…), the server spawns a golden chest carrying the **prize id**. The tool reads it from the replay.
- **★ Every treasure received (exact moment).** Each treasure you receive is recorded with its exact time and the chest it came from; it only counts if that chest is **yours** (treasures shared by a teammate are not counted). When the item can be identified, it is named; otherwise it shows as **"Treasure gizmo"** (golden firework, portable extractor, locators, extraction accelerator), whose exact type is not in the replay.
- A treasure that came from something other than a chest (loot cannon, ammo box…) is highlighted with ⚑ in the report.

The game only sends prize ids, not names: `treasures.json` maps them to names and can be edited with Notepad. New ids are added automatically and the tool tells you when one needs a name.

## Profiles (several accounts on one PC)

The replays saved on a PC are normally those of its player, so there is **no name to type**: the tool counts the player who recorded each replay. If replays from several accounts are found, each account gets its own profile with its own statistics. The tool then asks which account you play on at each launch, and you can switch at any time with **P**. A match played on another account is saved in that account's profile, never lost.

## Updates

At startup, the counter checks GitHub for a new version. When there is one, it tells you, and pressing **U** downloads it, checks it and restarts the counter: your history and settings are kept. Was the counter already open when the version came out? Press **U**: it checks GitHub again. In the settings (**C**) you can choose between *offered* (default), *installed automatically* (only between two matches) or *off* (no network access at all).

## Your data

Everything is stored in `%APPDATA%\ChestCounter`:

| File | Content |
|---|---|
| `report.html` | The report (opens in your browser). |
| `overlay.html` | Transparent overlay page for OBS. |
| `matches.csv` | One line per match, for Excel or Google Sheets. |
| `data.json` | Your full history. **Back it up** if you change PC. |
| `config.json` | Language, profile, replays folder, overlay position, size and opacity. |
| `treasures.json` | Names of the treasure hunt prizes. |
| `errors.log` | Details of unreadable replays, to attach to a bug report. |

To start over: run `ChestCounter.exe --reset-config`, or delete that folder.

## FAQ

**Nothing is counted.** Check that replays are turned on in Fortnite, and that the tool is running when the match ends. The window tells you if the replays folder is missing or empty.

**Are ammo boxes counted?** No. Ammo boxes, food boxes and loot cannons are listed separately in the report and are not part of the chest total. Chests you **destroy** instead of opening are counted. A chest that is refilled during the match and that you open again counts once more; coming back near a chest you already opened (dug-up buried chest, relic chest) does not count it twice.

**Creative modes?** Only Battle Royale matches are counted; other modes are skipped.

**Fortnite closed in the middle of a match.** Its replay is incomplete (Fortnite never wrote the decryption key): that match cannot be counted, and the tool marks it as an "incomplete replay".

**"Cannot read this replay".** A Fortnite update can change the replay format; the parser then needs an update. Matches already counted are kept. Please [open an issue](../../issues) and attach `errors.log` (in `%APPDATA%\ChestCounter`).

**Already open?** Only one counter can run at a time: launching it again just tells you it is already open.

**Can a match be counted twice?** No, even if you rename or save the replay.

## Command line options

```
ChestCounter.exe --lang <fr|en|es>   change the language
ChestCounter.exe --demos <folder>    other replays folder
ChestCounter.exe --data <folder>     other data folder
ChestCounter.exe --once              analyze the finished replays, then quit
ChestCounter.exe --reset-config      run the setup again
ChestCounter.exe --overlay-preview <file.png>   save previews of the overlay, then quit
```

## Building from source

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows x64), then double-click **`build.bat`**: the exe is written to `dist\ChestCounter.exe`. The GitHub Actions workflow builds the exe on every push and attaches it to a release when a `v*` tag is pushed.

## License and credits

[MIT](LICENSE). Replay parsing is based on [FortniteReplayDecompressor](https://github.com/Shiqan/FortniteReplayDecompressor) by Shiqan (MIT), slightly modified: see [`lib/PATCHES.md`](lib/PATCHES.md).

This project is not affiliated with, endorsed by or sponsored by Epic Games. Fortnite is a trademark of Epic Games, Inc.
