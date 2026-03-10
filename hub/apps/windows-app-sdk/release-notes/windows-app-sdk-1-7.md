---
title: Windows App SDK 1.7 release notes
description: Provides information about what's new in Windows App SDK 1.7.
ms.topic: release-notes
ms.date: 09/22/2025
keywords: windows win32, windows app development, Windows App SDK, release notes
ms.localizationpriority: medium
zone_pivot_groups: wasdk-release-channels
---

# Windows App SDK 1.7 release notes

[!INCLUDE [wasdk-releasenotes](../../../includes/wasdk-release-notes.md)]

:::zone pivot="stable"

## Version 1.7.9 (1.7.260224002)

Released: **March 10, 2026** <br><br>

<details><summary>Bug fixes</summary>

>
> * Fixed a pointer arithmetic issue in the app-instance activation-redirection shared-memory queue that could cause incorrect behavior when using `AppInstance.RedirectActivationTo`. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): AppLifecycle_SharedMemoryRedirectionQueueFix)
> * Fixed an issue in the deployment manager where activity data was being incorrectly reset during package installation and license operations. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): DeploymentManager_ActivityCollectionFix)
>

</details>


## Version 1.7.8 (1.7.260208005)

Released: **February 10, 2026** <br><br>

<details><summary>Bug fixes</summary>

>
> * Fixed a package downgrade init failure and added 1.7.8 runtime support—ensuring reliable deployment and correct app recognition across configurations. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): DeploymentManager_PackageDowngradeFix).
> 

</details>


## Version 1.7.7 (1.7.260114001)

Released: **January 13, 2026** <br><br>

<details><summary>Bug fixes</summary>

>
> * Fixed an issue that prevented Image Super Resolution from being available for some applications. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A)
> 

</details>

## Version 1.7.6 (1.7.251107005)

Released: **November 11, 2025** <br><br>

<details><summary>Bug fixes</summary>

>
> - Fixed a potential crash if OrientedVirtualizingPanel hits an overflow when computing bounds. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): OrientedVirtualizingPanel_FixBoundsOverflow).
> - Fixed a potential crash on app launch if the computer has not been rebooted in a long time. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): ContentIsland_StartupCrashWhenLongRunning).
> 

</details>

<details><summary>Windows AI Object Erase</summary>

> 
> Object Erase can be used to remove objects from images. The model takes both an image and a greyscale mask indicating the object to be removed, erases the masked area from the image, and replaces the erased area with the image background.
> 

</details>

<details><summary>New APIs for 1.7.6</summary>

>
> This release includes the following new APIs compared to the 1.7.5 release:
>
> ```
> Microsoft.Windows.AI.Imaging
>
>     ImageObjectRemover
>     ImageObjectRemoverContract
> ```
>

</details>

---

## Version 1.7.5 (1.7.251014001)

<details><summary>Bug fixes</summary>

> * Fixed issue where mouse wheel scrolling doesn't work when the Windows setting "Scroll inactive windows when hovering over them" is off. For more info, see GitHub issue [#10091](https://github.com/microsoft/microsoft-ui-xaml/issues/10091). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): InputPointerSource_FixScrollForInactiveWindowSetting)
> * Fixed an issue that caused a false positive warning about a missing mspdbcmf.exe. For more info, see GitHub issue [#5262](https://github.com/microsoft/WindowsAppSDK/issues/5262). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, build .targets change)
>

</details>

---

## Version 1.7.4 (1.7.250909003)

<details><summary>Bug fixes</summary>

>
> - Fixed an issue where the hover effects of other windows for the app could flicker when at least one window had ExtendsContentIntoTitleBar set to true. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): XamlIsland_ExitNonClientArea). For more information, see GitHub issue [#10572](https://github.com/microsoft/microsoft-ui-xaml/issues/10572).
> - Fixed an issue where UnhandledExceptions were not being propagated to managed code. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): DispatcherQueue_PropagateException). For more information, see GitHub issue [#10447](https://github.com/microsoft/microsoft-ui-xaml/issues/10447).
> - Fixed an occasional crash in `DirectUI::PointerRoutedEventArgs::GetCurrentPointImpl`. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): GetIntermediatePointsImpl_Crash)
> - Apps with an incorrect activation manifest no longer crash in certain situations but instead return an error. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): DesktopSiteBridge_ActivationErrorCrash)
> - Fixed an issue where setting `NavigationView.SelectedItem` as null did not correctly clear the selection state in collapsed mode. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): NavigationView_ClearSelectedItem)
> - Fixed certain corrupted upgrade scenarios from 1.6 to 1.7 by ensuring version numbers on certain binaries are consistently increasing. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, file version change)
> - Fixed an issue where resizing the window would fire an AppWindow.Changed event with `AppWindowChangedEventArgs.DidPresenterChange` set to true. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): AppWindow_NoDidPresenterChangeOnRestore)
> - Fixed a potential crash on window close if an InputPointerSource is disposed during certain reentrant scenarios. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): InputPointerSource_ReentrantDisposedCrash)
> - Fixed an issue where the installer failed to update the Singleton package if an older version was running in the background. The fix ensures AddPackage is always called with ForceTargetAppShutdown for the Singleton and restarts the NotificationLongRunningPlatform to complete the update. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, installer change)
>

</details>

---

## Version 1.7.3 (1.7.250606001)

<details><summary>Windows AI APIs</summary>

>
> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.
>
> The Windows App SDK now includes a suite of artificial intelligence (AI) APIs that can be used with a local language model to perform a variety of tasks on Copilot+ PCs. Your apps can now intelligently respond to prompts, recognize text within images, describe the content of images, remove objects from images, and more.
>
> For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.
>

</details>

<details><summary>New Rank property for Widgets</summary>

>
> Added a new `Rank` property to Widgets. Rank may be used by the platform's recommendation engine to sort Widgets from a same application package identity. Should multiple widgets from the same provider be recommended for a UI surface, the Rank property will determine the order in which they appear. The Rank property does not change how a Widget is placed compared to other provider's Widgets, nor does it affect the chance a Widget will be recommended.
>

</details>

<details><summary>Bug fixes</summary>

>
> - Added the following sentence to section 1a of the .nupkg license: When building Generative AI applications follow the guidelines in [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai).
> - Fixed a potential crash in ApplicationDataProvider::GetStateFolderUris caused by reentrancy. For more info, see GitHub issue [#10513](https://github.com/microsoft/microsoft-ui-xaml/issues/10513). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): ApplicationDataProvider_ReentrancyProtection)
> - Fixed a potential crash in WindowChrome::SetTitleBar when closing a window. For more info, see GitHub issue [#9203](https://github.com/microsoft/microsoft-ui-xaml/issues/9203). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): WindowChrome_SetTitleBarCrash)
> - Fixed a potential crash in PointerInputObserverWinRT::FlushCoalescedInput_Callback when there is reentrancy while processing input. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): InputPointerSource_FlushReentrancyCrash)
>

</details>

<details><summary>New APIs for 1.7.3</summary>

>  
> This release includes the following new APIs compared to the previous 1.7 release:
>
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

---

## Version 1.7.2 (1.7.250513003)

<details><summary>Windows AI APIs</summary>

