---
description: A XAML namescope stores relationships between the XAML-defined names of objects and their instance equivalents. This concept is similar to the wider meaning of the term namescope in other programming languages and technologies.
title: XAML namescopes
ms.assetid: EB060CBD-A589-475E-B83D-B24068B54C21
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# XAML namescopes


A *XAML namescope* stores relationships between the XAML-defined names of objects and their instance equivalents. This concept is similar to the wider meaning of the term *namescope* in other programming languages and technologies.

## How XAML namescopes are defined

Names in XAML namescopes enable user code to reference the objects that were initially declared in XAML. The internal result of parsing XAML is that the runtime creates a set of objects that retain some or all of the relationships these objects had in the XAML declarations. These relationships are maintained as specific object properties of the created objects, or are exposed to utility methods in the programming model APIs.

The most typical use of a name in a XAML namescope is as a direct reference to an object instance, which is enabled by the markup compile pass as a project build action, combined with a generated **InitializeComponent** method in the partial class templates.

You can also use the utility method [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) yourself at run time to return a reference to objects that were defined with a name in the XAML markup.

### More about build actions and XAML

What happens technically is that the XAML itself undergoes a markup compiler pass at the same time that the XAML and the partial class it defines for code-behind are compiled together. Each object element with a **Name** or [x:Name attribute](x-name-attribute.md) defined in the markup generates an internal field with a name that matches the XAML name. This field is initially empty. Then the class generates an **InitializeComponent** method that is called only after all the XAML is loaded. Within the **InitializeComponent** logic, each internal field is then populated with the [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) return value for the equivalent name string. You can observe this infrastructure for yourself by looking at the ".g" (generated) files that are created for each XAML page in the /obj subfolder of a Windows Runtime app project after compilation. You can also see the fields and **InitializeComponent** method as members of your resulting assemblies if you reflect over them or otherwise examine their interface language contents.

**Note**  Specifically for Visual C++ component extensions (C++/CX) apps, a backing field for an **x:Name** reference is not created for the root element of a XAML file. If you need to reference the root object from C++/CX code-behind, use other APIs or tree traversal. For example you can call [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) for a known named child element and then call [**Parent**](/uwp/api/windows.ui.xaml.frameworkelement.parent).

## Creating objects at run time with XamlReader.Load

XAML can be also be used as the string input for the [**XamlReader.Load**](/uwp/api/windows.ui.xaml.markup.xamlreader.load) method, which acts analogously to the initial XAML source parse operation. **XamlReader.Load** creates a new disconnected tree of objects at run time. The disconnected tree can then be attached to some point on the main object tree. You must explicitly connect your created object tree, either by adding it to a content property collection such as **Children**, or by setting some other property that takes an object value (for example, loading a new [**ImageBrush**](/uwp/api/Windows.UI.Xaml.Media.ImageBrush) for a [**Fill**](/uwp/api/Windows.UI.Xaml.Shapes.Shape.Fill) property value).

### XAML namescope implications of XamlReader.Load

The preliminary XAML namescope defined by the new object tree created by [**XamlReader.Load**](/uwp/api/windows.ui.xaml.markup.xamlreader.load) evaluates any defined names in the provided XAML for uniqueness. If names in the provided XAML are not internally unique at this point, **XamlReader.Load** throws an exception. The disconnected object tree does not attempt to merge its XAML namescope with the main application XAML namescope, if or when it is connected to the main application object tree. After you connect the trees, your app has a unified object tree, but that tree has discrete XAML namescopes within it. The divisions occur at the connection points between objects, where you set some property to be the value returned from a **XamlReader.Load** call.

The complication of having discrete and disconnected XAML namescopes is that calls to the [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) method as well as direct managed object references no longer operate against a unified XAML namescope. Instead, the particular object that **FindName** is called on implies the scope, with the scope being the XAML namescope that the calling object is within. In the direct managed object reference case, the scope is implied by the class where the code exists. Typically, the code-behind for run-time interaction of a "page" of app content exists in the partial class that backs the root "page", and therefore the XAML namescope is the root XAML namescope.

