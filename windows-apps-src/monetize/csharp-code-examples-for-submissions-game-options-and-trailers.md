---
description: Use the C# code examples in this section to learn more about submitting game options and trailers using the Microsoft Store submission API.
title: C# sample - app submission with game options and trailers
ms.date: 07/10/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, code examples, game options, trailers, advanced listings, C#
ms.localizationpriority: medium
---
# C\# sample: app submission with game options and trailers

This article provides C# code examples that demonstrate how to use the [Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md) for these tasks:

* Obtain an Azure AD access token to use with the Microsoft Store submission API.
* Create an app submission
* Configure Store listing data for the app submission, including the [gaming](manage-app-submissions.md#gaming-options-object) and [trailers](manage-app-submissions.md#trailer-object) advanced listing options.
* Upload the ZIP file containing the packages, listing images, and trailer files for the app submission.
* Commit the app submission.

You can review each example to learn more about the task it demonstrates, or you can build all the code examples in this article into a console application. To build the examples, create a C# console application named **DevCenterApiSample** in Visual Studio, copy each example to a separate code file in the project, and build the project.

## Prerequisites

These examples have the following requirements:

* Add a reference to the System.Web assembly in your project.
* Install the [Newtonsoft.Json](https://www.newtonsoft.com/json) NuGet package from Newtonsoft to your project.

<span id="create-app-submission" />

## Create an app submission

The ```CreateAndSubmitSubmissionExample``` class defines a public ```Execute``` method that calls other example methods to use the Microsoft Store submission API to create and commit an app submission that contains game options and a trailer. To adapt this code for your own use:

* Assign the ```tenantId``` variable to the tenant ID for your app, and assign the ```clientId``` and ```clientSecret``` variables to the client ID and key for your app. For more information, see [How to associate an Azure AD application with your Partner Center account](create-and-manage-submissions-using-windows-store-services.md#how-to-associate-an-azure-ad-application-with-your-partner-center-account)
* Assign the ```applicationId``` variable to the [Store ID](in-app-purchases-and-trials.md#store-ids) of the app for which you want to create a submission.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_SubmissionAdvancedListings/cs/CreateAndSubmitSubmissionExample.cs" id="CreateAndSubmitSubmissionExample":::

<span id="token" />

## Obtain an Azure AD access token

The ```DevCenterAccessTokenClient``` class defines a helper method that uses the your ```tenantId```, ```clientId``` and ```clientSecret``` values to create an Azure AD access token to use with the Microsoft Store submission API.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_SubmissionAdvancedListings/cs/DevCenterAccessTokenClient.cs" id="DevCenterAccessTokenClient":::

<span id="utilities" />

## Helper methods to invoke the submission API and upload submission files

The ```DevCenterClient``` class defines helper methods that invoke a variety of methods in the Microsoft Store submission API and upload the ZIP file containing the packages, listing images, and trailer files for the app submission.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_SubmissionAdvancedListings/cs/DevCenterClient.cs" id="DevCenterClient":::

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
