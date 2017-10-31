---
title: Use Connected Storage to load data
author: KevinAsgari
description: Learn how to use Connected Storage to load data.
ms.assetid: c660a456-fe7d-453a-ae7b-9ecaa2ba0a15
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, connected storage
localizationpriority: medium
---

# Use Connected Storage to load data

Data is asynchronously read using the *ReadAsync* or *GetAsync* connected storage method.

### To load data from Connected Storage

1.  Retrieve a *ConnectedStorageSpace* for the user by calling *GetForUserAsync*.

    In this example the returned *ConnectedStorageSpace* is being added to a map to enable easy management of *ConnectedStorageSpace* objects for multiple users.

2.  Create a *ConnectedStorageContainer* by calling *CreateContainer* on the *ConnectedStorageSpace*.
3.  Retrieve the data by calling *ReadAsync* or *GetAsync* on the *ConnectedStorageContainer*. *ReadAsync* requires you to pass in a buffer while *GetAsync* allocates new buffers to store the data that is read.

## Sample

```cpp
Platform::Guid gPrimarySCID;
Platform::Guid gSecondarySCID;
Windows::Xbox::Storage::ConnectedStorageSpace^ gConnectedStorageSpaceForMachine;
enum LoadSaveState { LOADING, LOAD_COMPLETED, LOAD_FAILED, NO_SAVE_MODE, RETRY_LOAD, GETTING_STORAGE_SPACE, DELETE_SAVE_UI, SAVING, SAVE_COMPLETED, NONE };
LoadSaveState loadSaveState = LoadSaveState::NONE;
auto gConnectedStorageSpaceForUsers = ref new Platform::Collections::Map<unsigned int, Windows::Xbox::Storage::ConnectedStorageSpace^>();

void SetGameState(LoadSaveState state) { loadSaveState = state; }
bool GetUserInputYesOrNo() { return true; };

User^ gCurrentUser;
byte* GetBufferData(Windows::Storage::Streams::IBuffer^ buffer);
IBuffer^ WrapRawBuffer(void* ptr, size_t size);

void PrepareConnectedStorage(User^ user)
{
    auto op = ConnectedStorageSpace::GetForUserAsync(user);
    op->Completed = ref new AsyncOperationCompletedHandler<ConnectedStorageSpace^>(
        [=](IAsyncOperation<ConnectedStorageSpace^>^ operation, Windows::Foundation::AsyncStatus status)
    {
        switch (status)
        {
        case Windows::Foundation::AsyncStatus::Completed:
            gConnectedStorageSpaceForUsers->Insert(user->Id, operation->GetResults());
            break;
        case Windows::Foundation::AsyncStatus::Error:
        case Windows::Foundation::AsyncStatus::Canceled:
            // Present user option: ?Would you like to continue without saving progress??
            if (GetUserInputYesOrNo())
                SetGameState(LoadSaveState::NO_SAVE_MODE);
            else
                SetGameState(LoadSaveState::RETRY_LOAD);
            break;
        }
    });
}

extern void SetGameState(LoadSaveState state);

void OnLoadCompleted(IAsyncAction^ action, Windows::Foundation::AsyncStatus status);

// Load data from a fixed container/blob name into an IBuffer
void Load(Windows::Storage::Streams::IBuffer^ buffer)
{

    auto reads = ref new Platform::Collections::Map<Platform::String^, Windows::Storage::Streams::IBuffer^>();
    reads->Insert("data", buffer);

    auto storageSpace = gConnectedStorageSpaceForUsers->Lookup(gCurrentUser->Id);
    auto container = storageSpace->CreateContainer("Saves/Checkpoint");

    SetGameState(LoadSaveState::LOADING);

    auto op = container->ReadAsync(reads->GetView());

    op->Completed = ref new AsyncActionCompletedHandler(OnLoadCompleted);
}

void OnLoadCompleted(IAsyncAction^ action, Windows::Foundation::AsyncStatus status)
{
    switch (action->Status)
    {
    case Windows::Foundation::AsyncStatus::Completed:
        SetGameState(LoadSaveState::LOAD_COMPLETED);
        break;

    case Windows::Foundation::AsyncStatus::Error:
    case Windows::Foundation::AsyncStatus::Canceled:
        SetGameState(LoadSaveState::LOAD_FAILED);
        break;

    default:
        SetGameState(LoadSaveState::LOAD_FAILED);
        break;
    }
}
```
