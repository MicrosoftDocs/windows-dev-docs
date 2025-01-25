---
title: How to add DALL-E image generation to your WinUI app
description: Get started with WinUI 3 / Windows App SDK by integrating DALL-E image capabilities into your desktop app. 
ms.topic: how-to
ms.date: 11/26/2024
keywords: windows app sdk, winappsdk, winui3
ms.localizationpriority: medium
ms.custom: template-quickstart
audience: new-desktop-app-developers
content-type: how-to
#Customer intent: As a Windows developer, I want to learn how to integrate DALL-E image generation capabilities into my WinUI 3 / Windows App SDK desktop app so that I can enhance my app's functionality.
---

# Add DALL-E to your WinUI 3 / Windows App SDK desktop app

In this how-to, we'll integrate DALL-E's image generation capabilities into your WinUI 3 / Windows App SDK desktop app.

## Prerequisites

- Set up your development computer (see [Get started with WinUI](../get-started/start-here.md)).
- A functional chat interface into which this capability will be integrated. See *[How to add OpenAI chat completions to your WinUI 3 / Windows App SDK desktop app](./chatgpt-openai-winui3.md)* - we'll demonstrate how to integrate DALL-E into the chat interface from this how-to.
- An OpenAI API key from your [OpenAI developer dashboard](https://platform.openai.com/api-keys) assigned to the `OPENAI_API_KEY` environment variable.
- An OpenAI SDK installed in your project. Refer to the [OpenAI documentation](https://platform.openai.com/docs/libraries) for a list of community libraries. In this how-to, we'll use the official [OpenAI .NET API library](https://github.com/openai/openai-dotnet).

## Install and initialize the OpenAI SDK

Ensure that the OpenAI .NET library is installed in your project by running `dotnet add package OpenAI` from Visual Studio's terminal window. Initialize the SDK with your OpenAI API key as follows:

```csharp MainWindow.xaml.cs
//...
using OpenAI;
using OpenAI.Chat;

namespace ChatGPT_WinUI3
{
    public sealed partial class MainWindow : Window
    {
        private OpenAIClient openAiService;

        public MainWindow()
        {
            this.InitializeComponent();
           
            var openAiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            openAiService = new(openAiKey);
        }
    }
}
```

## Modify your app's UI

Modify your existing `DateTemplate` in `MainWindow.xaml` to include an `Image` control that displays images within the conversation:

```xml MainWindow.xaml
<!-- ... existing XAML ... -->
<ItemsControl.ItemTemplate>
    <DataTemplate>
        <StackPanel Orientation="Vertical">
            <TextBlock Text="{Binding Text}" TextWrapping="Wrap" Margin="5" Foreground="{Binding Color}"/>
            <Image Source="{Binding ImageUrl}" Margin="5" Stretch="UniformToFill"/>
        </StackPanel>
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
        // Use the DALL-E 3 model for image generation.
        ImageClient imageClient = openAiService.GetImageClient("dall-e-3");

        ClientResult<GeneratedImage> imageResult = await imageClient.GenerateImageAsync(userInput,
            new ImageGenerationOptions
            {
                Size = GeneratedImageSize.W1024xH1024,
                ResponseFormat = GeneratedImageFormat.Uri,
                EndUserId = "TestUser"
            });

        if (imageResult.Value != null)
        {
            AddImageMessageToConversation(imageResult.Value.ImageUri);
        }
        else
        {
            AddMessageToConversation("GPT: Sorry, something bad happened.");
        }
    }
    ResponseProgressBar.Visibility = Visibility.Collapsed;
}

private void AddImageMessageToConversation(Uri imageUrl)
{
    var imageMessage = new MessageItem
    {
        ImageUrl = imageUrl.ToString()
    };
    ConversationList.Items.Add(imageMessage);

    // handle scrolling
    ConversationScrollViewer.UpdateLayout();
    ConversationScrollViewer.ChangeView(null, ConversationScrollViewer.ScrollableHeight, null);
}

```

The `imageClient.GenerateImageAsync()` method is responsible for calling OpenAI's DALL-E API. Refer to the [OpenAI .NET examples on GitHub](https://github.com/openai/openai-dotnet/tree/main/examples/Images) for more usage examples.

> [!TIP]
> Try asking Microsoft Copilot for some suggestions on different ways to use the DALL-E and chat APIs in your app.

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

Run your app, enter a prompt, and click the "Send" button. You should see something like this:

:::image type="content" source="images/dall-e-winui3/cat-laser-eyes.png" alt-text="A screenshot of the WinUI image generation demo app.":::

## Customize the UI on your own

Try adding some radio buttons to the UI to select whether to include an image in the conversation. You can then modify the `SendButton_Click` method to conditionally call the image generation method based on the radio button selection.

## Recap

In this guide, you've learned how to:

1. Accept image prompts from users within a `<TextBox>`.
2. Generate images using the OpenAI DALL-E API.
3. Display the image in an `<Image>`.

## Full code files

The following are the full code files for the chat interface with DALL-E image generation. The code has been updated to use radio buttons to conditionally call chat or image generation as suggested in the [Customize the UI on your own](#customize-the-ui-on-your-own) section above.

```xml MainWindow.xaml
<?xml version="1.0" encoding="utf-8"?>
<Window
    x:Class="ChatGPT_WinUI3.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:ChatGPT_WinUI3"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">
    <Grid VerticalAlignment="Bottom" HorizontalAlignment="Center">
        <StackPanel Orientation="Vertical" HorizontalAlignment="Center">
            <ScrollViewer x:Name="ConversationScrollViewer" VerticalScrollBarVisibility="Auto" MaxHeight="500">
                <ItemsControl x:Name="ConversationList" Width="300">
                    <ItemsControl.ItemTemplate>
                        <DataTemplate>
                            <StackPanel Orientation="Vertical">
                                <TextBlock Text="{Binding Text}" TextWrapping="Wrap" Margin="5" Foreground="{Binding Color}"/>
                                <Image Source="{Binding ImageUrl}" Margin="5" Stretch="UniformToFill"/>
                            </StackPanel>
                        </DataTemplate>
                    </ItemsControl.ItemTemplate>
                </ItemsControl>
            </ScrollViewer>
            <ProgressBar x:Name="ResponseProgressBar" Height="5" IsIndeterminate="True" Visibility="Collapsed"/>
            <StackPanel Orientation="Vertical" Width="300">
                <RadioButtons Header="Query type:">
                    <RadioButton x:Name="chatRadioButton" Content="Chat" IsChecked="True"/>
                    <RadioButton x:Name="imageRadioButton" Content="Image"/>
                </RadioButtons>
                <TextBox x:Name="InputTextBox" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" KeyDown="InputTextBox_KeyDown" TextWrapping="Wrap" MinHeight="100" MaxWidth="300"/>
                <Button x:Name="SendButton" Content="Send" Click="SendButton_Click" HorizontalAlignment="Right"/>
            </StackPanel>
        </StackPanel>
    </Grid>
</Window>

```

```csharp MainWindow.xaml.cs
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OpenAI;
using OpenAI.Chat;
using OpenAI.Images;

namespace ChatGPT_WinUI3
{
    public class MessageItem
    {
        public string Text { get; set; }
        public SolidColorBrush Color { get; set; }
        public string ImageUrl { get; set; }
    }

    public sealed partial class MainWindow : Window
    {
        private OpenAIService openAiService;

        public MainWindow()
        {
            this.InitializeComponent();

            var openAiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            openAiService = new(openAiKey);
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ResponseProgressBar.Visibility = Visibility.Visible;
            string userInput = InputTextBox.Text;

            try
            {
                if (imageRadioButton.IsChecked == true)
                {
                    await ProcessImageRequestAsync(userInput);
                }
                else
                {
                    await ProcessChatRequestAsync(userInput);
                }
            }
            catch (Exception ex)
            {
                AddMessageToConversation($"GPT: Sorry, something bad happened: {ex.Message}");
            }
            finally
            {
                ResponseProgressBar.Visibility = Visibility.Collapsed;
            }
        }

        private async Task ProcessImageRequestAsync(string userInput)
        {
            if (!string.IsNullOrEmpty(userInput))
            {
                InputTextBox.Text = string.Empty;
                // Use the DALL-E 3 model for image generation.
                ImageClient imageClient = openAiService.GetImageClient("dall-e-3");

                ClientResult<GeneratedImage> imageResult = await imageClient.GenerateImageAsync(userInput,
                    new ImageGenerationOptions
                    {
                        Size = GeneratedImageSize.W1024xH1024,
                        ResponseFormat = GeneratedImageFormat.Uri,
                        EndUserId = "TestUser"
                    });

                if (imageResult.Value != null)
                {
                    AddImageMessageToConversation(imageResult.Value.ImageUri);
                }
                else
                {
                    AddMessageToConversation("GPT: Sorry, something bad happened.");
                }
            }
        }

        private async Task ProcessChatRequestAsync(string userInput)
        {
            if (!string.IsNullOrEmpty(userInput))
            {
                AddMessageToConversation($"User: {userInput}");
                InputTextBox.Text = string.Empty;
                var chatClient = openAiService.GetChatClient("gpt-4o");
                var chatOptions = new ChatCompletionOptions
                {
                    MaxOutputTokenCount = 300
                };
                var completionResult = await chatClient.CompleteChatAsync(
                    [
                        ChatMessage.CreateSystemMessage("You are a helpful assistant."),
                        ChatMessage.CreateUserMessage(userInput)
                    ],
                    chatOptions);

                if (completionResult != null && completionResult.Value.Content.Count > 0)
                {
                    AddMessageToConversation($"GPT: {completionResult.Value.Content.First().Text}");
                }
                else
                {
                    AddMessageToConversation($"GPT: Sorry, something bad happened: {completionResult?.Value.Refusal ?? "Unknown error."}");
                }
            }
        }

        private void AddImageMessageToConversation(Uri imageUrl)
        {
            var imageMessage = new MessageItem
            {
                ImageUrl = imageUrl.ToString()
            };
            ConversationList.Items.Add(imageMessage);

            // handle scrolling
            ConversationScrollViewer.UpdateLayout();
            ConversationScrollViewer.ChangeView(null, ConversationScrollViewer.ScrollableHeight, null);
        }

        private void AddMessageToConversation(string message)
        {
            var messageItem = new MessageItem
            {
                Text = message,
                Color = message.StartsWith("User:") ? new SolidColorBrush(Colors.LightBlue)
                                                    : new SolidColorBrush(Colors.LightGreen)
            };
            ConversationList.Items.Add(messageItem);

            // handle scrolling
            ConversationScrollViewer.UpdateLayout();
            ConversationScrollViewer.ChangeView(null, ConversationScrollViewer.ScrollableHeight, null);
        }

        private void InputTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                SendButton_Click(this, new RoutedEventArgs());
            }
        }
    }
}

```

## Related content

- [OpenAI API Documentation](https://platform.openai.com/docs/)
- [Windows App SDK Samples](../get-started/samples.md)
- [OpenAI .NET library on GitHub](https://github.com/openai/openai-dotnet)
