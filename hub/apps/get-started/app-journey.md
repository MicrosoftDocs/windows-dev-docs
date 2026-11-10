---
title: Your path from idea to the Microsoft Store
description: See every step of building a Windows app and publishing it to the Microsoft Store, with links to the article for each step, whether you build with an AI agent or on your own.
ms.date: 11/04/2026
ms.topic: overview
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Your path from idea to the Microsoft Store

This article shows the whole journey from an idea to an app in the Microsoft Store. Follow the steps in order, or jump to the step you're on. Each step links to the article that covers it. You can do every step on your own, or with help from an AI agent.

The Microsoft Store is one way to share your app, and this article follows that path. You don't have to use the Store: you can also distribute your app yourself from your own website, GitHub Releases, or winget. To compare the options, see [Choose a distribution path for your Windows app](../package-and-deploy/choose-distribution-path.md).

## The journey at a glance

:::image type="content" source="images/app-journey-map.png" alt-text="Diagram of the eight steps from idea to the Microsoft Store: 1. Plan, 2. Build, 3. Design, 4. Add Windows features, 5. Package and sign, 6. Test, 7. Publish, and 8. Fix and update." lightbox="images/app-journey-map.png":::

After you publish, the journey repeats for each update: go back to step 2, build the next version, and move through the steps again.

## Steps and articles

| Step | What you do | Start here |
|---|---|---|
| 1. Plan | Choose a framework and how you'll distribute your app. For Store distribution, open your developer account early, allow time for verification, and reserve your app's name once your account is ready. | [Plan your Windows app](plan-your-app.md)<br>[Set up your developer account early](../publish/prepare-developer-account.md)<br>[Reserve your app's name](../publish/publish-your-app/msix/reserve-your-apps-name.md) |
| 2. Build | Create your app, with help from an AI agent or step by step. | [Build an app with an AI agent](build-with-agent.md)<br>[Create your first WinUI app](start-here.md) |
| 3. Design | Make your app look and feel at home on Windows, and make it accessible. | [Design Windows apps](../design/index.yml) |
| 4. Add Windows features | Add notifications, file access, windowing, and other Windows capabilities. | [Add Windows features](../develop/index.yml) |
| 5. Package and sign | For Store distribution, apply your app's Product identity values from Partner Center to the manifest before building the package. Package your app with MSIX and decide how it's signed. | [View app identity details](../publish/view-app-identity-details.md)<br>[MSIX packaging](../package-and-deploy/packaging/index.md)<br>[Code signing options](../package-and-deploy/code-signing-options.md) |
| 6. Test | Test on a clean device, check accessibility, run the Windows App Certification Kit, and go through the Store readiness checklist. | [Test your app before you ship](../develop/test-before-you-ship.md)<br>[Microsoft Store readiness checklist](../publish/store-readiness-checklist.md) |
| 7. Publish | Create a Store submission and send it for certification. Or share your app directly from your website, GitHub Releases, or winget. | [Publish your first Windows app](../package-and-deploy/publish-first-app.md)<br>[Create an app submission](../publish/publish-your-app/msix/create-app-submission.md)<br>[Choose a distribution path](../package-and-deploy/choose-distribution-path.md) |
| 8. Fix and update | Fix anything that stops certification, then ship updates. | [Fix submission and certification failures](../publish/fix-submission-failures.md)<br>[Publish an update](../publish/publish-your-app/msix/publish-update-to-your-app-on-store.md) |

## Steps you do yourself

An AI agent can help with most of this journey, but some steps need a person:

- Reviewing what the agent built, and testing the app with real use.
- If you publish to the Store: opening your Microsoft Store developer account and completing identity verification, accepting agreements in Partner Center, and approving each submission.
- If you distribute your app yourself: getting a code-signing certificate and signing each release.

## Next steps

- [Build an app with an AI agent](build-with-agent.md)
- [Plan your Windows app](plan-your-app.md)
