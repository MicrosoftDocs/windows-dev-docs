---
title: Use UTF-8 code pages in Windows apps
description: How to use UTF code pages in Windows apps.
ms.date: 06/21/2023
ms.topic: article
---

# Use UTF-8 code pages in Windows apps

Use [UTF-8](http://www.utf-8.com/) character encoding for optimal compatibility between web apps and other \*nix-based platforms (Unix, Linux, and variants), minimize localization bugs, and reduce testing overhead.

UTF-8 is the universal code page for internationalization and is able to encode the entire Unicode character set. It is used pervasively on the web, and is the default for *nix-based platforms.

## Set a process code page to UTF-8

As of Windows Version 1903 (May 2019 Update), you can use the ActiveCodePage property in the appxmanifest for packaged apps, or the fusion manifest for unpackaged apps, to force a process to use UTF-8 as the process code page.

> [!NOTE]
> GDI doesn't currently support setting the ActiveCodePage property per process. Instead, GDI defaults to the active system codepage. To configure your app to render UTF-8 text via GDI, go to Windows **Settings** > **Time \& language** > **Language \& region** > **Administrative language settings** > **Change system locale**, and check **Beta: Use Unicode UTF-8 for worldwide language support**. Then reboot the PC for the change to take effect.

You can declare the ActiveCodePage property, and target/run on earlier Windows builds, but you must handle legacy code page detection and conversion as usual. With a minimum target version of Windows Version 1903, the process code page will always be UTF-8, so legacy code page detection and conversion can be avoided.

> [!NOTE]
> In UTF-8, an encoded character is represented by a sequence of 1 to 4 bytes. (See definition D92 in Chapter 3 of [The Unicode Standard](https://www.unicode.org/versions/latest/) for the formal specification.)

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

> [!NOTE]
> Add a manifest to an existing executable from the command line with `mt.exe -manifest <MANIFEST> -outputresource:<EXE>;#1`.

## -A vs. -W APIs
  
Win32 APIs often support both -A and -W variants.

-A variants recognize the ANSI code page configured on the system and support `char*`, while -W variants operate in UTF-16 and support `WCHAR`.

Until recently, Windows has emphasized "Unicode" -W variants over -A APIs. However, recent releases have used the ANSI code page and -A APIs as a means to introduce UTF-8 support to apps. If the ANSI code page is configured for UTF-8, then -A APIs typically operate in UTF-8. This model has the benefit of supporting existing code built with -A APIs without any code changes.

## Code page conversion

Because Windows operates natively in UTF-16 (`WCHAR`), you might need to convert UTF-8 data to UTF-16 (or vice versa) to interoperate with Windows APIs.

[MultiByteToWideChar](/windows/desktop/api/stringapiset/nf-stringapiset-multibytetowidechar) and [WideCharToMultiByte](/windows/desktop/api/stringapiset/nf-stringapiset-widechartomultibyte) let you convert between UTF-8 and UTF-16 (`WCHAR`) (and other code pages). This is particularly useful when a legacy Win32 API might only understand `WCHAR`. These functions allow you to convert UTF-8 input to `WCHAR` to pass into a -W API and then convert any results back if necessary.

Use `dwFlags` of either `0` or `MB_ERR_INVALID_CHARS` when using these functions with `CodePage` set to `CP_UTF8` (otherwise an `ERROR_INVALID_FLAGS` occurs).

> [!NOTE]
> `CP_ACP` equates to `CP_UTF8` only if running on Windows Version 1903 (May 2019 Update) or above and the ActiveCodePage property described above is set to UTF-8. Otherwise, it honors the legacy system code page. We recommend using `CP_UTF8` explicitly.

## Related topics

- [Code pages](/windows/desktop/Intl/code-pages)
- [Code Page Identifiers](/windows/desktop/Intl/code-page-identifiers)
