---
title: WinUI Notes tutorial - Step 4 - All notes
description: WinUI Notes - Step 4 - Add a view and model for all notes.
author: jwmsft
ms.author: jimwalk
ms.date: 09/02/2025
ms.topic: tutorial
no-loc: ["NotePage.xaml", "NotePage.xaml.cs", "Note.cs", "AllNotesPage.xaml", "AllNotes.cs", "WinUI 3 Gallery"]
---
# Add a view and model for all notes

This portion of the tutorial adds a new page to the app, a view that displays all of the notes previously created.

## Multiple notes and navigation

Currently the **note** view displays a single note. To display all your saved notes, create a new view and model: **AllNotes**.

01. In the **Solution Explorer** pane, right-click on the **:::no-loc text="Views":::** folder and select **Add** > **New Item...**
01. In the **Add New Item** dialog, select **WinUI** in the template list on the left-side of the window. Next, select the **Blank Page (WinUI 3)** template. Name the file _AllNotesPage.xaml_ and press **Add**.
01. In the **Solution Explorer** pane, right-click on the **:::no-loc text="Models":::** folder and select **Add** > **Class...**
01. Name the class _AllNotes.cs_ and press **Add**.

> [!TIP]
> You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes). To see the code as it is in this step, see this commit: [all notes view and model](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/df064567a0b2fdadb2a692afbe15bd24303dfe0b/WinUINotes).

## Code the AllNotes model

The new data model represents the data required to display multiple notes. Here, you'll get all the notes from the app's local storage and create a collection of `Note` objects that you'll display in the `AllNotesPage`.

01. In the **Solution Explorer** pane, open the **Models\AllNotes.cs** file.
01. Replace the code in the **AllNotes.cs** file with this code:

    ```csharp
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Windows.Storage;
    
    namespace WinUINotes.Models
    {
        public class AllNotes
        {
            public ObservableCollection<Note> Notes { get; set; } = 
                                        new ObservableCollection<Note>();
    
            public AllNotes()
            {
                LoadNotes();
            }
    
            public async void LoadNotes()
            {
                Notes.Clear();
                // Get the folder where the notes are stored.
                StorageFolder storageFolder = 
                              ApplicationData.Current.LocalFolder;
                await GetFilesInFolderAsync(storageFolder);
            }
    
            private async Task GetFilesInFolderAsync(StorageFolder folder)
            {
                // Each StorageItem can be either a folder or a file.
                IReadOnlyList<IStorageItem> storageItems = 
                                            await folder.GetItemsAsync();
                foreach (IStorageItem item in storageItems)
                {
                    if (item.IsOfType(StorageItemTypes.Folder))
                    {
                        // Recursively get items from subfolders.
                        await GetFilesInFolderAsync((StorageFolder)item);
                    }
                    else if (item.IsOfType(StorageItemTypes.File))
                    {
                        StorageFile file = (StorageFile)item ;
                        Note note = new Note()
                        {
                            Filename = file.Name,
                            Text = await FileIO.ReadTextAsync(file),
                            Date = file.DateCreated.DateTime
                        };
                        Notes.Add(note);
                    }
                }
            }
        }
    }
    ```

The previous code declares a collection of `Note` items, named `Notes`, and uses the `LoadNotes` method to load notes from the app's local storage.

The `Notes` collection uses an [ObservableCollection](/dotnet/api/system.collections.objectmodel.observablecollection-1), which is a specialized collection that works well with data binding. When a control that lists multiple items, such as an [ItemsView](../../design/controls/itemsview.md), is bound to an `ObservableCollection`, the two work together to automatically keep the list of items in sync with the collection. If an item is added to the collection, the control is automatically updated with the new item. If an item is added to the list, the collection is updated.

 :::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [StorageFolder class](/uwp/api/windows.storage.storagefolder), [StorageFile class](/uwp/api/windows.storage.storagefile), [IStorageItem.IsOfType method](/uwp/api/windows.storage.istorageitem.isoftype)
- [Access files and folders with Windows App SDK and WinRT APIs](../../develop/files/winrt-files.md)

Now that the `AllNotes` model is ready to provide data for the view, you need to create an instance of the model in `AllNotesPage` so the view can access the model.

1. In the **Solution Explorer** pane, open the **Views\AllNotesPage.xaml.cs** file.
1. In the `AllNotesPage` class, add this code to create an `AllNotes` model named _notesModel_:

    ```csharp
    public sealed partial class AllNotesPage : Page
    {
        // ↓ Add this. ↓
        private AllNotes notesModel = new AllNotes();
        // ↑ Add this. ↑

        public AllNotesPage()
        {
            this.InitializeComponent();
        }
    }
    ```

## Design the AllNotes page

