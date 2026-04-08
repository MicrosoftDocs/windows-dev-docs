---
title: WinUI Notes tutorial - Step 2 - Note page
description: WinUI Notes tutorial - Step 2 - Create a page for a note.
author: jwmsft
ms.author: jimwalk
ms.date: 09/02/2025
ms.topic: tutorial
no-loc: ["App.xaml", "App.xaml.cs", "MainWindow.xaml", "MainWindow.xaml.cs", "NotePage.xaml", "NotePage.xaml.cs"]
---
# Create a page for a note

Now you'll create a page that allows a user to edit a note, and then you'll write the code to save or delete the note.

> [!TIP]
> You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes). To see the code as it is in this step, see this commit: [note page - initial](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/1cfe40378cd9ffe18acfe39a8707b7705546cfa2/WinUINotes).

First, add the new page to the project:

1. In the **Solution Explorer** pane of Visual Studio, right-click on the **WinUINotes** project > **Add** > **New Item...**.
1. In the **Add New Item** dialog, select **WinUI** in the template list on the left-side of the window. Next, select the **Blank Page (WinUI)** template. Name the file _NotePage.xaml_, and then select **Add**.
1. The **NotePage.xaml** file will open in a new tab, displaying all of the XAML markup that represents the UI of the page. Replace the `<Grid> ... </Grid>` element in the XAML with the following markup:

    ```xaml
    <Grid Padding="16" RowSpacing="8">
        <Grid.RowDefinitions>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"/>
            <ColumnDefinition Width="400"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>

        <TextBox x:Name="NoteEditor"
             AcceptsReturn="True"
             TextWrapping="Wrap"
             PlaceholderText="Enter your note"
             Header="New note"
             ScrollViewer.VerticalScrollBarVisibility="Auto"
             Width="400"
             Grid.Column="1"/>

        <StackPanel Orientation="Horizontal"
                HorizontalAlignment="Right"
                Spacing="4"
                Grid.Row="1" Grid.Column="1">
            <Button Content="Save" Style="{StaticResource AccentButtonStyle}"/>
            <Button Content="Delete"/>
        </StackPanel>
    </Grid>
    ```

1. Save the file by pressing <kbd>CTRL + S</kbd>, clicking the Save icon in the tool bar, or by selecting the menu **File** > **Save NotePage.xaml**.

    If you run the app right now, you won't see the note page you just created. That's because you still need to set it as the content of the `Frame` control in `MainWindow`.

1. Open **MainWindow.xaml** and set `NotePage` as the [SourcePageType](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.sourcepagetype) on the `Frame`, like this:

    ```xaml
    <Frame x:Name="rootFrame" Grid.Row="1"
           SourcePageType="local:NotePage"/>
    ```

    Now when you run the app, the `Frame` will load an instance of `NotePage` and show it to the user.

> [!IMPORTANT]
> [XAML namespace (xmlns) mappings](/windows/apps/develop/platform/xaml/xaml-namespaces-and-namespace-mapping) are the XAML counterpart to the C# `using` statement. `local:` is a prefix that is mapped for you within the XAML pages for your app project (`xmlns:local="using:WinUINotes"`). It's mapped to refer to the same namespace that's created to contain the `x:Class` attribute and code for all the XAML files including **App.xaml**. As long as you define any custom classes you want to use in XAML in this same namespace, you can use the `local:` prefix to refer to your custom types in XAML.

Let's break down the key parts of the XAML controls placed on the page:

:::image type="content" source="media/note/app-layout.png" alt-text="The new note page UI with the grid highlighted by Visual Studio.":::

- The [Grid.RowDefinitions](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.grid.rowdefinitions) and [Grid.ColumnDefinitions](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.grid.columndefinitions) define a grid with 2 rows and 3 columns (placed below the title bar).
  - The bottom row is automatically (`Auto`) sized to fit its content, the two buttons. The top row uses all the remaining vertical space (`*`).
  - The middle column is `400`epx wide and is where the note editor goes. The columns on either side are empty and split all the remaining horizontal space between them (`*`).

  > [!NOTE]
  > Because of how the scaling system works, when you design your XAML app, you're designing in effective pixels, not actual physical pixels. Effective pixels (epx) are a virtual unit of measurement, and they're used to express layout dimensions and spacing, independent of screen density.

- `<TextBox x:Name="NoteEditor" ... > ... </TextBox>` is a text entry control ([TextBox](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox)) configured for multi-line text entry, and is placed in the top center cell of the `Grid` (`Grid.Column="1"`).  Row and column indexes are 0-based, and by default, controls are placed in row 0 and column 0 of the parent `Grid`. So this is the equivalent of specifying Row 0, Column 1.

