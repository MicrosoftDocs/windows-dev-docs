---
title: Windows App SDK 2.0 release notes
description: Provides information about what's new in Windows App SDK 2.0.
ms.topic: release-notes
ms.date: 07/16/2026
keywords: windows win32, windows app development, Windows App SDK, release notes
ms.localizationpriority: medium
zone_pivot_groups: wasdk-release-channels
---

# Windows App SDK 2.0 release notes

[!INCLUDE [wasdk-releasenotes](../../../includes/wasdk-release-notes.md)]

:::zone pivot="stable"

## Version 2.3.1

Released: **July 16, 2026** <br><br>

<details><summary>Structured JSON Output API</summary>

>
> Added the Structured JSON Output API (`Microsoft.Windows.AI.Text.LanguageModel.GenerateStructuredJsonResponseAsync`), which generates language-model responses constrained to a caller-supplied JSON Schema. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): LanguageModel_StructuredJsonOutput)
>

</details>

<details><summary>XamlOptionalChanges API</summary>

>
> Added the **XamlOptionalChanges** API, which lets apps opt into optional breaking changes before XAML initialization. For more information, see GitHub PR [microsoft/microsoft-ui-xaml#11110](https://github.com/microsoft/microsoft-ui-xaml/pull/11110). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): XamlSettings_OptionalChangesApi)
>
> Changed the behavior of the `DISABLE_XAML_GENERATED_MAIN` preprocessor constant so that, when defined, it now renames the generated `main()` method (C#: `XamlGeneratedProgram.XamlGeneratedMain()`; C++/WinRT: `wXamlGeneratedMain()`) instead of removing it entirely. This lets a custom `main()` method invoke the default implementation.
>

</details>

<details><summary>CompositionEngine API</summary>

>
> Added the **CompositionEngine** API as an unstable [Limited Access Feature (LAF)](/uwp/api/windows.applicationmodel.limitedaccessfeatures), which allows apps to opt into using the OS as the engine for the Composition APIs. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): CompositionEngine_API)
>

</details>

<details><summary>Video Super Resolution improvements</summary>

>
> Fixed NPU detection, added CPU support, and improved performance for Video Super Resolution. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): VideoSuperResolution_HardwareDetection)
>

</details>

<details><summary>Windows ML</summary>

>
> - Fixed execution provider enumeration in certain scenarios. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): WindowsML_ExecutionProviderEnumerationUpdate)
> - Added ARM64EC support for Windows ML.
>

</details>

<details><summary>AI package documentation</summary>

>
> Added IntelliSense documentation for stable APIs in the `Microsoft.WindowsAppSDK.AI` NuGet package.
>

</details>

<details><summary>Performance improvements</summary>

>
> Several of the optimizations below are opt-in. To enable them, set the specified XamlChangeId as noted in each item.
>
> - Added new optimized `XamlControlsResources` styles, including an optimized `ScrollBar` template. Use the `DefaultStyleOptimizations` XamlChangeId to opt in to these styles. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_DefaultStyleOptimizations)
> - `FontIcon` and `BitmapIcon` avoid inserting an extra `Grid` in their visual tree for apps that opt in with the `IconNoGridOptimization` XamlChangeId. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_IconNoGridOptimization)
> - Stopped loading `Style` property setters that don't apply a value. Use the `OptimizeApplyStyles` XamlChangeId to opt in. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_DeferStylePropertySetters)
> - Delayed applying `FrameworkElement` and `Control` styles until the element is in the tree. Use the `OptimizeApplyStyles` XamlChangeId to opt in. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_DelayApplyStylesAfterCreationComplete)
> - Optimized `CommandBar` initialization by no longer loading the entire visual state manager state. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_OptimizedCommandBarInitialization)
> - Improved performance of adding to `CommandBarFlyout`'s `PrimaryCommands` and `SecondaryCommands`. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_OptimizedCommandBarFlyoutUpdateCommands)
> - Used a more performant hash table for `ResourceDictionary`. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_FasterResourceDictionary)
> - Optimized away a resource-key string copy and comparison when loading deferred resources. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_LoadDeferredResourceOptimization)
> - Improved performance of `ThemeResource` lookups by reducing string comparisons. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_ThemeResourceUseResourceKeys)
> - Improved CPU performance during XAML theme resource lookup by reducing resource-key hashing overhead. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_ReduceThemeResourceLookupHashing)
> - Improved CPU performance for common internal XAML type checks on `UIElement` and `FrameworkElement`. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_CachedTypeChecks)
> - Improved CPU performance in internal activation factory lookup paths. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_ActivationFactoryFastPaths)
> - Improved performance of decoding integers stored in XBF files. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_FasterXbfIntLoading)
> - Reduced heap allocations on startup paths and tree traversal by reserving vector memory and optimizing an RAII helper. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_ReserveVectorSpace)
> - Improved performance by reserving space in tracker collections before appending multiple items. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_ReserveTrackerCollectionSpace)
> - Optimized bindings for `XamlUICommand` property bindings. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): XamlUICommand_LightweightBindings)
> - Improved performance when binding `XamlUICommand`s that don't define keyboard accelerators. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): XamlUICommand_SkipEmptyKeyboardAcceleratorBinding)
> - Initialized the `TintLuminosityOpacity` property in markup for default-style `AcrylicBrush` objects rather than in code, avoiding forced realization. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): AcrylicBrush_InitializePropertyInMarkup)
> - Avoided unnecessary default `ContextFlyout` initialization on `TextBlock` and `RichTextBlock` when entering or leaving the element tree. Use the `DeferContextFlyoutInit` XamlChangeId to opt in. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_DeferContextFlyoutInit)
> - Reduced startup waits on Direct3D lock contention by registering XAML's D3D device-removed event during background device creation instead of later on the UI thread. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Xaml_EagerDeviceRemovedEventRegistration)
> - Removed a redundant power-notification registration from `RefreshRateInfo`, reducing startup overhead. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): RefreshRateInfo_RemovePowerNotification)
> - Removed a redundant `version.dll` dependency from the WinUI startup path by computing the logged runtime version at compile time. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): XamlInit_GetMuxVersionCompileTime)
>

</details>

<details><summary>Bug fixes</summary>

>
> - Fixed a use-after-free crash in `CPopupRoot::ReplayPointerUpdate` when a popup is closed synchronously during pointer-event replay. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): CPopupRoot_ReplayPointerUpdateCrash)
> - Fixed an access violation in `CXamlIslandRoot::GetScreenOffset` triggered by re-entrant UI Automation queries during XAML island teardown. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): XamlIslandRoot_UiaTeardownReentrancyCrash)
> - Fixed a crash in `ModernCollectionBasePanel` where a viewport-tracked container or header recycled by a sibling layout pass could leave a stale anchor that tripped a fail-fast on the next measure. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): ModernCollectionBasePanel_StaleViewportAnchorRecycle)
> - Fixed a fail-fast in `CPopup::RemoveChild` when a `ToolTip` close pumped messages and re-entered the close path. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Popup_RemoveChildIdempotentWhenClosing)
> - Fixed a reentrancy crash in `CXcpDispatcher` when a `Popup` destructor triggered `UIADisconnectAllProviders` with active UI Automation clients. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): UIAWindow_PauseNewDispatchOnDisconnectProviders)
> - Fixed a crash when a cascading-menu or `AutoSuggestBox` delay-open timer fired after the owning island or dispatcher had begun teardown. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): CascadingMenuHelper_DelayOpenTimerTickAfterUnload)
> - Fixed a potential use-after-free crash during DLL unload when COM pointers in custom metadata types were not properly released. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): CustomMetadataTypes_InvalidateUseAfterFree)
> - Fixed a crash when opening a `Popup` from a `CompositionTarget.Rendering` callback due to dispatcher reentrancy during focus changes. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): CPopup_OpenSetFocusReentrancy)
> - Fixed a crash in `MediaPlayerPresenter` when the GPU device is lost during media playback event handling. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): MediaPlayerPresenter_DeviceLostCrash)
> - Fixed a potential access violation in Direct2D text rendering when device-lost recovery occurred during glyph-run texture building. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): HWBuildGlyphRunTextures_DeviceGuardScope)
> - Fixed a crash in `CFocusRectManager::FindFocusRectHost` when focus lands on a control hosted inline via `InlineUIContainer` in `RichTextBlock` or `TextBlock`. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FocusRectManager_FindFocusRectHostCrash)
> - Fixed a crash that could repeatedly bring down `backgroundTaskHost.exe` when a background task had no stored CLSID, and added graceful handling when `CoCreateInstance` fails. For more info, see GitHub issue [#5870](https://github.com/microsoft/WindowsAppSDK/issues/5870). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): UniversalBGTask_RunCrash)
> - Fixed windowed and non-windowed flyouts appearing misaligned (about one item-height above the parent item) for side placements when they open upward near the bottom of the screen. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Flyout_SidePlacementFlipUpAlignmentFix)
> - Fixed a crash (null `LayoutState` dereference) in `UniformGridLayout` and `FlowLayout` that could occur when a collection change was raised on an `ItemsRepeater` that was no longer loaded. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): UniformGridLayoutFlowLayout_OnItemsChangedNullLayoutStateCrash)
> - Fixed `ApplicationData.GetForUnpackaged().LocalSettings()` opening a registry key at the wrong path (`HKCU\SOFTWARE\publisher\product` rather than `HKCU\SOFTWARE\Classes\Local Settings\Software\publisher\product`), which is a roaming location rather than one local to the machine. For more info, see GitHub issue [#6559](https://github.com/microsoft/WindowsAppSDK/issues/6559). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): ApplicationData_GetForUnpackaged_LocalSettings)
> - Fixed a race condition in keyboard modifier-state handling that could crash apps under concurrent UI-thread input. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): WindowsServices_GetKeyboardModifiersStateRace)
> - Fixed an AppxSvc crash that can occur during package install, update, or uninstall. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, service-host crash fix)
>

