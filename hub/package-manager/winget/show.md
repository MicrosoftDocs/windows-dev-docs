---
title: show Command
description: Displays details for the specified application, including details on the source of the application as well as the metadata associated with the application.
ms.date: 04/28/2020
ms.topic: overview
ms.localizationpriority: medium
---

# show command (winget)

The **show** command of the [winget](index.md) tool displays details for the specified application, including details on the source of the application as well as the metadata associated with the application.

The **show** command only shows metadata that was submitted with the application. If the submitted application excludes some metadata, then the data will not be displayed.

## Aliases

The following aliases are available for this command:

- view

## Usage

`winget show [[-q] \<query>] [\<options>]`

![show command](./images/show.png)

## Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **-q,--query** |  The query used to search for an application. |

> [!NOTE]
> The query argument is positional. Wild-card style syntax is not supported. This is most often the string of characters you expect to help find the package you are searching for.

## Options

The following options are available.

| Option  | Description |
|--------------|-------------|
| **-m,--manifest** | The path to the manifest of the application to show. |
| **--id**         |  Filter results by ID. |
| **--name**   |      Filter results by name. |
| **--moniker**   |  Filter results by application moniker. |
| **-v,--version** |  Use the specified version. The default is the latest version. |
| **-s,--source** |   Find the application using the specified [source](source.md). |
| **-e,--exact**     | Find the application using exact match. |
| **--scope**     | Select install scope (user or machine). |
| **-a,--architecture**     | Select the architecture to show. |
| **--installer-type**   |  Select the installer type to show. See [supported installer types for WinGet client](./index.md#supported-installer-formats). |
| **--locale**     | Locale to use (BCP47 format). |
| **--versions**    | Show available versions of the application. |
| **--header** | Optional Windows-Package-Manager REST source HTTP header. |
| **--accept-source-agreements** | Used to accept the source license agreement, and avoid the prompt. |
| **-?,--help** | Shows help about the selected command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

## Multiple selections

If the query provided to **winget** does not result in a single application, then **winget** will display the results of the search. This will provide you with the additional data necessary to refine the search.

## Results of show

If a single application is detected, the following data will be displayed.

### Metadata

| Value  | Description |
|--------------|-------------|
| **Version** | Version of the application. |
| **Publisher** | Publisher of the application. |
| **Moniker** | AppMoniker of the application. |
| **Description** | Description of the application. |
| **Homepage**  | Homepage of the application. |
| **License**  | License of the application. |
| **LicenseUrl** | The URL to the license file of the application. |

### Installer details

| Value  | Description |
|--------------|-------------|
| **Type**  | The type of installer. |
| **Download Url** | The Url of the installer. |
| **SHA256** | The Sha-256 of the installer.  |

## Related topics

* [Use the winget tool to install and manage applications](./index.md)
