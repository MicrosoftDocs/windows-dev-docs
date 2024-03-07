---
description: Describes how to use power management and notification features with the app lifecycle API (Windows App SDK).
title: Power management with the app lifecycle API (Windows App SDK)
ms.topic: article
ms.date: 03/07/2024
keywords: AppLifecycle, Windows, ApplicationModel, power, battery,
ms.localizationpriority: medium
---

# Power management with the app lifecycle API

The app lifecycle API in the Windows App SDK provides a set of power management APIs in the [Microsoft.Windows.System.Power](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power) namespace. These APIs provide visibility into how an app affects the device's power state, and they enable the app to make intelligent decisions about resource usage. For example, an app might use this API to postpone resource-intensive background tasks while the device is running on battery power.

The power management APIs use a callback-based model similar to the existing [PowerSettingRegisterNotification](/windows/win32/api/powersetting/nf-powersetting-powersettingregisternotification) function. Using a callback model extends the reach of the API to all apps, including background apps, headless apps, and others.

## Prerequisites

To use the app lifecycle API in the Windows App SDK:

1. Download and install the latest release of the Windows App SDK. For more information, see [Install tools for the Windows App SDK](../set-up-your-development-environment.md).
2. Follow the instructions to [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md) or to [use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md).

## Subscribe and respond to events

The following example demonstrates how to subscribe and respond to [PowerManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager) events. This code subscribes to the [BatteryStatusChanged](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager.batterystatuschanged) event during startup. The app then responds to changes by checking the current power level and adjusting its resource usage appropriately. For example, if the battery discharges at a low power state, the app might defer any non-critical background work.

### [C++ sample](#tab/cpp)

> [!NOTE]
> Apps can register and unregister for these events at any time, but most apps will want to set callbacks in `WinMain` that persist as long as the app continues to run.

```cpp
BOOL bWorkInProgress;
winrt::event_token batteryToken;
winrt::event_token powerToken;
winrt::event_token powerSourceToken;
winrt::event_token chargeToken;
winrt::event_token dischargeToken;

void RegisterPowerManagerCallbacks()
{
    batteryToken = PowerManager::BatteryStatusChanged([&](
        const auto&, winrt::Windows::Foundation::IInspectable obj) { OnBatteryStatusChanged(); });
    powerToken = PowerManager::PowerSupplyStatusChanged([&](
        const auto&, winrt::Windows::Foundation::IInspectable obj) { OnPowerSupplyStatusChanged(); });
    powerSourceToken = PowerManager::PowerSourceKindChanged([&](
        const auto&, winrt::Windows::Foundation::IInspectable obj) { OnPowerSourceKindChanged(); });
    chargeToken = PowerManager::RemainingChargePercentChanged([&](
        const auto&, winrt::Windows::Foundation::IInspectable obj) { OnRemainingChargePercentChanged(); });
    dischargeToken = PowerManager::RemainingDischargeTimeChanged([&](
        const auto&, winrt::Windows::Foundation::IInspectable obj) { OnRemainingDischargeTimeChanged(); });

    if (batteryToken && powerToken && powerSourceToken && chargeToken && dischargeToken)
    {
        OutputMessage(L"Successfully registered for state notifications");
    }
    else
    {
        OutputMessage(L"Failed to register for state notifications");
    }
}

void OnBatteryStatusChanged()
{
    const size_t statusSize = 16;
    WCHAR szStatus[statusSize];
    wmemset(&(szStatus[0]), 0, statusSize);

    BatteryStatus batteryStatus = PowerManager::BatteryStatus();
    int remainingCharge = PowerManager::RemainingChargePercent();
    switch (batteryStatus)
    {
    case BatteryStatus::Charging:
        wcscpy_s(szStatus, L"Charging");
        break;
    case BatteryStatus::Discharging:
        wcscpy_s(szStatus, L"Discharging");
        break;
    case BatteryStatus::Idle:
        wcscpy_s(szStatus, L"Idle");
        break;
    case BatteryStatus::NotPresent:
        wcscpy_s(szStatus, L"NotPresent");
        break;
    }

    OutputFormattedMessage(
        L"Battery status changed: %s, %d%% remaining", 
        szStatus, remainingCharge);
    DetermineWorkloads();
}

void OnPowerSupplyStatusChanged()
{
//...etc
}
```

