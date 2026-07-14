---
title: WinUI Notes part 2 - navigation cache
description: WinUI Notes part 2 navigation cache.
ms.date: 06/16/2026
ms.topic: tutorial
ms.localizationpriority: medium
---
# Navigation cache and change notification

The first step is to do the basic setup for the changes you need to make:

- enable the navigation cache.
- implement property change notifications for the `Note.Text` property.

One that's done, you'll adapt other parts of the app to work with these changes.

> [!TIP]
> You can download or view the completed code for this tutorial from the [GitHub repo at WinUI Notes part 2](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes-part-2). To see the differences between the start and end points for the project, see this commit: [updates for part 2](https://github.com/MicrosoftDocs/windows-topic-specific-samples/commit/bb4d94785247247fbfbce80173bb3a9097e843d6).

## Enable NavigationCacheMode

By default, a new [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page) instance is created with its default values every time navigation occurs. In the WinUI Notes app, this is also where the `notesModel`, which stores all the `Note` instances, is created.

In `AllNotesPage.xaml`, set [NavigationCacheMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page.navigationcachemode) to [Enabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.navigation.navigationcachemode) (`NavigationCacheMode="Enabled"`). With `NavigationCacheMode` enabled, the same page instance is kept around, so a new `Page` instance isn't created on each navigation, and `notesModel` is not re-created.

```xaml
<Page
    x:Class="WinUI_Notes.Views.AllNotesPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:WinUI_Notes.Views"
    xmlns:models="using:WinUI_Notes.Models"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}"
    //  ↓ Add this. ↓
    NavigationCacheMode="Enabled">
```

Run the app now and you'll notice some side effects of this change.

1. When you edit an existing note, your changes aren't reflected in the all-notes page when you navigate back.
1. When you create and save a new note, it doesn't appear in the all-notes list when you navigate back.
1. When you delete an existing note, it's not removed from the all-notes list when you navigate back.

You'll fix these issues next.

:::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Implement navigation between two pages](../../develop/ui/navigation/navigate-between-two-pages.md)
- [Page.NavigationCacheMode class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page.navigationcachemode), [NavigationCacheMode enum](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.navigation.navigationcachemode)

## Implement INotifyPropertyChanged

When you edit and save an existing note, your change is saved to the file system, but the change isn't propagated to the all-notes list. This is because the `Note` class doesn't notify the data binding, which connects the `TextBox` to the `Note` text, that an update has occurred. To make this notification happen, the `Note` class needs to implement the `INotifyPropertyChanged` interface for its `Text` property.

> [!NOTE]
> WinUI includes the [Microsoft.UI.Xaml.Data.INotifyPropertyChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.inotifypropertychanged) interface. This is used only by C++ apps, which don't use .NET.
>
> C# apps created with .NET use the [System.ComponentModel.INotifyPropertyChanged](/dotnet/api/system.componentmodel.inotifypropertychanged) interface instead.

The implementation of `INotifyPropertyChanged` follows a set pattern.

1. Add `using` statements for the required namespaces.

    ```csharp
    // ↓ Add this. ↓
    using System.ComponentModel
    using System.Runtime.CompilerServices
    ```

1. Implement `INotifyPropertyChanged`. The `Note` class now implements this interface.

    ```csharp
    // ↓ Update this. ↓
    public class Note : INotifyPropertyChanged
    ```

1. Create a backing field (`_text`) for the `Text` property.

    ```csharp
    // ↓ Delete this. ↓
    // public string Text { get; set; } = string.Empty;

    // ↓ Add this. ↓
    private string _text = string.Empty;
    ```

1. Modify the `Text` property to use a getter/setter pattern with property change notification.

    ```csharp
    // ↓ Add this. ↓
    public string Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged();
            }
        }
    }
    ```

1. Add the [PropertyChanged](/dotnet/api/system.componentmodel.inotifypropertychanged.propertychanged) event required by the `INotifyPropertyChanged` interface.

    ```csharp
    // ↓ Add this. ↓
    public event PropertyChangedEventHandler? PropertyChanged;
    ```

1. Add the `OnPropertyChanged` method. This helper method raises the `PropertyChanged` event using the [CallerMemberName](/dotnet/api/system.runtime.compilerservices.callermembernameattribute) attribute for automatic property name detection.

    ```csharp
    // ↓ Add this. ↓
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    ```

> [!TIP]
> You can use [GitHub Copilot](/windows/apps/how-tos/ai-setup?tabs=visualstudio) to quickly implement `INotifyPropertyChanged` in your app. These code changes were generated with the prompt: "Implement INotifyPropertyChanged for the Note.Text property".

### Binding mode

Now the `Text` property will notify any UI elements bound to it whenever its value changes, so your UI can be updated automatically. However, in order for the bound UI elements to react to the update notification, you have to ensure the correct [BindingMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.bindingmode) is used.

> [!IMPORTANT]
> It's important to choose the correct [BindingMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.bindingmode); otherwise, your data binding might not work as expected. (A common mistake with `{x:Bind}` is to forget to change the default `BindingMode` when `OneWay` or `TwoWay` is needed.)

| Name | Description |
| -- | -- |
| `OneTime` | Updates the target property only when the binding is created. Default for `{x:Bind}`. |
| `OneWay` | Updates the target property when the binding is created. Changes to the source object can also propagate to the target. |
| `TwoWay` | Updates either the target or the source object when either changes. When the binding is created, the target property is updated from the source. |

In `AllNotesPage.xaml`, find the `NoteItemTemplate` in `Page.Resources`. Then, in the template, find the `TextBlock` that's bound to the `Text` property. Update the binding to use the `OneWay` binding mode.

```csharp
// ↓ Update this. ↓              ↓    ↓
<TextBlock Text="{x:Bind Text, Mode=OneWay}"
           Margin="4" TextWrapping="Wrap"
           TextTrimming="WordEllipsis"/>
```

Since the user can't update the text in the `TextBlock`, only a `OneWay` binding is needed, from the source (`Note.Text`) to the target (`TextBlock.Text`).

:::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Create your first WinUI 3 app, Step 3](../winui-notes/view-model.md)
- [Windows data binding in depth](../../develop/data-binding/data-binding-in-depth.md)
- [INotifyPropertyChanged.PropertyChanged event](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.inotifypropertychanged.propertychanged)
- [CallerMemberName attribute](/dotnet/api/system.runtime.compilerservices.callermembernameattribute)

> [!div class="nextstepaction"]
> [Continue to step 2 - Track note state](2-note-state.md)
