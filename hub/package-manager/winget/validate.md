---
title: winget validate Command
description: Validates a manifest file for submitting software to the Microsoft Community Package Manifest Repository on GitHub.
ms.date: 12/11/2024
ms.topic: article
ms.localizationpriority: medium
---

# validate command (winget)

The **validate** command of the [winget](index.md) tool validates a [manifest](../package/manifest.md) for submitting software to the **Microsoft Community Package Manifest Repository** on GitHub. 

A few different tools are available to help you author a manifest, including Windows Package Manager Manifest Creator, YamlCreate.ps1, and tools authored by the user community. See [Authoring a Manifest in the winget-pkgs repo on GitHub](https://github.com/microsoft/winget-pkgs/blob/master/doc/README.md#authoring-a-manifest).

## Usage

`winget validate [--manifest] <manifest> [<options>]`

## Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **--manifest** |  The path to the manifest to be validated. |

## Options

The options allow you to customize the validate experience to meet your needs.

| Option  | Description |
|-------------|-------------|
| **-?,--help** | Shows help about the selected command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--nowarn,--ignore-warnings** | Suppresses warning outputs. |
| **--disable-interactivity** | Disable interactive prompts. |
| **--proxy** | Set a proxy to use for this execution. |
| **--no-proxy** | Disable the use of proxy for this execution. |

## Related topics

* [Use the winget tool to install and manage applications](index.md)
* [Submit packages to Windows Package Manager](../package/index.md)
