---
title: User activities best practices
description: Follow these best practices for creating user activities in your Windows App SDK desktop app for an optimal task-resumption experience.
author: GrantMeStrength
ms.author: jken
ms.topic: best-practice
ms.date: 07/08/2026
---

# User activities best practices

User activities help users resume tasks they started in your app. Follow these guidelines to create activities that are useful, clear, and well-structured.

## General guidelines

### Create activities for meaningful tasks

Create activities for tasks that the user would want to return to later. Good candidates include:

- **Documents** — A specific document, spreadsheet, or file the user is editing.
- **Projects** — A project workspace, design, or codebase.
- **Media** — A song, video, or podcast that the user was playing.
- **Game progress** — A game session, level, or checkpoint.

Do not create activities for trivial actions like viewing settings, scrolling through a list, or navigating between pages.

### Use descriptive display text

- Set `DisplayText` to a concise, recognizable name (for example, "Quarterly Report" or "Chapter 5: The Journey").
- Set `Description` to indicate context or progress (for example, "Editing Section 3 — Revenue Analysis").
- Avoid generic text like "Untitled" or "Working on something."

### Update activities as the user progresses

Call `SaveAsync()` periodically to update the description with the user's current position:

```csharp
UserActivity activity = new UserActivity("quarterly-report");
int currentPage = 3;
int totalPages = 10;

activity.VisualElements.Description = $"Page {currentPage} of {totalPages}";
await activity.SaveAsync();
```

## Activity patterns by app type

### Document-based apps

- Use the document file path or unique identifier as the activity ID.
- Set `ActivationUri` to open the specific document.
- Update `Description` with the current section or edit location.

### Games

- Use the save slot or session identifier as the activity ID.
- Set `DisplayText` to the current level or mission name.
- Include progress in the description (for example, "Level 12 — 85% complete").

### Media apps

- Use the media item identifier as the activity ID.
- Set `DisplayText` to the track or episode name.
- Include playback position in the description (for example, "34:15 / 1:02:00").

### Line-of-business apps

- Use the business object identifier (order number, customer ID, case number) as the activity ID.
- Set `DisplayText` to the object name or number.
- Update frequently as the user progresses through a workflow.

## Rich visual guidelines

When setting the activity's visual details:

- Keep `DisplayText` short — one line that identifies the task.
- Use `Description` for a single line of context or progress, not a paragraph.
- Set an `Attribution` icon so the activity is recognizable in activity history.
- Always set `DisplayText`, even if you also set other visual properties, so the activity has a readable fallback.

```csharp
UserActivity activity = new UserActivity("quarterly-report");

activity.VisualElements.DisplayText = "Quarterly Report"; // Fallback
activity.VisualElements.Description = "Page 3 of 10";
```

> [!NOTE]
> Earlier versions of this guidance recommended attaching a full Adaptive Card (`AdaptiveCardBuilder`, in the `Windows.UI.Shell` namespace) as the activity's visual. That API was part of Windows Timeline, which Microsoft retired. Don't use `AdaptiveCardBuilder` in new code — use the `VisualElements` properties shown above instead.

## Activation URI guidelines

- Use a custom protocol scheme registered to your app (for example, `myapp://`).
- Include enough information in the URI to navigate directly to the task.
- Keep URIs stable — do not include session-specific tokens that expire.

Example:

```text
myapp://document/quarterly-report-2026?page=12
```

## Session management

- Create a `UserActivitySession` when the user starts working on a task.
- Dispose the session when the user switches to a different task.
- Only maintain one active session at a time per activity channel.

## Related content

- [Create user activities](user-activities.md)
- [UserActivity class](/uwp/api/windows.applicationmodel.useractivities.useractivity)
