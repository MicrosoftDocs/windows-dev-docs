---
title: Advanced Paste Tool for Clipboard Management in PowerToys
description: Learn how to use PowerToys Advanced Paste to transform clipboard content into any format - plain text, JSON, Markdown, or files. Includes opt-in AI-powered features and local OCR capabilities.
ms.date: 11/12/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Paste as Plain Text, Advanced Paste, Win]
# Customer intent: Learn how to use the Advanced Paste feature in PowerToys to paste text from your clipboard into any format needed.
---

# Advanced Paste tool for clipboard management

PowerToys **Advanced Paste** is a powerful clipboard management tool that transforms your clipboard content into any format you need. This tool enables you to paste content as plain text, markdown, JSON, or various file formats (.txt, .html, .png) by using either the interface or keyboard shortcuts. Advanced Paste can extract text from images by using local OCR technology and transcode audio and video files to .mp3 or .mp4 formats. All processing happens locally on your machine, with an optional AI-powered feature that requires either an API key for a cloud AI service or a local model configured in Foundry Local or Ollama.

## Get started with Advanced Paste

Get familiar with the features and capabilities of Advanced Paste.

### Enable Advanced Paste

To start using Advanced Paste, enable it in the PowerToys Settings.

### Activate the Advanced Paste window

Open the **Advanced Paste** window with the activation shortcut (default: <kbd>Win</kbd>+<kbd>Shift</kbd>+<kbd>V</kbd>). For more information on customizing the activation shortcut and additional shortcut actions, see the [Settings](#settings) section.

## Settings

From the Settings menu, configure the following options:

| Setting | Description |
| :--- | :--- |
| Enable Paste with AI | Enables the Paste with AI feature from the Advanced Paste window. See [Paste text with AI](#paste-text-with-ai) for more information. |
| Model providers | Lists the configured AI model providers for use with Paste with AI. You can add providers with the **Add model** button. See [Adding a model provider](#adding-a-model-provider) for a list of supported providers and more information about configuring them. An account and valid API key is required for online model providers. You can also remove any of the configured providers from this list. |
| Clipboard history | Enable to automatically save clipboard history. |
| Automatically close the Advanced Paste window after it loses focus | Determines whether the Advanced Paste window closes after it loses focus. |
| Show clipboard preview | Display a preview of the current clipboard content in the Advanced Paste window. |
| Custom format preview | Enable to preview the output of the custom format before pasting. |
| Auto-copy selection for custom action hotkeys | When enabled, attempts to copy the current selection before running a custom action shortcut, so you can trigger custom actions directly on selected text without manually copying first. |
| Actions (Create and manage Advanced Paste custom actions) | When using Paste with AI, save the prompts you frequently use and give them descriptive names, so you can easily select them from the Advanced Paste window without having to type them out. You can also assign each action a keyboard command, so you can execute them without opening the Advanced Paste window. |
| Open Advanced Paste window shortcut | The customizable keyboard command to open the **Advanced Paste** window. |
| Paste as plain text directly shortcut | The customizable keyboard command to paste as plain text without opening the **Advanced Paste** window. |
| Paste as Markdown directly shortcut | The customizable keyboard command to paste as Markdown without opening the **Advanced Paste** window. |
| Paste as JSON directly shortcut | The customizable keyboard command to paste as JSON without opening the **Advanced Paste** window. |
| Additional actions \| Image to Text | Turn on/off the Image to text paste action and configure the customizable keyboard command. |
| Additional actions \| Paste as file | Turn on/off the set of Paste as File actions which include Paste as .txt file, Paste as .png file, Paste as .html file. Optionally configure the customizable keyboard command for each of these actions. |
| Additional actions \| Transcode audio / video | Turn on/off both the Transcode audio and video paste actions. The transcode settings are all enabled by default. |
| Additional actions \| Transcode to .mp3 | Turn on/off the Transcode to .mp3 paste action and configure the customizable keyboard command to transcode audio or video on the clipboard without opening the **Advanced Paste** window. |
| Additional actions \| Transcode to .mp4 (H.264/AAC) | Turn on/off the Transcode to .mp4 (H.264/AAC) paste action and configure the customizable keyboard command to transcode video on the clipboard without opening the **Advanced Paste** window. |

> [!IMPORTANT]
> You can set <kbd>Ctrl</kbd>+<kbd>V</kbd> as an activation shortcut. This choice isn't recommended, as overriding this shortcut might have unintended consequences.

### Adding a model provider

To add a model provider for use with Paste with AI, follow these steps:

1. In PowerToys Settings, go to the **Advanced Paste** section.
1. Under **Model providers**, select **Add model**.
1. In the **Add model provider** window, select the model provider you want from the dropdown list.
1. Fill in the required fields for the model provider you selected.
   - For online model providers, you usually need to provide the API key, endpoint URL, and any extra configuration options.
     - If you select OpenAI as the model provider, you can also enable Paste with Advanced AI using Semantic Kernel.
   - For local model providers, you usually need to provide the model path and any extra configuration options.
1. Select **Save** to add the model provider.

The new model provider appears in the list of configured model providers and you can use it with the Paste with AI feature.

The following model providers are supported:

| Model Provider | Type | Description |
| :--- | :--- | :--- |
| OpenAI | Online | Provides access to various AI models through an API. You need an API key and might pay for usage based on the model and how much you use it. For more information, see [OpenAI's pricing page](https://openai.com/pricing). |
| Azure OpenAI | Online | Offers access to OpenAI models hosted on Microsoft Azure. You need an API key and endpoint URL and might pay for usage based on the model and how much you use it. For more information, see [Azure OpenAI pricing page](https://azure.microsoft.com/pricing/details/cognitive-services/openai-service/). |
| Mistral | Online | Offers access to Mistral AI models through an API. You need an API key and might pay for usage based on the model and how much you use it. For more information, see [Mistral pricing page](https://mistral.ai/pricing). |
| Google | Online | Offers access to Google's AI models through an API. You need an API key and might pay for usage based on the model and how much you use it. For more information, see [Google Cloud AI pricing page](https://cloud.google.com/pricing/list). |
| Azure AI Inference | Online | Offers access to various AI models hosted on Microsoft Azure. You need an API key and endpoint URL and might pay for usage based on the model and how much you use it. For more information, see [Azure AI pricing page](https://azure.microsoft.com/pricing/details/cognitive-services/). |
| Foundry Local | Local | Allows you to run AI models on your own machine by using Foundry Local. You need to install and configure the Foundry Local application. For more information, see the [Foundry Local documentation](/azure/ai-foundry/foundry-local/). |
| Ollama | Local | Allows you to run AI models on your own machine by using Ollama. You need to install and configure the Ollama application. For more information, see the [Ollama documentation](https://docs.ollama.com/). |

## Advanced text paste

Advanced Paste includes several text-based paste options. You find these options in the **Advanced Paste** window. Open the window by using the activation shortcut. You can also use customizable keyboard commands to directly invoke a paste action with quick keys.

:::image type="content" source="images/pt-advanced-paste.png" alt-text="Advanced Paste screenshot":::

### Paste as Plain Text

**Paste as Plain Text** enables you to paste text stored in your clipboard, excluding any text-formatting, by using a quick key shortcut. The feature replaces any formatting included with the clipboard text with an unformatted version of the text.

:::image type="content" source="images/pt-paste-as-plain-text.png" alt-text="Paste as Plain Text screenshot":::

> [!NOTE]
> Paste as Plain Text is a feature that runs locally and doesn't use AI.

### Paste as JSON

**Paste as JSON** enables you to paste text stored in your clipboard, updating any text-formatting to JSON, by using a quick key shortcut. The feature replaces any formatting included with the clipboard text with a JSON formatted version of the text.

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

> [!NOTE]
> Paste as JSON is a feature that runs locally and doesn't use AI.

### Paste as Markdown

**Paste as Markdown** enables you to paste text stored in your clipboard, updating any text formatting to markdown by using a quick key shortcut. The feature replaces any formatting included with the clipboard text with a markdown formatted version of the text.

Sample input:

```html
<b>Paste</b> <i>as</i> <a href="https://en.wikipedia.org/wiki/Markdown">Markdown</a>
```

Markdown output:

```md
**Paste** *as* [Markdown](https://en.wikipedia.org/wiki/Markdown)
```

> [!NOTE]
> Paste as Markdown is a feature that runs locally and doesn't use AI.

### Paste as .txt file

**Paste as .txt file** enables you to paste text stored in your clipboard as a .txt file with an auto-generated file name. You can optionally set a quick key shortcut in settings.

Sample input:

```text
Hello World!
```

If the application that you're using accepts pasting files (for example, File Explorer), the paste as .txt file action takes the input text and pastes a .txt file.

> [!NOTE]
> Paste as .txt file is a feature that runs locally and doesn't use AI.

### Paste as .html file

**Paste as .html file** enables you to paste HTML data stored in your clipboard as a .html file with an auto-generated file name. This feature is especially useful for saving a part of a webpage from a browser - including links, formatted text, and images. You can optionally set a quick key shortcut in settings.

If the application you're using accepts pasting files (for example, File Explorer), the paste as .html file action takes the input data and pastes a .html file.

> [!NOTE]
> Paste as .html file is a feature that runs locally and doesn't use AI.

### Paste text with AI

When you paste text with AI, the text is analyzed and formatted based on the context of the text and the prompt you provide to the OpenAI call. To use this feature, you need to configure an AI provider in the PowerToys settings. For online model providers, you must also have available credits in your account.

> [!NOTE]
> If you use this feature and see an error `API key quota exceeded`, you don't have credits in your selected online AI account and need to purchase them.

Some examples of how you can use this feature include:

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
> As with any AI tool, the quality of the output depends on the quality of the input. The more context you provide, the better the AI can understand and respond to your request. Be sure to carefully review the output before using it. For more info on AI usage in this feature, review the terms of service and privacy policy pages for your selected model provider.

#### Advanced AI paste scenarios with OpenAI

If you use an OpenAI online model provider, the Paste with AI feature can use [Semantic Kernel](/semantic-kernel/overview/) to allow you to define a chain of actions to perform. By using custom prompts, you can:

- Work with non-text input such as images.
- Produce non-text output like files.
- Chain multiple actions together and execute them in sequence. For example, Image to text --> Text to JSON text --> JSON text to .txt file.
- Produce meaningful AI-generated error messages.

For these example commands, assume there's an image in the clipboard that contains some text that you want to save to a text file in another language. You can phrase multiple steps explicitly:

```
Convert this image to text using OCR, translate the text to French, and then save the text as a .txt file.
```

Or you can phrase the steps to be more implicit:

```
Translate to French and save as a .txt file.
```

> [!NOTE]
> Currently, Semantic Kernel functionality is only available when using OpenAI as the model provider.

## Advanced image paste

Advanced Paste includes several image-based paste options. You can find these options in the **Advanced Paste** window. Open the window by using the activation shortcut. You can also set a quick key shortcut in settings.

:::image type="content" source="images/pt-advanced-paste-img.png" alt-text="Advanced Paste image screenshot":::

### Paste Image to text

**Paste image to text** enables you to extract the text from an image in your clipboard and quickly paste the extracted text by using a quick key shortcut.

> [!NOTE]
> Paste as Image to text is a feature that runs locally by using local OCR.

### Paste as .png file

**Paste as .png file** enables you to quickly paste an image format, like a bitmap, to a .png file. You can optionally create a quick key shortcut to invoke this paste action.

> [!NOTE]
> Paste as .png file is a feature that runs locally and doesn't use AI.

## Transcode to audio or video

The **Advanced Paste** window offers two paste options that work with media files. You can open the **Advanced Paste** window by using the activation shortcut. You can also use customizable keyboard commands to directly invoke a paste action with quick keys. To the extent possible, the feature maintains quality settings (such as video dimensions and audio bitrate) from the source file, as well as any container metadata (such as title and album).

You can cancel paste actions by selecting the cancel (**X**) button:

:::image type="content" source="images/pt-advanced-paste-cancel.png" alt-text="A screenshot of the PowerToys Advanced Paste window processing a transcode operation with an available cancel button.":::

This cancel option is useful for media transcoding but also for other potentially long-running actions, such as the Paste with AI operations.

Paste actions for transcoding display their fractional progress through a progress ring. This feature might be useful for other paste actions in the future, but for now, only media transcoding uses it.

The feature uses the [Windows.Media.Transcoding](/uwp/api/windows.media.transcoding) APIs to transcode audio and video files. You can find the list of supported codecs [here](/windows/uwp/audio-video-camera/supported-codecs).

> [!NOTE]
> The Transcode to audio and video features run locally and don't use AI.

### Transcode to .mp3

The Transcode to .mp3 feature works with both audio and video files. It extracts the audio channel from the media on the clipboard and saves it as an .mp3 file.

:::image type="content" source="images/pt-advanced-paste-transcode.png" alt-text="A screenshot of the PowerToys Advanced Paste window with the Transcode to .mp3 and Transcode to .mp4 (H.264/AAC) options enabled.":::

Use this feature to extract audio from combined audio and video files to save disk space and to work with audio-only apps and devices.

### Transcode to .mp4 (H.264/AAC)

The Transcode to .mp4 (H.264/AAC) feature transcodes video files to use the H.264 video codec and AAC audio codec (if audio is present) and saves the streams to an .mp4 file. Use this feature to transcode existing video files to a more widely supported format.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
