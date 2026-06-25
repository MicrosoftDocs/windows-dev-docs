---
description: Describes the concept of automation peers for Microsoft UI Automation, and how you can provide automation support for your own custom UI class.
ms.assetid: AA8DA53B-FE6E-40AC-9F0A-CB09637C87B4
title: Custom automation peers
label: Custom automation peers
template: detail.hbs
ms.date: 03/17/2026
ms.topic: article
keywords: windows 11, winui, winapp sdk
ms.localizationpriority: medium
---

# Custom automation peers

This article explains automation peers in Microsoft UI Automation and how to provide robust automation support for custom UI classes.

> [!NOTE]
> This guidance uses WinUI 3 / Windows App SDK namespaces and references based on [**Microsoft.UI.Xaml.Automation.Peers.AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer).

UI Automation provides a framework used by automation clients to inspect and interact with UI across platforms. In Windows apps, most built-in controls already provide UI Automation support. When you derive a new control or support type from existing non-sealed classes, you may introduce behavior not represented by the default peer. In that case, extend automation support by deriving from the corresponding [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer), adding the required provider behavior, and ensuring the control infrastructure creates your peer.

UI Automation supports both accessibility tools (such as screen readers) and automated testing systems. In both scenarios, external code can inspect UI elements and simulate interaction with your app. For broader platform guidance, see [UI Automation Overview](/windows/desktop/WinAuto/uiauto-uiautomationoverview).

For teams that prioritize accessibility and automated testing, automation peers are also a reliability contract. Stable automation structure and accurate pattern support improve both assistive technology behavior and long-term test stability.

There are two primary audiences for UI Automation.

* **UI Automation *clients*** call UI Automation APIs to discover and interact with currently available UI. For example, a screen reader is a UI Automation client. Clients navigate a tree of automation elements, and can target one app or the full desktop tree.
* **UI Automation *providers*** publish information into that tree by implementing APIs for controls introduced by the app. When you build a custom control, you are participating in the provider model and should ensure your control is usable by both accessibility and test clients.

