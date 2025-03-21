---
title: Command Palette Extensibility
description: The Command Palette provides a full extension model, allowing you to create custom experiences for the palette. Learn how to create an extension and publish it.
ms.date: 2/28/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to develop an extension for the Command Palette.
---

# Creating an extension

The fastest way to get started writing extensions is from the Command Palette itself. Just run the "Create a new extension" command, fill out the fields to populate the template project, and you should be ready to start.

The form will ask you for the following information:
* **ExtensionName**: The name of your extension. This will be used as the name of the project and the name of the class that implements your commands. Make sure it's a valid C# class name - it shouldn't have any spaces or special characters, and should start with a capital letter.
* **Extension Display Name**: The name of your extension as it will appear in the Command Palette. This can be a more human-readable name. 
* **Output Path**: The folder where the project will be created. 
  * The project will be created in a subdirectory of the path you provided. 
  * If this path doesn't exist, it will be created for you.

![](../../images/command-palette/create-extension-page.png)

Once you submit the form, Command Palette will automatically generate the project for you. At this point, your projects structure should look like the following:

```plaintext
ExtensionName/
│   Directory.Build.props
│   Directory.Packages.props
│   nuget.config
│   ExtensionName.sln
└───ExtensionName
    │   app.manifest
    │   Package.appxmanifest
    │   Program.cs
    │   ExtensionName.cs
    │   ExtensionName.csproj
    │   ExtensionNameCommandsProvider.cs
    ├───Assets
    │   <A bunch of placeholder images>
    ├───Pages
    │   ExtensionNamePage.cs
    └───Properties
        │   launchSettings.json
        └───PublishProfiles
                win-arm64.pubxml
                win-x64.pubxml
```

(with `ExtensionName` replaced with the name you provided)

From here, you can immediately build the project and run it. Once your package is deployed and running, Command Palette will automatically discover your extension and load it into the palette. 

> [!TIP]
> Make sure you deploy your app! Just **build**ing your application won't update the package in the same way that deploying it will.

> [!WARNING]
> Running "ExtensionName (Unpackaged)" from Visual Studio will not **deploy** your app package.

You should be able to see your extension in the Command Palette at the end of the list of commands. Entering that command should take you to the page for your command, and you should see a single command that says "TODO: Implement your extension here".

![](../../images/command-palette/initial-created-extension-list.png)

Congrats! You've made your first extension! Now let's go ahead and actually add some commands to it.

When you make changes to your extension, you can rebuild your project and deploy it again. Command Palette will **not** notice changes to packages that are re-ran through Visual Studio, so you'll need to manually run the "**Reload**" command to force Command Palette to re-instantiate your extension.

Let's make that command do something.

We can start by navigating to the `ExtensionNamePage.cs` file. This file is the [`ListPage`](./microsoft-commandpalette-extensions-toolkit/listpage.md) that will be displayed when the user selects your extension. In there you should see:

```csharp   
    public DocsSamplePage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "My sample extension";
        Name = "Open";
    }
    public override IListItem[] GetItems()
    {
        return [
            new ListItem(new NoOpCommand()) { Title = "TODO: Implement your extension here" }
        ];
    }
```

https://learn.microsoft.com/windows/powertoys/command-palette/overview


### Next up: [Update a list of commands](update-a-list-of-commands.md)

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extension samples](samples.md)
