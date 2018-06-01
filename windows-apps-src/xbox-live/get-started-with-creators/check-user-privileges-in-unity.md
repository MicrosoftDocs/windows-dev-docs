---
title: Check user privilege settings in Unity
author: aablackm
description: Learn how to check privilege settings for a signed in Xbox Live account.
ms.assetid:
ms.author: aablackm
ms.date: 10/26/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, accounts, test accounts, parental controls, user privileges, enforcement bans, upsell
---
# Check user privilege settings in Unity
On Xbox Live, every authenticated user’s account has associated privileges. Privileges control which features of Xbox Live a user can access at a given point in time. Some of these privileges are for system-controlled features, while others may be associated with specific games or extension subscriptions. In addition, parental controls and bans issued by the Xbox Live enforcement team can restrict the privileges of a user. These privileges cover a number of common scenarios, including multiplayer, accessing user generated content, chatting, or to streaming video. Games use this information to make access control and personalization decisions.

## Prerequisites
In order to determine user privilege settings, you must have configured your game for authentication with Xbox Live and successfully signed a user in.

>[!IMPORTANT]
> If you are testing your game in the Unity editor, your game is not connected to the Xbox Live service, and is using fake data to simulate a connection. This results in a null value when you check for privileges. To test with real data, perform a Universal Windows Platform build of your Unity game and open the generated project file in Visual Studio.

The following articles outline the steps that you can take:

* [Sign in to Xbox Live in Unity (build and test sign-in)](unity-prefabs-and-sign-in.md#build-and-test-sign-in)
* [Test your Unity game build in Visual Studio](test-visual-studio-build.md)

## Determine privileges
A user’s privileges are carried in the token received at authentication time. In Unity, you can access the list of privileges that a user has in the `XboxLiveUser` class, after the user has successfully signed in. Privileges are stored as a single string, separated by a space. For example, you might see a user with the following privileges:

```csharp
public XboxLiveUserInfo XboxLiveUser;

//sign in is done and the user has been successfully signed in

Debug.Log(XboxLiveUser.User.Privileges);

//Console would read:
// Privileges: "188 191 192 193 194 195 196 198 199 200 201 203 204 205 206 207 208 211 214 215 216 217 220 224 227 228 235 238 245 247 249 252 254 255"
```

If you want to look for a specific permission, you can check to see if the `Privileges` property contains the associated code:

```csharp
public XboxLiveUserInfo XboxLiveUser;

//sign in is done and the user has been successfully signed in

if (XboxLiveUser.User.Privileges.Contains("247"))
{
    Debug.Log("User has the user_created_content privilege");
}
```

## Privilege Codes
The following is a list of possible privilege codes that may be returned.

| Code  | Privilege  | Description   |
|------ |-----------------------------  |-------------------    |
| 190   | broadcast             | Can broadcast live game play.     |
| 197   | view_friends_list     | Can view other user's friends list.   |
| 198   | game_dvr              | Can upload recorded in-game videos to the cloud.      |
| 199   | share_kinect_content          | Kinect recorded content can be uploaded to the cloud for the user and made accessible to anyone. |
| 203   | multiplayer_parties           | Can join a party session.     |
| 205   | communication_voice_ingame    | Can participate in voice chat during parties and multiplayer game sessions.    |
| 206   | communication_voice_skype     | Can use voice communication with Skype on Xbox One.   |
| 207   | cloud_gaming_manage_session   | Can allocate and manage a cloud compute cluster for a hosted game session.    |
| 208   | cloud_gaming_join_session     | Can join a cloud compute session.     |
| 209   | cloud_saved_games     | Can save games in cloud title storage.    |
| 211   | share_content     | Can share content with others.    |
| 214   | premium_content   | Can purchase, download and launch premium content available with the Xbox Live Gold subscription.     |
| 219   | subscription_content  | Can purchase and download premium subscription content and use premium subscription features.     |
| 220   | social_network_sharing    | Can share progress information on social networks.    |
| 224   | premium_video     | Can access premium video services.    |
| 235   | purchase_content  | Can purchase content.     |
| 247   | user_created_content  | Can download and view online user created content.    |
| 249   | profile_viewing   | Can view other user's profiles.   |
| 252   | communications    | Can use asynchronous text messaging with anyone.    |
| 254   | multiplayer_sessions  | Can join a multiplayer sessions for a game.   |
| 255   | add_friend    | Can follow other Xbox Live users and add Xbox Live friends.   |
