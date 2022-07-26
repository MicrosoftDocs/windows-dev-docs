---
title: Enabling tab completion with the winget tool
description: Provides steps on how to enable and use tab completion with the winget tool.
ms.date: 05/18/2022
ms.topic: article
---

# Tab completion (winget)

The winget command line tool offers a `complete` command to provide context-sensitive tab completion. It supports completion of command names, argument names, and argument values, dependent on the current command line state.

## Enable tab completion

To enable tab completion with winget, you must add the following script to your `$PROFILE` in PowerShell.

1. Open PowerShell and enter the following command to open your `$PROFILE` in Notepad: `notepad.exe $PROFILE`

2. Copy and paste the following script into the `$PROFILE` file that has opened in Notepad:

    ```PowerShell
    Register-ArgumentCompleter -Native -CommandName winget -ScriptBlock {
        param($wordToComplete, $commandAst, $cursorPosition)
            [Console]::InputEncoding = [Console]::OutputEncoding = $OutputEncoding = [System.Text.Utf8Encoding]::new()
            $Local:word = $wordToComplete.Replace('"', '""')
            $Local:ast = $commandAst.ToString().Replace('"', '""')
            winget complete --word="$Local:word" --commandline "$Local:ast" --position $cursorPosition | ForEach-Object {
                [System.Management.Automation.CompletionResult]::new($_, $_, 'ParameterValue', $_)
            }
    }
    ```

3. Save the `$PROFILE` with your script. Then close and reopen PowerShell. Once PowerShell has been reopened, winget tab completion will be enabled.

## Examples of tab completion

Repeated presses of tab (`⇥`) will result in cycling through the possible values.

Input | Result | Reason
--- | --- | ---
`winget ⇥` | `winget install` | `install` is the first command below the root
`winget sh⇥` | `winget show` | `show` is the first command that starts with `sh`
`winget source l⇥` | `winget source list` | `list` is the first sub-command of source that starts with `l`
`winget -⇥` | `winget --version` | `--version` is the first argument defined for the root
`winget install power⇥` | `winget install "Power Toys"` | `"Power Toys"` is the first package whose Id, Name, or Moniker starts with `power`
`winget install "Power Toys" --version ⇥` | `winget install "Power Toys" --version 0.19.2` | `0.19.2` is the highest version of Power Toys at the time of writing

## Command Reference

The complete command takes 3 required arguments:

Argument | Description
--- | ---
`--word` | The current word that is being completed; the token that the cursor is located within. Can be empty to indicate no current value at the cursor, but if provided, it must appear as a substring in the command line.
`--commandline` | The entire current command line, including `winget`. See the examples above; everything but the tab character (`⇥`) should be provided to this argument.
`--position` | The current position of the cursor in the command line. Can be greater than the length of the command line string to indicate at the end.

When a word value is provided, the completion operates in replacement mode.  It will suggest completions that would fit correctly at this location that also start with the given word value.

When a word value is not provided (an empty value is provided for word, ex. `--word=`), the completion operates in insertion mode.  It will suggest completions that would fit as a new value in the cursor's location.

Based on the arguments, the completions suggested can be one of:

1. A sub command :: The cursor is located just after a command and there are sub commands available.
2. An argument specifier :: The cursor is not positioned after an argument specifier that expects a value, and there are arguments available.
3. An argument value :: The cursor is positioned after an argument specifier that expects a value, or a positional argument is expected.

After evaluating all of these cases, the potential completions are output, one on each line. If the completion string contains a space, it is wrapped in quotations.

## Related topics

* [Use the winget tool to install and manage applications](index.md)
* [Submit packages to Windows Package Manager](../package/index.md)
