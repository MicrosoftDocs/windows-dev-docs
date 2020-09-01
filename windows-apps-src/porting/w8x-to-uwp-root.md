---
description: If you have a Universal 8.1 app&\#8212;whether it's targeting Windows 8.1, Windows Phone 8.1, or both&\#8212;then you'll find that your source code and skills will port smoothly to Windows 10.
title: Move from Windows Runtime 8.x to UWP'
ms.assetid: ac163b57-dee0-43fa-bab9-8c37fbee3913
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Move from Windows Runtime 8.x to UWP


If you have a Universal 8.1 app—whether it's targeting Windows 8.1, Windows Phone 8.1, or both—then you'll find that your source code and skills will port smoothly to Windows 10. With Windows 10, you can create a Universal Windows Platform (UWP) app, which is a single app package that your customers can install onto every kind of device. For more background on Windows 10, UWP apps, and the concepts of adaptive code and adaptive UI that we'll mention in this porting guide, see [Guide to UWP apps](../get-started/universal-application-platform-guide.md).

While porting, you'll find that Windows 10 shares the majority of APIs with the previous platforms, as well as XAML markup, UI framework, and tooling, and you'll find it all reassuringly familiar. Just as before, you can still choose between C++, C#, and Visual Basic for the programming language to use along with the XAML UI framework. Your first steps in planning exactly what to do with your current app or apps will depend on the kinds of apps and projects you have. That's explained in the following sections.

## If you have a Universal 8.1 app

A Universal 8.1 app is built from an 8.1 Universal App project. Let's say the project's name is AppName\_81. It contains these sub-projects.

-   AppName\_81.Windows. This is the project that builds the app package for Windows 8.1.
-   AppName\_81.WindowsPhone. This is the project that builds the app package for Windows Phone 8.1.
-   AppName\_81.Shared. This is the project that contains source code, markup files, and other assets and resources that are used by both of the other two projects.

Often, an 8.1 Universal Windows app offers the same features—and does so using the same code and markup—in both its Windows 8.1 and Windows Phone 8.1 forms. An app like that is an ideal candidate for porting to a single Windows 10 app that targets the Universal device family (and that you can install onto the widest range of devices). You'll essentially port the contents of the Shared project and you'll need to use little or nothing from the other two projects because there'll be little or nothing in them.

Other times, the Windows 8.1 and/or the Windows Phone 8.1 form of the app contain unique features. Or they contain the same features but they implement those features using different techniques or different technology. With an app like that, you can choose to port it to a single app that targets the Universal device family (in which case you will want the app to adapt itself to different devices), or you can choose to port it as more than one app, perhaps one targeting the Desktop device family and another targeting the Mobile device family. The nature of the Universal 8.1 app will determine which of these options is best for your case.

1.  Port the contents of the Shared project to an app targeting the Universal device family. If applicable, salvage any other content from the Windows and WindowsPhone projects, and use that content either unconditionally in the app or conditional on the device that your app happens to be running on at the time (the latter behavior is known as *adaptive*).
2.  Port the contents of the WindowsPhone project to an app targeting the Universal device family. If applicable, salvage any other content from the Windows project, using it either unconditionally or adaptively.
3.  Port the contents of the Windows project to an app targeting the Universal device family. If applicable, salvage any other content from the WindowsPhone project, using it either unconditionally or adaptively.
4.  Port the contents of the Windows project to an app targeting the Universal or the Desktop device family and also port the contents of the WindowsPhone project to an app targeting the Universal or the Mobile device family. You can create a solution with a Shared project, and continue to share source code, markup files, and other assets and resources between the two projects. Or, you can create different solutions and still share the same items using links.

## If you have a Windows 8.1 app

Port the project to an app targeting the Universal or the Desktop device family. If you choose the Universal device family, and your app calls APIs that are implemented only in the Desktop device family, then you can guard those calls with adaptive code.

## If you have a Windows Phone 8.1 app

Port the project to an app targeting the Universal or the Mobile device family. If you choose the Universal device family, and your app calls APIs that are implemented only in the Mobile device family, then you can guard those calls with adaptive code.

## Adapting your app to multiple form factors

The option you choose from the previous sections will determine the range of devices that your app or apps will run on, and that may well be a very wide range of devices. Even limiting your app to the Mobile device family still leaves you with a wide range of screen sizes to support. So, if your app will be running on form factors that it didn't formerly support, then test your UI on those form factors and make any change necessary, so that your UI adapts appropriately on each. You can think of this is a post-porting task, or a porting stretch-goal, and there are some examples of it in practice in the [Bookstore2](w8x-to-uwp-case-study-bookstore2.md) and [QuizGame](w8x-to-uwp-case-study-quizgame.md) case studies.

## Approaching porting layer-by-layer

When porting a Universal 8.1 app to the model for UWP apps, virtually all of your knowledge and experience will transfer, as will most of your source code and markup and the software patterns you use.

