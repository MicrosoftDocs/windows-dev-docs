---
author: laurenhughes
ms.assetid: ff2523cb-8109-42be-9dfc-cb5d09002574
title: Create and convert a source content group map
description: To get your Universal Windows Platform (UWP) app ready for UWP App Streaming Install, you'll need to create a content group map. This article will help you with the specifics of creating and converting a content group map while providing some tips and tricks along the way.
ms.author: lahugh
ms.date: 4/05/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, content group map, streaming install, uwp app streaming install, source content group map
localizationpriority: medium
---

# Create and convert a source content group map

To get your Universal Windows Platform (UWP) app ready for UWP App Streaming Install, you'll need to create a content group map. This article will help you with the specifics of creating and converting a content group map while providing some tips and tricks along the way.

## Creating the source content group map

You'll need to create a `SourceAppxContentGroupMap.xml` file, and then either use Visual Studio or the **MakeAppx.exe** tool to convert this file to the final version: `AppxContentGroupMap.xml`. It's possible to skip a step by creating the `AppxContentGroupMap.xml` from scratch, but it's recommended (and generally easier) to create the `SourceAppxContentGroupMap.xml` and convert it, since wildcards are not allowed in the `AppxContentGroupMap.xml` (and they're really helpful). 

Let's walk through a simple scenario where UWP App Streaming Install is beneficial. 

Say you've created a UWP game, but the size of your final app is over 100 GB. That's going to take a long time to download from the Microsoft Store, which can be inconvenient. If you choose to use UWP App Streaming Install, you can specify the order in which your app's files are downloaded. By telling the Store to download essential files first, the user will be able to engage with your app sooner while other non-essential files are downloaded in the background.

> [!NOTE]
> Using UWP App Streaming Install heavily relies on your app's file organization. It's recommended that you think about your app's content layout with respect to UWP App Streaming Install as soon as possible to make segmenting your app's files simpler.

First, we'll create a `SourceAppxContentGroupMap.xml` file.

Before we get in to the details, here's an example of a simple, complete `SourceAppxContentGroupMap.xml` file:

```xml
<?xml version="1.0" encoding="utf-8"?>  
<ContentGroupMap xmlns="http://schemas.microsoft.com/appx/2016/sourcecontentgroupmap" 
                 xmlns:s="http://schemas.microsoft.com/appx/2016/sourcecontentgroupmap"> 
    <Required>
        <ContentGroup Name="Required">
            <File Name="StreamingTestApp.exe"/>
        </ContentGroup>
    </Required>
    <Automatic>
        <ContentGroup Name="Level2">
            <File Name="Assets\Level2\*"/>
        </ContentGroup>
        <ContentGroup Name="Level3">
            <File Name="Assets\Level3\*"/>
        </ContentGroup>
    </Automatic>
</ContentGroupMap>
```

There are two main components to a content group map: the **required** section, which contains the required content group, and the **automatic** section, which can contain multiple automatic content groups.

### Required content group

The required content group is a single content group within the `<Required>` element of the `SourceAppxContentGroupMap.xml`. A required content group should contain all of the essential files necessary to launch the app with the minimal user experience. Due to .NET Native compilation, all code (the application executable) must be part of the required group, leaving assets and other files for the automatic groups.

For example, if your app is a game, the required group may include files used in the main menu or game home screen.

Here's the snippet from our original `SourceAppxContentGroupMap.xml` example file: 
```xml
<Required>
    <ContentGroup Name="Required">
        <File Name="StreamingTestApp.exe"/>
    </ContentGroup>
</Required>
```

There are a few important things to notice here:

- The `<ContentGroup>` within the `<Required>` element **must** be named "Required." This name is reserved for the required content group only, and cannot be used with any other `<ContentGroup>` in the final content group map.
- There's only one `<ContentGroup>`. This is intentional, since there should be only one group of essential files.
- The file in this example is a single `.exe` file. A required content group isn't restricted to one file, there can be several. 

An easy way to get started writing this file is to open up a new page in your favorite text editor, do a quick "Save As" of your file to your app's project folder, and name your newly created file: `SourceAppxContentGroupMap.xml`.

> [!IMPORTANT]
> If you are developing a C++ UWP app, you will need to adjust the file properties of your `SourceAppxContentGroupMap.xml`. Set the `Content` property to **true** and the `File Type` property to **XML File**. 

