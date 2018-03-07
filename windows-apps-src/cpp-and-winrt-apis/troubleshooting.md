---
author: stevewhims
description: ???
title: Troubleshooting C++/WinRT issues
ms.author: stwhi
ms.date: 03/06/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, troubleshooting, HRESULT, error
ms.localizationpriority: medium
---

# Troubleshooting C++/WinRT issues
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

We highly recommend reading to the end of this porting guide, but we also understand that you're eager to forge ahead and get to the stage where your project builds and runs. To that end, you can make temporary progress by commenting or stubbing out any non-essential code, and then returning to pay off that debt later. The table of troubleshooting symptoms and remedies in this topic may be helpful to you at this stage, although it's not a substitute for reading the next few topics. You can always refer back to the table as you progress through the later topics.

Tracking down issues
XAML parse exceptions can be difficult to diagnose, particularly if there are no meaningful error messages within the exception. Make sure that the debugger is configured to catch first-chance exceptions (to try and catch the parsing exception early on). You may be able to inspect the exception variable in the debugger to determine whether the HRESULT or message has any useful information. Also, check Visual Studio's output window for error messages output by the XAML parser.

If your app terminates and all you know is that an unhandled exception was thrown during XAML markup parsing, then that could be the result of a reference to a missing resource (that is, a resource whose key exists for Windows Phone Silverlight apps but not for Windows 10 apps, such as some system TextBlock Style keys). Or, it could be an exception thrown inside a UserControl, a custom control, or a custom layout panel.

A last resort is a binary split. Remove about half of the markup from a Page and re-run the app. You will then know whether the error is somewhere inside the half you removed (which you should now restore in any case) or in the half you did not remove. Repeat the process by splitting the half that contains the error, and so on, until you've zeroed in on the issue.

## Symptoms and remedies
| Symptom | Remedy |
|---------|--------|
| An exception is thrown at runtime with a HRESULT value of REGDB_E_CLASSNOTREGISTERED. | One cause of this error is that your Windows Runtime Component can't be loaded. Make sure that the component's Windows Metadata file (`.winmd`) has the same name as the component binary (the `.dll`), which is also the name of the project and the name of the root namespace. Also make sure that the Windows Metadata and the binary have been corectly copied by the build process to the consuming app's `Appx` folder. And confirm that the consuming app's `AppxManifest.xml` (also in the `Appx` folder) contains an **&lt;InProcessServer&gt;** element correctly declaring the activatable class and the binary name. This error can also happen if you're instantiating a runtime class via the consuming wrapper's default constructor. See [Runtime class instantiation, activation, and construction](ctors-runtimeclass-activation.md) for more information about how to correctly use the consuming wrapper. |
