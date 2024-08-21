---
title: Using Python for scripting and automation 
description: How to get started using Python for scripting, automation, and systems administration on Windows.
ms.topic: article
keywords: python, windows 10, microsoft, python system administration, python file automation, python scripts on windows, set up python on windows, python developer environment on windows, python dev environment on windows, python with powershell, python scripts for file system tasks
ms.localizationpriority: medium
ms.date: 05/07/2021
---

# Get started using Python on Windows for scripting and automation

The following is a step-by-step guide for setting up your developer environment and getting you started using Python for scripting and automating file system operations on Windows.

> [!NOTE]
> This article will cover setting up your environment to use some of the helpful libraries in Python that can automate tasks across platforms, like searching your file system, accessing the internet, parsing file types, etc., from a Windows-centered approach. For Windows-specific operations, check out [ctypes](https://docs.python.org/3/library/ctypes.html), a C-compatible foreign function library for Python, [winreg](https://docs.python.org/3/library/winreg.html), functions exposing the Windows registry API to Python, and [Python/WinRT](https://pypi.org/project/winrt/), enabling access Windows Runtime APIs from Python.

## Set up your development environment

When using Python to write scripts that perform file system operations, we recommend you [install Python from the Microsoft Store](https://www.microsoft.com/p/python-37/9nj46sx7x90p?activetab=pivot:overviewtab). Installing via the Microsoft Store uses the basic Python3 interpreter, but handles set up of your PATH settings for the current user (avoiding the need for admin access), in addition to providing automatic updates.

If you are using Python for **web development**  on Windows, we recommend a different setup using the Windows Subsystem for Linux. Find a walkthrough in our guide: [Get started using Python for web development on Windows](./web-frameworks.md). If you're brand new to Python, try our guide: [Get started using Python on Windows for beginners](./beginners.md). For some advanced scenarios (like needing to access/modify Python's installed files, make copies of binaries, or use Python DLLs directly), you may want to consider downloading a specific Python release directly from [python.org](https://www.python.org/downloads/) or consider installing an [alternative](https://www.python.org/download/alternatives), such as Anaconda, Jython, PyPy, WinPython, IronPython, etc. We only recommend this if you are a more advanced Python programmer with a specific reason for choosing an alternative implementation.

## Install Python

To install Python using the Microsoft Store:

1. Go to your **Start** menu (lower left Windows icon), type "Microsoft Store", select the link to open the store.

2. Once the store is open, select **Search** from the upper-right menu and enter "Python". Select which version of Python you would like to use from the results under Apps. We recommend using the most recent unless you have a reason not to (such as aligning with the version used on a pre-existing project that you plan to work on). Once you've determined which version you would like to install, select **Get**.

3. Once Python has completed the downloading and installation process, open Windows PowerShell using the **Start** menu (lower left Windows icon). Once PowerShell is open, enter `Python --version` to confirm that Python3 has been installed on your machine.

4. The Microsoft Store installation of Python includes **pip**, the standard package manager. Pip allows you to install and manage additional packages that are not part of the Python standard library. To confirm that you also have pip available to install and manage packages, enter `pip --version`.

## Install Visual Studio Code

By using VS Code as your text editor / integrated development environment (IDE), you can take advantage of [IntelliSense](https://code.visualstudio.com/docs/editor/intellisense) (a code completion aid), [Linting](https://code.visualstudio.com/docs/python/linting) (helps avoid making errors in your code), [Debug support](https://code.visualstudio.com/docs/python/debugging) (helps you find errors in your code after you run it), [Code snippets](https://code.visualstudio.com/docs/editor/userdefinedsnippets) (templates for small reusable code blocks), and [Unit testing](https://code.visualstudio.com/docs/python/unit-testing) (testing your code's interface with different types of input).

Download VS Code for Windows and follow the installation instructions: [https://code.visualstudio.com](https://code.visualstudio.com).

## Install the Microsoft Python extension

You will need to install the Microsoft Python extension in order to take advantage of the VS Code support features. [Learn more](https://code.visualstudio.com/docs/languages/python).

1. Open the VS Code Extensions window by entering **Ctrl+Shift+X** (or use the menu to navigate to **View** > **Extensions**).

2. In the top **Search Extensions in Marketplace** box, enter:  **Python**.

3. Find the **Python (ms-python.python) by Microsoft** extension and select the green **Install** button.

## Open the integrated PowerShell terminal in VS Code

VS Code contains a [built-in terminal](https://code.visualstudio.com/docs/editor/integrated-terminal) that enables you to open a Python command line with PowerShell, establishing a seamless workflow between your code editor and command line.

1. Open the terminal in VS Code, select **View** > **Terminal**, or alternatively use the shortcut **Ctrl+`** (using the backtick character).

    > [!NOTE]
    > The default terminal should be PowerShell, but if you need to change it, use **Ctrl+Shift+P** to enter the command palette. Enter **Terminal: Select Default Shell** and a list of terminal options will display containing PowerShell, Command Prompt, WSL, etc. Select the one you'd like to use and enter **Ctrl+Shift+`** (using the backtick) to create a new terminal.

2. Inside your VS Code terminal, open Python by entering: `python`

3. Try the Python interpreter out by entering: `print("Hello World")`. Python will return your statement "Hello World".

    ![Python command line in VS Code](../images/python-in-vscode.png)

4. To exit Python, you can enter `exit()`, `quit()`, or select Ctrl-Z.

## Install Git (optional)

If you plan to collaborate with others on your Python code, or host your project on an open-source site (like GitHub), VS Code supports [version control with Git](https://code.visualstudio.com/docs/editor/versioncontrol#_git-support). The Source Control tab in VS Code tracks all of your changes and has common Git commands (add, commit, push, pull) built right into the UI. You first need to install Git to power the Source Control panel.

1. Download and install Git for Windows from [the git-scm website](https://git-scm.com/download/win).

2. An Install Wizard is included that will ask you a series of questions about settings for your Git installation. We recommend using all of the default settings, unless you have a specific reason for changing something.

3. If you've never worked with Git before, [GitHub Guides](https://guides.github.com/) can help you get started.

## Example script to display the structure of your file system directory

Common system administration tasks can take a huge amount of time, but with a Python script, you can automate these tasks so that they take no time at all. For example, Python can read the contents of your computer's file system and perform operations like printing an outline of your files and directories, moving folders from one directory to another, or renaming hundreds of files. Normally, tasks like these could take up a ton of time if you were to perform them manually. Use a Python script instead!

Let's begin with a simple script that walks a directory tree and displays the directory structure.

1. Open PowerShell using the **Start** menu (lower left Windows icon).

2. Create a directory for your project: `mkdir python-scripts`, then open that directory: `cd python-scripts`.

3. Create a few directories to use with our example script:

    ```powershell
    mkdir food, food\fruits, food\fruits\apples, food\fruits\oranges, food\vegetables
    ```

4. Create a few files within those directories to use with our script:

    ```powershell
    new-item food\fruits\banana.txt, food\fruits\strawberry.txt, food\fruits\blueberry.txt, food\fruits\apples\honeycrisp.txt, food\fruits\oranges\mandarin.txt, food\vegetables\carrot.txt
    ```

5. Create a new python file in your python-scripts directory:

    ```powershell
    mkdir src
    new-item src\list-directory-contents.py
    ```

6. Open your project in VS Code by entering: `code .`

7. Open the VS Code File Explorer window by entering **Ctrl+Shift+E** (or use the menu to navigate to **View** > **Explorer**) and select the list-directory-contents.py file that you just created. The Microsoft Python extension will automatically load a Python interpreter. You can see which interpreter was loaded on the bottom of your VS Code window.

    > [!NOTE]
    > Python is an interpreted language, meaning that it acts as a virtual machine, emulating a physical computer. There are different types of Python interpreters that you can use: Python 2, Python 3, Anaconda, PyPy, etc. In order to run Python code and get Python IntelliSense, you must tell VS Code which interpreter to use. We recommend sticking with the interpreter that VS Code chooses by default (Python 3 in our case) unless you have a specific reason for choosing something different. To change the Python interpreter, select the interpreter currently displayed in blue bar on the bottom of your VS Code window or open the **Command Palette** (Ctrl+Shift+P) and enter the command **Python: Select Interpreter**. This will display a list of the Python interpreters that you currently have installed. [Learn more about configuring Python environments](https://code.visualstudio.com/docs/python/environments).

    ![Select Python interpreter in VS Code](../images/interpreterselection.gif)

8. Paste the following code into your list-directory-contents.py file and then select **save**:

    ```python
    import os

    root = os.path.join('..', 'food')
    for directory, subdir_list, file_list in os.walk(root):
        print('Directory:', directory)
        for name in subdir_list:
            print('Subdirectory:', name)
        for name in file_list:
            print('File:', name)
        print()
    ```

9. Open the VS Code integrated terminal (**Ctrl+`**, using the backtick character) and enter the src directory where you just saved your Python script:

    ```powershell
    cd src
    ```

10. Run the script in PowerShell with:

    ```powershell
    python3 .\list-directory-contents.py
    ```

    You should see output that looks like this:

    ```powershell
    Directory: ..\food
    Subdirectory: fruits
    Subdirectory: vegetables

    Directory: ..\food\fruits
    Subdirectory: apples
    Subdirectory: oranges
    File: banana.txt
    File: blueberry.txt
    File: strawberry.txt

    Directory: ..\food\fruits\apples
    File: honeycrisp.txt

    Directory: ..\food\fruits\oranges
    File: mandarin.txt

    Directory: ..\food\vegetables
    File: carrot.txt
    ```

11. Use Python to print that file system directory output to it's own text file by entering this command directly in your PowerShell terminal: `python3 list-directory-contents.py > food-directory.txt`

Congratulations! You've just written an automated systems administration script that reads the directory and files you created and uses Python to display, and then print, the directory structure to it's own text file.

> [!NOTE]
> If you're unable to install Python 3 from the Microsoft Store, see this [issue](https://github.com/MicrosoftDocs/windows-uwp/issues/2901) for an example of how to handle the pathing for this sample script.

## Example script to modify all files in a directory

This example uses the files and directories you just created, renaming each of the files by adding the file's last modified date to the beginning of the filename.

1. Inside the **src** folder in your **python-scripts** directory, create a new Python file for your script:

    ```powershell
    new-item update-filenames.py
    ```

2. Open the update-filenames.py file, paste the following code into the file, and save it:

    > [!NOTE]
    > os.getmtime returns a timestamp in ticks, which is not easily readable. It must be converted to a standard datetime string first.

    ```python
    import datetime
    import os

    root = os.path.join('..', 'food')
    for directory, subdir_list, file_list in os.walk(root):
        for name in file_list:
            source_name = os.path.join(directory, name)
            timestamp = os.path.getmtime(source_name)
            modified_date = str(datetime.datetime.fromtimestamp(timestamp)).replace(':', '.')
            target_name = os.path.join(directory, f'{modified_date}_{name}')

            print(f'Renaming: {source_name} to: {target_name}')

            os.rename(source_name, target_name)
    ```

3. Test your update-filenames.py script by running it: `python3 update-filenames.py` and then running your list-directory-contents.py script again: `python3 list-directory-contents.py`

4. You should see output that looks like this:

    ```powershell
    Renaming: ..\food\fruits\banana.txt to: ..\food\fruits\2019-07-18 12.24.46.385185_banana.txt
    Renaming: ..\food\fruits\blueberry.txt to: ..\food\fruits\2019-07-18 12.24.46.391170_blueberry.txt
    Renaming: ..\food\fruits\strawberry.txt to: ..\food\fruits\2019-07-18 12.24.46.389174_strawberry.txt
    Renaming: ..\food\fruits\apples\honeycrisp.txt to: ..\food\fruits\apples\2019-07-18 12.24.46.395160_honeycrisp.txt
    Renaming: ..\food\fruits\oranges\mandarin.txt to: ..\food\fruits\oranges\2019-07-18 12.24.46.398151_mandarin.txt
    Renaming: ..\food\vegetables\carrot.txt to: ..\food\vegetables\2019-07-18 12.24.46.402496_carrot.txt

    PS C:\src\python-scripting\src> python3 .\list-directory-contents.py
    ..\food\
    Directory: ..\food
    Subdirectory: fruits
    Subdirectory: vegetables

    Directory: ..\food\fruits
    Subdirectory: apples
    Subdirectory: oranges
    File: 2019-07-18 12.24.46.385185_banana.txt
    File: 2019-07-18 12.24.46.389174_strawberry.txt
    File: 2019-07-18 12.24.46.391170_blueberry.txt

    Directory: ..\food\fruits\apples
    File: 2019-07-18 12.24.46.395160_honeycrisp.txt

    Directory: ..\food\fruits\oranges
    File: 2019-07-18 12.24.46.398151_mandarin.txt

    Directory: ..\food\vegetables
    File: 2019-07-18 12.24.46.402496_carrot.txt

    ```

5. Use Python to print the new file system directory names with the last-modified timestamp prepended to it's own text file by entering this command directly in your PowerShell terminal: `python3 list-directory-contents.py > food-directory-last-modified.txt`

Hope you learned a few fun things about using Python scripts for automating basic systems administration tasks. There is, of course, a ton more to know, but we hope this got you started on the right foot. We've shared a few additional resources to continue learning below.

## Additional resources

- [Python Docs: File and Directory Access](https://docs.python.org/3.7/library/filesys.html): Python documentation about working with file systems and using modules for reading the properties of files, manipulating paths in a portable way, and creating temporary files.
- [Learn Python: String_Formatting tutorial](https://www.learnpython.org/en/String_Formatting): More about using the "%" operator for string formatting.
- [10 Python File System Methods You Should Know](https://towardsdatascience.com/10-python-file-system-methods-you-should-know-799f90ef13c2): Medium article about manipulating files and folders With `os` and `shutil`.
- [The Hitchhikers Guide to Python: Systems Administration](https://docs.python-guide.org/scenarios/admin/): An "opinionated guide" that offers overviews and best practices on topics related to Python. This section covers System Admin tools and frameworks. This guide is hosted on GitHub so you can file issues and make contributions.
