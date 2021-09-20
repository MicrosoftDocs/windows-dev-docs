---
ms.assetid: 
description: Learn how to manage game broadcasting for a Universal Windows Platform (UWP) app using the Windows system UI and the Windows Desktop Extensions for UWP.
title: Manage game broadcasting
ms.date: 09/27/2017
ms.topic: article
keywords: windows 10, game, broadcasting
ms.localizationpriority: medium
---
# Manage game broadcasting
This article shows you how to manage game broadcasting for a UWP app. Users must initiate broadcasting by using the system UI that is built into Windows, but starting with Windows 10, version 1709, apps can launch the system broadcasting UI and can receive notifications when broadcasting starts and stops.

## Add the Windows Desktop Extensions for the UWP to your app
The APIs for managing app broadcasting, found in the **[Windows.Media.AppBroadcasting](/uwp/api/windows.media.appbroadcasting)** namespace, are not included in the Universal API contract. To access the APIs, you must add a reference to the Windows Desktop Extensions for the UWP to your app with the following steps.

1. In Visual Studio, in **Solution Explorer**, expand your UWP project and right-click **References** and then select **Add Reference...**. 
2. Expand the **Universal Windows** node and select **Extensions**.
3. In the list of extensions check the checkbox next to the **Windows Desktop Extensions for the UWP** entry that matches the target build for your project. For the app broadcast features, the version must be 1709 or greater.
4. Click **OK**.

## Launch the system UI to allow the user to initiate broadcasting
There are several reasons that your app may not currently be able to broadcast, including if the current device doesn't meet the hardware requirements for broadcasting or if another app is currently broadcasting. Before launching the system UI, you can check to see if your app is currently able to broadcast. First, check to see if the broadcast APIs are available on the current device. The APIs are not available on devices running an OS version earlier than Windows 10, version 1709. Rather than check for a specific OS version, use the **[ApiInformation.IsApiContractPresent](/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent)** method to query for the *Windows.Media.AppBroadcasting.AppBroadcastingContract* version 1.0. If this contract is present, then the broadcasting APIs are available on the device.

Next, get an instance of the **[AppBroadcastingUI](/uwp/api/windows.media.appbroadcasting.appbroadcastingui)** class by calling the factory method **[GetDefault](/uwp/api/windows.media.appbroadcasting.appbroadcastingui.GetDefault)** on PC, where there is a single user signed in at a time. On Xbox, where multiple users can be signed in, call **[GetForUser](/uwp/api/windows.media.appbroadcasting.appbroadcastingui.getforuser)** instead. Then call **[GetStatus](/uwp/api/windows.media.appbroadcasting.appbroadcastingui.GetStatus)** to get the broadcasting status of your app.

The **[CanStartBroadcast](/uwp/api/windows.media.appbroadcasting.appbroadcastingstatus.CanStartBroadcast)** property of the **AppBroadcastingStatus** class tells you whether the app can currently start broadcasting. If not, you can check the **[Details](/uwp/api/windows.media.appbroadcasting.appbroadcastingstatus.Details)** property to determine the reason broadcasting is not available. Depending on the reason, you may want to display the status to the user or show instructions for enabling broadcasting.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppBroadcast/cpp/AppBroadcastExampleApp/App.cpp" id="SnippetCanStartBroadcast":::

Request that the app broadcast UI be shown by the system by calling **[ShowBroadcastUI](/uwp/api/windows.media.appbroadcasting.appbroadcastingui.ShowBroadcastUI)**.

> [!NOTE] 
> The **ShowBroadcastUI** method represents a request that may not succeed, depending on the current state of the system. Your app should not assume that broadcasting has begun after calling this method. Use the **[IsCurrentAppBroadcastingChanged](/uwp/api/windows.media.appbroadcasting.appbroadcastingmonitor.IsCurrentAppBroadcastingChanged)** event to be notified when broadcasting starts or stops.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppBroadcast/cpp/AppBroadcastExampleApp/App.cpp" id="SnippetLaunchBroadcastUI":::

## Receive notifications when broadcasting starts and stops
Register to receive notifications when the user uses the system UI to start or stop broadcasting your app by initializing an instance of **[AppBroadcastingMonitor](/uwp/api/windows.media.appbroadcasting.appbroadcastingmonitor)** class and registering a handler for the  **[IsCurrentAppBroadcastingChanged](/uwp/api/windows.media.appbroadcasting.appbroadcastingmonitor.IsCurrentAppBroadcastingChanged)** event. As discussed in the previous section, be sure to use the **[ApiInformation.IsApiContractPresent](/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent)** at some point to verify that the broadcasting APIs are present on the device before attempting to use them. 

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppBroadcast/cpp/AppBroadcastExampleApp/App.cpp" id="SnippetAppBroadcastingRegisterChangedHandler":::

In the handler for the **IsCurrentAppBroadcastingChanged** event, you may want to update your app's UI to reflect the current broadcasting state.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppBroadcast/cpp/AppBroadcastExampleApp/App.cpp" id="SnippetAppBroadcastingChangedHandler":::

## Related topics

* [Gaming](index.md)

 

 
