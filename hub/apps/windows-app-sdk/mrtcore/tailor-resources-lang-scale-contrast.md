---
description: This topic explains the general concept of resource qualifiers, how to use them, and the purpose of each qualifier name.
title: Tailor your resources for language, scale, high contrast and other qualifiers
ms.date: 08/19/2024
ms.topic: article
keywords: windows 10, windows 11, windows app sdk, winui, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---

# Tailor your resources for language, scale, high contrast, and other qualifiers

This topic explains the general concept of resource qualifiers, how to use them, and the purpose of each of the qualifier names. See [**ResourceContext.QualifierValues**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcecontext.qualifiervalues) for a reference table of all the possible qualifier values.

Your app can load assets and resources that are tailored to runtime contexts such as display language, high contrast, [display scale factor](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design#effective-pixels-and-scale-factor), and many others. The way you do this is to name your resources' folders or files to match the qualifier names and qualifier values that correspond to those contexts. For example, you may want your app to load a different set of image assets in high contrast mode.

For more info about the value proposition of localizing your app, see [Globalization and localization](/windows/apps/design/globalizing/globalizing-portal).

## Qualifier name, qualifier value, and qualifier

A qualifier name is a key that maps to a set of qualifier values. Here are the qualifier name and qualifier values for contrast.

| Context | Qualifier name | Qualifier values |
| :--------------- | :--------------- | :--------------- |
| The high contrast setting | contrast | standard, high, black, white |

You combine a qualifier name with a qualifier value to form a qualifier. `<qualifier name>-<qualifier value>` is the format of a qualifier. `contrast-standard` is an example of a qualifier.

So, for high contrast, the set of qualifiers is `contrast-standard`, `contrast-high`, `contrast-black`, and `contrast-white`. Qualifier names and qualifier values are not case sensitive. For example, `contrast-standard` and `Contrast-Standard` are the same qualifier.

## Use qualifiers in folder names

Here is an example of using qualifiers to name folders that contain asset files. Use qualifiers in folder names if you have several asset files per qualifier. That way, you set the qualifier once at the folder level, and the qualifier applies to everything inside the folder.

```console
\Assets\Images\contrast-standard\<logo.png, and other image files>
\Assets\Images\contrast-high\<logo.png, and other image files>
\Assets\Images\contrast-black\<logo.png, and other image files>
\Assets\Images\contrast-white\<logo.png, and other image files>
```

If you name your folders as in the example above, then your app uses the high contrast setting to load resource files from the folder named for the appropriate qualifier. So, if the setting is High Contrast Black, then the resource files in the `\Assets\Images\contrast-black` folder are loaded. If the setting is None (that is, the computer is not in high contrast mode), then the resource files in the `\Assets\Images\contrast-standard` folder are loaded.

## Use qualifiers in file names

Instead of creating and naming folders, you can use a qualifier to name the resource files themselves. You might prefer to do this if you only have one resource file per qualifier. Here's an example.

```console
\Assets\Images\logo.contrast-standard.png
\Assets\Images\logo.contrast-high.png
\Assets\Images\logo.contrast-black.png
\Assets\Images\logo.contrast-white.png
```

The file whose name contains the qualifier most appropriate for the setting is the one that is loaded. This matching logic works the same way for file names as for folder names.

## Reference a string or image resource by name

See the following topics for more info about how to reference a string or image resource by name:

- [Refer to a string resource identifier from XAML markup](localize-strings.md#refer-to-a-string-resource-identifier-from-xaml)
- [Refer to a string resource identifier from code](localize-strings.md#refer-to-a-string-resource-identifier-from-code)
- [Reference an image or other asset from XAML markup and code](images-tailored-for-scale-theme-contrast.md#reference-an-image-or-other-asset-from-xaml-markup-and-code)

## Actual and neutral qualifier matches

You don't need to provide a resource file for *every* qualifier value. For example, if you find that you only need one visual asset for high contrast and one for standard contrast, then you can name those assets like this.

```console
\Assets\Images\logo.contrast-high.png
\Assets\Images\logo.png
```

The first file name contains the `contrast-high` qualifier. That qualifier is an *actual* match for any high contrast setting when high contrast is *on*. In other words, it's a close match so it's preferred. An *actual* match can only occur if the qualifier contains an *actual* value, as this one does. In this case, `high` is an *actual* value for `contrast`.

The file named `logo.png` has no contrast qualifier on it at all. The absence of a qualifier is a *neutral* value. If no preferred match can be found, then the neutral value serves as a fallback match. In this example, if high contrast is *off*, then there is no actual match. The *neutral* match is the best match that can be found, and so the asset `logo.png` is loaded.

If you were to change the name of `logo.png` to `logo.contrast-standard.png`, then the file name would contain an actual qualifier value. With high contrast off, there would be an actual match with `logo.contrast-standard.png`, and that's the asset file that would be loaded. So, the same files would be loaded, under the same conditions, but because of different matches.

If you only need one set of assets for high contrast and one set for standard contrast, then you can use folder names instead of file names. In this case, omitting the folder name entirely gives you the neutral match.

```console
\Assets\Images\contrast-high\<logo.png, and other images to load when high contrast theme is not None>
\Assets\Images\<logo.png, and other images to load when high contrast theme is None>
```

For more details on how qualifier matching works, see [Manage resources with MRT Core](mrtcore-overview.md).

## Multiple qualifiers

You can combine qualifiers in folder and file names. For example, you may want your app to load image assets when high contrast mode is on *and* the display scale factor is 400. One way to do this is with nested folders.

```console
\Assets\Images\contrast-high\scale-400\<logo.png, and other image files>
```

For `logo.png` and the other files to be loaded, the settings must match *both* qualifiers.

Another option is to combine multiple qualifiers in one folder name.

```console
\Assets\Images\contrast-high_scale-400\<logo.png, and other image files>
```

In a folder name, you combine multiple qualifiers separated with an underscore. `<qualifier1>[_<qualifier2>...]` is the format.

You can combine multiple qualifiers in a file name in the same format.

```console
\Assets\Images\logo.contrast-high_scale-400.png
```

Depending on the tools and workflow you use for asset-creation, or on what you find easiest to read and/or manage, you can either choose a single naming strategy for all qualifiers, or you can combine them for different qualifiers.

## AlternateForm

The `alternateform` qualifier is used to provide an alternate form of a resource for some special purpose. This is typically used only by Japanese app developers to provide a furigana string for which the value `msft-phonetic` is reserved (see the section "Support Furigana for Japanese strings that can be sorted" in [How to prepare for localization](/previous-versions/windows/apps/hh967762(v=win.10))).

Either your target system or your app must provide a value against which `alternateform` qualifiers are matched. Do not use the `msft-` prefix for your own custom `alternateform` qualifier values.

## Configuration

It's unlikely that you'll need the `configuration` qualifier name. It can be used to specify resources that are applicable only to a given authoring-time environment, such as test-only resources.

The `configuration` qualifier is used to load a resource that best matches the value of the `MS_CONFIGURATION_ATTRIBUTE_VALUE` environment variable. So, you can set the variable to the string value that has been assigned to the relevant resources, for example `designer`, or `test`.

## Contrast

The `contrast` qualifier is used to provide resources that best match high contrast settings.

## DXFeatureLevel

It's unlikely that you'll need the `dxfeaturelevel` qualifier name. It was designed to be used with Direct3D game assets, to cause downlevel resources to be loaded to match a particular downlevel hardware configuration of the time. But the prevalence of that hardware configuration is now so low that we recommend you don't use this qualifier.

## HomeRegion

The `homeregion` qualifier corresponds to the user's setting for country or region. It represents the home location of the user. Values include any valid [BCP-47 region tag](https://tools.ietf.org/html/bcp47). That is, any **ISO 3166-1 alpha-2** two-letter region code, plus the set of **ISO 3166-1 numeric** three-digit geographic codes for composed regions (see [United Nations Statistic Division M49 composition of region codes](https://unstats.un.org/unsd/methods/m49/m49regin.htm)). Codes for "Selected economic and other groupings" are not valid.

## Language

A `language` qualifier corresponds to the display language setting. Values include any valid [BCP-47 language tag](https://tools.ietf.org/html/bcp47). For a list of languages, see the [IANA language subtag registry](https://www.iana.org/assignments/language-subtag-registry).

If you want your app to support different display languages, and you have string literals in your code or in your XAML markup, then move those strings out of the code/markup and into a Resources File (`.resw`). You can then make a translated copy of that Resources File for each language that your app supports.

You typically use a `language` qualifier to name the folders that contain your Resources Files (`.resw`).

```console
\Strings\language-en\Resources.resw
\Strings\language-ja\Resources.resw
```

You can omit the `language-` part of a `language` qualifier (that is, the qualifier name). You can't do this with the other kinds of qualifiers; and you can only do it in a folder name.

```console
\Strings\en\Resources.resw
\Strings\ja\Resources.resw
```

Instead of naming folders, you can use `language` qualifiers to name the Resources Files themselves.

```console
\Strings\Resources.language-en.resw
\Strings\Resources.language-ja.resw
```

See [Localize your UI strings](localize-strings.md) for more information on making your app localizable by using string resources, and how to reference a string resource in your app.

## LayoutDirection

A `layoutdirection` qualifier corresponds to the layout direction of the display language setting. For example, an image may need to be mirrored for a right-to-left language such as Arabic or Hebrew. Layout panels and images in your UI will respond to layout direction appropriately if you set their [FlowDirection](/uwp/api/Windows.UI.Xaml.FrameworkElement.FlowDirection) property (see [Adjust layout and fonts, and support RTL](/windows/apps/design/globalizing/adjust-layout-and-fonts--and-support-rtl)). However, the `layoutdirection` qualifier is for cases where simple flipping isn't adequate, and it allows you to respond to the directionality of specific reading order and text alignment in more general ways.

## Scale

Windows automatically selects a scale factor for each display based on its DPI (dots-per-inch) and the viewing distance of the device. See [Effective pixels and scale factor](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design#effective-pixels-and-scale-factor). You should create your images at several recommended sizes (at least 100, 200, and 400) so that Windows can either choose the perfect size or can use the nearest size and scale it. So that Windows can identify which physical file contains the correct size of image for the display scale factor, you use a `scale` qualifier. The scale of a resource matches the value of [DisplayInformation.ResolutionScale](/uwp/api/windows.graphics.display.displayinformation.ResolutionScale), or the next-largest-scaled resource.

Here's an example of setting the qualifier at the folder level.

```console
\Assets\Images\scale-100\<logo.png, and other image files>
\Assets\Images\scale-200\<logo.png, and other image files>
\Assets\Images\scale-400\<logo.png, and other image files>
```

And this example sets it at the file level.

```console
\Assets\Images\logo.scale-100.png
\Assets\Images\logo.scale-200.png
\Assets\Images\logo.scale-400.png
```

For info about qualifying a resource for both `scale` and `targetsize`, see [Qualify an image resource for targetsize](images-tailored-for-scale-theme-contrast.md#qualify-an-image-resource-for-targetsize).

## TargetSize

The `targetsize` qualifier is primarily used to specify [file type association icons](/windows/desktop/shell/how-to-assign-a-custom-icon-to-a-file-type) or [protocol icons](/windows/desktop/search/-search-3x-wds-ph-ui-extensions) to be shown in File Explorer. The qualifier value represents the side length of a square image in raw (physical) pixels. The resource whose value matches the View setting in File Explorer is loaded; or the resource with the next-largest value in the absence of an exact match.

You can define assets that represent several sizes of `targetsize` qualifier value for the App Icon (`/Assets/Square44x44Logo.png`) in the Visual Assets tab of the app package manifest designer.

For info about qualifying a resource for both `scale` and `targetsize`, see [Qualify an image resource for targetsize](images-tailored-for-scale-theme-contrast.md#qualify-an-image-resource-for-targetsize).

## Theme

The `theme` qualifier is used to provide resources that best match the default app mode setting, or your app's override using [Application.RequestedTheme](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.requestedtheme).

## Shell light theme and unplated resources

The *Windows 10 May 2019 Update* introduced a new "light" theme for the Windows Shell. As a result, some application assets that were previously shown on a dark background will now be shown on a light background. For apps that apps that provided altform-unplated assets for the taskbar and window switchers (Alt+Tab, Task View, etc), you should verify that they have acceptable contrast on a light background.

### Providing light theme specific assets

Apps that want to provide a tailored resource for shell light theme can use a new alternate form resource qualifier: `altform-lightunplated`. This qualifier mirrors the existing altform-unplated qualifier.

### Downlevel considerations

Apps should not use the `theme-light` qualifier with the `altform-unplated` qualifier. This will cause unpredictable behavior on RS5 and earlier versions of Windows due to the way resources are loaded for the Taskbar. On earlier versions of windows, the theme-light version may be used incorrectly. The `altform-lightunplated` qualifier avoids this issue.

### Compatibility behavior

For backwards compatibility, Windows includes logic to detect a monochromatic icons and check whether it contrasts with the intended background. If the icon fails to meet contrast requirements, Windows will look for a contrast-white version of the asset. If that's not available, Windows will fall back to using the plated version of the asset.

## Important APIs

- [ResourceContext.QualifierValues](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcecontext.qualifiervalues)

## Related topics

- [Effective pixels and scale factor](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design#effective-pixels-and-scale-factor)
- [Manage resources with MRT Core](mrtcore-overview.md)
- [How to prepare for localization](/previous-versions/windows/apps/hh967762(v=win.10))
- [Localize your UI strings](localize-strings.md)
- [BCP-47](https://tools.ietf.org/html/bcp47)
- [United Nations Statistic Division M49 composition of region codes](https://unstats.un.org/unsd/methods/m49/m49regin.htm)
- [IANA language subtag registry](https://www.iana.org/assignments/language-subtag-registry)
- [Adjust layout and fonts, and support RTL](/windows/apps/design/globalizing/adjust-layout-and-fonts--and-support-rtl)
