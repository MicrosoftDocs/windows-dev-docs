---
title: Use a Cosmos DB database from a Windows app
description: Learn how to connect your Windows app to a Cosmos DB database and test the connection programmatically.
ms.date: 09/14/2023
ms.topic: article
keywords: windows, windows app sdk, Cosmos DB, azure, cloud, nosql, database, wpf, uwp, winforms, windows forms, winui
ms.localizationpriority: medium
---

# Use a Cosmos DB database from a Windows app

This article contains the steps required to enable working with a Cosmos DB database from a Windows app. It also contains a small code snippet showing how you can interact with the database in code.

## Set up your solution

This example can be used with any WPF, Windows Forms, WinUI 3, and UWP project to connect your Windows app to a Cosmos DB database. Follow these steps to install the package and try out example code for some basic tasks.

Open the **Package Manager Console** (View -> Other Windows -> Package Manager Console). Use the command `Install-Package Microsoft.Azure.Cosmos` to install the NuGet package for the **Azure Cosmos DB for NoSQL client library for .NET**. This will allow you to programmatically access Cosmos DB databases.

Build your project and make sure that the build was successful with no errors.

Next, you'll need to create a Cosmos DB instance in Azure. You can do this by following the steps in [Create a NoSQL database account in Azure Cosmos DB](/azure/cosmos-db/create-cosmosdb-resources-portal).

## Work with Cosmos DB using sample code

The following sample code gets a container from a Cosmos DB instance in Azure, then adds a new item to that container. Then it uses Cosmos DB's NoSQL query API to retrieve the item from the container and output the response status. Note that the endpoint, auth key, and database name will need to be customized based on the Cosmos DB instance you created in the previous section.

> [!NOTE]
> For a complete example, including information about required Cosmos DB setup and configuration, see [Develop a .NET console application with Azure Cosmos DB for NoSQL](/azure/cosmos-db/nosql/tutorial-dotnet-console-app).

```csharp
using Microsoft.Azure.Cosmos;

...

public async Task CosmosSample(string endpoint, string authKey)
{
    // CONNECT
    var client = new CosmosClient(
        accountEndpoint: endpoint,
        authKeyOrResourceToken: authKey
    );
    Database database = client.GetDatabase("sample_customers");
    ContainerProperties properties = new(
        id: "customers",
        partitionKeyPath: "/location"
    );
    Container container = await database.CreateContainerIfNotExistsAsync(properties);

    // WRITE DATA
    string customerId = "1234";
    string state = "WA";
    var customer = new
    {
        id = customerId,
        name = "John Doe",
        location = state
    };
    var createResponse = await container.CreateItemAsync(customer);
    Console.WriteLine($"[Status code: {createResponse.StatusCode}]\t{customerId}");

    // READ DATA
    string sql = "SELECT * FROM customers c WHERE c.id = @id";
    var query = new QueryDefinition(
        query: sql
    ).WithParameter("@id", customerId);
    using var feed = container.GetItemQueryIterator<dynamic>(queryDefinition: query);
    var queryResponse = await feed.ReadNextAsync();
    Console.WriteLine($"[Status code: {queryResponse.StatusCode}]\t{customerId}");
}
```

## See also

- [Azure Cosmos DB for NoSQL client library for .NET](/azure/cosmos-db/nosql/quickstart-dotnet)

- [Use a MySQL database in a Windows app](mysql-database.md)
