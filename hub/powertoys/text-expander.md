---
title: PowerToys Text Expander utility for Windows
description: Learn how to create and manage PowerToys Text Expander snippets, use dynamic values and prompts, edit the snippet file, and store snippets in a synced folder.
ms.date: 10/22/2026
ms.topic: concept-article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, Text Expander, OneDrive, PowerToysTextExpanderSnippets.txt, snippets.txt]
# Customer intent: As a Windows power user, I want to replace short triggers with reusable text snippets so that I can enter frequently used text more quickly.
---

# Text Expander utility

PowerToys Text Expander replaces short triggers that you type with reusable text snippets. A replacement can include multiple lines, dates and times, clipboard text, questions that Text Expander asks when the snippet runs, and a final cursor position.

## Enable Text Expander

To use Text Expander:

1. Open PowerToys Settings.
1. Select **Text Expander**.
1. Turn on **Enable Text Expander**.

PowerToys must remain running in the background for triggers to expand.

## Create and manage snippets

To create a snippet:

1. In PowerToys Settings, select **Text Expander**.
1. Select **New snippet**.
1. Enter a **Trigger**, such as `!mysig`.
1. Enter the **Replacement** text.
1. Optionally, select **Insert variable** to add a dynamic value.
1. Select **Save**.

Start triggers with a symbol such as `!`, `#`, or `:` to reduce accidental matches in ordinary words.

The snippets list provides these management options:

- Select a snippet row or **Edit** to change its trigger or replacement.
- Use the search box to search both triggers and replacement text.
- Select **Duplicate** to create a copy with a new trigger.
- Select **Delete** to permanently remove a snippet.

### Configure a Prompt window

Select **Insert variable** > **Prompt window** in the replacement editor to immediately open the Prompt window editor. Enter the question that Text Expander should ask, and choose whether an answer is required.

When the snippet expands, Text Expander opens a Prompt window over the app where you were typing. Your answer is inserted into the replacement. When **Require an answer** is selected, you must enter text before you can accept the prompt. Pressing <kbd>Esc</kbd> cancels the prompt, leaves that value empty, and continues the snippet. Clear **Require an answer** to allow an empty response.

You can also select an existing Prompt window token in the replacement editor to change its question or required state.

## Control when triggers expand

Text Expander uses the following matching settings:

| Setting | Behavior |
| :--- | :--- |
| **Wait for space or punctuation** | Waits until you type a space, punctuation mark, or symbol after a trigger. This setting is on by default for a clean setup, and new snippets use this behavior. It prevents a shorter trigger from expanding while you are still typing a longer word or trigger. You can turn it off to expand immediately after the last trigger character. An existing saved choice remains unchanged. |
| **Case-sensitive snippets** | Requires capitalization to match the saved trigger. This setting is on by default. Turn it off to ignore capitalization. |
| **Use PowerToys Keyboard Manager remaps when matching triggers** | Treats the output of a Keyboard Manager remap as typed text. For example, if Keyboard Manager remaps <kbd>A</kbd> to <kbd>B</kbd>, pressing <kbd>A</kbd> can match a trigger that contains `B`. This setting is on by default. Turn it off to match only the physical keys you press. |

When **Wait for space or punctuation** is on, Text Expander keeps the character that completed the trigger. For example, typing `!mysig,` inserts the replacement followed by the comma.

## Use dynamic values

Add the following values to replacement text:

| Value | Result |
| :--- | :--- |
| `{{date}}` | The current date, such as `06/01/26`. |
| `{{time}}` | The current time, such as `02:30 PM`. |
| `{{datetime}}` | The current date and time, such as `06/01/26 02:30 PM`. |
| `{{isodate}}` | The current date in `yyyy-MM-dd` format, such as `2026-06-01`. |
| `{{date:FORMAT}}` | The current date and time using a custom format, such as `{{date:dddd, MMMM d}}`. The editor provides common [.NET custom date and time format strings](/dotnet/standard/base-types/custom-date-and-time-format-strings). Existing `strftime`-style formats that contain `%` are also supported. |
| `{{clipboard}}` | The text currently on the clipboard. |
| `{{prompt:Question}}` | Opens a Prompt window and requires a nonempty answer to accept the prompt. Canceling the prompt leaves the value empty. |
| `{{prompt?:Question}}` | Opens a Prompt window but allows an empty answer. |
| `$|$` | Places the cursor at this position after insertion. The first cursor marker controls the final position. |
| `\n` | Inserts a new line. |
| `\t` | Inserts a tab. |

