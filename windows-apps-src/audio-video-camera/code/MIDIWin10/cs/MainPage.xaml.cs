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
using System.Threading.Tasks;

//<SnippetUsing>
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;
//</SnippetUsing>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MIDIWin10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //<SnippetDeclareDeviceWatchers>
        MyMidiDeviceWatcher inputDeviceWatcher;
        MyMidiDeviceWatcher outputDeviceWatcher;
        //</SnippetDeclareDeviceWatchers>

        //<SnippetDeclareMidiPorts>
        MidiInPort midiInPort;
        IMidiOutPort midiOutPort;
        //</SnippetDeclareMidiPorts>

        public MainPage()
        {
            this.InitializeComponent();
        }
        //<SnippetEnumerateMidiInputDevices>
        private async Task EnumerateMidiInputDevices()
        {
            // Find all input MIDI devices
            string midiInputQueryString = MidiInPort.GetDeviceSelector();
            DeviceInformationCollection midiInputDevices = await DeviceInformation.FindAllAsync(midiInputQueryString);

            midiInPortListBox.Items.Clear();

            // Return if no external devices are connected
            if (midiInputDevices.Count == 0)
            {
                this.midiInPortListBox.Items.Add("No MIDI input devices found!");
                this.midiInPortListBox.IsEnabled = false;
                return;
            }

            // Else, add each connected input device to the list
            foreach (DeviceInformation deviceInfo in midiInputDevices)
            {
                this.midiInPortListBox.Items.Add(deviceInfo.Name);
                this.midiInPortListBox.IsEnabled = true;
            }
        }
        //</SnippetEnumerateMidiInputDevices>

        //<SnippetEnumerateMidiOutputDevices>
        private async Task EnumerateMidiOutputDevices()
        {

            // Find all output MIDI devices
            string midiOutportQueryString = MidiOutPort.GetDeviceSelector();
            DeviceInformationCollection midiOutputDevices = await DeviceInformation.FindAllAsync(midiOutportQueryString);

            midiOutPortListBox.Items.Clear();

            // Return if no external devices are connected
            if (midiOutputDevices.Count == 0)
            {
                this.midiOutPortListBox.Items.Add("No MIDI output devices found!");
                this.midiOutPortListBox.IsEnabled = false;
                return;
            }

            // Else, add each connected input device to the list
            foreach (DeviceInformation deviceInfo in midiOutputDevices)
            {
                this.midiOutPortListBox.Items.Add(deviceInfo.Name);
                this.midiOutPortListBox.IsEnabled = true;
            }
        }
        //</SnippetEnumerateMidiOutputDevices>



        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            //EnumerateMidiInputDevices();
            //EnumerateMidiOutputDevices();


            //<SnippetStartWatchers>
            inputDeviceWatcher =
                new MyMidiDeviceWatcher(MidiInPort.GetDeviceSelector(), midiInPortListBox, Dispatcher);

            inputDeviceWatcher.StartWatcher();

            outputDeviceWatcher =
                new MyMidiDeviceWatcher(MidiOutPort.GetDeviceSelector(), midiOutPortListBox, Dispatcher);

            outputDeviceWatcher.StartWatcher();
            //</SnippetStartWatchers>
        }

        //<SnippetInPortSelectionChanged>
        private async void midiInPortListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var deviceInformationCollection = inputDeviceWatcher.DeviceInformationCollection;

            if (deviceInformationCollection == null)
            {
                return;
            }

            DeviceInformation devInfo = deviceInformationCollection[midiOutPortListBox.SelectedIndex];

            if (devInfo == null)
            {
                return;
            }

            midiInPort = await MidiInPort.FromIdAsync(devInfo.Id);

            if(midiInPort == null)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create MidiInPort from input device");
                return;
            }
            midiInPort.MessageReceived += MidiInPort_MessageReceived;
        }
        //</SnippetInPortSelectionChanged>

        //<SnippetMessageReceived>
        private void MidiInPort_MessageReceived(MidiInPort sender, MidiMessageReceivedEventArgs args)
        {
            IMidiMessage receivedMidiMessage = args.Message;

            System.Diagnostics.Debug.WriteLine(receivedMidiMessage.Timestamp.ToString());

            if (receivedMidiMessage.Type == MidiMessageType.NoteOn)
            {
                System.Diagnostics.Debug.WriteLine(((MidiNoteOnMessage)receivedMidiMessage).Channel);
                System.Diagnostics.Debug.WriteLine(((MidiNoteOnMessage)receivedMidiMessage).Note);
                System.Diagnostics.Debug.WriteLine(((MidiNoteOnMessage)receivedMidiMessage).Velocity);
            }
        }
        //</SnippetMessageReceived>

        //<SnippetOutPortSelectionChanged>
        private async void midiOutPortListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var deviceInformationCollection = outputDeviceWatcher.DeviceInformationCollection;

            if(deviceInformationCollection == null)
            {
                return;
            }

            DeviceInformation devInfo = deviceInformationCollection[midiOutPortListBox.SelectedIndex];

            if(devInfo == null)
            {
                return;
            }

            midiOutPort = await MidiOutPort.FromIdAsync(devInfo.Id);

            if (midiOutPort == null)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create MidiOutPort from output device");
                return;
            }

        }
        //</SnippetOutPortSelectionChanged>
        private void SendMidiMessage()
        {
            //<SnippetSendMessage>
            byte channel = 0;
            byte note = 60;
            byte velocity = 127;
            IMidiMessage midiMessageToSend = new MidiNoteOnMessage(channel, note, velocity);

            midiOutPort.SendMessage(midiMessageToSend);
            //</SnippetSendMessage>
        }

        private void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            SendMidiMessage();
        }

        private void CleanUp()
        {
            //<SnippetCleanUp>
            inputDeviceWatcher.StopWatcher();
            inputDeviceWatcher = null;

            outputDeviceWatcher.StopWatcher();
            outputDeviceWatcher = null;

            midiInPort.MessageReceived += MidiInPort_MessageReceived;
            midiInPort.Dispose();
            midiInPort = null;

            midiOutPort.Dispose();
            midiOutPort = null;
            //</SnippetCleanUp>
        }
    }
}
