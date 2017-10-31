---
title: Authentication for UWP projects
author: KevinAsgari
description: Learn how to sign in Xbox Live users in a Universal Windows Platform (UWP) title.
ms.assetid: e54c98ce-e049-4189-a50d-bb1cb319697c
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, authentication, sign-in
localizationpriority: medium
---

# Authentication for UWP projects

To take advantage of Xbox Live features in games, a user needs to create an Xbox Live profile to identify themselves in the Xbox Live community.  Xbox Live services keep track of game related activities using that Xbox Live profile, such as the user's gamertag and gamer picture, who the user's gaming friends are, what games the user has played, what achievements the user has unlocked, where the user stands on the leaderboard for a particular game, etc.

When a user wants to access Xbox Live services in a particular game on a particular device, the user needs to authenticate first.  The game can call Xbox Live APIs to initiate the authenticate process.  In some cases, the user will be presented with an interface to provide additional information, such as entering the username and password of the Microsoft Account to use, giving permission consent to the game, resolving account issues, accepting new terms of use, etc.

Once authenticated, the user is associated with on that device until they explicitly sign out of Xbox Live from the Xbox app.  Only one player is allowed to be authenticated on a device at a time (for all Xbox Live games);  for a new player to be authenticated on the device, the existing authenticated player must sign out first.

At a high level, you use the Xbox Live APIs by following these steps:

1. Create an XboxLiveUser object to represent the user
2. Sign-in silently to Xbox Live at startup
3. Attempt to sign-in with UX if required
4. Create an Xbox Live context based on the interacting user
5. Use the Xbox Live context to access Xbox Live services
6. When the game exits or the user signs-out, release the XboxLiveUser object and XboxLiveContext object by setting them to null

### Creating an XboxLiveUser object ###
Most of the Xbox Live activities are related to the Xbox Live Users.  As a game developer, you need to first create an XboxLiveUser object to represent the local user.

C++:
```
auto user = std::make_shared<xbox_live_user>(Windows::System::User^ windowsSystemUser);
```

WinRT:
```
XboxLiveUser user = ref new XboxLiveUser(Windows::System::User^ windowsSystemUser);
```

