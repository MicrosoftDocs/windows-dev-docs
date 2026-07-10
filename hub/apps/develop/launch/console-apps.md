---
title: Console apps with Windows App SDK
description: Learn how to create console apps that use Windows App SDK features, and understand the differences from UWP console apps.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Console apps with Windows App SDK

You can create console apps that use Windows App SDK features such as app lifecycle management, rich activation, push notifications, and more. Unlike UWP console apps (which required special templates and C++/WinRT), desktop console apps use standard .NET or C++ project types.

## Create a console app with Windows App SDK

### Step 1: Create a .NET console project

Use the standard .NET console app template:

```console
dotnet new console -n MyConsoleApp
cd MyConsoleApp
```

### Step 2: Add the Windows App SDK NuGet package

```console
dotnet add package Microsoft.WindowsAppSDK
```

### Step 3: Use Windows App SDK APIs

```csharp
using Microsoft.Windows.AppLifecycle;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Console app with Windows App SDK");

        // Check activation kind
        var activatedArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
        Console.WriteLine($"Activation kind: {activatedArgs.Kind}");

        // Use single-instance pattern
        var instance = AppInstance.FindOrRegisterForKey("my-console-app");
        if (!instance.IsCurrent)
        {
            Console.WriteLine("Another instance is running. Redirecting...");
            await instance.RedirectActivationToAsync(activatedArgs);
            return;
        }

        // Your app logic here
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }
}
```

## Package identity for console apps

Some Windows App SDK features require MSIX package identity. You can give your console app package identity in two ways:

- **Package as MSIX** — Create a Windows Application Packaging Project and add your console app as a reference.
- **Use external identity** — Register a sparse package for identity without full MSIX packaging.

> [!NOTE]
> Features like push notifications, background tasks, and app services require package identity. Basic lifecycle and activation APIs work without packaging.

## Differences from UWP console apps

| Feature | UWP console apps | Desktop console apps |
|---------|-----------------|---------------------|
| Language support | C++/WinRT and C++/CX only | C#, C++, and any .NET language |
| Project template | Special UWP console template (VS 2017/2019) | Standard .NET console or Win32 |
| Manifest requirement | `desktop4:Subsystem="console"` | Not required |
| Activation | `AppExecutionAlias` in manifest | Standard command line or rich activation |
| SDK integration | Limited to WinRT APIs | Full Windows App SDK access |

> [!IMPORTANT]
> UWP console apps required special Visual Studio templates that are no longer actively maintained. For new console apps, use a standard .NET console project with the Windows App SDK NuGet package.

## Common scenarios

### Command-line tools with Windows integration

Build CLI tools that integrate with Windows features:

```csharp
using Microsoft.Windows.AppNotifications;

// Send a notification from a console app (requires package identity)
var builder = new AppNotificationBuilder()
    .AddText("Build complete")
    .AddText("Your project built successfully.");

AppNotificationManager.Default.Show(builder.BuildNotification());
```

### Background processing

Console apps work well for background processing tasks:

- Data import/export utilities
- File watchers and processors
- Scheduled task executables
- Developer tools and automation scripts

## Related content

- [App lifecycle for Windows App SDK](app-lifecycle.md)
- [Multi-instance apps](multi-instance-apps.md)
- [Windows App SDK overview](/windows/apps/windows-app-sdk/)
