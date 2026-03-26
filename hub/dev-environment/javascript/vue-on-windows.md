---
title: Install Vue.js directly on Windows
description: A guide to help you get started using Vue.js on Windows using npm create vue@latest and Vite.
ms.topic: install-set-up-deploy
ms.date: 03/23/2026
---

# Install Vue.js directly on Windows

A guide to set up a Vue.js development environment on Windows. For background, see the [Vue.js overview](./vue-overview.md).

Vue can be installed directly on Windows or on the Windows Subsystem for Linux (WSL). If you plan to interact with a Node.js backend, deploy to Linux servers, or follow tutorials that use Bash commands, consider [installing Vue on WSL](./vue-on-wsl.md) instead.

## Prerequisites

- [Install Node.js on Windows](./nodejs-on-windows.md): This includes a version manager, package manager, and Visual Studio Code.

## Create a Vue project

The recommended way to start a new Vue 3 project is `npm create vue@latest`, which uses [create-vue](https://github.com/vuejs/create-vue) — the official Vite-based scaffolding tool:

1. Open PowerShell or Windows Command Prompt.

2. Navigate to your projects directory:

   ```powershell
   cd C:\Users\YourName\Projects
   ```

3. Create a new Vue project:

   ```powershell
   npm create vue@latest
   ```

   The installer prompts you to name your project and choose optional features (TypeScript, JSX support, Vue Router, Pinia state management, Vitest, ESLint).

4. Navigate into the project folder, install dependencies, and start the dev server:

   ```powershell
   cd <your-project-name>
   npm install
   npm run dev
   ```

   Your app will be available at `http://localhost:5173`.

## Additional resources

- [Vue docs](https://vuejs.org/guide/introduction.html)
- [Vue.js overview](./vue-overview.md)
- [Install Vue.js on WSL](./vue-on-wsl.md)
- [Take your first steps with Vue.js](/training/paths/vue-first-steps/) learning path
- [Vue tutorial with VS Code](https://code.visualstudio.com/docs/nodejs/vuejs-tutorial)
