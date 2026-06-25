---
description: Accessibility fundamentals map to name, role, and value. This topic shows how to expose these properties in your app so assistive technologies can interpret your UI correctly.
ms.assetid: 9641C926-68C9-4842-8B55-C38C39A9E5C5
title: Expose basic accessibility information
label: Expose basic accessibility information
template: detail.hbs
ms.date: 03/17/2026
ms.topic: how-to
keywords: windows 11, winui, winappsdk, windows app sdk
ms.localizationpriority: medium
---

# Expose basic accessibility information

Accessibility fundamentals map to name, role, and value. This topic shows how to expose these properties in your app so assistive technologies can interpret your UI correctly.

## Accessible name

An accessible name is the label a screen reader announces for a UI element. Set it on elements that convey meaning or support interaction, including images, input fields, buttons, controls, and regions.

The following table describes how to define or obtain an accessible name for various types of elements in a XAML UI.

| Element type | Description |
|--------------|-------------|
| Static text | For [**TextBlock**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock) and [**RichTextBlock**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock) elements, an accessible name is automatically determined from the visible (inner) text. All of the text in that element is used as the name. See [Name from inner text](#name-from-inner-text). |
| Images | The XAML [**Image**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image) element does not have a direct analog to the HTML **alt** attribute of **img** and similar elements. Either use [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) to provide a name, or use the captioning technique. See [Accessible names for images](#accessible-names-for-images). |
| Form elements | The accessible name for a form element should be the same as the label that is displayed for that element. See [Labels and LabeledBy](#labels-and-labeledby). |
| Buttons and links | By default, the accessible name of a button or link is based on the visible text, using the same rules as described in [Name from inner text](#name-from-inner-text). In cases where a button contains only an image, use [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) to provide a text-only equivalent of the button's intended action. |

Most container elements (such as panels) do not expose an accessible name. In UI Automation, the meaningful child items should provide the name and role, while the container primarily exposes structure for traversal.

## Role and value

XAML controls expose role (and, when applicable, value) through their built-in UI Automation support. Inspect these properties with UI Automation tools or in each control's [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer) documentation. Roles map to [**AutomationControlType**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationcontroltype) and are surfaced to assistive technologies through the control's **AutomationPeer**.

Only controls with value semantics expose a UI Automation value. For example, [**TextBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox) supports [**IValueProvider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.ivalueprovider) via [**TextBoxAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.textboxautomationpeer), so assistive technologies can detect and read its current value.

> [!NOTE]
> If you set [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) explicitly, do not repeat role/type terms such as "button" or "list" in the name. Role/type comes from **LocalizedControlType**, and many assistive technologies append it to the name. Repeating role text can produce output like "button button". Validate this behavior with Narrator.

## Influencing the UI Automation tree views

UI Automation represents element relationships in three tree views: raw, control, and content. Each view serves a different purpose. The raw view includes nearly all automation elements, the control view emphasizes interactive controls and structural navigation points, and the content view focuses on elements that communicate user-facing content. In practice, assistive technologies and accessibility inspection tools most often rely on the control view because it provides the most useful balance between completeness and usability.

By default, most [**Control**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control)-derived elements appear in the control view when your app is exposed through UI Automation. In composed UIs, this can introduce duplicate or low-value nodes that add noise for assistive technology users. Use [**AutomationProperties.AccessibilityView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.accessibilityviewproperty) to control how specific elements are exposed across the tree views. For example, placing an element in **Raw** typically keeps it available for diagnostic and traversal scenarios while excluding it from the primary views used by many assistive technologies. To review real-world patterns, inspect control templates in generic.xaml and search for **AutomationProperties.AccessibilityView**.

## Name from inner text

Many XAML controls can derive a default accessible name from text that is already visible in the UI. This behavior reduces the need to set [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) explicitly for common text-based patterns, and helps keep what users hear aligned with what they see.

* [**TextBlock**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock), [**RichTextBlock**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock), and [**TextBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox) typically promote their text content as the default accessible name.
* [**ContentControl**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol.content) subclasses evaluate their [**Content**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol.content) value and use an iterative "ToString" strategy to extract string content for the default accessible name.

> [!NOTE]
> UI Automation enforces a 2048-character maximum for the accessible name. If automatic name generation produces a longer string, the value is truncated.

## Accessible names for images

For non-text content such as images and charts, provide a text alternative so screen readers can identify and announce the element correctly. Because these elements typically do not expose inner text, UI Automation cannot derive a default accessible name automatically. (Purely decorative or structural visuals are an exception and generally should not be named.) When a meaningful image needs to be announced, set [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) explicitly, as shown in the following example.

```xaml
<Image
    Source="Assets/product.png"
    AutomationProperties.Name="Customer using the product" />
```

As an alternative, expose a visible caption and associate it to the image through [**AutomationProperties.LabeledBy**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.labeledbyproperty). This keeps the spoken label aligned with on-screen text and avoids duplicating strings in markup. The following WinUI example shows this pattern:

```xaml
<StackPanel Spacing="8">
    <Image
        x:Name="heroImage"
        Width="480"
        Source="Assets/snoqualmie-NF.jpg"
        AutomationProperties.LabeledBy="{Binding ElementName=heroCaption}" />
    <TextBlock x:Name="heroCaption" Text="Mount Snoqualmie Skiing" />
</StackPanel>
```

## Labels and LabeledBy

For form fields, the preferred labeling pattern is to define visible label text in a [**TextBlock**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock) and reference that element from the input control through [**AutomationProperties.LabeledBy**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.labeledbyproperty). This creates an association between the UI label and the control in the automation tree, so assistive technologies can announce a field name that matches what is shown on screen. This pattern is generally more maintainable than duplicating label text in multiple properties because the same source string drives both visual and accessible labeling.

```xaml
<StackPanel x:Name="LayoutRoot" Spacing="12">
    <StackPanel Orientation="Horizontal" Spacing="8">
        <TextBlock x:Name="firstNameLabel" Text="First name" />
        <TextBox
            x:Name="firstNameTextBox"
            Width="180"
            AutomationProperties.LabeledBy="{Binding ElementName=firstNameLabel}" />
    </StackPanel>

    <StackPanel Orientation="Horizontal" Spacing="8">
        <TextBlock x:Name="lastNameLabel" Text="Last name" />
        <TextBox
            x:Name="lastNameTextBox"
            Width="180"
            AutomationProperties.LabeledBy="{Binding ElementName=lastNameLabel}" />
    </StackPanel>
</StackPanel>
```

## Accessible description (optional)

An accessible description provides supplemental information about a UI element when the accessible name alone is not enough. Use it to add clarifying context, such as intent, usage hints, or important behavior details that help assistive technology users understand how to work with the control.

In Narrator, the description is typically read on demand rather than as part of the default announcement. Users can request this additional detail by pressing CapsLock+F.

Treat the accessible name as the primary identifier for the control, and keep it concise. When more explanation is required, provide that extra detail through [**AutomationProperties.HelpText**](/dotnet/api/system.windows.automation.automationproperties.helptext) in addition to [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name).

## Testing accessibility early and often

The most reliable way to validate screen reader support is to test your app directly with a screen reader during development, not only at release time. Early and repeated testing helps you identify missing or misleading accessible names, incorrect control exposure, and navigation issues while changes are still inexpensive to fix. After each pass, refine your UI structure and UI Automation properties based on what users actually hear and how they move through the interface. For more details, see [Accessibility testing](accessibility-testing.md).

**AccScope** is a useful tool for this workflow because it visualizes your UI as an automation tree, making it easier to inspect what assistive technologies can discover. Its Narrator-focused view helps you verify how text is sourced and how elements are grouped and ordered for spoken output. Use it throughout the product lifecycle, including early design and control-template validation, to catch structural accessibility issues before they appear in user testing. For additional details, see [AccScope](/windows/desktop/WinAuto/accscope).

## Accessible names from dynamic data

Many Windows controls render content through *data binding*, which means accessible names are often determined from runtime data rather than static XAML. When list or item templates are populated dynamically, verify that each generated item exposes a meaningful accessible name after binding completes. Depending on the control and template composition, you may need to set or update accessibility properties programmatically so the automation tree reflects the final rendered state. For an end-to-end example, see the [WinUI Gallery accessibility sample](https://github.com/microsoft/WinUI-Gallery/blob/main/WinUIGallery/Samples/ControlPages/Accessibility/AccessibilityScreenReaderPage.xaml).

## Accessible names and localization

Accessible names must be localized with the same rigor as visible UI text. Store label strings in localization resources and connect them through [x:Uid directive](/windows/apps/develop/platform/xaml/x-uid-directive) mappings so spoken output matches the user's language. If you set [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) explicitly, ensure that value also comes from localized resources rather than hard-coded text.

Attached properties in [**AutomationProperties**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties) use a qualified resource-key syntax so localization can target the attached property on a specific element. For example, if the element is named `MediumButton`, the resource key for [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) is `MediumButton.[using:Microsoft.UI.Xaml.Automation]AutomationProperties.Name`.

## Related topics

* [Accessibility overview](accessibility-overview.md)
* [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name)
* [WinUI Gallery accessibility sample](https://github.com/microsoft/WinUI-Gallery/blob/main/WinUIGallery/Samples/ControlPages/Accessibility/AccessibilityScreenReaderPage.xaml)
* [Accessibility testing](accessibility-testing.md)