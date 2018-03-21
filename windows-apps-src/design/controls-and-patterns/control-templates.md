---
author: Jwmsft
Description: You can customize a control's visual structure and visual behavior by creating a control template in the XAML framework.
MS-HAID: dev\_ctrl\_layout\_txt.control\_templates
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: Control templates
ms.assetid: 6E642626-A1D6-482F-9F7E-DBBA7A071DAD
label: Control templates
template: detail.hbs
ms.author: jimwalk
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Control templates

 

You can customize a control's visual structure and visual behavior by creating a control template in the XAML framework. Controls have many properties, such as [**Background**](https://msdn.microsoft.com/library/windows/apps/br209395), [**Foreground**](https://msdn.microsoft.com/library/windows/apps/br209414), and [**FontFamily**](https://msdn.microsoft.com/library/windows/apps/br209404), that you can set to specify different aspects of the control's appearance. But the changes that you can make by setting these properties are limited. You can specify additional customizations by creating a template using the [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391) class. Here, we show you how to create a **ControlTemplate** to customize the appearance of a [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316) control.

> **Important APIs**: [**ControlTemplate class**](https://msdn.microsoft.com/library/windows/apps/br209391), [**Control.Template property**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.control.template.aspx)


## Custom control template example


By default, a [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316) control puts its content (the string or object next to the **CheckBox**) to the right of the selection box, and a check mark indicates that a user selected the **CheckBox**. These characteristics represent the visual structure and visual behavior of the **CheckBox**.

Here's a [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316) using the default [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391) shown in the `Unchecked`, `Checked`, and `Indeterminate` states.

![default checkbox template](images/templates-checkbox-states-default.png)

You can change these characteristics by creating a [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391) for the [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316). For example, if you want the content of the check box to be below the selection box, and you want to use an **X** to indicate that a user selected the check box. You specify these characteristics in the **ControlTemplate** of the **CheckBox**.

To use a custom template with a control, assign the [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391) to the [**Template**](https://msdn.microsoft.com/library/windows/apps/br209465) property of the control. Here's a [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316) using a **ControlTemplate** called `CheckBoxTemplate1`. We show the Extensible Application Markup Language (XAML) for the **ControlTemplate** in the next section.

```XAML
<CheckBox Content="CheckBox" Template="{StaticResource CheckBoxTemplate1}" IsThreeState="True" Margin="20"/>
```

Here's how this [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316) looks in the `Unchecked`, `Checked`, and `Indeterminate` states after we apply our template.

![custom checkbox template](images/templates-checkbox-states.png)

## Specify the visual structure of a control


When you create a [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391), you combine [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) objects to build a single control. A **ControlTemplate** must have only one **FrameworkElement** as its root element. The root element usually contains other **FrameworkElement** objects. The combination of objects makes up the control's visual structure.

This XAML creates a [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391) for a [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316) that specifies that the content of the control is below the selection box. The root element is a [**Border**](https://msdn.microsoft.com/library/windows/apps/br209250). The example specifies a [**Path**](/uwp/api/Windows.UI.Xaml.Shapes.Path) to create an **X** that indicates that a user selected the **CheckBox**, and an [**Ellipse**](/uwp/api/Windows.UI.Xaml.Shapes.Ellipse) that indicates an indeterminate state. Note that the [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) is set to 0 on the **Path** and the **Ellipse** so that by default, neither appear.

A [TemplateBinding](../../xaml-platform/templatebinding-markup-extension.md) is a special binding that links the value of a property in a control template to the value of some other exposed property on the templated control. TemplateBinding can only be used within a ControlTemplate definition in XAML. See [TemplateBinding markup extension](../../xaml-platform/templatebinding-markup-extension.md) for more info.

```XAML
<ControlTemplate x:Key="CheckBoxTemplate1" TargetType="CheckBox">
    <Border BorderBrush="{TemplateBinding BorderBrush}" 
            BorderThickness="{TemplateBinding BorderThickness}" 
            Background="{TemplateBinding Background}">
        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition Height="*"/>
                <RowDefinition Height="25"/>
            </Grid.RowDefinitions>
            <Rectangle x:Name="NormalRectangle" Fill="Transparent" Height="20" Width="20" 
                       Stroke="{ThemeResource SystemControlForegroundBaseMediumHighBrush}" 
                       StrokeThickness="{ThemeResource CheckBoxBorderThemeThickness}" 
                       UseLayoutRounding="False"/>
            <!-- Create an X to indicate that the CheckBox is selected. -->
            <Path x:Name="CheckGlyph" 
                  Data="M103,240 L111,240 119,248 127,240 135,240 123,252 135,264 127,264 119,257 111,264 103,264 114,252 z" 
                  Fill="{ThemeResource CheckBoxForegroundThemeBrush}" 
                  FlowDirection="LeftToRight" 
                  Height="14" Width="16" Opacity="0" Stretch="Fill"/>
            <Ellipse x:Name="IndeterminateGlyph" 
                     Fill="{ThemeResource CheckBoxForegroundThemeBrush}" 
                     Height="8" Width="8" Opacity="0" UseLayoutRounding="False" />
            <ContentPresenter x:Name="ContentPresenter" 
                              ContentTemplate="{TemplateBinding ContentTemplate}" 
                              Content="{TemplateBinding Content}" 
                              Margin="{TemplateBinding Padding}" Grid.Row="1" 
                              HorizontalAlignment="Center" 
                              VerticalAlignment="{TemplateBinding VerticalContentAlignment}"/>
        </Grid>
    </Border>
</ControlTemplate>
```

## Specify the visual behavior of a control


A visual behavior specifies the appearance of a control when it is in a certain state. The [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316) control has 3 check states: `Checked`, `Unchecked`, and `Indeterminate`. The value of the [**IsChecked**](https://msdn.microsoft.com/library/windows/apps/br209798) property determines the state of the **CheckBox**, and its state determines what appears in the box.

This table lists the possible values of [**IsChecked**](https://msdn.microsoft.com/library/windows/apps/br209798), the corresponding states of the [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316), and the appearance of the **CheckBox**.

|                     |                    |                         |
|---------------------|--------------------|-------------------------|
| **IsChecked** value | **CheckBox** state | **CheckBox** appearance |
| **true**            | `Checked`          | Contains an "X".        |
| **false**           | `Unchecked`        | Empty.                  |
| **null**            | `Indeterminate`    | Contains a circle.      |

 

You specify the appearance of a control when it is in a certain state by using [**VisualState**](https://msdn.microsoft.com/library/windows/apps/br209007) objects. A **VisualState** contains a [**Setter**](https://msdn.microsoft.com/library/windows/apps/br208817) or [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/br243053) that changes the appearance of the elements in the [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391). When the control enters the state that the [**VisualState.Name**](https://msdn.microsoft.com/library/windows/apps/br209031) property specifies, the property changes in the **Setter** or [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/br210490) are applied. When the control exits the state, the changes are removed. You add **VisualState** objects to [**VisualStateGroup**](https://msdn.microsoft.com/library/windows/apps/br209014) objects. You add **VisualStateGroup** objects to the [**VisualStateManager.VisualStateGroups**](https://msdn.microsoft.com/library/windows/apps/hh738505) attached property, which you set on the root [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) of the **ControlTemplate**.

This XAML shows the [**VisualState**](https://msdn.microsoft.com/library/windows/apps/br209007) objects for the `Checked`, `Unchecked`, and `Indeterminate` states. The example sets the [**VisualStateManager.VisualStateGroups**](https://msdn.microsoft.com/library/windows/apps/hh738505) attached property on the [**Border**](https://msdn.microsoft.com/library/windows/apps/br209250), which is the root element of the [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391). The `Checked` **VisualState** specifies that the [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) of the [**Path**](/uwp/api/Windows.UI.Xaml.Shapes.Path) named `CheckGlyph` (which we show in the previous example) is 1. The `Indeterminate` **VisualState** specifies that the **Opacity** of the [**Ellipse**](/uwp/api/Windows.UI.Xaml.Shapes.Ellipse) named `IndeterminateGlyph` is 1. The `Unchecked` **VisualState** has no [**Setter**](https://msdn.microsoft.com/library/windows/apps/br208817) or [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/br210490), so the [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316) returns to its default appearance.

```XAML
<ControlTemplate x:Key="CheckBoxTemplate1" TargetType="CheckBox">
    <Border BorderBrush="{TemplateBinding BorderBrush}" 
            BorderThickness="{TemplateBinding BorderThickness}" 
            Background="{TemplateBinding Background}">
            
        <VisualStateManager.VisualStateGroups>
            <VisualStateGroup x:Name="CheckStates">
                <VisualState x:Name="Checked">
                    <VisualState.Setters>
                        <Setter Target="CheckGlyph.Opacity" Value="1"/>
                    </VisualState.Setters>
                    <!-- This Storyboard is equivalent to the Setter. -->
                    <!--<Storyboard>
                        <DoubleAnimation Duration="0" To="1" 
                         Storyboard.TargetName="CheckGlyph" Storyboard.TargetProperty="Opacity"/>
                    </Storyboard>-->
                </VisualState>
                <VisualState x:Name="Unchecked"/>
                <VisualState x:Name="Indeterminate">
                    <VisualState.Setters>
                        <Setter Target="IndeterminateGlyph.Opacity" Value="1"/>
                    </VisualState.Setters>
                    <!-- This Storyboard is equivalent to the Setter. -->
                    <!--<Storyboard>
                        <DoubleAnimation Duration="0" To="1"
                         Storyboard.TargetName="IndeterminateGlyph" Storyboard.TargetProperty="Opacity"/>
                    </Storyboard>-->
                </VisualState>
            </VisualStateGroup>
        </VisualStateManager.VisualStateGroups>

        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition Height="*"/>
                <RowDefinition Height="25"/>
            </Grid.RowDefinitions>
            <Rectangle x:Name="NormalRectangle" Fill="Transparent" Height="20" Width="20" 
                       Stroke="{ThemeResource SystemControlForegroundBaseMediumHighBrush}" 
                       StrokeThickness="{ThemeResource CheckBoxBorderThemeThickness}" 
                       UseLayoutRounding="False"/>
            <!-- Create an X to indicate that the CheckBox is selected. -->
            <Path x:Name="CheckGlyph" 
                  Data="M103,240 L111,240 119,248 127,240 135,240 123,252 135,264 127,264 119,257 111,264 103,264 114,252 z" 
                  Fill="{ThemeResource CheckBoxForegroundThemeBrush}" 
                  FlowDirection="LeftToRight" 
                  Height="14" Width="16" Opacity="0" Stretch="Fill"/>
            <Ellipse x:Name="IndeterminateGlyph" 
                     Fill="{ThemeResource CheckBoxForegroundThemeBrush}" 
                     Height="8" Width="8" Opacity="0" UseLayoutRounding="False" />
            <ContentPresenter x:Name="ContentPresenter" 
                              ContentTemplate="{TemplateBinding ContentTemplate}" 
                              Content="{TemplateBinding Content}" 
                              Margin="{TemplateBinding Padding}" Grid.Row="1" 
                              HorizontalAlignment="Center" 
                              VerticalAlignment="{TemplateBinding VerticalContentAlignment}"/>
        </Grid>
    </Border>
</ControlTemplate>
```

To better understand how [**VisualState**](https://msdn.microsoft.com/library/windows/apps/br209007) objects work, consider what happens when the [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/br209316) goes from the `Unchecked` state to the `Checked` state, then to the `Indeterminate` state, and then back to the `Unchecked` state. Here are the transitions.

|                                      |                                                                                                                                                                                                                                                                                                                                                |                                                   |
|--------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------|
| State transition                     | What happens                                                                                                                                                                                                                                                                                                                                   | CheckBox appearance when the transition completes |
| From `Unchecked` to `Checked`.       | The [**Setter**](https://msdn.microsoft.com/library/windows/apps/br208817) value of the `Checked` [**VisualState**](https://msdn.microsoft.com/library/windows/apps/br209007) is applied, so the [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) of `CheckGlyph` is 1.                                                                                                                                                         | An X is displayed.                                |
| From `Checked` to `Indeterminate`.   | The [**Setter**](https://msdn.microsoft.com/library/windows/apps/br208817) value of the `Indeterminate` [**VisualState**](https://msdn.microsoft.com/library/windows/apps/br209007) is applied, so the [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) of `IndeterminateGlyph` is 1. The **Setter** value of the `Checked` **VisualState** is removed, so the [**Opacity**](https://msdn.microsoft.com/library/windows/apps/br228078) of `CheckGlyph` is 0. | A circle is displayed.                            |
| From `Indeterminate` to `Unchecked`. | The [**Setter**](https://msdn.microsoft.com/library/windows/apps/br208817) value of the `Indeterminate` [**VisualState**](https://msdn.microsoft.com/library/windows/apps/br209007) is removed, so the [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) of `IndeterminateGlyph` is 0.                                                                                                                                           | Nothing is displayed.                             |

 
For more info about how to create visual states for controls, and in particular how to use the [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/br210490) class and the animation types, see [Storyboarded animations for visual states](https://msdn.microsoft.com/library/windows/apps/xaml/jj819808).

## Use tools to work with themes easily

A fast way to apply themes to your controls is to right-click on a control on the Microsoft Visual Studio **Document Outline** and select **Edit Theme** or **Edit Style** (depending on the control you are right-clicking on). You can then apply an existing theme by selecting **Apply Resource** or define a new one by selecting **Create Empty**.

## Controls and accessibility

When you create a new template for a control, in addition to possibly changing the control's behavior and visual appearance, you might also be changing how the control represents itself to accessibility frameworks. The Universal Windows Platform (UWP) supports the Microsoft UI Automation framework for accessibility. All of the default controls and their templates have support for common UI Automation control types and patterns that are appropriate for the control's purpose and function. These control types and patterns are interpreted by UI Automation clients such as assistive technologies, and this enables a control to be accessible as a part of a larger accessible app UI.

To separate the basic control logic and also to satisfy some of the architectural requirements of UI Automation, control classes include their accessibility support in a separate class, an automation peer. The automation peers sometimes have interactions with the control templates because the peers expect certain named parts to exist in the templates, so that functionality such as enabling assistive technologies to invoke actions of buttons is possible.

When you create a completely new custom control, you sometimes also will want to create a new automation peer to go along with it. For more info, see [Custom automation peers](../accessibility/custom-automation-peers.md).

## Learn more about a control's default template

The topics that document the styles and templates for XAML controls show you excerpts of the same starting XAML you'd see if you used the **Edit Theme** or **Edit Style** techniques explained previously. Each topic lists the names of the visual states, the theme resources used, and the full XAML for the style that contains the template. The topics can be useful guidance if you've already started modifying a template and want to see what the original template looked like, or to verify that your new template has all of the required named visual states.

## Theme resources in control templates

For some of the attributes in the XAML examples, you may have noticed resource references that use the [{ThemeResource} markup extension](../../xaml-platform/themeresource-markup-extension.md). This is a technique that enables a single control template to use resources that can be different values depending on which theme is currently active. This is particularly important for brushes and colors, because the main purpose of the themes is to enable users to choose whether they want a dark, light, or high contrast theme applied to the system overall. Apps that use the XAML resource system can use a resource set that's appropriate for that theme, so that the theme choices in an app's UI are reflective of the user's systemwide theme choice.

 ## Get the sample code
* [XAML UI basics sample](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/XamlUIBasics)
* [Custom text edit control sample](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/CustomEditControl)

 



