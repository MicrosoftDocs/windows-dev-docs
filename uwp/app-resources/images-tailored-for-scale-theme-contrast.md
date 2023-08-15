---
description: Your app can load image resource files containing images tailored for display scale factor, theme, high contrast, and other runtime contexts.
title: Load images and assets tailored for scale, theme, high contrast, and more
template: detail.hbs
ms.date: 08/15/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---

# Load images and assets tailored for scale, theme, high contrast, and more

Your app can load image resource files (or other asset files) tailored for [display scale factor](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design), theme, high contrast, and other runtime contexts. These images can be referenced from imperative code or from XAML markup, for example as the **Source** property of an **Image**. They can also appear in your app package manifest source file (the `Package.appxmanifest` file)&mdash;for example, as the value for App Icon on the Visual Assets tab of the Visual Studio Manifest Designer&mdash;or on your tiles and toasts. By using qualifiers in your images' file names, and optionally dynamically loading them with the help of a [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext), you can cause the most appropriate image file to be loaded that best matches the user's runtime settings for display scale, theme, high contrast, language, and other contexts.

An image resource is contained in an image resource file. You can also think of the image as an asset, and the file that contains it as an asset file; and you can find these kinds of resource files in your project's \Assets folder. For background on how to use qualifiers in the names of your image resource files, see [Tailor your resources for language, scale, and other qualifiers](tailor-resources-lang-scale-contrast.md).

