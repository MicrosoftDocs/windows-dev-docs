---
title: Create adaptive layouts tutorial
description: Learn how to use the adaptive layout features in XAML to create apps that look good at any window size.
keywords: XAML, UWP, Getting Started
ms.date: 08/20/2020
ms.topic: article
ms.localizationpriority: medium
---
# Tutorial: Create adaptive layouts

This tutorial covers the basics of using XAML's adaptive layout features, which let you create apps that look good at any size. You'll learn how to add window breakpoints, create a new DataTemplate, and use the VisualStateManager class tailor your app's layout. You'll use these tools to optimize an image editing program for smaller window sizes.

The image editing program has two pages. The _main page_ displays a photo gallery view, along with some information about each image file.

![MainPage](../basics/images/xaml-basics/mainpage.png)

The *details page* displays a single photo after it has been selected. A flyout editing menu allows the photo to be altered, renamed, and saved.

![DetailPage](../basics/images/xaml-basics/detailpage.png)

## Prerequisites

+ Visual Studio 2019: [Download Visual Studio 2019](https://visualstudio.microsoft.com/downloads/) (The Community edition is free.)
+ Windows 10 SDK (10.0.17763.0 or later):  [Download the latest Windows SDK (free)](https://developer.microsoft.com/windows/downloads/windows-10-sdk)
+ Windows 10, Version 1809 or later

## Part 0: Get the starter code from github

For this tutorial, you'll start with a simplified version of the PhotoLab sample.

1. Go to the GitHub page for the sample: [https://github.com/Microsoft/Windows-appsample-photo-lab](https://github.com/Microsoft/Windows-appsample-photo-lab).
2. Next, you'll need to clone or download the sample. Click the **Clone or download** button. A sub-menu appears.
    ![The Clone or download menu on the PhotoLab sample's GitHub page](images/xaml-basics/clone-repo.png)

    **If you're not familiar with GitHub:**

    a. Click **Download ZIP** and save the file locally. This downloads a .zip file that contains all the project files you need.

    b. Extract the file. Use File Explorer to browse to the .zip file you just downloaded, right-click it, and select **Extract All...**.

    c. Browse to your local copy of the sample and go the `Windows-appsample-photo-lab-master\xaml-basics-starting-points\adaptive-layout` directory.

    **If you are familiar with GitHub:**

    a. Clone the master branch of the repo locally.

    b. Browse to the `Windows-appsample-photo-lab\xaml-basics-starting-points\adaptive-layout` directory.

3. Double-click `Photolab.sln` to open the solution in Visual Studio.

## Part 1: Define window breakpoints

Run the app. It looks good at full screen, but the user interface (UI) isn't ideal when you shrink the window too small. You can ensure that the end-user experience always looks and feels right by using the [VisualStateManager](/uwp/api/windows.ui.xaml.visualstatemanager) class to adapt the UI to different window sizes.

![Small window: before](../basics/images/xaml-basics/adaptive-layout-small-before.png)

For more info about app layout, see the [Layout](../layout/index.md) section of the docs.

### Add window breakpoints

The first step is to define the _breakpoints_ at which different visual states are applied. See [Screen sizes and breakpoints](../layout/screen-sizes-and-breakpoints-for-responsive-design.md) for more information about the breakpoints for small, medium, and large screens.

Open App.xaml from the Solution Explorer, and add the following code after the `MergedDictionaries`, right before the closing `</ResourceDictionary>` tag.

```xaml
    <!--  Window width adaptive breakpoints.  -->
    <x:Double x:Key="MinWindowBreakpoint">0</x:Double>
    <x:Double x:Key="MediumWindowBreakpoint">641</x:Double>
    <x:Double x:Key="LargeWindowBreakpoint">1008</x:Double>
```

This creates 3 breakpoints, which lets you create new visual states for 3 ranges of window sizes:

+ Small (0 - 640 pixels wide)
+ Medium (641 - 1007 pixels wide)
+ Large (> 1007 pixels wide)

In this example, you create a new look only for the small window size. The medium and large sizes use the same look.

## Part 2: Add a data template for small window sizes

To make this app look good even when it's shown in a small window, you can create a new data template that optimizes how the images in the image gallery view are shown when the user shrinks the window.

### Create a new DataTemplate

 Open MainPage.xaml from the Solution Explorer, and add the following code within the `Page.Resources` tags.

```xaml
<DataTemplate x:Key="ImageGridView_SmallItemTemplate"
              x:DataType="local:ImageFileInfo">

    <!-- Create image grid -->
    <Grid Height="{Binding ItemSize, ElementName=page}"
          Width="{Binding ItemSize, ElementName=page}">

        <!-- Place image in grid, stretching it to fill the pane-->
        <Image x:Name="ItemImage"
               Source="{x:Bind ImageSource, Mode=OneWay}"
               Stretch="UniformToFill">
        </Image>

    </Grid>
</DataTemplate>
```

This gallery template saves screen real estate by eliminating the border around images, and getting rid of the image metadata (filename, ratings, and so on.) below each thumbnail. Instead, you show each thumbnail as a simple square.

### Add metadata to a tooltip

You still want the user to be able to access the metadata for each image, so add a tooltip to each image item. Add the following code within the `Image` tags of the DataTemplate you just created.

```xaml
    <!-- Add a tooltip to the image that displays metadata -->
    <ToolTipService.ToolTip>
        <ToolTip x:Name="tooltip">

            <!-- Arrange tooltip elements vertically -->
            <StackPanel Orientation="Vertical"
                        Grid.Row="1">

                <!-- Image title -->
                <TextBlock Text="{x:Bind ImageTitle, Mode=OneWay}"
                           HorizontalAlignment="Center"
                           Style="{StaticResource SubtitleTextBlockStyle}" />

                <!-- Arrange elements horizontally -->
                <StackPanel Orientation="Horizontal"
                            HorizontalAlignment="Center">

                    <!-- Image file type -->
                    <TextBlock Text="{x:Bind ImageFileType}"
                               HorizontalAlignment="Center"
                               Style="{StaticResource CaptionTextBlockStyle}" />

                    <!-- Image dimensions -->
                    <TextBlock Text="{x:Bind ImageDimensions}"
                               HorizontalAlignment="Center"
                               Style="{StaticResource CaptionTextBlockStyle}"
                               Margin="8,0,0,0" />
                </StackPanel>
            </StackPanel>
        </ToolTip>
    </ToolTipService.ToolTip>
```

This will show the title, file type, and dimensions of an image when you hover the mouse over the thumbnail (or press and hold on a touch screen).

## Part 3: Define visual states

You've now created a new layout for your data, but the app currently has no way of knowing when to use this layout over the default styles. To fix this, you need to add a [VisualStateManager](/uwp/api/windows.ui.xaml.visualstatemanager) and [VisualState](/uwp/api/windows.ui.xaml.visualstate) definitions.

### Add a VisualStateManager

Add the following code to the root element of the page, `RelativePanel`.

```xaml
<VisualStateManager.VisualStateGroups>
    <VisualStateGroup>
    ...

        <!-- Large window VisualState -->
        <VisualState x:Key="LargeWindow">

        </VisualState>

        <!-- Medium window VisualState -->
        <VisualState x:Key="MediumWindow">

        </VisualState>

        <!-- Small window VisualState -->
        <VisualState x:Key="SmallWindow">

        </VisualState>

    </VisualStateGroup>
</VisualStateManager.VisualStateGroups>
```

### Create StateTriggers to apply the visual state

Next, create the `StateTriggers` that correspond to each snap point. In MainPage.xaml, add the following code to the `VisualStateManager` that you created in Part 2.

```xaml
<VisualStateManager.VisualStateGroups>
    <VisualStateGroup>
    ...

        <!-- Large window VisualState -->
        <VisualState x:Key="LargeWindow">

            <!-- Large window trigger -->
            <VisualState.StateTriggers>
                <AdaptiveTrigger MinWindowWidth="{StaticResource LargeWindowSnapPoint}"/>
            </VisualState.StateTriggers>

        </VisualState>

        <!-- Medium window VisualState -->
        <VisualState x:Key="MediumWindow">

            <!-- Medium window trigger -->
            <VisualState.StateTriggers>
                <AdaptiveTrigger MinWindowWidth="{StaticResource MediumWindowSnapPoint}"/>
            </VisualState.StateTriggers>

        </VisualState>

        <!-- Small window VisualState -->
        <VisualState x:Key="SmallWindow">

            <!-- Small window trigger -->
            <VisualState.StateTriggers >
                <AdaptiveTrigger MinWindowWidth="{StaticResource MinWindowSnapPoint}"/>
            </VisualState.StateTriggers>

        </VisualState>

    </VisualStateGroup>
</VisualStateManager.VisualStateGroups>
```

When each visual state is triggered, the app will use whatever layout attributes are assigned to the active `VisualState`.

### Set properties for each visual state

Finally, set properties for each visual state to tell the `VisualStateManager` what attributes to apply when the state is triggered. Each setter targets one property of a particular XAML element and sets it to the given value. Add this code to the `SmallWindow` visual state you just created, after the `StateTriggers`.

```xaml
    <!-- Small window setters -->
    <VisualState.Setters>

        <!-- Apply small template and styles -->
        <Setter Target="ImageGridView.ItemTemplate"
                Value="{StaticResource ImageGridView_SmallItemTemplate}" />
        <Setter Target="ImageGridView.ItemContainerStyle"
                Value="{StaticResource ImageGridView_SmallItemContainerStyle}" />

        <!-- Adjust the zoom slider to fit small windows-->
        <Setter Target="ZoomSlider.Minimum"
                Value="80" />
        <Setter Target="ZoomSlider.Maximum"
                Value="180" />
        <Setter Target="ZoomSlider.TickFrequency"
                Value="20" />
        <Setter Target="ZoomSlider.Value"
                Value="100" />
    </VisualState.Setters>
```

These setters set the `ItemTemplate` of the image gallery to the new `DataTemplate` that you created in the previous section. They also tweak the zoom slider to better fit the small screen.

### Run the app

Run the app. When the app loads, try changing the size of the window. When you shrink the window to a small size, you should see the app switch to the small layout you created in Part 2.

![Small window: after](../basics/images/xaml-basics/adaptive-layout-small-after.png)

## Going further

Now that you've completed this lab, you have enough adaptive layout knowledge to experiment further on your own. For a bigger challenge, try optimizing the layout for larger screen sizes, like Surface Hub. See [Test Surface Hub apps using Visual Studio](../../debug-test-perf/test-surface-hub-apps-using-visual-studio.md) if you'd like to test a Surface Hub layout.

If you get stuck, you can find more guidance in these sections of [Define page layouts with XAML](../layout/layouts-with-xaml.md).

+ [Visual states and state triggers](../layout/layouts-with-xaml.md#visual-states-and-state-triggers)
+ [Tailored layouts](../layout/layouts-with-xaml.md#tailored-layouts)

Alternatively, if you want to learn more about how the initial photo editing app was built, check out these tutorials on XAML [user interfaces](../basics/xaml-basics-ui.md) and [data binding](../../data-binding/xaml-basics-data-binding.md).

## Get the final version of the PhotoLab sample

This tutorial doesn't build up to the complete photo editing app, so be sure to check out the [final version](https://github.com/Microsoft/Windows-appsample-photo-lab) to see other features such as custom animations and styles.