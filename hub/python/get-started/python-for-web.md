---
title: Web development with Python on Windows
description: How to get started using Python for web development on Windows, including set up for frameworks like Flask and Django.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
ms.prod: windows
ms.technology: hub
keywords: python, windows 10, microsoft, python on windows, python web with wsl, python web app with windows subsystem for linux, python web development on windows, flask app on windows, django app on windows, python web, flask web dev on windows, django web dev on windows, windows web dev with python, vs code python web dev, remote wsl extension, ubuntu, wsl, venv, pip, microsoft python extension, run python on windows, use python on windows, build with python on windows
ms.localizationpriority: medium
ms.date: 07/19/2019
---

# Get started using Python for web development on Windows

The following is a step-by-step guide to get you started using Python for web development on Windows, using the Windows Subsystem for Linux (WSL).

## Set up your development environment

We recommend installing Python on WSL when building web applications. Many of the tutorials and instructions for Python web development are written for Linux users and use Linux-based packaging and installation tools. Most web apps are also deployed on Linux, so this will ensure you have consistency between your development and production environments.

If you are using Python for something other than web development, we recommend you install Python directly on Windows 10 using the Microsoft Store. WSL does not support GUI desktops or applications (like PyGame, Gnome, KDE, etc). Install and use Python directly on Windows for these cases. If you're new to Python, see our guide: [Get started using Python on Windows for beginners](./python-for-education.md). If you're interested in automating common tasks on your operating system, see our guide: [Get started using Python on Windows for scripting and automation](./python-for-scripting.md). For some advanced scenarios, you may want to consider downloading a specific Python release directly from [python.org](https://www.python.org/downloads/windows/) or consider installing an [alternative](https://www.python.org/download/alternatives), such as Anaconda, Jython, PyPy, WinPython, IronPython, etc. We only recommend this if you are a more advanced Python programmer with a specific reason for choosing an alternative implementation.

## Enable Windows Subsystem for Linux

WSL lets you run a GNU/Linux environment - including most command-line tools, utilities, and applications - directly on Windows, unmodified and fully integrated with your Windows file system and favorite tools like Visual Studio Code. Before enabling WSL, please check that you have the [most recent version of Windows 10](https://www.microsoft.com/software-download/windows10).

To enable WSL on your computer, you need to:

1. Go to your **Start** menu (lower left Windows icon), type "Turn windows features on or off", and select the link to the **Control panel** to open the **Windows features** pop-up menu. Find "Windows Subsystem for Linux" in the list and select the checkbox to turn the feature on.

2. Restart your computer when prompted.

## Install a Linux distribution

There are several Linux distributions available to run on WSL. You can find and install your favorite in the Microsoft Store. We recommend starting with [Ubuntu 18.04 LTS](https://www.microsoft.com/store/productId/9N9TNGVNDL3Q) as it's current, popular, and well supported.

1. Open this [Ubuntu 18.04 LTS](https://www.microsoft.com/store/productId/9N9TNGVNDL3Q) link, open the Microsoft Store, and select **Get**. *(This is a fairly large download and may take some time to install.)*

2. After the download completes, select **Launch** from the Microsoft Store or launch by typing "Ubuntu 18.04 LTS" into the **Start** menu.

3. You will be asked to create an account name and password when you run the distribution for the first time. After this, you'll be automatically signed in as this user by default. You can choose any user name and password. They have no bearing on your Windows user name.

You can check the Linux distribution that you are currently using by entering: `lsb_release -d`. To update your Ubuntu distribution, use: `sudo apt update && sudo apt upgrade`. We recommend updating regularly to ensure you have the most recent packages. Windows doesn't automatically handle this update. For links to other Linux distributions available in the Microsoft Store, alternative installation methods, or troubleshooting, see [Windows Subsystem for Linux Installation Guide for Windows 10](https://docs.microsoft.com/windows/wsl/install-win10).

## Set up Visual Studio Code

Take advantage of [IntelliSense](https://code.visualstudio.com/docs/editor/intellisense), [Linting](https://code.visualstudio.com/docs/python/linting), [Debug support](https://code.visualstudio.com/docs/python/debugging), [Code snippets](https://code.visualstudio.com/docs/editor/userdefinedsnippets), and [Unit testing](https://code.visualstudio.com/docs/python/unit-testing) by using VS Code. VS Code integrates nicely with the Windows Subsystem for Linux, providing a [built-in terminal](https://code.visualstudio.com/docs/editor/integrated-terminal) to establish a seamless workflow between your code editor and your command line, in addition to supporting [Git for version control](https://code.visualstudio.com/docs/editor/versioncontrol#_git-support) with common Git commands (add, commit, push, pull) built right into the UI.

1. [Download and install VS Code for Windows](https://code.visualstudio.com). VS Code is also available for Linux, but Windows Subsystem for Linux does not support GUI apps, so we need to install it on Windows. Not to worry, you'll still be able to integrate with your Linux command line and tools using the Remote - WSL Extension.

2. Install the [Remote - WSL Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-wsl) on VS Code. This allows you to use WSL as your integrated development environment and will handle compatibility and pathing for you. [Learn more](https://code.visualstudio.com/docs/remote/remote-overview).

> [!IMPORTANT]
> If you already have VS Code installed, you need to ensure that you have the [1.35 May release](https://code.visualstudio.com/updates/v1_35) or later in order to install the [Remote - WSL Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-wsl). We do not recommend using WSL in VS Code without the Remote-WSL extension as you will lose support for auto-complete, debugging, linting, etc. Fun fact: This WSL extension is installed in $HOME/.vscode-server/extensions.

## Create a new project

Let's create a new project directory on our Linux (Ubuntu) file system that we will then work on with Linux apps and tools using VS Code.

1. Close VS Code and open Ubuntu 18.04 (your WSL command line) by going to your **Start** menu (lower left Windows icon) and typing: "Ubuntu 18.04".

2. In your Ubuntu command line, navigate to where you want to put your project, and create a directory for it: `mkdir HelloWorld`.

![Ubuntu terminal](../../images/ubuntu-terminal.png)

> [!TIP]
> An important thing to remember when using Windows Subsystem for Linux (WSL) is that **you are now working between two different file systems**: 1) your Windows file system, and 2) your Linux file system (WSL), which is Ubuntu for our example. You will need to pay attention to where you install packages and store files. You can install one version of a tool or package in the Windows file system and a completely different version in the Linux file system. Updating the tool in the Windows file system will have no effect on the tool in the Linux file system, and vice-versa. WSL mounts the fixed drives on your computer under the /mnt/<drive> folder in your Linux distribution. For example, your Windows C: drive is mounted under `/mnt/c/`. You can access your Windows files from the Ubuntu terminal and use Linux apps and tools on those files and vice-versa. We recommend working in the Linux file system for Python web development given that much of the web tooling is originally written for Linux and deployed in a Linux production environment. It also avoids mixing file system semantics (like Windows being case-insensitive regarding file names). That said, WSL now supports jumping between the Linux and Windows files systems, so you can host your files on either one. [Learn more](https://devblogs.microsoft.com/commandline/do-not-change-linux-files-using-windows-apps-and-tools/). We are also excited to share that [WSL2 is coming soon to Windows](https://devblogs.microsoft.com/commandline/wsl-2-is-now-available-in-windows-insiders/) and will offer some great improvements. You can [try it now on Windows Insiders build 18917](https://docs.microsoft.com/windows/wsl/wsl2-install).

## Install Python, pip, and venv

Ubuntu 18.04 LTS comes with Python 3.6 already installed, but it does not come with some of the modules that you may expect to get with other Python installations. We will still need to install **pip**, the standard package manager for Python, and **venv**, the standard module used to create and manage lightweight virtual environments.  

1. Confirm that Python3 is already installed by opening your Ubuntu terminal and entering: `python3 --version`. This should return your Python version number. If you need to update your version of Python, first update your Ubuntu version by entering: `sudo apt update && sudo apt upgrade`, then update Python using `sudo apt upgrade python3`.

2. Install **pip** by entering: `sudo apt install python3-pip`. Pip allows you to install and manage additional packages that are not part of the Python standard library.

3. Install **venv** by entering: `sudo apt install python3-venv`.

## Create a virtual environment

Using virtual environments is a recommended best practice for Python development projects. By creating a virtual environment, you can isolate your project tools and avoid versioning conflicts with tools for your other projects. For example, you may be maintaining an an older web project that requires the Django 1.2 web framework, but then an exciting new project comes along using Django 2.2. If you update Django globally, outside of a virtual environment, you could run into some versioning issues later on. In addition to preventing accidental versioning conflicts, virtual environments let you install and manage packages without administrative privileges.

1. Open your terminal and, inside your *HelloWorld* project folder, use the following command to create a virtual environment named **.venv**:  `python3 -m venv .venv`.

2. To activate the virtual environment, enter: `source .venv/bin/activate`. If it worked, you should see **(.venv)** before the command prompt. You now have a self-contained environment ready for writing code and installing packages. When you're finished with your virtual environment, enter the following command to deactivate it: `deactivate`.

    ![Create a virtual environment](../../images/wsl-venv.png)

> [!TIP]
> We recommend creating the virtual environment inside the directory in which you plan to have your project. Since each project should have it's own separate directory, each will have it's own virtual environment, so there is not a need for unique naming. Our suggestion is to use the name **.venv** to follow the Python convention. Some tools (like pipenv) also default to this name if you install into your project directory. You don't want to use **.env** as that conflicts with environment variable definition files. We generally do not recommend non-dot-leading names, as you don't need `ls` constantly reminding you that the directory exists. We also recommend adding **.venv** to your .gitignore file. (Here is [GitHub's default gitignore template for Python](https://github.com/github/gitignore/blob/50e42aa1064d004a5c99eaa72a2d8054a0d8de55/Python.gitignore#L99-L106) for reference.) For more information about working with virtual environments in VS Code, see [Using Python environments in VS Code](https://code.visualstudio.com/docs/python/environments).

## Open a WSL - Remote window

VS Code uses the Remote - WSL Extension (installed previously) to treat your Linux subsystem as a remote server. This allows you to use WSL as your integrated development environment. [Learn more](https://code.visualstudio.com/docs/remote/wsl). 

1. Open your project folder in VS Code from your Ubuntu terminal by entering: `code .` (the "." tells VS Code to open the current folder).

2. A Security Alert will pop-up from Windows Defender, select "Allow access". Once VS Code opens, you should see the Remote Connection Host indicator, in the bottom-left corner, letting you know that you are editing on **WSL: Ubuntu-18.04**.

    ![VS Code Remote Connection Host indicator](../../images/wsl-remote-extension.png)

3. Close your Ubuntu terminal. Moving forward we will use the WSL terminal integrated into VS Code.

4. Open the WSL terminal in VS Code by pressing **Ctrl+`** (using the backtick character) or selecting  **View** > **Terminal**. This will open a bash (WSL) command-line opened to the project folder path that you created in your Ubuntu terminal.

    ![VS Code with WSL terminal](../../images/vscode-bash-remote.png)

## Install the Microsoft Python extension

You will need to install any VS Code extensions for your Remote - WSL. Extensions already installed locally on VS Code will not automatically be available. [Learn more](https://code.visualstudio.com/docs/remote/wsl#_managing-extensions).

1. Open the VS Code Extensions window by entering **Ctrl+Shift+X** (or use the menu to navigate to **View** > **Extensions**).

2. In the top **Search Extensions in Marketplace** box, enter:  **Python**.

3. Find the **Python (ms-python.python) by Microsoft** extension and select the green **Install** button.

4. Once the extension is finished installing, you will need to select the blue **Reload Required** button. This will reload VS Code and display a **WSL: UBUNTU-18.04 - Installed** section in your VS Code Extensions window showing that you've installed the Python extension.

## Run a simple Python program

Python is an interpreted language and supports different types of interpretors (Python2, Anaconda, PyPy, etc). VS Code should default to the interpreter associated with your project. If you have a reason to change it, select the interpreter currently displayed in blue bar on the bottom of your VS Code window or open the **Command Palette** (Ctrl+Shift+P) and enter the command **Python: Select Interpreter**. This will display a list of the Python interpreters that you currently have installed. [Learn more about configuring Python environments](https://code.visualstudio.com/docs/python/environments).

Let's create and run a simple Python program as a test and ensure that we have the correct Python interpreter selected.

1. Open the VS Code File Explorer window by entering **Ctrl+Shift+E** (or use the menu to navigate to **View** > **Explorer**).

2. If it's not already open, open your integrated WSL terminal by entering **Ctrl+Shift+`** and ensure that your **HelloWorld** python project folder is selected.

3. Create a python file by entering: `touch test.py`. You should see the file you just created appear in your Explorer window under the .venv and .vscode folders already in your project directory.

4. Select the **test.py** file that you just created in your Explorer window to open it in VS Code. Because the .py in our file name tells VS Code that this is a Python file, the Python extension you loaded previously will automatically choose and load a Python interpreter that you will see displayed on the bottom of your VS Code window.

    ![Select Python interpreter in VS Code](../../images/interpreterselection.gif)

5. Paste this Python code into your test.py file and then save the file (Ctrl+S): 

    ```python
    print("Hello World")
    ```

6. To run the Python "Hello World" program that we just created, select the **test.py** file in the VS Code Explorer window, then right-click the file to display a menu of options. Select **Run Python File in Terminal**. Alternatively, in your integrated WSL terminal window, enter: `python test.py` to run your "Hello World" program. The Python interpreter will print "Hello World" in your terminal window.

Congratulations. You're all set up to create and run Python programs! Now let's try creating a Hello World app with two of the most popular Python web frameworks: Flask and Django.

## Additional resources

- [Python Tutorial with VS Code](https://code.visualstudio.com/docs/python/python-tutorial): An intro tutorial to VS Code as a Python environment, primarily how to edit, run, and debug code.
- [Git support in VS Code](https://code.visualstudio.com/docs/editor/versioncontrol#_git-support): Learn how to use Git version control basics in VS Code.  
- [Learn about updates coming soon with WSL 2!](https://docs.microsoft.com/windows/wsl/wsl2-index): This new version changes how Linux distributions interact with Windows, increasing file system performance and adding full system call compatibility.
- [Working with multiple Linux distributions on Windows](https://docs.microsoft.com/windows/wsl/wsl-config): Learn how to manage multiple different Linux distributions on your Windows machine.
