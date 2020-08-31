---
ms.assetid: E2B73380-D673-48C6-9026-96976D745017
title: Getting started with Common Controls
description: View a list of links to topics about common Universal Windows Platform (UWP) controls and their equivalent iOS controls.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Getting started: Common Controls


## Common controls list

In the previous section, you worked with only two controls: buttons and textblocks. There are, of course, many more controls that are available to you. Here are some common controls you'll be using in your apps, and their iOS equivalents. The iOS controls are listed in alphabetical order, next to the most similar Universal Windows Platform (UWP) controls.

The rather clever thing about UWP controls is that they can sense the type of device they are running on, and change their appearance and functionality accordingly. For example, if your project uses the [**DatePicker**](/previous-versions/windows/apps/br211681(v=win.10)) control, it is smart enough to optimize itself to look and behave differently on a desktop computer compared to, say, a phone. You don't need to do anything: the controls adjust themselves at run-time.

| iOS control (class/protocol) | Equivalent UWP control |
|------------------------------|--------------------------------------|
| Activity indicator (**UIActivityIndicatorView**) | [**ProgressRing**](/uwp/api/Windows.UI.Xaml.Controls.ProgressRing) <br/> See also [Quickstart: adding progress controls](/previous-versions/windows/apps/hh780651(v=win.10)) |
| Ad banner view (**ADBannerView**) and ad banner view delegate (**ADBannerViewDelegate**) | [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) <br/> See also [Display ads in your app](../monetize/display-ads-in-your-app.md) |
| Button (UIButton) | [Button](/uwp/api/Windows.UI.Xaml.Controls.Button) <br/> See also [Quickstart: Adding button controls](/previous-versions/windows/apps/jj153346(v=win.10)) |
| Date picker (UIDatePicker) | [DatePicker](/previous-versions/windows/apps/br211681(v=win.10)) |
| Image view (UIImageView) | [Image](/uwp/api/Windows.UI.Xaml.Controls.Image) <br/> See also [Image and ImageBrush](../design/controls-and-patterns/images-imagebrushes.md) |
| Label (UILabel) | [TextBlock](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) <br/> See also [Quickstart: displaying text](/previous-versions/windows/apps/hh700392(v=win.10)) |
| Map view (MKMapView) and map view delegate (MKMapViewDelegate) | See [Bing Maps for UWP apps](/previous-versions/windows/apps/dn642089(v=win.10)) |
| Navigation controller (UINavigationController) and navigation controller delegate (UINavigationControllerDelegate) | [Frame](/uwp/api/Windows.UI.Xaml.Controls.Frame) <br/> See also [Navigation](../design/basics/navigation-basics.md) |
| Page control (UIPageControl) | [Page](/uwp/api/Windows.UI.Xaml.Controls.Page) <br/> See also [Navigation](../design/basics/navigation-basics.md) |
| Picker view (UIPickerView) and picker view delegate (UIPickerViewDelegate) | [ComboBox](/uwp/api/Windows.UI.Xaml.Controls.ComboBox) <br/> See also [Adding combo boxes and list boxes](/previous-versions/windows/apps/hh780616(v=win.10)) |
| Progress bar (UIProgressView) | [ProgressBar](/uwp/api/Windows.UI.Xaml.Controls.ProgressBar) <br/> See also [Quickstart: adding progress controls](/previous-versions/windows/apps/hh780651(v=win.10)) |
| Scroll view (UIScrollView) and scroll view delegate (UIScrollViewDelegate) | [ScrollViewer](/uwp/api/Windows.UI.Xaml.Controls.ScrollViewer) <br/>  See also [Extensible Application Markup Language (XAML) scrolling, panning, and zooming sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/XAML%20scrolling%2C%20panning%2C%20and%20zooming%20sample%20(Windows%208)) |
| Search bar (UISearchBar) and search bar delegate (UISearchBarDelegate) | See [Adding search to an app](/previous-versions/windows/apps/jj130767(v=win.10)) <br/>  See also [Quickstart: Adding search to an app](/previous-versions/windows/apps/hh868180(v=win.10)) |
| Segmented control (UISegmentedControl) | None |
| Slider (UISlider) | [Slider](/uwp/api/Windows.UI.Xaml.Controls.Slider) <br/>  See also [How to add a slider](/previous-versions/windows/apps/hh868197(v=win.10)) |
| Split view controller (UISplitViewController) and split view controller delegate (UISplitViewControllerDelegate) | None |
| Switch (UISwitch) | [ToggleSwitch](/uwp/api/Windows.UI.Xaml.Controls.ToggleSwitch) <br/>  See also [How to add a toggle switch](/previous-versions/windows/apps/hh868198(v=win.10)) |
| Tab bar controller (UITabBarController) and tab bar controller delegate (UITabBarControllerDelegate) | None |
| Table view controller (UITableViewController), table view (UITableView), table view delegate (UITableViewDelegate), and table cell (UITableViewCell) | [ListView](/uwp/api/Windows.UI.Xaml.Controls.ListView) <br/>  See also [Quickstart: adding ListView and GridView controls](/previous-versions/windows/apps/hh780650(v=win.10)) |
| Text field (UITextField) and text field delegate (UITextFieldDelegate) | [TextBox](/uwp/api/Windows.UI.Xaml.Controls.TextBox) <br/>  See also [Display and edit text](../design/controls-and-patterns/text-controls.md) |
| Text view (UITextView) and text view delegate (UITextViewDelegate) | [TextBlock](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) <br/>  See also [Quickstart: displaying text](/previous-versions/windows/apps/hh700392(v=win.10)) |
| View (UIView) and view controller (UIViewController) | [Page](/uwp/api/Windows.UI.Xaml.Controls.Page) <br/>  See also [Navigation](../design/basics/navigation-basics.md) |
| Web view (UIWebView) and web view delegate (UIWebViewDelegate) | [WebView](/uwp/api/Windows.UI.Xaml.Controls.WebView) <br/>  See also [XAML WebView control sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/XAML%20WebView%20control%20sample%20(Windows%208)) |
| Window (UIWindow) | [Frame](/uwp/api/Windows.UI.Xaml.Controls.Frame) <br/>  See also [Navigation](../design/basics/navigation-basics.md) |

For even more controls, see [Controls list](../design/controls-and-patterns/index.md).

**Note**  For a list of controls for UWP apps using JavaScript and HTML, see [Controls list](/previous-versions/windows/apps/hh465453(v=win.10)).

### Next step

[Getting Started: Navigation](getting-started-navigation.md)

## Related topics

* [build 2014: What about XAML UI and Controls?](https://channel9.msdn.com/Events/Build/2014/2-516)
* [build 2014: Developing Apps using the Common XAML UI Framework](https://channel9.msdn.com/Events/Build/2014/2-507)
* [build 2014: Using Visual Studio to Build XAML Converged Apps](https://channel9.msdn.com/Events/Build/2014/3-591)