-   **View**. The view (together with the view model) makes up your app's UI. Ideally, the view consists of markup bound to observable properties of a view model. Another pattern (common and convenient, but only in the short term) is for imperative code in a code-behind file to directly manipulate UI elements. In either case, your UI markup and design—and even imperative code that manipulates UI elements—will be straightforward to port.
-   **View models and data models**. Even if you don't formally embrace separation-of-concerns patterns (such as MVVM), there is inevitably code present in your app that performs the function of view model and data model. View model code makes use of types in the UI framework namespaces. Both view model and data model code also use non-visual operating system and .NET Framework APIs (including APIs for data-access). And those APIs are [available for UWP apps, too](/previous-versions/windows/br211369(v=win.10)), so most if not all of this code will port without change.
-   **Cloud services**. It's likely that some of your app (perhaps a great deal of it) runs in the cloud in the form of services. The part of the app running on the client device connects to those. This is the part of a distributed app most likely to remain unchanged when porting the client part. If you don't already have one, a good cloud services option for your UWP app is [Microsoft Azure Mobile Services](https://azure.microsoft.com/services/mobile-services/), which provides powerful back-end components that your app can call for services ranging from simple notifications for live tiles updates up to the kind of heavy-lifting scalability a server farm can provide.

Before or during the porting, consider whether your app could be improved by refactoring it so that code with a similar purpose is gathered together in layers and not scattered arbitrarily. Factoring your app into layers like those described above makes it easier for you to make your app correct, to test it, and then subsequently to read and maintain it. You can make functionality more reusable by following the Model-View-ViewModel ([MVVM](/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern)) pattern. This pattern keeps the data, business, and UI parts of your app separate from one another. Even within the UI it can keep state and behavior separate, and separately testable, from the visuals. With MVVM, you can write your data and business logic once and use it on all devices no matter the UI. It's likely that you'll be able to re-use much of the view model and view parts across devices, too.

| Topic | Description |
|-------|-------------|
| [Porting the project](w8x-to-uwp-porting-to-a-uwp-project.md) | You have two options when you begin the porting process. One is to edit a copy of your existing project files, including the app package manifest (for that option, see the info about updating your project files in [Migrate apps to the Universal Windows Platform (UWP)](/visualstudio/misc/migrate-apps-to-the-universal-windows-platform-uwp?view=vs-2015)). The other option is to create a new Windows 10 project in Visual Studio and copy your files into it. |
| [Troubleshooting](w8x-to-uwp-troubleshooting.md) | We highly recommend reading to the end of this porting guide, but we also understand that you're eager to forge ahead and get to the stage where your project builds and runs. To that end, you can make temporary progress by commenting or stubbing out any non-essential code, and then returning to pay off that debt later. The table of troubleshooting symptoms and remedies in this topic may be helpful to you at this stage, although it's not a substitute for reading the next few topics. You can always refer back to the table as you progress through the later topics. |
| [Porting XAML and UI](w8x-to-uwp-porting-xaml-and-ui.md) | The practice of defining UI in the form of declarative XAML markup translates extremely well from Universal 8.1 apps to UWP apps. You'll find that most of your markup is compatible, although you may need to make some adjustments to the system Resource keys or custom templates that you're using. |
| [Porting for I/O, device, and app model](w8x-to-uwp-input-and-sensors.md) | Code that integrates with the device itself and its sensors involves input from, and output to, the user. It can also involve processing data. But, this code is not generally thought of as either the UI layer or the data layer. This code includes integration with the vibration controller, accelerometer, gyroscope, microphone and speaker (which intersect with speech recognition and synthesis), (geo)location, and input modalities such as touch, mouse, keyboard, and pen. |
| [Case study: Bookstore1](w8x-to-uwp-case-study-bookstore1.md) | This topic presents a case study of porting a very simple Universal 8.1 app to a Windows 10 UWP app. A Universal 8.1 app is one that builds one app package for Windows 8.1, and a different app package for Windows Phone 8.1. With Windows 10, you can create a single app package that your customers can install onto a wide range of devices, and that's what we'll do in this case study. See [Guide to UWP apps](../get-started/universal-application-platform-guide.md). |
| [Case study: Bookstore2](w8x-to-uwp-case-study-bookstore2.md) | This case study—which builds on the info given in [SemanticZoom](/uwp/api/Windows.UI.Xaml.Controls.SemanticZoom) control. In the view model, each instance of the class Author represents the group of the books written by that author, and in the SemanticZoom, we can either view the list of books grouped by author or we can zoom out to see a jump list of authors. |
| [Case study: QuizGame](w8x-to-uwp-case-study-quizgame.md) | This topic presents a case study of porting a functioning peer-to-peer quiz game WinRT 8.1 sample app to a Windows 10 UWP app. |

## Related topics

**Documentation**
* [Windows Runtime reference](/uwp/api/)
* [Building Universal Windows apps for all Windows devices](../get-started/universal-application-platform-guide.md)
* [Designing UX for apps](/previous-versions/windows/hh767284(v=win.10))