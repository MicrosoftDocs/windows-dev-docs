---
title: Configure access policies in Partner Center

description: Describes how you can configure access policies in Partner Center to allow other apps, games, and services to access the Xbox Live settings.
ms.assetid:
ms.date: 02/21/2018
ms.topic: article


ms.localizationpriority: medium
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, udc, universal developer center
---
# Configure access policies in Partner Center

You can use [Partner Center](https://partner.microsoft.com/dashboard) to allow other services, games, and apps to access your title's Xbox Live settings and data. For example, you may want a web service to display leaderboards on your website, or you may have a companion app that can access the game's title storage to view or modify saved game data.

By default, only the title itself can access the settings and data stored on the Xbox Live service. You can change this by configuring access policies in Partner Center.

> [!NOTE]
> This topic does not apply to titles in the Xbox Live Creators Program.

Add configuration by doing the following:

1. After selecting your title in [Partner Center](https://partner.microsoft.com/dashboard), navigate to **Services** > **Xbox Live**.

2. Click on the link to **access policies**.

3. Click on the setting you want to grant access to, and click the Add app/service button. This will add a new row to the bottom of the list of apps/services configured to access that setting.

4. Select the type of app or service in the dropdown box, and fill in the detail box to indicate the app, title id, or service id of the app or service that will access the data.

5. Select if the app or service can only read the data, or if it has full access to the data.

6. Repeat for each setting, and for each app or service that needs access to those settings. You can click **Delete** to remove an entry.

7. When you are finished, click the **Save** button to save your changes.

![Access Policies add app or service screen](../../images/dev-center/data-sharing-2.png)
