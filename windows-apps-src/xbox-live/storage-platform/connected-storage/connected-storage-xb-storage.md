---
title: Managing local Connected Storage
author: aablackm
description: Learn how to manage local Connected Storage data in a development environment.
ms.assetid: 630cb5fc-5d48-4026-8d6c-3aa617d75b2e
ms.author: aablackm
ms.date: 02/27/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, connected storage
ms.localizationpriority: low
---

# Managing local Connected Storage
While Connected Storage is used to store your game data in the cloud, there is also a local storage component to the Connected Storage service. Whether you are on a PC or console there is a local cache of the Connected Storage data which contains the data synced to the cloud. Whether you are creating an XDK or UWP title there is a tool to allow you to manage your local Connected Storage data.

Refer to the following table to find the appropriate tool to manage your locally cached Connected Storage data:

|Title Classification  |Device  |Local Storage Tool  |
|---------|---------|---------|
|XDK     |Xbox One Console     |*xbstorage*         |
|UWP     |PC         |*gamesaveutil*         |
|UWP     |Xbox One Console     |Xbox Device Portal(XDP |)

- *Xbstorage* is a command line tool, run from the XDK command prompt, for managing locally cached Connected Storage on the Xbox One Console. The *xbstorage* tool can be found in the Xbox One XDK under the file path: **/Program Files (x86)/Microsoft Durango XDK/bin/xbstorage.exe**
- *Gamesaveutil* is a command line tool for managing UWP locally cached Connected Storage on PC. The *gamesaveutil* tool comes packaged with the [Windows 10 SDK](https://developer.microsoft.com/en-US/windows/downloads/windows-10-sdk) for Fall Creators Update and later( build 10.0.16299.15 and later). Once you've installed the appropriate version of the Windows 10 SDK, *gamesaveutil* can be found under the folder: **ProgramFiles(x86)/Windows Kits/10/bin/[SDK Version]/x64/gamesaveutil.exe**.
- *Xbox Device Portal(XDP)* is an online portal that allows you to manage the locally cached Connected Storage UWP data on your Xbox One Console. This article does not cover XDP usage. To learn to use XDP read [Device Portal for Xbox](https://docs.microsoft.com/en-us/windows/uwp/debug-test-perf/device-portal-xbox).

## Xbstorage

*Xbstorage* allows clearing locally cached data Connected Storage data from the hard drive, as well as importing and exporting of data for users or machines from Connected Storage spaces by using XML files.

When an operation is performed on local data using the *xbstorage* tool, the system will behave as if that operation had been performed by the app itself, so the act of reading the data from a Connected Storage space to a local file will cause synchronization with the cloud prior to copying.

Similarly, a copy of data from an XML file on the development PC to a Connected Storage container on the Xbox One dev kit will cause the console to start uploading that data to the cloud. However, there are conditions in which this will not occur: if the dev kit cannot acquire the lock, or if there is a data conflict between the containers on the console and those in the cloud, the console will behave as if the user had decided not to resolve the conflict by picking one version of the container to keep, and the console will behave as if the user is playing offline until the next time the title is started.

The one exception to these commands is **reset** **/force** which clears the local storage of saved data for all SCIDs and users, but does not alter the data stored in the cloud. This is useful for putting a console into the state it would be in if a user was roaming to a console and downloading data from the cloud upon playing a title.

### Xbstorage commands

Xbstorage has the following six commands developers can use with the XDK command prompt to manage local data on their Xbox One Development Kit:

<a id="xbstorage_reset"></a>

|Command  |Description  |
|---------|---------|
|reset    |Performs a factory reset on Connected Storage.         |
|import   |Imports data from the specified XML file to a Connected Storage space.         |
|export   |Exports data from a Connected Storage space to the specified XML file.         |
|delete   |Deletes data from a Connected Storage space.         |
|generate |Generates dummy data and saves to the specified XML file.         |
|simulate |Simulates out of storage space conditions.         |

### Xbstorage reset

`xbstorage reset [/force]`

Erases all local data in Connected Storage from the local console, restoring it to factory settings. Data that has been persisted to the cloud is not modified and will be downloaded again as necessary.

|Option  |Description  |
|---------|---------|
|/force   |Confirms that Connected Storage should be reset. Running the reset command without specifying **/force** causes the following message to be displayed:   As Connected Storage factory reset is a potentially destructive operation this command does not perform the reset unless the **/force** flag is present. |

<a id="xbstorage_import"></a>

### Xbstorage import

`xbstorage import *file-name* [/scid:*SCID*] [/machine] [/msa:*account*] [/replace] [/verbose]`

Imports data specified in *filename* to a Connected Storage space.
   The file is an XML file that contains the data. For an example, see [xbstorage generate](#xbstorage_generate). For more information about the file's XML format, see [Import and export file format](#xbstorage_fileformat), later in this topic.
   There are two ways to specify the Connected Storage space:

- If the input file has a **ContextDescription** section that is correctly populated, then this will be used to specify the Connected Storage space.
- The storage space can also be partially or fully specified via command-line parameters, which take precedence over the respective elements of the storage space specified in the input file.

Examples of usage:

```cmd
  xbstorage import mydata.xml
  xbstorage import mydata.xml /replace
  xbstorage import mydata.xml /machine /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
  xbstorage import mydata.xml /msa:user@domain.com /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
  xbstorage import mydata.xml /verbose 
```

> [!NOTE]
> Before importing to the specified Connected Storage space, the system will attempt to synchronize with the cloud using the same logic that runs when a Connected Storage space is acquired by a running app.
>
> If an application with the same Primary SCID is running, this operation could cause a race condition, and the contents of the Connected Storage space could be in an indeterminate state.
>
> If **/replace** is not specified, the containers specified in the input file will be erased before writing the blobs specified in the input file. Containers in the Connected Storage space not specified in the input file will remain untouched.

|Option  |Description  |
|---------|---------|
|*file-name*     |Specifies an XML file that contains the data to import.         |
|/scid:*SCID*    |Specifies the Service Configuration Identifier (SCID).         |
|/machine        |Specifies a per-machine Connected Storage space.  This option cannot be used simultaneously with the **/msa** option.         |
|/msa:*account*  |Specifies an account to use for per-user Connected Storage. The user must be signed in to the console for the space to be used.  This option cannot be used simultaneously with the **/machine** option.         |
|/replace        |Deletes all containers in the specified Connected Storage space before importing.         |
|/verbose        |Displays the status of the importation.         |


 <a id="xbstorage_export"></a>

### Xbstorage export

`xbstorage export *outfile* [/context:*input-file*] [/scid:*SCID*] [/machine] [/msa:*account*] [/verbose]`

Exports data from a Connected Storage space to the file specified by **outfile**.    The file is an XML file that contains the data. See [xbstorage generate](#xbstorage_generate) to see how to generate an example. For more information about the file's XML format, see [Import and export file format](#xbstorage_fileformat), later in this topic. There are two ways to specify the Connected Storage space:

- If the **/context** parameter is used, and the file name specified by \<infile> has a **ContextDescription** section that is correctly populated, then that file will be used to specify the Connected Storage space.
- The storage space can also be partially or fully specified via command-line parameters, which take precedence over the respective elements of the storage space specified in the **/context** file.

Examples of usage:

```cmd
  xbstorage export exporteddata.xml /context:space.xml
  xbstorage export exporteddata.xml /machine /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
  xbstorage export exporteddata.xml /msa:user@domain.com /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
  xbstorage export exporteddata.xml /context:space.xml /verbose
```

> [!NOTE]
> Before exporting the specified Connected Storage space, the system will attempt to synchronize with the cloud using the same logic that runs when a Connected Storage space is acquired by a running app.
>
> If an application with the same Primary SCID is running, this operation could cause a race condition, and the contents of the Connected Storage space could be in an indeterminate state.

|Option  |Description  |
|---------|---------|
|*outfile*             |XML file the data will be exported to.         |
|/context:*input-file* |Specifies an input file from which to read the space information.         |
|/scid:*SCID*          |Specifies the service configuration identifier (SCID).         |
|/machine              |Specifies a per-machine Connected Storage space.  This option cannot be used simultaneously with the **/msa** option.         |
|/msa:*account*        |Specifies an account to use for per-user Connected Storage. The user must be signed in to the console for the space to be used.  This option cannot be used simultaneously with the **/machine** option.         |
|/verbose              |Displays the status of the export operation.         |

<a id="xbstorage_delete"></a>

### Xbstorage delete

`xbstorage delete [/context:*input-file*] [/scid:*SCID*] [/machine] [/msa:*account*] [/verbose]`

Deletes all data from a Connected Storage space.
There are two ways to specify the Connected Storage space:

- If the **/context** parameter is used, and the file name specified by \<infile> has a **ContextDescription** section that is correctly populated, then that file will be used to specify the Connected Storage space.
- The storage space can also be partially or fully specified via command-line parameters, which take precedence over the respective elements of the storage space specified in the **/context** file.

Examples of usage:

```cmd
  xbstorage delete /context:space.xml
  xbstorage delete /machine /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
  xbstorage delete /msa:user@domain.com /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
  xbstorage delete /context:space.xml /verbose
```

> [!NOTE]
> Before deleting the specified Connected Storage space, the system will attempt to synchronize with the cloud using the same logic that runs when a Connected Storage space is acquired by a running app.
>> If an application with the same Primary SCID is running, this operation could cause a race condition, and the contents of the Connected Storage space could be in an indeterminate state.

|Option  |Description |
|---------|---------|
|/context:*input-file*     |Specifies an input file from which to read the space information.         |
|/scid:*SCID*              |Specifies the service configuration identifier (SCID).         |
|/machine                  |Specifies a per-machine Connected Storage space.  This option cannot be used simultaneously with the **/msa** option.         |
|/msa:*account*            |Specifies an account to use for per-user Connected Storage. The user must be signed in to the console for the space to be used.  This option cannot be used simultaneously with the **/machine** option.         |
|/verbose                  |Displays the status of the delete operation.         |

 <a id="xbstorage_generate"></a>

### Xbstorage generate

`xbstorage generate *file-name* [/containers:*n*] [/blobs:*n*] [/blobsize:*n*]`

Generates dummy data and saves to a file specified by \<filename>. For more information about the file's XML format, see [Import and export file format](#xbstorage_fileformat), later in this topic.    The service configuration identifier (SCID) will be set to 00000000-0000-0000-0000-000000000000, and the account will be set for a per-machine Connected Storage space. If you want to change those values, you can edit the file directly after it is generated.

Examples of usage:

```cmd
  xbstorage generate dummydata.xml
  xbstorage generate dummydata.xml /containers:4
  xbstorage generate dummydata.xml /blobs:10
  xbstorage generate dummydata.xml /containers:4 /blobs:10
  xbstorage generate dummydata.xml /containers:4 /blobs:10 /blobsize:512
```

> [!NOTE]
> The byte data is a simple ascending sequence; for example, a five-byte blob would have the following bytes: 00 01 02 03 04. >>  If you want to specify a per-user Connected Storage space, change the **Account** node in the XML file to something like the following:
>  ```
>    <Account msa="user@domain.com"/>
>  ```

|Option  |Description  |
|---------|---------|
|*file-name*     | XML file the data will be written to. |
|/containers:*n* | Specifies the number, *n*, of containers to generate. The default count is 2.  |
|/blobs:*n*      | Specifies the number, *n*, of blobs to generate. The default count is 3.  |
|/blobsize:*n*   | Specifies the number, *n*, of bytes per blob. The default size is 1024 bytes.  |

 <a id="xbstorage_simulate"></a>

### Xbstorage simulate

`xbstorage simulate [/reserveremainingspace] [/forceoutoflocalstorage] [/stop] [/verbose]`

Simulates out of local storage conditions for the Connected Storage service.

|Option  |Description  |
|---------|---------|
|/reserveremainingspace | Reserves all remaining space in Connected Storage. Deleting something from ConnectedStorage will open up space that you can use. |

|/forceoutoflocalstorage | Simulates the Connected Storage Service having no available space left. Deleting something from Connected Storage will not change the Connected storage service from reporting out of memory. |

|/stop | Stops all simulations. |

|/verbose | Displays the status of the simulated operation. |

 <a id="ID4E4MAC"></a>

### Common Options

`xbstorage [/?] [/X*:address* [*+accesskey*] ]`

|Option  |Description  |
|---------|---------|
| /?                           |  Displays help for xbstorage.exe |
| /X*:address* [*+accesskey*]  | Specifies the host name or address (shown as **Tools IP** on the console) of a targeted console, but does not change the default console. If you do not use this option, the default console is used.*Accesskey* is a string that you can use to restrict access to a console to only those people who know the access key. Set the access key by using the command **xbconfig** **accesskey=***your-key*; then, restart your console to make the access key effective. To access a console that is configured with an access key, you must include a plus sign (+) and the access key after the IP address or host name of the console.
> [!NOTE]
> If an access key is provided when the default console is set by xbconnect, then the access key is stored as part of the address of the default console.
|

## Gamesaveutil

*Gamesaveutil* allows you to manage locally cached Connected Storage for your app with all of the same functions that *xbstorage* provides. Just like the xbstorage tool, *gamesaveutil* offers the same six data managing functions, with some differences in behavior. The import, export, delete, and reset commands require an Xbox Live user to be signed in. You can use the Xbox App in Windows 10 to view and change the current user.

### Commands

|Command  |Description  |
|---------|---------|
|import   |Imports data from the specified XML file         |
|export   |Exports data to the specified xml file         |
|delete   |Deletes data from the specified app        |
|reset    |deletes local data only for the specified app         |
|generate |generates dummy data and saves to the specified xml file         |
|simulate |simulates special conditions that are difficult to test         |

### Gamesaveutil import

`gamesaveutil import <filename> [/pfn:<PFN>] [/scid:<SCID>] [/replace]`

Imports data specified in \<filename>

The file is an XML file that contains the data. Type `gamesaveutil help generate`
to see how to generate an example.

There are two ways to specify the app PFN and SCID where the data is saved:

If the input file has a ContextDescription section that is correctly
populated, then this will be used to specify the target app PFN and SCID.

The PFN and SCID can be partially or fully specified via command-line
parameters, which take precedence over the respective elements of the specified
PFN and SCID from the input file.

|Option  |Description  |
|---------|---------|
|/pfn:\<PFN>       |Specifies the Package Family Name(PFN) for the app to perform the import for. The app must be installed.         |
|/scid:\<SCID>     |Specifies the Service Configuration Identifier (SCID). This is from your Xbox Live configuration.         |
|/replace         |Delete all containers before performing the import.         |

Example Usages:

```cmd
gamesaveutil import mydata.xml
gamesaveutil import mydata.xml /replace
gamesaveutil import mydata.xml /pfn:MyGame_xxxxxx /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
```


> [!NOTE]
> The app must be installed and have synced data already in order to import.
> 
> If /replace is not specified, existing containers will not be touched unless
> they are specified in the input file.

### Gamesaveutil export

`gamesaveutil export <outfile> [/pfn:<PFN>] [/scid:<SCID>] [/context:<infile>]`

Exports data to the file specified by \<outfile>.

The file is an XML file that contains the data. Type gamesaveutil help generate
to see how to generate an example.

There are two ways to specify the location of the data to export:

If the /context parameter is used, and the filename specified by \<infile>
has a ContextDescription section that is correctly populated, then that file
will be used to specify the location of the source data.

The location can also be specified via command-line parameters, which take
precedence over the respective elements specified by the /context file.

|Option  |Description  |
|---------|---------|
|/context:\<infile>     |Use the specified file to specify the app PFN and SCID.         |
|/pfn:\<PFN>            |Specifies the Package Family Name(PFN) for the app to perform the export from. The app must be installed.       |
|/scid:\<SCID>          |Specifies the Service Configuration Identifier (SCID). This is from your Xbox Live configuration.        |

Example Usages:

```cmd
gamesaveutil export exporteddata.xml /context:target.xml
gamesaveutil export exporteddata.xml /pfn:MyGame_xxxxxx /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
```


> [!NOTE]
> The app must be installed and have synced data already in order to export.

### Gamesaveutil delete

`gamesaveutil delete [/pfn:<PFN>] [/scid:<SCID>] [/context:<infile>]`

Deletes all data for the specified PFN and SCID.

There are two ways to specify the location of the data to delete:

If the /context parameter is used, and the filename specified by \<infile>
has a ContextDescription section that is correctly populated, then that file
will be used to specify the location of the source data.

The location can also be specified via command-line parameters, which take
precedence over the respective elements specified by the /context file.

|Option  |Description  |
|---------|---------|
|/context:\<infile>     |Use the specified file to specify the app PFN and SCID.         |
|/pfn:\<PFN>            |Specifies the Package Family Name(PFN) for the app to delete the data from. The app must be installed.       |
|/scid:\<SCID>          |Specifies the Service Configuration Identifier (SCID). This is from your Xbox Live configuration.        |

Example Usages:

```cmd
gamesaveutil delete /context:target.xml
gamesaveutil delete /pfn:MyGame_xxxxxx /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
```


> [!NOTE]
> The app must be installed in order to delete containers.

### Gamesaveutil reset

`gamesaveutil reset [/pfn:<PFN>] [/scid:<SCID>] [/context:<infile>]`

Deletes all local data for the specified PFN and SCID.

There are two ways to specify the location of the data to delete:

If the /context parameter is used, and the filename specified by \<infile>
has a ContextDescription section that is correctly populated, then that file
will be used to specify the location of the source data.

The location can also be specified via command-line parameters, which take
precedence over the respective elements specified by the /context file.

|Option  |Description  |
|---------|---------|
|/context:\<infile>     |Use the specified file to specify the app PFN and SCID.         |
|/pfn:\<PFN>            |Specifies the Package Family Name(PFN) for the app to delete the data from. The app must be installed.       |
|/scid:\<SCID>          |Specifies the Service Configuration Identifier (SCID). This is from your Xbox Live configuration.        |

Example Usages:

```cmd
gamesaveutil reset /context:target.xml
gamesaveutil reset /pfn:MyGame_xxxxxx /scid:2AAEB34B-DAB2-4879-B625-D970069C1D22
```


> [!NOTE]
> The app must be installed in order to delete local data.

### Gamesaveutil generate

`gamesaveutil generate <filename> [/containers:<n>] [/blobs:<n>] [/blobsize:<n>]`

Generates dummy data and saves to a file specified by \<filename>.
The Service Configuration Identifier (SCID) will be set to
00000000-0000-0000-0000-000000000000. Edit the file manually after
generation to change those values if you desire.

|Option  |Description  |
|---------|---------|
|/containers:\<n>     |Specifies how many containers to generate. Default is 2.         |
|/blobs:\<n>          |Specifies how many blobs per container to generate. Default is 3.        |
|/blobsize:\<n>       |Specifies how many bytes per blob. Default is 1024.         |

Example Usages:

```cmd
gamesaveutil generate dummydata.xml
gamesaveutil generate dummydata.xml /containers:4
gamesaveutil generate dummydata.xml /blobs:10
gamesaveutil generate dummydata.xml /containers:4 /blobs:10
gamesaveutil generate dummydata.xml /containers:4 /blobs:10 /blobsize:512
```


> [!NOTE]
> The byte data is a simple ascending sequence, i.e. a five byte blob would
> have the bytes 00 01 02 03 04.

### Gamesaveutil simulate

`gamesaveutil simulate [/markcontainerschanged] [/stop]`

Simulates special conditions for testing certain scenarios.

|Option  |Description  |
|---------|---------|
|/markcontainerschanged     |Forces all containers to look like they have been changed when an app resumes from suspend and gets a new provider. Affects all apps until cleared with /stop.      |
|/stop                      |Stops all simulations.         |


 <a id="xbstorage_fileformat"></a>

## Import and export file format

The XML file used with the **import**, **export**, and **generate** commands with the *xbstorage* tool has the format shown in the following example.

```xml
<?xml version="1.0" encoding="UTF-8"?>
  <XbConnectedStorageSpace>
    <ContextDescription>
      <Account msa="user@domain.com" />
      <Title scid="00000000-0000-0000-0000-000000000000" />
    </ContextDescription>
    <Data>
      <Containers>
        <Container name="Container1" displayName="Optional Display Name">
          <Blobs>
            <Blob name="Blob1">
              <![CDATA[... ] ]>
            </Blob>
            ...
            <Blob name="BlobN">
              <![CDATA[... ] ]>
            </Blob>
          </Blobs>
        </Container>
        ...
        <Container name="ContainerN">
        ...
        </Container>
      </Containers>
    </Data>
  </XbConnectedStorageSpace>
```

The only change needed to format this xml for **import**, **export**, and **generate** in *gamesaveutil* is to replace the \<Account> member node of the \<ContextDescription> node with a \<PackageFamilyName> node.
This will change the \<ContextDescription> node from this:

```xml
<ContextDescription>
    <Account msa="user@domain.com" />
    <Title scid="00000000-0000-0000-0000-000000000000" />
</ContextDescription>
```

to this:

```xml
<ContextDescription>
    <PackageFamilyName pfn="MyGame_xxxxxx" />
    <Title scid="00000000-0000-0000-0000-000000000000" />
</ContextDescription>
```

> [!NOTE]
> The format of data in these XML files is not identical to what's on the platform, it is for import and export purposes only. The data format for these XML files could potentially change in the future, so they should be treated as an intermediate or utility format, not an archival format.

The **ContextDescription** node is optional. If you are making a Connected Storage space for a machine, you can use `<Account machine="true"/>` instead of `<Account msa="user@domain.com"/>`. Otherwise, the context can be specified on the command line for importation.
Blobs and containers should have the corresponding names given to them by the game or application for which the file is being generated.
The contents of each blob should be a string wrapped in a **CDATA** tag, which is generated by calling **CryptBinaryToStringW** with the flag **CRYPT_STRING_BASE64** providing the data for that blob as a raw byte array. Conversely, blob data can be converted back by calling **CryptStringToBinary** and providing the formerly encrypted string. An example of using these two functions is shown in [CryptBinaryToString returns invalid bytes](http://social.msdn.microsoft.com/Forums/vstudio/en-US/9acac904-c226-4ae0-9b7f-261993b9fda2/cryptbinarytostring-returns-invalid-bytes?forum=vcgeneral) in the MSDN forums for Visual Studio.

<a id="ID4EWBAE"></a>