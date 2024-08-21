---
title: How to add OpenAI chat completions to your WinUI 3 / Windows App SDK desktop app
description: Get started with WinUI 3 / Windows App SDK by integrating OpenAI's chat completions API into your WinUI 3 / Windows App SDK desktop app. 
ms.topic: article
ms.date: 12/11/2023
keywords: windows app sdk, winappsdk, winui3, openai, chatgpt
ms.localizationpriority: medium
ms.custom: template-quickstart
audience: new-desktop-app-developers
content-type: how-to
---

# Add OpenAI chat completions to your WinUI 3 / Windows App SDK desktop app

In this how-to, you'll learn how to integrate OpenAI's API into your WinUI 3 / Windows App SDK desktop app. We'll build a chat-like interface that lets you generate responses to messages using OpenAI's [chat completions API](https://platform.openai.com/docs/guides/text-generation/chat-completions-api):

:::image type="content" source="images/chatgpt-openai/long-story.png" alt-text="A less minimal chat app.":::

<!--todo: The source code for the app we're building in this how-to is available todo -->

## Prerequisites

- Set up your development computer (see [Get started with WinUI](../get-started/start-here.md)).
- Familiarity with the core concepts in *[How to build a Hello World app using C# and WinUI 3 / Windows App SDK](./hello-world-winui3.md)* - we'll build upon that how-to in this one.
- An OpenAI API key from your [OpenAI developer dashboard](https://platform.openai.com/api-keys).
- An OpenAI SDK installed in your project. Refer to the [OpenAI documentation](https://platform.openai.com/docs/libraries) for a list of community libraries. In this how-to, we'll use [betalgo/openai](https://github.com/betalgo/openai).


## Create a project

 1. Open Visual Studio and create a new project via `File` > `New` > `Project`.
 2. Search for `WinUI` and select the `Blank App, Packaged (WinUI 3 in Desktop)` C# project template.
 3. Specify a project name, solution name, and directory. In this example, our `ChatGPT_WinUI3` project belongs to a `ChatGPT_WinUI3` solution, which will be created in `C:\Projects\`.

After creating your project, you should see the following default file structure in your Solution Explorer:

:::image type="content" source="images/chatgpt-openai/collapsed-file-structure-chatgpt.png" alt-text="The default directory structure.":::


## Set your environment variable

In order to use the OpenAI SDK, you'll need to set an environment variable with your API key. In this example, we'll use the `OPENAI_API_KEY` environment variable. Once you have your API key from the [OpenAI developer dashboard](https://platform.openai.com/api-keys), you can set the environment variable from the command line as follows:

```powershell
setx OPENAI_API_KEY <your-api-key>
```

Note that this method works well for development, but you'll want to use a more secure method for production apps (for example: you could store your API key in a secure key vault that a remote service can access on behalf of your app). See [Best practices for OpenAI key safety](https://help.openai.com/articles/5112595-best-practices-for-api-key-safety).


## Install the OpenAI SDK

From Visual Studio's `View` menu, select `Terminal`. You should see an instance of `Developer Powershell` appear. Run the following command from your project's root directory to install the SDK:

```powershell
dotnet add package Betalgo.OpenAI
```


## Initialize the SDK

In `MainWindow.xaml.cs`, initialize the SDK with your API key:

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


## Build the chat UI

We'll use a `StackPanel` to display a list of messages, and a `TextBox` to let users enter new messages. Update `MainWindow.xaml` as follows:


```xml MainWindow.xaml
<Window
    x:Class="ChatGPT_WinUI3.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:ChatGPT_WinUI3"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">
    <Grid>
        <StackPanel Orientation="Vertical" HorizontalAlignment="Stretch">
            <ListView x:Name="ConversationList" />
            <StackPanel Orientation="Horizontal">
                <TextBox x:Name="InputTextBox" HorizontalAlignment="Stretch"/>
                <Button x:Name="SendButton" Content="Send" Click="SendButton_Click"/>
            </StackPanel>
        </StackPanel>
    </Grid>
</Window>
```

## Implement message sending, receiving, and displaying

Add a `SendButton_Click` event handler to handle the sending, receiving, and display of messages:


```csharp MainWindow.xaml.cs
public sealed partial class MainWindow : Window
{
    // ...

    private async void SendButton_Click(object sender, RoutedEventArgs e)
    {
        string userInput = InputTextBox.Text;
        if (!string.IsNullOrEmpty(userInput))
        {
            AddMessageToConversation($"User: {userInput}");
            InputTextBox.Text = string.Empty;
            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser(userInput)
                },
                Model = Models.Gpt_4_1106_preview,
                MaxTokens = 300
            });

            if (completionResult != null && completionResult.Successful) {
                AddMessageToConversation("GPT: " + completionResult.Choices.First().Message.Content);
            } else {
                AddMessageToConversation("GPT: Sorry, something bad happened: " + completionResult.Error?.Message);
            }
        }
    }

    private void AddMessageToConversation(string message)
    {
        ConversationList.Items.Add(message);
        ConversationList.ScrollIntoView(ConversationList.Items[ConversationList.Items.Last()]);
    }
}
```


## Run the app

Run the app and try chatting! You should see something like this:

:::image type="content" source="images/chatgpt-openai/hello-gpt.png" alt-text="A minimal chat app.":::


## Improve the chat interface

Let's make the following improvements to the chat interface:

- Add a `ScrollViewer` to the `StackPanel` to enable scrolling.
- Add a `TextBlock` to display the GPT response in a way that's more visually distinct from the user's input.
- Add a `ProgressBar` to indicate when the app is waiting for a response from the GPT API.
- Center the `StackPanel` in the window, similar to ChatGPT's [web interface](https://chatgpt.com/).
- Ensure that messages wrap to the next line when they reach the edge of the window.
- Make the `TextBox` larger and responsive to the `Enter` key.

Starting from the top:

### Add `ScrollViewer`

Wrap the `ListView` in a `ScrollViewer` to enable vertical scrolling on long conversations:

```xml MainWindow.xaml
        <StackPanel Orientation="Vertical" HorizontalAlignment="Stretch">
            <ScrollViewer x:Name="ConversationScrollViewer" VerticalScrollBarVisibility="Auto" MaxHeight="500">
                <ListView x:Name="ConversationList" />
            </ScrollViewer>
            <!-- ... -->
        </StackPanel>
```

### Use `TextBlock`

Modify the `AddMessageToConversation` method to style the user's input and the GPT response differently:

```csharp MainWindow.xaml.cs
    // ...
    private void AddMessageToConversation(string message)
    {
        var messageBlock = new TextBlock();
        messageBlock.Text = message;
        messageBlock.Margin = new Thickness(5);
        if (message.StartsWith("User:"))
        {
            messageBlock.Foreground = new SolidColorBrush(Colors.LightBlue);
        }
        else
        {
            messageBlock.Foreground = new SolidColorBrush(Colors.LightGreen);
        }
        ConversationList.Items.Add(messageBlock);
        ConversationList.ScrollIntoView(ConversationList.Items.Last()); 
    }
```

### Add `ProgressBar`

To indicate when the app is waiting for a response, add a `ProgressBar` to the `StackPanel`:

```xml MainWindow.xaml
        <StackPanel Orientation="Vertical" HorizontalAlignment="Stretch">
            <ScrollViewer x:Name="ConversationScrollViewer" VerticalScrollBarVisibility="Auto" MaxHeight="500">
                <ListView x:Name="ConversationList" />
            </ScrollViewer>
            <ProgressBar x:Name="ResponseProgressBar" Height="5" IsIndeterminate="True" Visibility="Collapsed"/> <!-- new! -->
        </StackPanel>
```

Then, update the `SendButton_Click` event handler to show the `ProgressBar` while waiting for a response:

```csharp MainWindow.xaml.cs
    private async void SendButton_Click(object sender, RoutedEventArgs e)
    {
        ResponseProgressBar.Visibility = Visibility.Visible; // new!

        string userInput = InputTextBox.Text;
        if (!string.IsNullOrEmpty(userInput))
        {
            AddMessageToConversation("User: " + userInput);
            InputTextBox.Text = string.Empty;
            var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = userInput,
                Model = Models.TextDavinciV3
            });

            if (completionResult != null && completionResult.Successful) {
                AddMessageToConversation("GPT: " + completionResult.Choices.First().Text);
            } else {
                AddMessageToConversation("GPT: Sorry, something bad happened: " + completionResult.Error?.Message);
            }
        }
        ResponseProgressBar.Visibility = Visibility.Collapsed; // new!
    }
