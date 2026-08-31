---
title: "AI-assisted WinUI tutorial - Add data and persistence"
description: Use an AI coding assistant to add observable task data, commands, error reporting, and local JSON persistence to a WinUI 3 app.
author: GrantMeStrength
ms.author: jken
ms.date: 08/31/2026
ms.topic: tutorial
---

# Add data and persistence

In this step, ask the assistant for the nonvisual parts of the app. Keeping this change separate from the XAML makes compiler errors and design problems easier to isolate.

## Ask for a model and storage service

Use this prompt:

```text
Add the data layer for TaskTally.

- Create a TaskItem model with an ID, title, and completion state.
- Use MVVM Toolkit observable properties.
- Create a TaskStorage service that serializes the task list to JSON.
- Store tasks in ApplicationData.Current.LocalFolder because this is
  a packaged WinUI 3 app.
- Return an empty list when the file doesn't exist.
- Serialize save operations so rapid changes don't write the file concurrently.
- Surface real read and write errors to the caller.
- Build the project after the change.
```

Compare the generated model with this implementation:

```csharp
using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskTally.Models;

public partial class TaskItem : ObservableObject
{
    public Guid Id { get; init; } = Guid.NewGuid();

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool IsComplete { get; set; }
}
```

The `[ObservableProperty]` attribute generates public properties and change notifications. The class must be `partial` so the MVVM Toolkit source generator can add that code.

The storage service should look like this:

```csharp
using System.Text.Json;
using System.Threading;
using TaskTally.Models;
using Windows.Storage;

namespace TaskTally.Services;

internal static class TaskStorage
{
    private const string FileName = "tasks.json";
    private static readonly SemaphoreSlim SaveLock = new(1, 1);

    public static async Task<IReadOnlyList<TaskItem>> LoadAsync()
    {
        StorageFile? file =
            await ApplicationData.Current.LocalFolder.TryGetItemAsync(FileName) as StorageFile;

        if (file is null)
        {
            return [];
        }

        string json = await FileIO.ReadTextAsync(file);
        return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? [];
    }

    public static async Task SaveAsync(IEnumerable<TaskItem> tasks)
    {
        await SaveLock.WaitAsync();
        try
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                FileName,
                CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, json);
        }
        finally
        {
            SaveLock.Release();
        }
    }
}
```

`ApplicationData.Current.LocalFolder` is appropriate here because the app has package identity. `SaveLock` prevents add, delete, and completion operations from writing the same file concurrently. If you later change the distribution model, verify the package identity assumption again instead of copying this storage choice automatically.

Learn more: [Store and retrieve settings and other app data](../../develop/data/store-and-retrieve-app-data.md) and [ApplicationData.LocalFolder](/uwp/api/windows.storage.applicationdata.localfolder).

## Ask for the view model

Next, use this prompt:

```text
Add MainPageViewModel.

- Expose an ObservableCollection<TaskItem>.
- Add observable properties for the new-task text and status message.
- Add asynchronous commands to add and delete tasks.
- Disable AddTaskCommand when the title is blank.
- Load tasks once when the page starts.
- Save after add, delete, or completion changes.
- Expose a summary and empty-state property for x:Bind.
- Catch storage exceptions only where the UI can report them.
- Build and fix all warnings.
```

The main patterns to inspect in the response are:

```csharp
public ObservableCollection<TaskItem> Tasks { get; } = [];

[ObservableProperty]
public partial string NewTaskTitle { get; set; } = string.Empty;

public string Summary =>
    Tasks.Count == 0
        ? "No tasks yet"
        : $"{Tasks.Count(task => !task.IsComplete)} of {Tasks.Count} remaining";

[RelayCommand(CanExecute = nameof(CanAddTask))]
private async Task AddTaskAsync()
{
    string title = NewTaskTitle.Trim();
    Tasks.Add(new TaskItem { Title = title });
    NewTaskTitle = string.Empty;
    RefreshTaskState();
    await SaveAsync($"Added \"{title}\".");
}

[RelayCommand]
private async Task DeleteTaskAsync(TaskItem task)
{
    Tasks.Remove(task);
    RefreshTaskState();
    await SaveAsync($"Deleted \"{task.Title}\".");
}

private bool CanAddTask() => !string.IsNullOrWhiteSpace(NewTaskTitle);

partial void OnNewTaskTitleChanged(string value)
{
    AddTaskCommand.NotifyCanExecuteChanged();
}
```

Check that the assistant doesn't block asynchronous work with `.Wait()` or `.Result`. Storage operations should be awaited.

The view model can catch storage exceptions when it converts them into a message for the page:

```csharp
private async Task SaveAsync(string successMessage)
{
    try
    {
        await TaskStorage.SaveAsync(Tasks);
        ShowStatus(successMessage);
    }
    catch (Exception ex)
    {
        ShowStatus($"Changes couldn't be saved: {ex.Message}");
    }
}
```

This isn't a silent fallback: the user receives an error, and the view model doesn't claim that the save succeeded.

## Build and inspect

Build for x64. Treat warnings as work items, especially binding and nullable-reference warnings:

```powershell
dotnet build -p:Platform=x64
```

Ask the assistant to explain every package it added. For this app, no new package should be needed beyond the Windows App SDK and MVVM Toolkit references in the template.

> [!div class="nextstepaction"]
> [Build the accessible and adaptive interface](interface.md)
