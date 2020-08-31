---
Description: A progress control provides feedback to the user that a long-running operation is underway.
title: Guidelines for progress controls
ms.assetid: FD53B716-C43D-408D-8B07-522BC1F3DF9D
label: Progress controls
template: detail.hbs
ms.date: 11/29/2019
ms.topic: article
keywords: windows 10, uwp
pm-contact: kisai
design-contact: jeffarn
dev-contact: mitra
doc-status: Published
ms.localizationpriority: medium
---
# Progress controls

A progress control provides feedback to the user that a long-running operation is underway. It can mean that the user cannot interact with the app when the progress indicator is visible, and can also indicate how long the wait time might be, depending on the indicator used.

**Get the Windows UI Library**

|  |  |
| - | - |
| ![WinUI logo](images/winui-logo-64x64.png) | The **ProgressBar** control is included as part of the Windows UI Library, a NuGet package that contains new controls and UI features for Windows apps. For more info, including installation instructions, see [Windows UI Library](/uwp/toolkits/winui/). |

> **Windows UI Library APIs:** [ProgressBar class](/uwp/api/Microsoft.UI.Xaml.Controls.ProgressBar), [IsIndeterminate property](/uwp/api/Microsoft.ui.xaml.controls.progressbar.isindeterminate), [ProgressRing class](/uwp/api/Microsoft.UI.Xaml.Controls.ProgressRing), [IsActive property](/uwp/api/Microsoft.ui.xaml.controls.progressring.isactive)
>
> **Platform APIs:** [ProgressBar class](/uwp/api/Windows.UI.Xaml.Controls.ProgressBar), [IsIndeterminate property](/uwp/api/windows.ui.xaml.controls.progressbar.isindeterminate), [ProgressRing class](/uwp/api/Windows.UI.Xaml.Controls.ProgressRing), [IsActive property](/uwp/api/windows.ui.xaml.controls.progressring.isactive)

> [!NOTE]
> There are two versions of the ProgressBar and ProgressRing controls: one in the platform, represented by the Windows.UI.Xaml namespace; the other in the Windows UI Library, the Microsoft.UI.Xaml namespace. Although the APIs for ProgressRing and ProgressBar are the same, the control's appearances differ between the two versions. This document will show images of the newer Windows UI Library version.
Throughout this document, we will use the **muxc** alias in XAML to represent the Windows UI Library APIs that we have included in our project. We have added this to our [Page](/uwp/api/windows.ui.xaml.controls.page) element:

```xaml
xmlns:muxc="using:Microsoft.UI.Xaml.Controls"
```

In the code-behind, we will also use the **muxc** alias in C# to represent the Windows UI Library APIs that we have included in our project. We have added this **using** statement at the top of the file:

```csharp
using muxc = Microsoft.UI.Xaml.Controls;
```

```vb
Imports muxc = Microsoft.UI.Xaml.Controls
```

## Types of progress

There are two controls to show the user that an operation is underway – either through a ProgressBar or through a ProgressRing.

-   The ProgressBar *determinate* state shows the percentage completed of a task. This should be used during an operation whose duration is known, but its progress should not block the user's interaction with the app.
-   The ProgressBar *indeterminate* state shows that an operation is underway, does not block user interaction with the app, and its completion time is unknown.
-   The ProgressRing only has an *indeterminate* state, and should be used when any further user interaction is blocked until the operation has completed.

Additionally, a progress control is read only and not interactive. Meaning that the user cannot invoke or use these controls directly.

![ProgressBar states](images/progress-bar-two-states.png)

*Top to bottom - Indeterminate ProgressBar and a determinate ProgressBar*

![ProgressRing state](images/ProgressRing_SingleState.png)

*An indeterminate ProgressRing*

## Examples

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to open the app and see the <a href="xamlcontrolsgallery:/item/ProgressBar">ProgressBar</a> or <a href="xamlcontrolsgallery:/item/ProgressRing">ProgressRing</a> in action.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## When to use each control

It's not always obvious what control or what state (determinate vs indeterminate) to use when trying to show something is happening. Sometimes a task is obvious enough that it doesn't require a progress control at all – and sometimes even if a progress control is used, a line of text is still necessary in order to explain to the user what operation is underway.

### ProgressBar
-   **Does the control have a defined duration or predictable end?**

    Use a determinate ProgressBar then, and update the percentage or value accordingly.

-   **Can the user continue without having to monitor the operation's progress?**

    When a ProgressBar is in use, interaction is non-modal, typically meaning that the user is not blocked by that operation's completion, and can continue to use the app in other ways until that aspect has completed.

