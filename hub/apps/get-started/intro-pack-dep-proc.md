---
title: Windows apps--packaging, deployment, and process
description: This topic discusses your options around app packaging, deploy/distribute/install, and your app's run-time process.
ms.topic: article
ms.date: 08/19/2024
keywords: intro, introduction, all-up, all, up, Windows, Windows apps, packaging, deployment, process, run-time
ms.localizationpriority: medium
---

# Windows apps: packaging, deployment, and process

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

This topic discusses your options concerning:

* Whether or not your app will be packaged.
* How you'll deploy/distribute your app, and how it'll be installed.
* Your app's run-time process&mdash;how isolated it'll be; and what APIs will be available to it.

You can make those decisions for both new and existing apps. But if you're still in the planning stage for a new app, then before you start to think about the considerations above, first decide what development platform and user interface (UI) framework you'll use for your app. And for that decision, see [An overview of Windows development options](/windows/apps/get-started/).

## Packaged or unpackaged

The decision to make your app either packaged or unpackaged is first determined by a concept known as *package identity*, which we'll describe in this section. If you don't need that, then the decision comes down to the desired installer experience for yourself and for your users. Let's drill into the details of those things.

Many Windows extensibility features&mdash;including background tasks, notifications, live tiles, custom context menu extensions, and share targets&mdash;can be used by an app only if that app has package identity at runtime. That's because the operating system (OS) needs to be able to identify the caller of the corresponding API. See [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps).

