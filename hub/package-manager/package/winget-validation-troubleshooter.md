---
title: Troubleshooting submissions to Windows Package Manager 
description: Provides additional help for how to troubleshooting submission errors for Windows Package Manager.
ms.date: 05/25/2021
ms.topic: overview
ms.localizationpriority: medium
---

# Troubleshooting submissions to Windows Package Manager

When Windows Package Manager is processing the manifest files in the pipeline, it [displays labels](winget-validation.md). If your pull request fails then then you may need to investigate to understand the failure better.  

This article walks you through how you can get more information on your pull request failure.

## Walkthrough of investigating a failure

1. When a pull request fails, it indicates the failure at the bottom of the web page. It will indicate a failure with the string **Some checks were not successful**. Click the **Details** link.
    ![Screenshot of a pull request failure](.\images\some-checks-were-not-successful.png).

2. After you click **Details**, you will go to an Azure Pipelines page. Click the link with the string **0 errors / 0 warnings**.
    ![Screenshot of the Azure Pipelines page](.\images\details.png).

3. The next page lists the job that failed. In the following screenshot, the failed job is **Manifest Content Validation**. Click the failed job.
    ![Screenshot of the error details](.\images\manifest-content-validation.PNG).

4. The next page displays the output for the failed job. You can use this information to debug the issue. In the following example, the failure was during **Installation Validation** task. The output should help you identify the change that needs to be made to fix the manifest.
    ![Screenshot of the failed job output](.\images\installation-validation.png).
