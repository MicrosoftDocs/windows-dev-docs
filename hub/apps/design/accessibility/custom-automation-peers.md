---
description: Describes the concept of automation peers for Microsoft UI Automation, and how you can provide automation support for your own custom UI class.
ms.assetid: AA8DA53B-FE6E-40AC-9F0A-CB09637C87B4
title: Custom automation peers
label: Custom automation peers
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Custom automation peers  

Describes the concept of automation peers for Microsoft UI Automation, and how you can provide automation support for your own custom UI class.

UI Automation provides a framework that automation clients can use to examine or operate the user interfaces of a variety of UI platforms and frameworks. If you are writing a Windows app, the classes that you use for your UI already provide UI Automation support. You can derive from existing, non-sealed classes to define a new kind of UI control or support class. In the process of doing so, your class might add behavior that should have accessibility support but that the default UI Automation support does not cover. In this case, you should extend the existing UI Automation support by deriving from the [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer) class that the base implementation used, adding any necessary support to your peer implementation, and informing the Windows app control infrastructure that it should create your new peer.

UI Automation enables not only accessibility applications and assistive technologies, such as screen readers, but also quality-assurance (test) code. In either scenario, UI Automation clients can examine user-interface elements and simulate user interaction with your app from other code outside your app. For info about UI Automation across all platforms and in its wider meaning, see [UI Automation Overview](/windows/desktop/WinAuto/uiauto-uiautomationoverview).

There are two distinct audiences who use the UI Automation framework.

* **UI Automation *clients*** call UI Automation APIs to learn about all of the UI that is currently displayed to the user. For example, an assistive technology such as a screen reader acts as a UI Automation client. The UI is presented as a tree of automation elements that are related. The UI Automation client might be interested in just one app at a time, or in the entire tree. The UI Automation client can use UI Automation APIs to navigate the tree and to read or change information in the automation elements.
* **UI Automation *providers*** contribute information to the UI Automation tree, by implementing APIs that expose the elements in the UI that they introduced as part of their app. When you create a new control, you should now act as a participant in the UI Automation provider scenario. As a provider, you should ensure that all UI Automation clients can use the UI Automation framework to interact with your control for both accessibility and testing purposes.

