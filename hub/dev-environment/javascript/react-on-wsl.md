---
title: Install React on Windows Subsystem for Linux
description: Install React on Windows Subsystem for Linux (WSL) and start developing web apps using Vite and Node.js.
ms.topic: install-set-up-deploy
keywords: react, install react on wsl, react wsl, react linux on windows, react vite wsl
ms.date: 03/23/2026
---

# Install React on Windows Subsystem for Linux

This guide walks through setting up a React development environment on WSL (Windows Subsystem for Linux) using the [Vite](https://vitejs.dev/) frontend tooling.

WSL is recommended if you plan to deploy to a Linux server, use Docker containers, or work with Bash-based tooling. If you are new to React and just want to get started quickly, consider [installing React directly on Windows](./react-on-windows.md) instead.

For background on React and the different scenarios — web apps, mobile apps (React Native), and native desktop apps (React Native for Desktop) — see the [React overview](./react-overview.md).

## Prerequisites

- **[WSL 2](/windows/wsl/install)**: Install WSL with a Linux distribution (Ubuntu recommended) and confirm it is running in WSL 2 mode: `wsl -l -v`. Requires Windows 10 version 2004 or later, or Windows 11.
- **[Node.js on WSL 2](./nodejs-on-wsl.md)**: Install Node.js inside your WSL distribution using nvm. Do not use the Windows-installed version of Node.js inside WSL.

> [!IMPORTANT]
> Store your project files inside the WSL filesystem (e.g., `~/projects`), not on the mounted Windows drive (`/mnt/c/`). Working across the filesystem boundary significantly slows down install and build times.

## Create your React app

1. Open your WSL terminal (e.g., Ubuntu).

2. Create a new project folder and enter it:

    ```bash
    mkdir ~/ReactProjects
    cd ~/ReactProjects
    ```

3. Create a new React app using Vite:

    ```bash
    npm create vite@latest my-react-app -- --template react
    ```

    Vite will scaffold a new React project in the `my-react-app` folder.

4. Navigate into the new app folder and install dependencies:

    ```bash
    cd my-react-app
    npm install
    ```

5. Start the local development server:

    ```bash
    npm run dev
    ```

    Your app will be available at `http://localhost:5173`. Use **Ctrl+C** to stop the server.

6. When you are ready to deploy, build a production bundle:

    ```bash
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
- [Install NodeJS on WSL](./nodejs-on-wsl.md)
- [React Native for Desktop (UWP/WPF)](https://microsoft.github.io/react-native-windows/docs/getting-started)
