---
author: QuinnRadich
title: URI scheme activation in Windows Template Studio
description: The custom URI scheme feature in Windows Template Studio provides an extention to the standard app activate features.
keywords: template, Windows Template Studio, template studio, URI
ms.author: quradic
ms.date: 4/4/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# URI scheme activation in Windows Template Studio

The custom URI scheme feature builds upon the [ActivationService](application-activation.md) included by default in Windows Template Studio projects.

## Change the protocol from the default name

When this feature is added, it sets the supported scheme (or protocol) to `wtsapp`. **You need to change this** as is appropriate to your app. To do this:

- Open Package.appxmanifest
- Select 'Declarations'
- Choose the 'Protocol' declaration if not already selected.
- Change the name as appropriate. (highlighted in image below)

![Changing the application's protocol name](images/change-protocol-name.png)

The protocol name you specify MUST meet these rules:

- It must be a string between 2 and 39 characters in length
- It must contain only numbers, lowercased letters, dots ('.'), pluses('+'), or hyphens ('-').
- It cannot start with a dot ('.').
- It cannot be a reserved value. ([list of reserved scheme names](../../launch-resume/reserved-uri-scheme-names.md#reserved-uri-scheme-names))

## Debugging the app being launched via the URI

- In project properties, go to the **Debug** tab and check the option 'Do no launch, but debug my code when it starts'

![Debug configuration](images/debug-when-my-code-starts.png)

- Press F5 to start debugging your app
- Then, in Edge, open a new tab and type in `wtsapp:sample?secret=mine` (or adjust depending on any changes you've made.) You could also launch it from another app using `LaunchUriAsync()`, but we recommend this method for testing.
- Your app will launch in debug mode and you can debug it like normal.

## Supporting multiple protocols

If you wish to support multiple custom protocols handled by different ActivationHandlers, then in the `CanHandleInternal` method of the `SchemeActivationHandler` you will need to check both the value of `args.Uri.Scheme` and the ActivationKind. This will allow you to have different handlers for each protocol.

## URI calls on running apps

- By default, if the app is already running when launched from a URI, it will just remain on the page that is currently displayed. To alter this behavior remove the check against `PreviousExecutionState` in `SchemeActivationHandler.HandleInternalAsync`.
- If the app is already running and displaying the sample page and you launch another Uri that points to the sample page, the original version of the sample page will remain displayed. If in your app you would like to navigate to a copy of the page that is currently displayed but with different arguments passed to it, remove the conditional check in `NavigationService.Navigate`. (Note that this doesn't apply to projects using MVVM Light.)