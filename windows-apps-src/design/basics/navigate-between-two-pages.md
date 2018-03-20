---
author: serenaz
Description: Learn how to enable peer-to-peer navigation between two basic pages in an Universal Windows Platform (UWP) app.
title: Peer-to-peer navigation between two pages
ms.assetid: 0A364C8B-715F-4407-9426-92267E8FB525
label: Peer-to-peer navigation between two pages
template: detail.hbs
op-migration-status: ready
ms.author: sezhen
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Implement navigation between two pages

Learn how to use a frame and pages to enable basic peer-to-peer navigation in your app. 

> **Important APIs**: [**Windows.UI.Xaml.Controls.Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) class, [**Windows.UI.Xaml.Controls.Page**](https://msdn.microsoft.com/library/windows/apps/br227503) class, [**Windows.UI.Xaml.Navigation**](https://msdn.microsoft.com/library/windows/apps/br243300) namespace

![peer to peer navigation](images/peertopeer.png)

## 1. Create a blank app

1.  On the Microsoft Visual Studio menu, choose **File** > **New Project**.
2.  In the left pane of the **New Project** dialog box, choose the **Visual C#** > **Windows** > **Universal** or the **Visual C++** > **Windows** > **Universal** node.
3.  In the center pane, choose **Blank App**.
4.  In the **Name** box, enter **NavApp1**, and then choose the **OK** button.
    The solution is created, and the project files appear in **Solution Explorer**.
5.  To run the program, choose **Debug** > **Start Debugging** from the menu, or press F5.
    A blank page is displayed.
6.  To stop debugging and return to Visual Studio, exit the app, or click **Stop Debugging** from the menu.

## 2. Add basic pages

Next, add two pages to the project.

1.  In **Solution Explorer**, right-click the **BlankApp** project node to open the shortcut menu.
2.  Choose **Add** > **New Item** from the shortcut menu.
3.  In the **Add New Item** dialog box, choose **Blank Page** in the middle pane.
4.  In the **Name** box, enter **Page1** (or **Page2**) and press the **Add** button.
5. Repeat steps 1-4 to add the second page.

Now, these files should be listed as part of your NavApp1 project.

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

 

In Page1.xaml, add the following content:

-   A [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) element named `pageTitle` as a child element of the root [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704). Change the [**Text**](https://msdn.microsoft.com/library/windows/apps/br209676) property to `Page 1`.
```xaml
<TextBlock x:Name="pageTitle" Text="Page 1" />
```

-   A [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) element as a child element of the root [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) and after the `pageTitle` [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) element.
```xaml
<HyperlinkButton Content="Click to go to page 2"
                 Click="HyperlinkButton_Click"
                 HorizontalAlignment="Center"/>
```

In the Page1.xaml code-behind file, add the following code to handle the `Click` event of the [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) you added to navigate to Page2.xaml.

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

In Page2.xaml, add the following content:

-   A [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) element named `pageTitle` as a child element of the root [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704). Change the value of the [**Text**](https://msdn.microsoft.com/library/windows/apps/br209676) property to `Page 2`.
```xaml
<TextBlock x:Name="pageTitle" Text="Page 2" />
```

-   A [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) element as a child element of the root [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) and after the `pageTitle` [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) element.
```xaml
<HyperlinkButton Content="Click to go to page 1" 
                 Click="HyperlinkButton_Click"
                 HorizontalAlignment="Center"/>
```

In the Page2.xaml code-behind file, add the following code to handle the `Click` event of the [**HyperlinkButton**](https://msdn.microsoft.com/library/windows/apps/br242739) to navigate to Page1.xaml.

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
> [!NOTE]
> For C++ projects, you must add a `#include` directive in the header file of each page that references another page. For the inter-page navigation example presented here, page1.xaml.h file contains `#include "Page2.xaml.h"`, in turn, page2.xaml.h contains `#include "Page1.xaml.h"`.

Now that we've prepared the pages, we need to make Page1.xaml display when the app starts.

Open the App.xaml code-behind file and change the `OnLaunched` handler.

Here, we specify `Page1` in the call to [**Frame.Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694) instead of `MainPage`.


```csharp
protected override void OnLaunched(LaunchActivatedEventArgs e)
{
    Frame rootFrame = Window.Current.Content as Frame;
 
    // Do not repeat app initialization when the Window already has content,
    // just ensure that the window is active
    if (rootFrame == null)
    {
        // Create a Frame to act as the navigation context and navigate to the first page
        rootFrame = new Frame();
        rootFrame.NavigationFailed += OnNavigationFailed;
 
        if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
            //TODO: Load state from previously suspended application
        }
 
        // Place the frame in the current Window
        Window.Current.Content = rootFrame;
    }
 
    if (rootFrame.Content == null)
    {
        // When the navigation stack isn't restored navigate to the first page,
        // configuring the new page by passing required information as a navigation
        // parameter
        rootFrame.Navigate(typeof(Page1), e.Arguments);
    }
    // Ensure the current window is active
    Window.Current.Activate();
}
```
```cpp
void App::OnLaunched(Windows::ApplicationModel::Activation::LaunchActivatedEventArgs^ e)
{
    auto rootFrame = dynamic_cast<Frame^>(Window::Current->Content);

    // Do not repeat app initialization when the Window already has content,
    // just ensure that the window is active
    if (rootFrame == nullptr)
    {
        // Create a Frame to act as the navigation context and associate it with
        // a SuspensionManager key
        rootFrame = ref new Frame();

        rootFrame->NavigationFailed += 
            ref new Windows::UI::Xaml::Navigation::NavigationFailedEventHandler(
                this, &App::OnNavigationFailed);

        if (e->PreviousExecutionState == ApplicationExecutionState::Terminated)
        {
            // TODO: Load state from previously suspended application
        }
        
        // Place the frame in the current Window
        Window::Current->Content = rootFrame;
    }

    if (rootFrame->Content == nullptr)
    {
        // When the navigation stack isn't restored navigate to the first page,
        // configuring the new page by passing required information as a navigation
        // parameter
        rootFrame->Navigate(Windows::UI::Xaml::Interop::TypeName(Page1::typeid), e->Arguments);
    }

    // Ensure the current window is active
    Window::Current->Activate();
}
```

**Note**:  The code here uses the return value of [**Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694) to throw an app exception if the navigation to the app's initial window frame fails. When **Navigate** returns **true**, the navigation happens.

Now, build and run the app. Click the link that says "Click to go to page 2". The second page that says "Page 2" at the top should be loaded and displayed in the frame.

### About the Frame and Page classes

Before we add more functionality to our app, let's look at how the pages we added provide navigation within our app.

First, a [**Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) called `rootFrame` is created for the app in the `App.OnLaunched` method in the App.xaml code-behind file. The **Frame** class supports various navigation methods such as [**Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694), [**GoBack**](https://msdn.microsoft.com/library/windows/apps/dn996568), and [**GoForward**](https://msdn.microsoft.com/library/windows/apps/br242693), and properties such as [**BackStack**](https://msdn.microsoft.com/library/windows/apps/dn279543), [**ForwardStack**](https://msdn.microsoft.com/library/windows/apps/dn279547), and [**BackStackDepth**](https://msdn.microsoft.com/library/windows/apps/hh967995).

 
The [**Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694) method is used to display content in this **Frame**. By default, this method loads MainPage.xaml. In our example, `Page1` is passed to the **Navigate** method, so the method loads `Page1` in the **Frame**. 

`Page1` is a subclass of the [**Page**](https://msdn.microsoft.com/library/windows/apps/br227503) class. The **Page** class has a read-only **Frame** property that gets the **Frame** containing the **Page**. When the **Click** event handler of the **HyperlinkButton** in `Page1` calls `this.Frame.Navigate(typeof(Page2))`, the **Frame** displays the content of Page2.xaml.

Finally, whenever a page is loaded into the frame, that page is added as a [**PageStackEntry**](https://msdn.microsoft.com/library/windows/apps/dn298572) to the [**BackStack**](https://msdn.microsoft.com/library/windows/apps/dn279543) or [**ForwardStack**](https://msdn.microsoft.com/library/windows/apps/dn279547) of the [**Frame**](https://msdn.microsoft.com/library/windows/apps/br227504), allowing for [history and backwards navigation](navigation-history-and-backwards-navigation.md).

## 3. Pass information between pages

Our app navigates between two pages, but it really doesn't do anything interesting yet. Often, when an app has multiple pages, the pages need to share information. Let's pass some information from the first page to the second page.

In Page1.xaml, replace the the **HyperlinkButton** you added earlier with the following [**StackPanel**](https://msdn.microsoft.com/library/windows/apps/br209635).

Here, we add a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) label and a [**TextBox**](https://msdn.microsoft.com/library/windows/apps/br209683) `name` for entering a text string.

```xaml
<StackPanel>
    <TextBlock HorizontalAlignment="Center" Text="Enter your name"/>
    <TextBox HorizontalAlignment="Center" Width="200" Name="name"/>
    <HyperlinkButton Content="Click to go to page 2" 
                     Click="HyperlinkButton_Click"
                     HorizontalAlignment="Center"/>
</StackPanel>
```

In the `HyperlinkButton_Click` event handler of the Page1.xaml code-behind file, add a parameter referencing the `Text` property of the `name` **TextBox** to the `Navigate` method.

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

In Page2.xaml, replace the **HyperlinkButton** you added earlier with the following **StackPanel**.

Here, we add a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) for displaying the text string passed from Page1.

```xaml
<StackPanel>
    <TextBlock HorizontalAlignment="Center" Name="greeting"/>
    <HyperlinkButton Content="Click to go to page 1" 
                     Click="HyperlinkButton_Click"
                     HorizontalAlignment="Center"/>
</StackPanel>
```

In the Page2.xaml code-behind file, add the following to override the `OnNavigatedTo` method:

```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
    {
        greeting.Text = $"Hi, {e.Parameter.ToString()}";
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

Run the app, type your name in the text box, and then click the link that says **Click to go to page 2**. 

When the **Click** event of the **HyperlinkButton** in `Page1` calls `this.Frame.Navigate(typeof(Page2), name.Text)`, the `name.Text` property is passed to `Page2`, and the value from the event data is used for the message displayed on the page.

## 4. Cache a page

Page content and state is not cached by default, so if you'd like to cache information, you must enable it in each page of your app.

In our basic peer-to-peer example, there is no back button (we demonstrate back navigation in [backwards navigation](navigation-history-and-backwards-navigation.md)), but if you did click a back button on `Page2`, the **TextBox** (and any other field) on `Page1` would be set to its default state. One way to work around this is to use the [**NavigationCacheMode**](https://msdn.microsoft.com/library/windows/apps/br227506) property to specify that a page be added to the frame's page cache. 

In the constructor of `Page1`, you can set **NavigationCacheMode** to **Enabled** to retains all content and state values for the page until the page cache for the frame is exceeded. Set [**NavigationCacheMode**](https://msdn.microsoft.com/library/windows/apps/br227506) to [**Required**](https://msdn.microsoft.com/library/windows/apps/br243284) if you want to ignore [**CacheSize**](https://msdn.microsoft.com/library/windows/apps/br242683) limits, which specify the number of pages in the navigation history that can be cached for the frame. However, keep in mind that cache size limits might be crucial, depending on the memory limits of a device.

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
 

 




