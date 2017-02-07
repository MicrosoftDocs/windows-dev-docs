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
using Windows.ApplicationModel.DataTransfer;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShareData
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //<SnippetRegisterShare>
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Share_Click(object s, RoutedEventArgs e)
        {
            //<SnippetPrepareToShare>
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
            //</SnippetPrepareToShare>


            //<SnippetShowUI>
            DataTransferManager.ShowShareUI();
            //</SnippetShowUI>
        }

        void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            //<SnippetCreateRequest>
            DataRequest request = args.Request;
            //</SnippetCreateRequest>

            //<SnippetSetContent>
            request.Data.SetText("Hello world!");
            //</SnippetSetContent>

            //<SnippetSetProperties>
            request.Data.Properties.Title = "Share Example";
            request.Data.Properties.Description = "A demonstration on how to share";
            //</SnippetSetProperties>
        }
    }
}