```


### Center the `StackPanel`

To center the `StackPanel` and pull the messages down towards the `TextBox`, adjust the `Grid` settings in `MainWindow.xaml`:

```xml MainWindow.xaml
    <Grid VerticalAlignment="Bottom" HorizontalAlignment="Center">
        <!-- ... -->
    </Grid>
```

### Wrap messages

To ensure that messages wrap to the next line when they reach the edge of the window, update `MainWindow.xaml` to use an `ItemsControl`.

Replace this:

```xml MainWindow.xaml
    <ScrollViewer x:Name="ConversationScrollViewer" VerticalScrollBarVisibility="Auto" MaxHeight="500">
        <ListView x:Name="ConversationList" />
    </ScrollViewer>
```

With this:

```xml MainWindow.xaml
    <ScrollViewer x:Name="ConversationScrollViewer" VerticalScrollBarVisibility="Auto" MaxHeight="500">
        <ItemsControl x:Name="ConversationList" Width="300">
            <ItemsControl.ItemTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding Text}" TextWrapping="Wrap" Margin="5" Foreground="{Binding Color}"/>
                </DataTemplate>
            </ItemsControl.ItemTemplate>
        </ItemsControl>
    </ScrollViewer>
```

We'll then introduce a `MessageItem` class to facilitate binding and coloring:

```csharp MainWindow.xaml.cs
    // ...
    public class MessageItem
    {
        public string Text { get; set; }
        public SolidColorBrush Color { get; set; }
    }
    // ...
