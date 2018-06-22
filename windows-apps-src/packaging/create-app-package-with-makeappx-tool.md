---
author: laurenhughes
title: Create an app package with the MakeAppx.exe tool
description: MakeAppx.exe creates, encrypts, decrypts, and extracts files from app packages and bundles.
ms.author: lahugh
ms.date: 06/21/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, packaging
ms.assetid: 7c1c3355-8bf7-4c9f-b13b-2b9874b7c63c
ms.localizationpriority: medium
---

# Create an app package with the MakeAppx.exe tool


**MakeAppx.exe** creates both app packages and app package bundles. **MakeAppx.exe** also extracts files from an app package or bundle and encrypts or decrypts app packages and bundles. This tool is included in the Windows 10 SDK and can be used from a command prompt or a script file.

> [!IMPORTANT] 
> If you used Visual Studio to develop your app, it's recommended that you use the Visual Studio wizard to create your app package. For more information, see [Package a UWP app with Visual Studio](https://msdn.microsoft.com/windows/uwp/packaging/packaging-uwp-apps).

Note that **MakeAppx.exe** does not create an .appxupload file. The .appxupload file is created as part of the Visual Studio packaging process and contains two other files: .msix or .appx and .appxsym. The .appxsym file is a compressed .pdb file containing public symbols of your app used for [crash analytics](https://blogs.windows.com/buildingapps/2015/07/13/crash-analysis-in-the-unified-dev-center/) in the Windows Dev Center. A regular .appx file can be submitted as well, but there will be no crash analytic or debugging information available. For more information on submitting packages to the store, see [Upload app packages](https://msdn.microsoft.com/windows/uwp/publish/upload-app-packages). 

> [!NOTE]
> This page has been updated to include support for the creation of .msix packages, pre-released in the [Windows 10 Insider Preview Build 17682](https://blogs.windows.com/windowsexperience/2018/05/31/announcing-windows-10-insider-preview-build-17682/#UPs6rko5Z56SJsQ6.97). If you're using .appx packages, you can still use this tool as described on this page, replacing instances of `.appx` with `.msix`.

To manually create an .appxupload file:
- Place the .msix and the .appxsym in a folder
- Zip the folder
- Change the zipped folder extension name from .zip to .appxupload

## Using MakeAppx.exe

Based on your installation path of the SDK, this is where **MakeAppx.exe** is on your Windows 10 PC:
- x86: C:\Program Files (x86)\Windows Kits\10\bin\x86\makeappx.exe
- x64: C:\Program Files (x86)\Windows Kits\10\bin\x64\makeappx.exe

There is no ARM version of this tool.

### MakeAppx.exe syntax and options

General **MakeAppx.exe** syntax:

``` Usage
MakeAppx <command> [options]      
```

The following table describes the commands for **MakeAppx.exe**.

| **Command**   | **Description**                       |
|---------------|---------------------------------------|
| pack          | Creates a package.                    |
| unpack        | Extracts all files in the specified package to the specified output directory. |
| bundle        | Creates a bundle.                     |
| unbundle      | Unpacks all packages to a subdirectory under the specified output path named after the bundle full name. |
| encrypt       | Creates an encrypted app package or bundle from the input package/bundle at the specified output package/bundle. |
| decrypt       | Creates an decrypted app package or bundle from the input app package/bundle at the specified output package/bundle. |


This list of options applies to all commands:

| **Option**    | **Description**                       |
|---------------|---------------------------------------|
| /d            | Specifies the input, output, or content directory. |
| /l            | Used for localized packages. The default validation trips on localized packages. This options disables only that specific validation, without requiring that all validation be disabled. |
| /kf           | Encrypts or decrypts the package or bundle using the key from the specified key file. This can't be used with /kt. |
| /kt           | Encrypts the or decrypts package or bundle using the global test key. This can't be used with /kf. |
| /no           | Prevents an overwrite of the output file if it exists. If you don't specify this option or the /o option, the user is asked whether they want to overwrite the file. |
| /nv           | Skips semantic validation. If you don't specify this option, the tool performs a full validation of the package. |
| /o            | Overwrites the output file if it exists. If you don't specify this option or the /no option, the user is asked whether they want to overwrite the file. |
| /p            | Specifies the app package or bundle.  |
| /v            | Enables verbose logging output to the console. |
| /?            | Displays help text.                   |


The following list contains possible arguments:

| **Argument**                          | **Description**                       |
|---------------------------------------|---------------------------------------|
| &lt;output package name&gt;           | The name of the package created. This is the file name appended with .msix or .appx. |
| &lt;encrypted output package name&gt; | The name of the encrypted package created. This is the file name appended with .emsix or .eappx. |
| &lt;input package name&gt;            | The name of the package. This is the file name appended with .msix or .appx. |
| &lt;encrypted input package name&gt;  | The name of the encrypted package. This is the file name appended with .emsix or .eappx. |
| &lt;output bundle name&gt;            | The name of the bundle created. This is the file name appended with .msixbundle or .appxbundle. |
| &lt;encrypted output bundle name&gt;  | The name of the encrypted bundle created. This is the file name appended with .emsixbundle or .eappxbundle. |
| &lt;input bundle name&gt;             | The name of the bundle. This is the file name appended with .msixbundle or .appxbundle. |
| &lt;encrypted input bundle name&gt;   | The name of the encrypted bundle. This is the file name appended with .emsixbundle or .eappxbundle. |
| &lt;content directory&gt;             | Path for the app package or bundle content. |
| &lt;mapping file&gt;                  | File name that specifies the package source and destination. |
| &lt;output directory&gt;              | Path to the directory for output packages and bundles. |
| &lt;key file&gt;                      | Name of the file containing a key for encryption or decryption. |
| &lt;algorithm ID&gt;                  | Algorithms used when creating a block map. Valid algorithms include: SHA256 (default), SHA384, SHA512. |


### Create an app package

An app package is a complete set of the app's files packaged in to a .msix or .appx package file. To create an app package using the **pack** command, you must provide either a content directory or a mapping file for the location of the package. You can also encrypt a package while creating it. If you want to encrypt the package, you must use /ep and specify if you are using a key file (/kf) or the global test key (/kt). For more information on creating an encrypted package, see [Encrypt or decrypt a package or bundle](#encrypt-or-decrypt-a-package-or-bundle).

Options specific to the **pack** command:

| **Option**    | **Description**                       |
|---------------|---------------------------------------|
| /f            | Specifies the mapping file.           |
| /h            | Specifies the hash algorithm to use when creating the block map. This can only be used with the pack command. Valid algorithms include: SHA256 (default), SHA384, SHA512. |
| /m            | Specifies the path to an input app manifest which will be used as the basis for generating the output app package or resource package's manifest.  When you use this option, you must also use /f and include a [ResourceMetadata] section in the mapping file to specify the resource dimensions to be included in the generated manifest.|
| /nc           | Prevents compression of the package files. By default, files are compressed based on detected file type. |
| /r            | Builds a resource package. This must be used with /m and implies the use of the /l option. |  


The following usage examples show some possible syntax options for the **pack** command:

``` syntax 
MakeAppx pack [options] /d <content directory> /p <output package name>
MakeAppx pack [options] /f <mapping file> /p <output package name>
MakeAppx pack [options] /m <app package manifest> /f <mapping file> /p <output package name>
MakeAppx pack [options] /r /m <app package manifest> /f <mapping file> /p <output package name>
MakeAppx pack [options] /d <content directory> /ep <encrypted output package name> /kf <key file>
MakeAppx pack [options] /d <content directory> /ep <encrypted output package name> /kt

```
The following shows command line examples for the **pack** command:

``` examples
MakeAppx pack /v /h SHA256 /d "C:\My Files" /p MyPackage.msix
MakeAppx pack /v /o /f MyMapping.txt /p MyPackage.msix
MakeAppx pack /m "MyApp\AppxManifest.xml" /f MyMapping.txt /p AppPackage.msix
MakeAppx pack /r /m "MyApp\AppxManifest.xml" /f MyMapping.txt /p ResourcePackage.msix
MakeAppx pack /v /h SHA256 /d "C:\My Files" /ep MyPackage.emsix /kf MyKeyFile.txt
MakeAppx pack /v /h SHA256 /d "C:\My Files" /ep MyPackage.emsix /kt
```

### Create an app bundle

An app bundle is similar to an app package, but a bundle can reduce the size of the app that users download. App bundles are helpful for language-specific assets, varying image-scale assets, or resources that apply to specific versions of Microsoft DirectX, for example. Similar to creating an encrypted app package, you can also encrypt the app bundle while bundling it. To encrypt the app bundle, use the /ep option and specify if you are using a key file (/kf) or the global test key (/kt). For more information on creating an encrypted bundle, see [Encrypt or decrypt a package or bundle](#encrypt-or-decrypt-a-package-or-bundle).

Options specific to the **bundle** command:

| **Option**    | **Description**                       |
|---------------|---------------------------------------|
| /bv           | Specifies the version number of the bundle. The version number must be in four parts separated by periods in the form: &lt;Major&gt;.&lt;Minor&gt;.&lt;Build&gt;.&lt;Revision&gt;. |
| /f            | Specifies the mapping file.           |

Note that if the bundle version is not specified or if it is set to "0.0.0.0" the bundle is created using the current date-time.

The following usage examples show some possible syntax options for the **bundle** command:

``` syntax
MakeAppx bundle [options] /d <content directory> /p <output bundle name>
MakeAppx bundle [options] /f <mapping file> /p <output bundle name>
MakeAppx bundle [options] /d <content directory> /ep <encrypted output bundle name> /kf MyKeyFile.txt
MakeAppx bundle [options] /f <mapping file> /ep <encrypted output bundle name> /kt
```
The following block contains examples for the **bundle** command:

``` examples
MakeAppx bundle /v /d "C:\My Files" /p MyBundle.msixbundle
MakeAppx bundle /v /o /bv 1.0.1.2096 /f MyMapping.txt /p MyBundle.msixbundle
MakeAppx bundle /v /o /bv 1.0.1.2096 /f MyMapping.txt /ep MyBundle.emsixbundle /kf MyKeyFile.txt
MakeAppx bundle /v /o /bv 1.0.1.2096 /f MyMapping.txt /ep MyBundle.emsixbundle /kt
```

### Extract files from a package or bundle

In addition to packaging and bundling apps, **MakeAppx.exe** can also unpack or unbundle existing packages. You must provide the content directory as a destination for the extracted files. If you are trying to extract files from an encrypted package or bundle, you can decrypt and extract the files at the same time using the /ep option and specifying whether it should be decrypted using a key file (/kf) or the global test key (/kt). For more information on decrypting a package or bundle, see [Encrypt or decrypt a package or bundle](#encrypt-or-decrypt-a-package-or-bundle).

Options specific to **unpack** and **unbundle** commands:

| **Option**    | **Description**                       |
|---------------|---------------------------------------|
| /nd           | Does not perform decryption when unpacking or unbundling the package/bundle. |
| /pfn          | Unpacks/unbundles all files to a subdirectory under the specified output path, named after the package or bundle full name |

The following usage examples show some possible syntax options for the **unpack** and **unbundle** commands:

``` syntax
MakeAppx unpack [options] /p <input package name> /d <output directory>
MakeAppx unpack [options] /ep <encrypted input package name> /d <output directory> /kf <key file>
MakeAppx unpack [options] /ep <encrypted input package name> /d <output directory> /kt

MakeAppx unbundle [options] /p <input bundle name> /d <output directory>
MakeAppx unbundle [options] /ep <encrypted input bundle name> /d <output directory> /kf <key file>
MakeAppx unbundle [options] /ep <encrypted input bundle name> /d <output directory> /kt
```

The following block contains examples for using the **unpack** and **unbundle** commands:

``` examples
MakeAppx unpack /v /p MyPackage.msix /d "C:\My Files"
MakeAppx unpack /v /ep MyPackage.emsix /d "C:\My Files" /kf MyKeyFile.txt
MakeAppx unpack /v /ep MyPackage.emsix /d "C:\My Files" /kt

MakeAppx unbundle /v /p MyBundle.msixbundle /d "C:\My Files"
MakeAppx unbundle /v /ep MyBundle.emsixbundle /d "C:\My Files" /kf MyKeyFile.txt
MakeAppx unbundle /v /ep MyBundle.emsixbundle /d "C:\My Files" /kt
```

### Encrypt or decrypt a package or bundle

The **MakeAppx.exe** tool can also encrypt or decrypt an existing package or bundle. You must simply provide the package name, the output package name, and whether encryption or decryption should use a key file (/kf) or the global test key (/kt).

Encryption and decryption are not available through the Visual Studio packaging wizard. 

Options specific to **encrypt** and **decrypt** commands:

| **Option**    | **Description**                       |
|---------------|---------------------------------------|
| /ep           | Specifies an encrypted app package or bundle. |

The following usage examples show some possible syntax options for the **encrypt** and **decrypt** commands:

``` syntax
MakeAppx encrypt [options] /p <package name> /ep <output package name> /kf <key file>
MakeAppx encrypt [options] /p <package name> /ep <output package name> /kt

MakeAppx decrypt [options] /ep <package name> /p <output package name> /kf <key file>
MakeAppx decrypt [options] /ep <package name> /p <output package name> /kt
```

The following block contains examples for using the **encrypt** and **decrypt** commands:

``` examples
MakeAppx.exe encrypt /p MyPackage.msix /ep MyEncryptedPackage.emsix /kt
MakeAppx.exe encrypt /p MyPackage.msix /ep MyEncryptedPackage.emsix /kf MyKeyFile.txt

MakeAppx.exe decrypt /p MyPackage.msix /ep MyEncryptedPackage.emsix /kt
MakeAppx.exe decrypt p MyPackage.msix /ep MyEncryptedPackage.emsix /kf MyKeyFile.txt
```

## Key files

Key files must begin with a line containing the string "[Keys]" followed by lines describing the keys to encrypt each package with. Each key is represented by a pair of strings in quotation marks, separated by either spaces or tabs. The first string represents the base64 encoded 32-byte key ID and the second represents the base64 encoded 32-byte encryption key. A key file should be a simple text file.

Example of a key file:

``` Example
[Keys]
"OWVwSzliRGY1VWt1ODk4N1Q4R2Vqc04zMzIzNnlUREU="    "MjNFTlFhZGRGZEY2YnVxMTBocjd6THdOdk9pZkpvelc="
```

## Mapping files
Mapping files must begin with a line containing the string "[Files]" followed by lines describing the files to add to the package. Each file is described by a pair of paths in quotation marks, separated by either spaces or tabs. Each file represents its source (on disk) and destination (in the package). A mapping file should be a simple text file.

Example of a mapping file (without the /m option):

``` Example
[Files]
"C:\MyApp\StartPage.html"               "default.html"
"C:\Program Files (x86)\example.txt"    "misc\example.txt"
"\\MyServer\path\icon.png"              "icon.png"
"my app files\readme.txt"               "my app files\readme.txt"
"CustomManifest.xml"                    "AppxManifest.xml"
``` 

When using a mapping file, you can choose whether you would like to use the /m option. The /m option allows the user to specify the resource metadata in the mapping file to be included in the generated manifest. If you use the /m option, the mapping file must contain a section that begins with the line "[ResourceMetadata]", followed by lines that specify "ResourceDimensions" and "ResourceId." It is possible for an app package to contain multiple "ResourceDimensions", but there can only ever be one "ResourceId."

Example of a mapping file (with the /m option):

``` Example
[ResourceMetadata]
"ResourceDimensions"                    "language-en-us"
"ResourceId"                            "English"

[Files]
"images\en-us\logo.png"                 "en-us\logo.png"
"en-us.pri"                             "resources.pri"
```

## Semantic validation performed by MakeAppx.exe

**MakeAppx.exe** performs limited sematic validation that is designed to catch the most common deployment errors and help ensure that the app package is valid. See the /nv option if you want to skip validation while using **MakeAppx.exe**. 

This validation ensures that:
- All files referenced in the package manifest are included in the app package.
- An application does not have two identical keys.
- An application does not register for a forbidden protocol from this list: SMB, FILE, MS-WWA-WEB, MS-WWA. 

This is not a complete semantic validation as it is only designed to catch common errors. Packages built by **MakeAppx.exe** are not guaranteed to be installable.