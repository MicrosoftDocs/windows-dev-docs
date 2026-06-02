---
title: tar on Windows
description: Use the built-in tar command on Windows to create and extract .tar, .tar.gz, and .zip archives from the command line.
ms.topic: article
ms.date: 05/19/2026
---

# tar on Windows

**tar** is a command-line archiving tool that's included with Windows. It lets you create, list, and extract archive files — including `.tar`, `.tar.gz`, `.zip`, and `.7z` — directly from the command line, without installing additional software.

The Windows build of `tar` is based on [libarchive](https://www.libarchive.org/)'s `bsdtar` and supports the same flags developers already use on Linux and macOS, so existing scripts and habits carry over.

## Common commands

Extract an archive:

```powershell
tar -xf archive.tar.gz
```

Create a `.tar.gz` from a folder:

```powershell
tar -czf archive.tar.gz .\my-folder
```

Create a `.zip` from a folder (the `-a` flag auto-selects the archive format from the file extension, replacing format-specific flags like `-z`, `-j`, `-J`, and `-Z`):

```powershell
tar -caf archive.zip .\my-folder
```

List the contents of an archive without extracting:

```powershell
tar -tf archive.tar.gz
```

For the full list of options, run `tar --help` or the [FreeBSD Tar manual](https://man.freebsd.org/cgi/man.cgi?query=bsdtar&sektion=1&format=html).
