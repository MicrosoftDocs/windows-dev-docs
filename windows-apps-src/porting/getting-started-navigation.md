---
title: Getting started with Navigation
description: Getting started with navigation
ms.assetid: F4DF5C5F-C886-4483-BBDA-498C4E2C1BAF
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Getting started: Navigation


## Adding navigation

iOS provides the **UINavigationController** class to help with in-app navigation: you can push and pop views to create the hierarchy of **UIViewControllers** that define your app.

In contrast, a Windows 10 app containing multiple views takes more of a web-site approach to navigation. You can imagine your users hopping from page to page as they click on controls to work their way through the app. For more info, see [Navigation design basics](https://msdn.microsoft.com/library/windows/apps/dn958438).

One of the ways to manage this navigation in a Windows 10 app is to use the [**Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) class. The following walkthrough shows you how to try this out.

Continuing with the solution you started earlier, open the **MainPage.xaml** file, and add a button in the **Design** view. Change the button's **Content** property from "Button" to "Go To Page". Then, create a handler for the button's **Click** event, as shown in the following figure. If you don't remember how to do this, review the walkthrough in the previous section (Hint: double-click the button in the **Design** view).

![adding a button and its click event in visual studio](images/ios-to-uwp/vs-go-to-page.png)

Let's add a new page. In the **Solution** view, tap the **Project** menu, and tap **Add New Item**. Tap **Blank Page** as shown in the following figure, and then tap **Add**.

![adding a new page in visual studio](images/ios-to-uwp/vs-add-new-page.png)

Next, add a button to the BlankPage.xaml file. Let's use the AppBarButton control, and let's give it a back arrow image: in the **XAML** view, add ` <AppBarButton Icon="Back"/>` between the `<Grid> </Grid>` elements.

Now, let's add an event handler to the button: double-click the control in the **Design** view and Microsoft Visual Studio adds the text "AppBarButton\_Click" to the **Click** box, as shown in the following figure, and then adds and displays the corresponding event handler in the BlankPage.xaml.cs file.

![adding a back button and its click event in visual studio](images/ios-to-uwp/vs-add-back-button.png)

If you return to the BlankPage.xaml file's **XAML** view, the `<AppBarButton>` element's Extensible Application Markup Language (XAML) code should now look like this:

` <AppBarButton Icon="Back" Click="AppBarButton_Click"/>`

Return to the BlankPage.xaml.cs file, and add this code to go to the previous page after the user taps the button.

```csharp
private void AppBarButton_Click(object sender, RoutedEventArgs e)
{
    // Add the following line of code.    
    Frame.GoBack();
}
```

Finally, open the MainPage.xaml.cs file and add this code. It opens BlankPage after the user taps the button.

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    // Add the following line of code.
    Frame.Navigate(typeof(BlankPage1));
}
```

Now, run the program. Tap the "Go To Page" button to go to the other page, and then tap the back-arrow button to return to the previous page.

Page navigation is managed by the [**Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) class. As the **UINavigationController** class in iOS uses **pushViewController** and **popViewController** methods, the **Frame** class for UWP apps provides [**Navigate**](https://msdn.microsoft.com/library/windows/apps/br242694) and [**GoBack**](https://msdn.microsoft.com/library/windows/apps/dn996568) methods. The **Frame** class also has a method called [**GoForward**](https://msdn.microsoft.com/library/windows/apps/br242693), which does what you might expect.

This walkthrough creates a new instance of BlankPage each time you navigate to it. (The previous instance will be freed, or *released*, automatically). If you don't want a new instance to be created each time, add the following code to the BlankPage class's constructor in the BlankPage.xaml.cs file. This will enable the [**NavigationCacheMode**](https://msdn.microsoft.com/library/windows/apps/br227506) behavior.

```csharp
public BlankPage()
{
    this.InitializeComponent();
    // Add the following line of code.
    this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
}
```

You can also get or set the **Frame** class's [**CacheSize**](https://msdn.microsoft.com/library/windows/apps/br242683) property to manage how many pages in the navigation history can be cached.

For more info about navigation, see [Navigation](https://msdn.microsoft.com/library/windows/apps/mt187344) and [XAML personality animations sample](http://go.microsoft.com/fwlink/p/?LinkID=242401).

**Note**  For info about navigation for UWP apps using JavaScript and HTML, see [Quickstart: Using single-page navigation](https://msdn.microsoft.com/library/windows/apps/hh452768).
 
### Next step

[Getting started: Animation](getting-started-animation.md)

