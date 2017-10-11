---
author: stevewhims
Description: Your app can load images tailored for display language, display scale factor, high contrast, and other runtime contexts.
title: Localize strings in your UI and app package manifest
label: Image qualifiers for localization, scale, and accessibility
template: detail.hbs
ms.author: stwhi
ms.date: 10/10/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Image qualifiers for localization, scale, and accessibility
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Your app can load image resource files tailored for display language, [display scale factor](../layout/screen-sizes-and-breakpoints-for-responsive-design.md), high contrast, and other runtime contexts. These images can be referenced from imperative code or from XAML markup, for example as the **Source** property of an **Image**. They can also appear in your app package manifest (the `Package.appxmanifest` file), for example as the value for App Icon on the Visual Assets tab of the Visual Studio Manifest Designer. By using qualifiers in your images' file names, and optionally dynamically loading them with the help of a [**ResourceContext**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=master), you can cause the most appropriate image file to be loaded that best matches the user's runtime settings for language, display scale, and contrast.

For background on how to use qualifiers in your images' file names, see [Tailor your resources for language, scale, and other qualifiers](how-to-name-resources-by-using-qualifiers.md).

## Refer to an image file from your app package manifest

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