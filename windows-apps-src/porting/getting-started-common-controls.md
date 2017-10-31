---
author: mcleblanc
ms.assetid: E2B73380-D673-48C6-9026-96976D745017
description: Getting started with common controls
title: Getting started with Common Controls
ms.author: markl
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# Getting started: Common Controls


## Common controls list

In the previous section, you worked with only two controls: buttons and textblocks. There are, of course, many many more controls that are available to you. Here are some common controls you'll be using in your apps, and their iOS equivalents. The iOS controls are listed in alphabetical order, next to the most similar Universal Windows Platform (UWP) controls.

The rather clever thing about UWP controls is that they can sense the type of device they are running on, and change their appearance and functionality accordingly. For example, if your project uses the [**DatePicker**](https://msdn.microsoft.com/library/windows/apps/br211681) control, it is smart enough to optimize itself to look and behave differently on a desktop computer compared to, say, a phone. You don't need to do anything: the controls adjust themselves at run-time.

| iOS control (class/protocol) | Equivalent UWP control |
|------------------------------|--------------------------------------|
| Activity indicator (**UIActivityIndicatorView**) | [**ProgressRing**](https://msdn.microsoft.com/library/windows/apps/br227538) <br/> See also [Quickstart: adding progress controls](https://msdn.microsoft.com/library/windows/apps/xaml/hh780651) |
| Ad banner view (**ADBannerView**) and ad banner view delegate (**ADBannerViewDelegate**) | [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) <br/> See also [Display ads in your app](../monetize/display-ads-in-your-app.md) |
| Button (UIButton) | [Button](https://msdn.microsoft.com/library/windows/apps/br209265) <br/> See also [Quickstart: Adding button controls](https://msdn.microsoft.com/library/windows/apps/xaml/jj153346) |
| Date picker (UIDatePicker) | [DatePicker](https://msdn.microsoft.com/library/windows/apps/br211681) |
| Image view (UIImageView) | [Image](https://msdn.microsoft.com/library/windows/apps/br242752) <br/> See also [Image and ImageBrush](https://msdn.microsoft.com/library/windows/apps/mt280382) |
| Label (UILabel) | [TextBlock](https://msdn.microsoft.com/library/windows/apps/br209652) <br/> See also [Quickstart: displaying text](https://msdn.microsoft.com/library/windows/apps/xaml/hh700392) |
| Map view (MKMapView) and map view delegate (MKMapViewDelegate) | See [Bing Maps for UWP apps](http://go.microsoft.com/fwlink/p/?LinkId=263496) |
| Navigation controller (UINavigationController) and navigation controller delegate (UINavigationControllerDelegate) | [Frame](https://msdn.microsoft.com/library/windows/apps/br242682) <br/> See also [Navigation](https://msdn.microsoft.com/library/windows/apps/mt187344) |
| Page control (UIPageControl) | [Page](https://msdn.microsoft.com/library/windows/apps/br227503) <br/> See also [Navigation](https://msdn.microsoft.com/library/windows/apps/mt187344) |
| Picker view (UIPickerView) and picker view delegate (UIPickerViewDelegate) | [ComboBox](https://msdn.microsoft.com/library/windows/apps/br209348) <br/> See also [Adding combo boxes and list boxes](https://msdn.microsoft.com/library/windows/apps/xaml/hh780616) |
| Progress bar (UIProgressView) | [ProgressBar](https://msdn.microsoft.com/library/windows/apps/br227529) <br/> See also [Quickstart: adding progress controls](https://msdn.microsoft.com/library/windows/apps/xaml/hh780651) |
| Scroll view (UIScrollView) and scroll view delegate (UIScrollViewDelegate) | [ScrollViewer](https://msdn.microsoft.com/library/windows/apps/br209527) <br/>  See also [Extensible Application Markup Language (XAML) scrolling, panning, and zooming sample](http://go.microsoft.com/fwlink/p/?LinkId=238577) |
| Search bar (UISearchBar) and search bar delegate (UISearchBarDelegate) | See [Adding search to an app](https://msdn.microsoft.com/library/windows/apps/xaml/jj130767) <br/>  See also [Quickstart: Adding search to an app](https://msdn.microsoft.com/library/windows/apps/xaml/hh868180) |
| Segmented control (UISegmentedControl) | None |
| Slider (UISlider) | [Slider](https://msdn.microsoft.com/library/windows/apps/br209614) <br/>  See also [How to add a slider](https://msdn.microsoft.com/library/windows/apps/xaml/hh868197) |
| Split view controller (UISplitViewController) and split view controller delegate (UISplitViewControllerDelegate) | None |
| Switch (UISwitch) | [ToggleSwitch](https://msdn.microsoft.com/library/windows/apps/br209712) <br/>  See also [How to add a toggle switch](https://msdn.microsoft.com/library/windows/apps/xaml/hh868198) |
| Tab bar controller (UITabBarController) and tab bar controller delegate (UITabBarControllerDelegate) | None |
| Table view controller (UITableViewController), table view (UITableView), table view delegate (UITableViewDelegate), and table cell (UITableViewCell) | [ListView](https://msdn.microsoft.com/library/windows/apps/br242878) <br/>  See also [Quickstart: adding ListView and GridView controls](https://msdn.microsoft.com/library/windows/apps/xaml/hh780650) |
| Text field (UITextField) and text field delegate (UITextFieldDelegate) | [TextBox](https://msdn.microsoft.com/library/windows/apps/br209683) <br/>  See also [Display and edit text](https://msdn.microsoft.com/library/windows/apps/mt280218) |
| Text view (UITextView) and text view delegate (UITextViewDelegate) | [TextBlock](https://msdn.microsoft.com/library/windows/apps/br209652) <br/>  See also [Quickstart: displaying text](https://msdn.microsoft.com/library/windows/apps/xaml/hh700392) |
| View (UIView) and view controller (UIViewController) | [Page](https://msdn.microsoft.com/library/windows/apps/br227503) <br/>  See also [Navigation](https://msdn.microsoft.com/library/windows/apps/mt187344) |
| Web view (UIWebView) and web view delegate (UIWebViewDelegate) | [WebView](https://msdn.microsoft.com/library/windows/apps/br227702) <br/>  See also [XAML WebView control sample](http://go.microsoft.com/fwlink/p/?LinkId=238582) |
| Window (UIWindow) | [Frame](https://msdn.microsoft.com/library/windows/apps/br242682) <br/>  See also [Navigation](https://msdn.microsoft.com/library/windows/apps/mt187344) |

For even more controls, see [Controls list](https://msdn.microsoft.com/library/windows/apps/mt185406).

**Note**  For a list of controls for UWP apps using JavaScript and HTML, see [Controls list](https://msdn.microsoft.com/library/windows/apps/hh465453).

### Next step

[Getting Started: Navigation](getting-started-navigation.md)

## Related topics

* [build 2014: What about XAML UI and Controls?](http://go.microsoft.com/fwlink/p/?LinkID=397897)
* [build 2014: Developing Apps using the Common XAML UI Framework](http://go.microsoft.com/fwlink/p/?LinkID=397898)
* [build 2014: Using Visual Studio to Build XAML Converged Apps](http://go.microsoft.com/fwlink/p/?LinkID=397876)
