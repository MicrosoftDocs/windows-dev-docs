---
description: A high-level look at app patterns commonly used across our in-box applications.
title: Windows app silhouettes
template: detail.hbs
ms.date: 09/30/2021
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Windows app silhouettes

Silhouettes indicate a common pattern of relationships between elements such as app layering, menus, navigation, commanding and content areas. This article focuses on the common silhouettes as used in several Windows in-box apps.

Also refer to [Content Basics](content-basics.md) for common arrangements of content and controls.

## Top navigation silhouette

:::image type="content" source="images/top-nav-silhouette.png" alt-text="An example of a top nav app":::

A [NavigationView](../controls/navigationview.md) can be used at the top of the app’s [content layer](../signature-experiences/layering.md), building a connection with the content below. Note the location of user [identity/person/picture control](../controls/person-picture.md) when using top navigation.

Placing navigation on the same row as commands can be useful when trying to maximize the amount of vertical space for the content below.

Content margins can vary. This example uses 56epx margins, complementing large pieces of media. Use smaller margins for smaller/tighter content needs.

In Windows 11, Photos is a good example of an app that uses a top navigation silhouette.

## Menu bar silhouette

:::image type="content" source="images/menu-bar-silhouette.png" alt-text="An example of a menu bar app":::

A [MenuBar](../controls/menus.md) can be used as part of the of the [base layer](../signature-experiences/layering.md) along with a [CommandBar](../controls/command-bar.md). This brings more focus to the content area’s primary task, in this case composition and editing.

This example depicts a text editor using 12epx margins to compliment the utility of the app.

In Windows 11, Notepad is a good example of an app that uses a menu bar silhouette.

## Left navigation silhouette

:::image type="content" source="images/left-nav-silhouette.png" alt-text="An example of a left nav app":::

[NavigationView](../controls/navigationview.md) controls automatically rests on the app's [base layer](../signature-experiences/layering.md). This brings more focus to the content area’s primary task. Note the location of user [identity/person/picture control](../controls/person-picture.md) when using left navigation.

Content margins can vary. This example uses 56epx margins to compliment the cohesion of the content within the expanders. Use smaller margins when content cohesion is less of a concern, because other design elements reinforce cohesion, content is not nested in expanders, or content should not logically be grouped together.

In Windows 11, Settings is a good example of an app that uses a left navigation silhouette.

## Tab View Silhouette

:::image type="content" source="images/tab-view-silhouette.png" alt-text="An example of a tab view app":::

A [TabView](../controls/tab-view.md) can integrate with the app’s [base layer](../signature-experiences/layering.md), and [title bar](/windows/apps/develop/title-bar)control. This brings more focus to the content area’s primary task, in this case code composition and editing.

This example depicts a text editor using 12epx margins to compliment the utility of the app.

In Windows 11, Terminal is a good example of an app that uses a tab view silhouette.