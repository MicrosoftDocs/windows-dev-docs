---
title: Stable channel release notes for the Windows App SDK 
description: Provides information about the stable release channel for the Windows App SDK.
ms.topic: release-notes
ms.date: 09/09/2025
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Latest stable channel release notes for the Windows App SDK

The stable channel provides releases of the Windows App SDK that are supported for use by apps in production environments. Apps that use the stable release of the Windows App SDK can also be published to the Microsoft Store.

**Important links:**

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).

## Downloads for the Windows App SDK

> [!NOTE]
> The Windows App SDK Visual Studio Extensions (VSIX) are no longer distributed as a separate download. They are available in the Visual Studio Marketplace inside Visual Studio.

## Version 1.8

In an existing Windows App SDK app, you can update your Nuget package to 1.8.250907003 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

### Version 1.8.0 (1.8.250907003)

This is the latest service release for Version 1.8 of the Windows App SDK.

#### Windows AI APIs

The Windows App SDK now includes a suite of artificial intelligence (AI) APIs that can be used with a local language model to perform a variety of tasks on Copilot+ PCs. Your apps can now intelligently respond to prompts, recognize text within images, describe the content of images, remove objects from images, and more.

For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.

#### Windows AI Prompt Size Limit Reporting
Allows applications to determine if an input exceeds the allowable size for a Text Summarizer call. If the input is too large, the API returns an index indicating the current limit, enabling developers to adjust the input accordingly. This limit is based on token count rather than byte or character length, and it can vary over time due to multiple factors. Therefore, applications should treat the limit as dynamic and subject to change.

#### Windows AI Text Rewriter Tone
Enables text rewriting with specific tones. The Casual option rephrases content to sound more informal and conversational, using natural, spontaneous phrasing while preserving meaning and format. The Formal option transforms text into a polished, professional version, maintaining the original structure and details with precise language suitable for formal context. The General option retains the original tone and intent, ensuring the meaning remains unchanged.

#### Text Intelligence - Conversation Summary 
Phi Silica now has a Summarize Conversation feature that allows you to summarize what people have said over an email, chat, or thread. See [Phi Silica](/windows/ai/apis/phi-silica) for more details.

#### Conversation Summary Options
Enables developers to specify the desired output language for conversation summarization. This allows applications to generate summaries in a targeted language, enhancing localization, and user experience.

#### Windows AI Object Erase

Object Erase can be used to remove objects from images. The model takes both an image and a greyscale mask indicating the object to be removed, erases the masked area from the image, and replaces the erased area with the image background.

#### Decimal DataType

The new `Decimal` support offers a high-precision base-10 numeric data type that is invaluable for financial and scientific calculations, avoiding imprecision and rounding errors inherent to floating-point data types. It is structured as a 96-bit (12-byte) unsigned integer, scaled by a variable power of 10, allowing for precise representation of decimal values. This enables decimal support for programming languages lacking decimal data types and provides interoperability with languages that do support decimal (e.g. C#, Python).

#### NuGet Metapackage

The Windows App SDK NuGet package has been converted to a NuGet metapackage. Each component contributing to the Windows App SDK is now a component NuGet package and is listed as a dependency by the metapackage. This allows developers to choose either the metapackage or select specific component packages for their applications. The use of individual component packages enables developers to include only the APIs and functionalities that are necessary for their apps. The default experience behaves as if `WindowsAppSDKSelfContained` had been set as True, but the `Microsoft.WindowsAppSDK.Runtime` package can be referenced to use framework package deployment.

#### Microsoft.Windows.SDK.BuildTools.MSIX Refactor

The MSIX publishing support has been factored into a standalone nuget package, which can be independently maintained and consumed by Windows App SDK and other projects.  In addition, several feature gaps with Single-Project solutions have been addressed including generation of MSIX bundles and MSIX upload packages.

#### Storage Pickers

The Microsoft.Windows.Storage.Pickers API in the Windows App SDK provides a modernized file and folder picker experience for desktop applications. This API is based on the existing Windows.Storage.Pickers API design, but with key improvements for desktop scenarios. The new Microsoft.Windows.Storage.Pickers API addresses two critical limitations of the UWP file and folder pickers on Apps developed with WinAppSDK/WinUI 3:
- Elevated Process Support: The existing Windows.Storage.Pickers APIs do not work when the application is running as an administrator. The new API enables file and folder selection in elevated mode.
- Simplified Usage in WinUI 3: Using the existing UWP pickers in WinUI 3 requires initializing a window handle for window association. The new pickers eliminate this requirement by accepting a WindowId directly in the constructor, making them easier to use.

#### Other notable changes
- Prior to WinAppSDK 1.8, packaged apps running in the AppContainer did not require the packageManagement capability, due to a DeploymentManager auto-initialization issue.  That issue has now been resolved, and in turn, the packageManagement capability is now required for AppContainer-based apps.
- The experimental WinML APIs have been removed from this release and will be included in a future release.

#### Bug Fixes
- Fixed an issue where the hover effects of other windows for the app could flicker when at least one window had ExtendsContentIntoTitleBar set to true.
- NavigationView: Fixed a bug where setting SelectedItem as null did not correctly clear the selection state in collapsed mode.
- TabView: Fixed an issue where closing a tab would move keyboard focus to the “Add tab” button instead of the newly selected tab.
- SplitButton: Fixed UI inconsistency where the SplitButton control appeared shorter than standard Button controls
- TabView: Fixed issue TabView spacing in WinUI, When setting the TabWidthMode property of a TabView to SizeToContent, the padding between the header text and the left/right edges of the tab becomes uneven

### New APIs for 1.8.0
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
Microsoft.Windows.Foundation
 
    DecimalContract
    DecimalHelper
    DecimalValue
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

### New APIs compared to 1.8-Preview1
```
Microsoft.Windows.AI.Text
 
    TextRewriteTone
        Concise
```
```
Microsoft.Windows.Foundation
 
    DecimalContract
    DecimalHelper
    DecimalValue
```


</details>

## Archive of stable channel release notes

<details>

<summary>Expand for links to archived experimental channel release notes</summary>

- [Stable channel release notes for the Windows App SDK 1.7](release-notes-archive/stable-channel-1-7.md)
- [Stable channel release notes for the Windows App SDK 1.6](release-notes-archive/stable-channel-1.6.md)
- [Stable channel release notes for the Windows App SDK 1.5](release-notes-archive/stable-channel-1.5.md)
- [Stable channel release notes for the Windows App SDK 1.4](release-notes-archive/stable-channel-1.4.md)
- [Stable channel release notes for the Windows App SDK 1.3](release-notes-archive/stable-channel-1.3.md)
- [Stable channel release notes for the Windows App SDK 1.2](release-notes-archive/stable-channel-1.2.md)
- [Stable channel release notes for the Windows App SDK 1.1](release-notes-archive/stable-channel-1.1.md)
- [Stable channel release notes for the Windows App SDK 1.0](release-notes-archive/stable-channel-1.0.md)
- [Stable channel release notes for the Windows App SDK 0.8](release-notes-archive/stable-channel-0.8.md)
- [Stable channel release notes for the Windows App SDK 0.5](release-notes-archive/stable-channel-0.5.md)

</details>

## Related topics

- [Preview channel](preview-channel.md)
- [Experimental channel](experimental-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)
