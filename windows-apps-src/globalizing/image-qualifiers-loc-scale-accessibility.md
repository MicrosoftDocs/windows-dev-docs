---
author: stevewhims
Description: Your app can load image resource files containing images tailored for display language, display scale factor, high contrast, and other runtime contexts.
title: Load images and assets tailored for language, scale, and high contrast
template: detail.hbs
ms.author: stwhi
ms.date: 10/10/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Load images and assets tailored for language, scale, and high contrast
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Your app can load image resource files containing images tailored for display language, [display scale factor](../layout/screen-sizes-and-breakpoints-for-responsive-design.md), high contrast, and other runtime contexts. These images can be referenced from imperative code or from XAML markup, for example as the **Source** property of an **Image**. They can also appear in your app package manifest (the `Package.appxmanifest` file), for example as the value for App Icon on the Visual Assets tab of the Visual Studio Manifest Designer. By using qualifiers in your images' file names, and optionally dynamically loading them with the help of a [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=master), you can cause the most appropriate image file to be loaded that best matches the user's runtime settings for language, display scale, and contrast.

An image resource is contained in an image resource file. You can also think of the image as an asset, and the file that contains it as an asset file; and you can find these kinds of resource files in your project's \Assets folder. For background on how to use qualifiers in the names of your image resource files, see [Tailor your resources for language, scale, and other qualifiers](how-to-name-resources-by-using-qualifiers.md).

## Reference an image resource by name

The name&mdash;or identifier&mdash;of an image resource is its path and file name with any and all qualifiers removed. Take for example these two equivalent sets of image files.

`\Assets\Images\contrast-standard\logo.png`

`\Assets\Images\contrast-high\logo.png`

`\Assets\Images\contrast-black\logo.png`

`\Assets\Images\contrast-white\logo.png`

<br/>

`\Assets\Images\logo.contrast-standard.png`

`\Assets\Images\logo.contrast-high.png`

`\Assets\Images\logo.contrast-black.png`

`\Assets\Images\logo.contrast-white.png`

If you name folders and/or files as in either of the two examples above, then you have a single image resource and its name (as an absolute path) is `/Assets/Images/logo.png`. Here’s how you use that name in imperative code. Notice how the `ms-appx` URI (Uniform Resource Identifier) scheme is used.

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

## Refer to an image file from your app package manifest

Take for example these two app icon image files.

`\Assets\en\Square44x44Logo.scale-200.png`

`\Assets\ja\Square44x44Logo.scale-200.png`

If you name your app icon folders and/or files as in the example above, then you will have a single app icon image resource and its name (as a relative path) is `Assets\Square44x44Logo.png`. In your app package manifest, simply refer to the resource by name without including the `ms-appx` URI scheme.

![add resource, english](images/app-icon.png)

For a list of all items in the app package manifest that you can localize, see [Localizable manifest items](/uwp/schemas/appxpackage/uapmanifestschema/localizable-manifest-items-win10?branch=master).

## Load an image for a specific language or other context

The default [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=master) (obtained from [**ResourceContext.GetForCurrentView**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=master#Windows_ApplicationModel_Resources_Core_ResourceContext_GetForCurrentView)) contains a qualifier value for each qualifier name, representing the default runtime context (in other words, the settings for the current user and machine). Image files are matched&mdash;based on the qualifiers in their names&mdash;against the qualifier values in that runtime context.

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

For the same effect at a global level, you *can* override the qualifier values in the default **ResourceContext**. But instead we advise you to call [**ResourceContext.SetGlobalQualifierValue**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=master#Windows_ApplicationModel_Resources_Core_ResourceContext_SetGlobalQualifierValue_System_String_System_String_Windows_ApplicationModel_Resources_Core_ResourceQualifierPersistence_). You set values one time with a call to **SetGlobalQualifierValue** and then those values are in effect on the default **ResourceContext** each time you use it for lookups. By default, the [**ResourceManager**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager?branch=master) class uses the default **ResourceContext**.

**C#**
```csharp
Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue("Contrast", "high");
var namedResource = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap[@"Files/Assets/Logo.png"];
this.myXAMLImageElement.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(namedResource.Uri);
```

## Updating images in response to qualifier value change events

Your running app can respond to changes in system settings that affect the qualifier values in the default resource context. Any of these system settings invokes the [**MapChanged**](/uwp/api/windows.foundation.collections.iobservablemap_k_v_?branch=master#Windows_Foundation_Collections_IObservableMap_2_MapChanged) event on [**ResourceContext.QualifierValues**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=master#Windows_ApplicationModel_Resources_Core_ResourceContext_QualifierValues).

In response to this event, you can reload your images with the help of the default **ResourceContext**, which [**ResourceManager**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager?branch=master) uses by default.

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
* [Localizable manifest items](/uwp/schemas/appxpackage/uapmanifestschema/localizable-manifest-items-win10?branch=master)