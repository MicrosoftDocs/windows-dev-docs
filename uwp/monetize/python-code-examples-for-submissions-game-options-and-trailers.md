---
description: Use the Python code examples in this section to learn more about submitting game options and trailers using the Microsoft Store submission API.
title: Python sample - app submission with game options and trailers
ms.date: 07/10/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, code examples, game options, trailers, advanced listings, python
ms.localizationpriority: medium
---
# Python sample: app submission with game options and trailers

This article provides Python code examples that demonstrate how to use the [Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md) for these tasks:

* Obtain an Azure AD access token to use with the Microsoft Store submission API.
* Create an app submission
* Configure Store listing data for the app submission, including the [gaming](manage-app-submissions.md#gaming-options-object) and [trailers](manage-app-submissions.md#trailer-object) advanced listing options.
* Upload the ZIP file containing the packages, listing images, and trailer files for the app submission.
* Commit the app submission.

<span id="create-app-submission" />

## Create an app submission

This code calls other example classes and functions to use the Microsoft Store submission API to create and commit an app submission that contains game options and a trailer. To adapt this code for your own use:

* Assign the `tenant` variable to the tenant ID for your app, and assign the `client` and `secret` variables to the client ID and key for your app. For more information, see [How to associate an Azure AD application with your Partner Center account](create-and-manage-submissions-using-windows-store-services.md#how-to-associate-an-azure-ad-application-with-your-partner-center-account)
* Assign the `application_id` variable to the [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to create a submission.

> [!div class="tabbedCodeSnippets"]
:::code language="python" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_SubmissionAdvancedListings/python/CreateAndSubmitAppSubmissionExample.py" range="1-74":::

<span id="token" />

## Obtain an Azure AD access token and invoke the submission API

The following example defines the following classes:

* The `DevCenterAccessTokenClient` class defines a helper method that uses the your `tenantId`, `clientId` and `clientSecret` values to create an Azure AD access token to use with the Microsoft Store submission API.
* The `DevCenterClient` class defines helper methods that invoke a variety of methods in the Microsoft Store submission API and upload the ZIP file containing the packages, listing images, and trailer files for the app submission.

> [!div class="tabbedCodeSnippets"]
:::code language="python" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_SubmissionAdvancedListings/python/devcenterclient.py" range="1-126":::

<span id="token" />

## Get app submission listing data

The following example defines helper functions that return JSON-formatted listing data for a new sample app submission.

> [!div class="tabbedCodeSnippets"]
:::code language="python" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_SubmissionAdvancedListings/python/submissiondatasamples.py" range="1-170":::

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
