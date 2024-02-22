---
title: Quickstart - Add DALL-E image generation to your .NET MAUI app for Windows
description: A quickstart to get started with .NET MAUI on Windows by integrating DALL-E image capabilities into your app. 
ms.service: windows
ms.topic: quickstart
ms.date: 02/21/2024
ms.localizationpriority: medium
keywords: windows, desktop development, Windows App SDK, .net maui, openai, dall-e, ai
content-type: how-to
---

# Quickstart: Add DALL-E to your .NET MAUI Windows desktop app

In this quickstart, we'll demonstrate how to integrate DALL-E's image generation capabilities into your .NET MAUI Windows desktop app.

## Prerequisites

- Visual Studio 2022 17.8 or greater, with the .NET Multi-platform App UI workload installed. For more information, see [Installation](/dotnet/maui/get-started/installation).
- A functional .NET MAUI project with OpenAI integration into which this capability will be integrated. See *[Create a recommendation app with .NET MAUI and ChatGPT](tutorial-maui-ai.md)* - we'll demonstrate how to integrate DALL-E into the user interface from this how-to.
- An OpenAI API key from your [OpenAI developer dashboard](https://platform.openai.com/api-keys) assigned to the `openAIKey` variable in your project.
- An [Azure.AI.OpenAI](https://www.nuget.org/packages/Azure.AI.OpenAI/) NuGet package installed in your project. If you've followed along with the .NET MAUI ChatGPT tutorial, you will have this dependency installed and configured.

## What problem will we solve?

You want to add DALL-E's image generation capabilities to your .NET MAUI Windows desktop app to provide users with a rich, interactive experience. They can already use the app to generate text-based recommendations, and you want to add the ability to generate images that visualize an activity in the location they have entered.

## Install and initialize the Azure OpenAI SDK

In this section, we'll install the SDK into the .NET MAUI project and initialize it with your OpenAI API key.

1. If you haven't already installed the `Azure.AI.OpenAI` NuGet package, you can do so by running `dotnet add package Azure.AI.OpenAI -IncludePrerelease` from Visual Studio's terminal window.

1. Once installed, you can initialize the `OpenAIClient` instance from the SDK with your OpenAI API key as follows:

    ```csharp MainPage.xaml.cs
    private OpenAIClient _chatGptClient;
    private Guid _sessionGuid = Guid.Empty;
    private string openAIKey = "MY_OPEN_AI_API_KEY";
    private string openAIEndpoint = null;
    private char[] trimChars = { '\n', '?' };
    
    public MainPage()
    {
        InitializeComponent();
        this.Loaded += MainPage_Loaded;
    }
    
    private void MainPage_Loaded(object sender, EventArgs e)
    {
        _chatGptClient = !string.IsNullOrWhiteSpace(openAIEndpoint)
            ? new OpenAIClient(
                new Uri(openAIEndpoint),
                new AzureKeyCredential(openAIKey))
            : new OpenAIClient(openAIKey);
    }
    ```

1. Replace `MY_OPEN_AI_API_KEY` with your OpenAI API key.

## Modify your app's UI

Next, we'll modify the user interface to include an `Image` control that displays a generated image below the recommendation text.

1. If you are are starting with a new project, copy the XAML for `MainPage.xaml` from the [Create a recommendation app with .NET MAUI and ChatGPT](tutorial-maui-ai.md) tutorial.

1. Add a `StackLayout` containing a `Label` control and a  `CheckBox` control to `MainPage.xaml` below the `LocationEntry` control to allow users to select whether to generate an image:

    ```xml MainPage.xaml
    ...
    <Entry
        x:Name="LocationEntry"
        Placeholder="Enter your location"
        SemanticProperties.Hint="Enter the location for recommendations"
        HorizontalOptions="Center"/>
    
    <!-- Add this markup -->
    <StackLayout Orientation="Horizontal"
                    HorizontalOptions="Center">
        <Label Text="Generate image" VerticalOptions="Center"/>
        <CheckBox x:Name="IncludeImageChk" VerticalOptions="Center"/>
    </StackLayout>
    ...
    ```

1. Add an `Image` control below the `SmallLabel` control to display the generated image:

    ```xml MainPage.xaml
    ...
        <Image x:Name="GeneratedImage"
               WidthRequest="256"
               HeightRequest="256"
               HorizontalOptions="Center"/>
    </VerticalStackLayout>
    ```

## Implement DALL-E image generation

In this section, we'll add a method to handle image generation and call it from the existing `GetRecommendation` method to display the generated image.

1. If you are are starting with a new project, make sure your code in `MainPage.xaml.cs` matches the code from the [Create a recommendation app with .NET MAUI and ChatGPT](tutorial-maui-ai.md) tutorial.

1. Add a method named `GetImageAsync` to handle image generation. The new method will call the OpenAI API to generate an image based on the prompt we'll build in the next step. It returns an `ImageSource` object that is used to display the image in the UI:

    ```csharp MainPage.xaml.cs
    public async Task<ImageSource> GetImageAsync(string prompt)
    {
        Response<ImageGenerations> imageGenerations = await _chatGptClient.GetImageGenerationsAsync(
            new ImageGenerationOptions()
            {
                Prompt = prompt,
                Size = ImageSize.Size256x256,
            });

        // Image Generations responses provide URLs you can use to retrieve requested images
        Uri imageUri = imageGenerations.Value.Data[0].Url;

        return ImageSource.FromUri(imageUri);
    }
    ```

1. Add the following code to the end of the `GetRecommendation` method to conditionally call the `GetImageAsync` method and display the generated image:

    ```csharp MainPage.xaml.cs
    ...
    if (IncludeImageChk.IsChecked)
    {
        var imagePrompt = $"Show some fun things to do in {LocationEntry.Text} when visiting a {recommendationType}.";
        GeneratedImage.Source = await GetImageAsync(imagePrompt);
    }
    ```

    The `imagePrompt` string is built based on the user's location input and the recommendation type selected.

## Run and test

Run your app, enter a valid location, and click one of the recommendation buttons. You should see something like this:

:::image type="content" source="images/maui-dalle-recommendation-with-image.png" alt-text="Image generation demo":::

## How did we solve the problem?

We added DALL-E's image generation capabilities to our .NET MAUI Windows desktop app. Users can now generate images based on the location they enter and the type of recommendation they select. This provides a rich, interactive experience for users and enhances the app's functionality.

## Clean up resources

It's important to make sure your OpenAI account is secure. If you're not planning to use the [OpenAI API](https://platform.openai.com/api-keys) key for any other projects, you should delete it from your OpenAI developer dashboard. You should also set a reasonable spending limit on your OpenAI account to avoid any unexpected charges. You can check your current usage and spending in the OpenAI dashboard on the [Usage](https://platform.openai.com/usage) page.

## Related content

- [.NET MAUI Installation](/dotnet/maui/get-started/installation)
- [Create a recommendation app with .NET MAUI and ChatGPT](tutorial-maui-ai.md)
- [Add DALL-E to your WinUI 3 / Windows App SDK desktop app](../how-tos/dall-e-winui3.md)
- [Create a .NET MAUI app with C# Markup and the Community Toolkit](tutorial-csharp-ui-maui-toolkit.md)
