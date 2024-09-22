---
description: When you publish an update to a submission, you can choose to gradually roll out the updated packages to a percentage of customers who have your package installed on Windows 10 or Windows 11 (including Xbox).
title: Gradual package rollout
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 65d578a6-4e26-484c-90af-b2cd916f3634
ms.localizationpriority: medium
---
# Gradual package rollout

When you publish an update to a submission, you can choose to gradually roll out the updated packages to a percentage of customers who have your package installed on Windows 10 or Windows 11 (including Xbox). This allows you to monitor feedback and analytic data for the specific packages to make sure you’re confident about the update before rolling it out more broadly. You can increase the percentage (or halt the update) any time without having to create a new submission. 

> [!IMPORTANT]
> Your rollout selections apply to all of your packages, but will only apply to your customers running OS versions that support package flights (Windows.Desktop build 10586 or later and Xbox), including any customers who get the app via [Store-managed (online) licensing](organizational-licensing.md) via [Microsoft Store for Business](https://businessstore.microsoft.com/store) or [Microsoft Store for Education](https://educationstore.microsoft.com/store). When using gradual package rollout, customers on earlier OS versions will not get packages from the latest submission until you finalize the package rollout as described below.

Note that all of your customers will see the Store listing details that you entered with your latest submission. The rollout settings only apply to the packages that customers install, for updates to existing customers.

> [!TIP]
> Package rollout distributes packages to a random selection of customers who have your package installed
in the percentages that you specify. To distribute specific packages to selected customers that you specify, you can use package flights. You can also combine rollout with your package flights if you want to gradually distribute an update to one of your flight groups.


## Setting the rollout percentage

You can select to roll out your update on the **Packages** page of an updated submission. To do so, check the box that says **Roll out update gradually after this submission is published (to Windows 10 or Windows 11 customers only)**. Then enter the percentage of customers who should get the update when the submission is first published. For example, you might enter 5 if you want to start by rolling the update out to only a small percentage of your app’s existing customers who have already installed this app.

Click **Update** to save your selections. After your app completes the certification process, the packages will be distributed to existing customers according to the percentage that you specified for updates.


## Adjusting the rollout after the submission is published

To adjust the rollout after the submission has been published, go to your app’s Overview page. You can drag the selector to change the percentage of customers getting the packages from your newest submission. Click **Update** to save your selections. The packages will then start to be distributed to existing customers who have your package installed according to the percentage that you specified for updates.


## Completing the rollout

Before you can create a new submission, you'll need to complete the package rollout. You can **finalize** the rollout and distribute the latest packages to all of your customers, or **halt** the rollout to stop distributing the latest packages.

If you have confidence in the update and would like to make it available to all of your customers, click **Finalize package rollout** to distribute the newest packages to all of your customers.

> [!TIP]
> Changing the rollout percentage to 100% does not ensure that all of your customers will get the packages from the latest submissions, because some customers may be on OS versions that don’t support rollout. You must finalize the rollout in order to stop distributing the older packages and update all existing customers to the newer ones.

If you find that there are problems with the update and you don’t want to distribute it any further, you can click **Halt package rollout** to stop distributing packages from the latest submission. Once you halt a package rollout, those packages will no longer be distributed to any customers; only the packages from the previous submission will be used for any new or updating customers. However, any customers who already had the newer packages will keep those packages; they won’t be rolled back to the previous version. To provide an update to these customers, you’ll need to create a new submission with the packages you’d like them to get. Note that if you use a gradual rollout in your next submission, customers who had the package you halted will be offered the new update in the same order they were offered the halted package. The new rollout will be between your last finalized submission and your newest submission; once you halt a package rollout, those packages will no longer be distributed to any customers.
