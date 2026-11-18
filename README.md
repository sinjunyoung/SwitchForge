# SwitchForge

Nintendo Switch ROM Packaging Utility

SwitchForge is a Windows desktop tool for unpacking, repacking, merging, converting and compressing Nintendo Switch titles. It was split out from RomForge.
* [RomForge](https://github.com/sinjunyoung/RomForge)

<img width="850" height="684" alt="image" src="https://github.com/user-attachments/assets/0bdf4bcf-16a2-48f1-8790-7401cbea777f" />


## Features

* **Repack** — NSP/XCI unpack & repack with file replacement, patch application (IPS, IPS32, BPS, UPS, xdelta) and per-file Lz4 romfs compression
* **Merge** — base + update + DLC merge
* **Split** — base, update, DLC split
* **Format conversion** — NSP / NSZ / XCI / XCZ conversion and compression
* **Saturn conversion** — Sega Saturn ROM NSP conversion
* **Rename** — batch file renaming with a custom format


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
