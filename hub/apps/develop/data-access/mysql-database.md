---
title: Use a MySQL database in a Windows app
description: Learn how to connect to a MySQL database from your Windows app, and test your connection using sample code.
ms.date: 01/23/2025
ms.collection: ce-skilling-ai-copilot
ms.custom: copilot-scenario-highlight
ms.topic: how-to
keywords: windows, windows app sdk, MySQL, database, uwp, wpf, winforms, windows forms, winui
ms.localizationpriority: medium
#customer intent: As a Windows developer, I want to learn how to connect to a MySQL database from my Windows app so that I can store and retrieve data.
---

# Use a MySQL database in a Windows app

This article contains the steps required to enable working with a MySQL database from a Windows app. It also contains a small code snippet showing how you can interact with the database in code.

> [!TIP]
> You can use AI assistance to [create a MySQL connection string with GitHub Copilot](#building-a-connection-string-with-github-copilot).

## Set up your solution

This example can be used with any WPF, Windows Forms, WinUI 3, and UWP project to connect your Windows app to a MySQL database. Follow these steps to install the package and try out the example code to read data from an existing MySQL database.

1. Open the **Package Manager Console** (View -> Other Windows -> Package Manager Console).
1. Use the command `Install-Package MySql.Data` to install the NuGet package for the MySQL core class library.

This will allow you to programmatically access MySQL databases.

> [!NOTE]
> [MySQL Connector/NET](https://dev.mysql.com/downloads/connector/net/) version 6.4.4 or later is required to use the `MySql.Data` package with Windows authentication.

## Test your connection using sample code

The following is an example of connecting to and reading from a remote MySQL database. Note that the server address and database name will need to be customized.

``` csharp
const string M_str_sqlcon = "Server=myServerAddress;Database=myDataBase;IntegratedSecurity=yes;Uid=auth_windows;";
using (var mySqlCn = new MySqlConnection(M_str_sqlcon))
{
    using (var mySqlCmd = new MySqlCommand("select * from table1", mySqlCn))
    {
        mySqlCn.Open();
        using (MySqlDataReader mySqlReader = mySqlCmd.ExecuteReader(CommandBehavior.CloseConnection))
        {
            while (mySqlReader.Read())
            {
                Debug.WriteLine($"{mySqlReader.GetString(0)}:{mySqlReader.GetString(1)}");
            }
        }
    }
}
```

> [!IMPORTANT]
> In production applications, connection information should be stored securely in app configuration (see [**Adding Azure App Configuration by using Visual Studio Connected Services**](/visualstudio/azure/vs-azure-tools-connected-services-app-configuration)). Connection strings and other secrets should not be hard-coded.

## Building a connection string with GitHub Copilot

You can use GitHub Copilot to build the connection string for your MySQL database. You can customize the prompt to create a connection string per your requirements.

The following text shows an example prompt for Copilot Chat that generates a connection string similar to the one shown in the previous code snippet:

```copilot-prompt
Show me how to create a MySQL connection string to a server named myServerAddress and a database called myDatabase. Use Windows authentication.
```

GitHub Copilot is powered by AI, so surprises and mistakes are possible. For more information, see [Copilot FAQs](https://aka.ms/copilot-general-use-faqs).

Learn more about [GitHub Copilot in Visual Studio](/visualstudio/ide/visual-studio-github-copilot-install-and-states) and [GitHub Copilot in VS Code](https://code.visualstudio.com/docs/copilot/overview).

## Related content

- [Use a SQL Server database in a Windows app](sql-server-database.md)
- [Data access in Windows apps](index.md)
