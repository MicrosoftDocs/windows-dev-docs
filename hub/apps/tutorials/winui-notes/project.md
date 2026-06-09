---
title: WinUI Notes tutorial - Step 1 - Project setup
description: WinUI Notes tutorial - Step 1 - Project setup.
author: jwmsft
ms.author: jimwalk
ms.date: 09/02/2025
ms.topic: tutorial
no-loc: ["App.xaml", "App.xaml.cs", "MainWindow.xaml", "MainWindow.xaml.cs", "Package.appxmanifest"]
---
# Project setup

Before you get into coding the app, we'll take a quick look at the Visual Studio project and take care of some project setup. When Visual Studio creates a WinUI 3 project, several important folders and code files are generated. These can be seen in the **Solution Explorer** pane of Visual Studio:

:::image type="content" source="media/project/vs-solution-explorer.png" alt-text="Solution Explorer showing the files for a WinUI project in Visual Studio.":::

The items listed here are the ones you'll primarily interact with. These files help get the WinUI app configured and running. Each file serves a different purpose, described below:

- **Assets** folder

  This folder contains your app's logo, images, and other media assets. It starts out populated with placeholder files for your app's logo. This logo represents your app in the Windows Start Menu, the Windows taskbar, and in the Microsoft Store when your app is published there.

- **App.xaml** and **App.xaml.cs**

  The **App.xaml** file contains app-wide XAML resources, such as colors, styles, or templates. The **App.xaml.cs** file generally contains code that instantiates and activates the application window. In this project, it points to the `MainWindow` class.

- **MainWindow.xaml** and **MainWindow.xaml.cs**

  These files represent your app's window.

- **Package.appxmanifest**

  This [package manifest file](/uwp/schemas/appxpackage/uapmanifestschema/generate-package-manifest) lets you configure publisher information, logos, processor architectures, and other details that determine how your app appears in the Microsoft Store.

### XAML files and partial classes

_Extensible Application Markup Language_ (XAML) is a declarative language that can initialize objects and set properties of objects. You can create visible UI elements in the declarative XAML markup. You can then associate a separate code file for each XAML file (typically called a _code-behind_ file) that can respond to events and manipulate the objects that you originally declare in XAML.

There are generally two files with any XAML file, the `.xaml` file itself, and a corresponding code-behind file that is a child item of it in the **Solution Explorer**.

- The `.xaml` file contains XAML markup that defines your app UI. The class name is declared with the `x:Class` attribute.
- The code file contains code you create to interact with the XAML markup and a call to the `InitializeComponent` method. The class is defined as a `partial class`.

When you build your project, the `InitializeComponent` method is called to parse the `.xaml` file and generate code that's joined with the code `partial class` to create the complete class.

