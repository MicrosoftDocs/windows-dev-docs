---
Description: Discover the different options desktop Win32 apps have for sending toast notifications
title: Toast notifications from desktop apps
label: Toast notifications from desktop apps
template: detail.hbs
ms.date: 05/01/2018
ms.topic: article
keywords: windows 10, uwp, win32, desktop, toast notifications, desktop bridge, msix, sparse package, options for sending toasts, com server, com activator, com, fake com, no com, without com, send toast
ms.localizationpriority: medium
---
# Toast notifications from desktop apps

Desktop apps (including packaged [MSIX](/windows/msix/desktop/source-code-overview) apps, apps that use [sparse packages](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps) to obtain package identity, and classic non-packaged Win32 apps) can send interactive toast notifications just like Windows apps. However, there are a few different options for desktop apps due to the different activation schemes.

In this article, we list out the options you have for sending a toast notification on Windows 10. Every option fully supports...

* Persisting in Action Center
* Being activatable from both the popup and inside Action Center
* Being activatable while your EXE isn't running

## All options

The table below illustrates your options for supporting toasts within your desktop app, and the corresponding supported features. You can use the table to select the best option for your scenario.<br/><br/>

| Option | Visuals | Actions | Inputs | Activates in-process |
| -- | -- | -- | -- | -- |
| [COM activator](#preferred-option---com-activator) | ✔️ | ✔️ | ✔️ | ✔️ |
| [No COM / Stub CLSID](#alternative-option---no-com--stub-clsid) | ✔️ | ✔️ | ❌ | ❌ |


## Preferred option - COM activator

This is the preferred option that works for desktop apps, and supports all notification features. Don't be afraid of the "COM activator"; we have a library [for C#](send-local-toast-desktop.md) and [C++ apps](send-local-toast-desktop-cpp-wrl.md) that makes this very straightforward, even if you've never written a COM server before.<br/><br/>

| Visuals | Actions | Inputs | Activates in-process |
| -- | -- | -- | -- |
| ✔️ | ✔️ | ✔️ | ✔️ |

With the COM activator option, you can use the following notification templates and activation types in your app.<br/><br/>

| Template and activation type | MSIX/sparse package | Classic Win32 |
| -- | -- | -- |
| ToastGeneric Foreground | ✔️ | ✔️ |
| ToastGeneric Background | ✔️ | ✔️ |
| ToastGeneric Protocol | ✔️ | ✔️ |
| Legacy templates | ✔️ | ❌ |

> [!NOTE]
> If you add the COM activator to your existing MSIX/sparse package app, Foreground/Background and Legacy notification activations will now activate your COM activator instead of your command line.

To learn how to use this option, see [Send a local toast notification from desktop C# apps](send-local-toast-desktop.md) or [Send a local toast notification from desktop C++ WRL apps](send-local-toast-desktop-cpp-wrl.md).


## Alternative option - No COM / Stub CLSID

This is an alternative option if you cannot implement a COM activator. However, you will sacrifice a few features, such as input support (text boxes on toasts) and activating in-process.<br/><br/>

| Visuals | Actions | Inputs | Activates in-process |
| -- | -- | -- | -- |
| ✔️ | ✔️ | ❌ | ❌ |

With this option, if you support classic Win32, you are much more limited in the notification templates and activation types that you can use, as seen below.<br/><br/>

| Template and activation type | MSIX/sparse package | Classic Win32 |
| -- | -- | -- |
| ToastGeneric Foreground | ✔️ | ❌ |
| ToastGeneric Background | ✔️ | ❌ |
| ToastGeneric Protocol | ✔️ | ✔️ |
| Legacy templates | ✔️ | ❌ |

For packaged [MSIX](/windows/msix/desktop/source-code-overview) apps and apps that use [sparse packages](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps), just send toast notifications like a UWP app would. When the user clicks on your toast, your app will be command line launched with the launch args that you specified in the toast.

For classic Win32 apps, set up the AUMID so that you can send toasts, and then also specify a CLSID on your shortcut. This can be any random GUID. Don't add the COM server/activator. You're adding a "stub" COM CLSID, which will cause Action Center to persist the notification. Note that you can only use protocol activation toasts, as the stub CLSID will break activation of any other toast activations. Therefore, you have to update your app to support protocol activation, and have the toasts protocol activate your own app.


## Resources

* [Send a local toast notification from desktop C# apps](send-local-toast-desktop.md)
* [Send a local toast notification from desktop C++ WRL apps](send-local-toast-desktop-cpp-wrl.md)
* [Toast content documentation](adaptive-interactive-toasts.md)