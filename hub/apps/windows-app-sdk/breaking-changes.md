---
title: Breaking changes in the Windows App SDK
description: If you're migrating an app to Windows App SDK 1.0 from 0.8 Stable, the breaking changes listed here might affect you. Changes are grouped by technology area, such as input and MRT Core.
ms.topic: article
ms.date: 11/17/2021
keywords: windows, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Breaking changes in Windows App SDK 1.0

If you're migrating an app to Windows App SDK 1.0 from 0.8 Stable, the breaking changes listed here might affect you. Changes are grouped by technology area, such as input and MRT Core.

## Input

### API changes from 0.8 to 1.0

- No access to experimental types | API change |
- Stable [**Microsoft.UI.Input.PointerPoint**](/windows/winui/api/microsoft.ui.input.pointerpoint) added. It replaces **Microsoft.UI.Input.Experimental.ExpPointerPoint**.
  - [**PointerPoint**](/windows/winui/api/microsoft.ui.input.pointerpoint) now references [**Microsoft.UI.Input**](/windows/winui/api/microsoft.ui.input) enums and structs instead of **Windows.UI.\*** enums and structs (for example, [**IPointerPointTransform**](/windows/winui/api/microsoft.ui.input.ipointerpointtransform), [**PointerUpdateKind**](/windows/winui/api/microsoft.ui.input.pointerupdatekind), [**PointerDeviceType**](/windows/winui/api/microsoft.ui.input.pointerdevicetype)).
  - [**PointerPoint**](/windows/winui/api/microsoft.ui.input.pointerpoint) is now fully agile.
  - [**PointerPoint**](/windows/winui/api/microsoft.ui.input.pointerpoint) static functions **GetCurrentPoint**, **GetCurrentPointTransformed**, **GetIntermediatePoints**, and **GetIntermediatePointsTransformed** removed. They are replaced by member functions on [**PointerPoint**](/windows/winui/api/microsoft.ui.input.pointerpoint) and [**PointerEventArgs**](/windows/winui/api/microsoft.ui.input.pointereventargs).
  - New [**GetTransformedPoint**](/windows/winui/api/microsoft.ui.input.pointerpoint.gettransformedpoint) method.
  - Removed **ContactRectRaw** from [**PointerPointProperties**](/windows/winui/api/microsoft.ui.input.pointerpointproperties).
  - Removed **RawPosition** properties.
- Stable [**Microsoft.UI.Input.PointerEventArgs**](/windows/winui/api/microsoft.ui.input.pointereventargs) added. It replaces **Microsoft.UI.Input.Internal.ExpPointerEventArgs)**.
  - New [**GetIntermediateTransformedPoints**](/windows/winui/api/microsoft.ui.input.pointereventargs.getintermediatetransformedpoints) method.
- Stable [**Microsoft.UI.Input.GestureRecognizer**](/windows/winui/api/microsoft.ui.input.gesturerecognizer) added. It replaces **Microsoft.UI.Input.Experimental.ExpGestureRecognizer**. References [**Microsoft.UI.Input**](/windows/winui/api/microsoft.ui.input) enums, structs, and classes instead of **Windows.UI.Input.\*** and **Windows.UI.Core.\*** types (for example, [**GestureSettings**](/windows/winui/api/microsoft.ui.input.gesturesettings), [**ManipulationDelta**](/windows/winui/api/microsoft.ui.input.manipulationdelta), [**TappedEventArgs**](/windows/winui/api/microsoft.ui.input.tappedeventargs)).
- Public stable [**InputCursor**](/windows/winui/api/microsoft.ui.input.inputcursor), [**InputSystemCursor**](/windows/winui/api/microsoft.ui.input.inputsystemcursor), and [**InputDesktopResourceCursor**](/windows/winui/api/microsoft.ui.input.inputdesktopresourcecursor) added. This replaces previous references to **Windows.UI.Core.CoreCursor** in the 0.8 API surface.
- Public stable [**InputPointerSource**](/windows/winui/api/microsoft.ui.input.inputpointersource) added – this is returned from the XAML [**SwapChainPanel**](/windows/winui/api/microsoft.ui.xaml.controls.swapchainpanel) class instead of the previous experimental **Microsoft.UI.Input.Experimental.ExpIndependentPointerInputObserver** class.
  - References [**Microsoft.UI.Input.InputCursor**](/windows/winui/api/microsoft.ui.input.inputcursor) instead of **Windows.UI.Core.CoreCursor**.
  - References [**Microsoft.UI.Input**](/windows/winui/api/microsoft.ui.input) structs, enums, and types instead of **Windows.UI.\*** types, such as  [**PointerEventArgs**](/windows/winui/api/microsoft.ui.input.pointereventargs) and [**InputPointerSourceDeviceKinds**](/windows/winui/api/microsoft.ui.input.inputpointersourcedevicekinds)).
  - **Type** is no longer explicitly closeable.
- **Microsoft.UI.Input.KeyboardInput** renamed to [**Microsoft.UI.Input.InputKeyboardSource**](/windows/winui/api/microsoft.ui.input.inputkeyboardsource).

### Behavior changes from 0.8 to 1.0

- Underlying input system infrastructure upgraded to use an independent message queue for processing.
  - Supports low-latency off-UI-thread input such as inking.
  - Fully supports lifted interaction tracker APIs such as [**Microsoft.UI.Composition.VisualInteractionSource**](/windows/winui/api/microsoft.ui.composition.interactions.visualinteractionsource) (and others).
  - Fully supports hover input for off-thread input delivery (this was a limitation in 0.8).
  - System input messages, such as [**WM_POINTERDOWN**](/windows/win32/inputmsg/wm-pointerdown), are no longer visible through Win32 APIs on the UI thread as they are routed to an independent message queue inside the infrastructure.
- [**PointerPoint**](/windows/winui/api/microsoft.ui.input.pointerpoint) is now agile and can be accessed on any thread.
- [**PointerPoint**](/windows/winui/api/microsoft.ui.input.pointerpoint) objects can no longer be constructed statically from a pointer ID.
- XAML-based drag and drop operations fully support mouse, touch, and pen input (0.8 used pen-to-mouse downleveling).
- Direct use of **Windows.ApplicationModel.DataTransfer.DragDrop.Core.CoreDragOperation** will no longer work on the UI thread. XAML drag and drop must be used instead.

## MRT Core  

MRT Core APIs have moved from the [Microsoft.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.applicationmodel.resources) namespace to the [Microsoft.Windows.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace.
