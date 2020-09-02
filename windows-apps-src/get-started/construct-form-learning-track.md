---
title: Learning track - Construct and configure a form
description: Learn how to construct and configure a robust form  in a Universal Windows Platform (UWP) app for handling the input of a significant amount of information.
ms.date: 05/07/2018
ms.topic: article
keywords: get started, uwp, windows 10, learning track, layout, form
ms.localizationpriority: medium
ms.custom: RS5
---
# Create and customize a form

If you're creating an app that requires users to input a significant amount of information, chances are you'll want to create a form for them to fill out. This article will show you what you need to know in order to create a form that is useful and robust.

This is not a tutorial. If you want one, see our [adaptive layout tutorial](../design/basics/xaml-basics-adaptive-layout.md), which will provide you with a step-by-step guided experience.

We'll discuss what **XAML controls** go into a form, how to best arrange them on your page, and how to optimize your form for changing screen sizes. But because a form is about the relative position of visual elements, let's first discuss page layout with XAML.

## What do you need to know?

UWP does not have an explicit form control that you can add to your app and configure. Instead, you'll need to create a form by arranging a collection of UI elements on a page.

To do so, you'll need to understand **layout panels**. These are containers that hold your app's UI elements, allowing you to arrange and group them. Placing layout panels within other layout panels gives you a great deal of control over where and how your items display in relation to one another. This also makes it far easier to adapt your app to changing screen sizes.

Read [this documentation on layout panels](../design/layout/layout-panels.md). Because forms are usually displayed in one or more vertical columns, you'll want to group similar items in a **StackPanel**, and arrange those within a **RelativePanel** if you need to. Start putting together some panels now — if you need a reference, below is a basic layout framework for a two-column form:

```xaml
<RelativePanel>
    <StackPanel x:Name="Customer" Margin="20">
        <!--Content-->
    </StackPanel>
    <StackPanel x:Name="Associate" Margin="20" RelativePanel.RightOf="Customer">
        <!--Content-->
    </StackPanel>
    <StackPanel x:Name="Save" Orientation="Horizontal" RelativePanel.Below="Customer">
        <!--Save and Cancel buttons-->
    </StackPanel>
</RelativePanel>
```

## What goes in a form?

You'll need to fill your form with an assortment of [XAML Controls](../design/controls-and-patterns/controls-and-events-intro.md). You're probably familiar with those, but feel free to read up if you need a refresher. In particular, you'll want controls that allow your user to input text or choose from a list of values. This is a basic list of options you could add – you don't need to read everything about them, just enough so you understand what they look like and how they work.

* [TextBox](../design/controls-and-patterns/text-box.md) lets a user input text into your app.
* [ToggleSwitch](../design/controls-and-patterns/toggles.md) lets a user choose between two options.
* [DatePicker](../design/controls-and-patterns/date-picker.md) lets a user select a date value.
* [TimePicker](../design/controls-and-patterns/time-picker.md) lets a user select a time value.
* [ComboBox](/uwp/api/Windows.UI.Xaml.Controls.ComboBox) expand to display a list of selectable items. You can learn more about them [here](../design/controls-and-patterns/combo-box.md)

You also might want to add [buttons](../design/controls-and-patterns/buttons.md), so the user can save or cancel.

## Format controls in your layout

You know how to arrange layout panels and have items you'd like to add, but how should they be formatted? The [forms](../design/controls-and-patterns/forms.md) page has some specific design guidance. Read through the sections on **Types of forms** and **layout** for useful advice. We'll discuss accessibility and relative layout more shortly.

With that advice in mind, you should start adding your controls of choice into your layout, being sure they're given labels and spaced properly. As an example, here's the bare-bones outline for a single-page form using the above layout, controls, and design guidance:

