---
title: Quick Accent Utility for Windows PowerToys
description: Learn how to use Quick Accent utility in PowerToys to type accented characters on Windows when your keyboard lacks accent support. Configure settings and activation methods.
ms.date: 06/08/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Quick Accent, Win]
# Customer intent: As a Windows power user, I want to learn how to configure and use the Quick Accent utility in PowerToys for Windows.
---

# Quick Accent utility

:::image type="content" source="images/quick-accent/quick-accent.gif" alt-text="An animated GIF of Quick Accent utility overlay showing accented character options in PowerToys.":::

Quick Accent utility provides an alternative way to type accented characters in Windows PowerToys. This tool helps users whose keyboards don't support specific accents with quick key combinations, making it easier to type international characters. The utility is based on [Damien Leroy's PowerAccent](https://github.com/damienleroy/PowerAccent).

In order to use the Quick Accent utility, open PowerToys Settings, select the **Quick Accent** page, and turn on the **Enable** toggle.

## How to activate

Activate by holding the key for the character you want to add an accent to, then (while held down) press the activation key (Space key or Left / Right arrow keys). If you continue to hold, an overlay to choose the accented character will appear.

For example: If you want "à", press and hold <kbd>A</kbd> and press <kbd>Space</kbd>.

With the dialog enabled, keep pressing your activation key.

## Character sets

You can limit the available characters by selecting character sets from the settings menu. Character sets are organized into two groups, matching what you see in PowerToys Settings.

**Language sets:**

- Bulgarian
- Catalan
- Crimean Tatar
- Croatian
- Czech
- Danish
- Dutch
- Esperanto
- Estonian
- Finnish
- French
- Gaeilge
- Gàidhlig
- German
- Greek
- Hebrew
- Hungarian
- Icelandic
- Italian
- Kurdish
- Lithuanian
- Macedonian
- Maltese
- Maori
- Norwegian
- Pinyin
- Polish
- Portuguese
- Romanian
- Serbian
- Serbian Cyrillic
- Slovak
- Slovenian
- Spanish
- Swedish
- Turkish
- Vietnamese
- Welsh

**Special sets:**

- Special Characters
- Currency
- Greek Polytonic
- IPA
- Middle Eastern Romanization
- Proto Indo European

For the canonical, always up-to-date list, see [`CharacterMappings.cs`](https://github.com/microsoft/PowerToys/blob/main/src/modules/poweraccent/PowerAccent.Common/CharacterMappings.cs) in the PowerToys source.

## Settings

From the Settings menu, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Activation key | Choose **Left/Right Arrow**, **Space** or **Left, Right or Space**. |
| Do not activate when Game Mode is on | Prevents Quick Accent from activating when Game Mode is on, avoiding interference while gaming. |
| Character set | Show only characters that are in the chosen sets. |
| Toolbar location | Position of the toolbar. |
| Show the Unicode code and name of the currently selected character | Shows the Unicode code (in hexadecimal) and name of the currently selected character under the selector. |
| Sort characters by usage frequency | |
| Start selection from the left | Starts the selection from the leftmost character for all activation keys (including Left/Right arrow). |
| Input delay | The delay in milliseconds before the dialog appears. |
| Excluded apps | Add an application's name, or part of the name, one per line (e.g. adding `Notepad` will match both `Notepad.exe` and `Notepad++.exe`; to match only `Notepad.exe` add the `.exe` extension). |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
