---
title: "Choose the right visual tree viewer for your Windows app"
description: "This topic describes various visual tree viewers, also known as a UI visualizers, which are tools used to inspect and interact with UI components in a Windows app at run time."
ms.topic: product-comparison
ms.date: 01/11/2024
ms.localizationpriority: medium

#customer intent: As a developer, I want to test my app UI so that I can improve UX and debug issues.

---

# Choose the right visual tree viewer for your Windows app

A visual tree viewer, also known as a UI visualizer, is a tool used to inspect and interact with UI components in a Windows app at run time.

This can be helpful for prototyping, improving user experiences, and debugging UI issues through capabilities such as viewing and traversing component hierarchy, component highlighting, getting and setting state, and deep-linking to associated code.

## Recommended tools

The following table identifies several UI visualization tools and the UI frameworks and Windows app platforms they support. A summary of each tool can be found after the table.

|UI platform/framework    |Visual Studio - [**Live Visual Tree**](#visual-studio---live-visual-tree)  |[Spy++](#spy)        |[Accessibility Insights](#accessibility-insights-for-windows---live-inspect)  |[Chromium UI DevTools](#chromium-ui-devtools-for-windows)  |
|-------------------------|------------------|-------------|------------------------|---------------------|
|[WinUI](../../winui/winui3/index.md) in the [Windows App SDK](../../windows-app-sdk/index.md)                   | ✅      | ❌           | ✅            | ❌                   |
|[WPF](/dotnet/desktop/wpf/)                      | ✅      | ❌           | ✅            | ❌                   |
|[React Native for Windows](/windows/dev-environment/javascript/react-native-for-windows)     | ✅      | ❌           | ✅            | ✅         |
|[.NET MAUI](/dotnet/maui/)                | ✅      | ❌           | ✅            | ❌                   |
|[WinForms](/dotnet/desktop/winforms/)                 | ✅      | ✅ | ✅            | ❌                   |
|[WinUI 2](../../winui/winui2/index.md) for [UWP](/windows/uwp/)               | ✅      | ❌           | ✅            | ❌                   |
|[Classic Visual Basic apps](/previous-versions/visualstudio/visual-basic-6/visual-basic-6.0-documentation) | ❌                | ✅ | ❌                      | ❌                   |
|[Classic Win32 apps](/windows/win32/)        | ❌                | ✅ | ❌                      | ❌                   |
|[Chromium-based apps](https://developer.chrome.com/docs/chromium)      | ❌                | ❌           | ❌                      | ✅         |

### Visual Studio - Live Visual Tree

The Live Visual Tree and Live Property Explorer features ship with Visual Studio and work in tandem to provide an interactive runtime view of the UI elements in your app.

#### When to use Live Visual Tree

Use these tools when building apps with [WinUI](../../winui/winui3/index.md) in the [Windows App SDK](../../windows-app-sdk/index.md), [WinUI 2](../../winui/winui2/index.md) for [UWP](/windows/uwp/), [WPF](/dotnet/desktop/wpf/), [.NET MAUI](/dotnet/maui/), [WinForms](/dotnet/desktop/winforms/), or [React Native for Windows](/windows/dev-environment/javascript/react-native-for-windows).

- For more information on WinUI in the Windows App SDK, WinUI 2 for UWP, and WPF, see [Inspect XAML properties while debugging](/visualstudio/xaml-tools/inspect-xaml-properties-while-debugging).
- For more information on .NET MAUI, see [Inspect the visual tree of a .NET MAUI app](/dotnet/maui/user-interface/live-visual-tree).

> [!NOTE]
> The [WPF Tree Visualizer](/visualstudio/debugger/how-to-use-the-wpf-tree-visualizer) is a legacy feature and is not in active development. You can use the WPF Tree visualizer to explore the visual tree of a WPF object, and to view the WPF dependency properties for the objects that are contained in that tree.

#### How to use Live Visual Tree

[XAML Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload) must be enabled to view the Live Visual Tree (this feature is enabled by default in Visual Studio 2019 and later).

To check if XAML Hot Reload is enabled, run your app with the Visual Studio debugger attached (**F5** or **Debug -> Start Debugging**). When the app starts, you should see the in-app toolbar, which confirms that XAML Hot Reload is available (as shown in the following image).

:::image type="content" source="images/in-app-toolbar.png" alt-text="Screenshot of the Visual Studio in-app debugging toolbar.":::

If you don't see the in-app toolbar, select **Debug > Options > XAML Hot Reload** from the Visual Studio menu bar. In the **Options** dialog box, make sure that the **Enable XAML Hot Reload** option is selected.

:::image type="content" source="images/debugging-options-enable-xaml-hot-reload.png" alt-text="Screenshot of the Visual Studio Debugging Options dialog box with Enable XAML Hot Reload highlighted.":::

Once your app is running in debug configuration (with the debugger attached), navigate to the Visual Studio menu bar (**Debug > Windows > Live Visual Tree**). This opens the Live Visual Tree pane with a real-time view of your XAML code.

By default, the **Just My XAML** option for the Live Visual Tree is selected. This provides a simplified view of the XAML element collection in your app and can be toggled on or off through the **Show Just My XAML** button as shown in the following image.

:::image type="content" source="images/live-visual-tree-just-my-xaml.png" alt-text="Screenshot of the Visual Studio Live Visual Tree with the Just My Xaml option enabled.":::

#### Capabilities of Live Visual Tree

The Live Visual Tree toolbar provides several options for selecting elements to examine through your application's UI at runtime.

- **Select Element in the Running Application**. With this mode on, when you select a UI element in the application, the Live Visual Tree automatically updates to show the node and it's properties in the tree corresponding to that element.

  :::image type="content" source="images/live-visual-tree-select-element-in-running-app.png" alt-text="Screenshot of the Visual Studio Live Visual Tree with the Select Element In The Running Application option enabled.":::

- **Display Layout Adorners in the Running Application**. With this mode on, the application window shows horizontal and vertical lines along the bounds of a selected object and a rectangle outlining its margins.

  :::image type="content" source="images/live-visual-tree-display-layoutladorners-in-running-app.png" alt-text="Screenshot of the Visual Studio Live Visual Tree with the Display Layout Adorners in the Running Application option enabled.":::

- **Preview selection**. With this mode on, Visual Studio shows the XAML where the element is declared (if you have access to the application source code).

  :::image type="content" source="images/live-visual-tree-preview-selection.png" alt-text="Screenshot of the Visual Studio Live Visual Tree with the Preview selection option enabled.":::

### Spy++

Spy++ (SPYXX.EXE) is a Win32-based utility that ships with Visual Studio and provides a graphical view of the system's processes, threads, windows, and window messages.

#### When to use Spy++

Use Spy++ when building a classic Win32 application or one that uses Win32 APIs to draw its UI elements, such as WinForms and [Classic Visual Basic apps](/previous-versions/visualstudio/visual-basic-6/visual-basic-6.0-documentation).

> [!NOTE]
> For .NET framework apps, Spy++ is of limited usefulness as the window messages and classes intercepted by Spy++ don't correspond to managed events and property values.

#### How to use Spy++

Spy++ can be started from either Visual Studio or the Developer Command Prompt for Visual Studio.

To start Spy++ from Visual Studio:

- Confirm that your Visual Studio installation includes the **C++ core desktop features** component from the **Desktop development with C++** workload. (This is installed by default with Visual Studio 2022.)
  :::image type="content" source="images/vs-installer-cpp-core-desktop-features.png" alt-text="Screenshot of the Visual Studio Installer with the Desktop development with C++ card checked and the C++ core desktop features installation status highlighted.":::
- When installed, Spy++ is available from the **Tools** menu.
- Spy++ runs independently of Visual Studio, which can be closed if no longer required.

To start Spy++ from the Developer Command Prompt for Visual Studio:

- Launch **Developer Command Prompt for Visual Studio** from the Windows **Start** menu.
  :::image type="content" source="images/vs-developer-command-prompt-from-start-menu.png" alt-text="Screenshot of the Windows Start menu with the Developer Command Prompt for VS 2022 highlighted.":::
- At the command prompt, enter spyxx.exe (or spyxx_amd64.exe for the 64-bit version) and press Enter.

For more specific information on how to use Spy++ from Visual Studio, see [Spy++ Toolbar](/visualstudio/debugger/spy-increment-toolbar).

#### Capabilities of Spy++

Spy++ displays a graphical tree of relationships among system objects, with the current desktop window at the top of the tree and child nodes representing all other windows listed according to the standard window hierarchy. It can provide valuable insight into an application's behavior that is not available through the Visual C++ debugger.

:::image type="content" source="images/spy++-app-window.png" alt-text="Screenshot of the Spy++ application window.":::

With Spy++ you can:

- Search for specific windows, threads, processes, or messages.
- View the properties of selected threads processes or messages.
- Select a window, thread, process, or message directly in the tree view.
- Use the **Finder Tool** to select a window using the mouse pointer (**Search -> Find Window**).
  :::image type="content" source="images/spy++-finder-tool.png" alt-text="Screenshot of the Spy++ Find Window dialog.":::
- Set a message option by using a complex message log selection parameter.

For Spy++ documentation, see [Spy++ Help](/visualstudio/debugger/spy-increment-help).

### Accessibility Insights for Windows - Live Inspect

[Accessibility Insights for Windows - Live Inspect](https://accessibilityinsights.io/docs/windows/overview/) is a downloadable Microsoft application that can help developers find and fix accessibility issues in Windows apps that support UI Automation. It helps developers verify that an element in an app has the correct UI Automation properties simply by hovering over the element or setting keyboard focus to it.

#### When to use Accessibility Insights - Live Inspect

Live Inspect is typically used in conjunction with Live Visual Tree, Spy++, and other tools when building apps with [WinUI](../../winui/winui3/index.md) in the [Windows App SDK](../../windows-app-sdk/index.md), [WinUI 2](../../winui/winui2/index.md) for [UWP](/windows/uwp/), [WPF](/dotnet/desktop/wpf/), [.NET MAUI](/dotnet/maui/), [WinForms](/dotnet/desktop/winforms/), or [React Native for Windows](/windows/dev-environment/javascript/react-native-for-windows).

#### How to use Accessibility Insights - Live Inspect

Accessibility Insights must be downloaded and installed from [Download Accessibility Insights](https://accessibilityinsights.io/downloads/).

#### Capabilities of Accessibility Insights - Live Inspect

Live Inspect displays a graphical tree of relationships among system objects with detail panes containing the UI Automation properties and patterns corresponding to the selected element. The current desktop window is displayed at the top of the tree and child nodes representing all other windows listed according to the standard window hierarchy.

With Live Inspect you can:

- Verify that an element in an app has the right UI Automation properties simply by hovering over the element or setting keyboard focus on it.
- Visually highlights elements in the target application.
- Test controls or patterns with manual or automated checks for compliance with numerous accessibility rules and guidelines.

:::image type="content" source="images/accessibility-insights-live-inspect.png" alt-text="Screenshot of the Accessibility Insights tool beside a basic target app.":::

To learn more about UI Automation, see [UI Automation Overview](/dotnet/framework/ui-automation/ui-automation-overview).

To learn more about Accessibility Insights, see [Accessibility Insights for Windows](https://accessibilityinsights.io/docs/windows/overview/)

### Chromium UI DevTools for Windows

[Chromium UI DevTools for Windows](https://chromium.googlesource.com/chromium/src/+/main/docs/ui/ui_devtools/index.md) is a tool from Google that lets you inspect the UI system like a webpage using the frontend DevTools Inspector.

#### When to use Chromium UI DevTools for Windows

Use Chrome UI DevTools if you're developing a Chromium project, including progressive web apps or Electron desktop apps. For more information on Electron, see the [DevTools extension](https://github.com/electron/electron/blob/main/docs/tutorial/devtools-extension.md) on GitHub.

#### How to use Chromium UI DevTools for Windows

The Chromium UI DevTools for Windows codebase must be downloaded from GitHub and built with Visual Studio. For more information, see the [UI DevTools Overview](https://chromium.googlesource.com/chromium/src/+/main/docs/ui/ui_devtools/index.md).

#### Capabilities of Chromium UI DevTools for Windows

Chromium UI DevTools for Windows uses a webpage front end to display and traverse views, windows, and other UI elements.

With Chromium UI DevTools for Windows you can:

- Use a hierarchical UI element tree to inspect UI elements and their properties.
- Use Inspect mode, which highlights a UI element when you hover over it and reveals the element's nodes in the UI element tree.
- Use the Elements panel to open a search bar and find an element in the UI element tree using name, tag, and style properties.
- Use the Sources panel to open the class header file in Chromium code search and pull in the code from local files.

## Related content
