---
title: curl on Windows
description: Use the built-in curl command on Windows to transfer data to and from servers over HTTP, HTTPS, FTP, and other protocols from the command line.
ms.topic: article
ms.date: 05/19/2026
---

# curl on Windows

**curl** is a command-line tool for transferring data to and from a server. It's included with Windows and supports a wide range of protocols including HTTP, HTTPS, FTP, and SFTP, making it a convenient way to call REST APIs, download files, and test endpoints without installing extra tools.

The Windows version is built from the upstream [curl](https://curl.se/) project, so the same flags and behavior you know from Linux and macOS work the same way on Windows.

> [!NOTE]
> Windows PowerShell 5.1 defines a built-in alias named `curl` that maps to `Invoke-WebRequest`, which shadows `curl.exe` and accepts different parameters. To use the real curl in Windows PowerShell 5.1, either remove the alias with `Remove-Item Alias:curl` or invoke it explicitly as `curl.exe` (for example, `curl.exe -O https://example.com/file.zip`). PowerShell 7+ does not define this alias.

## Common commands

Download a file:

```powershell
curl -O https://example.com/file.zip
```

Make a GET request and print the response:

```powershell
curl https://api.example.com/data
```

Send a JSON POST request:

```powershell
curl -X POST https://api.example.com/items `
    -H "Content-Type: application/json" `
    -d '{"name":"widget"}'
```

For the full list of options, run `curl --help` or see the [official documentation](https://curl.se/docs/).
