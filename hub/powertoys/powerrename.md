---
title: PowerToys PowerRename utility for Windows 10
description: A windows shell extension for bulk renaming of files
ms.date: 12/02/2020
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerRename, Pampalona, Windows, File Explorer, regex]
---

# PowerRename utility

PowerRename is a bulk renaming tool that enables you to:

- Modify the file names of a large number of files *(without renaming all of the files the same name)*.
- Perform a search and replace on a targeted section of file names.
- Perform a regular expression rename on multiple files.
- Check expected rename results in a preview window before finalizing a bulk rename.
- Undo a rename operation after it is completed.

## Demo

In this demo, all instances of the file name "Pampalona" are replaced with "Pamplona". Since all of the files are uniquely named, this would have taken a long time to complete manually one-by-one. PowerRename enables a single bulk rename. Notice that the "Undo Rename" (Ctrl+Z) command enables the ability to undo the change.

![PowerRename Demo](../images/powerrename-demo.gif)

## PowerRename menu

After selecting some files in Windows File Explorer, right-clicking and selecting *PowerRename* (which will appear only when enabled in PowerToys), the PowerRename menu will appear. The number of items (files) you've selected will be displayed, along with search and replace values, a list of options, and a preview window displaying results of the search and replace values you've entered.

![PowerRename Menu screenshot](../images/powerrename-menu.png)

### Search for

