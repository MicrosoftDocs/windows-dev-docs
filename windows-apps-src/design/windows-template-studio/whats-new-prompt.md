---
author: QuinnRadich
title: What's new prompt in Windows Template Studio
description: A What's new prompt allows your Windows Template Studio app to display a custom prompt on the first launch after an update.
keywords: template, Windows Template Studio, template studio, what's new, update
ms.author: quradic
ms.date: 4/4/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# What's new prompts in Windows Template Studio

This feature will display a customizable prompt when the app is used for the first time after it's been updated.

## Testing this feature

This feature works by storing the current version number of the app being used and displaying the dialog when the version number of the app is different to what it stored previously.  
The version number of the app will change every time you release a new update through the store. However, it can be tested by manually changing the version number.

Test this feature by following these steps:

- Run the app. (You'll see no prompt.)
- Close the  app
- In the projecct open `package.appxmanifest`
- Under **Packaging** change the version number (one or more of the major, minor, or build.)
- Run the app again and you'll see the dialog.
- Run it again without changing the version number and you won't see the dialog.

Be sure to update the contents of the dialog every time you release an update so your users always know about what's new.