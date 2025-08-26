---
title: Preview release channel for the Windows App SDK 
description: Provides info about the preview release channel for the Windows App SDK.
ms.topic: article
ms.date: 08/19/2025
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Latest preview channel release notes for the Windows App SDK

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store.

The preview channel includes releases of the Windows App SDK with [preview channel features](release-channels.md#features-available-by-release-channel) in late stages of development. Preview releases do not include experimental features and APIs but may still be subject to breaking changes before the next stable release.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).
- For documentation on preview releases, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md).

## Version 1.8 Preview (1.8-preview)

This is the latest release of the preview channel for version 1.8.

In an existing Windows App SDK 1.7 (from the stable channel) app, you can update your Nuget package to 1.8.0-preview (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

### New features

#### Prompt Size Limit Reporting
 
Allows applications to determine if an input exceeds the allowable size for a Text Summarizer call. If the input is too large, the API returns an index indicating the current limit, enabling developers to adjust the input accordingly. This limit is based on token count rather than byte or character length, and it can vary over time due to multiple factors. Therefore, applications should treat the limit as dynamic and subject to change.
 
#### Text Rewriter Tone
 
Enables text rewriting with specific tones. The Casual option rephrases content to sound more informal and conversational, using natural, spontaneous phrasing while preserving meaning and format. The Formal option transforms text into a polished, professional version, maintaining the original structure and details with precise language suitable for formal context. The General option retains the original tone and intent, ensuring the meaning remains unchanged.
 
#### Conversation Summary Options
 
Enables developers to specify the desired output language for conversation summarization. This allows applications to generate summaries in a targeted language, enhancing localization, and user experience.

#### Other notable changes
 
* Prior to WinAppSDK 1.8, packaged apps running in the AppContainer did not require the packageManagement capability, due to a DeploymentManager auto-initialization issue.  That issue has now been resolved, and in turn, the packageManagement capability is now required for AppContainer-based apps.
 
### New APIs
 
This release includes the following new APIs compared to the stable 1.7 release:
 
```
Microsoft.Windows.AI.Foundation
 
    AIFoundationContract
    EmbeddingVector
```
```
Microsoft.Windows.AI.Imaging
 
    ImageObjectRemover
    ImageObjectRemoverContract
```
```
Microsoft.Windows.AI.Text
 
    ConversationItem
    ConversationSummaryOptions
    InputKind
    LanguageModel
        CreateContext
        CreateContext
        CreateContext
        GenerateEmbeddingVectors
        GenerateEmbeddingVectors
        GenerateResponseAsync
        GenerateResponseAsync
        GenerateResponseAsync
        GenerateResponseFromEmbeddingsAsync
        GenerateResponseFromEmbeddingsAsync
        GenerateResponseFromEmbeddingsAsync
        GetUsablePromptLength
        GetUsablePromptLength
        GetVectorSpaceId

    LanguageModelEmbeddingVectorResult
    TextRewriter
        RewriteAsync
 
    TextRewriteTone
    TextSummarizer
        IsPromptLargerThanContext
        IsPromptLargerThanContext
        SummarizeConversationAsync
```
```
Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
 
    Task
        Run
```
```
Microsoft.Windows.Storage.Pickers
 
    FileOpenPicker
    FileSavePicker
    FolderPicker
    PickerLocationId
    PickerViewMode
    PickFileResult
    PickFolderResult
    StoragePickersContract
```
```
Microsoft.Windows.Widgets.Feeds.Providers
 
    FeedManager
        TryRemoveAnnouncementById
 
    IFeedManager3
```

### New APIs compared to 1.8-exp4
```
Microsoft.Windows.AI.Text
 
    TextSummarizer
        IsPromptLargerThanContext
 ```
```
Microsoft.Windows.Storage.Pickers
 
    FileSavePicker
        SuggestedFolder
 
    StoragePickersContract
```

### Known Issues

* Standalone use of component packages (such as Microsoft.WindowsAppSDK.WinUI) will require an app-level package reference to the latest Microsoft.Windows.SDK.BuildTools.MSIX, to address an issue with some wapproj-based solutions breaking due to a "WinAppSdkExpandPriContent" task not found error.  Referencing the full top-level Microsoft.WindowsAppSDK package (the common scenario) does not require this.

## Archive of preview channel release notes

<details>

<summary>Expand for links to archived preview channel release notes</summary>

- [Preview channel release notes for the Windows App SDK 1.7](release-notes-archive/preview-channel-1-7.md)
- [Preview channel release notes for the Windows App SDK 1.6](release-notes-archive/preview-channel-1.6.md)
- [Preview channel release notes for the Windows App SDK 1.5](release-notes-archive/preview-channel-1.5.md)
- [Preview channel release notes for the Windows App SDK 1.4](release-notes-archive/preview-channel-1.4.md)
- [Preview channel release notes for the Windows App SDK 1.3](release-notes-archive/preview-channel-1.3.md)
- [Preview channel release notes for the Windows App SDK 1.2](release-notes-archive/preview-channel-1.2.md)
- [Preview channel release notes for the Windows App SDK 1.1](release-notes-archive/preview-channel-1.1.md)
- [Preview channel release notes for the Windows App SDK 1.0](release-notes-archive/preview-channel-1.0.md)

</details>

## Related topics

- [Stable channel](stable-channel.md)
- [Experimental channel](experimental-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)

