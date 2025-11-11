---
title: Windows Terminal Search
description: Learn how to search in Windows Terminal.
ms.date: 11/10/2025
ms.topic: how-to 
---

# How to search in Windows Terminal

Windows Terminal includes a search feature that lets you look through the text buffer for a specific keyword. This feature is useful when you want to find a command you ran earlier or a specific file name.

## Using search

By default, you can open the search dialog by typing <kbd>Ctrl+Shift+F</kbd>. When the dialog opens, type the keyword you're looking for into the text box and press <kbd>Enter</kbd> to start the search.

![Windows Terminal search screenshot](./images/search.png)

## Directional search

The terminal searches from the bottom to the top of the text buffer by default. You can change the search direction by selecting one of the arrows in the search dialog.

![Windows Terminal directional search screenshot](./images/search-direction.gif)

## Case match search

To narrow down your search results, add case matching as an option in your search. You can toggle case matching by selecting the case match button. The results that appear only match the keyword entered with its specific letter casing.

![Windows Terminal case matching search screenshot](./images/search-case-match.gif)

## Searching within panes

The search dialog works with [panes](./panes.md) as well. When you focus on a pane, you can open the search dialog, and it appears on the upper-right of that pane. Then, any keyword you enter only shows results found within that pane.

![Windows Terminal panes search screenshot](./images/search-panes.gif)

## Customize the search key binding

You can open the search dialog with any key binding (shortcut key combination) that you prefer. To change the search key binding, open your [settings.json file](./install.md#settings-json-file) and search for the `find` command. By default, this command is set to <kbd>Ctrl+Shift+F</kbd>.

```json
// Press ctrl+shift+f to open the search box
        { "command": "find", "keys": "ctrl+shift+f" },
```

For example, you can change `"ctrl+shift+f"` to `"ctrl+f"`, so when typing <kbd>Ctrl+F</kbd>.

To learn more about key bindings, visit the [Actions page](./customize-settings/actions.md).
