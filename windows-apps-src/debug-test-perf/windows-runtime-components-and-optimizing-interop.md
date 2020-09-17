---
ms.assetid: 9899F6A0-7EDD-4988-A76E-79D7C0C58126
title: Optimizing interop for UWP components
description: Create Universal Windows Platform (UWP) apps that use UWP Components and interop between native and managed types while avoiding interop performance issues.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# UWP Components and optimizing interop


Create Universal Windows Platform (UWP) apps that use UWP Components and interop between native and managed types while avoiding interop performance issues.

## Best practices for interoperability with UWP Components

If you are not careful, using UWP Components can have a large impact on your app performance. This section discusses how to get good performance when your app uses UWP Components.

### Introduction

Interoperability can have a big impact on performance and you might be using it without even realizing that you are. The UWP handles a lot of the interoperability for you so that you can be more productive and reuse code that was written in other languages. We encourage you to take advantage of what the UWP does for you, but be aware that it can impact performance. This section discusses things you can do to lessen the impact that interoperability has on your app's performance.

The UWP has a library of types that are accessible from any language that can write a UWP app. You use the UWP types in C# or Microsoft Visual Basic the same way you use .NET objects. You don't need to make platform invoke method calls to access the UWP components. This makes writing your apps much less complex, but it is important to realize that there might be more interoperability occurring than you expect. If a UWP component is written in a language other than C# or Visual Basic, you cross interoperability boundaries when you use that component. Crossing interoperability boundaries can impact the performance of an app.

When you develop a UWP app in C# or Visual Basic, the two most common set of APIs that you use are the Windows Runtime APIs and the .NET APIs for UWP apps. In general, types that are defined in the UWP are in namespaces that begin with "Windows." and .NET types are in namespaces that begin with "System." There are exceptions, though. The types in .NET for UWP apps do not require interoperability when they are used. If you find that you have bad performance in an area that uses UWP, you might be able to use .NET for UWP apps instead to get better performance.

**Note**  
Most of the UWP components that ship with Windows 10 are implemented in C++ so you cross interoperability boundaries when you use them from C# or Visual Basic. As always, make sure to measure your app to see if using UWP components affects your app's performance before you invest in making changes to your code.

In this topic, when we say "UWP components", we mean components that are written in a language other than C# or Visual Basic.

 

Each time you access a property or call a method on a UWP component, an interoperability cost is incurred. In fact, creating a UWP component is more costly than creating a .NET object. The reasons for this are that the UWP must execute code that transitions from your app's language to the component's language. Also, if you pass data to the component, the data must be converted between managed and unmanaged types.

### Using UWP Components efficiently

If you find that you need to get better performance, you can ensure that your code uses UWP components as efficiently as possible. This section discusses some tips for improving performance when you use UWP components.

It takes a significant number of calls in a short period of time for the performance impact to be noticeable. A well-designed application that encapsulates calls to UWP components from business logic and other managed code should not incur huge interoperability costs. But if your tests indicate that using UWP components is affecting your app's performance, the tips discussed in this section help you improve performance.

### Consider using .NET for UWP apps

There are certain cases where you can accomplish a task by using either UWP or .NET for UWP apps. It is a good idea to try to not mix .NET types and UWP types. Try to stay in one or the other. For example, you can parse a stream of xml by using either the [**Windows.Data.Xml.Dom.XmlDocument**](/uwp/api/Windows.Data.Xml.Dom.XmlDocument) type (a UWP type) or the [**System.Xml.XmlReader**](/dotnet/api/system.xml.xmlreader) type (a .NET type). Use the API that is from the same technology as the stream. For example, if you read xml from a [**MemoryStream**](/dotnet/api/system.io.memorystream), use the **System.Xml.XmlReader** type, because both types are .NET types. If you read from a file, use the **Windows.Data.Xml.Dom.XmlDocument** type because the file APIs and **XmlDocument** are UWP components.

### Copy Window Runtime objects to .NET types

When a UWP component returns a UWP object, it might be beneficial to copy the returned object into a .NET object. Two places where this is especially important is when you're working with collections and streams.

