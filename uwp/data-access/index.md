---
ms.assetid: 76776b0f-3163-48c9-835b-3f4213968079
title: Data access
description: This section discusses storing data on the device in a private database and using object relational mapping in Universal Windows Platform (UWP) apps.
ms.date: 09/14/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, data, database, relational, tables, sqlite, mongodb, mysql
ms.localizationpriority: medium
---

# Data access

You can store data on the user's device by using a SQLite database. You can also connect your app directly to a SQL Server, MongoDB, or MySQL database without having to use any sort of service layer.

| Topic | Description|
|-------|------------|
| [Use a SQLite database in a UWP app](sqlite-databases.md) | Shows you how to use SQLite to store and retrieve data in a light-weight database on the users device. SQLite is a server-less, embedded database engine. |
| [Use a SQL server database in a UWP app](sql-server-databases.md) | Shows you how to connect directly to a SQL Server database and then store and retrieve data by using classes in the [System.Data.SqlClient](/dotnet/api/system.data.sqlclient) namespace. No service layer required. |
| [Use a MongoDB database in a Windows app](/windows/apps/develop/data-access/mongodb-database) | Shows you how to work with a MongoDB database and and test the connection programmatically. MongoDB is a cross-platform document database. |
| [Use a MySQL database in a Windows app](/windows/apps/develop/data-access/mysql-database) | Shows you how to connect to a MySQL database and interact with the database in a Windows application. MySQL is an open source, cross-platform relational database. |
| [Use a Cosmos DB database in a Windows app](/windows/apps/develop/data-access/cosmos-db-data-access) | Shows you how to work with a [Cosmos DB](/azure/cosmos-db/introduction) database and and test the connection programmatically. Cosmos DB is a cloud-based document database. |

## Related topics

* [Customer Orders Database sample](https://github.com/Microsoft/Windows-appsample-customers-orders-database)
