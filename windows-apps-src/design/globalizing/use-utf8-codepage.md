---
Description: Use UTF-8 character encoding for optimal compatibility between web apps and other *nix-based platforms (Unix, Linux, and variants), minimize localization bugs, and reduce testing overhead.
title: Use the UTF-8 code page
template: detail.hbs
ms.date: 06/12/2019
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
ms.localizationpriority: medium
---

# Use the UTF-8 code page

Use [UTF-8](http://www.utf-8.com/) character encoding for optimal compatibility between web apps and other *nix-based platforms (Unix, Linux, and variants), minimize localization bugs, and reduce testing overhead.

UTF-8 is the universal code page for internationalization and supports all Unicode code points using 1-4 byte variable-width encoding. It is used pervasively on the web, and competing OS platforms (such as Unix-based systems) operate in UTF-8 by default.

Because Windows operates natively in UTF-16 (WCHAR), code page conversions using [MultiByteToWideChar](https://docs.microsoft.com/windows/desktop/api/stringapiset/nf-stringapiset-multibytetowidechar) and [WideCharToMultiByte](https://docs.microsoft.com/windows/desktop/api/stringapiset/nf-stringapiset-widechartomultibyte) are required. This is a unique burden that Windows places on code that targets multiple platforms. Even more challenging is the Windows concept of an ANSI code page which can differ per region and user configuration and can result in wildly inconsistent behavior when relied upon incorrectly.

## -A vs. -W APIs
  
Win32 APIs often offer -A and -W variants. -A variants honor the ANSI code page configured on the system and deal with char*. -W variants operate in UTF-16 and deal with ```WCHAR```.

Until recently, -A APIs were considered legacy as Windows has been pushing "Unicode" -W variants for decades. In recent releases though, Windows has leveraged the ANSI code page and -A APIs as a means to introduce UTF-8 support on newer SKUs. If the ANSI code page is configured to UTF-8, -A APIs will operate in UTF-8. This model has the benefit of supporting existing code built with -A APIs without any code changes.

With Windows Version 1903 (May 2019 Update), UTF-8 is the default and only ANSI code page on console, so -A APIs on console will always operate in UTF-8 and are the recommended APIs for optimal portability. -W APIs are also provided for source compatibility with existing Windows code.

## MultiByteToWideChar/WideCharToMultiByte

[MultiByteToWideChar](https://docs.microsoft.com/windows/desktop/api/stringapiset/nf-stringapiset-multibytetowidechar) and [WideCharToMultiByte](https://docs.microsoft.com/windows/desktop/api/stringapiset/nf-stringapiset-widechartomultibyte) let you convert between UTF-8 and UTF-16 (WCHAR) (and other code pages). This is particularly useful when a legacy Win32 API might only understand WCHAR. These functions allow you to convert UTF-8 input to WCHAR to pass into a -W API and then convert any results back if necessary.
When using these functions in Windows, use CodePage CP_UTF8 with dwFlags of either 0 or MB_ERR_INVALID_CHARS, otherwise you will receive error ERROR_INVALID_FLAGS.

Note: CP_ACP equates to CP_UTF8 only if running on Windows Version 1903 (May 2019 Update) and the ActiveCodePage property described above is set to UTF-8. Otherwise, it honors the legacy system code page. We recommend using CP_UTF8 explicitly.

## App manifest

You can force a process to use UTF-8 as the process code page through the appxmanifest for packaged apps, or the fusion manifest for unpackaged apps using the ActiveCodePage property.

You can declare this property and target/run on earlier Windows builds, but you must handle legacy code page detection and conversion as usual (with a minimum target version of 19H1, the process code page will always be UTF-8).

> [!NOTE]
> Add a manifest to an existing executable from the command line with `mt.exe -manifest <MANIFEST> -outputresource:<EXE>;#1`

## Examples

**Appx manifest for a packaged app:**

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

**Fusion manifest for an unpackaged Win32 app:**

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

## Related topics

- [Code pages](https://docs.microsoft.com/windows/desktop/Intl/code-pages)
- [Code Page Identifiers](https://docs.microsoft.com/windows/desktop/Intl/code-page-identifiers)