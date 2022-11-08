---
title: Xbox Device Portal REST API
description: See a list of reference topics for the Xbox Device Portal REST API, used to remotely configure and manage your console.
ms.date: 10/25/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 5ae8e953-0465-487b-81dd-54a85c904daf
ms.localizationpriority: medium
---
# Xbox Device Portal REST API

This section contains reference topics for the Xbox Device Portal REST API, used to remotely configure and manage your console.

| URI        | Description |
|------------|-------------|
|[/api/app/packagemanager/register](wdp-loose-folder-register-api.md)| Registers an app that is contained in a loose folder. |
|[/api/app/packagemanager/upload](wdp-folder-upload.md)| Uploads an entire folder to the console. |
|[/ext/app/sshpins](uwp-sshpins-api.md)| Clear all trusted SSH pins remotely. Will require doing pin pairing again for Visual Studio UWP development. |
|[/ext/app/deployinfo](uwp-deployinfo-api.md)| Requests deployment information for one or more installed packages. |
|[/ext/fiddler](wdp-fiddler-api.md)| Enable and disable Fiddler network tracing. |
|[/ext/httpmonitor/sessions](wdp-httpMonitor-api.md)| Get HTTP traffic from the focused app on Xbox. |
|[/ext/networkcredential](uwp-networkcredentials-api.md)| Add, remove, or update network credentials. |
|[/ext/remoteinput](uwp-remoteinput-api.md)| Send keyboard, mouse, or controller input remotely to an Xbox. |
|[/ext/remoteinput/controllers](uwp-remoteinput-controllers-api.md)| Get the number of attached physical controllers or turn off all physical controllers. |
|[/ext/screenshot](wdp-media-capture-api.md)| Captures a PNG representation of the screen currently displayed on the console. |
|[/ext/settings](wdp-xboxsettings-api.md)| Accesses Xbox One developer settings. |
|[/ext/smb/developerfolder](wdp-smb-api.md)| Accesses the developer folder on your console through File Explorer on your development PC. |
|[/ext/user](wdp-user-management.md)| Manages users on the Xbox One console. |
|[/ext/xbox/info](wdp-xboxinfo-api.md)| Gives information about the Xbox One device. |
|[/ext/xboxlive/sandbox](wdp-sandbox-api.md)| Manages your Xbox Live sandbox. |

## See also

- [UWP on Xbox One](index.md)
- [Windows Device Portal](../debug-test-perf/device-portal.md)