Enter text or a [regular expression](https://wikipedia.org/wiki/Regular_expression) to find the files in your selection that contain the criteria matching your entry. You will see the matching items in the *Preview* window.

### Replace with

Enter text to replace the *Search for* value entered previously that match you're selected files. You can view the original file name and renamed file in the *Preview* window.

### Options - Use Regular Expressions

If checked, the Search value will be interpreted as a [regular expression](https://wikipedia.org/wiki/Regular_expression) (regex). The Replace value can also contain regex variables (see examples below).  If not checked, the Search value will be interpreted as plain text to be replaced with the text in the Replace field.

For more information regarding the `Use Boost library` option in the settings menu for extended regex functionalities, see the [regular expressions section](#regular-expressions).

### Options - Case Sensitive

If checked, the text specified in the Search field will only match text in the items if the text is the same case. Case matching will be insensitive (not recognizing a difference between upper and lowercase letters) by default.

### Options - Match All Occurrences

If checked, all matches of text in the Search field will be replaced with the Replace text.  Otherwise, only the first instance of the Search for text in the file name will be replaced (left to right).

For example, given the file name: `powertoys-powerrename.txt`:

- Search for: `power`
- Rename with: `super`

The value of the renamed file would result in:

- Match All Occurrences (unchecked): `supertoys-powerrename.txt`
- Match All Occurrences (checked): `supertoys-superrename.txt`

### Options - Exclude Files

Files will not be included in the operation. Only folders will be included.

### Options - Exclude Folders

Folders will not be included in the operation. Only files will be included.

### Options - Exclude Subfolder Items

Items within folders will not be included in the operation. By default, all subfolder items are included.

### Options - Enumerate Items

Appends a numeric suffix to file names that were modified in the operation. For example: `foo.jpg` -> `foo (1).jpg`

### Options - Item Name Only

Only the file name portion (not the file extension) is modified by the operation. For example: `txt.txt` ->  `NewName.txt`

### Options - Item Extension Only

Only the file extension portion (not the file name) is modified by the operation. For example: `txt.txt` -> `txt.NewExtension`

## Replace using file creation date and time

The creation date and time attributes of a file can be used in the *Replace with* text by entering a variable pattern according to the table below.

Variable pattern |Explanation
|:---|:---|
|`$YYYY`|Year represented by a full four or five digits, depending on the calendar used.
|`$YY`|Year represented only by the last two digits. A leading zero is added for single-digit years.
|`$Y`|Year represented only by the last digit.
|`$MMMM`|Name of the month
|`$MMM`|Abbreviated name of the month
|`$MM`|Month as digits with leading zeros for single-digit months.
|`$M`|Month as digits without leading zeros for single-digit months.
|`$DDDD`|Name of the day of the week
|`$DDD`|Abbreviated name of the day of the week
|`$DD`|Day of the month as digits with leading zeros for single-digit days.
|`$D`|Day of the month as digits without leading zeros for single-digit days.
|`$hh`|Hours with leading zeros for single-digit hours
|`$h`|Hours without leading zeros for single-digit hours
|`$mm`|Minutes with leading zeros for single-digit minutes.
|`$m`|Minutes without leading zeros for single-digit minutes.
|`$ss`|Seconds with leading zeros for single-digit seconds.
|`$s`|Seconds without leading zeros for single-digit seconds.
|`$fff`|Milliseconds represented by full three digits.
|`$ff`|Milliseconds represented only by the first two digits.
|`$f`|Milliseconds represented only by the first digit.

For example, given the file names:

- `powertoys.png`, created on 11/02/2020
- `powertoys-menu.png`, created on 11/03/2020

Enter the criteria to rename the items:

- Search for: `powertoys`
- Rename with: `$MMM-$DD-$YY-powertoys`

The value of the renamed file would result in:

- `Nov-02-20-powertoys.png`
- `Nov-03-20-powertoys-menu.png`

## Regular Expressions

For most use cases, a simple search and replace is sufficient. There may be occasions, however, in which complicated renaming tasks come along that require more control. [Regular Expressions](https://wikipedia.org/wiki/Regular_expression) can help.

Regular Expressions define a search pattern for text. They can be used to search, edit and manipulate text. The pattern defined by the regular expression may match once, several times, or not at all for a given string. PowerRename uses the [ECMAScript](https://wikipedia.org/wiki/ECMAScript) grammar, which is common amongst modern programming languages.

To enable regular expressions, check the "Use Regular Expressions" checkbox.

**Note:** You will likely want to check "Match All Occurrences" while using regular expressions.

To use the [Boost library](https://www.boost.org/doc/libs/1_74_0/libs/regex/doc/html/boost_regex/syntax/perl_syntax.html) instead of the standard library, check the `Use Boost library` option in the PowerToys settings. It enables extended features, like [lookbehind](https://www.boost.org/doc/libs/1_74_0/libs/regex/doc/html/boost_regex/syntax/perl_syntax.html#boost_regex.syntax.perl_syntax.lookbehind), which are not supported by the standard library.

### Examples of regular expressions

#### Simple matching examples

| Search for       | Description                                           |
| ---------------- | ------------- |
| `^`              | Match the beginning of the filename                   |
| `$`              | Match the end of the filename                         |
| `.*`             | Match all the text in the name                        |
| `^foo`           | Match text that begins with "foo"                     |
| `bar$`           | Match text that ends with "bar"                       |
| `^foo.*bar$`     | Match text that begins with "foo" and ends with "bar" |
| `.+?(?=bar)`     | Match everything up to "bar"                          |
| `foo[\s\S]*bar`  | Match everything between "foo" and "bar"              |

#### Matching and variable examples

*When using the variables, the "Match All Occurrences" option must be enabled.*

| Search for   | Replace With    | Description                                |
| ------------ | --------------- |--------------------------------------------|
| `(.*).png`   | `foo_$1.png`   | Prepends "foo\_" to the existing file name |
| `(.*).png`   | `$1_foo.png`   | Appends "\_foo" to the existing file name  |
| `(.*)`       | `$1.txt`        | Appends ".txt" extension to existing file name |
| `(^\w+\.$)|(^\w+$)` | `$2.txt` | Appends ".txt" extension to existing file name only if it does not have an extension |
|  `(\d\d)-(\d\d)-(\d\d\d\d)` | `$3-$2-$1` | Move numbers in the filename: "29-03-2020" becomes "2020-03-29" |

### Additional resources for learning regular expressions

There are great examples/cheat sheets available online to help you

[Regex tutorial — A quick cheatsheet by examples](https://medium.com/factory-mind/regex-tutorial-a-simple-cheatsheet-by-examples-649dc1c3f285)

[ECMAScript Regular Expressions Tutorial](https://o7planning.org/en/12219/ecmascript-regular-expressions-tutorial)

## File List Filters

Filters can be used in PowerRename to narrow the results of the rename. Use the *Preview* window to check expected results. Select the column headers to switch between filters.

- **Original**, the first column in the *Preview* window cycles between:
  - Checked: The file is selected be renamed.
  - Unchecked: The file is not selected to be renamed (even though it fits the value entered in the search criteria).

- **Renamed**, the second column in the *Preview* windows can be toggled.
  - The default preview will show all selected files, with only files matching the *Search for* criteria displaying the updated rename value.
  - Selecting the *Renamed* header will toggle the preview to only display files that will be renamed. Other selected files from your original selection will not be visible.

![PowerToys PowerRename Filter demo](../images/powerrename-demo2.gif)
