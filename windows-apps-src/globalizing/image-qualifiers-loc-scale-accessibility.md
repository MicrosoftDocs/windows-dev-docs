---
author: stevewhims
Description: Your app can load image resource files containing images tailored for display scale factor, theme, high contrast, and other runtime contexts.
title: Load images and assets tailored for scale, theme, high contrast, and others
template: detail.hbs
ms.author: stwhi
ms.date: 10/10/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Load images and assets tailored for scale, theme, high contrast, and others
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Your app can load image resource files (or other asset files) tailored for [display scale factor](../layout/screen-sizes-and-breakpoints-for-responsive-design.md), theme, high contrast, and other runtime contexts. These images can be referenced from imperative code or from XAML markup, for example as the **Source** property of an **Image**. They can also appear in your app package manifest (the `Package.appxmanifest` file)&mdash;for example, as the value for App Icon on the Visual Assets tab of the Visual Studio Manifest Designer&mdash;or on your tiles and toasts. By using qualifiers in your images' file names, and optionally dynamically loading them with the help of a [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=live), you can cause the most appropriate image file to be loaded that best matches the user's runtime settings for display scale, theme, high contrast, language, and other contexts.

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=live)</li>
<li>[**MapChanged**](/uwp/api/windows.foundation.collections.iobservablemap_k_v_?branch=live#Windows_Foundation_Collections_IObservableMap_2_MapChanged)</li></ul>
</div>

An image resource is contained in an image resource file. You can also think of the image as an asset, and the file that contains it as an asset file; and you can find these kinds of resource files in your project's \Assets folder. For background on how to use qualifiers in the names of your image resource files, see [Tailor your resources for language, scale, and other qualifiers](how-to-name-resources-by-using-qualifiers.md).

Some common qualifiers for images are [scale](how-to-name-resources-by-using-qualifiers.md#scale), [theme](how-to-name-resources-by-using-qualifiers.md#theme), [contrast](how-to-name-resources-by-using-qualifiers.md#contrast), and [targetsize](how-to-name-resources-by-using-qualifiers.md#targetsize).

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

## Reference an image resource in XAML markup and code

The name&mdash;or identifier&mdash;of an image resource is its path and file name with any and all qualifiers removed. If you name folders and/or files as in any of the examples in the previous section, then you have a single image resource and its name (as an absolute path) is `/Assets/Images/logo.png`. Here’s how you use that name in imperative code. Notice how the `ms-appx` URI (Uniform Resource Identifier) scheme is used.

**C#**
```csharp
return new BitmapImage(new Uri("ms-appx:///Assets/Images/logo.png"));
```

And here’s how you refer to that same resource in XAML markup.

**XAML**
```xml
<Image Source="ms-appx:///Assets/Images/logo.png"/>
```

Notice how in this example URI the scheme ("`ms-appx`") is followed by "`://`" which is followed by an absolute path (an absolute path begins with "`/`").

**Note** The `ms-resource` (for [string resources](put-ui-strings-into-resources.md)) and `ms-appx` (for image resources) URI schemes perform automatic qualifier matching to find the resource that's most appropriate for the current context. The `ms-appdata` URI scheme&mdash;which is used to load app data&mdash;does not perform any such automatic matching. For more info about app data, see [Store and retrieve settings and other app data](/uwp/app-settings/store-and-retrieve-app-data?branch=live).

Also see [Tile and toast support for language, scale, and high contrast](tile-toast-language-scale-contrast.md).

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

That's all you need to do, and the OS will perform automatic qualifier matching to find the resource that's most appropriate for the current context. For a list of all items in the app package manifest that you can localize or otherwise qualify in this way, see [Localizable manifest items](/uwp/schemas/appxpackage/uapmanifestschema/localizable-manifest-items-win10?branch=live).

## Qualify an image resource for layoutdirection

See [Mirroring images](/uwp/globalizing/adjust-layout-and-fonts--and-support-rtl?branch=live#mirroring-images).

## Load an image for a specific language or other context

The default [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=live) (obtained from [**ResourceContext.GetForCurrentView**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=live#Windows_ApplicationModel_Resources_Core_ResourceContext_GetForCurrentView)) contains a qualifier value for each qualifier name, representing the default runtime context (in other words, the settings for the current user and machine). Image files are matched&mdash;based on the qualifiers in their names&mdash;against the qualifier values in that runtime context.

But there might be times when you want your app to override the system settings and be explicit about the language, scale, or other qualifier value to use when looking for a matching image to load. For example, you might want to control exactly when and which high contrast images are loaded.

You can do that by constructing a new **ResourceContext** (instead of using the default one), overriding its values, and then using that context object in your image lookups.

**C#**
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

For the same effect at a global level, you *can* override the qualifier values in the default **ResourceContext**. But instead we advise you to call [**ResourceContext.SetGlobalQualifierValue**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=live#Windows_ApplicationModel_Resources_Core_ResourceContext_SetGlobalQualifierValue_System_String_System_String_Windows_ApplicationModel_Resources_Core_ResourceQualifierPersistence_). You set values one time with a call to **SetGlobalQualifierValue** and then those values are in effect on the default **ResourceContext** each time you use it for lookups. By default, the [**ResourceManager**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager?branch=live) class uses the default **ResourceContext**.

**C#**
```csharp
Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue("Contrast", "high");
var namedResource = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap[@"Files/Assets/Logo.png"];
this.myXAMLImageElement.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(namedResource.Uri);
```

## Updating images in response to qualifier value change events

Your running app can respond to changes in system settings that affect the qualifier values in the default resource context. Any of these system settings invokes the [**MapChanged**](/uwp/api/windows.foundation.collections.iobservablemap_k_v_?branch=live#Windows_Foundation_Collections_IObservableMap_2_MapChanged) event on [**ResourceContext.QualifierValues**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=live#Windows_ApplicationModel_Resources_Core_ResourceContext_QualifierValues).

In response to this event, you can reload your images with the help of the default **ResourceContext**, which [**ResourceManager**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager?branch=live) uses by default.

**C#**
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

## Related topics

* [Tailor your resources for language, scale, and other qualifiers](how-to-name-resources-by-using-qualifiers.md)
* [Localize strings in your UI and app package manifest](put-ui-strings-into-resources.md)
* [Store and retrieve settings and other app data](/uwp/app-settings/store-and-retrieve-app-data?branch=live)
* [Tile and toast support for language, scale, and high contrast](tile-toast-language-scale-contrast.md)
* [Localizable manifest items](/uwp/schemas/appxpackage/uapmanifestschema/localizable-manifest-items-win10?branch=live)