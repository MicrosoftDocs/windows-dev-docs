---
title: Latest experimental channel release notes for the Windows App SDK
description: Learn about the latest experimental releases of the Windows App SDK.
ms.topic: article
ms.date: 04/25/2024
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

**Experimental channel release note archive:**

- [Experimental channel release notes for the Windows App SDK 1.7](release-notes-archive/experimental-channel-1.7.md)
- [Experimental channel release notes for the Windows App SDK 1.6](release-notes-archive/experimental-channel-1.6.md)
- [Experimental channel release notes for the Windows App SDK 1.5](release-notes-archive/experimental-channel-1.5.md)
- [Experimental channel release notes for the Windows App SDK 1.4](release-notes-archive/experimental-channel-1.4.md)
- [Experimental channel release notes for the Windows App SDK 1.3](release-notes-archive/experimental-channel-1.3.md)
- [Experimental channel release notes for the Windows App SDK 1.2](release-notes-archive/experimental-channel-1.2.md)
- [Experimental channel release notes for the Windows App SDK 1.0](release-notes-archive/experimental-channel-1.0.md)
- [Experimental channel release notes for the Windows App SDK 0.8](release-notes-archive/experimental-channel-0.8.md)

## Version 1.8 Experimental (1.8.0-experimental2)

### Use on-device AI with Windows AI APIs

> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.

The Windows App SDK incorporates advanced Windows AI capabilities, enabling developers to seamlessly integrate intelligent features into their applications. These enhancements include local AI functionalities such as responding to incoming prompts, recognizing text within images, describing image contents, extract objects from pictures, and more.

For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.

#### Decimal DataType

