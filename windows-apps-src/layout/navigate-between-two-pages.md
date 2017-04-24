---
author: Jwmsft
Description: Learn how to navigate in a basic two page peer-to-peer Universal Windows Platform (UWP) app.
title: Peer-to-peer navigation between two pages
ms.assetid: 0A364C8B-715F-4407-9426-92267E8FB525
label: Peer-to-peer navigation between two pages
template: detail.hbs
op-migration-status: ready
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Peer-to-peer navigation between two pages

<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Learn how to navigate in a basic two page peer-to-peer Universal Windows Platform (UWP) app.

![two-page peer-to-peer navigation example](images/nav-peertopeer-2page.png)

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**Windows.UI.Xaml.Controls.Frame**](https://msdn.microsoft.com/library/windows/apps/br242682)</li>
<li>[**Windows.UI.Xaml.Controls.Page**](https://msdn.microsoft.com/library/windows/apps/br227503)</li>
<li>[**Windows.UI.Xaml.Navigation**](https://msdn.microsoft.com/library/windows/apps/br243300)</li>
</ul>
</div>



## Create the blank app


1.  On the Microsoft Visual Studio menu, choose **File &gt; New Project**.
2.  In the left pane of the **New Project** dialog box, choose the **Visual C# -&gt; Windows -&gt; Universal** or the **Visual C++ -&gt; Windows -&gt; Universal** node.
3.  In the center pane, choose **Blank App**.
4.  In the **Name** box, enter **NavApp1**, and then choose the **OK** button.

    The solution is created and the project files appear in **Solution Explorer**.

5.  To run the program, choose **Debug** &gt; **Start Debugging** from the menu, or press F5.

    A blank page is displayed.

6.  Press Shift+F5 to stop debugging and return to Visual Studio.

## Add basic pages

Next, add two content pages to the project.

Do the following steps two times to add two pages to navigate between.

1.  In **Solution Explorer**, right-click the **BlankApp** project node to open the shortcut menu.
2.  Choose **Add** &gt; **New Item** from the shortcut menu.
3.  In the **Add New Item** dialog box, choose **Blank Page** in the middle pane.
4.  In the **Name** box, enter **Page1** (or **Page2**) and press the **Add** button.

These files should now be listed as part of your NavApp1 project.

<table>
<thead>
<tr class="header">
<th align="left">C#</th>
<th align="left">C++</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td style="vertical-align:top;"><ul>
<li>Page1.xaml</li>
<li>Page1.xaml.cs</li>
<li>Page2.xaml</li>
<li>Page2.xaml.cs</li>
</ul></td>
<td style="vertical-align:top;"><ul>
<li>Page1.xaml</li>
<li>Page1.xaml.cpp</li>
<li>Page1.xaml.h</li>
<li>Page2.xaml</li>
<li>Page2.xaml.cpp</li>
<li>Page2.xaml.h

</li>
</ul></td>
</tr>
</tbody>
</table>

 

Add the following content to the UI of Page1.xaml.

-   Add a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) element named `pageTitle` as a child element of the root [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704). Change the [**Text**](https://msdn.microsoft.com/library/windows/apps/br209676) property to `Page 1`.

```xaml
<TextBlock x:Name="pageTitle" Text="Page 1" />
```

-   Add the following [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) element as a child element of the root [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) and after the `pageTitle` [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) element.

    
```xaml
<HyperlinkButton Content="Click to go to page 2"
                 Click="HyperlinkButton_Click"
                 HorizontalAlignment="Center"/>
```

Add the following code to the `Page1` class in the Page1.xaml code-behind file to handle the `Click` event of the [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) you added previously. Here, we navigate to Page2.xaml.

> [!div class="tabbedCodeSnippets"]
```csharp
private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
{
    this.Frame.Navigate(typeof(Page2));
}
```
```cpp
void Page1::HyperlinkButton_Click(Platform::Object^ sender, RoutedEventArgs^ e)
{
    this->Frame->Navigate(Windows::UI::Xaml::Interop::TypeName(Page2::typeid));
}
```

Make the following changes to the UI of Page2.xaml.

-   Add a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) element named `pageTitle` as a child element of the root [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704). Change the value of the [**Text**](https://msdn.microsoft.com/library/windows/apps/br209676) property to `Page 2`.

```xaml
<TextBlock x:Name="pageTitle" Text="Page 2" />
```

-   Add the following [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) element as a child element of the root [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) and after the `pageTitle` [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) element.

```xaml
<HyperlinkButton Content="Click to go to page 1" 
                 Click="HyperlinkButton_Click"
                 HorizontalAlignment="Center"/>
```

Add the following code to the `Page2` class in the Page2.xaml code-behind file to handle the `Click` event of the [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) you added previously. Here, we navigate to Page1.xaml.

> [!NOTE]
> For C++ projects, you must add a `#include` directive in the header file of each page that references another page. For the inter-page navigation example presented here, page1.xaml.h file contains `#include "Page2.xaml.h"`, in turn, page2.xaml.h contains `#include "Page1.xaml.h"`.

> [!div class="tabbedCodeSnippets"]
```csharp
private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
{
    this.Frame.Navigate(typeof(Page1));
}
```
```cpp
void Page2::HyperlinkButton_Click(Platform::Object^ sender, RoutedEventArgs^ e)
{
    this->Frame->Navigate(Windows::UI::Xaml::Interop::TypeName(Page1::typeid));
}
```

Now that we've prepared the content pages, we need to make Page1.xaml display when the app starts.

Open the app.xaml code-behind file and change the `OnLaunched` handler.

Here, we specify `Page1` in the call to [**Frame.Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694) instead of `MainPage`.

> [!div class="tabbedCodeSnippets"]
> ```csharp
> protected override void OnLaunched(LaunchActivatedEventArgs e)
> {
>     Frame rootFrame = Window.Current.Content as Frame;
> 
>     // Do not repeat app initialization when the Window already has content,
>     // just ensure that the window is active
>     if (rootFrame == null)
>     {
>         // Create a Frame to act as the navigation context and navigate to the first page
>         rootFrame = new Frame();
> 
>         rootFrame.NavigationFailed += OnNavigationFailed;
> 
>         if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
>         {
>             //TODO: Load state from previously suspended application
>         }
> 
>         // Place the frame in the current Window
>         Window.Current.Content = rootFrame;
>     }
> 
>     if (rootFrame.Content == null)
>     {
>         // When the navigation stack isn't restored navigate to the first page,
>         // configuring the new page by passing required information as a navigation
>         // parameter
>         rootFrame.Navigate(typeof(Page1), e.Arguments);
>     }
>     // Ensure the current window is active
>     Window.Current.Activate();
> }
> ```
> ```cpp
> void App::OnLaunched(Windows::ApplicationModel::Activation::LaunchActivatedEventArgs^ e)
> {
>     auto rootFrame = dynamic_cast<Frame^>(Window::Current->Content);
> 
>     // Do not repeat app initialization when the Window already has content,
>     // just ensure that the window is active
>     if (rootFrame == nullptr)
>     {
>         // Create a Frame to act as the navigation context and associate it with
>         // a SuspensionManager key
>         rootFrame = ref new Frame();
> 
>         rootFrame->NavigationFailed += 
>             ref new Windows::UI::Xaml::Navigation::NavigationFailedEventHandler(
>                 this, &App::OnNavigationFailed);
> 
>         if (e->PreviousExecutionState == ApplicationExecutionState::Terminated)
>         {
>             // TODO: Load state from previously suspended application
>         }
>         
>         // Place the frame in the current Window
>         Window::Current->Content = rootFrame;
>     }
> 
>     if (rootFrame->Content == nullptr)
>     {
>         // When the navigation stack isn't restored navigate to the first page,
>         // configuring the new page by passing required information as a navigation
>         // parameter
>         rootFrame->Navigate(Windows::UI::Xaml::Interop::TypeName(Page1::typeid), e->Arguments);
>     }
> 
>     // Ensure the current window is active
>     Window::Current->Activate();
> }
> ```

**Note**  The code here uses the return value of [**Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694) to throw an app exception if the navigation to the app's initial window frame fails. When **Navigate** returns **true**, the navigation happens.

Now, build and run the app. Click the link that says "Click to go to page 2". The second page that says "Page 2" at the top should be loaded and displayed in the frame.

## Frame and Page classes

Before we add more functionality to our app, let's look at how the pages we added provide navigation support for the app.

First, a [**Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) (`rootFrame`) is created for the app in the `App.OnLaunched` method of the App.xaml code-behind file. The [**Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694) method is used to display content in this **Frame**.

**Note**  
The [**Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) class supports various navigation methods such as [**Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694), [**GoBack**](https://msdn.microsoft.com/library/windows/apps/dn996568), and [**GoForward**](https://msdn.microsoft.com/library/windows/apps/br242693), and properties such as [**BackStack**](https://msdn.microsoft.com/library/windows/apps/dn279543), [**ForwardStack**](https://msdn.microsoft.com/library/windows/apps/dn279547), and [**BackStackDepth**](https://msdn.microsoft.com/library/windows/apps/hh967995).

 
In our example, `Page1` is passed to the [**Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694) method. This method sets the content of the app's current window to the [**Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) and loads the content of the page you specify into the **Frame** (Page1.xaml in our example, or MainPage.xaml, by default).

`Page1` is a subclass of the [**Page**](https://msdn.microsoft.com/library/windows/apps/br227503) class. The **Page** class has a read-only [**Frame**](https://msdn.microsoft.com/library/windows/apps/br227504) property that gets the [**Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) containing the **Page**. When the [**Click**](https://msdn.microsoft.com/library/windows/apps/br227737) event handler of the [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) calls` Frame.Navigate(typeof(Page2))`, the **Frame** in the app's window displays the content of Page2.xaml.

Whenever a page is loaded into the frame, that page is added as a [**PageStackEntry**](https://msdn.microsoft.com/library/windows/apps/dn298572) to the [**BackStack**](https://msdn.microsoft.com/library/windows/apps/dn279543) or [**ForwardStack**](https://msdn.microsoft.com/library/windows/apps/dn279547) of the [**Frame**](https://msdn.microsoft.com/library/windows/apps/br227504).

## Pass information between pages

Our app navigates between two pages, but it really doesn't do anything interesting yet. Often, when an app has multiple pages, the pages need to share information. Let's pass some information from the first page to the second page.

In Page1.xaml, replace the the [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) you added earlier with the following [**StackPanel**](https://msdn.microsoft.com/library/windows/apps/br209635).

Here, we add a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) label and a [**TextBox**](https://msdn.microsoft.com/library/windows/apps/br209683) (`name`) for entering a text string.

```xaml
<StackPanel>
    <TextBlock HorizontalAlignment="Center" Text="Enter your name"/>
    <TextBox HorizontalAlignment="Center" Width="200" Name="name"/>
    <HyperlinkButton Content="Click to go to page 2" 
                     Click="HyperlinkButton_Click"
                     HorizontalAlignment="Center"/>
</StackPanel>
```

In the `HyperlinkButton_Click` event handler of the Page1.xaml code-behind file, add a parameter referencing the `Text` property of the `name` [**TextBox**](https://msdn.microsoft.com/library/windows/apps/br209683) to the `Navigate` method.

> [!div class="tabbedCodeSnippets"]
```csharp
private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
{
    this.Frame.Navigate(typeof(Page2), name.Text);
}
```
```cpp
void Page1::HyperlinkButton_Click(Platform::Object^ sender, RoutedEventArgs^ e)
{
    this->Frame->Navigate(Windows::UI::Xaml::Interop::TypeName(Page2::typeid), name->Text);
}
```

In Page2.xaml, replace the the [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) you added earlier with the following [**StackPanel**](https://msdn.microsoft.com/library/windows/apps/br209635).

Here, we add a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) for displaying a text string passed from Page1.

```xaml
<StackPanel>
    <TextBlock HorizontalAlignment="Center" Name="greeting"/>
    <HyperlinkButton Content="Click to go to page 1" 
                     Click="HyperlinkButton_Click"
                     HorizontalAlignment="Center"/>
</StackPanel>
```

In the Page2.xaml code-behind file, override the `OnNavigatedTo` method with the following:

> [!div class="tabbedCodeSnippets"]
```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    if (e.Parameter is string)
    {
        greeting.Text = "Hi, " + e.Parameter.ToString();
    }
    else
    {
        greeting.Text = "Hi!";
    }
    base.OnNavigatedTo(e);
}
```
```cpp
void Page2::OnNavigatedTo(NavigationEventArgs^ e)
{
    if (dynamic_cast<Platform::String^>(e->Parameter) != nullptr)
    {
        greeting->Text = "Hi," + e->Parameter->ToString();
    }
    else
    {
        greeting->Text = "Hi!";
    }
    ::Windows::UI::Xaml::Controls::Page::OnNavigatedTo(e);
}
```

Run the app, type your name in the text box, and then click the link that says **Click to go to page 2**. When you called `this.Frame.Navigate(typeof(Page2), name.Text)` in the [**Click**](https://msdn.microsoft.com/library/windows/apps/br227737) event of the [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739), the `name.Text` property was passed to `Page2` and the value from the event data is used for the message displayed on the page.

## Cache a page

Page content and state is not cached by default, you must enable it in each page of your app.

In our basic peer-to-peer example, there is no back button (we demonstrate back navigation in [Back button navigation](navigation-history-and-backwards-navigation.md)), but if you did click a back button on `Page2`, the [**TextBox**](https://msdn.microsoft.com/library/windows/apps/br209683) (and any other field) on `Page1` would be set to its default state. One way to work around this is to use the [**NavigationCacheMode**](https://msdn.microsoft.com/library/windows/apps/br227506) property to specify that a page be added to the frame's page cache.

In the constructor of `Page1`, set [**NavigationCacheMode**](https://msdn.microsoft.com/library/windows/apps/br227506) to [**Enabled**](https://msdn.microsoft.com/library/windows/apps/br243284). This retains all content and state values for the page until the page cache for the frame is exceeded.

Set [**NavigationCacheMode**](https://msdn.microsoft.com/library/windows/apps/br227506) to [**Required**](https://msdn.microsoft.com/library/windows/apps/br243284) if you want to ignore cache size limits for the frame. However, cache size limits might be crucial, depending on the memory limits of a device.

> [!NOTE]
> The [**CacheSize**](https://msdn.microsoft.com/library/windows/apps/br242683) property specifies the number of pages in the navigation history that can be cached for the frame.

> [!div class="tabbedCodeSnippets"]
```csharp
public Page1()
{
    this.InitializeComponent();
    this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
}
```
```cpp
Page1::Page1()
{
    this->InitializeComponent();
    this->NavigationCacheMode = Windows::UI::Xaml::Navigation::NavigationCacheMode::Enabled;
}
```

## Related articles

* [Navigation design basics for UWP apps](https://msdn.microsoft.com/library/windows/apps/dn958438)
* [Guidelines for tabs and pivots](https://msdn.microsoft.com/library/windows/apps/dn997788)
* [Guidelines for navigation panes](https://msdn.microsoft.com/library/windows/apps/dn997766)
 

 




