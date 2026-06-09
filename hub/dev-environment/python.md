---
title: Python on Windows for beginners
description: A guide to help you get started if your brand new to using Python on Windows.
ms.topic: get-started
ms.date: 03/23/2026
ms.custom: copilot-scenario-highlight
---

# Set up your Python development environment on Windows

Get your Python environment ready on Windows in minutes — install from the command line with winget or set it up manually. You'll need the following:

- **Python** – The current stable Python runtime (3.14 or later)
- **Visual Studio Code** – A lightweight, powerful code editor
- **Python extension for Visual Studio Code** – Adds Python language support, debugging, linting, and more

#### [WinGet](#tab/winget)

1. Open PowerShell in Windows Terminal and install Python:

    ```powershell
    winget install Python.Python.3.14
    ```

2. Install Visual Studio Code:

    ```powershell
    winget install Microsoft.VisualStudioCode
    ```

3. Close and reopen PowerShell, then verify Python is installed:

    ```powershell
    python --version
    ```

4. Open VS Code and install the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python) from the VS Code Marketplace.

You're now ready for Python development.


#### [Manual installation](#tab/manual)

1. **Install Python**: Install [Python from the Microsoft Store](https://apps.microsoft.com/search?query=python). The Microsoft Store version automatically configures your PATH and provides automatic updates. Once installed, open PowerShell and run `python --version` to verify.

2. **Install Visual Studio Code**: Download and install [Visual Studio Code](https://code.visualstudio.com).

3. **Install the Python extension**: Install the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python) from the VS Code Marketplace.

---

> [!div class="nextstepaction"]
> [Get started with Python](https://docs.python.org/3/tutorial/)

## Frequently Asked Questions

<details><summary>Trouble installing a package with pip install</summary>

> There are a number of reasons why an installation will fail--in many cases the right solution is to contact the package developer.
>
> A common cause for trouble is trying to install into a location that you do not have permission to modify. For example, the default install location might require Administrative privileges, but by default Python will not have them. The best solution is to create a [virtual environment](https://docs.python.org/3/tutorial/venv.html) and install there.
>
> Some packages include native code that requires a C or C++ compiler to install. In general, package developers should publish pre-compiled versions, but often do not. Some of these packages might work if you [install Build Tools for Visual Studio](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio) and select the C++ option, however in most cases you will need to contact the package developer.
>
> [Follow the discussion on StackOverflow](https://stackoverflow.com/questions/4750806/how-do-i-install-pip-on-windows/12476379)

</details>

<details><summary>Trouble installing pip with WSL</summary>

> When installing a package (like Flask) with pip on Windows Subsystem for Linux (WSL or WSL2), for example `python3 -m pip install flask`, you may encounter an error like this:
>
> ```bash
> WARNING: Retrying (Retry(total=4, connect=None, read=None, redirect=None, status=None))
> after connection broken by 'NewConnectionError('<urllib3.connection.VerifiedHTTPSConnection
> object at 0x7f655471da30>: Failed to establish a new connection: [Errno -3]
> Temporary failure in name resolution')': /simple/flask/
> ```
>
> Unless you are running an aftermarket firewall, the likely solution is to simply re-install pip:
>
> ```bash
> sudo apt -y purge python3-pip
> sudo python3 -m pip uninstall pip
> sudo apt -y install python3-pip --fix-missing
> ```

</details>

<details><summary>What is py.exe?</summary>

> You may end up with multiple versions of Python installed on your machine because you are working on different types of Python projects. Because these all use the `python` command, it may not be obvious which version of Python you are using. As a standard, it is recommended to use the `python3` command (or `python3.7` to select a specific version).
>
> The [py.exe launcher](https://docs.python.org/3/using/windows.html#launcher) will automatically select the most recent version of Python you've installed. You can also use commands like `py -3.7` to select a particular version, or `py --list` to see which versions can be used. **HOWEVER**, the py.exe launcher will only work if you are using a version of Python installed from [python.org](https://www.python.org/downloads/windows/). When you install Python from the Microsoft Store, the `py` command is **not included**. For Linux, macOS, WSL and the Microsoft Store version of Python, you should use the `python3` (or `python3.7`) command.

</details>

<details><summary>Why does running python.exe open the Microsoft Store?</summary>

> To help new users find a good installation of Python, we added a shortcut to Windows that will take you directly to the latest version of the community's package published in the Microsoft Store. This package can be installed easily, without administrator permissions, and will replace the default `python` and `python3` commands with the real ones.
>
> Running the shortcut executable with any command-line arguments will return an error code to indicate that Python was not installed. This is to prevent batch files and scripts from opening the Store app when it was probably not intended.
>
> If you install Python using the installers from [python.org](https://www.python.org/downloads/windows/) and select the "add to PATH" option, the new `python` command will take priority over the shortcut. Note that other installers may add `python` at a _lower_ priority than the built-in shortcut.
>
> You can disable the shortcuts without installing Python by opening "Manage app execution aliases" from Start, finding the "App Installer" Python entries and switching them to "Off".

</details>

<details><summary>Why don't file paths work in Python when I copy-paste them?</summary>

> Python strings use "escapes" for special characters. For example, to insert a new line character into a string, you would type `\n`. Because file paths on Windows use backslashes, some parts might be being converted into special characters.
>
> To paste a path as a string in Python, add the `r` prefix. This indicates that it is a `raw` string, and no escape characters will be used except for \" (you might need to remove the last backslash in your path). So your path might look like:
> `r"C:\Users\MyName\Documents\Document.txt"`
>
> When working with paths in Python, we recommend using the standard pathlib module. This will let you convert the string to a rich Path object that can do path manipulations consistently whether it uses forward slashes or backslashes, making your code work better across different operating systems.

</details>

<details><summary>What is PYTHONPATH?</summary>

> The PYTHONPATH environment variable is used by Python to specify a list of directories that modules can be imported from. When running, you can inspect the `sys.path` variable to see which directories will be searched when you import something.
>
> To set this variable from the Command Prompt, use: `set PYTHONPATH=list;of;paths`.
>
> To set this variable from PowerShell, use: `$env:PYTHONPATH='list;of;paths'` just before you launch Python.
>
> Setting this variable globally through the **Environment Variables** settings is **not** recommended, as it may be used by any version of Python instead of the one that you intend to use.

</details>

<details><summary>Where can I find help with packaging and deployment?</summary>

> [Docker](https://code.visualstudio.com/docs/azure/docker): [VSCode extension](https://code.visualstudio.com/docs/azure/docker) helps you quickly package and deploy with Dockerfile and docker-compose.yml templates (generate the proper Docker files for your project).
>
> [Azure Kubernetes Service (AKS)](/azure/aks/) enables you to deploy and manage containerized applications while scaling resources on demand.

</details>

<details><summary>What if I need to work across different machines?</summary>

> VS Code has built-in [Settings Sync](https://code.visualstudio.com/docs/configure/settings-sync) that lets you share your settings, keybindings, extensions, and more across machines using your GitHub or Microsoft account. No extension needed.

</details>

<details><summary>What if I'm used to using PyCharm, Atom, Sublime Text, Emacs, or Vim?</summary>

> The VSCode extension [Keymaps](https://marketplace.visualstudio.com/search?target=VSCode&category=Keymaps&sortBy=Downloads) can help your environment feel right at home.

</details>

<details><summary>How do Mac shortcut keys map to Windows shortcut keys?</summary>

> Some of the keyboard buttons and system shortcuts are slightly different between a Windows machine and a Macintosh. This [Mac to Windows transition guide](../dev-environment/mac-to-windows.md) covers the basics.

</details>

<details><summary>Where can I learn more about using Python in VS Code?</summary>

> - [Editing Python in VS Code](https://code.visualstudio.com/docs/python/editing): Learn more about how to take advantage of VS Code's autocomplete and IntelliSense support for Python, including how to customize their behavior... or just turn them off.
> - [Linting Python](https://code.visualstudio.com/docs/python/linting): Linting is the process of running a program that will analyse code for potential errors. Learn about the different forms of linting support VS Code provides for Python and how to set it up.
> - [Debugging Python](https://code.visualstudio.com/docs/python/debugging): Debugging is the process of identifying and removing errors from a computer program. This article covers how to initialize and configure debugging for Python with VS Code, how to set and validate breakpoints, attach a local script, perform debugging for different app types or on a remote computer, and some basic troubleshooting.
> - [Unit testing Python](https://code.visualstudio.com/docs/python/unit-testing): Covers some background explaining what unit testing means, an example walkthrough, enabling a test framework, creating and running your tests, debugging tests, and test configuration settings.

</details>
