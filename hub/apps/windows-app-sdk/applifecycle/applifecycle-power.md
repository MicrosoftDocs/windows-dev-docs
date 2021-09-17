---
description: Use PowerManager to respond to changes in the device's power state
title:  Power management in AppLifecycle (Windows App SDK)
ms.topic: article
ms.date: 09/13/2021
keywords: AppLifecycle, Windows, ApplicationModel, power, battery,
ms.author: hickeys
author: hickeys
ms.localizationpriority: medium
---

# Power management in AppLifecycle

The **AppLifecycle** power management API provides visibility into how an app affects the device's power state; it allows the app to make intelligent decisions about resource usage. An example of its functionality is postponing resource-intensive background tasks while the device is running on battery power. The **AppLifecycle** power management APIs use a callback-based model similar to the existing [PowerSettingRegisterNotification](/windows/win32/api/powersetting/nf-powersetting-powersettingregisternotification). Using a callback model extends the reach of the API to all apps, including background apps, headless apps, and others.

## Prerequisites

> [!IMPORTANT]
> AppLifecycle APIs are currently supported in the [preview release channel](../preview-channel.md) and [experimental release channel](../experimental-channel.md) of the Windows App SDK. This feature is not currently supported for use by apps in production environments.

To use the AppLifecycle APIs in the Windows App SDK:

1. Download and install the latest preview or experimental release of the Windows App SDK. For more information, see [Install developer tools](../set-up-your-development-environment.md#4-install-the-windows-app-sdk-extension-for-visual-studio).
2. Follow the instructions to [create a new project that uses the Windows App SDK](../../winui/winui3/create-your-first-winui3-app.md) or to [use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md).

## Subscribe and respond to events

The following example demonstrates how to subscribe and respond to [PowerManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager) events. This code subscribes to the [BatteryStatusChanged](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager.batterystatuschanged) event during startup. The app then responds to changes by checking the current power level and adjusting its resource usage appropriately. For example, if the battery discharges at a low power state, the app might defer any non-critical background work.

> [!NOTE]
> Apps can register and unregister for these events at any time, but most apps will want to set callbacks in WinMain that persist as long as the app continues to run.

```cpp
int APIENTRY wWinMain(
    _In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
    // Initialize COM.
    winrt::init_apartment();

    // Optionally, register callbacks for power/battery state changes.
    PowerManager::BatteryStatusChanged([](auto&&...)
        { OnBatteryStatusChanged(); });

    ///////////////////////////////////////////////////////////////////////////
    // Standard Win32 window configuration/creation and message pump:
    // i.e., the app performs normal function
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_CLASSIC, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);
    if (!InitInstance(hInstance, nCmdShow))
    {
        return FALSE;
    }
    MSG msg;
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }
    return (int)msg.wParam;
}

void OnBatteryStatusChanged()
{
    BatteryStatus batteryStatus = PowerManager::BatteryStatus();
    int remainingCharge = PowerManager::RemainingChargePercent();

    if (batteryStatus == BatteryStatus::Discharging && remainingCharge < 25)
    {
        // In non-ideal battery state; non-critical work paused.
        PauseNonCriticalWork();
    }
    else if (batteryStatus == BatteryStatus::Charging && remainingCharge > 75)
    {
        // Battery in good shape; intensive work can be performed.
        StartPowerIntensiveWork();
    }
}
```

## Configure app logic based on multiple status values

[PowerManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager) events are relatively low-level, and in some scenarios, a single event handler being called might not provide enough information for the app to decide how to behave. In this example, the [PowerSupplyStatusChanged](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager.powersupplystatuschanged) event could be called when the device is disconnected from power. In that case, the app must check the current battery status before deciding how to proceed.

```cpp
void OnPowerSupplyStatusChanged()
{
    PowerSupplyStatus powerStatus = PowerManager::PowerSupplyStatus();
    BatteryStatus batteryStatus = PowerManager::BatteryStatus();
    int remainingCharge = PowerManager::RemainingChargePercent();

    // Note if the BatteryStatus is BatteryStatus::NotPresent,
    // then RemainingChargePercent is 100.

    if (batteryStatus == BatteryStatus::Discharging
        && remainingCharge < 25
        && powerStatus != PowerSupplyStatus::Adequate)
    {
        // Power/battery not in ideal state; non-critical work paused.
        PauseNonCriticalWork();
    }
    else if (powerStatus == PowerSupplyStatus::Adequate ||
        (batteryStatus == BatteryStatus::Charging && remainingCharge > 75))
    {
        // Power/battery is in great shape; intensive work can be performed.
        StartPowerIntensiveWork();
    }
}
```

## Check screen status

The [PowerManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager) class offers information about other device states relevant to an app's power usage. For example, apps can disable graphics processing when the device's display is turned off.

```cpp
void OnDisplayStatusChanged([](auto&&...)
{
    DisplayStatus displayStatus = PowerManager::DisplayStatus();
    if (displayStatus == DisplayStatus::Off)
    {
        // The screen is off, foreground graphics rendering paused,
        // and background work can now begin.
        StopRenderingGraphics();
        StartDoingBackgroundWork();
    }
});
```

## Unsubscribe from events

Apps can register and deregister for notifications during their lifecycle. Use your language's preferred event registration management system if your app doesn't need to receive power status notifications during its entire lifecycle.

```cpp
winrt::event_token s_batteryToken;
winrt::event_token s_powerToken;

void MyRegisterPowerManagerCallbacks()
{
    s_batteryToken = PowerManager::BatteryStatusChanged([](IInspectable sender, auto args)
        { OnBatteryStatusChanged(sender, args); });
    s_powerToken = PowerManager::PowerSupplyStatusChanged([](IInspectable sender, auto args)
        { OnPowerSupplyStatusChanged(sender, args); });
}

void MyUnregisterPowerManagerCallbacks()
{
    PowerManager::BatteryStatusChanged(s_batteryToken);
    PowerManager::PowerSupplyStatusChanged(s_powerToken);
}
```

## Related topics

* [Windows.System.Power.PowerManager](/uwp/api/Windows.System.Power.PowerManager)
* [PowerSettingRegisterNotification](/windows/win32/api/powersetting/nf-powersetting-powersettingregisternotification)
* [PowerSettingUnregisterNotification](/windows/win32/api/powersetting/nf-powersetting-powersettingunregisternotification)