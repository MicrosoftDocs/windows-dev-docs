---
description: Discover the different options desktop apps have for sending toast notifications
title: Activating toast notifications from desktop apps
label: Activating toast notifications from desktop apps
template: detail.hbs
ms.date: 02/27/2025
ms.topic: article
keywords: windows 10, uwp, win32, desktop, toast notifications, desktop bridge, msix, external location, options for sending toasts, com server, com activator, com, fake com, no com, without com, send toast
ms.localizationpriority: medium
---

# Activating toast notifications from desktop apps

Packaged and unpackaged Win32 apps can send interactive toast notifications just like UWP apps can. That includes packaged apps (see [Create a new project for a packaged WinUI 3 desktop app](../../../winui/winui3/create-your-first-winui3-app.md#packaged-create-a-new-project-for-a-packaged-c-or-c-winui-3-desktop-app)); packaged apps with external location (see [Grant package identity by packaging with external location](../../../desktop/modernize/grant-identity-to-nonpackaged-apps.md)); and unpackaged apps (see [Create a new project for an unpackaged WinUI 3 desktop app](../../../winui/winui3/create-your-first-winui3-app.md#unpackaged-create-a-new-project-for-an-unpackaged-c-or-c-winui-3-desktop-app)).

However, for an unpackaged Win32 app there are a few special steps. That's due to the different activation schemes, and the lack of package identity at runtime.

In this topic, we list out the options you have for sending a toast notification on Windows 10. Every option fully supports...

* Persisting in Action Center
* Being activatable from both the popup and inside Action Center
* Being activatable while your EXE isn't running

## All options

The table below illustrates your options for supporting toasts within your desktop app, and the corresponding supported features. You can use the table to select the best option for your scenario.<br/><br/>

| Option | Visuals | Actions | Inputs | Activates in-process |
| -- | -- | -- | -- | -- |
| [COM activator](#preferred-option---com-activator) | **Supported** | **Supported** | **Supported** | **Supported** |
| [No COM / Stub CLSID](#alternative-option---no-com--stub-clsid) | **Supported** | **Supported** | Not supported | Not supported |

## Preferred option - COM activator

This is the preferred option that works for desktop apps, and supports all notification features. Don't be afraid of the "COM activator"; we have a library [for C#](./send-local-toast.md) and [C++ apps](send-local-toast-desktop-cpp-wrl.md) that makes this very straightforward, even if you've never written a COM server before.<br/><br/>

| Visuals | Actions | Inputs | Activates in-process |
| -- | -- | -- | -- |
| **Supported** | **Supported** | **Supported** | **Supported** |

With the COM activator option, you can use the following notification templates and activation types in your app.<br/><br/>

| Template and activation type | Packaged | Unpackaged |
| -- | -- | -- |
| ToastGeneric Foreground | **Supported** | **Supported** |
| ToastGeneric Background | **Supported** | **Supported** |
| ToastGeneric Protocol | **Supported** | **Supported** |
| Legacy templates | **Supported** | Not supported |

> [!NOTE]
> If you add the COM activator to your existing packaged app, then Foreground/Background and Legacy notification activations will activate your COM activator instead of your command line.

To learn how to use this option, see [Send a local toast notification from desktop C# apps](./send-local-toast.md) or [Send a local toast notification from Win32 C++ WRL apps](send-local-toast-desktop-cpp-wrl.md).

## Alternative option - No COM / Stub CLSID

This is an alternative option if you can't implement a COM activator. However, you'll sacrifice a few features, such as input support (text boxes on toasts) and activating in-process.<br/><br/>

| Visuals | Actions | Inputs | Activates in-process |
| -- | -- | -- | -- |
| **Supported** | **Supported** | Not supported | Not supported |

With this option, if you support desktop, then you're much more limited in the notification templates and activation types that you can use, as seen below.<br/><br/>

| Template and activation type | Packaged | Unpackaged |
| -- | -- | -- |
| ToastGeneric Foreground | **Supported** | Not supported |
| ToastGeneric Background | **Supported** | Not supported |
| ToastGeneric Protocol | **Supported** | **Supported** |
| Legacy templates | **Supported** | Not supported |

For packaged apps, just send toast notifications like a UWP app would. When the user clicks on your toast, your app will be command-line launched with the launch args that you specified in the toast.

For unpackaged apps, set up the AUMID so that you can send toasts, and then also specify a CLSID on your shortcut. That can be any random GUID. Don't add the COM server/activator. You're adding a "stub" COM CLSID, which will cause Action Center to persist the notification. Note that you can use only protocol activation toasts, because the stub CLSID will break activation of any other toast activations. Therefore, you have to update your app to support protocol activation, and have the toast's protocol activate your own app.

## Resources

* [Send a local toast notification from desktop C# apps](./send-local-toast.md)
* [Send a local toast notification from Win32 C++ WRL apps](send-local-toast-desktop-cpp-wrl.md)
* [Toast content documentation](adaptive-interactive-toasts.md)