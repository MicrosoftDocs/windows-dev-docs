---
description: Learn how to get certain kinds of activation info for packaged .NET and C++ desktop (Win32) apps
title: Get activation info for packaged apps
ms.date: 09/17/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Get activation info for packaged apps

Starting in Windows 10, version 1809, packaged desktop apps can call the [AppInstance.GetActivatedEventArgs](/uwp/api/windows.applicationmodel.appinstance.getactivatedeventargs) method to retrieve certain kinds of app activation info during startup. For example, you can call this method to get info related to app activation from opening a file, clicking an interactive toast, or using a protocol. Starting in Windows 10, version 2004, this feature is also supported in packaged apps with external location (see [Grant package identity by packaging with external location](./grant-identity-to-nonpackaged-apps.md)).

> [!NOTE]
> In addition to retrieving certain types of activation info by using the [AppInstance.GetActivatedEventArgs](/uwp/api/windows.applicationmodel.appinstance.getactivatedeventargs) method as described in this article, you can also retrieve activation info for background tasks by defining a COM class. For more info, see [Create and register a winmain COM background task](/windows/uwp/launch-resume/create-and-register-a-winmain-background-task).

## Code example

The following code example demonstrates how to call the [AppInstance.GetActivatedEventArgs](/uwp/api/windows.applicationmodel.appinstance.getactivatedeventargs) method from the **Main** function in a Windows Forms app. For each activation type your app supports, cast the `args` return value to the corresponding event args type. In this code example, the `Handlexxx` methods are assumed to be dedicated activation handler code that you have defined elsewhere.

```csharp
static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    var args = AppInstance.GetActivatedEventArgs();
    switch (args.Kind)
    {
        case ActivationKind.Launch:
            HandleLaunch(args as LaunchActivatedEventArgs);
            break;
        case ActivationKind.ToastNotification:
            HandleToastNotification(args as ToastNotificationActivatedEventArgs);
            break;
        case ActivationKind.VoiceCommand:
            HandleVoiceCommand(args as VoiceCommandActivatedEventArgs);
            break;
        case ActivationKind.File:
            HandleFile(args as FileActivatedEventArgs);
            break;
        case ActivationKind.Protocol:
            HandleProtocol(args as ProtocolActivatedEventArgs);
            break;
        case ActivationKind.StartupTask:
            HandleStartupTask(args as StartupTaskActivatedEventArgs);
            break;
        default:
            HandleLaunch(null);
            break;
    }
```

## Supported activation types

You can use the [AppInstance.GetActivatedEventArgs](/uwp/api/windows.applicationmodel.appinstance.getactivatedeventargs) method to retrieve activation info from the supported set of event args objects listed in the following table. Some of these activation types require the use of a package extension in the package manifest.

[ShareTargetActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.sharetargetactivatedeventargs) activation info is supported only on Windows 10, version 2004, and later. All other activation info types are supported on Windows 10, version 1809, and later.

| Event args type | Package extension | Related docs | 
|-------------------|-----------------|-----------------------|
| [ShareTargetActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.sharetargetactivatedeventargs) | [uap:ShareTarget](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-sharetarget) | [Making your desktop application a share target](./desktop-to-uwp-extend.md#making-your-desktop-application-a-share-target) |
| [ProtocolActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.protocolactivatedeventargs) | [uap:Protocol](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol) | [Start your application by using a protocol](./desktop-to-uwp-extensions.md#start-your-application-by-using-a-protocol) |
| [ToastNotificationActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.toastnotificationactivatedeventargs) | desktop:ToastNotificationActivation | [Toast notifications from desktop apps](/windows/uwp/design/shell/tiles-and-notifications/toast-desktop-apps). |
| [StartupTaskActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.startuptaskactivatedeventargs)  | desktop:StartupTask | [Start an executable file when users log into Windows](./desktop-to-uwp-extensions.md#start-an-executable-file-when-users-log-into-windows) |
| [FileActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.fileactivatedeventargs) | [uap:FileTypeAssociation](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation) | [Associate your packaged application with a set of file types](./desktop-to-uwp-extensions.md#associate-your-packaged-application-with-a-set-of-file-types) |
| [VoiceCommandActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.voicecommandactivatedeventargs) | None | [Activate a foreground app with voice commands through Cortana](../../design/input/cortana-launch-a-foreground-app-with-voice-commands.md) |
| [LaunchActivatedEventArgs](/uwp/api/windows.applicationmodel.activation.launchactivatedeventargs) | None |  |
