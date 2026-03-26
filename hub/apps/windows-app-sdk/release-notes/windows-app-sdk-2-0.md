---
title: Windows App SDK 2.0 release notes
description: Provides information about what's new in Windows App SDK 2.0.
ms.topic: release-notes
ms.date: 09/22/2025
keywords: windows win32, windows app development, Windows App SDK, release notes
ms.localizationpriority: medium
zone_pivot_groups: wasdk-release-channels
---

# Windows App SDK 2.0 release notes

[!INCLUDE [wasdk-releasenotes](../../../includes/wasdk-release-notes.md)]

:::zone pivot="stable"

**There are no stable releases yet.**

:::zone-end

:::zone pivot="preview"

## Version 2.0 Preview 2 (2.0.0-Preview2)

Released: **TBD** <br><br>

### Windows App SDK 2.0.0-Preview2 Release Notes
 
---

<details><summary>Bootstrapper SemVer Support</summary>

> * Updated the Bootstrapper to support Windows App SDK 2.x SemVer versioning. The Bootstrapper now correctly handles the new major version numbering, ensuring proper package resolution and deployment for 2.x applications.
>
</details>

<details><summary>WinUI Improvements</summary>

> * Added drag support for WebView2 controls, enabling drag operations within WebView2-hosted content.
> * New `IXAMLCondition` API changes for XAML conditional support, allowing more flexible conditional XAML scenarios.
>
</details>

<details><summary>Windows ML</summary>

> * New Ask Mode payload, enabling new ML inference scenarios.
> * Added Flat C Python binding for Windows ML, enabling Python interoperability for native ML workloads.
> * Improved standalone NuGet package support, including dependency management for standalone scenarios.
> * Updated execution provider catalogs and artifacts for 4D, expanding hardware acceleration options.
> * Changed OpenVINO execution provider package source to the Microsoft Store for improved distribution.
>
</details>

<details><summary>Telemetry Improvements</summary>

> * Added `IsPackagedProcess` and `IsSelfContained` flags to common telemetry PartB fields, improving diagnostic capabilities for packaged and self-contained deployment scenarios.
>
</details>

<details><summary>Bug Fixes</summary>

