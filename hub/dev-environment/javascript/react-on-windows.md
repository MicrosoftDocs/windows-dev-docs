---
title: Install React on Windows
description: Install a React development environment on Windows 10.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: React, React JS, install react on windows, install react with windows, react on windows
ms.localizationpriority: medium
ms.date: 04/13/2021
---

# Install React directly on Windows

This guide will walk through installing React directly on Windows using the [create-react-app](https://github.com/facebook/create-react-app) toolchain.

We recommend following these instructions if you are new to React and just interested in learning. If you are creating a single-page app (SPA) that you would like to use Bash commands or tools with and/or plan to deploy to a Linux server, we recommend that you [install with create-react-app on Windows Subsystem for Linux (WSL)](./react-on-wsl.md).

For more general information about React, deciding between React (web apps), React Native (mobile apps), and React Native for Windows (desktop apps), see the [React overview](./react-overview.md).

## Prerequisites

- Install the latest version of Windows 10 (Version 1903+, Build 18362+)
- [Install Windows Subsystem for Linux (WSL)](/windows/wsl/install-win10), including a Linux distribution (like Ubuntu) and make sure it is running in WSL 2 mode. You can check this by opening PowerShell and entering: `wsl -l -v`
- [Install Node.js on WSL 2](./nodejs-on-wsl.md): These instructions use Node Version Manager (nvm) for installation, you will need a recent version of NodeJS to run create-react-app, as well as a recent version of Node Package Manager (npm). For exact version requirements, see the [Create React App website](https://reactjs.org/docs/create-a-new-react-app.html#create-react-app).

## Create your React app

To install the full React toolchain on WSL, we recommend using create-react-app:

1. Open a terminal(Windows Command Prompt or PowerShell).
2. Create a new project folder: `mkdir ReactProjects` and enter that directory: `cd ReactProjects`.
3. Install React using create-react-app, a tool that installs all of the dependencies to build and run a full React.js application:

    ```powershell
    npx create-react-app my-app
    ```

    >[!NOTE]
    > [npx](https://www.npmjs.com/package/npx) is the package runner used by npm to execute packages in place of a global install. It basically creates a temporary install of React so that with each new project you are using the most recent version of React (not whatever version was current when you performed the global install above). Using the NPX package runner to execute a package can also help reduce the pollution of installing lots of packages on your machine.

4. This will first ask for your permission to temporarily install create-react-app and it's associated packages. Once completed, change directories into your new app ("my-app" or whatever you've chosen to call it): `cd my-app`.

5. Start your new React app:

    ```PowerShell
    npm start
    ```

    This command will start up the Node.js server and launch a new browser window displaying your app. You can use **Ctrl + c** to stop running the React app in your command line.

    > [!NOTE]
    > Create React App includes a frontend build pipeline using [Babel](https://babeljs.io/) and [webpack](https://webpack.js.org/), but doesn't handle backend logic or databases. If you are seeking to build a server-rendered website with React that uses a Node.js backend, we recommend [installing Next.js](./nextjs-on-wsl.md), rather than this create-react-app installation, which is intended more for single-page apps. You also may want to consider [installing Gatsby](./gatsby-on-wsl.md) if you want to build a static content-oriented website.

6. When you're ready to deploy your web app to production, running `npm run build` will create a build of your app in the "build" folder. You can learn more in the [Create React App User Guide](https://create-react-app.dev/docs/deployment).

## Additional resources

- [React docs](https://reactjs.org/)
- [Create React App docs](https://create-react-app.dev/docs/getting-started)
- [Should I install on Windows or Windows Subsystem for Linux (WSL)?](./windows-or-wsl.md)
- [Install Next.js](./nextjs-on-wsl.md)
- [Install Gatsby](./gatsby-on-wsl.md)
- [Install React Native for Windows](https://microsoft.github.io/react-native-windows/docs/getting-started)
- [Install NodeJS on Windows](./nodejs-on-windows.md)
- [Install NodeJS on WSL](./nodejs-on-wsl.md)
- Try the tutorial: [Using React in Visual Studio Code](https://code.visualstudio.com/docs/nodejs/reactjs-tutorial)
- Try the Microsoft Learn online course: [React](/learn/paths/react/)