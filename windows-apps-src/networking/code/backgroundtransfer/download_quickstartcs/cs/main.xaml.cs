//<Snippetdownload_quickstartcs_A>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
//</Snippetdownload_quickstartcs_A>
//<Snippetdownload_quickstartcs_B>
private async void StartDownload_Click(object sender, RoutedEventArgs e)
{
	try
	{
		Uri source = new Uri(serverAddressField.Text.Trim());
		string destination = fileNameField.Text.Trim();

		StorageFile destinationFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
			destination, CreationCollisionOption.GenerateUniqueName);

		BackgroundDownloader downloader = new BackgroundDownloader();
		DownloadOperation download = downloader.CreateDownload(source, destinationFile);

		// Attach progress and completion handlers.
		HandleDownloadAsync(download, true);
	}
	catch (Exception ex)
	{
		LogException("Download Error", ex);
	}
}
//</Snippetdownload_quickstartcs_B>
//<Snippetdownload_quickstartcs_C>
private async void DiscoverActiveDownloads()
{
	activeDownloads = new List<DownloadOperation>();

	try
	{
		IReadOnlyList<DownloadOperation> downloads = await BackgroundDownloader.GetCurrentDownloadsAsync();

		foreach (DownloadOperation download in downloads)
		{                    
			// Attach progress and completion handlers.
			HandleDownloadAsync(download, false);
		}
	}
	catch (Exception ex)
	{
		LogException("Discovery error", ex);
	}
}
//</Snippetdownloadquickstartcs_C>