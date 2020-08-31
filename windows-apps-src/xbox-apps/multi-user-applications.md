---
title: Introduction to multi-user applications
description: Get a high-level introduction to the Xbox One multi-user model that supports multiple users playing games cooperatively on a single device.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 2dde6ed3-7f53-48a6-aebe-2605230decb8
ms.localizationpriority: medium
---
# Introduction to multi-user applications

This topic is intended to be a simple high-level introduction to the Xbox multi-user model.

The Xbox One user model is tuned to the requirements of a gaming console that supports multiple users playing games cooperatively on a single device. 
It enables multiple users, each with their own controller, to be signed in and using the console at the same time in a single interactive session. 
This is different from other Windows devices. For example:
* **Windows desktop PCs** allow multiple users to use the same device, but each user has their own interactive session and each session is completely independent of the other sessions on the device.
* **Windows phones** allow only a single user to use the device. That single user is determined during the OOBE (out-of-box-experience) and the user cannot sign out after they are signed in. In effect, if a different user wants to use the device, the device has to be reset. 
* **Xbox One** allows multiple users to be signed in and use the device at the same time in a single interactive session.

Each user in the Xbox One user model is backed by a local user account. 
This local user account is associated with an Xbox Live account (and therefore a Microsoft account). 
This means that there is a strict one-to-one mapping of an Xbox user account to an Xbox Live account and to a Microsoft account.

## Single user applications
By default, Universal Windows Platform (UWP) apps run in the context of the user that launched the application. 
These *single user applications* (SUAs) are only aware of that single user, and run in a mode that is compatible with the user model on other Windows devices. 
The Xbox user model manages which user is associated with the app and guarantees that a user is signed in when the app is launched. 
In this model, UWP app and game authors do not have to do anything special to run on Xbox. 

## Multi-user applications
UWP games can choose to opt into the Xbox One multi-user model. 
These *multi-user applications* (MUAs) run in the context of a system account (called the Default Account) and can take full advantage of the flexibility and power of the Xbox One user model. 
For these games, the Xbox user model does not manage which user is associated with the game and does not even require that a user is signed in for the game to run. 
This means that they have to be written to be explicitly aware of, and manage their user requirements: whether they require a signed-in user or not, whether they implement the concept of a current user, whether they allow simultaneous input from multiple users, and so on.
   
To opt into the multi-user model:   
1. Open your project in Visual Studio.   
2. Select the package.appxmanifest.xml file.   
3. Right-click and select **View Code**.   
4. Add the following line in the `<Properties></Properties>` section:

```
<uap:SupportedUsers>multiple</uap:SupportedUsers>
```

### Identifying users and inputs
Developers can use KeyRoutedEventArgs.DeviceId, used by KeyUp and KeyDown routed events, to differentiate the events generated from different inputs.
Using the Windows.System.UserDeviceAssociation.FindUserFromDeviceId method will help to identify the user associated to a specific input.

See the [KeyRoutedEventArgs.DeviceId](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.deviceid) topic for more information.


## Guidance on which model to choose
All UWP apps and the majority of single user games can be written to be SUAs. 
We recommend that only cooperative multi-player games consider opting into the Xbox One multi-user model.

## See also
- [UWP on Xbox One](index.md)