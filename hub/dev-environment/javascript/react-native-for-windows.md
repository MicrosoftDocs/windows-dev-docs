---
title: React Native for Windows desktop app development
description: Install React Native for Windows and get started with Windows desktop app development using React Native components.
ms.topic: article
keywords: react native for windows, windows, react native, install react native on windows, install react native for windows, build a desktop app with react, create a windows app with react, react for desktop apps, npx react-native, react-native-windows-init 
ms.date: 03/30/2021
---

# Get started build a desktop app with React Native for Windows

[React Native for Windows](https://microsoft.github.io/react-native-windows) allows you to create a Universal Windows Platform (UWP) app using React.

## Overview of React Native

React Native is an [open-source](https://github.com/facebook/react-native) mobile application framework created by Facebook. It is used to develop applications for Android, iOS, Web and UWP (Windows) providing native UI controls and full access to the native platform. Working with React Native requires an understanding of JavaScript fundamentals.

For more general information about React, see the [React overview](./react-overview.md) page.

## Prerequisites

The setup requirements for using React Native for Windows can be found on the [System Requirements](https://microsoft.github.io/react-native-windows/docs/rnw-dependencies) page. Ensure Developer Mode is turned ON in Windows Settings App.

## Install React Native for Windows

You can create a Windows desktop app using React Native for Windows by following these steps.

1. Open a command line window (terminal) and navigate to the directory where you want to create your Windows desktop app project.
2. You can use this command with the Node Package Executor (NPX) to create a React Native project without the need to install locally or globally install additional tools. The  command will generate a React Native app in the directory specified by `<projectName>`.

    ```powershell
    npx react-native init <projectName>
    ```
    If you want to start a new project with a specific React Native version, you can use the `--version` argument. For information about versions of React Native, see [Versions - React Native](https://reactnative.dev/versions).
   
    ```powershell
    npx react-native@X.XX.X init  <projectName> --version X.XX.X
    ```

4. Switch to the project directory and run the following command to install the React Native for Windows packages:

    ```powershell
    cd projectName
    npx react-native-windows-init --overwrite
    ```

5. To run the app, first launch your web browser (ie. Microsoft Edge), then execute the following command:

    ```powershell
    npx react-native run-windows
    ```

## Debug your React Native desktop app using Visual Studio

- [Install Visual Studio 2022](/visualstudio/install/install-visual-studio) with the following options checked:
  - Workloads: Universal Windows Platform development & Desktop development with C++.
  - Individual Components: Development activities & Node.js development support.

- Open the solution file in the application folder in Visual Studio (e.g., AwesomeProject/windows/AwesomeProject.sln if you used AwesomeProject as \<projectName>).

- Select the Debug configuration and the x64 platform from the combo box controls to the left of the Run button and underneath the Team and Tools menu item.

- Run `yarn start` from your project directory, and wait for the React Native packager to report success.

- Select **Run** to the right of the platform combo box control in VS, or select the Debug->Start without Debugging menu item. You now see your new app and Chrome should have loaded http://localhost:8081/debugger-ui/ in a new tab.

- Select the F12 key or Ctrl+Shift+I to open Developer Tools in your web browser.

## Debug your React Native desktop app using Visual Studio Code

- [Install Visual Studio Code](https://code.visualstudio.com/download)
- Open your applications folder in VS Code.
- Install the [React Native Tools plugin for VS Code](https://marketplace.visualstudio.com/items?itemName=msjsdiag.vscode-react-native).
- Create a new file in the applications root directory, .vscode/launch.json and paste the following configuration:
    ```json
    {
        "version": "0.2.0",
        "configurations": [
            {
                "name": "Debug Windows",
                "cwd": "${workspaceFolder}",
                "type": "reactnative",
                "request": "launch",
                "platform": "windows"
            }
        ]
    }
    ```

- Press F5 or navigate to the debug menu (alternatively press Ctrl+Shift+D) and in the Debug dropdown select "Debug Windows" and press the green arrow to run the application.

## Additional resources

- [React Native for Windows docs](https://microsoft.github.io/react-native-windows/docs/getting-started)
- [React Native docs](https://reactnative.dev/docs/getting-started)
- [React docs](https://reactjs.org/)
- [Install NodeJS on Windows](./nodejs-on-windows.md)
- Try the [React learning path](/training/paths/react/)
