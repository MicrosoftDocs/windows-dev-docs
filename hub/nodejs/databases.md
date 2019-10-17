---
title: Get started connecting Node.js apps to a database
description: Get started connecting Node.js apps to a database on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: NodeJS, Node.js, windows 10, microsoft, learning nodejs, node on windows, node on wsl, node on linux on windows, install node on windows, nodejs with vs code, develop with node on windows, develop with nodejs on windows, install node on WSL, NodeJS on Windows Subsystem for Linux
ms.localizationpriority: medium
ms.date: 09/19/2019
---

# Get started using MongoDB or PostgreSQL with Node.js on Windows

Node.js applications often need to persist data, which can happen through files, local storage, cloud services or databases. This step-by-step guide will help you get started connecting your Node.js app to a database. We chose to focus on two popular options: MongoDB and PostgreSQL.

## Prerequisites

This guide assumes that you've already completed the steps to [set up your Node.js development environment with WSL 2](./setup-on-wsl2.md), including:

- Install Windows 10 Insider Preview build 18932 or later.
- Enable the WSL 2 feature on Windows.
- Install a Linux distribution (Ubuntu 18.04 for our examples). You can check this with: `wsl lsb_release -a`
- Ensure your Ubuntu 18.04 distribution is running in WSL 2 mode. (WSL can run distributions in both v1 or v2 mode.) You can check this by opening PowerShell and entering: `wsl -l -v`
- Using PowerShell, set Ubuntu 18.04 as your default distribution, with: `wsl -s ubuntu 18.04`

## Differences between MongoDB and PostgreSQL

[!INCLUDE [Postgres vs Mongo](../includes/postgres-v-mongo.md)]

## Install MongoDB

[!INCLUDE [Install and run Mongo](../includes/install-and-run-mongo.md)]

### VS Code support for MongoDB

VS Code supports working with MongoDB databases via the [Azure CosmosDB extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-cosmosdb), you can create, manage and query MongoDB databases from within VS Code.

To learn more, visit the VS Code docs: [Working with MongoDB](https://code.visualstudio.com/docs/azure/mongodb).

Learn more in the MongoDB docs:

- [Introduction to using MongoDB](https://docs.mongodb.com/manual/introduction/)
- [Create users](https://docs.mongodb.com/manual/tutorial/create-users/)
- [Connect to a MongoDB instance on a remote host](https://docs.mongodb.com/manual/mongo/#mongodb-instance-on-a-remote-host)
- [CRUD: Create, Read, Update, Delete](https://docs.mongodb.com/manual/crud/)
- [Reference Docs](https://docs.mongodb.com/manual/reference/)

## Install PostgreSQL

[!INCLUDE [Install and run PostgresQL](../includes/install-and-run-postgres.md)]

### VS Code support for PostgreSQL

VS Code supports working with PostgreSQL databases via the [PostgreSQL extension](https://marketplace.visualstudio.com/items?itemName=ms-ossdata.vscode-postgresql), you can create, connect to, manage and query PostgreSQL databases from within VS Code.

## Set up profile aliases

[!INCLUDE [Set up profile aliases](../includes/profile-aliases.md)]
