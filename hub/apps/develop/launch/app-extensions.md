---
title: Create app extensions for Windows App SDK apps
description: Learn how to create and consume app extensions to make your Windows App SDK desktop app extensible with add-ins from other packages.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Create app extensions for Windows App SDK apps

App extensions let your app host add-in content and functionality from other packages. This is the Windows equivalent of a plug-in model. An extension provider declares content or services, and your host app discovers and loads them at runtime.

> [!IMPORTANT]
> App extensions require **MSIX package identity** for both the host and extension apps. The extensions are discovered through the package catalog.

## How app extensions work

The app extension model has two roles:

- **Extension host** — Your app declares which extension types it accepts and discovers installed extensions.
- **Extension provider** — Another app (or the same app) declares that it provides an extension of a particular type.

Extensions communicate through the package manifest declarations, not through code. The host app uses the [AppExtensionCatalog](/uwp/api/windows.applicationmodel.appextensions.appextensioncatalog) API to find and manage installed extensions.

## Declare an extension host

Add an `AppExtensionHost` declaration to your app's `Package.appxmanifest`:

```xml
<Package xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3" ...>
  <Applications>
    <Application ...>
      <Extensions>
        <uap3:Extension Category="windows.appExtensionHost">
          <uap3:AppExtensionHost>
            <uap3:Name>com.example.myapp.extensions</uap3:Name>
          </uap3:AppExtensionHost>
        </uap3:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

## Declare an extension provider

In the extension app's `Package.appxmanifest`:

```xml
<Package xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3" ...>
  <Applications>
    <Application ...>
      <Extensions>
        <uap3:Extension Category="windows.appExtension">
          <uap3:AppExtension Name="com.example.myapp.extensions"
                             Id="MathExtension"
                             DisplayName="Math Functions"
                             Description="Provides math operations"
                             PublicFolder="Public">
          </uap3:AppExtension>
        </uap3:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

The `PublicFolder` points to a folder in the extension package that the host app can read.

## Discover and load extensions

In your host app, use `AppExtensionCatalog` to discover installed extensions:

```csharp
private AppExtensionCatalog? _catalog;

private async Task InitializeExtensionsAsync()
{
    _catalog = AppExtensionCatalog.Open("com.example.myapp.extensions");

    _catalog.PackageInstalled += OnExtensionInstalled;
    _catalog.PackageUpdated += OnExtensionUpdated;
    _catalog.PackageUninstalling += OnExtensionRemoved;

    var extensions = await _catalog.FindAllAsync();
    foreach (var extension in extensions)
    {
        await LoadExtensionAsync(extension);
    }
}

private async Task LoadExtensionAsync(AppExtension extension)
{
    string extensionId = extension.Id;
    string displayName = extension.DisplayName;

    // Get the extension's public folder
    var folder = await extension.GetPublicFolderAsync();
    if (folder != null)
    {
        // Read configuration, content, or scripts from the folder
        var configFile = await folder.TryGetItemAsync("config.json");
        if (configFile != null)
        {
            // Parse and use the extension configuration
        }
    }
}

// See "Handle extension lifecycle events" below for the
// OnExtensionInstalled, OnExtensionUpdated, and OnExtensionRemoved handlers.
private async void OnExtensionInstalled(AppExtensionCatalog sender,
    AppExtensionPackageInstalledEventArgs args) { }
private async void OnExtensionUpdated(AppExtensionCatalog sender,
    AppExtensionPackageUpdatedEventArgs args) { }
private void OnExtensionRemoved(AppExtensionCatalog sender,
    AppExtensionPackageUninstallingEventArgs args) { }
```

## Handle extension lifecycle events

```csharp
private async Task LoadExtensionAsync(AppExtension extension) => await Task.CompletedTask;
// (See "Discover and load extensions" above for the real implementation.)

private async void OnExtensionInstalled(AppExtensionCatalog sender,
    AppExtensionPackageInstalledEventArgs args)
{
    // Re-query the catalog to get the AppExtension instances added by this package
    var extensions = await sender.FindAllAsync();
    foreach (var extension in extensions.Where(e => e.Package.Id.FamilyName == args.Package.Id.FamilyName))
    {
        await LoadExtensionAsync(extension);
    }
}

private void OnExtensionRemoved(AppExtensionCatalog sender,
    AppExtensionPackageUninstallingEventArgs args)
{
    // args.Package identifies the package that is uninstalling
    string familyName = args.Package.Id.FamilyName;
    // Remove any extensions from that package from your app
}

private async void OnExtensionUpdated(AppExtensionCatalog sender,
    AppExtensionPackageUpdatedEventArgs args)
{
    // Re-query the catalog to get the refreshed AppExtension instances
    var extensions = await sender.FindAllAsync();
    foreach (var extension in extensions.Where(e => e.Package.Id.FamilyName == args.Package.Id.FamilyName))
    {
        // Reload with updated content
        await LoadExtensionAsync(extension);
    }
}
```

## Related content

- [Package extensions](package-extensions.md)
- [Extensibility overview](extensibility-overview.md)
- [App services](app-services.md)
- [AppExtensionCatalog class](/uwp/api/windows.applicationmodel.appextensions.appextensioncatalog)
