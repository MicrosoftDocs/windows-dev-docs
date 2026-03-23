---
title: Install React on Windows
description: Install a React development environment directly on Windows using Vite and Node.js.
ms.topic: install-set-up-deploy
ms.date: 03/23/2026
---

# Install React directly on Windows

This guide walks through setting up a React development environment directly on Windows using the [Vite](https://vitejs.dev/) frontend tooling.

We recommend these instructions if you are new to React or building a project that does not need a Linux environment. If you plan to use Bash tooling extensively or deploy to a Linux server, consider [installing React on WSL](./react-on-wsl.md) instead.

For background on React and the different scenarios — web apps, mobile apps (React Native), and native desktop apps (React Native for Desktop) — see the [React overview](./react-overview.md).

## Prerequisites

- **[Node.js](./nodejs-on-windows.md)**: Required to run Vite and npm. Install using nvm-windows for easy version management.

## Create your React app

1. Open a terminal (Windows Command Prompt or PowerShell).

2. Create a new project folder and enter it:

    ```powershell
    mkdir ReactProjects
    cd ReactProjects
    ```

3. Create a new React app using Vite:

    ```powershell
    npm create vite@latest my-react-app -- --template react
    ```

    Vite will scaffold a new React project in the `my-react-app` folder.

4. Navigate into the new app folder and install dependencies:

    ```powershell
    cd my-react-app
    npm install
    ```

5. Start the local development server:

    ```powershell
    npm run dev
    ```

    Your app will be available at `http://localhost:5173`. Use **Ctrl+C** to stop the server.

6. When you are ready to deploy, build a production bundle:

    ```powershell
    npm run build
    ```

    Output is placed in the `dist` folder. See [Deploying a Static Site](https://vitejs.dev/guide/static-deploy.html) for hosting options.

> [!NOTE]
> Vite is ideal for single-page apps (SPAs). If you need server-side rendering or a Node.js backend, consider [Next.js](https://nextjs.org/docs) instead. For static site generation, see [Gatsby](https://www.gatsbyjs.com/docs/).

## Additional resources

- [React docs](https://react.dev)
- [Vite docs](https://vitejs.dev/)
- [Using React in Visual Studio Code](https://code.visualstudio.com/docs/nodejs/reactjs-tutorial)
- [React learning path on Microsoft Learn](/training/paths/react/)
- [Install NodeJS on Windows](./nodejs-on-windows.md)
- [React Native for Desktop (UWP/WPF)](https://microsoft.github.io/react-native-windows/docs/getting-started)
