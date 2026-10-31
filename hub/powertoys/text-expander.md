---
title: PowerToys Text Expander utility for Windows
description: Learn how to use PowerToys Text Expander to insert text snippets by typing short triggers, manage snippets in the snippet editor, and use your snippets on several PCs.
ms.date: 10/31/2026
ms.topic: concept-article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, Text Expander, OneDrive, PowerToysTextExpanderSnippets.json]
# Customer intent: As a Windows power user, I want to replace short triggers with reusable text snippets so that I can enter frequently used text more quickly.
---

# Text Expander utility

PowerToys Text Expander inserts text that you use often, such as an email signature or a standard reply, when you type a short trigger in any app. A snippet can include several lines, dates and times, clipboard text, your answers to questions, and a cursor position.

## Enable Text Expander

1. Open PowerToys Settings.
1. Select **Text Expander**.
1. Turn on **Enable Text Expander**.

PowerToys must keep running in the background for triggers to expand.

## Manage snippets

On the **Text Expander** page in PowerToys Settings, select **Manage snippets** to open the snippet editor, **PowerToys Text Expander - Manage snippets**. You can manage snippets even while Text Expander is turned off.

Each row shows a trigger, then "inserts" and a preview of the snippet. If the snippet asks questions, the row reads "asks" followed by the questions and "then inserts".

- To find snippets, use **Search snippets** at the top of the window, or press <kbd>Ctrl</kbd>+<kbd>F</kbd>. Search matches triggers and replacement text.
- To edit a snippet, select its row, or move to it with the arrow keys and press <kbd>Enter</kbd>.
- Select **More options** on a row to **Edit**, **Duplicate**, or **Delete** the snippet. **Duplicate** opens an unsaved copy with a new trigger.
- Select **Good to know** (the information icon) for notes about privacy, undo, input methods, and where snippets don't expand.

If a row shows **Never expands**, the row explains why its trigger can't fire.

### Create a snippet

1. In the snippet editor, select **New snippet**.
1. In **Trigger**, type a short trigger, such as `!mysig`.
1. In **Replacement**, type the text to insert. Press <kbd>Enter</kbd> for a new line.
1. Optionally, select **Insert variable** to add a value that's filled in when the snippet runs.
1. Check the **Preview**, then select **Save**.

Start triggers with a symbol such as `!` so that they don't fire while you type ordinary words. Each trigger must be unique and can't start or end with a space.

Text Expander types a backslash as is, so `\n` in a replacement isn't a new line. Because <kbd>Tab</kbd> moves to the next control, add a tab by pasting one, or by writing `\t` in the [snippets database](#edit-the-snippets-database).

### Insert variables

Select **Insert variable** to add one of these values, or type the value. Values are case-sensitive. Dates and times use your Windows regional format, except `{{isodate}}`. The examples use the English (United States) format.

| Value | Insert variable item | Inserts |
| :--- | :--- | :--- |
| `{{date}}` | **Date** | The current date, such as `6/1/2026`. |
| `{{time}}` | **Time** | The current time, such as `2:30 PM`. |
| `{{datetime}}` | **Date and time** | The current date and time, such as `6/1/2026 2:30 PM`. |
| `{{isodate}}` | **ISO date** | The current date as `yyyy-MM-dd`, such as `2026-06-01`. |
| `{{date:FORMAT}}` | **Custom date format** | The current date and time in a custom format. For example, `{{date:dddd, MMMM d}}` inserts `Monday, June 1`. |
| `{{clipboard}}` | **Clipboard contents** | The text on the clipboard. |
| `{{prompt:Question}}` | **Ask a question**, with **Required** on | Your answer to *Question*, asked when the snippet runs. An answer is required. |
| `{{prompt?:Question}}` | **Ask a question**, with **Required** off | Your answer to *Question*, which can be empty. |

**Cursor position** adds `$|$`, which marks where the cursor ends up after the snippet is inserted. Only the first `$|$` in a snippet counts.

A custom format uses [.NET custom date and time format strings](/dotnet/standard/base-types/custom-date-and-time-format-strings) or `strftime` codes, such as `{{date:%Y-%m-%d}}`. **Custom date format** offers ready-made formats, and **Date format codes** under **Replacement** lists common codes while you edit one.

### Ask a question when a snippet runs

1. In the snippet dialog, select **Insert variable** > **Ask a question**.
1. In **Ask when the snippet runs**, type your question, such as `Ticket number`.
1. If the answer can be empty, turn off **Required**.
1. Select **Add**.

To change a question, select it in **Replacement**, or put the cursor in it and press <kbd>F2</kbd>.

When the snippet runs, a **PowerToys Text Expander** window asks the question. Type your answer, then press <kbd>Enter</kbd> or select **OK**. For a required question, **OK** isn't available until you type an answer. To leave the answer blank and continue, press <kbd>Esc</kbd> or select **Cancel**.

## Control when triggers expand

The **Advanced** group on the **Text Expander** page in PowerToys Settings has these matching settings:

| Setting | Behavior |
| :--- | :--- |
| **Wait for space or punctuation** | On by default. A trigger expands after you type a space, punctuation mark, or symbol, which stays after the snippet. For example, typing `!mysig,` inserts the snippet and a comma. <kbd>Enter</kbd> and <kbd>Tab</kbd> don't expand a trigger. Turn off this setting to expand a trigger as soon as you type its last character. |
| **Case-sensitive snippets** | On by default. A trigger must match the capitalization you saved. Turn off this setting to ignore capitalization. |
| **Use PowerToys Keyboard Manager remaps when matching triggers** | On by default. A key that [Keyboard Manager](keyboard-manager.md) remaps counts as the key it produces. For example, if <kbd>A</kbd> is remapped to <kbd>B</kbd>, pressing <kbd>A</kbd> can match a trigger that contains `B`. Turn off this setting to match only the physical keys you press. |

