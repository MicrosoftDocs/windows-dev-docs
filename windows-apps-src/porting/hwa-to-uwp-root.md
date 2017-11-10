---
author: seksenov
title: Hosted Web Apps - Convert your web application to a Universal Windows Platform (UWP) app and access native Windows 10 features
description: Find resources to convert your web app to a Universal Windows Platform (UWP) app for the Microsoft Store.
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: Hosted Web Apps, convert a website to Windows app, web apps on Microsoft Store, Chrome apps for Windows
localizationpriority: medium
---

# Hosted Web Apps - Access Windows 10 features from your web app

Your web application can have full access to the Universal Windows Platform (UWP), including calling Windows Runtime APIs directly from script hosted on a server, leveraging Cortana integration, and using an online authentication provider. Hybrid apps are also supported as you can include local code to be called from the hosted script and manage app navigation between remote and local pages.

## Get started

Whether you're on a Mac or a PC, you can create a Hosted Web App in a matter of minutes. The best way to get started is by using free, full-featured [Visual Studio Community 2015](https://www.visualstudio.com/vs/community/), especially if you're on a Windows device. If you do not have access to Visual Studio, there are a few options from which you can choose. If you are familiar with command-line interface (CLI) utilities, check out [ManifoldJS](http://manifoldjs.com/). You can also use [App Studio](http://appstudio.windows.com/), a free, no coding required, online creation tool that allows you to quickly build Windows 10 apps.

- [Step-by-step instructions to convert your web application to a Universal Windows Platform (UWP) app using Windows](hwa-create-windows.md)

- [Step-by-step instructions to convert your web application to a Universal Windows Platform (UWP) app using a Mac](hwa-create-mac.md)

## Enhance your app

- Make your app shine with [native Windows Runtime features](hwa-access-features.md) you can call directly from JavaScript.

- Keep your app secure by setting Application Content URI Rules (ACURs) with our Content Security Policy (CSP) model.

- Run your app with the power of voice by integrating Cortana commands.

- Grant programmatic access to user resources and device functionality by declaring app cabilities.

- Create a simple and smooth login flow for your users by verifying their identity with OpenID and OAuth.

- Stop having to decide between a Packaged and Hosted Web app. You can have a bit of both by creating a Hybrid App.

## Convert your existing Chrome app

We have made it easy to [convert your existing Chrome hosted app](hwa-chrome-conversion.md) to a Windows Hosted Web App. [ManifoldJS](http://manifoldjs.com/) now accepts Chrome manifests as a form of input. We have also developed a [CLI tool](https://github.com/MicrosoftEdge/hwa-cli) that generates an `.appx` package from your existing `.zip` or `.crx` files.

## Demos

- [Contoso Travel App](http://contosotravel.azurewebsites.net/)

