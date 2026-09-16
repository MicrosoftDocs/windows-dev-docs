---
title: Windows developer FAQ
description: Find answers about Windows app frameworks, SDKs, tooling, interoperability, packaging, deployment, performance, compatibility, and migration.
author: GrantMeStrength
ms.author: jken
ms.topic: faq
ms.date: 09/16/2026
ms.localizationpriority: medium
ms.collection: windows11
audience: new-desktop-app-developers
---

# Windows App Development Frequently Asked Questions

This FAQ provides answers to common questions about Windows application development, including guidance on choosing the right framework for your projects.
Topics covered include:

- Getting started and the Windows app development landscape.
- Native Windows-only app development with WinUI 3, Windows Presentation Foundation (WPF), and Windows Forms (WinForms).
- Windows Software Development Kit (SDK) and Windows App SDK.
- Targeting Windows as part of your cross-platform development strategy.
- Hybrid and web app development with .NET MAUI, Blazor, and ASP.NET Core.
- How to choose an approach while understanding Microsoft's investments.

## Windows app development landscape

<details><summary>Where can I find a straightforward overview of Windows development technologies?</summary>

> For an overview of today's options for Windows developers, watch the Windows Dev Chat episode [Choosing your ideal dev platform](https://www.youtube.com/live/4PJBJ8GICjM?si=T1uu4Dm8UKdf6lGn), which discusses WinUI 3, .NET MAUI, React Native, Blazor, and Progressive Web Apps (PWAs). You can find other episodes in the [Windows Dev Chat playlist](https://youtube.com/playlist?list=PLI_J2v67C23bxTffW4XewbUEAOfSVZkrk&si=uARk7gCetDMnrxkJ).
>
> You can also refer to the [overview of app development options](./index.md) for Windows developers.

</details>

<details><summary>Why is client app development still crucial for modern digital transformation in the era of cloud services?</summary>

> In the age of cloud services, client app development remains important for delivering responsive, meaningful interactions on user devices.
>
> Here's why client apps matter:
>
> - **Device reach:** Client apps let you bring your application directly to users on their devices of choice.
> - **Gateway to Intelligent Services:** Client apps are often the first interaction users have with your services. They offer a rich, interactive interface that allows you to showcase intelligent features and differentiate your product from others.
> - **Scalability with Cloud Integration:** A well-integrated client app can sync effortlessly with backend cloud services, enabling real-time data access and seamless scalability as your user base grows.
> - **Enhanced Productivity and User Loyalty:** A thoughtfully designed app can enhance productivity and keep users engaged with your product or service over time.

</details>

## Native Windows-only app development

<details><summary>What is the Windows App SDK?</summary>

> The Windows App SDK provides independently serviced components for Windows desktop apps, including WinUI 3, app lifecycle, windowing, notifications, resources, and text APIs. It supports apps that run on Windows 10, version 1809 and later, subject to the support lifecycle of the Windows release and Windows App SDK version.

</details>

<details><summary>What's the difference between the Windows App SDK and the Windows SDK?</summary>

> Both are software development kits (SDKs) that let you build Windows apps.
>
> The **Windows App SDK** provides components that ship independently from Windows and work across supported Windows releases down to Windows 10, version 1809. It includes WinUI 3 and APIs for app lifecycle, windowing, notifications, resources, text, and other capabilities.
>
> The **Windows SDK** provides headers, libraries, metadata, and tools for operating-system APIs such as Win32, WinRT, COM, DirectX, devices, and shell capabilities.
>
> **The Windows App SDK doesn't replace the Windows SDK.** Apps that adopt the Windows App SDK can continue to use Windows SDK APIs, and WinUI 3 apps commonly use both.

</details>

<details><summary>I'm building a new team to develop a Windows-only app. Why should I choose to develop with a native Windows framework like WinUI 3, WPF, or WinForms?</summary>

> Here are some reasons to choose a native Windows framework for your Windows-only app:
>
> - **Performance:** Native Windows frameworks are optimized to leverage modern Windows hardware, providing fast and responsive user experiences.
> - **Integration:** Windows ships with a wide variety of APIs that enable sophisticated experiences only available on Windows. Native frameworks provide deep integration with these features and APIs.
> - **Native user experience:** Native frameworks provide a consistent experience across Windows devices, ensuring that your app looks and works great everywhere.
> - **Offline support:** Native frameworks support offline scenarios, allowing apps to function even without internet connectivity.
> - **Support and tooling:** Microsoft maintains the native frameworks and provides current SDKs, documentation, debugging tools, and samples.

</details>

<details><summary>Which framework should I use to leverage Microsoft's latest investments in Windows app development?</summary>

> If you're building a new general-purpose Windows desktop app, we recommend using WinUI 3. WinUI 3 is the native UI framework delivered with the Windows App SDK. It supports Windows desktop apps and provides access to current Fluent controls and Windows platform capabilities.

