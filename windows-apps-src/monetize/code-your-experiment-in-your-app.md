---
Description: To run an experiment in your Universal Windows Platform (UWP) app with A/B testing, you must code the experiment in your app.
title: Code your app for experimentation
ms.assetid: 6A5063E1-28CD-4087-A4FA-FBB511E9CED5
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store Services SDK, A/B tests, experiments
ms.localizationpriority: medium
---
# Code your app for experimentation

After you [create a project and define remote variables in Partner Center](create-a-project-and-define-remote-variables-in-the-dev-center-dashboard.md), you are ready to update the code in your Universal Windows Platform (UWP) app to:
* Receive remote variable values from Partner Center.
* Use remote variables to configure app experiences for your users.
* Log events to Partner Center that indicate when users have viewed your experiment and performed a desired action (also called a *conversion*).

To add this behavior to your app, you'll use APIs provided by the Microsoft Store Services SDK.

The following sections describe the general process of getting variations for your experiment and logging events to Partner Center. After you code your app for experimentation, you can [define an experiment in Partner Center](define-your-experiment-in-the-dev-center-dashboard.md). For a walkthrough that demonstrates the end-to-end process of creating and running an experiment, see [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md).

> [!NOTE]
> Some of the experimentation APIs in the Microsoft Store Services SDK use the [asynchronous pattern](../threading-async/asynchronous-programming-universal-windows-platform-apps.md) to retrieve data from Partner Center. This means that part of the execution of these methods may take place after the methods are invoked, so your app's UI can remain responsive while the operations complete. The asynchronous pattern requires your app to use the **async** keyword and **await** operator when calling the APIs, as demonstrated by the code examples in this article. By convention, asynchronous methods end with **Async**.

## Configure your project

To get started, install the Microsoft Store Services SDK on your development computer and add the necessary references to your project.

1. [Install the Microsoft Store Services SDK](microsoft-store-services-sdk.md#install-the-sdk).
2. Open your project in Visual Studio.
3. In Solution Explorer, expand your project node, right-click **References**, and click **Add Reference**.
3. In **Reference Manager**, expand **Universal Windows** and click **Extensions**.
4. In the list of SDKs, select the check box next to **Microsoft Engagement Framework** and click **OK**.

> [!NOTE]
> The code examples in this article assume that your code file has **using** statements for the **System.Threading.Tasks** and **Microsoft.Services.Store.Engagement** namespaces.

## Get variation data and log the view event for your experiment

In your project, locate the code for the feature that you want to modify in your experiment. Add code that retrieves data for a variation, use this data to modify the behavior of the feature you are testing, and then log the view event for your experiment to the A/B testing service in Partner Center.

The specific code you need will depend on your app, but the following example demonstrates the basic process. For a complete code example, see [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/ExperimentExamples.cs" id="ExperimentCodeSample":::

The following steps describe the important parts of this process in detail.

1. Declare a [StoreServicesExperimentVariation](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation) object that represents the current variation assignment and a [StoreServicesCustomEventLogger](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger) object that you will use to log view and conversion events to Partner Center.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/ExperimentExamples.cs" id="Snippet1":::

2. Declare a string variable that is assigned to the [project ID](run-app-experiments-with-a-b-testing.md#terms) for the experiment you want to retrieve.
    > [!NOTE]
    > You obtain a project ID when you [create a project in Partner Center](create-a-project-and-define-remote-variables-in-the-dev-center-dashboard.md). The project ID shown below is for example purposes only.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/ExperimentExamples.cs" id="Snippet2":::

3. Get the current cached variation assignment for your experiment by calling the static [GetCachedVariationAsync](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation.getcachedvariationasync) method, and pass the project ID for your experiment to the method. This method returns a [StoreServicesExperimentVariationResult](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariationresult) object that provides access to the variation assignment via the [ExperimentVariation](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariationresult.experimentvariation) property.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/ExperimentExamples.cs" id="Snippet3":::

4. Check the [IsStale](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation.isstale) property to determine whether the cached variation assignment needs to be refreshed with a remote variation assignment from the server. If it does need to be refreshed, call the static [GetRefreshedVariationAsync](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation.getrefreshedvariationasync) method to check for an updated variation assignment from the server and refresh the local cached variation.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/ExperimentExamples.cs" id="Snippet4":::

5. Use the [GetBoolean](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation.getboolean), [GetDouble](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation.getdouble), [GetInt32](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation.getint32), or [GetString](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation.getstring) methods of the [StoreServicesExperimentVariation](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation) object to get the values for the variation assignment. In each method, the first parameter is the name of the variation that you want to retrieve (this is the same name of a variation that you enter in Partner Center). The second parameter is the default value that the method should return if it is not able to retrieve the specified value from Partner Center (for example, if there is no network connectivity), and a cached version of the variation is not available.

    The following example uses [GetString](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation.getstring) to get a variable named *buttonText* and specifies a default variable value of **Grey Button**.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/ExperimentExamples.cs" id="Snippet5":::

6. In your code, use the variable values to modify the behavior of the feature you are testing. For example, the following code assigns the *buttonText* value to the content of a button in your app. This example assumes you have already defined this button elsewhere in your project.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/ExperimentExamples.cs" id="Snippet6":::

7. Finally, log the [view event](run-app-experiments-with-a-b-testing.md#terms) for your experiment to the A/B testing service in Partner Center. Initialize the ```logger``` field to a [StoreServicesCustomEventLogger](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger) object and call the [LogForVariation](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger.logforvariation) method. Pass the [StoreServicesExperimentVariation](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation) object that represents the current variation assignment (this object provides context about the event to Partner Center) and the name of the view event for your experiment. This must match the view event name that you enter for your experiment in Partner Center. Your code should log the view event when the user starts viewing a variation that is part of your experiment.

    The following example shows how to log a view event named **userViewedButton**. In this example, the goal of the experiment is to get the user to click a button in the app, so the view event is logged after the app has retrieved the variation data (in this case, the button text) and assigned it to the content of the button.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/ExperimentExamples.cs" id="Snippet7":::

## Log conversion events to Partner Center

Next, add code that logs [conversion events](run-app-experiments-with-a-b-testing.md#terms) to the A/B testing service in Partner Center. Your code should log a conversion event when the user reaches an objective for your experiment. The specific code you need will depend on your app, but here are the general steps. For a complete code example, see [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md).

1. In code that runs when the user reaches an objective for one of the goals of the experiment, call the [LogForVariation](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger.logforvariation) method again and pass the [StoreServicesExperimentVariation](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation) object and the name of a conversion event for your experiment. This must match one of the conversion event names that you enter for your experiment in Partner Center.

    The following example logs a conversion event named **userClickedButton** from the **Click** event handler for a button. In this example, the goal of the experiment is to get the user to click the button.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/ExperimentExamples.cs" id="Snippet8":::

## Next steps

After you code the experiment in your app, you are ready for the following steps:
1. [Define your experiment in Partner Center](define-your-experiment-in-the-dev-center-dashboard.md). Create an experiment that defines the view events, conversion events, and unique variations for your A/B test.
2. [Run and manage your experiment in Partner Center](manage-your-experiment.md).


## Related topics

* [Create a project and define remote variables in Partner Center](create-a-project-and-define-remote-variables-in-the-dev-center-dashboard.md)
* [Define your experiment in Partner Center](define-your-experiment-in-the-dev-center-dashboard.md)
* [Manage your experiment in Partner Center](manage-your-experiment.md)
* [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md)
* [Run app experiments with A/B testing](run-app-experiments-with-a-b-testing.md)