:::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [XAML Overview](/windows/apps/develop/platform/xaml/xaml-overview)
- [Partial Classes and Methods (C# Programming Guide)](/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods)
- [x:Class attribute](/windows/apps/develop/platform/xaml/x-class-attribute), [x:Class Directive](/dotnet/desktop/xaml-services/xclass-directive)

## Update MainWindow

The `MainWindow` class included with the project is a sub-class of the XAML [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) class, which is used to define the shell of the app. The app's window has two parts:

- The _client_ portion of the window is where your content goes.
- The _non-client_ portion is the part controlled by the Windows Operating System. It includes the title bar, where the caption controls (Min/Max/Close buttons), app icon, title, and drag area are. It also includes the frame around the outside of the window.

To make the WinUI Notes app consistent with [Fluent Design guidelines](/windows/apps/design), you'll make a few modifications to `MainWindow`. First, you'll apply [Mica](/windows/apps/design/style/mica) material as the window backdrop. Mica is an opaque, dynamic material that incorporates theme and desktop wallpaper to paint the background of the window. Then, you'll extend your app's content into the [title bar](/windows/apps/design/basics/titlebar-design) area and replace the system title bar with a XAML [TitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.titlebar) control. This makes better use of space and gives you more control over the design, while providing all the functionality required of the title bar.

You'll also add a [Frame](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame) as the content of the window. The `Frame` class works with the [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page) class to let you navigate between pages of content in your app. You'll add the pages in a later step.

> [!TIP]
> You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes). To see the code as it is in this step, see this commit: [note page - initial](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/1cfe40378cd9ffe18acfe39a8707b7705546cfa2/WinUINotes).

1. Double-click **MainWindow.xaml** in **Solution Explorer** to open it.
1. Between the opening and closing`<Window.. >` tags, replace any existing XAML with this:

    ```xaml
    <Window.SystemBackdrop>
        <MicaBackdrop Kind="Base"/>
    </Window.SystemBackdrop>

    <Grid>
        <Grid.RowDefinitions>
            <!-- Title Bar -->
            <RowDefinition Height="Auto" />
            <!-- App Content -->
            <RowDefinition Height="*" />     
        </Grid.RowDefinitions>
        <TitleBar x:Name="AppTitleBar"
                  Title="WinUI Notes">
            <TitleBar.IconSource>
                <FontIconSource Glyph="&#xF4AA;"/>
            </TitleBar.IconSource>
        </TitleBar>

        <!-- App content -->
        <Frame x:Name="rootFrame" Grid.Row="1"/>
    </Grid>
    ```

1. Save the file by pressing <kbd>CTRL + S</kbd>, clicking the Save icon in the tool bar, or by selecting the menu **File** > **Save MainWindow.xaml**.

Don't worry if you don't understand what all this XAML markup does right now. As you build the rest of the app UI, we'll explain XAML concepts in more detail.

> [!NOTE]
> In this markup, you see two different ways to set properties in XAML. The first and shortest way is to use XAML _attribute syntax_, like this: `<object attribute="value">`. This works great for simple values, such as `<MicaBackdrop Kind="Base"/>`. But it only works for values where the XAML parser knows how to convert the string to the expected value type.
>
> If the property value is more complex, or the XAML parser doesn't know how to convert it, you must use _property element syntax_ to create the object. like this:
>
> ```xaml
> <object ... >
>     <object.property>
>         value
>     </object.property>
> </object>
> ```
>
> For example, to set the `Window.SystemBackdrop` property, you have to use property element syntax and explicitly create the `MicaBackdrop` element. But you can use simple attribute syntax to set the `MicaBackdrop.Kind` property.
>>
> ```xaml
> <Window ... >
>    <Window.SystemBackdrop>
>        <MicaBackdrop Kind="Base"/>
>    </Window.SystemBackdrop>
>     ...
> </Window>
> ```
>
> In `MainWindow.xaml`, `<Window.SystemBackdrop>`, `<Grid.RowDefinitions>`, and `<TitleBar.IconSource>` contain complex values that must be set with property element syntax.
>
> :::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:
>
> - [XAML syntax guide](/windows/apps/develop/platform/xaml/xaml-syntax-guide)

If you run the app now, you'll see the XAML `TitleBar` element you added, but it will be below the system title bar, which is still showing.

:::image type="content" source="media/project/title-bars.png" alt-text="The empty WinUI Notes app window with both the system title bar and custom title bar showing.":::

You need to write a bit of code to finish replacing the system title bar.

1. Open **MainWindow.xaml.cs**. You can open the code-behind for **MainWindow.xaml** (or any XAML file) in three ways:

    - If the **MainWindow.xaml** file is open and is the active document being edited, press <kbd>F7</kbd>.
    - If the **MainWindow.xaml** file is open and is the active document being edited, right-click in the text editor and select **View Code**.
    - Use **Solution Explorer** to expand the **MainWindow.xaml** entry, revealing the **MainWindow.xaml.cs** file. Double-click the file to open it.

1. In the constructor for `MainWindow`, add this code after the call to `InitializeComponent`:

    ```csharp
    public MainWindow()
    {
        this.InitializeComponent();

        // ↓ Add this. ↓
        // Hide the default system title bar.
        ExtendsContentIntoTitleBar = true;
        // Replace system title bar with the WinUI TitleBar.
        SetTitleBar(AppTitleBar);
        // ↑ Add this. ↑
    }
    ```

    The [ExtendsContentIntoTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.extendscontentintotitlebar) property hides the default system title bar and extends your app XAML into that area. The call to [SetTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.settitlebar) then tells the system to treat the XAML element you specified (`AppTitleBar`) as the title bar for the app.

1. Build and run the project by pressing <kbd>F5</kbd>, clicking the Debug "Start" button in the tool bar, or by selecting the menu **Debug** > **Start Debugging**.

When you run the app now, you'll see an empty window with a mica background and the XAML title bar that's replaced the system title bar.

:::image type="content" source="media/project/empty-window.png" alt-text="The empty WinUI Notes app window with the icon and app name in the title bar.":::

> [!div class="nextstepaction"]
> [Continue to step 2 - Add a note page](note.md)