</details>

<details><summary>Can I use Windows App SDK / WinUI 3 in my existing Windows app?</summary>

> Note that [WinUI 3](../winui/winui3/index.md) (a UI framework) ships with the [Windows App SDK](../windows-app-sdk/index.md) (a Windows platform development framework).
>
> You can migrate an app's UI to WinUI 3, or use [WinUI XAML Islands](../desktop/modernize/host-controls-existing-desktop-apps.md) to host Windows App SDK controls in a supported existing desktop host. Legacy system XAML Islands host UWP XAML controls and use different APIs.
>
> Elements of the Windows App SDK can often be used in desktop apps, depending on how the existing app was built. UWP apps are not supported by Windows App SDK.
>
> This means **WPF/MFC/WinForms** apps can use Windows App SDK APIs that are unrelated to WinUI 3. Examples include app lifecycle, windowing, and app notifications.
>
> See [Use the Windows App SDK in an existing project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) for more info.

</details>

<details><summary>Do I need to use Visual Studio to build WinUI 3 apps?</summary>

> No. WinUI 3 XAML builds use MSBuild, but you can build with the .NET SDK and current WinUI 3 templates from the command line in another editor. See the [command-line quickstart](start-here.md?tabs=command-line).
>
> [Visual Studio 2026](/visualstudio/windows/) provides the richest integrated editing, debugging, profiling, and XAML Hot Reload experience. Use the workflow that matches your tooling requirements.

</details>

<details><summary>I get an "Unable to load DLL 'Microsoft.ui.xaml.dll'" error when running my app. How do I fix it?</summary>

> This error usually occurs in **unpackaged** app scenarios where the Windows App SDK runtime hasn't been installed on the machine. Try the following:
>
> - If you're running a **packaged** app (the recommended default), ensure you're launching via Visual Studio with the **MsixPackage** launch profile selected (not the plain executable profile). The MSIX packaging step installs the required runtime components.
> - If you're running a framework-dependent **unpackaged** app, install the matching [Windows App SDK runtime](../windows-app-sdk/downloads.md). A self-contained deployment includes its Windows App SDK dependencies.
> - Confirm that your project matches your deployment model. For a normal .NET unpackaged app, setting `<WindowsPackageType>None</WindowsPackageType>` enables Windows App SDK runtime auto-initialization. Use the bootstrapper API directly only when you need explicit control over dynamic dependency initialization.
>
> See [Deploy apps that use the Windows App SDK](../package-and-deploy/deploy-overview.md) for more details on deployment requirements.

</details>

<details><summary>What is the difference between WinUI 3 and WinUI 2 for UWP?</summary>

> **WinUI 3** is Microsoft's current native UI framework for Windows desktop apps and is delivered as part of the Windows App SDK.
>
> **WinUI 2**, also called **WinUI for UWP**, is a control and styling library for UWP apps. WinUI 2 and WinUI 3 use different XAML namespaces and aren't binary-compatible.
>
</details>

<details><summary>When I build an app using Windows App SDK and WinUI 3, am I building a "WinUI app"?</summary>

> Yes. **WinUI 3 app** is the clearest term for an app whose UI uses WinUI 3 and the Windows App SDK. **WinUI app** is also commonly used when the context is unambiguous.

</details>

<details><summary>Can I incrementally update my UWP app with WinUI for UWP controls to WinUI 3 by gradually replacing the controls?</summary>

> No. Windows App SDK can't be used in UWP apps, and WinUI for UWP can't be mixed with WinUI 3. See [Migrate from UWP to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md).

</details>

<details><summary>How hard is it to migrate a UWP app to WinUI 3?</summary>

> UWP and WinUI 3 share many XAML concepts, but migration isn't a direct namespace change. The cost depends primarily on:
>
> 1. **Project file and MSBuild customization:** Migration effort varies depending on advanced MSBuild usage.
> 2. **.NET API migration:** UWP apps using .NET Native can move to a currently supported .NET release with Native AOT. This modernization is separate from migrating the UI to WinUI 3.
> 3. **UI component libraries:** Libraries must have versions targeting WinUI 3.
> 4. **Windowing and application-model APIs:** UWP APIs tied to concepts such as `CoreWindow`, `ApplicationView`, or `GetForCurrentView` require Windows App SDK replacements or another desktop approach.
> 5. **C++ language projection:** If the UWP app uses the superseded C++/CX projection, port that code to [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/move-to-winrt-from-cx).
>
> For more info, see [Migrate from UWP to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md) and the [UWP to Windows App SDK API mapping](../windows-app-sdk/migrate-to-windows-app-sdk/api-mapping-table.md).

</details>

<details><summary>If I have an existing UWP app in the Store, can I publish a new packaged WinUI 3 app using the same identifiers?</summary>

