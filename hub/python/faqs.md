---
title: Frequently Asked Questions about using Python on Windows
description: Get help by reviewing answers to frequently asked questions (FAQs) about using Python on Windows for development.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: python, windows 10, microsoft, pip, py.exe, file paths, PYTHONPATH, python deployment, python packaging
ms.localizationpriority: medium
ms.date: 07/19/2019
---

# Frequently Asked Questions about using Python on Windows

## Why can’t I “pip install” a certain package?

There are a number of reasons why an installation will fail--in most cases the right solution is to contact the package developer.

The most common cause of problems is trying to install into a location that you do not have permission to modify. For example, the default install location might require Administrative privileges, but by default Python will not have them. The best solution is to create a virtual environment and install there.

Some packages include native code that requires a C or C++ compiler to install. In general, package developers should publish pre-compiled versions, but often do not. Some of these packages might work if you [install Build Tools for Visual Studio](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2019) and select the C++ option, however in most cases you will need to contact the package developer.

[Follow the discussion on StackOverflow](https://stackoverflow.com/questions/4750806/how-do-i-install-pip-on-windows/12476379).

## What is py.exe?

You may end up with multiple versions of Python installed on your machine because you are working on different types of Python projects. Because these all use the `python` command, it may not be obvious which version of Python you are using. As a standard, it is recommended to use the `python3` command (or `python3.7` to select a specific version).

The [py.exe launcher](https://docs.python.org/3/using/windows.html#launcher) will automatically select the most recent version of Python you've installed. You can also use commands like `py -3.7` to select a particular version, or `py --list` to see which versions can be used. **HOWEVER**, the py.exe launcher will only work if you are using a version of Python installed from [python.org](https://www.python.org/downloads/windows/). When you install Python from the Microsoft Store, the `py` command is **not included**. For Linux, macOS, WSL and the Microsoft Store version of Python, you should use the `python3` (or `python3.7`) command.

## Why does running python.exe open the Microsoft Store?

To help new users find a good installation of Python, we added a shortcut to Windows that will take you directly to the latest version of the community's package published in the Microsoft Store. This package can be installed easily, without administrator permissions, and will replace the default `python` and `python3` commands with the real ones.

Running the shortcut executable with any command-line arguments will return an error code to indicate that Python was not installed. This is to prevent batch files and scripts from opening the Store app when it was probably not intended.

If you install Python using the installers from [python.org](https://www.python.org/downloads/windows/) and select the "add to PATH" option, the new `python` command will take priority over the shortcut. Note that other installers may add `python` at a _lower_ priority than the built-in shortcut.

You can disable the shortcuts without installing Python by opening "Manage app execution aliases" from Start, finding the "App Installer" Python entries and switching them to "Off".

## Why don’t file paths work in Python when I copy-paste them?

Python strings use “escapes” for special characters. For example, to insert a new line character into a string, you would type `\n`. Because file paths on Windows use backslashes, some parts might be being converted into special characters.

To paste a path as a string in Python, add the `r` prefix. This indicates that it is a `raw` string, and no escape characters will be used except for \” (you might need to remove the last backslash in your path). So your path might look like:
`r"C:\Users\MyName\Documents\Document.txt"`

When working with paths in Python, we recommend using the standard pathlib module. This will let you convert the string to a rich Path object that can do path manipulations consistently whether it uses forward slashes or backslashes, making your code work better across different operating systems.

## What is PYTHONPATH?

The PYTHONPATH environment variable is used by Python to specify a list of directories that modules can be imported from. When running, you can inspect the `sys.path` variable to see which directories will be searched when you import something.

To set this variable from the Command Prompt, use: `set PYTHONPATH=list;of;paths`.

To set this variable from PowerShell, use: `$env:PYTHONPATH=’list;of;paths’` just before you launch Python.

Setting this variable globally through the **Environment Variables** settings is **not** recommended, as it may be used by any version of Python instead of the one that you intend to use.

## Where can I find help with packaging and deployment?

[Docker](https://code.visualstudio.com/docs/azure/docker): [VSCode extension](https://code.visualstudio.com/docs/azure/docker) helps you quickly package and deploy with Dockerfile and docker-compose.yml templates (generate the proper Docker files for your project).

[Azure Kubernetes Service (AKS)](/azure/aks/) enables you to deploy and manage containerized applications while scaling resources on demand.

## What if I need to work across different machines?

[Settings Sync](https://marketplace.visualstudio.com/items?itemName=Shan.code-settings-sync) allows you to synchronize your VS Code settings across different installations using GitHub. If you work on different machines, this helps keep your environment consistent across them.

## What if I'm used to using PyCharm, Atom, Sublime Text, Emacs, or Vim?

The VSCode extension [Keymaps](https://marketplace.visualstudio.com/search?target=VSCode&category=Keymaps&sortBy=Downloads) can help your environment feel right at home.

## How do Mac shortcut keys map to Windows shortcut keys?

Some of the keyboard buttons and system shortcuts are slightly different between a Windows machine and a Macintosh. This [Mac to Windows transition guide](../dev-environment/mac-to-windows.md) covers the basics.