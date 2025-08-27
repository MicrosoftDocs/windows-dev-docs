---
title: Quick Accent Utility for Windows PowerToys
description: Learn how to use Quick Accent utility in PowerToys to type accented characters on Windows when your keyboard lacks accent support. Configure settings and activation methods.
ms.date: 08/20/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Quick Accent, Win]
# Customer intent: As a Windows power user, I want to learn how to configure and use the Quick Accent utility in PowerToys for Windows.
---

# Quick Accent utility

:::image type="content" source="../images/pt-quick-accent.gif" alt-text="An animated GIF of Quick Accent utility overlay showing accented character options in PowerToys.":::

Quick Accent utility provides an alternative way to type accented characters in Windows PowerToys. This tool helps users whose keyboards don't support specific accents with quick key combinations, making it easier to type international characters. The utility is based on [Damien Leroy's PowerAccent](https://github.com/damienleroy/PowerAccent).

In order to use the Quick Accent utility, open PowerToys Settings, select the **Quick Accent** page, and turn on the **Enable** toggle.

## How to activate

Activate by holding the key for the character you want to add an accent to, then (while held down) press the activation key (Space key or Left / Right arrow keys). If you continue to hold, an overlay to choose the accented character will appear.

For example: If you want "à", press and hold <kbd>A</kbd> and press <kbd>Space</kbd>.

With the dialog enabled, keep pressing your activation key.

## Character discovery

Quick Accent maps accented characters to their base letter keys. To find a specific character:

1. **Hold the base letter key** (like A, E, I, O, U, C, N, S, Z, etc.) that the accented character is based on
2. **Press your activation key** (Space or arrow keys) while holding the base key
3. **Browse available characters** using your activation key to cycle through options
4. **Release both keys** when the desired character is highlighted

### Supported base keys

Most alphabetic keys support accented characters, including:
- **Vowels**: A, E, I, O, U (most accented variations)
- **Consonants**: C, D, G, H, J, K, L, N, R, S, T, W, Y, Z (language-specific accents)
- **Special keys**: Some punctuation and symbol keys for currency and mathematical symbols

### Unsupported keys

The following keys do **not** trigger Quick Accent overlays:
- **Semicolon (;)** - No accented variations available
- **Most numbers (0-9)** - Limited special character support
- **Function keys (F1-F12)** - Not supported
- **Modifier keys** (Ctrl, Alt, Shift) - Used for activation only
- **Space bar** - Used as activation key only

## Character sets

You can limit the available characters by selecting character sets from the settings menu. Available character sets are:

- Catalan
- Currency
- Croatian
- Czech
- Danish
- Gaeilge
- Gàidhlig
- Dutch
- Greek
- Estonian
- Finnish
- French
- German
- Hebrew
- Hungarian
- Icelandic
- Italian
- Kurdish
- Lithuanian
- Macedonian
- Māori
- Norwegian
- Pinyin
- Polish
- Portuguese
- Romanian
- Slovak
- Slovenian
- Spanish
- Serbian
- Serbian Cyrillic
- Swedish
- Turkish
- Vietnamese
- Welsh

### Common character mappings

Here are examples of character mappings for popular character sets:

#### French character set
- **A key**: à, á, â, ä, ā, ă, ą
- **E key**: è, é, ê, ë, ē, ĕ, ė, ę, ě
- **C key**: ç, ć, ĉ, ċ, č
- **O key**: ò, ó, ô, ö, ō, ŏ, ő, ø, œ

#### German character set  
- **A key**: ä, á, à, â, ā, ă, ą
- **O key**: ö, ó, ò, ô, ō, ŏ, ő, ø
- **U key**: ü, ú, ù, û, ū, ŭ, ů, ű, ų
- **S key**: ß (eszett), ś, ŝ, ş, š

#### Spanish character set
- **A key**: á, à, â, ä, ā, ă, ą
- **E key**: é, è, ê, ë, ē, ĕ, ė, ę, ě
- **N key**: ñ, ń, ņ, ň, ŋ
- **U key**: ú, ù, û, ü, ū, ŭ, ů, ű, ų

#### Currency character set
- **S key**: $, £, ¥, ₹, ₽, ₩, ₴, ₦, ₡, ₨, ₪, ₫, ₵, ₶, ₷, ₸, ₹, ₺, ₻, ₼, ₽, ₾, ₿, ﷼
- **E key**: €, ₤, ₧, ₨, ₩, ₫
- **C key**: ¢, ₡, ₵
- **F key**: ₣, ₦
- **L key**: £, ₤, ₦, ₧, ₨, ₩, ₫, €, ₯, ₰, ₱, ₲, ₳, ₴, ₵, ₶, ₷, ₸, ₹, ₺, ₻, ₼, ₽, ₾, ₿

### International Phonetic Alphabet (IPA) characters

IPA symbols are available across multiple character sets. Common IPA characters include:

- **Vowel symbols**: Available through vowel keys (A, E, I, O, U) in various character sets
- **Consonant symbols**: Available through consonant keys in language-specific character sets  
- **Special IPA symbols**: Some available through Greek character set
- **Length markers**: The vowel length/gemination marker **ː** (triangular colon) may be available through colon (:) key in certain character sets, or through T key in some language sets

> **Note**: If you cannot find a specific IPA symbol like ː (vowel length marker), try:
> 1. Enable multiple character sets (Greek, Hebrew, or language sets that use similar symbols)
> 2. Try different base keys (colon, T, or vowel keys)
> 3. Use the "Show Unicode code and name" setting to identify available characters

## Settings

From the Settings menu, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Activation key | Choose **Left/Right Arrow**, **Space** or **Left, Right or Space**. |
| Character set | Show only characters that are in the chosen sets. |
| Toolbar location | Position of the toolbar. |
| Show the Unicode code and name of the currently selected character | Shows the Unicode code (in hexadecimal) and name of the currently selected character under the selector. |
| Sort characters by usage frequency | |
| Start selection from the left | Starts the selection from the leftmost character for all activation keys (including Left/Right arrow). |
| Input delay | The delay in milliseconds before the dialog appears. |
| Excluded apps | Add an application's name, or part of the name, one per line (e.g. adding `Notepad` will match both `Notepad.exe` and `Notepad++.exe`; to match only `Notepad.exe` add the `.exe` extension). |

## Troubleshooting character discovery

If you cannot find a specific character you need:

### Enable relevant character sets
1. **Multiple character sets**: Enable several character sets that might contain your character
2. **Language-specific sets**: Choose character sets for languages that use similar symbols
3. **Specialized sets**: Try Currency, Greek, or Hebrew sets for special symbols

### Try different base keys
1. **Phonetic similarity**: Try keys that sound similar to your target character
2. **Visual similarity**: Try keys that look similar to your target character  
3. **Related characters**: Try base keys of characters in the same family

### Use Unicode information
1. **Enable Unicode display**: Turn on "Show the Unicode code and name of the currently selected character" in settings
2. **Character identification**: Use this to identify what characters are available on each key
3. **Character lookup**: Search online for the Unicode name if you know the character you need

### Common troubleshooting examples
- **ñ (Spanish n with tilde)**: Try N key with Spanish character set enabled
- **ß (German eszett)**: Try S key with German character set enabled  
- **œ (Latin small ligature oe)**: Try O key or E key with French character set enabled
- **ø (Latin small letter o with stroke)**: Try O key with Danish or Norwegian character set enabled
- **ː (IPA triangular colon)**: Try colon (:) key, T key, or vowel keys with Greek or Hebrew character sets enabled

### Alternative solutions
If Quick Accent doesn't have the character you need:
1. **Windows Character Map**: Use Windows built-in Character Map utility
2. **Alt codes**: Use numeric Alt codes for specific characters
3. **Unicode input**: Use Windows Unicode input method (Alt + numeric keypad)
4. **Copy-paste**: Copy characters from online Unicode references

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
