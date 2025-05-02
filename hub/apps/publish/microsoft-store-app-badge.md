---
description: This section announces the new Store badge and describes how you can use it for your app
title: The new Microsoft Store app badge
ms.topic: article
ms.date: 05/07/2024
keywords: uwp, microsoft store, store badge, app badge
ms.localizationpriority: medium
---

# The new Microsoft Store badges are here

We've refreshed the Microsoft Store badge to feature the new logo, with a more refined call-to-action to give users more confidence to acquire your app. Generate the badge for your app on the [badge creator page](https://apps.microsoft.com/badge).

## How do I get the new badge?

If you're a developer with an app published on the Microsoft Store, and if you've been using these badges externally using code from our badge generator page linked above, you'll get the new badge automatically once it goes live. However, if you've got a custom implementation of the badge on your website, you should switch to the new design.

## Old

| <img src="../images/old-badge-dark.png" width="200" alt="Old Store badge for dark mode">  | <img src="../images/old-badge-light.png" width="200" alt="Old Store badge for light mode"> |
| ------------- | ------------- |

## New

| <img src="../images/new-badge-dark.png" width="256" alt="New Store badge for dark mode">  | <img src="../images/new-badge-light.png" width="256" alt="New Store badge for light mode"> |
| ------------- | ------------- |

If you don't have a badge yet, visit the [Microsoft Store badge creator page](https://apps.microsoft.com/badge) to choose the options that work best for your badge and hit Generate to create one for your app.

## Customizing the badge appearance on your site
The badge is a [web component](https://developer.mozilla.org/en-US/docs/Web/API/Web_components) that automatically detects the user's language and theme and displays the appropriate badge image. As a web component, the badge won't inherit styles from your page.

But you may need to customize the appearance of the badge. For example, you may wish to adjust the size of the badge on your page to match other button sizes. To do this, you can use a [CSS part selector](https://developer.mozilla.org/en-US/docs/Web/CSS/::part) to style the badge:

```css
/* The badge should have a max width of 200px to match other buttons on my page.
   The badge image preserves aspect ratio, so changing the max-width will also affect the badge height.
*/
ms-store-badge::part(img) {
   max-width: 200px;
}
```

### Some tips to make the most of your badge
* The badge will detect light or dark mode automatically based on the user's preference. However, you can optionally override the badge theme on the [badge creator page](https://apps.microsoft.com/badge).
* We recommend using badge web component because it detects the user's theme and language. But if you need to display your app badge on a page that doesn't support JavaScript, such as GitHub markdown pages, you can optionally create a non-JS version of the badge on the [badge creator page](https://apps.microsoft.com/badge).
* Remember to add a campaign ID with a unique string so you can better track your traffic sources on the Microsoft Store Partner Center dashboard.

Please avoid altering the badge color, text, or elements within.
