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


//<SnippetUsing>
using Windows.Media.Import;
using System.Threading;
using Windows.UI.Core;
using System.Text;
//</SnippetUsing>


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PhotoImport_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //<SnippetDeclareCts>
        CancellationTokenSource cts;
        //</SnippetDeclareCts>

        //<SnippetOnCancel>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
                System.Diagnostics.Debug.WriteLine("Operation canceled by the Cancel button.");
            }
        }
        //</SnippetOnCancel>


        //<SnippetFindSourcesClick>
        private async void findSourcesButton_Click(object sender, RoutedEventArgs e)
        {
            var sources = await PhotoImportManager.FindAllSourcesAsync();
            foreach (PhotoImportSource source in sources)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = source.DisplayName;
                item.Tag = source;
                sourcesComboBox.Items.Add(item);
            }
        }
        //</SnippetFindSourcesClick>

        //<SnippetDeclareImportSource>
        PhotoImportSource importSource;
        //</SnippetDeclareImportSource>

        //<SnippetSourcesSelectionChanged>
        private void sourcesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.importSource = (PhotoImportSource)((ComboBoxItem)sourcesComboBox.SelectedItem).Tag;
            FindItems();
        }
        //</SnippetSourcesSelectionChanged>

        //<SnippetDeclareImport>
        PhotoImportSession importSession;
        PhotoImportFindItemsResult itemsResult;
        //</SnippetDeclareImport>

        //<SnippetGeneratorIncrementalLoadingClass>
        GeneratorIncrementalLoadingClass<ImportableItemWrapper> itemsToImport = null;
        //</SnippetGeneratorIncrementalLoadingClass>

        //<SnippetFindItems>
        private async void FindItems()
        {
            this.cts = new CancellationTokenSource();

            try
            {
                this.importSession = this.importSource.CreateImportSession();

                // Progress handler for FindItemsAsync
                var progress = new Progress<uint>((result) =>
                {
                    System.Diagnostics.Debug.WriteLine(String.Format("Found {0} Files", result.ToString()));
                });

                this.itemsResult =
                    await this.importSession.FindItemsAsync(PhotoImportContentTypeFilter.ImagesAndVideos, PhotoImportItemSelectionMode.SelectAll)
                    .AsTask(this.cts.Token, progress);

                // GeneratorIncrementalLoadingClass is used to incrementally load items in the Listview view including thumbnails
                this.itemsToImport = new GeneratorIncrementalLoadingClass<ImportableItemWrapper>(this.itemsResult.TotalCount,
                (int index) =>
                {
                    return new ImportableItemWrapper(this.itemsResult.FoundItems[index]);
                });

                // Set the items source for the ListView control
                this.fileListView.ItemsSource = this.itemsToImport;

                // Log the find results
                if (this.itemsResult != null)
                {
                    var findResultProperties = new System.Text.StringBuilder();
                    findResultProperties.AppendLine(String.Format("Photos\t\t\t :  {0} \t\t Selected Photos\t\t:  {1}", itemsResult.PhotosCount, itemsResult.SelectedPhotosCount));
                    findResultProperties.AppendLine(String.Format("Videos\t\t\t :  {0} \t\t Selected Videos\t\t:  {1}", itemsResult.VideosCount, itemsResult.SelectedVideosCount));
                    findResultProperties.AppendLine(String.Format("SideCars\t\t :  {0} \t\t Selected Sidecars\t:  {1}", itemsResult.SidecarsCount, itemsResult.SelectedSidecarsCount));
                    findResultProperties.AppendLine(String.Format("Siblings\t\t\t :  {0} \t\t Selected Sibilings\t:  {1} ", itemsResult.SiblingsCount, itemsResult.SelectedSiblingsCount));
                    findResultProperties.AppendLine(String.Format("Total Items Items\t :  {0} \t\t Selected TotalCount \t:  {1}", itemsResult.TotalCount, itemsResult.SelectedTotalCount));
                    System.Diagnostics.Debug.WriteLine(findResultProperties.ToString());
                }

                if (this.itemsResult.HasSucceeded)
                {
                    // Update UI to indicate success
                    System.Diagnostics.Debug.WriteLine("FindItemsAsync succeeded.");
                }
                else
                {
                    // Update UI to indicate that the operation did not complete
                    System.Diagnostics.Debug.WriteLine("FindItemsAsync did not succeed or was not completed.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Photo import find items operation failed. " + ex.Message);
            }


            this.cts = null;
        }
        //</SnippetFindItems>

        //<SnippetDeclareImportResult>
        private PhotoImportImportItemsResult importedResult;
        //</SnippetDeclareImportResult>

        //<SnippetImportClick>
        private async void importButton_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            progressBar.Value = 0;

            try
            {
                if (itemsResult.SelectedTotalCount <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("Nothing Selected for Import.");
                }
                else
                {
                    var progress = new Progress<PhotoImportProgress>((result) =>
                    {
                        progressBar.Value = result.ImportProgress;
                    });

                    this.itemsResult.ItemImported += async (s, a) =>
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            System.Diagnostics.Debug.WriteLine(String.Format("Imported: {0}", a.ImportedItem.Name));
                        });
                    };

                    // import items from the our list of selected items
                    this.importedResult = await this.itemsResult.ImportItemsAsync().AsTask(cts.Token, progress);

                    if (importedResult != null)
                    {
                        StringBuilder importedSummary = new StringBuilder();
                        importedSummary.AppendLine(String.Format("Photos Imported   \t:  {0} ", importedResult.PhotosCount));
                        importedSummary.AppendLine(String.Format("Videos Imported    \t:  {0} ", importedResult.VideosCount));
                        importedSummary.AppendLine(String.Format("SideCars Imported   \t:  {0} ", importedResult.SidecarsCount));
                        importedSummary.AppendLine(String.Format("Siblings Imported   \t:  {0} ", importedResult.SiblingsCount));
                        importedSummary.AppendLine(String.Format("Total Items Imported \t:  {0} ", importedResult.TotalCount));
                        importedSummary.AppendLine(String.Format("Total Bytes Imported \t:  {0} ", importedResult.TotalSizeInBytes));

                        System.Diagnostics.Debug.WriteLine(importedSummary.ToString());
                    }

                    if (!this.importedResult.HasSucceeded)
                    {
                        System.Diagnostics.Debug.WriteLine("ImportItemsAsync did not succeed or was not completed");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Files could not be imported. " + "Exception: " + ex.ToString());
            }

            cts = null;
        }
        //</SnippetImportClick>

        //<SnippetDeleteClick>

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            progressBar.Value = 0;

            try
            {
                if (importedResult == null)
                {
                    System.Diagnostics.Debug.WriteLine("Nothing was imported for deletion.");
                }
                else
                {
                    var progress = new Progress<double>((result) =>
                    {
                        this.progressBar.Value = result;
                    });

                    PhotoImportDeleteImportedItemsFromSourceResult deleteResult = await this.importedResult.DeleteImportedItemsFromSourceAsync().AsTask(cts.Token, progress);

                    if (deleteResult != null)
                    {
                        StringBuilder deletedResults = new StringBuilder();
                        deletedResults.AppendLine(String.Format("Total Photos Deleted:\t{0} ", deleteResult.PhotosCount));
                        deletedResults.AppendLine(String.Format("Total Videos Deleted:\t{0} ", deleteResult.VideosCount));
                        deletedResults.AppendLine(String.Format("Total Sidecars Deleted:\t{0} ", deleteResult.SidecarsCount));
                        deletedResults.AppendLine(String.Format("Total Sibilings Deleted:\t{0} ", deleteResult.SiblingsCount));
                        deletedResults.AppendLine(String.Format("Total Files Deleted:\t{0} ", deleteResult.TotalCount));
                        deletedResults.AppendLine(String.Format("Total Bytes Deleted:\t{0} ", deleteResult.TotalSizeInBytes));
                        System.Diagnostics.Debug.WriteLine(deletedResults.ToString());
                    }

                    if (!deleteResult.HasSucceeded)
                    {
                        System.Diagnostics.Debug.WriteLine("Delete operation did not succeed or was not completed");
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Files could not be Deleted." + "Exception: " + ex.ToString());
            }

            // set the CancellationTokenSource to null when the work is complete.
            cts = null;


        }
        //</SnippetDeleteClick>
    }
}