```xaml
<RelativePanel>
    <StackPanel x:Name="Customer" Margin="20">
        <TextBox x:Name="CustomerName" Header= "Customer Name" Margin="0,24,0,0" HorizontalAlignment="Left" />
        <TextBox x:Name="Address" Header="Address" PlaceholderText="Address" Margin="0,24,0,0" HorizontalAlignment="Left" />
        <TextBox x:Name="Address2" Margin="0,24,0,0" PlaceholderText="Address 2" HorizontalAlignment="Left" />
	        <RelativePanel>
	            <TextBox x:Name="City" PlaceholderText="City" Margin="0,24,0,0" HorizontalAlignment="Left" />
	            <ComboBox x:Name="State" PlaceholderText="State" Margin="24,24,0,0" RelativePanel.RightOf="City">
	                <!--List of valid states-->
	            </ComboBox>
	        </RelativePanel>
    </StackPanel>
    <StackPanel x:Name="Associate" Margin="20" RelativePanel.Below="Customer">
        <TextBox x:Name="AssociateName" Header= "Associate" Margin="0,24,0,0" HorizontalAlignment="Left" />
	    <DatePicker x:Name="TargetInstallDate" Header="Target install Date" HorizontalAlignment="Left" Margin="0,24,0,0"></DatePicker>
	    <TimePicker x:Name="InstallTime" Header="Install Time" HorizontalAlignment="Left" Margin="0,24,0,0"></TimePicker>
    </StackPanel>
    <StackPanel x:Name="Save" Orientation="Horizontal" RelativePanel.Below="Associate">
        <Button Content="Save" Margin="24" />
        <Button Content="Cancel" Margin="24" />
    </StackPanel>
</RelativePanel>
```

Feel free to customize your controls with more properties for a better visual experience.

## Make your layout responsive

Users might view your UI on a variety of devices with different screen widths. To ensure that they have a good experience regardless of their screen, you should use [responsive design](../design/layout/responsive-design.md). Read through that page for good advice on the design philosophies to keep in mind as you proceed.

The [Responsive layouts with XAML](../design/layout/layouts-with-xaml.md) page gives a detailed overview of how to implement this. For now, we'll focus on **fluid layouts** and **visual states in XAML**.

The basic form outline that we've put together is already a **fluid layout**, as it's depending mostly on the relative position of controls with only minimal use of specific pixel sizes and positions. Keep this guidance in mind for more UIs you might create in the future, though.

