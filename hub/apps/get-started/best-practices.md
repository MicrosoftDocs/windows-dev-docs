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

The following best practices will help you deliver secure Windows applications:

 - Follow the [Security Development Lifecycle](https://www.microsoft.com/en-us/securityengineering/sdl/) for all development. 
   - **Threat modeling** can help you avoid security flaws. 
   - Using **secure libraries, languages, and tools** minimize implementation flaws. 
   - **Secure defaults** can prevent security issues caused by user error. 
 - **Don't require administrative privileges to install your app**. Ideally, your app should support both administrative installs and per-user installs. Using MSIX packaging is one way to achieve this. 
 - **Don't require administrative privileges to run your app.** If there are certain features that need administrative privileges, consider separating them into their own processes to reduce attack surface.  
 - Consider using techniques such as **AppContainer** (UWP) or **[process attribute flags](/windows/win32/api/processthreadsapi/nf-processthreadsapi-updateprocthreadattribute)** to mitigate risk of vulnerabilities. This may require separating your code into a regular UI process and a more-secure child process where you can execute especially risky code like parsing untrusted data.
 - Prefer to use languages with **guaranteed memory safety** (such as C#, JavaScript, or Rust), especially for risky code paths (like parsing untrusted data). 
 - Use all the provided security mitigations provided by your compiler / toolset (e.g. [see here](https://devblogs.microsoft.com/cppblog/security-features-in-microsoft-visual-c/) for Visual C++).
 - Always use standard libraries for cryptography or other security-sensitive code; do not try and build your own. 
 
Privacy best practices include the following:

 - Familiarize yourself with privacy regulations in the markets where your app will be available, and ensure your app meets or exceeds any requirements for disclosure, usage rights, deletion requests, etc. 
 - Ensure you're collecting the least amount of personal data to complete your app’s experiences. 
   - Don't collect data “just in case” – there should be a valid reason for collecting data that improves the customer’s experience (or is absolutely necessary for monetization purposes e.g. advertising). 
 - Always get the user’s consent before collecting and storing personal data, other than the most basic unavoidable information (e.g. the IP address used to connect to a service). 
 - Always transmit data over the network using secured connections, e.g. over SSL. 
 - Store collected data in encrypted secure.  
 - Verify that any 3rd-party libraries or SDKs you use also have good privacy practices. Note this is not limited to just advertising SDKs – any library that connects to the internet may impact your app’s privacy. 
   - For every piece of data you collect, store, or useBefore collecting, storing, or using any customer. 
 - Ensure you have an accurate Privacy Policy for your app.


## Appendix on tooling

TODO
