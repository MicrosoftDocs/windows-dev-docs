---
title: Submit your manifest to the repository
description: 
author: denelon
ms.author: denelon
ms.date: 04/29/2020
ms.topic: article
ms.localizationpriority: medium
---

# Submit your manifest to the repository

[!INCLUDE [preview-note](../../includes/package-manager-preview.md)]

After you create a [package manifest](manifest.md) that describes your application, you're ready to submit your manifest to the Windows Package Manager repository. This a public-facing repository that contains a collection of manifests that the **winget** tool can access. To submit your manifest, you'll upload it to the open source [https://github.com/microsoft/winget-pkgs](https://github.com/microsoft/winget-pkgs) repository on GitHub.

After you submit a pull request to add a new manifest to the GitHub repository, an automated process will validate your manifest file and check to make sure the package is not known to be malicious. If this validation is successful, your package will be added to the public-facing Windows Package Manager repository so it can be discovered by the **winget** client tool. Note the distinction between the manifests in the open source GitHub repository and the public-facing Windows Package Manager repository.

> [!IMPORTANT]
> Microsoft reserves the right to refuse a submission for any reason.

## Third-party repositories

There are currently no known third party repositories. Microsoft is working with multiple partners to develop protocols or an API to enable third party repositories.

## Manifest validation

When you submit a manifest to the [https://github.com/microsoft/winget-pkgs](https://github.com/microsoft/winget-pkgs) repository on GitHub, your manifest will be automatically validated and evaluated for the safety of the Windows ecosystem. Manifests may also be reviewed manually.

## How to submit your manifest

To submit a manifest to the repository, follow these steps.

### Step 1: Validate your manifest

The **winget** tool provides the [validate](..\winget\validate.md) command to confirm that you have created your manifest correctly. To validate your manifest, use this command.

```CMD
winget vaidate \<manifest-file>
```

If your validation fails, use the errors to locate the line number and make a correction. After your manifest is validated, you can submit it to the repository.

### Step 2: Clone the repository

Next, create a fork of the repository and clone it.

1. Go to [https://github.com/microsoft/winget-pkgs](https://github.com/microsoft/winget-pkgs) in your browser and click **Fork**.
    ![picture of fork](images\fork.png)

2. From a command line environment such as the Windows Command Prompt or PowerShell, use the following command to clone your fork.
    ```CMD
    git clone \<your-fork-name>
    ```

 3. If you are making multiple submissions, make a branch instead of a fork. We currently allow only one manifest file per submission.
    ```CMD
    git checkout -b \<branch-name>
    ```

### Step 3: Add your manifest to the local repository

You must add your manifest file to the repository in the following folder structure:

**manifests** / **publisher** / **application** / **version.yaml**

* The **manifests** folder is the root folder for all manifests in the repository.
* The **publisher** folder is the name of the company that publishes the software. For example, **Microsoft**.
* The **application** folder is the name of the application or tool. For example, **VSCode**.
* **version.yaml** is the file name of the manifest. The file name must be set to the current version of the application. For example, **1.0.0.yaml**.

>[!IMPORTANT]
> The `Id` value in the manifest must match the publisher and application names in the manifest folder path, and the `version` value in the manifest must match the version in the file name. For more information, see [Create your package manifest](manifest.md#tips-and-best-practices).

### Step 4: Submit your manifest to the remote repository

You're now ready to push your new manifest to the remote repository.

1. Use the `add` command to prepare for submission.
    ```CMD
    git add manifests\Contoso\ContosoApp\1.0.0.yaml
    ```

2. Use the `commit` command to commit the change and provide information on the submission.
    ```CMD
    git commit -m "Submitting  ContosoApp version 1.0.0.yaml"
    ```

3. Use the `push` command to push the changes the remote repository.
    ```CMD
    `git push`
    ```

### Step 5: Create a pull request

After you push your changes, return to [https://github.com/microsoft/winget-pkgs](https://github.com/microsoft/winget-pkgs) and create a pull request to merge your fork or branch to the **master** branch.

![picture of pull request tab](images\pull-request.png)

## Validation process

When you create a pull request, this will start an automation process that validates the manifest and processes your pull request. We add labels to your pull request so you cab track progress.

### Validation steps

Our validation process includes these verification steps.

* Check the manifest file for valid YAML.
* Check that the manifest complies with [our specification](manifest-specification.md). Certain fields are required. Missing fields or unsupported fields will cause the bot to reject the package.
* Analyze every URL in the manifest using SmartScreen.
* Evaluate each installer wfor viruses, and make sure the installer matches the provided hash. Following the approval of the manifest, the installer and binaries are analyzed further.
* Confirm the package installs correctly. Applications must successfully install whether the user is installing from a regular command prompt or with administrator privileges.
* Check whether the installers install silently.
    > [!NOTE]
    > The current preview of Windows Package Manager only supports packages that support a silent or passive install.
* Confirm that the tools installed by the installer match those implied by the manifest.
* Confirm that the specified publisher and application name are correct. Manifests should accurately identify the publisher and application name.
* Confirm that the URL the installer is installing from is a good distribution point for the publisher.
* Check the installed package for viruses.
* Confirm the package uninstalls correctly.

### Pull request labels

During validation, we apply a series of labels to our pull request to communicate progress.

* **Needs: author feedback**: This label indicates there was a failure with the submission. We will reassign pull request back to you. If you do not address the issue within 10 days, we will close the pull request.
* **AzurePipelinePassed**: This label indicates that the manifest has completed the first portion of validation. After this step, your pull request is assigned to our test team for final validation.
* **Validation Completed**: This label indicates that the validation is complete and your pull request will be merged.

![Labels](images\labels.png)
