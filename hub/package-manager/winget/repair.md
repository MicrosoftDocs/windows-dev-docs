---
title: repair Command
description: Repairs the specified application.
ms.date: 07/15/2025
ms.topic: overview
---

# repair command (winget)

The **repair** command of the [winget](index.md) tool repairs the specified application. This is useful when an app is malfunctioning or has corrupted files but doesn't require a full reinstall.

Use the [**list**](list.md) command to identify the application you want to repair. The **repair** command requires that you specify the exact string to repair. If there is any ambiguity, you will be prompted to further filter the **repair** command to  an exact application.

## Usage

`winget repair [[-q] <query> ...] [<options>]`

:::image type="content" source="./images/repair.png" alt-text="Screenshot listing winget repair command options." lightbox="./images/repair.png":::

## Aliases

The following aliases are available for this command:

- fix

## Arguments

The following arguments are available.

| Argument      | Description |
|-------------|-------------|
| **-q,--query**  |  The query used to search for an app. |

The query argument must be provided as a positional parameter. Wildcard syntax is not supported. Typically, this is a string that uniquely identifies the package you want to install.

## Options

These options allow you to customize the install experience to meet your needs.

| Option  | Description |
|-------------|-------------|
| **-m, --manifest** |  Must be followed by the path to the manifest (YAML) file. You can use the manifest to run the repair experience from a local YAML file. |
| **--id**    |  Limits the install to the ID of the application.   |
| **--name**   |  Limits the search to the name of the application. |
| **--moniker**   | Limits the search to the moniker listed for the application. |
| **-v, --version**  |  Enables you to specify an exact version to install. If not specified, latest will install the highest versioned application. |
| **--product-code** | Filters using the product code. |
| **-a, --architecture**   |  Select the architecture to install. |
| **--scope**   |  Allows you to specify if the installer should target user or machine scope. See [known issues relating to package installation scope](./troubleshooting.md#scope-for-specific-user-vs-machine-wide).|
| **-s, --source**   |  Restricts the search to the source name provided. Must be followed by the source name. |
| **-i, --interactive** |  Runs the installer in interactive mode. The default experience shows installer progress. |
| **-h, --silent** |  Runs the installer in silent mode. This suppresses all UI. The default experience shows installer progress. |
| **-o, --log**  |  Directs the logging to a log file. You must provide a path to a file that you have the write rights to. |
| **--ignore-local-archive-malware-scan** |    Ignore the malware scan performed as part of installing an archive type package from local manifest. |
| **--accept-source-agreements** | Used to accept the source license agreement, and avoid the prompt. |
| **--accept-package-agreements** | Used to accept the license agreement, and avoid the prompt. |
| **--locale** | Specifies which locale to use (BCP47 format). |
| **--header** | Optional Windows-Package-Manager REST source HTTP header. |
| **--authentication-mode** | Specify authentication window preference (silent, silentPreferred or interactive). |
| **--authentication-account** | Specify the account to be used for authentication. |
| **--force** | Direct run the command and continue with non security related issues. |
| **--ignore-security-hash** |    Ignore the installer hash check failure. Not recommended. |
| **-e, --exact**   |   Uses the exact string in the query, including checking for case-sensitivity. It will not use the default behavior of a substring. |
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--nowarn,--ignore-warnings** | Suppresses warning outputs. |
| **--disable-interactivity** | Disable interactive prompts. |
| **--proxy** | Set a proxy to use for this execution. |
| **--no-proxy** | Disable the use of proxy for this execution. |

### Example queries

The following example repairs an application.

```CMD
winget repair Micrososft.WinGetCreate
```

## Related topics

* [Use the winget tool to install and manage applications](index.md)
