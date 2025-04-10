---
ms.assetid: dc686801-edd0-44b1-afb9-20486fd9c34e
title: Data access in Windows apps
description: Learn how to store data on the device in a private database and use object relational mapping in Windows apps.
ms.date: 08/01/2024
ms.topic: concept-article
keywords: windows 10, windows 11, windows app sdk, data, database, relational, tables, sqlite, cosmosdb, mysql
ms.localizationpriority: medium
#customer intent: As a Windows app developer, I want to learn how to store data on the device in a private database and use object relational mapping in Windows apps so that I can create apps that store and retrieve data.
---

# Data access in Windows apps?

You can store data on the user's device by using a SQLite database. You can also connect your Windows app directly to a SQL Server, Cosmos DB, MySQL, or MongoDB database without having to use an external service layer.

## Topics in this section

In this section, you'll learn how to store and retrieve data in a Windows app by using different back-end databases. Each topic provides a guide to help you get started.

| Topic | Description |
|-------|------------|
| [Use a SQLite database on Windows](sqlite-data-access.md) | Shows you how to use SQLite to store and retrieve data in a light-weight database on the user's device. SQLite is a server-less, embedded database engine. |
| [Use a SQL Server database in a Windows app](sql-server-database.md) | Shows you how to connect directly to a SQL Server database and then store and retrieve data by using classes in the [System.Data.SqlClient](/dotnet/api/system.data.sqlclient) namespace. No service layer required. |
| [Use a Cosmos DB database in a Windows app](cosmos-db-data-access.md) | Shows you how to work with a [Cosmos DB](/azure/cosmos-db/introduction) database and test the connection programmatically. Cosmos DB is a cloud-based document database. |
| [Use a MySQL database in a Windows app](mysql-database.md) | Shows you how to connect to a MySQL database and interact with the database in a Windows application. MySQL is an open source, cross-platform relational database. |
| [Use a MongoDB database in a Windows app](mongodb-database.md) | Shows you how to work with a MongoDB database and test the connection programmatically. MongoDB is a cross-platform document database. |

## Related content

- [Customer Orders Database sample](https://github.com/Microsoft/Windows-appsample-customers-orders-database)
- [UWP data access](/windows/uwp/data-access/)
- [.NET data documentation](/ef/dotnet-data/)
