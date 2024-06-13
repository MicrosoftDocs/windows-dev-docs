---
description: This topic explains the XML/XAML namespace (xmlns) mappings as found in the root element of most XAML files. It also describes how to produce similar mappings for custom types and assemblies.
title: XAML namespaces and namespace mapping
ms.assetid: A19DFF78-E692-47AE-8221-AB5EA9470E8B
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# XAML namespaces and namespace mapping


This topic explains the XML/XAML namespace (**xmlns**) mappings as found in the root element of most XAML files. It also describes how to produce similar mappings for custom types and assemblies.

## How XAML namespaces relate to code definition and type libraries

Both in its general purpose and for its application to Windows Runtime app programming, XAML is used to declare objects, properties of those objects, and object-property relationships expressed as hierarchies. The objects you declare in XAML are backed by type libraries or other representations that are defined by other programming techniques and languages. These libraries might be:

-   The built-in set of objects for the Windows Runtime. This is a fixed set of objects, and accessing these objects from XAML uses internal type-mapping and activation logic.
-   Distributed libraries that are provided either by Microsoft or by third parties.
-   Libraries that represent the definition of a third-party control that your app incorporates and your package redistributes.
-   Your own library, which is part of your project and which holds some or all of your user code definitions.

Backing type info is associated with particular XAML namespace definitions. XAML frameworks such as the Windows Runtime can aggregate multiple assemblies and multiple code namespaces to map to a single XAML namespace. This enables the concept of a XAML vocabulary that covers a larger programming framework or technology. A XAML vocabulary can be quite extensive—for example, most of the XAML documented for Windows Runtime apps in this reference constitutes a single XAML vocabulary. A XAML vocabulary is also extensible: you extend it by adding types to the backing code definitions, making sure to include the types in code namespaces that are already used as mapped namespace sources for the XAML vocabulary.

A XAML processor can look up types and members from the backing assemblies associated with that XAML namespace when it creates a run-time object representation. This is why XAML is useful as a way to formalize and exchange definitions of object-construction behavior, and why XAML is used as a UI definition technique for a UWP app.

## XAML namespaces in typical XAML markup usage

A XAML file almost always declares a default XAML namespace in its root element. The default XAML namespace defines which elements you can declare without qualifying them by a prefix. For example, if you declare an element `<Balloon />`, a XAML parser will expect that an element **Balloon** exists and is valid in the default XAML namespace. In contrast, if **Balloon** is not in the defined default XAML namespace, you must instead qualify that element name with a prefix, for example `<party:Balloon />`. The prefix indicates that the element exists in a different XAML namespace than the default namespace, and you must map a XAML namespace to the prefix **party** before you can use this element. XAML namespaces apply to the specific element on which they are declared, and also to any element that is contained by that element in the XAML structure. For this reason, XAML namespaces are almost always declared on root elements of a XAML file to take advantage of this inheritance.

## The default and XAML language XAML namespace declarations

Within the root element of most XAML files, there are two **xmlns** declarations. The first declaration maps a XAML namespace as the default: `xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"`

This is the same XAML namespace identifier used in several predecessor Microsoft technologies that also use XAML as a UI definition markup format. The use of the same identifier is deliberate, and is helpful when you migrate previously defined UI to a Windows Runtime app using C++, C#, or Visual Basic.

The second declaration maps a separate XAML namespace for the XAML-defined language elements, mapping it (typically) to the "x:" prefix: `xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"`

This **xmlns** value, and the "x:" prefix it is mapped to, is also identical to the definitions used in several predecessor Microsoft technologies that use XAML.

The relationship between these declarations is that XAML is a language definition, and the Windows Runtime is one implementation that uses XAML as a language and defines a specific vocabulary where its types are referenced in XAML.

The XAML language specifies certain language elements, and each of these should be accessible through XAML processor implementations working against the XAML namespace. The "x:" mapping convention for the XAML language XAML namespace is followed by project templates, sample code, and the documentation for language features. The XAML language namespace defines several commonly used features that are necessary even for basic Windows Runtime apps using C++, C#, or Visual Basic. For example, to join any code-behind to a XAML file through a partial class, you must name that class as the [x:Class attribute](x-class-attribute.md) in the root element of the relevant XAML file. Or, any element as defined in a XAML page as a keyed resource in a [ResourceDictionary and XAML resource references](/windows/apps/design/style/xaml-resource-dictionary) must have the [x:Key attribute](x-key-attribute.md) set on the object element in question.

## Code namespaces that map to the default XAML namespace

The following is a list of code namespaces that are currently mapped to the default XAML namespace.

