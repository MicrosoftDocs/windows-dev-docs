---
title: Custom events and event accessors in Windows Runtime components
description: .NET support for Windows Runtime components makes it easy to declare events components by hiding the differences between the Universal Windows Platform (UWP) event pattern and the .NET event pattern.
ms.assetid: 6A66D80A-5481-47F8-9499-42AC8FDA0EB4
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Custom events and event accessors in Windows Runtime components

.NET support for Windows Runtime components makes it easy to declare events components by hiding the differences between the Universal Windows Platform (UWP) event pattern and the .NET event pattern. However, when you declare custom event accessors in a Windows Runtime component, you must follow the pattern used in the UWP.

## Registering events

When you register to handle an event in the UWP, the add accessor returns a token. To unregister, you pass this token to the remove accessor. This means that the add and remove accessors for UWP events have different signatures from the accessors you're used to.

Fortunately, the Visual Basic and C# compilers simplify this process: When you declare an event with custom accessors in a Windows Runtime component, the compilers automatically use the UWP pattern. For example, you get a compiler error if your add accessor doesn't return a token. .NET provides two types to support the implementation:

-   The [EventRegistrationToken](/uwp/api/windows.foundation.eventregistrationtoken) structure represents the token.
-   The [EventRegistrationTokenTable&lt;T&gt;](/dotnet/api/system.runtime.interopservices.windowsruntime.eventregistrationtokentable-1) class creates tokens and maintains a mapping between tokens and event handlers. The generic type argument is the event argument type. You create an instance of this class for each event, the first time an event handler is registered for that event.

The following code for the NumberChanged event shows the basic pattern for UWP events. In this example, the constructor for the event argument object, NumberChangedEventArgs, takes a single integer parameter that represents the changed numeric value.

> **Note**  This is the same pattern the compilers use for ordinary events that you declare in a Windows Runtime component.

 
> [!div class="tabbedCodeSnippets"]
> ```csharp
> private EventRegistrationTokenTable<EventHandler<NumberChangedEventArgs>>
>     m_NumberChangedTokenTable = null;
>
> public event EventHandler<NumberChangedEventArgs> NumberChanged
> {
>     add
>     {
>         return EventRegistrationTokenTable<EventHandler<NumberChangedEventArgs>>
>             .GetOrCreateEventRegistrationTokenTable(ref m_NumberChangedTokenTable)
>             .AddEventHandler(value);
>     }
>     remove
>     {
>         EventRegistrationTokenTable<EventHandler<NumberChangedEventArgs>>
>             .GetOrCreateEventRegistrationTokenTable(ref m_NumberChangedTokenTable)
>             .RemoveEventHandler(value);
>     }
> }
>
> internal void OnNumberChanged(int newValue)
> {
>     EventHandler<NumberChangedEventArgs> temp =
>         EventRegistrationTokenTable<EventHandler<NumberChangedEventArgs>>
>         .GetOrCreateEventRegistrationTokenTable(ref m_NumberChangedTokenTable)
>         .InvocationList;
>     if (temp != null)
>     {
>         temp(this, new NumberChangedEventArgs(newValue));
>     }
> }
> ```
> ```vb
> Private m_NumberChangedTokenTable As  _
>     EventRegistrationTokenTable(Of EventHandler(Of NumberChangedEventArgs))
>
> Public Custom Event NumberChanged As EventHandler(Of NumberChangedEventArgs)
>
>     AddHandler(ByVal handler As EventHandler(Of NumberChangedEventArgs))
>         Return EventRegistrationTokenTable(Of EventHandler(Of NumberChangedEventArgs)).
>             GetOrCreateEventRegistrationTokenTable(m_NumberChangedTokenTable).
>             AddEventHandler(handler)
>     End AddHandler
>
>     RemoveHandler(ByVal token As EventRegistrationToken)
>         EventRegistrationTokenTable(Of EventHandler(Of NumberChangedEventArgs)).
>             GetOrCreateEventRegistrationTokenTable(m_NumberChangedTokenTable).
>             RemoveEventHandler(token)
>     End RemoveHandler
>
>     RaiseEvent(ByVal sender As Class1, ByVal args As NumberChangedEventArgs)
>         Dim temp As EventHandler(Of NumberChangedEventArgs) = _
>             EventRegistrationTokenTable(Of EventHandler(Of NumberChangedEventArgs)).
>             GetOrCreateEventRegistrationTokenTable(m_NumberChangedTokenTable).
>             InvocationList
>         If temp IsNot Nothing Then
>             temp(sender, args)
>         End If
>     End RaiseEvent
> End Event
> ```