```

Finally, update the `AddMessageToConversation` method to use the new `MessageItem` class:

```csharp MainWindow.xaml.cs
    // ...
    private void AddMessageToConversation(string message)
    {
        var messageItem = new MessageItem();
        messageItem.Text = message;
        messageItem.Color = message.StartsWith("User:") ? new SolidColorBrush(Colors.LightBlue) : new SolidColorBrush(Colors.LightGreen);
        ConversationList.Items.Add(messageItem);

        // handle scrolling
        ConversationScrollViewer.UpdateLayout();
        ConversationScrollViewer.ChangeView(null, ConversationScrollViewer.ScrollableHeight, null);
    }
    // ...
```


### Improve the `TextBox`

To make the `TextBox` larger and responsive to the `Enter` key, update `MainWindow.xaml` as follows:

```xml MainWindow.xaml
    <!-- ... -->
    <StackPanel Orientation="Vertical" Width="300">
        <TextBox x:Name="InputTextBox" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" KeyDown="InputTextBox_KeyDown" TextWrapping="Wrap" MinHeight="100" MaxWidth="300"/>
        <Button x:Name="SendButton" Content="Send" Click="SendButton_Click" HorizontalAlignment="Right"/>
    </StackPanel>
    <!-- ... -->
```

Then, add the `InputTextBox_KeyDown` event handler to handle the `Enter` key:

```csharp MainWindow.xaml.cs
    //...
    private void InputTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrWhiteSpace(InputTextBox.Text))
        {
            SendButton_Click(this, new RoutedEventArgs());
        }
    }
    //...
```

<!-- commit d28ead6f96d69c8e8d3b5b5bdf17a0d5 -->

## Run the improved app

Your new-and-improved chat interface should look something like this:

:::image type="content" source="images/chatgpt-openai/long-story.png" alt-text="A less minimal chat app.":::


## Recap

Here's what you accomplished in this how-to:

 1. You added OpenAI's API capabilities to your WinUI 3 / Windows App SDK desktop app by installing a community SDK and initializing it with your API key.
 2. You built a chat-like interface that lets you generate responses to messages using OpenAI's [chat completions API](https://platform.openai.com/docs/guides/text-generation/chat-completions-api).
 3. You improved the chat interface by:
    1. adding a `ScrollViewer`,
    2. using a `TextBlock` to display the GPT response,
    3. adding a `ProgressBar` to indicate when the app is waiting for a response from the GPT API,
    4. centering the `StackPanel` in the window,
    5. ensuring that messages wrap to the next line when they reach the edge of the window, and
    6. making the `TextBox` larger, resizable, and responsive to the `Enter` key.



## Full code files

<!--todo: embed from github -->

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
                            <TextBlock Text="{Binding Text}" TextWrapping="Wrap" Margin="5" Foreground="{Binding Color}"/>
                        </DataTemplate>
                    </ItemsControl.ItemTemplate>
                </ItemsControl>
            </ScrollViewer>
            <ProgressBar x:Name="ResponseProgressBar" Height="5" IsIndeterminate="True" Visibility="Collapsed"/>
            <StackPanel Orientation="Vertical" Width="300">
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

using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;

namespace ChatGPT_WinUI3
{
    public class MessageItem
    {
        public string Text { get; set; }
        public SolidColorBrush Color { get; set; }
    }

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

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ResponseProgressBar.Visibility = Visibility.Visible;

            string userInput = InputTextBox.Text;
            if (!string.IsNullOrEmpty(userInput))
            {
                AddMessageToConversation("User: " + userInput);
                InputTextBox.Text = string.Empty;
                var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
                {
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem("You are a helpful assistant."),
                        ChatMessage.FromUser(userInput)
                    },
                    Model = Models.Gpt_4_1106_preview,
                    MaxTokens = 300
                });

                Console.WriteLine(completionResult.ToString());

                if (completionResult != null && completionResult.Successful)
                {
                    AddMessageToConversation("GPT: " + completionResult.Choices.First().Message.Content);
                }
                else
                {
                    AddMessageToConversation("GPT: Sorry, something bad happened: " + completionResult.Error?.Message);
                }
            }
            ResponseProgressBar.Visibility = Visibility.Collapsed;
        }

        private void AddMessageToConversation(string message)
        {
            var messageItem = new MessageItem();
            messageItem.Text = message;
            messageItem.Color = message.StartsWith("User:") ? new SolidColorBrush(Colors.LightBlue) : new SolidColorBrush(Colors.LightGreen);
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


## Related

- [Sample applications for Windows development](../get-started/samples.md)
- [Windows developer FAQ](../get-started/windows-developer-faq.yml)
- [Windows developer glossary](../get-started/windows-developer-glossary.md)
- [Windows development best practices](../get-started/best-practices.md)
- [How to target multiple platforms with your WinUI 3 app](uno-multiplatform.md)