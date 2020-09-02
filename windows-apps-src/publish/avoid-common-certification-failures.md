---
Description: Review this list to help avoid issues that frequently prevent apps from getting certified, or that might be identified during a spot check after the app is published.
title: Avoid common certification failures
ms.assetid: 9E9E3841-2F9B-42D4-B5F8-4C7C31E42E3D
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Avoid common certification failures


Review this list to help avoid issues that frequently prevent apps from getting certified, or that might be identified during a spot check after the app is published.

> [!NOTE]
> Be sure to review the [Microsoft Store Policies](store-policies.md) to ensure your app meets all of the requirements listed there.

-   Submit your app only when it's finished. You're welcome to use your app's description to mention upcoming features, but make sure that your app doesn't contain incomplete sections, links to web pages that are under construction, or anything else that would give a customer the impression that your app is incomplete.

-   [Test your app with the Windows App Certification Kit](../debug-test-perf/windows-app-certification-kit.md) before you submit your app.

-   Test your app on several different configurations to ensure that it's as stable as possible.

-   Ensure that your app doesn't crash without network connectivity. Even if a connection is required to actually use your app, it needs to perform appropriately when no connection is present.

-   [Provide any necessary info](notes-for-certification.md) required to use your app, such as the user name and password for a test account if your app requires users to log in to a service, or any steps required to access hidden or locked features.

-   Include a [privacy policy URL](enter-app-properties.md#privacy-policy-url) if your app requires one; for example, if your app accesses any kind of personal information in any way or is otherwise required by law. To help determine if your app requires a privacy policy, review the [App Developer Agreement](/legal/windows/agreements/app-developer-agreement) and the [Microsoft Store Policies](store-policies.md).

-   Make sure that your app's description clearly represents what your app does. For help, see our guidance on [writing a great app description](write-a-great-app-description.md).

-   Provide complete and accurate responses to all of the questions in the [Age ratings](age-ratings.md) section.

-   Don't [declare your app as accessible](product-declarations.md#this-app-has-been-tested-to-meet-accessibility-guidelines) unless you have specifically engineered and tested it for accessibility scenarios.

-   If your app uses the commerce APIs from the [**Windows.ApplicationModel.Store**](/uwp/api/Windows.ApplicationModel.Store) namespace, make sure to test the app and verify that it handles typical exceptions. Also, make sure that your app uses the [**CurrentApp**](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) class and not the [**CurrentAppSimulator**](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator) class, which is for testing purposes only. (Note that if your app targets Windows 10, version 1607 or later, we recommend that you use members of the [Windows.Services.Store](/uwp/api/windows.services.store) namespace instead of the Windows.ApplicationModel.Store namespace.)


 

 