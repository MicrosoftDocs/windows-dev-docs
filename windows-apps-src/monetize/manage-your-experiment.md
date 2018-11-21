---
Description: After you define your experiment in Partner Center and code your experiment in your app, you are ready to active your experiment and use Partner Center to review the results of your experiment.
title: Manage your experiment in Partner Center
ms.assetid: D48EE0B4-47F2-455C-8FB9-630769AC5ACE
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store Services SDK, A/B tests, experiments
ms.localizationpriority: medium
---
# Manage your experiment in Partner Center

After you [define your experiment in Partner Center](define-your-experiment-in-the-dev-center-dashboard.md) and [code your app for experimentation](code-your-experiment-in-your-app.md), you are ready to activate your experiment and use Partner Center to review the results of your experiment. After you have obtained all the data you need, you can end your experiment and choose whether to keep using the variable values in the control variation for all your apps, or switch to using the variable values in one of your other variations.

> [!NOTE]
> When you activate an experiment, Partner Center immediately starts collecting data from any apps that are instrumented to log data for your experiment. However, it can take several hours for experiment data to appear in Partner Center.

For a walkthrough that demonstrates the end-to-end process of creating and running an experiment, see [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md).

## Activate your experiment

When you are satisfied with the parameters of your experiment in Partner Center and you have updated your app code, you are ready to activate your experiment so you can start collecting experiment data from your app. When the experiment is active, your app can retrieve variation values and report view and conversion events to Partner Center.

1. Sign in to [Partner Center](https://partner.microsoft.com/dashboard).
2. Under **Your apps**, select the app with the experiment that you want to activate.
3. In the navigation pane, select **Services** and then select **Experimentation**.
4. In the table of projects in the **Projects** section, expand the project that contains your experiment and then do one of the following:
  * Click the **Activate** link for your experiment. Your experiment is added to the **Active experiments** section near the top of the page.
  * Click the experiment name, scroll to the bottom of the experiment page, and click **Activate**.

> [!IMPORTANT]
> After you activate an experiment, you can no longer modify the experiment parameters unless you clicked the **Editable experiment** check box when you created the experiment. We recommend that you code the experiment in your app before activating your experiment.

## Review the results of your experiment

1. In Partner Center, return to the **Experimentation** page for your app.
2. In the **Active experiments** section, click the name of your active experiment to go to the experiment page.
3. For an active or completed experiment, the first two sections in this page provide the results of your experiment:
  * The **Results summary** section lists your experiment goals and the conversion rate percentage for each variation.
  * The **Results details** section provides more details for each variation of all the goals in your experiment, including the views, conversions, unique users, conversion rate, delta %, confidence, and significance. The *confidence* is a statistical measure of the reliability of an estimate, which calculates the margin of error. The *significance* is a statistical measure, based on sample size, to determine the likelihood that a result is not due to chance, but is instead attributed to a specific cause.

> [!NOTE]
> Partner Center reports only the first conversion event for each user in a 24-hour time period. If a user triggers multiple conversion events in your app within a 24-hour period, only the first conversion event is reported. This is intended to help prevent a single user with many conversion events from skewing the experiment results for a sample group of users.


## Complete your experiment

1. In Partner Center, return to your experiment page. For instructions, see the previous section.
2. In the **Results summary** section, do one of the following:
  * If you want to end the experiment and continue using the variable values in the control variation in your app, click **Keep**.
  * If you want to end the experiment but switch to using the variable values in a different variation in your app, click **Switch** under the variation to which you want to switch.
3. Click **OK** to confirm that you want to end the experiment.


## Related topics

* [Create a project and define remote variables in Partner Center](create-a-project-and-define-remote-variables-in-the-dev-center-dashboard.md)
* [Code your app for experimentation](code-your-experiment-in-your-app.md)
* [Define your experiment in Partner Center](define-your-experiment-in-the-dev-center-dashboard.md)
* [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md)
* [Run app experiments with A/B testing](run-app-experiments-with-a-b-testing.md)
