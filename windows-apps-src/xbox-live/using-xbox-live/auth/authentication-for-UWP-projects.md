---
title: Authentication for UWP projects
author: aablackm
description: Learn how to sign in Xbox Live users in a Universal Windows Platform (UWP) title.
ms.assetid: e54c98ce-e049-4189-a50d-bb1cb319697c
ms.author: aablackm
ms.date: 03/14/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, authentication, sign-in
ms.localizationpriority: low
---
# Authentication for UWP projects

To take advantage of Xbox Live features in games, a user needs to create an Xbox Live profile to identify themselves in the Xbox Live community.  Xbox Live services keep track of game related activities using that Xbox Live profile, such as the user's gamertag and gamer picture, who the user's gaming friends are, what games the user has played, what achievements the user has unlocked, where the user stands on the leaderboard for a particular game, etc.

When a user wants to access Xbox Live services in a particular game on a particular device, the user needs to authenticate first.  The game can call Xbox Live APIs to initiate the authenticate process.  In some cases, the user will be presented with an interface to provide additional information, such as entering the username and password of the Microsoft Account to use, giving permission consent to the game, resolving account issues, accepting new terms of use, etc.

Once authenticated, the user is associated with on that device until they explicitly sign out of Xbox Live from the Xbox app.  Only one player is allowed to be authenticated on a device at a time (for all Xbox Live games);  for a new player to be authenticated on the device, the existing authenticated player must sign out first.

## Steps To Sign-In

At a high level, you use the Xbox Live APIs by following these steps:

1. Create an XboxLiveUser object to represent the user
2. Sign-in silently to Xbox Live at startup
3. Attempt to sign-in with UX if required
4. Create an Xbox Live context based on the interacting user
5. Use the Xbox Live context to access Xbox Live services
6. When the game exits or the user signs-out, release the XboxLiveUser object and XboxLiveContext object by setting them to null

### Creating an XboxLiveUser object

Most of the Xbox Live activities are related to the Xbox Live Users.  As a game developer, you need to first create an XboxLiveUser object to represent the local user.

C++:

```cpp
auto xboxUser = std::make_shared<xbox_live_user>(Windows::System::User^ windowsSystemUser);
```

C++/CX (WinRT):

```cpp
XboxLiveUser xboxUser = ref new XboxLiveUser(Windows::System::User^ windowsSystemUser);
```

C# (WinRT):

```csharp
XboxLiveUser xboxUser = new XboxLiveUser(Windows.System.User windowsSystemUser);
```

