//<Snippetdownload_quickstart_A>
function DownloadOp() {
    var download = null;
    var promise = null;
	var imageStream = null;

    this.start = function (uriString, fileName) {
        try {
            // Asynchronously create the file in the pictures folder.
            Windows.Storage.KnownFolders.picturesLibrary.createFileAsync(fileName, Windows.Storage.CreationCollisionOption.generateUniqueName).done(function (newFile) {
                var uri = Windows.Foundation.Uri(uriString);
                var downloader = new Windows.Networking.BackgroundTransfer.BackgroundDownloader();

                // Create a new download operation.
                download = downloader.createDownload(uri, newFile);

                // Start the download and persist the promise to be able to cancel the download.
                promise = download.startAsync().then(complete, error, progress);
            }, error);
        } catch (err) {
            displayException(err);
        }
    };
    // On application activation, reassign callbacks for a download
    // operation persisted from previous application state.
    this.load = function (loadedDownload) {
		try {
			download = loadedDownload;
			printLog("Found download: " + download.guid + " from previous application run.<br\>");
			promise = download.attachAsync().then(complete, error, progress);
		} catch (err) {
		    displayException(err);
		}
    };
}
//</Snippetdownload_quickstart_A>
//<Snippetdownload_quickstart_B>
// Cancel download.
this.cancel = function () {
	try {
		if (promise) {
			promise.cancel();
			promise = null;
			printLog("Canceling download: " + download.guid + "<br\>");
			if (imageStream) {
				imageStream.close();
			}
		}
		else {
			printLog("Download " + download.guid + " already canceled.<br\>");
		}
	} catch (err) {
		displayException(err);
	}
};
//</Snippetdownload_quickstart_B>
//<Snippetdownload_quickstart_C>
function downloadFile() {
	var newDownload = new DownloadOp();

	newDownload.start(fileName, fileUri);

}
//</Snippetdownload_quickstart_C>
//<Snippetdownload_quickstart_D>
var downloadOps = [];
//</Snippetdownload_quickstart_D>
//<Snippetdownload_quickstart_E>
// Enumerate outstanding downloads.
Windows.Networking.BackgroundTransfer.BackgroundDownloader.getCurrentDownloadsAsync().done(function (downloads) {

	for (var i = 0; i < downloads.size; i++) {
		var download = new DownloadOp();
		download.load(downloads[i]);
		downloadOps.push(download);
	}
});
//</Snippetdownload_quickstart_E>
