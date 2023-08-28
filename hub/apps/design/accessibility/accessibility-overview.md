---
description: This article is an overview of the concepts and technologies related to accessibility scenarios for Windows apps.
ms.assetid: AA053196-F331-4CBE-B032-4E9CBEAC699C
title: Accessibility overview
label: Accessibility overview
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Accessibility overview

This article is an overview of the concepts and technologies related to accessibility scenarios for Windows apps.

## Accessibility and your app

There are many possible disabilities or impairments, including limitations in mobility, vision, color perception, hearing, speech, cognition, and literacy. However, you can address most requirements by following the guidelines offered here. This means providing:

* Support for keyboard interactions and screen readers.
* Support for user customization, such as font, zoom setting (magnification), color, and high-contrast settings.
* Alternatives or supplements for parts of your UI.

Controls for XAML provide built-in keyboard support and support for assistive technologies such as screen readers, which take advantage of accessibility frameworks that already support UWP apps, HTML, and other UI technologies. This built-in support enables a basic level of accessibility that you can customize with very little work, by setting just a handful of properties. If you are creating your own custom XAML components and controls, you can also add similar support to those controls by using the concept of an *automation peer*.

In addition, data binding, style, and template features make it easy to implement support for dynamic changes to display settings and text for alternative UIs.

## UI Automation

Accessibility support comes primarily from the integrated support for the Microsoft UI Automation framework. That support is provided through base classes and the built-in behavior of the class implementation for control types, and an interface representation of the UI Automation provider API. Each control class uses the UI Automation concepts of automation peers and automation patterns that report the control's role and content to UI Automation clients. The app is treated as a top-level window by UI Automation, and through the UI Automation framework all the accessibility-relevant content within that app window is available to a UI Automation client. For more info about UI Automation, see [UI Automation Overview](/windows/desktop/WinAuto/uiauto-uiautomationoverview).

## Assistive technology

Many user accessibility needs are met by assistive technology products installed by the user or by tools and settings provided by the operating system. This includes functionality such as screen readers, screen magnification, and high-contrast settings.

Assistive technology products include a wide variety of software and hardware. These products work through the standard keyboard interface and accessibility frameworks that report information about the content and structure of a UI to screen readers and other assistive technologies. Examples of assistive technology products include:

* The On-Screen Keyboard, which enables people to use a pointer in place of a keyboard to type text.
* Voice-recognition software, which converts spoken words into typed text.
* Screen readers, which convert text into spoken words or other forms such as Braille.
* The Narrator screen reader, which is specifically part of Windows. Narrator has a touch mode, which can perform screen reading tasks by processing touch gestures, for when there is no keyboard available.
* Programs or settings that adjust the display or areas of it, for example high contrast themes, dots per inch (dpi) settings of the display, or the Magnifier tool.

Apps that have good keyboard and screen reader support usually work well with various assistive technology products. In many cases, a UWP app works with these products without additional modification of information or structure. However, you may want to modify some settings for optimal accessibility experience or to implement additional support.

Some of the options that you can use for testing basic accessibility scenarios with assistive technologies are listed in [Accessibility testing](accessibility-testing.md).

## Screen reader support and basic accessibility information

Screen readers provide access to the text in an app by rendering it in some other format, such as spoken language or Braille output. The exact behavior of a screen reader depends on the software and on the user's configuration of it.

For example, some screen readers read the entire app UI when the user starts or switches to the app being viewed, which enables the user to receive all of the available informational content before attempting to navigate it. Some screen readers also read the text associated with an individual control when it receives focus during tab navigation. This enables users to orient themselves as they navigate among the input controls of an application. Narrator is an example of a screen reader that provides both behaviors, depending on user choice.

The most important information that a screen reader or any other assistive technology needs in order to help users understand or navigate an app is an *accessible name* for the element parts of the app. In many cases, a control or element already has an accessible name that is calculated from other property values that you have otherwise provided. The most common case in which you can use an already-calculated name is with an element that supports and displays inner text. For other elements, you sometimes need to account for other ways to provide an accessible name by following best practices for element structure. And sometimes you need to provide a name that is explicitly intended as the accessible name for app accessibility. For a listing of how many of these calculated values work in common UI elements, and for more info about accessible names in general, see [Basic accessibility information](basic-accessibility-information.md).

There are several other automation properties available (including the keyboard properties described in the next section). However, not all screen readers support all automation properties. In general, you should set all appropriate automation properties and test to provide the widest possible support for screen readers.

## Keyboard support

To provide good keyboard support, you must ensure that every part of your application can be used with a keyboard. If your app uses mostly the standard controls and doesn't use any custom controls, you are most of the way there already. The basic XAML control model provides built-in keyboard support including tab navigation, text input, and control-specific support. The elements that serve as layout containers (such as panels) use the layout order to establish a default tab order. That order is often the correct tab order to use for an accessible representation of the UI. If you use [**ListBox**](/uwp/api/Windows.UI.Xaml.Controls.ListBox) and [**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView) controls to display data, they provide built-in arrow-key navigation. Or if you use a [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) control, it already handles the Spacebar or Enter keys for button activation.

For more info about all the aspects of keyboard support, including tab order and key-based activation or navigation, see [Keyboard accessibility](keyboard-accessibility.md).

## Media and captioning

You typically display audiovisual media through a [**MediaElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaElement) object. You can use **MediaElement** APIs to control the media playback. For accessibility purposes, provide controls that enable users to play, pause, and stop the media as needed. Sometimes, media includes additional components that are intended for accessibility, such as captioning or alternative audio tracks that include narrative descriptions.

## Accessible text

Three main aspects of text are relevant to accessibility:

