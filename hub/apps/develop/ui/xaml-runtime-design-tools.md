---
title: XAML runtime design tools for WinUI 3
description: Learn how to use XAML Hot Reload, Live Visual Tree, Live Property Explorer, and a UI playground page to design and iterate on WinUI 3 UI at runtime.
ms.topic: how-to
ms.date: 05/28/2026
---

# XAML runtime design tools for WinUI 3

Visual Studio includes a set of runtime tools that let you design, iterate on, and inspect your WinUI 3 XAML while your app is running. Used together, they give you a fast edit-reload-inspect loop that shows your real UI — real styles, real data templates, real animations — rather than a static approximation.

> [!NOTE]
> The Visual Studio **XAML Designer** (the drag-and-drop Design tab) does not currently support WinUI 3 projects. If you've opened a `.xaml` file and see only the XML editor, your installation isn't broken — the designer is not available for WinUI 3. The feature request is tracked on Developer Community: [Add XAML Designer support for WinUI 3 desktop apps](https://developercommunity.visualstudio.com/t/add-xaml-designer-support-for-winui-3-desktop-apps/1496648). For background on the engineering effort, see [WindowsAppSDK discussion #4710](https://github.com/microsoft/WindowsAppSDK/discussions/4710).

The runtime tools described in this article are valuable whether or not a designer is available. This article walks through the workflow.

## What you'll use

| Tool | What it does | How to open it |
|------|-------------|----------------|
| **XAML Hot Reload** | Pushes XAML changes to your running app without restarting | Automatic — edit any `.xaml` file while debugging |
| **Live Visual Tree** | Shows the full runtime element tree and lets you select elements in the app | **Debug** > **Windows** > **Live Visual Tree** |
| **Live Property Explorer** | Displays and lets you edit every property on the selected element at runtime | **Debug** > **Windows** > **Live Property Explorer** |
| **XAML Live Preview** | Renders your running app inside Visual Studio so you can inspect without switching windows | **Debug** > **Windows** > **XAML Live Preview** |

> [!TIP]
> Dock **Live Visual Tree** and **Live Property Explorer** side by side. Together they give you a detailed inspection view that shows actual runtime values.

## Prerequisites

- Visual Studio 2022 version 17.0 or later (XAML Hot Reload and Live Visual Tree are included).
- A WinUI 3 project using the Windows App SDK. See [Start developing Windows apps](/windows/apps/get-started/start-here) to create one.
- Your project should target **.NET 6** or later (C#) or use C++/WinRT.

## Step 1: Start your app under the debugger

Press **F5** (or **Debug** > **Start Debugging**). XAML Hot Reload activates automatically when the debugger attaches.

The **XAML Hot Reload** toolbar appears on the in-app overlay. If you don't see it, verify the setting is enabled:

1. Go to **Tools** > **Options** > **Debugging** > **XAML Hot Reload**.
2. Confirm that **Enable XAML Hot Reload** is checked.

> [!IMPORTANT]
> XAML Hot Reload requires the debugger. Running without debugging (**Ctrl+F5**) disables Hot Reload, Live Visual Tree, and Live Property Explorer.

## Step 2: Edit XAML and see changes instantly

With your app running, switch to any `.xaml` file in the editor and make a change — for example, change a `Background` color or add a new `Button`. The running app updates within a second or two, without restarting.

### What Hot Reload can and can't do

Hot Reload handles most common edits:

- Adding, removing, or reordering elements
- Changing property values (colors, margins, text, sizes)
- Adding or modifying styles and resource dictionaries
- Changing data-template content

Some changes require a restart:

- Adding new classes, code-behind event handlers, or `x:Class` changes
- Modifying `App.xaml` merged dictionaries (sometimes)
- Changing `x:Bind` expressions that reference new properties

When Hot Reload can't apply a change, the toolbar shows a notification. Just restart the app to pick up those changes.

For more information, see [XAML Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload).

## Step 3: Inspect the visual tree and properties

Open the **Live Visual Tree** window (**Debug** > **Windows** > **Live Visual Tree**). This window shows every element in your running app's UI tree, organized by parent-child relationships.

### Select elements visually

1. In the **Live Visual Tree** toolbar, enable **Select Element in the Running Application** (the crosshair button).
2. Click any element in your running app — the tree navigates to that element and the **Live Property Explorer** shows its properties.

This is the runtime equivalent of clicking an element on a design surface, except it reflects exactly what your app renders, including elements created dynamically in code.

### Edit properties at runtime

In the **Live Property Explorer**, you can change property values in real time. For example:

- Change a `Margin` to adjust spacing and see the effect immediately.
- Set `Visibility` to `Collapsed` to test what happens when an element is hidden.
- Adjust `Width` or `Height` to test responsive layouts.

These runtime edits aren't saved back to your XAML file — they're for experimentation. Once you find values you like, type them into your `.xaml` file (where Hot Reload picks them up and makes them permanent).

## Step 4: Use XAML Live Preview

**XAML Live Preview** renders your running app inside a Visual Studio tab, so you can view the app and your XAML source side by side without switching windows.

To open it: **Debug** > **Windows** > **XAML Live Preview**.

This is especially useful on single-monitor setups where Alt-Tabbing between the app and Visual Studio is inconvenient.

For more information, see [XAML Live Preview](/visualstudio/xaml-tools/xaml-live-preview).

## Recommended: Create a UI playground page

Create a dedicated **playground page** in your project — a page whose only purpose is to let you experiment with controls, styles, and layouts. This gives you a scratchpad that's always ready for visual iteration, and it pairs especially well with the runtime tools above.

### Set up the playground

1. Right-click your project in **Solution Explorer** and select **Add** > **New Item** > **Blank Page (WinUI 3)**. Name it `UIPlayground.xaml`.

2. In `UIPlayground.xaml`, add a `ScrollViewer` with sections for each control or pattern you want to try:

    ```xml
    <Page
        x:Class="MyApp.UIPlayground"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

        <ScrollViewer Padding="24">
            <StackPanel Spacing="24">

                <!-- Section: Buttons -->
                <TextBlock Text="Buttons" Style="{StaticResource SubtitleTextBlockStyle}"/>
                <StackPanel Orientation="Horizontal" Spacing="8">
                    <Button Content="Standard"/>
                    <Button Content="Accent" Style="{StaticResource AccentButtonStyle}"/>
                    <ToggleButton Content="Toggle"/>
                    <HyperlinkButton Content="Link" NavigateUri="https://learn.microsoft.com"/>
                </StackPanel>

                <!-- Section: Text input -->
                <TextBlock Text="Text input" Style="{StaticResource SubtitleTextBlockStyle}"/>
                <TextBox PlaceholderText="Type here..." Width="300" HorizontalAlignment="Left"/>
                <PasswordBox PlaceholderText="Password" Width="300" HorizontalAlignment="Left"/>

                <!-- Add more sections as needed -->

            </StackPanel>
        </ScrollViewer>
    </Page>
    ```

3. Make the playground easy to reach. During development, set it as the startup page in `App.xaml.cs`:

    ```csharp
    // In App.xaml.cs, OnLaunched method — swap in for quick playground access:
    // rootFrame.Navigate(typeof(UIPlayground));
    ```

    Or, if your app uses a `NavigationView`, add a menu item that only appears in debug builds. For example, in your main window's code-behind:

    ```csharp
    // In your main window or shell page constructor, after InitializeComponent():
    #if DEBUG
        NavView.MenuItems.Add(new NavigationViewItem
        {
            Content = "UI Playground",
            Tag = typeof(UIPlayground)
        });
    #endif
    ```

    where `NavView` is the `x:Name` of your `NavigationView` control.

### How to use the playground

1. Press **F5** to start the app and navigate to the playground page.
2. Open the `.xaml` file side by side with the running app (or use XAML Live Preview).
3. Add or modify controls in the XAML editor — Hot Reload pushes changes instantly.
4. Use **Live Visual Tree** to click on rendered controls and inspect their actual layout values in **Live Property Explorer**.
5. When you're happy with a control's look, copy the XAML into your real page.

This pattern gives you a "type-reload-inspect" cycle — and because it runs real code, you see real data templates, real styles, and real animations.

## Comparing design-time and runtime approaches

Here's how common UI design tasks map to the runtime tools:

| Task | Runtime tool |
|------|-------------|
| Add a control to the page | Type the XAML element — IntelliSense completes the tag and required properties |
| Preview the control | See it live in the running app via **Hot Reload** |
| Select an element to inspect | Use **Live Visual Tree** > **Select Element** (crosshair) |
| View or edit properties | Use **Live Property Explorer** |
| Adjust size or position | Edit `Width`, `Height`, `Margin` in XAML or in Live Property Explorer |

## Tips for an efficient workflow

- **Use the WinUI 3 Gallery app.** Install it from the [Microsoft Store](https://apps.microsoft.com/detail/9p3jfpwwdzrc) or build it from [GitHub](https://github.com/microsoft/WinUI-Gallery). It shows every WinUI 3 control with interactive examples and copy-ready XAML. Think of it as a visual catalog you can browse before writing XAML.
- **Keep your playground page open.** Treat it like a living style guide for your app.
- **Use `d:DesignHeight` and `d:DesignWidth`.** Even without the designer, these attributes help IntelliSense and keep your pages a consistent size when you open them in the editor.
- **Combine with XAML Binding Failures window.** Open it from **Debug** > **Windows** > **XAML Binding Failures** to catch binding errors that the designer would never have shown you.
- **Use the community toolkit.** The [Windows Community Toolkit](https://github.com/CommunityToolkit/Windows) provides controls and helpers that reduce the amount of layout you need to hand-code.

## Related content

- [XAML Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload)
- [XAML Live Preview](/visualstudio/xaml-tools/xaml-live-preview)
- [Inspect XAML properties while debugging](/visualstudio/xaml-tools/inspect-xaml-properties-while-debugging)
- [Choose the right visual tree viewer](visual-tree.md)
- [Controls overview](controls/index.md)
- [WinUI 3 Gallery app](https://github.com/microsoft/WinUI-Gallery)