- `<StackPanel Orientation="Horizontal" ... > ... </StackPanel>` defines a layout control ([StackPanel](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.stackpanel)) that stacks its children either vertically (default) or horizontally. It's placed in the bottom center cell of the `Grid` (`Grid.Row="1" Grid.Column="1"`).

  > [!NOTE]
  > `Grid.Row="1" Grid.Column="1"` is an example of XAML [attached properties](/windows/apps/develop/platform/xaml/attached-properties-overview). Attached properties let one XAML object set a property that belongs to a different XAML object. Often, as in this case, child elements can use attached properties to inform their parent element of how they are to be presented in the UI.

- Two `<Button>` controls are inside the `<StackPanel>` and arranged horizontally. You'll add the code to handle the buttons' [Click](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.buttonbase.click) events in the next section.

 :::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Responsive layouts with XAML](../../design/layout/layouts-with-xaml.md)
- [Layout panels](../../design/layout/layout-panels.md)
- [XAML namespaces and namespace mapping](/windows/apps/develop/platform/xaml/xaml-namespaces-and-namespace-mapping)

## Load and save a note

Open the **NotePage.xaml.cs** code-behind file. When you add a new XAML file, the code-behind contains a single line in the constructor, a call to the `InitializeComponent` method:

```csharp
namespace WinUINotes
{
    public sealed partial class NotePage : Page
    {
        public NotePage()
        {
            this.InitializeComponent();
        }
    }
}
```

The `InitializeComponent` method reads the XAML markup and initializes all of the objects defined by the markup. The objects are connected in their parent-child relationships, and the event handlers defined in code are attached to events set in the XAML.

Now you're going to add code to the **NotePage.xaml.cs** code-behind file to handle loading and saving notes.

1. Add the following variable declarations to the `NotePage` class:

    ```csharp
    public sealed partial class NotePage : Page
    {
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        private StorageFile? noteFile = null;
        private string fileName = "note.txt";
    ```

    When a note is saved, it's saved to the app's local storage as a text file.

    You use the [StorageFolder](/uwp/api/windows.storage.storagefolder) class to access the app's local data folder. This folder is specific to your app, so notes saved here can't be accessed by other apps. You use the [StorageFile](/uwp/api/windows.storage.storagefile) class to access the text file saved in this folder. The name of the file is represented by the `fileName` variable. For now, set `fileName` to "_note.txt_".

1. Create an event handler for the note page's [Loaded](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.loaded) event.

    ```csharp
    public NotePage()
    {
        this.InitializeComponent();
        // ↓ Add this. ↓
        Loaded += NotePage_Loaded;
    }

    // ↓ Add this event handler method. ↓
    private async void NotePage_Loaded(object sender, RoutedEventArgs e)
    {
        noteFile = (StorageFile)await storageFolder.TryGetItemAsync(fileName);
        if (noteFile is not null)
        {
            NoteEditor.Text = await FileIO.ReadTextAsync(noteFile);
        }
    }
    ```

    In this method, you call [TryGetItemAsync](/uwp/api/windows.storage.storagefolder.trygetitemasync) to retrieve the text file from the folder. If the file doesn't exist, it returns `null`. If the file does exist, call [ReadTextAsync](/uwp/api/windows.storage.fileio.readtextasync) to read the text from the file into the `NoteEditor` control's [Text](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox.text) property. (Remember, `NoteEditor` is the `TextBox` control you created in the XAML file. You reference it here in your code-behind file using the `x:Name` you assigned to it.)

    > [!IMPORTANT]
    > You need to mark this method with the `async` keyword because the file access calls are asynchronous. In short, if you call a method that ends in `...Async` (like `TryGetItemAsync`), you can add the [await](/dotnet/csharp/language-reference/operators/await) operator to the call. This keeps subsequent code from executing until the awaited call completes and keeps your UI responsive. When you use `await`, the method that you're calling from needs to be marked with the [async](/dotnet/csharp/language-reference/keywords/async) keyword. For more info, see [Call asynchronous APIs in C#](/windows/uwp/threading-async/call-asynchronous-apis-in-csharp-or-visual-basic).

 :::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Access files and folders with WinRT APIs](/windows/apps/develop/files/winrt-files)
