---
description: Learn about the guidelines for optimizing the viewability of your ad units and how to measure your viewability metrics with the Advertising performance report. 
title: Optimize the viewability of your ad units
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, guidelines, viewability
ms.localizationpriority: medium
---
# Optimize the viewability of your ad units

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

The [Advertising performance report](../publish/advertising-performance-report.md) includes viewability metrics for your ad units. Viewability is an important metric because the advertising industry is moving towards valuing viewable impressions rather than just delivered impressions. Advertisers tend to bid for viewable impressions because they have an increased chance of their ads being seen by users.  

In alignment with the IAB viewability guidelines, a banner ad impression is counted as viewable if it meets the following criteria:

* Pixel requirement: Greater than or equal to 50% of the pixels in the advertisement were on the viewable space of the app.
* Time requirement: The time the pixel requirement is met was greater than or equal to one continuous second, post ad render.

A video ad impression is counted as viewable if it meets the following criteria:

* Pixel requirement: Greater than or equal to 50% of the pixels in the advertisement were on the viewable part of the app.
* Time requirement: The video met the pixel requirement and played for two continuous seconds post ad render.

Viewability is calculated using the following formula:

**Viewability = [Viewed impressions] * 100 / [ Total ad impressions]**

## Guidelines to improve ad unit viewability

To optimize the viewable impressions for your ad units, follow these guidelines.

1. **Measure performance**&nbsp;&nbsp;Use the [Advertising performance report](../publish/advertising-performance-report.md) to review the viewability metrics of each of your ad units.
2.	**Position your ad control appropriately**&nbsp;&nbsp;In general, the most viewable position for an ad is just above the fold (that is, at the bottom of the visible portion of the page that users can see without having to scroll). Ads that are displayed at top of the page are not as viewable. Consider inspecting each element of your page to see how you can optimize the viewable space in your app. Make sure your ad controls aren't placed in the app background.
3.	**Manage page length**&nbsp;&nbsp;Shorter page content results in higher viewability. Implement infinite scrolling if your app pages need to be longer.
4.	**Fix the ad position**&nbsp;&nbsp;Make sure your ads stay at a fixed position even as the user scrolls. This will help you get a higher view time and increase your viewability rate.
5.	**Set opacity**&nbsp;&nbsp;Ensure the opacity of the container that contains the ad control is 100%.
6.	**Implement lazy loading**&nbsp;&nbsp;Don't load your ads until the users scroll into the view that contains the ad control. This will ensure the ad displays longer for the user to view.
7.	**Experiment with various ad sizes**&nbsp;&nbsp;Select a few ad sizes and measure viewability for each of them using the [Advertising performance report](../publish/advertising-performance-report.md). Pick the one that works best for you.
8.	**Revisit app design**&nbsp;&nbsp;Make improvements to your app page design to reduce page load time. Users tend to stay longer on pages that load quicker.
