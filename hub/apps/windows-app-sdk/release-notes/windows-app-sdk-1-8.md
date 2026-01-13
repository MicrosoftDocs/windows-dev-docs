---
title: Windows App SDK 1.8 release notes
description: Provides information about what's new in Windows App SDK 1.8.
ms.topic: release-notes
ms.date: 09/22/2025
keywords: windows win32, windows app development, Windows App SDK, release notes
ms.localizationpriority: medium
zone_pivot_groups: wasdk-release-channels
---

# Windows App SDK 1.8 release notes

[!INCLUDE [wasdk-releasenotes](../../../includes/wasdk-release-notes.md)]

:::zone pivot="stable"



## Version 1.8.4 (1.8.260101001)

Released: **January 13, 2026** <br><br>

<details><summary>Windows AI Text Rewriter: New Custom tones</summary>

>
> The **TextRewriter** supports new custom tones based on user-provided instructions. The new **RewriteCustomAsync** API lets you provide an input string that guides Phi Silica in rewriting selected text. You can experiment with new custom tones based on user-provided instructions to transform your content as desired. Try out changes like "Rewrite as Shakespeare" or "Rewrite in Sci fi".
>
</details>

<details><summary>Windows ML size reduction</summary>

>
> Reduced the size of `Microsoft.Windows.AI.MachineLearning.dll` by approximately 160 KB.
>
</details>

<details><summary>Bug fixes</summary>

