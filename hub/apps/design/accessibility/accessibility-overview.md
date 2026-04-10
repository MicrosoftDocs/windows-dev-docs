---
description: This article is an overview of the concepts and technologies related to accessibility scenarios for Windows apps.
ms.assetid: AA053196-F331-4CBE-B032-4E9CBEAC699C
title: Accessibility overview
label: Accessibility overview
template: detail.hbs
ms.date: 03/17/2026
ms.topic: concept-article
keywords: windows 11, winui, winappsdk, windows app sdk
ms.localizationpriority: medium
---

# Accessibility overview

This topic introduces concepts and technologies related to building accessible Windows apps.

Accessible Windows applications support not only people with disabilities (both temporary and permanent) but also those with personal preferences, specific work styles, or situational and environmental constraints (such as shared work spaces, low bandwidth, bright sunlight, noisy or quiet surroundings, while cooking, and so on).

The guidance is written for development teams that treat accessibility as a core quality requirement and use automated accessibility checks as part of regular engineering workflows. Use these topics to define accessibility expectations early, validate them continuously, and prevent regressions as features evolve.

Many disabilities, including impaired mobility, vision, color perception, hearing, speech, cognition, and literacy, can be addressed by providing:

- Support for keyboard interactions and screen readers.
- Support for user customization, such as font, zoom setting (magnification), color, and high-contrast settings.
- Alternative or supplemental UI.

XAML controls provide built-in support for keyboard access and assistive technologies such as screen readers. This built-in support enables a basic level of accessibility that you can customize through various properties. If you are creating your own custom XAML components and controls, you can also add similar support to those controls by using an *automation peer- (for more info, see [Custom automation peers](custom-automation-peers.md)).

In addition, data binding, style, and template features let you dynamically support alternative UI and changes to display settings.

## UI Automation

Accessibility support comes primarily from the integration of the Microsoft UI Automation framework. That support is provided through base classes and the built-in behavior of the class implementation for control types, as well as an interface representation of the UI Automation provider API. Each control class uses the UI Automation concepts of automation peers and automation patterns that report the control's role and content to UI Automation clients. The app is treated as a top-level window by UI Automation, and through the UI Automation framework all the accessibility-relevant content within that app window is available to a UI Automation client. For more info about UI Automation, see [UI Automation Overview](/windows/desktop/WinAuto/uiauto-uiautomationoverview).

## Assistive technology

Many accessibility needs are met by assistive technology products installed by the user or by tools and settings provided by the operating system. This includes screen readers, screen magnifiers, and high-contrast settings.

Assistive technology products include a wide variety of software and hardware. These products work through the standard keyboard interface and accessibility frameworks that report information about the content and structure of the UI to screen readers and other assistive technologies. Examples of assistive technology products include:

- The On-Screen Keyboard (OSK), or software keyboard, which lets people use a pointer instead of a hardware keyboard to type text.
- Voice-recognition software, which converts spoken words into typed text.
- Screen readers, which convert text into spoken words or formats like Braille.
- The Narrator screen reader, built into Windows, has a touch mode that can perform screen reading tasks through touch gestures.
- Programs or settings that adjust the display, for example high contrast themes, dots per inch (dpi), or the Magnifier tool.

Apps that have good keyboard and screen reader support usually work well with various assistive technology products. In many cases, a Windows app works with these products without additional modification of information or structure. However, you may want to modify some settings for optimal accessibility experience or to implement additional support.

See [Accessibility testing](accessibility-testing.md) for how to test basic accessibility scenarios with assistive technologies.

## Screen reader support and basic accessibility information

Screen readers provide access to text withiin an app by rendering it in another format, such as spoken language or Braille output. The exact behavior of a screen reader depends on the software how the user configured it.

For example, some screen readers read the entire UI when the user starts or switches to an app, which enables the user to receive all informational content before attempting to navigate it. Some screen readers also read the text associated with an individual control when it receives focus during tab navigation. This enables users to orient themselves as they navigate among the input controls of an application. Narrator is an example of a screen reader that supports both behaviors.

The most important information that a screen reader or any other assistive technology needs in order to help users understand or navigate an app is an **accessible name**  for each element in an app. In many cases, a control or element already has an accessible name derived from other property values, such as an element that supports and displays inner text. For other elements, you might need to provide an accessible name through the element structure. And sometimes you need to explicitly provide the accessible name. For more details on how these derived values work in common UI elements, and for more info about accessible names in general, see [Expose basic accessibility information](basic-accessibility-information.md).

There are several other automation properties available (including the keyboard properties described in the next section). However, not all screen readers support all automation properties, so you should test all appropriate automation properties with a variety of screen readers.

## Keyboard support

Building keyboard accessibility (for traditional, modified, or keyboard emulation hardware) into your app, helps users who are blind, have low vision, or have motor control issues, to navigate through and use the full functionality of your app. It also lets users without disabilities choose the keyboard for navigation due to preference or efficiency.

