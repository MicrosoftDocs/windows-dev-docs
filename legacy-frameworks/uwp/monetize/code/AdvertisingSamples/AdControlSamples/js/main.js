// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509

(function () {
	"use strict";

	var app = WinJS.Application;
	var activation = Windows.ApplicationModel.Activation;
	var isFirstActivation = true;

	app.onactivated = function (args) {
		if (args.detail.kind === activation.ActivationKind.voiceCommand) {
			// TODO: Handle relevant ActivationKinds. For example, if your app can be started by voice commands,
			// this is a good place to decide whether to populate an input field or choose a different initial view.
		}
		else if (args.detail.kind === activation.ActivationKind.launch) {
			// A Launch activation happens when the user launches your app via the tile
			// or invokes a toast notification by clicking or tapping on the body.
			if (args.detail.arguments) {
				// TODO: If the app supports toasts, use this value from the toast payload to determine where in the app
				// to take the user in response to them invoking a toast notification.
			}
			else if (args.detail.previousExecutionState === activation.ApplicationExecutionState.terminated) {
				// TODO: This application had been suspended and was then terminated to reclaim memory.
				// To create a smooth user experience, restore application state here so that it looks like the app never stopped running.
				// Note: You may want to record the time when the app was last suspended and only restore state if they've returned after a short period.
			}
		}

		if (!args.detail.prelaunchActivated) {
			// TODO: If prelaunchActivated were true, it would mean the app was prelaunched in the background as an optimization.
			// In that case it would be suspended shortly thereafter.
			// Any long-running operations (like expensive network or disk I/O) or changes to user state which occur at launch
			// should be done here (to avoid doing them in the prelaunch case).
			// Alternatively, this work can be done in a resume or visibilitychanged handler.
		}

		if (isFirstActivation) {
			// TODO: The app was activated and had not been running. Do general startup initialization here.
			document.addEventListener("visibilitychange", onVisibilityChanged);
			args.setPromise(WinJS.UI.processAll());
		}

		isFirstActivation = false;

        //<DeclareAdControl>
		var adDiv = document.getElementById("myAd");
		var myAdControl = new MicrosoftNSJS.Advertising.AdControl(adDiv,
        {
            applicationId: "3f83fe91-d6be-434d-a0ae-7351c5a997f1",
            adUnitId: "test",
        });

		myAdControl.isAutoRefreshEnabled = false;
		myAdControl.onErrorOccurred = myAdError;
		myAdControl.onAdRefreshed = myAdRefreshed;
		myAdControl.onEngagedChanged = myAdEngagedChanged;
        //</DeclareAdControl>
	};

    //<EventHandlers>
	WinJS.Utilities.markSupportedForProcessing(
    myAdError = function (sender, msg) {
        // Add code to gracefully handle errors that occurred while serving an ad.
        // For example, you may opt to show a default experience, or reclaim the div 
        // for other purposes.
    });

	WinJS.Utilities.markSupportedForProcessing(
    myAdRefreshed = function (sender) {
        // Add code here that you wish to execute when the ad refreshes.
    });

	WinJS.Utilities.markSupportedForProcessing(
    myAdEngagedChanged = function (sender) {
        if (true == sender.isEngaged) {
            // Add code here to change behavior while the user engaged with ad.
            // For example, if the app is a game, you might pause the game.
        }
        else {
            // Add code here to update app behavior after the user is no longer
            // engaged with the ad. For example, you might unpause a game. 
        }
    });
    //</EventHandlers>

	function onVisibilityChanged(args) {
		if (!document.hidden) {
			// TODO: The app just became visible. This may be a good time to refresh the view.
		}
	}

	app.oncheckpoint = function (args) {
		// TODO: This application is about to be suspended. Save any state that needs to persist across suspensions here.
		// You might use the WinJS.Application.sessionState object, which is automatically saved and restored across suspension.
		// If you need to complete an asynchronous operation before your application is suspended, call args.setPromise().
	};

	app.start();

})();
