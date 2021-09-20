---
description: If you’re a developer with a Windows Phone Silverlight app, then you can make great use of your skill set and your source code in the move to Windows 10.
title: Move from Windows Phone Silverlight to UWP
ms.assetid: 9E0C0315-6097-488B-A3AF-7120CCED651A
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
#  Move from Windows Phone Silverlight to UWP


If you’re a developer with a Windows Phone Silverlight app, then you can make great use of your skill set and your source code in the move to Windows 10. With Windows 10, you can create a Universal Windows Platform (UWP) app, which is a single app package that your customers can install onto every kind of device. For more background on Windows 10, UWP apps, and the concepts of adaptive code and adaptive UI that we'll mention in this porting guide, see the [Guide to Universal Windows Platform (UWP) apps](../get-started/universal-application-platform-guide.md).

When you port your Windows Phone Silverlight app to a Windows 10 app, you'll be able to catch up on the mobile features that were [introduced in Windows Phone 8.1](/previous-versions/windows/apps/ff402535(v=vs.105)), and go far beyond them to use the Universal Windows Platform (UWP) whose app model and UI framework are universal across all Windows 10 devices. That makes it possible to support PCs, tablets, phones, and a large number of other kinds of devices, from one code base and with one app package. And that will multiply your app's potential audience and create new possibilities with shared data, purchased consumables, and so on. For more info on new features, see [What's new for developers in Windows 10](../whats-new/windows-10-build-19041.md).

If you choose to, the Windows Phone Silverlight version of your app and the Windows 10 version of it can both be available to customers at the same time.

**Note**  This guide is designed to help you port your Windows Phone Silverlight app to Windows 10 manually. In addition to using the information in this guide to port your app, you can try the developer preview of **Mobilize.NET's Silverlight Bridge** to help automate the porting process. This tool analyzes your app's source code and converts references to Windows Phone Silverlight controls and APIs to their UWP counterparts. Because this tool is still in developer preview, it does not yet handle all conversion scenarios. However, most developers should be able to save some time and effort by starting with this tool. To try the developer preview, visit [Mobilize.NET's website](https://www.mobilize.net/uwp-bridge).

## XAML and .NET, or HTML?

Windows Phone Silverlight has a XAML UI framework based on Silverlight 4.0, and you program against a version of the .NET Framework and a small subset of Windows Runtime APIs. Since you used Extensible Application Markup Language (XAML) in your Windows Phone Silverlight app, it's likely that XAML will be your choice for your Windows 10 version because most of your knowledge and experience will transfer, as will much of your source code and the software patterns you use. Even your UI markup and design can port over readily. You will find the managed APIs, the XAML markup, the UI framework, and the tooling all reassuringly familiar, and you can use C++, C#, or Visual Basic along with XAML in a UWP app. You may be surprised at how relatively easy the process is, even if there is a challenge or two along the way.

See [Roadmap for Universal Windows Platform (UWP) apps using C# or Visual Basic](/previous-versions/windows/apps/br229583(v=win.10)).

**Note**  Windows 10 supports much more of the .NET Framework than a Windows Phone Store app does. For example, Windows 10 has several System.ServiceModel.\* namespaces as well as System.Net, System.Net.NetworkInformation, and System.Net.Sockets. So, now is a great time to port your Windows Phone Silverlight and have your .NET code just compile and work on the new platform. See [Namespace and class mappings](wpsl-to-uwp-namespace-and-class-mappings.md).
Another great reason to recompile your existing .NET source code into a Windows 10 app is that you will benefit from .NET Native, which an ahead-of-time compilation technology that converts MSIL into natively-runnable machine code. .NET Native apps start faster, use less memory, and use less battery than their MSIL counterparts.

This porting guide will focus on XAML but, alternatively, you can build a functionally equivalent app—calling many of the same Windows Runtime APIs—using JavaScript, Cascading Style Sheets (CSS), and HTML5 along with the Windows Library for JavaScript. Although the Windows Runtime UI frameworks of XAML and HTML are different from one another, whichever one you choose will work universally across the full range of Windows devices.

## Targeting the universal or the mobile device family

One option you have is to port your app to an app that targets the universal device family. In this case, the app can be installed onto the widest range of devices. If your app calls APIs that are implemented only in the mobile device family, then you can guard those calls with adaptive code. Alternatively, you can choose to port your app to an app that targets the mobile device family in which case you don't need to write adaptive code.

## Adapting your app to multiple form factors

The option you choose from the previous section will determine the range of devices that your app or apps will run on, and that may well be a very wide range of devices. Even limiting your app to the mobile device family still leaves you with a wide range of screen sizes to support. So, since your app will be running on form factors that it didn't formerly support, test your UI on those form factors and make any change necessary so that your UI adapts appropriately on each. You can think of this is a post-porting task, or a porting stretch-goal, and there is an example of it in practice in the [Bookstore2](wpsl-to-uwp-case-study-bookstore2.md) case study.

## Approaching porting layer-by-layer

-   **View**. The view (together with the view model) makes up your app's UI. Ideally, the view consists of markup bound to observable properties of a view model. Another pattern (common and convenient, but only in the short term) is for imperative code in a code-behind file to directly manipulate UI elements. In either case, much of your UI markup and design—and even imperative code that manipulates UI elements—will be straightforward to port.
-   **View models and data models**. Even if you don't formally embrace separation-of-concerns patterns (such as MVVM), there is inevitably code present in your app that performs the function of view model and data model. View model code makes use of types in the UI framework namespaces. Both view model and data model code also use non-visual operating system and .NET APIs (including APIs for data-access). And the vast majority of those are [available to a UWP app](/previous-versions/windows/br211369(v=win.10)), so you can expect to be able to port much of this code without change. Remember, though: a view model is a model, or *abstraction*, of a view. A view model provides the state and behavior of UI, while the view itself provides the visuals. For this reason, any UI you adapt to the different form factors that the UWP allows you to run on will likely need corresponding view model changes. For networking and calling cloud services, you typically have the option between using .NET or Windows Runtime APIs. For the factors involved in making that decision, see [Cloud services, networking, and databases](wpsl-to-uwp-business-and-data.md).
-   **Cloud services**. It's likely that some of your app (perhaps a great deal of it) runs in the cloud in the form of services. The part of the app running on the client device connects to those. This is the part of a distributed app most likely to remain unchanged when porting the client part. If you don't already have one, a good cloud services option for your UWP app is [Microsoft Azure Mobile Services](https://azure.microsoft.com/services/mobile-services/), which provides powerful back-end components that Universal Windows apps can call for services ranging from simple notifications for live tiles updates up to the kind of heavy-lifting scalability a server farm can provide.

Before or during the porting, consider whether your app could be improved by refactoring it so that code with a similar purpose is gathered together in layers and not scattered arbitrarily. Factoring your UWP app into layers like those described above makes it easier for you to make your app correct, to test it, and then subsequently to read and maintain it. You can make functionality more reusable—and avoid some issues of UI API differences between platforms—by following the Model-View-ViewModel ([MVVM](/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern)) pattern. This pattern keeps the data, business, and UI parts of your app separate from one another. Even within the UI it can keep state and behavior separate, and separately testable, from the visuals. With MVVM, you can write your data and business logic once and use it on all devices no matter the UI. It's likely that you'll be able to re-use much of the view model and view parts across devices, too.

## One or two exceptions to the rule

As you read this porting guide, you can refer to [Namespace and class mappings](wpsl-to-uwp-namespace-and-class-mappings.md). Fairly straightforward mapping is the general rule, and the namespace and class mappings table describes any exceptions.

At the feature level, the good news is that there's very little that's not supported in the UWP. Most of your skill set and source code translates very well over to UWP apps, as you'll read in the rest of this porting guide. But, here are the few Windows Phone Silverlight features that you may have used for which there is no UWP equivalent.

| Feature for which there is no UWP equivalent | Windows Phone Silverlight documentation for the feature |
|----------------------------------------------|---------------------------------------------------------|
| Microsoft XNA. In general, [Microsoft DirectX](/windows/desktop/directx) using C++ is the replacement. See [Developing games](/previous-versions/windows/apps/hh452744(v=win.10)) and [DirectX and XAML interop](/previous-versions/windows/apps/hh825871(v=win.10)). | [XNA Framework Class Library](/previous-versions/windows/xna/bb200104(v=xnagamestudio.41)) | 
|Lens apps | [Lenses for Windows Phone 8](/previous-versions/windows/apps/jj206990(v=vs.105)) |

&nbsp;

| Topic| Description|
|------|------------| 
| [Namespace and class mappings](wpsl-to-uwp-namespace-and-class-mappings.md) | This topic provides a comprehensive mapping of Windows Phone Silverlight APIs to their UWP equivalents. |
| [Porting the project](wpsl-to-uwp-porting-to-a-uwp-project.md) | You begin the porting process by creating a new Windows 10 project in Visual Studio and copying your files into it. |
| [Troubleshooting](wpsl-to-uwp-troubleshooting.md) | We highly recommend reading to the end of this porting guide, but we also understand that you're eager to forge ahead and get to the stage where your project builds and runs. To that end, you can make temporary progress by commenting or stubbing out any non-essential code, and then returning to pay off that debt later. The table of troubleshooting symptoms and remedies in this topic may be helpful to you at this stage, although it's not a substitute for reading the next few topics. You can always refer back to the table as you progress through the later topics. |
| [Porting XAML and UI](wpsl-to-uwp-porting-xaml-and-ui.md) | The practice of defining UI in the form of declarative XAML markup translates extremely well from Windows Phone Silverlight to UWP apps. You'll find that large sections of your markup are compatible once you've updated system Resource key references, changed some element type names, and changed "clr-namespace" to "using". |
| [Porting for I/O, device, and app model](wpsl-to-uwp-input-and-sensors.md) | Code that integrates with the device itself and its sensors involves input from, and output to, the user. It can also involve processing data. But, this code is not generally thought of as either the UI layer or the data layer. This code includes integration with the vibration controller, accelerometer, gyroscope, microphone and speaker (which intersect with speech recognition and synthesis), (geo)location, and input modalities such as touch, mouse, keyboard, and pen. |
| [Porting business and data layers](wpsl-to-uwp-business-and-data.md) | Behind your UI are your business and data layers. The code in these layers calls operating system and .NET Framework APIs (for example, background processing, location, the camera, the file system, network, and other data access). The vast majority of those are [available to a UWP app](/previous-versions/windows/br211369(v=win.10)), so you can expect to be able to port much of this code without change. |
| [Porting for form factor and UX](wpsl-to-uwp-form-factors-and-ux.md) | Windows apps share a common look-and-feel across PCs, mobile devices, and many other kinds of devices. The user interface, input, and interaction patterns are very similar, and a user moving between devices will welcome the familiar experience.|
|[Case study: Bookstore1](wpsl-to-uwp-case-study-bookstore1.md) | This topic presents a case study of porting a very simple Windows Phone Silverlight app to a Windows 10 UWP app. With Windows 10, you can create a single app package that your customers can install onto a wide range of devices, and that's what we'll do in this case study. |
| [Case study: Bookstore2](wpsl-to-uwp-case-study-bookstore2.md) | This case study—which builds on the info given in [Bookstore1](wpsl-to-uwp-case-study-bookstore1.md)—begins with a Windows Phone Silverlight app that displays grouped data in a **LongListSelector**. In the view model, each instance of the class **Author** represents the group of the books written by that author, and in the **LongListSelector**, we can either view the list of books grouped by author or we can zoom out to see a jump list of authors. |

## Related topics

**Documentation**
* [What's new for developers in Windows 10](../whats-new/windows-10-build-19041.md)
* [Guide to Universal Windows Platform (UWP) apps](../get-started/universal-application-platform-guide.md)
* [Roadmap for Universal Windows Platform (UWP) apps using C# or Visual Basic](/previous-versions/windows/apps/br229583(v=win.10))
* [What's next for Windows Phone 8 developers](/previous-versions/windows/apps/dn655121(v=vs.105))

**Magazine articles**
* [Visual Studio Magazine: Windows Phone 8.1: A Giant Leap Forward for Convergence](https://visualstudiomagazine.com/articles/2014/05/01/whats-new-for-developers-with-windows-phone-8_1.aspx)

**Presentations**
* [The Story of Bringing Nokia Music from Windows Phone to Windows 8](https://channel9.msdn.com/Events/Build/2013/2-219)
 