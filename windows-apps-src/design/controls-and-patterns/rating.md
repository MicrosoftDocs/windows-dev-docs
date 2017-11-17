---
author: serenaz
description: Enables users to rate content with touch, pen, mouse, gamepad and keyboard.
title: Ratings Control
template: detail.hbs
ms.author: sezhen
ms.date: 10/25/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
pm-contact: abarlow
design-contact: kimsea
dev-contact: mitra
doc-status: Published
localizationpriority: medium
---

# Ratings control

Allowing users to easily view and set ratings that reflect degrees of satisfaction with content and services is a critical app scenario.  The ratings control allows your users to do this with touch, pen, mouse, gamepad and keyboard. Use the ratings control to let your users rate movies, music and books with high quality interaction and animation. The ratings control has several great features that provide flexibility and customization.

> **Important APIs**: [RatingControl class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.ratingcontrol)

![Example of Ratings Control](images/rating_rs2_doc_ratings_intro.png)

In this article, we describe some key scenarios that the ratings control supports. Each scenario includes an example to provide context, and code that shows how to achieve the scenario.

## Examples

<div style="overflow: hidden; margin: 0 -8px;">
    <div style="float: left; margin: 0 8px 16px; min-width: calc(25% - 16px); max-width: calc(100% - 16px); width: calc((580px - 100%) * 580);">
        <div style="height: 133px; width: 100%">
            <img src="images/xaml-controls-gallery.png" alt="XAML controls gallery"></img>
        </div>
    </div>
    <div style="float: left; margin: -22px 8px 16px; min-width: calc(75% - 16px); max-width: calc(100% - 16px); width: calc((580px - 100%) * 580);">
        <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/RatingControl">open the app and see the RatingControl in action</a>.</p>
        <ul>
        <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
        <li><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics">Get the source code (GitHub)</a></li>
        </ul>
    </div>
</div>

### Editable rating with placeholder value

Perhaps the most common way to use the ratings control is to display an average rating while still allowing the user to enter their own rating value. In this scenario, the ratings control is initially set to reflect the average satisfaction rating of all users of a particular service or type of content (such as a music, videos, books, etc.). It remains in this state until a user interacts with the control with the goal of individually rating an item. This interaction changes the state of the ratings control to reflect the user's personal satisfaction rating.

#### Initial average rating state
![Initial Average Rating State](images/rating_rs2_doc_movie_aggregate.png)

#### Representation of user rating once set

![Representation of User Rating Once Set](images/rating_rs2_doc_movie_user.png)

```XAML
<RatingsControl x:Name="MyRatings" ValueChanged="RatingChanged"/>
```

```csharp
private void RatingChanged(RatingsControl sender, object args)
{
    if (sender.Value == null)
    {
        MyRatings.Caption = "(" + SomeWebService.HowManyPreviousRatings() + ")";
    }

    else
    {
        MyRatings.Caption = "Your rating";
    }
}
```

### Read-only rating mode

Sometimes you need to show ratings of secondary content, such as that displayed in recommended content or when displaying a list of comments and their corresponding ratings. In this case, the user shouldn’t be able to edit the rating, so you can make the control read-only.
The read only mode is also the recommended way of using the ratings control when it is used in very large virtualized lists of content, for both UI design and performance reasons.


![Read-Only Long List](images/rating_rs2_doc_reviews.png)

To do this you would do the following:

```XAML
<RatingsControl IsReadOnly="True"/>
```

## Additional functionality

The ratings control has many additional features which can be used. Details for using these features can be found in our MSDN reference documentation.
Here is a non-comprehensive list of additional functionality:
-   Great long list performance
-   Compact sizing for tight UI scenarios
-   Continuous value fill and rating
-   Spacing customization
-   Disable growth animations
-   Customization of the number of stars

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics) - See all the XAML controls in an interactive format.