Next, you need to design the view to support the `AllNotes` model.

1. In the **Solution Explorer** pane, open the **Views\AllNotesPage.xaml** file.
1. Replace the `<Grid> ... </Grid>` element with the following markup:

    ```xaml
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>

        <CommandBar DefaultLabelPosition="Right">
            <AppBarButton Icon="Add" Label="New note"/>
            <CommandBar.Content>
                <TextBlock Text="Quick notes" Margin="16,8" 
                       Style="{ThemeResource SubtitleTextBlockStyle}"/>
            </CommandBar.Content>
        </CommandBar>

        <ItemsView ItemsSource="{x:Bind notesModel.Notes}" 
               Grid.Row="1" Padding="16" >
            <ItemsView.Layout>
                <UniformGridLayout MinItemWidth="200"
                               MinColumnSpacing="12"
                               MinRowSpacing="12"
                               ItemsJustification="Start"/>
            </ItemsView.Layout>
        </ItemsView>
    </Grid>
    ```

The previous XAML introduces a few new concepts:

- The [CommandBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.commandbar) control contains an [AppBarButton](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.appbarbutton). This button has a `Label` and `Icon`, and is influenced by the `CommandBar` that contains it. For example, this `CommandBar` sets the label position of its buttons to `Right`. Command bars are usually displayed at the top of the app, along with the page title.
- The [ItemsView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview) control displays a collection of items, and in this case, is bound to the model's `Notes` property. The way items are presented by the items view is set through the `ItemsView.Layout` property. Here, you use a [UniformGridLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout).

Now that you've created `AllNotesPage`, you need to update `MainWindow.xaml` one last time so that it loads `AllNotesPage` instead of an individual `NotePage`.

1. In the **Solution Explorer** pane, open the **MainWindow.xaml** file.
1. Update the `rootFrame` element so that the `SourcePageType` points to `views.AllNotesPage`, like this:

    ```xaml
    <Frame x:Name="rootFrame" Grid.Row="1"
           SourcePageType="views:AllNotesPage"/>
    ```

If you run the app now, you'll see that the note you created previously is loaded into the `ItemsView` control. However, it's just shown as the string representation of the object. The `ItemsView` doesn't know how this item should be displayed. You'll correct this in the next section.

:::image type="content" source="media/all-notes/itemsview-no-template.png" alt-text="The notes app UI with the note list showing the Note class name instead of the note content.":::

### Add a data template

You need to specify a [DataTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.datatemplate) to tell the `ItemsView` how your data item should be shown. The `DataTemplate` is assigned to the [ItemsTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.itemtemplate) property of the `ItemsView`. For each item in the collection, the `ItemsView.ItemTemplate` generates the declared XAML.

1. In the **Solution Explorer** pane, double-click on the **AllNotesPage.xaml** entry to open it in the XAML editor.
1. Add this new namespace mapping on the line below the mapping for `local`:

    ```xaml
    xmlns:models="using:WinUINotes.Models"
    ```

1. Add a `<Page.Resources>` element after the opening `<Page...>` tag. This gets the [ResourceDictionary](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.resourcedictionary) from the `Page`'s [Resources](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.resources) property so that you can add XAML resources to it.

    ```xaml
    <Page
        x:Class="WinUINotes.Views.AllNotesPage"
        ... >
    <!-- ↓ Add this. ↓ -->
    <Page.Resources>

    </Page.Resources>
    ```

1. Inside the `<Page.Resources>` element, add the `DataTemplate` that describes how to display a `Note` item.

    ```xaml
    <Page.Resources>
        <!-- ↓ Add this. ↓ -->
        <DataTemplate x:Key="NoteItemTemplate" 
                      x:DataType="models:Note">
            <ItemContainer>
                <Grid Background="LightGray">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="120"/>
                        <RowDefinition Height="Auto"/>
                    </Grid.RowDefinitions>
                    <TextBlock Text="{x:Bind Text}" Margin="12,8"
                               TextWrapping="Wrap"
                               TextTrimming="WordEllipsis"/>
                    <Border Grid.Row="1" Padding="8,6,0,6"
                            Background="Gray">
                        <TextBlock Text="{x:Bind Date}"
                                   Foreground="White"/>
                    </Border>
                </Grid>
            </ItemContainer>
        </DataTemplate>
        <!-- ↑ Add this. ↑ -->
    </Page.Resources>
    ```

1. In the XAML for `ItemsView`, assign the `ItemTemplate` property to the data template you just created:

    ```xaml
    <ItemsView ItemsSource="{x:Bind notesModel.Notes}"
               Grid.Row="1" Margin="24"
               <!-- ↓ Add this. ↓ -->
               ItemTemplate="{StaticResource NoteItemTemplate}">
    ```

