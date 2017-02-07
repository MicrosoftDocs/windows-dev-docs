using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//<SnippetBuiltInCastingUsing>
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Media.Core;
//</SnippetBuiltInCastingUsing>

//<SnippetCastingNamespace>
using Windows.Media.Casting;
//</SnippetCastingNamespace>

//<SnippetEnumerationNamespace>
using Windows.Devices.Enumeration;
//</SnippetEnumerationNamespace>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaCasting_RS1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //<SnippetInitCastingPicker>
            //Initialize our picker object
            castingPicker = new CastingDevicePicker();

            //Set the picker to filter to video capable casting devices
            castingPicker.Filter.SupportsVideo = true;

            //Hook up device selected event
            castingPicker.CastingDeviceSelected += CastingPicker_CastingDeviceSelected;
            //</SnippetInitCastingPicker>
        }
        //<SnippetOpenButtonClick>
        private async void openButton_Click(object sender, RoutedEventArgs e)
        {
            //Create a new picker
            FileOpenPicker filePicker = new FileOpenPicker();

            //Add filetype filters.  In this case wmv and mp4.
            filePicker.FileTypeFilter.Add(".wmv");
            filePicker.FileTypeFilter.Add(".mp4");

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            //Retrieve file from picker
            StorageFile file = await filePicker.PickSingleFileAsync();

            //If we got a file, load it into the media lement
            if (file != null)
            {
                mediaPlayerElement.Source = MediaSource.CreateFromStorageFile(file);
                mediaPlayerElement.MediaPlayer.Play();
            }
        }
        //</SnippetOpenButtonClick>

        //<SnippetDeclareCastingPicker>
        CastingDevicePicker castingPicker;
        //</SnippetDeclareCastingPicker>

        //<SnippetCastPickerButtonClick>
        private void castPickerButton_Click(object sender, RoutedEventArgs e)
        {
            //Retrieve the location of the casting button
            GeneralTransform transform = castPickerButton.TransformToVisual(Window.Current.Content as UIElement);
            Point pt = transform.TransformPoint(new Point(0, 0));

            //Show the picker above our casting button
            castingPicker.Show(new Rect(pt.X, pt.Y, castPickerButton.ActualWidth, castPickerButton.ActualHeight),
                Windows.UI.Popups.Placement.Above);
        }
        //</SnippetCastPickerButtonClick>

        //<SnippetCastingDeviceSelected>
        private async void CastingPicker_CastingDeviceSelected(CastingDevicePicker sender, CastingDeviceSelectedEventArgs args)
        {
            //Casting must occur from the UI thread.  This dispatches the casting calls to the UI thread.
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                //Create a casting conneciton from our selected casting device
                CastingConnection connection = args.SelectedCastingDevice.CreateCastingConnection();

                //Hook up the casting events
                connection.ErrorOccurred += Connection_ErrorOccurred;
                connection.StateChanged += Connection_StateChanged;

                //Cast the content loaded in the media element to the selected casting device
                await connection.RequestStartCastingAsync(mediaPlayerElement.MediaPlayer.GetAsCastingSource());
            });
        }
        //</SnippetCastingDeviceSelected>
        /*
        //<SnippetEmptyStateHandlers>
        private async void Connection_StateChanged(CastingConnection sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ShowMessageToUser("Casting Connection State Changed: " + sender.State);
            });
        }

        private async void Connection_ErrorOccurred(CastingConnection sender, CastingConnectionErrorOccurredEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ShowMessageToUser("Casting Connection State Changed: " + sender.State);
            });
        }
        //</SnippetEmptyStateHandlers>
        */

        //<SnippetDeclareDeviceWatcher>
        DeviceWatcher deviceWatcher;
        CastingConnection castingConnection;
        //</SnippetDeclareDeviceWatcher>

        //<SnippetStartWatcherButtonClick>
        private void startWatcherButton_Click(object sender, RoutedEventArgs e)
        {
            startWatcherButton.IsEnabled = false;
            watcherProgressRing.IsActive = true;

            castingDevicesListBox.Items.Clear();

            //Create our watcher and have it find casting devices capable of video casting
            deviceWatcher = DeviceInformation.CreateWatcher(CastingDevice.GetDeviceSelector(CastingPlaybackTypes.Video));

            //Register for watcher events
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Removed += DeviceWatcher_Removed;
            deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
            deviceWatcher.Stopped += DeviceWatcher_Stopped;
        }
        //</SnippetStartWatcherButtonClick>

        //<SnippetWatcherAdded>
        private async void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                //Add each discovered device to our listbox
                CastingDevice addedDevice = await CastingDevice.FromIdAsync(args.Id);
                castingDevicesListBox.Items.Add(addedDevice);
            });
        }
        //</SnippetWatcherAdded>

        //<SnippetWatcherRemoved>
        private async void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                foreach (CastingDevice currentDevice in castingDevicesListBox.Items)
                {
                    if (currentDevice.Id == args.Id)
                    {
                        castingDevicesListBox.Items.Remove(currentDevice);
                    }
                }
            });
        }
        //</SnippetWatcherRemoved>

        //<SnippetWatcherEnumerationCompleted>
        private async void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //If enumeration completes, update UI and transition watcher to the stopped state
                ShowMessageToUser("Watcher completed enumeration of devices");
                deviceWatcher.Stop();
            });
        }
        //</SnippetWatcherEnumerationCompleted>

        //<SnippetWatcherStopped>
        private async void DeviceWatcher_Stopped(DeviceWatcher sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //Update UX when the watcher stops
                startWatcherButton.IsEnabled = true;
                watcherProgressRing.IsActive = false;
            });
        }
        //</SnippetWatcherStopped>


        //<SnippetSelectionChanged>
        private async void castingDevicesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (castingDevicesListBox.SelectedItem != null)
            {
                //When a device is selected, first thing we do is stop the watcher so it's search doesn't conflict with streaming
                if (deviceWatcher.Status != DeviceWatcherStatus.Stopped)
                {
                    deviceWatcher.Stop();
                }

                //Create a new casting connection to the device that's been selected
                castingConnection = ((CastingDevice)castingDevicesListBox.SelectedItem).CreateCastingConnection();

                //Register for events
                castingConnection.ErrorOccurred += Connection_ErrorOccurred;
                castingConnection.StateChanged += Connection_StateChanged;

                //Cast the loaded video to the selected casting device.
                await castingConnection.RequestStartCastingAsync(mediaPlayerElement.MediaPlayer.GetAsCastingSource());
                disconnectButton.Visibility = Visibility.Visible;
            }
        }
        //</SnippetSelectionChanged>

        //<SnippetStateChanged>
        private async void Connection_StateChanged(CastingConnection sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //Update the UX based on the casting state
                if (sender.State == CastingConnectionState.Connected || sender.State == CastingConnectionState.Rendering)
                {
                    disconnectButton.Visibility = Visibility.Visible;
                    watcherProgressRing.IsActive = false;
                }
                else if (sender.State == CastingConnectionState.Disconnected)
                {
                    disconnectButton.Visibility = Visibility.Collapsed;
                    castingDevicesListBox.SelectedItem = null;
                    watcherProgressRing.IsActive = false;
                }
                else if (sender.State == CastingConnectionState.Connecting)
                {
                    disconnectButton.Visibility = Visibility.Collapsed;
                    ShowMessageToUser("Connecting");
                    watcherProgressRing.IsActive = true;
                }
                else
                {
                    //Disconnecting is the remaining state
                    disconnectButton.Visibility = Visibility.Collapsed;
                    watcherProgressRing.IsActive = true;
                }
            });
        }
        //</SnippetStateChanged>

        //<SnippetErrorOccurred>
        private async void Connection_ErrorOccurred(CastingConnection sender, CastingConnectionErrorOccurredEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //Clear the selection in the listbox on an error
                ShowMessageToUser("Casting Error: " + args.Message);
                castingDevicesListBox.SelectedItem = null;
            });
        }
        //</SnippetErrorOccurred>

        //<SnippetDisconnectButton>
        private async void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (castingConnection != null)
            {
                //When disconnect is clicked, the casting conneciton is disconnected.  The video should return locally to the media element.
                await castingConnection.DisconnectAsync();
            }
        }
        //</SnippetDisconnectButton>
        private void ShowMessageToUser(string message)
        {

        }


    }
}
