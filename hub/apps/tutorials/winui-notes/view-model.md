---
title: WinUI Notes - Step 3 - View-model
description: WinUI Notes - Step 3 - Add a view and model for the note
author: jwmsft
ms.author: jimwalk
ms.date: 09/02/2025
ms.topic: tutorial
no-loc: ["NotePage.xaml", "NotePage.xaml.cs", "Note.cs", "AllNotesPage", "WinUINotes"]
---
# Add a view and model for the note

This portion of the tutorial introduces the concepts of data views and models.

In the previous steps of the tutorial, you added a new page to the project that lets the user save, edit, or delete a single note. However, because the app needs to handle more than one note, you need to add another page that displays all the notes (call it `AllNotesPage`). This page lets the user choose a note to open in the editor page so they can view, edit, or delete it. It should also let the user create a new note.

To accomplish this, `AllNotesPage` needs to have a collection of notes, and a way to display the collection. This is where the app runs into trouble because the note data is tightly bound to the `NotePage` file. In `AllNotesPage`, you just want to display all the notes in a list or other collection view, with information about each note, like the date it was created and a preview of the text. With the note text being tightly bound to the `TextBox` control, there's no way to do this.

Before you add a page to show all the notes, let's make some changes to separate the note data from the note presentation.

## Views and models

Typically, a WinUI 3 app has at least a _view layer_ and a _data layer_.

The view layer defines the UI using XAML markup. The markup includes data binding expressions (such as x:Bind) that define the connection between specific UI components and data members. Code-behind files are sometimes used as part of the view layer to contain additional code needed to customize or manipulate the UI, or to extract data from event handler arguments before calling a method that performs the work on the data.

The data layer, or _model_, defines the types that represent your app data and related logic. This layer is independent of the view layer, and  you can create multiple different views that interact with the data.

Currently, the `NotePage` represents a view of data (the note text). However, after the data is read into the app from the system file, it exists only in the `Text` property of the `TextBox` in `NotePage`. It's not represented in the app in a way that lets you present the data in different ways or in different places; that is, the app doesn't have a data layer. You'll restructure the project now to create the data layer.

## Separate the view and model

> [!TIP]
> You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes). To see the code as it is in this step, see this commit: [note page - view-model](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/ca3a8ee6ebf268c54258ac7ce2eaede3f8a5466c/WinUINotes).

Refactor the existing code to separate the model from the view. The next few steps will organize the code so that views and models are defined separately from each other.

1. In **Solution Explorer**, right-click on the **WinUINotes** project and select **Add** > **New Folder**. Name the folder _:::no-loc text="Models":::_.
1. Right-click on the **WinUINotes** project again and select **Add** > **New Folder**. Name the folder _:::no-loc text="Views":::_.
1. Find the **NotePage.xaml** item and drag it to the **:::no-loc text="Views":::** folder. The **NotePage.xaml.cs** file should move with it.

    > [!NOTE]
    > When you move a file, Visual Studio usually prompts you with a warning about how the move operation may take a long time. This shouldn't be a problem here, press **OK** if you see this warning.
    >
    > Visual Studio may also ask you if you want to adjust the namespace of the moved file. Select **No**. You'll change the namespace in the next steps.

### Update the view namespace

Now that the view has been moved to the **:::no-loc text="Views":::** folder, you'll need to update the namespaces to match. The namespace for the XAML and code-behind files of the pages is set to `WinUINotes`. This needs to be updated to `WinUINotes.Views`.

1. In the **Solution Explorer** pane, expand **NotePage.xaml** to reveal the code-behind file.

1. Double-click on the **NotePage.xaml.cs** item to open the code editor if it's not already open. Change the namespace to `WinUINotes.Views`:

    ```csharp
    namespace WinUINotes.Views
    ```

1. Double-click on the **NotePage.xaml** item to open the XAML editor if it's not already open. The old namespace is referenced through the `x:Class` attribute, which defines which class type is the code-behind for the XAML. This entry isn't just the namespace, but the namespace with the type. Change the `x:Class` value to `WinUINotes.Views.NotePage`:

    ```xaml
    x:Class="WinUINotes.Views.NotePage"
    ```

### Fix the namespace reference in MainWindow

In the previous step, you created the note page and updated `MainWindow.xaml` to navigate to it. Remember that it was mapped with the `local:` namespace mapping. It's common practice to map the name `local` to the root namespace of your project, and the Visual Studio project template already does this for you (`xmlns:local="using:WinUINotes"`). Now that the page has moved to a new namespace, the type mapping in the XAML is now invalid.

Fortunately, you can add your own namespace mappings as needed. You need to do this to access items in different folders you create in your project. This new XAML namespace will map to the namespace of `WinUINotes.Views`, so name it `views`. The declaration should look like the following attribute: `xmlns:views="using:WinUINotes.Views"`.

