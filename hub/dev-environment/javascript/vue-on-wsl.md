---
title: Install Vue.js on WSL
description: A guide to help you get started using Vue.js on Windows Subsystem for Linux using npm create vue@latest and Vite.
ms.topic: install-set-up-deploy
keywords: Vue, Vue.js, windows, install vue on wsl, install vue on windows subsystem for linux, vue wsl
ms.localizationpriority: medium
ms.date: 03/23/2026
---

# Install Vue.js on Windows Subsystem for Linux

A guide to set up a Vue.js development environment on Windows Subsystem for Linux (WSL). For background, see the [Vue.js overview](./vue-overview.md).

Vue can be installed [directly on Windows](./vue-on-windows.md) or on WSL. WSL is recommended if you plan to interact with a Node.js backend, deploy to Linux servers, or follow tutorials that use Bash commands.

## Prerequisites

- [Install Windows Subsystem for Linux (WSL)](/windows/wsl/install), including a Linux distribution (like Ubuntu) running in WSL 2 mode. Verify with: `wsl -l -v`
- [Install Node.js on WSL 2](./nodejs-on-wsl.md): This includes a version manager, package manager, Visual Studio Code, and the Remote Development extension.

> [!IMPORTANT]
> Store your project files inside the WSL filesystem (e.g., `~/projects`), not on the mounted Windows drive (`/mnt/c/`). Working across the filesystem boundary significantly slows down install and build times.

## Create a Vue project

The recommended way to start a new Vue 3 project is `npm create vue@latest`, which uses [create-vue](https://github.com/vuejs/create-vue) — the official Vite-based scaffolding tool:

1. Open your WSL terminal (e.g., Ubuntu).

2. Navigate to your projects directory:

   ```bash
   mkdir -p ~/projects
   cd ~/projects
   ```

3. Create a new Vue project:

   ```bash
   npm create vue@latest
   ```

   The installer prompts you to name your project and choose optional features (TypeScript, JSX support, Vue Router, Pinia state management, Vitest, ESLint).

4. Navigate into the project folder, install dependencies, and start the dev server:

   ```bash
   cd <your-project-name>
   npm install
   npm run dev
   ```

   Your app will be available at `http://localhost:5173`.

## Additional resources

- [Vue docs](https://vuejs.org/guide/introduction.html)
- [Vue.js overview](./vue-overview.md)
- [Install Vue.js on Windows](./vue-on-windows.md)
- [Take your first steps with Vue.js](/training/paths/vue-first-steps/) learning path
- [Vue tutorial with VS Code](https://code.visualstudio.com/docs/nodejs/vuejs-tutorial)