## Choose an insertion method

**Insertion method**, also in the **Advanced** group, controls how a snippet reaches the app you're typing in:

| Method | Behavior |
| :--- | :--- |
| **Automatic** | The default. Types one-line snippets shorter than **Minimum length to paste**, 5 characters by default, and pastes the rest. Snippets with several lines are always pasted. |
| **Always paste** | Pastes every snippet. |
| **Always type** | Types every snippet, about one character every 25 milliseconds. Use this method for apps that block pasting, or to keep snippets off your clipboard. |

When Text Expander pastes a snippet, it restores your clipboard after the app takes the paste, and the snippet isn't added to clipboard history. If the app stops responding for 5 seconds, the snippet stays on the clipboard, and Text Expander tells you that it couldn't restore your clipboard. A snippet that uses `{{clipboard}}` reads your clipboard with any method.

You can keep typing right after a trigger. Text Expander holds your keystrokes and sends them after the snippet. With **Always type**, let a long snippet finish before you type more.

Right after a snippet expands, press <kbd>Ctrl</kbd>+<kbd>Z</kbd> to change it back to the trigger you typed. Undo isn't available for snippets that use `{{clipboard}}` or ask a question.

## Edit the snippets database

Text Expander saves your snippets in a JSON file, which the snippet editor calls the *snippets database*. By default, it's here:

```text
%LOCALAPPDATA%\Microsoft\PowerToys\TextExpander\PowerToysTextExpanderSnippets.json
```

When there's no database in this location, Text Expander creates one with six starter snippets: `!ddate`, `!ttime`, `!ddt`, `!isodate`, `!mysig`, and `!myemail`. It never adds starter snippets to an existing database, even an empty one, or to a database in another location.

In the snippet editor, select **Saved on this PC** to see the database's location and snippet count. When the database is somewhere else, the button says **Saved in OneDrive**, or **Saved in** and a folder name. Select **Open in text editor** to open the database in your default app for JSON files, or in Notepad. Text Expander picks up the changes you save automatically. To read the database again, select **Refresh**.

The database looks like this:

```json
{
  "version": 1,
  "snippets": [
    { "trigger": "!mysig", "replacement": "Best regards,\nYour name" },
    { "trigger": "!hello", "replacement": "Hello, and thanks for reaching out." }
  ]
}
```

- Write a line break as `\n` and a tab as `\t`. Write a quotation mark as `\"` and a backslash as `\\`.
- Comments and trailing commas are allowed. The snippet editor doesn't keep comments or your formatting, so it backs up such a file before it first saves over it. Backups are saved next to the database, with `.backup-` and the date and time added to the file name.
- If several snippets use the same trigger, the last one wins.

If the snippet editor can't load the database, it shows an error above the list:

- If the error says "Your snippets database has a mistake on line *N*," fix that line in a text editor, then select **Retry**. Until then, Text Expander keeps using the snippets it last read.
- If the error says "Can't find your snippets database," the database was moved, renamed, or deleted. Put it back and select **Retry**, or select **Sync from another PC** to choose it again.
- If the error says "Can't reach your snippets in OneDrive" or in another folder, check that OneDrive is running and that you're signed in, or that the drive or network folder is available. Then select **Retry**.

If a newer version of PowerToys saved the database, this version can use its snippets but can't change them. Update PowerToys on this PC, then try again.

## Use your snippets on several PCs

Text Expander doesn't sync snippets itself, but OneDrive or another sync service can sync your snippets database:

1. On the PC that has your snippets, open the snippet editor and select **Saved on this PC** > **Move snippets database**. Save the database in OneDrive or another synced folder.
1. On each of your other PCs, open the snippet editor, select **Saved on this PC** > **Sync from another PC**, and pick that database.

**Move snippets database** saves your snippets to the file you choose and uses that file from then on, leaving the previous database where it was. If the file already has snippets, Text Expander says "This database already has snippets" and asks what to do:

- **Use its snippets** switches to that database without changing it.
- **Replace them** backs up that database, then replaces its snippets with this PC's snippets.
- **Cancel** changes nothing.

**Sync from another PC** switches to an existing snippets database without changing it. Text Expander doesn't merge databases.

> [!WARNING]
> Avoid editing your snippets on several PCs at the same time. A sync service can create conflicting copies or apply one PC's changes over another's. Let it finish syncing before you edit on another PC. If the database changes after the snippet editor loads it, the editor reloads the database instead of saving your change, so you can try again.

## Privacy and limitations

> [!IMPORTANT]
> Don't save passwords, Social Security numbers, credit card numbers, or other sensitive info in snippets. The snippets database is plain text and isn't encrypted. PowerToys also shows this warning in the snippet editor and in **Welcome to PowerToys**.

- Snippets don't expand in apps running as administrator or in standard Windows password boxes. They can still expand in password fields on websites and in some apps.
- If you type with an input method editor (IME), for example for Japanese, Chinese, or Korean, switch it to direct input or English before you type a trigger.
- You can't manage snippets while PowerToys [runs as administrator](administrator.md), but snippets still expand in apps that don't. To manage snippets, restart PowerToys without administrator rights. If **Always run as administrator** is on in **General**, turn it off first.
- Some apps, such as some remote desktop and terminal apps, block pasting. If a snippet doesn't appear in an app, set **Insertion method** to **Always type**.
- Administrators can turn Text Expander on or off with the `ConfigureEnabledUtilityTextExpander` [Group Policy](grouppolicy.md#configure-enabled-state-for-individual-utilities) setting. When the policy turns Text Expander off, the snippet editor doesn't open either.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
