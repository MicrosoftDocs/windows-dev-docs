---
title: Create package extensions for Windows App SDK apps
description: Learn how to use package extensions to make your Windows App SDK desktop app extensible with content and services from other packages.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Create package extensions for Windows App SDK apps

Package extensions expand the app extension model by allowing broader content sharing between packages. A package extension host can accept extensions from any package, making it suitable for platform-level extensibility scenarios.

> [!NOTE]
> Package extensions require **Windows 11** and use the `uap17` namespace. For an earlier-compatible approach, see [App extensions](app-extensions.md) which use `uap3`.

## Package extensions vs. app extensions

| Feature | App extensions (`uap3`) | Package extensions (`uap17`) |
|---------|------------------------|------------------------------|
| Minimum OS | Windows 10 1607 | Windows 11 |
| Scope | Per-application | Per-package |
| Content sharing | Public folder only | Full package content access |
| Manifest namespace | `uap3:AppExtension` | `uap17:PackageExtension` |

## Declare a package extension host

Add a `PackageExtensionHost` declaration to the host app's manifest:

```xml
<Package xmlns:uap17="http://schemas.microsoft.com/appx/manifest/uap/windows10/17" ...>
  <Applications>
    <Application ...>
      <Extensions>
        <uap17:Extension Category="windows.packageExtensionHost">
          <uap17:PackageExtensionHost>
            <uap17:Name>com.example.myplatform.extensions</uap17:Name>
          </uap17:PackageExtensionHost>
        </uap17:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

## Declare a package extension

In the extension package's manifest:

```xml
<Package xmlns:uap17="http://schemas.microsoft.com/appx/manifest/uap/windows10/17" ...>
  <Applications>
    <Application ...>
      <Extensions>
        <uap17:Extension Category="windows.packageExtension">
          <uap17:PackageExtension Name="com.example.myplatform.extensions"
                                   Id="MyExtension"
                                   DisplayName="My Extension"
                                   Description="Adds features to the platform">
            <uap17:Properties>
              <ExtensionType>content</ExtensionType>
              <Version>1.0</Version>
            </uap17:Properties>
          </uap17:PackageExtension>
        </uap17:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

## Discover package extensions

Use the same [AppExtensionCatalog](/uwp/api/windows.applicationmodel.appextensions.appextensioncatalog) API to discover package extensions at runtime. The host name matches the `Name` declared in the manifest:

```csharp
var catalog = AppExtensionCatalog.Open("com.example.myplatform.extensions");
var extensions = await catalog.FindAllAsync();

foreach (var ext in extensions)
{
    System.Diagnostics.Debug.WriteLine(
        $"Found extension: {ext.DisplayName} ({ext.Id})");

    // Read custom properties
    var properties = await ext.GetExtensionPropertiesAsync();
    // Access properties as needed
}
```

## Best practices

- Use a reverse-domain naming convention for extension names (for example, `com.company.app.extensions`).
- Version your extension protocol so host apps can handle old and new extension formats.
- Validate extension content before using it — extensions come from other packages and should not be blindly trusted.

## Related content

- [App extensions](app-extensions.md)
- [Extensibility overview](extensibility-overview.md)
- [App services](app-services.md)
