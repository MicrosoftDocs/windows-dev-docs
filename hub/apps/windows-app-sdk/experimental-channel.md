---
title: Latest experimental channel release notes for the Windows App SDK
description: Learn about the latest experimental releases of the Windows App SDK.
ms.topic: release-notes
ms.date: 09/08/2025
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.localizationpriority: medium
---

# Latest experimental channel release notes for the Windows App SDK

> [!IMPORTANT]
> The experimental channel is **not supported** for use in production environments, and apps that use the experimental releases cannot be published to the Microsoft Store.

The experimental channel includes releases of the Windows App SDK with [experimental channel features](release-channels.md#features-available-by-release-channel) in early stages of development. APIs for experimental features have the [Experimental](/uwp/api/Windows.Foundation.Metadata.ExperimentalAttribute) attribute. If you call an experimental API in your code, you will receive a build-time warning. All APIs in the experimental channel are subject to extensive revisions and breaking changes. Experimental features and APIs may be removed from subsequent releases at any time.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).
- For documentation on experimental releases, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md).

## Version 2.0 Experimental (2.0.0-Experimental3)

### Use on-device AI with Windows AI APIs

> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.

The Windows App SDK incorporates advanced Windows AI capabilities, enabling developers to seamlessly integrate intelligent features into their applications. These enhancements include local AI functionalities such as responding to incoming prompts, recognizing text within images, describing image contents, extracting objects from pictures, and more.


For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.

This release includes the following new and modified experimental APIs:

### Bug Fixes:

* Fixed bounding box calculation when text is rotated. In some circumstances, the OCR text matching within images reported inaccurate or empty region bounds when the text was rotated.


### Windows ML Updates

#### Renamed Types

* Renamed `WinMLCatalogModel` to `ModelCatalog`
* Renamed `CatalogModelSource` to `ModelCatalogSource`

#### Method Updates

* `CatalogModelInfo`: Renamed `GetInstance` to `GetInstanceAsync`
* `ModelCatalogSource`: Renamed `CreateFromUri` to `CreateFromUriAsync`
* `ModelCatalog`: Renamed `FindModel` to `FindModelAsync`
* `ModelCatalog`: Renamed `FindAllModels` to `FindAllModelsAsync`


#### Property Changes

* Updated `CatalogModelInfo.Size` to `CatalogModelInfo.ModelSizeInBytes`

#### Behavior Updates

* Retrieve instance from `CatalogModelInstanceResult` using .GetInstance()
* `CatalogModelStatus` now returns Ready or NotReady based on local availability
* Added `CatalogModelInstanceStatus` to separate instance status from model status

#### CatalogModelInfo Enhancements

* Renamed `Alias` to `Name`
* Renamed `Revision` to `Version`
* Added `Publisher`
* Removed `DisplayName`

#### JSON Changes

* Renamed `alias` to `id`.
* Removed `modelType` and `description`.
* Renamed `executionProvider` to `executionProviders`.
* Updated `executionProviders` to be an array of JSON objects instead of a comma-separated list.

#### Additional Changes


* `ModelCatalog` now returns a list of Execution Providers (EPs) when an instance is created.
* Added support for Windows 10 (1809) and above.
* Added support for local files, including both regular files and MSIX packages.
* Fixed crashes caused by invalid catalog JSON.

### AppContentIndexer Updates

* The previous `AppIndexQuery` type, which included `GetNextTextMatches` and `GetNextImageMatches` methods, has been split into two distinct types: `AppIndexTextQuery` and `AppIndexImageQuery`. The `AppContentIndexer.CreateQuery` method has been replaced with: `CreateTextQuery` and `CreateImageQuery`.

* These methods now return `AppIndexTextQuery` and `AppIndexImageQuery` respectively. To simplify usage, the options types have also been updated:

    * Removed: `AppIndexQueryOptions`, `TextMatchOptions` and `ImageMatchOptions`
    * Added: `TextQueryOptions` and `ImageQueryOptions`

* The APIs in the `AppContentIndex` namespace that previously returned arrays now return `IVectorView` for improved consistency and performance.


