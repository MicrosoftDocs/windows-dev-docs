---
title: Host WinUI controls with XAML Islands
description: Learn how Windows App SDK XAML Islands let WPF, Windows Forms, and Win32 apps host WinUI controls during incremental modernization.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 09/02/2026
ms.localizationpriority: medium
---

# Host WinUI controls with XAML Islands

WinUI XAML Islands let you host controls from the Windows App SDK inside an existing desktop app. You can add a WinUI experience to part of a WPF, Windows Forms, or Win32 app while keeping the rest of its UI and application architecture in place.

XAML Islands support incremental modernization. They work well when a full UI migration isn't practical, but a specific workflow would benefit from WinUI controls, Fluent styling, or another Windows App SDK UI capability.

> [!IMPORTANT]
> WinUI XAML Islands use the `Microsoft.UI.Xaml` APIs in the Windows App SDK. They aren't the same as legacy *system XAML Islands*, which use `Windows.UI.Xaml` to host UWP controls. Don't combine the APIs or setup instructions from the two implementations.

## Decide whether to use XAML Islands

Use WinUI XAML Islands when:

- You need to preserve a substantial WPF, Windows Forms, or Win32 UI while modernizing one area at a time.
- A new or redesigned surface needs WinUI controls.
- You can manage a native child window, UI-thread initialization, input, focus, and deployment requirements in the host app.

Consider migrating the complete UI to WinUI instead when most screens need to change, or when maintaining two UI frameworks would add more complexity than it removes. For migration options, see [Choose your migration path](../../windows-app-sdk/migrate-to-windows-app-sdk/migration-decision-guide.md).

## Understand the hosting model

The [`DesktopWindowXamlSource`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.hosting.desktopwindowxamlsource) class is the primary Windows App SDK XAML hosting API. It connects a tree of `Microsoft.UI.Xaml` content to a native child window inside your desktop app.

A typical integration has these parts:

1. **Host application**: Your existing WPF, Windows Forms, or Win32 process owns the top-level window and message loop.
1. **Windows App SDK runtime**: The host initializes the runtime according to its packaging and deployment model.
1. **XAML environment**: The host initializes WinUI on its UI thread. It also creates a dispatcher queue on that thread if one doesn't already exist.
1. **Island source**: A `DesktopWindowXamlSource` owns the native child window that displays the WinUI content.
1. **WinUI content**: A `UIElement`, often supplied by a separate WinUI class library, becomes the island's content.

The Windows App SDK XAML hosting API supports WPF, Windows Forms, and Win32 hosts. The details for obtaining the host window handle and integrating with its message loop differ by framework. The maintained [Windows App SDK Islands sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Islands) demonstrates an unpackaged .NET 8 Windows Forms host.

For a managed host, target a .NET version supported by the Windows App SDK release you select. The framework requirements in the legacy system XAML Islands documentation don't describe WinUI XAML Islands.

## Set up the host

Before you create an island:

1. Add the Windows App SDK package and configure its runtime initialization. For an existing project, see [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).
1. Put the XAML controls that you want to host in a WinUI project, such as a WinUI class library, and reference that project from the host.
1. Configure the host UI thread as a single-threaded apartment (STA), create a dispatcher queue on that thread, and then create a `Microsoft.UI.Xaml.Application`.
1. Configure the application to supply XAML metadata and resources. For a C# host, implement `IXamlMetadataProvider`, add `XamlControlsXamlMetaDataProvider` and the generated metadata provider from each WinUI class library, and merge `XamlControlsResources` into the application resources. Without the library's generated provider, creating its XAML controls can throw `XamlParseException`. See the sample's [XamlApp implementation](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Islands/cs-winforms-unpackaged/XamlApp.cs) and [provider registration](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Islands/cs-winforms-unpackaged/Program.cs).
1. Call [`WindowsXamlManager.InitializeForCurrentThread`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.hosting.windowsxamlmanager.initializeforcurrentthread) on the same UI thread before creating the island.
1. Keep all XAML objects on the thread where you initialized XAML. WinUI objects have thread affinity.

The following excerpt shows the core of a custom Windows Forms host control. It assumes that the app has already initialized the Windows App SDK runtime, dispatcher queue, and XAML environment, and that `IslandView` is a WinUI `UserControl` from a referenced WinUI class library.

