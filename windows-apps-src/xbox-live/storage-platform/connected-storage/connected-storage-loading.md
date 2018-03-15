---
title: Use Connected Storage to load data
author: aablackm
description: Learn how to use Connected Storage to load data.
ms.assetid: c660a456-fe7d-453a-ae7b-9ecaa2ba0a15
ms.author: aablackm
ms.date: 02/27/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, connected storage
ms.localizationpriority: low
---

# Use Connected Storage to load data

Data is asynchronously read using the `ReadAsync` or `GetAsync` connected storage method.

### To load data from Connected Storage

1.  Retrieve a `ConnectedStorageSpace` for the user by calling `GetForUserAsync`.

    In the XDK example the returned `ConnectedStorageSpace` is being added to a map to enable easy management of `ConnectedStorageSpace` objects for multiple users.

2.  Create a `ConnectedStorageContainer` by calling `CreateContainer` on the `ConnectedStorageSpace`.
3.  Retrieve the data by calling `ReadAsync` or `GetAsync` on the `ConnectedStorageContainer`. `ReadAsync` requires you to pass in a buffer while `GetAsync` allocates new buffers to store the data that is read.

## C++ XDK sample

```cpp
auto gConnectedStorageSpaceForUsers = ref new Platform::Collections::Map<unsigned int, Windows::Xbox::Storage::ConnectedStorageSpace^>();

bool GetUserInputYesOrNo() { return true; };

User^ gCurrentUser;
IBuffer^ WrapRawBuffer(void* ptr, size_t size);

// Acquire a Connected Storage space for a user. A Connected Storage space is required to manipulate Connected Storage Data.
void PrepareConnectedStorage(User^ user)
{
  auto op = ConnectedStorageSpace::GetForUserAsync(user);
  op->Completed = ref new AsyncOperationCompletedHandler<ConnectedStorageSpace^>(
    [=](IAsyncOperation<ConnectedStorageSpace^>^ operation, Windows::Foundation::AsyncStatus status)
    {
      switch(status)
      {
        case Windows::Foundation::AsyncStatus::Completed:
          gConnectedStorageSpaceForUsers->Insert(user->Id, operation->GetResults());
          break;
        case Windows::Foundation::AsyncStatus::Error:
        case Windows::Foundation::AsyncStatus::Canceled:
          // Present user option: ?Would you like to continue without saving progress??
          if( GetUserInputYesOrNo() )
            //If the users opts yes, continue in offline mode
          else
            //If the users opts no, retry.
          break;
      }
    });
}

void OnLoadCompleted(IAsyncAction^ action, Windows::Foundation::AsyncStatus status);

// Load data from a fixed container/blob name into an IBuffer
void Load(Windows::Storage::Streams::IBuffer^ buffer)
{

    auto reads = ref new Platform::Collections::Map<Platform::String^, Windows::Storage::Streams::IBuffer^>();
    reads->Insert("data", buffer);

    auto storageSpace = gConnectedStorageSpaceForUsers->Lookup(gCurrentUser->Id);
    auto container = storageSpace->CreateContainer("Saves/Checkpoint");

    //Save Data is Loading

    auto op = container->ReadAsync(reads->GetView());

    op->Completed = ref new AsyncActionCompletedHandler(OnLoadCompleted);
}

void OnLoadCompleted(IAsyncAction^ action, Windows::Foundation::AsyncStatus status)
{
    switch (action->Status)
    {
    case Windows::Foundation::AsyncStatus::Completed:
        //Successful load logic here.
        break;

    case Windows::Foundation::AsyncStatus::Error:
    case Windows::Foundation::AsyncStatus::Canceled:
        //Fail logic here
        break;

    default:
        //all other possible values of action->status are also failures, alternate fail logic here. 
        break;
    }
}
```

You can find the XDK Connected Storage APIs documented in the XDK .chm file under the path:
**Xbox ONE XDK >> API Reference >> Platform API Reference >> System API Reference >> Windows.Xbox.Storage**.
The XDK APIs are also documented on the [developer.microsoft.com site](https://developer.microsoft.com/en-us/games/xbox/docs/xdk/storage-xbox-microsoft-n).
The link to XDK APIs requires that you have a Microsoft Account(MSA) that has been enabled for Xbox Developer Kit(XDK) access.
Windows.Xbox.Storage is the name of the Connected Storage namespace for Xbox One consoles.

## C# UWP sample

While XDK games and UWP apps may use different APIs, the UWP API is modeled after the XDK API very closely. To load data you will still need to follow the same basic steps while making note of some namespace and class name changes. Instead of using the namespace `Windows::Xbox::Storage` you will use `Windows.Gaming.XboxLive.Storage`. The class `ConnectedStorageSpace`, is equivalent to `GameSaveProvider`. The class `ConnectedStorageContainer` is equivalent to `GameSaveContainer`. These changes are further detailed in the Connected Storage Section of [Porting Xbox Live Code From XDK to UWP](../../using-xbox-live/porting-xbox-live-code-from-xdk-to-uwp.md).

```csharp
//Namespace Required
Windows.Gaming.XboxLive.Storage

//Get The User
var users = await Windows.System.User.FindAllAsync();

int intData = 0;
const string c_saveBlobName = "Jersey";
const string c_saveContainerDisplayName = "GameSave";
const string c_saveContainerName = "GameSaveContainer";
GameSaveProvider gameSaveProvider;

GameSaveProviderGetResult gameSaveTask = await GameSaveProvider.GetForUserAsync(users[0], context.AppConfig.ServiceConfigurationId); 
//Parameters
//Windows.System.User user
//string SCID

if(gameSaveTask.Status == GameSaveErrorStatus.Ok)
{
	gameSaveProvider = gameSaveTask.Value;
}
else
{
    return;
    //throw new Exception("Game Save Provider Initialization failed");;
}

//Now you have a GameSaveProvider
//Next you need to call CreateContainer to get a GameSaveContainer

GameSaveContainer gameSaveContainer = gameSaveProvider.CreateContainer(c_saveContainerName);
//Parameter
//string name (name of the GameSaveContainer Created)

//form an array of strings containing the blob names you would like to read.
string[] blobsToRead = new string[] { c_saveBlobName };

// GetAsync allocates a new Dictionary to hold the retrieved data. You can also use ReadAsync
// to provide your own preallocated Dictionary.
GameSaveBlobGetResult result = await container.GetAsync(blobsToRead);

int loadedData = 0;

//Check status to make sure data was read from the container
if(result.Status == GameSaveErrorStatus.Ok)
{
    //prepare a buffer to receive blob
    IBuffer loadedBuffer;

    //retrieve the named blob from the GetAsync result, place it in loaded buffer.
    result.Value.TryGetValue(c_saveBlobName, out loadedBuffer);

    if(loadedBuffer == null)
    {

        throw new Exception(String.Format("Didn't find expected blob \"{0}\" in the loaded data.", c_saveBlobName));

    }
    DataReader reader = DataReader.FromBuffer(loadedBuffer);
    loadedData = reader.ReadInt32();
}
```

Connected Storage APIs for UWP apps are documented in the [Xbox Live API reference](https://docs.microsoft.com/en-us/uwp/api/windows.gaming.xboxlive.storage).
To see another sample that uses Connected Storage check out the [Xbox Live API Samples Game Save project](https://github.com/Microsoft/xbox-live-samples/tree/master/Samples/ID%40XboxSDK/GameSave).