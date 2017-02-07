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

//<SnippetUsings>
using Windows.ApplicationModel.AppService;
using Windows.System.RemoteSystems;
//</SnippetUsings>


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RemoteAppService
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private RemoteSystem remoteSys;
        int min_value;
        int max_value;


        public MainPage()
        {
            this.InitializeComponent();
        }

        //<SnippetAppService>
        // This method returns an open connection to a particular app service on a remote system.
        // param "remotesys" is a RemoteSystem object representing the device to connect to.
        private async void openRemoteConnectionAsync(RemoteSystem remotesys)
        {
            // Set up a new app service connection. The app service name and package family name that
            // are used here correspond to the AppServices UWP sample.
            AppServiceConnection connection = new AppServiceConnection
            {
                AppServiceName = "com.microsoft.randomnumbergenerator",
                PackageFamilyName = "Microsoft.SDKSamples.AppServicesProvider.CS_8wekyb3d8bbwe"
            };
            //</SnippetAppService>

            //<SnippetRemoteConnection>
            // a valid RemoteSystem object is needed before going any further
            if (remotesys == null)
            {
                return;
            }

            // Create a remote system connection request for the given remote device
            RemoteSystemConnectionRequest connectionRequest = new RemoteSystemConnectionRequest(remotesys);

            // "open" the AppServiceConnection using the remote request
            AppServiceConnectionStatus status = await connection.OpenRemoteAsync(connectionRequest);

            // only continue if the connection opened successfully
            if (status != AppServiceConnectionStatus.Success)
            {
                return;
            }
            //</SnippetRemoteConnection>

            //<SnippetSendMessage>
            // create the command input
            ValueSet inputs = new ValueSet();

            // min_value and max_value vars are obtained somewhere else in the program
            inputs.Add("minvalue", min_value);
            inputs.Add("maxvalue", max_value);

            // send input and receive output in a variable
            AppServiceResponse response = await connection.SendMessageAsync(inputs);

            string result = "";
            // check that the service successfully received and processed the message
            if (response.Status == AppServiceResponseStatus.Success)
            {
                // Get the data that the service returned:
                result = response.Message["Result"] as string;
            }
        }
        //</SnippetSendMessage>
    }
}