* Tools must determine whether the text is to be read as part of a tab-sequence traversal or only as part of an overall document representation. You can help control this determination by choosing the appropriate element for displaying the text or by adjusting properties of those text elements. Each text element has a specific purpose, and that purpose often has a corresponding UI Automation role. Using the wrong element can result in reporting the wrong role to UI Automation and creating a confusing experience for an assistive technology user.
* Many users have sight limitations that make it difficult for them to read text unless it has adequate contrast against the background. How this impacts the user is not intuitive for app designers who do not have that sight limitation. For example, for color-blind users, poor color choices in the design can prevent some users from being able to read the text. Accessibility recommendations that were originally made for web content define standards for contrast that can avoid these problems in apps as well. For more info, see [Accessible text requirements](accessible-text-requirements.md).
* Many users have difficulty reading text that is simply too small. You can prevent this issue by making the text in your app's UI reasonably large in the first place. However, that's challenging for apps that display large quantities of text, or text interspersed with other visual elements. In such cases, make sure that the app correctly interacts with the system features that can scale up the display, so that any text in apps scales up along with it. (Some users change dpi values as an accessibility option. That option is available from **Make things on the screen larger** in **Ease of Access**, which redirects to a **Control Panel** UI for **Appearance and Personalization** / **Display**.)

## Supporting high-contrast themes

UI controls use a visual representation that is defined as part of a XAML resource dictionary of themes. One or more of these themes is specifically used when the system is set for high contrast. When the user switches to high contrast, by looking up the appropriate theme from a resource dictionary dynamically, all your UI controls will use an appropriate high-contrast theme too. Just make sure that you haven't disabled the themes by specifying an explicit style or using another styling technique that prevents the high-contrast themes from loading and overriding your style changes. For more info, see [High-contrast themes](high-contrast-themes.md).

## Design for alternative UI

When you design your apps, consider how they may be used by people with limited mobility, vision, and hearing. Because assistive technology products make extensive use of standard UI, it is particularly important to provide good keyboard and screen-reader support even if you make no other adjustments for accessibility.

In many cases, you can convey essential information by using multiple techniques to widen your audience. For example, you can highlight information using both icon and color information to help users who are color blind, and you can display visual alerts along with sound effects to help users who are deaf or hard of hearing.

If necessary, you can provide alternative, accessible user interface elements that completely remove nonessential elements and animations, and provide other simplifications to streamline the user experience. The following code example demonstrates how to display one [**UserControl**](/uwp/api/Windows.UI.Xaml.Controls.UserControl) instance in place of another depending on a user setting.

XAML

```xml
<StackPanel x:Name="LayoutRoot" Background="White">

  <CheckBox x:Name="ShowAccessibleUICheckBox" Click="ShowAccessibleUICheckBox_Click">
    Show Accessible UI
  </CheckBox>

  <UserControl x:Name="ContentBlock">
    <local:ContentPage/>
  </UserControl>

</StackPanel>
```

Visual Basic

```vb
Private Sub ShowAccessibleUICheckBox_Click(ByVal sender As Object,
    ByVal e As RoutedEventArgs)

    If (ShowAccessibleUICheckBox.IsChecked.Value) Then
        ContentBlock.Content = New AccessibleContentPage()
    Else
        ContentBlock.Content = New ContentPage()
    End If
End Sub
```

C#

```csharp
private void ShowAccessibleUICheckBox_Click(object sender, RoutedEventArgs e)
{
    if ((sender as CheckBox).IsChecked.Value)
    {
        ContentBlock.Content = new AccessibleContentPage();
    }
    else
    {
        ContentBlock.Content = new ContentPage();
    }
}
```

## Verification and publishing

For more info about accessibility declarations and publishing your app, see [Accessibility in the Store](accessibility-in-the-store.md).

> [!NOTE]
> Declaring the app as accessible is only relevant to the Microsoft Store.

## Assistive technology support in custom controls

When you create a custom control, we recommend that you also implement or extend one or more [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer) subclasses to provide accessibility support. In some cases, so long as you use the same peer class as was used by the base control class, the automation support for your derived class is adequate at a basic level. However, you should test this, and implementing a peer is still recommended as a best practice so that the peer can correctly report the class name of your new control class. Implementing a custom automation peer has a few steps involved. For more info, see [Custom automation peers](custom-automation-peers.md).

## Assistive technology support in apps that support XAML / Microsoft DirectX interop

Microsoft DirectX content that's hosted in a XAML UI (using [**SwapChainPanel**](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) or [**SurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource)) is not accessible by default. The [XAML SwapChainPanel DirectX interop sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208.1%20Store%20app%20samples/%5BC%23%5D-Windows%208.1%20Store%20app%20samples/XAML%20SwapChainPanel%20DirectX%20interop%20sample) shows how to create UI Automation peers for the hosted DirectX content. This technique makes the hosted content accessible through UI Automation.

## Examples

> [!TIP]
> [!div class="nextstepaction"]
> Open the WinUI 3 Gallery app and see the following Accessibility principles in action:
> - [Screen reader support](winui3gallery://item/AccessibilityScreenReader)
> - [Keyboard support](winui3gallery://item/AccessibilityKeyboard)
> - [Color contrast](winui3gallery://item/AccessibilityColorContrast)

> The **WinUI 3 Gallery** app includes interactive examples of most WinUI 3 controls, features, and functionality. Get the app from the [Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) or get the source code on [GitHub](https://github.com/microsoft/WinUI-Gallery)

## Related topics


* [**Windows.UI.Xaml.Automation**](/uwp/api/Windows.UI.Xaml.Automation)
* [Design for accessibility]()
* [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample)
* [Accessibility](accessibility.md)
* [Get started with Narrator](https://support.microsoft.com/help/22798/windows-10-complete-guide-to-narrator)
