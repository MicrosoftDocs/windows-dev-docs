---
title: Selection
description: Learn how to select text in Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 07/06/2022
ms.topic: how-to
---

# Selecting text in Windows Terminal

Selecting text is straightforward in Windows Terminal, but there are a lot of additional features in this space that make it even better.

## Mouse support

Left-click and drag your mouse to create a selection. Double-click expands the selection by word, whereas triple-click expands by line.

If you are holding the <kbd>Alt</kbd> key, you will create a block selection (as opposed to a line selection). Block selections create a rectangular region that do not wrap to the end of the line.

If you are holding the <kbd>Shift</kbd> key, you can explicitly expand the selection to a specific point on the terminal without the need to click and drag.

Once you have a selection present, you have a few options. A single left-click will clear your selection. If you actually want to use it, you can right-click to copy the selected text to your clipboard and clear the selection. If you right-click again, the contents of your clipboard will then be pasted into the terminal.

> [!NOTE]
> Windows Terminal supports mouse input in Windows Subsystem for Linux (WSL) applications as well as Windows applications that use virtual terminal (VT) input. This means applications such as [tmux](https://github.com/tmux/tmux/wiki) and [Midnight Commander](https://www.linuxhelp.com/how-to-install-midnight-commander-in-linux) will recognize when you select items in the terminal window. If an application is in mouse mode, you can hold down <kbd>Shift</kbd> to make a selection instead of sending VT input.

## Keyboard support

You can create a selection by using the `selectAll` or `markMode` actions. The `selectAll` action selects all the text in the buffer. The `markMode` action toggles a special mode where a selection is created at the cursor's position in the terminal. When in mark mode, you can use the following non-configurable key bindings to move the cursor around:

| Key binding | Result |
| ----------- | ------ |
| Arrow keys | Move by character in the specified direction |
| <kbd>Ctrl</kbd> + <kbd>Left</kbd> | Move to the beginning of the previous or existing word |
| <kbd>Ctrl</kbd> + <kbd>Right</kbd> | Move to the end of the next or existing word |
| <kbd>Home</kbd> | Move to the beginning of the line |
| <kbd>End</kbd> | Move to the end of the line |
| <kbd>Pgup</kbd> | Move up by a page (viewport) |
| <kbd>Pgdn</kbd> | Move down by a page (viewport) |
| <kbd>Ctrl</kbd> + <kbd>Home</kbd> | Move to the beginning of the buffer |
| <kbd>Ctrl</kbd> + <kbd>End</kbd> | Move to the end of the buffer |

Regardless of being in mark mode, you can expand an existing selection using the following non-configurable key bindings:

| Key binding | Result |
| ----------- | ------ |
| <kbd>Shift</kbd> + Arrow keys | Expand by character in the specified direction |
| <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>Left</kbd> | Expand to the beginning of the previous or existing word |
| <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>Right</kbd> | Expand to the end of the next or existing word |
| <kbd>Shift</kbd> + <kbd>Home</kbd> | Expand to the beginning of the line |
| <kbd>Shift</kbd> + <kbd>End</kbd> | Expand to the end of the line |
| <kbd>Shift</kbd> + <kbd>Pgup</kbd> | Expand up by a page (viewport) |
| <kbd>Shift</kbd> + <kbd>Pgdn</kbd> | Expand down by a page (viewport) |
| <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>Home</kbd> | Expand to the beginning of the buffer |
| <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>End</kbd> | Expand to the end of the buffer |

Use the `toggleBlockSelection` action to transform the existing selection into a block selection.

Any selection created or modified by the keyboard also displays selection markers to indicate which end of the selection is actively being moved. You can use the `switchSelectionEndpoint` action to begin moving the other end of the selection.

Once you have a selection present, you have a few options. You can use the <kbd>ESC</kbd> key to clear the selection. Alternatively, most key input clears the selection and passes the key event directly to the underlying shell. If you actually want to use the selected text, you can use the `copy` action to copy it to your clipboard.

## Copying selected text

As mentioned above, selected text can be copied with a right-click or the `copy` action. However, there are a number of settings regarding copying text that you can customize:
- Copying formatted text
    - You can use the `copyFormatting` global setting to also copy the formatting of the selected text itself to the clipboard. This allows you to copy the terminal's font information such as foreground color, background color, and font.
    - If you want to limit copying the formatting to certain key bindings (or commands), you can modify the `copyFormatting` parameter on a `copy` action.
- Copying as a single line
    - You can copy text as a single line using the `singleLine` parameter in the `copy` action.
- Removing trailing whitespace from block selections
    - You can remove the trailing whitespace from a block selection using the `trimBlockSelection` global setting.

You can also use the `copyOnSelect` global setting to have newly selected text automatically copied to your clipboard. With this setting enabled, if a selection is present, right-clicking the terminal copies and pastes the selected text to your terminal.

> [!NOTE]
> If `copyOnSelect` is enabled, modifying the selection using the keyboard does not automatically copy the newly selected text. You will need to manually copy the text using the `copy` action or by right-clicking the terminal.

## Customizing the appearance of selections

Color schemes let you customize the selection color using the `selectionBackground` property in a color scheme. Alternatively, you can override the selection color for a specific profile using the `selectionBackground` profile setting.

## Customizing word delimiters

As mentioned above, double-clicking and using <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + Arrow keys (or <kbd>Ctrl</kbd> + Arrow keys when in mark mode) allow you to navigate by word. However, words can be separated by more than just whitespace. You can customize these word boundaries using the `wordDelimiters` global setting.