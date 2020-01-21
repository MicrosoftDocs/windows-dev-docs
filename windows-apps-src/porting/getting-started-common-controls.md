---
ms.assetid: E2B73380-D673-48C6-9026-96976D745017
description: Getting started with common controls
title: Getting started with Common Controls
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Getting started: Common Controls


## Common controls list

In the previous section, you worked with only two controls: buttons and textblocks. There are, of course, many more controls that are available to you. Here are some common controls you'll be using in your apps, and their iOS equivalents. The iOS controls are listed in alphabetical order, next to the most similar Universal Windows Platform (UWP) controls.

The rather clever thing about UWP controls is that they can sense the type of device they are running on, and change their appearance and functionality accordingly. For example, if your project uses the [**DatePicker**](https://docs.microsoft.com/previous-versions/windows/apps/br211681(v=win.10)) control, it is smart enough to optimize itself to look and behave differently on a desktop computer compared to, say, a phone. You don't need to do anything: the controls adjust themselves at run-time.

| iOS control (class/protocol) | Equivalent UWP control |
|------------------------------|--------------------------------------|
| Activity indicator (**UIActivityIndicatorView**) | [**ProgressRing**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ProgressRing) <br/> See also [Quickstart: adding progress controls](https://docs.microsoft.com/previous-versions/windows/apps/hh780651(v=win.10)) |
| Ad banner view (**ADBannerView**) and ad banner view delegate (**ADBannerViewDelegate**) | [AdControl](https://docs.microsoft.com/uwp/api/microsoft.advertising.winrt.ui.adcontrol) <br/> See also [Display ads in your app](../monetize/display-ads-in-your-app.md) |
| Button (UIButton) | [Button](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Button) <br/> See also [Quickstart: Adding button controls](https://docs.microsoft.com/previous-versions/windows/apps/jj153346(v=win.10)) |
| Date picker (UIDatePicker) | [DatePicker](https://docs.microsoft.com/previous-versions/windows/apps/br211681(v=win.10)) |
| Image view (UIImageView) | [Image](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Image) <br/> See also [Image and ImageBrush](https://docs.microsoft.com/windows/uwp/controls-and-patterns/images-imagebrushes) |
| Label (UILabel) | [TextBlock](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.TextBlock) <br/> See also [Quickstart: displaying text](https://docs.microsoft.com/previous-versions/windows/apps/hh700392(v=win.10)) |
| Map view (MKMapView) and map view delegate (MKMapViewDelegate) | See [Bing Maps for UWP apps](https://msdn.microsoft.com/library/hh846481) |
| Navigation controller (UINavigationController) and navigation controller delegate (UINavigationControllerDelegate) | [Frame](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Frame) <br/> See also [Navigation](https://docs.microsoft.com/windows/uwp/layout/navigation-basics) |
| Page control (UIPageControl) | [Page](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Page) <br/> See also [Navigation](https://docs.microsoft.com/windows/uwp/layout/navigation-basics) |
| Picker view (UIPickerView) and picker view delegate (UIPickerViewDelegate) | [ComboBox](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ComboBox) <br/> See also [Adding combo boxes and list boxes](https://docs.microsoft.com/previous-versions/windows/apps/hh780616(v=win.10)) |
| Progress bar (UIProgressView) | [ProgressBar](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ProgressBar) <br/> See also [Quickstart: adding progress controls](https://docs.microsoft.com/previous-versions/windows/apps/hh780651(v=win.10)) |
| Scroll view (UIScrollView) and scroll view delegate (UIScrollViewDelegate) | [ScrollViewer](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ScrollViewer) <br/>  See also [Extensible Application Markup Language (XAML) scrolling, panning, and zooming sample](https://code.msdn.microsoft.com/windowsapps/xaml-scrollviewer-pan-and-949d29e9) |
| Search bar (UISearchBar) and search bar delegate (UISearchBarDelegate) | See [Adding search to an app](https://docs.microsoft.com/previous-versions/windows/apps/jj130767(v=win.10)) <br/>  See also [Quickstart: Adding search to an app](https://docs.microsoft.com/previous-versions/windows/apps/hh868180(v=win.10)) |
| Segmented control (UISegmentedControl) | None |
| Slider (UISlider) | [Slider](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Slider) <br/>  See also [How to add a slider](https://docs.microsoft.com/previous-versions/windows/apps/hh868197(v=win.10)) |
| Split view controller (UISplitViewController) and split view controller delegate (UISplitViewControllerDelegate) | None |
| Switch (UISwitch) | [ToggleSwitch](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ToggleSwitch) <br/>  See also [How to add a toggle switch](https://docs.microsoft.com/previous-versions/windows/apps/hh868198(v=win.10)) |
| Tab bar controller (UITabBarController) and tab bar controller delegate (UITabBarControllerDelegate) | None |
| Table view controller (UITableViewController), table view (UITableView), table view delegate (UITableViewDelegate), and table cell (UITableViewCell) | [ListView](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ListView) <br/>  See also [Quickstart: adding ListView and GridView controls](https://docs.microsoft.com/previous-versions/windows/apps/hh780650(v=win.10)) |
| Text field (UITextField) and text field delegate (UITextFieldDelegate) | [TextBox](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.TextBox) <br/>  See also [Display and edit text](https://docs.microsoft.com/windows/uwp/design/controls-and-patterns/text-controls) |
| Text view (UITextView) and text view delegate (UITextViewDelegate) | [TextBlock](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.TextBlock) <br/>  See also [Quickstart: displaying text](https://docs.microsoft.com/previous-versions/windows/apps/hh700392(v=win.10)) |
| View (UIView) and view controller (UIViewController) | [Page](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Page) <br/>  See also [Navigation](https://docs.microsoft.com/windows/uwp/layout/navigation-basics) |
| Web view (UIWebView) and web view delegate (UIWebViewDelegate) | [WebView](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.WebView) <br/>  See also [XAML WebView control sample](https://code.msdn.microsoft.com/windowsapps/XAML-WebView-control-sample-58ad63f7) |
| Window (UIWindow) | [Frame](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Frame) <br/>  See also [Navigation](https://docs.microsoft.com/windows/uwp/layout/navigation-basics) |

For even more controls, see [Controls list](https://docs.microsoft.com/windows/uwp/design/controls-and-patterns/).

**Note**  For a list of controls for UWP apps using JavaScript and HTML, see [Controls list](https://docs.microsoft.com/previous-versions/windows/apps/hh465453(v=win.10)).

### Next step

[Getting Started: Navigation](getting-started-navigation.md)

## Related topics

* [build 2014: What about XAML UI and Controls?](https://channel9.msdn.com/Events/Build/2014/2-516)
* [build 2014: Developing Apps using the Common XAML UI Framework](https://channel9.msdn.com/Events/Build/2014/2-507)
* [build 2014: Using Visual Studio to Build XAML Converged Apps](https://channel9.msdn.com/Events/Build/2014/3-591)
