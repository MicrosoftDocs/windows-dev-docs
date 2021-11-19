---
description: A high-level look at app patterns commonly used across our in-box applications.
title: Windows app title bar
template: detail.hbs
ms.date: 09/30/2021
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Title bar

The title bar sits at the top of an app on the [base layer](../signature-experiences/layering.md). Its main purpose is to allow users to be able to identify the app via its title, move the app window, and minimize, maximize, or close the app.

:::image type="content" source="images/titlebar/titlebar-overview.png" alt-text="An example of a title bar":::

## Standard design

A standard title bar design features:

- 32px height
- 16px app icon
- Caption size title text
- Caption controls (minimize, maximize, close)

## Additional design patterns

### Back button

If a backstack is present, the back button should be placed to the left of the app title or image/title combination.

:::image type="content" source="images/titlebar/back-button.png" alt-text="An example of a back button in the title bar":::

### Search

If global search functionality is present, a searchbox should be added to the titlebar, centered to the window. Increase the size of the title bar to 48px when including a searchbox.

:::image type="content" source="images/titlebar/search.png" alt-text="An example of a search box centered in the title bar":::

Searchbox in this area will need to be designed to be responsive to react to window size changes.

### People

If account representation is present, the person-picture control should be placed to the left of the caption controls.
Increase the size of the title bar to 48px when including a person-picture.

:::image type="content" source="images/titlebar/people.png" alt-text="An example of a person picture control in the title bar":::

### Tabs

If using tabs as the main element of an app, use the titlebar space and keep caption controls anchored to the right.

:::image type="content" source="images/titlebar/tabs.png" alt-text="An example of a tab view with tabs in the titlebar area":::
