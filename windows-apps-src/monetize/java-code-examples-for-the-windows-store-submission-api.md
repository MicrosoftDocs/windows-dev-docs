---
ms.assetid: 4920D262-B810-409E-BA3A-F68AADF1B1BC
description: Use the Java code examples in this section to learn more about using the Microsoft Store submission API.
title: Java sample - submissions for apps, add-ons, and flights
ms.date: 07/10/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, code examples, java
ms.localizationpriority: medium
---
# Java sample: submissions for apps, add-ons, and flights

This article provides Java code examples that demonstrate how to use the [Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md) for these tasks:

* [Obtain an Azure AD access token](#token)
* [Create an add-on](#create-add-on)
* [Create a package flight](#create-package-flight)
* [Create an app submission](#create-app-submission)
* [Create an add-on submission](#create-add-on-submission)
* [Create a package flight submission](#create-flight-submission)

You can review each example to learn more about the task it demonstrates, or you can build all the code examples in this article into a console application. For the complete code listing, see the [code listing](java-code-examples-for-the-windows-store-submission-api.md#code-listing) section at the end of this article.

## Prerequisites

These examples use the following libraries:

* [Apache Commons Logging 1.2](https://commons.apache.org/proper/commons-logging/)  (commons-logging-1.2.jar).
* [Apache HttpComponents Core 4.4.5 and Apache HttpComponents Client 4.5.2](https://hc.apache.org/) (httpcore-4.4.5.jar and httpclient-4.5.2.jar).
* [JSR 353 JSON Processing API 1.0](https://mvnrepository.com/artifact/javax.json/javax.json-api/1.0) and [JSR 353 JSON Processing Default Provider API 1.0.4](https://mvnrepository.com/artifact/org.glassfish/javax.json/1.0.4) (javax.json-api-1.0.jar and javax.json-1.0.4.jar).

## Main program and imports

The following example shows the imports statements used by all of the code examples and implements a command line program that calls the other example methods.

:::code language="java" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/java/MainExample.java" range="1-64":::

<span id="token" />

## Obtain an Azure AD access token

The following example demonstrates how to [obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) that you can use to call methods in the Microsoft Store submission API. After you obtain a token, you have 60 minutes to use this token in calls to the Microsoft Store submission API before the token expires. After the token expires, you can generate a new token.

:::code language="java" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/java/CompleteExample.java" range="65-95":::

<span id="create-add-on" />

## Create an add-on

The following example demonstrates how to [create](create-an-add-on.md) and then [delete](delete-an-add-on.md) an add-on.

:::code language="java" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/java/CompleteExample.java" range="310-345":::

<span id="create-package-flight" />

## Create a package flight

The following example demonstrates how to [create](create-a-flight.md) and then [delete](delete-a-flight.md) a package flight.

:::code language="java" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/java/CompleteExample.java" range="185-221":::

<span id="create-app-submission" />

## Create an app submission

The following example shows how to use several methods in the Microsoft Store submission API to create an app submission. To do this, the `SubmitNewApplicationSubmission` method creates a new submission as a clone of the last published submission, and then it updates and commits the cloned submission to Partner Center. Specifically, the `SubmitNewApplicationSubmission` method performs these tasks:

1. To begin, the method [gets data for the specified app](get-an-app.md).
2. Next, it [deletes the pending submission for the app](delete-an-app-submission.md), if one exists.
3. It then [creates a new submission for the app](create-an-app-submission.md) (the new submission is a copy of the last published submission).
4. It changes some details for the new submission and upload a new package for the submission to Azure Blob storage.
5. Next, it [updates](update-an-app-submission.md) and then [commits](commit-an-app-submission.md) the new submission to Partner Center.
6. Finally, it periodically [checks the status of the new submission](get-status-for-an-app-submission.md) until the submission is successfully committed.

:::code language="java" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/java/CompleteExample.java" range="97-183":::

<span id="create-add-on-submission" />

## Create an add-on submission

The following example shows how to use several methods in the Microsoft Store submission API to create an add-on submission. To do this, the `SubmitNewInAppProductSubmission` method creates a new submission as a clone of the last published submission, and then updates and commits the cloned submission to Partner Center. Specifically, the `SubmitNewInAppProductSubmission` method performs these tasks:

1. To begin, the method [gets data for the specified add-on](get-an-add-on.md).
2. Next, it [deletes the pending submission for the add-on](delete-an-add-on-submission.md), if one exists.
3. It then [creates a new submission for the add-on](create-an-add-on-submission.md) (the new submission is a copy of the last published submission).
4. It uploads a ZIP archive that contains icons for the submission to Azure Blob storage.
5. Next, it [updates](update-an-add-on-submission.md) and then [commits](commit-an-add-on-submission.md) the new submission to Partner Center.
6. Finally, it periodically [checks the status of the new submission](get-status-for-an-add-on-submission.md) until the submission is successfully committed.

:::code language="java" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/java/CompleteExample.java" range="347-431":::

<span id="create-flight-submission" />

## Create a package flight submission

The following example shows how to use several methods in the Microsoft Store submission API to create a package flight submission. To do this, the `SubmitNewFlightSubmission` method creates a new submission as a clone of the last published submission, and then updates and commits the cloned submission to Partner Center. Specifically, the `SubmitNewFlightSubmission` method performs these tasks:

1. To begin, the method [gets data for the specified package flight](get-a-flight.md).
2. Next, it [deletes the pending submission for the package flight](delete-a-flight-submission.md), if one exists.
3. It then [creates a new submission for the package flight](create-a-flight-submission.md) (the new submission is a copy of the last published submission).
4. It uploads a new package for the submission to Azure Blob storage.
5. Next, it [updates](update-a-flight-submission.md) and then [commits](commit-a-flight-submission.md) the new submission to PartnerCenter.
6. Finally, it periodically [checks the status of the new submission](get-status-for-a-flight-submission.md) until the submission is successfully committed.

:::code language="java" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/java/CompleteExample.java" range="223-308":::

<span id="utilities" />

## Utility methods to upload submission files and handle request responses

The following utility methods demonstrate these tasks:

* How to upload a ZIP archive containing new assets for an app or add-on submission to Azure Blob storage. For more information about uploading a ZIP archive to Azure Blob storage for app and add-on submissions, see the relevant instructions in [Create an app submission](manage-app-submissions.md#create-an-app-submission), [Create an add-on submission](manage-add-on-submissions.md#create-an-add-on-submission), and [Create a package flight submission](manage-flight-submissions.md#create-a-package-flight-submission).
* How to handle request responses.

:::code language="java" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/java/CompleteExample.java" range="433-490":::

<span id="code-listing" />

## Complete code listing

The following code listing contains all of the previous examples organized into one source file.

:::code language="java" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/java/CompleteExample.java" range="1-491":::

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
