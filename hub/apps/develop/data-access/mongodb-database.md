---
title: Use a MongoDB database in a Windows app
description: Learn how to connect your Windows app directly to a MongoDB database and test the connection programmatically.
ms.date: 08/01/2024
ms.topic: how-to
keywords: windows, windows app sdk, mongodb, nosql, database, uwp, wpf, winforms, windows forms, winui
ms.localizationpriority: medium
#customer intent: As a Windows developer, I want to learn how to connect my Windows app directly to a MongoDB database so that I can store and retrieve data.
---

# Use a MongoDB database in a Windows app

This article contains the steps required to enable working with a MongoDB database from a Windows app. It also contains a small code snippet showing how you can interact with the database in code.

## Set up your solution

This example can be used with any WPF, Windows Forms, WinUI 3, and UWP project to connect your Windows app to MongoDB. Follow these steps to install the package and try out the example code to read data from an existing MongoDB database.

1. Open the **Package Manager Console** (View -> Other Windows -> Package Manager Console).
1. Use the command `Install-Package MongoDB.Driver` to install the NuGet package for the official driver for MongoDB.

This will allow you to programmatically access MongoDB databases.

## Test your connection using sample code

The following sample code gets a collection from a remote MongoDB client, then adds a new document to that collection. Then it uses MongoDB APIs to retrieve the new size of the collection as well as the inserted document, and prints them out.

``` csharp
var client = new MongoClient("mongodb://10.xxx.xx.xxx:27017");
IMongoDatabase database = client.GetDatabase("foo");
IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("bar");
var document = new BsonDocument
{
     { "name","MongoDB"},
     { "type","Database"},
     { "count",1},
     { "info",new BsonDocument { { "x", 203 }, { "y", 102 } }}
};
collection.InsertOne(document);
long count = collection.CountDocuments(document);
Console.WriteLine(count);
IFindFluent<BsonDocument, BsonDocument> document1 = collection.Find(document);
Console.WriteLine(document1.ToString());
```

Note that the IP address and database name will need to be customized. The port, 27017, is the default MongoDB port number. In a production application, connection information such as server address and database name should be stored in app configuration rather than hard-coded (see [**Adding Azure App Configuration by using Visual Studio Connected Services**](/visualstudio/azure/vs-azure-tools-connected-services-app-configuration)).

## Related content

- [Use a SQL Server database in a Windows app](sql-server-database.md)
- [Use a MySQL database in a Windows app](mysql-database.md)
