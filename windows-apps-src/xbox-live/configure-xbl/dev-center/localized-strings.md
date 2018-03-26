---
title: Localized strings
author: shrutimundra
description: Learn how to localize strings on Windows Dev Center
ms.assetid: e0f307d2-ea02-48ea-bcdf-828272a894d4
ms.author: kevinasg
ms.date: 11/17/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: low
keywords: Xbox Live, Xbox, games, uwp, windows 10, Xbox one, Localized strings, Windows Dev Center
---
# Configuring Localized strings on Windows Dev Center

You can use this page to localize all your Xbox Live configurations to all the languages that your game supports. All of the service configurations that you have created on any of the subsequent Xbox Live pages will be added to the file that you would download.

You can use [Windows Dev Center](https://developer.microsoft.com/dashboard) to configure the localized strings in all languages associated with your game. Add configuration by doing the following:

1. Navigate to the **Localized strings** section for your title, located under **Services** > **Xbox Live** > **Localized strings**.
2. Click the **Download** button which will download a localization.xml file on your local machine.

![Screenshot of the localized strings configuration page on dev center](../../images/dev-center/localized-strings/localized-strings-1.png)

3. You can add the localized strings by duplicating the <Value locale="en-US">Mazes Played</Value> tag and changing the value of the locale to the language of your choice and the value of the localized string. You must have atleast one value tag within the developer display locale to avoid errors.

![edit localized strings](../../images/dev-center/localized-strings/localized-strings.gif)

4. Once you have added all the localized strings, you can upload the file by dragging or browsing your local machine.

![Image of the button to upload the localization.xml file](../../images/dev-center/localized-strings/localized-strings-2.png)

Please note the following errors might appear when you upload the localization.xml file:

| Error | Reason |
|---------------------------|-------------|
| Failed XSD Validation: The element 'LocalizedString' in namespace 'http://config.mgt.xboxlive.com/schema/localization/1' cannot contain text. List of possible elements expected: 'Value' in namespace 'http://config.mgt.xboxlive.com/schema/localization/1' | This occurs when the XML document is malformed |
| Localization string is missing an entry for the developer display locale | This occurs when a localized string is missing an entry whose locale does not match the dev display locale |
| Failed XSD Validation: The 'locale' attribute is invalid - The value ' ' is invalid according to its datatype 'http://config.mgt.xboxlive.com/schema/localization/1:NonEmptyString' - The Pattern constraint failed. | This occurs when a localized string is missing the locale value in the <Value> tag|