---
description: Many enterprises use firewalls to block unwanted traffic. This doc describes how to allow WNS traffic to pass through firewalls.
title: Adding WNS Traffic to the Firewall Allowlist
ms.assetid: 2125B09F-DB90-4515-9AA6-516C7E9ACCCD
template: detail.hbs
ms.date: 06/30/2021
ms.topic: article
keywords: windows 10, uwp, WNS, windows notifications service, notification, windows, firewall, troubleshooting, IP, traffic, enterprise, network, IPv4, VIP, FQDN, public IP address
ms.localizationpriority: medium
---
# Enterprise Firewall Configurations to Support WNS Traffic

## Background
Many enterprises use firewalls to block unwanted network traffic and ports; unfortunately, this can also block important things like Windows Notification Service communications. This means all notifications sent through WNS will be dropped under certain network configurations. To avoid this, network admins can add the list of approved WNS FQDNs or VIPs to their exemption list to allow the WNS traffic to pass through the firewall.

## Proxy Support

> [!Note]
> WNS Push Notifications *does not* support proxies. For best results, the connection to WNS must be a direct connection, however, VPN interfaces can be used. 

We welcome any feedback about proxy support. Please direct your feedback to [https://aka.ms/windowsappsdk](https://aka.ms/windowsappsdk) and file an issue for tracking interest in proxy support for WNS. Feel free to add the "area-Notifications" label to your issue for quicker visibility with the notifications team.


## What information should be added to the allowlist
Below is a list that contains the FQDNs, VIPs, and IP address ranges used by the Windows Notification Service. 

> [!IMPORTANT]
> We strongly suggest that you allow list by FQDN, because these will not change. If you allow list by FQDN, you do not need to also allow the IP address ranges.

> [!IMPORTANT]
> The IP address ranges will change periodically; because of this, they are not included on this page. If you want to see the list of IP ranges, you can download the file from Download Center: [Windows Notification Service (WNS) VIP and IP Ranges](https://www.microsoft.com/download/details.aspx?id=44238). Please check back regularly to make sure you have the most up-to-date information. 


### FQDNs, VIPs, IPs, and Ports
Regardless of the method you choose from below, you'll need to allow network traffic to the listed destinations through **port 443**. Each of the elements in the following XML document is explained in the table that follows it (in [Terms and Notations](#terms-and-notations)). The IP ranges were intentionally left out of this document to encourage you to use only the FQDNs as the FQDNs will remain constant. However, you can download the XML file containing the complete list from Download Center: [Windows Notification Service (WNS) VIP and IP Ranges](https://www.microsoft.com/download/details.aspx?id=44238). New VIPs or IP ranges will be **effective one week after they are uploaded**.

```XML
<?xml version="1.0" encoding="UTF-8"?>
<WNSPublicIpAddresses xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <!-- This file contains the FQDNs, VIPs, and IP address ranges used by the Windows Notification Service. A new text file will be uploaded every time a new VIP or IP range is released in production.  Please copy the below information and perform the necessary changes on your site. Endpoints in CloudService nodes are used for cloud services to send notifications to WNS. Endpoints in Client nodes are used by devices to receive notifications from WNS. --> 
    <CloudServiceDNS>
    <DNS FQDN="*.notify.windows.com"/>
    </CloudServiceDNS>
    <ClientDNS>
        <DNS FQDN="*.wns.windows.com"/>
        <DNS FQDN="*.notify.live.net"/>
    </ClientDNS>
    <CloudServiceIPs>
        <IpRange Subnet=""/>
        <!-- See the file in Download Center for the complete list of IP ranges -->
    </CloudServiceIPs>
    <ClientIPsIPv4>
        <IpRange Subnet=""/>
        <!-- See the file in Download Center for the complete list of IP ranges -->
    </ClientIPsIPv4>
    <IdentityServiceDNS>
        <DNS FQDN="login.microsoftonline.com"/>
        <DNS FQDN="login.live.com"/>
    </IdentityServiceDNS>
</WNSPublicIpAddresses>

```

### Terms and notations
Below are explanations on the notations and elements used in the above XML snippet.

| Term | Explanation |
|---|---|
| **Dot-decimal notation (i.e. 64.4.28.0/26)** | Dot-decimal notation is a way to describe the range of IP addresses. For example, 64.4.28.0/26 means the first 26 bits of 64.4.28.0 are constant, while the last 6 bits are variable.  In this case, the IPv4 range is 64.4.28.0 - 64.4.28.63. |
| **ClientDNS** | These are the Fully-Qualified Domain Name (FQDN) filters for the client devices (Windows PCs, desktops) receiving notifications from WNS. These must be allowed through the firewall in order for WNS clients to use the WNS Functionality.  It is recommended to allow-list by the FQDNs instead of the IP/VIP ranges, since these will never change. |
| **ClientIPsIPv4** | These are the IPv4 addresses of the servers accessed by client devices (Windows PCs, desktops) receiving notifications from WNS. |
| **CloudServiceDNS** | These are the Fully-Qualified Domain Name (FQDN) filters for the WNS servers your cloud service will talk to send notifications to WNS. These must be allowed through the firewall in order for services to send WNS notifications.  It is recommended to allow-list by the FQDNs instead of the IP/VIP ranges, since these will never change.|
| **CloudServiceIPs** | CloudServiceIPs are the IPv4 addresses of the servers used for cloud services to send notifications to WNS  |


## Microsoft Push Notifications Service (MPNS) public IP ranges
If you are using the legacy notification service, MPNS, the IP address ranges that you will need to add to the allow list are available from Download Center: [Microsoft Push Notifications Service (MPNS) Public IP ranges](https://www.microsoft.com/download/details.aspx?id=44535).


## Related topics

* [Quickstart: Sending a push notification](/previous-versions/windows/apps/hh868252(v=win.10))
* [How to request, create, and save a notification channel](/previous-versions/windows/apps/hh465412(v=win.10))
* [How to intercept notifications for running applications](/previous-versions/windows/apps/jj709907(v=win.10))
* [How to authenticate with the Windows Push Notification Service (WNS)](/previous-versions/windows/apps/hh465407(v=win.10))
* [Push notification service request and response headers](/previous-versions/windows/apps/hh465435(v=win.10))
* [Guidelines and checklist for push notifications](./windows-push-notification-services--wns--overview.md)
 
