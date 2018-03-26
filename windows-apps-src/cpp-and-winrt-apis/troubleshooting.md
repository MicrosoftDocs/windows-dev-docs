---
author: stevewhims
description: The table of troubleshooting symptoms and remedies in this topic may be helpful to you whether you're cutting new code or porting an existing app.
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

The table of troubleshooting symptoms and remedies in this topic may be helpful to you whether you're cutting new code or porting an existing app. If you're porting, and you're eager to forge ahead and get to the stage where your project builds and runs, then you can make temporary progress by commenting or stubbing out any non-essential code that's causing issues, and then returning to pay off that debt later.

## Tracking down XAML issues
XAML parse exceptions can be difficult to diagnose&mdash;particularly if there are no meaningful error messages within the exception. Make sure that the debugger is configured to catch first-chance exceptions (to try and catch the parsing exception early on). You may be able to inspect the exception variable in the debugger to determine whether the HRESULT or message has any useful information. Also, check Visual Studio's output window for error messages output by the XAML parser.

If your app terminates and all you know is that an unhandled exception was thrown during XAML markup parsing, then that could be the result of a reference (by key) to a missing resource. Or, it could be an exception thrown inside a UserControl, a custom control, or a custom layout panel. A last resort is a binary split. Remove about half of the markup from a XAML Page and re-run the app. You will then know whether the error is somewhere inside the half you removed (which you should now restore in any case) or in the half you did not remove. Repeat the process by splitting the half that contains the error, and so on, until you've zeroed in on the issue.

## Symptoms and remedies
| Symptom | Remedy |
|---------|--------|
| An exception is thrown at runtime with a HRESULT value of REGDB_E_CLASSNOTREGISTERED. | One cause of this error is that your Windows Runtime Component can't be loaded. Make sure that the component's Windows Metadata file (`.winmd`) has the same name as the component binary (the `.dll`), which is also the name of the project and the name of the root namespace. Also make sure that the Windows Metadata and the binary have been corectly copied by the build process to the consuming app's `Appx` folder. And confirm that the consuming app's `AppxManifest.xml` (also in the `Appx` folder) contains an **&lt;InProcessServer&gt;** element correctly declaring the activatable class and the binary name. This error can also happen if you make the mistake of instantiating a locally-implemented runtime class via the consuming wrapper's default constructor. See [C++/WinRT runtime class instantiation, activation, and construction](ctors-runtimeclass-activation.md) for more information about how to correctly use the consuming wrapper in that case. |
| The C++ compiler gives the error "*'implements_type': is not a member of any direct or indirect base class of '&lt;projected type&gt;'*". | This can happen when you call **make** with the namespace-unqualified name of your implementation type (**MyRuntimeClass**, for example), and you haven't included that type's header. The compiler interprets **MyRuntimeClass** as the projected type. The solution is to include the header for your implementation type (`MyRuntimeClass.h`, for example). |
| The C++ compiler gives the error "*attempting to reference a deleted function*". | This can happen when you call **make** and the implementation type that you pass as the template parameter has an `= delete` default constructor. Edit the implementation type's header file and change `= delete` to `= default`. You can also add a constructor into the IDL for the runtime class. |
| You've implemented [**INotifyPropertyChanged**](/uwp/api/windows.ui.xaml.data.inotifypropertychanged), but your XAML bindings are not updating (and the UI is not subscribing to [**PropertyChanged**](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged)). | Remember to set `Mode=OneWay` (or TwoWay) on your binding expression in XAML markup. See [XAML controls; binding to a C++/WinRT property](binding-property.md). |
| You're binding an items control to an observable collection and an exception is thrown at runtime with the message  "The parameter is incorrect". | In your IDL and your implementation, declare any observable collection as the type **Windows.Foundation.Collections.IVector<IInspectable>**. But return an object that implements **Windows.Foundation.Collections.IObservableVector<T>**, where T is your element type. See [XAML items controls; binding to a C++/WinRT collection](binding-collection.md).  |
