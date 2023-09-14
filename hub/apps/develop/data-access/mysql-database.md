---
title: Use a MySQL database in a Windows app
description: Learn how to connect to a MySQL database from your Windows app, and test your connection using sample code.
ms.date: 12/06/2022
ms.topic: article
keywords: windows, windows app sdk, MySQL, database, uwp, wpf, winforms, windows forms, winui
ms.localizationpriority: medium
---

# Use a MySQL database in a Windows app

This article contains the steps required to enable working with a MySQL database from a Widows app. It also contains a small code snippet showing how you can interact with the database in code.

## Set up your solution

This example can be used with any WPF, Windows Forms, WinUI 3, and UWP project to connect your Windows app to a MySQL database. Follow these steps to install the package and try out the example code to read data from an existing MySQL database.

Open the **Package Manager Console** (View -> Other Windows -> Package Manager Console). Use the command `Install-Package MySql.Data` to install the NuGet package for the MySQL core class library. This will allow you to programmatically access MySQL databases.

## Test your connection using sample code

The following is an example of connecting to and reading from a remote MySQL database. Note that the IP address, credentials, and database name will need to be customized.

``` csharp
const string M_str_sqlcon = "server=10.xxx.xx.xxx;user id=foo;password=bar;database=baz";
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

## See also

- [Use a SQL Server database in a Windows app](sql-server-database.md)