Values are case-sensitive and must use the spelling shown in the table.

## Choose an insertion method

The **Insertion method** setting controls how replacement text reaches the focused app:

| Method | Behavior |
| :--- | :--- |
| **Automatic** | Types short, one-line replacements and pastes longer replacements. The default **Minimum length to paste** is 5 characters. Replacements with multiple lines are pasted. |
| **Always paste** | Uses a paste operation for every replacement. |
| **Always type** | Types every replacement. Use this option if an app blocks synthetic paste operations, or if you don't want Text Expander to use the clipboard for insertion. |

When Text Expander pastes a replacement, it temporarily uses the clipboard and then attempts to restore the previous clipboard contents. A snippet that contains `{{clipboard}}` still reads clipboard text even when **Always type** is selected.

Immediately after most snippets expand, you can press <kbd>Ctrl</kbd>+<kbd>Z</kbd> to restore the trigger. This undo behavior isn't available for snippets that use `{{clipboard}}` or a Prompt window.

## Edit the snippets file

The active snippet file is named `PowerToysTextExpanderSnippets.txt`. Its default location is:

```text
%LOCALAPPDATA%\Microsoft\PowerToys\TextExpander\PowerToysTextExpanderSnippets.txt
```

Text Expander also recognizes the legacy file name `snippets.txt`. When the legacy file is the active database, Text Expander continues to use it rather than renaming it.

In **Save snippets to a folder**, use:

- **Open snippets file** to open the active file in your default text editor.
- **Reload file** to read the active file again after another device, app, or text editor changes it.

Text Expander watches the active file and normally reloads changes after you save it. **Reload file** provides a manual refresh if an external change doesn't appear.

### Single-line syntax

Write one snippet per line with an equals sign between the trigger and replacement:

```text
!hello=Hello, thanks for reaching out.
!today=Today is {{date:dddd, MMMM d}}.
```

Lines that start with `//` are comments and aren't loaded as snippets. Blank lines and comments are preserved when you edit other snippets in PowerToys Settings.

### Multiline syntax

For a replacement with several lines, add `<<<` directly after the trigger. End the replacement with `>>>` on its own line:

```text
!meeting<<<
Meeting notes - {{date:dddd, MMMM d, yyyy}}
Attendees: $|$
Next steps:
>>>
```

A multiline replacement can't contain another line that consists only of `>>>`, because that line ends the block.

## Store snippets in a synced folder

Text Expander doesn't provide a synchronization service. To use the same snippet database on multiple devices, choose a folder managed by OneDrive or another sync provider:

1. Open PowerToys Settings and select **Text Expander**.
1. Expand **Save snippets to a folder**.
1. Select **Choose folder...** and choose the provider-managed folder.

If the folder already contains `PowerToysTextExpanderSnippets.txt` or `snippets.txt`, choose one of these actions:

- **Use folder file** adopts the database already in the selected folder. Text Expander creates a backup of the previously active file.
- **Replace folder file** replaces the database in the selected folder with the snippets currently open on this PC. Text Expander creates a backup of the file being replaced.

Text Expander doesn't merge the two databases automatically. Review the backup if you need snippets from both files.

> [!WARNING]
> Avoid editing the same snippet database on multiple devices at the same time. A sync provider can create conflicts or apply one device's changes over another device's changes. Let the provider finish syncing and resolve any provider-reported conflict before you continue editing.

Starter snippets and commented guidance are added only when neither supported file exists in a new database folder. Existing `PowerToysTextExpanderSnippets.txt`, legacy `snippets.txt`, synced, or intentionally empty files are preserved and aren't populated or merged automatically.

## Accessibility, privacy, and limitations

- The runtime Prompt window follows the Windows theme and high-contrast setting, resizes for larger text, and supports <kbd>Enter</kbd> to accept and <kbd>Esc</kbd> to cancel.
- Text Expander requires PowerToys to run without administrator elevation. You also can't edit snippets while PowerToys Settings runs as administrator.
- Snippets don't expand in elevated apps or standard Windows password boxes. They can still expand in password fields on websites and in some apps, so don't store or trigger sensitive text where it could be exposed.
- Some remote desktop, terminal, or security-hardened apps can block synthetic paste operations. Change **Insertion method** to **Always type** if automatic insertion doesn't work in an app.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
