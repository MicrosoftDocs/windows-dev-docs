---
title: Windows Application Development - Best Practices
description: A collection of best practices related to UI/UX, security, performance, and more.
ms.topic: article
ms.date: 05/16/2022
ms.author: jken
author: grantmestrength
ms.localizationpriority: medium
ms.collection: windows11
---

# Windows Application Development - Best Practices

The best practices in this document will help you build great Windows apps that reach and delight ~1.5 billion diverse PC users around the world. This document is divided into the following sections:

1. **[User experience](#user-experience-ux)**: Guidance in this section will help you improve the look, feel, and usability of your apps.
2. **[Performance and fundamentals](#performance--fundamentals)**: Guidance in this section will help you improve your app's performance and resource utilization. 
3. **[Operating system / hardware optimization](#operating-system--hardware-optimization)**: Guidance in this section will help you optimize your packaging and distribution for a variety of hardware configurations.
4. **[Application discovery and management](#application-discovery-and-management)**: Guidance in this section will make it easier for users to discover, install, update, and uninstall your app.
5. **[Accessibility](#accessibility)**: Guidance in this section will help you build accessible and inclusive experiences.
6. **[Security and privacy](#security-and-privacy)**: Guidance in this section will help you mitigate security risks and meet your users' privacy needs.



### User experience (UX)

**How can I improve the look, feel, and usability of my Windows apps?**

Windows 11 marks a visual evolution of the Windows operating system that improves the look, feel, and usability of Windows. Our studies show that users have high expectations for Windows apps:

- They expect Windows apps to work with a complete range of inputs.
- They expect design and interaction patterns that look and feel native on current and future devices.
- They expect support for modern windowing workflows and shell integration points.

When applications adhere to Windows styles and standard Windows behaviors, users don't have to re-learn interaction patterns. This makes it much easier for users to use your app. An app that looks great can create a great first impression, but an app that's also easy to use and helps the user accomplish their goals will create a great lasting impression.

Windows 11 was built on the [Windows 11 design principles](/windows/apps/design/signature-experiences/design-principles). Following these guidelines as you build your apps will help you meet your customers' expectations of a great app experience. The following resources will help you incorporate the latest and recommended Windows application UI/UX patterns into your Windows applications.

#### Common controls

Use the latest **[common controls](/windows/apps/get-started/make-apps-great-for-windows#4-use-the-latest-common-controls)** to get the benefits of compatibility and accessibility by default. [WinUI](/windows/apps/winui/) provides new styles for common controls, and the default styles have been updated with new visuals and animations. If you can't use WinUI, you can copy the styles from the [design toolkits](https://www.figma.com/community/file/989931624019688277) and [WinUI Gallery](https://aka.ms/xamlcontrolsgallery).

#### Materials: Acrylic and Mica

[Materials](/windows/apps/design/signature-experiences/materials) are visual effects applied to UX surfaces that resemble real life artifacts. [Acrylic](/windows/apps/design/style/acrylic) and [Mica](/windows/apps/design/style/mica) materials are used as base layers beneath interactive UI controls. Use [Acrylic](/windows/apps/design/style/acrylic) for transient surfaces that light-dismiss, like context menus. [Mica](/windows/apps/design/style/mica) is a very performant material that is meant to be used on long-lived UI surfaces like the title bar to communicate the active or inactive state of the app.

More information about Acrylic and Mica materials can be found in [Things you can do to make your app great on Windows 11](/windows/apps/get-started/make-apps-great-for-windows#5-use-the-latest-design-materials-acrylic-and-mica).


#### Dark and Light themes

[Dark and Light themes](/windows/apps/get-started/make-apps-great-for-windows#7-support-dark-and-light-themes) give users a way to adapt your app to their visual preferences. Windows 11 updates the color tones to be softer on the eyes by avoiding pure white and black, which makes the colors much more delightful. WinUI supports switching between Dark and Light themes by default (see [XAML theme resources](/windows/apps/design/style/xaml-theme-resources)). For Win32 apps, see [Support Dark and Light themes in Win32 apps](/windows/apps/desktop/modernize/apply-windows-themes).

#### Iconography and typography

Windows 11 has [updated icons ("Segoe Fluent Icons")](/windows/apps/design/signature-experiences/iconography), improved support for [animated icons](/windows/apps/design/controls/animated-icon), and a [new UI font ("Segoe UI Variable")](/windows/apps/design/signature-experiences/typography). We recommend that you use these new icons and font whenever possible to be coherent on Windows 11. The new font brings much softer geometry and makes the text much more legible.

More information about iconography and typography on Windows can be found in [Things you can do to make your app great on Windows 11](/windows/apps/get-started/make-apps-great-for-windows#9-use-beautiful-iconography--typography).


#### On-object commanding

Use [on-object commanding](/windows/apps/design/controls/collection-commanding#creating-context-menus) such as [context menus](/windows/apps/design/controls/menus-and-context-menus), [swipe commands](/windows/apps/design/controls/swipe), and [keyboard shortcuts](/windows/apps/design/input/keyboard-accelerators). It's important to make app commands available in various ways to support all users and input types.

- **[Context menu integration](/windows/apps/get-started/make-apps-great-for-windows#context-menus)**
 
  For Windows 11, we improved the behavior of the right-click context menu. If your app creates context menus, you may need to make some changes to ensure that these work well with Windows 11.

- **Text editing**
  
  Anywhere a user can edit text, you should support Cut/Copy/Paste commands and ensure that these commands are exposed by all input types. WinUI text controls do this by default, but you might need to do some extra work if you're not using WinUI.

#### Geometry and app silhouettes

[Windows 11 geometry](/windows/apps/design/signature-experiences/geometry) has been crafted to support modern app experiences. Progressively rounded corners, nested elements, and consistent gutters combine to create a soft, calm, and approachable effect that emphasizes unity of purpose and ease of use. Another feature of [Windows 11 app silhouettes](/windows/apps/design/basics/app-silhouette) is the integration of app and title bar content.

- **[Title bar integration](/windows/apps/design/basics/titlebar-design)**
  
  Use the WindowsAppSDK APIs to [integrate app content with the title bar](/windows/apps/develop/title-bar). You can use these APIs with WinUI 3, Win32, and .NET apps.

- **[Rounded corners](/windows/apps/get-started/make-apps-great-for-windows#6-use-rounded-corners-for-your-windows-and-support-snap-layouts)**
  
  In most cases, your app's window will have rounded corners by default on Windows 11. If you've customized your app window and don't have rounded corners, see [Apply rounded corners in desktop apps for Windows 11](/windows/apps/desktop/modernize/apply-rounded-corners) for some things you can do. You should also avoid customizing window borders and shadows, which can prevent the system from rounding the window corners.

#### **Page layout**

The most important thing to remember in relation to page layout is that your app window can be resized to many shapes and sizes and run on devices with different DPI and scale settings. Content and commands should not disappear when the app is resized, especially to smaller sizes like `800x600`.

- **Responsive layout**
  
  Use [responsive design techniques](/windows/apps/design/layout/responsive-design) to optimize your app pages for different window sizes. Follow the [guidelines for panning or scrolling](/windows/apps/design/input/guidelines-for-panning) to ensure that users can always access your content, no matter how small the app window gets.

- **[Snap layouts](/windows/apps/get-started/make-apps-great-for-windows#6-use-rounded-corners-for-your-windows-and-support-snap-layouts)**
  
  Snap layouts are a new Windows 11 feature to help introduce users to the power of window snapping. Use the snap layouts menu to test your app in different snap layouts an ensure your app supports different snap sizes (1/2, 1/3, 1/4 screen). 
  
  If the snap layouts menu doesn't appear for your app by default, see [Support snap layouts for desktop apps on Windows 11](/windows/apps/desktop/modernize/apply-snap-layout-menu) for some steps you can take to enable it.

- **DPI awareness**
  
  WinUI applications automatically scale for each display that they're running on. Other Windows programming technologies (Win32, WinForms, WPF, etc.) don't automatically handle DPI scaling so you need to do some additional work. Without this work, applications will appear blurry or incorrectly-sized in many common usage scenarios. For information about what is involved in updating a desktop application to render correctly, see [High DPI Desktop Application Development on Windows](https://docs.microsoft.com/windows/win32/hidpi/high-dpi-desktop-application-development-on-windows).

#### Toast notifications

[Toast notifications](/windows/apps/design/shell/tiles-and-notifications/toast-ux-guidance) are the Windows notifications that appear in the lower right of the user’s screen and the Notification Center.

Following toast notification best practices can help you drive engagement with your app:

 - Notifications should be personalized, actionable, and useful to your users. Try to give your users what they want, not what you want them to know.
 - Notifications shouldn't be noisy. Too many interruptions from your app leads to users turning off this critical communication channel for your app.
 - Respond to the user's intent. Selecting a notification should launch your app in the notification’s context. The only exception to this guideline is when the user selects a button on your notification that's attached to a background task, such as a quick reply.
 - Provide a consistent Notification Center experience. Keep Notification Center tidy by clearing out old notifications.

For more information about toast notifications, see [Notifications design basics](/windows/apps/design/shell/tiles-and-notifications/toast-ux-guidance).

### Performance & fundamentals

**How can I optimize my app for better performance, memory usage, responsiveness, power consumption, and reliability?**

Windows users expect Windows apps to exhibit great performance and fundamentals. As you design and build your app, it is important to keep in mind optimizing for memory usage, power consumption, responsiveness, reliability, and the impact on long-term sustainability. Allocating time to test and measure your application's fundamentals and performance will ensure that your users have a first-class experience.

Following the best practices in this section will help you meet your customers' expectations across these criteria.

- [Minimize application memory usage](../performance/disk-memory.md):
  - Reduce foreground memory usage.
  - Minimize background work.
  - Release resources while in the background.
  - Ensure your application does not leak memory.

- [Make efficient use of the disk footprint](../performance/disk-memory.md#efficiently-use-disk-space):
  - Enable “pay for play” for optional functionality.
  - Ensure any caches are sized efficiently.
  - Implement new experiences in a disk-efficient manner.
  - Optimize individual binary sizes where possible.

- [Improve power consumption and battery life by minimizing background work](../performance/power.md):
  - Do not wake the CPU or use system resources while in the background.

- [Improve the responsiveness of your app's launch and key interactions](../performance/responsive.md)
  - Define your key interaction scenarios and add ETW events to measure.
  - Set goals based on the interaction class associated with user expectations.

To learn more, see the [Performance and fundamentals overview](/windows/apps/performance/), which will cover questions such as "What is application performance and why is it important?" or "What tools can I use to measure Windows application performance?", as well as linking to case studies, related blogs, support communities, and information on how performance engineering intersects with sustainability--reducing the impact your application will have on our planet.

### Operating system / hardware optimization

**How can I optimize my Windows app to take advantage of operating system and hardware configurations?**

Windows apps can be built, packaged, and delivered in a variety of ways. The best practices in this section will help you optimize these aspects of your application across hardware configurations.

#### MSIX app attach and Azure Virtual Desktop

If you want your app to run best in an enterprise environment, add support for MSIX app attach.

[MSIX app attach](https://docs.microsoft.com/azure/virtual-desktop/what-is-app-attach) lets you deliver MSIX applications to both physical and virtual machines. It's made specifically for [Azure Virtual Desktop](https://docs.microsoft.com/azure/virtual-desktop/overview) (AVD), a desktop and app virtualization service that runs on the cloud. Using MSIX app attach with AVD can help you improve sign-in times for users, and it can reduce infrastructure costs for your enterprise.  

#### Windows on ARM

Windows can run on ARM devices. ARM PCs benefit from extended battery life and integrated support for mobile data networks. These PCs also provide great application compatibility and allow you to run your existing `x86` and `x64` applications unmodified. 

For best performance, you should enable your apps to take full advantage of the energy-efficient ARM processor architecture by either building a full ARM version or by optimizing the parts of the codebase that would benefit most from native performance. For more information on these techniques, refer to [Windows on ARM](/windows/uwp/porting/apps-on-arm) and [ARM64EC for Windows 11 apps on ARM](/windows/uwp/porting/arm64ec).


#### Push notifications

[Push notifications](/windows/apps/windows-app-sdk/notifications/push/index) allow you send information from your cloud service to your app in a performance-optimized way. Push notifications include raw notifications, badge notifications, and toast notifications sent from your cloud service.

- Use raw notifications (shoulder taps) to wake up the app/client rather than always keeping it running to optimize performance on the user’s device.
- Notification channels are not meant to be used to send advertisements.  
- Respect `retry-after` headers – this protects our service and ensures notification delivery success.
- Remove expired/revoked channels from the system. [Windows Notification Service](/windows/apps/design/shell/tiles-and-notifications/windows-push-notification-services--wns--overview) (WNS) does not process requests for expired/revoked channels.
- Avoid sudden, large bursts of requests to WNS. This can lead to throttled responses.
- Utilize the `MS-CV` header. This will help with end-to-end traceability and diagnostics.
- Have a back-up mechanism for when notifications don’t work. 
- Use [Azure Notification Hubs](https://docs.microsoft.com/azure/notification-hubs/notification-hubs-push-notification-overview) (ANH). ANH gives you access to engagement features like targeting audiences, scheduling notifications, and broadcasting notifications. If you're a Windows-only developer today, using ANH will make it easy for you to transition your notifications infrastructure to other platforms in the future.
- We do not encourage the use of push notifications for tiles - Windows 11 no longer supports this pattern. 


### Application discovery and management

**How do I make my app discoverable and easy to install/update/uninstall?**

Application discovery and installation are the first two interactions that a user will have with your application. Reliable installation, update, and uninstallation experiences are important pieces of a consistent, high-quality user experience. The following best practices will help ensure that your application leaves a good impression when discovered and managed by users:
 
#### Application discovery

  - Listing your app on [Microsoft Store](https://blogs.windows.com/windowsexperience/2021/06/24/building-a-new-open-microsoft-store-on-windows-11/) can make your app more discoverable for users.   
  - If you're hosting your app across multiple channels (for example - on a website and on the Microsoft Store), your application should have a consistent application identity and update mechanism across all channels.
  - Adding your app to the Microsoft Store will also make it available in the Windows Package Manager [WinGet](/windows/package-manager/winget/). If you don't publish to the Microsoft Store, you can still make your app easily discoverable in winget via the [winget repository](/windows/package-manager/package/).

#### Installation and uninstallation

  - Support a per-user install. This will enable users to install more easily and avoid UAC prompts.
  - Ensure that your application's installation is error free, transparent, and thoughtful about its file management. Your application's installation shouldn't leave any temporary files behind.   
  - Avoid requiring elevated permissions to install and requiring operating system reboots when possible.
  - Support silent installation. This is important for app manageability in enterprise environments.
  - Ensure your app is listed in the Apps -> Installed Apps list.
  - Consider using MSIX to ensure that users experience a seamless installation, update, and uninstallation experience. MSIX automatically removes the app binaries and data. For information about how packaged apps handle files and registry entries, see [Understanding how packaged desktop apps run on Windows](/windows/msix/desktop/desktop-to-uwp-behind-the-scenes). 
  - For unpackaged apps, ensure that your application can be easily uninstalled through the Apps -> Installed Apps list in Settings. When your application is uninstalled, ensure that Start menu entries, files, directories, registry entries, and temporary files are also removed. Consider giving your users the option to preserve their data when they uninstall your application.  
  - Ensure that during uninstallation your app removes all binaries and application data. User-created content should be stored in locations like `Documents`, which can then be retained by users even after the app is uninstalled.
  - Avoid installing or updating system binaries that may require a reboot.   
  - Integrate with [RestartManager](/windows/win32/rstmgr/about-restart-manager) to save and restore state between OS updates.

#### Updates

  - Support an update mechanism that allows your app to restart when its convenient for the user. Consider using the Windows App SDK Restart APIs to manage app behavior. 
  - Ensure that your update mechanism downloads only the essential changed components that need to be updated. This can minimize the network bandwidth required.  
  - Ensure that you provide a way to update and repair your app. Consider MSIX, which automatically handles update repair. For more information, see [Auto-update and repair apps](/windows/msix/app-installer/auto-update-and-repair--overview).
  - Consider push notification-based updates or checking for available updates at app startup or at restart. 



#### Additional resources
  - [MSIX documentation](/windows/msix/)
  - [Windows Installer Best Practices](/windows/win32/msi/windows-installer-best-practices)

### Accessibility

**How do I ensure that my Windows apps are accessible and inclusive?**

Accessible Windows applications support rich and [inclusive experiences](https://www.microsoft.com/design/inclusive/) for as many people as possible, including those with disabilities (both temporary and permanent), personal preferences, specific work styles, or situational constraints (such as shared work spaces, driving, cooking, glare, and so on).

In fact, the [World Health Organization](https://www.who.int/news-room/fact-sheets/detail/disability-and-health) defines disability not as a personal characteristic, but rather as a mismatched interaction between a person and the physical and digital world around them.

> #### Accessibility is good for both people and business
>
> **Accessibility is a responsibility**
>
> More than 1 billion people worldwide experience some form of disability. However, only 1 in 10 have access to the assistive technology needed to fully participate in our economies and societies. Typically, the unemployment rate for people with disabilities is twice that of people without a disability. And disabilities—whether situational, temporary, or permanent—can affect any of us at any time.
>
> **Accessibility is an opportunity**
>
> According to the [Microsoft Accessibility Approach Datasheet](https://query.prod.cms.rt.microsoft.com/cms/api/am/binary/RE4wNu4): Inclusive organizations that embrace best practices for employing and supporting persons with disabilities in the workplace outperform their peers and do better at attracting and keeping top talent. Millennials, who will be 75% of the global workforce by 2020, typically choose employers who reflect their values. Diversity and inclusion top that list.

#### Incorporating accessibility

Incorporating accessibility into your Windows apps can maximize user engagement, increase product satisfaction, and encourage product loyalty. Proactively designing and implementing accessible experiences typically results in reduced development and maintenance costs over the long-term.

For detailed guidance on building accessible Windows apps, see [Accessibility in Windows 11 and Windows 10](../develop/accessibility.md).

#### Accessibility testing

**Accessibility Insights** is a powerful suite of tools for developers to test the accessibility of their apps and services. Here are some tools to leverage in testing accessibility:

1. [Inspect in Accessibility Insights for Windows](https://accessibilityinsights.io/docs/windows/getstarted/inspect/). Inspect the accessibility tree to find low-hanging fruit like hints in labels, incorrect roles, etc.
2. [Event monitoring in Accessibility Insights for Windows · Accessibility Insights](https://accessibilityinsights.io/docs/en/windows/getstarted/eventmonitoring/). See [Supporting UI Automation Control Types](/windows/win32/winauto/uiauto-supportinguiautocontroltypes) for more info on event monitoring.
3. Run Accessibility Insights automated checks in your PRs or CI/CD.  For more info, see [axe-pipelines-samples](https://github.com/microsoft/axe-pipelines-samples).
4. Fix all bugs you find, they all have direct impact on accessibility.

### Security and privacy

**How do I ensure that my app is secure?**

An insecure application can be an entry point that allows an attacker to perform malicious activities. Even if your app doesn't have security bugs, attackers can use your app to initiate their attacks through phishing and other forms of social engineering that violate security and privacy boundaries. The best practices in this section will help you mitigate risks related to security and user privacy.

#### Security guidelines

- Follow the [Security Development Lifecycle](https://www.microsoft.com/securityengineering/sdl/) for all development.
  - **Threat modeling** can help you avoid security flaws.
  - Using **secure libraries, languages, and tools** minimizes implementation flaws.
  - **Secure defaults** can prevent security issues caused by user error.
- Don't require administrative privileges to _install_ your app.
  - Ideally, your app should support both administrative installs and per-user installs.
  - Using [MSIX packaging](/windows/msix/packaging-tool/tool-overview) is one way to achieve this.
- [Don't require administrative privileges to _run_ your app.](/windows/win32/win7appqual/standard-user-analyzer--sua--tool-and-standard-user-analyzer-wizard--sua-wizard-)
  - If there are certain features that need administrative privileges, [consider separating them](/windows/win32/secauthz/developing-applications-that-require-administrator-privilege) into their own processes to **reduce attack surface**.
- Consider using techniques such as **AppContainer** (UWP) or **[process attribute flags](/windows/win32/api/processthreadsapi/nf-processthreadsapi-updateprocthreadattribute)** to mitigate risk of vulnerabilities.
  - This may require separating your code into a regular UI process and a more-secure child process where you can execute especially risky code like parsing untrusted data.
- Prefer to use languages with **guaranteed memory safety** (such as C#, JavaScript, or Rust), especially for risky code paths (like parsing untrusted data).
- Use all security mitigations provided by your compiler and toolset (see [Security Features In Microsoft Visual C++](https://devblogs.microsoft.com/cppblog/security-features-in-microsoft-visual-c/) for Visual C++).
- Always use your chosen language or framework's standard libraries for cryptography and other security-sensitive code. _Do not try to build your own._
- **Digitally sign all components of your application** – not just the installer, but also the uninstaller (if you have one). Also sign all the EXE, DLL, and other executable files that make up your app.
  - Digital signatures enable the user to **verify the authenticity of your app** and allow Enterprise admins to secure their devices using [Windows Defender Application Control](/windows/security/threat-protection/windows-defender-application-control/wdac-and-applocker-overview).
  - Using MSIX packaging is one way to achieve this.
- Ensure all network communication is over a secure transport, such as SSL.
- Provide guardrails or other mitigations that can help **protect users from accidentally performing harmful actions**, even when coerced into doing so by attackers.
  - Simple "Are you sure you want to do _X_? _Yes / No_" dialogs are typically not effective, because users have been conditioned to click "Yes."

**How do I ensure that my app follows appropriate privacy practices?**

Most modern apps collect and use a large amount of data – including personal data – for various reasons. Telemetry, product improvement, and monetization are three common reasons for using data, but users and regulators alike are becoming more sensitive to the privacy implications of these practices. They expect transparency and control over the data collected and used by apps. Use the following tips to help meet the privacy needs of your users.

#### Privacy guidelines

- **Ensure that your app provides an accurate Privacy Policy.** Ideally, provide both a summary document written for a casual audience (your users) in addition to a long-form legal policy (written for your lawyers).
- **Familiarize yourself with privacy regulations** in the markets where your app will be available, and ensure your app meets or exceeds any requirements for disclosure, usage rights, deletion requests, etc.
- **Consider using technologies such as AppContainer (UWP)** to automatically minimize the amount of data available to your app. If your app is blocked from accessing data, it's impossible for your app to collect it, even accidentally (e.g. due to a bug in your code or a data-hungry 3rd party library).
- **Ensure you're collecting the least amount of personal data needed** to complete your app's experiences.
  - **Don't collect data "just in case"** – there should be a valid reason for collecting all data, e.g. to improve the customer's experience or to facilitate monetization.
- **Always get the user's consent** before collecting and storing personal data and provide the user with an easy way to revert their decision in the future. Avoid "[dark patterns](https://www.reuters.com/legal/legalindustry/dark-patterns-new-frontier-privacy-regulation-2021-07-29/#:~:text=Some%20examples%20of%20dark%20pattern%20usage%20include)" such as making the "Yes" button larger or more prominent than the "No" button in a consent dialog.
  - **Consult with applicable regulations** to determine what specific disclosures and consent is required for specified kinds of data. For example, some regions may allow users to view, change, or delete the data you have stored about them.
- If you must transmit data over the network, **always use secured connections**, e.g. over TLS.
- **Avoid storing personal data in a centralized location** (e.g. website). If you must store personal data, minimize the amount of data you store, store it only for as long as strictly necessary, and ensure it is securely encrypted.
- **Verify that any 3rd-party libraries or SDKs you use also have good privacy practices.** Note this is _not_ limited to just advertising SDKs – any library that connects to the internet may impact the privacy of your app's users.


## Conclusion

Building applications on Windows gives you access to over a billion devices and users worldwide. Implementing the best practices in this document will help you reduce costs, reach more users, improve discoverability, maintan accessibility, increase engagement, and reduce risk.

## Related articles

- [Windows Dev Center](https://developer.microsoft.com/windows/)
- [What's cool in Windows 11](/windows/windows-for-developers/)
