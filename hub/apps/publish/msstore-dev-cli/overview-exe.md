---
description: The Microsoft Store Command Line Interface for MSI/EXE apps is a cross-platform CLI that helps developers access the Microsoft Store APIs, for both managed, as well as unmanaged applications.
title: Microsoft Store Developer CLI (MSI/EXE)
ms.date: 09/08/2026
ms.topic: article
ms.localizationpriority: medium
---

# Microsoft Store Developer CLI (MSI/EXE)

> [!NOTE]
> This page covers MSI/EXE app publishing using Microsoft Store Developer CLI. For information on MSIX app publishing using Microsoft Store Developer CLI, click [here.](./overview.md)

The Microsoft Store Command Line Interface is a cross-platform (Windows, macOS, Linux) CLI that helps developers publish their applications to the Microsoft Store. It allows developers to locally configure their applications projects to publish to the Microsoft Store, as well as actually publish their applications' packages to the Microsoft Store, automatically calling the right [Partner Center APIs](/partner-center/develop/partner-center-rest-api-reference) to upload its packages.

> [!IMPORTANT]
> You can use the Microsoft Store Developer CLI only to update an app that is already published in the Microsoft Store. You can't use the CLI to create an app or to complete its first submission. Reserve the app's name and complete its first submission in Partner Center, and then use the CLI for later submissions. The [Microsoft Store submission API for MSI or EXE app](../store-submission-api.md) has the same requirement.

The process to use Store Developer CLI is the same for both MSI/EXE and MSIX apps. So, to understand how to use it, check out the following video:

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=cbc4aad9-e79b-4ff6-a30b-c2399c2624a3]

## Prerequisites

To use the Microsoft Store Developer CLI, you'll need to:

- [Register as a Windows app developer in Partner Center](/windows/apps/publish/partner-center/partner-center-developer-account)
- Have a tenant associated with your Partner Center account. You can achieve that by either [associating an existing Microsoft Entra ID in Partner Center](/windows/apps/publish/partner-center/associate-existing-azure-ad-tenant-with-partner-center-account) or by [creating a new Microsoft Entra ID in Partner Center](/windows/apps/publish/partner-center/create-new-azure-ad-tenant).
- Have your app already created in Partner Center. If your app doesn't exist yet, [create your app by reserving its name](../publish-your-app/msi/reserve-your-apps-name.md) in Partner Center. The CLI can't create an app for you.
- Have completed the app's first submission in Partner Center. [Create one submission for the app](../publish-your-app/msi/create-app-submission.md) in Partner Center, including the [age ratings](../publish-your-app/msi/age-ratings.md) questionnaire, and publish it. After that submission is complete, you can use the CLI to create and publish later submissions for the app.

## Installation

The Microsoft Store Developer CLI supports Windows 10+, Linux, and macOS:

[Install the Microsoft Store Developer CLI (preview) now!](./commands-exe.md#installation)

## Getting Started

After installing the Microsoft Store Developer CLI, you have to configure your environment to be able to run commands. You can do this by simply running the CLI for the first time. The CLI will guide you through the configuration process:

```console
msstore
```

> [!Important]
> When signing in, don't use your MSA! The **Microsoft Store Developer CLI** requires you to use your **Microsoft Entra ID credentials**. You can find more information about this in our [prerequisites](#prerequisites) section.

Running in CI environments is also supported, and the Microsoft Store Developer CLI (preview) can be used in your CI/CD pipelines to, for example, automatically publish your applications to the Microsoft Store. More instructions on how to do this can be found [here](./commands.md#cicd-environments).

> [!NOTE]
> App update operations through Microsoft Store Developer CLI is currently supported for free products only. Paid products will be supported in a future release.

## Commands

These are the Microsoft Store Developer CLI available commands:

| Command                                                | Description                                                                                                                        |
| ------------------------------------------------------ | ---------------------------------------------------------------------------------------------------------------------------------- |
| [info](./commands-exe.md#info-command)               | Print existing configuration.                                                                                                      |
| [reconfigure](./commands-exe.md#reconfigure-command) | Re-configure the Microsoft Store Developer CLI.                                                                                    |
| [settings](./commands-exe.md#settings-command)       | Change settings of the Microsoft Store Developer CLI.                                                                              |
| [submission](./commands-exe.md#submission-command)   | Submission related commands, such as 'status', 'get', 'getListingAssets', 'updateMetadata', 'update', 'poll', 'publish'. |

For more info, see: [Commands](commands-exe.md).
