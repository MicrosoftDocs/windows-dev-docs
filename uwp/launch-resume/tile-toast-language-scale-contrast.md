---
description: Your tiles and toasts can load strings and images tailored for display language, display scale factor, high contrast, and other runtime contexts.
title: Tile and toast notification support for language, scale, and high contrast
template: detail.hbs
ms.date: 08/08/2024
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# Tile and toast notification support for language, scale, and high contrast

[!INCLUDE [notes](includes/live-tiles-note.md)]

Your tiles and toasts can load strings and images tailored for display language, [display scale factor](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design), high contrast, and other runtime contexts. For background on how to use qualifiers in the names of your resource files, see [Tailor your resources for language, scale, and other qualifiers](/windows/uwp/app-resources/tailor-resources-lang-scale-contrast) and [App icons](/windows/apps/design/style/iconography/overview).

For more info about the value proposition of localizing your app, see [Globalization and localization](/windows/apps/design/globalizing/globalizing-portal).

## Refer to a string resource from a template

In your tile or toast template, you can refer to a string resource using the `ms-resource` URI (Uniform Resource Identifier) scheme followed by a simple string resource identifier. For example, if you have a Resources.resx file that contains a resource entry whose name is "Farewell", then you have a string resource with the identifier "Farewell". For more info on string resource identifiers and Resources Files (.resw), see [Localize strings in your UI and app package manifest](/windows/uwp/app-resources/localize-strings-ui-manifest).

This is how a reference to the "Farewell" string resource identifier would look in the [text](/uwp/schemas/tiles/tilesschema/element-text) body of your template content, using `ms-resource`.

```xml
<text id="1">ms-resource:Farewell</text>
```

If you omit the `ms-resource` URI scheme, then the text body is just a string literal, and *not* a reference to an identifier.

```xml
<text id="1">Farewell</text>
```

## Refer to an image resource from a template

In your tile or toast template, you can refer to an image resource using the `ms-appx` URI (Uniform Resource Identifier) scheme followed by the name of the image resource. This is the same way that you refer to an image resource in XAML markup (for more details, see [Reference an image or other asset from XAML markup and code](/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code)).

For example, you might name folders like this.

```
\Assets\Images\contrast-standard\welcome.png
\Assets\Images\contrast-high\welcome.png
```

In that case, you have a single image resource and its name (as an absolute path) is `/Assets/Images/welcome.png`. Here’s how you use that name in your template.

```xml
<image id="1" src="ms-appx:///Assets/Images/welcome.png"/>
```

Notice how in this example URI the scheme ("`ms-appx`") is followed by "`://`" which is followed by an absolute path (an absolute path begins with "`/`").

## Hosting and loading images in the cloud

The `ms-resource` and `ms-appx` URI schemes perform automatic qualifier matching to find the resource that's most appropriate for the current context. Web URI schemes (for example, `http`, `https`, and `ftp`) do not perform any such automatic matching.

Instead, append onto your image's URI a query string describing the requested qualifier value or values.

```xml
<image id="1" src="http://www.contoso.com/Assets/Images/welcome.png?ms-lang=en-US"/>
```

Then, in the app service that provides your images, implement an HTTP handler that inspects and uses the query string to determine which image to return.

You also need to set the [**addImageQuery**](/uwp/schemas/tiles/tilesschema/element-visual) attribute to `true` in the [tile](/uwp/schemas/tiles/tilesschema/schema-root) or [toast](/uwp/schemas/tiles/toastschema/schema-root) notification XML payload. The **addImageQuery** attribute appears in the `visual`, `binding`, and `image` elements of both the tile and toast schemas. Explicitly setting **addImageQuery** on an element overrides any value set on an ancestor. For instance, an **addImageQuery** value of `true` in an `image` element overrides an **addImageQuery** of `false` in its parent `binding` element.

These are the query strings you can use.

| Qualifier | Query string | Example |
| --------- | ------------ | ------- |
| Scale | ms-scale | ?ms-scale=400 |
| Language | ms-lang | ?ms-lang=en-US |
| Contrast | ms-contrast | ?ms-contrast=high |

For a reference table of all the possible qualifier values that you can use in your query strings, see [ResourceContext.QualifierValues](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.QualifierValues).

## Important APIs

* [ResourceContext.QualifierValues](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.QualifierValues)

## Related topics

* [Screen sizes and break points for responsive design](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design)
* [Tailor your resources for language, scale, and other qualifiers](/windows/uwp/app-resources/tailor-resources-lang-scale-contrast)
* [App icons](/windows/apps/design/style/iconography/overview).
* [Globalization and localization](/windows/apps/design/globalizing/globalizing-portal)
* [Localize strings in your UI and app package manifest](/windows/uwp/app-resources/localize-strings-ui-manifest)
* [Reference an image or other asset from XAML markup and code](/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast)
* [addImageQuery](/uwp/schemas/tiles/tilesschema/element-visual)
* [Tile schema](/uwp/schemas/tiles/tilesschema/schema-root)
* [Toast schema](/uwp/schemas/tiles/toastschema/schema-root)