When you're creating the `SourceAppxContentGroupMap.xml`, it's helpful to take advantage of using wildcards in file names, for more info, see the [Tips and tricks for using wildcards](#wildcards) section.

If you developed your app using Visual Studio, it's recommended that you include this in your required content group:

```xml
<File Name="*"/>
<File Name="WinMetadata\*"/>
<File Name="Properties\*"/>
<File Name="Assets\*Logo*"/>
<File Name="Assets\*SplashScreen*"/>
```

Adding the single wildcard file name will include files added to the project directory from Visual Studio, such as the app executable or DLLs. The WinMetadata and Properties folders are to include the other folders Visual Studio generates. The Assets wildcards are to select the Logo and SplashScreen images that are necessary for the app to be installed.

Note that you cannot use the double wild card, "**", at the root of the file structure to include every file in the project since this will fail when attempting to convert `SourceAppxContentGroupMap.xml` to the final `AppxContentGroupMap.xml`.

It's also important to note that footprint files (AppxManifest.xml, AppxSignature.p7x, resources.pri, etc.) should not be included in the content group map. If footprint files are included within one of the wildcard file names you specify, they will be ignored.

### Automatic content groups

Automatic content groups are the assets that are downloaded in the background while the user is interacting with the already downloaded content groups. These contain any additional files that are not essential to launching the app. For example, you could break up automatic content groups in to different levels, defining each level as a separate content group. As noted in the required content group section: due to .NET Native compilation, all code (the application executable) must be part of the required group, leaving assets and other files for the automatic groups.

Let's take a closer look at the automatic content group from our `SourceAppxContentGroupMap.xml` example:
```xml
<Automatic>
    <ContentGroup Name="Level2">
        <File Name="Assets\Level2\*"/>
    </ContentGroup>
    <ContentGroup Name="Level3">
        <File Name="Assets\Level3\*"/>
    </ContentGroup>
</Automatic>
```

The layout of the automatic group is pretty similar to the required group, with a few exceptions:

- There are multiple content groups.
- Automatic content groups can have unique names except for the name "Required" which is reserved for the required content group.
- Automatic content groups cannot contain **any** files from the required content group. 
- An automatic content group can contain files that are also in other automatic content groups. The files will be downloaded only once, and will be downloaded with the first automatic content group that contains them.

#### Tips and tricks for using wildcards<a name="wildcards"></a>

The file layout for content group maps is always relative to your project root folder.

In our example, wildcards are used within both `<ContentGroup>` elements to retrieve all files within one file level of "Assets\Level2" or "Assets\Level3." If you're using a deeper folder structure, you can use the double wildcard:

```xml
<ContentGroup Name="Level2">
    <File Name="Assets\Level2\**"/>
</ContentGroup>
```

You can also use wildcards with text for file names. For example, if you want to include every file in your "Assets" folder with a file name that contains "Level2" you can use something like this:

```xml
<ContentGroup Name="Level2">
    <File Name="Assets\*Level2*"/>
</ContentGroup>
```

## Convert SourceAppxContentGroupMap.xml to AppxContentGroupMap.xml

To convert the `SourceAppxContentGroupMap.xml` to the final version, `AppxContentGroupMap.xml`, you can use Visual Studio 2017 or the **MakeAppx.exe** command line tool.

To use Visual Studio to convert your content group map:
1. Add the `SourceAppxContentGroupMap.xml` to your project folder
2. Change the Build Action of the `SourceAppxContentGroupMap.xml`to "AppxSourceContentGroupMap" in the Properties window
2. Right click the project in the solution explorer
3. Navigate to Store -> Convert Content Group Map File

If you didn't develop your app in Visual Studio, or if you just prefer using the command line, use the **MakeAppx.exe** tool to convert your `SourceAppxContentGroupMap.xml`. 

A simple **MakeAppx.exe** command might look something like this:
```syntax
MakeAppx convertCGM /s MyApp\SourceAppxContentGroupMap.xml /f MyApp\AppxContentGroupMap.xml /d MyApp\
```

The /s option specifies the path to the `SourceAppxContentGroupMap.xml`, and /f specifies the path to the `AppxContentGroupMap.xml`. The final option, /d, specifies which directory should be used for expanding file name wildcards, in this case, its the app project directory.

For more information about options you can use with **MakeAppx.exe**, open a command prompt, navigate to **MakeAppx.exe** and enter:

```syntax
MakeAppx convertCGM /?
```

That's all you'll need to get your final `AppxContentGroupMap.xml` ready for your app! There's still more to do before your app is fully ready for the Microsoft Store. For more information on the complete process of adding UWP App Streaming Install to your app, check out [this blog post](https://blogs.msdn.microsoft.com/appinstaller/2017/03/15/uwp-streaming-app-installation/).
