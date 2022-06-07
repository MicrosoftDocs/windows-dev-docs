---
description: Dialogs and flyouts display transient UI elements that appear when the user requests them or when something happens that requires notification or approval.
title: Dialogs and flyouts
template: detail.hbs
ms.date: 06/24/2021
ms.topic: article
ms.assetid: ad6affd9-a3c0-481f-a237-9a1ecd561be8
doc-status: Published
ms.localizationpriority: medium
---
# Dialogs and flyouts

Dialogs and flyouts are transient UI elements that appear when something happens that requires notification, approval, or additional information from the user.

**Dialogs**

![Example of a dialog](../images/dialogs/dialog_RS2_delete_file.png)

Dialogs are modal UI overlays that provide contextual app information. Dialogs block interactions with the app window until being explicitly dismissed. They often request some kind of action from the user.

**Flyouts**

![Example of a flyout](../images/flyout-example2.png)

A flyout is a lightweight contextual popup that displays UI related to what the user is doing. It includes placement and sizing logic, and can be used to reveal a secondary control or show more detail about an item.

Unlike a dialog, a flyout can be quickly dismissed by tapping or clicking somewhere outside the flyout, pressing the Escape key or Back button, resizing the app window, or changing the device's orientation.

## Is this the right control?

Dialogs and flyouts make sure that users are aware of important information, but they also disrupt the user experience. Because dialogs are modal (blocking), they interrupt users, preventing them from doing anything else until they interact with the dialog. Flyouts provide a less jarring experience, but displaying too many flyouts can be distracting.

Once you've determined that you want to use a dialog or flyout, you need to choose which one to use.

Given that dialogs block interactions and flyouts do not, dialogs should be reserved for situations where you want the user to drop everything to focus on a specific bit of information or answer a question. Flyouts, on the other hand, can be used when you want to call attention to something, but it's ok if the user wants to ignore it.

**Use a dialog for...**

- Expressing important information that the user **must** read and acknowledge before proceeding. Examples include:
  - When the user's security might be compromised
  - When the user is about to permanently alter a valuable asset
  - When the user is about to delete a valuable asset
  - To confirm an in-app purchase
- Error messages that apply to the overall app context, such as a connectivity error.
- Questions, when the app needs to ask the user a blocking question, such as when the app can't choose on the user's behalf. A blocking question can't be ignored or postponed, and should offer the user well-defined choices.

**Use a flyout for...**

- Collecting additional information needed before an action can be completed.
- Displaying info that's only relevant some of the time. For example, in a photo gallery app, when the user clicks an image thumbnail, you might use a flyout to display a large version of the image.
- Displaying more information, such as details or longer descriptions of an item on the page.

## Ways to avoid using dialogs and flyouts

Consider the importance of the information you want to share: is it important enough to interrupt the user? Also consider how frequently the information needs to be shown; if you're showing a dialog or notification every few minutes, you might want to allocate space for this info in the primary UI instead. For example, in a chat client, rather than showing a flyout every time a friend logs in, you might display a list of friends who are online at the moment and highlight friends as they log on.

Dialogs are frequently used to confirm an action (such as deleting a file) before executing it. If you expect the user to perform a particular action frequently, consider providing a way for the user to undo the action if it was a mistake, rather than forcing users to confirm the action every time.

## Examples

> [!div class="checklist"]
>
> - **Important APIs**: [ContentDialog class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.ContentDialog), [Flyout class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.Flyout)

> - If you have the **WinUI 3 Gallery** app installed, click here to open the app and see the [ContentDialog](winui3gallery:/item/ContentDialog) or [Flyout](winui3gallery:/item/Flyout) in action. Get the app from the [Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) or get the source code on [GitHub](https://github.com/microsoft/WinUI-Gallery/tree/winui3).

## How to create a dialog

See the [Dialogs article](dialogs.md).

## How to create a flyout

See the [Flyout article](flyouts.md).