* Windows.UI
* Windows.UI.Xaml
* Windows.UI.Xaml.Automation
* Windows.UI.Xaml.Automation.Peers
* Windows.UI.Xaml.Automation.Provider
* Windows.UI.Xaml.Automation.Text
* Windows.UI.Xaml.Controls
* Windows.UI.Xaml.Controls.Primitives
* Windows.UI.Xaml.Data
* Windows.UI.Xaml.Documents
* Windows.UI.Xaml.Input
* Windows.UI.Xaml.Interop
* Windows.UI.Xaml.Markup
* Windows.UI.Xaml.Media
* Windows.UI.Xaml.Media.Animation
* Windows.UI.Xaml.Media.Imaging
* Windows.UI.Xaml.Media.Media3D
* Windows.UI.Xaml.Navigation
* Windows.UI.Xaml.Resources
* Windows.UI.Xaml.Shapes
* Windows.UI.Xaml.Threading
* Windows.UI.Text

<span id="other-XAML-namespaces"></span>

## Other XAML namespaces

In addition to the default namespace and the XAML language XAML namespace "x:", you may also see other mapped XAML namespaces in the initial default XAML for apps as generated by Microsoft Visual Studio.

### **d: (`http://schemas.microsoft.com/expression/blend/2008`)**

The "d:" XAML namespace is intended for designer support, specifically designer support in the XAML design surfaces of Microsoft Visual Studio. The" d:" XAML namespace enables designer or design-time attributes on XAML elements. These designer attributes affect only the design aspects of how XAML behaves. The designer attributes are ignored when the same XAML is loaded by the Windows Runtime XAML parser when an app runs. Generally, the designer attributes are valid on any XAML element, but in practice there are only certain scenarios where applying a designer attribute yourself is appropriate. In particular, many of the designer attributes are intended to provide a better experience for interacting with data contexts and data sources while you are developing XAML and code that use data binding.

-   **d:DesignHeight and d:DesignWidth attributes:** These attributes are sometimes applied to the root of a XAML file that Visual Studio or another XAML designer surface creates for you. For example, these attributes are set on the [**UserControl**](/uwp/api/Windows.UI.Xaml.Controls.UserControl) root of the XAML that is created if you add a new **UserControl** to your app project. These attributes make it easier to design the composition of the XAML content, so that you have some anticipation of the layout constraints that might exist once that XAML content is used for a control instance or other part of a larger UI page.

   **Note**  If you are migrating XAML from Microsoft Silverlight you might have these attributes on root elements that represent an entire UI page. You might want to remove the attributes in this case. Other features of the XAML designers such as the simulator are probably more useful for designing page layouts that handle scaling and view states well than is a fixed size page layout using **d:DesignHeight** and **d:DesignWidth**.

-   **d:DataContext attribute:** You can set this attribute on a page root or a control to override any explicit or inherited [**DataContext**](/uwp/api/windows.ui.xaml.frameworkelement.datacontext) that object otherwise has.
-   **d:DesignSource attribute:** Specifies a design-time data source for a [**CollectionViewSource**](/uwp/api/Windows.UI.Xaml.Data.CollectionViewSource), overriding [**Source**](/uwp/api/windows.ui.xaml.data.collectionviewsource.source).
-   **d:DesignInstance and d:DesignData markup extensions:** These markup extensions are used to provide the design-time data resources for either **d:DataContext** or **d:DesignSource**. We won't fully document how to use design-time data resources here. For more info, see [Design-Time Attributes](/previous-versions/windows/silverlight/dotnet-windows-silverlight/ff602277(v=vs.95)). For some usage examples, see [Sample data on the design surface, and for prototyping](../data-binding/displaying-data-in-the-designer.md).

### **mc: (`http://schemas.openxmlformats.org/markup-compatibility/2006`)**

" mc:" indicates and supports a markup compatibility mode for reading XAML. Typically, the "d:" prefix is associated with the attribute **mc:Ignorable**. This technique enables run-time XAML parsers to ignore the design attributes in "d:".

### **local:** and **common:**

"local:" is a prefix that is often mapped for you within the XAML pages for a templated UWP app project. It's mapped to refer to the same namespace that's created to contain the [x:Class attribute](x-class-attribute.md) and code for all the XAML files including app.xaml. So long as you define any custom classes you want to use in XAML in this same namespace, you can use the **local:** prefix to refer to your custom types in XAML. A related prefix that comes from a templated UWP app project is **common:**. This prefix refers to a nested "Common" namespace that contains utility classes such as converters and commands, and you can find the definitions in the Common folder in the **Solution Explorer** view.

### **vsm:**