Some common qualifiers for images are [scale](tailor-resources-lang-scale-contrast.md#scale), [theme](tailor-resources-lang-scale-contrast.md#theme), [contrast](tailor-resources-lang-scale-contrast.md#contrast), and [targetsize](tailor-resources-lang-scale-contrast.md#targetsize).

## Qualify an image resource for scale, theme, and contrast

The default value for the `scale` qualifier is `scale-100`. So, these two variants are equivalent (they both provide an image at scale 100, or scale factor 1).

```
\Assets\Images\logo.png
\Assets\Images\logo.scale-100.png
```

You can use qualifiers in folder names instead of file names. That would be a better strategy if you have several asset files per qualifier. For purposes of illustration, these two variants are equivalent to the two above.

```
\Assets\Images\logo.png
\Assets\Images\scale-100\logo.png
```

Next is an example of how you can provide variants of an image resource&mdash;named `/Assets/Images/logo.png`&mdash;for different settings of display scale, theme, and high contrast. This example uses folder naming.

```
\Assets\Images\contrast-standard\theme-dark
	\scale-100\logo.png
	\scale-200\logo.png
\Assets\Images\contrast-standard\theme-light
	\scale-100\logo.png
	\scale-200\logo.png
\Assets\Images\contrast-high
	\scale-100\logo.png
	\scale-200\logo.png
```

## Reference an image or other asset from XAML markup and code

The name&mdash;or identifier&mdash;of an image resource is its path and file name with any and all qualifiers removed. If you name folders and/or files as in any of the examples in the previous section, then you have a single image resource and its name (as an absolute path) is `/Assets/Images/logo.png`. Here’s how you use that name in XAML markup.

```xaml
<Image x:Name="myXAMLImageElement" Source="ms-appx:///Assets/Images/logo.png"/>
```

Notice that you use the `ms-appx` URI scheme because you're referring to a file that comes from your app's package. See [URI schemes](uri-schemes.md). And here’s how you refer to the same image resource in imperative code.

```csharp
this.myXAMLImageElement.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/logo.png"));
```

You can use `ms-appx` to load any arbitrary file from your app package.

```csharp
var uri = new System.Uri("ms-appx:///Assets/anyAsset.ext");
var storagefile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
```

The `ms-appx-web` scheme accesses the same files as `ms-appx`, but in the web compartment.

```xaml
<WebView x:Name="myXAMLWebViewElement" Source="ms-appx-web:///Pages/default.html"/>
```

```csharp
this.myXAMLWebViewElement.Source = new Uri("ms-appx-web:///Pages/default.html");
```

For any of the scenarios shown in these examples, use the [Uri constructor](/dotnet/api/system.uri.-ctor?view=netcore-2.0&preserve-view=true#System_Uri__ctor_System_String_) overload that infers the [UriKind](/dotnet/api/system.urikind). Specify a valid absolute URI including the scheme and authority, or just let the authority default to the app's package as in the example above.

Notice how in these example URIs the scheme ("`ms-appx`" or "`ms-appx-web`") is followed by "`://`" which is followed by an absolute path. In an absolute path, the leading "`/`" causes the path to be interpreted from the root of the package.

> [!NOTE]
> The `ms-resource` (for [string resources](localize-strings-ui-manifest.md)) and `ms-appx(-web)` (for images and other assets) URI schemes perform automatic qualifier matching to find the resource that's most appropriate for the current context. The `ms-appdata` URI scheme (which is used to load app data) does not perform any such automatic matching, but you can respond to the contents of [ResourceContext.QualifierValues](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.QualifierValues) and explicitly load the appropriate assets from app data using their full physical file name in the URI. For info about app data, see [Store and retrieve settings and other app data](/windows/apps/design/app-settings/store-and-retrieve-app-data). Web URI schemes (for example, `http`, `https`, and `ftp`) do not perform automatic matching, either. For info about what to do in that case, see [Hosting and loading images in the cloud](/windows/apps/design/shell/tiles-and-notifications/tile-toast-language-scale-contrast#hosting-and-loading-images-in-the-cloud).

Absolute paths are a good choice if your image files remain where they are in the project structure. If you want to be able to move an image file, but you're careful that it remains in the same location relative to its referencing XAML markup file, then instead of an absolute path you might want to use a path that's relative to the containing markup file. If you do that, then you needn't use a URI scheme. You will still benefit from automatic qualifier matching in this case, but only because you are using the relative path in XAML markup.

```xaml
<Image Source="Assets/Images/logo.png"/>
```

Also see [Tile and toast support for language, scale, and high contrast](/windows/apps/design/shell/tiles-and-notifications/tile-toast-language-scale-contrast).

## Qualify an image resource for targetsize

You can use the `scale` and `targetsize` qualifiers on different variants of the same image resource; but you can't use them both on a single variant of a resource. Also, you need to define at least one variant without a `TargetSize` qualifier. That variant must either define a value for `scale`, or let it default to `scale-100`. So, these two variants of the `/Assets/Square44x44Logo.png` resource are valid.

```
\Assets\Square44x44Logo.scale-200.png
\Assets\Square44x44Logo.targetsize-24.png
```

And these two variants are valid. 

```
\Assets\Square44x44Logo.png // defaults to scale-100
\Assets\Square44x44Logo.targetsize-24.png
```

But this variant is not valid.

```
\Assets\Square44x44Logo.scale-200_targetsize-24.png
```

## Refer to an image file from your app package manifest

If you name folders and/or files as in either of the two valid examples in the previous section, then you have a single app icon image resource and its name (as a relative path) is `Assets\Square44x44Logo.png`. In your app package manifest, simply refer to the resource by name. There's no need to use any URI scheme.

![add resource, english](images/app-icon.png)

That's all you need to do, and the OS will perform automatic qualifier matching to find the resource that's most appropriate for the current context. For a list of all items in the app package manifest that you can localize or otherwise qualify in this way, see [Localizable manifest items](/uwp/schemas/appxpackage/uapmanifestschema/localizable-manifest-items-win10).

## Qualify an image resource for layoutdirection
See [Mirroring images](/windows/apps/design/globalizing/adjust-layout-and-fonts--and-support-rtl#mirroring-images).

## Load an image for a specific language or other context
For more info about the value proposition of localizing your app, see [Globalization and localization](/windows/apps/design/globalizing/globalizing-portal).

The default [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext) (obtained from [**ResourceContext.GetForCurrentView**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.GetForCurrentView)) contains a qualifier value for each qualifier name, representing the default runtime context (in other words, the settings for the current user and machine). Image files are matched&mdash;based on the qualifiers in their names&mdash;against the qualifier values in that runtime context.

But there might be times when you want your app to override the system settings and be explicit about the language, scale, or other qualifier value to use when looking for a matching image to load. For example, you might want to control exactly when and which high contrast images are loaded.

You can do that by constructing a new **ResourceContext** (instead of using the default one), overriding its values, and then using that context object in your image lookups.

```csharp
var resourceContext = new Windows.ApplicationModel.Resources.Core.ResourceContext(); // not using ResourceContext.GetForCurrentView 
resourceContext.QualifierValues["Contrast"] = "high";
var namedResource = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap[@"Files/Assets/Logo.png"];
var resourceCandidate = namedResource.Resolve(resourceContext);
var imageFileStream = resourceCandidate.GetValueAsStreamAsync().GetResults();
var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
bitmapImage.SetSourceAsync(imageFileStream);
this.myXAMLImageElement.Source = bitmapImage;
```

For the same effect at a global level, you *can* override the qualifier values in the default **ResourceContext**. But instead we advise you to call [**ResourceContext.SetGlobalQualifierValue**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.setglobalqualifiervalue#Windows_ApplicationModel_Resources_Core_ResourceContext_SetGlobalQualifierValue_System_String_System_String_Windows_ApplicationModel_Resources_Core_ResourceQualifierPersistence_). You set values one time with a call to **SetGlobalQualifierValue** and then those values are in effect on the default **ResourceContext** each time you use it for lookups. By default, the [**ResourceManager**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager) class uses the default **ResourceContext**.

```csharp
Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue("Contrast", "high");
var namedResource = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap[@"Files/Assets/Logo.png"];
this.myXAMLImageElement.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(namedResource.Uri);
```

## Updating images in response to qualifier value change events

Your running app can respond to changes in system settings that affect the qualifier values in the default resource context. Any of these system settings invokes the [**MapChanged**](/uwp/api/windows.foundation.collections.iobservablemap-2.mapchanged) event on [**ResourceContext.QualifierValues**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.QualifierValues).

In response to this event, you can reload your images with the help of the default **ResourceContext**, which [**ResourceManager**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager) uses by default.

```csharp
public MainPage()
{
    this.InitializeComponent();

    ...

    // Subscribe to the event that's raised when a qualifier value changes.
    var qualifierValues = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
    qualifierValues.MapChanged += new Windows.Foundation.Collections.MapChangedEventHandler<string, string>(QualifierValues_MapChanged);
}

private async void QualifierValues_MapChanged(IObservableMap<string, string> sender, IMapChangedEventArgs<string> @event)
{
    var dispatcher = this.myImageXAMLElement.Dispatcher;
    if (dispatcher.HasThreadAccess)
    {
        this.RefreshUIImages();
    }
    else
    {
        await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => this.RefreshUIImages());
    }
}

private void RefreshUIImages()
{
    var namedResource = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap[@"Files/Assets/Logo.png"];
    this.myImageXAMLElement.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(namedResource.Uri);
}
```

## Important APIs

* [ResourceContext](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=live)
* [ResourceContext.SetGlobalQualifierValue](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.setglobalqualifiervalue?branch=live#Windows_ApplicationModel_Resources_Core_ResourceContext_SetGlobalQualifierValue_System_String_System_String_Windows_ApplicationModel_Resources_Core_ResourceQualifierPersistence_)
* [MapChanged](/uwp/api/windows.foundation.collections.iobservablemap-2.mapchanged?branch=live)

## Related topics

* [Tailor your resources for language, scale, and other qualifiers](tailor-resources-lang-scale-contrast.md)
* [Localize strings in your UI and app package manifest](localize-strings-ui-manifest.md)
* [Store and retrieve settings and other app data](/windows/apps/design/app-settings/store-and-retrieve-app-data)
* [Tile and toast support for language, scale, and high contrast](/windows/apps/design/shell/tiles-and-notifications/tile-toast-language-scale-contrast)
* [Localizable manifest items](/uwp/schemas/appxpackage/uapmanifestschema/localizable-manifest-items-win10?branch=live)
* [Mirroring images](/windows/apps/design/globalizing/adjust-layout-and-fonts--and-support-rtl#mirroring-images)
* [Globalization and localization](/windows/apps/design/globalizing/globalizing-portal)
