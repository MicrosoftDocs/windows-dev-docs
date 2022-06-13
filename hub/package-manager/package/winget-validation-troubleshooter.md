---
title: Troubleshoot Windows Package Manager failures
description: Learn how to troubleshoot submission errors for Windows Package Manager by investigating pull request failure labels.
ms.date: 06/13/2022
ms.topic: troubleshooting
ms.localizationpriority: medium
ms.custom: kr2b-contr-experiment
---

# Troubleshoot Windows Package Manager failures

This article shows you how to get more information about failed Windows Package Manager manifest validation in a pipeline. Pull request processing [displays labels](winget-validation.md#pull-request-labels) to communicate validation progress. If validation fails, you can investigate the labels to understand the failure better.

To investigate validation pull request failure, take the following steps:

1. A pull request failure appears at the bottom of the web page with the string **Some checks were not successful**. Select the **Details** link next to a failed validation to go to the Azure Pipelines page.

   :::image type="content" source="images/some-checks-were-not-successful.png" alt-text="Screenshot of a pull request failure.":::

1. On the Azure Pipelines page, select the **0 errors / 0 warnings** link.

   :::image type="content" source="images/details.png" alt-text="Screenshot of the Azure Pipelines page.":::

1. On the next page, select the failed job.

   :::image type="content" source="images/fix-manifest-content-validation.png" alt-text="Screenshot of the error details.":::

1. The next page shows the output for the failed job. The output should help you identify the change you need to make to fix the manifest.

   In the following example, the failure was during the **Installation Validation** task.

   :::image type="content" source="images/fix-installation-validation.png" alt-text="Screenshot of the failed job output.":::