>
> * Fixed "Class not registered" errors when using Windows ML in self-contained deployments. Developers using self-contained deployment no longer need to register all the Foundation package activatable classes that were used internally.
> * Fixed a crash  that occurs during process shutdown after using Windows ML.
> * Improved `FileOpenPicker`/`FileSavePicker` behavior:
>     * Filter names display correctly when extensions are hidden. For more info, see GitHub issue [#5837](https://github.com/microsoft/WindowsAppSDK/issues/5837). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): StoragePickers_DisplayFileTypeFilterNames)
>     * Existing files are not truncated on save unless overwritten. For more info, see GitHub issue [#5976](https://github.com/microsoft/WindowsAppSDK/issues/5976). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): StoragePickers_DoNotTruncateExistingFileOnSave)
>     * File type choices preserve insertion order. For more info, see GitHub issue [#5827](https://github.com/microsoft/WindowsAppSDK/issues/5827). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): StoragePickers_PreserveFileTypeChoicesInsertionOrder)
> * Fixed an issue that prevented Image Super Resolution from being available for some applications.
> * Fixed problem with apps not launching when using `PublishSingleFile` support with component packages. For more info, see GitHub issue [#5969](https://github.com/microsoft/WindowsAppSDK/issues/5969#issuecomment-3551259519). [RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, build .targets change)
> * Fixed an issue where IconElements created using IconSource.CreateIconElement were not rendered on the screen. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FixCreateIconElementRendering)
> * Fixed issue with incremental builds rebuilding too much when using WinAppSDKSelfContained. [RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, build .targets change)
> * Fixed build failure when referencing DWrite component package with WinAppSDKSelfContained. [RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, build .targets change)
</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new APIs compared to the 1.8.3 release:
> ```
> Microsoft.Windows.AI.Text
> 
>     TextRewriter
>         RewriteCustomAsync
> ```
</details>


## Version 1.8.3 (1.8.251106002)

Released: **December 8, 2025** <br><br>

<details><summary>Windows ML Updates</summary>

>
> **Expanded OS Support**
> Windows ML now supports Windows 10 (version 1809 and later) and Windows Server 2019 and later for CPU and GPU workloads with select execution providers.
Learn more:  [Supported Execution Providers](/windows/ai/new-windows-ml/supported-execution-providers)

> **AMD MiGraphX Execution Provider**
> Added support for the AMD MiGraphX execution provider, enabling ML workloads on AMD GPUs for the latest Ryzen AI 300-series processors. This provider is currently being flighted in Windows Insider Program channels and is targeted for retail availability by the end of the month. Learn more: [Supported Execution Providers](/windows/ai/new-windows-ml/supported-execution-providers)

> **Windows ML Model Catalog APIs**
> These APIs enable your app or library to dynamically discover and download large AI model files from your own online model catalogs, and share them across apps on the PC—without bundling those large files directly with your app or library. [See the docs](/windows/ai/new-windows-ml/model-catalog/overview) to learn how to use these APIs!
</details>

<details><summary>New APIs for 1.8.3</summary>

>
> This release includes the following new APIs compared to the previous 1.8 release:
>
> ```
> Microsoft.Windows.AI.MachineLearning
>
>     CatalogModelInfo
>     CatalogModelInstance
>     CatalogModelInstanceResult
>     CatalogModelInstanceStatus
>     CatalogModelStatus
>     ModelCatalog
>     ModelCatalogSource
> ```

</details>

<details><summary>Bug fixes</summary>

> * Fixed a potential crash if OrientedVirtualizingPanel hits an overflow when computing bounds. (RuntimeCompatibilityChange: OrientedVirtualizingPanel_FixBoundsOverflow).
>

</details>

---

## Version 1.8.2 (1.8.251003001)

Released: **October 14, 2025** <br><br>

<details><summary>Updated ONNX Runtime</summary>

> Updated the `onnxruntime.dll` to 1.23.1 introducing several enhancements to ONNX Runtime's Python and C++ APIs, focusing on improved device and memory information handling, synchronization stream support, and tensor copy functionality. It adds new Python bindings for device/memory types, exposes more detailed session input/output metadata, and provides a Python-accessible tensor copy API. The changes also refactor and extend the C++ API for better stream and memory info management.
>
> **Key enhancements include:**
> * Python bindings for `OrtMemoryInfoDeviceType`, `OrtDeviceMemoryType`, and expanded `OrtDevice` to expose the memory type via a new `mem_type` method. The `OrtMemoryInfo` Python class now supports both legacy and new V2 constructors and exposes additional properties such as device memory type and Vendor ID.
> * Extended the Python `InferenceSession` object to provide access to imput/output `OrtMemoryInfo` and `OrtEpDevice` objects through new properties and methods
> * Introduced Python bindings for `OrtSyncStream`, including creation via `OrtEpDevice.create_sync_stream()` and retrieval of device-specific `OrtMemoryInfo` via `OrtEpDevice.memory_info()`.
> * Refactored the C++ API to generalize `SyncStream` handling, allowing for unowned streams and improved type safety.
> * Added a new Python-level `copy_tensors` function and corresponding C++ binding, enabling efficient copying of tensor data between OrtValue objects, optionally using a synchronization stream.
> * Changed the return type of the `OrtValue.data_ptr` method in the Python binding from `int64_t` to `uintptr_t` for better cross-platform compatibility.
> * Minor improvements to error messages and device type handling in the Python API (e.g., for OrtDevice).
> * Addressed edge cases in memory information handling
> * Resolved minor issues to improve stability and reliability
>

</details>

<details><summary>Bug fixes</summary>

> * Fixed deployment handler code to report the actual failure HRESULT for increased clarity when troubleshooting.
>

</details>

---

## Version 1.8.1 (1.8.250916003)

Released: **September 23, 2025** <br><br>

<details><summary>LanguageModel text generation</summary>

>  
> LanguageModel is now available using [**Phi Silica**](/windows/ai/apis/phi-silica) to generate text responses to broad user prompts with built in content moderation. Phi Silica, Microsoft's most powerful NPU-tuned local language model, is optimized for efficiency and performance on Windows Copilot+ PCs devices while still offering many of the capabilities found in Large Language Models (LLMs).
>  
> See [Get started with Phi Silica in the Windows App SDK](/windows/ai/apis/phi-silica) and [API ref for Phi Silica in the Windows App SDK](/windows/ai/apis/phi-silica-api-ref) for more information.
>

</details>
<details><summary>Microsoft Windows ML</summary>

>
> [Windows ML](/windows/ai/new-windows-ml/overview) enables developers to run ONNX AI models locally on Windows PCs on a shared system-wide copy of the ONNX Runtime using dynamically-installed hardware-specific execution providers.
>
> **Key benefits:**
>
> - **Dynamically get latest EPs** - Automatically downloads and manages the latest hardware-specific execution providers
> - **Shared ONNX Runtime** - Uses system-wide runtime instead of bundling your own, reducing app size
> - **Smaller downloads/installs** - No need to carry large EPs and the ONNX Runtime in your app
> - **Broad hardware support** - Runs on all Windows 11 PCs (x64 and ARM64) with any hardware configuration
>

</details>
<details><summary>New APIs for 1.8.1</summary>

>
> This release includes the following new APIs compared to the previous 1.8 release:
>
> ```
> Microsoft.Windows.AI.MachineLearning
>
>     ExecutionProvider
>     ExecutionProviderCatalog
>     ExecutionProviderCertification
>     ExecutionProviderReadyResult
>     ExecutionProviderReadyResultState
>     ExecutionProviderReadyState
>     MachineLearningContract
> ```
 > ```
> Microsoft.Windows.AI.Text
>
>     LanguageModel
>         CreateContext
>         GenerateEmbeddingVectors
>         GenerateResponseAsync
>         GenerateResponseFromEmbeddingsAsync
>         GetUsablePromptLength
>         GetVectorSpaceId
> ```
>

</details>
<details><summary>Known issues</summary>

>
> - C# developers must manually reference the  [System.Numerics.Tensors]() version 9.0.0 or greater NuGet package in order to use the `Microsoft.ML.OnnxRuntime.Tensors`. Without this NuGet package reference, you will experience the following runtime error when calling the `Microsoft.ML.OnnxRuntime.Tensors` APIs: `Could not load file or assembly 'System.Numerics.Tensors, Version=9.0.0.0`.
>

</details>

---

## Version 1.8.0 (1.8.250907003)

Released: **September 9, 2025** <br><br>

<details><summary>Windows AI APIs</summary>

>
> The Windows App SDK now includes a suite of artificial intelligence (AI) APIs that can be used with a local language model to perform a variety of tasks on Copilot+ PCs. Your apps can now intelligently respond to prompts, recognize text within images, describe the content of images, remove objects from images, and more.
>
> For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.
>

</details>
<details><summary>Windows AI Prompt Size Limit Reporting</summary>

>
> Allows applications to determine if an input exceeds the allowable size for a Text Summarizer call. If the input is too large, the API returns an index indicating the current limit, enabling developers to adjust the input accordingly. This limit is based on token count rather than byte or character length, and it can vary over time due to multiple factors. Therefore, applications should treat the limit as dynamic and subject to change.
>

</details>
<details><summary>Windows AI Text Rewriter Tone</summary>

>
> Enables text rewriting with specific tones. The Casual option rephrases content to sound more informal and conversational, using natural, spontaneous phrasing while preserving meaning and format. The Formal option transforms text into a polished, professional version, maintaining the original structure and details with precise language suitable for formal context. The General option retains the original tone and intent, ensuring the meaning remains unchanged.
>

</details>
<details><summary>Text Intelligence - Conversation Summary</summary>

>
> Phi Silica now has a Summarize Conversation feature that allows you to summarize what people have said over an email, chat, or thread. See [Phi Silica](/windows/ai/apis/phi-silica) for more details.
>

</details>
<details><summary>Conversation Summary Options</summary>

>
> Enables developers to specify the desired output language for conversation summarization. This allows applications to generate summaries in a targeted language, enhancing localization, and user experience.
>

</details>
<details><summary>indows AI Object Erase</summary>

>
> Object Erase can be used to remove objects from images. The model takes both an image and a greyscale mask indicating the object to be removed, erases the masked area from the image, and replaces the erased area with the image background.
>

</details>
<details><summary>Decimal DataType</summary>

>
> The new `Decimal` support offers a high-precision base-10 numeric data type that is invaluable for financial and scientific calculations, avoiding imprecision and rounding errors inherent to floating-point data types. It is structured as a 96-bit (12-byte) unsigned integer, scaled by a variable power of 10, allowing for precise representation of decimal values. This enables decimal support for programming languages lacking decimal data types and provides interoperability with languages that do support decimal (e.g. C#, Python).
>

</details>
<details><summary>NuGet Metapackage</summary>

>
> The Windows App SDK NuGet package has been converted to a NuGet metapackage. Each component contributing to the Windows App SDK is now a component NuGet package and is listed as a dependency by the metapackage. This allows developers to choose either the metapackage or select specific component packages for their applications. The use of individual component packages enables developers to include only the APIs and functionalities that are necessary for their apps. The default experience behaves as if `WindowsAppSDKSelfContained` had been set as True, but the `Microsoft.WindowsAppSDK.Runtime` package can be referenced to use framework package deployment.
>

</details>
<details><summary>Microsoft.Windows.SDK.BuildTools.MSIX Refactor</summary>

>
> The MSIX publishing support has been factored into a standalone nuget package, which can be independently maintained and consumed by Windows App SDK and other projects.  In addition, several feature gaps with Single-Project solutions have been addressed including generation of MSIX bundles and MSIX upload packages.
>

</details>
<details><summary>Storage Pickers</summary>

>
> The Microsoft.Windows.Storage.Pickers API in the Windows App SDK provides a modernized file and folder picker experience for desktop applications. This API is based on the existing Windows.Storage.Pickers API design, but with key improvements for desktop scenarios. The new Microsoft.Windows.Storage.Pickers API addresses two critical limitations of the UWP file and folder pickers on Apps developed with Windows App SDK/WinUI:
>
> - Elevated Process Support: The existing Windows.Storage.Pickers APIs do not work when the application is running as an administrator. The new API enables file and folder selection in elevated mode.
> - Simplified Usage in WinUI 3: Using the existing UWP pickers in WinUI 3 requires initializing a window handle for window association. The new pickers eliminate this requirement by accepting a WindowId directly in the constructor, making them easier to use.
>

</details>
<details><summary>Other notable changes</summary>

>
> - Prior to Windows App SDK 1.8, packaged apps running in the AppContainer did not require the packageManagement capability, due to a DeploymentManager auto-initialization issue.  That issue has now been resolved, and in turn, the packageManagement capability is now required for AppContainer-based apps.
> - The experimental WinML APIs have been removed from this release and will be included in a future release.
>

</details>
<details><summary>Bug fixes</summary>

>
> - Fixed an issue where the hover effects of other windows for the app could flicker when at least one window had ExtendsContentIntoTitleBar set to true.
> - NavigationView: Fixed a bug where setting SelectedItem as null did not correctly clear the selection state in collapsed mode.
> - TabView: Fixed an issue where closing a tab would move keyboard focus to the "Add tab" button instead of the newly selected tab.
> - SplitButton: Fixed UI inconsistency where the SplitButton control appeared shorter than standard Button controls
> - TabView: Fixed issue TabView spacing in WinUI, When setting the TabWidthMode property of a TabView to SizeToContent, the padding between the header text and the left/right edges of the tab becomes uneven
>

</details>
<details><summary>New APIs for 1.8.0</summary>

>
> ```
> Microsoft.Windows.AI.Foundation
>  
>     AIFoundationContract
>     EmbeddingVector
> ```
 > ```
> Microsoft.Windows.AI.Imaging
>  
>     ImageObjectRemover
>     ImageObjectRemoverContract
> ```
 > ```
> Microsoft.Windows.AI.Text
>  
>     ConversationItem
>     ConversationSummaryOptions
>     InputKind
>     LanguageModelEmbeddingVectorResult
>     TextRewriter
>         RewriteAsync
>  
>     TextRewriteTone
>     TextSummarizer
>         IsPromptLargerThanContext
>         SummarizeConversationAsync
> ```
 > ```
> Microsoft.Windows.Foundation
>  
>     DecimalContract
>     DecimalHelper
>     DecimalValue
> ```
 > ```
> Microsoft.Windows.Storage.Pickers
>  
>     FileOpenPicker
>     FileSavePicker
>     FolderPicker
>     PickerLocationId
>     PickerViewMode
>     PickFileResult
>     PickFolderResult
>     StoragePickersContract
> ```
 > ```
> Microsoft.Windows.Widgets.Feeds.Providers
>  
>     FeedManager
>         TryRemoveAnnouncementById
>  
>     IFeedManager3
> ```
>

</details>
<details><summary>New APIs compared to 1.8-Preview1</summary>

>
> ```
> Microsoft.Windows.AI.Text
>  
>     TextRewriteTone
>         Concise
> ```
 > ```
> Microsoft.Windows.Foundation
>  
>     DecimalContract
>     DecimalHelper
>     DecimalValue
> ```
>

</details>

:::zone-end
:::zone pivot="preview"

## Version 1.8 Preview (1.8-preview)

Released: **August 19, 2025** <br><br>

<details><summary>Prompt Size Limit Reporting</summary>

>  
> Allows applications to determine if an input exceeds the allowable size for a Text Summarizer call. If the input is too large, the API returns an index indicating the current limit, enabling developers to adjust the input accordingly. This limit is based on token count rather than byte or character length, and it can vary over time due to multiple factors. Therefore, applications should treat the limit as dynamic and subject to change.
>

</details>

<details><summary>Text Rewriter Tone</summary>

>  
> Enables text rewriting with specific tones. The Casual option rephrases content to sound more informal and conversational, using natural, spontaneous phrasing while preserving meaning and format. The Formal option transforms text into a polished, professional version, maintaining the original structure and details with precise language suitable for formal context. The General option retains the original tone and intent, ensuring the meaning remains unchanged.
>

</details>

<details><summary>Conversation Summary Options</summary>

>  
> Enables developers to specify the desired output language for conversation summarization. This allows applications to generate summaries in a targeted language, enhancing localization, and user experience.
> 

<details><summary>Other notable changes</summary>

>  
> * Prior to Windows App SDK 1.8, packaged apps running in the AppContainer did not require the packageManagement capability, due to a DeploymentManager auto-initialization issue.  That issue has now been resolved, and in turn, the packageManagement capability is now required for AppContainer-based apps.
>

</details>

<details><summary>New APIs</summary>

>
> This release includes the following new APIs compared to the stable 1.7 release:
>  
> ```
> Microsoft.Windows.AI.Foundation
>  
>     AIFoundationContract
>     EmbeddingVector
> ```
 > ```
> Microsoft.Windows.AI.Imaging
>  
>     ImageObjectRemover
>     ImageObjectRemoverContract
> ```
 > ```
> Microsoft.Windows.AI.Text
>  
>     ConversationItem
>     ConversationSummaryOptions
>     InputKind
>     LanguageModel
>         CreateContext
>         GenerateEmbeddingVectors
>         GenerateResponseAsync
>         GenerateResponseFromEmbeddingsAsync
>         GetUsablePromptLength
>         GetVectorSpaceId
> 
>     LanguageModelEmbeddingVectorResult
>     TextRewriter
>         RewriteAsync
>  
>     TextRewriteTone
>     TextSummarizer
>         IsPromptLargerThanContext
>         SummarizeConversationAsync
> ```
 > ```
> Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
>  
>     Task
>         Run
> ```
 > ```
> Microsoft.Windows.Storage.Pickers
>  
>     FileOpenPicker
>     FileSavePicker
>     FolderPicker
>     PickerLocationId
>     PickerViewMode
>     PickFileResult
>     PickFolderResult
>     StoragePickersContract
> ```
 > ```
> Microsoft.Windows.Widgets.Feeds.Providers
>  
>     FeedManager
>         TryRemoveAnnouncementById
>  
>     IFeedManager3
> ```
>

</details>

<details><summary>New APIs compared to 1.8-exp4</summary>

>
> ```
> Microsoft.Windows.AI.Text
>  
>     TextSummarizer
>         IsPromptLargerThanContext
>  ```
 > ```
> Microsoft.Windows.Storage.Pickers
>  
>     FileSavePicker
>         SuggestedFolder
>  
>     StoragePickersContract
> ```
>

</details>

<details><summary>Known Issues</summary>

>
> * Standalone use of component packages (such as Microsoft.WindowsAppSDK.WinUI) will require an app-level package reference to the latest Microsoft.Windows.SDK.BuildTools.MSIX, to address an issue with some wapproj-based solutions breaking due to a "WinAppSdkExpandPriContent" task not found error.  Referencing the full top-level Microsoft.WindowsAppSDK package (the common scenario) does not require this.
>

</details>

:::zone-end
:::zone pivot="experimental"

## Version 1.8 Experimental 4 (1.8.0-Experimental4)

Released: **July 8, 2025** <br><br>

<details>
<summary>Use on-device AI with Windows AI APIs</summary>

>
> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.
>
> The Windows App SDK incorporates advanced Windows AI capabilities, enabling developers to seamlessly integrate intelligent features into their applications. These enhancements include local AI functionalities such as responding to incoming prompts, recognizing text within images, describing image contents, extract objects from pictures, and more.
>
> For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.
>

</details>

<details>
<summary>Microsoft Windows ML</summary>

>
> [Windows ML](/windows/ai/new-windows-ml/overview) bringing hardware-accelerated machine learning capabilities to Windows applications. The Microsoft.WindowsAppSDK.ML package provides a Windows-optimized version of ONNX Runtime with simplified APIs for managing execution providers.
>
> **Key Features:**
>
> - Hardware Abstraction: Automatically discovers and manages execution providers compatible with your hardware.
> - Simplified EP Management: Handles acquisition, installation, and registration of execution providers on the local device your app runs on.
> - Seamless ONNX Runtime Integration: Works directly with ONNX Runtime APIs for model inference.
> - Multi-Language Support: Available for C++, C#, Python, and other languages.
>

</details>

<details>
<summary>WindowsAppSDK.Packages renamed</summary>

>
> The NuGet Component Package `Microsoft.WindowsAppSDK.Packages` was renamed to `Microsoft.WindowsAppSDK.Runtime`. This change better reflects that package's purpose and clarifies its role within the SDK - specifically, that it encapsulates the runtime component.
>

</details>

<details>
<summary>Prompt Size Limit Reporting</summary>

>
> Allows applications to determine if an input exceeds the allowable size for a Text Summarizer call. If the input is too large, the API returns an index indicating the current limit, enabling developers to adjust the input accordingly. This limit is based on token count rather than byte or character length, and it can vary over time due to multiple factors. Therefore, applications should treat the limit as dynamic and subject to change.
>

</details>

<details>
<summary>Text Rewriter Tone</summary>

>
> Enables text rewriting with specific tones. The Casual option rephrases content to sound more informal and conversational, using natural, spontaneous phrasing while preserving meaning and format. The Formal option transforms text into a polished, professional version, maintaining the original structure and details with precise language suitable for formal context. The General option retains the original tone and intent, ensuring the meaning remains unchanged.
>

</details>

<details>
<summary>Conversation Summary Options</summary>

>
> Enables developers to specify the desired output language for conversation summarization. This allows applications to generate summaries in a targeted language, enhancing localization, and user experience.
>

</details>

<details>
<summary>Bug Fixes</summary>

>
> - Removed duplicate .winmd files for AI components. For more information see [Windows App SDK GitHub Issue #5439](https://github.com/microsoft/WindowsAppSDK/issues/5439)
> - Addressed a potential crash in `ApplicationDataProvider::GetStateFolderUris` caused by reentrancy. For more information see [Windows App SDK GitHub Issue #10513](https://github.com/microsoft/Microsoft-UI-XAML/issues/10513)
> - Addressed a UI bug where the `TitleBar` displayed incorrect spacing when a short title was used. For more information see [Windows App SDK GitHub Issue #10492](https://github.com/microsoft/Microsoft-UI-XAML/issues/10492)
> - Addressed a UI bug where the `CalendarDatePicker` control displayed incorrect icon margins when a long header was set. For more information see [Windows App SDK GitHub Issue #10469](https://github.com/microsoft/Microsoft-UI-XAML/issues/10469)
> - Resolved an issue related to versioning mismatches between WIndowsAppSDK and Windows SDK NuGet packages, which can cause new projects to fail to build out of the box. For more information see [Windows App SDK GitHub Issue #10467](https://github.com/microsoft/Microsoft-UI-XAML/issues/10467)
> - Addressed a regression where the mouse wheel input was ignored if the "Scroll inactive windows when hovering over them" setting was disabled, making windows appear perpetually inactive. For more information see [Windows App SDK GitHub Issue #10091](https://github.com/microsoft/Microsoft-UI-XAML/issues/10091)
> - Addressed a deployment bug where failing to set `$(WindowsPackageType)=MSIX` in the project file prevents the Deployment Manager from being added, causing apps to require admin privileges unexpectedly. For more information see [Windows App SDK GitHub Issue #8182](https://github.com/microsoft/Microsoft-UI-XAML/issues/8182)
>

</details>

<details>
<summary>New APIs for 1.8-experimental4</summary>

>
> This release includes the following new and modified experimental APIs:
>
> ```
> Microsoft.UI.Composition
>  
>     CompositionNotificationDeferral
>     CompositionProjectedShadow
>         MaxOpacity
>         MinOpacity
>         OpacityFalloff
>  
>     CompositionProjectedShadowCaster
>         AncestorClip
>         Mask
>  
>     CompositionProjectedShadowDrawOrder
>     CompositionProjectedShadowReceiver
>         DrawOrder
>         Mask
> ```
> ```
> Microsoft.UI.Composition.Experimental
>  
>     ExpCompositionVisualSurface
>     ExpExpressionNotificationProperty
>     IExpCompositionPropertyChanged
>     IExpCompositionPropertyChangedListener
>     IExpCompositor
>     IExpVisual
> ```
> ```
> Microsoft.UI.Content
>  
>     ContentAppWindowBridge
>     ContentDisplayOrientations
>     ContentExternalBackdropLink
>     ContentExternalOutputLink
>     ContentIsland
>         Connected
>         ConnectionInfo
>         ConnectRemoteEndpoint
>         Disconnected
>         IsRemoteEndpointConnected
>         Root
>  
>     ContentIslandEnvironment
>         CurrentOrientation
>         NativeOrientation
>         ThemeChanged
>  
>     ContentSite
>         TryGetAutomationProvider
>  
>     ContentSiteEnvironment
>         CurrentOrientation
>         NativeOrientation
>         NotifyThemeChanged
>  
>     CoreWindowSiteBridge
>     CoreWindowTopLevelWindowBridge
>     DesktopChildSiteBridge
>         AcceptRemoteEndpoint
>         ConnectionInfo
>         IsRemoteEndpointConnected
>         RemoteEndpointConnecting
>         RemoteEndpointDisconnected
>         RemoteEndpointRequestedStateChanged
>  
>     DesktopPopupSiteBridge
>         AnchoringBehavior
>         AnchoringPixelAlignment
>  
>     DesktopSiteBridge
>         TryCreatePopupSiteBridge
>  
>     EndpointConnectionEventArgs
>     EndpointRequestedStateChangedEventArgs
>     IContentIslandEndpointConnectionPrivate
>     IContentNodeOwner
>     IContentSiteBridgeEndpointConnectionPrivate
>     PopupAnchoringOptions
>     PopupWindowSiteBridge
>     ProcessStarter
>     SystemVisualSiteBridge
> ```
> ```
> Microsoft.UI.Designer
>  
>     DesignerOutputHost
> ```
> ```
> Microsoft.UI.Input
>  
>     InputKeyboardSource
>         GetForWindowId
>  
>     InputLayoutPolicy
>     InputLightDismissAction
>         GetForIsland
>  
>     InputLightDismissEventArgs
>     InputPointerActivationBehavior
>     InputPointerSource
>         ActivationBehavior
>         DirectManipulationHitTest
>         GetForVisual
>         GetForWindowId
>         RemoveForVisual
>         TouchHitTesting
>         TrySetDeviceKinds
>  
>     InputPopupController
>     LightDismissReason
>     PopupPointerMode
>     ProximityEvaluation
>     TouchHitTestingEventArgs
> ```
> ```
> Microsoft.UI.Input.Experimental
>  
>     ExpInputSite
>     ExpPointerPoint
> ```
> ```
> Microsoft.UI.Windowing
>  
>     AppWindow
>         GetCurrentPlacement
>         PersistedStateId
>         PlacementRestorationBehavior
>         SaveCurrentPlacement
>         SaveCurrentPlacementForAllPersistedStateIds
>         SetCurrentPlacement
>  
>     AppWindowPlacementDetails
>     DisplayArea
>         GetMetricsFromWindowId
>  
>     PlacementInfo
>     PlacementRestorationBehavior
> ```
> ```
> Microsoft.UI.Xaml
>  
>     XamlIsland
>         ShouldConstrainPopupsToWorkArea
> ```
> ```
> Microsoft.UI.Xaml.Automation.Peers
>  
>     AutomationEvents
>         Notification
>  
>     InkCanvasAutomationPeer
>     PagerControlAutomationPeer
> ```
> ```
> Microsoft.UI.Xaml.Controls
>  
>     ContentDialogPlacement
>         UnconstrainedPopup
>  
>     DoInkPresenterWork
>     ElementFactory
>     FlowLayout
>     FlowLayoutAnchorInfo
>     FlowLayoutLineAlignment
>     FlowLayoutState
>     IApplicationViewSpanningRects
>     IndexPath
>     InfoBar
>         Opened
>  
>     InfoBarOpenedEventArgs
>     InkCanvas
>     ISelfPlayingAnimatedVisual
>     ItemContainer
>         CanUserInvoke
>         CanUserInvokeProperty
>         CanUserSelect
>         CanUserSelectProperty
>         ItemInvoked
>         MultiSelectMode
>         MultiSelectModeProperty
>  
>     ItemContainerInteractionTrigger
>     ItemContainerInvokedEventArgs
>     ItemContainerMultiSelectMode
>     ItemContainerUserInvokeMode
>     ItemContainerUserSelectMode
>     LayoutPanel
>     NumberBox
>         InputScope
>         InputScopeProperty
>         TextAlignment
>         TextAlignmentProperty
>  
>     PagerControl
>     PagerControlButtonVisibility
>     PagerControlDisplayMode
>     PagerControlSelectedIndexChangedEventArgs
>     PagerControlTemplateSettings
>     ProgressRing
>         DeterminateSource
>         DeterminateSourceProperty
>         IndeterminateSource
>         IndeterminateSourceProperty
>  
>     RecyclePool
>     RecyclingElementFactory
>     ScrollingScrollStartingEventArgs
>     ScrollingZoomStartingEventArgs
>     ScrollView
>         ScrollStarting
>         ZoomStarting
>  
>     SelectionModel
>     SelectionModelChildrenRequestedEventArgs
>     SelectionModelSelectionChangedEventArgs
>     SelectTemplateEventArgs
>     StackLayout
>         IsVirtualizationEnabled
>         IsVirtualizationEnabledProperty
>  
>     StackLayoutState
>     TeachingTip
>         Opened
>  
>     TeachingTipOpenedEventArgs
>     UniformGridLayoutState
> ```
> ```
> Microsoft.UI.Xaml.Controls.Primitives
>  
>     ScrollPresenter
>         ScrollStarting
>         ZoomStarting
> ```
> ```
> Microsoft.Windows.AI.Foundation
>  
>     AIFoundationContract
>     EmbeddingVector
> ```
> ```
> Microsoft.Windows.AI.Imaging
>  
>     ImageObjectRemover
>     ImageObjectRemoverContract
> ```
> ```
> Microsoft.Windows.AI.MachineLearning
>  
>     ExecutionProvider
>     ExecutionProviderCatalog
>     ExecutionProviderReadyResult
>     ExecutionProviderReadyResultState
>     ExecutionProviderReadyState
>     MachineLearningContract
> ```
> ```
> Microsoft.Windows.AI.Text
>  
>     ConversationItem
>     ConversationSummaryOptions
>     InputKind
>     LanguageModel
>         CreateContext
>         CreateContext
>         CreateContext
>         GenerateEmbeddingVectors
>         GenerateEmbeddingVectors
>         GenerateResponseAsync
>         GenerateResponseAsync
>         GenerateResponseAsync
>         GenerateResponseFromEmbeddingsAsync
>         GenerateResponseFromEmbeddingsAsync
>         GenerateResponseFromEmbeddingsAsync
>         GetUsablePromptLength
>         GetUsablePromptLength
>         GetVectorSpaceId
>  
>     LanguageModelEmbeddingVectorResult
>     TextRewriter
>         RewriteAsync
>  
>     TextRewriteTone
>     TextSummarizer
>         IsPromptLargerThanContext
>         SummarizeConversationAsync
> ```
> ```
> Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
>  
>     Task
>         Run
> ```
> ```
> Microsoft.Windows.ApplicationModel.WindowsAppRuntime
>  
>     DeploymentManager
>         Repair
>  
>     DeploymentStatus
>         PackageRepairFailed
> ```
> ```
> Microsoft.Windows.AppNotifications
>  
>     AppNotification
>         ConferencingConfig
>  
>     AppNotificationConferencingConfig
> ```
> ```
> Microsoft.Windows.AppNotifications.Builder
>  
>     AppNotificationBuilder
>         AddCameraPreview
>  
>     AppNotificationButton
>         SetSettingStyle
>  
>     AppNotificationButtonSettingStyle
> ```
> ```
> Microsoft.Windows.SemanticSearch
>  
>     EmbeddingVector
>     SemanticSearchContract
> ```
> ```
> Microsoft.Windows.Storage
>  
>     ApplicationData
>         GetForUnpackaged
> ```
> ```
> Microsoft.Windows.Storage.Pickers
>  
>     FileOpenPicker
>     FileSavePicker
>     FolderPicker
>     PickerLocationId
>     PickerViewMode
>     PickFileResult
>     PickFolderResult
> ```
> ```
> Microsoft.Windows.Vision
>  
>     ScreenRegionBoundingBox
>     ScreenRegionDetectionContract
>     ScreenRegionLabel
> ```
> ```
> Microsoft.Windows.Widgets.Feeds.Providers
>  
>     FeedManager
>         TryRemoveAnnouncementById
>  
>     IFeedManager3
> ```
>

</details>

<details>
<summary>Known Issues</summary>

>
> - When upgrading from version 1.8.250610002-experimental3 (or later) of the Microsoft.WindowsAppSDK NuGet package in a C++ project, you may see a compatibility error, such as with Microsoft.WindowsAppSDK.DWrite. This stems from a limitation in packages.config. To resolve it, remove all existing WindowsAppSDK references and re-add the updated Microsoft.WindowsAppSDK package.
>
> - Windows ML requires framework-dependent deployment; self-containment deployment is not supported. Apps using Windows ML must reference the Microsoft.WindowsAppSDK package, which includes transitive dependencies on the Microsoft.WindowsAppSDK.ML and Microsoft.WindowsAppSDK.Runtime components, both of which are required.
>
> - Windows ML is supported only on Windows 11 version 24H2 or newer (Build 26100+), and only on x64 and ARM64 architectures. x86 is not supported.
>
> - The StoragePickers APIs (FileOpenPicker, FileSavePicker, FolderPicker) only work in self-contained deployments due to a localization bug. Non-self-contained apps will crash at runtime when invoking these pickers. As a workaround, copy Microsoft.WindowsAppRuntime.pri to your project folder and configure it to copy to the output directory using:
> ```
> <ItemGroup>
>    <None Update="Microsoft.WindowsAppRuntime.pri">
>       <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
>    </None>
> </ItemGroup>
> ```
>

</details>

---

## Version 1.8 Experimental 3 (1.8.0-experimental3)

Released: **June 12, 2025** <br><br>

<details>
<summary>Use on-device AI with Windows AI APIs</summary>

>
> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.
>
> The Windows App SDK incorporates advanced Windows AI capabilities, enabling developers to seamlessly integrate intelligent features into their applications. These enhancements include local AI functionalities such as responding to incoming prompts, recognizing text within images, describing image contents, extract objects from pictures, and more.
>
> For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.
>

</details>

<details>
<summary>New APIs for 1.8-experimental3</summary>

>
> This release includes the following new and modified experimental APIs:
>
> ```
> Microsoft.UI.Composition
>
>     CompositionNotificationDeferral
>     CompositionProjectedShadow
>         MaxOpacity
>         MinOpacity
>         OpacityFalloff
>
>     CompositionProjectedShadowCaster
>         AncestorClip
>         Mask
>
>     CompositionProjectedShadowDrawOrder
>     CompositionProjectedShadowReceiver
>         DrawOrder
>         Mask
> ```
> ```
> Microsoft.UI.Composition.Experimental
>
>     ExpCompositionVisualSurface
>     ExpExpressionNotificationProperty
>     IExpCompositionPropertyChanged
>     IExpCompositionPropertyChangedListener
>     IExpCompositor
>     IExpVisual
> ```
> ```
> Microsoft.UI.Content
>
>     ContentAppWindowBridge
>     ContentDisplayOrientations
>     ContentExternalBackdropLink
>     ContentExternalOutputLink
>     ContentIsland
>         Connected
>         ConnectionInfo
>         ConnectRemoteEndpoint
>         Disconnected
>         IsRemoteEndpointConnected
>         Root
>
>     ContentIslandEnvironment
>         CurrentOrientation
>         NativeOrientation
>         ThemeChanged
>
>     ContentSite
>         TryGetAutomationProvider
>
>     ContentSiteEnvironment
>         CurrentOrientation
>         NativeOrientation
>         NotifyThemeChanged
>
>     CoreWindowSiteBridge
>     CoreWindowTopLevelWindowBridge
>     DesktopChildSiteBridge
>         AcceptRemoteEndpoint
>         ConnectionInfo
>         IsRemoteEndpointConnected
>         RemoteEndpointConnecting
>         RemoteEndpointDisconnected
>         RemoteEndpointRequestedStateChanged
>
>     DesktopSiteBridge
>         TryCreatePopupSiteBridge
>
>     EndpointConnectionEventArgs
>     EndpointRequestedStateChangedEventArgs
>     IContentIslandEndpointConnectionPrivate
>     IContentNodeOwner
>     IContentSiteBridgeEndpointConnectionPrivate
>     PopupWindowSiteBridge
>     ProcessStarter
>     SystemVisualSiteBridge
> ```
> ```
> Microsoft.UI.Designer
>
>     DesignerOutputHost
> ```
> ```
> Microsoft.UI.Input
>
>     InputKeyboardSource
>         GetForWindowId
>
>     InputLayoutPolicy
>     InputLightDismissAction
>         GetForIsland
>
>     InputLightDismissEventArgs
>     InputPointerActivationBehavior
>     InputPointerSource
>         ActivationBehavior
>         DirectManipulationHitTest
>         GetForVisual
>         GetForWindowId
>         RemoveForVisual
>         TouchHitTesting
>         TrySetDeviceKinds
>
>     InputPopupController
>     LightDismissReason
>     PopupPointerMode
>     ProximityEvaluation
>     TouchHitTestingEventArgs
> ```
> ```
> Microsoft.UI.Input.Experimental
>
>     ExpInputSite
>     ExpPointerPoint
> ```
> ```
> Microsoft.UI.Windowing
>
>     AppWindow
>         GetCurrentPlacement
>         PersistedStateId
>         PlacementRestorationBehavior
>         SaveCurrentPlacement
>         SaveCurrentPlacementForAllPersistedStateIds
>         SetCurrentPlacement
>
>     AppWindowPlacementDetails
>     DisplayArea
>         GetMetricsFromWindowId
>
>     PlacementInfo
>     PlacementRestorationBehavior
> ```
> ```
> Microsoft.UI.Xaml
>
>     XamlIsland
>         ShouldConstrainPopupsToWorkArea
> ```
> ```
> Microsoft.UI.Xaml.Automation.Peers
>
>     AutomationEvents
>         Notification
>
>     InkCanvasAutomationPeer
>     PagerControlAutomationPeer
>
> Microsoft.UI.Xaml.Controls
>
>     ContentDialogPlacement
>         UnconstrainedPopup
>
>     DoInkPresenterWork
>     ElementFactory
>     FlowLayout
>     FlowLayoutAnchorInfo
>     FlowLayoutLineAlignment
>     FlowLayoutState
>     IApplicationViewSpanningRects
>     IndexPath
>     InfoBar
>         Opened
>
>     InfoBarOpenedEventArgs
>     InkCanvas
>     ISelfPlayingAnimatedVisual
>     ItemContainer
>         CanUserInvoke
>         CanUserInvokeProperty
>         CanUserSelect
>         CanUserSelectProperty
>         ItemInvoked
>         MultiSelectMode
>         MultiSelectModeProperty
>
>     ItemContainerInteractionTrigger
>     ItemContainerInvokedEventArgs
>     ItemContainerMultiSelectMode
>     ItemContainerUserInvokeMode
>     ItemContainerUserSelectMode
>     LayoutPanel
>     NumberBox
>         InputScope
>         InputScopeProperty
>         TextAlignment
>         TextAlignmentProperty
>
>     PagerControl
>     PagerControlButtonVisibility
>     PagerControlDisplayMode
>     PagerControlSelectedIndexChangedEventArgs
>     PagerControlTemplateSettings
>     ProgressRing
>         DeterminateSource
>         DeterminateSourceProperty
>         IndeterminateSource
>         IndeterminateSourceProperty
>
>     RecyclePool
>     RecyclingElementFactory
>     ScrollingScrollStartingEventArgs
>     ScrollingZoomStartingEventArgs
>     ScrollView
>         ScrollStarting
>         ZoomStarting
>
>     SelectionModel
>     SelectionModelChildrenRequestedEventArgs
>     SelectionModelSelectionChangedEventArgs
>     SelectTemplateEventArgs
>     StackLayout
>         IsVirtualizationEnabled
>         IsVirtualizationEnabledProperty
>
>     StackLayoutState
>     TeachingTip
>         Opened
>
>     TeachingTipOpenedEventArgs
>     UniformGridLayoutState
> ```
> ```
> Microsoft.UI.Xaml.Controls.Primitives
>
>     ScrollPresenter
>         ScrollStarting
>         ZoomStarting
> ```
> ```
> Microsoft.Windows.AI.Foundation
>
>     AIFoundationContract
>     EmbeddingVector
> ```
> ```
> Microsoft.Windows.AI.Imaging
>
>     ImageObjectRemover
>     ImageObjectRemoverContract
> ```
> ```
> Microsoft.Windows.AI.Text
>
>     ConversationItem
>     ConversationSummaryOptions
>     InputKind
>     LanguageModel
>         CreateContext
>         CreateContext
>         CreateContext
>         GenerateEmbeddingVectors
>         GenerateEmbeddingVectors
>         GenerateResponseAsync
>         GenerateResponseAsync
>         GenerateResponseAsync
>         GenerateResponseFromEmbeddingsAsync
>         GenerateResponseFromEmbeddingsAsync
>         GenerateResponseFromEmbeddingsAsync
>         GetUsablePromptLength
>         GetUsablePromptLength
>         GetVectorSpaceId
>
>     LanguageModelEmbeddingVectorResult
>     TextSummarizer
>         SummarizeConversationAsync
> ```
> ```
> Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
>
>     Task
>         Run
> ```
> ```
> Microsoft.Windows.ApplicationModel.WindowsAppRuntime
>
>     DeploymentManager
>         Repair
>
>     DeploymentStatus
>         PackageRepairFailed
> ```
> ```
> Microsoft.Windows.AppNotifications
>
>     AppNotification
>         ConferencingConfig
>
>     AppNotificationConferencingConfig
> ```
> ```
> Microsoft.Windows.AppNotifications.Builder
>
>     AppNotificationBuilder
>         AddCameraPreview
>
>     AppNotificationButton
>         SetSettingStyle
>
>     AppNotificationButtonSettingStyle
> ```
> ```
> Microsoft.Windows.SemanticSearch
>
>     EmbeddingVector
>     SemanticSearchContract
> ```
> ```
> Microsoft.Windows.Storage
>
>     ApplicationData
>         GetForUnpackaged
> ```
> ```
> Microsoft.Windows.Storage.Pickers
>
>     FileOpenPicker
>     FileSavePicker
>     FolderPicker
>     PickerLocationId
>     PickerViewMode
>     PickFileResult
>     PickFolderResult
> ```
> ```
> Microsoft.Windows.Vision
>
>     ScreenRegionBoundingBox
>     ScreenRegionDetectionContract
>     ScreenRegionLabel
> ```
> ```
> Microsoft.Windows.Widgets.Feeds.Providers
>
>     FeedManager
>         TryRemoveAnnouncementById
>
>     IFeedManager3
> ```
>

</details>

---

## Version 1.8 Experimental 2 (1.8.0-experimental2)

Released: **May 16, 2025** <br><br>

<details>
<summary>Use on-device AI with Windows AI APIs</summary>

>
> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.
>
> The Windows App SDK incorporates advanced Windows AI capabilities, enabling developers to seamlessly integrate intelligent features into their applications. These enhancements include local AI functionalities such as responding to incoming prompts, recognizing text within images, describing image contents, extract objects from pictures, and more.
>
> For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.
>

</details>

<details>
<summary>Decimal DataType</summary>

>
> The new `Decimal` support offers a high-precision base-10 numeric data type that is invaluable for financial and scientific calculations, avoiding imprecision and rounding errors inherent to floating-point data types. It is structured as a 96-bit (12-byte) unsigned integer, scaled by a variable power of 10, allowing for precise representation of decimal values. This enables decimal support for programming languages lacking decimal data types and provides interoperability with languages that do support decimal (e.g. C#, Python).
>

</details>

<details>
<summary>NuGet Metapackage</summary>

>
> The Windows App SDK NuGet package has been converted to a NuGet metapackage. Each component contributing to the Windows App SDK is now a component NuGet package and is listed as a dependency by the metapackage. This allows developers to choose either the metapackage or select specific component packages for their applications. The use of individual component packages enables developers to include only the APIs and functionalities that are necessary for their apps. The default experience behaves as if `WindowsAppSDKSelfContained` had been set as True, but the `Microsoft.WindowsAppSDK.Packages` package can be referenced to use framework package deployment.
>

</details>

<details>
<summary>Microsoft.Windows.SDK.BuildTools.MSIX Refactor</summary>

>
> The MSIX publishing support has been factored into a standalone nuget package, which can be independently maintained and consumed by Windows App SDK and other projects.  In addition, several feature gaps with Single-Project solutions have been addressed including generation of MSIX bundles and MSIX upload packages.
>

</details>

<details>
<summary>Low-Rank Adaptation (LoRA) for Phi Silica</summary>

>
> Low-Rank Adaption (LoRA) for Phi Silica allows developers to fine-tune the on-device language model (Phi Silica) using their own custom data. This adapter enables output to align for specific scenarios like finance, medical, and education. See [Phi Silica LoRA](/windows/ai/apis/phi-silica-lora) for details.
>

</details>

<details>
<summary>Text Intelligence - Conversation Summary</summary> 

> Phi Silica now has a Summarize Conversation feature that allows you to summarize what people have said over an email, chat, or thread. See [Phi Silica](/windows/ai/apis/phi-silica) for more details.
>

</details>

<details>
<summary>New APIs for 1.8-experimental2</summary>

>
> This release includes the following new and modified experimental APIs:
>
> ```
> Microsoft.UI.Composition
>
>     CompositionNotificationDeferral
>     CompositionProjectedShadow
>         MaxOpacity
>         MinOpacity
>         OpacityFalloff
>
>     CompositionProjectedShadowCaster
>         AncestorClip
>         Mask
>
>     CompositionProjectedShadowDrawOrder
>     CompositionProjectedShadowReceiver
>         DrawOrder
>         Mask
> ```
> ```
> Microsoft.UI.Composition.Experimental
>
>     ExpCompositionVisualSurface
>     ExpExpressionNotificationProperty
>     IExpCompositionPropertyChanged
>     IExpCompositionPropertyChangedListener
>     IExpCompositor
>     IExpVisual
> ```
> ```
> Microsoft.UI.Content
>
>     ContentAppWindowBridge
>     ContentDisplayOrientations
>     ContentExternalBackdropLink
>     ContentExternalOutputLink
>     ContentIsland
>         Connected
>         ConnectionInfo
>         ConnectRemoteEndpoint
>         Disconnected
>         IsRemoteEndpointConnected
>         Root
>
>     ContentIslandEnvironment
>         CurrentOrientation
>         NativeOrientation
>         ThemeChanged
>
>     ContentSite
>         SetContentNodeParent
>         TryGetAutomationProvider
>
>     ContentSiteEnvironment
>         CurrentOrientation
>         NativeOrientation
>         NotifyThemeChanged
>
>     CoreWindowSiteBridge
>     CoreWindowTopLevelWindowBridge
>     DesktopChildSiteBridge
>         AcceptRemoteEndpoint
>         ConnectionInfo
>         IsRemoteEndpointConnected
>         RemoteEndpointConnecting
>         RemoteEndpointDisconnected
>         RemoteEndpointRequestedStateChanged
>
>     DesktopSiteBridge
>         TryCreatePopupSiteBridge
>
>     EndpointConnectionEventArgs
>     EndpointRequestedStateChangedEventArgs
>     IContentIslandEndpointConnectionPrivate
>     IContentNodeOwner
>     IContentSiteBridgeEndpointConnectionPrivate
>     PopupWindowSiteBridge
>     ProcessStarter
>     SystemVisualSiteBridge
> ```
> ```
> Microsoft.UI.Input
>
>     InputKeyboardSource
>         GetForWindowId
>
>     InputLayoutPolicy
>     InputLightDismissAction
>         GetForIsland
>
>     InputPointerActivationBehavior
>     InputPointerSource
>         ActivationBehavior
>         DirectManipulationHitTest
>         GetForVisual
>         GetForWindowId
>         RemoveForVisual
>         TouchHitTesting
>         TrySetDeviceKinds
>
>     ProximityEvaluation
>     TouchHitTestingEventArgs
> ```
> ```
> Microsoft.UI.Windowing
>
>     AppWindow
>         GetCurrentPlacement
>         PersistedStateId
>         PlacementRestorationBehavior
>         SaveCurrentPlacement
>         SaveCurrentPlacementForAllPersistedStateIds
>         SetCurrentPlacement
>
>     AppWindowPlacementDetails
>     DisplayArea
>         GetMetricsFromWindowId
>
>     PlacementInfo
>     PlacementRestorationBehavior
> ```
> ```
> Microsoft.UI.Xaml
>
>     XamlIsland
>         ShouldConstrainPopupsToWorkArea
> ```
> ```
> Microsoft.UI.Xaml.Automation.Peers
>
>     AutomationEvents
>         Notification
>
>     InkCanvasAutomationPeer
>     PagerControlAutomationPeer
> ```
> ```
> Microsoft.UI.Xaml.Controls
>
>     ContentDialogPlacement
>         UnconstrainedPopup
>
>     DoInkPresenterWork
>     ElementFactory
>     FlowLayout
>     FlowLayoutAnchorInfo
>     FlowLayoutLineAlignment
>     FlowLayoutState
>     IApplicationViewSpanningRects
>     IndexPath
>     InkCanvas
>     ISelfPlayingAnimatedVisual
>     ItemContainer
>         CanUserInvoke
>         CanUserInvokeProperty
>         CanUserSelect
>         CanUserSelectProperty
>         ItemInvoked
>         MultiSelectMode
>         MultiSelectModeProperty
>
>     ItemContainerInteractionTrigger
>     ItemContainerInvokedEventArgs
>     ItemContainerMultiSelectMode
>     ItemContainerUserInvokeMode
>     ItemContainerUserSelectMode
>     LayoutPanel
>     NumberBox
>         InputScope
>         InputScopeProperty
>         TextAlignment
>         TextAlignmentProperty
>
>     PagerControl
>     PagerControlButtonVisibility
>     PagerControlDisplayMode
>     PagerControlSelectedIndexChangedEventArgs
>     PagerControlTemplateSettings
>     ProgressRing
>         DeterminateSource
>         DeterminateSourceProperty
>         IndeterminateSource
>         IndeterminateSourceProperty
>
>     RecyclePool
>     RecyclingElementFactory
>     ScrollingScrollStartingEventArgs
>     ScrollingZoomStartingEventArgs
>     ScrollView
>         ScrollStarting
>         ZoomStarting
>
>     SelectionModel
>     SelectionModelChildrenRequestedEventArgs
>     SelectionModelSelectionChangedEventArgs
>     SelectTemplateEventArgs
>     StackLayout
>         IsVirtualizationEnabled
>         IsVirtualizationEnabledProperty
>
>     StackLayoutState
>     TeachingTip
>         Opened
>
>     TeachingTipOpenedEventArgs
>     UniformGridLayoutState
> ```
> ```
> Microsoft.UI.Xaml.Controls.Primitives
>
>     ScrollPresenter
>         ScrollStarting
>         ZoomStarting
> ```
> ```
> Microsoft.Windows.AI.Foundation
>
>     AIFoundationContract
>     EmbeddingVector
> ```
> ```
> Microsoft.Windows.AI.Imaging
>
>     ImageObjectRemover
>     ImageObjectRemoverContract
> ```
> ```
> Microsoft.Windows.AI.Text
>
>     ConversationItem
>     ConversationSummaryOptions
>     InputKind
>     LanguageModel
>         CreateContext
>         CreateContext
>         CreateContext
>         GenerateEmbeddingVectors
>         GenerateEmbeddingVectors
>         GenerateResponseAsync
>         GenerateResponseAsync
>         GenerateResponseAsync
>         GenerateResponseFromEmbeddingsAsync
>         GenerateResponseFromEmbeddingsAsync
>         GenerateResponseFromEmbeddingsAsync
>         GetUsablePromptLength
>         GetUsablePromptLength
>         GetVectorSpaceId
>
>     LanguageModelEmbeddingVectorResult
>     TextSummarizer
>         SummarizeConversationAsync
> ```
> ```
> Microsoft.Windows.AI.Text.Experimental (C#-only, see Known Issues)
>  
>     LowRankAdaptation
>     LanguageModelOptionsExperimental
>     LanguageModelExperimental
> ```
> ```
> Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
>
>     Task
>         Run
> ```
> ```
> Microsoft.Windows.ApplicationModel.WindowsAppRuntime
>
>     DeploymentManager
>         Repair
>
>     DeploymentStatus
>         PackageRepairFailed
> ```
> ```
> Microsoft.Windows.AppNotifications
>
>     AppNotification
>         ConferencingConfig
>
>     AppNotificationConferencingConfig
> ```
> ```
> Microsoft.Windows.AppNotifications.Builder
>
>     AppNotificationBuilder
>         AddCameraPreview
>
>     AppNotificationButton
>         SetSettingStyle
>
>     AppNotificationButtonSettingStyle
> ```
> ```
> Microsoft.Windows.Storage
>
>     ApplicationData
>         GetForUnpackaged
> ```
> ```
> Microsoft.Windows.Storage.Pickers
>
>     FileOpenPicker
>     FileSavePicker
>     FolderPicker
>     PickerLocationId
>     PickerViewMode
>     PickFileResult
>     PickFolderResult
> ```
> ```
> Microsoft.Windows.Vision
>
>     ScreenRegionBoundingBox
>     ScreenRegionDetectionContract
>     ScreenRegionLabel
> ```
> ```
> Microsoft.Windows.Widgets.Feeds.Providers
>
>     FeedManager
>         TryRemoveAnnouncementById
>
>     IFeedManager3
> ```
> ```
> Microsoft.Windows.Widgets.Providers
>
>     WidgetInfo
>         Rank
>
>     WidgetUpdateRequestOptions
>         Rank
> ```
>

</details>

<details>
<summary>Known issues</summary>

>
> * The Microsoft.Windows.AI.Text.Experimental API projections for C++ are missing in this release. The projections are available for use from C#.
> * If you're using the Microsoft.WindowsAppSDK.WinUI component package in its default self-contained mode, make sure to set the WebView2EnableCsWinRTProjection property to true when utilizing WebView2 APIs. This helps prevent version conflicts and avoids related warnings.
> * When using the WindowsAppSDK component packages, you may notice a warning `NU1603` indicating the specified version of a dependent component package was not found, but another was resolved instead.  This is expected with the experimental2 build and NuGet will correctly resolve a newer version of the package which will allow your project to build.  If you treat warnings as errors, you can temporarily treat this specific warning as not an error by specifying the property `<WarningsNotAsErrors>NU1603</WarningsNotAsErrors>`.
>

</details>

---

## Version 1.8 Experimental 1 (1.8.0-experimental1)

Released: **April 16, 2025** <br><br>

<details>
<summary>Use on-device AI with Windows AI APIs</summary>

>
> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.
>
> The Windows AI APIs offers several AI-powered features and APIs for you to easily, efficiently, and responsibly use on-device AI models in your Windows apps. In this release we are making available several scenario-focused APIs for you to leverage powerful capabilities without the need to find, run, or optimize your own Machine Learning (ML) models. 
>
> Learn more about responsible development practices used during Windows AI API development that you can also apply as you create AI-assisted features in the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.
>
> This is the latest release of the experimental channel.
>
> To download, retarget your Windows App SDK NuGet version to `1.8.250515001-experimental1`.
>

</details>

<details>
<summary>Object Erase</summary>

>
> The `ImageObjectRemover` can be used to remove objects from images. The model takes both an image and a greyscale mask indicating the object to be removed, erases the masked area from the image, and replaces the erased area with the image background.
>

</details>

<details>
<summary>New APIs for 1.8-experimental1</summary>

>
> This release includes the following new and modified experimental APIs:
>
> ```
> Microsoft.Graphics.Imaging
>
>     ImageBuffer
>     ImageBufferContract
>     ImageObjectExtractor
>     ImageObjectExtractorContract
>     ImageObjectExtractorHint
>     ImageObjectRemover
>     ImageObjectRemoverContract
>     ImageScaler
>     ImageScalerContract
>     PixelFormat
> ```
> ```
> Microsoft.UI.Composition
>
>     CompositionNotificationDeferral
>     CompositionProjectedShadow
>         MaxOpacity
>         MinOpacity
>         OpacityFalloff
>
>     CompositionProjectedShadowCaster
>         AncestorClip
>         Mask
>
>     CompositionProjectedShadowDrawOrder
>     CompositionProjectedShadowReceiver
>         DrawOrder
>         Mask
> ```
> ```
> Microsoft.UI.Composition.Experimental
>
>     ExpCompositionVisualSurface
>     ExpExpressionNotificationProperty
>     IExpCompositionPropertyChanged
>     IExpCompositionPropertyChangedListener
>     IExpCompositor
>     IExpVisual
> ```
> ```
> Microsoft.UI.Content
>
>     ContentAppWindowBridge
>     ContentDisplayOrientations
>     ContentExternalBackdropLink
>     ContentExternalOutputLink
>     ContentIsland
>         Connected
>         ConnectionInfo
>         ConnectRemoteEndpoint
>         Disconnected
>         IsRemoteEndpointConnected
>         Root
>
>     ContentIslandEnvironment
>         CurrentOrientation
>         NativeOrientation
>         ThemeChanged
>
>     ContentSite
>         SetContentNodeParent
>         TryGetAutomationProvider
>
>     ContentSiteEnvironment
>         CurrentOrientation
>         NativeOrientation
>         NotifyThemeChanged
>
>     CoreWindowSiteBridge
>     CoreWindowTopLevelWindowBridge
>     DesktopChildSiteBridge
>         AcceptRemoteEndpoint
>         ConnectionInfo
>         IsRemoteEndpointConnected
>         RemoteEndpointConnecting
>         RemoteEndpointDisconnected
>         RemoteEndpointRequestedStateChanged
>
>     DesktopSiteBridge
>         TryCreatePopupSiteBridge
>
>     EndpointConnectionEventArgs
>     EndpointRequestedStateChangedEventArgs
>     IContentIslandEndpointConnectionPrivate
>     IContentNodeOwner
>     IContentSiteBridgeEndpointConnectionPrivate
>     PopupWindowSiteBridge
>     ProcessStarter
>     SystemVisualSiteBridge
> ```
> ```
> Microsoft.UI.Input
>
>     InputKeyboardSource
>         GetForWindowId
>
>     InputLayoutPolicy
>     InputLightDismissAction
>         GetForIsland
>
>     InputPointerActivationBehavior
>     InputPointerSource
>         ActivationBehavior
>         DirectManipulationHitTest
>         GetForVisual
>         GetForWindowId
>         RemoveForVisual
>         TouchHitTesting
>         TrySetDeviceKinds
>
>     ProximityEvaluation
>     TouchHitTestingEventArgs
> ```
> ```
> Microsoft.UI.Input.Experimental
>
>     ExpInputSite
>     ExpPointerPoint
> ```
> ```
> Microsoft.UI.Windowing
>
>     AppWindow
>         GetCurrentPlacement
>         PersistedStateId
>         PlacementRestorationBehavior
>         SaveCurrentPlacement
>         SaveCurrentPlacementForAllPersistedStateIds
>         SetCurrentPlacement
>
>     AppWindowPlacementDetails
>     DisplayArea
>         GetMetricsFromWindowId
>
>     PlacementInfo
>     PlacementRestorationBehavior
> ```
> ```
> Microsoft.UI.Xaml
>
>     XamlIsland
>         ShouldConstrainPopupsToWorkArea
> ```
> ```
> Microsoft.UI.Xaml.Automation.Peers
>
>     AutomationEvents
>         Notification
>
>     InkCanvasAutomationPeer
>     PagerControlAutomationPeer
> ```
> ```
> Microsoft.UI.Xaml.Controls
>
>     ContentDialogPlacement
>         UnconstrainedPopup
>
>     DoInkPresenterWork
>     ElementFactory
>     FlowLayout
>     FlowLayoutAnchorInfo
>     FlowLayoutLineAlignment
>     FlowLayoutState
>     IApplicationViewSpanningRects
>     IndexPath
>     InkCanvas
>     ISelfPlayingAnimatedVisual
>     ItemContainer
>         CanUserInvoke
>         CanUserInvokeProperty
>         CanUserSelect
>         CanUserSelectProperty
>         ItemInvoked
>         MultiSelectMode
>         MultiSelectModeProperty
>
>     ItemContainerInteractionTrigger
>     ItemContainerInvokedEventArgs
>     ItemContainerMultiSelectMode
>     ItemContainerUserInvokeMode
>     ItemContainerUserSelectMode
>     LayoutPanel
>     NumberBox
>         InputScope
>         InputScopeProperty
>         TextAlignment
>         TextAlignmentProperty
>
>     PagerControl
>     PagerControlButtonVisibility
>     PagerControlDisplayMode
>     PagerControlSelectedIndexChangedEventArgs
>     PagerControlTemplateSettings
>     ProgressRing
>         DeterminateSource
>         DeterminateSourceProperty
>         IndeterminateSource
>         IndeterminateSourceProperty
>
>     RecyclePool
>     RecyclingElementFactory
>     ScrollingScrollStartingEventArgs
>     ScrollingZoomStartingEventArgs
>     ScrollView
>         ScrollStarting
>         ZoomStarting
>
>     SelectionModel
>     SelectionModelChildrenRequestedEventArgs
>     SelectionModelSelectionChangedEventArgs
>     SelectTemplateEventArgs
>     StackLayout
>         IsVirtualizationEnabled
>         IsVirtualizationEnabledProperty
>
>     StackLayoutState
>     UniformGridLayoutState
> ```
> ```
> Microsoft.UI.Xaml.Controls.Primitives
>
>     ScrollPresenter
>         ScrollStarting
>         ZoomStarting
> ```
> ```
> Microsoft.Windows.AI
>
>     AIFeatureReadyContract
>     AIFeatureReadyResult
>     AIFeatureReadyResultState
>     AIFeatureReadyState
> ```
> ```
> Microsoft.Windows.AI.ContentModeration
>
>     ContentFilterOptions
>     ContentModerationContract
>     ImageContentFilterSeverity
>     SeverityLevel
>     TextContentFilterSeverity
> ```
> ```
> Microsoft.Windows.AI.Generative
>
>     ImageDescriptionContract
>     ImageDescriptionGenerator
>     ImageDescriptionKind
>     ImageDescriptionResult
>     ImageDescriptionResultStatus
>     LanguageModel
>     LanguageModelContext
>     LanguageModelContract
>     LanguageModelEmbeddingVectorResult
>     LanguageModelOptions
>     LanguageModelResponseResult
>     LanguageModelResponseStatus
> ```
> ```
> Microsoft.Windows.ApplicationModel.WindowsAppRuntime
>
>     DeploymentManager
>         Repair
>
>     DeploymentStatus
>         PackageRepairFailed
> ```
> ```
> Microsoft.Windows.AppNotifications
>
>     AppNotification
>         ConferencingConfig
>
>     AppNotificationConferencingConfig
> ```
> ```
> Microsoft.Windows.AppNotifications.Builder
>
>     AppNotificationBuilder
>         AddCameraPreview
>
>     AppNotificationButton
>         SetSettingStyle
>
>     AppNotificationButtonSettingStyle
> ```
> ```
> Microsoft.Windows.SemanticSearch
>
>     EmbeddingVector
>     SemanticSearchContract
> ```
> ```
> Microsoft.Windows.Storage
>
>     ApplicationData
>         GetForUnpackaged
> ```
> ```
> Microsoft.Windows.Storage.Pickers
>
>     FileOpenPicker
>     FileSavePicker
>     FolderPicker
>     PickerLocationId
>     PickerViewMode
>     PickFileResult
>     PickFolderResult
> ```
> ```
> Microsoft.Windows.Vision
>
>     BoundingBox
>     DetectedLineStyle
>     OrientationDetectionOptions
>     RecognizedLine
>     RecognizedLineStyle
>     RecognizedText
>     RecognizedWord
>     TextRecognitionContract
>     TextRecognizer
>     TextRecognizerOptions
> ```
> ```
> Microsoft.Windows.Widgets.Feeds.Providers
>
>     FeedManager
>         TryRemoveAnnouncementById
>
>     IFeedManager3
> ```
> ```
> Microsoft.Windows.Workloads
>
>     WorkloadPriority
>     WorkloadsContract
> ```
>

</details>

<details>
<summary>Bug fixes</summary>

>
> This release includes the following bug fixes:
>
> - Fixed an issue where mouse wheel input is ignored if the "Scroll inactive windows when hovering over them" option in Windows Settings is disabled. For more info, see GitHub issue [#10091](https://github.com/microsoft/microsoft-ui-xaml/issues/10091).
>

</details>

:::zone-end