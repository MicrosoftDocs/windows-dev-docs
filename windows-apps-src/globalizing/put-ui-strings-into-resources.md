---
author: stevewhims
Description: If you want your app to support different display languages, and you have string literals in your code or XAML markup or app package manifest, then move those strings into a Resources File (.resw). You can then make a translated copy of that Resources File for each language that your app supports.
title: Localize strings in your UI and app package manifest
ms.assetid: E420B9BB-C0F6-4EC0-BA3A-BA2875B69722
label: Localize strings in your UI and app package manifest
template: detail.hbs
ms.author: stwhi
ms.date: 10/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Localize strings in your UI and app package manifest
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

If you want your app to support different display languages, and you have string literals in your code or XAML markup or app package manifest, then move those strings into a Resources File (.resw). You can then make a translated copy of that Resources File for each language that your app supports.

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**ApplicationModel.Resources.ResourceLoader**](https://msdn.microsoft.com/library/windows/apps/br206014)</li>
<li>[**WinJS.Resources.processAll**](https://msdn.microsoft.com/library/windows/apps/br211864)</li>
</ul>
</div>

Hardcoded string literals can appear in imperative code or in XAML markup, for example as the **Text** property of a **TextBlock**. They can also appear in your app package manifest (the `Package.appxmanifest` file), for example as the value for Display name on the Application tab of the Visual Studio Manifest Designer. Move these strings into a Resources File (.resw), and replace the hardcoded string literals in your app and in your manifest with references to resource identifiers.

## Create a Resources File (.resw) and put your strings in it
 
1. Set your app's default language.
    1. With your solution open in Visual Studio, open `Package.appxmanifest`.
    2. On the Application tab, confirm that the Default language is set appropriately (for example, "en" or "en-US"). The remaining steps will assume that you have set the default language to "en-US".
    <br>**Note** At a minimum, you need to provide resources localized for this default language. Those are the resources that will be loaded if no better match can be found for the user's preferred language or display language settings.
2. Create a Resources File (.resw) for the default language.
    1. Under your project node, create a new folder and name it "Strings".
    2. Under `Strings`, create a new sub-folder and name it "en-US".
    3. Under `en-US`, create a new Resources File (.resw) and confirm that it is named "Resources.resw".
    <br>**Note** If you have .NET Resources Files (.resx) that you want to port, see [Porting XAML and UI](../porting/wpsl-to-uwp-porting-xaml-and-ui.md#localization-and-globalization).
3.  Open `Resources.resw` and add these resources.

    `Strings/en-US/Resources.resw`

    ![add resource, english](images/addresource-en-us.png)

    In this example, "Greeting" is a resource identifier that you can refer to from your markup, as we'll show. For the identifier "Greeting", a string is provided for a Text property, and a string is provided for a Width property. "Greeting.Text" is an example of a property identifier because it corresponds to a property of a UI element. You could also, for example, add "Greeting.Foreground" in the Name column, and set its Value to "Red". The "Farewell" identifier is a simple resource identifier; it has no sub-properties and it can be loaded from imperative code, as we'll show. The Comment column is a good place to provide any special instructions to translators.

    In this example, since we have a simple resource identifier entry named "Farewell", we cannot *also* have property identifiers based on that same resource identifier. So, adding "Farewell.Text" would cause a Duplicate Entry error when building `Resources.resw`.

## Refer to a resource identifier from markup

You use an [x:Uid directive](../xaml-platform/x-uid-directive.md) to associate a control or other element in your markup with a resource identifier.

```XML
<TextBlock x:Uid="Greeting"/>
```

At run-time, `\Strings\en-US\Resources.resw` is loaded (since right now that's the only resources file in the project). The **x:Uid** directive on the **TextBlock** causes a lookup to take place, to find property identifiers inside `Resources.resw` that contain the resource identifier "Greeting". The "Greeting.Text" and "Greeting.Width" property identifiers are found and their values are applied to the **TextBlock**, overriding any values set locally in the markup. The "Greeting.Foreground" value would be applied, too, if you'd added that. But only property identifiers are used to set properties on XAML markup elements, so setting **x:Uid** to "Farewell" on this TextBlock would have no effect. `Resources.resw` *does* contain the resource identifier "Farewell", but it contains no property identifiers for it.

Instead of setting **Width** from a resources file, you'll probably want to allow controls to dynamically size to content.

**Note** For [attached properties](../xaml-platform/attached-properties-overview.md), you need a special syntax in the Name column of a .resw file. For example, to set a value for the [AutomationProperties.Name](/uwp/api/windows.ui.xaml.automation.automationproperties?branch=master#Windows_UI_Xaml_Automation_AutomationProperties_NameProperty) attached property for the "Greeting" identifier, this is what you would enter in the Name column.

```XML
Greeting.[using:Windows.UI.Xaml.Automation]AutomationProperties.Name
```

## Refer to a resource identifier from code

You can explicitly load a resource based on a simple resource identifier.

**C#**
```CSharp
var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
var str = loader.GetString("Farewell");
```

**C++**
```cpp
auto loader = ref new Windows::ApplicationModel::Resources::ResourceLoader();
auto str = loader->GetString("Farewell");
```

**Note** You can only load the value for a simple resource identifier this way, not for a property identifier. So we can load the value for "Farewell" using code like this, but we cannot do so for "Greeting.Text". Trying to do so will return an empty string.

## Refer to a resource identifier from your app package manifest

1. Open your app package manifest (the `Package.appxmanifest` file), in which by default your app's Display name is expressed as a string literal.

   ![add resource, english](images/display-name-before.png)

2. To make a localizable version of this string, open `Resources.resw` and add a new resource with the name "AppDisplayName" and the value "Adventure Works Cycles".

3. Replace the Display name string literal with a reference to the resource identifier that you just created ("AppDisplayName"). You use the `ms-resource` URI (Uniform Resource Identifier) scheme to do this.

   ![add resource, english](images/display-name-after.png)

4. Repeat this process for each string in your manifest that you want to localize. For example, your app's Short name (which you can configure to appear on your app's tile on Start). For a list of all items in the app package manifest that you can localize, see [Localizable manifest items](/uwp/schemas/appxpackage/uapmanifestschema/localizable-manifest-items-win10?branch=master).

## Localize the resources

1. Make a copy of your Resources File (.resw) for another language.
    1. Under "Strings", create a new sub-folder and name it "de-DE" for Deutsch (Deutschland).
   <br>**Note** For the folder name, you can use any [BCP-47 language tag](http://go.microsoft.com/fwlink/p/?linkid=227302). See [How to name resources using qualifiers](https://msdn.microsoft.com/library/windows/apps/xaml/hh965324) for details on the language qualifier and a list of common language tags.
   2. Make a copy of `Strings/en-US/Resources.resw` in the `Strings/de-DE` folder.
2. Translate the resources.
    1. Open `Strings/de-DE/Resources.resw` and translate the values in the Value column. You don't need to translate the comments.

    `Strings/de-DE/Resources.resw`

    ![add resource, german](images/addresource-de-de.png)

If you like, you can repeat steps 1 and 2 for a further language.

`Strings/fr-FR/Resources.resw`
    
![add resource, french](images/addresource-fr-fr.png)

## Test your app

Test the app for your default display language. You can then change the display language in **Settings** > **Time & language** > **Region & language** (or **Language**) and re-test your app. Look at strings in your UI and also in the shell (for example, your title bar&mdash;which is your Display name&mdash;and the Short name on your tiles).

**Note** If a folder name can be found that matches the display language setting, then the resources file inside that folder is loaded. Otherwise, fallback takes place, ending with the resources for your app's default language.

## Factoring strings into multiple Resources Files

You can keep all of your strings in a single Resources File (resw), or you can factor them across multiple Resources Files. For example, you might want to keep your error messages in one Resources File, your app package manifest strings in another, and your UI strings in a third. This is what your folder structure would look like in that case.

![add resource, english](images/manifest-resources.png)

To scope a resource identifier reference to a particular file, you just add `/<resources-file-name>/` before the resource identifier. The markup example below assumes that `ErrorMessages.resw` contains a resource whose name is "PasswordTooWeak.Text" and whose value describes the error.

```XML
<TextBlock x:Uid="/ErrorMessages/PasswordTooWeak"/>
```

The code example below assumes that `ErrorMessages.resw` contains a resource whose name is "MismatchedPasswords" and whose value describes the error.

**C#**
```CSharp
var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
var str = loader.GetString("/ErrorMessages/MismatchedPasswords");
```

**C++**
```cpp
auto loader = ref new Windows::ApplicationModel::Resources::ResourceLoader();
auto str = loader->GetString("/ErrorMessages/MismatchedPasswords");
```

If you were to move your "AppDisplayName" resource out of `Resources.resw` and into `ManifestResources.resw` then in your app package manifest you would change `ms-resource:AppDisplayName` to `ms-resource:/ManifestResources/AppDisplayName`.

You only need to add `/<resources-file-name>/` before the resource identifier for Resources Files *other than* `Resources.resw`. That's because "Resources.resw" is the default file name, so that's what's assumed if you omit a file name (as we did in the earlier examples in this topic).

## Related topics

* [Porting XAML and UI](../porting/wpsl-to-uwp-porting-xaml-and-ui.md#localization-and-globalization)
* [x:Uid directive](../xaml-platform/x-uid-directive.md)
* [attached properties](../xaml-platform/attached-properties-overview.md)
* [Localizable manifest items](/uwp/schemas/appxpackage/uapmanifestschema/localizable-manifest-items-win10?branch=master)
* [BCP-47 language tag](http://go.microsoft.com/fwlink/p/?linkid=227302)
* [How to name resources using qualifiers](https://msdn.microsoft.com/library/windows/apps/xaml/hh965324)
* [How to load string resources](https://msdn.microsoft.com/library/windows/apps/xaml/hh965323)