</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new APIs compared to the 2.2.0 release:
> ```
> Microsoft.UI.Composition
>
>     CompositionEngine
>     CompositionEngineType
> ```
> ```
> Microsoft.UI.Xaml.Settings
>
>     XamlChangeId
>     XamlOptionalChanges
> ```
> ```
> Microsoft.Windows.AI.Text
>
>     GenerateStructuredJsonResponseResult
>     GenerateStructuredJsonResponseStatus
>     LanguageModel
>         GenerateStructuredJsonResponseAsync
> ```
>

</details>

## Version 2.2.0

Released: **June 9, 2026** <br><br>

<details><summary>Video Super Resolution AI API</summary>

>
> The `Microsoft.Windows.AI.Video.VideoScaler` API delivers real-time video enhancement through advanced AI upscaling, optimized for streams featuring people in conversation. It enables developers to provide sharper, clearer visuals across conferencing, streaming, and editing platforms, even under poor network conditions. The API supports customization of output resolution, frame rate, and regions of interest, with compatibility for multiple video formats including BGR, RGB, and NV12.
>
> `VideoScaler` is disposable, and includes a capability check so apps can validate VSR support at runtime. Explicit Windows ML initialization is not required.
>

</details>

<details><summary>New ApplicationData API for Unpackaged Apps</summary>

>
> A new `Microsoft.Windows.Storage.ApplicationData.GetForUnpackaged()` API gives unpackaged apps a first-class WinRT entry point to per-user/per-machine application data, matching the surface previously available only to packaged apps. This simplifies future packaged↔unpackaged migration and removes the need for Registry-API workarounds.
>

</details>

<details><summary>New XamlBindingHelper APIs</summary>

>
> Added boxing-free value setter overloads to `XamlBindingHelper` (`SetPropertyFromThickness`, `SetPropertyFromCornerRadius`, `SetPropertyFromColor`) and exposed the `Setter.ValueProperty` dependency property.
>

</details>

<details><summary>Bug fixes</summary>

>
> - Fixed a crash in `RenderTargetBitmap` when the target element leaves the visual tree (for example, a popup closes) before `PreCommit` completes. `RenderAsync` now returns `E_ABORT` instead of crashing. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): RenderTargetBitmap_PreCommitCrashOnTreeLeave)
> - Fixed a use-after-free crash in `ScrollView` when the control is destroyed while its hide-indicators timer tick is still pending. For more info, see GitHub issue [#10514](https://github.com/microsoft/microsoft-ui-xaml/issues/10514). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): ScrollView_HideIndicatorsTimerUseAfterFree)
> - Fixed a potential crash in `Microsoft.UI.Xaml.dll!DirectUI::DXamlCore::GetPeerPrivate` caused by attempting to access an object scheduled to be freed. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): GetPeerPrivate_UseAfterFree)
> - Fixed an issue where `Microsoft.UI.System.ThemeSettings` could crash an app if it was destroyed on a background thread (for example, when destroyed by the .NET garbage collector). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): ThemeSettings_OffThreadDestructorFix)
> - Fixed a crash that could occur on certain devices when active touch contacts were canceled by input hardware, such as when pen input overrides them. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): InputPointerSource_PointerCancelCrashFix)
> - Fixed an issue where sparse-packaged apps were unable to discover module-specific PRI files. For more info, see GitHub issue [#6375](https://github.com/microsoft/WindowsAppSDK/issues/6375). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): MRTCore_SparsePackagedPriFallback)
> - Fixed a string ownership bug when using the Flat-C Windows ML Catalog API. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): WindowsMLFlatCCatalog_CallbackStringOwnership)
> - Fixed a potential issue in Windows ML during process shutdown. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): WindowsML_ComRundownFix)
> - Fixed potential COM initialization and teardown issues in Windows ML. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): WindowsML_ComInitializationFix)
>

</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new APIs compared to the 2.1.3 release:
>
> ```
> Microsoft.UI.Xaml
>
>     Setter
>         ValueProperty
> ```
 > ```
> Microsoft.UI.Xaml.Markup
>
>     XamlBindingHelper
>         SetPropertyFromColor
>         SetPropertyFromCornerRadius
>         SetPropertyFromThickness
> ```
 > ```
> Microsoft.Windows.AI.Video
>
>     VideoScaler
>     VideoScalerOptions
>     VideoScalerResult
>     VideoScalerStatus
> ```
 > ```
> Microsoft.Windows.Storage
>
>     ApplicationData
>         GetForUnpackaged
> ```
>

</details>

## Version 2.1.3

Released: **May 21, 2026** <br><br>

<details><summary>TitleBar Content Custom Drag Regions</summary>

>
> Improves how `TitleBar` decides which parts of `TitleBar.Content` are draggable, and adds APIs that give developers explicit control over drag regions. See PR [microsoft/microsoft-ui-xaml#10936](https://github.com/microsoft/microsoft-ui-xaml/pull/10936) and tracking issue [#10421](https://github.com/microsoft/microsoft-ui-xaml/issues/10421).
>
> **What changed**
>
> - **New default behavior** — The framework now recursively walks the visual tree and automatically excludes only **interactive controls** from the drag region. Empty gaps and non‑interactive visuals are **draggable by default**, with no markup changes required.
> - **New APIs for explicit control:**
>   - `TitleBar.IsDragRegion` — attached nullable bool to mark an element as draggable (`True`), clickable (`False`), or framework‑decided (unset).
>   - `TitleBar.AutoRefreshDragRegions` — when `True`, drag regions refresh automatically on layout updates.
>   - `TitleBar.RecomputeDragRegions()` — manually recomputes drag regions after dynamic content changes.
>
> **Why**
>
> Custom title bars often mix interactive controls with non‑interactive visuals inside `Grid`/`StackPanel` layouts. The previous "punch holes out of the full content area" approach left **unexpected non‑draggable gaps** in layouts with empty space, uneven spacing, or dynamic UI.
>
> **Visual comparison**
>
> *Default (previous) behavior — non‑draggable gaps:*
>
> ![Default behavior — non-draggable gaps](https://github.com/microsoft/microsoft-ui-xaml/raw/51babd8a0fb42cac339f504e5a821eb7fb58b7c9/specs/TitleBar/images/titlebar-drag-issue.png)
>
> The empty middle area is **not** draggable.
>
> *Current (new) behavior — gaps draggable, interactive controls excluded:*
>
> ![Current behavior — fixed drag regions](https://github.com/microsoft/microsoft-ui-xaml/raw/51babd8a0fb42cac339f504e5a821eb7fb58b7c9/specs/TitleBar/images/titlebar-drag-issue-fixed.png)
>
> The empty space is draggable; interactive controls are correctly excluded. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): TitleBar_ContentDragRegions)
>

</details>

<details><summary>LoRA API Promoted to Stable</summary>

>
> The Low-Rank Adapter (LoRA) API for Phi Silica has been promoted to stable.
>
> LoRA allows developers to fine-tune the on-device Phi Silica language model with their own custom data, aligning output for specific scenarios such as finance, medical, and education. Load an adapter with `LanguageModelLowRankAdapter.CreateFromPath` and pass it via `LanguageModelOptions.LowRankAdapter` when calling `GenerateResponseAsync`. See [Phi Silica LoRA](/windows/ai/apis/phi-silica-lora) for details.
>

</details>

<details><summary>New AICapabilities API</summary>

>
> A new **AICapabilities.HasAICapability** API enables third-party applications to determine whether the device is a Copilot+ PC.
>

</details>

<details><summary>Updated ONNX Runtime</summary>

>
> The version of ONNX Runtime has been updated to 1.24.6. See [ONNX Runtime versions](/windows/ai/new-windows-ml/onnx-versions) for more info.
>

</details>

<details><summary>Windows ML</summary>

>
> - Added support for multiple execution providers within a single MSIX package. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): WindowsML_MultiEpPerPackage)
> - Added support for discovering execution providers delivered as framework packages. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): WindowsML_FrameworkEpEnumeration)
> - Added execution provider selection mode for flexible deployment configurations. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): WindowsML_EpSelectionMode)
>

</details>

<details><summary>Bug fixes</summary>

