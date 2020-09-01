---
description: Introduces the XAML language and XAML concepts to the Windows Runtime app developer audience, and describes the different ways to declare objects and set attributes in XAML as it is used for creating a Windows Runtime app.
title: XAML overview
ms.assetid: 48041B37-F1A8-44A4-BB8E-1D4DE30E7823
ms.date: 07/18/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs:
  - csharp
  - vb
  - cppwinrt
  - cpp
---
# XAML overview

This article introduces the XAML language and XAML concepts to the Windows Runtime app developer audience, and describes the different ways to declare objects and set attributes in XAML as it is used for creating a Windows Runtime app.

## What is XAML?

Extensible Application Markup Language (XAML) is a declarative language. Specifically, XAML can initialize objects and set properties of objects using a language structure that shows hierarchical relationships between multiple objects and a backing type convention that supports extension of types. You can create visible UI elements in the declarative XAML markup. You can then associate a separate code-behind file for each XAML file that can respond to events and manipulate the objects that you originally declare in XAML.

The XAML language supports interchange of sources between different tools and roles in the development process, such as exchanging XAML sources between design tools and an interactive development environment (IDE) or between primary developers and localization developers. By using XAML as the interchange format, designer roles and developer roles can be kept separate or brought together, and designers and developers can iterate during the production of an app.

When you see them as part of your Windows Runtime app projects, XAML files are XML files with the .xaml file name extension.

## Basic XAML syntax

XAML has a basic syntax that builds on XML. By definition, valid XAML must also be valid XML. But XAML also has syntax concepts that are assigned a different and more complete meaning while still being valid in XML per the XML 1.0 specification. For example, XAML supports *property element syntax*, where property values can be set within elements rather than as string values in attributes or as content. To regular XML, a XAML property element is an element with a dot in its name, so it's valid to plain XML but doesn't have the same meaning.

## XAML and Visual Studio

Microsoft Visual Studio helps you to produce valid XAML syntax, both in the XAML text editor and in the more graphically oriented XAML design surface. When you write XAML for your app using Visual Studio, don't worry too much about the syntax with each keystroke. The IDE encourages valid XAML syntax by providing autocompletion hints, showing suggestions in Microsoft IntelliSense lists and dropdowns, showing UI element libraries in the **Toolbox** window, or other techniques. If this is your first experience with XAML, it might still be useful to know the syntax rules and particularly the terminology that is sometimes used to describe the restrictions or choices when describing XAML syntax in reference or other topics. The fine points of XAML syntax are covered in a separate topic, [XAML syntax guide](xaml-syntax-guide.md).

## XAML namespaces

In general programming, a namespace is an organizing concept that determines how identifiers for programming entities are interpreted. By using namespaces, a programming framework can separate user-declared identifiers from framework-declared identifiers, disambiguate identifiers through namespace qualifications, enforce rules for scoping names, and so on. XAML has its own XAML namespace concept that serves this purpose for the XAML language. Here's how XAML applies and extends the XML language namespace concepts:

- XAML uses the reserved XML attribute **xmlns** for namespace declarations. The value of the attribute is typically a Uniform Resource Identifier (URI), which is a convention inherited from XML.
- XAML uses prefixes in declarations to declare non-default namespaces, and prefix usages in elements and attributes reference that namespace.
- XAML has a concept of a default namespace, which is the namespace used when no prefix exists in a usage or declaration. The default namespace can be defined differently for each XAML programming framework.
- Namespace definitions inherit in a XAML file or construct, from parent element to child element. For example, if you define a namespace in the root element of a XAML file, all elements within that file inherit that namespace definition. If an element further into the page redefines the namespace, that element's descendants inherit the new definition.
- Attributes of an element inherit the element's namespaces. It's fairly uncommon to see prefixes on XAML attributes.

A XAML file almost always declares a default XAML namespace in its root element. The default XAML namespace defines which elements you can declare without qualifying them by a prefix. For typical Windows Runtime app projects, this default namespace contains all the built-in XAML vocabulary for the Windows Runtime that's used for UI definitions: the default controls, text elements, XAML graphics and animations, databinding and styling support types, and so on. Most of the XAML you'll write for Windows Runtime apps will thus be able to avoid using XAML namespaces and prefixes when referring to common UI elements.

