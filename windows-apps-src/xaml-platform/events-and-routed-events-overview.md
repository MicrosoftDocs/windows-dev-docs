---
description: We describe the programming concept of events in a Windows Runtime app, when using C#, Visual Basic or Visual C++ component extensions (C++/CX) as your programming language, and XAML for your UI definition.
title: Events and routed events overview
ms.assetid: 34C219E8-3EFB-45BC-8BBD-6FD937698832
ms.date: 07/12/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Events and routed events overview

**Important APIs**
- [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement)
- [**RoutedEventArgs**](/uwp/api/Windows.UI.Xaml.RoutedEventArgs)

We describe the programming concept of events in a Windows Runtime app, when using C#, Visual Basic or Visual C++ component extensions (C++/CX) as your programming language, and XAML for your UI definition. You can assign handlers for events as part of the declarations for UI elements in XAML, or you can add the handlers in code. Windows Runtime supports *routed events*: certain input events and data events can be handled by objects beyond the object that fired the event. Routed events are useful when you define control templates, or use pages or layout containers.

## Events as a programming concept

Generally speaking, event concepts when programming a Windows Runtime app are similar to the event model in most popular programming languages. If you know how to work with Microsoft .NET or C++ events already, you have a head start. But you don't need to know that much about event model concepts to perform some basic tasks, such as attaching handlers.

When you use C#, Visual Basic or C++/CX as your programming language, the UI is defined in markup (XAML). In XAML markup syntax, some of the principles of connecting events between markup elements and runtime code entities are similar to other Web technologies, such as ASP.NET, or HTML5.

**Note**  The code that provides the runtime logic for a XAML-defined UI is often referred to as *code-behind* or the code-behind file. In the Microsoft Visual Studio solution views, this relationship is shown graphically, with the code-behind file being a dependent and nested file versus the XAML page it refers to.

## Button.Click: an introduction to events and XAML

One of the most common programming tasks for a Windows Runtime app is to capture user input to the UI. For example, your UI might have a button that the user must click to submit info or to change state.

You define the UI for your Windows Runtime app by generating XAML. This XAML is usually the output from a design surface in Visual Studio. You can also write the XAML in a plain-text editor or a third-party XAML editor. While generating that XAML, you can wire event handlers for individual UI elements at the same time that you define all the other XAML attributes that establish property values of that UI element.