>
> - Fixed a memory leak in `ItemsRepeater` where recycled elements were never garbage collected due to a reference cycle through the `RecyclePool`, which could also cause crashes in `InvalidateChildrenMeasure`. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): ItemTemplateWrapper_RecyclePoolLeak)
> - Fixed a crash where an implicit Show/Hide animation completion callback could access a destroyed `CUIElement`, causing an access violation. The callback now uses a weak reference to safely handle the case where the element is destroyed before the animation completes. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Animation_FixImplicitHideAnimationCrash)
> - Fixed an issue where windowed popup content opened in a XAML Island did not respect `OverrideScale`, causing content to appear oversized and clipped. For more info, see GitHub issue [#11000](https://github.com/microsoft/microsoft-ui-xaml/issues/11000). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): Popup_WindowedPopupOverrideScale)
> - Fixed a fail-fast crash caused by re-entrant dispatch during cross-apartment COM release operations in `UIAffinityReleaseQueue::DoCleanup`. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): UIAffinityReleaseQueue_PauseDispatchDuringCleanup)
> - WinUI: Fixed ambiguous module lookup when multiple modules with the same name are loaded in the same process. `GetModuleHandleW` has been replaced with `GetModuleHandleExW` so the correct module is resolved by address. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): GetModuleHandle_FixAmbiguousModuleLookup)
> - Fixed an integer divide-by-zero crash in `UniformGridLayout::GetMajorSize` when an `ItemsRepeater` is laid out in an available width narrower than one item's minor stride. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): UniformGridLayout_GetItemsPerLineDivideByZero)
> - Fixed a potential crash when a package has been uninstalled prior to being processed. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): PackageManager_FixCrash)
> - Fixed an issue where `GetReadyState` could return incorrect error codes (for example, `DisabledByUser` or `NotSupportedOnCurrentSystem`) when required packages were not yet deployed. The API now correctly reports `NotReady` in this scenario, improving diagnostic clarity. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): ModelInitialization_GetReadyStateAvailabilityGuard)
> - Improved internal performance diagnostics for `LanguageModel.GenerateResponseAsync` to better identify sources of latency before the first token is returned. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): LanguageModelInsights_GetPartialResultLatency)
> - Fixed an issue where XAML compiler errors were silently lost when using `dotnet build`, showing only `MSB3073: exited with code 1` instead of the actual error messages. For more info, see GitHub issue [#9813](https://github.com/microsoft/microsoft-ui-xaml/issues/9813). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, build-time XAML compiler tooling change)
> - Fixed an issue where `Microsoft.Windows.Workloads.dll` failed to load on Windows builds prior to 22000 due to static imports of Dynamic Dependencies APIs unavailable on those OS versions. The functions are now resolved dynamically, so failures on unsupported OS versions surface as a normal `HRESULT` instead of a loader error dialog. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, no containment)
>

</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new APIs compared to the 2.0.1 release:
>
> ```
> Microsoft.UI.Xaml.Controls
>
>     TitleBar
>         AutoRefreshDragRegions
>         AutoRefreshDragRegionsProperty
>         GetIsDragRegion
>         IsDragRegionProperty
>         RecomputeDragRegions
>         SetIsDragRegion
> ```
 > ```
> Microsoft.Windows.AI
>
>     AICapabilities
>
>     AICapabilityCategory
> ```
 > ```
> Microsoft.Windows.AI.Text
>
>     LanguageModelLowRankAdapter
>
>     LanguageModelLowRankAdapterResult
>
>     LanguageModelOptions
>         LowRankAdapter
>
>     LanguageModelResponseStatus
>         IncompatibleLowRankAdapter
> ```
>

</details>

## Version 2.0 stable (2.0.1)

Released: **April 29, 2026**

Windows App SDK 2.0 ships new APIs and improvements across the platform: XAML conditionals, modern Storage Pickers, expanded popup and anchoring APIs in Microsoft.UI.Content, new package deployment and validation APIs, a refactored Windows ML stack, and additions to the Windows AI surface. It is also the first release on the new Semantic Versioning scheme and the first major version update since Windows App SDK 1.0 (November 2021).
<br><br>

<details><summary>Semantic Versioning</summary>

