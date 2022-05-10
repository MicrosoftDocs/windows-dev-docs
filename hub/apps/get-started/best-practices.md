---
title: Windows Application Development - Best Practices
description: A collection of best practices related to UI/UX, security, performance, and more.
ms.topic: article
ms.date: 05/02/2022
keywords: windows, win32, desktop development
ms.author: mikben
author: matchamatch
ms.localizationpriority: medium
ms.collection: windows11
---

# Windows Application Development - Best Practices

This document identifies a collection of best practices that will help you build great apps that run on Windows and are optimized to delight the ~1.5 billion diverse PC users around the world. These best practices apply to all forms of Windows application development.

> [!NOTE]
> This is a sparse draft that still needs a significant amount of editorial refinement. As the draft content stabilizes in the Word doc, content partners will begin importing that information into this doc, where customer-facing copy will be drafted and finalized.

## User Experience (UX)

Windows 11 marks a visual evolution of the Windows operating system that raises the bar for both the look and experience of Windows. And our studies show that users have the same expectations of the apps they use on Windows as they have of Windows itself. One primary area where we see these customer expectations manifest is user experience - the ability to work naturally with a complete range of inputs, design and interaction patterns that look and feel at home on current and future devices, and support for modern windowing workflows and shell integration points.

When you adhere to Windows styles and standard Windows behaviors, your users do not have to re-learn, and your app will feel natural to use. An app that looks great can create a great first impression, but an app that is also easy to use and helps the user accomplish their goals will create a great lasting impression.

Windows 11 was built on the [Windows 11 design principles](/windows/apps/design/signature-experiences/design-principles). Following these guidelines as you build your apps will help you meet your customer's expectations of a great app experience. The following resources will help you incorporate the latest and recommended Windows application UI/UX patterns into your Windows applications.

