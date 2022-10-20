---
title: Handle device removed scenarios in Direct3D 11
description: This topic explains how to recreate the Direct3D and DXGI device interface chain when the graphics adapter is removed or reinitialized.
ms.assetid: 8f905acd-08f3-ff6f-85a5-aaa99acb389a
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx 11, device lost
ms.localizationpriority: medium
---
# <span id="dev_gaming.handling_device-lost_scenarios"></span>Handle device removed scenarios in Direct3D 11



This topic explains how to recreate the Direct3D and DXGI device interface chain when the graphics adapter is removed or reinitialized.

In DirectX 9, applications could encounter a "[device lost](/windows/desktop/direct3d9/lost-devices)" condition where the D3D device enters a non-operational state. For example, when a full-screen Direct3D 9 application loses focus, the Direct3D device becomes "lost;" any attempts to draw with a lost device will silently fail. Direct3D 11 uses virtual graphics device interfaces, enabling multiple programs to share the same physical graphics device and eliminating conditions where apps lose control of the Direct3D device. However, it is still possible for graphics adapter availability to change. For example:

-   The graphics driver is upgraded.
-   The system changes from a power-saving graphics adapter to a performance graphics adapter.
-   The graphics device stops responding and is reset.
-   A graphics adapter is physically attached or removed.

When such circumstances arise, DXGI returns an error code indicating that the Direct3D device must be reinitialized and device resources must be recreated. This walkthrough explains how Direct3D 11 apps and games can detect and respond to any circumstance where the graphics adapter is reset, removed, or changed. Code examples are provided from the DirectX 11 App (Universal Windows) template provided with Microsoft Visual Studio 2015.

## Instructions

### <span></span>Step 1:

Include a check for the device removed error in the rendering loop. Present the frame by calling [**IDXGISwapChain::Present**](/windows/desktop/api/dxgi/nf-dxgi-idxgiswapchain-present) (or [**Present1**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1), and so on). Then, check whether it returned [**DXGI\_ERROR\_DEVICE\_REMOVED**](/windows/desktop/direct3ddxgi/dxgi-error) or **DXGI\_ERROR\_DEVICE\_RESET**.

First, the template stores the HRESULT returned by the DXGI swap chain:

```cpp
HRESULT hr = m_swapChain->Present(1, 0);
```

After taking care of all other work for presenting the frame, the template checks for the device removed error. If necessary, it calls a method to handle the device removed condition:

```cpp
// If the device was removed either by a disconnection or a driver upgrade, we
// must recreate all device resources.
if (hr == DXGI_ERROR_DEVICE_REMOVED || hr == DXGI_ERROR_DEVICE_RESET)
{
    HandleDeviceLost();
}
else
{
    DX::ThrowIfFailed(hr);
}
```

### Step 2:

Also, include a check for the device removed error when responding to window size changes. This is a good place to check for [**DXGI\_ERROR\_DEVICE\_REMOVED**](/windows/desktop/direct3ddxgi/dxgi-error) or **DXGI\_ERROR\_DEVICE\_RESET** for several reasons:

-   Resizing the swap chain requires a call to the underlying DXGI adapter, which can return the device removed error.
-   The app might have moved to a monitor that's attached to a different graphics device.
-   When a graphics device is removed or reset, the desktop resolution often changes, resulting in a window size change.

The template checks the HRESULT returned by [**ResizeBuffers**](/windows/desktop/api/dxgi/nf-dxgi-idxgiswapchain-resizebuffers):

```cpp
// If the swap chain already exists, resize it.
HRESULT hr = m_swapChain->ResizeBuffers(
    2, // Double-buffered swap chain.
    static_cast<UINT>(m_d3dRenderTargetSize.Width),
    static_cast<UINT>(m_d3dRenderTargetSize.Height),
    DXGI_FORMAT_B8G8R8A8_UNORM,
    0
    );

if (hr == DXGI_ERROR_DEVICE_REMOVED || hr == DXGI_ERROR_DEVICE_RESET)
{
    // If the device was removed for any reason, a new device and swap chain will need to be created.
    HandleDeviceLost();

    // Everything is set up now. Do not continue execution of this method. HandleDeviceLost will reenter this method 
    // and correctly set up the new device.
    return;
}
else
{
    DX::ThrowIfFailed(hr);
}
```

### Step 3:

Any time your app receives the [**DXGI\_ERROR\_DEVICE\_REMOVED**](/windows/desktop/direct3ddxgi/dxgi-error) error, it must reinitialize the Direct3D device and recreate any device-dependent resources. Release any references to graphics device resources created with the previous Direct3D device; those resources are now invalid, and all references to the swap chain must be released before a new one can be created.

