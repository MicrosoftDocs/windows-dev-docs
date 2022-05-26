---
description: Enables users to view and set ratings that reflect satisfaction with content and services. 
title: Rating Control
template: detail.hbs
ms.date: 03/16/2022
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
---
# Rating control

The rating control allows users to view and set ratings that reflect degrees of satisfaction with content and services. Users can interact with the rating control with touch, pen, mouse, gamepad, or keyboard. The follow guidance shows how to use the rating control's features to provide flexibility and customization.

![Example of Rating Control](images/rating_rs2_doc_ratings_intro.png)

## Overview

The rating control can be used to enter a rating, or made read-only to display a rating.

### Editable rating with placeholder value

Perhaps the most common way to use the rating control is to display an average rating while still allowing the user to enter their own rating value. In this scenario, the rating control is initially set to reflect the average satisfaction rating of all users of a particular service or type of content (such as a music, videos, books, etc.). It remains in this state until a user interacts with the control with the goal of individually rating an item. This interaction changes the state of the ratings control to reflect the user's personal satisfaction rating.

#### Initial average rating state

![Initial Average Rating State](images/rating_rs2_doc_movie_aggregate.png)

#### Representation of user rating once set

![Representation of User Rating Once Set](images/rating_rs2_doc_movie_user.png)

### Read-only rating mode

Sometimes you need to show ratings of secondary content, such as that displayed in recommended content or when displaying a list of comments and their corresponding ratings. In this case, the user shouldn't be able to edit the rating, so you can make the control read-only.
The read only mode is also the recommended way of using the rating control when it is used in very large virtualized lists of content, for both UI design and performance reasons.

![Read-Only Long List](images/rating_rs2_doc_reviews.png)

## UWP and WinUI 2

> [!IMPORTANT]
>The information and examples in this article are optimized for apps that use the [Windows App SDK](/windows/apps/windows-app-sdk/) and [WinUI 3](/windows/apps/winui/winui3/), but are generally applicable to UWP apps that use [WinUI 2](/windows/apps/winui/winui2/). See the UWP API reference for platform specific information and examples.
>
> This section contains information you need to use the control in a UWP or WinUI 2 app.

The RatingControl for UWP apps is included as part of the Windows UI Library 2. For more info, including installation instructions, see [Windows UI Library](/windows/apps/winui/winui2/). APIs for this control exist in both the [Windows.UI.Xaml.Controls](/uwp/api/Windows.UI.Xaml.Controls) and [Microsoft.UI.Xaml.Controls](/windows/winui/api/microsoft.ui.xaml.controls) namespaces.

> [!div class="checklist"]
>
> - **UWP APIs:** [RatingControl class](/uwp/api/windows.ui.xaml.controls.ratingcontrol)
> - **WinUI APIs:** [RatingControl class](/uwp/api/windows.ui.xaml.controls.ratingcontrol)
> - If you have the **WinUI 2 Gallery** app installed, click here to [open the app and see the RatingControl in action](winui2gallery:/item/RatingControl). Get the app from the [Microsoft Store](https://www.microsoft.com/store/productId/9MSVH128X2ZT) or get the source code on [GitHub](https://github.com/Microsoft/WinUI-Gallery).

We recommend using the latest [WinUI 2](/windows/apps/winui/winui2/) to get the most current styles, templates, and features for all controls.

To use the code in this article with WinUI 2, use an alias in XAML (we use `muxc`) to represent the Windows UI Library APIs that are included in our project. See [Get Started with WinUI 2](/windows/apps/winui/winui2/getting-started) for more info.

```xaml
xmlns:muxc="using:Microsoft.UI.Xaml.Controls"

<muxc:RatingControl />
```

## Create a rating control

> [!div class="checklist"]
>
> - **Important APIs**: [RatingControl class](/uwp/api/windows.ui.xaml.controls.ratingcontrol)
> - If you have the **WinUI 3 Gallery** app installed, click here to [open the app and see the RatingControl in action](winui3gallery:/item/RatingControl). Get the app from the [Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) or get the source code on [GitHub](https://github.com/microsoft/WinUI-Gallery/tree/winui3).

### Editable rating control

This code shows how to create an editable rating control with a placeholder value.

```xaml
<RatingControl x:Name="MyRating" ValueChanged="RatingChanged"/>
```

```csharp
private void RatingChanged(RatingControl sender, object args)
{
    if (sender.Value == null)
    {
        MyRating.Caption = "(" + SomeWebService.HowManyPreviousRatings() + ")";
    }
    else
    {
        MyRating.Caption = "Your rating";
    }
}
```

### Read-only rating control

This code shows how to create a read-only rating control.

```xaml
<RatingControl IsReadOnly="True"/>
```

## Additional functionality

The rating control has many additional features which can be used. Details for using these features can be found in our reference documentation. Here is a non-comprehensive list of additional functionality:

- Great long list performance
- Compact sizing for tight UI scenarios
- Continuous value fill and rating
- Spacing customization
- Disable growth animations
- Customization of the number of stars

## Get the sample code

- [WinUI Gallery sample](https://github.com/Microsoft/WinUI-Gallery) - See all the XAML controls in an interactive format.