1. Build and run the app.

When you use the `x:Bind` markup extension in a `DataTemplate`, you have to specify the `x:DataType` on the `DataTemplate`. In this case, that's an individual `Note` (so you have to add a XAML namespace reference for `Models`). The template for the note uses two `TextBlock` controls, which are bound to the note's `Text` and `Date` properties. The [Grid](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.grid) element is used for layout and to provide a background color. A [Border](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.border) element is used for the background of the date. (The XAML `Border` element can provide both an outline and background.)

When you run the app, the data template is applied to your `Note` items and looks like this if your Windows Personalization > Colors settings use the Light mode:

:::image type="content" source="media/all-notes/itemsview-with-template.png" alt-text="The notes app UI with the note list showing the note content and date formatted by a data template.":::

However, if your Windows Personalization > Colors settings use the Dark mode, it will look like this:

:::image type="content" source="media/all-notes/itemsview-with-template-dark.png" alt-text="The notes app UI with a dark background but light gray note template.":::

This is not the intended look for the app. It happened because there are hard-coded color values in the data template for the note. By default, WinUI elements adapt to the user's Dark or Light color preference. When you define you own elements, like a data template, you need to be careful to do the same.

When you define a resource in a XAML `ResourceDictionary`, you have to assign an `x:Key` value to identify the resource. Then, you can use that `x:Key` to retrieve the resource in XAML using the `{StaticResource}` markup extension or `{ThemeResource}` markup extension.

- A `{StaticResource}` is the same regardless of the color theme, so it's used for things like `Font` or `Style` settings.
- A `{ThemeResource}` changes based on the selected color theme, so it's used for `Foreground`, `Background`, and other color-related properties.

WinUI includes a variety of built-in resources that you can use to make your app follow Fluent style guidelines, as well as accessibility guidelines. You'll replace the hard-coded colors in the data template with built-in theme resources, and apply a few other resources to match the Fluent Design guidelines.

1. In the data template you added previously, update the sections indicated here to use built-in resources:

    ```xaml
    <DataTemplate x:Key="NoteItemTemplate" 
                  x:DataType="models:Note">

    <!-- ↓ Update this. ↓ -->
        <ItemContainer CornerRadius="{StaticResource OverlayCornerRadius}">
            <Grid Background="{ThemeResource CardBackgroundFillColorDefaultBrush}"
                  BorderThickness="1" 
                  BorderBrush="{ThemeResource CardStrokeColorDefaultBrush}"
                  CornerRadius="{StaticResource OverlayCornerRadius}">
    <!-- ↑ Update this. ↑ -->

                <Grid.RowDefinitions>
                    <RowDefinition Height="120"/>
                    <RowDefinition Height="Auto"/>
                </Grid.RowDefinitions>
                <TextBlock Text="{x:Bind Text}" Margin="12,8"
                           TextWrapping="Wrap"
                           TextTrimming="WordEllipsis"/>

    <!-- ↓ Update this. ↓ -->
                <Border Grid.Row="1" Padding="8,6,0,6"
                        Background="{ThemeResource SubtleFillColorSecondaryBrush}">
                    <TextBlock Text="{x:Bind Date}"
                        Style="{StaticResource CaptionTextBlockStyle}"
                        Foreground="{ThemeResource TextFillColorSecondaryBrush}"/>
    <!-- ↑ Update this. ↑ -->

                </Border>
            </Grid>
        </ItemContainer>
    </DataTemplate>
    ```

Now when you run the app with a Light color setting, it will look like this:

:::image type="content" source="media/all-notes/itemsview-themed-template.png" alt-text="The notes app UI with a light background and light note template.":::

And when you run the app with a Dark color setting, it will look like this:

:::image type="content" source="media/all-notes/itemsview-themed-template-dark.png" alt-text="The notes app UI with a dark background and dark note template.":::

:::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Item containers and templates](../../design/controls/item-containers-templates.md)
- [ResourceDictionary and XAML resource references](../../develop/platform/xaml/xaml-resource-dictionary.md)

> [!TIP]
> The WinUI 3 Gallery app is a great way to learn about different WinUI controls and design guidelines. To see the theme resources used in the data template, [open the WinUI 3 Gallery app to the Color guidance](winui3gallery://item/Color). From there, you can see what the resources look like and copy the values you need directly from the app.
>
> You can also open the [Typography page](winui3gallery://item/Typography) and [Geometry page](winui3gallery://item/Geometry) to see other built-in resources used in this data template.

[!INCLUDE [winui-3-gallery](../../../../hub/includes/winui-3-gallery.md)]

> [!div class="nextstepaction"]
> [Continue to step 5 - Add navigation between pages](navigation.md)
