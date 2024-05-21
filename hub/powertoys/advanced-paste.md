---
title: PowerToys Advanced Paste for Windows
description: An AI enhanced tool that enables you to intelligently paste the text from your clipboard into any format needed.
ms.date: 04/24/2024
ms.topic: article
no-loc: [PowerToys, Windows, Paste as Plain Text, Advanced Paste, Win]
---

# Advanced Paste

PowerToys **Advanced Paste** is an AI enhanced tool that enables you to intelligently paste the text from your clipboard into any format needed.

## Getting started

### Enabling

To start using Advanced Paste, enable it in the PowerToys Settings.

### Activating

Open the **Advanced Paste** window with the activation shortcut (default: <kbd>Win</kbd>+<kbd>Shift</kbd>+<kbd>V</kbd>). See the [Settings](#settings) section for more information on customizing the activation shortcut and additional shortcut actions.

## Settings

From the Settings menu, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Enable Paste with AI | Enables the AI-powered paste feature. An OpenAI API key is required. |
| Custom format preview | Enable to preview the output of the custom format before pasting. |
| Clipboard history | Enable to automatically save clipboard history. |
| Open Advanced Paste shortcut | The customizable keyboard command to open the **Advanced Paste** window. |
| Paste as plain text directly shortcut | The customizable keyboard command to paste as plain text without opening the **Advanced Paste** window. |
| Paste as Markdown directly shortcut | The customizable keyboard command to paste as Markdown without opening the **Advanced Paste** window. |
| Paste as JSON directly shortcut | The customizable keyboard command to paste as JSON without opening the **Advanced Paste** window. |

> [!IMPORTANT]
> It's possible to set <kbd>Ctrl</kbd>+<kbd>V</kbd> as an activation shortcut. This is not recommended, as overriding this shortcut may have unintended consequences.

## Advanced text paste

Advanced Paste includes several text-based paste options. These options are available in the **Advanced Paste** window, which can be opened using the activation shortcut. You can also paste as plain text, markdown, or JSON directly using the customizable keyboard commands.

:::image type="content" source="../images/pt-advanced-paste.png" alt-text="Advanced Paste screenshot":::

### Paste as Plain Text

**Paste as Plain Text** enables you to paste text stored in your clipboard, excluding any text-formatting, using a quick key shortcut. Any formatting included with the clipboard text will be replaced with an unformatted version of the text.

:::image type="content" source="../images/pt-paste-as-plain-text.png" alt-text="Paste as Plain Text screenshot":::

### Paste as JSON

**Paste as JSON** enables you to paste text stored in your clipboard, updating any text-formatting to JSON, using a quick key shortcut. Any formatting included with the clipboard text will be replaced with a JSON formatted version of the text.

Sample input:

```xml
<note>
    <to>Mr. Smith</to>
    <from>Ms. Nguyen</from>
    <body>Do you like PowerToys?</body>
</note>
```

JSON output:

```json
{
    "note": {
        "to": "Mr. Smith",
        "from": "Ms. Nguyen",
        "body": "Do you like PowerToys?"
    }
}
```

### Paste as Markdown

**Paste as Markdown** enables you to paste text stored in your clipboard, updating any text-formatting to markdown, using a quick key shortcut. Any formatting included with the clipboard text will be replaced with a markdown formatted version of the text.

Sample input:

```html
<b>Paste</b> <i>as</i> <a href="https://en.wikipedia.org/wiki/Markdown">Markdown</a>
```

Markdown output:

```md
**Paste** *as* [Markdown](https://en.wikipedia.org/wiki/Markdown)
```

### Paste text with AI

When you paste text with AI, the text is analyzed and formatted based on the context of the text and the prompt provided to the OpenAI call. This feature requires that an OpenAI API key be provided in the PowerToys settings, and that you have available credits in your account.

> [!NOTE]
> If you use this feature and see an error `API key quota exceeded`, that means you do not have credits in your OpenAI account and would need to purchase them.

Some examples of how this feature can be used include:

- **Summarize text**: Take long text from the clipboard and ask the AI to summarize it.
- **Translate text**: Take the text from the clipboard in one language and ask the AI to translate it to another language.
- **Generate code**: Take a description of a function from the clipboard and ask the AI to generate the code for it.
- **Transform text**: Take text from the clipboard and ask the AI to rewrite it in a specific style, such as a professional email or a casual message.
- **Stylize text**: Take text from the clipboard and ask the AI to rewrite it in the style of a well-known author, book, or speaker.

You could ask the AI to paste the text as if it were written by Mark Twain or Shakespeare, for example, or to summarize a long case study. The possibilities are endless.

Sample input:

> The new Advanced Paste feature in PowerToys is now available. You can use it to save time and improve your writing.

AI output when prompting to "Format the text as if it were written by Mark Twain":

> Say, have you heard the news? The newfangled Advanced Paste feature in PowerToys is finally here! It's a nifty tool that's sure to save you time and spruce up your writing. If you're in the market for a bit of writing wizardry, this here Advanced Paste just might be the ticket for ya.

> [!NOTE]
> As with any AI tool, the quality of the output is dependent on the quality of the input. The more context you provide, the better the AI will be able to understand and respond to your request. Be sure to carefully review the output before using it. Please see OpenAI's [privacy](https://openai.com/policies/privacy-policy) and [terms](https://openai.com/policies/terms-of-use) pages for more info on AI usage in this feature.

## See also

- [Microsoft PowerToys: Utilities to customize Windows](index.md)
- [Installing PowerToys](install.md)
- [General settings for PowerToys](general.md)
