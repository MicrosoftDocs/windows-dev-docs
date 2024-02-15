---
description: This section will guide you on how to distribute your Win32 application through Microrosft Store.
title: How to distribute your Win32 application through Microrosft Store.
ms.date: 02/15/2024
ms.topic: article
ms.localizationpriority: medium
---

# How to distribute your Win32 application through Microrosft Store

This article guides you on a smooth onboarding process, various distribution options, recommended best practices, and scenarios to consider when distributing your app via the Store, to ensure a better customer experience. 



## Create a Partner Center account

You must have a [Partner Center account](https://partner.microsoft.com/dashboard/) to submit an app to the Store. If you don’t have an account, follow these [steps](../publish/partner-center/partner-center-developer-account.md) for account creation and then follow these [steps](../publish/publish-your-app/overview.md) to effortlessly introduce your application to the Store.



## Distribution options – Select the one that works best for you!

When you are distributing your Win32 app — which may be built using a variety of frameworks and technologies such as Windows App SDK, WPF, WinForms, Electron, QT, and others — through the Microsoft Store, there are two main options you can choose:

### Package your application as MSIX to leverage all Store features
Streamline the user experience in discovery, acquisition, and installation by packaging your Win32 app as an MSIX using Desktop Bridge.

### List your existing EXE or MSI from your website</h2> List your Win32 app in its original form in the Microsoft Store.


#### Refer to the table below for a comprehensive comparison of these two methods.

| Feature | Packaged (MSIX) |	Unpackaged (Win32) |
| ----------- | ----------- | ----------- |
| Hosting | Complimentary, provided by Microsoft. | Publishers are responsible for hosting and associated costs. |	
| Commerce Platform (payment, in-apps, subscriptions, licensing) | Use Microsoft Store commerce platform or your own or 3P commerce platform. | Use your own or 3P commerce platform. |	
| Code signing | Complimentary, provided by Microsoft. | Publishers must sign with a certificate issued by a Certificate Authority (CA) that is part of the [Microsoft Trusted Root Program](/security/trusted-root/participants-list) and cover associated costs. |	
| Auto-Updates | The OS will automatically check updates every 24 hours. | The application is responsible for managing its own auto-updates. |
| S-Mode Support | Supported. | Not Supported. |
| Publish as Private Application | Available. | Not Available. | 	
| Package Flighting | Available. | Not Available. | 	
| Advanced Integration with Windows (e.g., Share dialog, Launch from the Store, ...) | Yes. | No. |	
| Windows 11 backup and restore feature | Can be automatically installed when users are restoring or migrating a device. |	Start Menu icons will be restored but will point to the Microsoft Store product page. |	



#### Let's explore each of these options in more detail in the following sections.

### Option 1 - Package your Win32 app as MSIX

Package your application into a MSIX is very simple, you can either use:

1. Visual Studio by adding the project Windows Application Packaging to your solution. See [Set up your desktop application for MSIX packaging in Visual Studio](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net).

1. Use installer solutions from one of our partners. See [Package a desktop app using third-party installers](/windows/msix/desktop/desktop-to-uwp-third-party-installer).

1. Microsoft MSIX Packaging Tool to create the MSIX from an existing installer. See [Create an MSIX package from any desktop installer (MSI, EXE, ClickOnce, or App-V)](/windows/msix/packaging-tool/create-app-package).

You can verify the compliance of your MSIX with the Microsoft Store by utilizing the [Windows App Certification Kit instructions](/windows/uwp/debug-test-perf/windows-app-certification-kit).

If your application was previously distributed on the web or if you intend to distribute it on the web as well, you can discover recommendations on how to migrate users from the web application to the Store version here.

### Option 2 - Bring your unmodified installer as-is

Microsoft Store has allowed unpackaged applications since June 2021. To publish your application on the Store, you only need to share a link to your installer through the Partner Center and provide some additional information. Once your installer has been tested by our certification team and the submission is published, users will be able to locate your application in the Store and proceed with the installation.

For your installer to be accepted, it must adhere to the following recommendations:
1. Must be a .msi or a .exe installer.
2. Must be offline
3. The binary hosted by the shared URL should remain unchanged.
4. Your installer should only install the product intended by the user.
