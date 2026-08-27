---
ms.assetid: 
description: Learn how to connect to remote cameras and get a MediaFrameSourceGroup to retrieve frames from each camera.
title: Connect to remote cameras
ms.date: 08/23/2026
ms.topic: article
ms.custom: 19H1
keywords: windows 10, winui 3
ms.localizationpriority: medium
---

# Connect to remote cameras

This article shows you how to connect to one or more remote cameras and get a [**MediaFrameSourceGroup**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceGroup) object that allows you to read frames from each camera. For more information on reading frames from a media source, see [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md). For more information on pairing with devices, see [Pair devices](/windows/apps/develop/devices-sensors/pair-devices).

> [!NOTE] 
> The features discussed in this article are available starting with Windows 10, version 1903.

## Create a DeviceWatcher class to watch for available remote cameras

The [**DeviceWatcher**](/uwp/api/windows.devices.enumeration.devicewatcher) class monitors the devices available to your app and notifies your app when devices are added or removed. Get an instance of **DeviceWatcher** by calling [**DeviceInformation.CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher#Windows_Devices_Enumeration_DeviceInformation_CreateWatcher_System_String_), passing in an Advanced Query Syntax (AQS) string that identifies the type of devices you want to monitor. The AQS string specifying network camera devices is the following:

```syntax
@"System.Devices.InterfaceClassGuid:=""{B8238652-B500-41EB-B4F3-4234F7F5AE99}"" AND System.Devices.InterfaceEnabled:=System.StructuredQueryType.Boolean#True"
```

> [!NOTE] 
> The helper method [**MediaFrameSourceGroup.GetDeviceSelector**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.getdeviceselector) returns an AQS string that will monitor locally-connected and remote network cameras. To monitor only network cameras, you should use the AQS string shown above.

When you start the returned **DeviceWatcher** by calling the [**Start**](/uwp/api/windows.devices.enumeration.devicewatcher.start) method, it will raise the [**Added**](/uwp/api/windows.devices.enumeration.devicewatcher.added) event for every network camera that is currently available. Until you stop the watcher by calling [**Stop**](/uwp/api/windows.devices.enumeration.devicewatcher.stop), the **Added** event will be raised when new network camera devices become available and the [**Removed**](/uwp/api/windows.devices.enumeration.devicewatcher.removed) event will be raised when a camera device becomes unavailable.

The event args passed into the **Added** and **Removed** event handlers are a [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) or a [**DeviceInformationUpdate**](/uwp/api/windows.devices.enumeration.deviceinformationupdate) object, respectively. Each of these objects has an **Id** property that is the identifier for the network camera for which the event was fired. Pass this ID into the [**MediaFrameSourceGroup.FromIdAsync**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.fromidasync) method to get a [**MediaFrameSourceGroup**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.fromidasync) object that you can use to retrieve frames from the camera.

## Remote camera pairing helper class

The following example shows a helper class that uses a **DeviceWatcher** to create and update an **ObservableCollection** of **MediaFrameSourceGroup** objects to support data binding to the list of cameras. Typical apps would wrap the **MediaFrameSourceGroup** in a custom model class. Note that the helper class maintains a reference to the app's [**DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) and updates the collection of cameras on the UI thread.

Also, this example handles the [**DeviceWatcher.Updated**](/uwp/api/windows.devices.enumeration.devicewatcher.updated) event in addition to the **Added** and **Removed** events. In the **Updated** handler, the associated remote camera device is removed from and then added back to the collection.

```csharp
class RemoteCameraPairingHelper : IDisposable
{
    private DispatcherQueue _dispatcherQueue;
    private DeviceWatcher _watcher;
    private ObservableCollection<MediaFrameSourceGroup> _remoteCameraCollection;

    public RemoteCameraPairingHelper(DispatcherQueue uiDispatcherQueue)
    {
        _dispatcherQueue = uiDispatcherQueue;
        _remoteCameraCollection = new ObservableCollection<MediaFrameSourceGroup>();
        var remoteCameraAqs = @"System.Devices.InterfaceClassGuid:=""{B8238652-B500-41EB-B4F3-4234F7F5AE99}"" AND System.Devices.InterfaceEnabled:=System.StructuredQueryType.Boolean#True";
        _watcher = DeviceInformation.CreateWatcher(remoteCameraAqs);
        _watcher.Added += Watcher_Added;
        _watcher.Removed += Watcher_Removed;
        _watcher.Updated += Watcher_Updated;
        _watcher.Start();
    }
    public void Dispose()
    {
        _watcher.Stop();
        _watcher.Updated -= Watcher_Updated;
        _watcher.Removed -= Watcher_Removed;
        _watcher.Added -= Watcher_Added;
    }
    public IReadOnlyList<MediaFrameSourceGroup> FrameSourceGroups
    {

        get { return _remoteCameraCollection; }
    }
    private async void Watcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
    {
        await RemoveDevice(args.Id);
        await AddDeviceAsync(args.Id);
    }
    private async void Watcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
    {
        await RemoveDevice(args.Id);
    }
    private async void Watcher_Added(DeviceWatcher sender, DeviceInformation args)
    {
        await AddDeviceAsync(args.Id);
    }
    private async Task AddDeviceAsync(string id)
    {
        var group = await MediaFrameSourceGroup.FromIdAsync(id);
        if (group != null)
        {
            _dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () => {
                _remoteCameraCollection.Add(group);
            });
        }
    }
    private async Task RemoveDevice(string id)
    {
        _dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        {
            var existing = _remoteCameraCollection.FirstOrDefault(item => item.Id == id);
            if (existing != null)
            {
                _remoteCameraCollection.Remove(existing);
            }
        });
    }
}
```

```cpp
struct RemoteCameraPairingHelper
{
    RemoteCameraPairingHelper(DispatcherQueue uiDispatcher) :
        m_dispatcherQueue(uiDispatcher)
    {
        m_remoteCameraCollection = winrt::single_threaded_observable_vector<MediaFrameSourceGroup>();
        auto remoteCameraAqs =
            LR"(System.Devices.InterfaceClassGuid:="{B8238652-B500-41EB-B4F3-4234F7F5AE99}")"
            LR"(AND System.Devices.InterfaceEnabled:=System.StructuredQueryType.Boolean#True)";

        m_watcher = DeviceInformation::CreateWatcher(remoteCameraAqs, nullptr);

        m_watcherAddedAutoRevoker = m_watcher.Added(winrt::auto_revoke, { this, &RemoteCameraPairingHelper::Watcher_Added });
        m_watcherRemovedAutoRevoker = m_watcher.Removed(winrt::auto_revoke, { this, &RemoteCameraPairingHelper::Watcher_Removed });
        m_watcherUpdatedAutoRevoker = m_watcher.Updated(winrt::auto_revoke, { this, &RemoteCameraPairingHelper::Watcher_Updated });
        m_watcher.Start();
    }
    ~RemoteCameraPairingHelper()
    {
        m_watcher.Stop();
    }
    IObservableVector<MediaFrameSourceGroup> FrameSourceGroups()
    {
        return m_remoteCameraCollection;
    }
    winrt::fire_and_forget Watcher_Added(DeviceWatcher /* sender */, DeviceInformation args)
    {
        co_await AddDeviceAsync(args.Id());
    }
    winrt::fire_and_forget Watcher_Removed(DeviceWatcher /* sender */, DeviceInformationUpdate args)
    {
        co_await RemoveDevice(args.Id());
    }
    winrt::fire_and_forget Watcher_Updated(DeviceWatcher /* sender */, DeviceInformationUpdate args)
    {
        co_await RemoveDevice(args.Id());
        co_await AddDeviceAsync(args.Id());
    }
    Windows::Foundation::IAsyncAction AddDeviceAsync(winrt::hstring id)
    {
        auto group = co_await MediaFrameSourceGroup::FromIdAsync(id);
        if (group)
        {
            //co_await m_dispatcherQueue;
            co_await wil::resume_foreground(m_dispatcherQueue);
            m_remoteCameraCollection.Append(group);
        }
    }
    Windows::Foundation::IAsyncAction RemoveDevice(winrt::hstring id)
    {
        //co_await m_dispatcherQueue;
        co_await wil::resume_foreground(m_dispatcherQueue);

        uint32_t ix{ 0 };
        for (auto const&& item : m_remoteCameraCollection)
        {
            if (item.Id() == id)
            {
                m_remoteCameraCollection.RemoveAt(ix);
                break;
            }
            ++ix;
        }
    }

private:
    DispatcherQueue m_dispatcherQueue{ nullptr };
    DeviceWatcher m_watcher{ nullptr };
    IObservableVector<MediaFrameSourceGroup> m_remoteCameraCollection;
    DeviceWatcher::Added_revoker m_watcherAddedAutoRevoker;
    DeviceWatcher::Removed_revoker m_watcherRemovedAutoRevoker;
    DeviceWatcher::Updated_revoker m_watcherUpdatedAutoRevoker;
};
```



## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)
* [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/CameraFrames)
* [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md)
