---
title: winget download command
description: Downloads an installer for a package.
ms.date: 10/02/2023
ms.topic: article
ms.localizationpriority: medium
---

# download command (winget)

The **download** command of the [winget](index.md) tool downloads the installer from the selected package. Use the [**search**](search.md) command and the [**show**](show.md) command to identify the package installer you want to download .

The **download** command requires that you specify the exact string to download. If there is any ambiguity, you will be prompted to further filter the **download** command to an exact application.

> [!NOTE]
> By default, the **download** command will download the appropriate installer to the user's Downloads folder. Use the **--download-directory** option to specify a custom download path.

> [!NOTE]
> As of March 2024, the download option does not support the msstore source.

## Usage

`winget download [[-q] <query>] [\<options>]`

![download command](./images/download.png)

## Arguments

The following arguments are available.

| Argument      | Description |
|-------------|-------------|
| **-q,--query**  |  The query used to search for an app. |

> [!NOTE]
> The query argument is positional. Wild-card style syntax is not supported. This is most often the string of characters you expect to uniquely identify the package you wish to download.

## Options

The options allow you to customize the download experience to meet your needs.

| Option  | Description |
|-------------|-------------|
| **-d, --download-directory** | Directory where the installers are downloaded to. |
| **-m, --manifest** |  Must be followed by the path to the manifest (YAML) file. |
| **--id**    |  Limits the download to the ID of the application.   |
| **--name**   |  Limits the search to the name of the application. |
| **--moniker**   | Limits the search to the moniker listed for the application. |
| **-v, --version**  |  Enables you to specify an exact version to install. If not specified, latest will download the highest versioned application. |
| **-s, --source**   |  Restricts the search to the source name provided. Must be followed by the source name. |
| **--scope**   |  Allows you to specify if the installer should target user or machine scope. See [known issues relating to package installation scope](./troubleshooting.md#scope-for-specific-user-vs-machine-wide).|
| **-a, --architecture**   |  Select the architecture to download. |
| **--installer-type**  | Select the installer type to download. |
| **-e, --exact**   |   Uses the exact string in the query, including checking for case-sensitivity. It will not use the default behavior of a substring. |
| **--locale** | Specifies which locale to use (BCP47 format). |
| **-o, --log**  |  Directs the logging to a log file. You must provide a path to a file that you have the write rights to. |
| **--ignore-security-hash** |    Ignore the installer hash check failure. Not recommended. |
| **--accept-package-agreements** | Used to accept the license agreement, and avoid the prompt. |
| **--accept-source-agreements** | Used to accept the source license agreement, and avoid the prompt. |
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

### Example queries

The following example downloads a specific version of an application from its ID.

```CMD
winget download --id Microsoft.PowerToys --version 0.15.2
```

The following example downloads an application with a specific installer type.

```CMD
winget download --id Microsoft.WingetCreate --installer-type msix
```

The following example downloads an application by architecture and scope to a specific download directory.

```CMD
winget install --id Microsoft.PowerToys --scope machine --architecture x64 --download-directory <Path>
```


## Related topics

* [Use the winget tool to install and manage applications](index.md)
* [Submit packages to Windows Package Manager](../package/index.md)
