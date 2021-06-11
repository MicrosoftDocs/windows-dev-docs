---
title: Windows Package Manager validation process
description: Provides details on the labels specified during submission process.
ms.date: 05/5/2021
ms.topic: overview
ms.localizationpriority: medium
---

# Windows Package Manager validation process

When you create a [pull request](repository.md) to submit your manifest to the Windows Package Manager repository, this will start an automation process that validates the manifest and processes your pull request. GitHub labels are used to share progress and allow you to communicate with us.

## Submission expectations

All application submissions to the Windows Package Manager repository should be well-behaved and adhere to the [Windows Package Manager repository policies](windows-package-manager-policies.md).
Here are some expectations for submissions:

- The manifest complies with the [schema requirements](manifest.md?tabs=minschema#manifest-contents).
- All URLs in the manifest lead to safe websites.

- The installer and application are virus free. The package may be identified as malware by mistake. If you believe it is a false  positive you can [submit the installer to the Microsoft Defender team for analysis](https://www.microsoft.com/wdsi/filesubmission).

- The application installs and uninstalls correctly for both administrators and non-administrators.

- The installer supports non-interactive modes.

- All manifest entries are accurate and not misleading.

- The installer comes directly from the publisher's website.

For a complete list of the policies, see [Windows Package Manager policies](.\windows-package-manager-policies.md).

## Pull request labels

During validation, we apply a series of labels to pull requests to communicate progress. Some labels will direct you to take action, while others will be directed to the Windows Package Manager engineering team.

### Status labels

The following table describes the **status labels** you might encounter.

| **Label** | **Details** |
|--------------|-------------|
| **Azure-Pipeline-Passed** | The manifest has completed the test pass. It is waiting for approval. If no issues are encountered during the test pass it will automatically be approved. If a test fails, it may be flagged for manual review. |
| **Blocking-Issue** | This label indicates that the pull request cannot be approved because there is a blocking issue. You can often tell what the blocking issue is by the included error label. |
| **Needs: Attention** | This label indicates that the pull request needs to be investigated by the Windows Package Manager development team. This is either due to a test failure that needs manual review, or a comment added to the pull request by the community. |
| **Needs: author feedback** | Indicates there is a failure with the submission. We will reassign the pull request back to you. If you do not address the issue within 10 days, the bot will close the pull request. **Needs: author feedback** labels are typically added when there was a failure with the pull request that should be updated, or if the person reviewing the pull request has a question. |
| **Validation-Completed** | Indicates that the test pass has been completed successfully and your pull request will be merged.|

### Error labels

The following table describes the **error labels** you might encounter. Not all of the error cases will be assigned to you immediately. Some may trigger manual validation.

| **Label** | **Details** |
|--------------|-------------|
| **Binary-Validation-Error** | The application included in this pull request failed to pass the **Installers Scan** test. This test is designed to ensure that the application installs on all environments without warnings. For more details on this error, see [Binary validation errors](binary-validation-errors.md). |
| **Error-Analysis-Timeout** | The **Binary-Validation-Test** test timed out. The pull request will get assigned to a Windows Package Manager engineer to investigate. |
| **Error-Hash-Mismatch** | The submitted manifest could not be processed because the **InstallerSha256** hash provided for the **InstallerURL** did not match. Update the **InstallerSha256** in the pull request and try again. |
| **Error-Installer-Availability** | The validation service was unable to download the installer. This may be related to Azure IP ranges being blocked, or the installer URL may be incorrect. Check that the **InstallerURL** is correct and try again. If you feel this has failed in error, add a comment and the pull request will get assigned to a Windows Package Manager engineer to investigate. |
| **Manifest-Path-Error** | The manifest files must be put into a specific folder structure. This label indicates a problem with the path of your submission. For example, the folder structure does not have the [required format](manifest.md?tabs=minschema%2Ccompschema). Update your manifest and path resubmit your pull request. |
| **Manifest-Validation-Error** | The submitted manifest contains a syntax error. Address the syntax issue with the manifest and re-submit. For details on the manifest format and schema, see [required format](manifest.md?tabs=minschema%2Ccompschema). |
| **PullRequest-Error** | The pull request is invalid because not all files submitted are under manifest folder or there is more than one package or version in the pull request. Update your pull request to address the issue and try again. |
| **URL-Validation-Error** | The **URLs Validation Test** could not locate the URL and responded with a [HTTP error status code](/troubleshoot/iis/http-status-code) (403 or 404), or the URL reputation test failed. You can identify which URL is in question by looking at the [pull request check details](winget-validation-troubleshooter.md). To address this issue, update the URLs in question to resolve the HTTP error status code. If the issue is not due to an HTTP error status code, you can [submit the URL for review](https://www.microsoft.com/wdsi/filesubmission/) to avoid the reputation failure. |
| **Validation-Defender-Error** | During dynamic testing, Microsoft Defender reported a problem. To reproduce this problem, install your application, then run a Microsoft Defender full scan. If you can reproduce the problem, fix the binary or [submit it for analysis](/microsoft-365/security/defender-endpoint/defender-endpoint-false-positives-negatives?#part-4-submit-a-file-for-analysis) for false positive assistance. If you are unable to reproduce the problem, add a comment to get the Windows Package Manager engineers to investigate. |
| **Validation-Domain** | The test has determined the domain if the **InstallerURL** does not match the domain expected. The Windows Package Manager policies requires that the [InstallerUrl](manifest.md?tabs=minschema%2Ccompschema) comes directly from the ISV's release location. If you believe this is a false detection, add a comment to the pull request to get the Windows Package Manager engineers to investigate. |
| **Validation-Error** | Validation of the Windows Package Manager failed during manual approval. Look at the accompanying comment for next steps. |
| **Validation-Executable-Error** | During installation testing, the test was unable to locate the primary application. Make sure the application installs correctly on all platforms. If your application does not install an application, but should still be included in the repository, add a comment to the pull request to get the Windows Package Manager engineers to investigate. |
| **Validation-Hash-Verification-Failed** | During installation testing, the application fails to install because the **InstallerSha256** no longer matches the **InstallerURL** hash. This can occur if the application is behind a vanity URL and the installer was updated without updating the **InstallerSha256**. To address this issue, update the **InstallerSha256** associated with the **InstallerURL** and submit again. |
| **Validation-HTTP-Error** | The URL used for the installer does not use the HTTPS protocol. Update the **InstallerURL** to use HTTPS and resubmit the **Pull Request.** |
| **Validation-Indirect-URL** | The URL is not coming directly from the ISVs server. Testing has determined a redirector has been used. This is not allowed because the Windows Package Manager policies require that the [InstallerUrl](manifest.md?tabs=minschema%2Ccompschema) comes directly from the ISV's release location. Remove the redirection and resubmit.
| **Validation-Installation-Error** | During manual validation of this package, there was a general error. Look at the accompanying comment for next steps. |
| **Validation-Merge-Conflict** | This package could not be validated due to a merge conflict. Please address the merge conflict and resubmit your pull request. |
| **Validation-MSIX-Dependency** | The MSIX package has a dependency on package that could not be resolved. Update the package to include the missing components or add the dependency to the manifest file and resubmit the pull request. |
| **Validation-Unapproved-URL** | The test has determined the domain if the **InstallerURL** does not match the domain expected. The Windows Package Manager policies requires that the [InstallerUrl](manifest.md?tabs=minschema%2Ccompschema) comes directly from the ISV's release location. |
| **Validation-Unattended-Failed** |  During installation, the test timed out. This most likely is due to the application not installing silently. It could also be due to some other error being encountered and stopping the test. Verify that you can install your manifest without user input. If you need assistance, add a comment to the pull request and the Windows Package Manager engineers will investigate. |
| **Validation-Uninstall-Error**  | During uninstall testing, the application did not clean up completely following uninstall. Look at the accompanying comment for more details. |
| **Validation-VCRuntime-Dependency** | The package has a dependency on the C++ runtime that could not be resolved. Update the package to include the missing components or add the dependency to the manifest file and resubmit the pull request. |

### Content policy labels

The following table lists **content policy labels**. If one of these labels is added, something in the manifest metadata triggered additional manual content review to ensure that the metadata is following the [Windows Package Manager policies](windows-package-manager-policies.md).

| **Label** | **Details** |
|--------------|-------------|
| **Policy-Test-2.1** | See [General Content Requirements](windows-package-manager-policies.md#21-general-content-requirements). |
| **Policy-Test-2.2** | See [Content Including Names, Logos, Original and Third Party](windows-package-manager-policies.md#22-content-including-names-logos-original-and-third-party) |
| **Policy-Test-2.3** | See [Risk of Harm](windows-package-manager-policies.md#23-risk-of-harm). |
| **Policy-Test-2.4** | See [Defamatory, Libelous, Slanderous and Threatening](windows-package-manager-policies.md#24-defamatory-libelous-slanderous-and-threatening). |
| **Policy-Test-2.5** | See [Offensive Content](windows-package-manager-policies.md#25-offensive-content). |
| **Policy-Test-2.6** | See [Alcohol, Tobacco, Weapons and Drugs](windows-package-manager-policies.md#26-alcohol-tobacco-weapons-and-drugs). |
| **Policy-Test-2.7** | See [Adult Content](windows-package-manager-policies.md#27-adult-content). |
| **Policy-Test-2.8** | See [Illegal Activity](windows-package-manager-policies.md#28-illegal-activity). |
| **Policy-Test-2.9** | See [Excessive Profanity and Inappropriate Content](windows-package-manager-policies.md#29-excessive-profanity-and-inappropriate-content). |
| **Policy-Test-2.10** | See [Country/Region Specific Requirements](windows-package-manager-policies.md#210-countryregion-specific-requirements). |
| **Policy-Test-2.11** | See [Age Ratings](windows-package-manager-policies.md#211-age-ratings). |
| **Policy-Test-2.12** | See [User Generated Content](windows-package-manager-policies.md#212-user-generated-content). |

### Internal labels

The following table lists internal error labels. When internal errors are encountered, your pull request will be assigned to the Windows Package
Manager engineers to investigate.

| **Label** | **Details** |
|--------------|-------------|
|**Internal-Error-Domain**| An error occurred during the domain validation of the URL. |
|**Internal-Error-Dynamic-Scan**| An error occurred during the validation of the installed binaries. |
|**Internal-Error-Keyword-Policy**| An error occurred during the validation of the manifest. |
|**Internal-Error-Manifest**| An error occurred during the validation of the manifest. |
|**Internal-Error-NoArchitectures**| An error occurred because the test could not determine the architecture if the application.|
|**Internal-Error-NoSupportedArchitectures**| An error occurred because the current architecture is not supported. |
|**Internal-Error-PR**| An error occurred during the processing of the pull request. |
|**Internal-Error-Static-Scan**| An error occurred during static analysis of the installers. |
|**Internal-Error-URL**| An error occurred during reputation validation of the installers. |
|**Internal-Error**| A generic failure or unknown error was encountered during the test pass. |