Typically there are parallel APIs in the UI Automation framework: one API for UI Automation clients and another, similarly named API for UI Automation providers. For the most part, this topic covers the APIs for the UI Automation provider, and specifically the classes and interfaces that enable provider extensibility in that UI framework. Occasionally we mention UI Automation APIs that the UI Automation clients use, to provide some perspective, or provide a lookup table that correlates the client and provider APIs. For more info about the client perspective, see [UI Automation Client Programmer's Guide](/windows/desktop/WinAuto/uiauto-clientportal).

> [!NOTE]
> UI Automation clients don't typically use managed code and aren't typically implemented as a UWP app (they are usually desktop apps). UI Automation is based on a standard and not a specific implementation or framework. Many existing UI Automation clients, including assistive technology products such as screen readers, use Component Object Model (COM) interfaces to interact with UI Automation, the system, and the apps that run in child windows. For more info on the COM interfaces and how to write a UI Automation client using COM, see [UI Automation Fundamentals](/windows/desktop/WinAuto/entry-uiautocore-overview).

<span id="Determining_the_existing_state_of_UI_Automation_support_for_your_custom_UI_class"></span>
<span id="determining_the_existing_state_of_ui_automation_support_for_your_custom_ui_class"></span>
<span id="DETERMINING_THE_EXISTING_STATE_OF_UI_AUTOMATION_SUPPORT_FOR_YOUR_CUSTOM_UI_CLASS"></span>

## Determining the existing state of UI Automation support for your custom UI class  
Before you attempt to implement an automation peer for a custom control, you should test whether the base class and its automation peer already provides the accessibility or automation support that you need. In many cases, the combination of the [**FrameworkElementAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer) implementations, specific peers, and the patterns they implement can provide a basic but satisfactory accessibility experience. Whether this is true depends on how many changes you made to the object model exposure to your control versus its base class. Also, this depends on whether your additions to base class functionality correlate to new UI elements in the template contract or to the visual appearance of the control. In some cases your changes might introduce new aspects of user experience that require additional accessibility support.

Even if using the existing base peer class provides the basic accessibility support, it is still a best practice to define a peer so that you can report precise **ClassName** information to UI Automation for automated testing scenarios. This consideration is especially important if you are writing a control that is intended for third-party consumption.

<span id="Automation_peer_classes__"></span>
<span id="automation_peer_classes__"></span>
<span id="AUTOMATION_PEER_CLASSES__"></span>

## Automation peer classes  
The UWP builds on existing UI Automation techniques and conventions used by previous managed-code UI frameworks such as Windows Forms, Windows Presentation Foundation (WPF) and Microsoft Silverlight. Many of the control classes and their function and purpose also have their origin in a previous UI framework.

By convention, peer class names begin with the control class name and end with "AutomationPeer". For example, [**ButtonAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.ButtonAutomationPeer) is the peer class for the [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) control class.

> [!NOTE]
> For purposes of this topic, we treat the properties that are related to accessibility as being more important when you implement a control peer. But for a more general concept of UI Automation support, you should implement a peer in accordance with recommendations as documented by the [UI Automation Provider Programmer's Guide](/windows/desktop/WinAuto/uiauto-providerportal) and [UI Automation Fundamentals](/windows/desktop/WinAuto/entry-uiautocore-overview). Those topics don't cover the specific [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer) APIs that you would use to provide the information in the UWP framework for UI Automation, but they do describe the properties that identify your class or provide other information or interaction.

<span id="Peers__patterns_and_control_types"></span>
<span id="peers__patterns_and_control_types"></span>
<span id="PEERS__PATTERNS_AND_CONTROL_TYPES"></span>

## Peers, patterns and control types  
A *control pattern* is an interface implementation that exposes a particular aspect of a control's functionality to a UI Automation client. UI Automation clients use the properties and methods exposed through a control pattern to retrieve information about capabilities of the control, or to manipulate the control's behavior at run time.

Control patterns provide a way to categorize and expose a control's functionality independent of the control type or the appearance of the control. For example, a control that presents a tabular interface uses the **Grid** control pattern to expose the number of rows and columns in the table, and to enable a UI Automation client to retrieve items from the table. As other examples, the UI Automation client can use the **Invoke** control pattern for controls that can be invoked, such as buttons, and the **Scroll** control pattern for controls that have scroll bars, such as list boxes, list views, or combo boxes. Each control pattern represents a separate type of functionality, and control patterns can be combined to describe the full set of functionality supported by a particular control.

Control patterns relate to UI as interfaces relate to COM objects. In COM, you can query an object to ask what interfaces it supports and then use those interfaces to access functionality. In UI Automation, UI Automation clients can query a UI Automation element to find out which control patterns it supports, and then interact with the element and its peered control through the properties, methods, events, and structures exposed by the supported control patterns.

One of the main purposes of an automation peer is to report to a UI Automation client which control patterns the UI element can support through its peer. To do this, UI Automation providers implement new peers that change the [**GetPattern**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpattern) method behavior by overriding the [**GetPatternCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpatterncore) method. UI Automation clients make calls that the UI Automation provider maps to calling **GetPattern**. UI Automation clients query for each specific pattern that they want to interact with. If the peer supports the pattern, it returns an object reference to itself; otherwise it returns **null**. If the return is not **null**, the UI Automation client expects that it can call APIs of the pattern interface as a client, in order to interact with that control pattern.

A *control type* is a way to broadly define the functionality of a control that the peer represents. This is a different concept than a control pattern because while a pattern informs UI Automation what info it can get or what actions it can perform through a particular interface, the control type exists one level above that. Each control type has guidance about these aspects of UI Automation:

* UI Automation control patterns: A control type might support more than one pattern, each of which represents a different classification of info or interaction. Each control type has a set of control patterns that the control must support, a set that is optional, and a set that the control must not support.
* UI Automation property values: Each control type has a set of properties that the control must support. These are the general properties, as described in [UI Automation Properties Overview](/windows/desktop/WinAuto/uiauto-propertiesoverview), not the ones that are pattern-specific.
* UI Automation events: Each control type has a set of events that the control must support. Again these are general, not pattern-specific, as described in [UI Automation Events Overview](/windows/desktop/WinAuto/uiauto-eventsoverview).
* UI Automation tree structure: Each control type defines how the control must appear in the UI Automation tree structure.

Regardless of how automation peers for the framework are implemented, UI Automation client functionality isn't tied to the UWP, and in fact it's likely that existing UI Automation clients such as assistive technologies will use other programming models, such as COM. In COM, clients can **QueryInterface** for the COM control pattern interface that implements the requested pattern or the general UI Automation framework for properties, events or tree examination. For the patterns, the UI Automation framework marshals that interface code across into UWP code running against the app's UI Automation provider and the relevant peer.

When you implement control patterns for a managed-code framework such as a UWP app using C\# or Microsoft Visual Basic, you can use .NET Framework interfaces to represent these patterns instead of using the COM interface representation. For example, the UI Automation pattern interface for a Microsoft .NET provider implementation of the **Invoke** pattern is [**IInvokeProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IInvokeProvider).

For a list of control patterns, provider interfaces, and their purpose, see [Control patterns and interfaces](control-patterns-and-interfaces.md). For the list of the control types, see [UI Automation Control Types Overview](/windows/desktop/WinAuto/uiauto-controltypesoverview).

<span id="Guidance_for_how_to_implement_control_patterns"></span>
<span id="guidance_for_how_to_implement_control_patterns"></span>
<span id="GUIDANCE_FOR_HOW_TO_IMPLEMENT_CONTROL_PATTERNS"></span>

### Guidance for how to implement control patterns  
The control patterns and what they're intended for are part of a larger definition of the UI Automation framework, and don't just apply to the accessibility support for a UWP app. When you implement a control pattern you should make sure you're implementing it in a way that matches the guidance as documented in these docs and also in the UI Automation specification. If you're looking for guidance, you can generally use the Microsoft documentation and won't need to refer to the specification. Guidance for each pattern is documented here: [Implementing UI Automation Control Patterns](/windows/desktop/WinAuto/uiauto-implementinguiautocontrolpatterns). You'll notice that each topic under this area has an "Implementation Guidelines and Conventions" section and "Required Members" section. The guidance usually refers to specific APIs of the relevant control pattern interface in the [Control Pattern Interfaces for Providers](/windows/desktop/WinAuto/uiauto-cpinterfaces) reference. Those interfaces are the native/COM interfaces (and their APIs use COM-style syntax). But everything you see there has an equivalent in the [**Windows.UI.Xaml.Automation.Provider**](/uwp/api/Windows.UI.Xaml.Automation.Provider) namespace.

If you're using the default automation peers and expanding on their behavior, those peers have already been written in conformance to UI Automation guidelines. If they support control patterns, you can rely on that pattern support conforming with guidance at [Implementing UI Automation Control Patterns](/windows/desktop/WinAuto/uiauto-implementinguiautocontrolpatterns). If a control peer reports that it's representative of a control type defined by UI Automation, then the guidance documented at [Supporting UI Automation Control Types](/windows/desktop/WinAuto/uiauto-supportinguiautocontroltypes) has been followed by that peer.

Nevertheless you might need additional guidance for control patterns or control types in order to follow the UI Automation recommendations in your peer implementation. That would be particularly true if you're implementing pattern or control type support that doesn't yet exist as a default implementation in a UWP control. For example, the pattern for annotations isn't implemented in any of the default XAML controls. But you might have an app that uses annotations extensively and therefore you want to surface that functionality to be accessible. For this scenario, your peer should implement [**IAnnotationProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IAnnotationProvider) and should probably report itself as the **Document** control type with appropriate properties to indicate that your documents support annotation.

We recommend that you use the guidance that you see for the patterns under [Implementing UI Automation Control Patterns](/windows/desktop/WinAuto/uiauto-implementinguiautocontrolpatterns) or control types under [Supporting UI Automation Control Types](/windows/desktop/WinAuto/uiauto-supportinguiautocontroltypes) as orientation and general guidance. You might even try following some of the API links for descriptions and remarks as to the purpose of the APIs. But for syntax specifics that are needed for UWP app programming, find the equivalent API within the [**Windows.UI.Xaml.Automation.Provider**](/uwp/api/Windows.UI.Xaml.Automation.Provider) namespace and use those reference pages for more info.

<span id="Built-in_automation_peer_classes"></span>
<span id="built-in_automation_peer_classes"></span>
<span id="BUILT-IN_AUTOMATION_PEER_CLASSES"></span>

## Built-in automation peer classes  
In general, elements implement an automation peer class if they accept UI activity from the user, or if they contain information needed by users of assistive technologies that represent the interactive or meaningful UI of apps. Not all UWP visual elements have automation peers. Examples of classes that implement automation peers are [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) and [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox). Examples of classes that do not implement automation peers are [**Border**](/uwp/api/Windows.UI.Xaml.Controls.Border) and classes based on [**Panel**](/uwp/api/Windows.UI.Xaml.Controls.Panel), such as [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid) and [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas). A **Panel** has no peer because it is providing a layout behavior that is visual only. There is no accessibility-relevant way for the user to interact with a **Panel**. Whatever child elements a **Panel** contains are instead reported to UI Automation trees as child elements of the next available parent in the tree that has a peer or element representation.

<span id="UI_Automation_and_UWP_process_boundaries"></span>
<span id="ui_automation_and_uwp_process_boundaries"></span>
<span id="UI_AUTOMATION_AND_UWP_PROCESS_BOUNDARIES"></span>

## UI Automation and UWP process boundaries  
Typically, UI Automation client code that accesses a UWP app runs out-of-process. The UI Automation framework infrastructure enables information to get across the process boundary. This concept is explained in more detail in [UI Automation Fundamentals](/windows/desktop/WinAuto/entry-uiautocore-overview).

<span id="OnCreateAutomationPeer"></span>
<span id="oncreateautomationpeer"></span>
<span id="ONCREATEAUTOMATIONPEER"></span>

## OnCreateAutomationPeer  
All classes that derive from [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) contain the protected virtual method [**OnCreateAutomationPeer**](/uwp/api/windows.ui.xaml.uielement.oncreateautomationpeer). The object initialization sequence for automation peers calls **OnCreateAutomationPeer** to get the automation peer object for each control and thus to construct a UI Automation tree for run-time use. UI Automation code can use the peer to get information about a control’s characteristics and features and to simulate interactive use by means of its control patterns. A custom control that supports automation must override **OnCreateAutomationPeer** and return an instance of a class that derives from [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer). For example, if a custom control derives from the [**ButtonBase**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ButtonBase) class, the object returned by **OnCreateAutomationPeer** should derive from [**ButtonBaseAutomationPeer**](/uwp/api/windows.ui.xaml.automation.peers.buttonbaseautomationpeer).

If you're writing a custom control class and intend to also supply a new automation peer, you should override the [**OnCreateAutomationPeer**](/uwp/api/windows.ui.xaml.uielement.oncreateautomationpeer) method for your custom control so that it returns a new instance of your peer. Your peer class must derive directly or indirectly from [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer).

For example, the following code declares that the custom control `NumericUpDown` should use the peer `NumericUpDownPeer` for UI Automation purposes.

```csharp
using Windows.UI.Xaml.Automation.Peers;
...
public class NumericUpDown : RangeBase {
    public NumericUpDown() {
    // other initialization; DefaultStyleKey etc.
    }
    ...
    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new NumericUpDownAutomationPeer(this);
    }
}
```

```vb
Public Class NumericUpDown
    Inherits RangeBase
    ' other initialization; DefaultStyleKey etc.
       Public Sub New()
       End Sub
       Protected Overrides Function OnCreateAutomationPeer() As AutomationPeer
              Return New NumericUpDownAutomationPeer(Me)
       End Function
End Class
```

```cppwinrt
// NumericUpDown.idl
namespace MyNamespace
{
    runtimeclass NumericUpDown : Windows.UI.Xaml.Controls.Primitives.RangeBase
    {
        NumericUpDown();
        Int32 MyProperty;
    }
}

// NumericUpDown.h
...
struct NumericUpDown : NumericUpDownT<NumericUpDown>
{
	...
    Windows::UI::Xaml::Automation::Peers::AutomationPeer OnCreateAutomationPeer()
    {
        return winrt::make<MyNamespace::implementation::NumericUpDownAutomationPeer>(*this);
    }
};
```

```cpp
//.h
public ref class NumericUpDown sealed : Windows::UI::Xaml::Controls::Primitives::RangeBase
{
// other initialization not shown
protected:
    virtual AutomationPeer^ OnCreateAutomationPeer() override
    {
         return ref new NumericUpDownAutomationPeer(this);
    }
};
```

> [!NOTE]
> The [**OnCreateAutomationPeer**](/uwp/api/windows.ui.xaml.uielement.oncreateautomationpeer) implementation should do nothing more than initialize a new instance of your custom automation peer, passing the calling control as owner, and return that instance. Do not attempt additional logic in this method. In particular, any logic that could potentially lead to destruction of the [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer) within the same call may result in unexpected runtime behavior.

In typical implementations of [**OnCreateAutomationPeer**](/uwp/api/windows.ui.xaml.uielement.oncreateautomationpeer), the *owner* is specified as **this** or **Me** because the method override is in the same scope as the rest of the control class definition.

The actual peer class definition can be done in the same code file as the control or in a separate code file. The peer definitions all exist in the [**Windows.UI.Xaml.Automation.Peers**](/uwp/api/Windows.UI.Xaml.Automation.Peers) namespace that is a separate namespace from the controls that they provide peers for. You can choose to declare your peers in separate namespaces also, as long as you reference the necessary namespaces for the [**OnCreateAutomationPeer**](/uwp/api/windows.ui.xaml.uielement.oncreateautomationpeer) method call.

<span id="Choosing_the_correct_peer_base_class"></span>
<span id="choosing_the_correct_peer_base_class"></span>
<span id="CHOOSING_THE_CORRECT_PEER_BASE_CLASS"></span>

### Choosing the correct peer base class  
Make sure that your [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer) is derived from a base class that gives you the best match for the existing peer logic of the control class you are deriving from. In the case of the previous example, because `NumericUpDown` derives from [**RangeBase**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.RangeBase), there is a [**RangeBaseAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.RangeBaseAutomationPeer) class available that you should base your peer on. By using the closest matching peer class in parallel to how you derive the control itself, you can avoid overriding at least some of the [**IRangeValueProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IRangeValueProvider) functionality because the base peer class already implements it.

The base [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control) class does not have a corresponding peer class. If you need a peer class to correspond to a custom control that derives from **Control**, derive the custom peer class from [**FrameworkElementAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer).

If you derive from [**ContentControl**](/uwp/api/Windows.UI.Xaml.Controls.ContentControl) directly, that class has no default automation peer behavior because there is no [**OnCreateAutomationPeer**](/uwp/api/windows.ui.xaml.uielement.oncreateautomationpeer) implementation that references a peer class. So make sure either to implement **OnCreateAutomationPeer** to use your own peer, or to use [**FrameworkElementAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer) as the peer if that level of accessibility support is adequate for your control.

> [!NOTE]
> You don't typically derive from [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer) rather than [**FrameworkElementAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer). If you did derive directly from **AutomationPeer** you'll need to duplicate a lot of basic accessibility support that would otherwise come from **FrameworkElementAutomationPeer**.

<span id="Initialization_of_a_custom_peer_class"></span>
<span id="initialization_of_a_custom_peer_class"></span>
<span id="INITIALIZATION_OF_A_CUSTOM_PEER_CLASS"></span>

## Initialization of a custom peer class  
The automation peer should define a type-safe constructor that uses an instance of the owner control for base initialization. In the next example, the implementation passes the *owner* value on to the [**RangeBaseAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.RangeBaseAutomationPeer) base, and ultimately it is the [**FrameworkElementAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer) that actually uses *owner* to set [**FrameworkElementAutomationPeer.Owner**](/uwp/api/windows.ui.xaml.automation.peers.frameworkelementautomationpeer.owner).

```csharp
public NumericUpDownAutomationPeer(NumericUpDown owner): base(owner)
{}
```

```vb
Public Sub New(owner As NumericUpDown)
    MyBase.New(owner)
End Sub
```

```cppwinrt
// NumericUpDownAutomationPeer.idl
import "NumericUpDown.idl";
namespace MyNamespace
{
    runtimeclass NumericUpDownAutomationPeer : Windows.UI.Xaml.Automation.Peers.AutomationPeer
    {
        NumericUpDownAutomationPeer(NumericUpDown owner);
        Int32 MyProperty;
    }
}

// NumericUpDownAutomationPeer.h
...
struct NumericUpDownAutomationPeer : NumericUpDownAutomationPeerT<NumericUpDownAutomationPeer>
{
    ...
    NumericUpDownAutomationPeer(MyNamespace::NumericUpDown const& owner);
};
```

```cpp
//.h
public ref class NumericUpDownAutomationPeer sealed :  Windows::UI::Xaml::Automation::Peers::RangeBaseAutomationPeer
//.cpp
public:    NumericUpDownAutomationPeer(NumericUpDown^ owner);
```

<span id="Core_methods_of_AutomationPeer"></span>
<span id="core_methods_of_automationpeer"></span>
<span id="CORE_METHODS_OF_AUTOMATIONPEER"></span>

## Core methods of AutomationPeer  
For UWP infrastructure reasons, the overridable methods of an automation peer are part of a pair of methods: the public access method that the UI Automation provider uses as a forwarding point for UI Automation clients, and the protected "Core" customization method that a UWP class can override to influence the behavior. The method pair is wired together by default in such a way that the call to the access method always invokes the parallel "Core" method that has the provider implementation, or as a fallback, invokes a default implementation from the base classes.

When implementing a peer for a custom control, override any of the "Core" methods from the base automation peer class where you want to expose behavior that is unique to your custom control. UI Automation code gets information about your control by calling public methods of the peer class. To provide information about your control, override each method with a name that ends with "Core" when your control implementation and design creates accessibility scenarios or other UI Automation scenarios that differ from what's supported by the base automation peer class.

At a minimum, whenever you define a new peer class, implement the [**GetClassNameCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getclassnamecore) method, as shown in the next example.

```csharp
protected override string GetClassNameCore()
{
    return "NumericUpDown";
}
```

> [!NOTE]
> You might want to store the strings as constants rather than directly in the method body, but that is up to you. For [**GetClassNameCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getclassnamecore), you won't need to localize this string. The **LocalizedControlType** property is used any time a localized string is needed by a UI Automation client, not **ClassName**.

### GetAutomationControlType
<span id="getautomationcontroltype"></span>
<span id="GETAUTOMATIONCONTROLTYPE"></span>

Some assistive technologies use the [**GetAutomationControlType**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getautomationcontroltype) value directly when reporting characteristics of the items in a UI Automation tree, as additional information beyond the UI Automation **Name**. If your control is significantly different from the control you are deriving from and you want to report a different control type from what is reported by the base peer class used by the control, you must implement a peer and override [**GetAutomationControlTypeCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getautomationcontroltypecore) in your peer implementation. This is particularly important if you derive from a generalized base class such as [**ItemsControl**](/uwp/api/Windows.UI.Xaml.Controls.ItemsControl) or [**ContentControl**](/uwp/api/Windows.UI.Xaml.Controls.ContentControl), where the base peer doesn't provide precise information about control type.

Your implementation of [**GetAutomationControlTypeCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getautomationcontroltypecore) describes your control by returning an [**AutomationControlType**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationControlType) value. Although you can return **AutomationControlType.Custom**, you should return one of the more specific control types if it accurately describes your control's main scenarios. Here's an example.

```csharp
protected override AutomationControlType GetAutomationControlTypeCore()
{
    return AutomationControlType.Spinner;
}
```

> [!NOTE]
> Unless you specify [**AutomationControlType.Custom**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationControlType), you don't have to implement [**GetLocalizedControlTypeCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getlocalizedcontroltypecore) to provide a **LocalizedControlType** property value to clients. UI Automation common infrastructure provides translated strings for every possible **AutomationControlType** value other than **AutomationControlType.Custom**.

<span id="GetPattern_and_GetPatternCore"></span>
<span id="getpattern_and_getpatterncore"></span>
<span id="GETPATTERN_AND_GETPATTERNCORE"></span>

### GetPattern and GetPatternCore  
A peer's implementation of [**GetPatternCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpatterncore) returns the object that supports the pattern that is requested in the input parameter. Specifically, a UI Automation client calls a method that is forwarded to the provider's [**GetPattern**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpattern) method, and specifies a [**PatternInterface**](/uwp/api/Windows.UI.Xaml.Automation.Peers.PatternInterface) enumeration value that names the requested pattern. Your override of **GetPatternCore** should return the object that implements the specified pattern. That object is the peer itself, because the peer should implement the corresponding pattern interface any time that it reports that it supports a pattern. If your peer does not have a custom implementation of a pattern, but you know that the peer's base does implement the pattern, you can call the base type's implementation of **GetPatternCore** from your **GetPatternCore**. A peer's **GetPatternCore** should return **null** if a pattern is not supported by the peer. However, instead of returning **null** directly from your implementation, you would usually rely on the call to the base implementation to return **null** for any unsupported pattern.

When a pattern is supported, the [**GetPatternCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpatterncore) implementation can return **this** or **Me**. The expectation is that the UI Automation client will cast the [**GetPattern**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpattern) return value to the requested pattern interface whenever it is not **null**.

If a peer class inherits from another peer, and all necessary support and pattern reporting is already handled by the base class, implementing [**GetPatternCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpatterncore) isn't necessary. For example, if you are implementing a range control that derives from [**RangeBase**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.RangeBase), and your peer derives from [**RangeBaseAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.RangeBaseAutomationPeer), that peer returns itself for [**PatternInterface.RangeValue**](/uwp/api/Windows.UI.Xaml.Automation.Peers.PatternInterface) and has working implementations of the [**IRangeValueProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IRangeValueProvider) interface that supports the pattern.

Although it is not the literal code, this example approximates the implementation of [**GetPatternCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpatterncore) already present in [**RangeBaseAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.RangeBaseAutomationPeer).


```csharp
protected override object GetPatternCore(PatternInterface patternInterface)
{
    if (patternInterface == PatternInterface.RangeValue)
    {
        return this;
    }
    return base.GetPatternCore(patternInterface);
}
```

If you are implementing a peer where you don't have all the support you need from a base peer class, or you want to change or add to the set of base-inherited patterns that your peer can support, then you should override [**GetPatternCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpatterncore) to enable UI Automation clients to use the patterns.

For a list of the provider patterns that are available in the UWP implementation of UI Automation support, see [**Windows.UI.Xaml.Automation.Provider**](/uwp/api/Windows.UI.Xaml.Automation.Provider). Each such pattern has a corresponding value of the [**PatternInterface**](/uwp/api/Windows.UI.Xaml.Automation.Peers.PatternInterface) enumeration, which is how UI Automation clients request the pattern in a [**GetPattern**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpattern) call.

A peer can report that it supports more than one pattern. If so, the override should include return path logic for each supported [**PatternInterface**](/uwp/api/Windows.UI.Xaml.Automation.Peers.PatternInterface) value and return the peer in each matching case. It is expected that the caller will request only one interface at a time, and it is up to the caller to cast to the expected interface.

Here's an example of a [**GetPatternCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpatterncore) override for a custom peer. It reports the support for two patterns, [**IRangeValueProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IRangeValueProvider) and [**IToggleProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IToggleProvider). The control here is a media display control that can display as full-screen (the toggle mode) and that has a progress bar within which users can select a position (the range control). This code came from the [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample).


```csharp
protected override object GetPatternCore(PatternInterface patternInterface)
{
    if (patternInterface == PatternInterface.RangeValue)
    {
        return this;
    }
    else if (patternInterface == PatternInterface.Toggle)
    {
        return this;
    }
    return null;
}
```

<span id="Forwarding_patterns_from_sub-elements"></span>
<span id="forwarding_patterns_from_sub-elements"></span>
<span id="FORWARDING_PATTERNS_FROM_sub-elementS"></span>

### Forwarding patterns from sub-elements  
A [**GetPatternCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpatterncore) method implementation can also specify a sub-element or part as a pattern provider for its host. This example mimics how [**ItemsControl**](/uwp/api/Windows.UI.Xaml.Controls.ItemsControl) transfers scroll-pattern handling to the peer of its internal [**ScrollViewer**](/uwp/api/Windows.UI.Xaml.Controls.ScrollViewer) control. To specify a sub-element for pattern handling, this code gets the sub-element object, creates a peer for the sub-element by using the [**FrameworkElementAutomationPeer.CreatePeerForElement**](/uwp/api/windows.ui.xaml.automation.peers.frameworkelementautomationpeer.createpeerforelement) method, and returns the new peer.


```csharp
protected override object GetPatternCore(PatternInterface patternInterface)
{
    if (patternInterface == PatternInterface.Scroll)
    {
        ItemsControl owner = (ItemsControl) base.Owner;
        UIElement itemsHost = owner.ItemsHost;
        ScrollViewer element = null;
        while (itemsHost != owner)
        {
            itemsHost = VisualTreeHelper.GetParent(itemsHost) as UIElement;
            element = itemsHost as ScrollViewer;
            if (element != null)
            {
                break;
            }
        }
        if (element != null)
        {
            AutomationPeer peer = FrameworkElementAutomationPeer.CreatePeerForElement(element);
            if ((peer != null) && (peer is IScrollProvider))
            {
                return (IScrollProvider) peer;
            }
        }
    }
    return base.GetPatternCore(patternInterface);
}
```

<span id="Other_Core_methods"></span>
<span id="other_core_methods"></span>
<span id="OTHER_CORE_METHODS"></span>

### Other Core methods  
Your control may need to support keyboard equivalents for primary scenarios; for more info about why this might be necessary, see [Keyboard accessibility](keyboard-accessibility.md). Implementing the key support is necessarily part of the control code and not the peer code because that is part of a control's logic, but your peer class should override the [**GetAcceleratorKeyCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getacceleratorkeycore) and [**GetAccessKeyCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getaccesskeycore) methods to report to UI Automation clients which keys are used. Consider that the strings that report key information might need to be localized, and should therefore come from resources, not hard-coded strings.

If you are providing a peer for a class that supports a collection, it's best to derive from both functional classes and peer classes that already have that kind of collection support. If you can't do so, peers for controls that maintain child collections may have to override the collection-related peer method [**GetChildrenCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getchildrencore) to properly report the parent-child relationships to the UI Automation tree.

Implement the [**IsContentElementCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.iscontentelementcore) and [**IsControlElementCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.iscontrolelementcore) methods to indicate whether your control contains data content or fulfills an interactive role in the user interface (or both). By default, both methods return **true**. These settings improve the usability of assistive technologies such as screen readers, which may use these methods to filter the automation tree. If your [**GetPatternCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpatterncore) method transfers pattern handling to a sub-element peer, the sub-element peer's **IsControlElementCore** method can return **false** to hide the sub-element peer from the automation tree.

Some controls may support labeling scenarios, where a text label part supplies information for a non-text part, or a control is intended to be in a known labeling relationship with another control in the UI. If it's possible to provide a useful class-based behavior, you can override [**GetLabeledByCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getlabeledbycore) to provide this behavior.

[**GetBoundingRectangleCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getboundingrectanglecore) and [**GetClickablePointCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getclickablepointcore) are used mainly for automated testing scenarios. If you want to support automated testing for your control, you might want to override these methods. This might be desired for range-type controls, where you can't suggest just a single point because where the user clicks in coordinate space has a different effect on a range. For example, the default [**ScrollBar**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ScrollBar) automation peer overrides **GetClickablePointCore** to return a "not a number" [**Point**](/uwp/api/Windows.Foundation.Point) value.

[**GetLiveSettingCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getlivesettingcore) influences the control default for the **LiveSetting** value for UI Automation. You might want to override this if you want your control to return a value other than [**AutomationLiveSetting.Off**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationLiveSetting). For more info on what **LiveSetting** represents, see [**AutomationProperties.LiveSetting**](/uwp/api/windows.ui.xaml.automation.automationproperties.livesettingproperty).

You might override [**GetOrientationCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getorientationcore) if your control has a settable orientation property that can map to [**AutomationOrientation**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationOrientation). The [**ScrollBarAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.ScrollBarAutomationPeer) and [**SliderAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.SliderAutomationPeer) classes do this.

<span id="Base_implementation_in_FrameworkElementAutomationPeer"></span>
<span id="base_implementation_in_frameworkelementautomationpeer"></span>
<span id="BASE_IMPLEMENTATION_IN_FRAMEWORKELEMENTAUTOMATIONPEER"></span>

### Base implementation in FrameworkElementAutomationPeer  
The base implementation of [**FrameworkElementAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer) provides some UI Automation information that can be interpreted from various layout and behavior properties that are defined at the framework level.

* [**GetBoundingRectangleCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getboundingrectanglecore): Returns a [**Rect**](/uwp/api/Windows.Foundation.Rect) structure based on the known layout characteristics. Returns a 0-value **Rect** if [**IsOffscreen**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.isoffscreen) is **true**.
* [**GetClickablePointCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getclickablepointcore): Returns a [**Point**](/uwp/api/Windows.Foundation.Point) structure based on the known layout characteristics, as long as there is a nonzero **BoundingRectangle**.
* [**GetNameCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getnamecore): More extensive behavior than can be summarized here; see [**GetNameCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getnamecore). Basically, it attempts a string conversion on any known content of a [**ContentControl**](/uwp/api/Windows.UI.Xaml.Controls.ContentControl) or related classes that have content. Also, if there is a value for [**LabeledBy**](/previous-versions/windows/silverlight/dotnet-windows-silverlight/ms591292(v=vs.95)), that item's **Name** value is used as the **Name**.
* [**HasKeyboardFocusCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.haskeyboardfocuscore): Evaluated based on the owner's [**FocusState**](/uwp/api/windows.ui.xaml.controls.control.focusstate) and [**IsEnabled**](/uwp/api/windows.ui.xaml.controls.control.isenabled) properties. Elements that aren't controls always return **false**.
* [**IsEnabledCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.isenabledcore): Evaluated based on the owner's [**IsEnabled**](/uwp/api/windows.ui.xaml.controls.control.isenabled) property if it is a [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control). Elements that aren't controls always return **true**. This doesn't mean that the owner is enabled in the conventional interaction sense; it means that the peer is enabled despite the owner not having an **IsEnabled** property.
* [**IsKeyboardFocusableCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.iskeyboardfocusablecore): Returns **true** if owner is a [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control); otherwise it is **false**.
* [**IsOffscreenCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.isoffscreencore): A [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) of [**Collapsed**](/uwp/api/windows.ui.xaml.visibility) on the owner element or any of its parents equates to a **true** value for [**IsOffscreen**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.isoffscreen). Exception: a [**Popup**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.Popup) object can be visible even if its owner's parents are not.
* [**SetFocusCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.setfocuscore): Calls [**Focus**](/uwp/api/windows.ui.xaml.controls.control.focus).
* [**GetParent**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getparent): Calls [**FrameworkElement.Parent**](/uwp/api/windows.ui.xaml.frameworkelement.parent) from the owner, and looks up the appropriate peer. This isn't an override pair with a "Core" method, so you can't change this behavior.

> [!NOTE]
> Default UWP peers implement a behavior by using internal native code that implements the UWP, not necessarily by using actual UWP code. You won't be able to see the code or logic of the implementation through common language runtime (CLR) reflection or other techniques. You also won't see distinct reference pages for subclass-specific overrides of base peer behavior. For example, there might be additional behavior for [**GetNameCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getnamecore) of a [**TextBoxAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.TextBoxAutomationPeer), which won't be described on the **AutomationPeer.GetNameCore** reference page, and there is no reference page for **TextBoxAutomationPeer.GetNameCore**. There isn't even a **TextBoxAutomationPeer.GetNameCore** reference page. Instead, read the reference topic for the most immediate peer class, and look for implementation notes in the Remarks section.

<span id="Peers_and_AutomationProperties"></span>
<span id="peers_and_automationproperties"></span>
<span id="PEERS_AND_AUTOMATIONPROPERTIES"></span>

## Peers and AutomationProperties  
Your automation peer should provide appropriate default values for your control's accessibility-related information. Note that any app code that uses the control can override some of that behavior by including [**AutomationProperties**](/uwp/api/Windows.UI.Xaml.Automation.AutomationProperties) attached-property values on control instances. Callers can do this either for the default controls or for custom controls. For example, the following XAML creates a button that has two customized UI Automation properties: `<Button AutomationProperties.Name="Special"      AutomationProperties.HelpText="This is a special button."/>`

For more info about [**AutomationProperties**](/uwp/api/Windows.UI.Xaml.Automation.AutomationProperties) attached properties, see [Basic accessibility information](basic-accessibility-information.md).

Some of the [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer) methods exist because of the general contract of how UI Automation providers are expected to report information, but these methods are not typically implemented in control peers. This is because that info is expected to be provided by [**AutomationProperties**](/uwp/api/Windows.UI.Xaml.Automation.AutomationProperties) values applied to the app code that uses the controls in a specific UI. For example, most apps would define the labeling relationship between two different controls in the UI by applying a [**AutomationProperties.LabeledBy**](/previous-versions/windows/silverlight/dotnet-windows-silverlight/ms591292(v=vs.95)) value. However, [**LabeledByCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getlabeledbycore) is implemented in certain peers that represent data or item relationships in a control, such as using a header part to label a data-field part, labeling items with their containers, or similar scenarios.

<span id="Implementing_patterns"></span>
<span id="implementing_patterns"></span>
<span id="IMPLEMENTING_PATTERNS"></span>

## Implementing patterns  
Let's look at how to write a peer for a control that implements an expand-collapse behavior by implementing the control pattern interface for expand-collapse. The peer should enable the accessibility for the expand-collapse behavior by returning itself whenever [**GetPattern**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getpattern) is called with a value of [**PatternInterface.ExpandCollapse**](/uwp/api/Windows.UI.Xaml.Automation.Peers.PatternInterface). The peer should then inherit the provider interface for that pattern ([**IExpandCollapseProvider**](/uwp/api/windows.ui.xaml.automation.provider.iexpandcollapseprovider)) and provide implementations for each of the members of that provider interface. In this case the interface has three members to override: [**Expand**](/uwp/api/windows.ui.xaml.automation.provider.iexpandcollapseprovider.expand), [**Collapse**](/uwp/api/windows.ui.xaml.automation.provider.iexpandcollapseprovider.collapse), [**ExpandCollapseState**](/uwp/api/windows.ui.xaml.automation.provider.iexpandcollapseprovider.expandcollapsestate).

It's helpful to plan ahead for accessibility in the API design of the class itself. Whenever you have a behavior that is potentially requested either as a result of typical interactions with a user who is working in the UI or through an automation provider pattern, provide a single method that either the UI response or the automation pattern can call. For example, if your control has button parts that have wired event handlers that can expand or collapse the control, and has keyboard equivalents for those actions, have these event handlers call the same method that you call from within the body of the [**Expand**](/uwp/api/windows.ui.xaml.automation.provider.iexpandcollapseprovider.expand) or [**Collapse**](/uwp/api/windows.ui.xaml.automation.provider.iexpandcollapseprovider.collapse) implementations for [**IExpandCollapseProvider**](/windows/desktop/api/uiautomationcore/nn-uiautomationcore-iexpandcollapseprovider) in the peer. Using a common logic method can also be a useful way to make sure that your control's visual states are updated to show logical state in a uniform way, regardless of how the behavior was invoked.

A typical implementation is that the provider APIs first call [**Owner**](/uwp/api/windows.ui.xaml.automation.peers.frameworkelementautomationpeer.owner) for access to the control instance at run time. Then the necessary behavior methods can be called on that object.


```csharp
public class IndexCardAutomationPeer : FrameworkElementAutomationPeer, IExpandCollapseProvider {
    private IndexCard ownerIndexCard;
    public IndexCardAutomationPeer(IndexCard owner) : base(owner)
    {
         ownerIndexCard = owner;
    }
}
```

An alternate implementation is that the control itself can reference its peer. This is a common pattern if you are raising automation events from the control, because the [**RaiseAutomationEvent**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.raiseautomationevent) method is a peer method.

<span id="UI_Automation_events"></span>
<span id="ui_automation_events"></span>
<span id="UI_AUTOMATION_EVENTS"></span>

## UI Automation events  

UI Automation events fall into the following categories.

| Event | Description |
|-------|-------------|
| Property change | Fires when a property on a UI Automation element or control pattern changes. For example, if a client needs to monitor an app's check box control, it can register to listen for a property change event on the [**ToggleState**](/uwp/api/windows.ui.xaml.automation.provider.itoggleprovider.togglestate) property. When the check box control is checked or unchecked, the provider fires the event and the client can act as necessary. |
| Element action | Fires when a change in the UI results from user or programmatic activity; for example, when a button is clicked or invoked through the **Invoke** pattern. |
| Structure change | Fires when the structure of the UI Automation tree changes. The structure changes when new UI items become visible, hidden, or removed on the desktop. |
| Global change | Fires when actions of global interest to the client occur, such as when the focus shifts from one element to another, or when a child window closes. Some events do not necessarily mean that the state of the UI has changed. For example, if the user tabs to a text-entry field and then clicks a button to update the field, a [**TextChanged**](/uwp/api/windows.ui.xaml.controls.textbox.textchanged) event fires even if the user did not actually change the text. When processing an event, it may be necessary for a client application to check whether anything has actually changed before taking action. |

<span id="AutomationEvents_identifiers"></span>
<span id="automationevents_identifiers"></span>
<span id="AUTOMATIONEVENTS_IDENTIFIERS"></span>

### AutomationEvents identifiers  
UI Automation events are identified by [**AutomationEvents**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationEvents) values. The values of the enumeration uniquely identify the kind of event.

<span id="Raising_events"></span>
<span id="raising_events"></span>
<span id="RAISING_EVENTS"></span>

### Raising events  
UI Automation clients can subscribe to automation events. In the automation peer model, peers for custom controls must report changes to control state that are relevant to accessibility by calling the [**RaiseAutomationEvent**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.raiseautomationevent) method. Similarly, when a key UI Automation property value changes, custom control peers should call the [**RaisePropertyChangedEvent**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.raisepropertychangedevent) method.

The next code example shows how to get the peer object from within the control definition code and call a method to fire an event from that peer. As an optimization, the code determines whether there are any listeners for this event type. Firing the event and creating the peer object only when there are listeners avoids unnecessary overhead and helps the control remain responsive.


```csharp
if (AutomationPeer.ListenerExists(AutomationEvents.PropertyChanged))
{
    NumericUpDownAutomationPeer peer =
        FrameworkElementAutomationPeer.FromElement(nudCtrl) as NumericUpDownAutomationPeer;
    if (peer != null)
    {
        peer.RaisePropertyChangedEvent(
            RangeValuePatternIdentifiers.ValueProperty,
            (double)oldValue,
            (double)newValue);
    }
}
```

<span id="Peer_navigation"></span>
<span id="peer_navigation"></span>
<span id="PEER_NAVIGATION"></span>

## Peer navigation  
After locating an automation peer, a UI Automation client can navigate the peer structure of an app by calling the peer object's [**GetChildren**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getchildren) and [**GetParent**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getparent) methods. Navigation among UI elements within a control is supported by the peer's implementation of the [**GetChildrenCore**](/uwp/api/windows.ui.xaml.automation.peers.automationpeer.getchildrencore) method. The UI Automation system calls this method to build up a tree of sub-elements contained within a control; for example, list items in a list box. The default **GetChildrenCore** method in [**FrameworkElementAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer) traverses the visual tree of elements to build the tree of automation peers. Custom controls can override this method to expose a different representation of child elements to automation clients, returning the automation peers of elements that convey information or allow user interaction.

<span id="Native_automation_support_for_text_patterns"></span>
<span id="native_automation_support_for_text_patterns"></span>
<span id="NATIVE_AUTOMATION_SUPPORT_FOR_TEXT_PATTERNS"></span>

## Native automation support for text patterns  
Some of the default UWP app automation peers provide control pattern support for the text pattern ([**PatternInterface.Text**](/uwp/api/Windows.UI.Xaml.Automation.Peers.PatternInterface)). But they provide this support through native methods, and the peers involved won't note the [**ITextProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ITextProvider) interface in the (managed) inheritance. Still, if a managed or non-managed UI Automation client queries the peer for patterns, it will report support for the text pattern, and provide behavior for parts of the pattern when client APIs are called.

If you intend to derive from one of the UWP app text controls and also create a custom peer that derives from one of the text-related peers, check the Remarks sections for the peer to learn more about any native-level support for patterns. You can access the native base behavior in your custom peer if you call the base implementation from your managed provider interface implementations, but it's difficult to modify what the base implementation does because the native interfaces on both the peer and its owner control aren't exposed. Generally you should either use the base implementations as-is (call base only) or completely replace the functionality with your own managed code and don't call the base implementation. The latter is an advanced scenario, you'll need good familiarity with the text services framework being used by your control in order to support the accessibility requirements when using that framework.

<span id="AutomationProperties.AccessibilityView"></span>
<span id="automationproperties.accessibilityview"></span>
<span id="AUTOMATIONPROPERTIES.ACCESSIBILITYVIEW"></span>

## AutomationProperties.AccessibilityView  
In addition to providing a custom peer, you can also adjust the tree view representation for any control instance, by setting [**AutomationProperties.AccessibilityView**](/uwp/api/windows.ui.xaml.automation.automationproperties.accessibilityview) in XAML. This isn't implemented as part of a peer class, but we'll mention it here because it's germane to overall accessibility support either for custom controls or for templates you customize.

The main scenario for using **AutomationProperties.AccessibilityView** is to deliberately omit certain controls in a template from the UI Automation views, because they don't meaningfully contribute to the accessibility view of the entire control. To prevent this, set **AutomationProperties.AccessibilityView** to "Raw".

<span id="Throwing_exceptions_from_automation_peers"></span>
<span id="throwing_exceptions_from_automation_peers"></span>
<span id="THROWING_EXCEPTIONS_FROM_AUTOMATION_PEERS"></span>

## Throwing exceptions from automation peers  
The APIs that you are implementing for your automation peer support are permitted to throw exceptions. It's expected any UI Automation clients that are listening are robust enough to continue on after most exceptions are thrown. In all likelihood that listener is looking at an all-up automation tree that includes apps other than your own, and it's an unacceptable client design to bring down the entire client just because one area of the tree threw a peer-based exception when the client called its APIs.

For parameters that are passed in to your peer, it's acceptable to validate the input, and for example throw [**ArgumentNullException**](/dotnet/api/system.argumentnullexception) if it was passed **null** and that's not a valid value for your implementation. However, if there are subsequent operations performed by your peer, remember that the peer's interactions with the hosting control have something of an asynchronous character to them. Anything a peer does won't necessarily block the UI thread in the control (and it probably shouldn't). So you could have situations where an object was available or had certain properties when the peer was created or when an automation peer method was first called, but in the meantime the control state has changed. For these cases, there are two dedicated exceptions that a provider can throw:

* Throw [**ElementNotAvailableException**](/dotnet/api/system.windows.automation.elementnotavailableexception) if you're unable to access either the peer's owner or a related peer element based on the original info your API was passed. For example, you might have a peer that's trying to run its methods but the owner has since been removed from the UI, such as a modal dialog that's been closed. For a non-.NET client, this maps to [**UIA\_E\_ELEMENTNOTAVAILABLE**](/windows/desktop/WinAuto/uiauto-error-codes).
* Throw [**ElementNotEnabledException**](/dotnet/api/system.windows.automation.elementnotenabledexception) if there still is an owner, but that owner is in a mode such as [**IsEnabled**](/uwp/api/windows.ui.xaml.controls.control.isenabled)`=`**false** that's blocking some of the specific programmatic changes that your peer is trying to accomplish. For a non-.NET client, this maps to [**UIA\_E\_ELEMENTNOTENABLED**](/windows/desktop/WinAuto/uiauto-error-codes).

Beyond this, peers should be relatively conservative regarding exceptions that they throw from their peer support. Most clients won't be able to handle exceptions from peers and turn these into actionable choices that their users can make when interacting with the client. So sometimes a no-op, and catching exceptions without rethrowing within your peer implementations, is a better strategy than is throwing exceptions every time something the peer tries to do doesn't work. Consider also that most UI Automation clients aren't written in managed code. Most are written in COM and are just checking for **S\_OK** in an **HRESULT** whenever they call a UI Automation client method that ends up accessing your peer.

<span id="related_topics"></span>

## Related topics  
* [Accessibility](accessibility.md)
* [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample)
* [**FrameworkElementAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer)
* [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer)
* [**OnCreateAutomationPeer**](/uwp/api/windows.ui.xaml.uielement.oncreateautomationpeer)
* [Control patterns and interfaces](control-patterns-and-interfaces.md)
