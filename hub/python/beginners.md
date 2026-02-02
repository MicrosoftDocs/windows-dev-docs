---
title: Python on Windows for beginners
description: A guide to help you get started if your brand new to using Python on Windows.
ms.topic: get-started
ms.date: 07/03/2025
ms.custom: copilot-scenario-highlight
---

# Set up your Python development environment on Windows

Get your Python environment ready on Windows in minutes — set it up manually or automate everything with winget.

#### [WinGet Configuration](#tab/winget)

This [WinGet configuration file](../package-manager/configuration/index.md) installs a ready-to-use Python development environment on Windows:

- **Python 3.13** – The latest Python runtime, installed from the Microsoft Store
- **Visual Studio Code** – A lightweight, powerful code editor
- **Python extension for Visual Studio Code** – Adds Python language support, debugging, linting, and more

To get started:

1. Open PowerShell in Windows Terminal and run the following command:

    ```powershell
    winget configure -f python-config
    ```
2. When the configuration starts, a terminal window shows the setup steps and required installs. Review them, then confirm by selecting [Y] Yes or [N] No to continue.

3. The required workloads are installed. Verify your setup by running `python --version`.

You're now ready for Python development.


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