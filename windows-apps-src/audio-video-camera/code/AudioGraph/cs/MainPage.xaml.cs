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
using Windows.Media.Audio;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Media.MediaProperties;
using Windows.Media;
using System.Runtime.InteropServices;
using Windows.Media.Effects;

//<SnippetUsingAudioEffectComponent>
using AudioEffectComponent;
//</SnippetUsingAudioEffectComponent>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AudioGraphSnippets
{
    // We are initializing a COM interface for use within the namespace
    // This interface allows access to memory at the byte level which we need to populate audio data that is generated
    //<SnippetComImportIMemoryBufferByteAccess>
    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess
    {
        void GetBuffer(out byte* buffer, out uint capacity);
    }
    //</SnippetComImportIMemoryBufferByteAccess>


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //<SnippetDeclareAudioGraph>
        AudioGraph audioGraph;
        //</SnippetDeclareAudioGraph>

        //<SnippetDeclareFileInputNode>
        AudioFileInputNode fileInputNode;
        //</SnippetDeclareFileInputNode>

        //<SnippetDeclareFileOutputNode>
        AudioFileOutputNode fileOutputNode;
        //</SnippetDeclareFileOutputNode>

        //<SnippetDeclareDeviceInputNode>
        AudioDeviceInputNode deviceInputNode;
        //</SnippetDeclareDeviceInputNode>

        //<SnippetDeclareDeviceOutputNode>
        AudioDeviceOutputNode deviceOutputNode;
        //</SnippetDeclareDeviceOutputNode>

        //<SnippetDeclareFrameInputNode>
        AudioFrameInputNode frameInputNode;
        //</SnippetDeclareFrameInputNode>

        //<SnippetDeclareFrameOutputNode>
        AudioFrameOutputNode frameOutputNode;
        //</SnippetDeclareFrameOutputNode>

        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await InitAudioGraph();
            await CreateFileInputNode();
            //await CreateFileOutputNode();
            await CreateDeviceOutputNode();
            //CreateFrameInputNode();
            //CreateFrameOutputNode();
            //CreateSubmixNode();

            

            //fileInputNode.AddOutgoingConnection(deviceOutputNode, .7);
            // AudioFileOutputNode fileOutputNode;
            //fileInputNode.AddOutgoingConnection(fileOutputNode, 1.0);
            //AudioSubmixNode submix = audioGraph.CreateSubmixNode();
            fileInputNode.AddOutgoingConnection(deviceOutputNode);
            //frameInputNode.AddOutgoingConnection(deviceOutputNode);
            //submix.AddOutgoingConnection(deviceOutputNode);
            //submix.AddOutgoingConnection(frameOutputNode);

            AddEffect();
            //AddCustomEffect();


            audioGraph.Start();
            //frameInputNode.Start();


        }

        private void FileInputNode_FileCompleted(AudioFileInputNode sender, object args)
        {
            throw new NotImplementedException();
        }
        //<SnippetInitAudioGraph>
        private async Task InitAudioGraph()
        {

            AudioGraphSettings settings = new AudioGraphSettings(Windows.Media.Render.AudioRenderCategory.Media);

            CreateAudioGraphResult result = await AudioGraph.CreateAsync(settings);
            if (result.Status != AudioGraphCreationStatus.Success)
            {
                ShowErrorMessage("AudioGraph creation error: " + result.Status.ToString());
            }

            audioGraph = result.Graph;

        }
        //</SnippetInitAudioGraph>

        private async Task EnumerateAudioRenderDevices()
        {
            AudioGraphSettings settings = new AudioGraphSettings(Windows.Media.Render.AudioRenderCategory.Media);

            //<SnippetEnumerateAudioRenderDevices>
            Windows.Devices.Enumeration.DeviceInformationCollection devices =
             await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Media.Devices.MediaDevice.GetAudioRenderSelector());

            // Show UI to allow the user to select a device
            Windows.Devices.Enumeration.DeviceInformation selectedDevice = ShowMyDeviceSelectionUI(devices);


            settings.PrimaryRenderDevice = selectedDevice;
            //</SnippetEnumerateAudioRenderDevices>
        }

        //<SnippetCreateFileInputNode>
        private async Task CreateFileInputNode()
        {
            if (audioGraph == null)
                return;

            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            filePicker.FileTypeFilter.Add(".mp3");
            filePicker.FileTypeFilter.Add(".wav");
            filePicker.FileTypeFilter.Add(".wma");
            filePicker.FileTypeFilter.Add(".m4a");
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            StorageFile file = await filePicker.PickSingleFileAsync();

            // File can be null if cancel is hit in the file picker
            if (file == null)
            {
                return;
            }
            CreateAudioFileInputNodeResult result = await audioGraph.CreateFileInputNodeAsync(file);

            if (result.Status != AudioFileNodeCreationStatus.Success)
            {
                ShowErrorMessage(result.Status.ToString());
            }

            fileInputNode = result.FileInputNode;
        }
        //</SnippetCreateFileInputNode>
        //<SnippetCreateFileOutputNode>
        private async Task CreateFileOutputNode()
        {
            FileSavePicker saveFilePicker = new FileSavePicker();
            saveFilePicker.FileTypeChoices.Add("Pulse Code Modulation", new List<string>() { ".wav" });
            saveFilePicker.FileTypeChoices.Add("Windows Media Audio", new List<string>() { ".wma" });
            saveFilePicker.FileTypeChoices.Add("MPEG Audio Layer-3", new List<string>() { ".mp3" });
            saveFilePicker.SuggestedFileName = "New Audio Track";
            StorageFile file = await saveFilePicker.PickSaveFileAsync();

            // File can be null if cancel is hit in the file picker
            if (file == null)
            {
                return;
            }

            Windows.Media.MediaProperties.MediaEncodingProfile mediaEncodingProfile;
            switch (file.FileType.ToString().ToLowerInvariant())
            {
                case ".wma":
                    mediaEncodingProfile = MediaEncodingProfile.CreateWma(AudioEncodingQuality.High);
                    break;
                case ".mp3":
                    mediaEncodingProfile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
                    break;
                case ".wav":
                    mediaEncodingProfile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.High);
                    break;
                default:
                    throw new ArgumentException();
            }


            // Operate node at the graph format, but save file at the specified format
            CreateAudioFileOutputNodeResult result = await audioGraph.CreateFileOutputNodeAsync(file, mediaEncodingProfile);

            if (result.Status != AudioFileNodeCreationStatus.Success)
            {
                // FileOutputNode creation failed
                ShowErrorMessage(result.Status.ToString());
                return;
            }

            fileOutputNode = result.FileOutputNode;
        }
        //</SnippetCreateFileOutputNode>

        //<SnippetCreateDeviceInputNode>
        private async Task CreateDeviceInputNode()
        {
            // Create a device output node
            CreateAudioDeviceInputNodeResult result = await audioGraph.CreateDeviceInputNodeAsync(Windows.Media.Capture.MediaCategory.Media);

            if (result.Status != AudioDeviceNodeCreationStatus.Success)
            {
                // Cannot create device output node
                ShowErrorMessage(result.Status.ToString());
                return;
            }

            deviceInputNode = result.DeviceInputNode;
        }
        //</SnippetCreateDeviceInputNode>

        //<SnippetCreateDeviceOutputNode>
        private async Task CreateDeviceOutputNode()
        {
            // Create a device output node
            CreateAudioDeviceOutputNodeResult result = await audioGraph.CreateDeviceOutputNodeAsync();

            if (result.Status != AudioDeviceNodeCreationStatus.Success)
            {
                // Cannot create device output node
                ShowErrorMessage(result.Status.ToString());
                return;
            }

            deviceOutputNode = result.DeviceOutputNode;
        }
        //</SnippetCreateDeviceOutputNode>


        private async Task EnumerateAudioCaptureDevices()
        {
            //<SnippetEnumerateAudioCaptureDevices>
            Windows.Devices.Enumeration.DeviceInformationCollection devices =
             await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Media.Devices.MediaDevice.GetAudioCaptureSelector());

            // Show UI to allow the user to select a device
            Windows.Devices.Enumeration.DeviceInformation selectedDevice = ShowMyDeviceSelectionUI(devices);

            CreateAudioDeviceInputNodeResult result =
                await audioGraph.CreateDeviceInputNodeAsync(Windows.Media.Capture.MediaCategory.Media, audioGraph.EncodingProperties, selectedDevice);
            //</SnippetEnumerateAudioCaptureDevices>
        }
        private Windows.Devices.Enumeration.DeviceInformation ShowMyDeviceSelectionUI(Windows.Devices.Enumeration.DeviceInformationCollection devices)
        {
            return devices[0];
        }


        //<SnippetCreateFrameInputNode>
        private void CreateFrameInputNode()
        {
            // Create the FrameInputNode at the same format as the graph, except explicitly set mono.
            AudioEncodingProperties nodeEncodingProperties = audioGraph.EncodingProperties;
            nodeEncodingProperties.ChannelCount = 1;
            frameInputNode = audioGraph.CreateFrameInputNode(nodeEncodingProperties);

            // Initialize the Frame Input Node in the stopped state
            frameInputNode.Stop();

            // Hook up an event handler so we can start generating samples when needed
            // This event is triggered when the node is required to provide data
            frameInputNode.QuantumStarted += node_QuantumStarted;
        }
        //</SnippetCreateFrameInputNode>

        //<SnippetQuantumStarted>
        private void node_QuantumStarted(AudioFrameInputNode sender, FrameInputNodeQuantumStartedEventArgs args)
        {
            // GenerateAudioData can provide PCM audio data by directly synthesizing it or reading from a file.
            // Need to know how many samples are required. In this case, the node is running at the same rate as the rest of the graph
            // For minimum latency, only provide the required amount of samples. Extra samples will introduce additional latency.
            uint numSamplesNeeded = (uint)args.RequiredSamples;

            if (numSamplesNeeded != 0)
            {
                AudioFrame audioData = GenerateAudioData(numSamplesNeeded);
                frameInputNode.AddFrame(audioData);
            }
        }
        //</SnippetQuantumStarted>


        // NOTE ABOUT COMPILING WITH /unsafe (Use unsafe code in project properties/build)
        public double theta = 0;

        //<SnippetGenerateAudioData>
        unsafe private AudioFrame GenerateAudioData(uint samples)
        {
            // Buffer size is (number of samples) * (size of each sample)
            // We choose to generate single channel (mono) audio. For multi-channel, multiply by number of channels
            uint bufferSize = samples * sizeof(float);
            AudioFrame frame = new Windows.Media.AudioFrame(bufferSize);

            using (AudioBuffer buffer = frame.LockBuffer(AudioBufferAccessMode.Write))
            using (IMemoryBufferReference reference = buffer.CreateReference())
            {
                byte* dataInBytes;
                uint capacityInBytes;
                float* dataInFloat;

                // Get the buffer from the AudioFrame
                ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacityInBytes);

                // Cast to float since the data we are generating is float
                dataInFloat = (float*)dataInBytes;

                float freq = 1000; // choosing to generate frequency of 1kHz
                float amplitude = 0.3f;
                int sampleRate = (int)audioGraph.EncodingProperties.SampleRate;
                double sampleIncrement = (freq * (Math.PI * 2)) / sampleRate;

                // Generate a 1kHz sine wave and populate the values in the memory buffer
                for (int i = 0; i < samples; i++)
                {
                    double sinValue = amplitude * Math.Sin(theta);
                    dataInFloat[i] = (float)sinValue;
                    theta += sampleIncrement;
                }
            }

            return frame;
        }
        //</SnippetGenerateAudioData>

        //<SnippetCreateFrameOutputNode>
        private void CreateFrameOutputNode()
        {
            frameOutputNode = audioGraph.CreateFrameOutputNode();
            audioGraph.QuantumProcessed += AudioGraph_QuantumProcessed;
        }
        //</SnippetCreateFrameOutputNode>

        //<SnippetQuantumProcessed>
        private void AudioGraph_QuantumProcessed(AudioGraph sender, object args)
        {
            AudioFrame frame = frameOutputNode.GetFrame();
            ProcessFrameOutput(frame);

        }
        //</SnippetQuantumProcessed>


        //<SnippetProcessFrameOutput>
        unsafe private void ProcessFrameOutput(AudioFrame frame)
        {
            using (AudioBuffer buffer = frame.LockBuffer(AudioBufferAccessMode.Write))
            using (IMemoryBufferReference reference = buffer.CreateReference())
            {
                byte* dataInBytes;
                uint capacityInBytes;
                float* dataInFloat;

                // Get the buffer from the AudioFrame
                ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacityInBytes);

                dataInFloat = (float*)dataInBytes;
            }
        }
        //</SnippetProcessFrameOutput>

        private void DemonstrateConnections()
        {
            //<SnippetAddOutgoingConnection1>
            fileInputNode.AddOutgoingConnection(deviceOutputNode);
            //</SnippetAddOutgoingConnection1>

            //<SnippetAddOutgoingConnection2>
            fileInputNode.AddOutgoingConnection(fileOutputNode);
            //</SnippetAddOutgoingConnection2>

            //<SnippetAddOutgoingConnection3>
            deviceInputNode.AddOutgoingConnection(deviceOutputNode, .5);
            //</SnippetAddOutgoingConnection3>

        }


        //<SnippetCreateSubmixNode>
        private void CreateSubmixNode()
        {
            AudioSubmixNode submixNode = audioGraph.CreateSubmixNode();
            fileInputNode.AddOutgoingConnection(submixNode);
            frameInputNode.AddOutgoingConnection(submixNode);
            submixNode.AddOutgoingConnection(fileOutputNode);
        }
        //</SnippetCreateSubmixNode>

        private void AddEffect()
        {
            AudioSubmixNode submixNode = audioGraph.CreateSubmixNode();

            //<SnippetAddEffect>
            EchoEffectDefinition echoEffect = new EchoEffectDefinition(audioGraph);
            echoEffect.Delay = 1000.0;
            echoEffect.Feedback = .2;
            echoEffect.WetDryMix = .5;

            submixNode.EffectDefinitions.Add(echoEffect);
            //</SnippetAddEffect>

            fileInputNode.EffectDefinitions.Add(echoEffect);
        }

        private void AddCustomEffect()
        {

            //<SnippetAddCustomEffect>
            // Create a property set and add a property/value pair
            PropertySet echoProperties = new PropertySet();
            echoProperties.Add("Mix", 0.5f);

            // Instantiate the custom effect defined in the 'AudioEffectComponent' project
            AudioEffectDefinition echoEffectDefinition = new AudioEffectDefinition(typeof(ExampleAudioEffect).FullName, echoProperties);
            fileInputNode.EffectDefinitions.Add(echoEffectDefinition);
            //</SnippetAddCustomEffect>

            
        }


        public async Task CreateFileInputNodeWithEmitter()
        {
            if (audioGraph == null)
                return;

            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            filePicker.FileTypeFilter.Add(".mp3");
            filePicker.FileTypeFilter.Add(".wav");
            filePicker.FileTypeFilter.Add(".wma");
            filePicker.FileTypeFilter.Add(".m4a");
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            StorageFile file = await filePicker.PickSingleFileAsync();

            // File can be null if cancel is hit in the file picker
            if (file == null)
            {
                return;
            }

            //<SnippetCreateEmitter>
            var emitterShape = AudioNodeEmitterShape.CreateOmnidirectional();
            var decayModel = AudioNodeEmitterDecayModel.CreateNatural(.1, 1, 10, 100);
            var settings = AudioNodeEmitterSettings.None;

            var emitter = new AudioNodeEmitter(emitterShape, decayModel, settings);
            emitter.Position = new System.Numerics.Vector3(10, 0, 5);

            CreateAudioFileInputNodeResult result = await audioGraph.CreateFileInputNodeAsync(file, emitter);

            if (result.Status != AudioFileNodeCreationStatus.Success)
            {
                ShowErrorMessage(result.Status.ToString());
            }

            fileInputNode = result.FileInputNode;
            //</SnippetCreateEmitter>


            

        }
        public void UpdateEmitter()
        {
            var newObjectPosition = new System.Numerics.Vector3(10, 0, 5);
            var oldObjectPosition = new System.Numerics.Vector3(10, 0, 5);

            //<SnippetUpdateEmitter>
            var emitter = fileInputNode.Emitter;
            emitter.Position = newObjectPosition;
            emitter.DopplerVelocity = newObjectPosition - oldObjectPosition;
            //</SnippetUpdateEmitter>
        }
        private void CreateListener()
        {
            
            //<SnippetListener>
            deviceOutputNode.Listener.Position = new System.Numerics.Vector3(100, 0, 0);
            deviceOutputNode.Listener.Orientation = System.Numerics.Quaternion.CreateFromYawPitchRoll(0, (float)Math.PI, 0);
            //</SnippetListener>
        }
        public void UpdateListener()
        {
            var newUserPosition = new System.Numerics.Vector3(10, 0, 5);

            //<SnippetUpdateListener>
            deviceOutputNode.Listener.Position = newUserPosition;
            //</SnippetUpdateListener>
        }

        public void ShowErrorMessage(string message)
        {
            MessageTextBlock.Text = message;
        }
    }
}
