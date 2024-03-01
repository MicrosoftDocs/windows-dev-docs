---
title: Install React on Windows
description: Install a React development environment on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
ms.date: 03/01/2024
---

# Install React directly on Windows

This guide will walk through installing React directly on Windows using the [create-react-app](https://github.com/facebook/create-react-app) toolchain.

We recommend following these instructions if you are new to React and just interested in learning. If you are creating a single-page app (SPA) that you would like to use Bash commands or tools with and/or plan to deploy to a Linux server, we recommend that you [install with create-react-app on Windows Subsystem for Linux (WSL)](./react-on-wsl.md).

For more general information about React, deciding between React (web apps), React Native (mobile apps), and React Native for Windows (desktop apps), see the [React overview](./react-overview.md).

> [!WARNING]
> If you've previously installed `create-react-app` on your machine, globally or via `npm install -g create-react-app`, it is recommended to uninstall the package using `npm uninstall -g create-react-app` or `yarn global remove create-react-app` to ensure that `npx` always uses the latest version.

## Create your React app

To install Create React App:

1. Open a terminal(Windows Command Prompt or PowerShell).
2. Create a new project folder: `mkdir ReactProjects` and enter that directory: `cd ReactProjects`.
3. Install create-react-app, a tool that installs all of the dependencies to build and run a full React.js application:

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

Learn more in the [Create React App repo on GitHub](https://github.com/facebook/create-react-app). You can also find or file issues here if something doesn't work or ask questions in the GitHub Discussions tab of the repo.

## Additional resources

- [React docs](https://reactjs.org/)
- [Install Next.js](./nextjs-on-wsl.md)
- [Install Gatsby](./gatsby-on-wsl.md)
- [Install React Native for Windows](https://microsoft.github.io/react-native-windows/docs/getting-started)
- [Install NodeJS on Windows](./nodejs-on-windows.md)
- [Install NodeJS on WSL](./nodejs-on-wsl.md)
- Try the tutorial: [Using React in Visual Studio Code](https://code.visualstudio.com/docs/nodejs/reactjs-tutorial)
- Try the [React learning path](/training/paths/react/)
