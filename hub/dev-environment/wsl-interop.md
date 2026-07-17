---
title: WSL interop — Windows and Linux integration
description: Use Windows Subsystem for Linux interop to run Linux commands from Windows, access files across operating systems, and integrate networking.
ms.topic: overview
ms.date: 07/16/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# WSL interop — Windows and Linux integration

Windows Subsystem for Linux (WSL) provides bidirectional interop between Windows and Linux. You can launch Linux commands from Windows processes, run Windows executables from within a Linux shell, share files across both file systems, and forward network connections. This page summarizes the key interop patterns and their trade-offs.

> [!IMPORTANT]
> WSL interop requires that WSL is installed and at least one Linux distribution is registered. Do not assume WSL is present — always check before calling `wsl.exe` or accessing `\\wsl$\` paths. On systems where WSL is not installed, `wsl.exe` returns a non-zero exit code with an error message, and `\\wsl$\` UNC paths fail to resolve.

## Command-line interop

### Run Linux commands from Windows

Use `wsl.exe` from any Windows shell (PowerShell, Command Prompt, or Windows Terminal) to execute Linux commands:

```powershell
# Run a single command in the default distribution
wsl ls -la /home

# Run a command in a specific distribution
wsl -d Ubuntu-22.04 -- cat /etc/os-release

# Pipe Windows output into a Linux command
ipconfig | wsl grep "IPv4"
```

### Run Windows executables from Linux

From within a WSL shell, call any Windows executable by its full name (including the `.exe` extension):

```bash
# Open File Explorer in the current Linux directory
explorer.exe .

# Run a PowerShell command from Linux
powershell.exe -Command "Get-Date"

# Pipe Linux output into a Windows command
cat /etc/hosts | clip.exe
```

> [!NOTE]
> Windows executables called from WSL use the Windows PATH. The `.exe` extension is required — without it, the shell looks for a Linux binary.

### Detect whether WSL is available

Before relying on WSL interop in your app or script, check availability:

```powershell
# PowerShell: Check if wsl.exe exists and WSL is functional
try {
    $wslStatus = wsl --status 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host "WSL is available"
    } else {
        Write-Host "WSL is installed but not functional"
    }
} catch {
    Write-Host "WSL is not installed"
}
```

```csharp
// C#: Check WSL availability before use
var wslPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.System), "wsl.exe");

if (!File.Exists(wslPath))
{
    // WSL is not installed — handle gracefully
    return;
}

var process = Process.Start(new ProcessStartInfo
{
    FileName = wslPath,
    Arguments = "--status",
    RedirectStandardOutput = true,
    UseShellExecute = false,
    CreateNoWindow = true
}) ?? throw new InvalidOperationException("Failed to start wsl.exe process.");

process.WaitForExit();
if (process.ExitCode != 0)
{
    // WSL is installed but no distribution is registered
}
```

## File system interop

### Access Linux files from Windows

Use the `\\wsl$\` (or `\\wsl.localhost\`) UNC path to access Linux file systems from Windows:

```text
\\wsl$\Ubuntu-22.04\home\username\project
\\wsl.localhost\Ubuntu-22.04\home\username\project
```

> [!IMPORTANT]
> The `\\wsl$\` path is only available when the WSL instance is running. If the distribution is shut down, the path does not resolve. Start the distribution first with `wsl -d <distro-name>` or use `wsl.localhost` which can auto-start the distribution on Windows 11.

### Access Windows files from Linux

Windows drives are mounted under `/mnt/` by default:

```bash
# Access C: drive
ls /mnt/c/Users/username/Documents

# Access D: drive
ls /mnt/d/projects
```

### Performance considerations

| Operation | Performance | Recommendation |
|---|---|---|
| Linux process reading Linux files (`/home/...`) | Fast (native ext4) | Keep project files here for Linux-based tools |
| Windows process reading Windows files (`C:\...`) | Fast (native NTFS) | Keep project files here for Windows-based tools |
| Linux process reading Windows files (`/mnt/c/...`) | Slow (cross-filesystem 9P protocol) | Avoid for build systems, `node_modules`, git repos |
| Windows process reading Linux files (`\\wsl$\...`) | Slow (cross-filesystem 9P protocol) | Use for occasional file access, not tight loops |

> [!TIP]
> **The most common performance mistake** is storing project files on the Windows file system (`/mnt/c/...`) while running Linux build tools (npm, cargo, make). This causes significant I/O overhead. Instead, clone repositories into the Linux file system (`~/projects/`) when you primarily use Linux tools.

### Path translation

Use `wslpath` inside WSL to convert between Windows and Linux paths:

```bash
# Windows path → Linux path
wslpath "C:\Users\username\file.txt"
# Output: /mnt/c/Users/username/file.txt

