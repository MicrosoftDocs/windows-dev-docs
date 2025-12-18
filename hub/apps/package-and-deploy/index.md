---
title: Package and deploy Windows apps overview
description: The topics in this section introduce options and guidance for deploying different types of Windows apps. Your first decision will be whether or not to package your app.
ms.topic: concept-article
ms.date: 12/17/2025
ms.localizationpriority: medium
---

<div class="buttons margin-top-xs">
  <a href="../introduction.md" class="button button-sm"><span>Start</span></a>
  <a href="../design/index.md" class="button button-sm"><span>Design</span></a>
  <a href="../develop/index.md" class="button button-sm"><span>Develop</span></a>
  <a href="" class="button button-sm button-primary button-filled"><span>Package</span></a>
  <a href="../publish/index.md" class="button button-sm"><span>Publish</span></a>
</div>

# Package and deploy Windows apps overview

:::image type="content" source="images/header-packaging.png" alt-text="Blue wrench and screwdriver icons on a light gray banner background representing tools for app packaging and deployment." border="false":::

---

App packaging provides your application with a predictable installation, update, and servicing model on Windows. While WinUI apps are packaged by default, many other types of apps aren't. And adding package identity unlocks a wide range of Windows capabilities. Features that depend on package identity to function include background tasks, notifications, live tiles, custom context menu extensions, share targets, and other extensibility points. Packaging also helps ensure cleaner deployments, reliable updates, and streamlined distribution through channels such as the Microsoft Store and enterprise deployment tools.

When deploying apps that use the Windows App SDK, you can choose between framework-dependent and self-contained deployment models. Framework-dependent apps rely on the Windows App SDK runtime and/or framework package being installed on the user’s machine. In contrast, self-contained apps bundle the Windows App SDK dependencies directly with the application, ensuring the app carries everything it needs to run. The right model depends on your distribution scenario, update strategy, and how much control you want over the app’s footprint and dependencies.

---

#### Get started with packaging and deploying your Windows app

[!INCLUDE [apps-packaging-overview](../../includes/apps-packaging-overview.md)]