Here's a snippet that shows a template-created <xref:Windows.UI.Xaml.Controls.Page> root of the initial page for an app (showing the opening tag only, and simplified). It declares the default namespace and also the **x** namespace (which we'll explain next).

```xml
<Page
    x:Class="Application1.BlankPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
>
```

## The XAML-language XAML namespace

One particular XAML namespace that is declared in nearly every Windows Runtime XAML file is the XAML-language namespace. This namespace includes elements and concepts that are defined by the XAML language specification. By convention, the XAML-language XAML namespace is mapped to the prefix "x". The default project and file templates for Windows Runtime app projects always define both the default XAML namespace (no prefix, just `xmlns=`) and the XAML-language XAML namespace (prefix "x") as part of the root element.

The "x" prefix/XAML-language XAML namespace contains several programming constructs that you use often in your XAML. Here are the most common ones:

| Term | Description |
|------|-------------|
| [x:Key](x-key-attribute.md) | Sets a unique user-defined key for each resource in a XAML <xref:Windows.UI.Xaml.ResourceDictionary>. The key's token string is the argument for the **StaticResource** markup extension, and you use this key later to retrieve the XAML resource from another XAML usage elsewhere in your app's XAML. |
| [x:Class](x-class-attribute.md) | Specifies the code namespace and code class name for the class that provides code-behind for a XAML page. This names the class that is created or joined by the build actions when you build your app. These build actions support the XAML markup compiler and combine your markup and code-behind when the app is compiled. You must have such a class to support code-behind for a XAML page. <xref:Windows.UI.Xaml.Window.Content%2A?displayProperty=nameWithType> in the default Windows Runtime activation model. |
| [x:Name](x-name-attribute.md) | Specifies a run-time object name for the instance that exists in run-time code after an object element defined in XAML is processed. You can think of setting **x:Name** in XAML as being like declaring a named variable in code. As you'll learn later, that's exactly what happens when your XAML is loaded as a component of a Windows Runtime app. <br/><div class="alert">**Note** <xref:Windows.UI.Xaml.FrameworkElement.Name%2A> is a similar property in the framework, but not all elements support it. Use **x:Name** for element identification whenever **FrameworkElement.Name** is not supported on that element type. |
| [x:Uid](x-uid-directive.md) | Identifies elements that should use localized resources for some of their property values. For more info on how to use **x:Uid**, see [Quickstart: Translating UI resources](/previous-versions/windows/apps/hh965329(v=win.10)). |
| [XAML intrinsic data types](xaml-intrinsic-data-types.md) | These types can specify values for simple value-types when that's required for an attribute or resource. These intrinsic types correspond to the simple value types that are typically defined as part of each programming language's intrinsic definitions. For example, you might need an object representing a **true** Boolean value to use in an <xref:Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames> storyboarded visual state. For that value in XAML, you'd use the **x:Boolean** intrinsic type as the object element, like this: <code>&lt;x:Boolean&gt;True&lt;/x:Boolean&gt;</code> |

Other programming constructs in the XAML-language XAML namespace exist but are not as common.

## Mapping custom types to XAML namespaces

One of the most powerful aspects of XAML as a language is that it's easy to extend the XAML vocabulary for your Windows Runtime apps. You can define your own custom types in your app's programming language and then reference your custom types in XAML markup. Support for extension through custom types is fundamentally built-in to how the XAML language works. Frameworks or app developers are responsible for creating the backing objects that XAML references. Neither frameworks nor the app developer are bound by specifications of what the objects in their vocabularies represent or do beyond the basic XAML syntax rules. (There are some expectations of what the XAML-language XAML namespace types should do, but the Windows Runtime provides all the necessary support.)

If you use XAML for types that come from libraries other than the Windows Runtime core libraries and metadata, you must declare and map a XAML namespace with a prefix. Use that prefix in element usages to reference the types that were defined in your library. You declare prefix mappings as **xmlns** attributes, typically in a root element along with the other XAML namespace definitions.

To make your own namespace definition that references custom types, you first specify the keyword **xmlns:**, then the prefix you want. The value of that attribute must contain the keyword **using:** as the first part of the value. The remainder of the value is a string token that references the specific code-backing namespace that contains your custom types, by name.

The prefix defines the markup token that is used to refer to that XAML namespace in the remainder of the markup in that XAML file. A colon character (:) goes between the prefix and the entity to be referenced within the XAML namespace.

