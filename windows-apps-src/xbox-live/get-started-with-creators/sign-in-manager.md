---
title: Sign-In with the SignInManager in Unity
author: aablackm
description: Overview of the Unity Plugin Sign-In Manager
ms.author: aablackm
ms.date: 5/08/2018
ms.topic: get-started-article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, unity
---
# Scripting Sign-In

In order to add sign-in to your own custom game objects you will need to script it into a GameObject. Let us say that the PlayerAuthentication prefab doesn't fit your game and you'd like to have your own sign-in panel, this article will take you through the basic steps of adding sign-in logic to your title.

## Sign in with the SignInManager

The Xbox Live Unity plug-in contains a script for the `SignInManager` under the file path **Assets >> XboxLive >> Scripts >> SignInManager.cs**. The manager is a Singleton class which can be called form anywhere in your title by referring to the title's *Instance* of the `SignInManager`. This *Instance* does not need to be initialized and you can use the it as soon as your game begins. You can access all of the its public properties and functions by referring to the *Instance* as `SignInManager.Instance`.

The `SignInManager` contains all of the code necessary for managing authentication for your title, this includes sign-in, sign-out, and getting information about which users are signed in as which player.

### Calls and Results

The `SignInManager` has three async co-routine functions `SignInPlayer(int playerNumber)`, `SignOutPlayer(int playerNumber)`, and `SwitchUser(int playerNumber)`, that trigger event functions to collect the results of the call and act accordingly. You can add corresponding functions to your script and assign them to the `SignInManager.Instance`'s callback list. The event functions are `OnPlayerSignIn(int playerNumber, UnityAction<XboxLiveUser, XboxLiveAuthStatus, string> callback)`, `OnPlayerSignOut(int playerNumber, UnityAction<XboxLiveUser, XboxLiveAuthStatus, string> callback)`, `OnAnyPlayerSignIn(UnityAction<XboxLiveUser, XboxLiveAuthStatus, string> callback)`, and `OnAnyPlayerSignOut(UnityAction<XboxLiveUser, XboxLiveAuthStatus, string> callback)`. Each on of the event functions listens for the event described in its name. You can add your own functions to the manager's callback list in your title's `Start()` function with the following code.

```csharp
using Microsoft.Xbox.Services;
using Microsoft.Xbox.Services.Client;

void Start () {
    try
    {
        SignInManager.Instance.OnPlayerSignOut(this.playerNumber, this.OnPlayerSignOut);
        SignInManager.Instance.OnPlayerSignIn(this.playerNumber, this.OnPlayerSignIn);
    }
    catch (Exception ex)
    {
        Debug.LogWarning(ex.Message);
    }

}
```

This code snippet adds sign-in and sign-out listeners for the player associated with this GameObject's playerNumber. This GameObject's `OnPlayerSignIn` function will be called when the `SignInManager` detects a sign-in attempt has completed and its `OnPlayerSignOut` function will be called when the SignInManager detects a sign-out. The event functions in your GameObject must have a return type and parameters to match the function type called by the SignInManager. Both the `OnPlayerSignIn` and `OnPlayerSignOut` are void functions which need an `XboxLiveUser`, `XboxLiveAuthStatus`, and a string as their parameters. The shell of your functions may look like the following:

```csharp
using Microsoft.Xbox.Services;
using Microsoft.Xbox.Services.Client;

private void OnPlayerSignIn(XboxLiveUser xboxLiveUserParam, XboxLiveAuthStatus authStatus, string errorMessage)
{
}

private void OnPlayerSignOut(XboxLiveUser xboxLiveUserParam, XboxLiveAuthStatus authStatus, string errorMessage)
{
}
```

In both functions check the `XboxLiveAuthStatus` to make sure that your call to the `SignInManager.Instance` was successful. On a successful call the `XboxLiveUser` will be the `XboxLiveUser`, that was signed in our out by `SignInManager`. When the call is unsuccessful the `errorMessage` string will contain details on the reason for failure.

Adding a few lines of code to check for a successful call would result in code like the following:

