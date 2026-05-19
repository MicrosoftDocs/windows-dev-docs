---
title: list Command
description: Displays the list of listed apps and if an update is available.
ms.date: 03/24/2026
no-loc: [winget, list]
ms.topic: overview
---

# list command (winget)

The **list** command of [WinGet](./index.md) displays a list of the applications currently installed on your computer. The list command will show apps that were installed through the Windows Package Manager as well as apps that were installed by other means.

The **list** command will also display if an update is available for an app, and you can use the [**upgrade**](./upgrade.md) command to update the app.

The **list** command also supports filters which can be used to limit your list query.

## Aliases

The following aliases are available for this command:

- `ls`

## Usage

`winget list [[-q] <query>] [<options>]`

:::image type="content" source="./images/list.png" alt-text="Screenshot listing winget list command options." lightbox="./images/list.png":::

To list all apps with available updates, use the command: `winget list --upgrade-available` (without any arguments).

## Arguments

The following arguments are available.

| Argument      | Description |
|-------------|-------------|
| **-q,--query**  |  The query used to search for an app. |

> [!NOTE]
> The query argument is positional. Wild-card style syntax is not supported. This is most often the string of characters you expect to help find the installed package you are searching for.

## Options

The options allow you to customize the list experience to meet your needs.

| Option      | Description |
|-------------|-------------|
| **--id**    |  Limits the list to the ID of the application.   |
| **--name**   |  Limits the list to the name of the application. |
| **--moniker**   | Limits the list to the moniker listed for the application. |
| **-s, --source**   |  Restricts the list to the source name provided. Must be followed by the source name. |
| **--tag** |  Filters results by tags. |
| **--cmd, --command** |  Filters results by command specified by the application. |
| **-n, --count** | Limits the number of apps displayed in one query.   |
| **-e, --exact**   |   Uses the exact string in the list query, including checking for case-sensitivity. It will not use the default behavior of a substring. |
| **--scope** | Select installed package scope filter (user or machine). |
| **--header** | Optional Windows-Package-Manager REST source HTTP header. |
| **--authentication-mode** | Specify authentication window preference (silent, silentPreferred or interactive). |
| **--authentication-account** | Specify the account to be used for authentication. |
| **--accept-source-agreements** | Used to accept the source license agreement, and avoid the prompt. |
| **--upgrade-available** | Lists only packages which have an upgrade available. |
| **-u, --unknown, --include-unknown** | Lists packages even if their current version cannot be determined. |
| **--pinned, --include-pinned** | Lists packages even if they have a pin that prevents upgrades by WinGet. |
| **--details** | Displays detailed, `show`-like output for each matched package instead of a table view. |
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--nowarn,--ignore-warnings** | Suppresses warning outputs. |
| **--disable-interactivity** | Disable interactive prompts. |
| **--proxy** | Set a proxy to use for this execution. |
| **--no-proxy** | Disable the use of proxy for this execution. |

### Example queries

The following example lists installed applications with a given substring in their name.

:::image type="content" source="./images/list-name.png" alt-text="Screenshot of using the winget --list git command to all installed applications with the name git." lightbox="./images/list-name.png":::

The following example lists all application by ID from a specific source.

:::image type="content" source="./images/list-id-source.png" alt-text="Screenshot listing winget list --id Git.Git to show all installed applications coming from that source." lightbox="./images/list-id-source.png":::

The following example limits the output of **list** to 9 apps.

:::image type="content" source="./images/list-count.png" alt-text="Screenshot listing winget list --count 9 to limit the number of installed apps listed to only 9." lightbox="./images/list-count.png":::

## List with update

As stated above, the **list** command allows you to see what apps you have installed that have upgrades available.

In the image below, you will notice applications with an upgrade available.

:::image type="content" source="./images/list-update.png" alt-text="Screenshot listing winget list --upgrade-available to show installed apps with an available upgrade." lightbox="./images/list-update.png":::

The **list** command will show not only the update version available, but the source that the update is available from.

If there are no updates available, **list** will only show you the currently installed version and the update column will not be displayed.

## List with details

The following example lists details for an installed application by its identifier.

:::image type="content" source="./images/list-details.png" alt-text="Screenshot listing winget winget list --id Microsoft.VisualStudioCode --details to display details about the installed application" lightbox="./images/list-details.png":::

* [Use the winget tool to list and manage applications](index.md)
