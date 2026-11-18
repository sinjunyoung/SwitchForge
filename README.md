# SwitchForge

Nintendo Switch ROM Packaging Utility

SwitchForge is a Windows desktop tool for unpacking, repacking, merging, converting and compressing Nintendo Switch titles. It was split out from RomForge.
* [RomForge](https://github.com/sinjunyoung/RomForge)

<img width="850" height="684" alt="image" src="https://github.com/user-attachments/assets/0bdf4bcf-16a2-48f1-8790-7401cbea777f" />


## Features

SwitchForge is organized into tabs, one per task. Every tool accepts **NSP, NSZ, XCI and XCZ** and needs `prod.keys` (see Requirements).

### Repack

Unpacks titles and rebuilds them as a new package, with support for patches, custom metadata and multiple titles at once.

* Drag and drop base games, updates and DLC into the title list. One base and one update are kept per title (the one added last replaces the earlier one), while DLC keeps accumulating. A file that bundles a base game and DLC is split into separate rows automatically.
* **Patches** can be assigned per title, or in bulk by Title ID. Supported patch types:
  * LayeredFS patches (`romfs` / `exefs` folders) that replace original files
  * xdelta patches, matched to the target file in `romfs` by file name
  * IPS / IPS32 patches in `exefs_patches`, matched by NSO module ID (the NSO is decompressed, patched and recompressed automatically)
* A patch can be a folder or a zip / 7z archive, including password-protected archives. The base game and update share the same patch automatically.
* **Per-title settings**: enabled languages (or force a single language), cover image, title name, publisher, Title ID, and multi-ROM index for packages that contain several games.
* **Lz4** column: choose per title whether romfs is Lz4-compressed on repack.
* **Unpack**, **Repack** and **Unpack + Repack (Full)** buttons with a work folder. Repack can be run on its own from a previously unpacked folder.

### Merge

Merges base game, update and DLC into a single file.

* **Group by Title ID** merges each game into its own file, so files from many games can be added at once.
* **All** merges everything in the list into one file.
* Outdated update content is dropped, so only the latest update remains in the result.
* Works even when only an update or DLC is available.
* The output is NSP, NSZ, XCI or XCZ depending on the selected format and compression settings.

### Split

The reverse of merge: splits a combined file into separate base game, update and DLC files.

* Expand a combined file in the list and choose which titles to split out.
* Choose the output format (NSP, NSZ, XCI, XCZ).

### Lz4 compression

Merges a base game and update (and DLC) into one file while compressing romfs with Lz4.

* Shows the estimated and the actual compression ratio for each title.
* Files that are already Lz4-compressed are skipped.
* Output as NSP or NSZ.

### Format conversion

Converts files between NSP, NSZ, XCI and XCZ. Every format can be converted into each of the other three, including compressed to compressed (for example NSZ to XCZ) in one step.

| Source | Available targets |
| ------ | ----------------- |
| NSP | XCI, NSZ, XCZ |
| XCI | NSP, NSZ, XCZ |
| NSZ | NSP, XCI, XCZ |
| XCZ | NSP, XCI, NSZ |

* The target format is chosen per file.
* Pressing Start again retries only failed or cancelled items.

### Saturn NSP conversion

Creates a Switch NSP from a Sega Saturn disc image by injecting it into a Saturn emulator forwarder NSP.

* Accepts CUE/BIN or CHD. CHD images are streamed, so no full extraction is needed.
* Game ID, version and name are read from the disc header, and a cover image is downloaded automatically by game ID. Name, publisher and cover can be edited.
* Options for widescreen and sound volume.

### Ignore version

Makes titles that require a newer system version installable, for example with DBI on a console that has not been updated.

* Re-encrypts every NCA with keygen 0 and removes the ticket files.
* The file format stays the same (NSP stays NSP, XCI stays XCI) and the result is written next to the source file.
* It cannot add features the game actually needs from a newer SDK.

### Rename

Renames files in bulk using a format string.

* Build the format with draggable tokens: `Name`, `KTitleName`, `TitleName`, `TitleID`, `Tag`, `DisplayVersion` and `Version`.
* The list shows the current and the new file name for every file, with a status such as will rename, no change, duplicate name, file exists or name too long.

### Settings

* **General** — UI language, always on top, update check on startup, sound effects.
* **Compression** — compression level (3 to 18) and mode (block or blockless) for NSZ / XCZ.
* **Key files** — path to `prod.keys`.

The interface is available in Korean, English, Japanese, Simplified Chinese, Traditional Chinese, French, German, Russian and Spanish.

## Requirements

* Windows x64
* [.NET 10 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)
* `prod.keys` — not included with SwitchForge

## Building

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

```
BUILD.bat          (framework-dependent, multiple files)
BUILD_Single.bat   (single-file)
```

The output is placed in `SwitchForge\bin\x64\Release\net10.0-windows\win-x64\publish`.

The native CHD library is built separately; see `CHD/BUILD.txt`.

## License

This project is licensed under the GNU General Public License v2.0 or later (GPL-2.0-or-later). See the `LICENSE` file for the full license text.

## Third-Party Components

SwitchForge incorporates modified versions of the following open-source software:

| Component | License | Link |
| --------- | ------- | ---- |
| CHDMan (MAME) | GPL-2.0-or-later | https://github.com/mamedev/mame |
| libchdr | BSD-3-Clause | https://github.com/rtissera/libchdr |
| xdelta3 | GPL-2.0 | https://github.com/jmacd/xdelta |
| LibHac | MIT | https://git.ryujinx.app/ryubing/libhac (original: https://github.com/Thealexbarney/LibHac) |
| 7-Zip | LGPL-2.1-or-later | https://www.7-zip.org/ |

The original copyright notices and license terms remain applicable to the respective components.

## Libraries

| Library | Purpose | Link |
| ------- | ------- | ---- |
| ZstdSharp.Port | Zstandard compression (NSZ, etc.) | https://github.com/oleg-st/ZstdSharp |
| K4os.Compression.LZ4 | LZ4 compression | https://github.com/MiloszKrajewski/K4os.Compression.LZ4 |
| Blake3 | BLAKE3 hashing | https://github.com/xoofx/Blake3.NET |
| SharpCompress | Archive reading (patch sources) | https://github.com/adamhathcock/sharpcompress |
| SevenZipSharp (Squid-Box) | 7-Zip wrapper | https://github.com/squid-box/SevenZipSharp |
| System.IO.Hashing | CRC32 and other hashes | https://github.com/dotnet/runtime |
| System.Management | Drive / system information | https://github.com/dotnet/runtime |
| NAudio | Audio playback | https://github.com/naudio/NAudio |
| Markdig | Markdown parsing | https://github.com/xoofx/markdig |
| Markdig.Wpf | Markdown rendering in WPF | https://github.com/Kryptos-FR/markdig.wpf |
| Ookii.Dialogs.Wpf | Native folder / file dialogs | https://github.com/ookii-dialogs/ookii-dialogs-wpf |
| Darnelix.Webp | WebP image decoding | https://www.nuget.org/packages/Darnelix.Webp |
| JetBrains.Annotations | Code annotations | https://github.com/JetBrains/JetBrains.Annotations |

Each library is used under its own license.

## Acknowledgements

* [hacPack](https://github.com/DarkMatterCore/hacPack)