```csharp
using Microsoft.Xbox.Services;
using Microsoft.Xbox.Services.Client;

private void OnPlayerSignIn(XboxLiveUser xboxLiveUserParam, XboxLiveAuthStatus authStatus, string errorMessage)
{
    if(authStatus == XboxLiveAuthStatus.Succeeded)
    {
        this.xboxLiveUser = xboxLiveUserParam; //store the xboxLiveUser SignedIn
        this.signedIn = true;
    }
    else
    {
        if (XboxLiveServicesSettings.Instance.DebugLogsOn)
        {
            Debug.LogError(errorMessage); //Log the error message in case of unsuccessful call. 
        }
    }
}

private void OnPlayerSignOut(XboxLiveUser xboxLiveUserParam, XboxLiveAuthStatus authStatus, string errorMessage)
{
    if (authStatus == XboxLiveAuthStatus.Succeeded)
    {
        this.xboxLiveUser = null;
        this.signedIn = false;
    }
    else
    {
        if (XboxLiveServicesSettings.Instance.DebugLogsOn)
        {
            Debug.LogError(errorMessage);
        }
    }
}
```

By calling sign-in and capturing the resulting event for the result, you can handle sign-in and sign-out for your title.

## Get Signed In Player Information

In addition to signing players into the service the SignInManager keeps track of all the signed in users. On PC this will be limited to a single signed in player, and on the Xbox it is limited to 16. You can check how near the limit you are by comparing the result of `SignInManager.Instance.GetCurrentNumberOfPlayers()` to the result of `SignInManager.Instance.GetMaximumNumberOfPlayers()`. The SignInManager has a dictionary of signed-in players indexed by that player's *playerNumber*. You can use this to retrieve some basic information about the player accessible from their associated `XboxLiveUser`.

```csharp
if (SignInManager.Instance.GetPlayer(this.playerNumber).IsSignedIn) // If there is a player signed in for this gameObjects player number
            {
                this.displayedGamertag = SignInManager.Instance.GetPlayer(this.playerNumber).Gamertag; // Set that users gamertag to the gamertag displayed
            }
```

This little bit of code checks to see if there is a player signed in to the player number slot for this GameObject and then stores that users gamertag to be displayed if they are signed in. While the `XboxLiveUser` contains the signed in users gamertag and Xbox user ID (xuid) you will need to call other services like the `SocialManager` to access information like gamerpic and gamerscore.

## Destroying your sign-in GameObject

When destroying a game object that uses one of the Xbox Live plugin managers like the `SignInManager` or the `SocialManager`, usually when loading a new scene, it is important to remove any functions added to the list of event listeners for the manager. In the example code for this article we would need to remove the functions we added to the event listeners for sign-in and sign-out. We would remove these functions from the `SignInManager` in the `OnDestroy()` function of our GameObject.

```csharp
private void OnDestroy()
{
    if (SignInManager.Instance != null)
    {
        SignInManager.Instance.RemoveCallbackFromPlayer(this.PlayerNumber, this.OnPlayerSignOut);
        SignInManager.Instance.RemoveCallbackFromPlayer(this.PlayerNumber, this.OnPlayerSignIn);
    }
```

This code will remove the sign-in and sign-out callback functions for the player associated with this GameObject.

## Testing you code in Visual Studio

In addition to the [steps required to build your game in Visual Studio](configure-xbox-live-in-unity.md#build-and-test-the-project), listed in the [Configure your Xbox Live Title for Unity](configure-xbox-live-in-unity.md) article, there is an additional step required to test your game properly in Visual Studio. You will need to update a property of the package.appxmanifest.xml file. To do this:

1. Search the Solution Explorer for the package.appxmanifest.xml file
2. Right click the file and choose View Code
3. Under the `<Properties><\/Properties>` section, add the following line: `<uap:SupportedUsers>multiple<\/uap:SupportedUsers>.
4. Deploy the game to your Xbox by starting a remote debugging build from Visual Studio. You can find instruction to set up your title on an Xbox in the [Set up your UWP on Xbox development environment](../../xbox-apps/development-environment-setup.md) article.

> [!NOTE]
> The piece of configuration changed may look like it is enabling multi-player but it is still necessary to run your game in single player scenarios.

## Policies and Limitations

There are a few sign-in policies and limitations of the Title's that you may want to consider when developing your sign-in experience.

- After your title's initial sign-in you must keep at least one player signed-in. The `SignInManager` will throw an error and fail the call if you attempt to sign out the last signed in user. It is also important to note that you cannot call `SignInManager.Instance.SwitchUser(int playerNumber)`, on the last signed in player as it will attempt to sign out the player before signing in a new player.

- PC can only sign-in one user at a time, Console may sign in up to 16 players at once.

- The title does not actually have permission to sign out a player from the OS, because of this SignOut may not work as expected. The SignInManager can sign a user in and out where the title is concerned but cannot sign anyone out from the machine the title is deployed to.

- Multiple user sign-in is only available on the Xbox One Console.

- Guest accounts are not available to Xbox Live Creators Program titles.