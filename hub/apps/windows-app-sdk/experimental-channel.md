---
title: Latest experimental channel release notes for the Windows App SDK
description: Learn about the latest experimental releases of the Windows App SDK.
ms.topic: release-notes
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

## Version 1.8 Experimental (1.8.0-Experimental4)

### Use on-device AI with Windows AI APIs

> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.

The Windows App SDK incorporates advanced Windows AI capabilities, enabling developers to seamlessly integrate intelligent features into their applications. These enhancements include local AI functionalities such as responding to incoming prompts, recognizing text within images, describing image contents, extract objects from pictures, and more.

For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.

### Microsoft Windows ML

[Windows ML](/windows/ai/new-windows-ml/overview) bringing hardware-accelerated machine learning capabilities to Windows applications. The Microsoft.WindowsAppSDK.ML package provides a Windows-optimized version of ONNX Runtime with simplified APIs for managing execution providers.

**Key Features:**

- Hardware Abstraction: Automatically discovers and manages execution providers compatible with your hardware.
- Simplified EP Management: Handles acquisition, installation, and registration of execution providers on the local device your app runs on.
- Seamless ONNX Runtime Integration: Works directly with ONNX Runtime APIs for model inference.
- Multi-Language Support: Available for C++, C#, Python, and other languages.


### WindowsAppSDK.Packages renamed

The NuGet Component Package `Microsoft.WindowsAppSDK.Packages` was renamed to `Microsoft.WindowsAppSDK.Runtime`. This change better reflects that package's purpose and clarifies its role within the SDK - specifically, that it encapsulates the runtime component.


### Prompt Size Limit Reporting


Allows applications to determine if an input exceeds the allowable size for a Text Summarizer call. If the input is too large, the API returns an index indicating the current limit, enabling developers to adjust the input accordingly. This limit is based on token count rather than byte or character length, and it can vary over time due to multiple factors. Therefore, applications should treat the limit as dynamic and subject to change.

### Text Rewriter Tone

Enables text rewriting with specific tones. The Casual option rephrases content to sound more informal and conversational, using natural, spontaneous phrasing while preserving meaning and format. The Formal option transforms text into a polished, professional version, maintaining the original structure and details with precise language suitable for formal context. The General option retains the original tone and intent, ensuring the meaning remains unchanged.

### Conversation Summary Options

Enables developers to specify the desired output language for conversation summarization. This allows applications to generate summaries in a targeted language, enhancing localization, and user experience.

### Bug Fixes

- Removed duplicate .winmd files for AI components. For more information see [Windows App SDK GitHub Issue #5439](https://github.com/microsoft/WindowsAppSDK/issues/5439)
- Addressed a potential crash in `ApplicationDataProvider::GetStateFolderUris` caused by reentrancy. For more information see [Windows App SDK GitHub Issue #10513](https://github.com/microsoft/WindowsAppSDK/issues/10513)
- Addressed a UI bug where the `TitleBar` displayed incorrect spacing when a short title was used. For more information see [Windows App SDK GitHub Issue #10492](https://github.com/microsoft/WindowsAppSDK/issues/10492)
- Addressed a UI bug where the `CalendarDatePicker` control displayed incorrect icon margins when a long header was set. For more information see [Windows App SDK GitHub Issue #10469](https://github.com/microsoft/WindowsAppSDK/issues/10469)
- Resolved an issue related to versioning mismatches between WIndowsAppSDK and Windows SDK NuGet packages, which can cause new projects to fail to build out of the box. For more information see [Windows App SDK GitHub Issue #10467](https://github.com/microsoft/WindowsAppSDK/issues/10467)
- Addressed a regression where the mouse wheel input was ignored if the "Scroll inactive windows when hovering over them" setting was disabled, making windows appear perpetually inactive. For more information see [Windows App SDK GitHub Issue #10091](https://github.com/microsoft/WindowsAppSDK/issues/10091)

- Addressed a deployment bug where failing to set `$(WindowsPackageType)=MSIX` in the project file prevents the Deployment Manager from being added, causing apps to require admin privileges unexpectedly. For more information see [Windows App SDK GitHub Issue #8182](https://github.com/microsoft/WindowsAppSDK/issues/8182)

### New APIs for 1.8-experimental4

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
    IContentNodeOwner
    IContentSiteBridgeEndpointConnectionPrivate
    PopupAnchoringOptions
    PopupWindowSiteBridge
    ProcessStarter
    SystemVisualSiteBridge
```
```
Microsoft.UI.Designer
 
    DesignerOutputHost
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
Microsoft.Windows.AI.MachineLearning
 
    ExecutionProvider
    ExecutionProviderCatalog
    ExecutionProviderReadyResult
    ExecutionProviderReadyResultState
    ExecutionProviderReadyState
    MachineLearningContract
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
        SummarizeConversationAsync
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

### Known Issues

- When upgrading from version 1.8.250610002-experimental3 (or later) of the Microsoft.WindowsAppSDK NuGet package in a C++ project, you may see a compatibility error, such as with Microsoft.WindowsAppSDK.DWrite. This stems from a limitation in packages.config. To resolve it, remove all existing WindowsAppSDK references and re-add the updated Microsoft.WindowsAppSDK package.

- Windows ML requires framework-dependent deployment; self-containment deployment is not supported. Apps using Windows ML must reference the Microsoft.WindowsAppSDK package, which includes transitive dependencies on the Microsoft.WindowsAppSDK.ML and Microsoft.WindowsAppSDK.Runtime components, both of which are required.

- Windows ML is supported only on Windows 11 version 24H2 or newer (Build 26100+), and only on x64 and ARM64 architectures. x86 is not supported.

- The StoragePickers APIs (FileOpenPicker, FileSavePicker, FolderPicker) only work in self-contained deployments due to a localization bug. Non-self-contained apps will crash at runtime when invoking these pickers. As a workaround, copy Microsoft.WindowsAppRuntime.pri to your project folder and configure it to copy to the output directory using:
```
<ItemGroup>
   <None Update="Microsoft.WindowsAppRuntime.pri">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
   </None>
</ItemGroup>
```

## Version 1.8 Experimental (1.8.0-experimental3)

<details>

<summary>Expand to see details for the Windows App SDK 1.8 Experimental (1.8.0-experimental3) release</summary>

### Use on-device AI with Windows AI APIs

> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.

The Windows App SDK incorporates advanced Windows AI capabilities, enabling developers to seamlessly integrate intelligent features into their applications. These enhancements include local AI functionalities such as responding to incoming prompts, recognizing text within images, describing image contents, extract objects from pictures, and more.

For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.

### New APIs for 1.8-experimental3

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
Microsoft.UI.Designer

    DesignerOutputHost
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

</details>

## Version 1.8 Experimental (1.8.0-experimental2)

<details>

<summary>Expand to see details for the Windows App SDK 1.8 Experimental (1.8.0-experimental2) release</summary>

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

</details>

## Version 1.8 Experimental (1.8.0-experimental1)

<details>

<summary>Expand to see details for the Windows App SDK 1.8 Experimental (1.8.0-experimental1) release</summary>

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

</details>

## Archive of experimental channel release notes

<details>

<summary>Expand for links to archived experimental channel release notes</summary>

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
