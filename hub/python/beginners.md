---
title: Python on Windows for beginners
description: A guide to help you get started if your brand new to using Python on Windows.
ms.topic: get-started
ms.date: 07/03/2025
ms.custom: copilot-scenario-highlight
---

# Set up your Python development environment on Windows

For beginners interested in learning Python using Windows, we recommend choosing between these two setup paths:

1. [Set up your Python development environment using a winget configuration file](#set-up-your-python-development-environment-using-a-winget-configuration-file)
2. [Manually set up your Python development environment](#manually-set-up-your-python-development-environment)

#### [WinGet Configuration](#tab/winget)

[Winget Configuration files](../package-manager/configuration/index.md) include all of the instructions needed to install requirements and setup your machine for a specific project. To use Microsoft's Beginner Python project WinGet Configuration setup file, follow the steps below:

1. Download the configuration file by opening this link and selecting "Raw file content > Download" (three dots menu on top-right):  [Winget Configuration: learn_python.winget](https://github.com/microsoft/winget-dsc/blob/main/samples/Configuration%20files/Learn%20tutorials/Python%203.13/learn_python.winget).
2. To run the file, double-click the downloaded configuration file (the first time you will need to select the "Windows Package Manager Client" app to open and run the file) or you can open Powershell in Windows Terminal and enter the following command:

    ```powershell
    winget configure -f <path to learn_python.winget file>
    ```

    The file path will look something like `winget configure -f C:\Users\<your-name>\Downloads\learn_python.winget`.

3. Once the configuration file begins running, you will see the setup steps listed in a terminal window, including the project requirements that will be installed. You will then need to confirm that you have reviewed these configuration updates and confirm that you would like to proceed by selecting [Y] Yes or [N] No.

4. Once you proceed, the project requirements will be installed and report whether the configuration has been successfully applied.

**Your machine is now setup to Learn Python!**

To confirm, check what version of Python is installed on your machine now by entering the command: `python --version`.

#### [Manual installtion](#tab/manual)

To setup your Python development environment manually, rather than using a winget configuration file, you will need to:

- Install Python
- Install Visual Studio Code
- Install the Visual Studio Code extension for Python

**Install Python**: There are multiple versions of Python available to install (based on updates that have been made to the coding language over time). You will first need to determine which version of Python you need. You can reference the versions of Python currently supported at [Status of Python versions | Python Developer's Guide](https://devguide.python.org/versions/#versions). We recommend either using a modern, supported version, or matching the version of the whatever Python project that you plan to contribute to. For this tutorial, we recommend that you use the Microsoft Store to install Python.

- **[Install Python 3 using Microsoft Store](https://apps.microsoft.com/search?query=python)** - select the most recent version available and then "Download". Installing Python via the Microsoft Store uses Python 3 and handles set up of your PATH settings for the current user (avoiding the need for admin access), in addition to providing automatic updates. Once Python has completed the downloading and installation process, open PowerShell in Windows Terminal and enter the command: `python --version` to confirm the Python version that has been installed on your machine.

If you are using Python on Windows for **web development**, we recommend a different set up for your development environment. Rather than installing directly on Windows, we recommend installing and using Python via the Windows Subsystem for Linux.

- [Get started using Python for web development on Windows](./web-frameworks.md).

If you're interested in automating common tasks on your operating system, see our guide: 

- [Get started using Python on Windows for scripting and automation](./scripting.md).

For some advanced scenarios (like needing to access/modify Python's installed files, make copies of binaries, or use Python DLLs directly), you may want to consider downloading a specific Python release directly from [python.org](https://www.python.org/downloads/) or installing an [alternative](https://www.python.org/download/alternatives), such as Anaconda, Jython, PyPy, WinPython, IronPython, etc. We only recommend this if you are a more advanced Python programmer with a specific reason for choosing an alternative implementation.

**Install Visual Studio Code**: Visual Studio Code is a code editing tool, sometimes called an Integrated Development Environment, or IDE. Visual Studio Code provides features such as [GitHub Copilot](https://code.visualstudio.com/docs/copilot/overview) (an AI-powered tool that provides coding suggestions), [IntelliSense](https://code.visualstudio.com/docs/editor/intellisense) (a code completion aid), [Linting](https://code.visualstudio.com/docs/python/linting) (helps avoid making errors in your code), [Debug support](https://code.visualstudio.com/docs/python/debugging) (helps you find errors in your code after you run it), [Code snippets](https://code.visualstudio.com/docs/editor/userdefinedsnippets) (templates for small reusable code blocks), and [Unit testing](https://code.visualstudio.com/docs/python/unit-testing) (testing your code's interface with different types of input).

- [Install Visual Studio Code on Windows](https://code.visualstudio.com)

**Install the Visual Studio Code extension for Python**: Visual Studio Code offers "extensions" allowing you to add on support features which *extend* support for whatever language or tools you are working with. In this case, the Python extension adds Python-specific support for code formatting, IntelliSense code completion suggestions, debugging, linting, refactoring, etc.

- [Install the Python extension from Visual Studio Code Marketplace](https://marketplace.visualstudio.com/items?itemName=ms-python.python)

---

## Resources for continued learning

We recommend the following resources to support you in continuing to learn about Python development on Windows.

- [Microsoft Dev Blogs: Python](https://devblogs.microsoft.com/python/): Read the latest updates about all things Python at Microsoft.

### Working with Python in VS Code

- [Editing Python in VS Code](https://code.visualstudio.com/docs/python/editing): Learn more about how to take advantage of VS Code's autocomplete and IntelliSense support for Python, including how to customize their behavior... or just turn them off.

- [Linting Python](https://code.visualstudio.com/docs/python/linting): Linting is the process of running a program that will analyse code for potential errors. Learn about the different forms of linting support VS Code provides for Python and how to set it up.

- [Debugging Python](https://code.visualstudio.com/docs/python/debugging): Debugging is the process of identifying and removing errors from a computer program. This article covers how to initialize and configure debugging for Python with VS Code, how to set and validate breakpoints, attach a local script, perform debugging for different app types or on a remote computer, and some basic troubleshooting.

- [Unit testing Python](https://code.visualstudio.com/docs/python/unit-testing): Covers some background explaining what unit testing means, an example walkthrough, enabling a test framework, creating and running your tests, debugging tests, and test configuration settings.