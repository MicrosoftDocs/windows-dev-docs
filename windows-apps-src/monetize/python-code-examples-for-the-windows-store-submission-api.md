---
ms.assetid: 8AC56AAF-8D8C-4193-A6B3-BB5D0669D994
description: Use the Python code examples in this section to learn more about using the Microsoft Store submission API.
title: Python code to submit apps, add-ons, and flights
ms.date: 07/10/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, code examples, python
ms.localizationpriority: medium
---
# Python sample: submissions for apps, add-ons, and flights

This article provides Python code examples that demonstrate how to use the [Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md) for these tasks:

* [Obtain an Azure AD access token](#token)
* [Create an add-on](#create-add-on)
* [Create a package flight](#create-package-flight)
* [Create an app submission](#create-app-submission)
* [Create an add-on submission](#create-add-on-submission)
* [Create a package flight submission](#create-flight-submission)

<span id="token" />

## Obtain an Azure AD access token

The following example demonstrates how to [obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) that you can use to call methods in the Microsoft Store submission API. After you obtain a token, you have 60 minutes to use this token in calls to the Microsoft Store submission API before the token expires. After the token expires, you can generate a new token..

:::code language="python" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/python/Examples.py" range="1-20":::

<span id="create-add-on" />

## Create an add-on

The following example demonstrates how to [create](create-an-add-on.md) and then [delete](delete-an-add-on.md) an add-on.

:::code language="python" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/python/Examples.py" range="26-52":::

<span id="create-package-flight" />

## Create a package flight

The following example demonstrates how to [create](create-a-flight.md) and then [delete](delete-a-flight.md) a package flight.

:::code language="python" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/python/Examples.py" range="58-87":::

<span id="create-app-submission" />

## Create an app submission

The following example shows how to use several methods in the Microsoft Store submission API to create an app submission. To do this, the code creates a new submission as a clone of the last published submission, and then updates and commits the cloned submission to Partner Center. Specifically, the example performs these tasks:

1. To begin, the example [gets data for the specified app](get-an-app.md).
2. Next, it [deletes the pending submission for the app](delete-an-app-submission.md), if one exists.
3. It then [creates a new submission for the app](create-an-app-submission.md) (the new submission is a copy of the last published submission).
4. It changes some details for the new submission and upload a new package for the submission to Azure Blob storage.
5. Next, it [updates](update-an-app-submission.md) and then [commits](commit-an-app-submission.md) the new submission to Partner Center.
6. Finally, it periodically [checks the status of the new submission](get-status-for-an-app-submission.md) until the submission is successfully committed.

:::code language="python" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/python/Examples.py" range="93-166":::

<span id="create-add-on-submission" />

## Create an add-on submission

The following example shows how to use several methods in the Microsoft Store submission API to create an add-on submission. To do this, the code creates a new submission as a clone of the last published submission, and then updates and commits the cloned submission to Partner Center. Specifically, the example performs these tasks:

1. To begin, the example [gets data for the specified add-on](get-an-add-on.md).
2. Next, it [deletes the pending submission for the add-on](delete-an-add-on-submission.md), if one exists.
3. It then [creates a new submission for the add-on](create-an-add-on-submission.md) (the new submission is a copy of the last published submission).
4. It uploads a ZIP archive that contains icons for the submission to Azure Blob storage. For more information, see the relevant instructions about uploading a ZIP archive to Azure Blob storage in [Create an add-on submission](manage-add-on-submissions.md#create-an-add-on-submission).
5. Next, it [updates](update-an-add-on-submission.md) and then [commits](commit-an-add-on-submission.md) the new submission to Partner Center.
6. Finally, it periodically [checks the status of the new submission](get-status-for-an-add-on-submission.md) until the submission is successfully committed.

:::code language="python" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/python/Examples.py" range="172-245":::

<span id="create-flight-submission" />

## Create a package flight submission

The following example shows how to use several methods in the Microsoft Store submission API to create a package flight submission. To do this, the code creates a new submission as a clone of the last published submission, and then updates and commits the cloned submission to Partner Center. Specifically, the example performs these tasks:

1. To begin, the example [gets data for the specified package flight](get-a-flight.md).
2. Next, it [deletes the pending submission for the package flight](delete-a-flight-submission.md), if one exists.
3. It then [creates a new submission for the package flight](create-a-flight-submission.md) (the new submission is a copy of the last published submission).
4. It uploads a new package for the submission to Azure Blob storage. For more information, see the relevant instructions about uploading a ZIP archive to Azure Blob storage in [Create a package flight submission](manage-flight-submissions.md#create-a-package-flight-submission).
5. Next, it [updates](update-a-flight-submission.md) and then [commits](commit-a-flight-submission.md) the new submission to Partner Center.
6. Finally, it periodically [checks the status of the new submission](get-status-for-a-flight-submission.md) until the submission is successfully committed.

:::code language="python" source="~/../snippets-windows/windows-uwp/monetize/StoreServicesExamples_Submission/python/Examples.py" range="251-325":::

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