> Yes, upgraded apps can be published without updating the application identity. Users of the old version will be updated to the new version. This applies to desktop apps only. Xbox, HoloLens, and standard Surface Hub apps cannot migrate to WinUI 3.

</details>

<details><summary>How do I package or distribute my WinUI 3 app?</summary>

> See [Deployment overview](../package-and-deploy/index.md).

</details>

<details><summary>Where can I find Windows App SDK migration guidance?</summary>

> See [Migrate from UWP to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md).

</details>

<details><summary>Do I need to use XAML markup if I want to use WinUI 3?</summary>

> No. UI controls can be created in code. However, representing the UI in declarative XAML markup provides many benefits, including an improved developer experience.
> 
> - Migrating from UWP to WinUI 3: Many XAML and UI concepts carry over, but the namespaces, project model, and some APIs differ.
> - Migrating from WPF to WinUI 3: Many concepts carry over, but the control set and APIs differ.

</details>

<details><summary>Does Visual Studio have a design surface or UI designer for WinUI 3?</summary>

> Not currently. Use [XAML Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload), Live Visual Tree, Live Property Explorer, and related runtime tools to inspect and update XAML while the app runs.
>
> For a complete walkthrough of the runtime design tools available for WinUI 3, see [XAML runtime design tools for WinUI 3](../develop/ui/xaml-runtime-design-tools.md).

</details>

<details><summary>Does Windows App SDK include WinUI 3?</summary>

> Yes. WinUI 3 ships as part of the Windows App SDK.

</details>

<details><summary>Does Windows App SDK include WinUI for UWP?</summary>

> No. WinUI for UWP is part of the UWP platform.

</details>

<details><summary>Are WinUI for UWP and WinUI 3 built on the same technology?</summary>

> Not quite. Although WinUI 3 started from the WinUI for UWP codebase, they are distinct technologies. Both are XAML-based UI frameworks that work across .NET and C++, but WinUI for UWP and WinUI 3 aren't compatible with each other.

</details>

<details><summary>Can I use WinUI 3 without using Windows App SDK?</summary>

> No. WinUI 3 ships as part of the Windows App SDK.

</details>

<details><summary>Can I use WinUI 3 in an unpackaged app?</summary>

> Yes. WinUI 3 and many Windows App SDK APIs work in unpackaged apps. However, some Windows capabilities require package identity, and framework-dependent unpackaged apps must initialize the Windows App SDK runtime. Compare the options in [Packaging overview](../package-and-deploy/packaging/index.md) and [Features that require package identity](../desktop/modernize/modernize-packaged-apps.md).

</details>

<details><summary>What's the difference between XAML Islands and WinUI 3?</summary>

> WinUI 3 is the UI framework included in the Windows App SDK. XAML Islands are a hosting technique that lets an existing desktop app place XAML content alongside UI from another framework.
>
> The term can refer to legacy [system XAML Islands](/windows/uwp/xaml-islands/xaml-islands) that host UWP XAML controls, or to [WinUI XAML Islands](../desktop/modernize/host-controls-existing-desktop-apps.md) that host Windows App SDK controls in supported desktop hosts. The APIs, namespaces, and host requirements differ.

</details>

<details><summary>If I create a WinUI 3 app, will it look modern on both Windows 11 and Windows 10?</summary>

> WinUI 3 controls use Fluent styling on supported versions of Windows 10 and Windows 11, in both packaged and unpackaged apps. Some operating-system effects and behaviors differ by Windows version. For example, Mica is available on Windows 11 and falls back to a solid color on Windows 10.

</details>

<details><summary>Can I use Mica or Acrylic backgrounds in apps built with Windows App SDK?</summary>

> Yes. Desktop Acrylic is supported on Windows 10, version 1809 and later. Mica requires Windows 11 and falls back to a solid theme color on Windows 10. Call `MicaController.IsSupported` or `DesktopAcrylicController.IsSupported` at run time before applying a backdrop. See [Apply Mica or Acrylic materials in desktop apps for Windows 11](../develop/ui/system-backdrops.md).

</details>

<details><summary>Where can I find WinUI 3 samples?</summary>