### [C# sample](#tab/csharp)

> [!NOTE]
> Apps can register and unregister for these events at any time, but most apps will want to set callbacks that persist as long as the app continues to run.

```csharp
private bool bWorkInProgress;
private EventRegistrationToken batteryToken;
private EventRegistrationToken powerToken;
private EventRegistrationToken powerSourceToken;
private EventRegistrationToken chargeToken;
private EventRegistrationToken dischargeToken;

private void RegisterPowerManagerCallbacks()
{
    batteryToken = PowerManager.BatteryStatusChanged += (s, e) => OnBatteryStatusChanged();
    powerToken = PowerManager.PowerSupplyStatusChanged += (s, e) => OnPowerSupplyStatusChanged();
    powerSourceToken = PowerManager.PowerSourceKindChanged += (s, e) => OnPowerSourceKindChanged();
    chargeToken = PowerManager.RemainingChargePercentChanged += (s, e) => OnRemainingChargePercentChanged();
    dischargeToken = PowerManager.RemainingDischargeTimeChanged += (s, e) => OnRemainingDischargeTimeChanged();

    if (batteryToken != null && powerToken != null && powerSourceToken != null && chargeToken != null && dischargeToken != null)
    {
        OutputMessage("Successfully registered for state notifications");
    }
    else
    {
        OutputMessage("Failed to register for state notifications");
    }
}

private void OnBatteryStatusChanged()
{
    BatteryStatus batteryStatus = PowerManager.BatteryStatus;
    int remainingCharge = PowerManager.RemainingChargePercent;
    string status = batteryStatus switch
    {
        BatteryStatus.Charging => "Charging",
        BatteryStatus.Discharging => "Discharging",
        BatteryStatus.Idle => "Idle",
        BatteryStatus.NotPresent => "NotPresent",
        _ => "Unknown"
    };

    OutputFormattedMessage($"Battery status changed: {status}, {remainingCharge}% remaining");
    DetermineWorkloads();
}

private void OnPowerSupplyStatusChanged()
{
    //...etc
}
```

---

## Configure app logic based on multiple status values

[PowerManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager) events are relatively low-level, and in some scenarios, a single event handler being called might not provide enough information for the app to decide how to behave. In this example, the [PowerSupplyStatusChanged](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager.powersupplystatuschanged) event could be called when the device is disconnected from power. In that case, the app must check the current battery status before deciding how to proceed.

### [C++ sample](#tab/cpp)

```cpp
void DetermineWorkloads()
{
    BatteryStatus batteryStatus = PowerManager::BatteryStatus();
    int remainingCharge = PowerManager::RemainingChargePercent();
    PowerSupplyStatus powerStatus = PowerManager::PowerSupplyStatus();
    PowerSourceKind powerSource = PowerManager::PowerSourceKind();

    if ((powerSource == PowerSourceKind::DC 
        && batteryStatus == BatteryStatus::Discharging 
        && remainingCharge < 25)
        || (powerSource == PowerSourceKind::AC
        && powerStatus == PowerSupplyStatus::Inadequate))
    {
        // The device is not in a good battery/power state, 
        // so we should pause any non-critical work.
        PauseNonCriticalWork();
    }
    else if ((batteryStatus != BatteryStatus::Discharging && remainingCharge > 75)
        && powerStatus != PowerSupplyStatus::Inadequate)
    {
        // The device is in good battery/power state,
        // so let's kick of some high-power work.
        StartPowerIntensiveWork();
    }
}
```

### [C# sample](#tab/csharp)

