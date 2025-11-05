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

## Version 2.0 Experimental (2.0.0-Experimental2)

> [!IMPORTANT]
> If you previously installed Windows App SDK 2.0 Experimental 1, follow the [NuGet Uninstall](/nuget/consume-packages/install-use-packages-visual-studio#uninstall-a-package) guide to remove the `Microsoft.WindowsAppSDK` NuGet Metapackage with version `2.0.250930001-experimental1` from your project and the associated WinAppSDK component packages from that release before trying this new version, since the previously released Experimental package has a higher version number than the current one.


#### App Content Search

The AppContentIndexer APIs empower developers to efficiently index app content, including text and images for rapid and relevant retrieval. Supporting both lexical (keyword-based) and semantic (meaning-based) searches, these APIs allow apps to deliver fast, relevant results based on user intent and context rather than just exact keywords.

This capability unlocks the following advanced scenarios:

- **Semantic Search**  
  Apps can return results based on intent and meaning rather than exact keyword matches.  
  *Example:* A query for **“project timeline”** can surface content that mentions **“schedule”** or **“delivery dates,”** even if those exact words weren’t used.
- **Retrieval-Augmented Generation (RAG)**  
  Indexed content can serve as a knowledge base for generative AI models. When a user asks a question, the app retrieves the most relevant documents or snippets from its index and feeds them into the model, enabling accurate, context-aware responses grounded in real data.

#### Windows ML Model Catalog

The Windows ML Model Catalog APIs enable your app or library to dynamically discover and download large AI model files from your own online model catalogs, eliminating the need to package these large files directly with your app or library. The model catalog helps ensure device compatibility by filtering models and downloading only those applicable for the specific Windows device in use.

#### Persistent File and Folder Locations
The latest `Microsoft.Windows.Storage.Pickers` update streamlines file and folder selection by letting developers set initial and persistent folder locations, and by grouping filetype filters with clear labels for easier navigation.

#### Relative Popup Positioning

The `PopupAnchor` API now allows `DesktopPopupSiteBridge` to support relative positioning by anchoring to its owning window or island, addressing the limitation where popups could only be positioned absolutely using screen coordinates.

#### Input Routing for SystemVisual ContentIslands

The `InputUnderlyingWindowController` API enables developers to designate the target HWND for receiving input messages that were originally sent to a ContentIsland created from a SystemVisual (see [ContentIsland.CreateForSystemVisual](/windows/windows-app-sdk/api/winrt/microsoft.ui.content.contentisland.createforsystemvisual)).

#### Flexible System Backdrop Placement

`SystemBackdropHost` enables placing a system backdrop (acrylic/mica) anywhere within an application's visual tree.

#### XAML Layout Sequential Positioning

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