```csharp
#nullable enable

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.UI;
using Microsoft.UI.Xaml.Hosting;
using Windows.Graphics;

internal sealed class WinUIIslandHost : Control
{
    private DesktopWindowXamlSource? _xamlSource;

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        if (DesignMode ||
            LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        {
            return;
        }

        _xamlSource = new DesktopWindowXamlSource();
        _xamlSource.Initialize(Win32Interop.GetWindowIdFromWindow(Handle));
        _xamlSource.Content = new MyWinUILibrary.IslandView();
        ResizeIsland();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        ResizeIsland();
    }

    private void ResizeIsland()
    {
        _xamlSource?.SiteBridge.MoveAndResize(
            new RectInt32(0, 0, ClientSize.Width, ClientSize.Height));
    }

    protected override void OnHandleDestroyed(EventArgs e)
    {
        ReleaseIsland();
        base.OnHandleDestroyed(e);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ReleaseIsland();
        }

        base.Dispose(disposing);
    }

    private void ReleaseIsland()
    {
        _xamlSource?.Dispose();
        _xamlSource = null;
    }
}
```

This code covers content creation, native parenting, resizing, and cleanup. A production integration must also connect the two frameworks' input and focus behavior.

> [!NOTE]
> `WinUIIslandHost` in this example and `DesktopWindowXamlSourceControl` in the official sample are application-defined helper controls. They aren't Windows App SDK controls.

## Integrate input and focus

The host and island share a thread but use different UI systems. Treat their boundary as an interoperability boundary:

- Call the native `ContentPreTranslateMessage` function for messages before the host dispatches them. This step is required for a host that uses the Windows App SDK UI stack. The function doesn't have a managed projection. The Windows Forms sample [P/Invokes it from Microsoft.UI.Windowing.Core.dll and registers an `IMessageFilter`](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Islands/cs-winforms-unpackaged/WindowsAppSdkHelper.cs).
- Use `DesktopWindowXamlSource.NavigateFocus` when keyboard navigation enters the island.
- Handle `DesktopWindowXamlSource.TakeFocusRequested` when keyboard navigation needs to return to the host.
- Test tab order in both directions, accelerator keys, mnemonics, pointer input, text input, and input method editors.
- Verify accessibility names, relationships, and focus order across the host and island with an accessibility inspection tool.

Input integration is required even when pointer interaction appears to work without it. Incomplete message or focus handling can leave keyboard users unable to enter or leave the island.

## Manage layout and display changes

`DesktopWindowXamlSource.SiteBridge` exposes the native child site used by the island. Resize it whenever the host's client area changes, as shown in the example.

Also test:

- Per-monitor DPI changes and display scaling.
- Host window resize, minimize, restore, and reparenting behavior.
- High contrast, text scaling, and light and dark themes.
- Clipping, scrolling, and z-order interactions with native host content.

Don't assume the host framework's layout engine automatically sizes the WinUI content. The host remains responsible for the island's native bounds.

## Clean up in the correct order

Close or dispose every `DesktopWindowXamlSource` when its host control or window is destroyed. When the process exits, release the islands before you close the XAML manager or shut down the dispatcher queue.

Keep references to the dispatcher queue controller and XAML manager for as long as the islands use them. Shutting down either component early can cause failures during window destruction or final message processing.

## Package and deploy the app

XAML Islands don't require package identity by themselves, but the host must deploy the Windows App SDK runtime and any other dependencies correctly. In a framework-dependent unpackaged app, initialize the runtime before you initialize XAML. A typical .NET host sets `<WindowsPackageType>None</WindowsPackageType>` to enable automatic runtime initialization; use the bootstrapper when you need explicit control.

For deployment requirements and tradeoffs, see [Windows App SDK deployment overview](../../package-and-deploy/deploy-overview.md). Test installation, servicing, and runtime initialization using the same deployment model that you plan to ship.

## Compare current and legacy XAML Islands

| Characteristic | WinUI XAML Islands | Legacy system XAML Islands |
|---|---|---|
| XAML framework | Windows App SDK WinUI | UWP XAML |
| Namespace | `Microsoft.UI.Xaml` | `Windows.UI.Xaml` |
| Primary low-level host | `Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource` | `Windows.UI.Xaml.Hosting.DesktopWindowXamlSource` |
| Content to use for new modernization work | Current Windows App SDK controls | Legacy UWP controls |

If an existing app already uses system XAML Islands, treat moving to WinUI XAML Islands as a migration between technologies, not as a package or namespace substitution. Reassess initialization, content libraries, input integration, and deployment. See [XAML Islands (UWP)](/windows/uwp/xaml-islands/xaml-islands) only when maintaining the legacy implementation.

## Related content

- [Windows App SDK Islands sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Islands)
- [`DesktopWindowXamlSource` API reference](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.hosting.desktopwindowxamlsource)
- [Modernize existing desktop apps](index.md)
- [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
- [Migrate to WinUI](../../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md)
