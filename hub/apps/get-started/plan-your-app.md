---
title: Plan your Windows app
description: Make two early decisions for a new Windows app, which framework to use and how you'll distribute it, before you start building.
ms.date: 11/10/2026
ms.topic: concept-article
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Plan your Windows app

Two decisions shape everything that follows when you build a Windows app: which UI framework you use, and how you distribute the app. Make them before you create a project, because changing them later costs time.

## Choose a framework

| If you want to... | Consider |
|---|---|
| Build a new app with the latest Windows design and features | [WinUI](../winui/winui3/index.md) |
| Build a new app with WPF or Windows Forms skills you already have | [WPF](/dotnet/desktop/wpf/overview/) or [Windows Forms](/dotnet/desktop/winforms/overview/). You can add Windows features later with the Windows App SDK. |
| Extend an existing WPF or Windows Forms app | Keep your framework, and add Windows features with the Windows App SDK. |
| Share most of your code with other platforms | A cross-platform framework, such as .NET MAUI or React Native for Windows. |

For more criteria, such as your team's existing skills and whether you need Windows-specific APIs, see the [Windows developer FAQ](windows-developer-faq.md) and [Choose tools and frameworks for your Windows app](../whats-new/personae.md).

## Choose how to distribute your app

You don't have to publish through the Microsoft Store. You can:

- **Publish through the Microsoft Store** for discovery, a trusted install experience, free signing for MSIX packages, and automatic updates.
- **Distribute your app yourself** from your own website, GitHub Releases, or winget. You sign your app and deliver updates.

Your distribution channel decides how you sign your app, how updates reach users, and what accounts you need. Compare the options in [Choose a distribution path for your Windows app](../package-and-deploy/choose-distribution-path.md).

If you choose the Microsoft Store, [set up your developer account early](../publish/prepare-developer-account.md), because verification can take several days. Once your account is ready, [reserve your app's name](../publish/publish-your-app/msix/reserve-your-apps-name.md).

Before building your Store package, make sure your package manifest uses your app's identity values from the **Product identity** page in Partner Center. See [View app identity details](../publish/view-app-identity-details.md). For the full path from a finished build to a published app, see [Your path from idea to the Microsoft Store](app-journey.md).

## Next steps

- [Build an app with an AI agent](build-with-agent.md)
- [Build your first WinUI app](start-here.md)
- [Code signing options for Windows app developers](../package-and-deploy/code-signing-options.md)