* If you need to use any of those features, then your app needs package identity. And therefore it *needs* to be a packaged app (packaged apps are the only kind that have package identity). A packaged app is packaged by using MSIX technology (see [What is MSIX?](/windows/msix/overview)).
  * For a new app, the packaging process is straightfoward (and at the end of this section there's info about how to do it).
  * For some existing apps, you can follow the same packaging process as for a new app. But because some existing apps aren't yet ready for all of their content to be present inside an MSIX package, there's an option for your app to be *packaged with external location*. That enables your app to have package identity; thereby being able to use those features that require it. For more info, see [Grant package identity by packaging with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).
* Even if you *don't* need to use any of those features, then creating a packaged app is still a good idea. It gives your users the easiest way to install, uninstall, and update your app. For more info, see [Deployment/distribution/installation](#deploymentdistributioninstallation) in this topic.
* But creating an unpackaged app is an option.

The takeaway is that packaged apps are the only kind that have package identity (and they have the best install experience). An unpackaged app doesn't have package identity; so it can't use the APIs/features mentioned above.

For more details about packaged versus unpackaged, see [Deployment overview](/windows/apps/package-and-deploy/); in particular, the **Advantages and disadvantages of packaging your app** section in that topic. That topic also mentions the *packaged with external location* option.

For info about how to configure your app as packaged or unpackaged:

* **WinUI 3 apps (Windows App SDK)**. See the `AppxPackage` Visual Studio project property in [Project properties](/windows/apps/package-and-deploy/project-properties); and see [Create your first WinUI 3 (Windows App SDK) project](/windows/apps/winui/winui3/create-your-first-winui3-app).
* **Desktop apps**. See [Set up your desktop app for MSIX packaging](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net).
* **Universal Windows Platform (UWP) apps**. UWP apps are already configured as packaged; and that configuration can't be changed.

Also see the [Windows Package Manager and the WinGet client](#windows-package-manager-and-the-winget-client) section in this topic.

## Deployment/distribution/installation

* A packaged app is packaged by using MSIX technology.
  * A packaged app is *installed* by using MSIX, also. But if you choose to *package with external location*, then you can think of that as a "bring-your-own-installer" model. So there *will* be some installer work for you to do with that option. For more info, see [Grant package identity by packaging with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).
* An unpackaged app doesn't involve MSIX at all.

So, why does it matter whether or not your app is packaged?

* Well, MSIX gives your users an easy way to install, uninstall, and update your app. Uninstall is clean&mdash;when your app is uninstalled, the system is restored to the same state it was in before installation; no artifacts are left behind.
* This kind of app also supports incremental and automatic updates.
* The Microsoft Store optimizes for apps of this kind (although they can be used in or out of the Store).
* It's an easy path for use via MSIX app attach (for Azure Virtual Desktop virtual machines). For more info, see [What is MSIX app attach?](/azure/virtual-desktop/what-is-app-attach).
* A signed package benefits from strong anti-tampering. This benefit is even greater than for an unpackaged app installed under **Program Files**.

Also see the [Windows Package Manager and the WinGet client](#windows-package-manager-and-the-winget-client) section in this topic.

## AppContainer or Medium IL

The option to run your app in an AppContainer, or not, is a question of security. An AppContainer app's process and its child processes run inside a lightweight app container where they can access only the resources that are specifically granted to them. And they're isolated using file system and registry virtualization. As a result, apps implemented in an AppContainer can't be hacked to allow malicious actions outside of the limited assigned resources.

Packaged or unpackaged apps can be configured to run in an AppContainer. But the process is more straightforward for packaged apps. If an app isn't an AppContainer app, then it's a *Medium IL* app.

For more info, see [AppContainer for legacy apps](/windows/win32/secauthz/appcontainer-for-legacy-applications-) and [MSIX AppContainer apps](/windows/msix/msix-container).

For info about how to configure your app to run in an AppContainer or Medium IL:

* **WinUI 3 apps (Windows App SDK)**. See the `uap10:TrustLevel` app package manifest attribute in [Configure a WinUI 3 project for AppContainer](/windows/msix/msix-container#configure-a-winui-3-project-for-appcontainer).
* **Desktop apps**. See the `TrustLevel` Visual Studio project property in [MSIX AppContainer apps](/windows/msix/msix-container) (in the section that's appropriate for your kind of app).
* **Universal Windows Platform (UWP) apps**. UWP apps are already configured to run in an AppContainer; and that configuration can't be changed.

Remember that unpackaged apps don't have an app package manifest. So for unpackaged apps, you declare your AppContainer-or-Medium-IL decision in your project file instead of in an app package manifest.

### Win32 app isolation

> [!IMPORTANT]
> The feature described in this section is available in pre-release versions of the [Windows Insider Preview](https://www.microsoft.com/software-download/windowsinsiderpreviewSDK).

Win32 app isolation is an upcoming security feature in Windows which, in the event of an app being compromised, helps contain the damage, and safeguard user privacy choices. This feature is built on the foundation of AppContainers and components that virtualize resources and provide brokered access to other resources. For documentation and tools to help you isolate your apps, see [Welcome to the Win32 app isolation repo](https://github.com/microsoft/win32-app-isolation).

## App capabilities

App capabilities (for example, internetClient, location, microphone, and bluetooth) are relevant mostly to *packaged apps that run in an AppContainer*. So that includes *all* Universal Windows Platform (UWP) apps, and *some* desktop apps.

But there are some scenarios where even a Medium IL app (that is, *not* an AppContainer app) should declare a capability. One example is the *runFullTrust* restricted capability.

For more details about app capabilities, what kinds of apps they apply to, and how to configure them, see [App capability declarations](/windows/uwp/packaging/app-capability-declarations). You configure capabilities in your app package manifest; and that's why they apply only to packaged apps.

## Kinds of apps

Desktop apps and Universal Windows Platform (UWP) apps are the two main kinds of apps&mdash;although, there are several kinds of apps in the *desktop apps* family. Choosing a user interface (UI) framework&mdash;WinForms, WPF, Win32, Direct 2D/3D, UWP, or WinUI 3&mdash;is one option that's to some degree independent of the configurations described in this topic.

But let's take a look at how those app kinds can differ from one another in terms of packaging, deployment, and process.

First off, all UWP apps are packaged, and run in an AppContainer. But for desktop apps, things are more flexible. You can choose to package your desktop app, or not. And, independently of that decision, you can choose to configure your desktop app as either an AppContainer or a Medium IL app.

| |Packaged|Unpackaged|
|-|-|-|
|**AppContainer**|Desktop apps<br/>UWP apps|Desktop apps|
|**Medium IL**|Desktop apps|Desktop apps|

For packaged apps, to configure the kind of app you want, you use the `uap10:RuntimeBehavior` attribute in your app package manifest (see [Application (Windows 10)](/uwp/schemas/appxpackage/uapmanifestschema/element-application)).

* **Desktop apps** are Windows `.exe`s, typically with a **main** or **WinMain** entry-point function. To configure your app as a desktop app, set `uap10:RuntimeBehavior` to either "packagedClassicApp" or "win32App".
  * The value "packagedClassicApp" indicates either a WinUI 3 app (Windows App SDK) or a Desktop Bridge app (Centennial). The difference is that a Centennial app runs in an AppContainer.
  * And "win32App" indicates any other kind of Win32 app (including an app packaged with external location).
* Lastly, setting `uap10:RuntimeBehavior` to  "windowsApp" gives you a UWP app.

For all of the options for the kinds of apps you can develop, see [Windows app development: options and features](/windows/apps/get-started/dev-options).

## Windows App SDK&mdash;framework-dependent or self-contained

If you're developing or maintaining an app that makes use of the [Windows App SDK](/windows/apps/windows-app-sdk/), then you have a further decision to make. Because there are the following two ways in which you can deploy the Windows App SDK that your app depends on:

* Framework-dependent (the default). Your app needs the Windows App SDK runtime and/or Framework package to be present on the target machine.
* Self-contained. Your app carries with it its Windows App SDK dependencies.

For more info, see [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview).

## Windows Package Manager and the WinGet client

A package manager can help your users to install/upgrade/configure your software by automating the workflow. Package managers can help install any software, but they tend to be used mostly to install developer tools. So if you're building a developer tool, then you might be particuarly interested in this option. But here's how it works:

* You, as the software developer, define to the package manager (in the form of declarative instructions) all of the pieces necessary for a successful install of your product.
* And then when a user installs your software, the package manager follows your declarative instructions to automate the install-and-configure workflow.

The result is a reduction in time spent getting a user's environment ready, and better compatibility between the components installed. And you can use Windows Package Manager to distribute your packaged or unpackaged apps in formats such as `.msix`, `.msi`, and `.exe`.

For more info, see [Windows Package Manager](/windows/package-manager/).
