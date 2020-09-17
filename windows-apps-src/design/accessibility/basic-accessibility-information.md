---
Description: Basic accessibility info is often categorized into name, role, and value. This topic describes code to help your app expose the basic information that assistive technologies need.
ms.assetid: 9641C926-68C9-4842-8B55-C38C39A9E5C5
title: Expose basic accessibility information
label: Expose basic accessibility information
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Expose basic accessibility information  



Basic accessibility info is often categorized into name, role, and value. This topic describes code to help your app expose the basic information that assistive technologies need.

<span id="accessible_name"/>
<span id="ACCESSIBLE_NAME"/>

## Accessible name  
An accessible name is a short, descriptive text string that a screen reader uses to announce a UI element. Set the accessible name for UI elements so that have a meaning that is important for understanding the content or interacting with the UI. Such elements typically include images, input fields, buttons, controls, and regions.

This table describes how to define or obtain an accessible name for various types of elements in a XAML UI.

| Element type | Description |
|--------------|-------------|
| Static text | For [**TextBlock**](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) and [**RichTextBlock**](/uwp/api/Windows.UI.Xaml.Controls.RichTextBlock) elements, an accessible name is automatically determined from the visible (inner) text. All of the text in that element is used as the name. See [Name from inner text](#name_from_inner_text). |
| Images | The XAML [**Image**](/uwp/api/Windows.UI.Xaml.Controls.Image) element does not have a direct analog to the HTML **alt** attribute of **img** and similar elements. Either use [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) to provide a name, or use the captioning technique. See [Accessible names for images](#images). |
| Form elements | The accessible name for a form element should be the same as the label that is displayed for that element. See [Labels and LabeledBy](#labels). |
| Buttons and links | By default, the accessible name of a button or link is based on the visible text, using the same rules as described in [Name from inner text](#name_from_inner_text). In cases where a button contains only an image, use [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) to provide a text-only equivalent of the button's intended action. |

Most container elements such as panels do not promote their content as accessible name. This is because it is the item content that should report a name and corresponding role, not its container. The container element might report that it is an element that has children in a Microsoft UI Automation representation, such that the assistive technology logic can traverse it. But users of assistive technologies don't generally need to know about the containers and thus most containers aren't named.

<span id="role_value"/>
<span id="ROLE_VALUE"/>

## Role and value  
The controls and other UI elements that are part of the XAML vocabulary implement UI Automation support for reporting role and value as part of their definitions. You can use UI Automation tools to examine the role and value information for the controls, or you can read the documentation for the [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer) implementations of each control. The available roles in a UI Automation framework are defined in the [**AutomationControlType**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationControlType) enumeration. UI Automation clients such as assistive technologies can obtain role information by calling methods that the UI Automation framework exposes by using the control's **AutomationPeer**.

Not all controls have a value. Controls that do have a value report this information to UI Automation through the peers and patterns that are supported by that control. For example, a [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox) form element does have a value. An assistive technology can be a UI Automation client and can discover both that a value exists and what the value is. In this specific case the **TextBox** supports the [**IValueProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IValueProvider) pattern through the [**TextBoxAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.TextBoxAutomationPeer) definitions.

> [!NOTE]
> For cases where you use [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) or other techniques to supply the accessible name explicitly, do not include the same text as is used by the control role or type information in the accessible name. For example do not include strings such as "button" or "list" in the name. The role and type information comes from a different UI Automation property (**LocalizedControlType**) that is supplied by the default control support for UI Automation. Many assistive technologies append the **LocalizedControlType** to the accessible name, so duplicating the role in the accessible name can result in unnecessarily repeated words. For example, if you give a [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) control an accessible name of "button" or include "button" as the last part of the name, this might be read by screen readers as "button button". You should test this aspect of your accessibility info using Narrator.

<span id="Influencing_the_UI_Automation_tree_views"/>
<span id="influencing_the_ui_automation_tree_views"/>
<span id="INFLUENCING_THE_UI_AUTOMATION_TREE_VIEWS"/>

## Influencing the UI Automation tree views  
The UI Automation framework has a concept of tree views, where UI Automation clients can retrieve the relationships between elements in a UI using three possible views: raw, control, and content. The control view is the view that's often used by UI Automation clients because it provides a good representation and organization of the elements in a UI that are interactive. Testing tools usually enable you to choose which tree view to use when the tool presents the organization of elements.

By default, any [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control) derived class and a few other elements will appear in the control view when the UI Automation framework represents the UI for a Windows app. But sometimes you don't want an element to appear in the control view because of UI composition, where that element is duplicating information or presenting information that's unimportant for accessibility scenarios. Use the attached property [**AutomationProperties.AccessibilityView**](/uwp/api/windows.ui.xaml.automation.automationproperties.accessibilityviewproperty) to change how elements are exposed to the tree views. If you put an element in the **Raw** tree, most assistive technologies won't report that element as part of their views. To see some examples of how this works in existing controls, open the generic.xaml design reference XAML file in a text editor, and search for **AutomationProperties.AccessibilityView** in the templates.

<span id="name_from_inner_text"/>
<span id="NAME_FROM_INNER_TEXT"/>

## Name from inner text  
To make it easier to use strings that already exist in the visible UI for accessible name values, many of the controls and other UI elements provide support for automatically determining a default accessible name based on inner text within the element, or from string values of content properties.

* [**TextBlock**](/uwp/api/Windows.UI.Xaml.Controls.TextBlock), [**RichTextBlock**](/uwp/api/Windows.UI.Xaml.Controls.RichTextBlock), [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox) and **RichTextBlock** each promote the value of the **Text** property as the default accessible name.
* Any [**ContentControl**](/uwp/api/windows.ui.xaml.controls.contentcontrol.content) subclass uses an iterative "ToString" technique to find strings in its [**Content**](/uwp/api/windows.ui.xaml.controls.contentcontrol.content) value, and promotes these strings as the default accessible name.

> [!NOTE]
> As enforced by UI Automation, the accessible name length cannot be greater than 2048 characters. If a string used for automatic accessible name determination exceeds that limit, the accessible name is truncated at that point.

<span id="images"/>
<span id="IMAGES"/>

## Accessible names for images
To support screen readers and to provide the basic identifying information for each element in the UI, you sometimes must provide text alternatives to non-textual information such as images and charts (excluding any purely decorative or structural elements). These elements don't have inner text so the accessible name won't have a calculated value. You can set the accessible name directly by setting the [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) attached property as shown in this example.

XAML
```xml
<!-- Comment -->
<Image Source="product.png"
  AutomationProperties.Name="An image of a customer using the product."/>
```

Alternatively, consider including a text caption that appears in the visible UI and that also serves as the label-associated accessibility information for the image content. Here's an example:

XAML
```xml
<Image HorizontalAlignment="Left" Width="480" x:Name="img_MyPix"
  Source="snoqualmie-NF.jpg"
  AutomationProperties.LabeledBy="{Binding ElementName=caption_MyPix}"/>
<TextBlock x:Name="caption_MyPix">Mount Snoqualmie Skiing</TextBlock>
```

<span id="labels"/>
<span id="LABELS"/>

## Labels and LabeledBy  
The preferred way to associate a label with a form element is to use a [**TextBlock**](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) with an **x:Name** for label text, and then to set the [**AutomationProperties.LabeledBy**](/previous-versions/windows/silverlight/dotnet-windows-silverlight/ms591292(v=vs.95)) attached property on the form element to reference the labeling **TextBlock** by its XAML name. If you use this pattern, when the user clicks the label, the focus moves to the associated control and assistive technologies can use the label text as the accessible name for the form field. Here's an example that shows this technique.

XAML
```xml
<StackPanel x:Name="LayoutRoot" Background="White">
   <StackPanel Orientation="Horizontal">
     <TextBlock Name="lbl_FirstName">First name</TextBlock>
     <TextBox
      AutomationProperties.LabeledBy="{Binding ElementName=lbl_FirstName}"
      Name="tbFirstName" Width="100"/>
   </StackPanel>
   <StackPanel Orientation="Horizontal">
     <TextBlock Name="lbl_LastName">Last name</TextBlock>
     <TextBox
      AutomationProperties.LabeledBy="{Binding ElementName=lbl_LastName}"
      Name="tbLastName" Width="100"/>
   </StackPanel>
 </StackPanel>
```

<span id="accessible_description"/>
<span id="ACCESSIBLE_DESCRIPTION"/>

## Accessible description (optional)  
An accessible description provides additional accessibility information about a particular UI element. You typically provide an accessible description when an accessible name alone does not adequately convey an element's purpose.

The Narrator screen reader reads an element's accessible description only when the user requests more information about the element by pressing CapsLock+F.

The accessible name is meant to identify the control rather than to fully document its behavior. If a brief description is not enough to explain the control, you can set the [**AutomationProperties.HelpText**](/dotnet/api/system.windows.automation.automationproperties.helptext) attached property in addition to [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name).

<span id="Testing_accessibility_early_and_often"/>
<span id="testing_accessibility_early_and_often"/>
<span id="TESTING_ACCESSIBILITY_EARLY_AND_OFTEN"/>

## Testing accessibility early and often  
Ultimately, the best approach for supporting screen readers is to test your app using a screen reader yourself. That will show you how the screen reader behaves and what basic accessibility information might be missing from the app. Then you can adjust the UI or UI Automation property values accordingly. For more info, see [Accessibility testing](accessibility-testing.md).

One of the tools you can use for testing accessibility is called **AccScope**. The **AccScope** tool is particularly useful because you can see visual representations of your UI that represent how assistive technologies might view your app as an automation tree. In particular, there's a Narrator mode that gives a view of how Narrator gets text from your app and how it organizes the elements in the UI. AccScope is designed so that it can be used and be useful throughout a development cycle for an app, even during the preliminary design phase. For more info see [AccScope](/windows/desktop/WinAuto/accscope).

<span id="Accessible_names_from_dynamic_data"/>
<span id="accessible_names_from_dynamic_data"/>
<span id="ACCESSIBLE_NAMES_FROM_DYNAMIC_DATA"/>

## Accessible names from dynamic data  
Windows supports many controls that can be used to display values that come from an associated data source, through a feature known as *data binding*. When you populate lists with data items, you may need to use a technique that sets accessible names for data-bound list items after the initial list is populated. For more info, see "Scenario 4" in the [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample).

<span id="Accessible_names_and_localization"/>
<span id="accessible_names_and_localization"/>
<span id="ACCESSIBLE_NAMES_AND_LOCALIZATION"/>

## Accessible names and localization  
To make sure that the accessible name is also an element that is localized, you should use correct techniques for storing localizable strings as resources and then referencing the resource connections with [x:Uid directive](../../xaml-platform/x-uid-directive.md) values. If the accessible name is coming from an explicitly set [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) usage, make sure that the string there is also localizable.

Note that attached properties such as the [**AutomationProperties**](/uwp/api/Windows.UI.Xaml.Automation.AutomationProperties) properties use a special qualifying syntax for the resource name, so that the resource references the attached property as applied to a specific element. For example, the resource name for [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name) as applied to a UI element named `MediumButton` is: `MediumButton.[using:Windows.UI.Xaml.Automation]AutomationProperties.Name`.

<span id="related_topics"/>

## Related topics  
* [Accessibility](accessibility.md)
* [**AutomationProperties.Name**](/dotnet/api/system.windows.automation.automationproperties.name)
* [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample)
* [Accessibility testing](accessibility-testing.md)