> * Fixed SharedMemory redirection queue to properly handle queued items and added telemetry events for improved diagnostics. For more info, see GitHub PR [#6204](https://github.com/microsoft/WindowsAppSDK/pull/6204).
> * Fixed an issue in WinUI where the `FocusedIndex` in Selector controls could go beyond the range of items, causing unexpected behavior.
> * Fixed an issue in WinUI where focus could incorrectly move into a hidden WebView2 control. The framework now checks visibility before transferring focus.
> * Fixed an issue where WinML binaries were not copied to the output when `WindowsAppSDKSelfContained` was not set to `true`, breaking WinML standalone C# scenarios.
>
</details>

## Version 2.0 Preview 1 (2.0.0-Preview1)

Released: **February 13, 2026** <br><br>

### Windows App SDK 2.0.0-Preview1 Release Notes
 
---

<details><summary>Windows ML</summary>

> * The version of ONNX Runtime in Windows ML has been updated to 1.24 RC
> * Fixed a bug where if RegisterCertifiedAsync is called again in the same process, it incorrectly returns 0 execution providers (EP)
>
</details>

<details><summary>Microsoft.UI.Content</summary>

> * New `InputFocusController.ShouldShowKeyboardCues` property to guide developers on whether to show keyboard cues right after the creation of a `ContentIsland`.
> * New convenience API `PointerPoint.GetCurrentPoint`, to allow developers to get the active `PointerPoint` data from the provided `pointerId`.
>
</details>

<details><summary>App Content Search</summary>

> * Improved `DeleteIndex` reliability. Sometimes `DeleteIndex` would fail with ERROR_SHARING_VIOLATION.
> * Fix for OCR Bounding boxes returning negative values in some edge cases.
> * App Content Search are part of a separate `Microsoft.Windows.Search` package instead of being part of `Microsoft.Windows.AI package`.
> * Fix for prefix search not working with short query strings.
> 
> * New APIs and renames based on official API review.
>   * AppContentIndexer
>     * GetContentItemsRequiringReindexing
>     * Remove -> RemoveContentItem
>     * RemoveMultiple -> RemoveContentItems
>     * RemoveAllContentItems
>     * GetContentIndexingStatus -> GetContentItemStatus
>     * GetMultipleContentIndexingStatus -> GetContentItemStatuses
>     * GetContentItems    
>   * AppContentIndexListener
>     * IndexingStatusChanged -> ContentItemStatusChanged                
>   * AppIndexTextQuerySession
>     * UpdateQuery -> UpdateQueryPhrase
>     * MostRecentResult -> GetResult
>     * MostRecentResultChanged -> ResultChanged
>   * AppIndexImageQuerySession
>     * UpdateQuery -> UpdateQueryPhrase
>     * MostRecentResult -> GetResult
>     * MostRecentResultChanged -> ResultChanged
>   * AppManagedImageQueryMatch
>     * Subregion -> RegionOfInterest      
>   * ContentItemStatusResult
>     * ReindexingStatus   * ContentItemReindexingStatus
>   * TextQuerySessionResult
>     * IsValid
>   * ImageQuerySessionResult
>     * IsValid  
>   * QueryContentItemsFilterFlags   
>
</details>

<details><summary>Bug Fixes</summary>

> * Fixed an issue where the WindowsAppSDK installer showed no progress during installation, making it appear stalled. The installer now provides clearer progress feedback.
> * Improved error handling of scenarios where WindowsAppSDKSelfContained is enabled for class libraries.
>
</details>
 
<details><summary>New or updated APIs</summary>

>
> This release includes the following new and modified experimental APIs compared to 1.8.5:
> 
> ---
> ```
> Microsoft.Graphics.Imaging
> 
>     ImageBufferPixelFormat
>         Bgr8
> ```
> ```
> Microsoft.UI.Content
> 
>     DesktopPopupSiteBridge
>         AnchoringBehavior
>         AnchoringPixelAlignment
> 
>     PopupAnchor
> ```
> ```
> Microsoft.UI.Input
> 
>     InputFocusController
>         ShouldShowKeyboardCues
> 
>     PointerPoint
>         GetCurrentPoint
> ```
> ```
> Microsoft.UI.Xaml.Automation.Peers
> 
>     SplitMenuFlyoutItemAutomationPeer
> ```
> ```
> Microsoft.UI.Xaml.Controls
> 
>     SplitMenuFlyoutItem
>     SystemBackdropElement
> ```
> ```
> Microsoft.Windows.Management.Deployment
> 
>     AddPackageOptions
>         GetValidationEventSourceForUri
>         IsPackageValidationSupported
>         PackageValidators
> 
>     IPackageValidator
>     PackageCertificateEkuValidator
>     PackageFamilyNameValidator
>     PackageMinimumVersionValidator
>     PackageValidationEventArgs
>     PackageValidationEventSource
>     PackageValidationHandler
>     PackageVolume
>         AddAsync
>         GetAvailableSpaceAsync
>         GetDefault
>         GetPackageVolumeByName
>         GetPackageVolumeByPath
>         IsFeatureSupported
>         IsOffline
>         RemoveAsync
>         SetDefault
>         SetOfflineAsync
>         SetOnlineAsync
> 
>     PackageVolumeFeature
>     StagePackageOptions
>         GetValidationEventSourceForUri
>         IsPackageValidationSupported
>         PackageValidators
> ```
> ```
> Microsoft.Windows.Search.AppContentIndex
> 
>     AppContentIndexContract
>     AppContentIndexer
>     AppContentIndexListener
>     AppIndexContentRegion
>     AppIndexImageQuery
>     AppIndexImageQuerySession
>     AppIndexQueryMatch
>     AppIndexTextQuery
>     AppIndexTextQuerySession
>     AppIndexTextStreamEncoding
>     AppManagedImageQueryMatch
>     AppManagedIndexableAppContent
>     AppManagedTextQueryMatch
>     ContentItemErrorDetail
>     ContentItemReader
>     ContentItemReindexingStatus
>     ContentItemStatus
>     ContentItemStatusResult
>     ContentRegionTextOptions
>     DeleteIndexResult
>     DeleteIndexStatus
>     DeleteIndexWhileInUseBehavior
>     GetOrCreateIndexOptions
>     GetOrCreateIndexResult
>     GetOrCreateIndexStatus
>     ImageQueryMatch
>     ImageQueryOptions
>     ImageQuerySessionResult
>     IndexableAppContent
>     IndexCapabilities
>     IndexCapabilitiesOfCurrentSystem
>     IndexCapability
>     IndexCapabilityInitializationStatus
>     IndexCapabilityLanguageStatus
>     IndexCapabilityOfCurrentSystemStatus
>     IndexCapabilityRequirement
>     IndexCapabilityState
>     IndexStatistics
>     QueryContentItemsFilterFlags
>     QueryMatchContentKind
>     QueryMatchScope
>     RegionContentKind
>     TextLexicalMatchType
>     TextQueryMatch
>     TextQueryOptions
>     TextQuerySessionResult
> ```
> ```
> Microsoft.Windows.SemanticSearch
> 
>     EmbeddingVector
>     SemanticSearchContract
> ```
> ```
> Microsoft.Windows.Storage.Pickers
> 
>     FileOpenPicker
>         FileTypeChoices
>         InitialFileTypeIndex
>         SettingsIdentifier
>         SuggestedFolder
>         SuggestedStartFolder
>         Title
> 
>     FileSavePicker
>         InitialFileTypeIndex
>         SettingsIdentifier
>         ShowOverwritePrompt
>         SuggestedStartFolder
>         Title
> 
>     FolderPicker
>         PickMultipleFoldersAsync
>         SettingsIdentifier
>         SuggestedFolder
>         SuggestedStartFolder
>         Title
> ```
> ```
> Microsoft.Windows.Vision
> 
>     ScreenRegionBoundingBox
>     ScreenRegionDetectionContract
>     ScreenRegionLabel
> ```
>
</details>

:::zone-end

:::zone pivot="experimental"

## Version 2.0 Experimental 6 (2.0.0-Experimental6)

Released: **March 13, 2026** <br><br>

<details><summary>Windows ML CMake support</summary>

>
> Windows ML can now be used from C++ projects using CMake. See the [Get started page](/windows/ai/new-windows-ml/get-started) to learn more.
>

</details>

<details><summary>Updated ONNX Runtime</summary>

>
> The version of ONNX Runtime has been updated to 1.24.2. See [ONNX Runtime versions](/windows/ai/new-windows-ml/onnx-versions) for more info.
>

</details>

<details><summary>Windows AI Language Model</summary>

>
> * `NPUPowerMode` has been deprecated in favor of newer power management APIs.
> * `GetReadyState` now gracefully returns `NotSupported` when the session broker is unavailable, instead of throwing an exception.
>

</details>

<details><summary>Video Super Resolution</summary>

>
> * Added NV12 output format support for Video Super Resolution.
>

</details>

<details><summary>App Content Search</summary>

> * Improved `DeleteIndex` reliability. Sometimes `DeleteIndex` would fail with ERROR_SHARING_VIOLATION.
> * Fix for OCR Bounding boxes returning negative values in some edge cases.
> * App Content Search are part of a separate `Microsoft.Windows.Search` package instead of being part of `Microsoft.Windows.AI package`.
> * Fix for prefix search not working with short query strings.
>
> * New APIs and renames based on official API review.
>   * AppContentIndexer
>     * GetContentItemsRequiringReindexing
>     * Remove -> RemoveContentItem
>     * RemoveMultiple -> RemoveContentItems
>     * RemoveAllContentItems
>     * GetContentIndexingStatus -> GetContentItemStatus
>     * GetMultipleContentIndexingStatus -> GetContentItemStatuses
>     * GetContentItems
>   * AppCOntentIndexListener
>     * IndexingStatusChanged -> ContentItemStatusChanged
>   * AppIndexTextQuerySession
>     * UpdateQuery -> UpdateQueryPhrase
>     * MostRecentResult -> GetResult
>     * MostRecentResultChanged -> ResultChanged
>   * AppIndexImageQuerySession
>     * UpdateQuery -> UpdateQueryPhrase
>     * MostRecentResult -> GetResult
>     * MostRecentResultChanged -> ResultChanged
>   * AppManagedImageQueryMatch
>     * Subregion -> RegionOfInterest
>   * ContentItemStatusResult
>     * ReindexingStatus * ContentItemReindexingStatus
>   * TextQuerySessionResult
>     * IsValid
>   * ImageQuerySessionResult
>     * IsValid
>   * QueryContentItemsFilterFlags
>

</details>

<details><summary>Bug fixes</summary>

>
> * Fixed `ImageDescription.DescribeAsync` failing with `InternalError` due to a race condition.
>

</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new and modified experimental APIs compared to 2.0.0-experimental5:
> 
> ```
> Microsoft.Windows.Search.AppContentIndex
> 
>     AppContentIndexer
>         GetContentItems
>         GetContentItemStatus
>         GetContentItemStatuses
>         RemoveAllContentItems
>         RemoveContentItem
>         RemoveContentItems
> 
>     AppContentIndexListener
>         ContentItemStatusChanged
> 
>     AppManagedImageQueryMatch
>     ContentItemErrorDetail
>         IndexCorruption
> 
>     ContentItemReindexingStatus
>         Unspecified
> 
>     QueryContentItemsFilterFlags
> ```
>
</details>

## Version 2.0 Experimental 5 (2.0.0-Experimental5)

Released: **February 13, 2026** <br><br>

<details><summary>Windows ML Licensing Simplification</summary>

>
> * Simplified license agreement with clearer terms for ISVs building applications with Windows ML.
> * Restructured sections covering Installation, Data, and Distributable Code with enhanced clarity.
> * New Execution Provider (EP) Compliance Notice section clarifies developer responsibilities regarding hardware-accelerated execution providers.
> * Direct links to vendor license agreements for NVIDIA TensorRT, Intel OpenVINO, and Qualcomm QNN (Qualcomm Neural Network SDK).
>
</details>

## Version 2.0 Experimental 4 (2.0.0-Experimental4)

Released: **January 13, 2026** <br><br>

<details><summary>ONNX Runtime Alignment and Execution Provider Stability</summary>

>
> * Windows ML experimental builds align with ONNX Runtime mainline version 1.24 to improve compatibility and stability.
> * Stable Application Binary Interface (ABI) execution providers are enforced to ensure predictable behavior across devices, with non-stable execution providers excluded from acquisition.
>   * Only the OpenVINO execution provider is supported, with additional execution providers to be added over time as they meet stability requirements.
> * The size of Microsoft.Windows.AI.MachineLearning.dll was reduced by approximately 160 KB.
>
</details>

<details><summary>App Content Search</summary>

> 
> *  New experimental APIs to lookup statistics of items being indexed. This provides visibility into what is happening in the index.
>  * Support for Query cancellation to allow apps to run a query and update it if there is a new query before the current one completes. This allows responding faster to the query as a user types in the query string.
> * Prefix matching support for short strings to improve results as user is typing.
> 
</details>

<details><summary>Video Super Resolution Improvements</summary>

>
> * VideoScaler is now disposable, improving resource management and lifecycle control.
> * The VSR model is now compiled and cached to improve performance and reduce repeated initialization costs.
> * A capability check has been added to validate VSR support before use.
> * Explicit WinML initialization is no longer required when using Video Super Resolution.
</details>


<details><summary>WinUI FlowLayout Spacing improvements</summary>

>
> * The `FlowLayout` control now uses ItemSpacing and LineSpacing terminology instead of horizontal and vertical spacing properties. The aligned naming improves clarity and consistency with modern layout patterns across UI frameworks.
</details>


<details><summary>WinUI WrapPanel improvements</summary>

>
> * The `WrapPanel` control now uses ItemSpacing and LineSpacing nomenclature instead of the previous horizontal and vertical spacing properties.
</details>

<details><summary>Custom XAML Predicates and IXamlPredicate Integration</summary>

>
> * Implemented the `IXamlPredicate` interface to define custom predicates that integrate seamlessly with XAML's conditional namespace syntax and are evaluated at XAML parse time.
> * Custom predicates enable conditional XAML scenarios based on application-specific factors such as: 
>   * Feature flags
>   * Device capabilities
>   * Business logic
>   * Configuration settings
>   * Other runtime conditions
</details>

<details><summary>WinUI Open-Source Enhancements</summary>

>
> * Updated packages to enable external usability, which includes changes to:
>   * Microsoft.UI.DCPP.Dependencies.Minimal
>   * Microsoft.UI.DCPP.Dependencies.Edge
>   * ExpPointerPointStatics
> * Added build support for Visual Studio 2026
> * Introduced tools and scripts for use by external developers to build and test WinUI.
>
</details>

<details><summary>WinUI API deprecation and rename</summary>

>
> - [deprecated] DependencyObject.Dispatcher 
> - [deprecated] Window.Current 
> - [deprecated] FocusManager.GetFocusedElement 
> - [renamed] SystemBackdropHost to SystemBackdropElement
</details>


<details><summary>WinAI API Namespace rename</summary>

>
> - [renamed] `Microsoft.Windows.AI.Search.Experimental.AppContentIndex` to `Microsoft.Windows.Search.AppContentIndex`
</details>


<details><summary>Bug fixes</summary>

>
> * Fixed "Class not registered" errors when using self-contained deployment with Windows ML. Developers using self-contained deployment no longer need to register all the Foundation package activatable classes that were used internally.
> * Fixed a potential crash occurring on process shutdown when using Windows ML.
> * Fixed the `ImageForegroundExtractor` API routing path so calls reach the correct endpoint.
> * Ensured execution provider install and download progress is correctly forwarded to apps during package deployment.
> * Fixed a crash in `SystemBackdrop` when the target disconnects by guarding invalid disconnection paths.
> * Fixed an issue that prevented Windows AI APIs from being available for some applications.
</details>


<details><summary>New or updated APIs</summary>

>
> This release includes the following new and modified experimental APIs compared to 2.0.0-experimental3:
> 
> ```
> Microsoft.UI.Content
> 
>     ChildSiteLink
>         IsHitTestVisible
> ```
> ```
> Microsoft.UI.Xaml.Controls
> 
>     FlowLayout
>         ActualItemSpacing
>         ActualLineSpacing
>         ItemSpacing
>         LineSpacing
> 
>     FlowLayoutLineAlignment
>     WrapPanel
>         ItemSpacing
>         LineSpacing
>     
>     XamlNamespace
>         XamlCondition
> ```
> ```
> Microsoft.UI.Xaml.Markup
> 
>     IXamlPredicate
> ```
> ```
> Microsoft.Windows.AI.Imaging
> 
>     ImageDescriptionContract
>         (no longer present)
> 
>     ImageObjectExtractorContract
>         (no longer present)
> ```
> ```
> Microsoft.Windows.AI.Text
> 
>     TextIntelligenceContract
>         (no longer present)
> ```
> ```
> Microsoft.Windows.Management.Deployment
> 
>     AddPackageOptions
>         GetValidationEventSourceForUri
>         IsPackageValidationSupported
>         PackageValidators
> 
>     IPackageValidator
>     PackageCertificateEkuValidator
>     PackageFamilyNameValidator
>     PackageMinimumVersionValidator
>     PackageValidationEventArgs
>     PackageValidationEventSource
>     PackageValidationHandler
>     PackageVolume
>         AddAsync
>         GetAvailableSpaceAsync
>         GetDefault
>         GetPackageVolumeByName
>         GetPackageVolumeByPath
>         IsFeatureSupported
>         IsOffline
>         RemoveAsync
>         SetDefault
>         SetOfflineAsync
>         SetOnlineAsync
> 
>     PackageVolumeFeature
>     StagePackageOptions
>         GetValidationEventSourceForUri
>         IsPackageValidationSupported
>         PackageValidators
> ```
> ```
> Microsoft.Windows.Search.AppContentIndex
> 
>     AppContentIndexContract
>     AppContentIndexer
>     AppContentIndexListener
>     AppIndexContentRegion
>     AppIndexImageQuery
>     AppIndexImageQuerySession
>     AppIndexQueryMatch
>     AppIndexTextQuery
>     AppIndexTextQuerySession
>     AppIndexTextStreamEncoding
>     AppManagedImageQueryMatch
>     AppManagedIndexableAppContent
>     AppManagedTextQueryMatch
>     ContentItemErrorDetail
>     ContentItemReader
>     ContentItemReindexingStatus
>     ContentItemStatus
>     ContentItemStatusResult
>     ContentRegionTextOptions
>     DeleteIndexResult
>     DeleteIndexStatus
>     DeleteIndexWhileInUseBehavior
>     GetOrCreateIndexOptions
>     GetOrCreateIndexResult
>     GetOrCreateIndexStatus
>     ImageQueryMatch
>     ImageQueryOptions
>     ImageQuerySessionResult
>     IndexableAppContent
>     IndexCapabilities
>     IndexCapabilitiesOfCurrentSystem
>     IndexCapability
>     IndexCapabilityInitializationStatus
>     IndexCapabilityLanguageStatus
>     IndexCapabilityOfCurrentSystemStatus
>     IndexCapabilityRequirement
>     IndexCapabilityState
>     IndexStatistics
>     QueryContentItemsFilterFlags
>     QueryMatchContentKind
>     QueryMatchScope
>     RegionContentKind
>     TextLexicalMatchType
>     TextQueryMatch
>     TextQueryOptions
>     TextQuerySessionResult
> ```
> ```
> Microsoft.Windows.SemanticSearch
> 
>     EmbeddingVector
>     SemanticSearchContract
> ```
> ```
> Microsoft.Windows.Storage.Pickers
> 
>     FileOpenPicker
>         FileTypeChoices
>         InitialFileTypeIndex
>         SettingsIdentifier
>         SuggestedFolder
>         SuggestedStartFolder
>         Title
> 
>     FileSavePicker
>         InitialFileTypeIndex
>         SettingsIdentifier
>         ShowOverwritePrompt
>         SuggestedStartFolder
>         Title
> 
>     FolderPicker
>         PickMultipleFoldersAsync
>         SettingsIdentifier
>         SuggestedFolder
>         SuggestedStartFolder
>         Title
> ```
> ```
> Microsoft.Windows.Vision
> 
>     ScreenRegionBoundingBox
>     ScreenRegionDetectionContract
>     ScreenRegionLabel
> ```
>
</details>

## Version 2.0 Experimental 3 (2.0.0-Experimental3)

Released: **November 6, 2025** <br><br>

<details><summary>SplitMenuFlyoutItem</summary>

>
> The new `SplitMenuFlyoutItem` control extends the standard menu flyout item with the ability to have a split button that provides both a default action and a secondary flyout menu.
>
</details>

<details><summary>SystemBackdrop</summary>

>
> The new `SystemBackdropElement` control (renamed from `SystemBackdropHost`) allows you to use system backdrop materials in your XAML tree declaratively. This is a simpler alternative to having to handle it in your Window code-behind.
>
</details>

<details><summary>WinUI API deprecation</summary>

>
> - [deprecated] DependencyObject.Dispatcher 
> - [deprecated] Window.Current 
> - [deprecated] FocusManager.GetFocusedElement 
> - [renamed] SystemBackdropHost to SystemBackdropElement

</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new and modified experimental APIs compared to 2.0.0-experimental2:
>
> ```
> Microsoft.UI.Xaml.Automation.Peers
> 
>     SplitMenuFlyoutItemAutomationPeer
> ```
> ```
> Microsoft.UI.Xaml.Controls
> 
>     SplitMenuFlyoutItem
>     SystemBackdropElement
> ```
>
</details>

## Version 2.0 Experimental 2 (2.0.0-Experimental2)

Released: **September 22, 2025** <br><br>

<details><summary>Storage Pickers</summary>

>
> Storage picker APIs allow unpackaged and packaged Win32 desktop apps to pick files and folders.
> For more info, see [Storage Pickers](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers). 
>
</details>

<details><summary>Windows AI</summary>

>
> New AI APIs are available in the Windows App SDK that bring new capabilities to developers, including:
> * **Image Description** – Generate textual descriptions of images. See [Image Description](/windows/ai/apis/imaging#how-do-i-use-image-description) for more info.
> * **Image Segmentation** – Extract foreground from images. See [Image Segmentation](/windows/ai/apis/imaging#how-do-i-use-image-segmentation) for more info.
> * **Image Super Resolution** – Upscale and enhance images. See [Image Super Resolution](/windows/ai/apis/imaging#how-do-i-use-image-super-resolution) for more info.
> * **Text Summarization** – Produce summaries of text. See [Text Summarization](/windows/ai/apis/text-summarization) for more info.
> * **Text Rewrite** – Rewrite text. See [Text Rewrite](/windows/ai/apis/text-rewrite) for more info.
> * **Semantic Search** - Do a semantic search on a set of texts or images. See [Semantic Search](/windows/ai/apis/semantic-search) for more info.
>
</details>

<details><summary>Windows ML</summary>

>
> Windows ML enables developers to integrate machine learning right into their apps, with built-in ONNX Runtime, execution provider management, and seamless hardware acceleration.
> For more info, see [What is Windows ML?](/windows/ai/new-windows-ml/overview)
>
</details>

<details><summary>Video Super Resolution</summary>

>
> The Video Super Resolution (VSR) API enhances video playback quality by using machine learning models to upscale low-resolution video frames in real time.
>
</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new and modified experimental APIs:
>
> ```
> Microsoft.Graphics.Imaging
> 
>     ImageBuffer
>     ImageBufferPixelFormat
> ```
> ```
> Microsoft.UI.Content
> 
>     DesktopPopupSiteBridge
>         AnchoringBehavior
>         AnchoringPixelAlignment
> 
>     PopupAnchor
> ```
> ```
> Microsoft.UI.Input
> 
>     InputFocusController
>         ShouldShowKeyboardCues
> 
>     PointerPoint
>         GetCurrentPoint
> ```
> ```
> Microsoft.Windows.AI.Imaging
> 
>     ImageDescriptionContract
>     ImageDescriptionGenerator
>     ImageDescriptionKind
>     ImageDescriptionResult
>     ImageDescriptionResultStatus
>     ImageObjectExtractorContract
>     ImageObjectExtractorHint
>     ImageObjectExtractorPreview
>     ImageSuperResolutionContract
>     ImageSuperResolutionPreview
> ```
> ```
> Microsoft.Windows.AI.Text
> 
>     TextIntelligenceContract
>     TextRewriter
>     TextRewriterFormat
>     TextRewriterTone
>     TextSummarizer
>     TextSummarizerFormat
> ```
> ```
> Microsoft.Windows.SemanticSearch
> 
>     EmbeddingVector
>     SemanticSearchContract
> ```
> ```
> Microsoft.Windows.Storage.Pickers
> 
>     FileOpenPicker
>     FileSavePicker
>     FolderPicker
> ```
> ```
> Microsoft.Windows.Vision
> 
>     ScreenRegionBoundingBox
>     ScreenRegionDetectionContract
>     ScreenRegionLabel
> ```
>
</details>

:::zone-end