* The `AppContentIndexer.WaitForIndexingIdleAsync` method has been updated to accept a `TimeSpan` parameter instead of an integer, providing clearer and more flexible timeout handling.

### Video Super Resolution AI API

The `VideoScaler` API delivers real-time video enhancement through advanced AI upscaling, optimized for streams featuring people in conversation. It enables developers to provide sharper, clearer visuals across conferencing, streaming, and editing platforms, even under poor network conditions. The API supports customization of output resolution, frame rate, and regions of interest, with compatibility for multiple video formats including BGR, RGB, and NV12.

### Windows AI Text Rewriter Tone

The new RewriteCustomAsync API lets you provide an input string that guides Phi Silica in rewriting selected text. You can experiment with new creative styles like "Goofy" or "Pirate" to instantly transform your content.


### New split menu item for condensed functionality

The new experimental SplitMenuFlyoutItem control is designed to provide a split button experience within a menu flyout. This control will enable developers to expose a default primary action while also offering additional options through a submenu, ideal for condensing complex functionality into a smaller footprint and saving overall menu length.

Along with the capabilities of MenuFlyoutItem and MenuFlyoutSubItem, the control comes with two other properties : `SubMenuPresenterStyle` and `SubMenuItemStyle`, which allows the customization of the submenu, like using GridView for the submenu presenter.

```
    <Button Text="Open File Button">
        <Button.Flyout>
            <MenuFlyout>
                <SplitMenuFlyoutItem Text="Open with Notepad">
                    <MenuFlyoutItem Text="Visual Studio" />
                    <MenuFlyoutItem Text="VS Code" />
                    <MenuFlyoutItem Text="Wordpad" />
                </SplitMenuFlyoutItem>
            </MenuFlyout>
        </Button.Flyout>
    </Button>
```

### AI image generation

The `ImageGenerator` class leverages Stable Diffusion models to provide powerful image generation capabilities. It supports multiple generation scenarios:

- **Text-to-Image:** Generate images from descriptive text prompts.  
- **Image-to-Image:** Transform existing images based on text descriptions.  
- **Magic Fill:** Fill masked regions of images with AI-generated content.  
- **Coloring Book Style:** Generate coloring-book-style images.  

- **Restyle:** Change the artistic style of existing images while preserving structure.

All generated images are returned in **RGB8** format through [ImageBuffer](/windows/windows-app-sdk/api/winrt/microsoft.graphics.imaging.imagebuffer) objects. The API includes built-in [content safety filters](/azure/ai-services/content-safety/) and supports customizable generation parameters.

#### Basic Text-to-Image Generation

```csharp
using Microsoft.Windows.AI.Imaging;
using Microsoft.Graphics.Imaging;
 
public async Task GenerateImageFromText()
{
    var readyState = ImageGenerator.GetReadyState();
    if (readyState != AIFeatureReadyState.Ready)
    {
        var progress = new Progress<double>(p => Console.WriteLine($"Download progress: {p:P}"));
        var result = await ImageGenerator.EnsureReadyAsync();
        if (result.Status != AIFeatureReadyResultState.Success)
        {
            Console.WriteLine("Failed to prepare models");
            return;
        }
    }
 
    using var generator = await ImageGenerator.CreateAsync();
 
    var options = new ImageGenerationOptions
    {
        MaxInferenceSteps = 6,
        Creativity = 0.8,
        Seed = 42
    };
 
    var result = generator.GenerateImageFromTextPrompt("A beautiful sunset over a mountain lake", options);
 
    if (result.Status == ImageGeneratorResultStatus.Success)
    {
        await SaveImageBufferAsync(result.Image, "generated_image.png");
    }
}
```
### New APIs for 2.0-experimental3

This release includes the following new and modified experimental APIs compared to 2.0-experimental2:


```
Microsoft.Graphics.Imaging

    ImageBufferPixelFormat
        Bgr8
```
```
Microsoft.UI.Xaml.Automation.Peers

    SplitMenuFlyoutItemAutomationPeer
```
```
Microsoft.UI.Xaml.Controls

    SplitMenuFlyoutItem
```
```
Microsoft.Windows.AI.Imaging

    ImageFromImageGenerationOptions
    ImageFromImageGenerationStyle
    ImageFromTextGenerationOptions
    ImageFromTextGenerationStyle
    ImageGenerationOptions
    ImageGenerator
    ImageGeneratorContract
    ImageGeneratorResult
    ImageGeneratorResultStatus
    TextRecognizer
        RecognizeTextFromImage
        RecognizeTextFromImageAsync

    TextRecognizerOptions
```
```
Microsoft.Windows.AI.MachineLearning

    CatalogModelInfo
    CatalogModelInstance
    CatalogModelInstanceResult
    CatalogModelInstanceStatus
    CatalogModelStatus
    ModelCatalog
    ModelCatalogSource
```
```
Microsoft.Windows.AI.Search.Experimental.AppContentIndex

    AppContentIndexer
        CreateImageQuery
        CreateTextQuery
        WaitForIndexingIdleAsync

    AppIndexImageQuery
    AppIndexTextQuery
    ImageQueryOptions
    TextQueryOptions
```
```
Microsoft.Windows.AI.Text

    TextRewriter
        RewriteCustomAsync
```
```
Microsoft.Windows.AI.Video

    ScaleFrameStatus
    VideoScaler
    VideoScalerOptions
    VideoScalerResult
```

## Version 2.0 Experimental (2.0.0-Experimental2)

