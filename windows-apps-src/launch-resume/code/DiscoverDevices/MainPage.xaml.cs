using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.RemoteSystems;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DiscoverDevices
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        // class variables:
        private RemoteSystemWatcher m_remoteSystemWatcher;
        private ObservableCollection<RemoteSystem> deviceList = new ObservableCollection<RemoteSystem>();
        private Dictionary<string, RemoteSystem> deviceMap = new Dictionary<string, RemoteSystem>();

        public MainPage()
        {
            this.InitializeComponent();
            initWatcher();
        }

        public async void initWatcher()
        {
            RemoteSystemAccessStatus status = await RemoteSystem.RequestAccessAsync();

            if (status == RemoteSystemAccessStatus.Allowed) {

                //<SnippetCreateWatcher>
                // store filter list
                List<IRemoteSystemFilter> listOfFilters = makeFilterList();

                // construct watcher with the list
                m_remoteSystemWatcher = RemoteSystem.CreateWatcher(listOfFilters);
                //</SnippetCreateWatcher>

                // assign to event handlers, etc...

                // start detecting
                m_remoteSystemWatcher.Start();
            }
        }

        //<SnippetMakeFilterList>
        private List<IRemoteSystemFilter> makeFilterList()
        {
            // construct an empty list
            List<IRemoteSystemFilter> localListOfFilters = new List<IRemoteSystemFilter>();

            // construct a discovery type filter that only allows "proximal" connections:
            RemoteSystemDiscoveryTypeFilter discoveryFilter = new RemoteSystemDiscoveryTypeFilter(RemoteSystemDiscoveryType.Proximal);


            // construct a device type filter that only allows desktop and mobile devices:
            // For this kind of filter, we must first create an IIterable of strings representing the device types to allow.
            // These strings are stored as static read-only properties of the RemoteSystemKinds class.
            List<String> listOfTypes = new List<String>();
            listOfTypes.Add(RemoteSystemKinds.Desktop);
            listOfTypes.Add(RemoteSystemKinds.Phone);

            // Put the list of device types into the constructor of the filter
            RemoteSystemKindFilter kindFilter = new RemoteSystemKindFilter(listOfTypes);


            // construct an availibility status filter that only allows devices marked as available:
            RemoteSystemStatusTypeFilter statusFilter = new RemoteSystemStatusTypeFilter(RemoteSystemStatusType.Available);


            // add the 3 filters to the listL
            localListOfFilters.Add(discoveryFilter);
            localListOfFilters.Add(kindFilter);
            localListOfFilters.Add(statusFilter);

            // return the list
            return localListOfFilters;
        }
        //</SnippetMakeFilterList>

        //<SnippetFindByHostName>
        private async Task<RemoteSystem> getDeviceByAddressAsync(string IPaddress)
        {
            // construct a HostName object
            Windows.Networking.HostName deviceHost = new Windows.Networking.HostName(IPaddress);

            // create a RemoteSystem object with the HostName
            RemoteSystem remotesys = await RemoteSystem.FindByHostNameAsync(deviceHost);

            return remotesys;
        }
        //</SnippetFindByHostName>
    }
}
