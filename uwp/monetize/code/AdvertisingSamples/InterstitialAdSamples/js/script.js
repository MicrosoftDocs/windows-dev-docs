//<script>
(function () {
    "use strict";

    // Assign applicationId and adUnitId to test values. Replace these values with live values 
    // from Dev Center before you submit your app to the Store.
    // <Snippet1>
    var interstitialAd = null;
    var applicationId = "d25517cb-12d4-4699-8bdc-52040c712cab";
    var adUnitId = "test";
    // </Snippet1>

    window.startInterstitial = function () {
        writeText("<br>Interstitial ads in JavaScript UWP apps");
        registerButtonEvents();

        // Initialize the InterstitialAd object and set up event handlers for it.
        prepareInterstitial();

        writeText("Press the buttons to request and show an interstitial ad.");
    };

    var registerButtonEvents = function () {
        requestAdButton.addEventListener("click", requestAdButtonClick);
        showAdButton.addEventListener("click", showAdButtonClick);
    };

    // This example requests an interstitial ad when the "Request ad" button is clicked. In a real app, 
    // you should request the interstitial ad close to when you think it will be shown, but with 
    // enough advance time to make the request and prepare the ad (say 30 seconds to a few minutes).
    // To show an interstitial banner ad instead of an interstitial video ad, replace InterstitialAdType.video 
    // with InterstitialAdType.display.
    var requestAdButtonClick = function (evt) {
        // <Snippet3>
        if (interstitialAd) {
            interstitialAd.requestAd(MicrosoftNSJS.Advertising.InterstitialAdType.video, applicationId, adUnitId);
        }
        // </Snippet3>
    }

    // This example attempts to show the interstitial ad when the "Show ad" button is clicked.
    var showAdButtonClick = function (evt) {
        if (interstitialAd && interstitialAd.state !== MicrosoftNSJS.Advertising.InterstitialAdState.showing) {
            showInterstitial();
        }
    }

    var restart = function () {
        if (interstitialAd) {
            interstitialAd.dispose();
        }
        interstitialAd = null;
        window.startInterstitial();
    };

    var clearText = function (msg) {
        description.innerHTML = "";
    };

    var writeText = function (msg) {
        description.innerHTML = description.innerHTML + msg + "<br>";
        description.scrollTop = description.scrollHeight;
    };

    var prepareInterstitial = function () {
        if (!interstitialAd) {
            // <Snippet2>
            interstitialAd = new MicrosoftNSJS.Advertising.InterstitialAd();
            interstitialAd.onErrorOccurred = errorOccurredHandler;
            interstitialAd.onAdReady = adReadyHandler;
            interstitialAd.onCancelled = cancelledHandler;
            interstitialAd.onCompleted = completedHandler;
            // </Snippet2>
        }
    };

    var showInterstitial = function () {
        if (interstitialAd && interstitialAd.state === MicrosoftNSJS.Advertising.InterstitialAdState.ready) {
            interstitialAd.show();
        } else {
            // No ad is available to show. Allow user to try again anyway
            clearText();
            writeText("<br>Unable to show an ad. Check the error log. You can try again.");
            restart();
        }
    };

    var errorOccurredHandler = function (sender, args) {
        console.log("error: " + args.errorMessage + " (" + args.errorCode + ")");
        if (!isPlaying) {
            clearText();
            writeText("<br>Unable to show an ad. Check the error log. You can try again.");
            restart();
        }
    };

    var adReadyHandler = function (sender) {
        console.log("Ad ready");
    };

    var cancelledHandler = function (sender) {
        console.log("Ad cancelled");
        writeText("<br>You must watch the entire ad to continue. <b>Press the button to watch the ad.</b>");
        interstitialAd.dispose();
        interstitialAd = null;
        prepareInterstitial();
    };

    var completedHandler = function (sender) {
        console.log("Ad complete");
        clearText();
        writeText("<br>Thanks for watching the ad! You can try again!");
        restart();
    };

})();
//</script>