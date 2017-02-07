//<Snippetupload_quickstart_A>
function UploadOp() {
	var upload = null;
	var promise = null;

	this.start = function (uriString, file) {
		try {
		
			var uri = new Windows.Foundation.Uri(uriString);
			var uploader = new Windows.Networking.BackgroundTransfer.BackgroundUploader();

			// Set a header, so the server can save the file (this is specific to the sample server).
			uploader.setRequestHeader("Filename", file.name);

			// Create a new upload operation.
			upload = uploader.createUpload(uri, file);

			// Start the upload and persist the promise to be able to cancel the upload.
			promise = upload.startAsync().then(complete, error, progress);
		} catch (err) {
			displayError(err);
		}
	};
	// On application activation, reassign callbacks for a upload
	// operation persisted from previous application state.
	this.load = function (loadedUpload) {
		try {
			upload = loadedUpload;
			promise = upload.attachAsync().then(complete, error, progress);
		} catch (err) {
			displayError(err);
		}
	};
}
//</Snippetupload_quickstart_A>

//<Snippetupload_quickstart_B>
function uploadFile() {
	var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
	filePicker.fileTypeFilter.replaceAll(["*"]);

	filePicker.pickSingleFileAsync().then(function (file) {
		if (!file) {
			printLog("No file selected");
			return;
		}

		var upload = new UploadOp();
		var uriString = document.getElementById("serverAddressField").value;
		upload.start(uriString, file);

		// Store the upload operation in the uploadOps array.
		uploadOperations.push(upload);
	});
}
//</Snippetupload_quickstart_B>
//<Snippetupload_quickstart_C>
var uploadOperations = [];
//</Snippetupload_quickstart_C>
 
//<Snippetupload_quickstart_D> 
function Windows.Networking.BackgroundTransfer.BackgroundUploader.getCurrentUploadsAsync() {
	.then(function (uploads) {
		for (var i = 0; i < uploads.size; i++) {
			var upload = new UploadOp();
			upload.load(uploads[i]);
			uploadOperations.push(upload);
		}
	}
};
//</Snippetupload_quickstart_D>