The new `Decimal` support offers a high-precision base-10 numeric data type that is invaluable for financial and scientific calculations, avoiding imprecision and rounding errors inherent to floating-point data types. It is structured as a 96-bit (12-byte) unsigned integer, scaled by a variable power of 10, allowing for precise representation of decimal values. This enables decimal support for programming languages lacking decimal data types and provides interoperability with languages that do support decimal (e.g. C#, Python).

#### NuGet Metapackage

The Windows App SDK NuGet package has been converted to a NuGet metapackage. Each component contributing to the Windows App SDK is now a component NuGet package and is listed as a dependency by the metapackage. This allows developers to choose either the metapackage or select specific component packages for their applications. The use of individual component packages enables developers to include only the APIs and functionalities that are necessary for their apps. The default experience behaves as if `WindowsAppSDKSelfContained` had been set as True, but the `Microsoft.WindowsAppSDK.Packages` package can be referenced to use framework package deployment.

#### Microsoft.Windows.SDK.BuildTools.MSIX Refactor

The MSIX publishing support has been factored into a standalone nuget package, which can be independently maintained and consumed by Windows App SDK and other projects.  In addition, several feature gaps with Single-Project solutions have been addressed including generation of MSIX bundles and MSIX upload packages.

#### Windows AI APIs

##### Low-Rank Adaptation (LoRA) for Phi Silica

Low-Rank Adaption (LoRA) for Phi Silica allows developers to fine-tune the on-device language model (Phi Silica) using their own custom data. This adapter enables output to align for specific scenarios like finance, medical, and education. See [Phi Silica LoRA](/windows/ai/apis/phi-silica-lora) for details.

##### Text Intelligence - Conversation Summary 
Phi Silica now has a Summarize Conversation feature that allows you to summarize what people have said over an email, chat, or thread. See [Phi Silica](/windows/ai/apis/phi-silica) for more details.

### New APIs for 1.8-experimental2

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
        SetContentNodeParent
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

    DesktopSiteBridge
        TryCreatePopupSiteBridge

    EndpointConnectionEventArgs
    EndpointRequestedStateChangedEventArgs
    IContentIslandEndpointConnectionPrivate
    IContentNodeOwner
    IContentSiteBridgeEndpointConnectionPrivate
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

    InputPointerActivationBehavior
    InputPointerSource
        ActivationBehavior
        DirectManipulationHitTest
        GetForVisual
        GetForWindowId
        RemoveForVisual
        TouchHitTesting
        TrySetDeviceKinds

    ProximityEvaluation
    TouchHitTestingEventArgs
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
    TextSummarizer
        SummarizeConversationAsync
```
```
Microsoft.Windows.AI.Text.Experimental (C#-only, see Known Issues)
 
    LowRankAdaptation
    LanguageModelOptionsExperimental
    LanguageModelExperimental
```
```
Microsoft.Windows.ApplicationModel.Background.UniversalBGTask

    Task
        Run
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
Microsoft.Windows.Storage

    ApplicationData
        GetForUnpackaged
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
```
```
Microsoft.Windows.Vision

    ScreenRegionBoundingBox
    ScreenRegionDetectionContract
    ScreenRegionLabel
```
```
Microsoft.Windows.Widgets.Feeds.Providers

    FeedManager
        TryRemoveAnnouncementById

    IFeedManager3
```
```
Microsoft.Windows.Widgets.Providers

    WidgetInfo
        Rank

    WidgetUpdateRequestOptions
        Rank
```

### Known Issues

* The Microsoft.Windows.AI.Text.Experimental API projections for C++ are missing in this release. The projections are available for use from C#.
* If you're using the Microsoft.WindowsAppSDK.WinUI component package in its default self-contained mode, make sure to set the WebView2EnableCsWinRTProjection property to true when utilizing WebView2 APIs. This helps prevent version conflicts and avoids related warnings.
* When using the WindowsAppSDK component packages, you may notice a warning `NU1603` indicating the specified version of a dependent component package was not found, but another was resolved instead.  This is expected with the experimental2 build and NuGet will correctly resolve a newer version of the package which will allow your project to build.  If you treat warnings as errors, you can temporarily treat this specific warning as not an error by specifying the property `<WarningsNotAsErrors>NU1603</WarningsNotAsErrors>`.

## Version 1.8 Experimental (1.8.0-experimental1)

### Use on-device AI with Windows AI APIs

> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.

The Windows AI APIs offers several AI-powered features and APIs for you to easily, efficiently, and responsibly use on-device AI models in your Windows apps. In this release we are making available several scenario-focused APIs for you to leverage powerful capabilities without the need to find, run, or optimize your own Machine Learning (ML) models. 

Learn more about responsible development practices used during Windows AI API development that you can also apply as you create AI-assisted features in the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.

This is the latest release of the experimental channel.

To download, retarget your WinAppSDK NuGet version to `1.8.250515001-experimental1`.

#### Object Erase

The `ImageObjectRemover` can be used to remove objects from images. The model takes both an image and a greyscale mask indicating the object to be removed, erases the masked area from the image, and replaces the erased area with the image background.

### New APIs for 1.8-experimental1

This release includes the following new and modified experimental APIs:

```
Microsoft.Graphics.Imaging

    ImageBuffer
    ImageBufferContract
    ImageObjectExtractor
    ImageObjectExtractorContract
    ImageObjectExtractorHint
    ImageObjectRemover
    ImageObjectRemoverContract
    ImageScaler
    ImageScalerContract
    PixelFormat
```
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
        SetContentNodeParent
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

    DesktopSiteBridge
        TryCreatePopupSiteBridge

    EndpointConnectionEventArgs
    EndpointRequestedStateChangedEventArgs
    IContentIslandEndpointConnectionPrivate
    IContentNodeOwner
    IContentSiteBridgeEndpointConnectionPrivate
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

    InputPointerActivationBehavior
    InputPointerSource
        ActivationBehavior
        DirectManipulationHitTest
        GetForVisual
        GetForWindowId
        RemoveForVisual
        TouchHitTesting
        TrySetDeviceKinds

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
    UniformGridLayoutState
```
```
Microsoft.UI.Xaml.Controls.Primitives

    ScrollPresenter
        ScrollStarting
        ZoomStarting
```
```
Microsoft.Windows.AI

    AIFeatureReadyContract
    AIFeatureReadyResult
    AIFeatureReadyResultState
    AIFeatureReadyState
```
```
Microsoft.Windows.AI.ContentModeration

    ContentFilterOptions
    ContentModerationContract
    ImageContentFilterSeverity
    SeverityLevel
    TextContentFilterSeverity
```
```
Microsoft.Windows.AI.Generative

    ImageDescriptionContract
    ImageDescriptionGenerator
    ImageDescriptionKind
    ImageDescriptionResult
    ImageDescriptionResultStatus
    LanguageModel
    LanguageModelContext
    LanguageModelContract
    LanguageModelEmbeddingVectorResult
    LanguageModelOptions
    LanguageModelResponseResult
    LanguageModelResponseStatus
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
Microsoft.Windows.Storage.Pickers

    FileOpenPicker
    FileSavePicker
    FolderPicker
    PickerLocationId
    PickerViewMode
    PickFileResult
    PickFolderResult
```
```
Microsoft.Windows.Vision

    BoundingBox
    DetectedLineStyle
    OrientationDetectionOptions
    RecognizedLine
    RecognizedLineStyle
    RecognizedText
    RecognizedWord
    TextRecognitionContract
    TextRecognizer
    TextRecognizerOptions
```
```
Microsoft.Windows.Widgets.Feeds.Providers

    FeedManager
        TryRemoveAnnouncementById

    IFeedManager3
```
```
Microsoft.Windows.Workloads

    WorkloadPriority
    WorkloadsContract
```

### Bug fixes

This release includes the following bug fixes:

- Fixed an issue where mouse wheel input is ignored if the "Scroll inactive windows when hovering over them" option in Windows Settings is disabled. For more info, see GitHub issue [#10091](https://github.com/microsoft/microsoft-ui-xaml/issues/10091).




## Related topics

- [Stable channel](stable-channel.md)
- [Preview channel](preview-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)
