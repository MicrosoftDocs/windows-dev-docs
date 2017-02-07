using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

//<SnippetLightsNamespace>
using Windows.Devices.Lights;
//</SnippetLightsNamespace>

//<SnippetEnumerationNamespace>
using Windows.Devices.Enumeration;
using System.Linq;
//</SnippetEnumerationNamespace>


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LampSnippets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //<SnippetDeclareLamp>
        Lamp lamp;
        //</SnippetDeclareLamp>

        public MainPage()
        {
            this.InitializeComponent();
        }
        public async void GetDefaultLamp()
        {
            //<SnippetGetDefaultLamp>
            lamp = await Lamp.GetDefaultAsync();

            if (lamp == null)
            {
                ShowErrorMessage("No Lamp device found");
                return;
            }
            //</SnippetGetDefaultLamp>
        }
        public async void GetLampUsingSelectionString()
        {
            //<SnippetGetLampWithSelectionString>
            string selectorString = Lamp.GetDeviceSelector();
            

            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(selectorString);

            DeviceInformation deviceInfo =
                devices.FirstOrDefault(di => di.EnclosureLocation != null && 
                    di.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back);

            if (deviceInfo == null)
            {
                ShowErrorMessage("No Lamp device found");
            }

            lamp = await Lamp.FromIdAsync(deviceInfo.Id);
            //</SnippetGetLampWithSelectionString>

        }
        //<SnippetDisposeLamp>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            lamp.AvailabilityChanged -= Lamp_AvailabilityChanged;
            lamp.IsEnabled = false;
            lamp.Dispose();
            lamp = null;
        }
        //</SnippetDisposeLamp>

        
        public void ChangeLampSettings()
        {
            //<SnippetLampSettingsOn>
            lamp.IsEnabled = true;
            //</SnippetLampSettingsOn>

            //<SnippetLampSettingsOff>
            lamp.IsEnabled = false;
            //</SnippetLampSettingsOff>

            //<SnippetLampSettingsColor>
            if (lamp.IsColorSettable)
            {
                lamp.Color = Windows.UI.Colors.Blue;
            }
            //</SnippetLampSettingsColor>
        }



        public async void AvailabilityChanged()
        {
            //<SnippetAvailabilityChanged>
            lamp = await Lamp.GetDefaultAsync();

            if (lamp == null)
            {
                ShowErrorMessage("No Lamp device found");
                return;
            }

            lamp.AvailabilityChanged += Lamp_AvailabilityChanged;
            //</SnippetAvailabilityChanged>
        }
        //<SnippetAvailabilityChangedHandler>
        private void Lamp_AvailabilityChanged(Lamp sender, LampAvailabilityChangedEventArgs args)
        {
            lampToggleSwitch.IsEnabled = args.IsAvailable;
        }
        //</SnippetAvailabilityChangedHandler>

        
     

        public void ShowErrorMessage(string message)
        {
            MessageTextBlock.Text = message;
        }
        
    }
}
