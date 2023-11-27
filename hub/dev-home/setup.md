---
title: Set up your Windows development environment with Dev Home
description: Get your machine to a development-ready state with Dev Home's integrated setup tool whether you prefer using the step-by-step graphical interface to walk through the setup process or using a WinGet Configuration file with pre-defined setup requirements.
ms.date: 05/23/2023
ms.topic: overview
---

# Set up your Windows development environment with Dev Home

The Dev Home machine configuration tool brings all of your dev environment set up tasks into one place, enabling you to efficiently set up a new machine or onboard new projects.

Avoid all of the fractured and tedious processes typically involved in getting your machine ready for development. Dev Home streamlines the process of searching for project requirements, cloning repositories, and finding specific versions of software and tools to install. Manage multiple tool sign-ins, minimize context switching, and reach productivity faster so you can focus on what you do best - developing.

![Screenshot of the Dev Home Machine configuration tool.](../images/devhome-machine-config.png)

## Machine configuration

Dev Home Machine configuration can manage everything you need to get to your machine's development environment to a ready-to-code state.

When you select **Machine configuration**, Dev Home will provide multiple set up options:

- **End-to-end setup**: Install applications, clone repositories, and add all of the requirements for a new development project using the built-in graphical configuration interface to enable unattended setup of your environment. The step-by-step tool will walk you through everything you need, including suggestions for popular dev tools or repositories connected to your GitHub account. Once you've made all of your choices, sit back and let Dev Home handle the rest.

- **Run a configuration file for an existing setup**: Use a [WinGet Configuration file](../package-manager/configuration/index.md) to consolidate all of your machine setup and project onboarding tasks into a single file, making the process of setting up your development environment reliable and repeatable. WinGet Configuration files use a YAML format with a JSON schema applying Windows Package Manager and PowerShell Desired State Configuration (DSC) Resource modules to handle every aspect of your machine set up. Remove any worry over finding the right software version, packages, tools, frameworks, and settings when onboarding to a new team or project. Be sure to [check the trustworthiness of a WinGet Configuration file](../package-manager/configuration/check.md) before running it.

- **Clone repositories**: Once you have connected your credentials using the [Dev Home GitHub extension](extensions.md#dev-home-github-extension), you can use Dev Home to clone repositories on to your machine.

- **Install applications**: Use Dev Home to discover and install software applications -- one at a time or have Dev Home install several while you take a snack break.

- **Add a Dev Drive**: To add a storage volume that utilizes ReFS and optimized security settings to be more performant for development-focused scenarios, consider adding Dev Drive. You must currently be running a Windows Insider Program Build on the Dev Channel in order to use Dev Drive. Learn more in the [Dev Drive](../dev-drive/index.md) docs.

## Clone a GitHub repo and store it on a Dev Drive

When using Dev Home to clone a GitHub repository, once you have selected a repo (or multiple repos), you can select which storage drive to clone them to. If you have already [set up a Dev Drive](../dev-drive/index.md#how-to-set-up-a-dev-drive), it will be used as the default path when cloning a repository.

If you have not yet created a Dev Drive, you will have the option to create one using Dev Home. Check the box to optimize the performance of your workloads with a Dev Drive. Then you can customize a few options, such as the drive letter, name, size, and location of the dynamic VHDX on which the Dev Drive will be created. The name will be used for both the VHDX file and the Dev Drive. By default, the options are the next available drive letter, size of 50GB, and created at `%userprofile%\DevDrives`.  

![Screenshot of Dev Home Machine configuration being used to clone a repository.](../images/devhome-github-setup.png)

Learn more about what you can do with **[Dev Home](./index.md)**.