The static (Shared in Visual Basic) GetOrCreateEventRegistrationTokenTable method creates the event’s instance of the EventRegistrationTokenTable&lt;T&gt; object lazily. Pass the class-level field that will hold the token table instance to this method. If the field is empty, the method creates the table, stores a reference to the table in the field, and returns a reference to the table. If the field already contains a token table reference, the method just returns that reference.

> **Important**  To ensure thread safety, the field that holds the event’s instance of EventRegistrationTokenTable&lt;T&gt; must be a class-level field. If it is a class-level field, the GetOrCreateEventRegistrationTokenTable method ensures that when multiple threads try to create the token table, all threads get the same instance of the table. For a given event, all calls to the GetOrCreateEventRegistrationTokenTable method must use the same class-level field.

Calling the GetOrCreateEventRegistrationTokenTable method in the remove accessor and in the [RaiseEvent](/dotnet/articles/visual-basic/language-reference/statements/raiseevent-statement) method (the OnRaiseEvent method in C#) ensures that no exceptions occur if these methods are called before any event handler delegates have been added.

The other members of the EventRegistrationTokenTable&lt;T&gt; class that are used in the UWP event pattern include the following:

-   The [AddEventHandler](/dotnet/api/system.runtime.interopservices.windowsruntime.eventregistrationtokentable-1.addeventhandler#System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_1_AddEventHandler__0_) method generates a token for the event handler delegate, stores the delegate in the table, adds it to the invocation list, and returns the token.
-   The [RemoveEventHandler(EventRegistrationToken)](/dotnet/api/system.runtime.interopservices.windowsruntime.eventregistrationtokentable-1.removeeventhandler#System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_1_RemoveEventHandler_System_Runtime_InteropServices_WindowsRuntime_EventRegistrationToken_) method overload removes the delegate from the table and from the invocation list.

    >**Note**  The AddEventHandler and RemoveEventHandler(EventRegistrationToken) methods lock the table to help ensure thread safety.

-   The [InvocationList](/dotnet/api/system.runtime.interopservices.windowsruntime.eventregistrationtokentable-1.invocationlist#System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_1_InvocationList) property returns a delegate that includes all the event handlers that are currently registered to handle the event. Use this delegate to raise the event, or use the methods of the Delegate class to invoke the handlers individually.

    >**Note**  We recommend that you follow the pattern shown in the example provided earlier in this article, and copy the delegate to a temporary variable before invoking it. This avoids a race condition in which one thread removes the last handler, reducing the delegate to null just before another thread tries to invoke the delegate. Delegates are immutable, so the copy is still valid.

Place your own code in the accessors as appropriate. If thread safety is an issue, you must provide your own locking for your code.

C# users: When you write custom event accessors in the UWP event pattern, the compiler doesn't provide the usual syntactic shortcuts. It generates errors if you use the name of the event in your code.

Visual Basic users: In .NET, an event is just a multicast delegate that represents all the registered event handlers. Raising the event just means invoking the delegate. Visual Basic syntax generally hides the interactions with the delegate, and the compiler copies the delegate before invoking it, as described in the note about thread safety. When you create a custom event in a Windows Runtime component, you have to deal with the delegate directly. This also means that you can, for example, use the [MulticastDelegate.GetInvocationList](/dotnet/api/system.multicastdelegate.getinvocationlist#System_MulticastDelegate_GetInvocationList) method to get an array that contains a separate delegate for each event handler, if you want to invoke the handlers separately.

## Related topics

* [Events (Visual Basic)](/dotnet/articles/visual-basic/programming-guide/language-features/events/index)
* [Events (C# Programming Guide)](/dotnet/articles/csharp/programming-guide/events/index)
* [.NET for UWP apps Overview](/previous-versions/windows/apps/br230302(v=vs.140))
* [.NET for UWP apps](/dotnet/api/index?view=dotnet-uwp-10.0)
* [Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript](walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript.md)