---
title: '`search` Command'
description: Queries the sources for available applications that can be installed
ms.date: 07/19/2026
no-loc: [winget, search, find]
ms.topic: overview
---

# search command (winget)

The `search` command of [`winget`](index.md) can be used to show all applications available for installation. It can also be used to identify the string or ID needed to install a specific application.

For example, the command `winget search vscode` will return all applications available that include "vscode" in the name, id, description, moniker or tags.

The `search` command includes parameters for filtering down the applications returned to help you identify the specific application you are looking for, including: `--id`, `--name`, `--moniker`, `--tag`, `--command`, or `--source`. See descriptions [below](#search-strings) or use `winget search --help` in your command line.

## Usage

```cmd
winget search [[-q] <query>] [<options>]
```

:::image type="content" source="./images/search.png" alt-text="Screenshot Windows PowerShell displaying the results of the winget search command." lightbox="./images/search.png":::

## Aliases

The following aliases are available for this command:

- `find`

## Arguments

The following arguments are available.

|      Argument       |                                                                                                 Description                                                                                                  |
|---------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **`-q`, `--query`** | The query flag is the default argument used to search for an app. It does not need to be specified. Entering the command `winget search foo` will default to using `--query` so including it is unnecessary. |

> [!NOTE]
> The query argument is positional. winget performs a **case-insensitive substring match** against the package name, ID, and moniker. Wild-card syntax is not supported. Use **`-e`, `--exact`** to require an exact, case-sensitive match. Use **`--id`**, **`--name`**, or **`--moniker`** to restrict which field is searched.

## Show all

To show all of the winget packages available, use the command:

```cmd
winget search --query ""
```

In Windows PowerShell, you will need to escape the quotes, so this command becomes:

```powershell
winget search -q `"`"
```

> [!NOTE]
> This is a change from previous versions of winget which supported `winget search` with no filters or options displaying all available packages. You can also search for all applications in another source by passing in the **`--source`** option.

## Search strings

Search strings can be filtered with the following options.

|               Option                |                                                            Description                                                            |
|-------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| **`--id`**                          | Limits the search to the ID of the application. The ID includes the publisher and the application name.                           |
| **`--name`**                        | Limits the search to the name of the application.                                                                                 |
| **`--moniker`**                     | Limits the search to the moniker specified.                                                                                       |
| **`--tag`**                         | Limits the search to the tags listed for the application.                                                                         |
| **`--cmd`, `--command`**            | Limits the search to the commands listed for the application.                                                                     |
| **`-s`, `--source`**                | Find package using the specified [source](source.md) name.                                                                        |
| **`-n`, `--count`**                 | Show no more than specified number of results (between 1 and 1000).                                                               |
| **`-e`, `--exact`**                 | Uses the exact string in the query, including checking for case-sensitivity. It will not use the default behavior of a substring. |
| **`--header`**                      | Optional Windows-Package-Manager REST source HTTP header.                                                                         |
| **`--authentication-mode`**         | Specify authentication window preference (silent, silentPreferred or interactive).                                                |
| **`--authentication-account`**      | Specify the account to be used for authentication.                                                                                |
| **`--accept-source-agreements`**    | Accept all source agreements during source operations.                                                                            |
| **`--versions`**                    | Show available versions of the package.                                                                                           |
| **`-?`, `--help`**                  | Gets additional help on this command.                                                                                             |
| **`--wait`**                        | Prompts the user to press any key before exiting.                                                                                 |
| **`--logs`, `--open-logs`**         | Open the default logs location.                                                                                                   |
| **`--verbose`, `--verbose-logs`**   | Used to override the logging setting and create a verbose log.                                                                    |
| **`--nowarn`, `--ignore-warnings`** | Suppresses warning outputs.                                                                                                       |
| **`--disable-interactivity`**       | Disable interactive prompts.                                                                                                      |
| **`--proxy`**                       | Set a proxy to use for this execution.                                                                                            |
| **`--no-proxy`**                    | Disable the use of proxy for this execution.                                                                                      |

The string will be treated as a substring. The search by default is also case insensitive. For example, `winget search micro` could return the following:

* Microsoft
* Microscope
* MyMicro

## Searching across multiple sources

If you want to narrow results down to a specific source, just pass the **`--source`** or **`-s`** parameter and specify what you want. For example, you might want to see if Visual Studio Code is in the store by running `winget search "Visual Studio Code" -s msstore`. This search uses "Visual Studio Code" as the query.

## Related topics

* [Use the winget tool to install and manage applications](index.md)
