---
title: Manage app state effectively
description: Learn how to manage application state in your Windows App SDK desktop application for a reliable and responsive user experience.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 05/22/2026
---

# Manage app state effectively

In a desktop Windows App SDK application, your process continues to run until the user explicitly closes it. Desktop apps don't go through the automatic suspend, resume, and terminate lifecycle that UWP apps use. This gives you more control, but also more responsibility for managing state.

## Prerequisites

- A Windows App SDK desktop project. For setup steps, see [Create your first WinUI 3 app](../../get-started/start-here.md).
- Familiarity with the `Microsoft.UI.Windowing.AppWindow` class and, for packaged apps, `Windows.Storage.ApplicationData`.

> [!IMPORTANT]
> Desktop Windows App SDK apps do **not** automatically suspend and resume like UWP apps. Your app runs continuously until the user or the system shuts it down. You should still implement good state management practices to handle unexpected shutdowns, system restarts, and power loss.

## Why state management matters

Even though desktop apps are not suspended by the OS, there are still situations where your app can lose unsaved state:

- The user closes the app while work is in progress
- The system restarts for an update
- Power loss or a crash terminates the process
- The user logs out of Windows

Design your app to handle these cases gracefully by saving important state frequently and restoring it when the app starts.

## Save state incrementally

Rather than saving all state at shutdown, save state incrementally as the user works. This reduces the risk of data loss and distributes the I/O cost across your app's lifetime.

```csharp
private async void OnThemeChanged(object sender, SelectionChangedEventArgs e)
{
    var selectedTheme = (sender as ComboBox)?.SelectedItem?.ToString();
    if (selectedTheme != null)
    {
        await SaveSettingAsync("AppTheme", selectedTheme);
    }
}

private async Task SaveSettingAsync(string key, string value)
{
    // Use local file storage, a database, or ApplicationData for packaged apps.
    var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    var settingsDir = System.IO.Path.Combine(localFolder, "MyApp");
    Directory.CreateDirectory(settingsDir);
    var settingsPath = System.IO.Path.Combine(settingsDir, "settings.json");

    // Read existing settings, update, and save asynchronously.
    var settings = LoadSettings(settingsPath);
    settings[key] = value;
    await File.WriteAllTextAsync(settingsPath, System.Text.Json.JsonSerializer.Serialize(settings));
}

private Dictionary<string, string> LoadSettings(string path)
{
    // Read and deserialize the settings file, or return an empty set if it doesn't exist yet.
    if (!File.Exists(path))
    {
        return new Dictionary<string, string>();
    }

    var json = File.ReadAllText(path);
    return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json)
        ?? new Dictionary<string, string>();
}
```

## Use ApplicationData for packaged apps

If your app is packaged with MSIX, you can use `Windows.Storage.ApplicationData.Current.LocalSettings` to store small settings and `LocalFolder` for larger data. These locations are managed by the system and cleaned up when the app is uninstalled.

> [!NOTE]
> `ApplicationData.Current` requires package identity. If your app is unpackaged, use `Environment.SpecialFolder.LocalApplicationData` or another standard file location instead.

```csharp
// For packaged apps with package identity:
string filePath = "C:\\Users\\Example\\Documents\\report.docx";
var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
localSettings.Values["LastOpenedFile"] = filePath;
```

## Restore state on startup

When your app starts, check for previously saved state and restore it. This gives the user a seamless experience — they pick up where they left off.

```csharp
private void MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    // Restore the last opened file, if any.
    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
    if (localSettings.Values.TryGetValue("LastOpenedFile", out var path))
    {
        OpenFile(path.ToString());
    }
}

private void OpenFile(string path)
{
    // Load the document at the given path and update the UI.
}
```

## Handle the AppWindow.Closing event

Use the `Closing` event on `Microsoft.UI.Windowing.AppWindow` to save critical state before the app terminates. This is your last opportunity to persist state when the user closes the window.

> [!IMPORTANT]
> In WinUI 3, the `Window.Closed` event does not support cancellation. To prompt the user before closing (for example, to save unsaved changes), use the `Microsoft.UI.Windowing.AppWindow.Closing` event instead, which provides an `AppWindowClosingEventArgs` object with a `Cancel` property.
>


```csharp
// In your Window constructor or initialization code:
var appWindow = this.AppWindow;
appWindow.Closing += AppWindow_Closing;

void AppWindow_Closing(AppWindow sender, AppWindowClosingEventArgs args)
{
    // See the following examples for what to do here.
}
```

```csharp
private void AppWindow_Closing(AppWindow sender, AppWindowClosingEventArgs args)
{
    SaveCurrentDocument();
    SaveWindowPosition();
}

private void SaveCurrentDocument()
{
    // Persist the current document to disk.
}

private void SaveWindowPosition()
{
    // Persist the window's current size and position.
}
```

To prompt the user before closing, set `args.Cancel = true` to prevent the window from closing, then close it programmatically after the user confirms. Use a guard flag to prevent re-entrancy, because calling `this.Close()` re-triggers the `Closing` event:

```csharp
private bool _isClosing = false;
private bool HasUnsavedChanges { get; set; }

private async void AppWindow_Closing(AppWindow sender, AppWindowClosingEventArgs args)
{
    if (_isClosing) return;

    if (HasUnsavedChanges)
    {
        args.Cancel = true; // Prevent the window from closing.

        // ShowSaveDialog returns true if the user chose to save.
        var shouldSave = await ShowSaveDialog();
        if (shouldSave)
        {
            await SaveCurrentDocumentAsync();
        }

        // Set the guard flag before closing to prevent re-entrancy.
        _isClosing = true;
        this.Close();
    }
}

private Task<bool> ShowSaveDialog()
{
    // Prompt the user to save, discard, or cancel.
    return Task.FromResult(true);
}

private Task SaveCurrentDocumentAsync()
{
    // Persist the current document to disk asynchronously.
    return Task.CompletedTask;
}
```

## Best practices

| Practice | Guidance |
|---|---|
| Save frequently | Don't wait for shutdown — save state as the user works |
| Use async I/O | Use `FileStream` with `useAsync: true` or `File.WriteAllTextAsync` to avoid blocking the UI |
| Minimize state size | Save only the data you need to restore the user's position |
| Handle power events | Listen for `PowerManager.EnergySaverStatusChanged` to adapt behavior |
| Test unexpected shutdowns | Use Task Manager to terminate your app and verify state recovery |

## Related content

- [Keep the UI thread responsive](keep-ui-thread-responsive.md)
- [Optimize background activity](optimize-background-activity.md)
- [Plan and measure performance](planning-measuring-performance.md)