# Linux path → Windows path
wslpath -w /home/username/file.txt
# Output: \\wsl.localhost\Ubuntu-22.04\home\username\file.txt

# Linux path → Windows path (with drive letter format)
wslpath -m /mnt/c/Users/username/file.txt
# Output: C:/Users/username/file.txt
```

## Networking interop

### Localhost forwarding

By default, WSL 2 forwards ports from the Linux guest to `localhost` on Windows. A server running on port 3000 inside WSL is accessible from Windows at `http://localhost:3000`.

> [!NOTE]
> Localhost forwarding works from Windows to WSL automatically on most Windows versions. However, connecting from WSL to a Windows-hosted server requires the Windows host IP (find it with `cat /etc/resolv.conf | grep nameserver` in older WSL configs, or use `localhost` directly if you have mirrored networking enabled).

### Mirrored networking mode (Windows 11)

Windows 11 (version 22H2 and later) supports **mirrored networking**, where WSL shares the same network stack as Windows. Enable it in `.wslconfig`:

```ini
# %USERPROFILE%\.wslconfig
[wsl2]
networkingMode=mirrored
```

With mirrored mode:
- WSL and Windows share the same IP address
- Both can reach services on `localhost` bidirectionally
- VPN connections on Windows are automatically available in WSL
- IPv6 works inside WSL

## Environment variable sharing

Pass Windows environment variables to WSL using the `WSLENV` variable:

```powershell
# Windows: Share GOPATH with WSL (translate the path)
$env:WSLENV = "GOPATH/p"
$env:GOPATH = "C:\Users\username\go"
wsl echo $GOPATH
# Output: /mnt/c/Users/username/go
```

The `/p` flag translates Windows paths to Linux format. Other flags: `/l` (colon-separated list), `/u` (only WSL→Windows), `/w` (only Windows→WSL).

## Common mistakes

> [!TIP]
> **Mistakes frequently produced by LLMs and code generators:**
>
> 1. **Assuming `\\wsl$\` is always accessible** — the path only resolves while the target WSL distribution is running. Code that opens `\\wsl$\` paths without starting the distribution first fails with a network path error.
> 2. **Not handling WSL-not-installed** — many machines (especially Windows Server or enterprise-managed PCs) do not have WSL. Always check for `wsl.exe` existence before use.
> 3. **Storing project files on the wrong file system** — using `/mnt/c/` for Linux projects or `\\wsl$\` for Windows projects causes severe I/O performance degradation due to 9P filesystem translation.
> 4. **Hardcoding `/mnt/c/`** — the mount point is configurable in `/etc/wsl.conf` (via `[automount] root`). Use `wslpath` for reliable path translation.
> 5. **Forgetting `.exe` when calling Windows programs from WSL** — `notepad` won't work; you need `notepad.exe`. This is a common copy-paste error from Windows-only documentation.
> 6. **Assuming network topology** — WSL 2 default (NAT) and mirrored modes have different network behaviors. Code that works on one developer's machine may fail on another depending on their `.wslconfig` settings.
> 7. **Using WSL 1 assumptions for WSL 2** — WSL 1 shared the Windows network stack and had direct file system access. WSL 2 uses a lightweight VM with a virtual network adapter and 9P file sharing. Performance characteristics and networking are different.

## Related content

- [WSL documentation](/windows/wsl/)
- [WSL basic commands](/windows/wsl/basic-commands)
- [Advanced settings configuration in WSL](/windows/wsl/wsl-config)
- [Networking considerations with WSL](/windows/wsl/networking)
- [File system access across WSL and Windows](/windows/wsl/filesystems)
- [Set up Node.js on WSL](javascript/nodejs-on-wsl.md)