To wire the events in XAML, you specify the string-form name of the handler method that you've already defined or will define later in your code-behind. For example, this XAML defines a [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) object with other properties ([x:Name attribute](x-name-attribute.md), [**Content**](/uwp/api/windows.ui.xaml.controls.contentcontrol.content)) assigned as attributes, and wires a handler for the button's [**Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event by referencing a method named `ShowUpdatesButton_Click`:

```xaml
<Button x:Name="showUpdatesButton"
  Content="{Binding ShowUpdatesText}"
  Click="ShowUpdatesButton_Click"/>
```

**Tip**  *Event wiring* is a programming term. It refers to the process or code whereby you indicate that occurrences of an event should invoke a named handler method. In most procedural code models, event wiring is implicit or explicit "AddHandler" code that names both the event and method, and usually involves a target object instance. In XAML, the "AddHandler" is implicit, and event wiring consists entirely of naming the event as the attribute name of an object element, and naming the handler as that attribute's value.

You write the actual handler in the programming language that you're using for all your app's code and code-behind. With the attribute `Click="ShowUpdatesButton_Click"`, you have created a contract that when the XAML is markup-compiled and parsed, both the XAML markup compile step in your IDE's build action and the eventual XAML parse when the app loads can find a method named `ShowUpdatesButton_Click` as part of the app's code. `ShowUpdatesButton_Click` must be a method that implements a compatible method signature (based on a delegate) for any handler of the [**Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event. For example, this code defines the `ShowUpdatesButton_Click` handler.

```csharp
private void ShowUpdatesButton_Click (object sender, RoutedEventArgs e) 
{
    Button b = sender as Button;
    //more logic to do here...
}
```

```vb
Private Sub ShowUpdatesButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
    Dim b As Button = CType(sender, Button)
    '  more logic to do here...
End Sub
```

```cppwinrt
void winrt::MyNamespace::implementation::BlankPage::ShowUpdatesButton_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& e)
{
    auto b{ sender.as<Windows::UI::Xaml::Controls::Button>() };
    // More logic to do here.
}
```

```cpp
void MyNamespace::BlankPage::ShowUpdatesButton_Click(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e) 
{
    Button^ b = (Button^) sender;
    //more logic to do here...
}
```

In this example, the `ShowUpdatesButton_Click` method is based on the [**RoutedEventHandler**](/uwp/api/windows.ui.xaml.routedeventhandler) delegate. You'd know that this is the delegate to use because you'll see that delegate named in the syntax for the [**Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) method.

**Tip**  Visual Studio provides a convenient way to name the event handler and define the handler method while you're editing XAML. When you provide the attribute name of the event in the XAML text editor, wait a moment until a Microsoft IntelliSense list displays. If you click **&lt;New Event Handler&gt;** from the list, Microsoft Visual Studio will suggest a method name based on the element's **x:Name** (or type name), the event name, and a numeric suffix. You can then right-click the selected event handler name and click **Navigate to Event Handler**. This will navigate directly to the newly inserted event handler definition, as seen in the code editor view of your code-behind file for the XAML page. The event handler already has the correct signature, including the *sender* parameter and the event data class that the event uses. Also, if a handler method with the correct signature already exists in your code-behind, that method's name appears in the auto-complete drop-down along with the **&lt;New Event Handler&gt;** option. You can also press the Tab key as a shortcut instead of clicking the IntelliSense list items.

## Defining an event handler

For objects that are UI elements and declared in XAML, event handler code is defined in the partial class that serves as the code-behind for a XAML page. Event handlers are methods that you write as part of the partial class that is associated with your XAML. These event handlers are based on the delegates that a particular event uses. Your event handler methods can be public or private. Private access works because the handler and instance created by the XAML are ultimately joined by code generation. In general, we recommend that you make your event handler methods private in the class.

**Note**  Event handlers for C++ don't get defined in partial classes, they are declared in the header as a private class member. The build actions for a C++ project take care of generating code that supports the XAML type system and code-behind model for C++.

### The *sender* parameter and event data

The handler you write for the event can access two values that are available as input for each case where your handler is invoked. The first such value is *sender*, which is a reference to the object where the handler is attached. The *sender* parameter is typed as the base **Object** type. A common technique is to cast *sender* to a more precise type. This technique is useful if you expect to check or change state on the *sender* object itself. Based on your own app design, you usually know a type that is safe to cast *sender* to, based on where the handler is attached or other design specifics.

The second value is event data, which generally appears in syntax definitions as the *e* parameter. You can discover which properties for event data are available by looking at the *e* parameter of the delegate that is assigned for the specific event you are handling, and then using IntelliSense or Object Browser in Visual Studio. Or you can use the Windows Runtime reference documentation.

For some events, the event data's specific property values are as important as knowing that the event occurred. This is especially true of the input events. For pointer events, the position of the pointer when the event occurred might be important. For keyboard events, all possible key presses fire a [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) and [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) event. To determine which key a user pressed, you must access the [**KeyRoutedEventArgs**](/uwp/api/Windows.UI.Xaml.Input.KeyRoutedEventArgs) that is available to the event handler. For more info about handling input events, see [Keyboard interactions](../design/input/keyboard-interactions.md) and [Handle pointer input](../design/input/handle-pointer-input.md). Input events and input scenarios often have additional considerations that are not covered in this topic, such as pointer capture for pointer events, and modifier keys and platform key codes for keyboard events.

### Event handlers that use the **async** pattern

In some cases you'll want to use APIs that use an **async** pattern within an event handler. For example, you might use a [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) in an [**AppBar**](/uwp/api/Windows.UI.Xaml.Controls.AppBar) to display a file picker and interact with it. However, many of the file picker APIs are asynchronous. They have to be called within an **async**/awaitable scope, and the compiler will enforce this. So what you can do is add the **async** keyword to your event handler such that the handler is now **async** **void**. Now your event handler is permitted to make **async**/awaitable calls.

For an example of user-interaction event handling using the **async** pattern, see [File access and pickers](/previous-versions/windows/apps/jj655411(v=win.10)) (part of the[Create your first Windows Runtime app using C# or Visual Basic](/previous-versions/windows/apps/hh974581(v=win.10)) series). See also [Call asynchronous APIs in C).

## Adding event handlers in code

XAML is not the only way to assign an event handler to an object. To add event handlers to any given object in code, including to objects that are not usable in XAML, you can use the language-specific syntax for adding event handlers.

In C#, the syntax is to use the `+=` operator. You register the handler by referencing the event handler method name on the right side of the operator.

If you use code to add event handlers to objects that appear in the run-time UI, a common practice is to add such handlers in response to an object lifetime event or callback, such as [**Loaded**](/uwp/api/windows.ui.xaml.frameworkelement.loaded) or [**OnApplyTemplate**](/uwp/api/windows.ui.xaml.frameworkelement.onapplytemplate), so that the event handlers on the relevant object are ready for user-initiated events at run time. This example shows a XAML outline of the page structure and then provides the C# language syntax for adding an event handler to an object.

```xaml
<Grid x:Name="LayoutRoot" Loaded="LayoutRoot_Loaded">
  <StackPanel>
    <TextBlock Name="textBlock1">Put the pointer over this text</TextBlock>
...
  </StackPanel>
</Grid>
```

```csharp
void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
{
    textBlock1.PointerEntered += textBlock1_PointerEntered;
    textBlock1.PointerExited += textBlock1_PointerExited;
}
```

**Note**  A more verbose syntax exists. In 2005, C# added a feature called delegate inference, which enables a compiler to infer the new delegate instance and enables the previous, simpler syntax. The verbose syntax is functionally identical to the previous example, but explicitly creates a new delegate instance before registering it, thus not taking advantage of delegate inference. This explicit syntax is less common, but you might still see it in some code examples.

```csharp
void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
{
    textBlock1.PointerEntered += new PointerEventHandler(textBlock1_PointerEntered);
    textBlock1.PointerExited += new MouseEventHandler(textBlock1_PointerExited);
}
```

There are two possibilities for Visual Basic syntax. One is to parallel the C# syntax and attach handlers directly to instances. This requires the **AddHandler** keyword and also the **AddressOf** operator that dereferences the handler method name.

The other option for Visual Basic syntax is to use the **Handles** keyword on event handlers. This technique is appropriate for cases where handlers are expected to exist on objects at load time and persist throughout the object lifetime. Using **Handles** on an object that is defined in XAML requires that you provide a **Name** / **x:Name**. This name becomes the instance qualifier that is needed for the *Instance.Event* part of the **Handles** syntax. In this case you don't need an object lifetime-based event handler to initiate attaching the other event handlers; the **Handles** connections are created when you compile your XAML page.

```vb
Private Sub textBlock1_PointerEntered(ByVal sender As Object, ByVal e As PointerRoutedEventArgs) Handles textBlock1.PointerEntered
' ...
End Sub
```

**Note**  Visual Studio and its XAML design surface generally promote the instance-handling technique instead of the **Handles** keyword. This is because establishing the event handler wiring in XAML is part of typical designer-developer workflow, and the **Handles** keyword technique is incompatible with wiring the event handlers in XAML.

In C++/CX, you also use the **+=** syntax, but there are differences from the basic C# form:

- No delegate inference exists, so you must use **ref new** for the delegate instance.
- The delegate constructor has two parameters, and requires the target object as the first parameter. Typically you specify **this**.
- The delegate constructor requires the method address as the second parameter, so the **&** reference operator precedes the method name.

```cppwinrt
textBlock1().PointerEntered({this, &MainPage::TextBlock1_PointerEntered });
```

```cpp
textBlock1->PointerEntered += 
ref new PointerEventHandler(this, &BlankPage::textBlock1_PointerEntered);
```

### Removing event handlers in code

It's not usually necessary to remove event handlers in code, even if you added them in code. The object lifetime behavior for most Windows Runtime objects such as pages and controls will destroy the objects when they are disconnected from the main [**Window**](/uwp/api/Windows.UI.Xaml.Window) and its visual tree, and any delegate references are destroyed too. .NET does this through garbage collection and Windows Runtime with C++/CX uses weak references by default.

There are some rare cases where you do want to remove event handlers explicitly. These include:

- Handlers you added for static events, which can't get garbage-collected in a conventional way. Examples of static events in the Windows Runtime API are the events of the [**CompositionTarget**](/uwp/api/Windows.UI.Xaml.Media.CompositionTarget) and [**Clipboard**](/uwp/api/Windows.ApplicationModel.DataTransfer.Clipboard) classes.
- Test code where you want the timing of handler removal to be immediate, or code where you what to swap old/new event handlers for an event at run time.
- The implementation of a custom **remove** accessor.
- Custom static events.
- Handlers for page navigations.

[**FrameworkElement.Unloaded**](/uwp/api/windows.ui.xaml.frameworkelement.unloaded) or [**Page.NavigatedFrom**](/uwp/api/windows.ui.xaml.controls.page.onnavigatedfrom) are possible event triggers that have appropriate positions in state management and object lifetime such that you can use them for removing handlers for other events.

For example, you can remove an event handler named **textBlock1\_PointerEntered** from the target object **textBlock1** using this code.

```csharp
textBlock1.PointerEntered -= textBlock1_PointerEntered;
```

```vb
RemoveHandler textBlock1.PointerEntered, AddressOf textBlock1_PointerEntered
```

You can also remove handlers for cases where the event was added through a XAML attribute, which means that the handler was added in generated code. This is easier to do if you provided a **Name** value for the element where the handler was attached, because that provides an object reference for code later; however, you could also walk the object tree in order to find the necessary object reference in cases where the object has no **Name**.

If you need to remove an event handler in C++/CX, you'll need a registration token, which you should've received from the return value of the `+=` event handler registration. That's because the value you use for the right side of the `-=` deregistration in the C++/CX syntax is the token, not the method name. For C++/CX, you can't remove handlers that were added as a XAML attribute because the C++/CX generated code doesn't save a token.

## Routed events

The Windows Runtime with C#, Microsoft Visual Basic or C++/CX supports the concept of a routed event for a set of events that are present on most UI elements. These events are for input and user interaction scenarios, and they are implemented on the [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) base class. Here's a list of input events that are routed events:

- [**BringIntoViewRequested**](/uwp/api/windows.ui.xaml.uielement.bringintoviewrequested)
- [**CharacterReceived**](/uwp/api/windows.ui.xaml.uielement.characterreceived)
- [**ContextCanceled**](/uwp/api/windows.ui.xaml.uielement.contextcanceled)
- [**ContextRequested**](/uwp/api/windows.ui.xaml.uielement.contextrequested)
- [**DoubleTapped**](/uwp/api/windows.ui.xaml.uielement.doubletapped)
- [**DragEnter**](/uwp/api/windows.ui.xaml.uielement.dragenter)
- [**DragLeave**](/uwp/api/windows.ui.xaml.uielement.dragleave)
- [**DragOver**](/uwp/api/windows.ui.xaml.uielement.dragover)
- [**DragStarting**](/uwp/api/windows.ui.xaml.uielement.dragstarting)
- [**Drop**](/uwp/api/windows.ui.xaml.uielement.drop)
- [**DropCompleted**](/uwp/api/windows.ui.xaml.uielement.dropcompleted)
- [**GettingFocus**](/uwp/api/windows.ui.xaml.uielement.gettingfocus)
- [**GotFocus**](/uwp/api/windows.ui.xaml.uielement.gotfocus)
- [**Holding**](/uwp/api/windows.ui.xaml.uielement.holding)
- [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown)
- [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup)
- [**LosingFocus**](/uwp/api/windows.ui.xaml.uielement.losingfocus)
- [**LostFocus**](/uwp/api/windows.ui.xaml.uielement.lostfocus)
- [**ManipulationCompleted**](/uwp/api/windows.ui.xaml.uielement.manipulationcompleted)
- [**ManipulationDelta**](/uwp/api/windows.ui.xaml.uielement.manipulationdelta)
- [**ManipulationInertiaStarting**](/uwp/api/windows.ui.xaml.uielement.manipulationinertiastarting)
- [**ManipulationStarted**](/uwp/api/windows.ui.xaml.uielement.manipulationstarted)
- [**ManipulationStarting**](/uwp/api/windows.ui.xaml.uielement.manipulationstarting)
- [**NoFocusCandidateFound**](/uwp/api/windows.ui.xaml.uielement.nofocuscandidatefoundeventargs)
- [**PointerCanceled**](/uwp/api/windows.ui.xaml.uielement.pointercanceled)
- [**PointerCaptureLost**](/uwp/api/windows.ui.xaml.uielement.pointercapturelost)
- [**PointerEntered**](/uwp/api/windows.ui.xaml.uielement.pointerentered)
- [**PointerExited**](/uwp/api/windows.ui.xaml.uielement.pointerexited)
- [**PointerMoved**](/uwp/api/windows.ui.xaml.uielement.pointermoved)
- [**PointerPressed**](/uwp/api/windows.ui.xaml.uielement.pointerpressed)
- [**PointerReleased**](/uwp/api/windows.ui.xaml.uielement.pointerreleased)
- [**PointerWheelChanged**](/uwp/api/windows.ui.xaml.uielement.pointerwheelchanged)
- [**PreviewKeyDown**](/uwp/api/windows.ui.xaml.uielement.previewkeydown)
- [**PreviewKeyUp**](/uwp/api/windows.ui.xaml.uielement.previewkeyup)
- [**PointerWheelChanged**](/uwp/api/windows.ui.xaml.uielement.pointerwheelchanged)
- [**RightTapped**](/uwp/api/windows.ui.xaml.uielement.righttapped)
- [**Tapped**](/uwp/api/windows.ui.xaml.uielement.tapped)

A routed event is an event that is potentially passed on (*routed*) from a child object to each of its successive parent objects in an object tree. The XAML structure of your UI approximates this tree, with the root of that tree being the root element in XAML. The true object tree might vary somewhat from the XAML element nesting, because the object tree doesn't include XAML language features such as property element tags. You can conceive of the routed event as *bubbling* from any XAML object element child element that fires the event, toward the parent object element that contains it. The event and its event data can be handled on multiple objects along the event route. If no element has handlers, the route potentially keeps going until the root element is reached.

If you know Web technologies such as Dynamic HTML (DHTML) or HTML5, you might already be familiar with the *bubbling* event concept.

When a routed event bubbles through its event route, any attached event handlers all access a shared instance of event data. Therefore, if any of the event data is writeable by a handler, any changes made to event data will be passed on to the next handler, and may no longer represent the original event data from the event. When an event has a routed event behavior, the reference documentation will include remarks or other notations about the routed behavior.

### The **OriginalSource** property of **RoutedEventArgs**

When an event bubbles up an event route, *sender* is no longer the same object as the event-raising object. Instead, *sender* is the object where the handler that is being invoked is attached.

In some cases, *sender* is not interesting, and you are instead interested in info such as which of the possible child objects the pointer is over when a pointer event fired, or which object in a larger UI held focus when a user pressed a keyboard key. For these cases, you can use the value of the [**OriginalSource**](/uwp/api/windows.ui.xaml.routedeventargs.originalsource) property. At all points on the route, **OriginalSource** reports the original object that fired the event, instead of the object where the handler is attached. However, for [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) input events, that original object is often an object that is not immediately visible in the page-level UI definition XAML. Instead, that original source object might be a templated part of a control. For example, if the user hovers the pointer over the very edge of a [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button), for most pointer events the **OriginalSource** is a [**Border**](/uwp/api/Windows.UI.Xaml.Controls.Border) template part in the [**Template**](/uwp/api/windows.ui.xaml.controls.control.template), not the **Button** itself.

**Tip**  Input event bubbling is especially useful if you are creating a templated control. Any control that has a template can have a new template applied by its consumer. The consumer that's trying to recreate a working template might unintentionally eliminate some event handling declared in the default template. You can still provide control-level event handling by attaching handlers as part of the [**OnApplyTemplate**](/uwp/api/windows.ui.xaml.frameworkelement.onapplytemplate) override in the class definition. Then you can catch the input events that bubble up to the control's root on instantiation.

### The **Handled** property

Several event data classes for specific routed events contain a property named **Handled**. For examples, see [**PointerRoutedEventArgs.Handled**](/uwp/api/windows.ui.xaml.input.pointerroutedeventargs.handled), [**KeyRoutedEventArgs.Handled**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.handled), [**DragEventArgs.Handled**](/uwp/api/windows.ui.xaml.drageventargs.handled). In all cases **Handled** is a settable Boolean property.

Setting the **Handled** property to **true** influences the event system behavior. When **Handled** is **true**, the routing stops for most event handlers; the event doesn't continue along the route to notify other attached handlers of that particular event case. What "handled" means in the context of the event and how your app responds to it is up to you. Basically, **Handled** is a simple protocol that enables app code to state that an occurrence of an event doesn't need to bubble to any containers, your app logic has taken care of what needs done. Conversely though, you do have to be careful that you aren't handling events that probably should bubble so that built-in system or control behaviors can act. For example, handling low-level events within the parts or items of a selection control can be detrimental. The selection control might be looking for input events to know that the selection should change.

Not all of the routed events can cancel a route in this way, and you can tell that because they won't have a **Handled** property. For example, [**GotFocus**](/uwp/api/windows.ui.xaml.uielement.gotfocus) and [**LostFocus**](/uwp/api/windows.ui.xaml.uielement.lostfocus) do bubble, but they always bubble all the way to the root, and their event data classes don't have a **Handled** property that can influence that behavior.

##  Input event handlers in controls

Specific Windows Runtime controls sometimes use the **Handled** concept for input events internally. This can make it seem like an input event never occurs, because your user code can't handle it. For example, the [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) class includes logic that deliberately handles the general input event [**PointerPressed**](/uwp/api/windows.ui.xaml.uielement.pointerpressed). It does so because buttons fire a [**Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event that is initiated by pointer-pressed input, as well as by other input modes such as handling keys like the Enter key that can invoke the button when it's focused. For purposes of the class design of **Button**, the raw input event is conceptually handled, and class consumers such as your user code can instead interact with the control-relevant **Click** event. Topics for specific control classes in the Windows Runtime API reference often note the event handling behavior that the class implements. In some cases, you can change the behavior by overriding **On**_Event_ methods. For example, you can change how your [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox) derived class reacts to key input by overriding [**Control.OnKeyDown**](/uwp/api/windows.ui.xaml.controls.control.onkeydown).

##  Registering handlers for already-handled routed events

Earlier we said that setting **Handled** to **true** prevents most handlers from being called. But the [**AddHandler**](/uwp/api/windows.ui.xaml.uielement.addhandler) method provides a technique where you can attach a handler that is always invoked for the route, even if some other handler earlier in the route has set **Handled** to **true** in the shared event data. This technique is useful if a control you are using has handled the event in its internal compositing or for control-specific logic. but you still want to respond to it from a control instance, or your app UI. But use this technique with caution, because it can contradict the purpose of **Handled** and possibly break a control's intended interactions.

Only the routed events that have a corresponding routed event identifier can use the [**AddHandler**](/uwp/api/windows.ui.xaml.uielement.addhandler) event handling technique, because the identifier is a required input of the **AddHandler** method. See the reference documentation for [**AddHandler**](/uwp/api/windows.ui.xaml.uielement.addhandler) for a list of events that have routed event identifiers available. For the most part this is the same list of routed events we showed you earlier. The exception is that the last two in the list: [**GotFocus**](/uwp/api/windows.ui.xaml.uielement.gotfocus) and [**LostFocus**](/uwp/api/windows.ui.xaml.uielement.lostfocus) don't have a routed event identifier, so you can't use **AddHandler** for those.

## Routed events outside the visual tree

Certain objects participate in a relationship with the primary visual tree that is conceptually like having an overlay over the main visuals. These objects are not part of the usual parent-child relationships that connect all tree elements to the visual root. This is the case for any displayed [**Popup**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.Popup) or [**ToolTip**](/uwp/api/Windows.UI.Xaml.Controls.ToolTip). If you want to handle routed events from a **Popup** or **ToolTip**, place the handlers on specific UI elements that are within the **Popup** or **ToolTip** and not the **Popup** or **ToolTip** elements themselves. Don't rely on routing inside any compositing that is performed for **Popup** or **ToolTip** content. This is because event routing for routed events works only along the main visual tree. A **Popup** or **ToolTip** is not considered a parent of subsidiary UI elements and never receives the routed event, even if it is trying to use something like the **Popup** default background as the capture area for input events.

## Hit testing and input events

Determining whether and where in UI an element is visible to mouse, touch, and stylus input is called *hit testing*. For touch actions and also for interaction-specific or manipulation events that are consequences of a touch action, an element must be hit-test visible in order to be the event source and fire the event that is associated with the action. Otherwise, the action passes through the element to any underlying elements or parent elements in the visual tree that could interact with that input. There are several factors that affect hit testing, but you can determine whether a given element can fire input events by checking its [**IsHitTestVisible**](/uwp/api/windows.ui.xaml.uielement.ishittestvisible) property. This property returns **true** only if the element meets these criteria:

- The element's [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) property value is [**Visible**](/uwp/api/Windows.UI.Xaml.Visibility).
- The element's **Background** or **Fill** property value is not **null**. A **null** [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) value results in transparency and hit test invisibility. (To make an element transparent but also hit testable, use a [**Transparent**](/uwp/api/windows.ui.colors.transparent) brush instead of **null**.)

**Note**  **Background** and **Fill** aren't defined by [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement), and are instead defined by different derived classes such as [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control) and [**Shape**](/uwp/api/Windows.UI.Xaml.Shapes.Shape). But the implications of brushes you use for foreground and background properties are the same for hit testing and input events, no matter which subclass implements the properties.

- If the element is a control, its [**IsEnabled**](/uwp/api/windows.ui.xaml.controls.control.isenabled) property value must be **true**.
- The element must have actual dimensions in layout. An element where either [**ActualHeight**](/uwp/api/windows.ui.xaml.frameworkelement.actualheight) and [**ActualWidth**](/uwp/api/windows.ui.xaml.frameworkelement.actualwidth) are 0 won't fire input events.

Some controls have special rules for hit testing. For example, [**TextBlock**](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) has no **Background** property, but is still hit testable within the entire region of its dimensions. [**Image**](/uwp/api/Windows.UI.Xaml.Controls.Image) and [**MediaElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaElement) controls are hit testable over their defined rectangle dimensions, regardless of transparent content such as alpha channel in the media source file being displayed. [**WebView**](/uwp/api/Windows.UI.Xaml.Controls.WebView) controls have special hit testing behavior because the input can be handled by the hosted HTML and fire script events.

Most [**Panel**](/uwp/api/Windows.UI.Xaml.Controls.Panel) classes and [**Border**](/uwp/api/Windows.UI.Xaml.Controls.Border) are not hit-testable in their own background, but can still handle the user input events that are routed from the elements that they contain.

You can determine which elements are located at the same position as a user input event, regardless of whether the elements are hit-testable. To do this, call the [**FindElementsInHostCoordinates**](/uwp/api/windows.ui.xaml.media.visualtreehelper.findelementsinhostcoordinates) method. As the name implies, this method finds the elements at a location relative to a specified host element. However, applied transforms and layout changes can adjust the relative coordinate system of an element, and therefore affect which elements are found at a given location.

## Commanding

A small number of UI elements support *commanding*. Commanding uses input-related routed events in its underlying implementation and enables processing of related UI input (a certain pointer action, a specific accelerator key) by invoking a single command handler. If commanding is available for a UI element, consider using its commanding APIs instead of any discrete input events. You typically use a **Binding** reference into properties of a class that defines the view model for data. The properties hold named commands that implement the language-specific **ICommand** commanding pattern. For more info, see [**ButtonBase.Command**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.command).

## Custom events in the Windows Runtime

For purposes of defining custom events, how you add the event and what that means for your class design is highly dependent on which programming language you are using.

- For C# and Visual Basic, you are defining a CLR event. You can use the standard .NET event pattern, so long as you aren't using custom accessors (**add**/**remove**). Additional tips:
    - For the event handler it's a good idea to use [**System.EventHandler<TEventArgs>**](/dotnet/api/system.eventhandler-1) because it has built-in translation to the Windows Runtime generic event delegate [**EventHandler<T>**](/uwp/api/windows.foundation.eventhandler).
    - Don't base your event data class on [**System.EventArgs**](/dotnet/api/system.eventargs) because it doesn't translate to the Windows Runtime. Use an existing event data class or no base class at all.
    - If you are using custom accessors, see [Custom events and event accessors in Windows Runtime components](/previous-versions/windows/apps/hh972883(v=vs.140)).
    - If you're not clear on what the standard .NET event pattern is, see [Defining Events for Custom Silverlight Classes](/previous-versions/windows/). This is written for Microsoft Silverlight but it's still a good summation of the code and concepts for the standard .NET event pattern.
- For C++/CX, see [Events (C++/CX)](/cpp/cppcx/events-c-cx).
    - Use named references even for your own usages of custom events. Don't use lambda for custom events, it can create a circular reference.

You can't declare a custom routed event for Windows Runtime; routed events are limited to the set that comes from the Windows Runtime.

Defining a custom event is usually done as part of the exercise of defining a custom control. It's a common pattern to have a dependency property that has a property-changed callback, and to also define a custom event that's fired by the dependency property callback in some or all cases. Consumers of your control don't have access to the property-changed callback you defined, but having a notification event available is the next best thing. For more info, see [Custom dependency properties](custom-dependency-properties.md).

## Related topics

* [XAML overview](xaml-overview.md)
* [Quickstart: Touch input](/previous-versions/windows/apps/hh465387(v=win.10))
* [Keyboard interactions](../design/input/keyboard-interactions.md)
* [.NET events and delegates](/previous-versions/17sde2xt(v=vs.110))
* [Creating Windows Runtime components](/previous-versions/windows/apps/hh441572(v=vs.140))
* [**AddHandler**](/uwp/api/windows.ui.xaml.uielement.addhandler)