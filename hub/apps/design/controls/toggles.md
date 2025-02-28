---
description: The toggle switch represents a physical switch that allows users to turn things on or off.
title: Guidelines for toggle switch controls
ms.assetid: 753CFEA4-80D3-474C-B4A9-555F872A3DEF
label: Toggle switches
template: detail.hbs
ms.date: 02/26/2025
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
---
# Toggle switches

The toggle switch represents a physical switch that allows users to turn things on or off, like a light switch. Use toggle switch controls to present users with two mutually exclusive options (such as on/off), where choosing an option provides immediate results.

To create a toggle switch control, you use the  [ToggleSwitch class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch).

## Is this the right control?

Use a toggle switch for binary operations that take effect right after the user flips the toggle switch.

![Toggle switch, on](images/toggle-switch-on.png)

![Toggle switch off](images/toggle-switch-off.png)

Think of the toggle switch as a physical power switch for a device: you flip it on or off when you want to enable or disable the action performed by the device.

To make the toggle switch easy to understand, label it with one or two words, preferably nouns, that describe the functionality it controls. For example, "WiFi" or "Kitchen lights." 

## Choosing between toggle switch and check box

For some actions, either a toggle switch or a check box might work. To decide which control would work better, follow these tips:

- Use a toggle switch for binary settings when changes become effective immediately after the user changes them.

    ![Toggle switch versus check box](images/toggleswitches02.png)

    In this example, it's clear with the toggle switch that the kitchen lights are set to "On." But with the checkbox, the user needs to think about whether the lights are on now or whether they need to check the box to turn the lights on.

- Use check boxes for optional ("nice to have") items.
- Use a checkbox when the user has to perform extra steps for changes to be effective. For example, if the user must click a "submit" or "next" button to apply changes, use a check box.
- Use check boxes when the user can select multiple items that are related to a single setting or feature.

## Recommendations

- Use the default On and Off labels when you can; only replace them when it's necessary for the toggle switch to make sense. If you replace them, use a single word that more accurately describes the toggle. In general, if the words "On" and "Off" don't describe the action tied to a toggle switch, you might need a different control.
- Avoid replacing the On and Off labels unless you must; stick with the default labels unless the situation calls for custom ones.

## Create a toggle switch

> [!div class="checklist"]
>
> - **Important APIs**: [ToggleSwitch class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch), [IsOn property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch.ison), [Toggled event](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch.toggled)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the ToggleSwitch in action](winui3gallery:/item/ToggleSwitch)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

Here's how to create a simple toggle switch. This XAML creates the toggle switch shown previously.

```xaml
<ToggleSwitch x:Name="lightToggle" Header="Kitchen Lights"/>
```

Here's how to create the same toggle switch in code.

```csharp
ToggleSwitch lightToggle = new ToggleSwitch();
lightToggle.Header = "Kitchen Lights";

// Add the toggle switch to a parent container in the visual tree.
stackPanel1.Children.Add(lightToggle);
```

### IsOn

The switch can be either on or off. Use the [IsOn](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch.ison) property to determine the state of the switch. When the switch is used to control the state of another binary property, you can use a binding as shown here.

```xaml
<StackPanel Orientation="Horizontal">
    <ToggleSwitch x:Name="ToggleSwitch1" IsOn="True"/>
    <ProgressRing IsActive="{x:Bind ToggleSwitch1.IsOn, Mode=OneWay}" 
                  Width="130"/>
</StackPanel>
```

### Toggled

In other cases, you can handle the [Toggled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch.toggled) event to respond to changes in the state.

This example shows how to add a Toggled event handler in XAML and in code. The Toggled event is handled to turn a progress ring on or off, and change its visibility.

```xaml
<ToggleSwitch x:Name="toggleSwitch1" IsOn="True"
              Toggled="ToggleSwitch_Toggled"/>
```

Here's how to create the same toggle switch in code.

```csharp
// Create a new toggle switch and add a Toggled event handler.
ToggleSwitch toggleSwitch1 = new ToggleSwitch();
toggleSwitch1.Toggled += ToggleSwitch_Toggled;

// Add the toggle switch to a parent container in the visual tree.
stackPanel1.Children.Add(toggleSwitch1);
```

Here's the handler for the Toggled event.

```csharp
private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
{
    ToggleSwitch toggleSwitch = sender as ToggleSwitch;
    if (toggleSwitch != null)
    {
        if (toggleSwitch.IsOn == true)
        {
            progress1.IsActive = true;
            progress1.Visibility = Visibility.Visible;
        }
        else
        {
            progress1.IsActive = false;
            progress1.Visibility = Visibility.Collapsed;
        }
    }
}
```

### On/Off labels

By default, the toggle switch includes literal On and Off labels, which are localized automatically. You can replace these labels by setting the [OnContent](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch.oncontent), and [OffContent](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch.offcontent) properties.

This example replaces the On/Off labels with Show/Hide labels.

```xaml
<ToggleSwitch x:Name="imageToggle" Header="Show images"
              OffContent="Show" OnContent="Hide"
              Toggled="ToggleSwitch_Toggled"/>
```

You can also use more complex content by setting the [OnContentTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch.oncontenttemplate) and [OffContentTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch.offcontenttemplate) properties.

## UWP and WinUI 2

[!INCLUDE [uwp-winui2-note](../../../includes/uwp-winui-2-note.md)]

APIs for this control exist in the [Windows.UI.Xaml.Controls](/uwp/api/Windows.UI.Xaml.Controls) namespace.

> [!div class="checklist"]
>
> - **UWP APIs:** [ToggleSwitch class](/uwp/api/windows.ui.xaml.controls.toggleswitch), [IsOn property](/uwp/api/windows.ui.xaml.controls.toggleswitch.ison), [Toggled event](/uwp/api/windows.ui.xaml.controls.toggleswitch.toggled)
> - [Open the WinUI 2 Gallery app and see the Slider in action](winui2gallery:/item/Slider). [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]

We recommend using the latest [WinUI 2](../../winui/winui2/index.md) to get the most current styles and templates for all controls.

## Related articles

- [ToggleSwitch class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.toggleswitch)
- [Radio buttons](radio-button.md)
- [Toggle switches](toggles.md)
- [Check boxes](checkbox.md)