>
> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.
>
> The Windows App SDK now includes a suite of artificial intelligence (AI) APIs that can be used with a local language model to perform a variety of tasks on Copilot+ PCs. Your apps can now intelligently respond to prompts, recognize text within images, describe the content of images, remove objects from images, and more.
>
> For information on responsible development practices utilized during the creation of the Windows AI APIs, which can also be applied when creating AI-assisted features, consult the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.
>
> ##### Phi Silica Text Intelligence 
>
> With [**Phi Silica**](/windows/ai/apis/phi-silica), Microsoft's most powerful NPU-tuned local language model, you can specify it to perform common tasks like summarizing a piece of text, rewriting a piece of text for clarity, and converting text to a table format. Phi Silica is optimized for efficiency and performance on Windows Copilot+ PCs devices while still offering many of the capabilities found in Large Language Models (LLMs). 
>
> See [Get started with Phi Silica in the Windows App SDK](/windows/ai/apis/phi-silica) and [API ref for Phi Silica in the Windows App SDK](/windows/ai/apis/phi-silica-api-ref) for more information.
>
> ##### Image Description 
> The Image Description APIs enable the generation of textual descriptions of images. The length and type of these descriptions can be configured to meet accessibility requirements, ranging from short captions to long descriptions.
>
> For additional details, see [What can I do with Image Description?](/windows/ai/apis/imaging#what-can-i-do-with-image-description) and [API ref for AI imaging features in the Windows App SDK](/windows/ai/apis/imaging-api-ref).
>
> ##### Text Recognition
>
> Text recognition, also known as optical character recognition (OCR), detects and extracts text within images, converting it into machine-readable character streams. These APIs identify characters, words, lines, polygonal text boundaries, and provide confidence levels for each match. Benefiting from NPU-assisted acceleration, the Windows AI AI-assisted APIs perform faster and more accurately than the legacy [Windows.Media.Ocr.OcrEngine](/uwp/api/windows.media.ocr.ocrengine) APIs.
>
> For additional details, see [Get Started with Text Recognition (OCR) in the Windows App SDK](/windows/ai/apis/text-recognition) and [API ref for AI-backed Text Recognition (OCR) in the Windows App SDK](/windows/ai/apis/text-recognition-api-ref).
>
> ##### Image Super Resolution 
> The 'ImageScaler' APIs can increase the sharpness and clarity of an image and upscale the image by up to 8x its original resolution.
>
> For additional details, see [What can I do with Image Super Resolution?](/windows/ai/apis/imaging#what-can-i-do-with-image-super-resolution) and [API ref for AI imaging features in the Windows App SDK](/windows/ai/apis/imaging-api-ref).
>
> ##### Image Segmentation 
>
> The Image Segmentation APIs allow for the identification of specific objects within an image. By inputting an image and a "hints" object, the model returns a mask of the identified object.
>
> For additional details, see [What can I do with Image Segmentation?](/windows/ai/apis/imaging#what-can-i-do-with-image-segmentation) and [API ref for AI imaging features in the Windows App SDK](/windows/ai/apis/imaging-api-ref).
>

</details>

<details><summary>ApplicationData.MachinePath folder creation support</summary>

>
> ApplicationData.MachineFolder is now easier to use on Windows >=10.0.26100.0 (Ge). Windows will [create the Machine folder](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/applicationdata/ApplicationData.md#343-machine-path-creationdeletion) when a [package manifesting opt-in support](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/applicationdata/ApplicationData.md#342-manifested-opt-in) is added to a system if Windows App SDK 1.7.2 is present on the system. For more details see the [ApplicationData spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/applicationdata/ApplicationData.md).
>

</details>

<details><summary>Bugfixes</summary>

>
> - Fixed PackageDeploymentManager telemetry to properly capture completion status. For more info, see GitHub issue [#5296](https://github.com/microsoft/WindowsAppSDK/pull/5296). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A)
> - Fixed a crash when using pen input on an x86 app. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): InputStateManager_PenInputCrashX86)
> - Fixed a potential crash if the window is already destroyed when WinUI is attempting to initialize for scrolling. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): ActivateDirectManipulationManager_CheckCanInit)
> - Fixed the WINDOWSAPPSDK_RELEASE_PATCH define and Microsoft::WindowsAppSDK::Release::Patch values in WindowsAppSDK-VersionInfo.h to not always be 0. The define is now the yymmdd date of the build, and the Patch value is the mmdd date. This change provides better runtime information on the version being used without changing any variable sizes or the version scheme. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, header change)
> - Fixed a potential issue in the Bootstrapper if it is used to load a 1.6 or earlier version of Windows App SDK. For more info, see GitHub issue [#5349](https://github.com/microsoft/WindowsAppSDK/pull/5349). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A)
> - Fixed an issue where using MSBuild to build a single-project app could incorrectly fail with a build error if it didn't have a correct launchSettings.json. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, build .targets change)
> - Improved the performance of rendering the first frame on application launch. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): DwmCoreI_OptimizeFirstFrameLatency)
>

</details>

<details><summary>New APIs for 1.7.2</summary>

>  
> This release includes the following new APIs compared to the previous 1.7 release:
>
> ```
> Microsoft.Graphics.Imaging
>
>     ImageBuffer
>     ImageBufferContract
>     ImageBufferPixelFormat
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
> Microsoft.Windows.AI.ContentSafety
>
>     ContentFilterOptions
>     ContentSafetyContract
>     ImageContentFilterSeverity
>     SeverityLevel
>     TextContentFilterSeverity
> ```
> ```
> Microsoft.Windows.AI.Imaging
>
>     ImageDescriptionContract
>     ImageDescriptionGenerator
>     ImageDescriptionKind
>     ImageDescriptionResult
>     ImageDescriptionResultStatus
>     ImageObjectExtractor
>     ImageObjectExtractorContract
>     ImageObjectExtractorHint
>     ImageScaler
>     ImageScalerContract
>     RecognizedLine
>     RecognizedLineStyle
>     RecognizedText
>     RecognizedTextBoundingBox
>     RecognizedWord
>     TextRecognitionContract
>     TextRecognizer
> ```
> ```
> Microsoft.Windows.AI.Text
>
>     LanguageModel
>     LanguageModelContext
>     LanguageModelContract
>     LanguageModelOptions
>     LanguageModelResponseResult
>     LanguageModelResponseStatus
>     TextIntelligenceContract
>     TextRewriter
>     TextSummarizer
>     TextToTableConverter
>     TextToTableResponseResult
>     TextToTableRow
> ```
> ```
> Microsoft.Windows.Workloads
>
>     WorkloadPriority
>     WorkloadsContract
> ```
>

</details>

---

## Version 1.7.1 (1.7.250401001)

<details><summary>Bugfixes</summary>