* **windowsSystemUser**
  The windows system user object to be used to associate with xbox live user. Could be nullptr if the app is a single user application(SUA).
  * For more information about Single User Application(SUA) and Multi User Application(MUA), please check [Introduction to multi-user applications](https://docs.microsoft.com/en-us/windows/uwp/xbox-apps/multi-user-applications#single-user-applications)
  * For more information about how to get Windows::System::User^ from Windows, please check [retrieving windows system user on UWP](retrieving-windows-system-user-on-UWP.md)

### Sign-in silently to Xbox Live at startup ###

Your game should start to authenticate the user to Xbox Live as early as possible after launching, even before you present the user interface, to pre-fetch data from Xbox Live services.

To authenticate the local user silently, call

C++:

```cpp
pplx::task<xbox_live_result<sign_in_result>> xbox_live_user::signin_silently(Platform::Object^ coreDispatcher)
```

C++/CX (WinRT):

```cpp
Windows::Foundation::IAsyncOperation<SignInResult^>^ XboxLiveUser::SignInSilentlyAsync(Platform::Object^ coreDispatcher)
```

C# (WinRT):

```csharp
Microsoft.Xbox.Services.System.SignInResult XboxLiveUser.SignInSilentlyAsync(Windows.UI.Core.CoreDispatcher coreDispatcher);
```

* **coreDispatcher**

  Thread Dispatcher is used to communication between threads. Although silent sign in API is not going to show any UI, XSAPI still need the UI thread dispatcher for getting the information about your appx's locale. You can get the static UI thread dispatcher by calling Windows::UI::Core::CoreWindow::GetForCurrentThread()->Dispatcher in the UI thread. Or if you're certain that this API is being called on the UI thread, you can pass in nullptr(for example on JS UWA).


There are 3 possible outcomes from the silent sign-in attempt

* **Success**

  If the device is online, this means the user authenticated to Xbox Live successfully, and we were able to get a valid token.

  if the device is offline, This means the user has previously authenticated to Xbox Live successfully, and has not explicitly signed-out from this title.  Note in this case there is no guarantee that title has access to a valid token, it is only guaranteed that the user’s identity is known and has been verified.    The identity of the user is known to the title via their xbox user id (xuid) and gamertag.

* **UserInteractionRequired**

  This means the runtime was unable to sign-in the user silently.  The game should call `xbox_live_user::sign_in` which invokes the Xbox Identity Provider to show the necessary UX flow for the user to sign-up/sign-in.  Common issues are:

  * User does not have a Microsoft Account
  * User has not set a preferred Microsoft Account for gaming
  * The selected Microsoft Account doesn’t have an Xbox Live profile
  * User needs to accept Microsoft Account consent

* **Other errors**

  The runtime was unable to sign-in due to other reasons.  Typically these issues are not actionable by the game or the user. When using c++ API, you would need to check error by checking xbox_live_result<>.err(); on WinRT, you would need to catch Platform::Exception^.

### Attempt to sign-in with UX if required ###

Your game should authenticate the user to Xbox Live with UX enabled when silent sign-in was unsuccessful, and you are ready to present the user interface.

To authenticate the local user with UX, call

C++:

```cpp
pplx::task<xbox_live_result<sign_in_result>> xbox_live_user::signin(Platform::Object^ coreDispatcher)
```


C++/CX (WinRT):

```cpp
Windows::Foundation::IAsyncOperation<SignInResult^>^ XboxLiveUser::SignInAsync(Platform::Object^ coreDispatcher)
```

C# (WinRT):

```csharp
Microsoft.Xbox.Services.System.SignInResult  XboxLiveUser.SignInAsync(Windows.UI.Core.CoreDispatcher coreDispatcher);
```

* **coreDispatcher**

  Thread Dispatcher is used to communication between threads. Sign in API requires the UI dispatcher so that it can show the sign in UI and get the information about your appx's locale. You can get the static UI thread dispatcher by calling Windows::UI::Core::CoreWindow::GetForCurrentThread()->Dispatcher in the UI thread. Or if you're certain that this API is being called on the UI thread, you can pass in nullptr(for example on JS UWA).

There are 3 possible outcomes from the sign-in attempt with UX:

* **Success**

  If the device is online, this means the user authenticated to Xbox Live successfully, and we were able to get a valid token.

  if the device is offline, This means the user has previously authenticated to Xbox Live successfully, and has not explicitly signed-out from this title.  Note in this case there is no guarantee that title has access to a valid token, it is only guaranteed that the user’s identity is known and has been verified.    The identity of the user is known to the title xbox user id (xuid) and gamertag.

* **UserCancel**

  This means that the user cancelled the sign-in operation before completion.  When this happens, the game should NOT automatically retry sign-in with UX.  Instead, it should present in-game UX that allows the user to retry the sign-in operation.  (For example, a sign-in button)

* **Other errors**

  The runtime was unable to sign-in due to other reasons.  Typically these issues are not actionable by the game or the user. When using c++ API, you would need to check error by checking xbox_live_result<>.err(); on WinRT, you would need to catch Platform::Exception^.

## Sign-In Code Examples

### C++

```cpp

#include "xsapi\services.h" // contains the xbox::services::system namespace

using namespace xbox::services::system; // contains definitions necessary for sign-in

void SignInSample::SignIn()
{
    //1. Create an xbox_live_user object
    m_user = std::make_shared<xbox::services::system::xbox_live_user>(); // m_user declared in header file

    //2. Sign-In silently to Xbox Live at startup
    m_user->signin_silently()
        .then([this](xbox::services::xbox_live_result<xbox::services::system::sign_in_result> result)
    {
        if (!result.err())
        {
            auto rPayload = result.payload();
            switch (rPayload.status())
            {
            case xbox::services::system::sign_in_status::success:
                // sign-in successful
                signIn = true;
                break;
            case xbox::services::system::sign_in_status::user_interaction_required:
                // 3. Attempt to sign-in with UX if required
                m_user->signin(Windows::UI::Core::CoreWindow::GetForCurrentThread()->Dispatcher)
                    .then([this](xbox::services::xbox_live_result<xbox::services::system::sign_in_result> loudResult) // use task_continuation_context::use_current() to make the continuation task running in current apartment 
                {
                    if (!loudResult.err())
                    {
                        auto resPayload = loudResult.payload();
                        switch (resPayload.status())
                        {
                        case xbox::services::system::sign_in_status::success:
                            // sign-in successful
                            signIn = true;
                            break;
                        case xbox::services::system::sign_in_status::user_cancel:
                            // user cancelled sign in 
                            // present in-game UX that allows the user to retry the sign-in operation. (For example, a sign-in button)
                            break;
                        }
                    }
                    else
                    {
                        //login has failed at this point
                    }
                }, concurrency::task_continuation_context::use_current());
                break;
            }
        }
    });
    if (signIn)
    {
        // 4. Create an Xbox Live context based on the interacting user
        m_xboxLiveContext = std::make_shared<xbox::services::xbox_live_context>(m_user); // m_xboxLiveContext declared in header file

        // add sign out event handler
        AddSignOut();
    }
}

void SignInSample::AddSignOut()
{
    xbox::services::system::xbox_live_user::add_sign_out_completed_handler(
		[this](const xbox::services::system::sign_out_completed_event_args&)

	{
        // 6. When the game exits or the user signs-out, release the XboxLiveUser object and XboxLiveContext object by setting them to null
		m_user = NULL;
		m_xboxLiveContext = NULL;
	});
}

```

### C# (WinRT)

```csharp

using System.Diagnostics;
using Microsoft.Xbox.Services.System;
using Microsoft.Xbox.Services;

public async Task SignIn()
{
    bool signedIn = false;

    // Get a list of the active Windows users.
    IReadOnlyList<Windows.System.User> users = await Windows.System.User.FindAllAsync();

    // Acquire the CoreDispatcher which will be required for SignInSilentlyAsync and SignInAsync.
    Windows.UI.Core.CoreDispatcher UIDispatcher = Windows.UI.Xaml.Window.Current.CoreWindow.Dispatcher; 

    try
    {
        // 1. Create an XboxLiveUser object to represent the user
        XboxLiveUser primaryUser = new XboxLiveUser(users[0]);

        // 2. Sign-in silently to Xbox Live
        SignInResult signInSilentResult = await primaryUser.SignInSilentlyAsync(UIDispatcher);
        switch (signInSilentResult.Status)
        {
            case SignInStatus.Success:
                signedIn = true;
                break;
            case SignInStatus.UserInteractionRequired:
                //3. Attempt to sign-in with UX if required
                SignInResult signInLoud = await primaryUser.SignInAsync(UIDispatcher);
                switch(signInLoud.Status)
                {
                    case SignInStatus.Success:
                        signedIn = true;
                        break;
                    case SignInStatus.UserCancel:
                        // present in-game UX that allows the user to retry the sign-in operation. (For example, a sign-in button)
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        if(signedIn)
        {
            // 4. Create an Xbox Live context based on the interacting user
            Microsoft.Xbox.Services.XboxLiveContext m_xboxLiveContext = new Microsoft.Xbox.Services.XboxLiveContext(user);

            //add the sign out event handler
            XboxLiveUser.SignOutCompleted += OnSignOut;
        }
    }
    catch (Exception e)
    {
        Debug.WriteLine(e.Message);
    }

}

public void OnSignOut(object sender, SignOutCompletedEventArgs e)
    {
        // 6. When the game exits or the user signs-out, release the XboxLiveUser object and XboxLiveContext object by setting them to null
        primaryUser = null;
        xboxLiveContext = null;
    }
```

## Sign Out

### Handling user sign-out completed event

The user will sign-out from a title if one of the following happens:

1. The user signed-out from the Xbox App (Windows 10) or console shell (Xbox One). Signing out will affect all Xbox Live enabled apps installed for this user.
2. The user switched to a different Microsoft Account
3. The user signed into the same title from a different device

In all these cases, the title will receive an event from the `xbox_live_user::add_sign_out_completed_handler` or `XboxLiveUser::SignOutCompleted` handlers.  The game must handle the sign out completed event appropriately:

1. The game should display clear visual indication to the user that she/he has signed-out from Xbox Live.
2. The game cannot call any Xbox Live service APIs in the event handler, because the user has already signed-out and there is no authorization token available.

## Sign Out Handler Code Samples

### C++

```cpp

xbox::services::system::xbox_live_user::add_sign_out_completed_handler(
		[this](const xbox::services::system::sign_out_completed_event_args&)

	{
        // 6. When the game exits or the user signs-out, release the XboxLiveUser object and XboxLiveContext object by setting them to null
		m_user = NULL;
		m_xboxLiveContext = NULL;
	});

```

### C# (WinRT)

```csharp
XboxLiveUser.SignOutCompleted += OnUserSignOut;

public void OnSignOut(object sender, SignOutCompletedEventArgs e)
        {
            // 6. When the game exits or the user signs-out, release the XboxLiveUser object and XboxLiveContext object by setting them to null
            primaryUser = null;
            xboxLiveContext = null;
        }
```

## Determining if the device is offline

Sign in APIs will still be success when offline if the user has signed in once, and the last signed in account will be returned.

If the title can be played offline (Campaign mode, etc.)
Regardless the device is online or offline, the title can allow the user to play and record game progress via WriteInGameEvent API and Connected Storage API, both of them work properly while the device is offline.

If the title cannot be played offline (Multiplayer game or Server based game, etc.)
The title should call the GetNetworkConnectivityLevel API to find out if the device is offline, and inform the user about the status and possible solutions (for example, ‘You need to connect to Internet to continue…’)

## Online Status Code Samples

### C++

```cpp

using namespace Windows::Networking::Connectivity;

//Retrieve the ConnectionProfile
ConnectionProfile^ InternetConnectionProfile = NetworkInformation::GetInternetConnectionProfile();

NetworkConnectivityLevel connectionLevel = InternetConnectionProfile->GetNetworkConnectivityLevel();

switch (connectionLevel)
{
case NetworkConnectivityLevel::InternetAccess:
    // User is connected to the internet.
    break;
case NetworkConnectivityLevel::ConstrainedInternetAccess: //Limited Internet Access Possible Authentication Required
     // display error message for user.
    LogConnectivityLine("Game Offline: Limited internet access, browser authentication may be required. "); //function writes to UI
    break;
default:
    LogConnectivityLine("Game Offline: No internet access.");
    break;
}

```

### C# (WinRT)

```csharp
using Windows.Networking.Connectivity;

//Retrieve the ConnectionProfile
string connectionProfileInfo = string.Empty;
ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

NetworkConnectivityLevel connectionLevel = InternetConnectionProfile.GetNetworkConnectivityLevel();

switch(connectionLevel)
    {
        case NetworkConnectivityLevel.InternetAccess:
            // User is connected to the internet.
            break;
        case NetworkConnectivityLevel.ConstrainedInternetAccess: //Limited Internet Access Possible Authentication Required
            // display error message for user.
            LogConnectivityLine("Game Offline: Limited internet access, browser authentication may be required. "); //function writes to UI
            break;
        default:
            LogConnectivityLine("Game Offline: No internet access.");
            break;
    }
```