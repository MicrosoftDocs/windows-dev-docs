---
title: Use Connected Storage to delete data
author: aablackm
description: Learn how to use Connected Storage to delete blob and container data.
ms.assetid: ccf7488c-5d55-480e-b3aa-412220d03104
ms.author: aablackm
ms.date: 02/27/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, connected storage
ms.localizationpriority: low
---
# Use Connected Storage to delete data

Data blobs are asynchronously deleted by creating a `ConnectedStorageContainer` in a `ConnectedStorageSpace` for a user and calling the `SubmitUpdatesAsync` method on the container providing a list of strings representing the named blobs to be deleted for the blobsToDelete parameter.

Data Containers are asynchronously deleted by creating a `ConnectedStorageContainer` and calling its `DeleteContainerAsync` method.

## To delete blob data from Connected Storage

1.  Retrieve a `ConnectedStorageSpace` object for the user by calling `GetForUserAsync`.

    In the XDK example the returned `ConnectedStorageSpace` object is being added to a map to enable easy management of `ConnectedStorageSpace` objects for multiple users.

2.  Create a `ConnectedStorageContainer` object by calling `CreateContainer` on the `ConnectedStorageSpace` object.
3.  Call `SubmitUpdatesAsync` on the `ConnectedStorageContainer` object.

## To delete a container from Connected Storage

1.  Retrieve a `ConnectedStorageSpace` object for the user by calling `GetForUserAsync`.

    In the XDK example the returned `ConnectedStorageSpace` object is being added to a map to enable easy management of `ConnectedStorageSpace` objects for multiple users.

2.  Call the `DeleteContainerAsync` method of the ConnectedStorageSpace Methods.

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

// Delete data from a fixed container/blob name into an IBuffer
void DeleteBlob(User^ user)
{

    //Create a list of blob names to delete
    std::vector<Platform::String^> blobsToDelete;

    //Add the blob name to your Generic Iterable
    blobsToDelete.push_back(L"someBlobName");

    auto blobNames = ref new Platform::Collections::VectorView<Platform::String^>(blobsToDelete);

    auto storageSpace = gConnectedStorageSpaceForUsers->Lookup(user->Id);
    auto container = storageSpace->CreateContainer("Saves/Checkpoint");

    //Delete is occurring asynchronously at this point.

    auto op = container->SubmitUpdatesAsync(nullptr, blobNames);

    op->Completed = ref new AsyncActionCompletedHandler(
        [=](IAsyncAction^ a, Windows::Foundation::AsyncStatus status){
            switch(status)
            {
                case Windows::Foundation::AsyncStatus::Completed:
                //Delete was successful no need for action
                break;
                case Windows::Foundation::AsyncStatus::Error:
                case Windows::Foundation::AsyncStatus::Canceled:
                // Handle failed deletion
            }
     });
}

void DeleteContainer(User^ user)
{
    auto storageSpace = gConnectedStorageSpaceForUsers->Lookup(user->Id);

    auto deleteOperation = storageSpace->DeleteContainerAsync("Saves/Checkpoint");

    deleteOperation->Completed = ref new AsyncActionCompletedHandler(
        [=](IAsyncAction^ a, Windows::Foundation::AsyncStatus status){
            switch(status)
            {
                case Windows::Foundation::AsyncStatus::Completed:
                //Delete was successful no need for action
                break;
                case Windows::Foundation::AsyncStatus::Error:
                case Windows::Foundation::AsyncStatus::Canceled:
                // Handle failed deletion
            }
     });

}
```

You can find the XDK Connected Storage APIs documented in the XDK .chm file under the path:
**Xbox ONE XDK >> API Reference >> Platform API Reference >> System API Reference >> Windows.Xbox.Storage**.
The XDK APIs are also documented on the [developer.microsoft.com site](https://developer.microsoft.com/en-us/games/xbox/docs/xdk/storage-xbox-microsoft-n).
The link to XDK APIs requires that you have a Microsoft Account(MSA) that has been enabled for Xbox Developer Kit(XDK) access.
Windows.Xbox.Storage is the name of the Connected Storage namespace for Xbox One consoles.


## C# UWP sample

While XDK games and UWP apps may use different APIs, the UWP API is modeled after the XDK API very closely. To delete data you will still need to follow the same basic steps while making note of some namespace and class name changes. Instead of using the namespace `Windows::Xbox::Storage` you will use `Windows.Gaming.XboxLive.Storage`. The class `ConnectedStorageSpace`, is equivalent to `GameSaveProvider`. The class `ConnectedStorageContainer` is equivalent to `GameSaveContainer`. These changes are further detailed in the Connected Storage Section of [Porting Xbox Live Code From XDK to UWP](../../using-xbox-live/porting-xbox-live-code-from-xdk-to-uwp.md).

```csharp
//Namespace Required
Windows.Gaming.XboxLive.Storage

//Get The User
var users = await Windows.System.User.FindAllAsync();

int intData = 23;
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
    //throw new Exception("Game Save Provider Initialization failed");
}

//Now you have a GameSaveProvider (formerly ConnectedStorageSpace)
//Next you need to call CreateContainer to get a GameSaveContainer (formerly ConnectedStorageContainer)

GameSaveContainer gameSaveContainer = gameSaveProvider.CreateContainer(c_saveContainerName); // this will create a new named game save container with the name = to the input name
//Parameter
//string name (name of container to be created) 

//DELETE A BLOB
//form an array of strings containing the blob names you would like to delete.
string[] blobsToDelete = new string[] { c_saveBlobName };

//Call SubmitUpdatesAsync with the dictionary of the names of blobs to delete as the second parameter.
GameSaveOperationResult gameSaveOperationResult = await gameSaveContainer.SubmitUpdatesAsync(null, blobsToDelete, c_saveContainerDisplayName);
//Parameters
//IReadOnlyDictionary<String, IBuffer> blobsToWrite
//IEnumerable<string> blobsToDelete
//string displayName

//Check status of the operation to ensure successful delete.
if(gameSaveOperationResult.Status == GameSaveErrorStatus.Ok)
{
    //Successful Deletion Logic
}
else
{
    //handle failed deletion logic. 
}

//DELETE A SAVE CONTAINER
GameSaveOperationResult deleteContainerResult = await gameSaveProvider.DeleteContainerAsync(c_saveContainerName);
//Parameter
//string name (name of container to be deleted)

//Check status of the operation to ensure successful delete.
if(deleteContainerResult.Status == GameSaveErrorStatus.Ok)
{
    //Successful Deletion Logic
}
else
{
    //handle failed deletion logic. 
}
```

Connected Storage APIs for UWP apps are documented in the [Xbox Live API reference](https://docs.microsoft.com/en-us/uwp/api/windows.gaming.xboxlive.storage).
To see another sample that uses Connected Storage check out the [Xbox Live API Samples Game Save project](https://github.com/Microsoft/xbox-live-samples/tree/master/Samples/ID%40XboxSDK/GameSave).
