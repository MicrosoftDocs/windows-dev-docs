---
author: seksenov
title: Hosted Web Apps - Accessing Universal Windows Platform (UWP) features and Runtime APIs
description: Access Universal Windows Platform (UWP) native features and Windows 10 Runtime APIs, including Cortona voice commands, Live Tiles, ACURs for security, OpenID and OAuth, all from remote JavaScript.
kw: Hosted Web Apps, Accessing Windows 10 features from remote JavaScript, Building a Win10 Web Application, Windows JavaScript Apps, Microsoft Web Apps, HTML5 app for PC, ACUR URI Rules for Windows App, Call Live Tiles with web app, Use Cortana with web app, Access Cortana from website, msapplication-cortanavcd
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: Hosted Web Apps, WinRT APIs for JavaScript, Win10 web app, Windows JavaScript app, ApplicationContentUriRules, ACURs, msapplication-cortanavcd, Cortana for web apps
localizationpriority: medium
---

# Accessing UWP features

Your web application can have full access to the Universal Windows Platform (UWP), activating native features on Windows devices, [benefiting from Windows security](#keep-your-app-secure--setting-application-content-uri-rules-acurs), [calling Windows Runtime APIs](#call-windows-runtime-apis) directly from script hosted on a server, leveraging [Cortana integration](#integrate-cortana-voice-commands), and using an [online authentication provider](#web-authentication-broker). [Hybrid apps](##create-hybrid-apps--packaged-web-apps-vs-hosted-web-apps) are also supported as you can include local code to be called from the hosted script and manage app navigation between remote and local pages.

## Keep your app secure – Setting Application Content URI Rules (ACURs)

Through ACURs, otherwise known as a URL allow list, you are able to give remote URLs direct access to Universal Windows APIs from remote HTML, CSS, and JavaScript. At the Windows OS level, the right policy bounds have been set to allow code hosted on your web server to directly call platform APIs. You define these bounds in the app package manifest when you place the set of URLs that make up your Hosted Web App in the Application Content URI Rules (ACURs). Your rules should include your app’s start page and any other pages you want included as app pages. Optionally, you can exclude specific URLs, too.

There are several ways to specify a URL match in your rules:

- An exact hostname
- A hostname for which a URI with any subdomain of that hostname is included or excluded
- An exact URI
- An exact URI that can contain a query property
- A partial path and a wildcard to indicate a particular file extension for an include rule
- Relative paths for exclude rules

If your user navigates to a URL that is not included in your rules, then Windows opens the target URL in a browser.

Here are a few examples of ACURs.

```HTML
<Application
Id="App"
StartPage="https://contoso.com/home">
<uap:ApplicationContentUriRules>
    <uap:Rule Type="include" Match="https://contoso.com/" WindowsRuntimeAccess="all" />
    <uap:Rule Type="include" Match="https://*.contoso.com/" WindowsRuntimeAccess="all" />
    <uap:Rule Type="exclude" Match="https://contoso.com/excludethispage.aspx" />
</uap:ApplicationContentUriRules>
```

## Call Windows Runtime APIs

If a URL is defined within the app’s bounds (ACURs), it can call Windows Runtime APIs with JavaScript using the “WindowsRuntimeAccess” attribute. The Windows namespace will be injected and present in the script engine when a URL with appropriate access is loaded in the App Host. This makes Universal Windows APIs available for the app’s scripts to call directly. As a developer, you just need to feature detect for the Windows API you would like to call and, if available, proceed to light-up platform features.

To enable this, you need to specify the `(WindowsRuntimeAccess="<<level>>")` attribute in the ACURs with the one of these values:

- **all**: Remote JavaScript code has access to all UWP APIs and any local packaged components.
- **allowForWeb**: Remote JavaScript code has access to custom in package code only. Local access to custom C++/C# components.
- **none**: Default. The specified URL has no platform access.

Here is an example rule type:

```HTML
<uap:ApplicationContentUriRules>
    <uap:Rule Type="include" Match="http://contoso.com/" WindowsRuntimeAccess="all"  />
</uap:ApplicationContentUriRules>
```

This gives script running on https://contoso.com/ access to Windows Runtime namespaces and custom packaged components in the package. See the [Windows.UI.Notifications.js](https://gist.github.com/Gr8Gatsby/3d471150e5b317eb1813#file-windows-ui-notifications-js) example on GitHub for toast notifications.

Here is an example of how to implement a Live Tile and update it from remote JavaScript:

```Javascript
function updateTile(message, imgUrl, imgAlt) {
    // Namespace: Windows.UI.Notifications

    if (typeof Windows !== 'undefined'&&
            typeof Windows.UI !== 'undefined' &&
            typeof Windows.UI.Notifications !== 'undefined') {	
        var notifications = Windows.UI.Notifications,
        tile = notifications.TileTemplateType.tileSquare150x150PeekImageAndText01,
        tileContent = notifications.TileUpdateManager.getTemplateContent(tile),
        tileText = tileContent.getElementsByTagName('text'),
        tileImage = tileContent.getElementsByTagName('image');	
        tileText[0].appendChild(tileContent.createTextNode(message || 'Demo Message'));
        tileImage[0].setAttribute('src', imgUrl || 'https://unsplash.it/150/150/?random');
        tileImage[0].setAttribute('alt', imgAlt || 'Random demo image');	
        var tileNotification = new notifications.TileNotification(tileContent);
        var currentTime = new Date();
        tileNotification.expirationTime = new Date(currentTime.getTime() + 600 * 1000);
        notifications.TileUpdateManager.createTileUpdaterForApplication().update(tileNotification);
    } else {
        //Alternative behavior

    }
}
```

This code will produce a tile that looks something like this:

![Windows 10 calling a live tile](images/hwa-to-uwp/hwa_livetile.png)

Call Windows Runtime APIs with whatever environment or technique is most familiar to you by keeping your resources on a server feature detecting for Windows capabilities prior to calling them. If platform capabilities are not available, and the web app is running in another host, you can provide the user with a standard default experience that works in the browser.

## Integrate Cortana voice commands

You can take advantage of Cortana integration by specifying a Voice Command Definition (VCD) file in your html page. The VCD file is an xml file that maps commands to specific phrases. For example, a user could tap the Start button and say “Contoso Books, show best sellers” to both launch the Contoso Books app and to navigate to a “best sellers” page.

When you add a `<meta>` element tag that lists the location of your VCD file, Windows automatically downloads and registers the Voice Command Definition file.

Here is an example of the use of the tag in an html page in a hosted web app:

```HTML
<meta name="msapplication-cortanavcd" content="https:// contoso.com/vcd.xml"/>
```

For more info on Cortana integration and VCDs, see [Cortana interactions and Voice Command Definition (VCD) elements and attributes v1.2](https://docs.microsoft.com/en-us/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2).

## Create Hybrid apps – Packaged web apps vs. Hosted web apps

You have options for creating your UWP app. The app might be designed to be downloaded from the Microsoft Store and fully hosted on the local client; often referred to as a **Packaged Web App**. This lets you run your app offline on any compatible platform. Or the app might be a fully hosted web app that runs on a remote web server; typically known as a **Hosted Web App**. But there is also a third option: the app can be hosted partially on the local client and partially on a remote web server. We call this third option a **Hybrid app** and it typically uses the **WebView** component to make remote content look like local content. Hybrid apps can include your HTML5, CSS, and Javascript code running as a package inside the local app client and retain the ability to interact with remote content.

## Web authentication broker

You can use the web authentication broker to handle the login flow for your users if you have an online identity provider that uses internet authentication and authorization protocols like OpenID or OAuth. You specify the start and end URIs in a `<meta>` tag on an html page in your app. Windows detects these URIs and passes them to the web authentication broker to complete the login flow. The start URI consists of the address where you send the authentication request to your online provider appended with other required information, such as an app ID, a redirect URI where the user is sent after completing authentication, and the expected response type. You can find out from your provider what parameters are required. Here is an example use of the `<meta>` tag:

```HTML
<meta name="ms-webauth-uris" content="https://<providerstartpoint>?client_id=<clientid>&response_type=token, https://<appendpoint>"/>
```

For more guidance, see [Web authentication broker considerations for online providers](../security/web-authentication-broker.md).

## App capability declarations

If your app needs programmatic access to user resources like pictures or music, or to devices like a camera or a microphone, you must declare the appropriate capability. There are three app capability declaration categories: 

- [General-use capabilities](https://docs.microsoft.com/en-us/windows/uwp/packaging/app-capability-declarations#general-use-capabilities) that apply to most common app scenarios. 
- [Device capabilities](https://docs.microsoft.com/en-us/windows/uwp/packaging/app-capability-declarations#device-capabilities) that allow your app to access peripheral and internal devices. 
- [Special-use capabilities](https://docs.microsoft.com/en-us/windows/uwp/packaging/app-capability-declarations#special-and-restricted-capabilities) that require a special company account for submission to the Store to use them. 

For more info about company accounts, see [Account types, locations, and fees](https://docs.microsoft.com/en-us/windows/uwp/publish/account-types-locations-and-fees).

> [!NOTE]
> It is important to know that when customers get your app from the Microsoft Store, they are notified of all the capabilities that the app declares. So do not use capabilities that your app does not need.

You request access by declaring capabilities in your app’s [package manifest](https://docs.microsoft.com/en-us/uwp/schemas/appxpackage/appx-package-manifest). For more information, see these articles on [Packaging for Universal Windows Platform (UWP) apps](https://docs.microsoft.com/en-us/windows/uwp/packaging/index).

Some capabilities provide apps access to a sensitive resource. These resources are considered sensitive because they can access the user’s personal data or cost the user money. Privacy settings, managed by the Settings app, let the user dynamically control access to sensitive resources. Thus, it’s important that your app doesn’t assume a sensitive resource is always available. For more info about accessing sensitive resources, see [Guidelines for privacy-aware apps](https://msdn.microsoft.com/library/windows/apps/hh768223.aspx).

## manifoldjs and the app manifest

An easy way to turn your website into a UWP app is to use an **app manifest** and **manifoldjs**. The app manifest is an xml file that contains metadata about the app. It specifies such things as the app’s name, links to resources, display mode, URLs, and other data that describes how the app should be deployed and run. manifoldjs makes this process very easy, even on systems that do not support web apps. Please go to [manifoldjs.com](http://www.manifoldjs.com/) for more information on how it works. You can also view a manifoldjs demonstration as part of this [Windows 10 Web Apps presentation](http://channel9.msdn.com/Events/WebPlatformSummit/2015/Hosted-web-apps-and-web-platform-innovations?wt.mc_id=relatedsession).

## Related topics
- [Windows Runtime API: JavaScript Code Samples](https://microsoft.github.io/WindowsRuntimeAPIs_Javascript_snippets/)
- [Codepen: sandbox to use for calling Windows Runtime APIs](http://codepen.io/seksenov/pen/wBbVyb/)
- [Cortana interactions](https://developer.microsoft.com/en-us/cortana)
- [Voice Command Definition (VCD) elements and attributes v1.2](https://msdn.microsoft.com/library/windows/apps/dn954977.aspx)
- [Web authentication broker considerations for online providers](https://docs.microsoft.com/en-us/windows/uwp/security/web-authentication-broker)
- [App capability declarations](https://docs.microsoft.com/en-us/windows/uwp/packaging/app-capability-declarations)