-   **Keywords**

    If your operation falls around these keywords, or if you're showing text that alongside the progress operation that matches these keywords; consider using a ProgressBar:

    - *Loading...*
    - *Retrieving*
    - *Working...*

### ProgressRing

-   **Will the operation cause the user to wait to continue?**

    If an operation requires all (or a large portion of) interaction with the app to wait until it has been completed, then the ProgressRing is the better choice. The ProgressRing control is used for modal interactions, meaning that the user is blocked until the ProgressRing disappears.

-   **Is the app waiting for the user to complete a task?**

    If so, use a ProgressRing, as they're meant to indicate an unknown wait time for the user.

-   **Keywords**

    If your operation falls around these keywords, or if you're showing text alongside the progress operation that matches these keywords; consider using a ProgressRing:

    - *Refreshing*
    - *Signing in...*
    - *Connecting...*

### No progress indication necessary
-   **Does the user need to know that something is happening?**

    For example, if the app is downloading something in the background and the user didn't initiate the download, the user doesn't necessarily need to know about it.

-   **Is the operation a background activity that doesn't block user activity and is of minimal (but still some) interest to the user?**

    Use text when your app is performing tasks that don't have to be visible all the time, but you still need to show the status.

-   **Does the user only care about the completion of the operation?**

    Sometimes it's best to show a notice only when the operation is completed, or give a visual that the operation has been completed immediately, and run the finishing touches in the background.

## Progress controls best practices

Sometimes it's best to see some visual representations of when and where to use these different progress controls:

**ProgressBar - Determinate**

![ProgressBar determinate example](images/progress-bar-determinate-example.png)

The first example is the determinate ProgressBar. When the duration of the operation is known, when installing, downloading, setting up, etc; a determinate ProgressBar is best.

**ProgressBar - Indeterminate**

![ProgressBar indeterminate example](images/progress-bar-indeterminate-example.png)

When it is not known how long the operation will take, use an indeterminate ProgressBar. Indeterminate ProgressBars are also good when filling a virtualized list, and creating a smooth visual transition between an indeterminate to determinate ProgressBar.

-   **Is the operation in a virtualized collection?**

    If so, do not put a progress indicator on each list item as they appear. Instead, use a ProgressBar and place it at the top of the collection of items being loaded in, to show that the items are being fetched.

**ProgressRing - Indeterminate**

![ProgressRing indeterminate example](images/PR_IndeterminateExample.png)

The indeterminate ProgressRing is used when any further user interaction with the app is halted, or the app is waiting for the user's input to continue. The "signing in…" example above is a perfect scenario for the ProgressRing, the user cannot continue using the app until the sign is has completed.

## Customizing a progress control

Both progress controls are rather simple; but some visual features of the controls are not obvious to customize.

**Sizing the ProgressRing**

The ProgressRing can be sized as large as you want, but can only be as small as 20x20epx. In order to resize a ProgressRing, you must set its height and width. If only height or width are set, the control will assume minimum sizing (20x20epx) – conversely if the height and width are set to two different sizes, the smaller of the sizes will be assumed.
To ensure your ProgressRing is correct for your needs, set both the height and the width to the same value:

```XAML
<ProgressRing Height="100" Width="100"/>
```

To make your ProgressRing visible, and animate, you must set the IsActive property to true:

```XAML
<ProgressRing IsActive="True" Height="100" Width="100"/>
```

```C#
progressRing.IsActive = true;
```

**Coloring the progress controls**

By default, the main coloring of the progress controls is set to the accent color of the system. To override this brush simply change the foreground property on either control.

```XAML
<ProgressRing IsActive="True" Height="100" Width="100" Foreground="Blue"/>
<muxc:ProgressBar Width="100" Foreground="Green"/>
```

Changing the foreground color for the ProgressRing will change the fill color of the ring. The foreground property for the ProgressBar will change the fill color of the bar – to alter the unfilled portion of the bar, simply override the background property.

**Showing a wait cursor**

Sometimes it's best to just show a brief wait cursor, when the app or operation needs time to think, and you need to indicate to the user that the app or area where the wait cursor is visible should not be interacted with until the wait cursor has disappeared.

```C#
Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 10);
```

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Xaml-Controls-Gallery) - See all the XAML controls in an interactive format.

## Related articles

- [ProgressBar class](/uwp/api/Windows.UI.Xaml.Controls.ProgressBar)
- [ProgressRing class](/uwp/api/Windows.UI.Xaml.Controls.ProgressRing)