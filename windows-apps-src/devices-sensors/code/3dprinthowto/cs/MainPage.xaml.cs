//<Snippet3DPrintNamespace>
using Windows.Graphics.Printing3D;
//</Snippet3DPrintNamespace>

//<SnippetOtherNamespaces>
using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//</SnippetOtherNamespaces>

using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.System.Threading;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq;
using System.Numerics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _3DPrintHowTo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //<SnippetDeclareVars>
        private Print3DTask printTask;
        private StorageFile file;
        private Printing3D3MFPackage package = new Printing3D3MFPackage();
        //</SnippetDeclareVars>

        public MainPage()
        {
            this.InitializeComponent();
        }

        //<SnippetFileLoad>
        private async void OnLoadClick(object sender, RoutedEventArgs e) {

            FileOpenPicker openPicker = new FileOpenPicker();

            // allow common 3D data file types
            openPicker.FileTypeFilter.Add(".3mf");
            openPicker.FileTypeFilter.Add(".stl");
            openPicker.FileTypeFilter.Add(".ply");
            openPicker.FileTypeFilter.Add(".obj");

            // pick a file and assign it to this class' 'file' member
            file = await openPicker.PickSingleFileAsync();
            if (file == null) {
                return;
            }
            //</SnippetFileLoad>

            //<SnippetFileCheck>
            // if user loaded a non-3mf file type
            if (file.FileType != ".3mf") {

                // elect 3D Builder as the application to launch
                LauncherOptions options = new LauncherOptions();
                options.TargetApplicationPackageFamilyName = "Microsoft.3DBuilder_8wekyb3d8bbwe";

                // Launch the retrieved file in 3D builder
                bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);

                // prompt the user to save as .3mf
                OutputTextBlock.Text = "save " + file.Name + " as a .3mf file and reload.";
                
                // have user choose another file (ideally the newly-saved .3mf file)
                file = await openPicker.PickSingleFileAsync();

            } else {
                // if the file type is .3mf
                // notify user that load was successful
                OutputTextBlock.Text = file.Name + " loaded as file";
            }
        }
        //</SnippetFileCheck>

        //<SnippetRepairModel>
        private async void OnFixClick(object sender, RoutedEventArgs e) {

            // read the loaded file's data as a data stream
            IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

            // assign a Printing3DModel to this data stream
            Printing3DModel model = await package.LoadModelFromPackageAsync(fileStream);

            // use Printing3DModel's repair function
            OutputTextBlock.Text = "repairing model";
            var data = model.RepairAsync();
            //</SnippetRepairModel>

            //<SnippetSaveModel>
            // save model to this class' Printing3D3MFPackage
            OutputTextBlock.Text = "saving model to 3MF package";
            await package.SaveModelToPackageAsync(model);

        }
        //</SnippetSaveModel>

        //<SnippetRegisterMyTaskRequested>
        private async void OnPrintClick(object sender, RoutedEventArgs e) {

            // get a reference to this class' Print3DManager
            Print3DManager myManager = Print3DManager.GetForCurrentView();

            // register the method 'MyTaskRequested' to the Print3DManager's TaskRequested event
            myManager.TaskRequested += MyTaskRequested;
            //</SnippetRegisterMyTaskRequested>

            //<SnippetShowDialog>
            // show the 3D print dialog
            OutputTextBlock.Text = "opening print dialog";
            var result = await Print3DManager.ShowPrintUIAsync();
            //</SnippetShowDialog>

            //<SnippetDeregisterMyTaskRequested>
            // remove the print task request after dialog is shown            
            myManager.TaskRequested -= MyTaskRequested;
        }
        //</SnippetDeregisterMyTaskRequested>
        
        // task handling (event response): sets up the class' printTask object.

        //<SnippetMyTaskTitle>
        private void MyTaskRequested(Print3DManager sender, Print3DTaskRequestedEventArgs args) {
            //</SnippetMyTaskTitle>

            //<SnippetSourceHandler>           
            // this delegate handles the API's request for a source package
            Print3DTaskSourceRequestedHandler sourceHandler = delegate (Print3DTaskSourceRequestedArgs sourceRequestedArgs) {
                sourceRequestedArgs.SetSource(package);
            };
            //</SnippetSourceHandler>

            //<SnippetCreateTask>
            // the Print3DTaskRequest ('Request'), a member of 'args', creates a Print3DTask to be sent down the pipeline.
            printTask = args.Request.CreateTask("Print Title", "Default", sourceHandler);
            //</SnippetCreateTask>

            //<SnippetOptional>
            // optional events to handle
            printTask.Completed += Task_Completed; 
            printTask.Submitting += Task_Submitting;
            //</SnippetOptional>
        }
        

        private void Task_Submitting(Print3DTask sender, object args) {
            // notify user if required
        }
        private void Task_Completed(Print3DTask sender, Print3DTaskCompletedEventArgs args) {
            // notify user if required
        }

        // ************************************** //


       

        /***************************/

        // NOT USED
        /*
        private async void On3DPrint8(object sender, RoutedEventArgs e) {
            bool result;
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".3mf");

            file = await openPicker.PickSingleFileAsync();
            if (file == null) {
                return;
            }
            var fileStream = await file.OpenAsync(FileAccessMode.Read);
            // register the PrintTaskRequest callback to get the Printing3D3MFPackage
            Print3DManager.GetForCurrentView().TaskRequested += MainPage_TaskRequested;
            // the package.LoadAsync needs to be invoked in the same threading model as when setsource is performed which currently is in a background thread.
            var op = ThreadPool.RunAsync(async delegate { result = await Compute(fileStream); });
            // register the PrintTaskRequest callback to get the Printing3D3MFPackage
            // show the PrintUI
            result = await Print3DManager.ShowPrintUIAsync();
            Print3DManager.GetForCurrentView().TaskRequested -= MainPage_TaskRequested;
        }

        // NOT USED
        private async Task<bool> Compute(IRandomAccessStream fileStream) {
            package = await Printing3D3MFPackage.LoadAsync(fileStream);
            return true;
        }

        private async Task<IRandomAccessStream> FixTextureContentType(IRandomAccessStream modelStream) {
            XDocument xmldoc = XDocument.Load(modelStream.AsStreamForRead());

            var outputStream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
            var writer = new Windows.Storage.Streams.DataWriter(outputStream);
            writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            writer.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;
            writer.WriteString("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            var text = xmldoc.ToString();

            var replacedText = text.Replace("contenttype=\"\"", "contenttype=\"image/png\"");
            writer.WriteString(replacedText);

            await writer.StoreAsync();
            await writer.FlushAsync();
            writer.DetachStream();
            return outputStream;
        }


    */
    }
}
