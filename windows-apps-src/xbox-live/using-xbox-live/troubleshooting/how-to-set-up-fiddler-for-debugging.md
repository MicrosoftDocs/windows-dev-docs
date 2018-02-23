---
title: Troubleshooting Xbox Live using Fiddler
author: KevinAsgari
description: Learn how to use Fiddler to troubleshoot Xbox Live service calls.
ms.assetid: 7d76e444-027b-4659-80d5-5b2bf56d199e
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, fiddler, service calls, troubleshoot
ms.localizationpriority: low
---

# Troubleshooting Xbox Live using Fiddler

Fiddler is a web debugging proxy which logs all HTTP and HTTPS traffic between your device and the Internet. You will use it to log and inspect traffic to and from the Xbox Live services and relying party web services, to understand and debug web service calls.

## For Windows UWP PC apps

1. Make sure that the current user is in the administrator group on the PC
1. Download Fiddler from [http://www.telerik.com/fiddler](http://www.telerik.com/fiddler)
1. Make sure that you select the version that is “Built for .NET 4”
1. Once installed, go to Tools->Fiddler Options and enable Capture HTTPS CONNECTs and Decrypt HTTPS traffic.  All communications between the runtime and Xbox LIVE services are encrypted with SSL.  Without this option you won’t see anything useful.  Accept all dialogs Fiddler pops up (should be 5 dialogs including UAC)
1. Go to “WinConfig”, “Exempt All”, and “Save Changes”.  Otherwise Fiddler won’t work with Store apps.
1. Now if you run your app you should see the requests/responses between the app, runtime, and Xbox LIVE.

To debug UWP OS level calls which isn't usually needed but can be helpful when debugging sign-in and in-game events, you need to configure Fiddler to capture WinHTTP traffic.
This can be done with:
```cpp
    netsh winhttp set proxy 127.0.0.1:8888 "<-loopback>"
```
where the port 8888 matches the port you've configured on Fiddlers Tools | Options | Connections tab which by default is 8888.
To undo this, do:
```cpp
    netsh winhttp reset proxy
```

## For Xbox One UWP based projects

Follow the steps here [https://docs.microsoft.com/en-us/windows/uwp/xbox-apps/uwp-fiddler](https://docs.microsoft.com/en-us/windows/uwp/xbox-apps/uwp-fiddler)

## For Xbox One XDK based projects

In normal operation, a console that communicates through a proxy is at risk of having its communications modified by the proxy, possibly allowing players to cheat. Thus, consoles are designed not to allow communication through a proxy. Using Fiddler with your Xbox One dev kit requires that you perform some special configuration steps on the dev kit to allow it to use the Fiddler proxy.

Fiddler is freeware, and can be downloaded from the [Fiddler website](http://www.telerik.com/fiddler/).

Fiddler can impact the network status reported by the console. If an upstream connection is disabled from the machine running Fiddler, the console may not detect this disconnection until the authentication of the console has expired. If you are using Fiddler, be sure to disconnect the connection between the console and the computer running Fiddler, rather than using Fiddler to simulate a disconnect. Better still, use the network stress tools so simulate disconnection for testing purposes.

Installing and enabling Fiddler on the development PC

Follow these steps to install and enable Fiddler to monitor traffic from your dev kit.

1. Install Fiddler on your development PC, following the directions on the [Fiddler website](http://www.telerik.com/fiddler/).
1. Launch Fiddler and select Fiddler Options from the Tools menu.
1. Select the Connections tab, and ensure that the Allow remote computers to connect checkbox is checked.
1. Click OK to accept your change to the settings. You will see a dialog box saying that Fiddler must be restarted for the change to take effect, and that you may need to configure your firewall manually. Click OK on this dialog, but do not restart Fiddler yet.
1. Configure the necessary firewall rule to allow remote computers to connect. Start the Windows Firewall Control Panel applet. Click Advanced Settings, and then Inbound Rule. Find the rule named "FiddlerProxy", and scroll to the right, verifying that each of the following settings appears for that rule.

| Setting          | Preferred Value                |
|------------------|--------------------------------|
| Name             | FiddlerProxy                   |
| Group            | (do not set a value for Group) |
| Profile          | All                            |
| Enabled          | Yes                            |
| Action           | Allow                          |
| Override         | No                             |
| Program          | path to fiddler.exe            |
| LocalAddress     | Any                            |
| RemoteAddress    | Any                            |
| Protocol         | TCP                            |
| LocalPort        | Any                            |
| RemotePort       | Any                            |
| AllowedUsers     | Any                            |
| AllowedComputers | Any                            |


1. Configure Fiddler to capture and decrypt HTTPS traffic.
	1. To enable best performance, set Fiddler to use Streaming Mode by clicking the Stream button on the button bar.
	1. In Fiddler, select Tools, then Fiddler Options, then HTTPS.
	1. Check the Decrypt HTTPS traffic checkbox. If a messagebox asks whether to configure Windows to trust the CA certificate, click No.
	1. Click the Export Root Certificate to Desktop button.
1. Exit Fiddler and start it again.

### To configure a dev kit to use Fiddler as its proxy to the Internet
Fiddler configuration on the dev kit has been simplified from the method used in previous releases.

1. Copy the Fiddler root certificate that you exported to the desktop, to the dev kit as``` xs:\Microsoft\Cert\FiddlerRoot.cer```.  You can use the following command:  ```xbcp [local Fiddler Root directory]\FiddlerRoot.cer xs:\Microsoft\Cert\FiddlerRoot.cer```
1. Create a text file named ```ProxyAddress.txt```, with the IP address or hostname of the development PC running Fiddler and the port number where Fiddler is listening as the only content in the file. Format the name/IP address and port as follows: "HOST:PORT". (By default, Fiddler uses port 8888.) For example, "10.124.220.250:8888" or "my_dev_pc.contoso.com:8888". Copy this file to the dev kit as xs:\Microsoft\Fiddler\ProxyAddress.txt.  You can use the following command:  ```xbcp [local Proxy Address file directory]\ProxyAddress.txt xs:\Microsoft\Fiddler\ProxyAddress.txt```
1. Reboot the dev kit by typing ```xbreboot``` into the command prompt

### To stop using Fiddler

To stop using Fiddler as a proxy to the Internet, and thus tracing all of the dev kit's network traffic in Fiddler, simply rename or delete the FiddlerRoot.cer file on the dev kit, and then reboot the dev kit.

### How it works

The presence of both a xs:\Microsoft\Cert\FiddlerRoot.cer file and a xs:\Microsoft\Fiddler\ProxyAddress.txt file at boot time causes the dev kit to configure itself to use the proxy server specified in ProxyAddress.txt. If either file is missing, then the dev kit will not configure itself to operate through a Fiddler proxy.

Each PC with Fiddler installed uses a different Fiddler root certificate. If you have more than one PC that might be used to provide a Fiddler proxy for your dev kit, you can copy each PC's Fiddler root certificate into the xs:\Microsoft\Cert directory, as long as one of them is named FiddlerRoot.cer. All of the certificates in the Cert directory are checked when a Fiddler proxy is being authenticated. The Fiddler instance on whichever PC's address is in ProxyAddress.txt will be used as a proxy, as long as its certificate is present in the Cert directory.
