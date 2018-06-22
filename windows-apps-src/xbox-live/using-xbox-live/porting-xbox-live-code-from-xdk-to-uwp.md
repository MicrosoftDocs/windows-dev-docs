---
title: Porting Xbox Live code from XDK to UWP
author: KevinAsgari
description: Learn how to port Xbox Live code from the Xbox Development Kit (XDK) platform to the Universal Windows Platform (UWP).
ms.assetid: 69939f95-44ad-4ffd-851f-59b0745907c8
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, xdk, porting
ms.localizationpriority: low
---

# Porting Xbox Live code from the Xbox Developer Kit (XDK) to Universal Windows Platform (UWP)

## Introduction

This article is intended to help developers who have used the Xbox One XDK to get started migrating their Xbox Live code to the Windows 10 Universal Windows Platform (UWP).

Part of this migration includes switching from XSAPI 1.0 (Xbox Live Services API, included in the Xbox One XDK through August 2015) to XSAPI 2.0 (included in the Xbox One XDK starting in November 2015, and also available in the Xbox Live SDK. The functionality of these APIs are virtually identical, but there are some important implementation differences.

Other topics to be covered in this article include preparing your Windows development computer and installing other APIs typically needed when using Xbox Live services, such as the Secure Sockets API as well as the Connected Storage API for managing cloud-backed game saves.

<a name="_Setting_up_and"></a>

## Setting up and configuring your project in Dev Center and XDP

A UWP title that uses Xbox Live services needs to be configured in the [Windows Dev Center](https://dev.windows.com/en-us) and the [Xbox Developer Portal (XDP)](https://xdp.xboxlive.com/). For the latest information, see [Adding Xbox Live to a new or existing UWP project](../get-started-with-partner/get-started-with-visual-studio-and-uwp.md) in the Xbox Live Programming Guide included with the [Xbox Live SDK](https://developer.xboxlive.com/en-us/live/development/Pages/Downloads.aspx).

Topics on that page include these steps for using Xbox Live services in your title:

-   Create the UWP app project in the Windows Dev Center.

-   Use XDP to set up your project for Xbox Live usage.

-   Link your Dev Center product to your XDP product.

-   Create developer accounts in XDP (required when running your Xbox Live title in your sandbox).

If your titles support multiplayer play, some additional settings may be required in your multiplayer session templates. All Windows 10 titles that use Xbox Live multiplayer and write to an MPSD (multiplayer session document) require this new field in the list of "capabilities" found in your session templates: ```userAuthorizationStyle: true```.

### Enabling cross-play

If you will support "cross-play" (a shared Xbox Live configuration between Xbox One and PC games, allowing cross-device multiplayer gaming), you will also need to add this capability to your session templates: **crossPlay: true**.

For additional information about supporting cross-play and its configuration requirements in XDP, see "Ingesting XDK and UWP Cross-Play Titles in XDP" in the Xbox Live Programming Guide.

Also, for some programmatic considerations, see the later section [Supporting multiplayer cross-play between Xbox One and PC](#_Supporting_multiplayer_cross-play).

## Setting up your Windows development environment

1.  [Download the latest **Xbox Live SDK**](https://developer.xboxlive.com/en-us/live/development/Pages/Downloads.aspx) and extract locally.

2.  [Install the **Xbox Live Platform Extensions SDK**](https://developer.xboxlive.com/en-us/live/development/Pages/Downloads.aspx) if you need the Secure Sockets API and/or the Game Save API (aka Connected Storage) for UWP.

3.  Add Xbox Live support to your Universal Windows app project in Visual Studio. You can add either the full source or reference the binaries by installing the NuGet package into your Visual Studio project. Packages are available for both C++ and WinRT. For more detail see [Adding Xbox Live to a new or existing UWP project](../get-started-with-partner/get-started-with-visual-studio-and-uwp.md)

4.  Configure your development computer to use your sandbox. There's a command-line script in the Tools directory of the Xbox Live SDK that you can use from an administrator command prompt (for example: SwitchSandbox.cmd XDKS.1).

  **Note** To switch back to the retail sandbox, you can either delete the registry key that the script modifies, or you can switch to the sandbox called RETAIL.

1.  Add a developer account to your development computer. A developer account created in XDP is required to interact with Xbox Live services at runtime when you are developing in your assigned sandbox or running samples. To add one or more accounts to Windows:

    1.  Open **Settings** (shortcut: Windows key + I).

    2.  Open **Accounts**.

    3.  On the **Your Account** tab, click **Add a Microsoft account**.

    4.  Enter the developer account email and password.

### AppxManifest changes

The most common changes between the Xbox and UWP versions of the appxmanifest.xml file are:

1. Package Identity matters in UWP, even during development. Both the Identity Name and Publisher *must match* what was defined in Dev Center for your UWP app.

1. A Package Dependency section is required. For example:

```xml
  <Dependencies>
    <TargetDeviceFamily Name="Windows.Universal" MinVersion="10.0.10240.0" MaxVersionTested="10.0.10240.0" />
  </Dependencies>
```

1.  Refer to an example UWP application manifest (for example, one of the UWP samples included with the Xbox Live SDK or a default Universal Windows app project created in Visual Studio) for other sections of the application manifest that have specific requirements for UWP, such as ```<VisualElements>```.

1.  Title and SCID are defined in the xboxservices.config file (see the [next section](#_Define_your_title)) instead of in the "xbox.live" extension category.

1.  The "xbox.system.resources" extension category is not needed.

1.  Secure Sockets are defined in the networkmanifest.xml file (see [Secure sockets](#_Secure_sockets)) instead of in the "windows.xbox.networking" category.

1.  A "windows.protocol" extension category must be defined in order to receive Xbox Live invites in your UWP title (see [Sending and receiving invites](#_Sending_and_receiving)).

1.  If you use the GameChat API, you'll want to add the microphone device capability inside the ```<Capabilities>``` element. For example:

  ```<DeviceCapability Name="microphone">```

<a name="_Define_your_title"></a>

### Define your title and SCID for the Xbox Live SDK in a config file

The Xbox Live SDK needs to know your title ID and SCID, which are no longer included in the appxmanifest.xml for UWP titles. Instead, you create a text file named **xboxservices.config** in your project root directory and add the following fields, replacing the values with the info for your title:

```
{
  "TitleId": 123456789,
  "PrimaryServiceConfigId": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"
}
```

> [!NOTE]
> All values inside xboxservices.config are case sensitive.

Include this config file as content in your project so that it is available in the build output.

**Note** These values will be available programmatically within your title by using the following API:

```cpp
Microsoft::Xbox::Services::XboxLiveAppConfiguration^ xblConfig = xblContext->AppConfig;
unsigned int titleId = xblConfig->TitleId;
Platform::String^ scid = xblConfig->ServiceConfigurationId;
```

### API namespace mapping

Table 1. Namespace mapping from XDK to UWP.

<table>
  <tr>
    <td></td>
    <td><b>Xbox One XDK</b></td><td><b>UWP</b></td>
    <td><b>API is available with...</b></td>
  </tr>
  <tr>
    <td>Xbox Services API (XSAPI)</td>
    <td>Microsoft::Xbox::Services</td>
    <td>Microsoft::Xbox::Services (<i>no change</i>)</td>
    <td>Xbox Live SDK (use NuGet binary or source)</td>
  </tr>
  <tr>
    <td>GameChat</td>
    <td>
      Microsoft::Xbox::GameChat
      Windows::Xbox::Chat
    </td>
    <td>
      Microsoft::Xbox::GameChat (*no change*)
      Microsoft::Xbox::ChatAudio
    </td>
    <td>
      Xbox Live SDK (use NuGet binary)
    </td>
  </tr>
  <tr>
    <td>SecureSockets</td>
    <td>Windows::Xbox::Networking</td>
    <td>Windows::Networking::XboxLive</td>
    <td>Xbox Live Platform Extensions SDK</td>
  </tr>
  <tr>
    <td>Connected Storage</td>
    <td>Windows::Xbox::Storage</td>
    <td>Windows::Gaming::XboxLive::Storage</td>
    <td>Xbox Live Platform Extensions SDK</td>
  </tr>
</table>

### Multiplayer subscriptions and event handling

One of the breaking changes from XSAPI 1.0 to XSAPI 2.0 that most multiplayer titles will encounter is the move of several methods and events from the **RealTimeActivityService** to the **MultiplayerService**.

For example:

-   **EnableMultiplayerSubscriptions()\*** method

-   **DisableMultiplayerSubscriptions()** method

-   **MultiplayerSessionChanged** event

-   **MultiplayerSubscriptionLost** event

-   **MultiplayerSubscriptionsEnabled** property

**Important implementation note** Even though you might not be explicitly using anything else in the **RealTimeActivityService** after moving these events and methods over to the **MultiplayerService**, you must still call **xblContext-&gt;RealTimeActivityService-&gt;Activate()** before calling **EnableMultiplayerSubscriptions()** because the multiplayer subscriptions require the RTA service.

## What's handled differently in UWP

Following is a very high level list of sections of code that will likely have differences between the XDK and UWP, as encountered in the new [NetRumble sample](https://developer.xboxlive.com/en-us/platform/development/education/Pages/Samples.aspx) (which includes both XDK and UWP versions):

-   Accessing title ID and SCID info

-   Prelaunch activation (new for UWP)

-   Suspend/resume PLM handling

-   Extended execution (new for UWP)

-   Xbox **User** object and user-handling differences

    -   Sign-in and sign-out handling

    -   Controller pairing (only handled on Xbox)

    -   Gamepad handling

-   Checking multiplayer privileges

-   Supporting multiplayer cross-play between Xbox One and PC

-   Sending game invites

    -   Ability to open party app from in-game - n/a on UWP

    -   Ability to enumerate party members from in-game - n/a on UWP

-   Showing the gamer profile

-   Secure Socket API surface changes

-   QoS measurement initiation and result handling

-   Writing game events

-   GameChat: events, settings, and ChatUser object

-   Connected Storage API surface changes

-   PIX events (only on Xbox; not covered in this white paper)

-   Some rendering differences

The following sections go into further detail on many of these differences.

### Accessing title ID and SCID info

In UWP, your title ID and service configuration ID are accessed through the AppConfig property on an instance of an **XboxLiveContext**.

```cpp
Microsoft::Xbox::Services::XboxLiveAppConfiguration^ xblConfig = xblContext->AppConfig;
unsigned int titleId = xblConfig->TitleId;
Platform::String^ scid = xblConfig->ServiceConfigurationId;
```

**Note** In the XDK, you can get these IDs by using either these new properties or the old static properties in **Windows::Xbox::Services::XboxLiveConfiguration**.

### Prelaunch activation

Frequently-used titles in Windows 10 may be prelaunched when the user signs in. To handle this, your title should have code that checks the launch arguments for **PreLaunchActivated**. For example, you probably don't want to load all your resources during this kind of activation. For more information, see the MSDN article [Handle app prelaunch](https://msdn.microsoft.com/library/windows/apps/mt593297.ASPx).

### Suspend/resume PLM handling

Suspend and resume, and PLM in general, work similarly in a Universal Windows app to the way they work on Xbox One; however, there are a few important differences to keep in mind:

-   There is no **Constrained** state on the PC—this is an Xbox One exclusive concept.

-   Suspending begins immediately when the title is minimized; for a way around this, see the section [Extended execution](#_Extended_execution).

-   The timing is different: you have 5 seconds to suspend on a PC instead of the 1 second on the console.

Another important consideration if you use connected storage is the new **ContainersChangedSinceLastSync** property in the UWP version of this API. When handling a resume event, you can check this property to see if any containers changed in the cloud while your title was suspended. This can happen if the player suspended the game on one PC, played elsewhere, and then returned to the first PC. If you had read data from these containers into memory before you had suspended, you probably want to read them again to see what changed and handle the changes accordingly.

For more information about handling PLM in a UWP app on Windows 10, see the MSDN article [Launching, resuming, and background tasks](https://msdn.microsoft.com/library/windows/apps/xaml/mt227652.aspx).

You may also find the [PLM for Xbox One](https://developer.xboxlive.com/en-us/platform/development/education/Documents/PLM%20for%20Xbox%20One.aspx) white paper on GDN useful because it was written with games in mind, and most of the concepts for handling the app lifecycle still apply on a PC.

<a name="_Extended_execution"></a>

### Extended execution

Minimizing a UWP app on a PC typically results in it immediately starting to suspend. By using extended execution, you have the opportunity to delay this process. Example implementation:

```cpp
using namespace Windows::ApplicationModel::ExtendedExecution;
//If this goes out of scope the request is nullified
ExtendedExecutionSession^ session;
void App::RequestExtension()
{
       if (!session)
       {
              session = ref new ExtendedExecutionSession();
       }
       session->Reason = ExtendedExecutionReason::Unspecified;
       session->Description = "foo";
       session->Revoked += ref new TypedEventHandler<Platform::Object^, ExtendedExecutionRevokedEventArgs^>(this, &App::ExtensionRevokedHandler);
       IAsyncOperation<ExtendedExecutionResult>^ request = session->RequestExtensionAsync();
       //At this point the request has been made. When the IAsyncOperation request completes, verify that the ExtendedExecutionResult == Allowed and you will not suspend for up to 10 minutes while minimized.
}

void App::ExtensionRevokedHandler(Platform::Object^ obj, ExtendedExecutionRevokedEventArgs^ args)
{
       if (args->Reason == Windows::ApplicationModel::ExtendedExecutionRevokedReason::Resumed)
       {
              //Request the extension again in preparation for the next suspend.
RequestExtension();
       }
       //The app will either complete suspending if the extension was revoked by system policy or resume running if the user has switched back to the app.
}

```

After the **ExtensionRevokedHandler** has been called, a new extension needs to be requested for future potential suspensions. The **ExtensionRevokedHandler** is called when there is memory pressure in the system, 10 minutes have elapsed, or the user switches back to the game while the game is minimized. So **RequestExtension()** should likely be called at these times:

-   During startup.

-   In the **ExtensionRevokedHandler** when args-&gt;Reason == Resumed (the user tabbed back while the game was minimized before the 10-minute timer expired).

-   In the **OnResuming** handler (if the title was suspended due to memory pressure or the 10-minute timer).

### Handling users and controllers

On Windows, you work with one signed-in user at a time. In the Xbox Live SDK, you first create an **XboxLiveUser** object, sign them in to Xbox Live, and then create **XboxLiveContext** objects from this user.

Before, on the Xbox One XDK:

1.  Acquire a user (from gamepad interaction, for example).
2.  Create an **XboxLiveContext** from that user:
  ```
  ref new Microsoft::Xbox::Services::XboxLiveContext( Windows::Xbox::System::User^ user )
  ```
1.  Handle a **SignOut** event:
  ```
  Windows::Xbox::System::User::SignOutStarted
  ```
1.  Handle gamepad/controller pairing using:
  ```
  Windows::Xbox::Input::Controller::ControllerRemoved
  Windows::Xbox::Input::Controller::ControllerPairingChanged
  ```

Now, for the UWP/Xbox Live SDK:

1.  Create an **XboxLiveUser**:

  ```
  auto xblUser = ref new Microsoft::Xbox::Services::System::XboxLiveUser();
  ```

1.  Try to sign them in by using the last Microsoft account they used, without bothering them with UI:

  ```
  xblUser->SignInSilentlyAsync();
  ```

1.  If you get **SignInResult::Success** in the result from this async operation, create the **XboxLiveContext**, and then you're finished:

  ```
  auto xblContext = ref new Microsoft::Xbox::Services::XboxLiveContext( xblUser );
  ```

1.  If instead you get **SignInResult::UserInteractionRequired**, you need to call the interactive sign-in method that brings up system UI:

  ```
  xblUser->SignInAsync();
  ```

1.  From here you may get **SignInResult::UserCancel**, in which case you don't have a signed-in user and you should consider providing a menu option for them to try signing in again.

  **Note** When providing menu options, it's a good idea to give them the option to switch to a different Microsoft account:

  ```
  xblUser->SwitchAccountAsync( nullptr );
  ```

1.  After you have a signed-in user, you may want to hook up the **XboxLiveUser::SignOutCompleted** event so that you can react to the user signing out:

  ```
  xblUser->SignOutCompleted += ref new Windows::Foundation::EventHandler<Microsoft::Xbox::Services::System::SignOutCompletedEventArgs^>( &OnSignOutCompleted );
  ```

1.  There is no controller pairing to handle in Windows 10.

This is a simplified example for C++ / WinRT. For a more detailed example, see "Xbox Live Authentication in Windows 10" in the Xbox Live Programming Guide. You may also find the broader example at "Adding Xbox Live to a new UWP project" helpful.

### Checking multiplayer privileges

The equivalent to **CheckPrivilegeAsync()** is not yet available in the Xbox Live SDK. For now, you will need to search for the privilege you need in the string list returned by the **Privileges** property for an **XboxLiveUser**. For example, to check for multiplayer privileges, look for privilege "254." Using the XDK documentation, you can find a list of all the Xbox Live privileges in the **Windows::Xbox::ApplicationModel::Store::KnownPrivileges** enumeration.

For a discussion on this topic, see the forum post [xsapi & user privileges](https://forums.xboxlive.com/questions/48513/xsapi-user-privileges.html).

<a name="_Supporting_multiplayer_cross-play"></a>

### Supporting multiplayer cross-play between Xbox One and PC UWP

In addition to new session template requirements in XDP (see [Setting up and configuring your project in Dev Center and XDP](#_Setting_up_and)), cross-play comes with new restrictions on session join ability. You can no longer use "None" as a session join restriction. You must use either "Followed" or "Local" (the default restriction is "Local").

Also, the join and read restrictions default to "Local" because of the required **userAuthorizationStyle** capability for Windows 10 multiplayer.

This forum article, [Is it possible to create a public multiplayer session](https://forums.xboxlive.com/questions/46781/is-it-possible-to-create-public-multiplayer-sessio.html), contains additional insight.

Further information and examples can be found in the updated multiplayer developer flowcharts, the cross-play-enabled multiplayer sample NetRumble, or from your Developer Account Manager (DAM).

<a name="_Sending_and_receiving"></a>

### Sending and receiving invites

The API to bring up the UI for sending invites is now **Microsoft::Xbox::Services::System::TitleCallableUI::ShowGameInviteUIAsync()**. You pass in a session-&gt; **SessionReference** object from your activity session (typically your lobby). You can optionally pass in a second parameter that references a custom invite string ID that's been defined in your service configuration in XDP. The string you define there will appear in the toast notification sent to the invited players. Note that what you are passing in as a parameter to this method is the ID number, and it must be formatted properly for the service. For example, string ID "1" must be passed in as "///1".

If you want to send invites directly by using the multiplayer service (that is, without showing any UI), you can still use the other invite method, **Microsoft::Xbox::Services::Multiplayer::MultiplayerService::SendInvitesAsync()** from the user's **XboxLiveContext**.

To allow for invites coming into Windows to protocol-activate your title, you need to add this extension to the **&lt;Application&gt;** element in the appxmanifest:

```xml
<Extensions>
  <uap:Extension Category="windows.protocol">
    <uap:Protocol Name="ms-xbl-multiplayer" />
  </uap:Extension>
</Extensions>
```

You can then handle the invite as you did before on Xbox One when your **CoreApplication** gets an **Activated** event and the activation Kind is an **ActivationKind::Protocol**.

### Showing the gamer profile card

To pop up the gamer profile card on UWP, use **Microsoft::Xbox::Services::System::TitleCallableUI::ShowProfileCardUIAsync()**, passing in the XUID for the target user.

<a name="_Secure_sockets"></a>

### Secure sockets

The Secure Socket API is included in the separate [Xbox Live Platform Extensions SDK](https://developer.xboxlive.com/en-us/live/development/Pages/Downloads.aspx).

See this forum post for API usage: [Setting up SecureDeviceAssociation for cross platform](https://forums.xboxlive.com/answers/45722/view.html).

**Note** For UWP, the **SocketDescriptions** section has moved out of the appxmanifest and into its own [networkmanifest.xml](https://forums.xboxlive.com/storage/attachments/410-networkmanifestxml.txt). The format inside the &lt;SocketDescriptions&gt; element is virtually identical, just without the **mx:** prefix.

For cross-play between Xbox and Windows 10, be *sure* that everything is defined *identically* between the two different kinds of manifests (Package.appxmanifest for Xbox One and networkmanifest.xml for Windows 10). The socket name, protocol, etc. must match *exactly*.

Also for cross-play, you will need to define the following four SDA usages inside the ```<AllowedUsages>``` element in *both* the Xbox One Package.appxmanifest and the Windows 10 networkmanifest.xml:

```xml
<SecureDeviceAssociationUsage Type="InitiateFromMicrosoftConsole" />
<SecureDeviceAssociationUsage Type="AcceptOnMicrosoftConsole" />
<SecureDeviceAssociationUsage Type="InitiateFromWindowsDesktop" />
<SecureDeviceAssociationUsage Type="AcceptOnWindowsDesktop" />
```

### Multiplayer QoS measurements

In addition to the namespace change in the Secure Sockets API, some of the object names and values have changed, too. The mapping for the typically-used measurement status is found in the following table.

Table 2. Typically used measurement status mapping.

| XDK (Windows::Xbox::Networking::QualityOfServiceMeasurementStatus)  | UWP (Windows::Networking::XboxLive::XboxLiveQualityOfServiceMeasurementStatus)  |
|------------------------------------|--------------------------------------------|
| HostUnreachable                    | NoCompatibleNetworkPaths                   |
| MeasurementTimedOut                | TimedOut                                   |
| PartialResults                     | InProgressWithProvisionalResults           |
| Success                            | Succeeded                                  |

The steps involved in *measuring* QoS (quality of service) and *processing the results* are in principle the same when you compare the XDK and UWP versions of the API. However, due to the name changes and a few design changes, the resulting code looks different in some places.

To measure the QoS for the **XDK**, you created a collection of secure device addresses and a collection of metrics and passed these into the **MeasureQualityOfServiceAsync()** method.

To measure the QoS for **UWP**, you create a new **XboxLiveQualityOfServiceMeasurement()** object, call **Append()** to its **Metrics** and **DeviceAddresses** properties, and then call the object's **MeasureAsync()** method.

For example:

```cpp
auto qosMeasurement = ref new Windows::Networking::XboxLive::XboxLiveQualityOfServiceMeasurement();
qosMeasurement->Metrics->Append(Windows::Networking::XboxLive::XboxLiveQualityOfServiceMetric::AverageInboundBitsPerSecond);
qosMeasurement->Metrics->Append(Windows::Networking::XboxLive::XboxLiveQualityOfServiceMetric::AverageOutboundBitsPerSecond);
qosMeasurement->Metrics->Append(Windows::Networking::XboxLive::XboxLiveQualityOfServiceMetric::AverageLatencyInMilliseconds);
qosMeasurement->NumberOfProbesToAttempt = myDefaultQosProbeCount;
qosMeasurement->TimeoutInMilliseconds = myDefaultQosMeasurementTimeout;

// Add secure addresses for each session member
for (const auto& member : session->GetMembers())
{
    if (!member->IsCurrentUser)
    {
        auto sda = member->SecureDeviceAddressBase64;

        if (!sda->IsEmpty())
        {
qosMeasurement->DeviceAddresses->Append(Windows::Networking::XboxLive::XboxLiveDeviceAddress::CreateFromSnapshotBase64(sda));
        }
    }
}

if (qosMeasurement->DeviceAddresses->Size > 0)
{
    qosMeasurement->MeasureAsync();
}

```

For more examples, see the **MatchmakingSession::MeasureQualityOfService()** and **MatchmakingSession::ProcessQosMeasurements()** functions in the NetRumble sample.

### Writing game events

Sending game events that are configured in your title's Service Configuration has a different API in UWP. The Xbox Live SDK uses the **EventsService** and a property bag model.

For example:

```cpp
auto properties = ref new Windows::Foundation::Collections::PropertySet();
properties->Insert("RoundId", m_roundId);
properties->Insert("SectionId", safe_cast<Platform::Object^>(0));
properties->Insert("MultiplayerCorrelationId", m_multiplayerCorrelationId);
properties->Insert("GameplayModeId", safe_cast<Platform::Object^>(0));
properties->Insert("MatchTypeId", safe_cast<Platform::Object^>(0));
properties->Insert("DifficultyLevelId", safe_cast<Platform::Object^>(0));

auto measurements = ref new Windows::Foundation::Collections::PropertySet();

xblContext->EventsService->WriteInGameEvent("MultiplayerRoundStart", properties, measurements);

```

For more information, see the Xbox Live SDK documentation.

**Tip** You can use the **xcetool.exe** provided with the Xbox Live SDK (located in the Tools directory) to convert the events.man file that you downloaded from XDP into a .h header file. Use the '-x' option to generate this C++ header by using the new v2 property bag schema. This header contains C++ functions that you can call for all of your configured events; for example, **EventWriteMultiplayerRoundStart()**. If you prefer to use a WinRT interface, you can still refer to this header file to see how the properties and measurements are constructed for each of your events.

### Game chat

GameChat in UWP is included with the Xbox Live SDK as a NuGet package binary. See instructions in the Xbox Live Programming Guide for how to add this NuGet package to your project.

Basic usage is virtually identical between the XDK and the UWP versions. A few differences in the API include:

1.  The **User::AudioDeviceAdded** event does not need to be hooked up by a UWP title. The underlying chat library handles device adds and removes.

2.  **ChatUser** is now called **GameChatUser**.

3.  **Microsoft::Xbox::GameChat** namespace remains the same, but the **Windows::Xbox::Chat** namespace has become **Microsoft::Xbox::ChatAudio**.

4.  **AddLocalUserToChatChannelAsync()** takes either a XUID or a **ChatAudio::IChatUser^** instead of an **XboxUser**.

5.  **RemoveLocalUserFromChatChannelAsync()** requires a **ChatAudio::IChatUser^** instead of an **XboxUser**. You can get an **IChatUser** from a **GameChatUser**-&gt;**User**.

### Connected storage

The Connected Storage API is provided in the separate [Xbox Live Platform Extensions SDK](https://developer.xboxlive.com/en-us/live/development/Pages/Downloads.aspx). Documentation is included in the Xbox Live SDK docs.

The overall flow is the same as on Xbox One, with the addition of the **ContainersChangedSinceLastSync** property in the UWP version. This property should be checked when your title handles a resume event, after calling **GetForUserAsync()** again, to see what containers changed in the cloud while your title was suspended. If you have data loaded in memory from one of the containers that changed, you probably want to read in the data again to see what changed and handle the changes accordingly.

Other notable differences in the UWP version include:

1.  Namespace change from **Windows::Xbox::Storage** to **Windows::Gaming::XboxLive::Storage**.

2.  **ConnectedStorageSpace** is renamed **GameSaveProvider**.

3.  **Windows::System::User** is used in **GetForUserAsync()** instead of an **XboxUser**, and the SCID is now required.

4.  No local "machine" storage (that is, **GetForMachineAsync()** has been removed). Consider using **Windows::Storage::ApplicationData** instead for your non-roaming, local save data.

5.  Async results are returned in an exception-free \*Result-type object (for example, **GameSaveProviderGetResult**); from this you can check the **Status** property, and if there is no error, read the returned object from the **Value** property.

6.  **ConnectedStorageErrorStatus enum** is renamed **GameSaveErrorStatus** and is returned in the **Status** property of a Result. All the old values exist, and a few new ones have been added:

-   Abort

-   ObjectExpired

-   Ok

-   UserHasNoXboxLiveInfo

Refer to the GameSave sample or the NetRumble sample for example usage.

**Note** Gamesaveutil.exe is the equivalent to xbstorage.exe (the command-line developer utility included with the XDK). After installing the Xbox Live Platform Extensions SDK, this utility can be found here: C:\\Program Files (x86)\\Windows Kits\\10\\Extension SDKs\\XboxLive\\1.0\\Bin\\x64

## Summary

The API changes and new requirements outlined in this white paper are ones that you are likely to encounter when porting existing game code from the Xbox One XDK to the new UWP. Particular emphasis has been given to application and environment setup, as well as feature areas related to Xbox Live services, such as multiplayer and connected storage. For more information, follow the links provided throughout this article and in the following references, and be sure to visit the “Windows 10” section of the [developer forums](https://forums.xboxlive.com) for more help, answers, and news.

## References

-   [Porting from Xbox One to Windows 10](https://developer.xboxlive.com/en-us/platform/development/education/Documents/Porting%20from%20Xbox%20One%20to%20Windows%2010.aspx)

-   [Xbox One White Papers](https://developer.xboxlive.com/en-us/platform/development/education/Pages/WhitePapers.aspx)

-   [Samples](https://developer.xboxlive.com/en-us/platform/development/education/Pages/Samples.aspx)