>  
> - Improved the telemetry for failure scenarios in WindowsAppRuntimeInstall-&lt;arch&gt;.exe. For more info, see GitHub issue [#5289](https://github.com/microsoft/WindowsAppSDK/pull/5289). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, installer change).
> - Fixed an issue where pointer input would stop working when using arrow keys at the same time. For more info, see GitHub issue [#10126](https://github.com/microsoft/microsoft-ui-xaml/issues/10126). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FixStuckPointerInputQueue).
> - Fixed an issue where apps in remote desktop stop responding to pointer input. For more info, see GitHub issue [#10009](https://github.com/microsoft/microsoft-ui-xaml/issues/10009). (This is the same fix as the pointer input plus arrow keys fix, due to remote desktop automatically sending some key input during the switch away and back.)  ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FixStuckPointerInputQueue).
> - Fixed a potential crash trying to restore focus if a window activation event is delivered for a window which is closing. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FixWindowCloseFocusCrash)
> - Fixed a performance regression introduced in Windows App SDK 1.6 due to WinUI binaries missing some linker optimizations. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): N/A, changed linker options)
> - Fixed a potential crash if ProgressBar::SetProgressBarIndicatorWidth is called on a ProgressBar which is not in the tree. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FixSetProgressBarIndicatorWidthCrash)
> - Fixed a potential crash caused by CPopup::EnsureBridgeClosed sometimes triggering reentrancy. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FixPopupClosingReentrancyCrash)
> - Fixed a potential crash when closing a popup due to CUIElement::FlushPendingKeepVisibleOperations using a null children collection. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FixPopupUnloadingCrash)
> - Fixed PackageDeploymentManager.EnsurePackage\*Ready to ensure version supersedence. For more info, see GitHub issue [#5191](https://github.com/microsoft/WindowsAppSDK/pull/5191).  ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): EnsurePackageReadyVersionSupercedence)
> - Fixed a potential crash caused by WebView2::UpdateCoreWebViewVisibility sometimes triggering reentrancy. For more info, see GitHub issue [#10305](https://github.com/microsoft/microsoft-ui-xaml/issues/10305). ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FixWebViewVisibilityReentrancyCrash)
> - Fixed an issue where app UI sometimes permanently freezes and can stop rendering due to the DispatcherQueue getting stuck. ([RuntimeCompatibilityChange](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.runtimecompatibilityoptions.disabledchanges): FixRandomUIFreezeInDispatcher)
>

</details>

---

## Version 1.7.0 (1.7.250310001)

<details><summary>New Badge Notifications</summary>

>
> The notification badge conveys a summary or status information specific to an app. This can be numeric (1-99) or a glyph from one of the system-provided glyphs. This new functionality provides an easy way for apps to show status, such as number of unread mails in a mail app or number of new posts in a social media app.
>
> For more info, see GitHub [#4926](https://github.com/microsoft/WindowsAppSDK/issues/4926).
>

</details>

<details><summary>New CameraCaptureUI API</summary>

>
> Developers have encountered challenges in the desktop environments due to WinRT CameraCaptureUI being dependent on CoreWindows, and lack of InitializeWithWindow support. The team has released this new `Microsoft.Windows.Media.Capture.CameraCaptureUI` API to Windows App SDK to provide a streamlined solution with feature parity, now supporting WindowID in the constructor for enhanced desktop compatibility.
>
> For more info, see GitHub issue [#4721](https://github.com/microsoft/WindowsAppSDK/issues/4721).
>

</details>

<details><summary>New Authentication API</summary>

>
> A new `OAuth2Manager` API provides a streamlined solution for web authentication, offering OAuth 2.0 capabilities with full feature parity across all Windows platforms supported by Windows App SDK. This new Authentication Manager is different from the public WebAuthentication Broker API, as it better aligns with OAuth best practices.
>
> For more info, see GitHub issue [#4772](https://github.com/microsoft/WindowsAppSDK/issues/4772).
>

</details>

<details><summary>New Background Task support</summary>

>
> Background tasks are app components that run in the background without a user interface, performing actions like download files, syncing data, sending notifications or updating files. The new `BackgroundTaskBuilder` API provides Windows App SDK dependent apps the ability to directly register the full trust COM components with background tasks, removing the need to implement a workaround.
>
> For more info, see GitHub [#4831](https://github.com/microsoft/WindowsAppSDK/issues/4831).
>

</details>

<details><summary>New TitleBar control</summary>

>
> A new `TitleBar` control makes it much easier to create a great, customizable titlebar for your app. Configure properties such as the titlebar icon, Title, and Subtitle, include an integrated back button, or even add a custom control like a search box! The control includes robust titlebar capabilities like empty-space draggable regions, theme responsiveness, caption buttons, and built-in accessibility support so you can focus on your personalized design and still get the same reliable titlebar as the default experience. 
>
> For more info, see GitHub [#10056](https://github.com/microsoft/microsoft-ui-xaml/issues/10056).
>

</details>

<details><summary>Support for MathML</summary>

>
> `RichEditBox` now supports MathML, via `RichEditTextDocument.SetMathMode` and `RichEditTextDocument.SetMathML`. 
>
> For more info, see GitHub [#4196](https://github.com/microsoft/microsoft-ui-xaml/issues/4196).
>

</details>

<details><summary>Enhanced runtime</summary>

>
> * Windows App SDK's [Dynamic Dependencies APIs](/windows/apps/desktop/modernize/framework-packages/use-the-dynamic-dependency-api) delegate all calls to Windows 11's implementation when running on \>= Windows 11 24H2 (10.0.26100.0) providing improved performance and robustness. This holds true for all C/C++ (Mdd*()) and WinRT (namespace Microsoft.Windows.ApplicationModel.DynamicDependency) APIs.
>     * Packaged processes calling Windows App SDK's Dynamic Dependencies APIs is now supported on \>= Windows 11 24H2 (10.0.26100.0). This is still unsupported on older systems (the Windows App SDK implementation doesn't support packaged apps).
>     * This has no impact to the developer experience. Callers can continue using the [Bootstrapper API](/windows/windows-app-sdk/api/win32/_bootstrap/) to add the Windows App SDK framework package to the calling process' package graph.
>     * For more info, see GitHub PR [#4949](https://github.com/microsoft/WindowsAppSDK/pull/4949).
> * Undocked Registration-free WinRT (URFW) is not enabled on \>= Windows 11 24H2 (10.0.26100.0). The OS' implementation handles all [Registration-free WinRT](https://blogs.windows.com/windowsdeveloper/2019/04/30/enhancing-non-packaged-desktop-apps-using-windows-runtime-components/) activity on these systems providing improved performance and robustness. For more info, see GitHub PR [#4949](https://github.com/microsoft/WindowsAppSDK/pull/4949).
> * Detours is not used on \>= Windows 11 24H2 (10.0.26100.0). Detours was only used by Windows App SDK's implementations of Dynamic Dependencies and Registration-free WinRT, but as those features are now handled by the OS' implementations there's no need for them to initialize or otherwise wire up Detours. This provides a small performance gain when loading Microsoft.WindowsAppRuntime.dll. For more info, see GitHub PR [#4949](https://github.com/microsoft/WindowsAppSDK/pull/4949).
>

</details>

<details><summary>New AppWindow APIs</summary>

>
> New `AppWindow` APIs make it easier to control your app windows to create a great experience. New capabilities include using `SetTaskBarIcon` and `SetTitleBarIcon` to independently set the taskbar and titlebar icons, using `AppWindowTitleBar.PreferredTheme` to set the light/dark theme of the titlebar, and using new properties like `OverlappedPresenter.PreferredMinimumWidth` and `OverlappedPresenter.PreferredMaximumHeight` to set a minimum or maximum width or height for the window.
>

</details>

<details><summary>New Island APIs</summary>

>
> The updates in the Microsoft.UI.Content namespace introduce several significant enhancements and new features aimed at improving the functionality and interoperability of the ContentIsland APIs. These changes are designed to support new hosting scenarios, enhance rendering capabilities, and ensure better synchronization of input and accessibility states. Key updates include:
> 1. New primitives for hosting ContentIslands:
>      * `DesktopPopupSiteBridge`: Enables hosting a `ContentIsland` in the environment of a Win32 window with WS_POPUP style, facilitating scenarios where applications use popup windows for dialog boxes and message boxes.
>      * `ChildSiteLink`: Allows a parent `ContentIsland` to host a nested child `ContentIsland`, providing a seamless partitioning of the rendering surface without user experience seams.
>      * `DesktopAttachedSiteBridge`: Attaches to an existing Win32 window instead of creating a new one, designed to host a `ContentIsland` with Windows.UI.Composition.Visuals at the root of the Win32 window hierarchy, ensuring full control over Win32-based input processing and accessibility.
> 2. Enhanced rendering and input synchronization:
>      * The `LocalToParentTransformMatrix` and `ActualSize` properties of a `ChildSiteLink` are updated relative to the parent `ContentIsland` before rendering, avoiding latency and ensuring synchronized input and accessibility states.
> 3. ContentIslands with Windows.UI.Composition.Visuals:
>      * `ContentIsland` can use Windows.UI.Composition.Visuals for rendering and Win32 window APIs for input processing, enabling interoperability with applications that use legacy UX frameworks. This allows for a gradual adoption of newer UX frameworks layered on top of the Windows App SDK Scene Graph, such as WinUI and React Native for Windows on Fabric.
> These updates collectively enhance the flexibility, performance, and interoperability of the ContentIsland APIs, enabling developers to create more sophisticated and responsive applications.
>
> Additionally, the updates in the Microsoft.UI.Xaml namespace introduce a new `XamlIsland` API, which allows for the hosting of Xaml content within a SiteBridge or a `ChildSiteLink`. The `XamlIsland` offers greater flexibility compared to the `DesktopWindowXamlSource` API. While `DesktopWindowXamlSource` requires hosting within an existing Win32 window, the `XamlIsland` exposes a `ContentIsland`, enabling more options for hosting Xaml content.
>

</details>

<details><summary>Other notable changes</summary>

>
> * New `RuntimeCompatibilityOptions` support will allow more control over how servicing changes affect apps. For more info, see GitHub [#4966](https://github.com/microsoft/WindowsAppSDK/issues/4966).
> * A new `ReleaseInfo` API provides easy access to the version of the Windows App SDK Runtime in use. For more info, see GitHub [#2893](https://github.com/microsoft/WindowsAppSDK/issues/2893).
> * Note: Windows AI APIs are not included this release. To experiment with these APIs, please continue to use the 1.7-experimental3 release and share your feedback!
>

</details>

<details><summary>New APIs for 1.7.0</summary>

>
> This release includes the following new APIs compared to the stable 1.6 release:
> 
> ```
> Microsoft.Security.Authentication.OAuth
> 
>     AuthFailure
>     AuthRequestParams
>     AuthRequestResult
>     AuthResponse
>     ClientAuthentication
>     CodeChallengeMethodKind
>     OAuth2Manager
>     TokenFailure
>     TokenFailureKind
>     TokenRequestParams
>     TokenRequestResult
>     TokenResponse
> ```
> ```
> Microsoft.UI.Content
> 
>     ChildSiteLink
>     ContentAutomationOptions
>     ContentEnvironmentStateChangedEventArgs
>         DidDisplayScaleChange
> 
>    ContentIsland
>         AutomationOption
>         Children
>         CreateForSystemVisual
>         FindAllForSystemCompositor
>        FragmentRootAutomationProvider
>         GetBySystemVisual
>         LocalToClientTransformMatrix
>         LocalToParentTransformMatrix
>         NextSiblingAutomationProvider
>         ParentAutomationProvider
>         Popups
>         PreviousSiblingAutomationProvider
>         ProcessesKeyboardInput
>         ProcessesPointerInput
> 
>     ContentIslandEnvironment
>         DisplayScale
> 
>     ContentIslandStateChangedEventArgs
>         DidLocalToClientTransformMatrixChange
>         DidLocalToParentTransformMatrixChange
> 
>     ContentSite
>         LocalToClientTransformMatrix
>         LocalToParentTransformMatrix
>         ProcessesKeyboardInput
>         ProcessesPointerInput
> 
>     ContentSiteAutomationProviderRequestedEventArgs
>     ContentSiteEnvironment
>         DisplayScale
> 
>     ContentSiteEnvironmentView
>         DisplayScale
> 
>     ContentSiteView
>         AutomationOption
>         LocalToClientTransformMatrix
>         LocalToParentTransformMatrix
>         ProcessesKeyboardInput
>         ProcessesPointerInput
> 
>     DesktopAttachedSiteBridge
>     DesktopChildSiteBridge
>         CreateWithDispatcherQueue
> 
>     DesktopPopupSiteBridge
>     IContentSiteAutomation
>     IContentSiteInput
>     IContentSiteLink
> ```
> ```
> Microsoft.UI.Input
> 
>     InputFocusNavigationHost
>         GetForSiteLink
> ```
> ```
> Microsoft.UI.Text
> 
>     RichEditTextDocument
>         GetMathML
>         GetMathMode
>         SetMathML
>         SetMathMode
> ```
> ```
> Microsoft.UI.Windowing
> 
>     AppWindow
>         SetTaskbarIcon
>         SetTitleBarIcon
> 
>     AppWindowTitleBar
>         PreferredTheme
> 
>     OverlappedPresenter
>         PreferredMaximumHeight
>         PreferredMaximumWidth
>         PreferredMinimumHeight
>         PreferredMinimumWidth
> 
>     TitleBarTheme
> ```
> ```
> Microsoft.UI.Xaml
> 
>     XamlIsland
>     XamlRoot
>         ContentIsland
> ```
> ```
> Microsoft.UI.Xaml.Controls
> 
>     TitleBar
>     TitleBarAutomationPeer
>     TitleBarTemplateSettings
> ```
> ```
> Microsoft.Windows.ApplicationModel.Background
> 
>     BackgroundTaskBuilder
> ```
> ```
> Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
> 
>     Task
> ```
> ```
> Microsoft.Windows.ApplicationModel.WindowsAppRuntime
> 
>     ReleaseInfo
>     RuntimeCompatibilityChange
>     RuntimeCompatibilityOptions
>     RuntimeInfo
>     WindowsAppRuntimeVersion
> ```
> ```
> Microsoft.Windows.BadgeNotifications
> 
>     BadgeNotificationGlyph
>     BadgeNotificationManager
> ```
> ```
> Microsoft.Windows.Media.Capture
> 
>     CameraCaptureUI
>     CameraCaptureUIMaxPhotoResolution
>     CameraCaptureUIMaxVideoResolution
>     CameraCaptureUIMode
>     CameraCaptureUIPhotoCaptureSettings
>     CameraCaptureUIPhotoFormat
>     CameraCaptureUIVideoCaptureSettings
>     CameraCaptureUIVideoFormat
> ```
</details>

<details><summary>Bug fixes</summary>

>
> - Changed `SplitButton` so touch input now matches the behavior of mouse input. For more info, see GitHub issue [#178](https://github.com/microsoft/microsoft-ui-xaml/issues/178).
> - Changed cascading menus so sub menus now open immediately if clicked. For more info, see GitHub issue [#939](https://github.com/microsoft/microsoft-ui-xaml/issues/939).
> - Fixed an issue where opening a `ComboBox` which is in a flyout closes all flyouts. For more info, see GitHub issue [#1467](https://github.com/microsoft/microsoft-ui-xaml/issues/1467).
> - Fixed an issue where `SwipeControl` would randomly crash in a `ListView`. For more info, see GitHub issue [#2527](https://github.com/microsoft/microsoft-ui-xaml/issues/2527).
> - Fixed an issue where drag-and-drop only a `ListViewItem` would leave it in the wrong visual state. For more info, see GitHub issue [#3458](https://github.com/microsoft/microsoft-ui-xaml/issues/3458).
> - Fixed an issue in `StackLayout` so that it respects the ItemsRepeater.HorizontalAlignment and ItemsRepeater.VerticalAlignment properties (when StackLayout.Orientation is Vertical and Horizontal respectively). The old layout behaved as if the ItemsRepeater alignment was Stretch. With the fix, the layout results in items aligned to the right when the Right alignment is used, for example. For more info, see GitHub issue [#3842](https://github.com/microsoft/microsoft-ui-xaml/issues/3842).
> - Fixed a potential crash when using a resource which contains an `x:Bind`. For more info, see GitHub issue [#5786](https://github.com/microsoft/microsoft-ui-xaml/issues/5786).
> - Fixed an issue where deleting items in the `ItemsRepeater`'s source would not generate items which moved up into view. For more info, see GitHub issue [#6661](https://github.com/microsoft/microsoft-ui-xaml/issues/6661).
> - Fixed an issue where the right Alt key would not show keytips for Access Keys. For more info, see GitHub issue [#8447](https://github.com/microsoft/microsoft-ui-xaml/issues/8447). **Note:** This may result in key events for the right Alt key no longer being delivered to handles in the app or controls.
> - Fixed an issue where using a ResourceDictionary containing only a single resource would fail to find that resource and likely cause a crash. For more info, see GitHub issue [#8832](https://github.com/microsoft/microsoft-ui-xaml/issues/8832).
> - Fixed a crash where `UniformGridLayout` would sometimes pick a wrong layout anchor and cause infinite layout passes when scrolling backwards. For more info, see GitHub issue [#9199](https://github.com/microsoft/microsoft-ui-xaml/issues/9199).
> - Fixed an issue where setting `NavigationFailedEventArgs.Handled` to True would still throw an exception. For more info, see GitHub issue [#9632](https://github.com/microsoft/microsoft-ui-xaml/issues/9632).
> - Fixed an issue where `TabView` would not apply any specified `CornerRadius`. For more info, see GitHub issue [#9846](https://github.com/microsoft/microsoft-ui-xaml/issues/9846).
> - Fixed a potential layout cycle crash in `StackLayout`. For more info, see GitHub issue [#9852](https://github.com/microsoft/microsoft-ui-xaml/issues/9852).
> - Fixed a potential crash in `ItemsView` when removing items. For more info, see GitHub issue [#9868](https://github.com/microsoft/microsoft-ui-xaml/issues/9868).
> - Fixed an issue in 1.7-preview1 where popups no longer correctly moved with their parent window. For more info, see GitHub issue [#10386](https://github.com/microsoft/microsoft-ui-xaml/issues/10386).
> - Based on feedback from 1.7-preview1, renamed some properties on the new `TitleBar` control.
>

</details>

:::zone-end

:::zone pivot="preview"

## Version 1.7 Preview 1 (1.7-preview1)

<details>
<summary>New Badge Notifications Feature</summary>

>
> New badge notification support allows showing a number or glyph badge on your app in the taskbar. For more info, see GitHub [#4926](https://github.com/microsoft/WindowsAppSDK/issues/4926).
>

</details>

<details>
<summary>New CameraCaptureUI API</summary>

>
> A new CameraCaptureUI API makes it easier to capture photos and videos in your Windows App SDK app. For more info, see GitHub issue [#4721](https://github.com/microsoft/WindowsAppSDK/issues/4721).
>

</details>

<details>
<summary>New Authentication API</summary>

>
> A new `OAuth2Manager` API provides a streamlined solution for web authentication, offering OAuth 2.0 capabilities with full feature parity across all Windows platforms supported by Windows App SDK. For more info, see GitHub issue [#4772](https://github.com/microsoft/WindowsAppSDK/issues/4772).
>

</details>

<details>
<summary>New Background Task support</summary>

>
> A new `BackgroundTaskBuilder` API enables registering background tasks for Windows App SDK apps. For more info, see GitHub [#4831](https://github.com/microsoft/WindowsAppSDK/issues/4831).
>

</details>

<details>
<summary>New TitleBar control</summary>

>
> A new `TitleBar` control makes it much easier to create a great, customizable titlebar for your app. Configure properties such as the titlebar icon, Title, and Subtitle, include an integrated back button, or even add a custom control like a search box! The control includes robust titlebar capabilities like empty-space draggable regions, theme responsiveness, caption buttons, and built-in accessibility support so you can focus on your personalized design and still get the same reliable titlebar as the default experience. For more info, see GitHub [#10056](https://github.com/microsoft/microsoft-ui-xaml/issues/10056).
>

</details>

<details>
<summary>Support for MathML</summary>

>
> `RichEditBox` now supports MathML, via `RichEditTextDocument.SetMathMode` and `RichEditTextDocument.SetMathML`. For more info, see GitHub [#4196](https://github.com/microsoft/microsoft-ui-xaml/issues/4196).
>

</details>

<details>
<summary>Other notable changes</summary>

>
> * New `RuntimeCompatibilityOptions` support will allow more control over how servicing changes affect apps. For more info, see GitHub [#4966](https://github.com/microsoft/WindowsAppSDK/issues/4966).
> * A new `ReleaseInfo` API provides easy access to the version of the Windows App SDK Runtime in use. For more info, see GitHub [#2893](https://github.com/microsoft/WindowsAppSDK/issues/2893).
> * Note: Windows Copilot Runtime APIs are not included this release. To experiment with these APIs, please continue to use the 1.7-experimental3 release and share your feedback!
> * Note: New APIs for windowing on `AppWindow` are not included in this release. To experiment with these APIs, please continue to use the 1.7-experimental3 release and share your feedback!
>

</details>

<details>
<summary>New APIs</summary>

>
> This release includes the following new APIs compared to the stable 1.6 release:
>
> ```
> Microsoft.Security.Authentication.OAuth
>
>     AuthFailure
>     AuthRequestParams
>     AuthRequestResult
>     AuthResponse
>     ClientAuthentication
>     CodeChallengeMethodKind
>     OAuth2Manager
>     OAuthContract
>     TokenFailure
>     TokenFailureKind
>     TokenRequestParams
>     TokenRequestResult
>     TokenResponse
> ```
> ```
> Microsoft.UI.Text
>
>     RichEditTextDocument
>         GetMathML
>         GetMathMode
>         SetMathML
>         SetMathMode
>
>     TextApiContract
> ```
> ```
> Microsoft.UI.Xaml
>
>     XamlIsland
>     XamlRoot
>         ContentIsland
> ```
> ```
> Microsoft.UI.Xaml.Controls
>
>     TitleBar
>     TitleBarAutomationPeer
>     TitleBarTemplateSettings
> ```
> ```
> Microsoft.Windows.ApplicationModel.Background
>
>     BackgroundTaskBuilder
>     BackgroundTaskContract
> ```
> ```
> Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
>
>     Task
>     UniversalBackgroundTaskContract
> ```
> ```
> Microsoft.Windows.ApplicationModel.WindowsAppRuntime
>
>     ReleaseInfo
>     RuntimeCompatibilityChange
>     RuntimeCompatibilityContract
>     RuntimeCompatibilityOptions
>     RuntimeInfo
>     VersionInfoContract
>     WindowsAppRuntimeVersion
> ```
> ```
> Microsoft.Windows.BadgeNotifications
>
>     BadgeNotificationGlyph
>     BadgeNotificationManager
>     BadgeNotificationsContract
> ```
> ```
> Microsoft.Windows.Media.Capture
>
>     CameraCaptureUI
>     CameraCaptureUIContract
>     CameraCaptureUIMaxPhotoResolution
>     CameraCaptureUIMaxVideoResolution
>     CameraCaptureUIMode
>     CameraCaptureUIPhotoCaptureSettings
>     CameraCaptureUIPhotoFormat
>     CameraCaptureUIVideoCaptureSettings
>     CameraCaptureUIVideoFormat
> ```
>

</details>

<details>
<summary>Bug fixes</summary>

>
> This release includes the following bug fixes:
>
> - Changed `SplitButton` so touch input now matches the behavior of mouse input. For more info, see GitHub issue [#178](https://github.com/microsoft/microsoft-ui-xaml/issues/178).
> - Changed cascading menus so sub menus now open immediately if clicked. For more info, see GitHub issue [#939](https://github.com/microsoft/microsoft-ui-xaml/issues/939).
> - Fixed an issue where opening a `ComboBox` which is in a flyout closes all flyouts. For more info, see GitHub issue [#1467](https://github.com/microsoft/microsoft-ui-xaml/issues/1467).
> - Fixed an issue where `SwipeControl` would randomly crash in a `ListView`. For more info, see GitHub issue [#2527](https://github.com/microsoft/microsoft-ui-xaml/issues/2527).
> - Fixed an issue where drag-and-drop only a `ListViewItem` would leave it in the wrong visual state. For more info, see GitHub issue [#3458](https://github.com/microsoft/microsoft-ui-xaml/issues/3458).
> - Fixed an issue in `StackLayout` so that it respects the ItemsRepeater.HorizontalAlignment and ItemsRepeater.VerticalAlignment properties (when StackLayout.Orientation is Vertical and Horizontal respectively). The old layout behaved as if the ItemsRepeater alignment was Stretch. With the fix, the layout results in items aligned to the right when the Right alignment is used, for example. For more info, see GitHub issue [#3842](https://github.com/microsoft/microsoft-ui-xaml/issues/3842).
> - Fixed an issue where deleting items in the `ItemsRepeater`'s source would not generate items which moved up into view. For more info, see GitHub issue [#6661](https://github.com/microsoft/microsoft-ui-xaml/issues/6661).
> - Fixed an issue where the right Alt key would not show keytips for Access Keys. For more info, see GitHub issue [#8447](https://github.com/microsoft/microsoft-ui-xaml/issues/8447). **Note:** This may result in key events for the right Alt key no longer being delivered to handles in the app or controls. 
> - Fixed a crash where `UniformGridLayout` would sometimes pick a wrong layout anchor and cause infinite layout passes when scrolling backwards. For more info, see GitHub issue [#9199](https://github.com/microsoft/microsoft-ui-xaml/issues/9199).
> - Fixed an issue where setting `NavigationFailedEventArgs.Handled` to True would still throw an exception. For more info, see GitHub issue [#9632](https://github.com/microsoft/microsoft-ui-xaml/issues/9632).
> - Fixed an issue where `TabView` would not apply any specified `CornerRadius`. For more info, see GitHub issue [#9846](https://github.com/microsoft/microsoft-ui-xaml/issues/9846).
> - Fixed a potential layout cycle crash in `StackLayout`. For more info, see GitHub issue [#9852](https://github.com/microsoft/microsoft-ui-xaml/issues/9852).
> - Fixed a potential crash in `ItemsView` when removing items. For more info, see GitHub issue [#9868](https://github.com/microsoft/microsoft-ui-xaml/issues/9868).
>

</details>

:::zone-end

:::zone pivot="experimental"

## Version 1.7 Experimental (1.7.0-experimental3)

<details>
<summary>Use on-device AI with Windows AI APIs</summary>

>
> [!IMPORTANT]
> The underlying ML models required for these APIs currently require your device to be running the latest Windows 11 Insider Preview Build on the Dev Channel. Additionally, these APIs require your device to be a Copilot+ PC. See [Copilot+ PCs Developer Guide](/windows/ai/npu-devices) to learn more about these devices. APIs will throw an exception when called on devices lacking the necessary support.
>
> Windows AI Foundry offers several AI-powered features and APIs for you to easily, efficiently, and responsibly use on-device AI models in your Windows apps. In this release we are making available several scenario focused APIs for you to leverage powerful capabilities without the need to find, run, or optimize your own Machine Learning (ML) models. 
>
> Learn more about responsible development practices used during Windows AI APIs development that you can also apply as you create AI-assisted features in the [Developing Responsible Generative AI Applications and Features on Windows](/windows/ai/rai) guidance.
>
> #### Phi Silica
>
> With [**Phi Silica**](/windows/ai/apis/phi-silica), Microsoft's most powerful NPU-tuned local language model, you can generate text responses to broad user prompts with built in content moderation. You can also specify it to perform common tasks like summarizing a piece of text, rewriting a piece of text for clarity, and converting text to a table format. Phi Silica is optimized for efficiency and performance on Windows Copilot+ PCs devices while still offering many of the capabilities found in Large Language Models (LLMs). 
>
> See [Get started with Phi Silica in the Windows App SDK](/windows/ai/apis/phi-silica) and [API ref for Phi Silica in the Windows App SDK](/windows/ai/apis/phi-silica-api-ref) for more information.
>
> #### Text Recognition (OCR)
>
> Text recognition, also known as optical character recognition (OCR), APIs in Windows AI Foundry can detect and extract text within images and convert it into machine readable character streams. These APIs can identify characters, words, lines, polygonal text boundaries, and provide confidence levels for each match. The set of AI-assisted APIs in Windows AI Foundry benefit from NPU-assisted acceleration to perform faster and more accurately than the legacy [Windows.Media.Ocr.OcrEngine](/uwp/api/windows.media.ocr.ocrengine) APIs.
>
> See [Get Started with Text Recognition (OCR) in the Windows App SDK](/windows/ai/apis/text-recognition) and [API ref for AI-backed Text Recognition (OCR) in the Windows App SDK](/windows/ai/apis/text-recognition-api-ref) for more information.
>
> #### Image Super Resolution
>
> Using the 'ImageScaler' APIs you can increase the sharpness and clarity of an image and upscale the image up to 8x its original resolution.
>
> See [What can I do with Image Super Resolution?](/windows/ai/apis/imaging#what-can-i-do-with-image-super-resolution) and [API ref for AI imaging features in the Windows App SDK](/windows/ai/apis/imaging-api-ref) to get started.
>
> #### Image Description
>
> The Image Description APIs can be used to generate a text description of an image. The APIs are configurable to specify the length and type of the text description. Image descriptions may include a short caption or a long description for users with accessibility needs.
>
> [!NOTE]
> When calling ImageDescriptionGenerator.DescribeAsync() in a Debug build, an error may occur that can be mitigated by continuing the build in Visual Studio.
>
> See [What can I do with Image Description?](/windows/ai/apis/imaging#what-can-i-do-with-image-description) and [API ref for AI imaging features in the Windows App SDK](/windows/ai/apis/imaging-api-ref) to learn more.
>
> #### Image Segmentation
>
> Using Image Segmentation APIs you can identify specific objects within an image. The model takes both an image and a "hints" object and returns a mask of the identified object.
>
> See [What can I do with Image Segmentation?](/windows/ai/apis/imaging#what-can-i-do-with-image-segmentation) and [API ref for AI imaging features in the Windows App SDK](/windows/ai/apis/imaging-api-ref) to get started.
>

</details>

<details>
<summary>New APIs for Windowing</summary>

>
> New `AppWindow` APIs make it easier to control your app windows and create a great experience. New capabilities include using `EnablePlacementPersistence` to automatically remember the size and position of your windows, using `SetTaskBarIcon` and `SetTitleBarIcon` to independently set the taskbar and titlebar icons, using `AppWindowTitleBar.PreferredTheme` to set the light/dark theme of the titlebar, and using `OverlappedPresenter.PreferredMinimum/MaximumSize` to set a minimum or maximum size for the window.
>

</details>

<details>
<summary>Other notable changes</summary>

>
> * The missing C# projections for the new `BadgeNotifications` have been added so these APIs are now usable from C#.
> * A class registration issue which prevented using the new `AppNotificationConferencingConfig` API has been fixed. Note that this enhanced user experience for video or audio calling in notifications is only available on the latest Windows Insider releases of Windows.
>

</details>

<details>
<summary>New APIs</summary>

>
> This release includes the following new and modified experimental APIs compared to 1.7-experimental2:
> ```
> Microsoft.Graphics.Imaging
>
>     ImageBuffer
>     ImageBufferContract
>     ImageObjectExtractor
>     ImageObjectExtractorContract
>     ImageObjectExtractorHint
>     ImageScaler
>     ImageScalerContract
>     PixelFormat
> ```
> ```
> Microsoft.UI.Content
>
>     ChildSiteLink
>         AutomationOption
>         ProcessKeyboardInput
>         ProcessPointerInput
>
>     ContentAutomationOptions
>     ContentIsland
>         AutomationOption
>         ProcessKeyboardInput
>         ProcessPointerInput
>
>     ContentSite
>         ProcessKeyboardInput
>         ProcessPointerInput
>
>     ContentSiteView
>         AutomationOption
>         ProcessKeyboardInput
>         ProcessPointerInput
>
>     DesktopPopupSiteBridge
>         AutomationOption
>
>     IContentSiteAutomation
>         AutomationOption
>
>     IContentSiteInput
>     ReadOnlyDesktopSiteBridge
>         ProcessKeyboardInput
>         ProcessPointerInput
> ```
> ```
> Microsoft.UI.Windowing
>
>     AppWindow
>         EnablePlacementPersistence
>         EnablePlacementPersistence
>         GetCurrentPlacement
>         SaveCurrentPlacement
>         SetPlacement
>         SetTaskBarIcon
>         SetTaskBarIcon
>         SetTitleBarIcon
>         SetTitleBarIcon
>
>     AppWindowTitleBar
>         PreferredTheme
>
>     OverlappedPresenter
>         PreferredMaximumSize
>         PreferredMinimumSize
>         SetPreferredBounds
>
>     PlacementPersistenceBehaviorFlags
>     TitleBarTheme
> ```
> ```
> Microsoft.Windows.AI.ContentModeration
>
>     ContentFilterOptions
>     ContentFilterOptionsContract
>     ImageContentFilterSeverity
>     SeverityLevel
>     TextContentFilterSeverity
> ```
> ```
> Microsoft.Windows.AI.Generative
>
>     ImageDescriptionContract
>     ImageDescriptionGenerator
>     ImageDescriptionScenario
>     LanguageModel
>     LanguageModelContext
>     LanguageModelContract
>     LanguageModelOptions
>     LanguageModelResponse
>     LanguageModelResponseStatus
>     LanguageModelSkill
> ```
> ```
> Microsoft.Windows.SemanticSearch
>
>     EmbeddingVector
>     SemanticSearchContract
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
> Microsoft.Windows.Workloads
>
>     WorkloadPriority
>     WorkloadsContract
> ```
>

</details>

---

## Version 1.7 Experimental (1.7.0-experimental2)

<details>
<summary>Background Task Registration</summary>

>
> A new `BackgroundTaskBuilder` API enables registering background tasks for Windows App SDK apps. For more info, see GitHub [#4831](https://github.com/microsoft/WindowsAppSDK/issues/4831).
>

</details>

<details>
<summary>New Notifications Features</summary>

>
> New badge notification support allows showing a number or glyph badge on your app in the taskbar. For more info, see GitHub [#4926](https://github.com/microsoft/WindowsAppSDK/issues/4926).
> [!IMPORTANT]
> In this release, the C# projections are missing for the new `BadgeNotifications` APIs, which prevents using them from C#. The APIs are available in C++.
>
> Video or audio calling can have an enhanced user experience in notifications. For more info, see GitHub [#4783](https://github.com/microsoft/WindowsAppSDK/issues/4783).
> [!IMPORTANT]
> This functionality is only available on the latest Windows Insider releases of Windows.
>

</details>

<details>
<summary>Other notable changes</summary>

>
> * `RichEditBox` now supports math mode, via `RichEditTextDocument.SetMathMode` and `RichEditTextDocument.SetMath`.
> * New `CompatibilityOptions` support will allow more control over how servicing changes affect apps. For more info, see GitHub [#4976](https://github.com/microsoft/WindowsAppSDK/issues/4976).
>

</details>

<details>
<summary>New APIs</summary>

> This release includes the following new and modified experimental APIs:
> ```
> Microsoft.Security.Authentication.OAuth
>
>     AuthFailure
>     AuthRequestParams
>     AuthRequestResult
>     AuthResponse
>     ClientAuthentication
>     CodeChallengeMethodKind
>     OAuth2Manager
>     OAuthContract
>     TokenFailure
>     TokenFailureKind
>     TokenRequestParams
>     TokenRequestResult
>     TokenResponse
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
>     AutomationTreeOptions
>     ChildSiteLink
>     ContentAppWindowBridge
>     ContentDisplayOrientations
>     ContentEnvironmentStateChangedEventArgs
>         DidDisplayScaleChange
>
>     ContentExternalBackdropLink
>     ContentExternalOutputLink
>     ContentIsland
>         AutomationTreeOption
>         Children
>         Connected
>         ConnectionInfo
>         ConnectRemoteEndpoint
>         CreateForSystemVisual
>         Disconnected
>         FindAllForSystemCompositor
>         FragmentRootAutomationProvider
>         GetBySystemVisual
>         InputCapabilities
>         IsRemoteEndpointConnected
>         LocalToClientTransformMatrix
>         LocalToParentTransformMatrix
>         NextSiblingAutomationProvider
>         ParentAutomationProvider
>         Popups
>         PreviousSiblingAutomationProvider
>         Root
>
>     ContentIslandEnvironment
>         CurrentOrientation
>         DisplayScale
>         NativeOrientation
>         ThemeChanged
>
>     ContentIslandStateChangedEventArgs
>         DidLocalToClientTransformMatrixChange
>         DidLocalToParentTransformMatrixChange
>
>     ContentSite
>         InputCapabilities
>         LocalToClientTransformMatrix
>         LocalToParentTransformMatrix
>         SetContentNodeParent
>         TryGetAutomationProvider
>
>     ContentSiteAutomationProviderRequestedEventArgs
>     ContentSiteEnvironment
>         CurrentOrientation
>         DisplayScale
>         NativeOrientation
>         NotifyThemeChanged
>
>     ContentSiteEnvironmentView
>         DisplayScale
>
>     ContentSiteView
>         AutomationTreeOption
>         InputCapabilities
>         LocalToClientTransformMatrix
>         LocalToParentTransformMatrix
>
>     CoreWindowSiteBridge
>     CoreWindowTopLevelWindowBridge
>     DesktopChildSiteBridge
>         AcceptRemoteEndpoint
>         ConnectionInfo
>         CreateWithDispatcherQueue
>         IsRemoteEndpointConnected
>         RemoteEndpointConnecting
>         RemoteEndpointDisconnected
>         RemoteEndpointRequestedStateChanged
>
>     DesktopPopupSiteBridge
>     DesktopSiteBridge
>         TryCreatePopupSiteBridge
>
>     EndpointConnectionEventArgs
>     EndpointRequestedStateChangedEventArgs
>     IContentIslandEndpointConnectionPrivate
>     IContentNodeOwner
>     IContentSiteAutomation
>     IContentSiteBridgeEndpointConnectionPrivate
>     IContentSiteInput
>     IContentSiteLink
>     IContentSiteLink2
>     InputCapabilities
>     PopupWindowSiteBridge
>     ProcessStarter
>     ReadOnlyDesktopSiteBridge
>     SystemVisualSiteBridge
> ```
> ```
> Microsoft.UI.Input
>
>     InputFocusNavigationHost
>         GetForSiteLink
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
> Microsoft.UI.Text
>
>     RichEditTextDocument
>         GetMath
>         SetMath
>         SetMathMode
> ```
> ```
> Microsoft.UI.Windowing
>
>     AppWindow
>         DefaultTitleBarShouldMatchAppModeTheme
>
>     DisplayArea
>         GetMetricsFromWindowId
> ```
> ```
> Microsoft.UI.Xaml
>
>     XamlIsland
>     XamlRoot
>         TryGetContentIsland
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
>     TitleBar
>     TitleBarAutomationPeer
>     TitleBarTemplateSettings
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
> Microsoft.Windows.ApplicationModel.Background
>
>     BackgroundTaskBuilder
>     BackgroundTaskContract
> ```
> ```
> Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
>
>     Task
> ```
> ```
> Microsoft.Windows.ApplicationModel.WindowsAppRuntime
>
>     CompatibilityChange
>     CompatibilityContract
>     CompatibilityOptions
>     DeploymentManager
>         Repair
>
>     DeploymentStatus
>         PackageRepairFailed
>
>     ReleaseInfo
>     RuntimeInfo
>     VersionInfoContract
>     WindowsAppRuntimeVersion
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
> Microsoft.Windows.BadgeNotifications
>
>     BadgeNotificationGlyph
>     BadgeNotificationManager
>     BadgeNotificationsContract
> ```
> ```
> Microsoft.Windows.Media.Capture
>
>     CameraCaptureUI
>     CameraCaptureUIContract
>     CameraCaptureUIMaxPhotoResolution
>     CameraCaptureUIMaxVideoResolution
>     CameraCaptureUIMode
>     CameraCaptureUIPhotoCaptureSettings
>     CameraCaptureUIPhotoFormat
>     CameraCaptureUIVideoCaptureSettings
>     CameraCaptureUIVideoFormat
> ```
> ```
> Microsoft.Windows.Storage
>
>     ApplicationData
>         GetForUnpackaged
> ```
>

</details>

---

## Version 1.7 Experimental (1.7.0-experimental1)

This is the latest release of the experimental channel.

To download, retarget your Windows App SDK NuGet version to `1.7.241114004-experimental1`. <br><br>

<details>
<summary>New CameraCaptureUI API</summary>

>
> A new CameraCaptureUI API makes it easier to capture photos and videos in your Windows App SDK app. For more info, see GitHub issue [#4721](https://github.com/microsoft/WindowsAppSDK/issues/4721).
>

</details>

<details>
<summary>New Authentication API</summary>

>
> A new `OAuth2Manager` API provides a streamlined solution for web authentication, offering OAuth 2.0 capabilities with full feature parity across all Windows platforms supported by Windows App SDK. For more info, see GitHub issue [#4772](https://github.com/microsoft/WindowsAppSDK/issues/4772).
>

</details>

<details>
<summary>New Background Task support</summary>

>
> A new `BackgroundTaskBuilder` API brings integrated support for background task registration to your Windows App SDK apps. For more info, see GitHub issue [#4822](https://github.com/microsoft/WindowsAppSDK/issues/4822).
>

</details>

<details>
<summary>New APIs for 1.7-experimental1</summary>

>
> This release includes the following new and modified experimental APIs:
>
> ```C#
> Microsoft.Security.Authentication.OAuth
>
>     AuthFailure
>     AuthRequestParams
>     AuthRequestResult
>     AuthResponse
>     ClientAuthentication
>     CodeChallengeMethodKind
>     OAuth2Manager
>     OAuthContract
>     TokenFailure
>     TokenFailureKind
>     TokenRequestParams
>     TokenRequestResult
>     TokenResponse
> ```
>
> ```C#
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
>
> ```C#
> Microsoft.UI.Composition.Experimental
>
>     ExpCompositionVisualSurface
>     ExpExpressionNotificationProperty
>     IExpCompositionPropertyChanged
>     IExpCompositionPropertyChangedListener
>     IExpCompositor
>     IExpVisual
> ```
>
> ```C#
> Microsoft.UI.Content
>
>     AutomationOptions
>     ChildContentLink
>     ContentAppWindowBridge
>     ContentDisplayOrientations
>     ContentEnvironmentStateChangedEventArgs
>         DidDisplayScaleChange
>
>     ContentExternalBackdropLink
>     ContentExternalOutputLink
>     ContentIsland
>         Children
>         Compositor
>         Connected
>         ConnectionInfo
>         ConnectRemoteEndpoint
>         Create
>         Disconnected
>         FindAllForCompositor
>         FragmentRootAutomationProvider
>         GetByVisual
>         IsRemoteEndpointConnected
>         NextSiblingAutomationProvider
>         ParentAutomationProvider
>         PreviousSiblingAutomationProvider
>         Root
>         TransformMatrix
>
>     ContentIslandEnvironment
>         AutomationOption
>         CurrentOrientation
>         DisplayScale
>         NativeOrientation
>         ThemeChanged
>
>     ContentSite
>         Compositor
>         SetContentNodeParent
>         SetIsInputPassThrough
>         SiteVisual
>         TransformMatrix
>         TryGetAutomationProvider
>
>     ContentSiteAutomationProviderRequestedEventArgs
>     ContentSiteEnvironment
>         CurrentOrientation
>         DisplayScale
>         NativeOrientation
>         NotifyThemeChanged
>
>     ContentSiteView
>         TransformMatrix
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
>     IContentLink
>     IContentNodeOwner
>     IContentSiteBridge2
>     IContentSiteBridgeAutomation
>     IContentSiteBridgeEndpointConnectionPrivate
>     PopupWindowSiteBridge
>     ProcessStarter
>     ReadOnlyDesktopSiteBridge
>     SystemVisualSiteBridge
> ```
>
> ```C#
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
>
> ```C#
> Microsoft.UI.Input.Experimental
>
>     ExpInputSite
>     ExpPointerPoint
> ```
>
> ```C#
> Microsoft.UI.Windowing
>
>     AppWindow
>         DefaultTitleBarShouldMatchAppModeTheme
>
>     DisplayArea
>         GetMetricsFromWindowId
> ```
>
> ```C#
> Microsoft.UI.Xaml
>
>     XamlIsland
>     XamlRoot
>         TryGetContentIsland
> ```
>
> ```C#
> Microsoft.UI.Xaml.Automation.Peers
>
>     AutomationEvents
>         Notification
>
>     InkCanvasAutomationPeer
>     PagerControlAutomationPeer
> ```
>
> ```C#
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
>     TitleBar
>     TitleBarAutomationPeer
>     TitleBarTemplateSettings
>     UniformGridLayoutState
> ```
>
> ```C#
> Microsoft.UI.Xaml.Controls.Primitives
>
>     ScrollPresenter
>         ScrollStarting
>         ZoomStarting
> ```
>
> ```C#
> Microsoft.Windows.ApplicationModel.Background
>
>     BackgroundTaskBuilder
>     BackgroundTaskContract
> ```
>
> ```C#
> Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
>
>     Task
> ```
>
> ```C#
> Microsoft.Windows.ApplicationModel.WindowsAppRuntime
>
>     DeploymentManager
>         Repair
>
>     DeploymentStatus
>         PackageRepairFailed
>
>     ReleaseInfo
>     RuntimeInfo
>     VersionInfoContract
> ```
>
> ```C#
> Microsoft.Windows.Media.Capture
>
>     CameraCaptureUI
>     CameraCaptureUIContract
>     CameraCaptureUIMaxPhotoResolution
>     CameraCaptureUIMaxVideoResolution
>     CameraCaptureUIMode
>     CameraCaptureUIPhotoCaptureSettings
>     CameraCaptureUIPhotoFormat
>     CameraCaptureUIVideoCaptureSettings
>     CameraCaptureUIVideoFormat
> ```
>
> ```C#
> Microsoft.Windows.Storage
>
>     ApplicationData
>         GetForUnpackaged
> ```
>

</details>

<details>
<summary>Bug fixes</summary>

>
> This release includes the following bug fixes:
>
> - Changed `SplitButton` so touch input now matches the behavior of mouse input. For more info, see GitHub issue [#178](https://github.com/microsoft/microsoft-ui-xaml/issues/178).
> - Changed cascading menus so sub menus now open immediately if clicked. For more info, see GitHub issue [#939](https://github.com/microsoft/microsoft-ui-xaml/issues/939).
> - Fixed an issue where opening a `ComboBox` which is in a flyout closes all flyouts. For more info, see GitHub issue [#1467](https://github.com/microsoft/microsoft-ui-xaml/issues/1467).
> - Fixed an issue where `SwipeControl` would randomly crash in a `ListView`. For more info, see GitHub issue [#2527](https://github.com/microsoft/microsoft-ui-xaml/issues/2527).
> - Fixed an issue where drag-and-drop only a `ListViewItem` would leave it in the wrong visual state. For more info, see GitHub issue [#3458](https://github.com/microsoft/microsoft-ui-xaml/issues/3458).
> - Fixed an issue in `StackLayout` so that it respects the ItemsRepeater.HorizontalAlignment and ItemsRepeater.VerticalAlignment properties (when StackLayout.Orientation is Vertical and Horizontal respectively). The old layout behaved as if the ItemsRepeater alignment was Stretch. With the fix, the layout results in items aligned to the right when the Right alignment is used, for example. For more info, see GitHub issue [#3842](https://github.com/microsoft/microsoft-ui-xaml/issues/3842).
> - Fixed an issue where deleting items in the `ItemsRepeater`'s source would not generate items which moved up into view. For more info, see GitHub issue [#6661](https://github.com/microsoft/microsoft-ui-xaml/issues/6661).
> - Fixed an issue where the right Alt key would not show keytips for Access Keys. For more info, see GitHub issue [#8447](https://github.com/microsoft/microsoft-ui-xaml/issues/8447). **Note:** This may result in key events for the right Alt key no longer being delivered to handles in the app or controls. 
> - Fixed a crash where `UniformGridLayout` would sometimes pick a wrong layout anchor and cause infinite layout passes when scrolling backwards. For more info, see GitHub issue [#9199](https://github.com/microsoft/microsoft-ui-xaml/issues/9199).
> - Fixed an issue where setting `NavigationFailedEventArgs.Handled` to True would still throw an exception. For more info, see GitHub issue [#9632](https://github.com/microsoft/microsoft-ui-xaml/issues/9632).
> - Fixed an issue where `TabView` would not apply any specified `CornerRadius`. For more info, see GitHub issue [#9846](https://github.com/microsoft/microsoft-ui-xaml/issues/9846).
> - Fixed a potential layout cycle crash in `StackLayout`. For more info, see GitHub issue [#9852](https://github.com/microsoft/microsoft-ui-xaml/issues/9852).
> - Fixed a potential crash in `ItemsView` when removing items. For more info, see GitHub issue [#9868](https://github.com/microsoft/microsoft-ui-xaml/issues/9868).
>

</details>

:::zone-end