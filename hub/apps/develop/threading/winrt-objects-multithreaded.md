---
title: Using Windows Runtime objects in a multithreaded environment
description: Learn how .NET handles calls to Windows Runtime objects from multiple threads and how to use DispatcherQueue for UI-thread access in WinUI 3.
author: GrantMeStrength
ms.author: jken
ms.topic: concept-article
ms.date: 07/14/2026
---

# Using Windows Runtime objects in a multithreaded environment

This article discusses how .NET handles calls from C# code to objects provided by the Windows Runtime or by Windows Runtime components.

In .NET, you can access any object from multiple threads by default, without special handling. All you need is a reference to the object. In the Windows Runtime, such objects are called *agile*. Most Windows Runtime classes are agile, but a few classes are not, and even agile classes may require special handling.

Wherever possible, the common language runtime (CLR) treats objects from other sources, such as the Windows Runtime, as if they were .NET objects:

- If the object implements the [IAgileObject](/windows/desktop/api/objidl/nn-objidl-iagileobject) interface, or has the [MarshalingBehaviorAttribute](/uwp/api/Windows.Foundation.Metadata.MarshalingBehaviorAttribute) attribute with [MarshalingType.Agile](/uwp/api/Windows.Foundation.Metadata.MarshalingType), the CLR treats it as agile.

- If the CLR can marshal a call from the thread where it was made to the threading context of the target object, it does so transparently.

- If the object has the [MarshalingBehaviorAttribute](/uwp/api/Windows.Foundation.Metadata.MarshalingBehaviorAttribute) attribute with [MarshalingType.None](/uwp/api/Windows.Foundation.Metadata.MarshalingType), the class does not provide marshaling information. The CLR cannot marshal the call, so it throws an [InvalidCastException](/dotnet/api/system.invalidcastexception) exception with a message indicating that the object can be used only in the threading context where it was created.

The following sections describe the effects of this behavior on objects from various sources.

## Objects from a Windows Runtime component written in C\#

All the types in the component that can be activated are agile by default.

> [!NOTE]
> Agility doesn't imply thread safety. In both the Windows Runtime and .NET, most classes are not thread safe because thread safety has a performance cost and most objects are never accessed by multiple threads. It's more efficient to synchronize access to individual objects (or to use thread-safe classes) only as necessary.

When you author a Windows Runtime component, you can override the default. See the [ICustomQueryInterface](/dotnet/api/system.runtime.interopservices.icustomqueryinterface) interface and the [IAgileObject](/windows/desktop/api/objidl/nn-objidl-iagileobject) interface.

## Objects from the Windows Runtime

Most classes in the Windows Runtime are agile, and the CLR treats them as agile. The documentation for these classes lists "MarshalingBehaviorAttribute(Agile)" among the class attributes. However, the members of some of these agile classes, such as XAML controls, throw exceptions if they aren't called on the UI thread. For example, the following code tries to use a background thread to set a property of the button that was clicked. The button's [Content](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol.content) property throws an exception.

```csharp
private async void Button_Click_2(object sender, RoutedEventArgs e)
{
    Button b = (Button)sender;
    await Task.Run(() => {
        b.Content += ".";
    });
}
```

You can access the button safely by using its [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.dependencyobject.dispatcherqueue) property, or the `DispatcherQueue` property of any object that exists in the context of the UI thread (such as the page the button is on). The following code uses [DispatcherQueue.TryEnqueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) to dispatch the call on the UI thread.

```csharp
private async void Button_Click_2(object sender, RoutedEventArgs e)
{
    Button b = (Button)sender;
    await Task.Run(() => {
        b.DispatcherQueue.TryEnqueue(() => {
            b.Content += ".";
        });
    });
}
```

> [!NOTE]
> The `DispatcherQueue` property does not throw an exception when it's called from another thread.

The lifetime of a Windows Runtime object that is created on the UI thread is bounded by the lifetime of the thread. Do not try to access objects on a UI thread after the window has closed.

If you create your own control by inheriting a XAML control, or by composing a set of XAML controls, your control is agile because it's a .NET object. However, if it calls members of its base class or constituent classes, or if you call inherited members, those members throw exceptions when called from any thread except the UI thread.

### Classes that can't be marshaled

Windows Runtime classes that do not provide marshaling information have the [MarshalingBehaviorAttribute](/uwp/api/Windows.Foundation.Metadata.MarshalingBehaviorAttribute) attribute with [MarshalingType.None](/uwp/api/Windows.Foundation.Metadata.MarshalingType). The documentation for such a class lists "MarshalingBehaviorAttribute(None)" among its attributes.