If you call a Windows Runtime API that returns a collection and then you save and access that collection many times, it might be beneficial to copy the collection into a .NET collection and use the .NET version from then on.

### Cache the results of calls to UWP components for later use

You might be able to get better performance by saving values into local variables instead of accessing a UWP type multiple times. This can be especially beneficial if you use a value inside of a loop. Measure your app to see if using local variables improves your app's performance. Using cached values can increase your app's speed because it will spend less time on interoperability.

### Combine calls to UWP components

Try to complete tasks with the fewest number of calls to UWP objects as possible. For example, it is usually better to read a large amount of data from a stream than to read small amounts at a time.

Use APIs that bundle work in as few calls as possible instead of APIs that do less work and require more calls. For example, prefer to create an object by calling constructors that initialize multiple properties instead of calling the default constructor and assigning properties one at a time.

### Building a UWP components

If you write a UWP Component that can be used by apps written in C++ or JavaScript, make sure that your component is designed for good performance. All the suggestions for getting good performance in apps apply to getting good performance in components. Measure your component to find out which APIs have high traffic patterns and for those areas, consider providing APIs that enable your users to do work with few calls.

## Keep your app fast when you use interop in managed code

The UWP makes it easy to interoperate between native and managed code, but if you're not careful it can incur performance costs. Here we show you how to get good performance when you use interop in your managed UWP apps.

The UWP allows developers to write apps using XAML with their language of choice thanks to the projections of the Windows Runtime APIs available in each language. When writing an app in C# or Visual Basic, this convenience comes at an interop cost because the Windows Runtime APIs are usually implemented in native code, and any UWP invocation from C# or Visual Basic requires that the CLR transition from a managed to a native stack frame and marshal function parameters to representations accessible by native code. This overhead is negligible for most apps. But when you make many calls (hundreds of thousands, to millions) to Windows Runtime APIs in the critical path of an app, this cost can become noticeable. In general you want to ensure that the time spent in transition between languages is small relative to the execution of the rest of your code. This is illustrated by the following diagram.

![Interop transitions should not dominate the program execution time.](images/interop-transitions.png)

The types listed at [**.NET for Windows apps**](https://dotnet.microsoft.com/apps/desktop) don't incur this interop cost when used from C# or Visual Basic. As a rule of thumb, you can assume that types in namespaces which begin with “Windows.” are part of the UWP, and types in namespaces which begin with “System.” are .NET types. Keep in mind that even simple usage of UWP types such as allocation or property access incurs an interop cost.

You should measure your app and determine if interop is taking up a large portion of your apps execution time before optimizing your interop costs. When analyzing your app’s performance with Visual Studio, you can easily get an upper bound on your interop costs by using the **Functions** view and looking at inclusive time spent in methods which call into the UWP.

If your app is slow because of interop overhead, you can improve its performance by reducing calls to Windows Runtime APIs on hot code paths. For example, a game engine that is doing tons of physics calculations by constantly querying the position and dimensions of [**UIElements**](/uwp/api/Windows.UI.Xaml.UIElement) can save a lot of time by storing the necessary info from **UIElements** to local variables, doing calculations on these cached values, and assigning the end result back to the **UIElements** after the calculations are done. Another example: if a collection is heavily accessed by C# or Visual Basic code, then it is more efficient to use a collection from the [**System.Collections**](/dotnet/api/system.collections) namespace, rather than a collection from the [**Windows.Foundation.Collections**](/uwp/api/Windows.Foundation.Collections) namespace. You may also consider combining calls to UWP components; one example where this is possible is by using the [**Windows.Storage.BulkAccess**](/uwp/api/Windows.Storage.BulkAccess) APIs.

### Building a UWP component

If you write a UWP component for use in apps written in C++ or JavaScript, make sure that your component is designed for good performance. Your API surface defines your interop boundary and defines the degree to which your users will have to think about the guidance in this topic. If you are distributing your components to other parties then this becomes especially important.

All of the suggestions for getting good performance in apps apply to getting good performance in components. Measure your component to find out which APIs have high traffic patterns, and for those areas, consider providing APIs that enable your users to do work with few calls. Significant effort was put into designing the UWP to allow apps to use it without requiring frequent crossing of the interop boundary.

 