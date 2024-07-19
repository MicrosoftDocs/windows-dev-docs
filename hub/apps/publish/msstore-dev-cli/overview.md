---
description: The Microsoft Store Command Line Interface is a cross-platform CLI that helps developers access the Microsoft Store APIs, for both managed, as well as unmanaged applications.
title: Microsoft Store Developer CLI (preview)
ms.date: 11/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Microsoft Store Developer CLI (preview)

The Microsoft Store Command Line Interface is a cross-platform (Windows, macOS, Linux) CLI that helps developers publish their applications to the Microsoft Store. It allows developers to locally configure their applications projects to publish to the Microsoft Store, as well as actually publish their applications' packages to the Microsoft Store, automatically calling the right [Partner Center APIs](/partner-center/develop/partner-center-rest-api-reference) to upload its packages.


> [!Important]
> **The Microsoft Store Developer CLI** is currently in preview, and we are looking for feedback from developers. Please [open an issue](https://github.com/microsoft/msstore-cli/issues) if you have any feedback or issues.

[Sign up today!](https://developer.microsoft.com/store/register)

## Prerequisites

To use the Microsoft Store Developer CLI, you'll need to:
- [Register as a Windows app developer in Partner Center](/windows/apps/publish/partner-center/partner-center-developer-account)
- Have a tenant associated with your Partner Center account. You can achieve that by either [associating an existing Azure AD in Partner Center](/windows/apps/publish/partner-center/associate-existing-azure-ad-tenant-with-partner-center-account) or by [creating a new Azure AD in Partner Center](/windows/apps/publish/partner-center/create-new-azure-ad-tenant).

## Installation

The Microsoft Store Developer CLI (preview) supports Windows 10+, Linux, and macOS:

[Install the Microsoft Store Developer CLI (preview) now!](install.md)

## Getting Started

After installing the Microsoft Store Developer CLI, you have to configure your environment to be able to run commands. You can do this by simply running the CLI for the first time. The CLI will guide you through the configuration process:

```console
msstore
```

> [!Important]
> When signing in, don't use your MSA! The **Microsoft Store Developer CLI** requires you to use your **Azure AD credentials**. You can find more information about this in our [prerequisites](#prerequisites) section.

Running in CI environments is also supported, and the Microsoft Store Developer CLI (preview) can be used in your CI/CD pipelines to, for example, automatically publish your applications to the Microsoft Store. More instructions on how to do this can be found [here](ci-cd-environments.md).

## Commands

These are the Microsoft Store Developer CLI (preview) available commands:

| Command                                                                                                     | Description          |
|-------------------------------------------------------------------------------------------------------------|----------------------|
| [info](info-command.md) | Print existing configuration. |
| [reconfigure](reconfigure-command.md) | Re-configure the Microsoft Store Developer CLI. |
| [settings](settings-command.md) | Change settings of the Microsoft Store Developer CLI. |
| [apps](apps-command.md) | Application related commands, such as listing the applications in your account and retrieving the application's details. |
| [submission](submission-command.md) | Submission related commands, such as 'status', 'get', 'getListingAssets', 'updateMetadata', 'update', 'poll', 'publish', 'delete'. |
| [flights](flights-command.md) | Execute flights related tasks. |
| [init](init-command.md) | Helps you setup your application to publish to the Microsoft Store. |
| [package](package-command.md) | Helps you package your Microsoft Store Application as an MSIX. |
| [publish](publish-command.md) | Publishes your application to the Microsoft Store. |