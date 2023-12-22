---
description: Learn about using reactive in-app help as the default method of displaying help for users, and about types of in-app help.
title: Guidelines for designing in-app help.
label: In-app help
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 6208b71b-37a7-40f5-91b0-19b665e7458a
ms.localizationpriority: medium
---
# In-app help pages

Most of the time, it is best that help be displayed within the application and when the user chooses to view it.

## When to use in-app help pages

In-app help should be the default method of displaying help for the user. It should be used for any help which is simple, straightforward, and does not introduce new content to the user. Instructions, advice, and tips & tricks are all suitable for in-app help.

Complex instructions or tutorials are not easy to reference quickly, and they take up large amounts of space. Therefore, they should be hosted externally, and not incorporated into the app itself.

Users should not have to seek out help for basic instructions or to discover new features. If you need to have help that educates users, use instructional UI.

## Types of In-app help

In-app help can come in several forms, though they all follow the same general principles of design and usability.

#### Help Pages

Having a separate page or pages of help within your app is a quick and easy way of displaying useful instructions.

-   **Be concise:** A large library of help topics is unwieldy and unsuited for in-app help.
-   **Be consistent:** Make sure that users can reach your help pages the same way from any part of your app. They should never have to search for it.
-   **Users scan, not read:** Because the help a user is looking for might be on the same page as other help topics, make sure they can easily tell which one they need to focus on.


#### Popups

Popups allow for highly contextual help, displaying instructions and advice that is relevant to the specific task that the user is attempting.

-   **Focus on one issue:** Space is even more restricted in a popup than a help page. Help popups needs to refer specifically a single task to be effective.
-   **Visibility is important:** Because help popups can only be viewed from one location, make sure that they're clearly visible to the user without being obstructive. If the user misses it, they might move away from the popup in search of a help page.
-   **Don't use too many resources:** Help shouldn't lag or be slow-loading. Using videos or audio files or high resolution images in popups is more likely to frustrate the user than it is to help them.

#### Descriptions

Sometimes, it can be useful to provide more information about a feature when a user inspects it. Descriptions are similar to instructive UI, but the key difference is that instructional UI attempts to teach and educate the user about features that they don't know about, whereas detailed descriptions enhance a user's understanding of app features that they're already interested in.

-   **Don't teach the basics:** Assume that the user already knows the fundamentals of how to use the item being described. Clarifying or offering further information is useful. Telling them what they already know is not.
-   **Describe interesting interactions:** One of the best uses for descriptions is to educate the user on how a features that they already know about can interact. This helps users learn more about things they already like to use.
-   **Stay out of the way:** Much like instructional UI, descriptions need to avoid interfering with a user's enjoyment of the app.

## Related articles

* [Guidelines for app help](guidelines-for-app-help.md)
