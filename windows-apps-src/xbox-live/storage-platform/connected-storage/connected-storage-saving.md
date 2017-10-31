---
title: Use Connected Storage to save data
author: KevinAsgari
description: Learn how to use Connected Storage to save data.
ms.assetid: ccf7488c-5d55-480e-b3aa-412220d03104
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
localizationpriority: medium
---

# Use Connected Storage to save data


Data is asynchronously saved by creating a *ConnectedStorageContainer* in a *ConnectedStorageSpace* for a user and calling the *SubmitUpdatesAsync* method on the container.

| Important                                                                                                                                                                                                                                                                                                                                                                        |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Data dependencies across connected storage containers are unsafe. For example, uploading of one container to the cloud might complete, while another might remain queued for uploading. If the user moved to another console, the synchronization operation would allow the first container to be synchronized and accessed on the second console, without the first container being present. |

### To save data to connected storage

1.  Retrieve a *ConnectedStorageSpace* object for the user by calling *GetForUserAsync*.

    In this example the returned *ConnectedStorageSpace* object is being added to a map to enable easy management of *ConnectedStorageSpace* objects for multiple users.

2.  Create a *ConnectedStorageContainer* object by calling *CreateContainer* on the *ConnectedStorageSpac* object.
3.  Call *SubmitUpdatesAsync* on the *ConnectedStorageContainer* object.

## Sample

```cpp
Platform::Guid gPrimarySCID;
Platform::Guid gSecondarySCID;
Windows::Xbox::Storage::ConnectedStorageSpace^ gConnectedStorageSpaceForMachine;
enum LoadSaveState { LOADING, LOAD_COMPLETED, LOAD_FAILED, NO_SAVE_MODE, RETRY_LOAD, GETTING_STORAGE_SPACE, DELETE_SAVE_UI, SAVING, SAVE_COMPLETED, NONE };
LoadSaveState loadSaveState = LoadSaveState::NONE;
auto gConnectedStorageSpaceForUsers = ref new Platform::Collections::Map<unsigned int, Windows::Xbox::Storage::ConnectedStorageSpace^>();

void SetGameState(LoadSaveState state) {loadSaveState = state;}
bool GetUserInputYesOrNo() {return true;};

User^ gCurrentUser;
byte* GetBufferData(Windows::Storage::Streams::IBuffer^ buffer);
IBuffer^ WrapRawBuffer( void* ptr, size_t size );

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
            SetGameState(LoadSaveState::NO_SAVE_MODE);
          else
            SetGameState(LoadSaveState::RETRY_LOAD);
          break;
      }
    });
}

uint8* GetBufferPointer(IBuffer^ buffer);




enum Color { RED, BLUE };
enum EngineSize { BIG, SMALL };

struct CarData
{
    Color color;
    bool hasWheels;
    bool hasFancyRims;
    EngineSize engineSize;
};


const int MAX_CARS = 10;

struct SaveData
{
    CarData cars[MAX_CARS];
    int numCars;
    int currentCar;
    int cash;
};

SaveData gMySaveData;

bool gSaveInProgress;
void SaveCheckpoint(Windows::Storage::Streams::IBuffer^ buffer, User^ user);

void RenderSpinner() {};
bool ItIsTimeToSaveACheckpoint() {return true;};

void RenderOneFrame()
{
    // ...

    if (gSaveInProgress)
        RenderSpinner();

    // ...
}

void Update()
{
    if (ItIsTimeToSaveACheckpoint())
        SaveCheckpoint(WrapRawBuffer(&gMySaveData,sizeof(SaveData)),gCurrentUser);
}


void SaveCheckpoint(Windows::Storage::Streams::IBuffer^ buffer, User^ user)
{
     auto storageSpace = gConnectedStorageSpaceForUsers->Lookup( user->Id );

     auto container = storageSpace->CreateContainer("Saves/Checkpoint");

     auto updates = ref new Platform::Collections::Map<Platform::String^, Windows::Storage::Streams::IBuffer^>();
     updates->Insert("data", buffer);

     auto op = container->SubmitUpdatesAsync(updates->GetView(), nullptr);

     SetGameState(LoadSaveState::SAVING);
     //gSaveInProgress = true;

     op->Completed = ref new AsyncActionCompletedHandler(
               [=](IAsyncAction^ a, Windows::Foundation::AsyncStatus status){
                   SetGameState(LoadSaveState::SAVE_COMPLETED);
     });
}
```
