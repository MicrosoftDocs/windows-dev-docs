---
Description: Radio buttons let users select one option from two or more choices.
title: Guidelines for radio buttons
ms.assetid: 41E3F928-AA55-42A2-9281-EC3907C4F898
label: Radio buttons
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
pm-contact: kisai
design-contact: kimsea
dev-contact: mitra
doc-status: Published
ms.localizationpriority: medium
---
# Radio buttons

Radio buttons allow users to select one option from a set. Each option is represented by one radio button, and users can only select one radio button in a radio button group.

(If you're curious about the name, radio buttons are named after the channel preset buttons on a radio.)

![Radio buttons](images/controls/radio-button.png)

> **Platform APIs**: [RadioButton class](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.RadioButton), [Checked event](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Primitives.ToggleButton.Checked), [IsChecked property](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Primitives.ToggleButton.IsChecked)

## Is this the right control?

Use radio buttons to present users with two or more mutually exclusive options.

![A group of radio buttons](images/radiobutton_basic.png)

Use radio buttons when users need to see all options to make a selection. Since radio buttons emphasize all options equally, they may draw more attention to the options than necessary. Unless the options deserve extra attention from the user, consider using other controls. For example, if the default option is recommended for most users in most situations, use a [drop-down list](lists.md) instead.

![drop-down list](images/combo_box_collapsed.png)

If there are only two mutually exclusive options, combine them into a single [checkbox](checkbox.md) or [toggle switch](toggles.md). For example, use a checkbox for "I agree" instead of two radio buttons for "I agree" and "I don't agree."

![Two ways of presenting a binary choice](images/radiobutton_vs_checkbox.png)

When the user can select multiple options, use a [checkbox](checkbox.md).

![Selecting multiple options with check boxes](images/checkbox2.png)

When options are numbers that have fixed steps (10, 20, 30), use a [slider](slider.md) control.

![slider control](images/controls/slider.png)

If there are more than 8 options, use a [drop-down list](lists.md) or [list box](lists.md).

![combo box](images/combo_box_scroll.png)

If the available options are based on the app's current context, or can otherwise vary dynamically, use a single-select [list box](lists.md).

## RadioButtons behavior
The RadioButtons control has special navigation behavior that helps not only keyboard accelerant users, but also accessibility users to navigate the list more quickly and more easily.

### Navigating a RadioButtons group

There are two states that the RadioButtons control can have with it's RadioButton content:
- A list of RadioButton controls where none are selected/checked
- A list of RadioButton controls where one is already selected/checked

The following two sections (below) cover the focus behavior when tabbing into the list in both cases outlined in the bullet list above.

#### An item is already selected

When a RadioButton is selected and the user tabs into the list, focus is put on the selected RadioButton.

|List not tab focused | Has initial tab focus|
|:--:|:--:|
| ![alt text](images/selecteditem_notabfocus.png) | ![alt text](images/selecteditem_tabfocus.png)|

#### No item is selected

When no RadioButton controls are selected in the RadioButtons list, focus is put on the first RadioButton in the list.

> The item that receives tab focus from this initial navigation will not be selected/checked.

|List not tab focused | Has initial tab focus|
|:--:|:--:|
| ![alt text](images/noselecteditem_notabfocus.png) | ![alt text](images/noselecteditem_tabfocus.png)|

### Keyboarding navigation

When you have a single column list of RadioButton control options and you have already put focus onto an item, you can navigate the RadioButtons list in logical sequential order using the keyboard.

- The up or down arrow keys will move to the "next" or "previous" logical item (defined in markup)
- The left or right arrow keys will move spatially

![alt text](images/keyboardnav_singlecol.png)

This means that in purely vertical RadioButton group lists, left/right arrow keys will not do anything, but up/down will still navigate as expected. However, in purely horizontal RadioButton group lists, left and right will navigate the same way as up and down.

#### Navigating with multiple columns
The keyboarding behavior is the same as the single-column navigation, it just wraps to the next column when there is more than one defined.

![alt text](images/keyboardnav_multicol.png)

##### Wrapping

The RadioButtons group does not wrap. This is because when using a screen reader, a sense of boundary and clear indication of ends versus beginnings is lost and thus making it difficult for blind users to navigate the list easily. There is also no enumeration with a RadioButtons group, since groups like this are meant to have a resonable number of items within.

## Selection follows Focus
When you are navigating a RadioButtons list via the keyboard, as focus is placed on a previous or next RadioButton item, that item will also get selected/checked. This means the previous item that was selected/checked will be unselected, and the currently focused one will be instead.

|Before keyboard navigation | After keyboard navigation|
|:--|:--|
| ![alt text](images/2selected_nonav.png) | ![alt text](images/3selected_yesnav.png)|
| Focus is on the "2" RadioButton, and it is shown as selected | The down or right arrow key has been pressed, so focus was moved to the "3" RadioButton, thus selected 3 and unselected 2. |

### Navigating with gamepad

If you're navigating the RadioButtons container with a gamepad controller, the selection follows focus behavior will be disabled. When navigating a RadioButtons control, no gamepad engagement is necessary to initiate navigation or to select RadioButton elements within the group.

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

Radio buttons in the Microsoft Edge browser settings.

![Radio buttons in the Microsoft Edge browser settings](images/control-examples/radio-buttons-edge.png)

## Using the RadioButtons Control
If you're using [WinUI](https://github.com/microsoft/microsoft-ui-xaml), then it's recommended that you utilize the RadioButtons control.

The RadioButtons control is easy to set up and use and ensures proper and expected keyboarding and narrator behavior.

```xaml
<RadioButtons Header="App Mode" SelectedIndex="2">
    <RadioButton>Item 1</RadioButton>
    <RadioButton>Item 2</RadioButton>
    <RadioButton>Item 3</RadioButton>         
</RadioButtons>
```

![Radio buttons in two groups](images/DefaultRadioButtonGroup.png)

### Definding multiple columns
If you want to define multiple columns of RadioButton items with a RadioButtons control, you can do so by specifying the `MaxColumns` property on the RadioButtons control element:

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

![Radio buttons in two groups](images/multicolumns.png)

### DataBinding with RadioButtons
With the RadioButtons control, you can also data bind to the RadioButton elements that can go within.

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

> It is recommended to use a RadioButtons control to group RadioButton element  unless you are using an older version of WinUI and cannot take advantage of it.

Radio buttons work in groups. There are 2 ways you can group radio button controls:
- Put them inside the same parent container.
- Set the [GroupName](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.RadioButton.GroupName) property on each radio button to the same value.

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

The radio button groups look like this.

![Radio buttons in two groups](images/radio-button-groups.png)

A radio button has two states: *selected* or *cleared*. When a radio button is selected, its [IsChecked](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Primitives.ToggleButton.IsChecked) property is **true**. When a radio button is cleared, its **IsChecked** property is **false**. A radio button can be cleared by clicking another radio button in the same group, but it cannot be cleared by clicking it again. However, you can clear a radio button programmatically by setting its IsChecked property to **false**. You can actually compare the **IsChecked** property with a bool by getting the **Value** of the **IsChecked** property

### Visuals to consider

This illustration shows the proper way to position and space radio buttons.

![A set of radio buttons](images/radiobutton-layout.png)

![spacing guidelines for radio buttons](images/radiobutton-redlines.png)

## Recommendations

-   Make sure the purpose and current state of a set of radio buttons is clear.
-   Limit the radio button's text content to a single line.
-   If the text content is dynamic, consider how the button will resize and what will happen to visuals around it.
-   Use the default font unless your brand guidelines tell you to use another.
-   Don't put two radio button groups side by side. When two radio button groups are right next to each other, it's difficult to determine which buttons belong to which group.

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