The framework generally exposes parallel API surfaces: one for clients and one for providers. This topic focuses on provider extensibility, especially peer classes and provider interfaces. Client-side APIs are referenced only for context. For client guidance, see [UI Automation Client Programmer's Guide](/windows/desktop/WinAuto/uiauto-clientportal).

> [!NOTE]
> UI Automation clients are usually desktop apps and don't typically use managed code. UI Automation is based on a standard and not a specific implementation or framework. Many existing UI Automation clients, including assistive technology products such as screen readers, use Component Object Model (COM) interfaces to interact with UI Automation, the system, and the apps that run in child windows. For more info on the COM interfaces and how to write a UI Automation client using COM, see [UI Automation Fundamentals](/windows/desktop/WinAuto/entry-uiautocore-overview).

## Determining the existing state of UI Automation support for your custom UI class

Before implementing a custom automation peer, validate whether the base control and its peer already provide the support you need. In many cases, [**FrameworkElementAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.frameworkelementautomationpeer), control-specific peers, and their built-in patterns are sufficient. The need for customization depends on how much your control diverges from the base object model and whether template or interaction changes introduce new user experience that require additional accessibility support.

As part of this evaluation, verify that the default peer behavior is sufficient for your automated accessibility tests, not just manual inspection. If your test scenarios depend on specific patterns, names, or hierarchy, implement a custom peer so those expectations remain explicit and maintainable.

Even if base behavior is functionally acceptable, defining your own peer is still recommended when precise **ClassName** reporting matters for automation and test reliability, especially for controls intended for reuse by others.

## Automation peer classes

WinUI and related XAML frameworks build on established UI Automation conventions from earlier managed UI stacks such as Windows Forms, WPF, and Silverlight. Many control and peer concepts follow these established patterns.

By convention, peer class names begin with the control class name and end with "AutomationPeer". For example, [**ButtonAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.buttonautomationpeer) is the peer class for the [**Button**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.button) control class.

> [!NOTE]
> For purposes of this topic, we treat the properties that are related to accessibility as being more important when you implement a control peer. But for a more general concept of UI Automation support, you should implement a peer in accordance with recommendations as documented by the [UI Automation Provider Programmer's Guide](/windows/desktop/WinAuto/uiauto-providerportal) and [UI Automation Fundamentals](/windows/desktop/WinAuto/entry-uiautocore-overview). Those topics don't cover the specific [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer) APIs that you use in WinUI and Windows App SDK, but they do describe the properties that identify your class or provide other information or interaction.

## Peers, patterns and control types

A *control pattern* is an interface implementation that exposes a particular aspect of a control's functionality to a UI Automation client. UI Automation clients use the properties and methods exposed through a control pattern to retrieve information about capabilities of the control, or to manipulate the control's behavior at run time.

Control patterns expose behavior independently of a control's visuals or specific class identity. For example, a tabular control can expose **Grid** so clients can query dimensions and retrieve items. Clients can use **Invoke** for invokable controls (such as buttons) and **Scroll** for scrollable surfaces (such as list controls). Patterns are composable, so a single control can expose multiple behaviors.

Control patterns relate to UI as interfaces relate to COM objects. In COM, you can query an object to ask what interfaces it supports and then use those interfaces to access functionality. In UI Automation, UI Automation clients can query a UI Automation element to find out which control patterns it supports, and then interact with the element and its peered control through the properties, methods, events, and structures exposed by the supported control patterns.

One of the automation peer's primary responsibilities is to declare which patterns are supported. Providers do this by overriding [**GetPatternCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpatterncore), which backs [**GetPattern**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpattern). Clients query one pattern at a time; if supported, the peer returns an object (typically itself), otherwise it returns **null**.

A *control type* broadly classifies the functionality represented by the peer. This differs from a control pattern: patterns describe specific capabilities, while control type describes the higher-level role. Each control type has guidance for these areas:

* UI Automation control patterns: A control type might support more than one pattern, each of which represents a different classification of info or interaction. Each control type has a set of control patterns that the control must support, a set that is optional, and a set that the control must not support.
* UI Automation property values: Each control type has a set of properties that the control must support. These are the general properties, as described in [UI Automation Properties Overview](/windows/desktop/WinAuto/uiauto-propertiesoverview), not the ones that are pattern-specific.
* UI Automation events: Each control type has a set of events that the control must support. Again these are general, not pattern-specific, as described in [UI Automation Events Overview](/windows/desktop/WinAuto/uiauto-eventsoverview).
* UI Automation tree structure: Each control type defines how the control must appear in the UI Automation tree structure.

Regardless of framework details, client behavior is not tied to a specific XAML app model. Many existing clients, including assistive technologies, interact through COM. In that model, clients **QueryInterface** for pattern interfaces or general automation interfaces, and the automation infrastructure marshals calls to your app's provider and peer implementation.

When you implement control patterns for a managed-code Windows app, you can use .NET interfaces to represent these patterns instead of COM interface syntax. For example, the UI Automation pattern interface for a .NET provider implementation of the **Invoke** pattern is [**IInvokeProvider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.iinvokeprovider).

For a list of control patterns, provider interfaces, and their purpose, see [Control patterns and interfaces](control-patterns-and-interfaces.md). For control types, see [UI Automation Control Types Overview](/windows/desktop/WinAuto/uiauto-controltypesoverview).

### Guidance for how to implement control patterns

Control-pattern guidance belongs to the overall UI Automation platform, not only to a specific app model. When implementing patterns, align behavior with Microsoft documentation and UI Automation conventions. Start with [Implementing UI Automation Control Patterns](/windows/desktop/WinAuto/uiauto-implementinguiautocontrolpatterns), especially the implementation and required-members sections for each pattern. Although those references often describe native COM interfaces, equivalent provider interfaces are available for WinUI in [**Microsoft.UI.Xaml.Automation.Provider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider).

If you're using the default automation peers and expanding on their behavior, those peers have already been written in conformance to UI Automation guidelines. If they support control patterns, you can rely on that pattern support conforming with guidance at [Implementing UI Automation Control Patterns](/windows/desktop/WinAuto/uiauto-implementinguiautocontrolpatterns). If a control peer reports that it's representative of a control type defined by UI Automation, then the guidance documented at [Supporting UI Automation Control Types](/windows/desktop/WinAuto/uiauto-supportinguiautocontroltypes) has been followed by that peer.

Additional interpretation is often required when implementing patterns or control types not covered by default controls. For example, default XAML controls do not implement annotation patterns. If your app depends on annotation workflows, your peer can implement [**IAnnotationProvider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.iannotationprovider) and report a **Document** control type with properties that communicate annotation capability.

We recommend that you use the guidance that you see for the patterns under [Implementing UI Automation Control Patterns](/windows/desktop/WinAuto/uiauto-implementinguiautocontrolpatterns) or control types under [Supporting UI Automation Control Types](/windows/desktop/WinAuto/uiauto-supportinguiautocontroltypes) as orientation and general guidance. The API links provide descriptions and remarks on the purpose of the APIs. For WinUI syntax specifics, use the equivalent API within the [**Microsoft.UI.Xaml.Automation.Provider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider) namespace.

## Built-in automation peer classes

In general, elements expose automation peers when they are interactive or provide meaningful information to assistive technologies. Not all visual elements need peers. For example, [**Button**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.button) and [**TextBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox) have peers, while [**Border**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.border) and [**Panel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.panel)-based layout types such as [**Grid**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.grid) and [**Canvas**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.canvas) do not. A **Panel** contributes layout only and has no direct accessibility interaction model, so its meaningful child elements are surfaced through the nearest ancestor that has a peer.

## UI Automation process boundaries

UI Automation clients that inspect Windows apps usually run out-of-process. UI Automation infrastructure handles cross-process communication. For details, see [UI Automation Fundamentals](/windows/desktop/WinAuto/entry-uiautocore-overview).

## OnCreateAutomationPeer

All classes that derive from [**UIElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.UIElement) contain the protected virtual method [**OnCreateAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.oncreateautomationpeer). The object initialization sequence for automation peers calls **OnCreateAutomationPeer** to get the automation peer object for each control and thus to construct a UI Automation tree for run-time use. UI Automation code can use the peer to get information about a control's characteristics and features and to simulate interactive use by means of its control patterns. A custom control that supports automation must override **OnCreateAutomationPeer** and return an instance of a class that derives from [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationPeer). For example, if a custom control derives from the [**ButtonBase**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Primitives.ButtonBase) class, the object returned by **OnCreateAutomationPeer** should derive from [**ButtonBaseAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.buttonbaseautomationpeer).

If you are writing a custom control and supplying a custom peer, override [**OnCreateAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.oncreateautomationpeer) so it returns a new instance of your peer type. The peer must derive directly or indirectly from [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationPeer).

For example, the following code declares that custom control `NumericUpDown` uses `NumericUpDownAutomationPeer` for UI Automation.

```csharp
using Microsoft.UI.Xaml.Automation.Peers;
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
    runtimeclass NumericUpDown : Microsoft.UI.Xaml.Controls.Primitives.RangeBase
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
    Microsoft::UI::Xaml::Automation::Peers::AutomationPeer OnCreateAutomationPeer()
    {
        return winrt::make<MyNamespace::implementation::NumericUpDownAutomationPeer>(*this);
    }
};
```

```cpp
//.h
public ref class NumericUpDown sealed : Microsoft::UI::Xaml::Controls::Primitives::RangeBase
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
> The [**OnCreateAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.oncreateautomationpeer) implementation should do nothing more than initialize a new instance of your custom automation peer, passing the calling control as owner, and return that instance. Do not attempt additional logic in this method. In particular, any logic that could potentially lead to destruction of the [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationPeer) within the same call may result in unexpected runtime behavior.

In typical [**OnCreateAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.oncreateautomationpeer) implementations, the *owner* is **this** or **Me**, because the override executes within the control instance scope.

You can define peer classes in the same file as the control or in separate files. Framework peer types live in [**Microsoft.UI.Xaml.Automation.Peers**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers), but your custom peers can live in any namespace, provided required namespaces are referenced where [**OnCreateAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.oncreateautomationpeer) is implemented.

### Choosing the correct peer base class

Derive your custom [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationPeer) from the closest peer base class that matches your control's functional base class. In the previous example, `NumericUpDown` derives from [**RangeBase**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Primitives.RangeBase), so [**RangeBaseAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.RangeBaseAutomationPeer) is the correct peer base. This lets you reuse existing [**IRangeValueProvider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Provider.IRangeValueProvider) behavior rather than re-implementing it.

The base [**Control**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Control) class does not have a corresponding peer class. If you need a peer class to correspond to a custom control that derives from **Control**, derive the custom peer class from [**FrameworkElementAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.FrameworkElementAutomationPeer).

If you derive from [**ContentControl**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ContentControl) directly, that class has no default automation peer behavior because there is no [**OnCreateAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.oncreateautomationpeer) implementation that references a peer class. So make sure either to implement **OnCreateAutomationPeer** to use your own peer, or to use [**FrameworkElementAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.FrameworkElementAutomationPeer) as the peer if that level of accessibility support is adequate for your control.

> [!NOTE]
> You don't typically derive from [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationPeer) rather than [**FrameworkElementAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.FrameworkElementAutomationPeer). If you did derive directly from **AutomationPeer** you'll need to duplicate a lot of basic accessibility support that would otherwise come from **FrameworkElementAutomationPeer**.

## Initialization of a custom peer class

A custom peer should expose a type-safe constructor that takes the owner control and passes it to the base class initializer. In the next example, `owner` is passed to [**RangeBaseAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.RangeBaseAutomationPeer), and eventually [**FrameworkElementAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.FrameworkElementAutomationPeer) uses that value to set [**FrameworkElementAutomationPeer.Owner**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.frameworkelementautomationpeer.owner).

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
    runtimeclass NumericUpDownAutomationPeer : Microsoft.UI.Xaml.Automation.Peers.RangeBaseAutomationPeer
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
public ref class NumericUpDownAutomationPeer sealed :  Microsoft::UI::Xaml::Automation::Peers::RangeBaseAutomationPeer
//.cpp
public:    NumericUpDownAutomationPeer(NumericUpDown^ owner);
```

## Core methods of AutomationPeer

Overridable automation methods are exposed as pairs: a public method used by provider infrastructure and a protected `Core` method intended for customization. By default, the public method forwards to the corresponding `Core` method, falling back to base implementation when needed.

When implementing a peer for a custom control, override `Core` methods wherever your behavior differs from base peer behavior. UI Automation retrieves information through public methods, but your customization point is the corresponding `Core` override.

At a minimum, whenever you define a new peer class, implement the [**GetClassNameCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getclassnamecore) method, as shown in the next example.

```csharp
protected override string GetClassNameCore()
{
    return "NumericUpDown";
}
```

> [!NOTE]
> You might want to store the strings as constants rather than directly in the method body, but that is up to you. For [**GetClassNameCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getclassnamecore), you won't need to localize this string. The **LocalizedControlType** property is used any time a localized string is needed by a UI Automation client, not **ClassName**.

### GetAutomationControlType

Some assistive technologies report [**GetAutomationControlType**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getautomationcontroltype) alongside the UI Automation **Name**. If your control's role differs meaningfully from the base class role, implement a custom peer and override [**GetAutomationControlTypeCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getautomationcontroltypecore). This is especially important when deriving from generalized bases such as [**ItemsControl**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ItemsControl) or [**ContentControl**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ContentControl).

Your implementation of [**GetAutomationControlTypeCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getautomationcontroltypecore) describes your control by returning an [**AutomationControlType**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationControlType) value. Although you can return **AutomationControlType.Custom**, you should return one of the more specific control types if it accurately describes your control's main scenarios. Here's an example.

```csharp
protected override AutomationControlType GetAutomationControlTypeCore()
{
    return AutomationControlType.Spinner;
}
```

> [!NOTE]
> Unless you specify [**AutomationControlType.Custom**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationControlType), you don't have to implement [**GetLocalizedControlTypeCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getlocalizedcontroltypecore) to provide a **LocalizedControlType** property value to clients. UI Automation common infrastructure provides translated strings for every possible **AutomationControlType** value other than **AutomationControlType.Custom**.

### GetPattern and GetPatternCore

[**GetPatternCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpatterncore) returns the object that supports the requested pattern. A UI Automation client requests a specific [**PatternInterface**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.PatternInterface) through [**GetPattern**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpattern). If supported, the override should return the implementing object, typically the peer itself. If unsupported, return **null** (often by delegating to base behavior and letting it return **null**).

When a pattern is supported, [**GetPatternCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpatterncore) can return **this** or **Me**. Clients then cast the [**GetPattern**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpattern) return value to the requested interface.

If a peer class inherits from another peer, and all necessary support and pattern reporting is already handled by the base class, implementing [**GetPatternCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpatterncore) isn't necessary. For example, if you are implementing a range control that derives from [**RangeBase**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Primitives.RangeBase), and your peer derives from [**RangeBaseAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.RangeBaseAutomationPeer), that peer returns itself for [**PatternInterface.RangeValue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.PatternInterface) and has working implementations of the [**IRangeValueProvider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Provider.IRangeValueProvider) interface that supports the pattern.

Although it is not the literal code, this example approximates the implementation of [**GetPatternCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpatterncore) already present in [**RangeBaseAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.RangeBaseAutomationPeer).

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

If you are implementing a peer where you don't have all the support you need from a base peer class, or you want to change or add to the set of base-inherited patterns that your peer can support, then you should override [**GetPatternCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpatterncore) to enable UI Automation clients to use the patterns.

For a list of UI Automation provider patterns supported by WinUI, see [**Microsoft.UI.Xaml.Automation.Provider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Provider). Each such pattern has a corresponding value of the [**PatternInterface**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.PatternInterface) enumeration, which is how UI Automation clients request the pattern in a [**GetPattern**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpattern) call.

A peer can report that it supports more than one pattern. If so, the override should include return path logic for each supported [**PatternInterface**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.PatternInterface) value and return the peer in each matching case. It is expected that the caller will request only one interface at a time, and it is up to the caller to cast to the expected interface.

Here's an example of a [**GetPatternCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpatterncore) override for a custom peer. It reports the support for two patterns, [**IRangeValueProvider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Provider.IRangeValueProvider) and [**IToggleProvider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Provider.IToggleProvider). The control here is a media display control that can display as full-screen (the toggle mode) and that has a progress bar within which users can select a position (the range control). This code came from the [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample) (archived legacy sample).

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

### Forwarding patterns from sub-elements

A [**GetPatternCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpatterncore) method implementation can also specify a sub-element or part as a pattern provider for its host. This example mimics how [**ItemsControl**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ItemsControl) transfers scroll-pattern handling to the peer of its internal [**ScrollViewer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ScrollViewer) control. To specify a sub-element for pattern handling, this code gets the sub-element object, creates a peer for the sub-element by using the [**FrameworkElementAutomationPeer.CreatePeerForElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.frameworkelementautomationpeer.createpeerforelement) method, and returns the new peer.

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

### Other Core methods

Your control may need keyboard alternatives for core scenarios; for more details, see [Keyboard accessibility](keyboard-accessibility.md). Key behavior belongs in control logic, but the peer should report key metadata through [**GetAcceleratorKeyCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getacceleratorkeycore) and [**GetAccessKeyCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getaccesskeycore). If key descriptions are user-facing, localize them through resources.

If your control exposes collections, use functional and peer base classes that already implement collection behavior. Otherwise, override [**GetChildrenCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getchildrencore) to accurately represent parent-child relationships in the automation tree.

Implement [**IsContentElementCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.iscontentelementcore) and [**IsControlElementCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.iscontrolelementcore) to indicate whether your control should be treated as content, control, or both. Both default to **true**. These values help clients such as screen readers filter and navigate the tree effectively. If [**GetPatternCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpatterncore) forwards behavior to a sub-element peer, the sub-element can return **false** from `IsControlElementCore` to stay out of the exposed tree.

Some controls may support labeling scenarios, where a text label part supplies information for a non-text part, or a control is intended to be in a known labeling relationship with another control in the UI. If it's possible to provide a useful class-based behavior, you can override [**GetLabeledByCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getlabeledbycore) to provide this behavior.

[**GetBoundingRectangleCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getboundingrectanglecore) and [**GetClickablePointCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getclickablepointcore) are used mainly for automated testing scenarios. If you want to support automated testing for your control, you might want to override these methods. This might be desired for range-type controls, where you can't suggest just a single point because where the user clicks in coordinate space has a different effect on a range. For example, the default [**ScrollBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Primitives.ScrollBar) automation peer overrides **GetClickablePointCore** to return a "not a number" [**Point**](/dotnet/api/windows.foundation.point) value.

[**GetLiveSettingCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getlivesettingcore) influences the control default for the **LiveSetting** value for UI Automation. You might want to override this if you want your control to return a value other than [**AutomationLiveSetting.Off**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationLiveSetting). For more info on what **LiveSetting** represents, see [**AutomationProperties.LiveSetting**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.livesettingproperty).

You might override [**GetOrientationCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getorientationcore) if your control has a settable orientation property that can map to [**AutomationOrientation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationOrientation). The [**ScrollBarAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.ScrollBarAutomationPeer) and [**SliderAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.SliderAutomationPeer) classes do this.

### Base implementation in FrameworkElementAutomationPeer

The [**FrameworkElementAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.FrameworkElementAutomationPeer) base implementation provides automation information inferred from framework-level layout and behavior state.

* [**GetBoundingRectangleCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getboundingrectanglecore): Returns a [**Rect**](/dotnet/api/windows.foundation.rect) structure based on the known layout characteristics. Returns a 0-value **Rect** if [**IsOffscreen**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.isoffscreen) is **true**.
* [**GetClickablePointCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getclickablepointcore): Returns a [**Point**](/dotnet/api/windows.foundation.point) structure based on the known layout characteristics, as long as there is a nonzero **BoundingRectangle**.
* [**GetNameCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getnamecore): More extensive behavior than can be summarized here; see [**GetNameCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getnamecore). Basically, it attempts a string conversion on any known content of a [**ContentControl**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol) or related classes that have content. Also, if there is a value for [**LabeledBy**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.labeledbyproperty), that item's **Name** value is used as the **Name**.
* [**HasKeyboardFocusCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.haskeyboardfocuscore): Evaluated based on the owner's [**FocusState**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.focusstate) and [**IsEnabled**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control.isenabled) properties. Elements that aren't controls always return **false**.
* [**IsEnabledCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.isenabledcore): Evaluated based on the owner's [**IsEnabled**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control.isenabled) property if it is a [**Control**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Control). Elements that aren't controls always return **true**. This doesn't mean that the owner is enabled in the conventional interaction sense; it means that the peer is enabled despite the owner not having an **IsEnabled** property.
* [**IsKeyboardFocusableCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.iskeyboardfocusablecore): Returns **true** if owner is a [**Control**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Control); otherwise it is **false**.
* [**IsOffscreenCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.isoffscreencore): A [**Visibility**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.visibility) of [**Collapsed**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.visibility) on the owner element or any of its parents equates to a **true** value for [**IsOffscreen**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.isoffscreen). Exception: a [**Popup**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Primitives.Popup) object can be visible even if its owner's parents are not.
* [**SetFocusCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.setfocuscore): Calls [**Focus**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.focus).
* [**GetParent**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getparent): Calls [**FrameworkElement.Parent**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.parent) from the owner, and looks up the appropriate peer. This isn't an override pair with a "Core" method, so you can't change this behavior.

> [!NOTE]
> Default framework peers implement behavior by using internal native code, not necessarily by using projected managed code. You won't be able to inspect full implementation details through common language runtime (CLR) reflection or similar techniques. You also won't see distinct reference pages for subclass-specific overrides of base peer behavior. For example, there might be additional behavior for [**GetNameCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getnamecore) of a [**TextBoxAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.textboxautomationpeer), which won't be described on the **AutomationPeer.GetNameCore** reference page, and there is no dedicated **TextBoxAutomationPeer.GetNameCore** reference page. Instead, read the reference topic for the most immediate peer class and look for implementation notes in the Remarks section.

## Peers and AutomationProperties

Your automation peer should provide sensible default values for accessibility metadata. You can still override parts of that metadata with [**AutomationProperties**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties) attached properties on control instances. This applies to both built-in and custom controls. For example: `<Button AutomationProperties.Name="Special"      AutomationProperties.HelpText="This is a special button."/>`

For more info about [**AutomationProperties**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties) attached properties, see [Basic accessibility information](basic-accessibility-information.md).

Some [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer) methods exist to satisfy general provider contracts but are rarely customized in control peers, because the app typically supplies that data through [**AutomationProperties**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties). For example, many apps establish labeling relationships with [**AutomationProperties.LabeledBy**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.labeledbyproperty). In contrast, [**LabeledByCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getlabeledbycore) is usually implemented only in peers that model internal item/header relationships.

## Implementing patterns

Consider a control that exposes expand/collapse behavior. Its peer should return itself when [**GetPattern**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getpattern) is queried for [**PatternInterface.ExpandCollapse**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.PatternInterface), implement [**IExpandCollapseProvider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.iexpandcollapseprovider), and define [**Expand**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.iexpandcollapseprovider.expand), [**Collapse**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.iexpandcollapseprovider.collapse), and [**ExpandCollapseState**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.iexpandcollapseprovider.expandcollapsestate).

Design accessibility into the control API itself. When behavior can be triggered either by direct UI interaction or by an automation pattern, route both paths through shared control logic. For example, if button handlers and keyboard commands can expand/collapse state, they should call the same internal methods used by peer implementations of [**Expand**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.iexpandcollapseprovider.expand) and [**Collapse**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.iexpandcollapseprovider.collapse). This keeps behavior and visual state transitions consistent, regardless of invocation path.

A typical implementation is that the provider APIs first call [**Owner**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.frameworkelementautomationpeer.owner) for access to the control instance at run time. Then the necessary behavior methods can be called on that object.

```csharp
public class IndexCardAutomationPeer : FrameworkElementAutomationPeer, IExpandCollapseProvider {
    private IndexCard ownerIndexCard;
    public IndexCardAutomationPeer(IndexCard owner) : base(owner)
    {
         ownerIndexCard = owner;
    }
}
```

An alternate implementation is that the control itself can reference its peer. This is a common pattern if you are raising automation events from the control, because the [**RaiseAutomationEvent**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.raiseautomationevent) method is a peer method.

## UI Automation events  

UI Automation events generally fall into these categories.

| Event | Description |
| ----- | ----------- |
| Property change | Fires when a property on a UI Automation element or control pattern changes. For example, if a client needs to monitor an app's check box control, it can register to listen for a property change event on the [**ToggleState**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.itoggleprovider.togglestate) property. When the check box control is checked or unchecked, the provider fires the event and the client can act as necessary. |
| Element action | Fires when a change in the UI results from user or programmatic activity; for example, when a button is clicked or invoked through the **Invoke** pattern. |
| Structure change | Fires when the structure of the UI Automation tree changes. The structure changes when new UI items become visible, hidden, or removed on the desktop. |
| Global change | Fires when actions of global interest to the client occur, such as when the focus shifts from one element to another, or when a child window closes. Some events do not necessarily mean that the state of the UI has changed. For example, if the user tabs to a text-entry field and then clicks a button to update the field, a [**TextChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox.textchanged) event fires even if the user did not actually change the text. When processing an event, it may be necessary for a client application to check whether anything has actually changed before taking action. |

### AutomationEvents identifiers

UI Automation events are identified by [**AutomationEvents**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationEvents) values. The values of the enumeration uniquely identify the kind of event.

### Raising events  
UI Automation clients can subscribe to automation events. In the automation peer model, peers for custom controls must report changes to control state that are relevant to accessibility by calling the [**RaiseAutomationEvent**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.raiseautomationevent) method. Similarly, when a key UI Automation property value changes, custom control peers should call the [**RaisePropertyChangedEvent**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.raisepropertychangedevent) method.

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

## Peer navigation

After obtaining a peer, UI Automation clients can navigate an app's peer structure by calling [**GetChildren**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getchildren) and [**GetParent**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getparent). Within a control, child navigation is produced by [**GetChildrenCore**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer.getchildrencore), which UI Automation uses to build the subtree (for example, items in a list). The default implementation in [**FrameworkElementAutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.FrameworkElementAutomationPeer) traverses the visual tree. Custom controls can override this to expose a more meaningful automation representation.

## Native automation support for text patterns

Some default framework peers support text patterns ([**PatternInterface.Text**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.patterninterface)) through native implementation details, so [**ITextProvider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.itextprovider) may not appear in managed inheritance. Even so, pattern queries from managed or unmanaged clients can still report text-pattern support and expose corresponding behavior.

If you intend to derive from one of the text controls and also create a custom peer that derives from one of the text-related peers, check the Remarks sections in the API reference for the peer to learn more about any native-level support for patterns. You can access the native base behavior in your custom peer if you call the base implementation from your managed provider interface implementations, but it's difficult to modify what the base implementation does because the native interfaces on both the peer and its owner control aren't exposed. Generally you should either use the base implementations as-is (call base only) or completely replace the functionality with your own managed code and don't call the base implementation. The latter is an advanced scenario, and it requires strong familiarity with the text services framework used by your control to satisfy accessibility requirements.

## AutomationProperties.AccessibilityView

In addition to custom peers, you can adjust automation-tree representation per control instance by setting [**AutomationProperties.AccessibilityView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.accessibilityview) in XAML. This is not peer code, but it is often important when refining accessibility for custom templates.

The primary use case is omitting template parts that do not contribute meaningful accessibility semantics for the composed control. To do this, set **AutomationProperties.AccessibilityView** to "Raw".

## Throwing exceptions from automation peers

Peer implementations are allowed to throw exceptions, and robust clients are expected to continue processing. In practice, clients often inspect automation trees spanning multiple apps, so failure in one subtree should not terminate the client process.

Input validation is appropriate. For example, throw [**ArgumentNullException**](/dotnet/api/system.argumentnullexception) when **null** is invalid for your implementation. Also account for timing: peer interactions are not always synchronized with current visual state, so control state may change between peer creation and method execution. For those scenarios, providers can use two dedicated exceptions:

* Throw [**ElementNotAvailableException**](/dotnet/api/system.windows.automation.elementnotavailableexception) if you're unable to access either the peer's owner or a related peer element based on the original info your API was passed. For example, you might have a peer that's trying to run its methods but the owner has since been removed from the UI, such as a modal dialog that's been closed. For a non-.NET client, this maps to [**UIA\_E\_ELEMENTNOTAVAILABLE**](/windows/desktop/WinAuto/uiauto-error-codes).
* Throw [**ElementNotEnabledException**](/dotnet/api/system.windows.automation.elementnotenabledexception) if there still is an owner, but that owner is in a mode such as [**IsEnabled**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control.isenabled)`=`**false** that's blocking some of the specific programmatic changes that your peer is trying to accomplish. For a non-.NET client, this maps to [**UIA\_E\_ELEMENTNOTENABLED**](/windows/desktop/WinAuto/uiauto-error-codes).

More generally, be conservative with exceptions. Many clients cannot convert provider exceptions into meaningful user actions. In some paths, no-op behavior or local exception handling is preferable to repeatedly throwing. Also remember that many UI Automation clients are COM-based and primarily evaluate **HRESULT** results such as **S\_OK**.

## Related topics  
* [Accessibility overview](accessibility-overview.md)
* [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample)
* [**FrameworkElementAutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.FrameworkElementAutomationPeer)
* [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer)
* [**OnCreateAutomationPeer**](/uwp/api/windows.ui.xaml.uielement.oncreateautomationpeer)