1. In the **Solution Explorer** pane, double-click on the **MainWindow.xaml** entry to open it in the XAML editor.
1. Add this new namespace mapping on the line below the mapping for `local`:

    ```xaml
    xmlns:views="using:WinUINotes.Views"
    ```

1. The `local` XAML namespace was used to set the `Frame.SourcePageType` property, so change it to `views` there. Your XAML should now look like this:

    ```xaml
    <Window
        x:Class="WinUINotes.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="using:WinUINotes"
        xmlns:views="using:WinUINotes.Views"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d"
        Title="WinUI Notes">

        <!-- ... Unchanged XAML not shown. -->

            <Frame x:Name="rootFrame" Grid.Row="1"
                   SourcePageType="views:NotePage"/>

        <!-- ... Unchanged XAML not shown. -->

    </Window>
    ```

1. Build and run the app. The app should run without any compiler errors, and everything should still work as before.

## Define the model

Currently, the model (the data) is embedded in the note view. You'll create a new class to represent a note page's data:

1. In the **Solution Explorer** pane, right-click on the **:::no-loc text="Models":::** folder and select **Add** > **Class...**.
1. Name the class _Note.cs_ and press **Add**. The **Note.cs** file will open in the code editor.
1. Replace the code in the **Note.cs** file with this code, which makes the class `public` and adds properties and methods for handling a note:

    ```csharp
    using System;
    using System.Threading.Tasks;
    using Windows.Storage;

    namespace WinUINotes.Models
    {
        public class Note
        {
            private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            public string Filename { get; set; } = string.Empty;
            public string Text { get; set; } = string.Empty;
            public DateTime Date { get; set; } = DateTime.Now;

            public Note()
            {
                Filename = "notes" + DateTime.Now.ToBinary().ToString() + ".txt";
            }

            public async Task SaveAsync()
            {
                // Save the note to a file.
                StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(Filename);
                if (noteFile is null)
                {
                    noteFile = await storageFolder.CreateFileAsync(Filename, CreationCollisionOption.ReplaceExisting);
                }
                await FileIO.WriteTextAsync(noteFile, Text);
            }

            public async Task DeleteAsync()
            {
                // Delete the note from the file system.
                StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(Filename);
                if (noteFile is not null)
                {
                    await noteFile.DeleteAsync();
                }
            }
        }
    }
    ```

1. Save the file.

You'll notice that this code is very similar to the code in **NotePage.xaml.cs**, with a few changes and additions.

`Filename` and `Text` have been changed to `public` properties, and a new `Date` property has been added.

The code to save and delete the files has been placed in `public` methods. It is mostly identical to the code you used in the button `Click` event handlers in `NotePage`, but extra code to update the view after the file is deleted has been removed. It's not needed here because you'll be using data binding to keep the model and view synchronized.

These async method signatures return [Task](/dotnet/api/system.threading.tasks.task) instead of `void`. The `Task` class represents a single asynchronous operation that does not return a value. Unless the method signature requires `void`, as is the case for the `Click` event handlers, `async` methods should return a `Task`.

You also won't be keeping a reference to the `StorageFile` that holds the note anymore. You just try to get the file when you need it to save or delete.

In `NotePage`, you used a placeholder for the file name: `note.txt`. Now that the app supports more than one note, file names for saved notes need to be different and unique. To do this, set the `Filename` property in the constructor. You can use the [DateTime.ToBinary](/dotnet/api/system.datetime.tobinary) method to create a part of the file name based on the current time and make the file names unique. The generated file name looks like this: `notes-8584626598945870392.txt`.

### Update the note page

Now you can update the `NotePage` view to use the `Note` data model and delete code that was moved to the `Note` model.

1. Open the **Views\NotePage.xaml.cs** file if it's not already open in the editor.
1. After the last `using` statement at the top of the page, add a new `using` statement to give your code access to the classes in the `Models` folder and namespace.

    ```csharp
    using WinUINotes.Models;
    ```

1. Delete these lines from the class:

    ```csharp
    private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
    private StorageFile? noteFile = null;
    private string fileName = "note.txt";
    ```

1. Instead, add a `Note` object named `noteModel` in their place. This represents the note data that `NotePage` provides a view of.

    ```csharp
    private Note? noteModel;
    ```

1. You also don't need the `NotePage_Loaded` event handler anymore. You won't be reading text directly from the text file into the TextBox. Instead, the note text will be read into `Note` objects. You'll add the code for this when you add the `AllNotesPage` in a later step. Delete these lines.

    ```csharp
    Loaded += NotePage_Loaded;

    ...

    private async void NotePage_Loaded(object sender, RoutedEventArgs e)
    {
        noteFile = (StorageFile)await storageFolder.TryGetItemAsync(fileName);
        if (noteFile is not null)
        {
          NoteEditor.Text = await FileIO.ReadTextAsync(noteFile);
        }
    }
    ```

