---
title: Creating a C++ notification addon for Electron
description: Learn how to create a C++ native addon that calls Windows App SDK notification APIs in your Electron app using the winapp CLI.
ms.date: 02/20/2026
ms.topic: how-to
---

# Creating a C++ notification addon for Electron

This guide shows you how to create a C++ native addon that uses the Windows App SDK notification APIs in your Electron app. This is a great starting point for understanding native addons before diving into more complex scenarios.

## Prerequisites

- Completed the [development environment setup](electron-setup.md)
- **Windows 11**

## Step 1: Create a C++ native addon

Use the winapp CLI to generate a C++ addon template:

```bash
npx winapp node create-addon --template cpp --name nativeWindowsAddon
```

This creates a `nativeWindowsAddon/` folder with:

- `addon.cc` - Your C++ code that will call Windows APIs
- `binding.gyp` - Build configuration for native compilation

The command also adds a `build-addon` script to your `package.json`.

Build the addon:

```bash
npm run build-addon
```

## Step 2: Add notification code

Open `nativeWindowsAddon/addon.cc` and update it to use the Windows App SDK notification APIs. The generated template includes the necessary Windows SDK and Windows App SDK headers.

Add includes for the notification API:

```cpp
#include <winrt/Microsoft.Windows.AppNotifications.h>
#include <winrt/Microsoft.Windows.AppNotifications.Builder.h>
```

Add a function that shows a Windows notification:

```cpp
Napi::Value ShowNotification(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    try {
        std::string title = info[0].As<Napi::String>().Utf8Value();
        std::string message = info[1].As<Napi::String>().Utf8Value();

        winrt::Microsoft::Windows::AppNotifications::Builder::AppNotificationBuilder builder;
        builder.AddText(winrt::to_hstring(title));
        builder.AddText(winrt::to_hstring(message));

        auto notification = builder.BuildNotification();
        winrt::Microsoft::Windows::AppNotifications::AppNotificationManager::Default().Show(notification);

        return Napi::Boolean::New(env, true);
    } catch (const winrt::hresult_error& e) {
        Napi::Error::New(env, winrt::to_string(e.message())).ThrowAsJavaScriptException();
        return env.Null();
    }
}
```

Register the function in the `Init` method:

```cpp
exports.Set("showNotification", Napi::Function::New(env, ShowNotification));
```

## Step 3: Build the addon

```bash
npm run build-addon
```

## Step 4: Test the notification

Open `src/index.js` and load the addon:

```javascript
const addon = require('../nativeWindowsAddon/build/Release/nativeWindowsAddon.node');
```

Add a test call:

```javascript
addon.showNotification('Hello from Electron!', 'This notification uses the Windows App SDK.');
```

Run the app:

```bash
npm start
```

You should see a Windows notification appear.

## Step 5: Update debug identity

Whenever you modify `appxmanifest.xml`, run:

```bash
npx winapp node add-electron-debug-identity
```

## Next steps

- [Creating a Phi Silica addon](electron-phi-silica-addon.md) - Use on-device AI APIs
- [Creating a WinML addon](electron-winml-addon.md) - Run machine learning models
- [Packaging for distribution](electron-packaging.md) - Create a signed MSIX package

## Related topics

- [Setting up Electron for Windows API development](electron-setup.md)
- [winapp CLI reference](../usage.md)
- [Windows App SDK notification APIs](/windows/apps/develop/notifications/app-notifications/app-notifications-quickstart)
