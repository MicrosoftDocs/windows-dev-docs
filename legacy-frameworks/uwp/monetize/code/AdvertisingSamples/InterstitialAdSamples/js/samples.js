var myAppId = "d25517cb-12d4-4699-8bdc-52040c712cab";
var myAdUnitId = "11389925";

var interstitialAd = null

interstitialAd = new MicrosoftNSJS.Advertising.InterstitialAd();
interstitialAd.onAdReady = adReadyHandler;
interstitialAd.onErrorOccurred = errorOccurredHandler;
interstitialAd.onCompleted = completedHandler;
interstitialAd.onCancelled = cancelledHandler;

var myAdType = MicrosoftNSJS.Advertising.InterstitialAdType.video;
window.interstitialAd.requestAd(myAdType, myAppId, myAdUnitId);


var showInterstitial = function () {
	// <Snippet4>
	if (interstitialAd && interstitialAd.state === MicrosoftNSJS.Advertising.InterstitialAdState.ready) {
		interstitialAd.show();
	}
	// </Snippet4>
};

// <Snippet5>
function adReadyHandler(sender) {
  // Your code goes here.
}

function errorOccurredHandler(sender, args) {
  // Your code goes here.
}

function completedHandler(sender) {
  // Your code goes here.
}

function cancelledHandler(sender) {
  // Your code goes here.
}
// </Snippet5>



