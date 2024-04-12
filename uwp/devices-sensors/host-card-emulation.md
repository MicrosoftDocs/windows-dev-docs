---
title: Create an NFC Smart Card app
description: This topic describes how to use Host Card Emulation (HCE) to communicate directly with a near-field communication (NFC) card reader and let your customers access your services through their phone (instead of a physical card) without a mobile-network operator (MNO).
ms.date: 06/13/2023
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Create an NFC Smart Card app

> [!IMPORTANT]
> This topic applies to Windows 10 Mobile only.

This topic describes how to use Host Card Emulation (HCE) to communicate directly with a near-field communication (NFC) card reader and let your customers access your services through their phone (instead of a physical card) without a mobile-network operator (MNO).

## What you need to develop an HCE app

To develop an HCE-based card emulation app, you need to install Microsoft Visual Studio 2015 (see the [Visual Studio download page](https://visualstudio.microsoft.com/vs/older-downloads/)) (includes the Windows developer tools) and the [Windows 10 Mobile emulator](https://www.microsoft.com/download/details.aspx?id=53424).

> For more information about getting setup, see [Test with the Microsoft Emulator for Windows 10 Mobile](../debug-test-perf/test-with-the-emulator.md).

Optionally, if you want to test with a real Windows 10 Mobile device instead of the included Windows 10 Mobile emulator, you will also need the following items.

- A Windows 10 Mobile device with NFC HCE support.
- A reader terminal that supports protocols ISO/IEC 14443-4 and ISO/IEC 7816-4

Windows 10 Mobile implements an HCE service that provides the following functionalities.

- Apps can register the applet identifiers (AIDs) for the cards they would like to emulate.
- Conflict resolution and routing of the Application Protocol Data Unit (APDU) command and response pairs to one of the registered apps based on the external reader card selection and user preference.
- Handling of events and notifications to the apps as a result of user actions.

Windows 10 supports emulation of smart cards that are based on ISO-DEP (ISO-IEC 14443-4) and communicates using APDUs as defined in the ISO-IEC 7816-4 specification. Windows 10 supports ISO/IEC 14443-4 Type A technology for HCE apps. Type B, type F, and non-ISO-DEP (eg MIFARE) technologies are routed to the SIM by default.

Only Windows 10 Mobile devices are enabled with the card emulation feature. SIM-based and HCE-based card emulation is not available on other versions of Windows 10.

The architecture for HCE and SIM based card emulation support is shown in the diagram below.

![Architecture for HCE and SIM card emulation](./images/nfc-architecture.png)

## App selection and AID routing

To develop an HCE app, you must understand how Windows 10 Mobile devices route AIDs to a specific app because users can install multiple different HCE apps. Each app can register multiple HCE and SIM-based cards.

When the user taps their Windows 10 Mobile device to a terminal, the data is automatically routed to the proper app installed on the device. This routing is based on the applet ID (AID) which is a 5-16 byte identifier. During a tap, the external terminal will transmit a SELECT command APDU to specify the AID it would like all subsequent APDU commands to be routed to. Subsequent SELECT commands, will change the routing again. Based on the AIDs registered by apps and user settings, the APDU traffic is routed to a specific app, which will send a response APDU. Be aware that a terminal may want to communicate with several different apps during the same tap. So you must ensure your app's background task exits as quickly as possible when deactivated to make room for another app's background task to respond to the APDU. We will discuss background tasks later in this topic.

HCE apps must register themselves with particular AIDs they can handle so they will receive APDUs for an AID. Apps declare AIDs by using AID groups. An AID group is conceptually equivalent to an individual physical card. For example, one credit card is declared with an AID group and a second credit card from a different bank is declared with a different, second AID group, even though both of them may have the same AID.

## Conflict resolution for payment AID groups

When an app registers physical cards (AID groups), it can declare the AID group category as either "Payment" or "Other." While there can be multiple payment AID groups registered at any given time, only one of these payment AID groups may be enabled for Tap and Pay at a time, which is selected by the user. This behavior exists because the user expects be in control of consciously choosing a single payment, credit, or debit card to use so they don't pay with a different unintended card when tapping their device to a terminal.

However, multiple AID groups registered as "Other" can be enabled at the same time without user interaction. This behavior exists because other types of cards like loyalty, coupons, or transit are expected to just work without any effort or prompting whenever they tap their phone.

All the AID groups that are registered as "Payment" appear in the list of cards in the NFC Settings page, where the user can select their default payment card. When a default payment card is selected, the app that registered this payment AID group becomes the default payment app. Default payment apps can enable or disable any of their AID groups without user interaction. If the user declines the default payment app prompt, then the current default payment app (if any) continues to remain as default. The following screenshot shows the NFC Settings page.

![Screenshot of NFC settings page](./images/nfc-settings.png)

Using the example screenshot above, if the user changes his default payment card to another card that is not registered by "HCE Application 1," the system creates a confirmation prompt for the user's consent. However, if the user changes his default payment card to another card that is registered by "HCE Application 1", the system does not create a confirmation prompt for the user as "HCE Application1" is already the default payment app.

## Conflict resolution for non-payment AID groups

Non-payment cards categorized as "Other" do not appear in the NFC settings page.

Your app can create, register and enable non-payment AID groups in the same manner as payment AID groups. The main difference is that for non-payment AID groups the emulation category is set to "Other" as opposed to "Payment". After registering the AID group with the system, you need to enable the AID group to receive NFC traffic. When you try to enable a non-payment AID group to receive traffic, the user is not prompted for a confirmation unless there is a conflict with one of the AIDs already registered in the system by a different app. If there is a conflict, the user will be prompted with information about which card and it's associated app will be disabled if the user chooses to enable the newly registered AID group.

### Coexistence with SIM based NFC applications

In Windows 10 Mobile, the system sets up the NFC controller routing table that is used to make routing decisions at the controller layer. The table contains routing information for the following items.

- Individual AID routes.
- Protocol based route (ISO-DEP).
- Technology based routing (NFC-A/B/F).

When an external reader sends a "SELECT AID" command, the NFC controller first checks AID routes in the routing table for a match. If there is no match, it will use the protocol-based route as the default route for ISO-DEP (14443-4-A) traffic. For any other non-ISO-DEP traffic it will use the technology based routing.

Windows 10 Mobile provides a menu option "SIM Card" in the NFC Settings page to continue to use legacy Windows Phone 8.1 SIM-based apps, which do not register their AIDs with the system. If the user selects "SIM card" as their default payment card, then the ISO-DEP route is set to UICC, for all other selections in the drop-down menu the ISO-DEP route is to the host.

The ISO-DEP route is set to "SIM Card" for devices that have an SE enabled SIM card when the device is booted for the first time with Windows 10 Mobile. When the user installs an HCE enabled app and that app enables any HCE AID group registrations, the ISO-DEP route will be pointed to the host. New SIM-based applications need to register the AIDs in the SIM in order for the specific AID routes to be populated in the controller routing table.

## Creating an HCE based app

Your HCE app has two parts.

- The main foreground app for the user interaction.
- A background task that is triggered by the system to process APDUs for a given AID.

Because of the extremely tight performance requirements for loading your background task in response to an NFC tap, we recommend that your entire background task be implementing in C++/CX native code (including any dependencies, references, or libraries you depend on) rather than C# or managed code. While C# and managed code normally performs well, there is overhead, like loading the .NET CLR, that can be avoided by writing it in C++/CX.

## Create and register your background task

You need to create a background task in your HCE app for processing and responding to APDUs routed to it by the system. During the first time your app is launched, the foreground registers an HCE background task that implements the [**IBackgroundTaskRegistration**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTaskRegistration) interface as shown in the following code.

```cppcx
var taskBuilder = new BackgroundTaskBuilder();
taskBuilder.Name = bgTaskName;
taskBuilder.TaskEntryPoint = taskEntryPoint;
taskBuilder.SetTrigger(new SmartCardTrigger(SmartCardTriggerType.EmulatorHostApplicationActivated));
bgTask = taskBuilder.Register();
```

Notice that the task trigger is set to [**SmartCardTriggerType**](/uwp/api/Windows.Devices.SmartCards.SmartCardTriggerType). **EmulatorHostApplicationActivated**. This means that whenever a SELECT AID command APDU is received by the OS resolving to your app, your background task will be launched.

## Receive and respond to APDUs

When there is an APDU targeted for your app, the system will launch your background task. Your background task receives the APDU passed through the [**SmartCardEmulatorApduReceivedEventArgs**](/uwp/api/Windows.Devices.SmartCards.SmartCardEmulatorApduReceivedEventArgs) object's [**CommandApdu**](/uwp/api/windows.devices.smartcards.smartcardemulatorapdureceivedeventargs.commandapdu) property and responds to the APDU using the [**TryRespondAsync**](/uwp/api/windows.devices.smartcards.smartcardemulatorapdureceivedeventargs.tryrespondwithcryptogramsasync) method of the same object. Consider keeping your background task for light operations for performance reasons. For example, respond to the APDUs immediately and exit your background task when all processing is complete. Due to the nature of NFC transactions, users tend to hold their device against the reader for only a very short amount of time. Your background task will continue to receive traffic from the reader until your connection is deactivated, in which case you will receive a [**SmartCardEmulatorConnectionDeactivatedEventArgs**](/uwp/api/Windows.Devices.SmartCards.SmartCardEmulatorConnectionDeactivatedEventArgs) object. Your connection can be deactivated because of the following reasons as indicated in the [**SmartCardEmulatorConnectionDeactivatedEventArgs.Reason**](/uwp/api/windows.devices.smartcards.smartcardemulatorconnectiondeactivatedeventargs.reason) property.

- If the connection is deactivated with the **ConnectionLost** value, it means that the user pulled their device away from the reader. If your app needs the user to tap to the terminal longer, you might want to consider prompting them with feedback. You should terminate your background task quickly (by completing your deferral) to ensure if they tap again it won't be delayed waiting for the previous background task to exit.
- If the connection is deactivated with the **ConnectionRedirected**, it means that the terminal sent a new SELECT AID command APDU directed to a different AID. In this case, your app should exit the background task immediately (by completing your deferral) to allow another background task to run.

The background task should also register for the [**Canceled event**](/uwp/api/windows.applicationmodel.background.ibackgroundtaskinstance.canceled) on [**IBackgroundTaskInstance interface**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTaskInstance), and likewise quickly exit the background task (by completing your deferral) because this event is fired by the system when it is finished with your background task. Below is code that demonstrates an HCE app background task.

```cppcx
void BgTask::Run(
    IBackgroundTaskInstance^ taskInstance)
{
    m_triggerDetails = static_cast<SmartCardTriggerDetails^>(taskInstance->TriggerDetails);
    if (m_triggerDetails == nullptr)
    {
        // May be not a smart card event that triggered us
        return;
    }

    m_emulator = m_triggerDetails->Emulator;
    m_taskInstance = taskInstance;

    switch (m_triggerDetails->TriggerType)
    {
    case SmartCardTriggerType::EmulatorHostApplicationActivated:
        HandleHceActivation();
        break;

    case SmartCardTriggerType::EmulatorAppletIdGroupRegistrationChanged:
        HandleRegistrationChange();
        break;

    default:
        break;
    }
}

void BgTask::HandleHceActivation()
{
 try
 {
        auto lock = m_srwLock.LockShared();
        // Take a deferral to keep this background task alive even after this "Run" method returns
        // You must complete this deferal immediately after you have done processing the current transaction
        m_deferral = m_taskInstance->GetDeferral();

        DebugLog(L"*** HCE Activation Background Task Started ***");

        // Set up a handler for if the background task is cancelled, we must immediately complete our deferral
        m_taskInstance->Canceled += ref new Windows::ApplicationModel::Background::BackgroundTaskCanceledEventHandler(
            [this](
            IBackgroundTaskInstance^ sender,
            BackgroundTaskCancellationReason reason)
        {
            DebugLog(L"Cancelled");
            DebugLog(reason.ToString()->Data());
            EndTask();
        });

        if (Windows::Phone::System::SystemProtection::ScreenLocked)
        {
            auto denyIfLocked = Windows::Storage::ApplicationData::Current->RoamingSettings->Values->Lookup("DenyIfPhoneLocked");
            if (denyIfLocked != nullptr && (bool)denyIfLocked == true)
            {
                // The phone is locked, and our current user setting is to deny transactions while locked so let the user know
                // Denied
                DoLaunch(Denied, L"Phone was locked at the time of tap");

                // We still need to respond to APDUs in a timely manner, even though we will just return failure
                m_fDenyTransactions = true;
            }
        }
        else
        {
            m_fDenyTransactions = false;
        }

        m_emulator->ApduReceived += ref new TypedEventHandler<SmartCardEmulator^, SmartCardEmulatorApduReceivedEventArgs^>(
            this, &BgTask::ApduReceived);

        m_emulator->ConnectionDeactivated += ref new TypedEventHandler<SmartCardEmulator^, SmartCardEmulatorConnectionDeactivatedEventArgs^>(
                [this](
                SmartCardEmulator^ emulator,
                SmartCardEmulatorConnectionDeactivatedEventArgs^ eventArgs)
            {
                DebugLog(L"Connection deactivated");
                EndTask();
            });

  m_emulator->Start();
        DebugLog(L"Emulator started");
 }
 catch (Exception^ e)
 {
        DebugLog(("Exception in Run: " + e->ToString())->Data());
        EndTask();
 }
}
```

## Create and register AID groups

During the first launch of your application when the card is being provisioned, you will create and register AID groups with the system. The system determines the app that an external reader would like to talk to and route APDUs accordingly based on the registered AIDs and user settings.

Most of the payment cards register for the same AID, Proximity Payment System Environment (PPSE), along with additional payment network card specific AIDs. Each AID group represents a card and when the user enables the card, all AIDs in the group are enabled. Similarly, when the user deactivates the card, all AIDs in the group are disabled.

To register an AID group, you need to create a [**SmartCardAppletIdGroup**](/uwp/api/Windows.Devices.SmartCards.SmartCardAppletIdGroup) object and set its properties to reflect that this is an HCE-based payment card. Your display name should be descriptive to the user because it will show up in the NFC settings menu as well as user prompts. For HCE payment cards, the [**SmartCardEmulationCategory**](/uwp/api/windows.devices.smartcards.smartcardappletidgroup.smartcardemulationcategory) property should be set to **Payment** and the [**SmartCardEmulationType**](/uwp/api/windows.devices.smartcards.smartcardappletidgroup.smartcardemulationtype) property should be set to **Host**.

```cppcx
public static byte[] AID_PPSE =
        {
            // File name "2PAY.SYS.DDF01" (14 bytes)
            (byte)'2', (byte)'P', (byte)'A', (byte)'Y',
            (byte)'.', (byte)'S', (byte)'Y', (byte)'S',
            (byte)'.', (byte)'D', (byte)'D', (byte)'F', (byte)'0', (byte)'1'
        };

var appletIdGroup = new SmartCardAppletIdGroup(
                        "Example DisplayName",
                                new List<IBuffer> {AID_PPSE.AsBuffer()},
                                SmartCardEmulationCategory.Payment,
                                SmartCardEmulationType.Host);
```

For non-payment HCE cards, the [**SmartCardEmulationCategory**](/uwp/api/windows.devices.smartcards.smartcardappletidgroup.smartcardemulationcategory) property should be set to **Other** and the [**SmartCardEmulationType**](/uwp/api/windows.devices.smartcards.smartcardappletidgroup.smartcardemulationtype) property should be set to **Host**.

```cppcx
public static byte[] AID_OTHER =
        {
            (byte)'1', (byte)'2', (byte)'3', (byte)'4',
            (byte)'5', (byte)'6', (byte)'7', (byte)'8',
            (byte)'O', (byte)'T', (byte)'H', (byte)'E', (byte)'R'
        };

var appletIdGroup = new SmartCardAppletIdGroup(
                        "Example DisplayName",
                                new List<IBuffer> {AID_OTHER.AsBuffer()},
                                SmartCardEmulationCategory.Other,
                                SmartCardEmulationType.Host);
```

You can include up to 9 AIDs (of length 5-16 bytes each) per AID group.

Use the [**RegisterAppletIdGroupAsync**](/uwp/api/windows.devices.smartcards.smartcardemulator.registerappletidgroupasync) method to register your AID group with the system, which will return a [**SmartCardAppletIdGroupRegistration**](/uwp/api/windows.devices.smartcards.smartcardappletidgroupregistration) object. By default, the [**ActivationPolicy**](/uwp/api/windows.devices.smartcards.smartcardappletidgroupregistration) property of the registration object is set to **Disabled**. This means even though your AIDs are registered with the system, they are not enabled yet and won't receive traffic.

```cppcx
reg = await SmartCardEmulator.RegisterAppletIdGroupAsync(appletIdGroup);
```

You can enable your registered cards (AID groups) by using the [**RequestActivationPolicyChangeAsync**](/uwp/api/windows.devices.smartcards.smartcardappletidgroupregistration) method of the[**SmartCardAppletIdGroupRegistration**](/uwp/api/windows.devices.smartcards.smartcardappletidgroupregistration) class as shown below. Because only a single payment card can be enabled at a time on the system, setting the [**ActivationPolicy**](/uwp/api/windows.devices.smartcards.smartcardappletidgroupregistration) of a payment AID group to **Enabled** is the same as setting the default payment card. The user will be prompted to allow this card as a default payment card, regardless of whether there is a default payment card already selected or not. This statement is not true if your app is already the default payment application, and is merely changing between it's own AID groups. You can register up to 10 AID groups per app.

```cppcx
reg.RequestActivationPolicyChangeAsync(AppletIdGroupActivationPolicy.Enabled);
```

You can query your app's registered AID groups with the OS and check their activation policy using the [**GetAppletIdGroupRegistrationsAsync**](/uwp/api/windows.devices.smartcards.smartcardemulator.getappletidgroupregistrationsasync) method.

Users will be prompted when you change the activation policy of a payment card from **Disabled** to **Enabled**, only if your app is not already the default payment app. Users will only be prompted when you change the activation policy of a non-payment card from **Disabled** to **Enabled** if there is an AID conflict.

```cppcx
var registrations = await SmartCardEmulator.GetAppletIdGroupRegistrationsAsync();
    foreach (var registration in registrations)
    {
registration.RequestActivationPolicyChangeAsync (AppletIdGroupActivationPolicy.Enabled);
    }
```

### Event notification when activation policy change

In your background task, you can register to receive events for when the activation policy of one of your AID group registrations changes outside of your app. For example, the user may change the default payment app through the NFC settings menu from one of your cards to another card hosted by another app. If your app needs to know about this change for internal setup such as updating live tiles, you can receive event notifications for this change and take action in your app accordingly.

```cppcx
var taskBuilder = new BackgroundTaskBuilder();
taskBuilder.Name = bgTaskName;
taskBuilder.TaskEntryPoint = taskEntryPoint;
taskBuilder.SetTrigger(new SmartCardTrigger(SmartCardTriggerType.EmulatorAppletIdGroupRegistrationChanged));
bgTask = taskBuilder.Register();
```

## Foreground override behavior

You can change the [**ActivationPolicy**](/uwp/api/windows.devices.smartcards.smartcardappletidgroupregistration) of any of your AID group registrations to **ForegroundOverride** while your app is in the foreground without prompting the user. When the user taps their device to a terminal while your app is in the foreground, the traffic is routed to your app even if none of your payment cards were chosen by the user as their default payment card. When you change a card's activation policy to **ForegroundOverride**, this change is only temporary until your app leaves the foreground and it will not change the current default payment card set by the user. You can change the **ActivationPolicy** of your payment or non-payment cards from your foreground app as follows. Note that the [**RequestActivationPolicyChangeAsync**](/uwp/api/windows.devices.smartcards.smartcardappletidgroupregistration) method can only be called from a foreground app and cannot be called from a background task.

```cppcx
reg.RequestActivationPolicyChangeAsync(AppletIdGroupActivationPolicy.ForegroundOverride);
```

Also, you can register an AID group consisting of a single 0-length AID which will cause the system to route all APDUs regardless of the AID and including any command APDUs sent before a SELECT AID command is received. However, such an AID group only works while your app is in the foreground because it can only be set to **ForegroundOverride** and cannot be permanently enabled. Also, this mechanism works both for **Host** and **UICC** values of the [**SmartCardEmulationType**](/uwp/api/Windows.Devices.SmartCards.SmartCardEmulationType) enumeration to either route all traffic to your HCE background task, or to the SIM card.

```cppcx
public static byte[] AID_Foreground =
        {};

var appletIdGroup = new SmartCardAppletIdGroup(
                        "Example DisplayName",
                                new List<IBuffer> {AID_Foreground.AsBuffer()},
                                SmartCardEmulationCategory.Other,
                                SmartCardEmulationType.Host);
reg = await SmartCardEmulator.RegisterAppletIdGroupAsync(appletIdGroup);
reg.RequestActivationPolicyChangeAsync(AppletIdGroupActivationPolicy.ForegroundOverride);
```

## Check for NFC and HCE support

Your app should check whether a device has NFC hardware, supports the card emulation feature, and supports host card emulation prior to offering such features to the user.

The NFC smart card emulation feature is only enabled on Windows 10 Mobile, so trying to use the smart card emulator APIs in any other versions of Windows 10, will cause errors. You can check for smart card API support in the following code snippet.

```cppcx
Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Devices.SmartCards.SmartCardEmulator");
```

You can additionally check to see if the device has NFC hardware capable of some form of card emulation by checking if the [**SmartCardEmulator.GetDefaultAsync**](/uwp/api/windows.devices.smartcards.smartcardemulator.getdefaultasync) method returns null. If it does, then no NFC card emulation is supported on the device.

```cppcx
var smartcardemulator = await SmartCardEmulator.GetDefaultAsync();<
```

Support for HCE and AID-based UICC routing is only available on recently launched devices such as the Lumia 730, 830, 640, and 640 XL. Any new NFC capable devices running Windows 10 Mobile and after should support HCE. Your app can check for HCE support as follows.

```cppcx
Smartcardemulator.IsHostCardEmulationSupported();
```

## Lock screen and screen off behavior

Windows 10 Mobile has device-level card emulation settings, which can be set by the mobile operator or the manufacturer of the device. By default, "tap to pay" toggle is disabled and the "enablement policy at device level" is set to "Always", unless the MO or OEM overwrites these values.

Your application can query the value of the [**EnablementPolicy**](/uwp/api/Windows.Devices.SmartCards.SmartCardEmulatorEnablementPolicy) at device level and take action for each case depending on the desired behavior of your app in each state.

```cppcx
SmartCardEmulator emulator = await SmartCardEmulator.GetDefaultAsync();

switch (emulator.EnablementPolicy)
{
case Never:
// you can take the user to the NFC settings to turn "tap and pay" on
await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-nfctransactions:"));
break;

 case Always:
return "Card emulation always on";

 case ScreenOn:
 return "Card emulation on only when screen is on";

 case ScreenUnlocked:
 return "Card emulation on only when screen unlocked";
}
```

Your app's background task will be launched even if the phone is locked and/or the screen is off only if the external reader selects an AID that resolves to your app. You can respond to the commands from the reader in your background task, but if you need any input from the user or if you want to show a message to the user, you can launch your foreground app with some arguments. Your background task can launch your foreground app with the following behavior.

- Under the device lock screen (the user will see your foreground app only after she unlocks the device)
- Above the device lock screen (after the user dismisses your app, the device is still in locked state)

```cppcx
        if (Windows::Phone::System::SystemProtection::ScreenLocked)
        {
            // Launch above the lock with some arguments
            var result = await eventDetails.TryLaunchSelfAsync("app-specific arguments", SmartCardLaunchBehavior.AboveLock);
        }
```

## AID registration and other updates for SIM based apps

Card emulation apps that use the SIM as the secure element can register with the Windows service to declare the AIDs supported on the SIM. This registration is very similar to an HCE-based app registration. The only difference is the [**SmartCardEmulationType**](/uwp/api/Windows.Devices.SmartCards.SmartCardEmulationType), which should be set to Uicc for SIM-based apps. As the result of the payment card registration, the display name of the card will also be populated in the NFC setting menu.

```cppcx
var appletIdGroup = new SmartCardAppletIdGroup(
                        "Example DisplayName",
                                new List<IBuffer> {AID_PPSE.AsBuffer()},
                                SmartCardEmulationCategory.Payment,
                                SmartCardEmulationType.Uicc);
```