>
> Windows App SDK 2.0 is the first release on the new Windows App SDK versioning scheme, standardized on [Semantic Versioning 2.0.0](https://semver.org/). This is also the first time we are incrementing the major version number.
>
> The new scheme simplifies versioning by aligning the Windows App SDK version with the NuGet package version. Referencing Windows App SDK 2.0 in your project now means you'll use the corresponding NuGet version, with no separate date-based build number to track:
>
> ```xml
> <PackageDependency Name="Microsoft.WindowsAppSDK" Version="2.0.1" />
> ```
>
> The package family name also aligns with the major version instead of the minor version, so the next side-by-side release of Windows App SDK will be version 3.0.0. Under SemVer, breaking changes are only allowed across major version updates (see [Windows App SDK deployment architecture](../deployment-architecture.md)).
>

</details>

<details><summary>Windows ML</summary>

>
> The `Microsoft.WindowsAppSDK.ML` NuGet package has been refactored, adding a new base dependency. The existing ML NuGet package's functionality remains the same, but the core Windows ML features have been refactored into a base package called `Microsoft.Windows.AI.MachineLearning`. This new package contains a minimal set of dependencies; supporting apps down to Windows 10 v1903. If you need Windows 10 v1809 support, continue using the existing `Microsoft.WindowsAppSDK.ML` package.
>
> **ONNX Runtime.** The version of ONNX Runtime included with Windows ML has been updated to 1.24.5. See [ONNX Runtime versions](/windows/ai/new-windows-ml/onnx-versions) for more info. This release also includes an additive ORT API change to support model compilation using graphs produced by the `OrtModelEditor` API (a feature gap in the prior `OrtCompileApi` surface). The change is non-breaking and was taken to unblock upcoming WebNN browser scenarios; see the underlying [ONNX Runtime PR #27332](https://github.com/microsoft/onnxruntime/pull/27332) for details.
>
> **Licensing.** The Windows ML license agreement has been simplified with clearer terms for ISVs:
>
> - Restructured Installation, Data, and Distributable Code sections for clarity.
> - A new Execution Provider (EP) Compliance Notice section clarifies developer responsibilities regarding hardware-accelerated execution providers.
> - Direct links to vendor license agreements for NVIDIA TensorRT, Intel OpenVINO, and Qualcomm QNN (Qualcomm Neural Network SDK).
>

</details>

<details><summary>Windows AI APIs</summary>

>
> - Phi Silica APIs are now enforced as part of a Limited Access Feature (LAF). See [Phi Silica](/windows/ai/apis/phi-silica) for details.
> - Added new states to `AIFeatureReadyState` to help apps explain transient and durable failures during the AI model acquisition process: `CapabilityMissing`, `NotCompatibleWithSystemHardware`, and `OSUpdateNeeded`. Apps can use these states to give users actionable guidance instead of treating every "not ready" condition as a generic failure.
> - Added checks for various network and Windows Update errors during AI model installation, flagging durable failures so users can understand why model packages failed to install.
> - Improved diagnosability for Text Intelligence APIs used in Windows AI scenarios.
>

</details>

<details><summary>WebView2 (WinUI 3) drag support</summary>

>
> Enabled drag support for WebView2 content hosted in WinUI 3 applications. This capability was previously unsupported and is now available without introducing new public APIs. (Dropping into a WebView2 control from external sources was already supported.)
>
> **Supported scenarios:**
>
> - **Standard drag-and-drop content.** Dragging text, HTML, images, and URLs is supported. Dragging an image outside the app currently results in a default file name (for example, `download.jpg`) when dropped into File Explorer.
> - **Drag cancellation.** On-demand cancellation of an active drag operation is supported, so an app can conditionally block drag operations for restricted or ephemeral content after the drag has been initiated.
> - **Custom drag visuals.** Custom drag UI, such as icons or previews, is supported to help users clearly identify the content being dragged.
> - **Customizable drag data.** Editing and customizing drag data is supported, enabling app-specific scenarios such as dragging messages within the app, or attaching contextual metadata (for example "From \<app name\>") that the destination can read on drop.
>
> **Limitations:**
>
> - Some additional drag data types are not currently supported, including `DownloadURL`.
>
> **Requirements:**
>
> - Minimum WebView2 Runtime version (Edge Beta channel): 144.0.3719.11.
>

</details>

<details><summary>Storage Pickers updates</summary>

>
> The `Microsoft.Windows.Storage.Pickers` API (introduced in Windows App SDK 1.8) has been extended to streamline file and folder selection by letting developers set initial and persistent folder locations and group file type filters with clear labels for easier navigation.
>
> - `FileOpenPicker` adds `FileTypeChoices`, `InitialFileTypeIndex`, `SettingsIdentifier`, `SuggestedFolder`, `SuggestedStartFolder`, and `Title`.
> - `FileSavePicker` adds `InitialFileTypeIndex`, `SettingsIdentifier`, `ShowOverwritePrompt`, `SuggestedStartFolder`, and `Title`.
> - `FolderPicker` adds `PickMultipleFoldersAsync`, `SettingsIdentifier`, `SuggestedFolder`, `SuggestedStartFolder`, and `Title`.
>

</details>

<details><summary>Microsoft.UI.Content additions</summary>

>
> - **Relative popup positioning.** The new `PopupAnchor` API allows `DesktopPopupSiteBridge` to support relative positioning by anchoring to its owning window or island, addressing the limitation where popups could only be positioned absolutely using screen coordinates. New `DesktopPopupSiteBridge.AnchoringBehavior` and `DesktopPopupSiteBridge.AnchoringPixelAlignment` properties control the anchoring behavior.
> - **Keyboard cues guidance.** The new `InputFocusController.ShouldShowKeyboardCues` property guides developers on whether to show keyboard cues right after the creation of a `ContentIsland`.
> - **PointerPoint convenience API.** The new `PointerPoint.GetCurrentPoint` method allows developers to get the active `PointerPoint` data from a provided `pointerId`.
>

</details>

<details><summary>Microsoft.UI.Xaml.Controls.SystemBackdropElement</summary>

>
> The new `Microsoft.UI.Xaml.Controls.SystemBackdropElement` is a lightweight `FrameworkElement` that lets apps place a system backdrop such as Mica or Acrylic anywhere within the XAML layout, with a `CornerRadius` property for rounded backdrop areas. It closes a long-standing WinUI 3 gap, where in-app acrylic effects (previously straightforward in WinUI 2 via `AcrylicBrush.BackgroundSource`) had no direct equivalent.
>

</details>

<details><summary>Package deployment and validation</summary>

>
> The `Microsoft.Windows.Management.Deployment` namespace adds a package validation framework and a `PackageVolume` API for managing the storage volumes that packages are staged onto.
>
> - **Validators.** New `IPackageValidator` interface plus three built-in validators (`PackageCertificateEkuValidator`, `PackageFamilyNameValidator`, `PackageMinimumVersionValidator`) that can be attached to `AddPackageOptions` / `StagePackageOptions` via the new `PackageValidators` property. `PackageValidationEventArgs`, `PackageValidationEventSource`, and `PackageValidationHandler` carry validation events; `IsPackageValidationSupported` and `GetValidationEventSourceForUri` let callers probe support before invoking deployment.
> - **PackageVolume.** New `PackageVolume` type with `GetDefault`, `GetPackageVolumeByName`, `GetPackageVolumeByPath`, `AddAsync`, `RemoveAsync`, `GetAvailableSpaceAsync`, `IsOffline`, `SetDefault`, `SetOfflineAsync`, `SetOnlineAsync`, and `IsFeatureSupported` for runtime capability detection (`PackageVolumeFeature`).
>

</details>

<details><summary>Custom XAML Conditionals (IXamlCondition)</summary>

>
> The new `IXamlCondition` interface enables developers to define custom conditions that integrate with XAML's conditional namespace syntax and are evaluated at XAML parse time. This replaces the experimental `IXamlPredicate` interface. Custom conditions enable conditional XAML scenarios based on application-specific factors such as feature flags, device capabilities, business logic, configuration settings, and other runtime conditions.
>

</details>

<details><summary>Bug fixes</summary>

>
> - Fixed an issue where the WindowsAppSDK installer showed no progress during installation, making it appear stalled. The installer now provides clearer progress feedback.
> - Improved error handling for scenarios where `WindowsAppSDKSelfContained` is enabled for class libraries.
> - Fixed `MSB8027` and `LNK4042` build warnings caused by duplicate `ClCompile` items in Windows App SDK NuGet `.targets` files by moving preprocessor definitions from `<Target>` blocks to `<ItemDefinitionGroup>`. An opt-out workaround (`WindowsAppSDK_Arm64EcCompilerWorkaround`) is included for ARM64EC+LTCG builds to avoid a known MSVC internal compiler error.
> - Fixed a ListView crash that could occur during keyboard navigation (Tab/Shift+Tab) after the items list was updated.
> - Fixed an issue where WinUI 3 could crash if focus was moved to the `CoreWebView2Controller` while the controller was not visible.
> - Improved `DeleteIndex` reliability so it no longer fails with `ERROR_SHARING_VIOLATION` in some scenarios (App Content Search; remains in the experimental channel).
> - Fixed OCR bounding boxes returning negative values in some edge cases.
> - Fixed a Windows ML bug where calling `RegisterCertifiedAsync` again in the same process incorrectly returned 0 execution providers (EP).
>

</details>

<details><summary>Upgrading to 2.0</summary>

>
> This release contains a refactoring of nuget transitive references. To upgrade an existing C++ project, we recommend that you use tooling (`nuget.exe` or Visual Studio) to remove the existing Windows App SDK package reference and add the new reference. This works around upgrade issues with packages.config-based projects.
>
> **Upgrading from 2.0 Experimental7.** The experimental `Microsoft.WindowsAppSDK.ML` package shipped with a higher patch number than 2.0.0 stable, which can surface as a NuGet downgrade error (especially in C++ projects). If you were on Experimental7, follow the [NuGet Uninstall](/nuget/consume-packages/install-use-packages-visual-studio#uninstall-a-package) guide to remove the experimental package before referencing 2.0.0.
>

</details>

<details><summary>Notes on prior preview content</summary>

>
> - **App Content Search remains experimental.** The `Microsoft.Windows.Search.AppContentIndex` API surface (App Content Search) was removed in 2.0-Preview2 to improve fundamentals and ensure future compatibility. It is **not** included in 2.0.1 stable. To experiment with it, continue to use experimental releases. If you previously installed the experimental `Microsoft.Windows.Search` package, uninstall it via the [NuGet Uninstall](/nuget/consume-packages/install-use-packages-visual-studio#uninstall-a-package) guide before moving to 2.0.1 stable.
> - **APIs already shipped in 1.8 servicing.** Some experimental features that previewed during the 2.0 cycle were promoted to stable APIs in 1.8 servicing releases and are therefore not new in 2.0.1. They include `ModelCatalog` (1.8.3), `TextRewriter.RewriteCustomAsync` (1.8.4), and `SplitMenuFlyoutItem` plus `SplitMenuFlyoutItemAutomationPeer` (1.8.6). See the [1.8 release notes](./windows-app-sdk-1-8.md) for details.
>

</details>

<details><summary>Known issues</summary>

>
> - **`AICapabilities` is missing from 2.0.1.** `Microsoft.Windows.AI.AICapabilities` and `AICapabilityCategory.CopilotPlusPCCapable` (a Copilot+ PC capability check) shipped in 1.8.7 but did not make it into 2.0.1. We plan to restore them in the May release.
>

</details>

<details><summary>New APIs for 2.0.1</summary>

>
> This release includes the following new APIs compared to the stable 1.8.7 release:
>
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
> Microsoft.UI.Xaml.Controls
>
>     SystemBackdropElement
> ```
> ```
> Microsoft.UI.Xaml.Markup
>
>     IXamlCondition
> ```
> ```
> Microsoft.Windows.AI
>
>     AIFeatureReadyState
>         CapabilityMissing
>         NotCompatibleWithSystemHardware
>         OSUpdateNeeded
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

<details><summary>New APIs compared to 2.0-Preview2</summary>

>
> ```
> Microsoft.Windows.AI
>
>     AIFeatureReadyState
>         CapabilityMissing
>         NotCompatibleWithSystemHardware
>         OSUpdateNeeded
> ```
>

</details>

:::zone-end

:::zone pivot="preview"

## Version 2.0 Preview 2 (2.0.0-Preview2)

Released: **March 31, 2026** <br><br>

<details><summary>Windows ML</summary>

>
> The Microsoft.WindowsAppSDK.ML NuGet package has been refactored, adding a new base dependency. The existing ML NuGet package's functionality remains the same, but the core Windows ML features have been refactored into a base dependent package called Microsoft.Windows.AI.MachineLearning. This new package contains a minimal set of dependencies, but on C#/WinRT or C++/WinRT it only supports down to Windows 10 18362. If you need Windows 10 17763 support, use the existing Microsoft.WindowsAppSDK.ML package.
>
> For new transitive dependencies, we recommend using tooling to upgrade the package (nuget.exe or VS). See also: [Windows App SDK 1.6 release notes](/windows/apps/windows-app-sdk/release-notes/windows-app-sdk-1-6?pivots=stable).
>

</details>

<details><summary>Updated ONNX Runtime</summary>

>
> The version of ONNX Runtime has been updated to 1.24.4. See [ONNX Runtime versions](/windows/ai/new-windows-ml/onnx-versions) for more info.
>

</details>

<details><summary>Windows AI APIs</summary>

>
> Enforced Phi Silica APIs to be part of a Limited Access Feature.
>
> Added checks for various network and Windows Update errors during AI model installation, flagging durable failures so users can understand why model packages failed to install.
>
> Improved diagnosability for Text Intelligence APIs used in Windows AI scenarios.
>

</details>

<details><summary>App Content Search</summary>

>
> App Content Search has been removed from 2.0 preview while we work to improve fundamentals and ensure future compatibility as the underlying AI models change.In the meantime, please continue to use the experimental channel to try the latest changes.
>

</details>

<details><summary>WebView2 (WinUI 3)</summary>

>
> Enabled drag support for WebView2 content hosted in WinUI 3 applications. This capability was previously unsupported and is now available without introducing new public APIs. Note: Drop into WebView2 from external sources is already supported.
>
> #### Supported scenarios
>
> - **Standard drag-and-drop content**
>   Dragging text, HTML, images, and URLs is supported. Based on current behavior, dragging images outside the app results in a default file name (for example, `download.jpg`) when dropped into File Explorer.
>   *Example scenario:* A user selects text or an image inside WebView2 and drags it either within the app or to another destination such as File Explorer.
>
> - **Drag cancellation**
>   On-demand cancellation of an active drag operation is supported.
>   *Example scenario:* An app conditionally blocks drag operations for certain content types (such as restricted or ephemeral content) by cancelling the drag after it has been initiated.
>
> - **Custom drag visuals**
>   Custom drag UI, such as icons or previews, is supported to help users clearly identify the content being dragged.
>   *Example scenario:* An app displays a thumbnail preview or icon while a user drags media content, making it clear which item is currently being moved.
>
> - **Customizable drag data**
>   Editing and customizing drag data is supported, enabling app-specific scenarios such as dragging messages within the app (for example, as an alternative interaction for message forwarding).
>   *Example scenario:* An app customizes the drag payload to include contextual metadata, such as the originating application name. When the content is dropped, the destination can display or process information like "From \<app name\>" to indicate the source of the dragged content.
>
> #### Limitations
>
> - Support for additional drag data types is limited. The following formats are not currently supported:
>   - `DownloadURL`
>
> #### Requirements
>
> - **Minimum WebView2 Runtime version (Edge Beta channel):** 144.0.3719.11
>

</details>

<details><summary>Custom XAML Conditionals (IXamlCondition)</summary>

>
> The `IXamlCondition` interface enables developers to define custom conditions that integrate with XAML's conditional namespace syntax and are evaluated at XAML parse time. This is a rename and graduation of the experimental `IXamlPredicate` interface. Custom conditions enable conditional XAML scenarios based on application-specific factors such as feature flags, device capabilities, business logic, configuration settings, and other runtime conditions.
>

</details>

<details><summary>Bug fixes</summary>

>
> * Fixed MSB8027 and LNK4042 build warnings caused by duplicate ClCompile items in Windows App SDK NuGet .targets files by moving preprocessor definitions from `<Target>` blocks to `<ItemDefinitionGroup>`. An opt-out workaround (`WindowsAppSDK_Arm64EcCompilerWorkaround`) is included for ARM64EC+LTCG builds to avoid a known MSVC internal compiler error.
> * Fixed a ListView crash that could occur during keyboard navigation (Tab/Shift+Tab) after the items list was updated.
> * Fixed an issue where WinUI 3 could crash if focus was moved to the CoreWebView2Controller while the controller was not visible.
>

</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new and modified APIs compared to 2.0-Preview1:
> ```
> Microsoft.UI.Xaml.Markup
>
>     IXamlCondition
> ```
>

</details>

## Version 2.0 Preview 1 (2.0.0-Preview1)

Released: **February 13, 2026** <br><br>

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

## Version 2.2 Experimental 9 (2.2.2-Experimental9)

Released: **June 9, 2026**

This experimental release ships alongside [Windows App SDK 2.2.0 stable](#version-220) and generally brings over the changes from that release; see the [2.2.0 stable notes](#version-220) for those details. The sections below describe the experimental-only additions.
<br><br>

<details><summary>Language Model APIs on GPU [Experimental]</summary>

>
> The Language Model APIs now run on non-Copilot+ PCs equipped with a supported GPU, bringing local language model capabilities to a broader range of Windows 11 devices. Supported hardware includes NVIDIA GeForce RTX 30 series and newer with 6+ GB vRAM. GPU inference requires Developer Mode to be enabled and a Windows Insider Experimental Channel build.
>
> The GPU model is not pre-installed. It is downloaded on demand via `EnsureReadyAsync` through Windows Update. Apps should check `GetReadyState` and display a consent dialog before triggering the download. Users can manage the model at Settings > System > AI Components.
>
> For responsible AI guidance, see the new Transparency Note: Language Model APIs on Non-Copilot+ PCs.
>

</details>

<details><summary>Speech Recognition APIs [Experimental]</summary>

>
> New on-device speech recognition APIs in `Microsoft.Windows.AI.Speech` enable both batch and streaming speech-to-text. `BatchRecognition` recognizes a complete audio source in a single call, and `StreamingRecognition` raises `Recognizing` and `Recognized` events. Audio can be sourced from a device (`AudioConfiguration.FromAudioDevice`), file (`FromFile`), input stream (`FromStream`), or pushed by the caller via `SpeechAudioProvider` (16 kHz, 16-bit, single-channel PCM). The on-device model is managed via `SpeechRecognitionModel.EnsureReadyAsync` / `TryCreateAsync`, with download and load progress reported through `SpeechRecognitionModelProgress`.
>

</details>

<details><summary>New NpuType API</summary>

>
> A new `Microsoft.Windows.Workloads.NpuType` enum surfaces the NPU class on the current device (for example, `Qnn`, `Lnl`, `Stx`, `Win365`, `Unknown`, `None`), so apps and Workloads infrastructure can route AI workloads to the appropriate execution path.
>

</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new APIs compared to the **[2.1 Experimental 8](#version-21-experimental-8-214-experimental8)** release:
>
> ```
> Microsoft.UI.Xaml
>
>     Setter
>         ValueProperty
> ```
 > ```
> Microsoft.UI.Xaml.Markup
>
>     XamlBindingHelper
>         SetPropertyFromColor
>         SetPropertyFromCornerRadius
>         SetPropertyFromThickness
> ```
 > ```
> Microsoft.Windows.AI.Speech
>
>     AudioConfiguration
>     BatchRecognition
>     SpeechAudioProvider
>     SpeechContract
>     SpeechRecognitionModel
>     SpeechRecognitionModelProgress
>     SpeechRecognitionModelProgressStatus
>     SpeechRecognitionModelResult
>     StreamingRecognition
>     StreamingRecognizedEventArgs
>     StreamingRecognizingEventArgs
> ```
 > ```
> Microsoft.Windows.AI.Text.Experimental
>
>     LanguageModelExperimental
>         CompressPromptAsync
>         GenerateResponseAsync
>         GenerateResponseFromEmbeddingsAsync
>
>     LanguageModelOptionsExperimental
>         PreferredRetentionRatio
> ```
 > ```
> Microsoft.Windows.Workloads
>
>     NpuType
> ```
>

</details>

## Version 2.1 Experimental 8 (2.1.4-Experimental8)

Released: **May 21, 2026**

This experimental release ships alongside [Windows App SDK 2.1.3 stable](#version-213) and generally brings over the changes from that release; see the [2.1.3 stable notes](#version-213) for those details. The sections below describe the experimental-only additions.
<br><br>

<details><summary>Strongly typed result and status for structured JSON responses [Experimental]</summary>

>
> [`LanguageModelExperimental.GenerateStructuredJsonResponseAsync`](/windows/ai/apis/phi-silica-structured-output) (introduced in 2.0 Experimental 7) now returns a dedicated **`GenerateStructuredJsonResponseResult`** type with a **`GenerateStructuredJsonResponseStatus`** property, instead of the generic `LanguageModelResponseResult` / `LanguageModelResponseStatus` pair. The new status enum is specific to schema-constrained generation: it includes a `ResponseInvalidJson` value for the case where the model output did not satisfy the requested JSON schema, separate from generic language-model response failures.
>
> As part of this split, the `ResponseInvalidJson` value has been **removed** from the stable `LanguageModelResponseStatus` enum (where it had previously been added to support structured JSON output). Apps that handled `ResponseInvalidJson` from `LanguageModelResponseStatus` should move that handling onto the new `GenerateStructuredJsonResponseStatus` returned by `GenerateStructuredJsonResponseAsync`. Other callers of `LanguageModelResponseStatus` no longer need to account for a JSON-specific failure value.
>

</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new APIs compared to the **[2.0 Experimental 7](#version-20-experimental-7-200-experimental7)** release:
>
> ```
> Microsoft.UI.Xaml.Controls
>
>     TitleBar
>         AutoRefreshDragRegions
>         AutoRefreshDragRegionsProperty
>         GetIsDragRegion
>         IsDragRegionProperty
>         RecomputeDragRegions
>         SetIsDragRegion
> ```
> ```
> Microsoft.Windows.AI
>
>     AICapabilities
>
>     AICapabilityCategory
> ```
> ```
> Microsoft.Windows.AI.Text
>
>     LanguageModelLowRankAdapter
>
>     LanguageModelLowRankAdapterResult
>
>     LanguageModelOptions
>         LowRankAdapter
>
>     LanguageModelResponseStatus
>         IncompatibleLowRankAdapter
> ```
> ```
> Microsoft.Windows.AI.Text.Experimental
>
>     GenerateStructuredJsonResponseResult
>
>     GenerateStructuredJsonResponseStatus
>
>     LanguageModelOptionsExperimental
>         LowRankAdapter [Experimental]
> ```
>

</details>

## Version 2.0 Experimental 7 (2.0.0-Experimental7)

Released: **April 21, 2026** <br><br>

<details><summary>CMake Support [Experimental]</summary>

>
> Windows App SDK 2.0.0-experimental7 introduces experimental CMake support (under active development), enabling C++ developers to consume Windows App SDK NuGet packages from CMake-based projects.
>
> The CMake integration is built on [NuGetCMakePackage](https://github.com/mschofie/NuGetCMakePackage), an open-source CMake module that bridges the gap between NuGet package management and CMake's native `find_package()` workflow. NuGetCMakePackage handles downloading and restoring NuGet packages at configure time via `add_nuget_packages()`, probes each package for an embedded cmake config (at `build/cmake/<package>-config.cmake`), and exposes the package's headers, import libraries, runtime DLLs, and WinRT metadata as standard CMake targets.
>
> Each Windows App SDK NuGet component and dependent package now embeds a CMake configuration file under `build/cmake/`, which with the help of the NuGetCMakePackage library, allows CMake's `find_package()` to automatically discover targets, headers, libraries, and runtime DLLs without manual path configuration. This covers all component packages including Foundation, InteractiveExperiences, DWrite, Widgets, AI, ML, and WinUI, as well as dependencies such as Base and Runtime.
>
> For an overview of the CMake consumption model and getting started guidance, see the [CMake landing page](https://github.com/mschofie/NuGetCMakePackage/blob/develop/.github/copilot-instructions.md).
>
> This feature is experimental and is being flighted early to collect community feedback. The target naming conventions, and configuration patterns are subject to change based on developer input. Because this is an early flight, there is some ceremony and setup involved - please refer to the [CMake sample applications](https://github.com/microsoft/WindowsAppSDK-Samples/tree/release/2.0-experimental/Samples/CMake) which illustrate end-to-end configuration and usage across all four deployment scenarios (`SelfContained|FrameworkDependent` x `Packaged|Unpackaged`).
>
> We want to hear from you! If you encounter a scenario that isn't currently covered, have suggestions for improving the developer experience, or run into issues, please file feedback under the [Issues](https://github.com/microsoft/WindowsAppSDK/issues) tab in the Windows App SDK open-source repository or start a conversation in [Discussions](https://github.com/microsoft/WindowsAppSDK/discussions). Your input will directly shape the active development of this feature.
>

</details>

<details><summary>Changes brought forward from the Preview channel</summary>

>
> This experimental release rolls up the changes and fixes shipped in **[2.0-Preview1](#version-20-preview-1-200-preview1)** and **[2.0-Preview2](#version-20-preview-2-200-preview2)**. Items brought forward are summarized below; refer to the linked Preview release notes for full details.
>
> **From 2.0-Preview1:**
> * **Windows ML**: Fix for `RegisterCertifiedAsync` returning 0 execution providers when called again in the same process.
> * **Microsoft.UI.Content**: New `InputFocusController.ShouldShowKeyboardCues` property and new convenience API `PointerPoint.GetCurrentPoint`.
> * **Microsoft.Windows.Management.Deployment**: New `PackageVolume` APIs for parity+ with `Windows.Management.Deployment` (including `AddAsync`, `GetDefault`, `GetAvailableSpaceAsync`, `IsAppxInstallSupported`, `IsOffline`, `IsFeatureSupported`, and more). New `IPackageValidator` extensibility point on `AddPackageOptions` and `StagePackageOptions`, with three built-in validators: `PackageCertificateEkuValidator`, `PackageFamilyNameValidator`, and `PackageMinimumVersionValidator`.
> * **Microsoft.Windows.Storage.Pickers**:
>    * New `Title` properties on `FileOpenPicker`, `FileSavePicker`, and `FolderPicker`, enabling custom dialog titles.
>    * New `SettingsIdentifier` properties on `FileOpenPicker`, `FileSavePicker`, and `FolderPicker`, enabling picker instance-specific state across sessions.
>    * New `InitialFileTypeIndex` properties on `FileOpenPicker` and `FileSavePicker`, allowing developers to set the default selected file type filter by index (0-based).
>    * New `ShowOverwritePrompt` property on `FileSavePicker`. It defaults to `true` and controls whether the picker warns about overwriting when user picks an existing file via `FileSavePicker`.
>    * Changed the default behavior of `FileSavePicker`: starting from WindowsAppSDK 2.0, the `FileSavePicker` will NOT create an empty file when the user picks a non-existing file, allowing developers to decide when to make the file.
>    * New `PickMultipleFoldersAsync` method on `FolderPicker`, enabling selection of multiple folders in a single operation.
> * **Bug fixes**: Installer progress reporting; improved error handling for `WindowsAppSDKSelfContained` class libraries.
>
> **From 2.0-Preview2:**
> * **Windows AI APIs**: Phi Silica APIs are enforced as a Limited Access Feature; new `AIFeatureReadyState` values surface model-acquisition issues such as missing capabilities, incompatible hardware, and required OS updates; durable network/Windows Update install failures are now flagged; improved diagnosability for Text Intelligence APIs.
> * **Custom XAML Conditionals**: `IXamlCondition` graduates and renames the experimental `IXamlPredicate` interface, enabling custom conditions evaluated at XAML parse time.
> * **WebView2 (WinUI 3)**: Drag support for content hosted in WebView2 (text, HTML, images, URLs), drag cancellation, custom drag visuals, and customizable drag data. Requires WebView2 Runtime 144.0.3719.11 or later. (`DownloadURL` not yet supported.)
> * **Bug fixes**: MSB8027/LNK4042 build warnings from duplicate ClCompile items in `.targets` files (with an opt-out workaround `WindowsAppSDK_Arm64EcCompilerWorkaround` for ARM64EC+LTCG); ListView crash during keyboard navigation after items list updates; WinUI 3 crash when focus moved to a non-visible CoreWebView2Controller.
>

</details>

<details><summary>Video Super Resolution</summary>

>
> * API rename pass for clarity and consistency:
>   * `ScaleFrameStatus` → `VideoScalerStatus`
>   * `VideoScalerOptions.RegionOfInterests` → `VideoScalerOptions.RegionsOfInterest`
>   * `VideoScaler.ScaleFrame` → `VideoScaler.Scale` and `VideoScaler.ScaleImageBuffer`
> * Various bug fixes and quality improvements in VSR.
>

</details>

<details><summary>Windows AI Language Model</summary>

>
> * New `LanguageModelExperimental.GenerateStructuredJsonResponseAsync` API under `Microsoft.Windows.AI.Text.Experimental` for generating structured JSON output from a language model.
> * New `LanguageModelResponseStatus.ResponseInvalidJson` status value to indicate a model returned a response that did not parse as valid JSON.
>

</details>

<details><summary>App Content Indexer</summary>

>
> * New `AppContentIndexer.GetExistingIndexes` and `AppContentIndexer.IsContentKindSupported` APIs.
> * New `AppManagedOcrTextQueryMatch` type and `QueryMatchContentKind.AppManagedOcrText` value to support querying app content via OCR-derived text.
> * New `GetOrCreateIndexOptions.CreateAlways` to force creation of a fresh index.
> * New `ContentItemErrorDetail.InsufficientDiskSpace` to surface low-disk-space failures during indexing.
>

</details>

<details><summary>New or updated APIs</summary>

>
> This release includes the following new and modified experimental APIs compared to 2.0.0-experimental6:
>
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
> Microsoft.UI.Xaml.Markup
>
>     IXamlCondition
> ```
> ```
> Microsoft.Windows.AI
>
>     AIFeatureReadyResult
>         PackageInstallationFailed
>
>     AIFeatureReadyState
>         CapabilityMissing
>         NotCompatibleWithSystemHardware
>         OSUpdateNeeded
> ```
> ```
> Microsoft.Windows.AI.Text
>
>     LanguageModelResponseStatus
>         ResponseInvalidJson
> ```
> ```
> Microsoft.Windows.AI.Text.Experimental
>
>     LanguageModelExperimental
>         GenerateStructuredJsonResponseAsync
> ```
> ```
> Microsoft.Windows.AI.Video
>
>     VideoScaler
>         Scale
>         ScaleImageBuffer
>
>     VideoScalerOptions
>         RegionsOfInterest
>
>     VideoScalerStatus
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
>     AppContentIndexer
>         GetExistingIndexes
>         IsContentKindSupported
>
>     AppManagedOcrTextQueryMatch
>     ContentItemErrorDetail
>         InsufficientDiskSpace
>
>     GetOrCreateIndexOptions
>         CreateAlways
>
>     QueryMatchContentKind
>         AppManagedOcrText
> ```
> ```
> Microsoft.Windows.Storage.Pickers
>
>     FileOpenPicker
>         InitialFileTypeIndex
>         SettingsIdentifier
>         Title
>
>     FileSavePicker
>         InitialFileTypeIndex
>         SettingsIdentifier
>         ShowOverwritePrompt
>         Title
>
>     FolderPicker
>         PickMultipleFoldersAsync
>         SettingsIdentifier
>         Title
> ```
>

</details>

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
> * Stable Application Binary Interface (ABI) execution providers are enforced to ensure predictable behavior across devices, with non‑stable execution providers excluded from acquisition.
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
>         LineSpacing
>         LineSpacingProperty
>         MinItemSpacing
>         MinItemSpacingProperty
>
>     SystemBackdropElement
>     WrapPanel
>         ItemSpacing
>         ItemSpacingProperty
>         ItemsStretch
>         ItemsStretchProperty
>         LineSpacing
>         LineSpacingProperty
>
>     WrapPanelItemsStretch
> ```
> ```
> Microsoft.UI.Xaml.Markup
> 
>     IXamlPredicate
> ```
> ```
> Microsoft.Windows.AI.Video
>
>     VideoScaler
>         Dispose
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
>     QueryMatchContentKind
>     QueryMatchScope
>     RegionContentKind
>     TextLexicalMatchType
>     TextQueryMatch
>     TextQueryOptions
>     TextQuerySessionResult
> ```

>
</details>

<details><summary>Known issues</summary>

> 
> - An ONNX Runtime regression causes Video Super Resolution to fail on Qualcomm devices. A pending update to the Qualcomm NPU execution provider resolves this issue.
> 
</details>

## Version 2.0 Experimental 3 (2.0.0-Experimental3)

Released: **November 17, 2025** <br><br>

<details><summary>New SplitMenuFlyoutItem control</summary>

>
> The new experimental SplitMenuFlyoutItem control is designed to provide a split button experience within a menu flyout. This control will enable developers to expose a default primary action while also offering additional options through a submenu, ideal for condensing complex functionality into a smaller footprint and saving overall menu length.
>
> Along with the capabilities of MenuFlyoutItem and MenuFlyoutSubItem, the control comes with two other properties : `SubMenuPresenterStyle` and `SubMenuItemStyle`, which allows the customization of the submenu, like using GridView for the submenu presenter.
>
> ```
>     <Button Content="Open file">
>         <Button.Flyout>
>             <MenuFlyout>
>                 <SplitMenuFlyoutItem Text="Open with Notepad">
>                     <MenuFlyoutItem Text="Visual Studio" />
>                     <MenuFlyoutItem Text="VS Code" />
>                     <MenuFlyoutItem Text="Word" />
>                 </SplitMenuFlyoutItem>
>             </MenuFlyout>
>         </Button.Flyout>
>     </Button>
> ```
> 

</details>


<details><summary>WindowsML</summary>

>
> #### Renamed Types
>
> * Renamed `WinMLCatalogModel` to `ModelCatalog`
> * Renamed `CatalogModelSource` to `ModelCatalogSource`
>
> #### Method Updates
>
> * `CatalogModelInfo`: Renamed `GetInstance` to `GetInstanceAsync`
> * `ModelCatalogSource`: Renamed `CreateFromUri` to `CreateFromUriAsync`
> * `ModelCatalog`: Renamed `FindModel` to `FindModelAsync`
> * `ModelCatalog`: Renamed `FindAllModels` to `FindAllModelsAsync`
>
>
> #### Property Changes
>
> * Updated `CatalogModelInfo.Size` to `CatalogModelInfo.ModelSizeInBytes`
>
> #### Behavior Updates
>
> * Retrieve instance from `CatalogModelInstanceResult` using .GetInstance()
> * `CatalogModelStatus` now returns Ready or NotReady based on local availability
> * Added `CatalogModelInstanceStatus` to separate instance status from model status
>
> #### CatalogModelInfo Enhancements
>
> * Renamed `Alias` to `Name`
> * Renamed `Revision` to `Version`
> * Added `Publisher`
> * Removed `DisplayName`
>
> #### JSON Changes
>
> * Renamed `alias` to `id`.
> * Removed `modelType` and `description`.
> * Renamed `executionProvider` to `executionProviders`.
> * Updated `executionProviders` to be an array of JSON objects instead of a comma-separated list.
>
> #### Additional Changes
>
> * `ModelCatalog` now returns a list of Execution Providers (EPs) when an instance is created.
> * Added support for Windows 10 (1809) and above.
> * Added support for local files, including both regular files and MSIX packages.
> * Fixed crashes caused by invalid catalog JSON.
>

</details>

<details><summary>AppContentIndexer</summary>

>
> * The previous `AppIndexQuery` type, which included `GetNextTextMatches` and `GetNextImageMatches` methods, has been split into two distinct types: `AppIndexTextQuery` and `AppIndexImageQuery`. The `AppContentIndexer.CreateQuery` method has been replaced with: `CreateTextQuery` and `CreateImageQuery`.
>
> * These methods now return `AppIndexTextQuery` and `AppIndexImageQuery` respectively. To simplify usage, the options types have also been updated:
>
>     * Removed: `AppIndexQueryOptions`, `TextMatchOptions` and `ImageMatchOptions`
>     * Added: `TextQueryOptions` and `ImageQueryOptions`
>
> * The APIs in the `AppContentIndex` namespace that previously returned arrays now return `IVectorView` for improved consistency and performance.
>
>
> * The `AppContentIndexer.WaitForIndexingIdleAsync` method has been updated to accept a `TimeSpan` parameter instead of an integer, providing clearer and more flexible timeout handling.
>

</details>

<details><summary>Video Super Resolution AI API</summary>

> The `VideoScaler` API delivers real-time video enhancement through advanced AI upscaling, optimized for streams featuring people in conversation. It enables developers to provide sharper, clearer visuals across conferencing, streaming, and editing platforms, even under poor network conditions. The API supports customization of output resolution, frame rate, and regions of interest, with compatibility for multiple video formats including BGR, RGB, and NV12.
> 

</details>

<details><summary>Windows AI Text Rewriter Tone</summary>

> 
> The new RewriteCustomAsync API lets you provide an input string that guides Phi Silica in rewriting selected text. You can experiment with new creative styles like "Goofy" or "Pirate" to instantly transform your content.
> 
</details>

<details><summary>AI image generation</summary>

> 
> The `ImageGenerator` class leverages Stable Diffusion models to provide powerful image generation capabilities. It supports multiple generation scenarios:
>
> - **Text-to-Image:** Generate images from descriptive text prompts.  
> - **Image-to-Image:** Transform existing images based on text descriptions.  
> - **Magic Fill:** Fill masked regions of images with AI-generated content.  
> - **Coloring Book Style:** Generate coloring-book-style images.  
>
> - **Restyle:** Change the artistic style of existing images while preserving structure.
>
> All generated images are returned in **RGB8** format through [ImageBuffer](/windows/windows-app-sdk/api/winrt/microsoft.graphics.imaging.imagebuffer) objects. The API includes built-in [content safety filters](/azure/ai-services/content-safety/) and supports customizable generation parameters.
> 

</details>
<details><summary>Basic Text-to-Image Generation</summary>

>
> ```csharp
> using Microsoft.Windows.AI.Imaging;
> using Microsoft.Graphics.Imaging;
>  
> public async Task GenerateImageFromText()
> {
>     var readyState = ImageGenerator.GetReadyState();
>     if (readyState != AIFeatureReadyState.Ready)
>     {
>         var progress = new Progress<double>(p => Console.WriteLine($"Download progress: {p:P}"));
>         var result = await ImageGenerator.EnsureReadyAsync();
>         if (result.Status != AIFeatureReadyResultState.Success)
>         {
>             Console.WriteLine("Failed to prepare models");
>             return;
>         }
>     }
>  
>     using var generator = await ImageGenerator.CreateAsync();
>  
>     var options = new ImageGenerationOptions
>     {
>         MaxInferenceSteps = 6,
>         Creativity = 0.8,
>         Seed = 42
>     };
>  
>     var result = generator.GenerateImageFromTextPrompt("A beautiful sunset over a mountain lake", options);
>  
>     if (result.Status == ImageGeneratorResultStatus.Success)
>     {
>         await SaveImageBufferAsync(result.Image, "generated_image.png");
>     }
> }
> ```
>

</details>
<details><summary>New APIs for 2.0-experimental3</summary>

>
> This release includes the following new and modified experimental APIs compared to 2.0-experimental2:
>
>
> ```
> Microsoft.Graphics.Imaging
>
>     ImageBufferPixelFormat
>         Bgr8
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
> ```
> ```
> Microsoft.Windows.AI.Imaging
>
>     ImageFromImageGenerationOptions
>     ImageFromImageGenerationStyle
>     ImageFromTextGenerationOptions
>     ImageFromTextGenerationStyle
>     ImageGenerationOptions
>     ImageGenerator
>     ImageGeneratorContract
>     ImageGeneratorResult
>     ImageGeneratorResultStatus
>     TextRecognizer
>         RecognizeTextFromImage
>         RecognizeTextFromImageAsync
>
>     TextRecognizerOptions
> ```
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
> ```
> Microsoft.Windows.AI.Search.Experimental.AppContentIndex
>
>     AppContentIndexer
>         CreateImageQuery
>         CreateTextQuery
>         WaitForIndexingIdleAsync
>
>     AppIndexImageQuery
>     AppIndexTextQuery
>     ImageQueryOptions
>     TextQueryOptions
> ```
> ```
> Microsoft.Windows.AI.Text
>
>     TextRewriter
>         RewriteCustomAsync
> ```
> ```
> Microsoft.Windows.AI.Video
>
>     ScaleFrameStatus
>     VideoScaler
>     VideoScalerOptions
>     VideoScalerResult
> ```
>

</details>

<details><summary>Bug fixes</summary>

> 
> * Fixed bounding box calculation when text is rotated. In some circumstances, the OCR text matching within images reported inaccurate or empty region bounds when the text was rotated.
>

</details>

---

## Version 2.0 Experimental 2 (2.0.0-Experimental2)

Released: **November 6, 2025** <br><br>

> [!IMPORTANT]
> If you previously installed Windows App SDK 2.0 Experimental 1, follow the [NuGet Uninstall](/nuget/consume-packages/install-use-packages-visual-studio#uninstall-a-package) guide to remove the `Microsoft.WindowsAppSDK` NuGet Metapackage with version `2.0.250930001-experimental1` from your project and the associated WinAppSDK component packages from that release before trying this new version, since the previously released Experimental package has a higher version number than the current one.

<details><summary>App Content Search</summary>

> 
> The AppContentIndexer APIs empower developers to efficiently index app content, including text and images for rapid and relevant retrieval. Supporting both lexical (keyword-based) and semantic (meaning-based) searches, these APIs allow apps to deliver fast, relevant results based on user intent and context rather than just exact keywords.
> 
> This capability unlocks the following advanced scenarios:
> 
> - **Semantic Search**  
>   Apps can return results based on intent and meaning rather than exact keyword matches.  
>   *Example:* A query for **"project timeline"** can surface content that mentions **"schedule"** or **"delivery dates,"** even if those exact words weren't used.
> - **Retrieval-Augmented Generation (RAG)**  
>   Indexed content can serve as a knowledge base for generative AI models. When a user asks a question, the app retrieves the most relevant documents or snippets from its index and feeds them into the model, enabling accurate, context-aware responses grounded in real data.
> 
</details>

<details><summary>Windows ML Model Catalog</summary>

> 
> The Windows ML Model Catalog APIs enable your app or library to dynamically discover and download large AI model files from your own online model catalogs, eliminating the need to package these large files directly with your app or library. The model catalog helps ensure device compatibility by filtering models and downloading only those applicable for the specific Windows device in use.
> 
</details>

<details><summary>Persistent File and Folder Locations</summary>

> 
> The latest `Microsoft.Windows.Storage.Pickers` update streamlines file and folder selection by letting developers set initial and persistent folder locations, and by grouping filetype filters with clear labels for easier navigation.
> 
</details>

<details><summary>Relative Popup Positioning</summary>

> 
> The `PopupAnchor` API now allows `DesktopPopupSiteBridge` to support relative positioning by anchoring to its owning window or island, addressing the limitation where popups could only be positioned absolutely using screen coordinates.
> 
</details>

<details><summary>Input Routing for SystemVisual ContentIslands</summary>

> 
> The `InputUnderlyingWindowController` API enables developers to designate the target HWND for receiving input messages that were originally sent to a ContentIsland created from a SystemVisual (see [ContentIsland.CreateForSystemVisual](/windows/windows-app-sdk/api/winrt/microsoft.ui.content.contentisland.createforsystemvisual)).
> 
</details>
<details><summary>Flexible System Backdrop Placement</summary>

> 
> `SystemBackdropHost` enables placing a system backdrop (acrylic/mica) anywhere within an application's visual tree.
> 
</details>
<details><summary>XAML Layout Sequential Positioning</summary>

> 
> The `WrapPanel` is a WinUI XAML layout panel that arranges child elements in a sequential position from left to right, items overflowing the line will break to the next line automatically at the end of the containing panel. It is useful for responsive layouts.
> 
> This is a port of the existing [Windows Community Toolkit control](/dotnet/communitytoolkit/windows/primitives/wrappanel).
> 
</details>
<details><summary>New APIs for 2.0-experimental2</summary>

> 
> This release includes the following new and modified experimental APIs compared to 2.0-experimental1:
> 
> ```
> Microsoft.UI.Content
> 
>     PopupAnchor
> ```
> ```
> Microsoft.UI.Input
> 
>     InputUnderlyingWindowController
> ```
> ```
> Microsoft.UI.Xaml.Controls
> 
>     StretchChild
>     SystemBackdropHost
>     WrapPanel
> ```
> ```
> Microsoft.Windows.AI.Imaging
> 
>     ImageForegroundExtractor
>     ImageForegroundExtractorContract
> ```
> ```
> Microsoft.Windows.AI.Search.Experimental.AppContentIndex
> 
>     AppContentIndexContract
>     AppContentIndexer
>     AppContentIndexListener
>     AppIndexContentRegion
>     AppIndexQuery
>     AppIndexQueryMatch
>     AppIndexQueryOptions
>     AppIndexTextStreamEncoding
>     AppManagedImageQueryMatch
>     AppManagedIndexableAppContent
>     AppManagedTextQueryMatch
>     ContentItemReader
>     ContentItemStatus
>     ContentItemStatusResult
>     ContentRegionTextOptions
>     DeleteIndexResult
>     DeleteIndexStatus
>     DeleteIndexWhileInUseBehavior
>     GetOrCreateIndexOptions
>     GetOrCreateIndexResult
>     GetOrCreateIndexStatus
>     ImageMatchOptions
>     ImageQueryMatch
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
>     QueryMatchContentKind
>     QueryMatchScope
>     RegionContentKind
>     TextLexicalMatchType
>     TextMatchOptions
>     TextQueryMatch
> ```
> ```
> Microsoft.Windows.AI.Text.Experimental
> 
>     LanguageModelExperimental
>     LanguageModelExperimentalContract
>     LanguageModelOptionsExperimental
>     LowRankAdaptation
> ```
> ```
> Microsoft.Windows.Storage.Pickers
> 
>     FileOpenPicker
>         FileTypeChoices
>         SuggestedFolder
>         SuggestedStartFolder
> 
>     FileSavePicker
>         SuggestedStartFolder
> 
>     FolderPicker
>         SuggestedFolder
>         SuggestedStartFolder
> ```
</details>

<details><summary>Known issues</summary>

> 
> - `AppContentIndexer` APIs should be called from a background thread. Using it in the UI thread may hang or cause long pauses impacting the user experience.
> - Query results using `AppIndexQuery.GetNextTextMatches` and `AppIndexQuery.GetNextImageMatches` will be null when there are no matches instead of an empty list.
> - Image matches using `AppManagedImageQueryMatch.Subregion` based on OCR values may occasionally be inaccurate, particularly when the text is rotated or skewed.
> - Image matches using `AppManagedImageQueryMatch.Subregion` may sometimes include zero-size or extremely small rectangles, leading to inaccurate results.
> - An Empty query from `AppContentIndex.CreateQuery` can throw an exception.
> 
</details>

---

## Version 2.0 Experimental 1 (2.0.0-Experimental1)

Released: **October 2, 2025** <br><br>

<details><summary>Use on-device AI with Windows AI APIs</summary>

>
> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.
>
> The Windows App SDK incorporates advanced Windows AI capabilities, enabling developers to seamlessly integrate intelligent features into their applications. These enhancements include local AI functionalities such as responding to incoming prompts, recognizing text within images, describing image contents, extract objects from pictures, and more.
>
> For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.
>

</details>

<details><summary>Microsoft Windows ML</summary>

>
> The [Windows ML](/windows/ai/new-windows-ml/overview) Model Catalog APIs allow your app or library to dynamically download large AI model files from your own online model catalogs without shipping those large files directly with your app or library. Additionally, the model catalog will help filter which models are compatible with the Windows device it's running on, so that the right model is downloaded to the device.
>
> **Key benefits:** 
>
> - **Add catalogs**: Add one or many online catalogs  
> - **Discover compatible models**: Automatically find models that work with the user's hardware and execution providers  
> - **Download models**: Download and store models from various sources  
> - **Share models across apps**: If multiple applications use the same catalog source, the models will be shared on disk without duplicating downloads  
>

</details>

<details><summary>Bug fixes</summary>

>
> - Fixed an issue in DeploymentManager which resulted in it incorrectly reporting PackageInstallRequired in some cases.
>

</details>

<details><summary>New APIs</summary>

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
>     ChildSiteLink
>         IsBelowContent
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
>     IContentSiteBridgeEndpointConnectionPrivate
>     PopupAnchoringOptions
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
> Microsoft.Windows.AI.MachineLearning
>
>     CatalogModelInfo
>     CatalogModelInstance
>     CatalogModelInstanceResult
>     CatalogModelSource
>     CatalogModelStatus
>     WinMLModelCatalog
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
> Microsoft.Windows.Vision
>
>     ScreenRegionBoundingBox
>     ScreenRegionDetectionContract
>     ScreenRegionLabel
> ```
>

</details>

:::zone-end
