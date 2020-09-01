---
ms.assetid: FABA802F-9CB2-4894-9848-9BB040F9851F
description: Use the C# code examples in this section to learn more about using the Microsoft Store submission API.
title: C# sample - submissions for apps, add-ons, and flights
ms.date: 08/03/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, code examples, C#
ms.localizationpriority: medium
---
# C\# sample: submissions for apps, add-ons, and flights

This article provides C# code examples that demonstrate how to use the [Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md) for these tasks:

* [Create an app submission](#create-app-submission)
* [Create an add-on submission](#create-add-on-submission)
* [Update an add-on submission](#update-add-on-submission)
* [Create a package flight submission](#create-flight-submission)

You can review each example to learn more about the task it demonstrates, or you can build all the code examples in this article into a console application. To build the examples, create a C# console application named **DeveloperApiCSharpSample** in Visual Studio, copy each example to a separate code file in the project, and build the project.

## Prerequisites

These examples use the following libraries:

* Microsoft.WindowsAzure.Storage.dll. This library is available in the [Azure SDK for .NET](https://azure.microsoft.com/downloads/), or you can obtain it by installing the [WindowsAzure.Storage NuGet package](https://www.nuget.org/packages/WindowsAzure.Storage).
* [Newtonsoft.Json](https://www.newtonsoft.com/json) NuGet package from Newtonsoft.

## Main program

The following example implements a command line program that calls the other example methods in this article to demonstrate different ways to use the Microsoft Store submission API. To adapt this program for your own use:

* Assign the ```ApplicationId```, ```InAppProductId```, and ```FlightId``` properties to the ID of the app, add-on, and package flight you want to manage.
* Assign the ```ClientId``` and ```ClientSecret``` properties to the client ID and key for your app, and replace the *tenantid* string in the ```TokenEndpoint``` URL with the tenant ID for your app. For more information, see [How to associate an Azure AD application with your Partner Center account](create-and-manage-submissions-using-windows-store-services.md#how-to-associate-an-azure-ad-application-with-your-partner-center-account)

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/cs/Program.cs" id="Main":::

<span id="clientconfiguration" />

## ClientConfiguration helper class

The sample app uses the ```ClientConfiguration``` helper class to pass Azure Active Directory data and app data to each of the example methods that use the Microsoft Store submission API.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/cs/ClientConfiguration.cs" id="ClientConfiguration":::

<span id="create-app-submission" />

## Create an app submission

The following example implements a class that uses several methods in the Microsoft Store submission API to update an app submission. The ```RunAppSubmissionUpdateSample``` method in the class creates a new submission as a clone of the last published submission, and then it updates and commits the cloned submission to Partner Center. Specifically, the ```RunAppSubmissionUpdateSample``` method performs these tasks:

1. To begin, the method [gets data for the specified app](get-an-app.md).
2. Next, it [deletes the pending submission for the app](delete-an-app-submission.md), if one exists.
3. It then [creates a new submission for the app](create-an-app-submission.md) (the new submission is a copy of the last published submission).
4. It changes some details for the new submission and upload a new package for the submission to Azure Blob storage.
5. Next, it [updates](update-an-app-submission.md) and then [commits](commit-an-app-submission.md) the new submission to Partner Center.
6. Finally, it periodically [checks the status of the new submission](get-status-for-an-app-submission.md) until the submission is successfully committed.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/cs/AppSubmissionUpdateSample.cs" id="AppSubmissionUpdateSample":::

<span id="create-add-on-submission" />

## Create an add-on submission

The following example implements a class that uses several methods in the Microsoft Store submission API to create a new add-on submission. The ```RunInAppProductSubmissionCreateSample``` method in the class performs these tasks:

1. To begin, the method [creates a new add-on](create-an-add-on.md).
2. Next, it [creates a new submission for the add-on](create-an-add-on-submission.md).
3. It uploads a ZIP archive that contains icons for the submission to Azure Blob storage.
4. Next, it [commits the new submission to Partner Center](commit-an-add-on-submission.md).
5. Finally, it periodically [checks the status of the new submission](get-status-for-an-add-on-submission.md) until the submission is successfully committed.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/cs/InAppProductSubmissionCreateSample.cs" id="InAppProductSubmissionCreateSample":::

<span id="update-add-on-submission" />

## Update an add-on submission

The following example implements a class that uses several methods in the Microsoft Store submission API to update an existing add-on submission. The ```RunInAppProductSubmissionUpdateSample``` method in the class creates a new submission as a clone of the last published submission, and then it updates and commits the cloned submission to Partner Center. Specifically, the ```RunInAppProductSubmissionUpdateSample``` method performs these tasks:

1. To begin, the method [gets data for the specified add-on](get-an-add-on.md).
2. Next, it [deletes the pending submission for the add-on](delete-an-add-on-submission.md), if one exists.
3. It then [creates a new submission for the add-on](create-an-add-on-submission.md) (the new submission is a copy of the last published submission).
5. Next, it [updates](update-an-add-on-submission.md) and then [commits](commit-an-add-on-submission.md) the new submission to Partner Center.
6. Finally, it periodically [checks the status of the new submission](get-status-for-an-add-on-submission.md) until the submission is successfully committed.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/cs/InAppProductSubmissionUpdateSample.cs" id="InAppProductSubmissionUpdateSample":::

<span id="create-flight-submission" />

## Create a package flight submission

The following example implements a class that uses several methods in the Microsoft Store submission API to update a package flight submission. The ```RunFlightSubmissionUpdateSample``` method in the class creates a new submission as a clone of the last published submission, and then it updates and commits the cloned submission to Partner Center. Specifically, the ```RunFlightSubmissionUpdateSample``` method performs these tasks:

1. To begin, the method [gets data for the specified package flight](get-a-flight.md).
2. Next, it [deletes the pending submission for the package flight](delete-a-flight-submission.md), if one exists.
3. It then [creates a new submission for the package flight](create-a-flight-submission.md) (the new submission is a copy of the last published submission).
4. It uploads a new package for the submission to Azure Blob storage.
5. Next, it [updates](update-a-flight-submission.md) and then [commits](commit-a-flight-submission.md) the new submission to Partner Center.
6. Finally, it periodically [checks the status of the new submission](get-status-for-a-flight-submission.md) until the submission is successfully committed.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/cs/FlightSubmissionUpdateSample.cs" id="FlightSubmissionUpdateSample":::

<span id="ingestionclient" />

## IngestionClient helper class

The ```IngestionClient``` class provides helper methods that are used by other methods in the sample app to perform the following tasks:

* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) that can be used to call methods in the Microsoft Store submission API. After you obtain a token, you have 60 minutes to use this token in calls to the Microsoft Store submission API before the token expires. After the token expires, you can generate a new token.
* Upload a ZIP archive containing new assets for an app or add-on submission to Azure Blob storage. For more information about uploading a ZIP archive to Azure Blob storage for app and add-on submissions, see the relevant instructions in [Create an app submission](manage-app-submissions.md#create-an-app-submission) and [Create an add-on submission](manage-add-on-submissions.md#create-an-add-on-submission).
* Process the HTTP requests for the Microsoft Store submission API.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/cs/IngestionClient.cs" id="IngestionClient":::

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