> See [Sample and resources](../dev-tools/samples.md). Some notable repositories:
> 
> - [WindowsAppSDK-Samples](https://github.com/microsoft/WindowsAppSDK-Samples): Demonstrates how to use specific Windows App SDK API sets.
> - [Windows topic-specific samples](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes): Contains the sample used in the [Build a WinUI 3 notes app](../tutorials/winui-notes/intro.md) tutorial.
> - [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery): Showcases WinUI and Windows App SDK. Also available in the [Microsoft Store](https://apps.microsoft.com/detail/9P3JFPWWDZRC).

</details>

<details><summary>If I have already invested heavily in WPF, should I continue to use WPF or consider migrating to WinUI 3?</summary>

> If you've already invested heavily in WPF, you can continue using it for existing apps. WPF is a mature, stable framework widely used to build Windows desktop apps.
> 
> Use [GitHub Copilot upgrade](/dotnet/core/porting/github-copilot-upgrade/overview) to assess and upgrade a .NET Framework WPF app to modern .NET. Review the generated plan and validate each change in your app.

</details>

<details><summary>If I build a new WPF app, will it look dated compared to other new Windows apps?</summary>

> When developing a WPF application with .NET 9 or later, you can ensure your app matches the sleek, modern look of Windows 11. The new Fluent theme for WPF introduces a contemporary Windows 11 aesthetic, with integrated Light/Dark mode and system accent color support. This modernizes your app’s appearance and delivers a polished, cohesive user experience.

</details>

<details><summary>My team is comfortable building WinForms apps, and it suits our needs. Should we consider migrating to WinUI 3 or another framework?</summary>

> If WinForms meets your needs and your team is comfortable with it, you can continue using WinForms for existing apps. WinForms is a mature and stable framework widely used for Windows desktop development.
> 
> The WinForms team continues to invest in the platform. Recent and ongoing work includes:
> 
> - Asynchronous form and dialog APIs
> - Dark mode and visual-style support
> - Accessibility, high-DPI, layout, and designer improvements
> - Clipboard and `DataObject` modernization

</details>

## Cross-platform native development

<details><summary>What are some reasons for building cross-platform, native apps that target Windows?</summary>

> If you're targeting users across multiple OS platforms, building cross-platform apps with .NET MAUI or React Native can offer several benefits:
> 
> - **Reach:** Cross-platform apps reach a larger audience across different devices and operating systems.
> - **Code reuse:** Reusing code across platforms reduces development time and cost. Building separate apps for Windows, Android, iOS, and macOS can be prohibitively expensive.
> - **Consistent user experience:** Cross-platform frameworks help provide a consistent look and feel across platforms.
> - **Integration:** Cross-platform apps can still integrate with platform-specific services to deliver a comprehensive experience.

</details>

<details><summary>Can I be confident that .NET MAUI apps will run well on Windows?</summary>

> When you build a [.NET MAUI app for Windows](/windows/apps/windows-dotnet-maui/), the output uses WinUI 3. During development, .NET MAUI offers a single .NET experience across platforms, but it generates platform-specific code under the hood.

</details>

<details><summary>How can .NET MAUI provide native device APIs across every platform?</summary>

> .NET MAUI provides a unified .NET experience across Windows, iOS, Android, and macOS. It offers cross-platform APIs for common capabilities such as storage, networking, and device sensors. You can also call platform-specific APIs or provide specialized implementations for each platform.

</details>

<details><summary>Can I start with WinUI 3, and later integrate .NET MAUI if I eventually want to target cross-platform scenarios?</summary>

> Not at this time. Although .NET MAUI uses WinUI 3 when running on Windows, teams expecting to target multiple platforms should start with .NET MAUI or React Native for Desktop.

</details>

<details><summary>Our team has strong web front-end development skills. Should we consider using React Native for Desktop?</summary>

> Teams with strong web development experience may want to consider React Native for Desktop. It includes React Native for [Windows](https://github.com/microsoft/react-native-windows) and [macOS](https://github.com/microsoft/react-native-macos). With the “Learn once, write anywhere” approach, existing JavaScript, TypeScript, and React skills can be used to build native Windows and macOS apps.
> 
> React Native for Desktop renders UI directly to native primitives, delivering native performance and platform capabilities.
> 
> See the [React Native for Desktop documentation](https://microsoft.github.io/react-native-windows/docs/getting-started) to get started.

</details>

<details><summary>Are any other Windows devices supported by React Native for Desktop?</summary>

> React Native for Windows supports the Windows versions listed in its [compatibility documentation](https://microsoft.github.io/react-native-windows/docs/win10-compat). Verify device-family support for the React Native for Windows version you target rather than assuming that every Windows device is supported.

</details>

<details><summary>What should I use if I want to build apps that work on Windows and Xbox?</summary>

> For an Xbox app, use UWP and account for the [Xbox-specific UWP limitations](/uwp/extension-sdks/uwp-limitations-on-xbox). For game development, use the [Microsoft Game Development Kit](https://github.com/microsoft/GDK).

</details>

<details><summary>What should I use if I want to build apps that work on Windows and Surface Hub?</summary>

> For a Surface Hub running the standard Teams Rooms or Surface Hub environment, use a UWP app that meets the [Surface Hub app requirements](/surface-hub/install-apps-on-surface-hub). A [Surface Hub 3 configured with Windows 11 Pro or Enterprise](/surface-hub/surface-hub-3-migrate-os) can run supported desktop app technologies, so UWP isn't the only option in that configuration.

</details>

## Hybrid and web development

<details><summary>What are hybrid apps, and why should I consider building one?</summary>

> Hybrid apps blend the best of web and native app development. Their core is built using web technologies like HTML, CSS, and JavaScript, and wrapped in a native container that gives access to certain native platform features and hardware. They can also be distributed through app stores.
> 
> The main advantage is that hybrid apps allow you to build a single app that can run on multiple native platforms and on the web, reducing development time and cost. Examples of hybrid app development platforms include:
> 
> - Electron for desktop apps
> - Ionic for mobile apps
> - .NET MAUI Blazor Hybrid for cross-platform apps

</details>

<details><summary>How do I build native-feeling progressive web apps (PWAs) on Windows?</summary>

> See [Web development on Windows](../../web/index.md) and [Overview of Progressive Web Apps](/microsoft-edge/progressive-web-apps-chromium/).

</details>

<details><summary>What is a .NET MAUI Blazor hybrid app?</summary>

> With .NET MAUI, Blazor apps can run natively on Windows, iOS, Android, and macOS. This allows you to create hybrid client apps that combine Blazor and .NET MAUI components in a single native client app, with full access to native platform capabilities.
> 
> Learn more at [ASP.NET Core Blazor Hybrid](/aspnet/core/blazor/hybrid/).

</details>

<details><summary>Do the web components of a .NET MAUI hybrid app need to be created with Blazor?</summary>

> No. Starting with .NET 9, .NET MAUI includes a HybridWebView control that allows hosting other JavaScript-based UIs inside a native app.
> 
> This allows you to host Angular, React, Vue, or other HTML/JavaScript apps inside a .NET MAUI app. The hybrid control provides interop between C# and JavaScript, so C# code can call JavaScript functions and vice versa.

</details>

<details><summary>Can any other native app types host Blazor hybrid components?</summary>

> Yes. WPF and WinForms apps can also host Blazor hybrid components, enabling the addition of modern web UI to existing apps. This is not supported for WPF or WinForms apps built on .NET Framework.

</details>

<details><summary>Does my entire app need to be a hybrid app, or can I mix and match native and hybrid components?</summary>

> Native and hybrid components can be mixed within an app. For example, the core of an app may be built with .NET MAUI components while hybrid components provide additional functionality. This allows combining the performance and capabilities of native components with the flexibility and cost efficiency of hybrid components.

</details>

<details><summary>What are my choices for building .NET-based web apps that look great on modern browsers on Windows?</summary>

> Web apps offer the broadest reach of any client app platform. Options for creating beautiful .NET web apps include:
> 
> - ASP.NET Core apps with Razor Pages
> - ASP.NET Core MVC apps
> - ASP.NET Core Blazor apps, with hosting model options:
>   - Blazor WebAssembly
>   - Blazor Server
> 
> Blazor hosting models can now be configured at the component level, enabling scenarios like hosting a Blazor WebAssembly component within a Blazor Server app.
> 
> See the [ASP.NET Core documentation](/aspnet/core/introduction-to-aspnet-core) for more details.

</details>

## Choose an approach and understand Microsoft's investments

<details><summary>There are so many framework options for building apps that target Windows! How do I decide?</summary>

> Windows is an open platform that supports many technologies. Here are some criteria that can help you choose a platform:
> 
> - Are you building Windows-first or cross-platform?
> - What languages or skills do you already have — .NET, JavaScript, something else?
> - Do you need access to Windows-specific APIs?
> - Which framework’s capabilities best match your app’s requirements?
> - See [this table](index.md) for additional comparison factors.
> 
> For many business apps, teams often choose based on existing skills and what the team is most comfortable using.

</details>

<details><summary>How do I choose the best development approach for my web app?</summary>

> Consider the following when choosing a development approach for your web app:
> 
> - Blazor is recommended for building front-end web apps with .NET. It lets you build both the front-end and back-end using .NET, saving time and cost, and it’s especially good for enterprise apps.
> - JavaScript web apps still make sense if you want to leverage existing JavaScript skills or need to integrate with established JS libraries or frameworks.
> - Existing apps using older frameworks like Web Forms, MVC, or Razor Pages remain supported and can continue to be developed and maintained.

</details>

<details><summary>Who is building apps with WinUI 3 today?</summary>

> Microsoft Photos is one documented example. The app migrated from UWP to the Windows App SDK and continues to use WinUI 3. For details about the architecture and migration, see [Microsoft Photos: Migrating from UWP to Windows App SDK](https://blogs.windows.com/windowsdeveloper/2024/06/03/microsoft-photos-migrating-from-uwp-to-windows-app-sdk/).

</details>

<details><summary>Who is building .NET MAUI apps today?</summary>

> Organizations use .NET MAUI to build cross-platform apps for Android, iOS, macOS, and Windows. See examples in the [.NET customer showcase](https://dotnet.microsoft.com/platform/customers/maui).

</details>

<details><summary>Who is building WPF apps today?</summary>

> Most of the Microsoft Visual Studio UI is built with WPF. The [Visual Studio](https://visualstudio.microsoft.com/vs/) IDE itself is a major example of a complex, high-performance WPF app.

</details>

<details><summary>Who is building Blazor apps today?</summary>

> GE Digital’s [FlightPulse](https://customers.microsoft.com/story/816181-ge-aviation-manufacturing-azure) airline system uses Blazor for the backend configuration of everything pilots see, bringing sensor data and analytics directly to pilots to improve safety and efficiency.
> 
> See more [Blazor customer stories](https://dotnet.microsoft.com/platform/customers/blazor) on the .NET site.

</details>

## Language choice (.NET vs C++)

<details><summary>Should I use C# or C++ for my Windows app?</summary>

> **Use C# (.NET)** in most cases. C# offers faster development, memory safety, rich libraries, and excellent tooling. Most Windows apps — including WinUI 3, WPF, WinForms, and .NET MAUI apps — are best built with C#.
>
> **Use C++** when you need direct hardware access, minimal runtime overhead, or interop with existing C++ codebases. Common C++ scenarios include game engines (DirectX), drivers, system-level utilities, and performance-critical components.
>
> | Factor | C# (.NET) | C++ |
> |---|---|---|
> | Development speed | ✅ Faster — managed memory, rich ecosystem | ⚠️ Slower — manual resource management |
> | Runtime performance | ✅ Excellent with modern .NET (AOT, Span\<T\>) | ✅ Best possible — no GC pauses |
> | Memory safety | ✅ Garbage-collected | ⚠️ Manual — risk of leaks and vulnerabilities |
> | Windows API access | ✅ Via C#/WinRT projection | ✅ Via C++/WinRT projection |
> | WinUI 3 support | ✅ Full support | ✅ Full support via C++/WinRT |
> | Cross-platform | ✅ .NET runs on Windows, Linux, macOS | ✅ With platform-specific code |
> | Best for | Business apps, CRUD, services, UI-heavy apps | Games, drivers, system tools, low-latency |
>
> You can also mix both: build your app in C# and call performance-critical native code via [P/Invoke (CsWin32)](../develop/interop/call-win32-apis.md) or a C++/WinRT component.

</details>

<details><summary>How do I call Win32 APIs from C#?</summary>

> Use [CsWin32](../develop/interop/call-win32-apis.md), a source generator that creates type-safe P/Invoke signatures at build time. You add the `Microsoft.Windows.CsWin32` NuGet package, list the APIs you need in a `NativeMethods.txt` file, and call them through a generated `PInvoke` class.
>
> CsWin32 replaces hand-written `[DllImport]` declarations and works in any C# project, including WinUI 3, WPF, WinForms, and console apps. See [Call Win32 APIs from a C# Windows app (CsWin32)](../develop/interop/call-win32-apis.md) for a step-by-step walkthrough.

</details>

<details><summary>What is C++/WinRT and when should I use it?</summary>

> [C++/WinRT](../develop/cpp-winrt/intro-to-using-cpp-with-winrt.md) is a standard C++17 language projection for Windows Runtime APIs. Use it when building Windows apps in C++ that consume or author WinRT APIs. It replaces C++/CX and the Windows Runtime C++ Template Library (WRL).
>
> Choose C++/WinRT when:
> - You're building a C++ WinUI 3 app
> - You need to author Windows Runtime components consumed by other languages
> - You're porting from C++/CX

</details>

<details><summary>What is C#/WinRT and when do I need it?</summary>

> [C#/WinRT](../develop/platform/csharp-winrt/index.md) provides WinRT projection support for C#. In most cases you don't interact with it directly — .NET apps targeting Windows automatically get access to WinRT APIs through target framework monikers (TFMs). You need C#/WinRT explicitly when authoring Windows Runtime components in C# or when generating interop assemblies for third-party WinRT components.

</details>

## Packaging, deployment, and updates

<details><summary>What's the difference between apps that are packaged, unpackaged, and packaged with external location?</summary>

> A **packaged app** contains its files, identity, and deployment information in a package such as MSIX. An **unpackaged app** uses an installer or deployment process outside the Windows package system and doesn't have package identity by default. An app **packaged with external location** uses a small identity package while retaining externally located binaries and its existing installer and update process.
>
> See [Packaging overview](../package-and-deploy/packaging/index.md) for requirements and tradeoffs.

</details>

<details><summary>Do I need package identity?</summary>

> It depends on the Windows features your app uses. Package identity is required for scenarios such as packaged background tasks, share targets, startup tasks, custom context-menu package extensions, manifest-based file-type and protocol associations, and many Windows AI APIs. Windows App SDK push notifications support limited foreground scenarios without identity, but background delivery and COM activation require identity. WinUI 3 and local app notifications can work without package identity.
>
> See [Features that require package identity](../desktop/modernize/modernize-packaged-apps.md). If you need identity but must retain an existing installer, consider [packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md).

</details>

<details><summary>What's the difference between framework-dependent and self-contained deployment?</summary>

> A **framework-dependent** app uses Windows App SDK runtime packages installed separately on the device. This reduces the app's deployment size and lets the installed framework receive servicing updates. A **self-contained** app carries its Windows App SDK dependencies with it, which increases deployment size and makes the app publisher responsible for distributing Windows App SDK servicing updates with new app versions.
>
> APIs that depend on additional MSIX packages, such as the Singleton package, can require separate deployment or runtime support checks even in a self-contained app. Packaging and runtime deployment are separate decisions. See [Windows App SDK deployment overview](../package-and-deploy/deploy-overview.md).

</details>

<details><summary>Will my WinUI 3 app automatically update for end-users?</summary>

> A WinUI 3 app can be delivered through the Microsoft Store, an `.appinstaller` file, or an MSI or setup executable. Store packages can be updated through Microsoft Store servicing, subject to Store and organizational settings. An `.appinstaller` deployment supports automatic updates only when its `UpdateSettings` configure launch-time or background checks. MSI and setup deployments must provide or integrate their own update mechanism.

</details>

<details><summary>Can I use Windows App SDK without using MSBuild?</summary>

> Yes, for some scenarios. WinUI 3 XAML projects currently require [MSBuild](/visualstudio/msbuild/msbuild), although Visual Studio isn't required and `dotnet build` can invoke MSBuild from the command line. You can use non-XAML Windows App SDK APIs from C++ and CMake projects through the preview [Windows App Development CLI](../dev-tools/winapp-cli/guides/cpp.md#6-using-windows-app-sdk-optional), or [integrate the runtime manually](../windows-app-sdk/use-windows-app-sdk-run-time.md).

</details>

## Windows AI

<details><summary>How do I choose between Windows AI APIs, Foundry Local, and Windows ML?</summary>

> The first three technologies are part of **Microsoft Foundry on Windows**. You can combine them with each other and with cloud models in the same app:
>
> - Use [Windows AI APIs](/windows/ai/apis/) for ready-to-use capabilities whose models and hardware acceleration Windows manages.
> - Use [Foundry Local](/windows/ai/foundry-local/get-started) to discover, download, and run supported open-source language and speech models locally.
> - Use [Windows ML](/windows/ai/new-windows-ml/overview) to run your own ONNX models with execution providers for available CPU, GPU, and NPU hardware.
> - Use [Microsoft Foundry](/windows/ai/cloud-ai), a separate cloud AI platform, when you need cloud-hosted models, retrieval, centralized governance, or capabilities that aren't available on the target device.
>
> Compare the options in [Choose your Windows AI solution](/windows/ai/windows-ai-comparison). Consider model capability, privacy, connectivity, latency, hardware coverage, deployment size, and operating cost.

</details>

<details><summary>Do Windows AI features require a Copilot+ PC?</summary>

> Not all of them. Many Windows AI APIs require a Copilot+ PC, but some APIs also support specific GPUs or CPUs. Foundry Local and Windows ML support broader hardware configurations, subject to their current operating-system, model, runtime, and execution-provider requirements.
>
> Check the [Windows AI API hardware table](/windows/ai/apis/#supported-hardware) and the requirements for the specific API or model. Detect support and model readiness at run time, and provide a non-AI, local-model, or cloud fallback when the feature is unavailable.

</details>

<details><summary>Can Windows AI features run locally and offline?</summary>

> Yes. Windows AI APIs, Foundry Local, and Windows ML can run inference on the user's device, which can reduce latency and keep input data local. Some models or execution providers must first be downloaded or provisioned and can require an internet connection during setup or servicing. Cloud AI services require connectivity and send data to the service according to its data-handling terms.
>
> Tell users when a model download is required and when data leaves the device. Don't describe a feature as offline-capable until you've tested its complete first-run, update, and fallback experience.

</details>

<details><summary>Can AI tools help me build or modernize a Windows app?</summary>

> Yes. AI coding agents can help scaffold projects, explain APIs, migrate code, generate tests, and diagnose build problems. Use the [AI-assisted Windows development](../develop/ai-assisted/index.md) guidance for GitHub Copilot, the WinUI agent plugin, the Microsoft Learn MCP Server, migration workflows, and AI-assisted testing.
>
> Review and test generated code as you would any other contribution. In particular, verify API names and versions, package capabilities, security-sensitive code, accessibility, and any UWP-to-WinUI 3 substitutions.

</details>

<details><summary>What should I consider before shipping an AI-assisted feature?</summary>

> Define the feature's intended use and limitations, evaluate quality and safety with representative data, disclose AI behavior where appropriate, protect user data, and provide a fallback when the model or required hardware isn't available. Keep secrets and privileged service credentials out of client apps, and require user confirmation before consequential or irreversible actions. See [Responsible generative AI development on Windows](/windows/ai/rai/) and [Security and responsible AI for Windows development](../develop/ai-assisted/security-and-responsible-ai.md).

</details>

## Performance and optimization

<details><summary>What can I do to make my Windows app feel great to end-users?</summary>

> See [Windows application development - Best practices](./best-practices.md) and [Windows app performance and fundamentals overview](../develop/performance/index.md).

</details>

## Compatibility

<details><summary>Will my users ever have to update Windows to use my WinUI 3 app?</summary>

> The Windows App SDK has a minimum compatible OS of Windows 10, version 1809, build 17763. Microsoft support requires a supported Windows App SDK release with its latest servicing update and a Windows edition, version, and servicing channel that is still supported. Individual APIs can require a newer Windows version or specific hardware. See [Windows App SDK support](/windows/apps/windows-app-sdk/support) and [Release channels](/windows/apps/windows-app-sdk/release-channels).

</details>

<details><summary>Can I target Arm64 with my WinUI 3 app?</summary>

> Yes. Build a native Arm64 app for the best performance and efficiency. For a large C++ codebase with x64 dependencies, [Arm64EC](/windows/arm/arm64ec) lets you migrate modules incrementally. Windows 11 on Arm can also run many existing x86 and x64 apps through Prism emulation, but you should test performance and compatibility on representative Arm devices.

</details>

## Deprecations and migrations

<details><summary>Are UWP / WinUI for UWP deprecated?</summary>

> UWP and WinUI 2 aren't formally deprecated. Visual Studio 2026 supports UWP with modern .NET and Native AOT, while WinUI 2.8 remains the latest stable WinUI release for UWP. However, Microsoft recommends WinUI 3 and the Windows App SDK for new general-purpose Windows desktop apps.
>
> UWP support for modern .NET with Native AOT is generally available and is the default C# UWP project type in Visual Studio 2026. Moving an existing UWP app from .NET Native to modern .NET is a separate modernization step from migrating its UI to WinUI 3. See [Modernize your UWP app with .NET and Native AOT](/windows/uwp/dotnet-native/modernize-uwp-apps-with-dotnet).

</details>

<details><summary>When should I migrate a UWP / WinUI for UWP app to WinUI 3?</summary>

> UWP developers should not feel pressured to migrate if they are satisfied with UWP and its feature set — for many apps, the right choice may be to stay on UWP.
> 
> Apps that want to benefit from the latest Windows platform and .NET investments should consider moving to WinUI 3 and the Windows App SDK. See [Migrate from UWP to the Windows App SDK](../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md).

</details>

<details><summary>When should I *not* migrate a UWP + WinUI for UWP app to WinUI 3?</summary>

> Continue using UWP when your target device or app model requires it, such as Xbox apps, HoloLens 2D apps, or apps for the standard Surface Hub environment. [Windows IoT Enterprise supports desktop app technologies](/windows/iot/iot-enterprise/development/app_dev), including the Windows App SDK, so an IoT target isn't by itself a reason to use UWP.

</details>

<details><summary>Is WPF deprecated?</summary>

> No. WPF is supported and continues to receive feature, performance, accessibility, and Fluent-style improvements in modern .NET. It remains a good choice for existing WPF apps and for new apps whose requirements fit WPF. For new general-purpose Windows desktop apps, Microsoft's primary recommendation is WinUI 3 with the Windows App SDK. See the [WPF roadmap on GitHub](https://github.com/dotnet/wpf/blob/main/roadmap.md).

</details>

<details><summary>Is WinForms deprecated?</summary>

> No. WinForms is supported and continues to receive feature updates. See the [Windows Forms Roadmap on GitHub](https://github.com/dotnet/winforms/blob/main/docs/roadmap.md).

</details>

<details><summary>Is the Windows Runtime (WinRT) deprecated?</summary>

> No. [WinRT](/windows/uwp/winrt-components) is an application binary interface (ABI) that enables interop across multiple languages. WinRT is the evolution of COM, and the Windows App SDK provides most of its functionality through WinRT APIs.

</details>

## Release notes

<details><summary>Where can I find release notes for Windows App SDK?</summary>

> See the [Windows App SDK release notes](../windows-app-sdk/release-notes/windows-app-sdk-2-0.md) for stable, preview, and experimental releases. The [What's new for Windows developers](../whats-new/whats-new-for-developers.md) page summarizes the latest Windows SDK, Windows App SDK, WinUI 3, tooling, and platform updates.

</details>

## Related content

- [Windows developer glossary](windows-developer-glossary.md)
- [Overview of app development options](./index.md)
