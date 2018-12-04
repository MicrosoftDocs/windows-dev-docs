---
Description: Before you can run an experiment in your Universal Windows Platform (UWP) app with A/B testing, you must define your experiment in Partner Center.
title: Define your experiment in Partner Center
ms.assetid: 675F2ADE-0D4B-41EB-AA4E-56B9C8F32C41
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store Services SDK, A/B tests, experiments
ms.localizationpriority: medium
---
# Define your experiment in Partner Center

After you [create a project and define remote variables in Partner Center](create-a-project-and-define-remote-variables-in-the-dev-center-dashboard.md) and [code your app for experimentation](code-your-experiment-in-your-app.md), you are ready to create an experiment in the project. When you create the experiment, you will define the goals and variations that your users will receive.

For a walkthrough that demonstrates the end-to-end process of creating and running an experiment, see [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md).

<span id="get-an-api-key" />
<span id="create-an-experiment" />

## Create your experiment

1. Sign in to [Partner Center](https://partner.microsoft.com/dashboard).
2. Under **Your apps**, select the app for which you want to create an experiment.
3. In the navigation pane, select **Services** and then select **Experimentation**.
4. On the **Experimentation** page, identify the project where you want to add an experiment in the projects table, and click the **Add experiment** link for that project.
5. In the **Experiment name** field, type a name that you can use to easily identify the experiment. After you create an experiment, this name appears in the list of existing experiments on the **Experimentation** page for your app and on the project's page.
6. If you want to edit the experiment while it is active, click the **Editable experiment** check box. Check this box only if you are creating an experiment to validate all the variations through internal testing. For more information, see [Create an experiment for internal testing](define-your-experiment-in-the-dev-center-dashboard.md#test_experiments).
    > [!NOTE]
    > Do not check this box if you are creating an experiment that you will release to customers (that is, an experiment that is associated with a project ID that is used in a version of your app that is available to customers). Editing an experiment while it is active will invalidate the experiment results.

7. In the **Project name** drop-down, the current project is automatically selected. If you want to add the new experiment to a different project, you can select that project here. Otherwise, leave this selection alone.
8.   Make note of the [Project ID](run-app-experiments-with-a-b-testing.md#terms) value. When you [code your app for experimentation](code-your-experiment-in-your-app.md), you must reference this ID in your code so you can receive variation data and report view and conversion events to Partner Center.
9. In the **View event** section, type the name of the [view event](run-app-experiments-with-a-b-testing.md#terms) for your experiment in the **View event name** field.
10. In the **Goals and conversion events** section, define at least one goal for your experiment:
  * In the **Goal name** field, type a descriptive name for your goal. After you run an experiment, this name appears in the results summary for the experiment.
  * In the **Conversion event name** field, type the name of the [conversion event](run-app-experiments-with-a-b-testing.md#terms) for this goal.
  * In the **Objective** field, choose **Maximize** or **Minimize**, depending on whether you want to maximize or minimize the occurrences of the conversion event. This information is used in the results summary for the experiment.

> [!NOTE]
> Partner Center reports only the first conversion event for each user view in a 24-hour time period. If a user triggers multiple conversion events in your app within a 24-hour period, only the first conversion event is reported. This is intended to help prevent a single user from skewing the experiment results for a sample group of users when the goal is to maximize the number of users who perform a conversion.

<span id="define-the-variations-and-settings-for-the-experiment" />

### Define the remote variables and variations for your experiment

Next, define the remote [variables](run-app-experiments-with-a-b-testing.md#terms) and [variations](run-app-experiments-with-a-b-testing.md#terms) for your experiment.

1. In the **Remote variables and variations** section, you should see two default variations, **Variation A (Control)** and **Variation B**. If you want more variations, click **Add variation**. Optionally, you can rename each variation.
2. By default, variations are distributed equally to your app users. If you want to choose a specific distribution percentage, clear the **Distribute equally** check box and type the percentages in the **Distribution (%)** row.
3. Add remote variables to your variations. In the drop-down control at the bottom of this section, choose each variable you want to add and click **Add variable**.
    > [!NOTE]
    > The variables listed in this control are inherited from the project for the experiment. The default value for the variable (as defined in the project) is automatically assigned to the control variation. If you want to create new variables that aren't listed here, go to the related project page and add the variables there.

4. Edit the variable values for each unique variation in the experiment (that is, the variations other than the control variation).

<span id="save-and-activate-your-experiment" />

### Save and activate your experiment

When you finish entering the required fields for your experiment, click **Save** to save your experiment.

If you are satisfied with the parameters of your experiment and you are ready to activate it so you can start collecting experiment data from your app, click **Activate**. When the experiment is active, your app can retrieve variation variables and report view and conversion events to Partner Center. For more information, see [Run and manage your experiment in Partner Center](manage-your-experiment.md).

> [!IMPORTANT]
> A project can only contain one active experiment at a time. After you activate an experiment, you can no longer modify the experiment parameters unless you selected the **Editable experiment** check box when you created the experiment. We recommend that you code the experiment in your app before activating your experiment.

<span id="test_experiments"/>

## Create an experiment for internal testing

You might want to test your experiment with a controlled audience (for example, a set of internal testers) and confirm that all of the variations are working as expected before you activate the experiment for your customers. You can accomplish this by creating an experiment that has the **Editable experiment** option selected.

To test your experiment before releasing it to customers, follow this process:

1. Create two projects: one for the public build of your app, and one for a private build of your app that is available only to your test audience. The following instructions refer to these projects as the public project and test project, respectively.
2. When you [code your app for experimentation](code-your-experiment-in-your-app.md), reference the project ID from your public project in the public build of your app. In the private build of your app, reference the project ID from your test project.
3. Create an experiment in the test project, and select the **Editable experiment** option for the experiment.
4. Activate the experiment in the test project. Allocate 100% distribution to one variation and verify that this variation works as expected for your testers. Repeat the process for other variations.
5. After you verify that the variations are working as expected, make any final changes to the experiment in the test project. When you are ready to release the experiment to your customers, clone the experiment to the public project. In this experiment, do not select the **Editable experiment** option.
4. Ensure that the target variation distribution is correct in the cloned experiment.
5. Activate the cloned experiment to release the experiment to your customers.

## Next steps

After you define your experiment in Partner Center and code the experiment in your app, you are ready to [run and manage your experiment in Partner Center](manage-your-experiment.md).

## Related topics

* [Create a project and define remote variables in Partner Center](create-a-project-and-define-remote-variables-in-the-dev-center-dashboard.md)
* [Code your app for experimentation](code-your-experiment-in-your-app.md)
* [Manage your experiment in Partner Center](manage-your-experiment.md)
* [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md)
* [Run app experiments with A/B testing](run-app-experiments-with-a-b-testing.md)
