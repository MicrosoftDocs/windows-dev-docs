---
title: How to use Fiddler with Xbox One when developing for UWP
description: Describes how to use the freeware Fiddler tool to see network traffic on a UWP Xbox One dev kit.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 9c133c77-fe9d-4b81-b4b3-462936333aa3
ms.localizationpriority: medium
---
# How to use Fiddler with Xbox One when developing for UWP

Fiddler is a web debugging proxy which logs all HTTP and HTTPS traffic between your Xbox One dev kit and the Internet. You will use it to log and inspect traffic to and from the Xbox services and relying party web services, to understand and debug web service calls. 

In normal operation, a console that communicates through a proxy is at risk of having its communications modified by the proxy, possibly allowing players to cheat. Thus, consoles are designed to not allow communication through a proxy. Using Fiddler with your Xbox One dev kit requires that you perform some special configuration steps on the dev kit to allow it to use the Fiddler proxy. 

Fiddler is freeware, and can be downloaded from the [Fiddler website](https://www.telerik.com/download/fiddler). 

Fiddler can impact the network status reported by the console. If an upstream connection is disabled from the machine running Fiddler, the console may not detect this disconnection until the authentication of the console has expired. If you are using Fiddler, be sure to disconnect the connection between the console and the computer running Fiddler, rather than using Fiddler to simulate a disconnect.

### To install and enable Fiddler on your development PC
Follow these steps to install and enable Fiddler to monitor traffic from your dev kit:

1. Install Fiddler on your development PC, following the directions on the [Fiddler website](https://www.telerik.com/download/fiddler). 
2. Launch Fiddler and select **Fiddler Options** from the **Tools** menu. 
3. Select the **Connections** tab and ensure that **Allow remote computers to connect** is selected. 
4. Click **OK** to accept your change to the settings. You will see a dialog box saying that Fiddler must be restarted for the change to take effect, and that you may need to configure your firewall manually. Click **OK** on this dialog, but *do not restart Fiddler yet*.
5. Configure the necessary firewall rule to allow remote computers to connect. Start the Windows Firewall Control Panel applet. Click **Advanced Settings**, and then **Inbound Rule**. Find the rule named "FiddlerProxy" and scroll to the right, verifying that each of the settings in the following table appears for that rule.
  
  | Setting           | Preferred Value                |
  | ----              | ----                           |
  | Name              | FiddlerProxy                   |
  | Group             | *No value* |
  | Profile           | All                            |
  | Enabled           | Yes                            |
  | Action            | Allow                          |
  | Override          | No                             |
  | Program           | *Path to fiddler.exe*          |
  | LocalAddress      | Any                            |
  | RemoteAddress     | Any                            |
  | Protocol          | TCP                            |
  | LocalPort         | Any                            |
  | RemotePort        | Any                            |
  | AllowedUsers      | Any                            |
  | AllowedComputers  | Any                            |


6. Configure Fiddler to capture and decrypt HTTPS traffic by doing the following:
  1. To enable best performance, set Fiddler to use Streaming Mode by clicking the **Stream** button on the button bar.
  2. In the Fiddler **Tools** menu, select **Fiddler Options**, and then click **HTTPS**.
  3. Select the **Decrypt HTTPS traffic** check box. If a dialog box asks whether to configure Windows to trust the CA certificate, click **No**.
  4. Click **Export Root Certificate to Desktop**.
7. Exit and restart Fiddler.

### To configure a dev kit to use Fiddler as its proxy to the Internet

1. Navigate to the **Network** tool in the Xbox Device Portal UI.
2. Browse for the Fiddler root certificate that you exported to the desktop. 
3. Type the IP address or hostname of the development PC running Fiddler.
4. Type the port number where Fiddler is listening (by default, Fiddler uses port 8888). 
5. Click **Enable**. This will restart your dev kit.

### To stop using Fiddler
To stop using Fiddler as a proxy to the Internet (and stop Fiddler from tracing all of the dev kit's network traffic), do the following:

1. Navigate to the **Network** tool in the Xbox Device Portal UI.
2. Click **Disable**.

> [!NOTE]
> Each PC with Fiddler installed uses a different Fiddler root certificate. If you have more than one PC that might be used to provide a Fiddler proxy for your dev kit, you will need to select the new root certificate when switching between them. If you are using only one PC, you need to select the root certificate only the first time you enable Fiddler. You must still specify the IP address and port.

## See also
- [Fiddler settings API reference](wdp-fiddler-api.md)
- [Frequently asked questions](frequently-asked-questions.md)
- [UWP on Xbox One](index.md)