- [Call asynchronous APIs in C#](/windows/uwp/threading-async/call-asynchronous-apis-in-csharp-or-visual-basic)

### Add event handlers

Next, add the [Click](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.buttonbase.click) event handlers for the **Save** and **Delete** buttons. Adding event handlers is something that you'll do often while creating your apps, so Visual Studio provides several features to make it easier.

1. In the **NotePage.xaml** file, place your cursor after the `Content` attribute in the **Save** `Button` control. Type `Click=`. At this point, Visual Studio should pop up an auto-complete UI that looks like this:

    :::image type="content" source="media/note/new-event-xaml.png" alt-text="A screenshot of the Visual Studio new event handler auto complete UI in the XAML editor":::

    - Press the down-arrow key to select **\<New Event Handler>**, then press <kbd>Tab</kbd>. Visual Studio will complete the attribute with `Click="Button_Click"` and add an event handler method named `Button_Click` in the **NotePage.xaml.cs** code-behind file.

    Now, you should rename the `Button_Click` method to something more meaningful. You'll do that in the following steps.

1. In **NotePage.xaml.cs**, find the method that was added for you:

    ```csharp
    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }
    ```

    > [!TIP]
    > To locate code in your app, click **Search** in the Visual  Studio title bar and use the **Code Search** option. Double-click the search result to open the code in the code editor.
    >
    > :::image type="content" source="media/note/vs-code-search.png" alt-text="Search feature in Visual Studio":::

1. Place your cursor before the "B" in `Button` and type `Save`. Wait a moment, and the method name will be highlighted in green.
1. When you hover over the method name, Visual Studio will show a tooltip with a screwdriver or lightbulb icon. Click the down-arrow button next to the icon, then click **Rename 'Button_Click' to 'SaveButton_Click**'.

    :::image type="content" source="media/note/method-rename.png" alt-text="The Visual Studio method rename popup UI.":::

    Visual Studio will rename the method everywhere in your app, including in the XAML file where you first added it to the `Button`.
1. Repeat these steps for the **Delete** button, and rename the method to `DeleteButton_Click`.

Now that the event handlers are hooked up, you can add the code to save and delete the note file.

1. Add this code in the `SaveButton_Click` method to save the file. Notice that you also need to update the method signature with the `async` keyword.

    ```csharp
    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (noteFile is null)
        {
            noteFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        }
        await FileIO.WriteTextAsync(noteFile, NoteEditor.Text);
    }
    ```

    In the `SaveButton_Click` method, you first check to see if `noteFile` has been created. If it's `null`, then you have to create a new file in the local storage folder with the name represented by the `fileName` variable, and assign the file to the `noteFile` variable. Then, you write the text in the `TextBox` control to the file represented by `noteFile`.
1. Add this code in the `DeleteButton_Click` method to delete the file. You need to update the method signature with the `async` keyword here, too.

    ```csharp
    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (noteFile is not null)
        {
            await noteFile.DeleteAsync();
            noteFile = null;
            NoteEditor.Text = string.Empty;
        }
    }
    ```

    In the `DeleteButton_Click` method, you first check to see if `noteFile` exists. If it does, delete the file represented by `noteFile` from the local storage folder and set `noteFile` to `null`. Then, reset the text in the `TextBox` control to an empty string.

    > [!IMPORTANT]
    > After the text file is deleted from the file system, it's important to set `noteFile` to `null`. Remember that `noteFile` is a [StorageFile](/uwp/api/windows.storage.storagefile) that provides access to the system file in your app. After the system file is deleted, `noteFile` still points to where the system file was, but doesn't know that it no longer exists. If you try to read, write, or delete the system file now, you'll get an error.

1. Save the file by pressing <kbd>CTRL + S</kbd>, clicking the Save icon in the tool bar, or by selecting the menu **File** > **Save NotePage.xaml.cs**.

The final code for the code-behind file should look like this:

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Storage;

namespace WinUINotes
{
    public sealed partial class NotePage : Page
    {
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        private StorageFile? noteFile = null;
        private string fileName = "note.txt";

        public NotePage()
        {
            this.InitializeComponent();
            Loaded += NotePage_Loaded;
        }

        private async void NotePage_Loaded(object sender, RoutedEventArgs e)
        {
            noteFile = (StorageFile)await storageFolder.TryGetItemAsync(fileName);
            if (noteFile is not null)
            {
                NoteEditor.Text = await FileIO.ReadTextAsync(noteFile);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (noteFile is null)
            {
                noteFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }
            await FileIO.WriteTextAsync(noteFile, NoteEditor.Text);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (noteFile is not null)
            {
                await noteFile.DeleteAsync();
                noteFile = null;
                NoteEditor.Text = string.Empty;
            }
        }
    }
}
```

## Test the note

With this code in place, you can test the app to make sure the note saves and loads correctly.

1. Build and run the project by pressing <kbd>F5</kbd>, clicking the Debug "Start" button in the tool bar, or by selecting the menu **Debug** > **Start Debugging**.
1. Type into the text entry box and press the **Save** button.
1. Close the app, then restart it. The note you entered should be loaded from the device's storage.
1. Press the **Delete** button.
1. Close the app, restart it. You should be presented with a new blank note.

> [!IMPORTANT]
> After you've confirmed that saving and deleting a note works correctly, create and save a new note again. You'll want to have a saved note to test the app in later steps.

> [!div class="nextstepaction"]
> [Continue to step 3 - Add a view and model for the note](view-model.md)