* **windowsSystemUser**
  The windows system user object to be used to associate with xbox live user. Could be nullptr if the app is a single user application(SUA).
  * For more information about Single User Application(SUA) and Multi User Application(MUA), please check [Introduction to multi-user applications](https://docs.microsoft.com/en-us/windows/uwp/xbox-apps/multi-user-applications#single-user-applications)
  * For more information about how to get Windows::System::User^ from Windows, please check [retrieving windows system user on UWP](retrieving-windows-system-user-on-UWP.md)

### Sign-in silently to Xbox Live at startup ###
Your game should start to authenticate the user to Xbox Live as early as possible after launching, even before you present the user interface, to pre-fetch data from Xbox Live services.

To authenticate the local user silently, call

C++:
```
pplx::task<xbox_live_result<sign_in_result>> xbox_live_user::signin_silently(Platform::Object^ coreDispatcher)
```

WinRT:
```
Windows::Foundation::IAsyncOperation<SignInResult^>^ XboxLiveUser::SignInSilentlyAsync(Platform::Object^ coreDispatcher)
```


* **coreDispatcher**

  Thread Dispatcher is used to communication between threads. Although silent sign in API is not going to show any UI, XSAPI still need the UI thread dispatcher for getting the information about your appx's locale. You can get the static UI thread dispatcher by calling Windows::UI::Core::CoreWindow::GetForCurrentThread()->Dispatcher in the UI thread. Or if you're certain that this API is being called on the UI thread, you can pass in nullptr(for example on JS UWA).


There are 3 possible outcomes from the silent sign-in attempt

* **Success**

  If the device is online, this means the user authenticated to Xbox Live successfully, and we were able to get a valid token.

  if the device is offline, This means the user has previously authenticated to Xbox Live successfully, and has not explicitly signed-out from this title.  Note in this case there is no guarantee that title has access to a valid token, it is only guaranteed that the user’s identity is known and has been verified.	The identity of the user is known to the title via their xbox user id (xuid) and gamertag.

* **UserInteractionRequired**

  This means the runtime was unable to sign-in the user silently.  The game should call `xbox_live_user::sign_in` which invokes the Xbox Identity Provider to show the necessary UX flow for the user to sign-up/sign-in.  Common issues are:

  *	User does not have a Microsoft Account
  *	User has not set a preferred Microsoft Account for gaming
  *	The selected Microsoft Account doesn’t have an Xbox Live profile
  *	User needs to accept Microsoft Account consent


* **Other errors**

  The runtime was unable to sign-in due to other reasons.  Typically these issues are not actionable by the game or the user. When using c++ API, you would need to check error by checking xbox_live_result<>.err(); on WinRT, you would need to catch Platform::Exception^.


### Attempt to sign-in with UX if required ###
Your game should authenticate the user to Xbox Live with UX enabled when silent sign-in was unsuccessful, and you are ready to present the user interface.

To authenticate the local user with UX, call

C++:
```
pplx::task<xbox_live_result<sign_in_result>> xbox_live_user::signin(Platform::Object^ coreDispatcher)
```

WinRT:
```
Windows::Foundation::IAsyncOperation<SignInResult^>^ XboxLiveUser::SignInAsync(Platform::Object^ coreDispatcher)
```


* **coreDispatcher**

  Thread Dispatcher is used to communication between threads. Sign in API requires the UI dispatcher so that it can show the sign in UI and get the information about your appx's locale. You can get the static UI thread dispatcher by calling Windows::UI::Core::CoreWindow::GetForCurrentThread()->Dispatcher in the UI thread. Or if you're certain that this API is being called on the UI thread, you can pass in nullptr(for example on JS UWA).

There are 3 possible outcomes from the sign-in attempt with UX:

* **Success**

  If the device is online, this means the user authenticated to Xbox Live successfully, and we were able to get a valid token.

  if the device is offline, This means the user has previously authenticated to Xbox Live successfully, and has not explicitly signed-out from this title.  Note in this case there is no guarantee that title has access to a valid token, it is only guaranteed that the user’s identity is known and has been verified.	The identity of the user is known to the title xbox user id (xuid) and gamertag.

* **UserCancel**

  This means that the user cancelled the sign-in operation before completion.  When this happens, the game should NOT automatically retry sign-in with UX.  Instead, it should present in-game UX that allows the user to retry the sign-in operation.  (For example, a sign-in button)

* **Other errors**

  The runtime was unable to sign-in due to other reasons.  Typically these issues are not actionable by the game or the user. When using c++ API, you would need to check error by checking xbox_live_result<>.err(); on WinRT, you would need to catch Platform::Exception^.

### Handling user sign-out completed event ###

The user will sign-out from a title if one of the following happens:

1.	The user signed-out from the Xbox App (Windows 10) or console shell (Xbox One). Signing out will affect all Xbox Live enabled apps installed for this user.
2.	The user switched to a different Microsoft Account
3.	The user signed into the same title from a different device

In all these cases, the title will receive an event from the `xbox_live_user::add_sign_out_completed_handler` or `XboxLiveUser::SignOutCompleted` handlers.  The game must handle the sign out completed event appropriately:
1. The game should display clear visual indication to the user that she/he has signed-out from Xbox Live.
2. The game cannot call any Xbox Live service APIs in the event handler, because the user has already signed-out and there is no authorization token available.

### Determining if the device is offline ###

Sign in APIs will still be success when offline if the user has signed in once, and the last signed in account will be returned.

If the title can be played offline (Campaign mode, etc.)
Regardless the device is online or offline, the title can allow the user to play and record game progress via WriteInGameEvent API and Connected Storage API, both of them work properly while the device is offline.

If the title cannot be played offline (Multiplayer game or Server based game, etc.)
The title should call the GetNetworkConnectivityLevel API to find out if the device is offline, and inform the user about the status and possible solutions (for example, ‘You need to connect to Internet to continue…’)