For example, the attribute syntax to map a prefix `myTypes` to the namespace `myCompany.myTypes` is: `    xmlns:myTypes="using:myCompany.myTypes"`, and a representative element usage is: `<myTypes:CustomButton/>`

For more info on mapping XAML namespaces for custom types, including special considerations for Visual C++ component extensions (C++/CX), see [XAML namespaces and namespace mapping](xaml-namespaces-and-namespace-mapping.md).

## Other XAML namespaces

You often see XAML files that define the prefixes "d" (for designer namespace) and "mc" (for markup compatibility). Generally, these are for infrastructure support or to enable scenarios in a design-time tool. For more info, see the ["Other XAML namespaces" section of the XAML namespaces topic](xaml-namespaces-and-namespace-mapping.md#other-XAML-namespaces).

## Markup extensions

Markup extensions are a XAML language concept that is often used in the Windows Runtime XAML implementation. Markup extensions often represent some kind of "shortcut" that enables a XAML file to access a value or behavior that isn't simply declaring elements based on backing types. Some markup extensions can set properties with plain strings or with additionally nested elements, with the goal of streamlining the syntax or the factoring between different XAML files.

In XAML attribute syntax, curly braces "{" and "}" indicate a XAML markup extension usage. This usage directs the XAML processing to escape from the general treatment of treating attribute values as either a literal string or a directly string-convertible value. Instead, a XAML parser calls code that provides behavior for that particular markup extension, and that code provides an alternate object or behavior result that the XAML parser needs. Markup extensions can have arguments, which follow the markup extension name and are also contained within the curly braces. Typically, an evaluated markup extension provides an object return value. During parsing, that return value is inserted into the position in the object tree where the markup extension usage was in the source XAML.

Windows Runtime XAML supports these markup extensions that are defined under the default XAML namespace and are understood by the Windows Runtime XAML parser:

- [{x:Bind}](x-bind-markup-extension.md): supports data binding, which defers property evaluation until run-time by executing special-purpose code, which it generates at compile-time. This markup extension supports a wide range of arguments.
- [{Binding}](binding-markup-extension.md): supports data binding, which defers property evaluation until run-time by executing general-purpose runtime object inspection. This markup extension supports a wide range of arguments.
- [{StaticResource}](staticresource-markup-extension.md): supports referencing resource values that are defined in a <xref:Windows.UI.Xaml.ResourceDictionary>. These resources can be in a different XAML file but must ultimately be findable by the XAML parser at load time. The argument of a `{StaticResource}` usage identifies the key (the name) for a keyed resource in a <xref:Windows.UI.Xaml.ResourceDictionary>.
- [{ThemeResource}](themeresource-markup-extension.md): similar to [{StaticResource}](staticresource-markup-extension.md) but can respond to run-time theme changes. {ThemeResource} appears quite often in the Windows Runtime default XAML templates, because most of these templates are designed for compatibility with the user switching the theme while the app is running.
- [{TemplateBinding}](templatebinding-markup-extension.md): a special case of [{Binding}](binding-markup-extension.md) that supports control templates in XAML and their eventual usage at run time.
- [{RelativeSource}](relativesource-markup-extension.md): enables a particular form of template binding where values come from the templated parent.
- [{CustomResource}](customresource-markup-extension.md): for advanced resource lookup scenarios.

Windows Runtime also supports the [{x:Null} markup extension](x-null-markup-extension.md). You use this to set [**Nullable**](/dotnet/api/system.nullable-1) values to **null** in XAML. For example you might use this in a control template for a <xref:Windows.UI.Xaml.Controls.CheckBox>, which interprets **null** as an indeterminate check state (triggering the "Indeterminate" visual state).

A markup extension generally returns an existing instance from some other part of the object graph for the app or defers a value to run time. Because you can use a markup extension as an attribute value, and that's the typical usage, you often see markup extensions providing values for reference-type properties that might have otherwise required a property element syntax.

For example, here's the syntax for referencing a reusable <xref:Windows.UI.Xaml.Style> from a <xref:Windows.UI.Xaml.ResourceDictionary>: `<Button Style="{StaticResource SearchButtonStyle}"/>`. A <xref:Windows.UI.Xaml.Style> is a reference type, not a simple value, so without the `{StaticResource}` usage, you would've needed a `<Button.Style>` property element and a `<Style>` definition within it to set the <xref:Windows.UI.Xaml.FrameworkElement.Style%2A?displayProperty=nameWithType> property.

By using markup extensions, every property that is settable in XAML is potentially settable in attribute syntax. You can use attribute syntax to provide reference values for a property even if it doesn't otherwise support an attribute syntax for direct object instantiation. Or you can enable specific behavior that defers the general requirement that XAML properties be filled by value types or by newly created reference types.

To illustrate, the next XAML example sets the value of the <xref:Windows.UI.Xaml.FrameworkElement.Style%2A?displayProperty=nameWithType> property of a <xref:Windows.UI.Xaml.Controls.Border> by using attribute syntax. The <xref:Windows.UI.Xaml.FrameworkElement.Style%2A?displayProperty=nameWithType> property takes an instance of the <xref:Windows.UI.Xaml.Style?displayProperty=nameWithType> class, a reference type that by default could not be created using an attribute syntax string. But in this case, the attribute references a particular markup extension, [StaticResource](staticresource-markup-extension.md). When that markup extension is processed, it returns a reference to a **Style** element that was defined earlier as a keyed resource in a resource dictionary.

```xml
<Canvas.Resources>
  <Style TargetType="Border" x:Key="PageBackground">
    <Setter Property="BorderBrush" Value="Blue"/>
    <Setter Property="BorderThickness" Value="5"/>
  </Style>
</Canvas.Resources>
...
<Border Style="{StaticResource PageBackground}">
  ...
</Border>
```

You can nest markup extensions. The innermost markup extension is evaluated first.

Because of markup extensions, you need special syntax for a literal "{" value in an attribute. For more info see [XAML syntax guide](xaml-syntax-guide.md).

## Events

XAML is a declarative language for objects and their properties, but it also includes a syntax for attaching event handlers to objects in the markup. The XAML event syntax can then integrate the XAML-declared events through the Windows Runtime programming model. You specify the name of the event as an attribute name on the object where the event is handled. For the attribute value, you specify the name of an event-handler function that you define in code. The XAML processor uses this name to create a delegate representation in the loaded object tree, and adds the specified handler to an internal handler list. Nearly all Windows Runtime apps are defined by both markup and code-behind sources.

Here's a simple example. The <xref:Windows.UI.Xaml.Controls.Button> class supports an event named <xref:Windows.UI.Xaml.Controls.Primitives.ButtonBase.Click>. You can write a handler for **Click** that runs code that should be invoked after the user clicks the **Button**. In XAML, you specify **Click** as an attribute on the **Button**. For the attribute value, provide a string that is the method name of your handler.

```xml
<Button Click="showUpdatesButton_Click">Show updates</Button>
```

When you compile, the compiler now expects that there will be a method named `showUpdatesButton_Click` defined in the code-behind file, in the namespace declared in the XAML page's [x:Class](x-class-attribute.md) value. Also, that method must satisfy the delegate contract for the <xref:Windows.UI.Xaml.Controls.Primitives.ButtonBase.Click> event. For example:

```csharp
namespace App1
{
    public sealed partial class MainPage: Page {
        ...
        private void showUpdatesButton_Click (object sender, RoutedEventArgs e) {
            //your code
        }
    }
}
```

```vb
' Namespace included at project level
Public NotInheritable Class MainPage
    Inherits Page
        ...
        Private Sub showUpdatesButton_Click (sender As Object, e As RoutedEventArgs e)
            ' your code
        End Sub
    ...
End Class
```

```cppwinrt
namespace winrt::App1::implementation
{
    struct MainPage : MainPageT<MainPage>
    {
        ...
        void showUpdatesButton_Click(Windows::Foundation::IInspectable const&, Windows::UI::Xaml::RoutedEventArgs const&);
    };
}
```

```cpp
// .h
namespace App1
{
    public ref class MainPage sealed {
        ...
    private:
        void showUpdatesButton_Click(Object^ sender, RoutedEventArgs^ e);
    };
}
```

Within a project, the XAML is written as a .xaml file, and you use the language you prefer (C#, Visual Basic, C++/CX) to write a code-behind file. When a XAML file is markup-compiled as part of a build action for the project, the location of the XAML code-behind file for each XAML page is identified by specifying a namespace and class as the [x:Class](x-class-attribute.md) attribute of the root element of the XAML page. For more info on how these mechanisms work in XAML and how they relate to the programming and application models, see [Events and routed events overview](events-and-routed-events-overview.md).

> [!NOTE]
> For C++/CX there are two code-behind files: one is a header (.xaml.h) and the other is implementation (.xaml.cpp). The implementation references the header, and it's technically the header that represents the entry point for the code-behind connection.

## Resource dictionaries

Creating a <xref:Windows.UI.Xaml.ResourceDictionary> is a common task that is usually accomplished by authoring a resource dictionary as an area of a XAML page or a separate XAML file. Resource dictionaries and how to use them is a larger conceptual area that is outside the scope of this topic. For more info see [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md).

## XAML and XML

The XAML language is fundamentally based on the XML language. But XAML extends XML significantly. In particular it treats the concept of schema quite differently because of its relationship to the backing type concept, and adds language elements such as attached members and markup extensions. **xml:lang** is valid in XAML, but influences runtime rather than parse behavior, and is typically aliased to a framework-level property. For more info, see <xref:Windows.UI.Xaml.FrameworkElement.Language%2A?displayProperty=nameWithType>. **xml:base** is valid in markup but parsers ignore it. **xml:space** is valid, but is only relevant for scenarios described in the [XAML and whitespace](xaml-and-whitespace.md) topic. The **encoding** attribute is valid in XAML. Only UTF-8 and UTF-16 encodings are supported. UTF-32 encoding is not supported.

###  Case sensitivity in XAML

XAML is case-sensitive. This is another consequence of XAML being based on XML, which is case-sensitive. The names of XAML elements and attributes are case-sensitive. The value of an attribute is potentially case-sensitive; this depends on how the attribute value is handled for particular properties. For example, if the attribute value declares a member name of an enumeration, the built-in behavior that type-converts a member name string to return the enumeration member value is not case-sensitive. In contrast, the value of the **Name** property, and utility methods for working with objects based on the name that the **Name** property declares, treat the name string as case-sensitive.

## XAML namescopes

The XAML language defines a concept of a XAML namescope. The XAML namescope concept influences how XAML processors should treat the value of **x:Name** or **Name** applied to XAML elements, particularly the scopes in which names should be relied upon to be unique identifiers. XAML namescopes are covered in more detail in a separate topic; see [XAML namescopes](xaml-namescopes.md).

## The role of XAML in the development process

XAML plays several important roles in the app development process.

- XAML is the primary format for declaring an app's UI and elements in that UI, if you are programming using C#, Visual Basic or C++/CX. Typically at least one XAML file in your project represents a page metaphor in your app for the initially displayed UI. Additional XAML files might declare additional pages for navigation UI. Other XAML files can declare resources, such as templates or styles.
- You use the XAML format for declaring styles and templates applied to controls and UI for an app.
- You might use styles and templates either for templating existing controls, or if you define a control that supplies a default template as part of a control package. When you use it to define styles and templates, the relevant XAML is often declared as a discrete XAML file with a <xref:Windows.UI.Xaml.ResourceDictionary> root.
- XAML is the common format for designer support of creating app UI and exchanging the UI design between different designer apps. Most notably, XAML for the app can be interchanged between different XAML design tools (or design windows within tools).
- Several other technologies also define the basic UI in XAML. In relationship to Windows Presentation Foundation (WPF) XAML and Microsoft Silverlight XAML, the XAML for Windows Runtime uses the same URI for its shared default XAML namespace. The XAML vocabulary for Windows Runtime overlaps significantly with the XAML-for-UI vocabulary also used by Silverlight and to a slightly lesser extent by WPF. Thus, XAML promotes an efficient migration pathway for UI originally defined for precursor technologies that also used XAML.
- XAML defines the visual appearance of a UI, and an associated code-behind file defines the logic. You can adjust the UI design without making changes to the logic in code-behind. XAML simplifies the workflow between designers and developers.
- Because of the richness of the visual designer and design surface support for the XAML language, XAML supports rapid UI prototyping in the early development phases.

Depending on your own role in the development process, you might not interact with XAML much. The degree to which you do interact with XAML files also depends on which development environment you are using, whether you use interactive design environment features such as toolboxes and property editors, and the scope and purpose of your Windows Runtime app. Nevertheless, it is likely that during development of the app, you will be editing a XAML file at the element level using a text or XML editor. Using this info, you can confidently edit XAML in a text or XML representation and maintain the validity of that XAML file's declarations and purpose when it is consumed by tools, markup compile operations, or the run-time phase of your Windows Runtime app.

## Optimize your XAML for load performance

Here are some tips for defining UI elements in XAML using best practices for performance. Many of these tips relate to using XAML resources, but are listed here in the general XAML overview for convenience. For more info about XAML resources see [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md). For some more tips on performance, including XAML that purposely demonstrates some of the poor performance practices that you should avoid in your XAML, see [Optimize your XAML markup](../debug-test-perf/optimize-xaml-loading.md).

- If you use the same color brush often in your XAML, define a <xref:Windows.UI.Xaml.Media.SolidColorBrush> as a resource rather than using a named color as an attribute value each time.
- If you use the same resource on more than one UI page, consider defining it in <xref:Windows.UI.Xaml.Application.Resources%2A> rather than on each page. Conversely, if only one page uses a resource, don't define it in **Application.Resources** and instead define it only for the page that needs it. This is good both for XAML factoring while designing your app and for performance during XAML parsing.
- For resources that your app packages, check for unused resources (a resource that has a key, but there's no [StaticResource](staticresource-markup-extension.md) reference in your app that uses it). Remove these from your XAML before you release your app.
- If you're using separate XAML files that provides design resources (<xref:Windows.UI.Xaml.ResourceDictionary.MergedDictionaries%2A>), consider commenting or removing unused resources from these files. Even if you have a shared XAML starting point that you're using in more than one app or that provides common resources for all your app, it's still your app that packages the XAML resources each time, and potentially has to load them.
- Don't define UI elements you don't need for composition, and use the default control templates whenever possible (these templates have already been tested and verified for load performance).
- Use containers such as <xref:Windows.UI.Xaml.Controls.Border> rather than deliberate overdraws of UI elements. Basically, don't draw the same pixel multiple times. For more info on overdraw and how to test for it, see <xref:Windows.UI.Xaml.DebugSettings.IsOverdrawHeatMapEnabled?displayProperty=nameWithType>.
- Use the default items templates for <xref:Windows.UI.Xaml.Controls.ListView> or <xref:Windows.UI.Xaml.Controls.GridView>; these have special **Presenter** logic that solves performance issues when building the visual tree for large numbers of list items.

## Debug XAML

Because XAML is a markup language, some of the typical strategies for debugging within Microsoft Visual Studio are not available. For example, there is no way to set a breakpoint within a XAML file. However, there are other techniques that can help you debug issues with UI definitions or other XAML markup while you're still developing your app.

When there are problems with a XAML file, the most typical result is that some system or your app will throw a XAML parse exception. Whenever there is a XAML parse exception, the XAML loaded by the XAML parser failed to create a valid object tree. In some cases, such as when the XAML represents the first "page" of your application that is loaded as the root visual, the XAML parse exception is not recoverable.

XAML is often edited within an IDE such as Visual Studio and one of its XAML design surfaces. Visual Studio can often provide design-time validation and error checking of a XAML source as you edit it. For example it might display "squiggles" in the XAML text editor as soon as you type a bad attribute value, and you won't even have to wait for a XAML compile pass to see that something's wrong with your UI definition.

Once the app actually runs, if any XAML parse errors have gone undetected at design time, these are reported by the common language runtime (CLR) as a [**XamlParseException**](/dotnet/api/Windows.UI.Xaml.markup.xamlparseexception?view=dotnet-uwp-10.0). For more info on what you might be able to do for a run-time **XamlParseException**, see [Exception handling for Windows Runtime apps in C# or Visual Basic](/previous-versions/windows/apps/dn532194(v=win.10)).

> [!NOTE]
> Apps that use C++/CX for code don't get the specific [**XamlParseException**](/dotnet/api/Windows.UI.Xaml.markup.xamlparseexception?view=dotnet-uwp-10.0). But the message in the exception clarifies that the source of the error is XAML-related, and includes context info such as line numbers in a XAML file, just like **XamlParseException** does.

Fore more info on debugging a Windows Runtime app, see [Start a debug session](/visualstudio/debugger/start-a-debugging-session-for-a-store-app-in-visual-studio-vb-csharp-cpp-and-xaml?view=vs-2015).