> [!IMPORTANT]
> If you previously installed Windows App SDK 2.0 Experimental 1, follow the [NuGet Uninstall](/nuget/consume-packages/install-use-packages-visual-studio#uninstall-a-package) guide to remove the `Microsoft.WindowsAppSDK` NuGet Metapackage with version `2.0.250930001-experimental1` from your project and the associated WinAppSDK component packages from that release before trying this new version, since the previously released Experimental package has a higher version number than the current one.


### App Content Search

The AppContentIndexer APIs empower developers to efficiently index app content, including text and images for rapid and relevant retrieval. Supporting both lexical (keyword-based) and semantic (meaning-based) searches, these APIs allow apps to deliver fast, relevant results based on user intent and context rather than just exact keywords.

This capability unlocks the following advanced scenarios:

- **Semantic Search**  
  Apps can return results based on intent and meaning rather than exact keyword matches.  
  *Example:* A query for **“project timeline”** can surface content that mentions **“schedule”** or **“delivery dates,”** even if those exact words weren’t used.
- **Retrieval-Augmented Generation (RAG)**  
  Indexed content can serve as a knowledge base for generative AI models. When a user asks a question, the app retrieves the most relevant documents or snippets from its index and feeds them into the model, enabling accurate, context-aware responses grounded in real data.

### Windows ML Model Catalog

The Windows ML Model Catalog APIs enable your app or library to dynamically discover and download large AI model files from your own online model catalogs, eliminating the need to package these large files directly with your app or library. The model catalog helps ensure device compatibility by filtering models and downloading only those applicable for the specific Windows device in use.

### Persistent File and Folder Locations
The latest `Microsoft.Windows.Storage.Pickers` update streamlines file and folder selection by letting developers set initial and persistent folder locations, and by grouping filetype filters with clear labels for easier navigation.

### Relative Popup Positioning

The `PopupAnchor` API now allows `DesktopPopupSiteBridge` to support relative positioning by anchoring to its owning window or island, addressing the limitation where popups could only be positioned absolutely using screen coordinates.

### Input Routing for SystemVisual ContentIslands

The `InputUnderlyingWindowController` API enables developers to designate the target HWND for receiving input messages that were originally sent to a ContentIsland created from a SystemVisual (see [ContentIsland.CreateForSystemVisual](/windows/windows-app-sdk/api/winrt/microsoft.ui.content.contentisland.createforsystemvisual)).

### Flexible System Backdrop Placement

`SystemBackdropHost` enables placing a system backdrop (acrylic/mica) anywhere within an application's visual tree.

### XAML Layout Sequential Positioning

The `WrapPanel` is a WinUI XAML layout panel that arranges child elements in a sequential position from left to right, items overflowing the line will break to the next line automatically at the end of the containing panel. It is useful for responsive layouts.

This is a port of the existing [Windows Community Toolkit control](/dotnet/communitytoolkit/windows/primitives/wrappanel).

### New APIs for 2.0-experimental2

This release includes the following new and modified experimental APIs compared to 2.0-experimental1:

```
Microsoft.UI.Content

    PopupAnchor
```
```
Microsoft.UI.Input

    InputUnderlyingWindowController
```
```
Microsoft.UI.Xaml.Controls

    StretchChild
    SystemBackdropHost
    WrapPanel
```
```
Microsoft.Windows.AI.Imaging

    ImageForegroundExtractor
    ImageForegroundExtractorContract
```
```
Microsoft.Windows.AI.Search.Experimental.AppContentIndex

    AppContentIndexContract
    AppContentIndexer
    AppContentIndexListener
    AppIndexContentRegion
    AppIndexQuery
    AppIndexQueryMatch
    AppIndexQueryOptions
    AppIndexTextStreamEncoding
    AppManagedImageQueryMatch
    AppManagedIndexableAppContent
    AppManagedTextQueryMatch
    ContentItemReader
    ContentItemStatus
    ContentItemStatusResult
    ContentRegionTextOptions
    DeleteIndexResult
    DeleteIndexStatus
    DeleteIndexWhileInUseBehavior
    GetOrCreateIndexOptions
    GetOrCreateIndexResult
    GetOrCreateIndexStatus
    ImageMatchOptions
    ImageQueryMatch
    IndexableAppContent
    IndexCapabilities
    IndexCapabilitiesOfCurrentSystem
    IndexCapability
    IndexCapabilityInitializationStatus
    IndexCapabilityLanguageStatus
    IndexCapabilityOfCurrentSystemStatus
    IndexCapabilityRequirement
    IndexCapabilityState
    IndexStatistics
    QueryMatchContentKind
    QueryMatchScope
    RegionContentKind
    TextLexicalMatchType
    TextMatchOptions
    TextQueryMatch
```
```
Microsoft.Windows.AI.Text.Experimental

    LanguageModelExperimental
    LanguageModelExperimentalContract
    LanguageModelOptionsExperimental
    LowRankAdaptation
```
```
Microsoft.Windows.Storage.Pickers

    FileOpenPicker
        FileTypeChoices
        SuggestedFolder
        SuggestedStartFolder

    FileSavePicker
        SuggestedStartFolder

    FolderPicker
        SuggestedFolder
        SuggestedStartFolder
```
### Known Issues

- `AppContentIndexer` APIs should be called from a background thread. Using it in the UI thread may hang or cause long pauses impacting the user experience.
- Query results using `AppIndexQuery.GetNextTextMatches` and `AppIndexQuery.GetNextImageMatches` will be null when there are no matches instead of an empty list.
- Image matches using `AppManagedImageQueryMatch.Subregion` based on OCR values may occasionally be inaccurate, particularly when the text is rotated or skewed.
- Image matches using `AppManagedImageQueryMatch.Subregion` may sometimes include zero-size or extremely small rectangles, leading to inaccurate results.
- An Empty query from `AppContentIndex.CreateQuery` can throw an exception.

## Version 2.0 Experimental (2.0.0-Experimental1)

### Use on-device AI with Windows AI APIs

> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.

The Windows App SDK incorporates advanced Windows AI capabilities, enabling developers to seamlessly integrate intelligent features into their applications. These enhancements include local AI functionalities such as responding to incoming prompts, recognizing text within images, describing image contents, extract objects from pictures, and more.

For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.

### Microsoft Windows ML 

The [Windows ML](/windows/ai/new-windows-ml/overview) Model Catalog APIs allow your app or library to dynamically download large AI model files from your own online model catalogs without shipping those large files directly with your app or library. Additionally, the model catalog will help filter which models are compatible with the Windows device it's running on, so that the right model is downloaded to the device.

**Key benefits:** 

- **Add catalogs**: Add one or many online catalogs  
- **Discover compatible models**: Automatically find models that work with the user's hardware and execution providers  
- **Download models**: Download and store models from various sources  
- **Share models across apps**: If multiple applications use the same catalog source, the models will be shared on disk without duplicating downloads  

### Bug Fixes
- Fixed an issue in DeploymentManager which resulted in it incorrectly reporting PackageInstallRequired in some cases.

### New APIs for 2.0-experimental1

This release includes the following new and modified experimental APIs:

```
Microsoft.UI.Composition

    CompositionNotificationDeferral
    CompositionProjectedShadow
        MaxOpacity
        MinOpacity
        OpacityFalloff

    CompositionProjectedShadowCaster
        AncestorClip
        Mask

    CompositionProjectedShadowDrawOrder
    CompositionProjectedShadowReceiver
        DrawOrder
        Mask
```
```
Microsoft.UI.Composition.Experimental

    ExpCompositionVisualSurface
    ExpExpressionNotificationProperty
    IExpCompositionPropertyChanged
    IExpCompositionPropertyChangedListener
    IExpCompositor
    IExpVisual
```
```
Microsoft.UI.Content

    ChildSiteLink
        IsBelowContent

    ContentAppWindowBridge
    ContentDisplayOrientations
    ContentExternalBackdropLink
    ContentExternalOutputLink
    ContentIsland
        Connected
        ConnectionInfo
        ConnectRemoteEndpoint
        Disconnected
        IsRemoteEndpointConnected
        Root

    ContentIslandEnvironment
        CurrentOrientation
        NativeOrientation
        ThemeChanged

    ContentSite
        TryGetAutomationProvider

    ContentSiteEnvironment
        CurrentOrientation
        NativeOrientation
        NotifyThemeChanged

    CoreWindowSiteBridge
    CoreWindowTopLevelWindowBridge
    DesktopChildSiteBridge
        AcceptRemoteEndpoint
        ConnectionInfo
        IsRemoteEndpointConnected
        RemoteEndpointConnecting
        RemoteEndpointDisconnected
        RemoteEndpointRequestedStateChanged

    DesktopPopupSiteBridge
        AnchoringBehavior
        AnchoringPixelAlignment

    DesktopSiteBridge
        TryCreatePopupSiteBridge

    EndpointConnectionEventArgs
    EndpointRequestedStateChangedEventArgs
    IContentIslandEndpointConnectionPrivate
    IContentSiteBridgeEndpointConnectionPrivate
    PopupAnchoringOptions
    PopupWindowSiteBridge
    ProcessStarter
    SystemVisualSiteBridge
```
```
Microsoft.UI.Input

    InputKeyboardSource
        GetForWindowId

    InputLayoutPolicy
    InputLightDismissAction
        GetForIsland

    InputLightDismissEventArgs
    InputPointerActivationBehavior
    InputPointerSource
        ActivationBehavior
        DirectManipulationHitTest
        GetForVisual
        GetForWindowId
        RemoveForVisual
        TouchHitTesting
        TrySetDeviceKinds

    InputPopupController
    LightDismissReason
    PopupPointerMode
    ProximityEvaluation
    TouchHitTestingEventArgs
```
```
Microsoft.UI.Input.Experimental

    ExpInputSite
    ExpPointerPoint
```
```
Microsoft.UI.Windowing

    AppWindow
        GetCurrentPlacement
        PersistedStateId
        PlacementRestorationBehavior
        SaveCurrentPlacement
        SaveCurrentPlacementForAllPersistedStateIds
        SetCurrentPlacement

    AppWindowPlacementDetails
    DisplayArea
        GetMetricsFromWindowId

    PlacementInfo
    PlacementRestorationBehavior
```
```
Microsoft.UI.Xaml

    XamlIsland
        ShouldConstrainPopupsToWorkArea
```
```
Microsoft.UI.Xaml.Automation.Peers

    AutomationEvents
        Notification

    InkCanvasAutomationPeer
    PagerControlAutomationPeer
```
```
Microsoft.UI.Xaml.Controls

    ContentDialogPlacement
        UnconstrainedPopup

    DoInkPresenterWork
    ElementFactory
    FlowLayout
    FlowLayoutAnchorInfo
    FlowLayoutLineAlignment
    FlowLayoutState
    IApplicationViewSpanningRects
    IndexPath
    InfoBar
        Opened

    InfoBarOpenedEventArgs
    InkCanvas
    ISelfPlayingAnimatedVisual
    ItemContainer
        CanUserInvoke
        CanUserInvokeProperty
        CanUserSelect
        CanUserSelectProperty
        ItemInvoked
        MultiSelectMode
        MultiSelectModeProperty

    ItemContainerInteractionTrigger
    ItemContainerInvokedEventArgs
    ItemContainerMultiSelectMode
    ItemContainerUserInvokeMode
    ItemContainerUserSelectMode
    LayoutPanel
    NumberBox
        InputScope
        InputScopeProperty
        TextAlignment
        TextAlignmentProperty

    PagerControl
    PagerControlButtonVisibility
    PagerControlDisplayMode
    PagerControlSelectedIndexChangedEventArgs
    PagerControlTemplateSettings
    ProgressRing
        DeterminateSource
        DeterminateSourceProperty
        IndeterminateSource
        IndeterminateSourceProperty

    RecyclePool
    RecyclingElementFactory
    ScrollingScrollStartingEventArgs
    ScrollingZoomStartingEventArgs
    ScrollView
        ScrollStarting
        ZoomStarting

    SelectionModel
    SelectionModelChildrenRequestedEventArgs
    SelectionModelSelectionChangedEventArgs
    SelectTemplateEventArgs
    StackLayout
        IsVirtualizationEnabled
        IsVirtualizationEnabledProperty

    StackLayoutState
    TeachingTip
        Opened

    TeachingTipOpenedEventArgs
    UniformGridLayoutState
```
```
Microsoft.UI.Xaml.Controls.Primitives

    ScrollPresenter
        ScrollStarting
        ZoomStarting
```
```
Microsoft.Windows.AI.MachineLearning

    CatalogModelInfo
    CatalogModelInstance
    CatalogModelInstanceResult
    CatalogModelSource
    CatalogModelStatus
    WinMLModelCatalog
```
```
Microsoft.Windows.ApplicationModel.WindowsAppRuntime

    DeploymentManager
        Repair

    DeploymentStatus
        PackageRepairFailed
```
```
Microsoft.Windows.AppNotifications

    AppNotification
        ConferencingConfig

    AppNotificationConferencingConfig
```
```
Microsoft.Windows.AppNotifications.Builder

    AppNotificationBuilder
        AddCameraPreview

    AppNotificationButton
        SetSettingStyle

    AppNotificationButtonSettingStyle
```
```
Microsoft.Windows.SemanticSearch

    EmbeddingVector
    SemanticSearchContract
```
```
Microsoft.Windows.Storage

    ApplicationData
        GetForUnpackaged
```
```
Microsoft.Windows.Vision

    ScreenRegionBoundingBox
    ScreenRegionDetectionContract
    ScreenRegionLabel
```

## Archive of experimental channel release notes

<details>

<summary>Expand for links to archived experimental channel release notes</summary>

- [Experimental channel release notes for the Windows App SDK 1.8](release-notes-archive/experimental-channel-1-8.md)
- [Experimental channel release notes for the Windows App SDK 1.7](release-notes-archive/experimental-channel-1.7.md)
- [Experimental channel release notes for the Windows App SDK 1.6](release-notes-archive/experimental-channel-1.6.md)
- [Experimental channel release notes for the Windows App SDK 1.5](release-notes-archive/experimental-channel-1.5.md)
- [Experimental channel release notes for the Windows App SDK 1.4](release-notes-archive/experimental-channel-1.4.md)
- [Experimental channel release notes for the Windows App SDK 1.3](release-notes-archive/experimental-channel-1.3.md)
- [Experimental channel release notes for the Windows App SDK 1.2](release-notes-archive/experimental-channel-1.2.md)
- [Experimental channel release notes for the Windows App SDK 1.0](release-notes-archive/experimental-channel-1.0.md)
- [Experimental channel release notes for the Windows App SDK 0.8](release-notes-archive/experimental-channel-0.8.md)

</details>

## Related topics

- [Stable channel](stable-channel.md)
- [Preview channel](preview-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)



