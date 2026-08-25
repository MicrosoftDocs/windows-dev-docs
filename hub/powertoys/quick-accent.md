---
title: Quick Accent Utility for Windows PowerToys
description: Learn how to use Quick Accent utility in PowerToys to type accented characters on Windows when your keyboard lacks accent support. Configure settings and activation methods.
ms.date: 08/25/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Quick Accent, Win]
# Customer intent: As a Windows power user, I want to learn how to configure and use the Quick Accent utility in PowerToys for Windows.
---

# Quick Accent utility

:::image type="content" source="images/quick-accent/quick-accent.gif" alt-text="An animated GIF of Quick Accent utility overlay showing accented character options in PowerToys.":::

Quick Accent utility provides an alternative way to type accented characters in Windows PowerToys. This tool helps users whose keyboards don't support specific accents with quick key combinations, making it easier to type international characters. The utility is based on [Damien Leroy's PowerAccent](https://github.com/damienleroy/PowerAccent).

In order to use the Quick Accent utility, open PowerToys Settings, select the **Quick Accent** page, and turn on the **Enable** toggle.

## How to activate

Quick Accent supports four **Activation key** options. **Left/Right Arrow**, **Space**, and **Left, Right or Space** use a trigger key. **Press and hold the letter** opens the picker automatically after you hold the base letter long enough.

### Trigger-key activation

In trigger-key mode, press and hold the base letter, then press the activation key. After the **Input delay (ms)** time passes, the picker opens. Keep using the activation key, or use the arrow keys and <kbd>Space</kbd>, to move the selection. Release the keys to insert the selected character.

For example, to type `à`, press and hold <kbd>A</kbd>, then press <kbd>Space</kbd>. By default, **Input delay (ms)** is `300`, and the minimum value is `100`.

### Press-and-hold activation

In **Press and hold the letter** mode, Quick Accent types the base letter immediately. If you keep holding the letter, the picker opens automatically after the **Hold duration (ms)** time passes. Use the arrow keys or <kbd>Space</kbd> to choose a character, then release the letter to replace the original letter with the selected character. If you don't select a character, the original letter remains.

A quick tap that is shorter than the hold duration types only the original letter. Quick Accent doesn't intercept <kbd>Ctrl</kbd>, <kbd>Alt</kbd>, <kbd>AltGr</kbd>, or <kbd>Windows</kbd>-key shortcuts. By default, **Hold duration (ms)** is `500`, and the range is `100` to `3000`.

## Character sets

Use **Characters** in PowerToys Settings to limit the available characters. Character sets are organized into two groups, matching what you see in Settings. Recent additions include **Belarusian Latin**, **Belarusian Cyrillic**, and **Pitjantjatjara/Yankunytjatjara**. The supported sets also include newer punctuation and currency entries, such as inverted question and exclamation marks in the relevant Spanish and Catalan menus, and the Philippine peso symbol in **Currency**.

**Language sets:**

- Belarusian Latin
- Belarusian Cyrillic
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
- Pitjantjatjara/Yankunytjatjara
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
| Activation key | Choose **Left/Right Arrow**, **Space**, **Left, Right or Space** (default), or **Press and hold the letter**. |
| Do not activate when Game Mode is on | Prevents Quick Accent from activating when Game Mode is on, avoiding interference while gaming. |
| Characters | Show only characters that are in the selected language and special sets. |
| Toolbar position | Choose **Top center** (default), **Bottom center**, **Left**, **Right**, **Top right corner**, **Top left corner**, **Bottom right corner**, **Bottom left corner**, or **Center**. |
| Show the Unicode code and name of the currently selected character | Shows the Unicode code (in hexadecimal) and name of the currently selected character under the selector. |
| Sort characters by usage frequency | Tracks the characters you use and moves the most-used characters earlier in the list. |
| Start selection from the left | Starts the selection from the leftmost character for all activation keys, including **Left/Right Arrow**. |
| Input delay (ms) | In trigger-key modes, controls how long you hold the key after pressing the activation key before the picker opens. The default is `300`, and the minimum value is `100`. |
| Hold duration (ms) | In **Press and hold the letter** mode, controls how long you hold the base letter before the picker opens automatically. The default is `500`, and the range is `100` to `3000`. |
| Excluded apps | Add an application's name, or part of the name, one per line (e.g. adding `Notepad` will match both `Notepad.exe` and `Notepad++.exe`; to match only `Notepad.exe` add the `.exe` extension). |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
