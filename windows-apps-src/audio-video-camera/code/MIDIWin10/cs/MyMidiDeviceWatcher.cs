using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Enumeration;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace MIDIWin10
{
    class MyMidiDeviceWatcher
    {
        //<SnippetWatcherVariables>
        DeviceWatcher deviceWatcher;
        string deviceSelectorString;
        ListBox deviceListBox;
        CoreDispatcher coreDispatcher;
        //</SnippetWatcherVariables>

        //<SnippetDeclareDeviceInformationCollection>
        public DeviceInformationCollection DeviceInformationCollection { get; set; }
        //</SnippetDeclareDeviceInformationCollection>

        //<SnippetWatcherConstructor>
        public MyMidiDeviceWatcher(string midiDeviceSelectorString, ListBox midiDeviceListBox, CoreDispatcher dispatcher)
        {
            deviceListBox = midiDeviceListBox;
            coreDispatcher = dispatcher;

            deviceSelectorString = midiDeviceSelectorString;

            deviceWatcher = DeviceInformation.CreateWatcher(deviceSelectorString);
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Removed += DeviceWatcher_Removed;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
        }
        //</SnippetWatcherConstructor>

        //<SnippetWatcherEventHandlers>
        private async void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            await coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }

        private async void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            await coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }

        private async void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            await coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }

        private async void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            await coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }
        //</SnippetWatcherEventHandlers>

        //<SnippetWatcherUpdateDevices>
        private async void UpdateDevices()
        {
            // Get a list of all MIDI devices
            this.DeviceInformationCollection = await DeviceInformation.FindAllAsync(deviceSelectorString);

            deviceListBox.Items.Clear();

            if (!this.DeviceInformationCollection.Any())
            {
                deviceListBox.Items.Add("No MIDI devices found!");
            }

            foreach(var deviceInformation in this.DeviceInformationCollection)
            {
                deviceListBox.Items.Add(deviceInformation.Name);
            }
        }
        //</SnippetWatcherUpdateDevices>

        //<SnippetWatcherStopStart>
        public void StartWatcher()
        {
            deviceWatcher.Start();
        }
        public void StopWatcher()
        {
            deviceWatcher.Stop();
        }
        //</SnippetWatcherStopStart>

        //<SnippetWatcherDestructor>
        ~MyMidiDeviceWatcher()
        {
            deviceWatcher.Added -= DeviceWatcher_Added;
            deviceWatcher.Removed -= DeviceWatcher_Removed;
            deviceWatcher.Updated -= DeviceWatcher_Updated;

            deviceWatcher = null;
        }
        //</SnippetWatcherDestructor>
    }
}
