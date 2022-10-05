---
title: Use a MySQL database in a UWP app
description: Learn how to connect to a MySQL database from your UWP app, and test your connection using sample code.
ms.date: 10/03/2022
ms.topic: article
keywords: windows 10, windows 11, uwp, MySQL, database
ms.localizationpriority: medium
---

# Use a MySQL database

This article contains the steps required to enable working with a MySQL database from a UWP app. It also contains a small code snippet showing how you can interact with the database in code.

## Set up your solution

To connect your app directly to a MySQL database, ensure that the minimum version of your project targets the Fall Creators update (Build 16299) or newer.  You can find that information in the properties page of your UWP project.

![Image of the Targeting property pane in VisualStudio showing the target and minimum versions set to the Fall Creators Update](images/min-version-fall-creators.png)

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
