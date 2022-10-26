(function () {

    function snippet1() {
        // <Snippet1>
        var mediaPlayerDiv = document.createElement("div");
        document.body.appendChild(mediaPlayerDiv);

        var videoElement = document.createElement("video");
        videoElement.src = "<URL to your content>";
        mediaPlayerDiv.appendChild(videoElement);

        var mediaPlayer = new TVJS.MediaPlayer(mediaPlayerDiv);
        // </Snippet1>
    }

    // <Snippet2>
    var myMediaPlayer = document.getElementById("MediaPlayerDiv");
    var myAdScheduler = new MicrosoftNSJS.Advertising.AdScheduler(myMediaPlayer);
    // </Snippet2>

    // <Snippet3>
    myAdScheduler.requestSchedule("your application ID", "your ad unit ID").then(
      function promiseSuccessHandler(schedule) {
          // Success: play the video content with the scheduled ads.
          myMediaPlayer.tvControl.play();
      },
      function promiseErrorHandler(err) {
          // Error: play the video content without the ads.
          myMediaPlayer.tvControl.play();
      });
    // </Snippet3>

    // <Snippet4>
    myAdScheduler.requestScheduleByUrl("your URL").then(
      function promiseSuccessHandler(schedule) {
          // Success: play the video content with the scheduled ads.
          myMediaPlayer.winControl.play();
      },
      function promiseErrorHandler(evt) {
          // Error: play the video content without the ads.
          myMediaPlayer.winControl.play();
      });
    // </Snippet4>

    // <Snippet5>
    // Raised when an ad pod starts. Make the countdown timer visible.
    myAdScheduler.onPodStart = function (sender, data) {
        myCounterDiv.style.visibility = "visible";
    }

    // Raised when an ad pod ends. Hide the countdown timer.
    myAdScheduler.onPodEnd = function (sender, data) {
        myCounterDiv.style.visibility = "hidden";
    }

    // Raised when an ad is playing and indicates how many seconds remain  
    // in the current pod of ads. This is useful when the app wants to show 
    // a visual countdown until the video content resumes.
    myAdScheduler.onPodCountdown = function (sender, data) {
        myCounterText = "Content resumes in: " +
        Math.ceil(data.remainingPodTime) + " seconds.";
    }

    // Raised during each quartile of progress through the ad clip.
    myAdScheduler.onAdProgress = function (sender, data) {
    }

    // Raised when the ads and content are complete.
    myAdScheduler.onAllComplete = function (sender) {
    }

    // Raised when an error occurs during playback after successfully scheduling.
    myAdScheduler.onErrorOccurred = function (sender, data) {
    }
    // </Snippet5>

}());