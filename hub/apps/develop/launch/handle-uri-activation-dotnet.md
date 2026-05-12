---
title: Handle URI activation with a WPF or Windows Forms app
description: Learn how to register a .NET app to become the default handler for a Uniform Resource Identifier (URI) scheme name.
ms.date: 02/11/2025
ms.topic: how-to
keywords: windows 10, windows 11
ms.localizationpriority: medium
# customer-intent: As a .NET developer, I want to learn how to register a Windows app to become the default handler for a Uniform Resource Identifier (URI) scheme name.
---

# Handle URI protocol activation in a .NET app

Protocol activation (also called *deep linking* or *URI activation*) lets another app, a browser, or the command line launch your app by navigating to a URI such as `myapp://action?param=value`.

This article shows code specifically for a WPF app. For complete guidance, see the main [Handle URI activation](handle-uri-activation.md) article. For full details on rich activation with the Windows App SDK, see [Rich activation with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md).

## Register for protocol activation

You must register your app to handle protocol activation. For an unpackaged app, you register in code. For a packaged app, you register in the app manifest.

### Unpackaged app

For an unpackaged .NET app (the default WPF/WinForms setup), register your protocol at startup using [ActivationRegistrationManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.activationregistrationmanager). Registrations are per-user and persist, so it's safe to call this on every launch.

In `App.xaml.cs`, override `OnStartup`:

```csharp
using Microsoft.Windows.AppLifecycle;

protected override void OnStartup(StartupEventArgs e)
{
    // Register the URI scheme "myapp://" for this app.
    // For the logo, pass the exe path + resource index (or "" to use the default icon).
    string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? "";
    string logo = string.IsNullOrEmpty(exePath) ? "" : exePath + ",0";
    ActivationRegistrationManager.RegisterForProtocolActivation(
        "myapp",                // URI scheme (no "://")
        logo,                   // logo: exe path + resource index, or "" for default icon
        "My App",               // display name for the protocol
        exePath);               // path of this EXE; pass "" to default to the current process

    base.OnStartup(e);
}
```

To clean up the registration (for example, in an uninstall step), call `ActivationRegistrationManager.UnregisterForProtocolActivation("myapp", "")`.

### Packaged app

For a packaged .NET app, declare the protocol in `Package.appxmanifest` under the `<Applications><Application>` element:

```xml
<Applications>
  <Application ...>
    <Extensions>
      <uap:Extension Category="windows.protocol">
        <uap:Protocol Name="myapp">
          <uap:DisplayName>My App</uap:DisplayName>
        </uap:Protocol>
      </uap:Extension>
    </Extensions>
  </Application>
</Applications>
```

Make sure the `uap` XML namespace is declared on the `Package` element: `xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"`.

## Handle the activation

Retrieve the activation arguments using [AppInstance.GetCurrent().GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs). The following example includes code for an unpackaged WPF app, which calls `RegisterForProtocolActivation` at startup. Packaged apps receive activation through the manifest registration, so they can skip the `RegisterForProtocolActivation` call.

```csharp
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel.Activation;

protected override void OnStartup(StartupEventArgs e)
{
    // Unpackaged apps only: register the protocol at startup.
    // Packaged apps (MSIX): skip these lines — the manifest handles registration.
    string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? "";
    string logo = string.IsNullOrEmpty(exePath) ? "" : exePath + ",0";
    ActivationRegistrationManager.RegisterForProtocolActivation(
        "myapp", logo, "My App", exePath);

    // Get the activation args for this specific launch.
    AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
    if (args?.Kind == ExtendedActivationKind.Protocol)
    {
        var protocolArgs = (ProtocolActivatedEventArgs)args.Data;
        HandleProtocolActivation(protocolArgs.Uri);
    }

    base.OnStartup(e);
}

private void HandleProtocolActivation(Uri uri)
{
    // Navigate to or open content based on uri.AbsolutePath or uri.Query.
}
```

> [!NOTE]
> WPF and Windows Forms apps *must* call `AppInstance.GetCurrent().GetActivatedEventArgs()` to retrieve URI activation data. Unlike C++ Win32 apps, .NET apps don't receive activation arguments through a startup entry-point parameter.

## Handle single-instance redirection

If your app should run only one instance at a time, use `AppInstance.FindOrRegisterForKey` to redirect subsequent URI launches to the running instance:

```csharp
protected override void OnStartup(StartupEventArgs e)
{
    string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? "";
    string logo = string.IsNullOrEmpty(exePath) ? "" : exePath + ",0";
    ActivationRegistrationManager.RegisterForProtocolActivation(
        "myapp", logo, "My App", exePath);

    // Try to claim the "main" key. If another instance already has it, redirect and exit.
    AppInstance currentInstance = AppInstance.FindOrRegisterForKey("main");
    if (!currentInstance.IsCurrent)
    {
        var activationArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
        // Run the async redirect on a thread-pool thread to avoid a potential deadlock
        // with the WPF SynchronizationContext. Signal completion via an event so that
        // this code path exits cleanly without re-entering the STA message pump.
        var redirectCompleted = new System.Threading.ManualResetEventSlim(false);
        System.Threading.Tasks.Task.Run(async () =>
        {
            await currentInstance.RedirectActivationToAsync(activationArgs);
            redirectCompleted.Set();
        });
        redirectCompleted.Wait();
        Shutdown();
        return;
    }

    // This is the first instance. Subscribe to future activations.
    currentInstance.Activated += OnActivated;
    base.OnStartup(e);
}

private void OnActivated(object sender, AppActivationArguments args)
{
    Dispatcher.Invoke(() =>
    {
        if (args.Kind == ExtendedActivationKind.Protocol)
        {
            var protocolArgs = (ProtocolActivatedEventArgs)args.Data;
            HandleProtocolActivation(protocolArgs.Uri);
        }
        MainWindow?.Activate();
    });
}
```

For more information about app instancing, see [App instancing with the app lifecycle API](/windows/apps/windows-app-sdk/applifecycle/applifecycle-instancing).


## Related content

- [Rich activation with the app lifecycle API](/windows/apps/windows-app-sdk/applifecycle/applifecycle-rich-activation)
- [Handle URI activation](handle-uri-activation.md)
- [Handle file activation](handle-file-activation.md)
- [Default Programs](/windows/desktop/shell/default-programs)
- [UWP Association launching sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/AssociationLaunching)