1. Replace the code in the `SaveButton_Click` method with this:

    ```csharp
    if (noteModel is not null)
    {
        await noteModel.SaveAsync();
    }
    ```

1. Replace the code in the `DeleteButton_Click` method with this:

    ```csharp
    if (noteModel is not null)
    {
        await noteModel.DeleteAsync();
    }
    ```

Now you can update the XAML file to use the `Note` model. Previously, you read the text directly from the text file into the `TextBox.Text` property in the code-behind file. Now, you use data binding for the `Text` property.

1. Open the **Views\NotePage.xaml** file if it's not already open in the editor.
1. Add a `Text` attribute to the `TextBox` control. Bind it to the `Text` property of `noteModel`: `Text="{x:Bind noteModel.Text, Mode=TwoWay}"`.
1. Update the `Header` to bind to the `Date` property of `noteModel`: `Header="{x:Bind noteModel.Date.ToString()}"`.

    ```xaml
    <TextBox x:Name="NoteEditor"
             <!-- ↓ Add this line. ↓ -->
             Text="{x:Bind noteModel.Text, Mode=TwoWay}"
             AcceptsReturn="True"
             TextWrapping="Wrap"
             PlaceholderText="Enter your note"
             <!-- ↓ Update this line. ↓ -->
             Header="{x:Bind noteModel.Date.ToString()}"
             ScrollViewer.VerticalScrollBarVisibility="Auto"
             Width="400"
             Grid.Column="1"/>
    ```

Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data. The `Mode=TwoWay` setting on the binding means that the `TextBox.Text` and `noteModel.Text` properties are automatically synchronized. When the text is updated in the `TextBox`, the changes are reflected in the `Text` property of the `noteModel`, and if `noteModel.Text` is changed, the updates are reflected in the `TextBox`.

The `Header` property uses the default `Mode` of `OneTime` because the `noteModel.Date` property doesn't change after the file is created. This code also demonstrates a powerful feature of `x:Bind` called [function binding](/windows/apps/develop/data-binding/function-bindings), which lets you use a function like `ToString` as a step in the binding path.

> [!IMPORTANT]
> It's important to choose the correct [BindingMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.bindingmode); otherwise, your data binding might not work as expected. (A common mistake with `{x:Bind}` is to forget to change the default `BindingMode` when `OneWay` or `TwoWay` is needed.)

| Name | Description |
| -- | -- |
| `OneTime` | Updates the target property only when the binding is created. Default for `{x:Bind}`. |
| `OneWay` | Updates the target property when the binding is created. Changes to the source object can also propagate to the target. Default for `{Binding}`. |
| `TwoWay` | Updates either the target or the source object when either changes. When the binding is created, the target property is updated from the source. |

Data binding supports the separation of your data and UI, and that results in a simpler conceptual model as well as better readability, testability, and maintainability of your app.

In WinUI, there are two kinds of binding you can choose from:

- The `{x:Bind}` markup extension is processed at compile-time. Some of its benefits are improved performance and compile-time validation of your binding expressions. It's recommended for binding in WinUI apps.
- The `{Binding}` markup extension is processed at run-time and uses general-purpose runtime object inspection.

 :::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Data binding in Windows apps](../../develop/data-binding/index.md)
- [{x:Bind}](/windows/apps/develop/platform/xaml/x-bind-markup-extension), [{Binding}](/windows/apps/develop/platform/xaml/binding-markup-extension)

## Data binding and MVVM

_Model-View-ViewModel_ (MVVM) is a UI architectural design pattern for decoupling UI and non-UI code that is popular with .NET developers. You'll probably see and hear it mentioned as you learn more about creating WinUI apps. Separating the views and models, as you've done here, is the first step towards a full MVVM implementation of the app, but it's as far as you'll go in this tutorial.

> [!NOTE]
> We've used the term "model" to refer to the data model in this tutorial, but it's important to note that this model is more closely aligned with the _ViewModel_ in a full MVVM implementation, while also incorporating aspects of the _Model_.

To learn more about MVVM, see these resources:

- [Windows data binding and MVVM](../../develop/data-binding/data-binding-and-mvvm.md)
- [Introduction to the MVVM Toolkit](/dotnet/communitytoolkit/mvvm/)
- [Data binding, dependency injection, and unit testing in WinUI](../winui-mvvm-toolkit/intro.md)
- [MVVM Building Blocks for WinUI and WPF Development](https://www.youtube.com/watch?v=83UVWrfYreU) on YouTube.

> [!div class="nextstepaction"]
> [Continue to step 4 - Add a view and model for all notes](all-notes.md)
