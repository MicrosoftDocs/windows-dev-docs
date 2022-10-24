using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Capture.Frames;
using Windows.UI.Core;

namespace Frames_Win10
{
    //<SnippetRemoteCameraPairingHelper>
    class RemoteCameraPairingHelper : IDisposable
    {
        private CoreDispatcher _dispatcher;
        private DeviceWatcher _watcher;
        private ObservableCollection<MediaFrameSourceGroup> _remoteCameraCollection;
        public RemoteCameraPairingHelper(CoreDispatcher uiDispatcher)
        {
            _dispatcher = uiDispatcher;
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
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    _remoteCameraCollection.Add(group);
                });
            }
        }
        private async Task RemoveDevice(string id)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var existing = _remoteCameraCollection.FirstOrDefault(item => item.Id == id);
                if (existing != null)
                {
                    _remoteCameraCollection.Remove(existing);
                }
            });
        }
        //</SnippetRemoteCameraPairingHelper>
    }
}
