        //<SnippetBackgroundTransferCS_A>
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
		//</SnippetBackgroundTransferCS_A>
		//<SnippetBackgroundTransferCS_B>
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
		//</SnippetBackgroundTransferCS_B>