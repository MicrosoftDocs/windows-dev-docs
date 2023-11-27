---
description: There are several URI (Uniform Resource Identifier) schemes that you can use to refer to files that come from your app's package, your app's data folders, or the cloud. You can also use a URI scheme to refer to strings loaded from your app's Resources Files (.resw).
title: URI schemes
template: detail.hbs
ms.date: 10/16/2017
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# URI schemes

There are several URI (Uniform Resource Identifier) schemes that you can use to refer to files that come from your app's package, your app's data folders, or the cloud. You can also use a URI scheme to refer to strings loaded from your app's Resources Files (.resw). You can use these URI schemes in your code, in your XAML markup, in your app package manifest, or in your tile and toast notification templates.

## Common features of the URI schemes

All of the schemes described in this topic follow typical URI scheme rules for normalization and resource retrieval. See [RFC 3986](https://www.ietf.org/rfc/rfc3986.txt) for the generic syntax of a URI.

All of the URI schemes define the hierarchical part per [RFC 3986](https://www.ietf.org/rfc/rfc3986.txt) as the authority and path components of the URI.

```syntax
URI         = scheme ":" hier-part [ "?" query ] [ "#" fragment ]
hier-part   = "//" authority path-abempty
            / path-absolute
            / path-rootless
            / path-empty
```

What this means is that there are essentially three components to a URI. Immediately following the two forward slashes of the URI *scheme* is a component (which can be empty) called the *authority*. And immediately following that is the *path*. Taking the URI `http://www.contoso.com/welcome.png` as an example, the scheme is "`http://`", the authority is "`www.contoso.com`", and the path is "`/welcome.png`". Another example is the URI `ms-appx:///logo.png`, where the authority components is empty and takes a default value.

The fragment component is ignored by the scheme-specific processing of the URIs mentioned in this topic. During resource retrieval and comparison, the fragment component has no bearing. However, layers above specific implementation may interpret the fragment to retrieve a secondary resource.

Comparison occurs byte for byte after normalization of all IRI components.

## Case-insensitivity and normalization

All the URI schemes described in this topic follow typical URI rules (RFC 3986) for normalization and resource retrieval for schemes. The normalized form of these URIs maintains case and percent-decodes RFC 3986 unreserved characters.

For all the URI schemes described in this topic, *scheme*, *authority*, and *path* are either case-insensitive by standard, or else are processed by the system in a case-insensitive way. **Note** The only exception to that rule is the *authority* of `ms-resource`, which is case-sensitive.

## ms-appx and ms-appx-web

Use the `ms-appx` or the `ms-appx-web` URI scheme to refer to a file that comes from your app's package (see [Packaging apps](../packaging/index.md)). Files in your app package are typically static images, data, code, and layout files. The `ms-appx-web` scheme accesses the same files as `ms-appx`, but in the web compartment. For examples and more info, see [Reference an image or other asset from XAML markup and code](images-tailored-for-scale-theme-contrast.md#reference-an-image-or-other-asset-from-xaml-markup-and-code).

### Scheme name (ms-appx and ms-appx-web)

The URI scheme name is the string "ms-appx" or "ms-appx-web".

```xml
ms-appx://
```

```xml
ms-appx-web://
```

### Authority (ms-appx and ms-appx-web)

The authority is the package identity name that is defined in the package manifest. It is therefore limited in both the URI and IRI (Internationalized resource identifier) form to the set of characters allowed in a package identity name. The package name must be the name of one of the packages in the current running app's package dependency graph.

```xml
ms-appx://Contoso.MyApp/
ms-appx-web://Contoso.MyApp/
```

If any other character appears in the authority, then retrieval and comparison fail. The default value for the authority is the currently running app's package.

```xml
ms-appx:///
ms-appx-web:///
```

### User info and port (ms-appx and ms-appx-web)

The `ms-appx` scheme, unlike other popular schemes, does not define a user info or port component. Since "@" and ":" are not allowed as valid authority values, lookup will fail if they are included. Each of the following fails.

```xml
ms-appx://john@contoso.myapp/default.html
ms-appx://john:password@contoso.myapp/default.html
ms-appx://contoso.myapp:8080/default.html
ms-appx://john:password@contoso.myapp:8080/default.html
```

### Path (ms-appx and ms-appx-web)

The path component matches the generic RFC 3986 syntax and supports non-ASCII characters in IRIs. The path component defines the logical or physical file path of a file. That file is in a folder associated with the installed location of the app package, for the app specified by the authority.

If the path refers to a physical path and file name then that physical file asset is retrieved. But if no such physical file is found then the actual resource returned during retrieval is determined by using content negotiation at runtime. This determination is based on app, OS, and user settings such as language, display scale factor, theme, high contrast, and other runtime contexts. For example, a combination of the app's languages, the system's display settings, and the user's high contrast settings may be taken into account when determining the actual resource value to be retrieved.

```xml
ms-appx:///images/logo.png
```

The URI above may actually retrieve a file within the current app's package with the following physical file name.

<pre>
\Images\fr-FR\logo.scale-100_contrast-white.png
</pre>

You could of course also retrieve that same physical file by referring to it directly by its full name.

```xaml
<Image Source="ms-appx:///images/fr-FR/logo.scale-100_contrast-white.png"/>
```

The path component of `ms-appx(-web)` is, like generic URIs, case sensitive. However, when the underlying file system by which the resource is accessed is case insensitive, such as for NTFS, the retrieval of the resource is done case-insensitively.

The normalized form of the URI maintains case, and percent-decodes (a "%" symbol followed by the two-digit hexadecimal representation) RFC 3986 unreserved characters. The characters "?", "#", "/", "*", and '"' (the double-quote character) must be percent-encoded in a path to represent data such as file or folder names. All percent-encoded characters are decoded before retrieval. Thus, to retrieve a file named Hello#World.html, use this URI.

```xml
ms-appx:///Hello%23World.html
```

### Query (ms-appx and ms-appx-web)

Query parameters are ignored during retrieval of resources. The normalized form of query parameters maintains case. Query parameters are not ignored during comparison.

## ms-appdata

Use the `ms-appdata` URI scheme to refer to files that come from the app's local, roaming, and temporary data folders. For more info about these app data folders, see [Store and retrieve settings and other app data](/windows/apps/design/app-settings/store-and-retrieve-app-data).

The `ms-appdata` URI scheme does not perform the runtime content negotiation that [ms-appx and ms-appx-web](#ms-appx-and-ms-appx-web) do. But you can respond to the contents of [ResourceContext.QualifierValues](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.QualifierValues) and load the appropriate assets from app data using their full physical file name in the URI.

### Scheme name (ms-appdata)

The URI scheme name is the string "ms-appdata".

```xml
ms-appdata://
```

### Authority (ms-appdata)

The authority is the package identity name that is defined in the package manifest. It is therefore limited in both the URI and IRI (Internationalized resource identifier) form to the set of characters allowed in a package identity name. The package name must be the name of the current running app's package.

```xml
ms-appdata://Contoso.MyApp/
```

If any other character appears in the authority, then retrieval and comparison fail. The default value for the authority is the currently running app's package.

```xml
ms-appdata:///
```

### User info and port (ms-appdata)

The `ms-appdata` scheme, unlike other popular schemes, does not define a user info or port component. Since "@" and ":" are not allowed as valid authority values, lookup will fail if they are included. Each of the following fails.

```xml
ms-appdata://john@contoso.myapp/local/data.xml
ms-appdata://john:password@contoso.myapp/local/data.xml
ms-appdata://contoso.myapp:8080/local/data.xml
ms-appdata://john:password@contoso.myapp:8080/local/data.xml
```

### Path (ms-appdata)

The path component matches the generic RFC 3986 syntax and supports non-ASCII characters in IRIs. Within the [Windows.Storage.ApplicationData](/uwp/api/Windows.Storage.ApplicationData?branch=live) location are three reserved folders for local, roaming, and temporary state storage. The `ms-appdata` scheme allows access to files and folders in those locations. The first segment of the path component must specify the particular folder in the following fashion. Thus the "path-empty" form of "hier-part" is not legal.

Local folder.

```xml
ms-appdata:///local/
```

Temporary folder.

```xml
ms-appdata:///temp/
```

Roaming folder.

```xml
ms-appdata:///roaming/
```

The path component of `ms-appdata` is, like generic URIs, case sensitive. However, when the underlying file system by which the resource is accessed is case insensitive, such as for NTFS, the retrieval of the resource is done case-insensitively.

The normalized form of the URI maintains case, and percent-decodes (a "%" symbol followed by the two-digit hexadecimal representation) RFC 3986 unreserved characters. The characters "?", "#", "/", "*", and '"' (the double-quote character) must be percent-encoded in a path to represent data such as file or folder names. All percent-encoded characters are decoded before retrieval. Thus, to retrieve a local file named Hello#World.html, use this URI.

```xml
ms-appdata://local/Hello%23World.html
```

Retrieval of the resource, and identification of the top level path segment, are handled after normalization of dots (".././b/c"). Therefore, URIs cannot dot themselves out of one of the reserved folders. Thus, the following URI is not allowed.

```xml
ms-appdata:///local/../hello/logo.png
```

But this URI is allowed (albeit redundant).

```xml
ms-appdata:///local/../roaming/logo.png
```

### Query (ms-appdata)

Query parameters are ignored during retrieval of resources. The normalized form of query parameters maintains case. Query parameters are not ignored during comparison.

## ms-resource

Use the `ms-resource` URI scheme to refer to strings loaded from your app's Resources Files (.resw). For examples and more info about Resources Files, see [Localize strings in your UI and app package manifest](localize-strings-ui-manifest.md).

### Scheme name (ms-resource)

The URI scheme name is the string "ms-resource".

```xml
ms-resource://
```

### Authority (ms-resource)

The authority is the top-level resource map defined in the Package Resource Index (PRI), which typically corresponds to the package identity name that is defined in the package manifest. See [Packaging apps](../packaging/index.md)). It is therefore limited in both the URI and IRI (Internationalized resource identifier) form to the set of characters allowed in a package identity name. The package name must be the name of one of the packages in the current running app's package dependency graph.

```xml
ms-resource://Contoso.MyApp/
ms-resource://Microsoft.WinJS.1.0/
```

If any other character appears in the authority, then retrieval and comparison fail. The default value for the authority is the case-sensitive package name of the currently running app.

```xml
ms-resource:///
```

The authority is case sensitive, and the normalized form maintains its case. Lookup of a resource, however, happens case-insensitively.

### User info and port (ms-resource)

The `ms-resource` scheme, unlike other popular schemes, does not define a user info or port component. Since "@" and ":" are not allowed as valid authority values, lookup will fail if they are included. Each of the following fails.

```xml
ms-resource://john@contoso.myapp/Resources/String1
ms-resource://john:password@contoso.myapp/Resources/String1
ms-resource://contoso.myapp:8080/Resources/String1
ms-resource://john:password@contoso.myapp:8080/Resources/String1
```

### Path (ms-resource)

The path identifies the hierarchical location of the [ResourceMap](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceMap?branch=live) subtree (see [Resource Management System](/previous-versions/windows/apps/jj552947(v=win.10))) and the [NamedResource](/uwp/api/Windows.ApplicationModel.Resources.Core.NamedResource?branch=live) within it. Typically, this corresponds to the filename (excluding extension) of a Resources Files (.resw) and the identifier of a string resource within it.

For examples and more info, see [Localize strings in your UI and app package manifest](localize-strings-ui-manifest.md) and [Tile and toast notification support for language, scale, and high contrast](/windows/apps/design/shell/tiles-and-notifications/tile-toast-language-scale-contrast).

The path component of `ms-resource` is, like generic URIs, case sensitive. However, the underlying retrieval does a [CompareStringOrdinal](/windows/desktop/api/winstring/nf-winstring-windowscomparestringordinal) with *ignoreCase* set to `true`.

The normalized form of the URI maintains case, and percent-decodes (a "%" symbol followed by the two-digit hexadecimal representation) RFC 3986 unreserved characters. The characters "?", "#", "/", "*", and '"' (the double-quote character) must be percent-encoded in a path to represent data such as file or folder names. All percent-encoded characters are decoded before retrieval. Thus, to retrieve a string resource from a Resources File named `Hello#World.resw`, use this URI.

```xml
ms-resource:///Hello%23World/String1
```

### Query (ms-resource)

Query parameters are ignored during retrieval of resources. The normalized form of query parameters maintains case. Query parameters are not ignored during comparison. Query parameters are compared case-sensitively.

Developers of particular components layered above this URI parsing may choose to use the query parameters as they see fit.

## Related topics

* [Uniform Resource Identifier (URI): Generic Syntax](https://www.ietf.org/rfc/rfc3986.txt)
* [Packaging apps](../packaging/index.md)
* [Reference an image or other asset from XAML markup and code](images-tailored-for-scale-theme-contrast.md#reference-an-image-or-other-asset-from-xaml-markup-and-code)
* [Store and retrieve settings and other app data](/windows/apps/design/app-settings/store-and-retrieve-app-data)
* [Localize strings in your UI and app package manifest](localize-strings-ui-manifest.md)
* [Resource Management System](/previous-versions/windows/apps/jj552947(v=win.10))
* [Tile and toast notification support for language, scale, and high contrast](/windows/apps/design/shell/tiles-and-notifications/tile-toast-language-scale-contrast)
