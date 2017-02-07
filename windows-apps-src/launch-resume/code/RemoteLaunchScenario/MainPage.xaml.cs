using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.System.RemoteSystems;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LaunchRemoteUri
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            BuildDeviceList();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            //<SnippetRemoteUriLaunch>
            if ( m_deviceList.Count > 0)
            {
                RemoteSystem SelectedDevice = m_deviceList[0];
                RemoteLaunchUriStatus launchUriStatus = 
                    await RemoteLauncher.LaunchUriAsync(
                        new RemoteSystemConnectionRequest(SelectedDevice), 
                        new Uri("bingmaps:?cp=47.6204~-122.3491&sty=3d&rad=200&pit=75&hdg=165"));
            }
            //</SnippetRemoteUriLaunch>
        }

        //<SnippetBuildDeviceList>
        private async Task BuildDeviceList()
        {
            RemoteSystemAccessStatus accessStatus = await RemoteSystem.RequestAccessAsync();

            if (accessStatus == RemoteSystemAccessStatus.Allowed)
            {
                m_remoteSystemWatcher = RemoteSystem.CreateWatcher();

                // Subscribing to the event raised when a new remote system is found by the watcher.
                m_remoteSystemWatcher.RemoteSystemAdded += RemoteSystemWatcher_RemoteSystemAdded;

                // Subscribing to the event raised when a previously found remote system is no longer available.
                m_remoteSystemWatcher.RemoteSystemRemoved += RemoteSystemWatcher_RemoteSystemRemoved;

                m_remoteSystemWatcher.Start();
            }
        }
        //</SnippetBuildDeviceList>

        //<SnippetEventHandlers>
        private void RemoteSystemWatcher_RemoteSystemRemoved(
            RemoteSystemWatcher sender, RemoteSystemRemovedEventArgs args)
        {
            if ( m_deviceMap.ContainsKey(args.RemoteSystemId))
            {
                m_deviceList.Remove(m_deviceMap[args.RemoteSystemId]);
                m_deviceMap.Remove(args.RemoteSystemId);
            }
        }

        private void RemoteSystemWatcher_RemoteSystemAdded(
            RemoteSystemWatcher sender, RemoteSystemAddedEventArgs args)
        {
            m_deviceList.Add(args.RemoteSystem);
            m_deviceMap.Add(args.RemoteSystem.Id, args.RemoteSystem);
        }
        //</SnippetEventHandlers>

        //<SnippetMembers>
        private RemoteSystemWatcher m_remoteSystemWatcher;
        private ObservableCollection<RemoteSystem> m_deviceList = new ObservableCollection<RemoteSystem>();
        private Dictionary<string, RemoteSystem> m_deviceMap = new Dictionary<string, RemoteSystem>();
        //</SnippetMembers>
    }
}
