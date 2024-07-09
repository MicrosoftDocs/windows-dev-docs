---
title: How to add DALL-E image generation to your WinUI 3 / Windows App SDK app
description: Get started with WinUI 3 / Windows App SDK by integrating DALL-E image capabilities into your app. 
ms.topic: article
ms.date: 1/12/2024
keywords: windows app sdk, winappsdk, winui3
ms.author: mikben
author: matchamatch
ms.localizationpriority: medium
ms.custom: template-quickstart
audience: new-desktop-app-developers
content-type: how-to
---

# Add DALL-E to your WinUI 3 / Windows App SDK desktop app

In this how-to, we'll integrate DALL-E's image generation capabilities into your WinUI 3 / Windows App SDK desktop app.

## Prerequisites

- Set up your development computer (see [Get started with WinUI](../get-started/start-here.md)).
- A functional chat interface into which this capability will be integrated. See *[How to add OpenAI chat completions to your WinUI 3 / Windows App SDK desktop app](./chatgpt-openai-winui3.md)* - we'll demonstrate how to integrate DALL-E into the chat interface from this how-to.
- An OpenAI API key from your [OpenAI developer dashboard](https://platform.openai.com/api-keys) assigned to the `OPENAI_API_KEY` environment variable.
- An OpenAI SDK installed in your project. Refer to the [OpenAI documentation](https://platform.openai.com/docs/libraries) for a list of community libraries. In this how-to, we'll use [betalgo/openai](https://github.com/betalgo/openai).

## Install and initialize the OpenAI SDK

Ensure that the `betalgo/OpenAI SDK` is installed in your project by running `dotnet add package Betalgo.OpenAI` from Visual Studio's terminal window. Initialize the SDK with your OpenAI API key as follows:

```csharp MainWindow.xaml.cs
//...
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;

namespace ChatGPT_WinUI3
{
    public sealed partial class MainWindow : Window
    {
        private OpenAIService openAiService;

        public MainWindow()
        {
            this.InitializeComponent();
           
            var openAiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            openAiService = new OpenAIService(new OpenAiOptions(){
                ApiKey = openAiKey
            });
        }
    }
}
```

## Modify your app's UI

Modify your existing `MainWindow.xaml` to include an `Image` control that displays images within the conversation:


```xml MainWindow.xaml
<!-- ... existing XAML ... -->
<ItemsControl.ItemTemplate>
    <DataTemplate>
        <Image Source="{Binding ImageUrl}" Margin="5" Stretch="UniformToFill"/>
    </DataTemplate>
</ItemsControl.ItemTemplate>
<!-- ... existing XAML ... -->
```

Note that this how-to assumes you have a chat interface with a `TextBox` and `Button`; see *[How to add OpenAI chat completions to your WinUI 3 / Windows App SDK desktop app](./chatgpt-openai-winui3.md)*.


## Implement DALL-E image generation

In your `MainWindow.xaml.cs`, add the following methods to handle image generation and display:

```csharp MainWindow.xaml.cs
// ... existing using statements ...

private async void SendButton_Click(object sender, RoutedEventArgs e)
{
    ResponseProgressBar.Visibility = Visibility.Visible;
    string userInput = InputTextBox.Text;
    if (!string.IsNullOrEmpty(userInput))
    {
        InputTextBox.Text = string.Empty;
        var imageResult = await openAiService.Image.CreateImage(new ImageCreateRequest
        {
            Prompt = userInput,
            N = 2,
            Size = StaticValues.ImageStatics.Size.Size256, // StaticValues is available as part of the Betalgo OpenAI SDK
            ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
            User = "TestUser"
        });

        if (imageResult.Successful)
        {
            foreach (var imageUrl in imageResult.Results.Select(r => r.Url))
            {
                AddImageMessageToConversation(imageUrl);
            }
        }
        else
        {
            AddMessageToConversation("GPT: Sorry, something bad happened: " + imageResult.Error?.Message);
        }
    }
    ResponseProgressBar.Visibility = Visibility.Collapsed;
}

private void AddImageMessageToConversation(string imageUrl)
{
    var imageMessage = new MessageItem
    {
        ImageUrl = imageUrl
    };
    ConversationList.Items.Add(imageMessage);
}

```

The `openAiService.Image.CreateImage()` method is responsible for calling OpenAI's DALL-E API. Refer to the [Betalgo OpenAI SDK wiki](https://github.com/betalgo/openai/wiki/Dall-E) for more usage examples.

Note the presence of `ImageUrl` in the `MessageItem` class. This is a new property:

```csharp MainWindow.xaml.cs
public class MessageItem
{
    public string Text { get; set; }
    public SolidColorBrush Color { get; set; }
    public string ImageUrl { get; set; } // new
}
```


## Run and test

Run your app, enter a prompt, and click the "Generate Image" button. You should see something like this:

:::image type="content" source="images/dall-e-winui3/cat-laser-eyes.png" alt-text="Image generation demo":::


## Recap

In this guide, you've learned how to:

1. Accept image prompts from users within a `<TextBox>`.
2. Generate images using the OpenAI DALL-E API.
3. Display the image in an `<Image>`.

<!-- todo: pointer to sample, or full code files -->


## Related

- [OpenAI API Documentation](https://platform.openai.com/docs/)
- [Windows App SDK Samples](../get-started/samples.md)
- [Betalgo/OpenAI SDK](https://github.com/betalgo/openai)