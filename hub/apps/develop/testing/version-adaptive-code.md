---
title: Version adaptive code
description: Use the ApiInformation class to use new APIs while maintaining compatibility with previous Windows versions in your Windows App SDK application.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 05/22/2026
---

# Version adaptive code

You can think about writing adaptive code similarly to how you think about creating an adaptive UI. Design your base code to run on the lowest OS version, and then add features when you detect that your app runs on a higher version where a new feature is available.

For background information about `ApiInformation`, API contracts, and configuring Visual Studio, see [Version adaptive apps](version-adaptive-apps.md).

## Prerequisites

- A Windows App SDK project (packaged or unpackaged). See [Quick start: Create your first WinUI 3 app](../../get-started/start-here.md).
- Familiarity with the Windows Runtime (WinRT) type system, since `ApiInformation` checks apply to `Windows.*` namespace types only.

## Runtime API checks

Use the [Windows.Foundation.Metadata.ApiInformation](/uwp/api/windows.foundation.metadata.apiinformation) class in a condition in your code to test for the presence of the API you want to call. This condition is evaluated wherever your app runs, but evaluates to `true` only on devices where the API is present and available to call.

> [!IMPORTANT]
> `ApiInformation` checks work only for **Windows Runtime** types in the `Windows.*` namespace. They do **not** detect WinUI types (`Microsoft.UI.Xaml.*`) because those types are part of the Windows App SDK framework package, not the OS, and aren't registered as WinRT metadata that `ApiInformation` can query. `#if` preprocessor directives don't help here either—they're evaluated at compile time based on target framework, not at runtime based on the OS or SDK version the app is actually running on. To light up a WinUI feature conditionally, check the Windows App SDK version your app was built against (see [Version adaptive apps](version-adaptive-apps.md)), or wrap the call in a try/catch and fall back if it fails at run time.

> [!TIP]
> Numerous runtime API checks can affect your app's performance. Perform the check once and cache the result, then use the cached result throughout your app.

### Adaptive code options

There are two ways to create adaptive code:

- **App code** — Use runtime API checks in code behind. This is the recommended approach for most scenarios.
- **State triggers** — Use extensible state triggers that activate visual states based on the presence of an API. Use state triggers when you have a simple property or enum change between OS versions that is connected to a visual state.

## Example: Check for an enum value

This example shows how to check whether a specific enum value is present before using it. If the value is not present, the code falls back to an alternative. `EnergySaverStatus` and `PowerManager` are genuine Windows Runtime types in the `Windows.System.Power` namespace, so `ApiInformation` can query them correctly.

```csharp
if (ApiInformation.IsEnumNamedValuePresent(
    "Windows.System.Power.EnergySaverStatus", "On"))
{
    if (PowerManager.EnergySaverStatus == EnergySaverStatus.On)
    {
        // Reduce background work to save battery.
        ReduceBackgroundActivity();
    }
}
else
{
    // Energy Saver status isn't available on this OS version; skip the check.
}

void ReduceBackgroundActivity()
{
    // Pause non-essential timers, syncs, and animations here.
}
```

> [!IMPORTANT]
> When you cache the result of an API check, use that cached value consistently throughout your app. Don't repeat the check in multiple places — check once, store the result, and reference it everywhere.

## Example: Check for a method

Use `IsMethodPresent` to verify that a specific method is available before calling it:

```csharp
DisplayRequest displayRequest = new DisplayRequest();

if (ApiInformation.IsMethodPresent(
    "Windows.System.Display.DisplayRequest", "RequestActive"))
{
    displayRequest.RequestActive();
}
```

## Example: Check for a property

Use `IsPropertyPresent` to verify that a specific property is available before reading it:

```csharp
if (ApiInformation.IsPropertyPresent(
    "Windows.System.Power.PowerManager", "RemainingChargePercent"))
{
    int chargePercent = PowerManager.RemainingChargePercent;
}
```

## Best practices

| Practice | Guidance |
|---|---|
| Use static strings | When checking API names with `ApiInformation`, use hard-coded strings rather than .NET reflection to avoid runtime type-loading issues |
| Cache results | Perform each API check once at startup and store the result for reuse |
| Keep minimum version low | Set your project's Minimum Version as low as practical to reach the widest audience, and use adaptive code to light up features on newer OS versions |
| Test on minimum version | Always test on the minimum supported OS version to verify that fallback paths work correctly |

## Related content

- [Version adaptive apps](version-adaptive-apps.md)
- [Conditional XAML](conditional-xaml.md)