```csharp
private void DetermineWorkloads()
{
    BatteryStatus batteryStatus = PowerManager.BatteryStatus();
    int remainingCharge = PowerManager.RemainingChargePercent();
    PowerSupplyStatus powerStatus = PowerManager.PowerSupplyStatus();
    PowerSourceKind powerSource = PowerManager.PowerSourceKind();

    if ((powerSource == PowerSourceKind.DC 
        && batteryStatus == BatteryStatus.Discharging 
        && remainingCharge < 25)
        || (powerSource == PowerSourceKind.AC
        && powerStatus == PowerSupplyStatus.Inadequate))
    {
        // The device is not in a good battery/power state, 
        // so we should pause any non-critical work.
        PauseNonCriticalWork();
    }
    else if ((batteryStatus != BatteryStatus.Discharging && remainingCharge > 75)
        && powerStatus != PowerSupplyStatus.Inadequate)
    {
        // The device is in good battery/power state,
        // so let's kick of some high-power work.
        StartPowerIntensiveWork();
    }
}
```

---

## Check screen status

The [PowerManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.power.powermanager) class offers information about other device states relevant to an app's power usage. For example, apps can disable graphics processing when the device's display is turned off.

### [C++ sample](#tab/cpp)

```cpp
void OnDisplayStatusChanged()
{
    const size_t statusSize = 16;
    WCHAR szStatus[statusSize];
    wmemset(&(szStatus[0]), 0, statusSize);

    DisplayStatus displayStatus = PowerManager::DisplayStatus();
    switch (displayStatus)
    {
    case DisplayStatus::Dimmed:
        wcscpy_s(szStatus, L"Dimmed");
        break;
    case DisplayStatus::Off:
        wcscpy_s(szStatus, L"Off");
        break;
    case DisplayStatus::On:
        wcscpy_s(szStatus, L"On");
        break;
    }

    OutputFormattedMessage(
        L"Display status changed: %s", szStatus);
    if (displayStatus == DisplayStatus::Off)
    {
        // The screen is off, let's stop rendering foreground graphics,
        // and instead kick off some background work now.
        StopUpdatingGraphics();
        StartDoingBackgroundWork();
    }
}
```

### [C# sample](#tab/csharp)

```csharp
private void OnDisplayStatusChanged()
{
    DisplayStatus displayStatus = PowerManager.DisplayStatus;
    string status = displayStatus switch
    {
        DisplayStatus.Dimmed => "Dimmed",
        DisplayStatus.Off => "Off",
        DisplayStatus.On => "On",
        _ => "Unknown"
    };

    OutputFormattedMessage($"Display status changed: {status}");
    if (displayStatus == DisplayStatus.Off)
    {
        // The screen is off, let's stop rendering foreground graphics,
        // and instead kick off some background work now.
        StopUpdatingGraphics();
        StartDoingBackgroundWork();
    }
}
```

---

## Unsubscribe from events

Apps can register and deregister for notifications during their lifecycle. Use your language's preferred event registration management system if your app doesn't need to receive power status notifications during its entire lifecycle.

### [C++ sample](#tab/cpp)

```cpp
void UnregisterPowerManagerCallbacks()
{
    OutputMessage(L"Unregistering state notifications");
    PowerManager::BatteryStatusChanged(batteryToken);
    PowerManager::PowerSupplyStatusChanged(powerToken);
    PowerManager::PowerSourceKindChanged(powerSourceToken);
    PowerManager::RemainingChargePercentChanged(chargeToken);
    PowerManager::RemainingDischargeTimeChanged(dischargeToken);
}
```

### [C# sample](#tab/csharp)

```csharp
private void UnregisterPowerManagerCallbacks()
{
    OutputMessage("Unregistering state notifications");
    PowerManager.BatteryStatusChanged -= batteryToken;
    PowerManager.PowerSupplyStatusChanged -= powerToken;
    PowerManager.PowerSourceKindChanged -= powerSourceToken;
    PowerManager.RemainingChargePercentChanged -= chargeToken;
    PowerManager.RemainingDischargeTimeChanged -= dischargeToken;
}
```

---

## Related topics

* [Windows.System.Power.PowerManager](/uwp/api/Windows.System.Power.PowerManager)
* [PowerSettingRegisterNotification](/windows/win32/api/powersetting/nf-powersetting-powersettingregisternotification)
* [PowerSettingUnregisterNotification](/windows/win32/api/powersetting/nf-powersetting-powersettingunregisternotification)
