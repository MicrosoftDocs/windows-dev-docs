---
ms.topic: include
ms.date: 06/14/2023
---
## Finalize your environment

1. Open a command-line prompt, Windows Terminal if you have it installed, or else Command Prompt or Windows Powershell from the Start Menu.

1. Install or update the `uno-check` tool:
    - Use the following command:

        ```dotnetcli
        dotnet tool install -g uno.check
        ```

    - To update the tool, if you already have previously installed an older version:

        ```dotnetcli
        dotnet tool update -g uno.check
        ```

1. Run the tool with the following command:

    ```
    uno-check
    ```

1. Follow the instructions indicated by the tool. Because it needs to modify your system, you may be prompted for elevated permissions.
