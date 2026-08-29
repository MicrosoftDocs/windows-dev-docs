---
title: Windows apps--packaging, deployment, and process
description: This topic discusses your options around app packaging, deploy/distribute/install, and your app's run-time process.
ms.topic: article
ms.date: 08/29/2026
keywords: intro, introduction, all-up, all, up, Windows, Windows apps, packaging, deployment, process, run-time
ms.localizationpriority: medium
---

# Windows apps: packaging, deployment, and process

This topic discusses your options concerning:

* Which packaging option to use for your app (packaged, packaged with external location, or unpackaged).
* How you'll deploy/distribute your app, and how it'll be installed.
* Your app's run-time process, including how isolated it will be and what APIs will be available to it.

You can make those decisions for both new and existing apps. But if you're still in the planning stage for a new app, then before you start to think about the considerations above, first decide what development platform and user interface (UI) framework you'll use for your app. And for that decision, see [An overview of Windows development options](index.md).

## Packaging options: packaged, packaged with external location, or unpackaged

The decision about which packaging option to use for your app is first determined by a concept known as *package identity*. Many Windows extensibility features (background tasks, push-notification scenarios that use background delivery or COM activation, custom context menu extensions, and share targets) can be used by an app only if that app has package identity at runtime, because the operating system (OS) needs to be able to identify the caller of the corresponding API. Only packaged apps (including apps *packaged with external location*) have package identity.

For a full explanation of the packaged, packaged with external location, and unpackaged models, and how to choose between them, see the [Packaging overview](../package-and-deploy/packaging/index.md). For the features that depend on package identity, see [Features that require package identity](../desktop/modernize/modernize-packaged-apps.md).

For info about how to configure your app as packaged or unpackaged:

* **WinUI 3 apps (Windows App SDK)**. See the `AppxPackage` Visual Studio project property in [Project properties](../package-and-deploy/project-properties.md); and see [Create your first WinUI (Windows App SDK) project](/windows/apps/winui/winui3/create-your-first-winui3-app).
* **Desktop apps**. See [Set up your desktop app for MSIX packaging](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net).
* **Universal Windows Platform (UWP) apps**. UWP apps are already configured as packaged; and that configuration can't be changed.

