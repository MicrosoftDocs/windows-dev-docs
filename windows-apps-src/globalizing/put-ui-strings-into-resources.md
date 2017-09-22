---
author: stevewhims
Description: If you want your app to support different display languages, and you have string literals in your code or in your XAML markup, then move those strings into a Resources File (.resw). You can then make a translated copy of that Resources File for each language that your app supports.
title: Localize your UI strings
ms.assetid: E420B9BB-C0F6-4EC0-BA3A-BA2875B69722
label: Localize your UI strings
template: detail.hbs
ms.author: stwhi
ms.date: 10/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Localize your UI strings
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

If you want your app to support different display languages, and you have string literals in your code or in your XAML markup, then move those strings into a Resources File (.resw). You can then make a translated copy of that Resources File for each language that your app supports.

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**ApplicationModel.Resources.ResourceLoader**](https://msdn.microsoft.com/library/windows/apps/br206014)</li>
<li>[**WinJS.Resources.processAll**](https://msdn.microsoft.com/library/windows/apps/br211864)</li>
</ul>
</div>

## Put your strings in a Resources File (.resw) instead of in your code or markup.

1. Set your app's default language.
    1. With your solution open in Visual Studio, open package.appxmanifest.
    2. On the **Application** tab, confirm that the Default language is set appropriately (for example, "en" or "en-US"). The remaining steps will assume that you have set the default language to "en-US".
    <br>**Note** At a minimum, you need to provide resources localized for this default language. Those are the resources that will be loaded if no better match can be found for the user's preferred language or display language settings.
2. Create a Resources File (.resw) for the default language.
    1. Under your project node, create a new folder and name it "Strings".
    2. Under "Strings", create a new sub-folder and name it "en-US".
    3. Under "en-US", create a new Resources File (.resw) and confirm that it's named "Resources.resw".
    <br>**Note** If you have .NET Resources Files (.resx) that you want to port, see [Porting XAML and UI](../porting/wpsl-to-uwp-porting-xaml-and-ui.md#localization-and-globalization).
3.  Open the file and add these resources.

    Strings/en-US/Resources.resw

    ![add resource, english](images/addresource-en-us.png)

    In this example, "Greeting" is a resource identifier that you can refer to from your markup, as you'll see. For the identifier "Greeting", a string is provided for a Text property, and a string is provided for a Width property. You could also set "Greeting.Foreground" to "Red", for example. The "Farewell" identifier is simpler; it has no sub-properties, and it can be loaded from code. The Comment column is a good place to provide any special instructions to translators.

## Refer to a resource identifier from markup

You use an [x:Uid directive](../xaml-platform/x-uid-directive.md) to associate a control in your markup with a resource identifier.

```XML
<TextBlock x:Uid="Greeting"/>
```

At run-time, `Strings/en-US/Resources.resw` is loaded (since right now that's the only resources file in the project). The **x:Uid** directive causes a lookup to take place, keyed on the "Greeting" resource identifier. Then, the ".Text" and ".Width" values for that identifier are applied to the **TextBlock**. The ".Foreground" value will be applied, too, if you added that. Instead of setting **Width** from a resources file, you'll probably want to allow controls to dynamically size to content.

Note that [attached properties](../xaml-platform/attached-properties-overview.md) need a special syntax in the Name column of a .resw file. For example, to set a value for the [AutomationProperties.Name](/uwp/api/windows.ui.xaml.automation.automationproperties?branch=master#Windows_UI_Xaml_Automation_AutomationProperties_NameProperty) attached property for the "Greeting" identifier, this is what you would enter in the Name column.

```XML
Greeting.[using:Windows.UI.Xaml.Automation]AutomationProperties.Name
```

## Refer to a resource identifier from code

You can explicitly load a resource based on a simple identifier.

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

## Localize the resources

1. Make a copy of your Resources File (.resw) for another language.
    1. Under "Strings", create a new sub-folder and name it "de-DE" for Deutsch (Deutschland).
   <br>**Note** For the folder name, you can use any [BCP-47 language tag](http://go.microsoft.com/fwlink/p/?linkid=227302). See [How to name resources using qualifiers](https://msdn.microsoft.com/library/windows/apps/xaml/hh965324) for details on the language qualifier and a list of common language tags.
   2. Make a copy of `Strings/en-US/Resources.resw` in the `Strings/de-DE` folder.
2. Translate the resources.
    1. Open `Strings/de-DE/Resources.resw` and translate the values in the Value column. You don't need to translate the comments.

    Strings/de-DE/Resources.resw

    ![add resource, german](images/addresource-de-de.png)

If you like, you can repeat steps 1 and 2 for a further language.

Strings/fr-FR/Resources.resw
    
![add resource, french](images/addresource-fr-fr.png)

## Test your app

Test the app for your default display language. You can then change the display language in **Settings** > **Time & language** > **Region & language** (or **Language**) and re-test your app.

If a folder name can be found that matches the display language setting, then the resources file inside that folder is loaded. Otherwise, fallback takes place, ending with the resources for your app's default language.

## Related topics

* [Porting XAML and UI](../porting/wpsl-to-uwp-porting-xaml-and-ui.md#localization-and-globalization)
* [x:Uid directive](../xaml-platform/x-uid-directive.md)
* [attached properties](../xaml-platform/attached-properties-overview.md)
* [BCP-47 language tag](http://go.microsoft.com/fwlink/p/?linkid=227302)
* [How to name resources using qualifiers](https://msdn.microsoft.com/library/windows/apps/xaml/hh965324)
* [How to load string resources](https://msdn.microsoft.com/library/windows/apps/xaml/hh965323)