**[Common controls](/windows/apps/get-started/make-apps-great-for-windows#4-use-the-latest-common-controls)**

Use the latest common controls to get the benefits of compatibility and accessibility by default. [WinUI](/windows/apps/winui/) provides new styles for common controls, and the default styles have been updated with new visuals and animations. If you can't use WinUI, you can copy the styles from the [design toolkits](https://www.figma.com/community/file/989931624019688277) and [WinUI Gallery](https://aka.ms/xamlcontrolsgallery).

**[Materials: Acrylic and Mica](/windows/apps/get-started/make-apps-great-for-windows#5-use-the-latest-design-materials-acrylic-and-mica)**

[Materials](/windows/apps/design/signature-experiences/materials) are visual effects applied to UX surfaces that resemble real life artifacts. [Acrylic](/windows/apps/design/style/acrylic) and [Mica](/windows/apps/design/style/mica) materials are used as base layers beneath interactive UI controls. Use [Acrylic](/windows/apps/design/style/acrylic) for transient surfaces that light-dismiss, like context menus. [Mica](/windows/apps/design/style/mica) is a very performant material that is meant to be used on long-lived UI surfaces like the title bar to communicate the active or inactive state of the app.

**[Dark and Light themes](/windows/apps/get-started/make-apps-great-for-windows#7-support-dark-and-light-themes)**

Light and Dark themes are a great way to let the user express their personality. Windows 11 updates the color tones to be softer on the eyes by avoiding pure white and black, which makes the colors much more delightful.

**[Iconography and Typography](/windows/apps/get-started/make-apps-great-for-windows#9-use-beautiful-iconography--typography)**

Windows 11 has [updated icons ("Segoe Fluent Icons")](/windows/apps/design/signature-experiences/iconography), improved support for [animated icons](/windows/apps/design/controls/animated-icon), and a [new UI font ("Segoe UI Variable")](/windows/apps/design/signature-experiences/typography). We recommend that you use these new icons and font whenever possible to be coherent on Windows 11. The new font brings much softer geometry and makes the text much more legible (not with GDI).

**[On-object commanding](/windows/apps/design/controls/collection-commanding#creating-context-menus)**

Use on-object commanding such as [context menus](/windows/apps/design/controls/menus-and-context-menus), [swipe commands](/windows/apps/design/controls/swipe), and [keyboard shortcuts](/windows/apps/design/input/keyboard-accelerators). It's important to make app commands available in various ways to support all users and input types.

- **[Context menu integration](/windows/apps/get-started/make-apps-great-for-windows#context-menus)**

  For Windows 11, we improved the behavior of the right-click context menu. If your app creates context menus, you may need to make some changes to ensure that these work well with Windows 11.

- **Text editing**

  Anywhere a user can edit text, you should support Cut/Copy/Paste commands and ensure they are exposed via all the input types. WinUI text controls do this by default, but you might need to do some extra work if you're not using WinUI.

**[Geometry and app silhouettes](/windows/apps/design/signature-experiences/geometry)**

[Windows 11 geometry](/windows/apps/design/signature-experiences/geometry) has been crafted to support modern app experiences. Progressively rounded corners, nested elements, and consistent gutters combine to create a soft, calm, and approachable effect that emphasizes unity of purpose and ease of use. Another feature of [Windows 11 app silhouettes](/windows/apps/design/basics/app-silhouette) is the integration of app and title bar content.

- **[Title bar integration](/windows/apps/design/basics/titlebar-design)**

  Use the WindowsAppSDK APIs to [integrate app content with the title bar](/windows/apps/develop/title-bar). You can use these APIs with WinUI 3, Win32, and .NET apps.

- **[Rounded corners](/windows/apps/get-started/make-apps-great-for-windows#6-use-rounded-corners-for-your-windows-and-support-snap-layouts)**

  In most cases, your app's window will have rounded corners by default on Windows 11. If you've customized your app window and don't have rounded corners, see [Apply rounded corners in desktop apps for Windows 11](/windows/apps/desktop/modernize/apply-rounded-corners) for some things you can do. You should also avoid customizing window borders and shadows, which can prevent the system from rounding the window corners.

**Page layout**

The most important thing to remember in relation to page layout is that your app window can be resized to many shapes and sizes and run on devices with different DPI and scale settings. Content and commands should not disappear when the app is resized, especially to smaller sizes like 800x600.

- **Responsive layout**

  Use [responsive design techniques](/windows/apps/design/layout/responsive-design) to optimize your app pages for different window sizes. Follow the [guidelines for panning or scrolling](/windows/apps/design/input/guidelines-for-panning) to ensure that users can always access your content, no matter how small the app window gets.

- **[Snap layouts](/windows/apps/get-started/make-apps-great-for-windows#6-use-rounded-corners-for-your-windows-and-support-snap-layouts)**

  Snap layouts are a new Windows 11 feature to help introduce users to the power of window snapping. Use the snap layouts menu to test your app in different snap layouts an ensure your app supports different snap sizes (1/2, 1/3, 1/4 screen).

  If the snap layouts menu doesn't appear for your app by default, see [Support snap layouts for desktop apps on Windows 11](/windows/apps/desktop/modernize/apply-snap-layout-menu) for some steps you can take to enable it.

- **DPI awareness**

  WinUI applications automatically scale for each display that they're running on. Other Windows programming technologies (Win32, WinForms, WPF, etc.) don't automatically handle DPI scaling so you need to do some additional work. Without this work, applications will appear blurry or incorrectly-sized in many common usage scenarios. For information about what is involved in updating a desktop application to render correctly, see[ High DPI Desktop Application Development on Windows](/windows/win32/hidpi/high-dpi-desktop-application-development-on-windows).

## Performance

Improving the performance of your Windows application will improve its overall user experience. We encourage you to review [What is application performance and why is it important?](/windows/apps/performance/#what-is-application-performance-and-why-is-it-important) to learn more.

Application performance considerations include:

 - CPU usage
 - Memory consumption
 - Power consumption
 - Network and storage utilization
 - Animation performance

Windows users expect applications to be responsive. You should be aware of how your application consumes system resources, and how you might be able to optimize. You can learn more about measuring your applications performance here: [When should you measure application performance?](/windows/apps/performance/introduction#when-should-you-measure-application-performance).

There are [several tools available](/windows/apps/performance/#what-tools-can-i-use-to-measure-application-performance) to help you assess the performance of your Windows apps. These tools will help you monitor your app and its source code, and they can even provide detailed event tracing for your entire Windows operating system. They'll also help you analyze the memory management of .NET framework. Measuring and analyzing the performance charactieristics of your application will help you identify performance optimization opportunities.

For help deciding between performance profiling tools, see [Choosing among Visual Studio Performance Profiler, Windows Performance Toolkit, and PerfView](/windows/apps/performance/choose-between-tools).


## Operating System / Hardware Optimization

TODO


## Application discovery and management

Application discovery and installation are one of the first interactions that a user will have with your application. A reliable update and uninstall mechanism are important for a consistent, high-quality user experience. The following best practices will help ensure that your application leaves a good impression when discovered and managed by end-users:
 
- **Application Discovery**  
  - Listing your app on [Microsoft Store](https://blogs.windows.com/windowsexperience/2021/06/24/building-a-new-open-microsoft-store-on-windows-11/) can make your app more discoverable for users.   
  - If you are hosting your app at multiple sites and Microsoft Store, it is important to have a consistent identity and consistent update mechanism across all hosting platforms.    

- **Installation**  
  - Ensure that your application's installation is error free, transparent, and clean.  
  - Avoid requiring elevated permissions to install and requiring operating system reboots when possible.  

- **Updates**  
  - Deliver a transparent update experience that minimizes the impact to the user and the system.   
  - With MSIX, updating app packages is optimized to ensure that only the essential changed bits of the app are downloaded to update an existing Windows app. 
  - Windows 10 and Windows 11 allow developers to make stronger guarantees around app updates from the Store. For more information, see [Auto-update and repair apps](/windows/msix/app-installer/auto-update-and-repair--overview).
  - Consider push notification-based updates or checking for available updates at app startup or at restart. 
  - When an MSIX is uninstalled by the user, all package installation content is removed, as well as any app configuration information stored in AppData. User created content should be stored in locations like Documents, which can then be retained by users even post app is uninstalled. For information about how packaged apps handle files and registry entries, see [Understanding how packaged desktop apps run on Windows](/windows/msix/desktop/desktop-to-uwp-behind-the-scenes). 
  - For unpackaged apps, ensure that your application can be easily uninstalled through the Add or Remove Programs control. When your application is uninstalled, ensure that ARP entries, Start menu entries, files and directories, registry entries, and temporary files are also removed. Consider giving your users the option to preserve their data when they uninstall your application.  

- **Additional Resources** 
  - [MSIX documentation](/windows/msix/)
  - [Windows Installer Best Practices](/windows/win32/msi/windows-installer-best-practices)

## Accessibility

TODO

## Security and Privacy

### How do I ensure that my app is secure?

As the Windows OS becomes more resilient to attack, malicious actors are increasingly looking towards applications as a key vector for harming people and organizations. An insecure application can be the entry point that allows an attacker to ransomware a person's files, steal a company's sensitive corporate data, or perform any number of malicious activities. Even if your application has no direct security bugs, attackers may manipulate your users into performing insecure actions via phishing, social engineering, or other attacks. Application security encompasses many different areas, some of which are outlined below.

**Security tips:**

- Follow the [Security Development Lifecycle](https://www.microsoft.com/securityengineering/sdl/) for all development.
  - **Threat modeling** can help you avoid security flaws.
  - Using **secure libraries, languages, and tools** minimizes implementation flaws.
  - **Secure defaults** can prevent security issues caused by user error.
- Don't require administrative privileges to _install_ your app.
  - Ideally, your app should support both administrative installs and per-user installs.
  - Using [MSIX packaging](/windows/msix/packaging-tool/tool-overview) is one way to achieve this.
- [Don't require administrative privileges to _run_ your app.](/windows/win32/win7appqual/standard-user-analyzer--sua--tool-and-standard-user-analyzer-wizard--sua-wizard-)
  - If there are certain features that need administrative privileges, consider separating them into their own processes to **reduce attack surface**.
- Consider using techniques such as **AppContainer** (UWP) or **[process attribute flags](/windows/win32/api/processthreadsapi/nf-processthreadsapi-updateprocthreadattribute)** to mitigate risk of vulnerabilities.
  - This may require separating your code into a regular UI process and a more-secure child process where you can execute especially risky code like parsing untrusted data.
- Prefer to use languages with **guaranteed memory safety** (such as C#, JavaScript, or Rust), especially for risky code paths (like parsing untrusted data).
- Use all the provided security mitigations provided by your compiler and toolset (e.g. [see here](https://devblogs.microsoft.com/cppblog/security-features-in-microsoft-visual-c/) for Visual C++).
- Always use your chosen language or framework’s standard libraries for cryptography and other security-sensitive code. _Do not try to build your own._
- **Digitally sign all components of your application** – not just the installer, but also the uninstaller (if you have one). Also sign all the EXE, DLL, and other executable files that make up your app.
  - Digital signatures enable the user to **verify the authenticity of your app** and allow Enterprise admins to secure their devices using [Windows Defender Application Control](/windows/security/threat-protection/windows-defender-application-control/wdac-and-applocker-overview).
  - Using MSIX packaging is one way to achieve this.
- Ensure all network communication is over a secure transport, such as SSL.
- Provide guardrails or other mitigations that can help **protect users from accidentally performing harmful actions**, even when coerced into doing so by attackers.
  - Simple “Are you sure you want to do _X_? _Yes / No_” dialogs are typically not effective, because users have been conditioned to click “Yes.”

### How do I ensure that my app follows appropriate privacy practices?

Most modern apps collect and use a large amount of data – including personal data – for various reasons. Telemetry, product improvement, and monetization are three common reasons for using data, but users and regulators alike are becoming more sensitive to the privacy implications of these practices. They are demanding more transparency and control over the data collected and used by apps. The simplest way to avoid privacy issues is to not collect or store any personal data, but that’s unrealistic for most apps. Instead, use the following tips to help minimize the privacy impact of your app.

**Privacy tips:**

- **Ensure you have an accurate Privacy Policy for your app.** Ideally, provide both a summary document written for a casual audience (your users) in addition to a long-form legal policy (written for your lawyers).
- **Familiarize yourself with privacy regulations** in the markets where your app will be available, and ensure your app meets or exceeds any requirements for disclosure, usage rights, deletion requests, etc.
- **Consider using technologies such as AppContainer (UWP)** to automatically minimize the amount of data available to your app. If your app is blocked from accessing data, it's impossible for your app to collect it, even accidentally (e.g. due to a bug in your code or a data-hungry 3rd party library).
- **Ensure you're collecting the least amount of personal data needed** to complete your app's experiences.
  - **Don't collect data "just in case"** – there should be a valid reason for collecting all data, e.g. to improve the customer's experience or to facilitate monetization.
- **Always get the user’s consent** before collecting and storing personal data and provide the user with an easy way to revert their decision in the future. Avoid "[dark patterns](https://www.reuters.com/legal/legalindustry/dark-patterns-new-frontier-privacy-regulation-2021-07-29/#:~:text=Some%20examples%20of%20dark%20pattern%20usage%20include)" such as making the "Yes" button larger or more prominent than the "No" button in a consent dialog.
  - **Consult with applicable regulations** to determine what specific disclosures and consent is required for specified kinds of data. For example, some regions may allow users to view, change, or delete the data you have stored about them.
- If you must transmit data over the network, **always use secured connections**, e.g. over TLS.
- **Avoid storing personal data in a centralized location** (e.g. website). If you must store personal data, minimize the amount of data you store, store it only for as long as strictly necessary, and ensure it is securely encrypted.
- **Verify that any 3rd-party libraries or SDKs you use also have good privacy practices.** Note this is _not_ limited to just advertising SDKs – any library that connects to the internet may impact the privacy of your app's users.

## Appendix on tooling

TODO
