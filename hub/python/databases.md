---
title: Get started using Python on Windows with a database
description: A guide to help you get started using PostgreSQL or MongoDB with Python on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: python, windows 10, postgresql, mongodb, postgres, mongo, microsoft, python on windows, install postgresql on windows, install mongodb on windows, use postgresql with python, use mongodb with python, postgresql on WSL, mongodb on WSL
ms.localizationpriority: medium
ms.date: 07/19/2019
---

# Get started using PostgreSQL or MongoDB with Python on Windows

This step-by-step guide will help you get started connecting your Python app to a database. We chose to focus on two popular options: PostgreSQL and MongoDB.

## Differences between MongoDB and PostgreSQL

[!INCLUDE [Postgres vs Mongo](../includes/postgres-v-mongo.md)]

> [!NOTE]
> You may also want to consider how integrated the framework and tools you're using are with a particular database system. The [Django web framework](./web-frameworks.md#hello-world-tutorial-for-django) seems to be better integrated with PostgreSQL (see the [Django docs](https://docs.djangoproject.com/en/2.2/ref/contrib/postgres/) and [psycopg2](https://github.com/psycopg/psycopg2)). The [Flask web framework](./web-frameworks.md#hello-world-tutorial-for-flask) seems to be better integrated with MongoDB (see [MongoEngine](https://github.com/MongoEngine/flask-mongoengine) and [PyMongo](https://github.com/dcrosta/flask-pymongo)).

## Install PostgreSQL

[!INCLUDE [Install and run PostgresQL](../includes/install-and-run-postgres.md)]

### VS Code support for PostgreSQL

VS Code supports working with PostgreSQL databases via the [PostgreSQL extension](https://marketplace.visualstudio.com/items?itemName=ms-ossdata.vscode-postgresql), you can create, connect to, manage and query PostgreSQL databases from within VS Code.

## Install MongoDB

[!INCLUDE [Install and run Mongo](../includes/install-and-run-mongo.md)]

### VS Code support for MongoDB

VS Code supports working with MongoDB databases via the [Azure CosmosDB extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-cosmosdb), you can create, connect to, manage and query MongoDB databases from within VS Code.

To learn more, visit the VS Code docs: [Working with MongoDB](https://code.visualstudio.com/docs/azure/mongodb).

## Set up profile aliases

[!INCLUDE [Set up profile aliases](../includes/profile-aliases.md)]
