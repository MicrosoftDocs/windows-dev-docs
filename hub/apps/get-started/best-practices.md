---
title: Windows Application Development - Best Practices
description: A collection of best practices related to UI/UX, security, performance, and more.
ms.topic: article
ms.date: 03/08/2022
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

The following resources will help you incorporate the latest and recommended Windows application UI/UX patterns into your Windows applications.

 - [Top 11 things you can do to make your app great on Windows 11](/windows/apps/get-started/make-apps-great-for-windows)
 - [Adopting Windows 11 visual styles](/windows/apps/design/signature-experiences/design-principles) 
   - [Acrylic](/windows/apps/design/style/acrylic) and [Mica](/windows/apps/design/style/mica) 
   - ContextMenu 
   - Dark/light theme 
   - [Iconography](/windows/apps/design/signature-experiences/iconography) 
   - Controls update 
   - [Typography](/windows/apps/design/signature-experiences/typography) 
   - [Caption control and title bar](/windows/apps/design/basics/titlebar-design) 
   - [Rounded geometry](/windows/apps/design/signature-experiences/geometry) 
   - Window border and shadow 
 - [On-object commanding](/windows/apps/design/controls/collection-commanding#creating-context-menus) 
 - [Panning and scrolling](/windows/apps/design/input/guidelines-for-panning) 
 - Text editing
 - DPI awareness
 - [Responsive layout](/windows/apps/design/layout/responsive-design)
 - [Supporting Snap layouts](/windows/apps/desktop/modernize/apply-snap-layout-menu)


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

Application discovery and installation are one of the first interactions that a user will have with your application. The following best practices will help ensure that your application leaves a good impression when discovered and managed by end-users:

 - Ensure that your application's installation is error free, transparent and clean. 
 - Avoid requiring elevated permissions to install when possible.
 - Avoid requiring operating system reboots when possible. 
 - Give users the option to add your application to the Start menu. 
 - Minimize and simplify the artifacts that your application leaves behind. 
 - Temporary files should be managed by your application so that your users don't have to manage them.
 - Use MSIX-packaging to ensure that your end-users experience an elegant installation and update experience.
 - Deliver a transparent update experience that utilizes either MSIX-packaging updates, the Windows OS update mechanism, automatic updates, or on-demand updates.  
 - Ensure that your application can be easily uninstalled through the `Add or Remove Programs` control. 
 - When your application is uninstalled, ensure that ARP entries, Start menu entries, files and directories, registry entries, and temporary files are also removed. Consider giving your users the option to preserve their application data when they uninstall your application.


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