More important to responsive layouts are **visual states.** A visual state defines property values that are applied to a given element when a given condition is true. [Read up on how to do this in xaml](../design/layout/layouts-with-xaml.md#set-visual-states-in-xaml-markup), and then implement them into your form. Here's what a *very* basic one might look like in our previous sample:

```xaml
<Page ...>
    <Grid>
        <VisualStateManager.VisualStateGroups>
            <VisualStateGroup>
                <VisualState>
                    <VisualState.StateTriggers>
                        <AdaptiveTrigger MinWindowWidth="640" />
                    </VisualState.StateTriggers>

                    <VisualState.Setters>
                        <Setter Target="Associate.(RelativePanel.RightOf)" Value="Customer"/>
                        <Setter Target="Associate.(RelativePanel.Below)" Value=""/>
                        <Setter Target="Save.(RelativePanel.Below)" Value="Customer"/>
                    </VisualState.Setters>
                </VisualState>
            </VisualStateGroup>
        </VisualStateManager.VisualStateGroups>

        <RelativePanel>
            <!-- Customer StackPanel -->
            <!-- Associate StackPanel -->
            <!-- Save StackPanel -->
        </RelativePanel>
    </Grid>
</Page>
```

> [!IMPORTANT]
> When you use StateTriggers, always ensure that VisualStateGroups is attached to the first child of the root. Here, **Grid** is the first child of the root **Page** element.

It's not practical to create visual states for a wide array of screen sizes, nor are more than a couple likely to have significant impact on the user experience of your app. We recommend designing instead for a few key breakpoints - you can [read more here](../design/layout/screen-sizes-and-breakpoints-for-responsive-design.md).

## Add accessibility support

Now that you have a well-constructed layout that responds to changes in screen sizes, a last thing you can do to improve the user experience is to [make your app accessible](../design/accessibility/accessibility-overview.md). There's a lot that can go into this, but in a form like this one it's easier than it looks. Focus on the following:

* Keyboard support - ensure the order of elements in your UI panels match how they're displayed on screen, so a user can easily tab through them.
* Screen reader support - ensure all your controls have a descriptive name.

When you're creating more complex layouts with more visual elements, you'll want to consult the [accessibility checklist](../design/accessibility/accessibility-checklist.md) for more details. After all, while accessibility isn't necessary for an app, it helps it reach and engage a larger audience.

## Going further

Though you've created a form here, the concepts of layouts and controls are applicable across all XAML UIs you might construct. Feel free to go back through the docs we've linked you to and experiment with the form you have, adding new UI features and further refining the user experience. If you want step-by-step guidance through more detailed layout features, see our [adaptive layout tutorial](../design/basics/xaml-basics-adaptive-layout.md)

Forms also don't have to exist in a vacuum - you could go one step forward and embed yours within a [master/details pattern](../design/controls-and-patterns/master-details.md) or a [pivot control](../design/controls-and-patterns/pivot.md). Or if you want to get to work on the code-behind for your form, you might want to get started with our [events overview](../xaml-platform/events-and-routed-events-overview.md).

## Useful APIs and docs

Here's a quick summary of APIs and other useful documentation to help you get started working with Data Binding.

### Useful APIs

| API | Description |
|------|---------------|
| [Controls useful for forms](../design/controls-and-patterns/forms.md#input-controls) | A list of useful input controls for creating forms, and basic guidance of where to use them. |
| [Grid](/uwp/api/Windows.UI.Xaml.Controls.Grid) | A panel for arranging elements in multi-row and multi-column layouts. |
| [RelativePanel](/uwp/api/Windows.UI.Xaml.Controls.RelativePanel) | A panel for arraging items in relation to other elements and to the panel's boundaries. |
| [StackPanel](/uwp/api/Windows.UI.Xaml.Controls.StackPanel) | A panel for arranging elements into a single horizontal or vertical line. |
| [VisualState](/uwp/api/Windows.UI.Xaml.VisualState) | Allows you to set the appearance of UI elements when they're in particular states. |

### Useful docs

| Topic | Description |
|-------|----------------|
| [Accessibility overview](../design/accessibility/accessibility-overview.md) | A broad-scale overview of accessibility options in apps. |
| [Accessibility checklist](../design/accessibility/accessibility-checklist.md) | A practical checklist to ensure your app meets accessibility standards. |
| [Events overview](../xaml-platform/events-and-routed-events-overview.md) | Details on adding and structuring events to handle UI actions. |
| [Forms](../design/controls-and-patterns/forms.md) | Overall guidance for creating forms. |
| [Layout panels](../design/layout/layout-panels.md) | Provides an overview of the types of layout panels and where to use them. |
| [Master/details pattern](../design/controls-and-patterns/master-details.md) | A design pattern that can be implemented around one or multiple forms. |
| [Pivot control](../design/controls-and-patterns/pivot.md) | A control that can contain one or multiple forms. |
| [Responsive design](../design/layout/responsive-design.md) | An overview of large-scale responsive design principles. |
| [Responsive layouts with XAML](../design/layout/layouts-with-xaml.md) | Specific information on visual states and other implementations of responsive design. |
| [Screen sizes for responsive design](../design/layout/screen-sizes-and-breakpoints-for-responsive-design.md) | Guidance on which screen sizes to which responsive layouts should be scoped. |

## Useful code samples

| Code sample | Description |
|-----------------|---------------|
| [Adaptive layout tutorial](../design/basics/xaml-basics-adaptive-layout.md) | A step-by-step guided experience through adaptive layouts and responsive design. |
| [Customer Orders Database](https://github.com/Microsoft/Windows-appsample-customers-orders-database) | See layout and forms in action on a multi-page enterprise sample. |
| [XAML Controls Gallery](https://github.com/Microsoft/Xaml-Controls-Gallery) | See a selection of XAML controls, and how they're implemented. |
| [Additional code samples](https://developer.microsoft.com/windows/samples) | Choose **Controls, layout, and text** in the category drop-down list to see related code samples. |