Do not use. "vsm:" is a prefix that is sometimes seen in older XAML templates imported from other Microsoft technologies. The namespace originally addressed a legacy namespace tooling issue. You should delete XAML namespace definitions for "vsm:" in any XAML you use for the Windows Runtime, and change any prefix usages for [**VisualState**](/uwp/api/Windows.UI.Xaml.VisualState), [**VisualStateGroup**](/uwp/api/Windows.UI.Xaml.VisualStateGroup) and related objects to use the default XAML namespace instead. For more info on XAML migration, see [Migrating Silverlight or WPF XAML/code to a Windows Runtime app](/previous-versions/windows/apps/br229571(v=win.10)).

## Mapping custom types to XAML namespaces and prefixes

You can map a XAML namespace so that you can use XAML to access your own custom types. In other words, you are mapping a code namespace as it exists in a code representation that defines the custom type, and assigning it a XAML namespace along with a prefix for usage. Custom types for XAML can be defined either in a Microsoft .NET language (C# or Microsoft Visual Basic) or in C++. The mapping is made by defining an **xmlns** prefix. For example, `xmlns:myTypes` defines a new XAML namespace that is accessed by prefixing all usages with the token `myTypes:`.

An **xmlns** definition includes a value as well as the prefix naming. The value is a string that goes inside quotation marks, following an equal sign. A common XML convention is to associate the XML namespace with a Uniform Resource Identifier (URI), so that there is a convention for uniqueness and identification. You also see this convention for the default XAML namespace and the XAML language XAML namespace, as well as for some lesser-used XAML namespaces that are used by Windows Runtime XAML. But for a XAML namespace that maps custom types, instead of specifying a URI, you begin the prefix definition with the token "using:". Following the "using:" token, you then name the code namespace.

For example, to map a "custom1" prefix that enables you to reference a "CustomClasses" namespace, and use classes from that namespace or assembly as object elements in XAML, your XAML page should include the following mapping on the root element: `xmlns:custom1="using:CustomClasses"`

Partial classes of the same page scope do not need to be mapped. For example, you don't need prefixes to reference any event handlers that you defined for handling events from the XAML UI definition of your page. Also, many of the starting XAML pages from Visual Studio generated projects for a Windows Runtime app using C++, C#, or Visual Basic already map a "local:" prefix, which references the project-specified default namespace and the namespace used by partial class definitions.

### CLR language rules

If you are writing your backing code in a .NET language (C# or Microsoft Visual Basic), you might be using conventions that use a dot (".") as part of namespace names to create a conceptual hierarchy of code namespaces. If your namespace definition contains a dot, the dot should be part of the value you specify after the "using:" token.

If your code-behind file or code definition file is a C++ file, there are certain conventions that still follow the common language runtime (CLR) language form, so that there is no difference in the XAML syntax. If you declare nested namespaces in C++, the separator between the successive nested namespace strings should be "." rather than "::" when you specify the value that follows the "using:" token.

Don't use nested types (such as nesting an enumeration within a class) when you define your code for use with XAML. Nested types can't be evaluated. There's no way for the XAML parser to distinguish that a dot is part of the nested type name rather than part of the namespace name.

## Custom types and assemblies

The name of the assembly that defines the backing types for a XAML namespace is not specified in the mapping. The logic for which assemblies are available is controlled at the app-definition level and is part of basic app deployment and security principles. Declare any assembly that you want included as a code-definition source for XAML as a dependent assembly in project settings. For more info, see [Creating Windows Runtime components in C# and Visual Basic](/previous-versions/windows/apps/hh441572(v=vs.140)).

If you are referencing custom types from the primary app's application definition or page definitions, those types are available without further dependent assembly configuration, but you still must map the code namespace that contains those types. A common convention is to map the prefix "local" for the default code namespace of any given XAML page. This convention is often included in starting project templates for XAML projects.

## Attached properties

If you are referencing attached properties, the owner-type portion of the attached property name must either be in the default XAML namespace or be prefixed. It's rare to prefix attributes separately from their elements but this is one case where it's sometimes required, particularly for a custom attached property. For more info, see [Custom attached properties](custom-attached-properties.md).

## Related topics

* [XAML overview](xaml-overview.md)
* [XAML syntax guide](xaml-syntax-guide.md)
* [Creating Windows Runtime components in C# and Visual Basic](/previous-versions/windows/apps/hh441572(v=vs.140))
* [C#, VB, and C++ project templates for Windows Runtime apps](/previous-versions/windows/apps/hh768232(v=win.10))
* [Migrating Silverlight or WPF XAML/code to a Windows Runtime app](/previous-versions/windows/apps/br229571(v=win.10))
 