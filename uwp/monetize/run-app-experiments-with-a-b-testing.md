---
description: You can use Partner Center to run experiments for your Universal Windows Platform (UWP) apps with A/B testing.
title: Run app experiments with A/B testing
ms.assetid: 790B4B37-C72D-4CEA-97AF-D226B2216DCC
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store Services SDK, A/B tests, experiments
ms.localizationpriority: medium
---
# Run app experiments with A/B testing

You can use Partner Center to define remote variables that you can retrieve at run time from your Universal Windows Platform (UWP) apps, and you can test variations of these values with your users to identify the most effective values for driving desired user behavior. Your app can use remote variables to configure app experiences such as in-app purchases, sign-up flow, captions, and ad placements.

The goal of your A/B test should be to identify a variation of your remote variable values that is likely to earn you improved conversion rates (for example, more in-app purchases) by providing a more engaging app experience. After you have identified a successful variation, you can immediately end the experiment and enable that variation for your entire user audience from Partner Center, without having to republish your app.

## Create and run an A/B test

To create and run an A/B test, follow these steps:

1. [Create a project and define remote variables in Partner Center](create-a-project-and-define-remote-variables-in-the-dev-center-dashboard.md). This project contains the variables and default variable values for your experiments.  
2. [Code your app for experimentation](code-your-experiment-in-your-app.md). Use an API in the Microsoft Store Services SDK to get remote variable values from the project you created in Partner Center, use this data to modify the behavior of the feature you are testing, and send view event and conversion events to Partner Center.
3. [Define your experiment in Partner Center ](define-your-experiment-in-the-dev-center-dashboard.md). Create an experiment in your project that defines the unique goals and variations for your A/B test.
4. [Run and manage your experiment in Partner Center dashboard](manage-your-experiment.md). Activate your experiment, and use Partner Center to review the results of the experiment and complete the experiment.

For a walkthrough that demonstrates the end-to-end process, see [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md).

## Requirements

A/B testing in Partner Center is supported only for UWP apps.

Before you can run experiments with A/B testing, you must set up your development computer:

* Follow the instructions [here](/windows/apps/get-started/get-set-up) to set up your development computer for UWP development.
* [Install the Microsoft Store Services SDK](microsoft-store-services-sdk.md#install-the-sdk). In addition to the API for experiments, this SDK also provides APIs for other features such as displaying ads and directing your customers to Feedback Hub to collect feedback on your app.

## Best practices

For the most useful results, we recommend that you follow these recommendations when running experiments with A/B testing:

* Consider running experiments with only two variations with a randomized 50/50 split distribution for variation assignments.
* Run experiments for at least 2 – 4 weeks to gather sufficient data that is statistically significant and actionable.

<span id="terms"></span>

## Related terms

|  Term  |  Definition  |
|--------|--------------|
| Project    |   A collection of remote variables with default values that your app can access by using the Microsoft Store Services SDK. A project can also optionally contain one or more experiments that share the same remote variables.  |
| Experiment    |   A set of parameters that define an A/B test that your users will receive. Experiments are defined in the scope of a project, and each experiment consists of: <p></p><ul><li>A *view event* that indicates when the user starts viewing a variation that is part of your experiment.</li><li>One or more goals with *conversion events* that indicate when an objective has been reached.</li><li>One or more *variations* that define the variable data used by your experiment. The *control* variation uses the default variable values that are defined in the project for the experiment. In addition to the control variation, experiments typically have at least one additional variation with variable values that are unique to the experiment. </li></ul>          |
| Project ID    |   A unique ID that associates your app with a project in your Partner Center account. You must use this ID to connect with the A/B testing service in your app code to receive variation data and report view and conversion events to Partner Center. For more information, see [Code your app for experimentation](code-your-experiment-in-your-app.md).<p></p><p>Each project, and all experiments in the project, are associated with exactly one project ID. You can use project IDs to help differentiate between different sets of experiments. For example, you might have one set of experiments that you release to testers in your organization and another set of experiments that you release only to external users of your app.  An app can reference multiple project IDs if it implements multiple experiments.</p>         |
| Variation    |   A collection of one or more variables that you are testing in your experiment. Every experiment must have at least one variable and two variations (including the control). An experiment can have up to five variations.           |
| Variable    |  A value that your app uses to initialize a property or some other value in your app. During an experiment, the value of the variable changes from variation to variation. After you end an experiment, the variable is assigned the value from the variation that you choose release to all users of your app. Variables can have the following types: string, Boolean, double, and integer.
| View event    |  An arbitrary string that represents an activity when the user starts viewing a variation that is part of your experiment. Typically, this is the name of an event in your code. Your app code will send this view event string to Partner Center when the user starts viewing a variation. For more information, see [Code your app for experimentation](code-your-experiment-in-your-app.md).
| Conversion event    |  An arbitrary string that represents an objective for a goal of an experiment. Typically, this is the name of an event in your code. Your app code will send this conversion event string to Partner Center when the user reaches an objective. For more information, see [Code your app for experimentation](code-your-experiment-in-your-app.md).  

## Related topics

* [Create a project and define remote variables in Partner Center](create-a-project-and-define-remote-variables-in-the-dev-center-dashboard.md)
* [Code your app for experimentation](code-your-experiment-in-your-app.md)
* [Define your experiment in Partner Center](define-your-experiment-in-the-dev-center-dashboard.md)
* [Manage your experiment in Partner Center](manage-your-experiment.md)
* [Create and run your first experiment with A/B testing](create-and-run-your-first-experiment-with-a-b-testing.md)