The basic XAML control model provides built-in keyboard support including tab navigation, text input, and control-specific support. The elements that serve as layout containers (such as panels) use the layout order to establish a default tab order and provide an accessible representation of the UI. [ListView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) and [GridView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.gridview) controls provide built-in arrow-key navigation. [Button](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.button) controls handle the Spacebar or Enter keys for button activation.

For more info about all the aspects of keyboard support, including tab order and key-based activation or navigation, see [Keyboard accessibility](keyboard-accessibility.md).

## Media and captioning

You typically display audio-visual media through a [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) object. For accessibility purposes, provide controls that enable users to play, pause, and stop the media, and also inlcude captions or alternative audio tracks.

## Accessible text

When you design and implement text in your app, focus on these three core accessibility requirements.

- Assistive technologies must be able to determine whether text should be read during tab-sequence navigation or as part of the overall document content. You can influence this behavior by choosing the correct text element and setting its properties appropriately. Because each text element maps to a specific purpose and often to a corresponding UI Automation role, using the wrong element can expose the wrong role and create a confusing experience for assistive technology users.
- Many users have low vision or color-vision deficiencies and cannot read text reliably unless there is sufficient contrast between text and background. This impact can be easy to underestimate during design. Following established contrast guidance helps prevent unreadable text for these users. For more info, see [Accessible text requirements](accessible-text-requirements.md).
- Text that is too small is difficult for many users to read. Start with a reasonable default text size, and ensure your app works correctly with system text and display scaling. This is especially important in UIs that contain large amounts of text or dense visual layouts.

## Supporting high-contrast themes

XAML controls get their visual appearance from theme resource dictionaries, including high-contrast theme resources. When a user enables a high-contrast mode, controls automatically resolve to the appropriate high-contrast resources. To preserve this behavior, avoid styling approaches that block theme resource lookup, such as hard-coded colors or explicit styles that prevent high-contrast theme values from overriding your custom values. For more info, see [High-contrast themes](high-contrast-themes.md).

## Design for alternative UI

In many cases, you can communicate essential information more effectively by using multiple cues. For example, combine icons with color to support users with color-vision deficiencies, and pair visual alerts with sound to support users who are deaf or hard of hearing.

When needed, provide an alternative accessible UI that removes nonessential visuals and animation and simplifies interaction flows. The following code example shows how to switch one [UserControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.usercontrol) for another based on a user setting.

```xaml
<StackPanel x:Name="LayoutRoot" Background="White">

  <CheckBox x:Name="ShowAccessibleUICheckBox" Click="ShowAccessibleUICheckBox_Click">
    Show Accessible UI
  </CheckBox>

  <ContentControl x:Name="ContentBlock">
    <local:ContentPage/>
  </ContentControl>

</StackPanel>
```

```csharp
private void ShowAccessibleUICheckBox_Click(object sender, RoutedEventArgs e)
{
    if (sender is CheckBox checkBox)
    {
        ContentBlock.Content = checkBox.IsChecked == true
            ? new AccessibleContentPage()
            : new ContentPage();
    }
}
```

## Assistive technology support in custom controls

When you create a custom control, implement or extend one or more [AutomationPeer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer) subclasses to provide accessibility support. In some cases, reusing the same peer class as the base control can provide acceptable baseline automation behavior for your derived control. However, you should validate this through testing, and in most cases you should still implement a dedicated peer so it can report the correct class name and behavior for your custom control. For implementation guidance, see [Custom automation peers](custom-automation-peers.md).

## Assistive technology support in apps that support XAML / Microsoft DirectX interop

By default, Microsoft DirectX content that's hosted in a XAML UI (using [SwapChainPanel](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.swapchainpanel) or [SurfaceImageSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.surfaceimagesource)) is not accessible. The [XAML SwapChainPanel DirectX interop sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208.1%20Store%20app%20samples/%5BC%23%5D-Windows%208.1%20Store%20app%20samples/XAML%20SwapChainPanel%20DirectX%20interop%20sample) (archived legacy sample) shows how to make hosted content accessible through UI Automation by creating an AutomationPeer for the DirectX content. This technique makes the hosted content accessible through UI Automation.

## Verification and publishing

Treat accessibility verification as an ongoing engineering activity, not a final QA pass. Run automated checks for every build where possible, and pair them with targeted manual assistive technology validation for critical user journeys.

For more info about accessibility declarations and publishing your app, see [Accessibility in the Store](accessibility-in-the-store.md).

## Examples

> [!div class="nextstepaction"]
> Open the WinUI 3 Gallery app and see the following Accessibility principles in action:
> - [Screen reader support](winui3gallery://item/AccessibilityScreenReader)
> - [Keyboard support](winui3gallery://item/AccessibilityKeyboard)
> - [Color contrast](winui3gallery://item/AccessibilityColorContrast)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]


## Related topics

- [Microsoft.UI.Xaml.Automation](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation)
- [Microsoft.UI.Xaml.Automation](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation)
- [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample) (archived legacy sample)
- [Accessibility overview](accessibility-overview.md)
- [Get started with Narrator](https://support.microsoft.com/help/22798/windows-10-complete-guide-to-narrator)
