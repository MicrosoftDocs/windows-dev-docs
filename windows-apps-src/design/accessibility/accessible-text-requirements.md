---
author: Xansky
Description: This topic describes best practices for accessibility of text in an app, by assuring that colors and backgrounds satisfy the necessary contrast ratio.
ms.assetid: BA689C76-FE68-4B5B-9E8D-1E7697F737E6
title: Accessible text requirements
label: Accessible text requirements
template: detail.hbs
ms.author: mhopkins
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Accessible text requirements  




This topic describes best practices for accessibility of text in an app, by assuring that colors and backgrounds satisfy the necessary contrast ratio. This topic also discusses the Microsoft UI Automation roles that text elements in a Universal Windows Platform (UWP) app can have, and best practices for text in graphics.

<span id="contrast_rations"/>
<span id="CONTRAST_RATIONS"/>

## Contrast ratios  
Although users always have the option to switch to a high-contrast mode, your app design for text should regard that option as a last resort. A much better practice is to make sure that your app text meets certain established guidelines for the level of contrast between text and its background. Evaluation of the level of contrast is based on deterministic techniques that do not consider color hue. For example, if you have red text on a green background, that text might not be readable to someone with a color blindness impairment. Checking and correcting the contrast ratio can prevent these types of accessibility issues.

