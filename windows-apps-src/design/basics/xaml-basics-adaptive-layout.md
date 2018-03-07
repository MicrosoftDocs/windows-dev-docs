---
author: muhsinking
title: Create adaptive layouts tutorial
description: This article covers the basics of adaptive layout in XAML
keywords: XAML, UWP, Getting Started
ms.author: mukin
ms.date: 08/30/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Tutorial: Create adaptive layouts

This tutorial covers the basics of using XAML's adaptive and tailored layout features, which let you create apps that look at home on any device. You'll learn how to create a new DataTemplate, add window snap points, and tailor your app's layout using the VisualStateManager and AdaptiveTrigger elements. We'll use these tools to optimize an image editing program for smaller device screens. 

The image editing program you'll be working on has two pages/screens:

The **main page**, which displays a photo gallery view, along with some information about each image file.

![MainPage](../basics/images/xaml-basics/mainpage.png)

The **details page**, which displays a single photo after it has been selected. A flyout editing menu allows the photo to be altered, renamed, and saved.

![DetailPage](../basics/images/xaml-basics/detailpage.png)

## Prerequisites

* Visual Studio 2017: [Download Visual Studio 2017 Community (free)](https://www.visualstudio.com/thank-you-downloading-visual-studio/?sku=Community&rel=15&campaign=WinDevCenter&ocid=wdgcx-windevcenter-community-download) 
* Windows 10 SDK (10.0.15063.468 or later):  [Download the latest Windows SDK (free)](https://developer.microsoft.com/windows/downloads/windows-10-sdk)
* Windows mobile emulator: [Download the Windows 10 mobile emulator (free)](https://developer.microsoft.com/en-us/windows/downloads/sdk-archive)

## Part 0: Get the starter code from github

For this tutorial, you'll start with a simplified version of the PhotoLab sample. 

1. Go to [https://github.com/Microsoft/Windows-appsample-photo-lab](https://github.com/Microsoft/Windows-appsample-photo-lab). This takes you to the GitHub page for the sample. 
2. Next, you'll need to clone or download the sample. Click the **Clone or download** button. A sub-menu appears.
    <figure>
        <img src="../basics/images/xaml-basics/clone-repo.png" alt="The Clone or download menu on GitHub">
        <figcaption>The <b>Clone or download</b> menu on the Photo lab sample's GitHub page.</figcaption>
    </figure>

    **If you're not familiar with GitHub:**
    
    a. Click **Download ZIP** and save the file locally. This downloads a .zip file that contains all the project files you need.
    b. Extract the file. Use the File Explorer to navigate to the .zip file you just downloaded, right-click it, and select **Extract All...**. 
    c. Navigate to your local copy of the sample and go the `Windows-appsample-photo-lab-master\xaml-basics-starting-points\adaptive-layout` directory.    

    **If you are familiar with GitHub:**

    a. Clone the master branch of the repo locally.
    b. Navigate to the `Windows-appsample-photo-lab\xaml-basics-starting-points\adaptive-layout` directory.

3. Open the project by clicking `Photolab.sln`.

## Part 1: Run the mobile emulator

In the Visual Studio toolbar, make sure your Solution Platform is set to x86 or x64, not ARM, and then change your target device from Local Machine to one of the mobile emulators that you've installed (for example, Mobile Emulator 10.0.15063 WVGA 5 inch 1GB). Try running the Photo Gallery app in the mobile emulator you've selected by pressing **F5**.

As soon as the app starts, you'll probably notice that while the app works, it doesn't look great on such a small viewport. The fluid Grid element tries to accommodate for the limited screen real estate by reducing the number of columns displayed, but we are left with a layout that looks uninspired and ill-fitted to such a small viewport.

![Mobile layout: after](../basics/images/xaml-basics/adaptive-layout-mobile-before.png)

## Part 2: Build a tailored mobile layout
To make this app look good on smaller devices, we're going to create a separate set of styles in our XAML page that will only be used if a mobile device is detected.

### Create a new DataTemplate
We're going to tailor the gallery view of the application by creating a new DataTemplate for the images. Open MainPage.xaml from the Solution Explorer, and add the following code within the **Page.Resources** tags.

```XAML
<DataTemplate x:Key="ImageGridView_MobileItemTemplate"
              x:DataType="local:ImageFileInfo">

    <!-- Create image grid -->
    <Grid Height="{Binding ItemSize, ElementName=page}"
          Width="{Binding ItemSize, ElementName=page}">
        
        <!-- Place image in grid, stretching it to fill the pane-->
        <Image x:Name="ItemImage"
               Source="{x:Bind ImagePreview}"
               Stretch="UniformToFill">
        </Image>

    </Grid>
</DataTemplate>
```

This gallery template saves screen real estate by eliminating the border around images, and getting rid of the image metadata (filename, ratings, and so on.) below each thumbnail. Instead, we show each thumbnail as a simple square.

### Add metadata to a tooltip
We still want the user to be able to access the metadata for each image, so we'll add a tooltip to each image item. Add the following code within the **Image** tags of the DataTemplate you just created.

```XAML
<Image ...>

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
</Image>
```

This will show the title, file type, and dimensions of an image when you hover the mouse over the thumbnail (or press and hold on a touch screen).

### Add a VisualStateManager and StateTrigger

We've now created a new layout for our data, but the app currently has no way of knowing when to use this layout over the default styles. To fix this, we'll need to add a **VisualStateManager**. Add the following code to the root element of the page, **RelativePanel**.

```XAML
<VisualStateManager.VisualStateGroups>
    <VisualStateGroup>

        <!-- Add a new VisualState for mobile devices -->
        <VisualState x:Key="Mobile">

            <!-- Trigger visualstate when a mobile device is detected -->
            <VisualState.StateTriggers>
                <local:MobileScreenTrigger InteractionMode="Touch" />
            </VisualState.StateTriggers>

        </VisualState>
    </VisualStateGroup>
</VisualStateManager.VisualStateGroups>
```

This adds a new **VisualState** and **StateTrigger**, which will be triggered when the app detects that it is running on a mobile device (the logic for this operation can be found in MobileScreenTrigger.cs, which is provided for you in the PhotoLab directory). When the **StateTrigger** starts, the app will use whatever layout attributes are assigned to this **VisualState**.

### Add VisualState setters
Next, we'll use **VisualState** setters to tell the **VisualStateManager** what attributes to apply when the state is triggered. Each setter targets one property of a particular XAML element and sets it to the given value. Add this code to the mobile **VisualState** you just created, below the **VisualState.StateTriggers** element. 

```XAML
<VisualStateManager.VisualStateGroups>
    <VisualStateGroup>

        <VisualState x:Key="Mobile">
            ...

            <!-- Add setters for mobile visualstate -->
            <VisualState.Setters>

                <!-- Move GridView about the command bar -->
                <Setter Target="ImageGridView.(RelativePanel.Above)"
                        Value="MainCommandBar" />

                <!-- Switch to mobile layout -->
                <Setter Target="ImageGridView.ItemTemplate"
                        Value="{StaticResource ImageGridView_MobileItemTemplate}" />

                <!-- Switch to mobile container styles -->
                <Setter Target="ImageGridView.ItemContainerStyle"
                        Value="{StaticResource ImageGridView_MobileItemContainerStyle}" />

                <!-- Move command bar to bottom of the screen -->
                <Setter Target="MainCommandBar.(RelativePanel.AlignBottomWithPanel)"
                        Value="True" />
                <Setter Target="MainCommandBar.(RelativePanel.AlignLeftWithPanel)"
                        Value="True" />
                <Setter Target="MainCommandBar.(RelativePanel.AlignRightWithPanel)"
                        Value="True" />

                <!-- Adjust the zoom slider to fit mobile screens -->
                <Setter Target="ZoomSlider.Minimum"
                        Value="80" />
                <Setter Target="ZoomSlider.Maximum"
                        Value="180" />
                <Setter Target="ZoomSlider.TickFrequency"
                        Value="20" />
                <Setter Target="ZoomSlider.Value"
                        Value="100" />
            </VisualState.Setters>

        </VisualState>
    </VisualStateGroup>
</VisualStateManager.VisualStateGroups>

```

These setters set the **ItemTemplate** of the image gallery to the new **DataTemplate** that we created in the first part, and align the command bar and zoom slider with the bottom of the screen, so they are easier to reach with your thumb on a mobile phone screen.

### Run the app
Now try running the app using a mobile emulator. Does the new layout display successfully? You should see a grid of small thumbnails as below. If you still see the old layout, there may be a typo in your **VisualStateManager** code.

![Mobile layout: after](../basics/images/xaml-basics/adaptive-layout-mobile-after.png)

## Part 3: Adapt to multiple window sizes on a single device
Creating a new tailored layout solves the challenge of responsive design for mobile devices, but what about desktops and tablets? The app may look good at full screen, but if the user shrinks the window, they may end up with an awkward interface. We can ensure that the end-user experience always looks and feels right by using the **VisualStateManager** to adapt to multiple window sizes on a single device.

![Small window: before](../basics/images/xaml-basics/adaptive-layout-small-before.png)

### Add window snap points
The first step is to define the "snap points" at which different **VisualStates** will be triggered. Open App.xaml from the Solution Explorer, and add the following code between the **Application** tags.

```XAML
<Application.Resources>
    <!--  window width adaptive snap points  -->
    <x:Double x:Key="MinWindowSnapPoint">0</x:Double>
    <x:Double x:Key="MediumWindowSnapPoint">641</x:Double>
    <x:Double x:Key="LargeWindowSnapPoint">1008</x:Double>
</Application.Resources>
```

This gives us three snap points, which allow us to create new **VisualStates** for three ranges of window sizes:
+ Small (0 - 640 pixels wide)
+ Medium (641 - 1007 pixels wide)
+ Large (> 1007 pixels wide)

### Create new VisualStates and StateTriggers
Next, we create the **VisualStates** and **StateTriggers** that correspond to each snap point. In MainPage.xaml, add the following code to the **VisualStateManager** that you created in Part 2.

```XAML
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

### Add setters
Finally, add these setters to the **SmallWindow** state.

```XAML

<VisualState x:Key="SmallWindow">
    ...

    <!-- Small window setters -->
    <VisualState.Setters>

        <!-- Apply mobile itemtemplate and styles -->
        <Setter Target="ImageGridView.ItemTemplate"
                Value="{StaticResource ImageGridView_MobileItemTemplate}" />
        <Setter Target="ImageGridView.ItemContainerStyle"
                Value="{StaticResource ImageGridView_MobileItemContainerStyle}" />

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

</VisualState>

```

These setters apply the mobile **DataTemplate** and styles to the desktop app, whenever the viewport is less than 641 pixels wide. They also tweak the zoom slider to better fit the small screen.

### Run the app

In the Visual Studio toolbar set the target device to **Local Machine**, and run the app. When the app loads, try changing the size of the window. When you shrink the window to a small size, you should see the app switch to the mobile layout you created in Part 2.

![Small window: after](../basics/images/xaml-basics/adaptive-layout-small-after.png)

## Going further

Now that you've completed this lab, you have enough adaptive layout knowledge to experiment further on your own. Try adding a rating control to the mobile-only tooltip you added earlier. Or, for a bigger challenge, try optimizing the layout for larger screen sizes (think TV screens or a Surface Studio)

If you get stuck, you can find more guidance in these sections of [Define page layouts with XAML](../layout/layouts-with-xaml.md).

+ [Visual states and state triggers](https://docs.microsoft.com/en-us/windows/uwp/layout/layouts-with-xaml#visual-states-and-state-triggers)
+ [Tailored layouts](https://docs.microsoft.com/en-us/windows/uwp/layout/layouts-with-xaml#tailored-layouts)

Alternatively, if you want to learn more about how the initial photo editing app was built, check out these tutorials on XAML [user interfaces](../basics/xaml-basics-ui.md) and [data binding](../../data-binding/xaml-basics-data-binding.md).

## Get the final version of the PhotoLab sample

This tutorial doesn't build up to the complete photo editing app, so be sure to check out the [final version](https://github.com/Microsoft/Windows-appsample-photo-lab) to see other features such as custom animations and phone support.