> [!IMPORTANT]
> The UWP [CameraCaptureUI](/uwp/api/Windows.Media.Capture.CameraCaptureUI) class
> (in the `Windows.Media.Capture` namespace) can't be used in a WinUI 3 desktop app.
> It depends on a `CoreWindow` context and doesn't support `IInitializeWithWindow`,
> so a desktop app can't supply the window association it needs. You have two
> replacements, depending on what you need:
>
> - **For the built-in Windows capture UI**, use the Windows App SDK
>   [CameraCaptureUI](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture.cameracaptureui)
>   class (in the `Microsoft.Windows.Media.Capture` namespace, WASDK 1.7 and later).
>   You construct it with a [WindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowid),
>   which provides the window association that the UWP class lacked. See
>   [Capture photos and video in a desktop app with the Windows built-in camera UI](/windows/apps/develop/camera/cameracaptureui).
> - **For a custom preview**, use [MediaCapture](/uwp/api/Windows.Media.Capture.MediaCapture)
>   with a [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement),
>   feeding it a `MediaSource` created from a `MediaFrameSource`. The UWP
>   `CaptureElement` control is *not* available in WinUI 3. See
>   [Show the camera preview in a WinUI app](/windows/apps/develop/camera/camera-quickstart-winui3).
>
> The marshaling concepts described below still apply to any non-agile Windows
> Runtime class you encounter.

The following code illustrates the marshaling issue using the UWP `CameraCaptureUI` as a representative non-agile (`MarshalingType.None`) class. It creates the object on the UI thread and then tries to set a property from a thread pool thread. The CLR is unable to marshal the call and throws a [System.InvalidCastException](/dotnet/api/system.invalidcastexception) exception with a message indicating that the object can be used only in the threading context where it was created.

> [!NOTE]
> The `Windows.Media.Capture.CameraCaptureUI` type shown here can't be instantiated in a WinUI 3 desktop app (see the note above). These snippets illustrate the threading pattern for any non-agile class; in a desktop app, use the WASDK `CameraCaptureUI` or another non-agile type.

```csharp
Windows.Media.Capture.CameraCaptureUI ccui;

private async void Button_Click_1(object sender, RoutedEventArgs e)
{
    ccui = new Windows.Media.Capture.CameraCaptureUI();

    await Task.Run(() => {
        ccui.PhotoSettings.AllowCropping = true;
    });
}
```

The documentation for [CameraCaptureUI](/uwp/api/Windows.Media.Capture.CameraCaptureUI) also lists "ThreadingAttribute(STA)" among the class's attributes, because it must be created in a single-threaded context such as the UI thread.

If you want to access a non-agile object from another thread, cache the [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) for the UI thread (by calling `DispatcherQueue.GetForCurrentThread()` on that thread) and use it later to dispatch the call from a background thread. You can also obtain the dispatcher queue from any XAML object via `DependencyObject.DispatcherQueue`.

```csharp
// Cache the UI thread's dispatcher queue when the object is created (on the UI thread).
private DispatcherQueue uiDispatcherQueue = DispatcherQueue.GetForCurrentThread();
private Windows.Media.Capture.CameraCaptureUI ccui2;

private async void Button_Click_3(object sender, RoutedEventArgs e)
{
    // Create the non-agile object on the UI thread.
    ccui2 = new Windows.Media.Capture.CameraCaptureUI();

    // From a background thread, dispatch the call back to the UI thread
    // using the cached dispatcher queue.
    await Task.Run(() =>
    {
        uiDispatcherQueue.TryEnqueue(() =>
        {
            ccui2.PhotoSettings.AllowCropping = true;
        });
    });
}
```

## Objects from a Windows Runtime component written in C++

By default, classes in the component that can be activated are agile. However, C++ allows a significant amount of control over threading models and marshaling behavior. As described earlier in this article, the CLR recognizes agile classes, tries to marshal calls when classes are not agile, and throws a [System.InvalidCastException](/dotnet/api/system.invalidcastexception) exception when a class has no marshaling information.

For objects that run on the UI thread and throw exceptions when called from a thread other than the UI thread, you can use the UI thread's [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) object to dispatch the call.

## See also

- [C# Guide](/dotnet/csharp/)
- [Threading and async programming](/windows/apps/develop/threading/)
- [DispatcherQueue class](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue)