The recommendations for text contrast documented here are based on a web accessibility standard, [G18: Ensuring that a contrast ratio of at least 4.5:1 exists between text (and images of text) and background behind the text](http://go.microsoft.com/fwlink/p/?linkid=221823). This guidance exists in the *W3C Techniques for WCAG 2.0* specification.

To be considered accessible, visible text must have a minimum luminosity contrast ratio of 4.5:1 against the background. Exceptions include logos and incidental text, such as text that is part of an inactive UI component.

Text that is decorative and conveys no information is excluded. For example, if random words are used to create a background, and the words can be rearranged or substituted without changing meaning, the words are considered to be decorative and do not need to meet this criterion.

Use color contrast tools to verify that the visible text contrast ratio is acceptable. See [Techniques for WCAG 2.0 G18 (Resources section)](http://www.w3.org/TR/WCAG20-TECHS/G18.html#G18-resources) for tools that can test contrast ratios.

> [!NOTE]
> Some of the tools listed by Techniques for WCAG 2.0 G18 can't be used interactively with a UWP app. You may need to enter foreground and background color values manually in the tool, or make screen captures of app UI and then run the contrast ratio tool over the screen capture image.

<span id="Text_element_roles"/>
<span id="text_element_roles"/>
<span id="TEXT_ELEMENT_ROLES"/>

## Text element roles  
A UWP app can use these default elements (commonly called *text elements* or *textedit controls*):

* [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/BR209652): role is [**Text**](https://msdn.microsoft.com/library/windows/apps/BR209182)
* [**TextBox**](https://msdn.microsoft.com/library/windows/apps/BR209683): role is [**Edit**](https://msdn.microsoft.com/library/windows/apps/BR209182)
* [**RichTextBlock**](https://msdn.microsoft.com/library/windows/apps/BR227565) (and overflow class [**RichTextBlockOverflow**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.richtextblockoverflow)): role is [**Text**](https://msdn.microsoft.com/library/windows/apps/BR209182)
* [**RichEditBox**](https://msdn.microsoft.com/library/windows/apps/BR227548): role is [**Edit**](https://msdn.microsoft.com/library/windows/apps/BR209182)

When a control reports that is has a role of [**Edit**](https://msdn.microsoft.com/library/windows/apps/BR209182), assistive technologies assume that there are ways for users to change the values. So if you put static text in a [**TextBox**](https://msdn.microsoft.com/library/windows/apps/BR209683), you are misreporting the role and thus misreporting the structure of your app to the accessibility user.

In the text models for XAML, there are two elements that are primarily used for static text, [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/BR209652) and [**RichTextBlock**](https://msdn.microsoft.com/library/windows/apps/BR227565). Neither of these are a [**Control**](https://msdn.microsoft.com/library/windows/apps/BR209390) subclass, and as such neither of them are keyboard-focusable or can appear in the tab order. But that does not mean that assistive technologies can't or won't read them. Screen readers are typically designed to support multiple modes of reading the content in an app, including a dedicated reading mode or navigation patterns that go beyond focus and the tab order, like a "virtual cursor". So don't put your static text into focusable containers just so that tab order gets the user there. Assistive technology users expect that anything in the tab order is interactive, and if they encounter static text there, that is more confusing than helpful. You should test this out yourself with Narrator to get a sense of the user experience with your app when using a screen reader to examine your app's static text.

<span id="Auto-suggest_accessibility"/>
<span id="auto-suggest_accessibility"/>
<span id="AUTO-SUGGEST_ACCESSIBILITY"/>

## Auto-suggest accessibility  
When a user types into an entry field and a list of potential suggestions appears, this type of scenario is called auto-suggest. This is common in the **To:** line of a mail field, the Cortana search box in Windows, the URL entry field in Microsoft Edge, the location entry field in the Weather app, and so on. If you are using a XAML [**AutosuggestBox**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.autosuggestbox) or the HTML intrinsic controls, then this experience is already hooked up for you by default. To make this experience accessible the entry field and the list must be associated. This is explained in the [Implementing auto-suggest](#implementing_auto-suggest) section.

Narrator has been updated to make this type of experience accessible with a special suggestions mode. At a high level, when the edit field and list are connected properly the end user will:

* Know the list is present and when the list closes
* Know how many suggestions are available
* Know the selected item, if any
* Be able to move Narrator focus to the list
* Be able to navigate through a suggestion with all other reading modes

![Suggestion list](images/autosuggest-list.png)<br/>
_Example of a suggestion list_

<span id="Implementing_auto-suggest"/>
<span id="implementing_auto-suggest"/>
<span id="IMPLEMENTING_AUTO-SUGGEST"/>

### Implementing auto-suggest  
To make this experience accessible the entry field and the list must be associated in the UIA tree. This association is done with the [UIA_ControllerForPropertyId](https://msdn.microsoft.com/windows/desktop/ee684017) property in desktop apps or the [ControlledPeers](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.automationproperties.getcontrolledpeers) property in UWP apps.

At a high level there are 2 types of auto-suggest experiences.

**Default selection**  
If a default selection is made in the list, Narrator looks for a  [**UIA_SelectionItem_ElementSelectedEventId**](https://msdn.microsoft.com/library/windows/desktop/ee671223) event in a desktop app, or the [**AutomationEvents.SelectionItemPatternOnElementSelected**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.peers.automationevents) event to be fired in a UWP app. Every time the selection changes, when the user types another letter and the suggestions have been updated or when a user navigates through the list, the **ElementSelected** event should be fired.

![List with a default selection](images/autosuggest-default-selection.png)<br/>
_Example where there is a default selection_

**No default selection**  
If there is no default selection, such as in the Weather app’s location box, then Narrator looks for the desktop [**UIA_LayoutInvalidatedEventId**](https://msdn.microsoft.com/library/windows/desktop/ee671223 ) event or the UWP [**LayoutInvalidated**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.peers.automationevents) event to be fired on the list every time the list is updated.

![List with no default selection](images/autosuggest-no-default-selection.png)<br/>
_Example where there is no default selection_

### XAML implementation  
If you are using the default XAML [**AutosuggestBox**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.autosuggestbox), then everything is already hooked up for you. If you are making your own auto-suggest experience using a [**TextBox**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.textbox) and a list then you will need to set the list as [**AutomationProperties.ControlledPeers**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.automationproperties.getcontrolledpeers) on the **TextBox**. You must fire the **AutomationPropertyChanged** event for the [**ControlledPeers**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.automationproperties.getcontrolledpeers) property every time you add or remove this property and also fire your own [**SelectionItemPatternOnElementSelected**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.peers.automationevents) events or [**LayoutInvalidated**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.peers.automationevents) events depending on your type of scenario, which was explained previously in this article.

### HTML implementation  
If you are using the intrinsic controls in HTML, then the UIA implementation has already been mapped for you. Below is an example of an implementation that is already hooked up for you:

``` HTML
<label>Sites <input id="input1" type="text" list="datalist1" /></label>
<datalist id="datalist1">
        <option value="http://www.google.com/" label="Google"></option>
        <option value="http://www.reddit.com/" label="Reddit"></option>
</datalist>
```

 If you are creating your own controls, you must set up your own ARIA controls, which are explained in the W3C standards.

<span id="Text_in_graphics"/>
<span id="text_in_graphics"/>
<span id="TEXT_IN_GRAPHICS"/>

## Text in graphics  
Whenever possible, avoid including text in a graphic. For example, any text that you include in the image source file that is displayed in the app as an [**Image**](https://msdn.microsoft.com/library/windows/apps/BR242752) element is not automatically accessible or readable by assistive technologies. If you must use text in graphics, make sure that the [**AutomationProperties.Name**](https://msdn.microsoft.com/library/windows/apps/Hh759770) value that you provide as the equivalent of "alt text" includes that text or a summary of the text's meaning. Similar considerations apply if you are creating text characters from vectors as part of a [**Path**](/uwp/api/Windows.UI.Xaml.Shapes.Path), or by using [**Glyphs**](https://msdn.microsoft.com/library/windows/apps/BR209921).

<span id="Text_font_size"/>
<span id="text_font_size"/>
<span id="TEXT_FONT_SIZE"/>

## Text font size  
Many readers have difficulty reading text in an app when that text is using a text font size that's simply too small for them to read. You can prevent this issue by making the text in your app's UI reasonably large in the first place. There are also assistive technologies that are part of Windows, and these enable users to change the view sizes of apps, or the display in general.

* Some users change dots per inch (dpi) values of their primary display as an accessibility option. That option is available from **Make things on the screen larger** in **Ease of Access**, which redirects to a **Control Panel** UI for **Appearance and Personalization** / **Display**. Exactly which sizing options are available can vary because this depends on the capabilities of the display device.
* The Magnifier tool can enlarge a selected area of the UI. However, it's difficult to use the Magnifier tool for reading text.

<span id="Text_scale_factor"/>
<span id="text_scale_factor"/>
<span id="TEXT_SCALE_FACTOR"/>

## Text scale factor  
Various text elements and controls have an [**IsTextScaleFactorEnabled**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.textblock.istextscalefactorenabled) property. This property has the value **true** by default. When its value is **true**, the setting called **Text scaling** on the phone (**Settings &gt; Ease of access**), causes the text size of text in that element to be scaled up. The scaling will affect text that has a small **FontSize** to a greater degree than it will affect text that has a large **FontSize**. But you can disable that automatic enlargement by setting an element's **IsTextScaleFactorEnabled** property to **false**. Try this markup, adjust the **Text size** setting on the phone, and see what happens to the **TextBlock**s:

XAML
```xml
<TextBlock Text="In this case, IsTextScaleFactorEnabled has been left set to its default value of true."
    Style="{StaticResource BodyTextBlockStyle}"/>

<TextBlock Text="In this case, IsTextScaleFactorEnabled has been set to false."
    Style="{StaticResource BodyTextBlockStyle}" IsTextScaleFactorEnabled="False"/>
```  

Please don't disable automatic enlargement routinely, though, because scaling UI text universally across all apps is an important accessibility experience for users and they will expect it to work in your app too.

You can also use the [**TextScaleFactorChanged**](https://msdn.microsoft.com/library/windows/apps/Dn633867) event and the [**TextScaleFactor**](https://msdn.microsoft.com/library/windows/apps/Dn633866) property to find out about changes to the **Text size** setting on the phone. Here’s how:

C#
```csharp
{
    ...
    var uiSettings = new Windows.UI.ViewManagement.UISettings();
    uiSettings.TextScaleFactorChanged += UISettings_TextScaleFactorChanged;
    ...
}

private async void UISettings_TextScaleFactorChanged(Windows.UI.ViewManagement.UISettings sender, object args)
{
    var messageDialog = new Windows.UI.Popups.MessageDialog(string.Format("It's now {0}", sender.TextScaleFactor), "The text scale factor has changed");
    await messageDialog.ShowAsync();
}
```

The value of **TextScaleFactor** is a double in the range \[1,2\]. The smallest text is scaled up by this amount. You might be able to use the value to, say, scale graphics to match the text. But remember that not all text is scaled by the same factor. Generally speaking, the larger text is to begin with, the less it’s affected by scaling.

These types have an **IsTextScaleFactorEnabled** property:  
* [**ContentPresenter**](https://msdn.microsoft.com/library/windows/apps/BR209378)
* [**Control**](https://msdn.microsoft.com/library/windows/apps/BR209390) and derived classes
* [**FontIcon**](https://msdn.microsoft.com/library/windows/apps/Dn279514)
* [**RichTextBlock**](https://msdn.microsoft.com/library/windows/apps/BR227565)
* [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/BR209652)
* [**TextElement**](https://msdn.microsoft.com/library/windows/apps/BR209967) and derived classes

<span id="related_topics"/>

## Related topics  
* [Accessibility](accessibility.md)
* [Basic accessibility information](basic-accessibility-information.md)
* [XAML text display sample](http://go.microsoft.com/fwlink/p/?linkid=238579)
* [XAML text editing sample](http://go.microsoft.com/fwlink/p/?linkid=251417)
* [XAML accessibility sample](http://go.microsoft.com/fwlink/p/?linkid=238570) 