The HandleDeviceLost method releases the swap chain and notifies app components to release device resources:

```cpp
m_swapChain = nullptr;

if (m_deviceNotify != nullptr)
{
    // Notify the renderers that device resources need to be released.
    // This ensures all references to the existing swap chain are released so that a new one can be created.
    m_deviceNotify->OnDeviceLost();
}
```

Then, it creates a new swap chain and reinitializes the device-dependent resources controlled by the device management class:

```cpp
// Create the new device and swap chain.
CreateDeviceResources();
m_d2dContext->SetDpi(m_dpi, m_dpi);
CreateWindowSizeDependentResources();
```

After the device and swap chain have been re-established, it notifies app components to reinitialize device-dependent resources:

```cpp
// Create the new device and swap chain.
CreateDeviceResources();
m_d2dContext->SetDpi(m_dpi, m_dpi);
CreateWindowSizeDependentResources();

if (m_deviceNotify != nullptr)
{
    // Notify the renderers that resources can now be created again.
    m_deviceNotify->OnDeviceRestored();
}
```

When the HandleDeviceLost method exits, control returns to the rendering loop, which continues on to draw the next frame.

## Remarks


### Investigating the cause of device removed errors

Repeat issues with DXGI device removed errors can indicate that your graphics code is creating invalid conditions during a drawing routine. It can also indicate a hardware failure or a bug in the graphics driver. To investigate the cause of device removed errors, call [**ID3D11Device::GetDeviceRemovedReason**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-getdeviceremovedreason) before releasing the Direct3D device. This method returns one of six possible DXGI error codes indicating the reason for the device removed error:

-   **DXGI\_ERROR\_DEVICE\_HUNG**: The graphics driver stopped responding because of an invalid combination of graphics commands sent by the app. If you get this error repeatedly, it is a likely indication that your app caused the device to hang and needs to be debugged.
-   **DXGI\_ERROR\_DEVICE\_REMOVED**: The graphics device has been physically removed, turned off, or a driver upgrade has occurred. This happens occasionally and is normal; your app or game should recreate device resources as described in this topic.
-   **DXGI\_ERROR\_DEVICE\_RESET**: The graphics device failed because of a badly formed command. If you get this error repeatedly, it may mean that your code is sending invalid drawing commands.
-   **DXGI\_ERROR\_DRIVER\_INTERNAL\_ERROR**: The graphics driver encountered an error and reset the device.
-   **DXGI\_ERROR\_INVALID\_CALL**: The application provided invalid parameter data. If you get this error even once, it means that your code caused the device removed condition and must be debugged.
-   **S\_OK**: Returned when a graphics device was enabled, disabled, or reset without invalidating the current graphics device. For example, this error code can be returned if an app is using [Windows Advanced Rasterization Platform (WARP)](/windows/desktop/direct3darticles/directx-warp) and a hardware adapter becomes available.

The following code will retrieve the [**DXGI\_ERROR\_DEVICE\_REMOVED**](/windows/desktop/direct3ddxgi/dxgi-error) error code and print it to the debug console. Insert this code at the beginning of the HandleDeviceLost method:

```cpp
    HRESULT reason = m_d3dDevice->GetDeviceRemovedReason();

#if defined(_DEBUG)
    wchar_t outString[100];
    size_t size = 100;
    swprintf_s(outString, size, L"Device removed! DXGI_ERROR code: 0x%X\n", reason);
    OutputDebugStringW(outString);
#endif
```

For more details, see [**GetDeviceRemovedReason**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-getdeviceremovedreason) and [**DXGI\_ERROR**](/windows/desktop/direct3ddxgi/dxgi-error).

### Testing Device Removed Handling

Visual Studio's Developer Command Prompt supports a command line tool 'dxcap' for Direct3D event capture and playback related to the Visual Studio Graphics Diagnostics. You can use the command line option "-forcetdr" while your app is running which will force a GPU Timeout Detection and Recovery event, thereby triggering DXGI\_ERROR\_DEVICE\_REMOVED and allowing you to test your error handling code.

> **Note** DXCap and its support DLLs are installed into system32/syswow64 as part of the Graphics Tools for Windows 10 which are no longer distributed via the Windows SDK. Instead they are provided via the Graphics Tools Feature on Demand that is an optional OS component and must be installed in order to enable and use the Graphics Tools on Windows 10. More information on how to Install the Graphics Tools for Windows 10 can be found here: <https://msdn.microsoft.com/library/mt125501.aspx#InstallGraphicsTools>