If you call [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) to get a named object in the root XAML namescope, the method will not find the objects from a discrete XAML namescope created by [**XamlReader.Load**](/uwp/api/windows.ui.xaml.markup.xamlreader.load). Conversely, if you call **FindName** from an object obtained from out of the discrete XAML namescope, the method will not find named objects in the root XAML namescope.

This discrete XAML namescope issue only affects finding objects by name in XAML namescopes when using the [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) call.

To get references to objects that are defined in a different XAML namescope, you can use several techniques:

-   Walk the entire tree in discrete steps with [**Parent**](/uwp/api/windows.ui.xaml.frameworkelement.parent) and/or collection properties that are known to exist in your object tree structure (such as the collection returned by [**Panel.Children**](/uwp/api/windows.ui.xaml.controls.panel.children)).
-   If you are calling from a discrete XAML namescope and want the root XAML namescope, it is always easy to get a reference to the main window currently displayed. You can get the visual root (the root XAML element, also known as the content source) from the current application window in one line of code with the call `Window.Current.Content`. You can then cast to [**FrameworkElement**](/uwp/api/Windows.UI.Xaml.FrameworkElement) and call [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) from this scope.
-   If you are calling from the root XAML namescope and want an object within a discrete XAML namescope, the best thing to do is to plan ahead in your code and retain a reference to the object that was returned by [**XamlReader.Load**](/uwp/api/windows.ui.xaml.markup.xamlreader.load) and then added to the main object tree. This object is now a valid object for calling [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) within the discrete XAML namescope. You could keep this object available as a global variable or otherwise pass it by using method parameters.
-   You can avoid names and XAML namescope considerations entirely by examining the visual tree. The [**VisualTreeHelper**](/uwp/api/Windows.UI.Xaml.Media.VisualTreeHelper) API enables you to traverse the visual tree in terms of parent objects and child collections, based purely on position and index.

## XAML namescopes in templates

Templates in XAML provide the ability to reuse and reapply content in a straightforward way, but templates might also include elements with names defined at the template level. That same template might be used multiple times in a page. For this reason, templates define their own XAML namescopes, independent of the containing page where the style or template is applied. Consider this example:

```xml
<Page
  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"  >
  <Page.Resources>
    <ControlTemplate x:Key="MyTemplate">
      ....
      <TextBlock x:Name="MyTextBlock" />
    </ControlTemplate>
  </Page.Resources>
  <StackPanel>
    <SomeControl Template="{StaticResource MyTemplate}" />
    <SomeControl Template="{StaticResource MyTemplate}" />
  </StackPanel>
</Page>
```

Here, the same template is applied to two different controls. If templates did not have discrete XAML namescopes, the "MyTextBlock" name used in the template would cause a name collision. Each instantiation of the template has its own XAML namescope, so in this example each instantiated template's XAML namescope would contain exactly one name. However, the root XAML namescope does not contain the name from either template.

Because of the separate XAML namescopes, finding named elements within a template from the scope of the page where the template is applied requires a different technique. Rather than calling [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) on some object in the object tree, you first obtain the object that has the template applied, and then call [**GetTemplateChild**](/uwp/api/windows.ui.xaml.controls.control.gettemplatechild). If you are a control author and you are generating a convention where a particular named element in an applied template is the target for a behavior that is defined by the control itself, you can use the **GetTemplateChild** method from your control implementation code. The **GetTemplateChild** method is protected, so only the control author has access to it. Also, there are conventions that control authors should follow in order to name parts and template parts and report these as attribute values applied to the control class. This technique makes the names of important parts discoverable to control users who might wish to apply a different template, which would need to replace the named parts in order to maintain control functionality.

## Related topics

* [XAML overview](xaml-overview.md)
* [x:Name attribute](x-name-attribute.md)
* [Quickstart: Control templates](/previous-versions/windows/apps/hh465374(v=win.10))
* [**XamlReader.Load**](/uwp/api/windows.ui.xaml.markup.xamlreader.load)
* [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname)
 