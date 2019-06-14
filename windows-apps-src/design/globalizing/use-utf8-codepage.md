---
Description: ???.
title: Use the UTF-8 code page
template: detail.hbs
ms.date: 06/12/2019
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
ms.localizationpriority: medium
---

# Use the UTF-8 code page

Use [UTF-8](http://www.utf-8.com/) character encoding for optimal compatibility between web apps and other non-packaged platforms, minimize localization bugs, and reduce testing overhead.

UTF-8 is the universal code page for internationalization and supports all Unicode code points using 1-4 byte variable-width encoding. It is used pervasively on the web, and competing OS platforms (such as Unix-based systems) operate in UTF-8 by default.

Because Windows operates natively in UTF-16 (WCHAR), code page conversions using [MultiByteToWideChar](https://docs.microsoft.com/windows/desktop/api/stringapiset/nf-stringapiset-multibytetowidechar) and [WideCharToMultiByte](https://docs.microsoft.com/windows/desktop/api/stringapiset/nf-stringapiset-widechartomultibyte) are required. This is a unique burden that Windows places on code that targets multiple platforms. Even more challenging is the Windows concept of an ANSI code page which can differ per region and user configuration and can result in wildly inconsistent behavior when relied upon incorrectly.

Starting with Windows Version 1903 (May 2019 Update), you can force a process to use UTF-8 as the process code page through the appxmanifest for packaged apps, or the fusion manifest for unpackaged apps using the ActiveCodePage property.

> [!NOTE]
> You can declare this property and target/run on earlier Windows builds, but you must handle legacy code page detection and conversion as as usual. With a minimum target version of 19H1, the process code page will always be UTF-8.

Here's an example of a fusion manifest for an unpackaged Win32 app:

``` xaml
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <assemblyIdentity type="win32" name="..." version="6.0.0.0"/>
  <application>
    <windowsSettings>
      <activeCodePage xmlns="http://schemas.microsoft.com/SMI/2019/WindowsSettings">UTF-8</activeCodePage>
    </windowsSettings>
  </application>
</assembly>
```

Here's an example of an appx manifest for a packaged app:

```xaml
<?xml version="1.0" encoding="utf-8"?>
<Package xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
         ...
         xmlns:uap7="http://schemas.microsoft.com/appx/manifest/uap/windows10/7"
         xmlns:uap8="http://schemas.microsoft.com/appx/manifest/uap/windows10/8"
         ...
         IgnorableNamespaces="... uap7 uap8 ...">

  <Applications>
    <Application ...>
      <uap7:Properties>
        <uap8:ActiveCodePage>UTF-8</uap8:ActiveCodePage>
      </uap7:Properties>
    </Application>
  </Applications>
</Package>
```

## Related topics

- [Code pages](https://docs.microsoft.com/windows/desktop/Intl/code-pages)
- [Code Page Identifiers](https://docs.microsoft.com/windows/desktop/Intl/code-page-identifiers)