Also see the [Windows Package Manager and the WinGet client](#windows-package-manager-and-the-winget-client) section in this topic.

## Deployment/distribution/installation

A packaged app is both packaged and installed by using MSIX. If you choose to *package with external location*, that's a "bring-your-own-installer" model, so you still do the installer work yourself (see [Grant package identity by packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps.md)). An unpackaged app doesn't involve MSIX at all.

Packaging matters because MSIX gives your users a clean install, uninstall, and update experience, supports incremental and automatic updates, is optimized by the Microsoft Store, works with [MSIX app attach](/azure/virtual-desktop/what-is-app-attach) for Azure Virtual Desktop, and benefits from strong anti-tampering when signed.

For deployment and distribution guidance, see the [Package and deploy overview](../package-and-deploy/index.md) and [Choose a distribution path](../package-and-deploy/choose-distribution-path.md). See also the [Windows Package Manager and the WinGet client](#windows-package-manager-and-the-winget-client) section in this topic.

## AppContainer or Medium IL

The option to run your app in an AppContainer, or not, is a question of security. An AppContainer app's process and its child processes run inside a lightweight app container where they can access only the resources that are specifically granted to them. And they're isolated using file system and registry virtualization. As a result, apps implemented in an AppContainer can't be hacked to allow malicious actions outside of the limited assigned resources.

Packaged or unpackaged apps can be configured to run in an AppContainer. But the process is more straightforward for packaged apps. If an app isn't an AppContainer app, then it's a *Medium IL* app.

For more info, see [AppContainer for legacy apps](/windows/win32/secauthz/appcontainer-for-legacy-applications-) and [MSIX AppContainer apps](/windows/msix/msix-container).

For info about how to configure your app to run in an AppContainer or Medium IL:

* **WinUI apps (Windows App SDK)**. See the `uap10:TrustLevel` app package manifest attribute in [Configure a WinUI project for AppContainer](/windows/msix/msix-container#configure-a-winui-3-project-for-appcontainer).
* **Desktop apps**. See the `TrustLevel` Visual Studio project property in [MSIX AppContainer apps](/windows/msix/msix-container) (in the section that's appropriate for your kind of app).
* **Universal Windows Platform (UWP) apps**. UWP apps are already configured to run in an AppContainer; and that configuration can't be changed.

Remember that unpackaged apps don't have an app package manifest. So for unpackaged apps, you declare your AppContainer-or-Medium-IL decision in your project file instead of in an app package manifest.

### Win32 app isolation

Win32 app isolation is a security feature (available in Windows 11, version 24H2 and later) that helps contain damage if an app is compromised, and safeguards user privacy choices. It builds on AppContainers and components that virtualize resources and provide brokered access. For more info, see [Win32 app isolation overview](/windows/win32/secauthz/app-isolation-overview) and the [Win32 app isolation GitHub repo](https://github.com/microsoft/win32-app-isolation).

## App capabilities

App capabilities (for example, internetClient, location, microphone, and bluetooth) are relevant mostly to *packaged apps that run in an AppContainer*. So that includes *all* Universal Windows Platform (UWP) apps, and *some* desktop apps.

But there are some scenarios where even a Medium IL app (that is, *not* an AppContainer app) should declare a capability. One example is the *runFullTrust* restricted capability.

For more details about app capabilities, what kinds of apps they apply to, and how to configure them, see [App capability declarations](/windows/uwp/packaging/app-capability-declarations). You configure capabilities in your app package manifest; and that's why they apply only to packaged apps.

## Kinds of apps

Desktop apps and Universal Windows Platform (UWP) apps are the two main kinds of apps, although there are several kinds of apps in the desktop apps family. Choosing a UI framework (WinForms, WPF, Win32, Direct 2D/3D, or WinUI 3) is to some degree independent of the configurations described in this topic.

But let's take a look at how those app kinds can differ from one another in terms of packaging, deployment, and process.

First off, all UWP apps are packaged, and run in an AppContainer. But for desktop apps, things are more flexible. You can choose to package your desktop app, or not. And, independently of that decision, you can choose to configure your desktop app as either an AppContainer or a Medium IL app.

| |Packaged|Unpackaged|
|-|-|-|
|**AppContainer**|Desktop apps<br/>UWP apps|Desktop apps|
|**Medium IL**|Desktop apps|Desktop apps|

For packaged apps, to configure the kind of app you want, you use the `uap10:RuntimeBehavior` attribute in your app package manifest (see [Application (Windows 10)](/uwp/schemas/appxpackage/uapmanifestschema/element-application)).

* **Desktop apps** are Windows `.exe`s, typically with a **main** or **WinMain** entry-point function. To configure your app as a desktop app, set `uap10:RuntimeBehavior` to either "packagedClassicApp" or "win32App".
  * The value "packagedClassicApp" indicates either a WinUI app (Windows App SDK) or a Desktop Bridge app (Centennial). The difference is that a Centennial app runs in an AppContainer.
  * And "win32App" indicates any other kind of Win32 app (including an app packaged with external location).
* Lastly, setting `uap10:RuntimeBehavior` to  "windowsApp" gives you a UWP app.

For all of the options for the kinds of apps you can develop, see [Windows app development: options and features](/windows/apps/get-started/dev-options).

## Windows App SDK: framework-dependent or self-contained

If your app uses the [Windows App SDK](../windows-app-sdk/index.md), then you also choose how to deploy the SDK runtime that your app depends on: framework-dependent (the default, where the Windows App SDK runtime and/or Framework package must be present on the target machine) or self-contained (your app carries its Windows App SDK dependencies with it). For more info, see the [Windows App SDK deployment overview](../package-and-deploy/deploy-overview.md).

## Windows Package Manager and the WinGet client

A package manager can help your users to install/upgrade/configure your software by automating the workflow. Package managers can help install any software, but they tend to be used mostly to install developer tools. So if you're building a developer tool, then you might be particularly interested in this option. But here's how it works:

* You, as the software developer, define to the package manager (in the form of declarative instructions) all of the pieces necessary for a successful install of your product.
* And then when a user installs your software, the package manager follows your declarative instructions to automate the install-and-configure workflow.

The result is a reduction in time spent getting a user's environment ready, and better compatibility between the components installed. And you can use Windows Package Manager to distribute your packaged or unpackaged apps in formats such as `.msix`, `.msi`, and `.exe`.

For more info, see [Windows Package Manager](../../package-manager/index.md).
