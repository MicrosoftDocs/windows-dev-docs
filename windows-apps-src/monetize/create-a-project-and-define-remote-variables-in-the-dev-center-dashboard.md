---
Description: Before you can run an experiment in your Universal Windows Platform (UWP) app with A/B testing, you must create a project and define your remote variables in Partner Center.
title: Create an experiment project in Partner Center
ms.assetid: C3809FF1-0A6A-4715-B989-BE9D0E8C9013
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store Services SDK, A/B tests, experiments
ms.localizationpriority: medium
---
# Create an experiment project in Partner Center

To get started with experimentation, create an experimentation [project](run-app-experiments-with-a-b-testing.md#terms) for your app in Partner Center and define the remote variables that your app can access.

The following instructions describe the core steps to create a project. For a detailed walkthrough that demonstrates the end-to-end process of creating a project and then running an experiment, see [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md).

## Instructions

1. Sign in to [Partner Center](https://partner.microsoft.com/dashboard).
2. Under **Your apps**, select the app for which you want to create an experiment.
3. In the navigation pane, select **Services** and then select **Experimentation**.
4. On the **Experimentation** page, click the **New project** button in the **Projects** section. If you have already created one or more projects, those projects are listed in the **Projects** section.
5. In the **New project** page, enter a name for your new project.
6. In the **Remote variables** section, add the [variables](run-app-experiments-with-a-b-testing.md#terms) that you want to be available to all experiments in this project, and define default values for each variable. The default values you specify here are used for the control group of the experiments, and for any users who do not participate in the experiment.
  1. If the **Remote variables** section is collapsed, click **Show** on the section heading.
  2. Click **Add variable** to create each new variable that you want to be available to any experiment in this project, and type the variable name and the default value of the variable.
  3. When you are done adding variables, click **Save**.
3. In the **SDK integration** section, make note of the [Project ID](run-app-experiments-with-a-b-testing.md#terms) value. When you [code your app for experimentation](code-your-experiment-in-your-app.md), you must reference this project ID in your code so you can receive variation data and report view and conversion events to Partner Center.

> [!NOTE]
> You cannot edit, add, or remove remote variables while an experiment in the project is active. This limitation helps protect the integrity of the data for the control group for the active experiment.


## Next steps

After you create a project, you can [code your app for experimentation](code-your-experiment-in-your-app.md) to start retrieving remote variable values in your app, and you can [create an experiment in the project](define-your-experiment-in-the-dev-center-dashboard.md).

## Related topics

* [Code your app for experimentation](code-your-experiment-in-your-app.md)
* [Define your experiment in Partner Center](define-your-experiment-in-the-dev-center-dashboard.md)
* [Manage your experiment in Partner Center](manage-your-experiment.md)
* [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md)
* [Run app experiments with A/B testing](run-app-experiments-with-a-b-testing.md)
