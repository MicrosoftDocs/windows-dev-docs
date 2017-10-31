---
author: laurenhughes
ms.assetid: df4d227c-21f9-4f99-9e95-3305b149d9c5
title: UWP App Streaming Install
description: Universal Windows Platform (UWP) App Streaming Install enables you to specify which parts of your app you would like the Microsoft Store to download first. When the essential files of the app are downloaded first, the user can launch and interact with the app while the rest of it finishes downloading in the background.
ms.author: lahugh
ms.date: 04/05/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, streaming install, uwp app streaming install
localizationpriority: medium
---


# UWP App Streaming Install
Universal Windows Platform (UWP) App Streaming Install enables you to specify which parts of your app you would like the Microsoft Store to download first. When the essential files of the app are downloaded first, the user can launch and interact with the app while the rest of it finishes downloading in the background. 

To use UWP App Streaming Install you'll need to divide your app's files into sections. To do this, you'll create a content group map, which is an XML file that's packaged with your app, allowing you to set download priority and order. See the topic linked below for more information.

For a complete guide on adding UWP App Streaming Install to your UWP app, check out this [blog series](https://blogs.msdn.microsoft.com/appinstaller/2017/03/15/uwp-streaming-app-installation/).

| Topic | Description | 
|-------|-------------|
| [Create and convert a source content group map](create-cgm.md) | To get your Universal Windows Platform (UWP) app ready for UWP App Streaming Install, you'll need to create a content group map. This article will help you with the specifics of creating and converting a content group map while providing some tips and tricks along the way. |