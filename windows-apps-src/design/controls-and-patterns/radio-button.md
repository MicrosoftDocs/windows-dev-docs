---
Description: Radio buttons let users select one option from two or more choices.
title: Guidelines for radio buttons
ms.assetid: 41E3F928-AA55-42A2-9281-EC3907C4F898
label: Radio buttons
template: detail.hbs
ms.date: 06/10/2020
ms.topic: article
keywords: windows 10, uwp
pm-contact: kisai
design-contact: kimsea
dev-contact: mitra
doc-status: Published
ms.localizationpriority: medium
---

# Radio buttons

Radio buttons let users select one option from a collection of two or more mutually-exclusive, but related, options. Each option is represented by one radio button.

In the default state, no radio buttons in a group are selected. However, when a radio button option is selected by a user, the unselected state of the group cannot be restored by the user.

The singular behavior of a radio button group distinguishes it from  [Check boxes](checkbox.md), which support multi-selection and deselection.

![Radio buttons](images/controls/radio-button.png)

**Get the Windows UI Library**

|  |  |
| - | - |
| ![WinUI logo](images/winui-logo-64x64.png) | The **RadioButtons** control is included as part of the Windows UI Library, a NuGet package that contains new controls and UI features for Windows apps. For more info, including installation instructions, see [Windows UI Library](https://docs.microsoft.com/uwp/toolkits/winui/). |

> **Windows UI Library APIs:** [RadioButtons class](/uwp/api/microsoft.ui.xaml.controls.radiobuttons), [SelectionChanged event](/uwp/api/microsoft.ui.xaml.controls.radiobuttons.selectionchanged), [SelectedItem property](/uwp/api/microsoft.ui.xaml.controls.radiobuttons.selecteditem), [SelectedIndex property](/uwp/api/microsoft.ui.xaml.controls.radiobuttons.selectedindex)
>
> **Platform APIs:** [RadioButton class](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.RadioButton), [Checked event](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Primitives.ToggleButton.Checked), [IsChecked property](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Primitives.ToggleButton.IsChecked)

## Is this the right control?

Use radio buttons to provide users with the ability to select from two or more mutually-exclusive options.

![A group of radio buttons](images/radiobutton_basic.png)

Use radio buttons when users need to see all options to make a selection. Since radio buttons emphasize all options equally, they may draw more attention to the options than is necessary or desired. Unless all options deserve equal attention from the user, consider using other controls. For example, if the default option is recommended for most users and in most situations, use a [combo box](combo-box.md) instead.

![A drop-down list used to emphasize a default option](images/combo_box_collapsed.png)

If there are only two mutually-exclusive options, combine them into a single [checkbox](checkbox.md) or [toggle switch](toggles.md). For example, use a checkbox for "I agree" instead of two radio buttons for "I agree" and "I don't agree."

![A checkbox is a good alternative for presenting a binary choice](images/radiobutton_vs_checkbox.png)

When the user can select multiple options, use a [checkbox](checkbox.md).

![Checkboxes support multi-selection](images/checkbox2.png)

When options are numbers that have fixed steps (10, 20, 30), use a [slider](slider.md) control.

![A slider used for selecting stepped values](images/controls/slider.png)

If there are more than 8 options, use a [combo box or a list box](combo-box.md).

![A list box used to present multiple options](images/combo_box_scroll.png)

> [!NOTE]
> If the available options are based on the app's current context, or can otherwise vary dynamically, use a single-select [list box](combo-box.md#list-boxes).

## RadioButtons behavior

Keyboard access and navigation behavior has been optimized in [RadioButton](/uwp/api/windows.ui.xaml.controls.radiobutton?view=winrt-19041) groups to help both accessibility and keyboard power users navigate the list of options more quickly and easily.

In addition to keyboard shortcuts and accessibility improvements, the default visual layout of individual radio buttons in a RadioButton group has also been optimized through automated orientation, spacing, and margin settings. This eliminates the requirement to specify these properties, as you might have to do when using a more primitive grouping control such as [StackPanel](../layout/layout-panels.md#stackpanel) or [Grid](../layout/layout-panels.md#grid).

### Navigating a RadioButtons group

There are two states that the RadioButtons control supports:

- A list of RadioButton controls where none are selected/checked
- A list of RadioButton controls where one is already selected/checked

The following two sections cover both radio button focus behaviors.

#### Item already selected

When a radio button is selected and the user tabs into the list, the selected radio button gets focus.

|List without tab focus | List with initial tab focus |
|:--:|:--:|
| ![List without tab focus](images/radiobutton-selected-item-no-tab-focus.png) | ![ist with initial tab focus](images/radiobutton-selected-item-tab-focus.png)|

#### No item selected

When no radio button is selected, the first radio button in the list gets focus.

> [!NOTE]
> The item that receives tab focus from the initial tab navigation will not be selected/checked.

|List without tab focus | List with initial tab focus|
|:--:|:--:|
| ![List without tab focus](images/radiobutton-no-selected-item-no-tab-focus.png) | ![List with initial tab focus](images/radiobutton-no-selected-item-tab-focus.png)|

### Keyboard navigation

When you have a single row/column of radio button options, and an item has already received tab focus, the arrow keys provide "inner navigation" between items within the RadioButtons control. For more detail on keyboard navigation behaviors, see [Keyboard interactions - Navigation](../input/keyboard-interactions.md#navigation).

For a RadioButtons control, when the list of options is arranged vertically (exclusively), the Up/Down arrow keys navigate between items and the Left/Right arrow keys don't do anything. However, in a list that is arranged horizontally (exclusively), Left/Right and Up/Down arrow keys all navigate between items in the same way.

![Example of keyboard navigation in a single column/row RadioButton group](images/radiobutton-keyboard-navigation-single-column-row.png)<br/>
*Example of keyboard navigation in a single column/row RadioButton group*

#### Navigating within multi-column/row layouts

Keyboard "inner navigation" behavior is the same as single-column navigation, but "wraps" to the next column.

![Example of keyboard navigation in a multi-column/row RadioButton group](images/radiobutton-keyboard-navigation-multi-column-row.png)

##### Wrapping

The RadioButtons group does not wrap. This is because when using a screen reader, a sense of boundary and a clear indication of beginning and end is lost, making it difficult for vision-impaired users to navigate the list. The RadioButtons control also doesn't support enumeration, as it is intended to contain a reasonable number of items (see [Is this the right control?](#is-this-the-right-control)).

## Selection follows Focus

When using the keyboard to navigate between items in a RadioButtons list (where an item is already selected), as focus moves from one item to the next, the newly focused item gets selected/checked and the previously focused item is deselected/unchecked.

|Before keyboard navigation | After keyboard navigation|
|:--|:--|
| ![Example of focus and selection before keyboard navigation](images/radiobutton-two-selected-before-keyboard-navigation.png)</br>*Example of focus and selection before keyboard navigation* | ![Example of focus and selection after keyboard navigation](images/radiobutton-three-selected-after-keyboard-navigation.png)<br/>*Example of focus and selection after keyboard navigation where the down or right arrow key moves focus to the "3" RadioButton, selects "3", and unselects "2".*

### Navigating with Xbox gamepad and remote control

If you're using a gamepad to navigate within a RadioButtons control, the selection follows focus behavior is disabled. When navigating a RadioButtons control, no gamepad engagement is necessary to initiate navigation or to select RadioButton elements within the group.

## Accessibility behavior

The following table details how Narrator handles a radio button group and what is announced (this depends on Narrator detail preferences set by the user).

| Initial focus | Focus moves to a selected item |
|:--|:--|
| "Group name" RadioButton collection, x of N selected | RadioButton "name" selected, x of N |
|"Group name" RadioButton collection, none selected| RadioButton "name" not selected, x of N <br> *(If navigating with shift-arrow keys, which indicates no selection following focus)* |

## Examples

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/RadioButton">open the app and see the RadioButton in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## Using the WinUI RadioButtons control

If you're using [WinUI](https://github.com/microsoft/microsoft-ui-xaml), we recommend using the [RadioButtons](/uwp/api/microsoft.ui.xaml.controls.radiobuttons) control.

The RadioButtons control is easy to set up and use and ensures proper and expected keyboarding and narrator behavior.

Here, we declare a basic RadioButtons control with three options.

```xaml
<RadioButtons Header="App Mode" SelectedIndex="2">
    <RadioButton>Item 1</RadioButton>
    <RadioButton>Item 2</RadioButton>
    <RadioButton>Item 3</RadioButton>
</RadioButtons>
```

![Radio buttons in two groups](images/default-radiobutton-group.png)

### Defining multiple columns

You can declare a multi-column RadioButtons control by specifying the [MaxColumns property](/uwp/api/microsoft.ui.xaml.controls.radiobuttons.maxcolumns).

```xaml
<muxc:RadioButtons Header="App Mode" MaxColumns="3">
    <x:String>Column 1</x:String>
    <x:String>Column 2</x:String>
    <x:String>Column 3</x:String>
    <x:String>Column 1</x:String>
    <x:String>Column 2</x:String>
    <x:String>Column 3</x:String>
</muxc:RadioButtons>
```

![Radio buttons in two column groups](images/radiobutton-multi-columns.png)

### Data binding

The RadioButtons control supports data binding using it's [ItemsSource](/uwp/api/microsoft.ui.xaml.controls.radiobuttons.itemssource) property, as shown in the following snippet.

```xaml
<RadioButtons Header="App Mode" ItemsSource="{x:Bind radioButtonItems}" />
```

```c#
public sealed partial class MainPage : Page
{
    public class OptionDataModel
    {
        public string Label;
        public override string ToString()
        {
            return Label;
        }
    }

    List<OptionDataModel> radioButtonItems;

    public MainPage()
    {
        this.InitializeComponent();

        radioButtonItems = new List<OptionDataModel>();
        radioButtonItems.Add(new OptionDataModel() { label = "Item 1" });
        radioButtonItems.Add(new OptionDataModel() { label = "Item 2" });
        radioButtonItems.Add(new OptionDataModel() { label = "Item 3" });
    }
}
```

## Create your own radio button group

> [!Important]
> We recommend using the WinUI RadioButtons control to group RadioButton elements (unless you are using an older version of WinUI).

Radio buttons work in groups. There are 2 ways you can group radio button controls:

- Put them inside the same parent container
- Set the [GroupName](/uwp/api/Windows.UI.Xaml.Controls.RadioButton.GroupName) property on each radio button to the same value

In this example, the first group of radio buttons is implicitly grouped by being in the same stack panel. The second group is divided between 2 stack panels, so they're explicitly grouped by GroupName.

```xaml
<StackPanel>
    <StackPanel>
        <TextBlock Text="Background" Style="{ThemeResource BaseTextBlockStyle}"/>
        <StackPanel Orientation="Horizontal">
            <RadioButton Content="Green" Tag="Green" Checked="BGRadioButton_Checked"/>
            <RadioButton Content="Yellow" Tag="Yellow" Checked="BGRadioButton_Checked"/>
            <RadioButton Content="Blue" Tag="Blue" Checked="BGRadioButton_Checked"/>
            <RadioButton Content="White" Tag="White" Checked="BGRadioButton_Checked" IsChecked="True"/>
        </StackPanel>
    </StackPanel>
    <StackPanel>
        <TextBlock Text="BorderBrush" Style="{ThemeResource BaseTextBlockStyle}"/>
        <StackPanel Orientation="Horizontal">
            <StackPanel>
                <RadioButton Content="Green" GroupName="BorderBrush" Tag="Green" Checked="BorderRadioButton_Checked"/>
                <RadioButton Content="Yellow" GroupName="BorderBrush" Tag="Yellow" Checked="BorderRadioButton_Checked" IsChecked="True"/>
            </StackPanel>
            <StackPanel>
                <RadioButton Content="Blue" GroupName="BorderBrush" Tag="Blue" Checked="BorderRadioButton_Checked"/>
                <RadioButton Content="White" GroupName="BorderBrush" Tag="White"  Checked="BorderRadioButton_Checked"/>
            </StackPanel>
        </StackPanel>
    </StackPanel>
    <Border x:Name="BorderExample1" BorderThickness="10" BorderBrush="#FFFFD700" Background="#FFFFFFFF" Height="50" Margin="0,10,0,10"/>
</StackPanel>
```

```csharp
private void BGRadioButton_Checked(object sender, RoutedEventArgs e)
{
    RadioButton rb = sender as RadioButton;

    if (rb != null && BorderExample1 != null)
    {
        string colorName = rb.Tag.ToString();
        switch (colorName)
        {
            case "Yellow":
                BorderExample1.Background = new SolidColorBrush(Colors.Yellow);
                break;
            case "Green":
                BorderExample1.Background = new SolidColorBrush(Colors.Green);
                break;
            case "Blue":
                BorderExample1.Background = new SolidColorBrush(Colors.Blue);
                break;
            case "White":
                BorderExample1.Background = new SolidColorBrush(Colors.White);
                break;
        }
    }
}

private void BorderRadioButton_Checked(object sender, RoutedEventArgs e)
{
    RadioButton rb = sender as RadioButton;

    if (rb != null && BorderExample1 != null)
    {
        string colorName = rb.Tag.ToString();
        switch (colorName)
        {
            case "Yellow":
                BorderExample1.BorderBrush = new SolidColorBrush(Colors.Gold);
                break;
            case "Green":
                BorderExample1.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
                break;
            case "Blue":
                BorderExample1.BorderBrush = new SolidColorBrush(Colors.DarkBlue);
                break;
            case "White":
                BorderExample1.BorderBrush = new SolidColorBrush(Colors.White);
                break;
        }
    }
}
```

The following image shows how this radio button group is rendered:

![Radio buttons in two groups](images/radio-button-groups.png)

## Radio button states

A radio button has two states: checked or unchecked. When a radio button is selected, its [IsChecked](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ToggleButton.IsChecked) property is **true**. When a radio button is unchecked, its **IsChecked** property is **false**. A radio button can be unchecked by clicking another radio button in the same group, but it cannot be unchecked by clicking it again. However, you can uncheck a radio button programmatically by setting its IsChecked property to **false**.

## Recommendations

- Make sure the purpose and current state of a set of radio buttons is clear.
- Limit the radio button's text content to a single line.
- If the text content is dynamic, consider how the button will resize and what will happen to visuals around it.
- Use the default font unless your brand guidelines tell you otherwise.
- Don't put two radio button groups side by side. When two radio button groups are right next to each other, it's difficult to determine which buttons belong to which group.

### Visuals to consider

The following images show how best to lay out the radio buttons in a RadioButton group.

![A set of radio buttons](images/radiobutton-layout.png)

![spacing guidelines for radio buttons](images/radiobutton-redline.png)

> [!NOTE]
> If using a WinUI RadioButtons control, spacing, margins, and orientation are already optimized.

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Xaml-Controls-Gallery) - See all the XAML controls in an interactive format.

## Related topics

### For designers

- [Buttons](buttons.md)
- [Toggle switches](toggles.md)
- [Checkboxes](checkbox.md)
- [Lists and combo boxes](lists.md)
- [Sliders](slider.md)

### For developers (XAML)

- [RadioButton class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.radiobutton)
