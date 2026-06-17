---
description: This section of the documentation provides developer guidance for implementing Windows Widgets.
title: Develop Windows Widgets
ms.topic: article
ms.date: 04/08/2026
ms.localizationpriority: medium
---

# Develop Windows Widgets and Dashboards

## Widgets

Windows Widgets are small UI containers that display text and graphics, associated with an app installed on the device. Widgets are displayed in a grid on the Widgets Board, a flyout surface that overlays the Windows desktop. They help users stay on top of what's important by surfacing personalized content and quick actions from installed apps. Widget content is defined using the [Adaptive Cards](https://adaptivecards.io/) format, which enables dynamic binding of data to the widget UI.

:::image type="content" source="../../design/widgets/images/widgets-hero-image.png" alt-text="Screenshot of the Widgets Board. The board is a rounded rectangle with the time displayed at the top, followed by a search bar. The rest of the board is a grid of rounded rectangles each representing a widget. The individual widgets show top news stories, current weather, current traffic, etc.":::

For design guidance, see [Windows Widgets design overview](../../design/widgets/index.md).

## Dashboards

Dashboards in the Widgets Board helps users stay on top of what matters, enabling them to easily discover useful information and empowering them to act on it. Feed providers enable users to see content from multiple apps and services at the same time. Users can access content from various apps directly on their Widgets Board without the need to open individual apps, ensuring they have the latest information at their fingertips.

## In this section

| Topic | Description |
|--|--|
| [Feed providers](../feeds/feed-providers.md) | Learn how to register your app's content feeds to appear directly in the Windows Widgets Board. |
| [Widget providers](widget-providers.md) | Learn how to implement a widget provider that supplies content, layout, and interactive elements for your widgets. |
| [Adaptive Cards Designer](widgets-create-a-template.md) | Walk through creating a widget template using the Adaptive Cards Designer. |
