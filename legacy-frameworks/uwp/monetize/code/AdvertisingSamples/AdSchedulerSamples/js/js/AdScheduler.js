
/*!
  Copyright (C) Microsoft. All rights reserved.
*/
(function () {
    "use strict";

    if (typeof (MicrosoftNSJS) === "undefined") {
        Object.defineProperty(window, "MicrosoftNSJS", { value: {}, writable: false, enumerable: true, configurable: true });
    }
    if (typeof (MicrosoftNSJS.Advertising) === "undefined") {
        Object.defineProperty(MicrosoftNSJS, "Advertising", { value: {}, writable: false, enumerable: true, configurable: true });
    }

    MicrosoftNSJS.Advertising.MediaProgress = {
        start: "start",
        firstQuartile: "firstQuartile",
        midpoint: "midpoint",
        thirdQuartile: "thirdQuartile",
        complete: "complete"
    };

    MicrosoftNSJS.Advertising.Timeslot = function (startTime) {
        /// <summary locid="MicrosoftNSJS.Advertising.Timeslot">
        ///   This class represents a point in the timeline.
        ///   It contains Breaks which will be played at the start time.
        ///   This class is used internally in the ClipScheduler and does not need to be 
        ///   instantiated when scheduling clips.
        /// </summary>
        /// <param name="startTime" type="number" locid="MicrosoftNSJS.Advertising.Timeslot_p:startTime">
        ///   The time in seconds.
        /// </param>
        /// <returns type="MicrosoftNSJS.Advertising.Timeslot" locid="MicrosoftNSJS.Advertising.Timeslot_returnValue">
        ///   A new Timeslot.
        /// </returns>

        this.startTime = startTime;
        this._breaks = [];
        this._remainingDuration = null;
    };

    MicrosoftNSJS.Advertising.Timeslot.prototype = {
        /// <field type="number" locid="MicrosoftNSJS.Advertising.Timeslot.startTime">
        ///   The number of seconds into the content timeline when this break should start.
        /// </field>
        get startTime() { return this._startTime; },
        set startTime(value) {
            this._startTime = (typeof (value) === "number" ? value : null);
        },

        /// <field type="number" locid="MicrosoftNSJS.Advertising.Timeslot.remainingDuration">
        ///   This is the total duration (in seconds) of unplayed clips in this Break.
        ///   Clips that have been played (or skipped) are not included in this value.
        ///   Call updateRemainingDuration function to manually trigger a recalculation.
        /// </field>
        get remainingDuration() {
            if (typeof (this._remainingDuration) !== "number") {
                this.updateRemainingDuration();
            }
            return this._remainingDuration;
        },

        addBreak: function (brk) {
            /// <summary locid="MicrosoftNSJS.Advertising.Timeslot.addBreak">
            ///   Adds the provided Break to this Timeslot.
            /// </summary>
            /// <param name="brk" type="MicrosoftNSJS.Advertising.Break" locid="MicrosoftNSJS.Advertising.Timeslot.addBreak_p:brk">
            ///   The Break to add to this Timeslot.
            /// </param>
            for (var ix = 0; ix < this._breaks.length; ix++) {
                if (this._breaks[ix] === brk) {
                    // break is already present, do not add again
                    return;
                }
            }

            this._breaks.push(brk);
            this._remainingDuration = null;
        },

        removeBreak: function (brk) {
            /// <summary locid="MicrosoftNSJS.Advertising.Timeslot.removeBreak">
            ///   Removes the provided Break from this Timeslot. No change is made if the Break
            ///   is not present in the Timeslot.
            /// </summary>
            /// <param name="brk" type="MicrosoftNSJS.Advertising.Break" locid="MicrosoftNSJS.Advertising.Timeslot.addBreak_p:brk">
            ///   The Break to remove from this Timeslot.
            /// </param>
            for (var ix = 0; ix < this._breaks.length; ix++) {
                if (this._breaks[ix] === brk) {
                    this._breaks.splice(ix, 1);
                    this._remainingDuration = null;
                    return;
                }
            }
        },

        getNextBreak: function () {
            /// <summary locid="MicrosoftNSJS.Advertising.Timeslot.getNextBreak">
            ///   Returns the next break with a playable Clip.
            /// </summary>
            /// <returns type="MicrosoftNSJS.Advertising.Break" locid="MicrosoftNSJS.Advertising.Timeslot.getNextBreak_returnValue">
            ///   The next Break, or null if no more Breaks.
            /// </returns>
            var nextBreak = null;
            while (this._breaks.length !== 0) {
                if (this._breaks[0].getNextClip()) {
                    return this._breaks[0];
                } else {
                    this._breaks.splice(0, 1);
                }
            }

            return nextBreak;
        },

        getBreakByPodId: function (podId) {
            /// <summary locid="MicrosoftNSJS.Advertising.Timeslot.getBreakByPodId">
            ///   Finds the break within this timeslot that has the specified podId.
            ///   Null is returned if no matching break is found.
            /// </summary>
            /// <param name="podId" type="string" locid="MicrosoftNSJS.Advertising.Timeslot.getBreakByPodId_p:podId">
            ///   An identifier to distinguish one break from another.
            /// </param>
            /// <returns type="MicrosoftNSJS.Advertising.Break" locid="MicrosoftNSJS.Advertising.Timeslot.getBreakByPodId_returnValue">
            ///   The Break with the matching podId, or null if none found.
            /// </returns>
            for (var ix = 0; ix < this._breaks.length; ix++) {
                if (this._breaks[ix].podId === podId) {
                    return this._breaks[ix];
                }
            }
            return null;
        },

        updateRemainingDuration: function () {
            /// <summary locid="MicrosoftNSJS.Advertising.Timeslot.updateRemainingDuration">
            ///   Recalculates the remainingDuration value. This may be necessary when
            ///   a clip has been modified/added/removed from a contained break.
            ///   This will be called automatically when breaks are added/removed to the timeslot.
            /// </summary>
            /// <returns type="number" locid="MicrosoftNSJS.Advertising.Timeslot.updateRemainingDuration_returnValue">
            ///   The updated remainingDuration value.
            /// </returns>
            var totalTime = 0;
            for (var ix = 0; ix < this._breaks.length; ix++) {
                var brk = this._breaks[ix];
                brk.updateRemainingDuration();
                totalTime += brk.remainingDuration;
            }
            this._remainingDuration = totalTime;
            return this._remainingDuration;
        }
    };

    MicrosoftNSJS.Advertising.ClipScheduler = function (mediaPlayerElement, options) {
        /// <summary locid="MicrosoftNSJS.Advertising.ClipScheduler">
        ///   This is a media scheduler that works with the MediaPlayer. It will insert media clips 
        ///   into the MediaPlayer's main content timeline.
        ///   If no mediaPlayerElement is provided an exception will be thrown.
        /// </summary>
        /// <param name="mediaPlayerElement" type="HTMLElement" domElement="true" locid="MicrosoftNSJS.Advertising.ClipScheduler_p:mediaPlayerElement">
        ///   The MediaPlayer element into which clips will be scheduled. Required.
        /// </param>
        /// <param name="options" type="Object" locid="MicrosoftNSJS.Advertising.ClipScheduler_p:options">
        ///   The set of options to be initially applied to the ClipScheduler.
        /// </param>
        /// <returns type="MicrosoftNSJS.Advertising.ClipScheduler" locid="MicrosoftNSJS.Advertising.ClipScheduler_returnValue">
        ///   A new ClipScheduler.
        /// </returns>

        var mpControl = (mediaPlayerElement ? mediaPlayerElement.tvControl || mediaPlayerElement.winControl : null);
        if (mpControl && mpControl.mediaElementAdapter) {
            this._mediaPlayer = mpControl;
        } else {
            var err = new Error(strings.mediaPlayerRequired);
            err.name = "MicrosoftNSJS.Advertising.ClipScheduler.mediaPlayerRequired";
            throw err;
        }

        // these hold the schedule data
        this._allClips = []; // master list of all clips
        this._timeslots = {};

        this._lastContentTime = 0;
        this._playSkippedMedia = true;
        // if the clip timeline jumps ahead by more than this amount, consider that the user has skipped ahead
        this._skipThresholdSeconds = 2;

        // threshold for determining if we've reached the end of a clip, in seconds, when playing normally and fast-forwarding
        this._endOfClipTimeDelta = 0.25;
        this._endOfClipTimeDeltaWhenFF = 1.5;

        this._isDisposed = false;

        // a media timeout of zero means don't timeout.
        this._mediaTimeout = 0;
        this._mediaTimeoutTimerId = null;

        // state related to playback of scheduled clip
        this._priorIsPauseAllowed = true;
        this._priorIsSeekAllowed = true;
        this._priorIsThumbnailEnabled = true;
        this._priorDisplayValue = null;
        this._mediaElement = null;
        this._currentBreak = null;
        this._currentTimeslot = null;
        this._currentClip = null;
        this._isInMediaBreak = false;
        this._firedEvents = {};
        this._transportControlsEnabled = true;
        this._lastClipTime = 0;
        this._maxClipTime = 0;
        this._userSkippedAhead = false;
        this._previousMarkers = null;
        this._cachedVideoElement = null;

        // event handlers
        this._onMediaProgress = null;
        this._onMediaBreakStart = null;
        this._onMediaBreakEnd = null;
        this._onAllComplete = null;
        this._onMediaError = null;
        this._onBreakCountdown = null;

        // our internal event handlers, which we keep handle on for cleanup
        this._clipEventSubscriptions = [];

        this._addContentEventListeners(this._mediaPlayer.mediaElementAdapter.mediaElement);

        mediaPlayerElement.addEventListener("mediaelementchanged", this._mediaElementChangedHandler.bind(this));

        this._setOptions(this, options);
    };

    MicrosoftNSJS.Advertising.ClipScheduler.prototype = {
        /// <field type="Boolean" locid="MicrosoftNSJS.Advertising.ClipScheduler.playSkippedMedia">
        ///   Boolean indicating whether scheduled media will play if user skips ahead 
        ///   to a point past a scheduled start time.
        ///      true = Skipped media will be played. If multiple scheduled breaks have 
        ///             been skipped, only the last time slot will play. (default)
        ///      false = Skipped media will not play. 
        /// </field>
        get playSkippedMedia() { return this._playSkippedMedia; },
        set playSkippedMedia(value) { this._playSkippedMedia = value; },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.ClipScheduler.onMediaProgress">
        ///   This event is fired when playback reaches timeline checkpoints.
        ///   Event parameters:
        ///       the ClipScheduler which fired the event,
        ///       JSON: 
        ///         {
        ///             progress: a MediaProgress enum,
        ///             clip: the Clip representing the video being played
        ///         }
        ///   See also: MicrosoftNSJS.Advertising.Clip
        ///   See also: MicrosoftNSJS.Advertising.MediaProgress enumeration
        /// </field>
        get onMediaProgress() { return this._onMediaProgress; },
        set onMediaProgress(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onMediaProgress = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.ClipScheduler.onMediaBreakStart">
        ///   This event is fired when a media break starts. Breaks consist of one or more media items
        ///   scheduled to start at the same time.
        ///   Event parameters:
        ///       the ClipScheduler which fired the event,
        ///       the Break instance which is starting
        ///   See also: MicrosoftNSJS.Advertising.Break
        /// </field>
        get onMediaBreakStart() { return this._onMediaBreakStart; },
        set onMediaBreakStart(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onMediaBreakStart = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.ClipScheduler.onMediaBreakEnd">
        ///   This event is fired when a media break ends. Breaks consist of one or more media items
        ///   scheduled to start at the same time.
        ///   Event parameters are:
        ///       the ClipScheduler firing the event
        ///       the Break instance which is starting
        ///   See also: MicrosoftNSJS.Advertising.Break
        /// </field>
        get onMediaBreakEnd() { return this._onMediaBreakEnd; },
        set onMediaBreakEnd(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onMediaBreakEnd = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.ClipScheduler.onMediaError">
        ///   This event is fired when an error occurs during the playback of a scheduled clip.
        ///   Event parameters:
        ///       the ClipScheduler which fired the event,
        ///       JSON: 
        ///         {
        ///             message: string description of error,
        ///             errorCode: a MediaProgress enum,
        ///             clip: the Clip representing the video being played
        ///         }
        ///   See also: MicrosoftNSJS.Advertising.AdErrorCode enumeration
        ///   See also: MicrosoftNSJS.Advertising.Clip
        /// </field>
        get onMediaError() { return this._onMediaError; },
        set onMediaError(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onMediaError = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.ClipScheduler.onAllComplete">
        ///   This event is fired when the main content reaches the end and any scheduled post-roll 
        ///   media are also ended. If no post-rolls, the event fires when the main content ends.
        ///   Event parameter: 
        ///       the ClipScheduler firing the event
        /// </field>
        get onAllComplete() { return this._onAllComplete; },
        set onAllComplete(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onAllComplete = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.ClipScheduler.onBreakCountdown">
        ///   This event is fired when a clip is playing and indicates how much time remains in 
        ///   the current break. 
        ///   Event parameters: 
        ///       the ClipScheduler firing the event,
        ///       JSON:
        ///         {
        ///           remainingAdTime: number of seconds left for the current ad, 
        ///           remainingBreakTime: number of seconds left for the current pod
        ///         }
        /// </field>
        get onBreakCountdown() { return this._onBreakCountdown; },
        set onBreakCountdown(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onBreakCountdown = value;
            }
        },

        /// <field type="Number" locid="MicrosoftNSJS.Advertising.ClipScheduler.mediaTimeout">
        ///   The number of milliseconds the media must be playable in, 0 means no 
        ///   timeout. The default is no timeout.
        /// </field>
        get mediaTimeout() { return this._mediaTimeout; },
        set mediaTimeout(value) {
            if (this._mediaTimeout !== value) {
                this._mediaTimeout = value;
            }
        },

        scheduleMedia: function (clip) {
            /// <summary locid="MicrosoftNSJS.Advertising.ClipScheduler.scheduleMedia">
            /// Adds a media clip to the schedule so that it will play when the main content reaches the specified
            /// point in the timeline.
            ///
            /// An exception will be thrown if clip is not valid.
            /// </summary>
            /// <param name="clip" type="MicrosoftNSJS.Advertising.Clip" locid="MicrosoftNSJS.Advertising.ClipScheduler.scheduleMedia_p:clip">
            ///   The Clip to insert into the content timeline.
            ///   See also: MicrosoftNSJS.Advertising.Clip
            /// </param>

            if (this._isDisposed || this._mediaPlayer._disposed) {
                return;
            }

            if (typeof (clip) !== "object" || clip === null) {
                var err = new Error(strings.scheduleDataNotAnObject);
                err.name = "MicrosoftNSJS.Advertising.ClipScheduler.scheduleDataNotAnObject";
                throw err;
            }

            if (typeof (this._allClips) !== "object" || this._allClips === null) {
                this._allClips = [];
            }

            // We used to take in an arbitrary JSON object rather than Clip class, so if JSON is
            // passed in then wrap it in a Clip (for backward compatibility).
            if (!MicrosoftNSJS.Advertising.Clip.prototype.isPrototypeOf(clip)) {
                clip = new MicrosoftNSJS.Advertising.Clip(clip);
            }

            clip.validate();

            this._allClips.push(clip);

            var mainMediaElement = this._getMainMediaElement();
            clip.calculateStartTime(mainMediaElement ? mainMediaElement.duration : NaN);

            this._addClipToBreak(clip);

            // If the main content is playing, check to see if we should start a clip right away, since preroll (time 0) 
            // should play immediately even if the main content isn't ready to start (e.g. hasn't buffered).
            // Also if there is no main content 'src', we should start any time 0 scheduled media immediately. 
            if (!this._currentClip &&
                (!mainMediaElement || mainMediaElement.src === "" || !mainMediaElement.paused)) {
                this._mainMediaTimeUpdate();
            }
        },

        dispose: function () {
            /// <summary locid="MicrosoftNSJS.Advertising.ClipScheduler.dispose">
            ///   A call to this method directs the <c>ClipScheduler</c> to release resources and unregister listeners.
            ///   If a scheduled clip is currently playing, it will be stopped and removed from the MediaPlayer.
            /// </summary>
            /// <remarks>
            ///   The <c>ClipScheduler</c> will not function after this is called.
            /// </remarks>

            if (!this._mediaPlayer) {
                // we must have been disposed already
                return;
            }

            this._clearMediaTimeout();

            if (this._mediaElement) {
                this._removeCurrentClip();
            }

            // Depending on when dispose is called, we may have paused the main content but not created/swapped 
            // media elements. Resume the main content here, whether _mediaElement is defined or not.
            if (this._isInMediaBreak) {
                this._resumeMainMedia();
            }

            // remove only the ad markers, leave external markers in place.
            this._removeAdMarkersFromTimeline();

            if (this._mediaPlayer && this._mediaPlayer.mediaElementAdapter) {
                this._removeContentEventListeners(this._mediaPlayer.mediaElementAdapter.mediaElement);
            }

            // clear out internal variables
            this._allClips = null;
            this._timeslots = null;
            this._priorDisplayValue = null;
            this._mediaElement = null;
            this._currentBreak = null;
            this._currentTimeslot = null;
            this._currentClip = null;
            this._firedEvents = null;
            this._mediaPlayer = null;
            this._previousMarkers = null;

            // event handlers
            this._onMediaProgress = null;
            this._onMediaBreakStart = null;
            this._onMediaBreakEnd = null;
            this._onAllComplete = null;

            this._cachedVideoElement = null;

            this._isDisposed = true;
        },

        _getMainMediaElement: function () {
            var elem = null;
            if (this._currentClip) {
                // Clip is currently playing, so look up main content element from DOM.
                elem = document.querySelector(".uac-main-content");
            } else {
                // Otherwise get element from MediaPlayer.
                elem = this._mediaPlayer.mediaElementAdapter.mediaElement;
            }
            return elem;
        },

        _addClipToBreak: function (clip) {
            if (clip === null || typeof (clip.startTime) !== "number") {
                return null;
            }

            var timeslot = this._getOrCreateTimeslot(clip.startTime);

            // See if there is already a break for this startTime
            var brk = timeslot.getBreakByPodId(clip.podId);
            if (!brk) {
                // No existing break is defined with this start time and podId, so create a new break.
                brk = this._createBreak(clip);
                timeslot.addBreak(brk);
            } else {
                brk.addClip(clip);
            }

            return brk;
        },

        _getOrCreateTimeslot: function (startTime) {
            var timeslot = null;
            if (!(startTime in this._timeslots)) {
                this._addMarkerToTimeline(startTime);
                timeslot = new MicrosoftNSJS.Advertising.Timeslot(startTime);
                this._timeslots[startTime] = timeslot;
            } else {
                timeslot = this._timeslots[startTime];
            }
            return timeslot;
        },

        _createBreak: function (clip) {
            var newBreak = new MicrosoftNSJS.Advertising.Break({ startTime: clip.startTime, podId: clip.podId });
            newBreak.addClip(clip);
            return newBreak;
        },

        _addMarkersToTimeline: function () {
            // add all saved non-scheduler markers back to the timeline
            if (this._previousMarkers !== null) {
                for (var i = 0; i < this._previousMarkers.length; i++) {
                    this._mediaPlayer.addMarker(this._previousMarkers[i].time, this._previousMarkers[i].type, this._previousMarkers[i].data, this._previousMarkers[i].extraClass);
                }
                this._previousMarkers = null;
            }

            // Add markers to the MTC timeline to indicate when ads will play.
            if (this._timeslots) {
                for (var startTime in this._timeslots) {
                    // Keys in the object return as strings, 
                    // making this the lookups fail, parseFloat
                    // converts back to numbers.
                    this._addMarkerToTimeline(parseFloat(startTime));
                }
            }
        },

        _addMarkerToTimeline: function (startTime) {
            if (typeof (startTime) === "number" && this._mediaPlayer && !this._mediaPlayer._disposed && !this._currentClip) {
                // Do not add marker if one already exists, e.g. a chapter marker.
                var marker = this._getMarkerAtTime(startTime);
                if (marker === null) {
                    if (typeof (TVJS) !== "undefined") {
                        this._mediaPlayer.addMarker(startTime, TVJS.MarkerType.advertisement, {}, "");
                    } else {
                        this._mediaPlayer.addMarker(startTime, XboxJS.UI.MarkerType.advertisement, {}, "");
                    }
                }
            }
        },

        _removeMarkersFromTimeline: function () {
            this._removeAdMarkersFromTimeline();

            // next, if there are any markers still in the timeline, remove and also store 
            // them so they can be restored when the main content resumes playing.
            if (this._mediaPlayer && this._mediaPlayer.markers && this._mediaPlayer.markers.length > 0) {
                this._previousMarkers = this._mediaPlayer.markers.slice();
                this._mediaPlayer.markers.length = 0;
            }
        },

        _removeAdMarkersFromTimeline: function () {
            // Iterate through breaks and remove all timeline markers.
            if (this._timeslots) {
                for (var startTime in this._timeslots) {
                    // Keys in the object return as strings, 
                    // making this the lookups fail, parseFloat
                    // converts back to numbers.
                    this._removeMarkerFromTimeline(parseFloat(startTime));
                }
            }
        },

        _removeMarkerFromTimeline: function (startTime) {
            if (typeof (startTime) === "number" && this._mediaPlayer && !this._mediaPlayer._disposed && !this._currentClip) {
                var adMarkerType = (typeof (TVJS) !== "undefined" ? TVJS.MarkerType.advertisement : XboxJS.UI.MarkerType.advertisement);
                var marker = this._getMarkerAtTime(startTime);
                if (marker && (marker.markerType || marker.type)== adMarkerType) {
                    this._mediaPlayer.removeMarker(startTime);
                }
            }
        },

        _getMarkerAtTime: function (time) {
            if (this._mediaPlayer && this._mediaPlayer.markers) {
                var markers = this._mediaPlayer.markers;
                for (var ix = 0; ix < markers.length; ix++) {
                    if (markers[ix].time === time) {
                        return markers[ix];
                    }
                }
            }
            return null;
        },

        _addContentEventListeners: function (mediaElement) {
            if (mediaElement) {
                this._eventHandlerMainTimeUpdate = this._mainMediaTimeUpdate.bind(this);
                mediaElement.addEventListener("timeupdate", this._eventHandlerMainTimeUpdate);

                this._eventHandlerDurationChange = this._contentDurationChangeHandler.bind(this);
                mediaElement.addEventListener("durationchange", this._eventHandlerDurationChange);

                this._eventHandlerSrcChange = this._contentSrcChangeHandler.bind(this);
                mediaElement.addEventListener("DOMAttrModified", this._eventHandlerSrcChange);

                this._eventHandlerVolumeChange = this._contentVolumeChangeHandler.bind(this);
                mediaElement.addEventListener("volumechange", this._eventHandlerVolumeChange);
            }
        },

        _removeContentEventListeners: function (mediaElement) {
            if (mediaElement) {
                if (this._eventHandlerDurationChange) {
                    mediaElement.removeEventListener("durationchange", this._eventHandlerDurationChange);
                }
                if (this._eventHandlerMainTimeUpdate) {
                    mediaElement.removeEventListener("timeupdate", this._eventHandlerMainTimeUpdate);
                }
                if (this._eventHandlerSrcChange) {
                    mediaElement.removeEventListener("DOMAttrModified", this._eventHandlerSrcChange);
                }
                if (this._eventHandlerVolumeChange) {
                    mediaElement.removeEventListener("volumechange", this._eventHandlerVolumeChange);
                }
            }

            this._eventHandlerDurationChange = null;
            this._eventHandlerMainTimeUpdate = null;
            this._eventHandlerSrcChange = null;
            this._eventHandlerVolumeChange = null;
        },

        _mediaElementChangedHandler: function (args) {
            // This is triggered when the MediaPlayer's mediaElement is changed.
            // Since we trigger this event ourselves when playing a clip, we set a flag to know when to ignore this event.
            if (!this._ignoreMediaElementChange && !this._isDisposed) {
                if (this._currentClip) {
                    // App should never remove our media from the player. Better to dispose of us and let us remove clip, even while clip is playing.
                    this._fireMediaErrorEvent(this, {
                        message: strings.contentChangedDuringClipPlayback,
                        errorCode: MicrosoftNSJS.Advertising.AdErrorCode.other,
                        clip: this._currentClip
                    });
                } else {
                    // Main content was changed. This can happen when user selects different closed-captions feed.
                    if (this._mediaPlayer.mediaElementAdapter.mediaElement) {
                        // Duration of media may have changed, so we need to recalculate clip start times. This will re-add markers to timeline.
                        var contentDuration = this._mediaPlayer.mediaElementAdapter.mediaElement.duration;
                        this._recalculateStartTimes(contentDuration);
                        // Add event listeners to new element.
                        this._addContentEventListeners(this._mediaPlayer.mediaElementAdapter.mediaElement);
                    }
                }
            }
        },

        _recalculateStartTimes: function (contentDuration) {
            // If the content media element changes, we need to recalculate start times since timeOffset may be a percentage.

            // Remove any existing clip markers.
            this._removeAdMarkersFromTimeline();

            this._timeslots = {};
            for (var ix = 0; ix < this._allClips.length; ix++) {
                var clip = this._allClips[ix];
                if (!clip.isPlayed && !clip.isSkipped) {
                    clip.calculateStartTime(contentDuration);
                    this._addClipToBreak(clip);
                }
            }
        },

        _contentSrcChangeHandler: function (args) {
            // When the content element's 'src' changes, MediaPlayer will remove ad markers. Restore them.
            if (args.attrName === "src" && !this._currentClip) {
                setImmediate(this._addMarkersToTimeline.bind(this));
            }
        },

        _contentVolumeChangeHandler: function (args) {
            if (this._mediaElement && args.srcElement) {
                this._mediaElement.muted = args.srcElement.muted;
                this._mediaElement.volume = args.srcElement.volume;
            }
        },

        _contentDurationChangeHandler: function (sender, args) {
            if (this._eventHandlerDurationChange) {
                sender.srcElement.removeEventListener("durationchange", this._eventHandlerDurationChange);
                this._eventHandlerDurationChange = null;
            }

            if (!this._isDisposed) {
                // determine the start time for any % based timeOffsets, now that we know the duration
                this._recalculateStartTimes(sender.srcElement.duration);
            }
        },

        _mainMediaTimeUpdate: function (sender) {
            // do not check for an ad if there is already one playing
            if (!this._isDisposed && !this._currentClip && !this._mediaPlayer._disposed) {
                var currentTime = (sender && sender.srcElement ? sender.srcElement.currentTime : 0);

                this._mainMediaEnded = sender && sender.srcElement && (currentTime >= sender.srcElement.duration);

                var timeslot = this._getTimeslotForCurrentTime(currentTime);
                if (timeslot) {
                    var clip = this._prepareNextClip(timeslot);
                    if (clip !== null) {
                        this._pauseMainMedia();
                        this._playClip(clip);
                    }
                }

                // If main content has reached end and there is no post-roll break, fire allComplete event.
                if (this._mainMediaEnded && !this._currentClip) {
                    this._fireAllCompleteEvent();
                }
            }
        },

        // Locate the appropriate timeslot for a specific time.
        _getTimeslotForCurrentTime: function (currentTime) {
            if (this._isDisposed) {
                return null;
            }

            var timeslot = null;

            // Check if the user has skipped ahead on the timeline, since we will not show skipped clips
            // if _playSkippedMedia==false. We use a value of 1 second when checking for seeking
            // since timeUpdate fires every 250ms or so.
            if (this._playSkippedMedia || (currentTime - this._lastContentTime <= 1)) {
                // Find the timeslot closest to the current time.
                for (var ix in this._timeslots) {
                    var time = parseFloat(ix);
                    // Find any timeslot within the time range.
                    if ((this._lastContentTime < time && time <= currentTime) ||
                    (this._lastContentTime === 0 && time === 0)) {
                        // Select the last matching slot.
                        if (!timeslot || timeslot.startTime < time) {
                            timeslot = this._timeslots[time];
                        }
                    }
                }
            }

            if (timeslot) {
                // Calculate the duration the first time a timeslot is retrieved for playback
                // (to avoid repeatedly calculating it).
                timeslot.updateRemainingDuration();
            }

            this._lastContentTime = currentTime;

            return timeslot;
        },

        // Determines if there is a clip to play in the provided timeslot. 
        // If so, sets internal state variables and returns the clip.
        // If not, the timeslot and its marker are removed.
        _prepareNextClip: function (timeslot) {
            var clip = null;
            if (timeslot) {
                var brk = timeslot.getNextBreak();
                if (brk) {
                    clip = brk.getNextClip();
                } else {
                    this._removeTimeslot(timeslot);
                }
            }

            if (clip !== null) {
                this._currentBreak = brk;
                this._currentTimeslot = timeslot;
            } else {
                this._currentBreak = null;
                this._currentTimeslot = null;
            }

            return clip;
        },

        _playClip: function (clip) {
            if (typeof (clip) !== "object" || clip === null || this._isDisposed || this._mediaPlayer._disposed) {
                return;
            }

            if (typeof (this._currentClip) === "object" && this._currentClip !== null) {
                // we are already playing a scheduled clip
                return;
            }

            // Remember what events we have fired for this clip. We only fire progress events once each, even if rewound.
            this._firedEvents = {};

            if (!this._isInMediaBreak) {
                this._isInMediaBreak = true;
                this._fireMediaBreakStartEvent(this._currentBreak);

                // We just allowed external code to execute in event handler, so verify we haven't been disposed.
                if (this._isDisposed || this._mediaPlayer._disposed) {
                    return;
                }
            }

            // swap out the main content
            var mainMediaElement = this._mediaPlayer.mediaElementAdapter.mediaElement;
            if (mainMediaElement) {
                // Add a class to the element so we can retrieve it after ad completes.
                this._addClassToElement(mainMediaElement, "uac-main-content");
            }

            this._removeMarkersFromTimeline();

            this._currentClip = clip;
            this._lastClipTime = 0;
            this._maxClipTime = 0;
            this._userSkippedAhead = false;

            this._mediaElement = this._createMediaElement(clip.type, clip.url);

            this._mediaElement.canHaveHTML = true;

            //document.body.appendChild(this._mediaElement);

            // Track the timeline progress so we can fire progress events.
            this._addClipEventListener(this._mediaElement, "timeupdate", this._clipTimeUpdate.bind(this));
            this._addClipEventListener(this._mediaElement, "ended", this._clipEndedHandler.bind(this));
            this._addClipEventListener(this._mediaElement, "error", this._clipErrorOccurred.bind(this));
            this._addClipEventListener(this._mediaElement, "durationchange", this._clipDurationChangeHandler.bind(this));

            this._ignoreMediaElementChange = true;
            this._mediaPlayer.mediaElementAdapter.mediaElement = this._mediaElement;
            this._ignoreMediaElementChange = false;

            // Some of the controls (e.g. timeline) do not update when the controls are visible and the media element is changed.
            this._mediaPlayer.hideControls();

            // disable buttons unless skipOffset==0
            this._mediaPlayer.mediaElementAdapter.isSeekAllowed = (clip.skipOffsetParsed === 0);

            // Hide the main media element and remember its display setting.
            if (mainMediaElement) {
                this._priorDisplayValue = mainMediaElement.style.display;
                mainMediaElement.style.display = "none";

                // Set initial values for mute and volume.
                this._mediaElement.muted = mainMediaElement.muted;
                this._mediaElement.volume = mainMediaElement.volume;
            }

            if (this.win8Overlay) {
                this.win8Overlay.style.display = "block";
            }

            this._startMediaTimeout();
        },

        // This is the event handler for the "ended" event.
        _clipEndedHandler: function (evt) {
            this._clipEnded(false);
        },

        _clipEnded: function (errorOccurred) {
            this._clearMediaTimeout();

            if (this._currentClip) {
                var currentClip = this._currentClip;

                this._removeCurrentClip();
                if (this._currentTimeslot) {
                    this._currentTimeslot.updateRemainingDuration();
                }

                if (!this._userSkippedAhead && !errorOccurred) {
                    this._fireProgressEvent(currentClip, MicrosoftNSJS.Advertising.MediaProgress.complete);
                }

                // Check for additional clips in this same break.
                var clip = (this._currentBreak ? this._currentBreak.getNextClip() : null);

                if (clip === null && !this._isDisposed) {
                    // If we've reached the end of a break, check if there are more breaks in this timeslot.
                    this._fireMediaBreakEndEvent(this._currentBreak);
                    if (this._currentTimeslot) {
                        this._currentTimeslot.removeBreak(this._currentBreak);
                    }
                    this._currentBreak = null;
                    this._isInMediaBreak = false;

                    if (this._currentTimeslot) {
                        clip = this._prepareNextClip(this._currentTimeslot);
                    }
                }

                if (clip !== null) {
                    // There is more media to show.
                    this._playClip(clip);
                } else {
                    this._resumeMainMedia();
                    this._checkForAllComplete();
                }
            }
        },

        _removeTimeslot: function (timeslot) {
            this._removeMarkerFromTimeline(timeslot.startTime);
            delete this._timeslots[timeslot.startTime];
        },

        _checkForAllComplete: function () {
            // If there is no clip playing, and no main content (or it has ended) we should fire the allComplete event.
            if (this._currentClip || this._isDisposed) {
                return;
            }

            var mainElem = (this._mediaPlayer && this._mediaPlayer.mediaElementAdapter ? this._mediaPlayer.mediaElementAdapter.mediaElement : null);
            if (!mainElem || mainElem.src === "" || this._mainMediaEnded) {
                this._fireAllCompleteEvent();
            }
        },

        _startMediaTimeout: function () {
            if (this._mediaTimeout !== 0 && !this._isDisposed && !this._mediaPlayer._disposed) {
                this._clearMediaTimeout();
                var expectedMediaElement = this._mediaPlayer.mediaElementAdapter.mediaElement;
                this._mediaTimeoutTimerId = setTimeout(function () {
                    this._mediaTimeoutTimerId = null;
                    if (this._mediaPlayer &&
                        this._mediaPlayer.mediaElementAdapter &&
                        this._mediaPlayer.mediaElementAdapter.mediaElement &&
                        expectedMediaElement === this._mediaPlayer.mediaElementAdapter.mediaElement &&
                        this._mediaPlayer.mediaElementAdapter.mediaElement.currentTime === 0) {

                        this._clipErrorOccurred(
                            {
                                errorMessage: strings.mediaTimeout,
                                errorCode: MicrosoftNSJS.Advertising.AdErrorCode.networkConnectionFailure
                            });
                    }
                    expectedMediaElement = null;
                }.bind(this), this._mediaTimeout);
            }
        },

        _clearMediaTimeout: function () {
            if (this._mediaTimeoutTimerId && typeof (this._mediaTimeoutTimerId) === "number") {
                clearTimeout(this._mediaTimeoutTimerId);
                this._mediaTimeoutTimerId = null;
            }
        },

        _clipErrorOccurred: function (evt) {
            var errorMessage = null;

            if (evt.target && evt.target.error) {
                // We are handling a MediaError event, so pull out the specific problem.
                var mediaErr = evt.target.error;
                switch (mediaErr.code) {
                    case MediaError.MEDIA_ERR_ABORTED:
                        errorMessage = "media playback aborted";
                        break;
                    case MediaError.MEDIA_ERR_DECODE:
                        errorMessage = "media decode failure";
                        break;
                    case MediaError.MEDIA_ERR_NETWORK:
                        errorMessage = "media download failure";
                        break;
                    case MediaError.MEDIA_ERR_SRC_NOT_SUPPORTED:
                        errorMessage = "media src not supported";
                        break;
                    default:
                        errorMessage = "media error code: " + mediaErr.code;
                        break;
                }
            } else {
                // We are called from the media timeout logic above. Use the parameter's errorMessage.
                errorMessage = evt.errorMessage ? evt.errorMessage : "An error occurred during media playback.";
            }

            var errorCode = evt.errorCode ? evt.errorCode : MicrosoftNSJS.Advertising.AdErrorCode.other;
            this._fireMediaErrorEvent(this,
                {
                    message: errorMessage,
                    errorCode: errorCode,
                    clip: this._currentClip
                });

            // remove the current ad which had the error
            this._clipEnded(true);
        },

        // Clean up the clip media element and restore the MediaPlayer to the state it was in before we started.
        _removeCurrentClip: function () {
            if (!this._mediaElement) {
                return;
            }

            this._removeAllClipEventListeners(this._mediaElement);

            if (this._mediaElement.parentNode) {
                this._mediaElement.parentNode.removeChild(this._mediaElement);
            }
            this._cacheMediaElement(this._mediaElement);
            this._mediaElement = null;

            this._currentClip.isPlayed = true;
            this._currentClip = null;

            // Restore visibility of main media element.
            var mainMediaElement = document.querySelector(".uac-main-content");
            if (mainMediaElement) {
                this._removeClassFromElement(mainMediaElement, "uac-main-content");
                mainMediaElement.style.display = this._priorDisplayValue;
                this._priorDisplayValue = null;
            }

            // Restore the original media element in the MediaPlayer if it hasn't been disposed.
            if (this._mediaPlayer.mediaElementAdapter) {
                this._ignoreMediaElementChange = true;
                this._mediaPlayer.mediaElementAdapter.mediaElement = mainMediaElement;
                this._ignoreMediaElementChange = false;
            }
        },

        // XboxJS uses "win-mediaplayer-closedcaptionsbutton" class, while TVJS uses "tv-mediaplayer-closedcaptionsbutton". Try both.
        _getClosedCaptionsButton: function () {
            var captionsButton = this._mediaPlayer.element.querySelector(".tv-mediaplayer-closedcaptionsbutton");
            if (!captionsButton) {
                captionsButton = this._mediaPlayer.element.querySelector(".win-mediaplayer-closedcaptionsbutton");
            }
            return captionsButton;
        },

        _pauseMainMedia: function () {
            if (this._mediaPlayer && this._mediaPlayer.mediaElementAdapter) {
                var adapter = this._mediaPlayer.mediaElementAdapter;

                this._priorIsPauseAllowed = adapter.isPauseAllowed;
                this._priorIsSeekAllowed = adapter.isSeekAllowed;
                this._priorIsThumbnailEnabled = this._mediaPlayer.isThumbnailEnabled;

                adapter.isPauseAllowed = true;
                adapter.isSeekAllowed = false;
                this._mediaPlayer.isThumbnailEnabled = false;

                if (this._mediaPlayer.element) {
                    var captionsButton = this._getClosedCaptionsButton();
                    if (captionsButton) {
                        this._priorCaptionsButtonDisabled = captionsButton.disabled;
                        captionsButton.disabled = true;
                    }
                }

                this._mediaPlayer.pause();
            }
        },

        _resumeMainMedia: function () {
            if (!this._isDisposed && this._mediaPlayer && this._mediaPlayer.mediaElementAdapter) {
                this._addMarkersToTimeline();

                if (this.win8Overlay) {
                    this.win8Overlay.style.display = "none";
                }

                if (this._mediaPlayer.element && typeof (this._priorCaptionsButtonDisabled) === "boolean") {
                    var captionsButton = this._getClosedCaptionsButton();
                    if (captionsButton) {
                        captionsButton.disabled = this._priorCaptionsButtonDisabled;
                        this._priorCaptionsButtonDisabled = null;
                    }
                }

                this._mediaPlayer.isThumbnailEnabled = this._priorIsThumbnailEnabled;

                var adapter = this._mediaPlayer.mediaElementAdapter;
                if (adapter) {
                    adapter.isPlayAllowed = true;
                    // Resume the main media unless we had reached the end (in which case play() would restart).
                    if (!this._mainMediaEnded) {
                        this._mediaPlayer.play();
                    }

                    if (typeof (this._priorIsSeekAllowed) === "boolean") {
                        adapter.isPauseAllowed = this._priorIsPauseAllowed;
                        adapter.isSeekAllowed = this._priorIsSeekAllowed;
                    }
                }
            }
        },

        _clipDurationChangeHandler: function (sender, args) {
            if (this._currentClip && !this._isDisposed && !this._mediaPlayer._disposed) {
                this._currentClip.duration = this._mediaPlayer.mediaElementAdapter.mediaElement.duration;
                this._currentTimeslot.updateRemainingDuration();
            }
        },

        _clipTimeUpdate: function () {
            if (this._currentClip && !this._isDisposed && this._mediaPlayer.mediaElementAdapter) {
                var currentTime = this._mediaPlayer.mediaElementAdapter.mediaElement.currentTime;

                this._fireCountdownEvent();

                // Event handler could have disposed us. Check again.
                if (this._isDisposed) {
                    return;
                }

                // Check if the user has skipped ahead in the ad. If so we will stop firing progress events.
                // A user can skip around in the content prior to what they've seen but if they skip over
                // clip content they haven't seen by an amount greater than skipThresholdSeconds then we 
                // detect that they've 'skipped ahead' of some advertisement content.
                if (!this._userSkippedAhead && (currentTime - this._maxClipTime > this._skipThresholdSeconds)) {
                    this._userSkippedAhead = true;
                }

                // If time has reached the skipOffset, we need to re-enable controls.
                if (this._mediaPlayer.mediaElementAdapter
                    && !this._mediaPlayer.mediaElementAdapter.isSeekAllowed
                    && typeof (this._currentClip.skipOffsetParsed) === "number" && currentTime > this._currentClip.skipOffsetParsed) {
                    this._mediaPlayer.mediaElementAdapter.isSeekAllowed = true;
                }

                // If duration has not been set, try to determine it now.
                if (!this._currentClip.duration || this._currentClip.duration === 0) {
                    if (this._mediaElement && this._mediaElement.duration > 0) {
                        this._currentClip.duration = this._mediaElement.duration;
                    }
                }

                // If we still don't know the duration, return now. The rest of this function requires it.
                if (this._currentClip.duration && this._currentClip.duration !== 0) {
                    if (!this._userSkippedAhead) {
                        // only fire progress events if the user has not skipped ahead
                        this._checkProgressEvents(currentTime, this._currentClip.duration);
                    }

                    // The "ended" event does not always fire, so we will also check if currentTime reaches the duration.
                    // We use _endOfClipTimeDelta to account for bug 1437986 filed on the MediaPlayer team. This bug is related
                    // to ended events not firing and currentTime != duration when content fast-forwards to end of clip.
                    if (this._currentClip) {
                        // We have two checks here:
                        // 1) if currentTime is very close to duration, or
                        // 2) if currentTime is pretty close to duration and user has been fast-forwarding
                        if ((currentTime + this._endOfClipTimeDelta >= this._currentClip.duration) ||
                            (currentTime + this._endOfClipTimeDeltaWhenFF >= this._currentClip.duration && (currentTime - this._lastClipTime > this._skipThresholdSeconds))) {
                            this._clipEnded(false);
                        }
                    }
                }

                if (currentTime > this._maxClipTime) {
                    this._maxClipTime = currentTime;
                }

                this._lastClipTime = currentTime;
            }
        },

        _checkProgressEvents: function (currentTime, duration) {
            this._fireEventIfTimeReached(currentTime, 0, MicrosoftNSJS.Advertising.MediaProgress.start);
            this._fireEventIfTimeReached(currentTime, 0.25 * duration, MicrosoftNSJS.Advertising.MediaProgress.firstQuartile);
            this._fireEventIfTimeReached(currentTime, 0.5 * duration, MicrosoftNSJS.Advertising.MediaProgress.midpoint);
            this._fireEventIfTimeReached(currentTime, 0.75 * duration, MicrosoftNSJS.Advertising.MediaProgress.thirdQuartile);
            // The complete event is fired in the _clipEnded function. No need to check here.
        },

        _fireEventIfTimeReached: function (currentTime, targetTime, progressEnum) {
            if (this._lastClipTime <= targetTime && targetTime <= currentTime) {
                this._fireProgressEvent(this._currentClip, progressEnum);
            }
        },

        _createMediaElement: function (elemType, mediaUrl) {
            var elem = null;
            if (elemType.toLowerCase() === "video" && this._cachedVideoElement) {
                elem = this._cachedVideoElement;
                this._cachedVideoElement = null;
            } else {
                elem = document.createElement(elemType);
            }
            elem.id = "_msAdElement";
            elem.src = mediaUrl;
            elem.autoplay = true;
            elem.controls = false;
            return elem;
        },

        _cacheMediaElement: function (elem) {
            // We reuse a single video element to prevent problems with the underlying media engine.
             if (elem && elem.nodeName === "VIDEO") {
                this._cachedVideoElement = elem;
                // We need to update the 'src' for the element to release the memory associated with the video stream.
                this._cachedVideoElement.removeAttribute("src");
            }
        },

        _fireProgressEvent: function (clip, progressEnum) {
            if (!this._isDisposed) {
                if (this._firedEvents[progressEnum]) {
                    return;
                }

                this._firedEvents[progressEnum] = true;

                if (typeof (this._onMediaProgress) === "function") {
                    this._onMediaProgress(this, { clip: clip, progress: progressEnum });
                }
            }
        },

        _fireCountdownEvent: function () {
            if (typeof (this._onBreakCountdown) === "function") {
                var currentAdTime = this._mediaElement.currentTime;
                var args = {
                    remainingAdTime: Math.round(100 * (this._currentClip.duration - currentAdTime)) / 100,
                    remainingBreakTime: Math.round(100 * (this._currentTimeslot.remainingDuration - currentAdTime)) / 100
                };
                this._onBreakCountdown(this, args);
            }
        },

        _fireMediaBreakStartEvent: function (brk) {
            if (typeof (this._onMediaBreakStart) === "function") {
                this._onMediaBreakStart(this, brk);
            }
        },

        _fireMediaBreakEndEvent: function (brk) {
            if (typeof (this._onMediaBreakEnd) === "function") {
                this._onMediaBreakEnd(this, brk);
            }
        },

        _fireMediaErrorEvent: function (sender, errorDetails) {
            // errorDetails is a JSON object containing: message, errorCode, clip
            if (typeof (this._onMediaError) === "function") {
                this._onMediaError(this, errorDetails);
            }
        },

        _fireAllCompleteEvent: function () {
            if (!this._firedEvents["allComplete"]) {
                this._firedEvents["allComplete"] = true;

                if (typeof (this._onAllComplete) === "function") {
                    this._onAllComplete(this);
                }
            }
        },

        // Add a media event listener for a scheduled clip. This method keeps a list of the handlers so they can be unsubscribed later.
        _addClipEventListener: function (mediaElement, eventName, handler) {
            if (mediaElement) {
                mediaElement.addEventListener(eventName, handler, false);
                this._clipEventSubscriptions.push({ eventName: eventName, handler: handler });
            }
        },

        // Unsubscribe a specific media event.
        _removeClipEventListener: function (mediaElement, eventName) {
            if (mediaElement) {
                for (var ix = 0; ix < this._clipEventSubscriptions.length; ix++) {
                    if (eventName === this._clipEventSubscriptions[ix].eventName) {
                        mediaElement.removeEventListener(eventName, this._clipEventSubscriptions[ix].handler);
                        this._clipEventSubscriptions.splice(ix, 1);
                        break;
                    }
                }
            }
        },

        // Unsubscribe from previously subscribed media events.
        _removeAllClipEventListeners: function (mediaElement) {
            if (mediaElement) {
                var mediaEventSubscriptionsLength = this._clipEventSubscriptions.length;
                for (var i = 0; i < mediaEventSubscriptionsLength; i++) {
                    mediaElement.removeEventListener(this._clipEventSubscriptions[i].eventName, this._clipEventSubscriptions[i].handler);
                }
            }
            this._clipEventSubscriptions = [];
        },

        _setOptions: function (control, options) {
            if (typeof options === "object") {
                var keys = Object.keys(options);
                for (var i = 0, len = keys.length; i < len; i++) {
                    var key = keys[i];
                    var value = options[key];
                    if (key.length > 2) {
                        var ch1 = key[0];
                        var ch2 = key[1];
                        if ((ch1 === 'o' || ch1 === 'O') && (ch2 === 'n' || ch2 === 'N')) {
                            if (typeof value === "function") {
                                if (control.addEventListener) {
                                    control.addEventListener(key.substr(2), value);
                                    continue;
                                }
                            }
                        }
                    }
                    control[key] = value;
                }
            }
        },

        // Adds a single class to the element's existing classes.
        _addClassToElement: function (elem, className) {
            var existing = elem.className || "";
            var existingList = existing.split(" ");
            if (existingList.indexOf(className) === -1) {
                elem.className = (existing + " " + className).trim();
            }
        },

        // Removes a single class from the element's existing classes.
        _removeClassFromElement: function (elem, className) {
            var existing = elem.className || "";
            var existingList = existing.split(" ");
            var match = -1;
            if ((match = existingList.indexOf(className)) !== -1) {
                // the class is present
                existingList.splice(match, 1);
                var newClassName = existingList.join(" ");
                elem.className = newClassName;
            }
        }
    };

    var strings = {
        get mediaPlayerRequired() { return "mediaPlayer is required input to the ClipScheduler"; },
        get scheduleDataNotAnObject() { return "clip is not an object"; },
        get mediaTimeout() { return "Request for media timed out."; },
        get contentChangedDuringClipPlayback() { return "media element changed while clip was playing"; }
    };

}());

(function () {
    "use strict";

    if (typeof (MicrosoftNSJS) === "undefined") {
        Object.defineProperty(window, "MicrosoftNSJS", { value: {}, writable: false, enumerable: true, configurable: true });
    }
    if (typeof (MicrosoftNSJS.Advertising) === "undefined") {
        Object.defineProperty(MicrosoftNSJS, "Advertising", { value: {}, writable: false, enumerable: true, configurable: true });
    }

    // This is used to retrieve ads from a remote server and schedule them into the timeline 
    // of the MediaPlayer.
    MicrosoftNSJS.Advertising.AdScheduler = function (mediaPlayerElement, options) {
        /// <summary locid="MicrosoftNSJS.Advertising.AdScheduler">
        ///   AdScheduler allows developers to retrieve video and audio ads and schedule them in their
        ///   content's timeline.
        /// </summary>
        /// <param name="mediaPlayerElement" type="HTMLElement" domElement="true" locid="MicrosoftNSJS.Advertising.AdScheduler_p:mediaPlayer">
        ///   The MediaPlayer into which the ads will be scheduled. If null or undefined, the ad
        ///   schedule data will be retrieved and made available, but not automatically played.
        ///   An exception will be thrown if mediaPlayerElement is provided but does not contain
        ///   a MediaPlayer winControl.
        /// </param>
        /// <param name="options" type="Object" locid="MicrosoftNSJS.Advertising.AdScheduler_p:options">
        ///   The set of options to be initially applied to the AdScheduler. Options must correspond
        ///   to properties on the AdScheduler.
        /// </param>
        /// <returns type="MicrosoftNSJS.Advertising.AdScheduler" locid="MicrosoftNSJS.Advertising.AdScheduler_returnValue">
        ///   A constructed AdScheduler.
        /// </returns>
        // Allow mediaPlayer to be null for scenarios where app just wants the schedule data but not have us play it.
        if (mediaPlayerElement) {
            var control = mediaPlayerElement.tvControl || mediaPlayerElement.winControl;
            if (control) {
                this._mediaPlayer = control;
            } else {
                // error - an element was passed in which does not have a winControl
                var err = new Error(strings.mediaPlayerMustHaveTvControl);
                err.name = "MicrosoftNSJS.Advertising.AdScheduler.mediaPlayerMustHaveTvControl";
                throw err;
            }

            this._element = this._createAnchorElement(mediaPlayerElement);
        }

        try {
            this._context = new Microsoft.Advertising.Shared.WinRT.ProjectedContext();
        }
        catch (err) {
            this._context = null;
        }

        this._schedule = null;
        this._clipScheduler = null;
        this._requestInProgress = false;
        this._isDisposed = false;
        this._playSkippedMedia = true;

        // event handlers
        this._eventManager = new MicrosoftNSJS.Advertising.EventManager();
        this._onAdProgress = null;
        this._onErrorOccurred = null;
        this._onPodStart = null;
        this._onPodEnd = null;
        this._onPodCountdown = null;
        this._onAllComplete = null;

        // internal event handlers that we need to clean up
        this._eventHandlerAdTimeUpdateInfo = null;
        this._afterHideControlsHandler = null;

        this._requestTimeout = MicrosoftNSJS.Advertising.AdScheduler._defaultRequestTimeoutMs;
        this._mediaTimeout = MicrosoftNSJS.Advertising.AdScheduler._defaultMediaTimeoutMs;

        this._setOptions(this, options);
    };

    MicrosoftNSJS.Advertising.AdScheduler.prototype = {
        /// <field type="Object" locid="MicrosoftNSJS.Advertising.AdScheduler.schedule">
        ///   This is a getter for the schedule data, after it has been retrieved from 
        ///   the server. This is a full object hierarchy (e.g. schedule/pods/adPackages)
        ///   corresponding to the structure of a VMAP/VAST payload.
        ///   See also: MicrosoftAdvertising.Shared.WinRT.AdSchedule
        /// </field>
        get schedule() {
            return this._schedule;
        },

        requestSchedule: function (applicationId, adUnitId, adTags) {
            /// <summary locid="MicrosoftNSJS.Advertising.AdScheduler.requestSchedule">
            ///   Request an ad schedule from Microsoft’s ad servers, and insert it into 
            ///   the MediaPlayer timeline (if MediaPlayer was provided).
            /// </summary>
            /// <param name="applicationId">
            ///   The application ID of the app. This value is provided to you when you 
            ///   register the app with PubCenter.
            /// </param>
            /// <param name="adUnitId">
            ///   The adUnitId as provisioned on PubCenter. This id specifies the width, 
            ///   height, and format of ad.
            /// </param>
            /// <param name="adTags">
            ///   Ad tags are name/value pairs which will be appended to the request URL. Optional.
            ///   Limits: 
            ///     Maximum of 10 tags. 
            ///     Tag names have maximum of 16 characters. 
            ///     Tag values have maximum of 128 characters.
            ///   If the limits are exceeded the request will fail. 
            /// </param>
            /// <returns type="Promise" locid="MicrosoftNSJS.Advertising.AdScheduler.requestSchedule_returnValue">
            ///   Promise object that completes when the schedule has been received.
            ///
            ///   Success handler argument is an instance of Microsoft.Advertising.WinRT.AdSchedule
            ///   which contains information about what ads were received and when they will play.
            ///
            ///   Error handler argument is JSON containing:
            ///     {
            ///         errorMessage: string describing the error
            ///         errorCode: a MicrosoftNSJS.Advertising.AdErrorCode enum value
            ///     }
            /// </returns>

            if (typeof (applicationId) === "undefined" || applicationId === null || applicationId === "" ||
                typeof (adUnitId) === "undefined" || adUnitId === null || adUnitId === "") {
                var errorArgs = this._createErrorArgs(strings.applicationIdAndAdUnitIdRequired, MicrosoftNSJS.Advertising.AdErrorCode.clientConfiguration);
                return new Promise(function (complete, error, progress) {
                    error(errorArgs);
                });
            }

            var adTagCollection = this._createAdTagCollectionFromJsonObject(adTags);

            return this._sendRequest({ applicationId: applicationId, adUnitId: adUnitId, adTags: adTagCollection });
        },

        requestScheduleByUrl: function (adServiceUrl) {
            /// <summary locid="MicrosoftNSJS.Advertising.AdScheduler.requestSchedule">
            ///   Request an ad schedule from the server, and insert ads into the MediaPlayer 
            ///   timeline (if MediaPlayer was provided).
            ///   The payload returned must be either VAST or VMAP standards:
            ///       VAST spec: http://www.iab.net/media/file/VASTv3.0.pdf
            ///       VMAP spec: http://www.iab.net/media/file/VMAP.pdf 
            /// </summary>
            /// <param name="adServiceUrl" type="String">
            ///   This URL is used to override the default ad server.
            /// </param>
            /// <returns type="Promise" locid="MicrosoftNSJS.Advertising.AdScheduler.requestSchedule_returnValue">
            ///   Promise object that completes when the schedule has been received.
            ///
            ///   Success handler argument is an instance of Microsoft.Advertising.WinRT.AdSchedule
            ///   which contains information about what ads were received and when they will play.
            ///
            ///   Error handler argument is JSON containing:
            ///     {
            ///         errorMessage: string describing the error
            ///         errorCode: a MicrosoftNSJS.Advertising.AdErrorCode enum value
            ///     }
            /// </returns>

            var errorArgs = null;
            if (typeof (adServiceUrl) === "string" && adServiceUrl !== "") {
                if (!this._isUrlValid(adServiceUrl)) {
                    errorArgs = this._createErrorArgs(strings.adServiceUrlNotValid, MicrosoftNSJS.Advertising.AdErrorCode.clientConfiguration);
                }
            } else {
                errorArgs = this._createErrorArgs(strings.adServiceUrlIsRequired, MicrosoftNSJS.Advertising.AdErrorCode.clientConfiguration);
            }
            if (errorArgs !== null) {
                return new Promise(function (complete, error, progress) {
                    error(errorArgs);
                });
            }

            return this._sendRequest({ serviceUrl: adServiceUrl });
        },

        addEventListener: function (type, listener) {
            /// <summary locid="MicrosoftNSJS.Advertising.AdScheduler.addEventListener">
            ///   Add an event listener to the AdScheduler. 
            ///   See AdScheduler.EventType enum for supported events.
            /// </summary>
            /// <param name="type" type="String" locid="MicrosoftNSJS.Advertising.AdScheduler.addEventListener_p:type">
            ///   Required. Event type to add.
            /// </param>
            /// <param name="listener" type="Function" locid="MicrosoftNSJS.Advertising.AdScheduler.addEventListener_p:listener">
            ///   Required. The event handler function to associate with this event.
            /// </param>
            if (this._eventManager) {
                this._eventManager.addEventListener(type, listener);
            }
        },

        removeEventListener: function (type, listener) {
            /// <summary locid="MicrosoftNSJS.Advertising.AdScheduler.removeEventListener">
            ///   Add an event listener to the AdScheduler.
            ///   See AdScheduler.EventType enum for supported events.
            /// </summary>
            /// <param name="type" type="String" locid="MicrosoftNSJS.Advertising.AdScheduler.removeEventListener_p:type">
            ///   Required. Event type to remove.
            /// </param>
            /// <param name="listener" type="Function" locid="MicrosoftNSJS.Advertising.AdScheduler.removeEventListener_p:listener">
            ///   Required. The event handler function to remove for this event.
            /// </param>
            if (this._eventManager) {
                this._eventManager.removeEventListener(type, listener);
            }
        },

        dispose: function () {
            /// <summary locid="MicrosoftNSJS.Advertising.AdScheduler.dispose">
            ///   Dispose of any resources.
            /// </summary>
            if (this._clipScheduler !== null) {
                this._clipScheduler.dispose();
                this._clipScheduler = null;
            }

            this._schedule = null;
            this._onAdProgress = null;
            this._onErrorOccurred = null;
            this._onPodStart = null;
            this._onPodEnd = null;
            this._onPodCountdown = null;
            this._onAllComplete = null;

            this._mediaPlayer = null;

            if (this._eventManager) {
                this._eventManager.dispose();
                this._eventManager = null;
            }

            if (this._element) {
                if (this._element.parentElement) {
                    this._element.parentElement.removeChild(this._element);
                }
                this._element = null;
            }

            this._isDisposed = true;
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.AdScheduler.onErrorOccurred">
        ///   This event is fired when the AdScheduler experiences an error.
        ///   The handler function takes two parameters: 
        ///       the AdScheduler which fired the event,
        ///       JSON:
        ///         { 
        ///            errorMessage: descriptive string,
        ///            errorCode: the AdErrorCode string
        ///         }
        ///   See also: MicrosoftNSJS.Advertising.AdErrorCode enumeration
        /// </field>
        get onErrorOccurred() {
            return this._onErrorOccurred;
        },
        set onErrorOccurred(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onErrorOccurred = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.AdScheduler.onAdProgress">
        ///   This event is fired when ad playback reaches quartile checkpoints (see MediaProgress
        ///   enumeration).
        ///   The handler function takes a two parameters: 
        ///       the AdScheduler which fired the event,
        ///       JSON: 
        ///         {
        ///             progress: a MediaProgress enum,
        ///             clip: the Clip representing the video being played,
        ///             adPackage: a Microsoft.Advertising.WinRT.AdPackage class representing the part
        ///                        of the ad server VAST payload corresponding to the currently playing ad
        ///         }
        ///   See also: MicrosoftNSJS.Advertising.Clip
        ///   See also: MicrosoftNSJS.Advertising.MediaProgress enumeration
        ///   See also: Microsoft.Advertising.WinRT.AdPackage
        /// </field>
        get onAdProgress() {
            return this._onAdProgress;
        },
        set onAdProgress(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onAdProgress = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.AdScheduler.onPodStart">
        ///   This event is fired when an ad pod starts. A pod is a set of ads scheduled to 
        ///   start at the same time and which will play in series.
        ///   The handler function takes two parameters: 
        ///       the AdScheduler which fired the event,
        ///       JSON:
        ///         {
        ///           startTime: number of seconds,
        ///           pod: the AdPod from the AdSchedule
        ///         }
        ///   See also: MicrosoftAdvertising.Shared.WinRT.AdPod
        /// </field>
        get onPodStart() {
            return this._onPodStart;
        },
        set onPodStart(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onPodStart = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.AdScheduler.onPodEnd">
        ///   This event is fired when an ad pod ends.
        ///   The handler function takes two parameters:
        ///       the AdScheduler which fired the event,
        ///       JSON:
        ///         {
        ///           startTime: number of seconds,
        ///           pod: the AdPod from the AdSchedule
        ///         }
        ///   See also: MicrosoftAdvertising.Shared.WinRT.AdPod
        /// </field>
        get onPodEnd() {
            return this._onPodEnd;
        },
        set onPodEnd(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onPodEnd = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.AdScheduler.onPodCountdown">
        ///   This event is fired when an ad is playing and indicates how much time remains in the 
        ///   current pod. 
        ///   The handler function takes two parameters:
        ///       the AdScheduler which fired the event,
        ///       JSON:
        ///         {
        ///           remainingAdTime: number of seconds left for the current ad, 
        ///           remainingPodTime: number of seconds left for the current pod
        ///         }
        /// </field>
        get onPodCountdown() {
            return this._onPodCountdown;
        },
        set onPodCountdown(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onPodCountdown = value;
            }
        },

        /// <field type="Function" locid="MicrosoftNSJS.Advertising.AdScheduler.onAllComplete">
        ///   This event is fired when the main content reaches the end and any scheduled post-roll ads
        ///   are also ended. If there are no post-rolls, the event fires when the main content ends.
        ///   The handler function takes one parameter: 
        ///       the AdScheduler which fired the event
        /// </field>
        get onAllComplete() {
            return this._onAllComplete;
        },
        set onAllComplete(value) {
            if (typeof (value) === "function" || value === null || typeof (value) === "undefined") {
                this._onAllComplete = value;
            }
        },

        /// <field type="Number" locid="MicrosoftNSJS.Advertising.AdControl.requestTimeout">
        ///   The number of milliseconds to wait for an ad request response before timing 
        ///   out, 0 or less means don't timeout. The default is 30000ms or 30 seconds.
        /// </field>
        get requestTimeout() {
            return this._requestTimeout;
        },
        set requestTimeout(value) {
            if (this._requestTimeout !== value && typeof (value) === "number" && value >= 0) {
                this._requestTimeout = value;
            }
        },

        /// <field type="Number" locid="MicrosoftNSJS.Advertising.AdControl.mediaTimeout">
        ///   The number of milliseconds the media must be playable in, 0 or less means 
        ///   don't timeout. The default is 30000ms or 30 seconds.
        /// </field>
        get mediaTimeout() {
            return this._mediaTimeout;
        },
        set mediaTimeout(value) {
            if (this._mediaTimeout !== value && typeof (value) === "number" && value >= 0) {
                this._mediaTimeout = value;
            }
        },

        /// <field type="boolean" locid="MicrosoftNSJS.Advertising.AdScheduler.playSkippedMedia">
        ///   Boolean indicating whether scheduled media will play if user skips ahead 
        ///   to a point past a scheduled start time.
        ///      true = Skipped media will be played. If multiple scheduled breaks have 
        ///             been skipped, only the last time slot will play. (default)
        ///      false = Skipped media will not play. 
        /// </field>
        get playSkippedMedia() {
            return this._playSkippedMedia;
        },
        set playSkippedMedia(value) {
            this._playSkippedMedia = value;
            if (this._clipScheduler) {
                this._clipScheduler.playSkippedMedia = this._playSkippedMedia;
            }
        },

        _isUrlValid: function (url) {
            var re = /^https?:\/\/.+$/;
            if (url.match(re) === null) {
                return false;
            }
            return true;
        },

        _getAdSchedulerClass: function () {
            // This just returns a reference to the WinRT class so we can mock it out for testing.
            return Microsoft.Advertising.Shared.WinRT.AdScheduler;
        },

        _getProjectedMediaConstraintsClass: function () {
            // This just returns a reference to the WinRT class.
            return Microsoft.Advertising.Shared.WinRT.ProjectedMediaConstraints;
        },

        _sendRequest: function (params) {
            return new Promise(function (complete, error, progress) {
                if (this._requestInProgress) {
                    error(this._createErrorArgs(strings.requestAlreadyInProgress, MicrosoftNSJS.Advertising.AdErrorCode.refreshNotAllowed));
                    return;
                }

                // We have to do some things before triggering the promise's error handler.
                var handleError = function (arg) {
                    this._requestInProgress = false;
                    this._fireAllCompleteIfNoContent();
                    error(arg);
                }.bind(this);

                try {
                    this._requestInProgress = true;

                    var promise = null;
                    if (params.adUnitId) {
                        promise = this._getAdSchedulerClass().getScheduleStrictAsync(params.applicationId, params.adUnitId, this._requestTimeout, 16, params.adTags, this._context, this._getProjectedMediaConstraintsClass().defaultMediaConstraints);
                    } else {
                        promise = this._getAdSchedulerClass().getScheduleStrictAsync(params.serviceUrl, this._requestTimeout, 16, this._context, this._getProjectedMediaConstraintsClass().defaultMediaConstraints);
                    }

                    promise.then(
                        function (schedule) {
                            this._requestInProgress = false;
                            if (schedule !== null) {
                                if (schedule.error !== null) {
                                    handleError(schedule.error);
                                } else if (schedule.value !== null) {
                                    this._schedule = schedule.value;
                                    this._scheduleAdsInPlayer(schedule.value);

                                    if (this._clipScheduler) {
                                        // Tell the scheduler to check whether any media is playing. If no ads are scheduled (perhaps 
                                        // because the server returned an empty schedule) and there is no main content, then it will 
                                        // trigger the allComplete event.
                                        this._clipScheduler._checkForAllComplete();
                                    }

                                    complete(schedule.value);
                                } else {
                                    handleError(this._createErrorArgs("schedule request returned empty object", MicrosoftNSJS.Advertising.AdErrorCode.other));
                                }
                            } else {
                                handleError(this._createErrorArgs("schedule is null", MicrosoftNSJS.Advertising.AdErrorCode.other));
                            }
                        }.bind(this),
                        function (evt) {
                            // When errors occur, the promise "success" handler will be triggered and the returned object
                            // will contain the error details. This error handler should not be triggered.
                            handleError(this._createErrorArgs("error occurred in request promise", MicrosoftNSJS.Advertising.AdErrorCode.other));
                        }.bind(this)
                    );

                }
                catch (e) {
                    handleError(this._createErrorArgs(e.message, MicrosoftNSJS.Advertising.AdErrorCode.other));
                }
            }.bind(this));
        },

        _createAnchorElement: function (parentElem) {
            // Creates a non-visible DIV element with win-disposable class which will allow us to auto-dispose when 
            // it is removed from the DOM.
            var elem = document.createElement("div");
            elem.style.width = "0px";
            elem.style.height = "0px";
            elem.style.display = "none";
            elem.className = "tv-disposable win-disposable uac-adscheduler-anchor";
            elem.winControl = this;
            parentElem.appendChild(elem);

            return elem;
        },

        _createAdTagCollectionFromJsonObject: function (adTags) {
            var adTagCollection = null;

            if (adTags && typeof (adTags) === "object") {
                adTagCollection = new Microsoft.Advertising.Shared.WinRT.AdTagCollection();
                for (var key in adTags) {
                    if (adTags.hasOwnProperty(key) && typeof (key) === "string" && typeof (adTags[key]) === "string") {
                        try {
                            adTagCollection.addAdTag(key, adTags[key]);
                        } catch (err) {
                            this._fireErrorOccurred("could not add ad tag with key: " + key, MicrosoftNSJS.Advertising.AdErrorCode.other);
                        }
                    } else {
                        this._fireErrorOccurred("could not add ad tag as key or value were not strings", MicrosoftNSJS.Advertising.AdErrorCode.other);
                    }
                }
            }

            return adTagCollection;
        },

        // When an ad response is received from the server, this function is called so the control can display it.
        _scheduleAdsInPlayer: function (schedule) {
            if (!this._mediaPlayer || !this._mediaPlayer.element) {
                // MediaPlayer has already been removed or disposed. Just return.
                return;
            }

            if (this._clipScheduler === null) {
                try {
                    this._clipScheduler = this._createScheduler();
                    this._addSchedulerListeners();
                }
                catch (err) {
                    var message = err.message ? err.message : "unable to create ClipScheduler";
                    this._fireErrorOccurred(message, MicrosoftNSJS.Advertising.AdErrorCode.other);
                    return;
                }
            }

            for (var i = 0; i < schedule.pods.length; i++) {
                var pod = schedule.pods[i];
                for (var j = 0; j < pod.packages.length; j++) {
                    var pkg = pod.packages[j];
                    // The package may not have any ads matched by the server, in which case there will be no videos.
                    if (pkg.video && pkg.video.length > 0) {
                        this._scheduleAd(pod, pkg);
                    }
                }
            }
        },

        _createScheduler: function () {
            return new MicrosoftNSJS.Advertising.ClipScheduler(this._mediaPlayer.element,
                { playSkippedMedia: this._playSkippedMedia, mediaTimeout: this._mediaTimeout });
        },

        _addSchedulerListeners: function () {
            if (this._clipScheduler) {
                this._clipScheduler.onMediaProgress = this._mediaProgressHandler.bind(this);
                this._clipScheduler.onMediaBreakEnd = this._mediaBreakEndHandler.bind(this);
                this._clipScheduler.onMediaBreakStart = this._mediaBreakStartHandler.bind(this);
                this._clipScheduler.onMediaError = this._mediaErrorHandler.bind(this);
                this._clipScheduler.onAllComplete = this._allCompleteHandler.bind(this);
                this._clipScheduler.onBreakCountdown = this._breakCountdownHandler.bind(this);
            }
        },

        _selectMedia: function (mediaFiles) {
            // this checks for the highest resolution video subject to bitrate and delivery type constraints
            var bestFile = null;
            var currentPriority = 0;
            for (var ix = 0; ix < mediaFiles.length; ix++) {
                var file = mediaFiles[ix];

                var updateBestStream = MicrosoftNSJS.Advertising.AdScheduler._isPreferredStream(file, bestFile);
                if (updateBestStream)
                    bestFile = file;
            }
            return bestFile;
        },

        _scheduleAd: function (pod, pkg) {
            var clip = this._createClip(pod, pkg);
            try {
                this._clipScheduler.scheduleMedia(clip);
            }
            catch (err) {
                this._fireErrorOccurred(err.message, MicrosoftNSJS.Advertising.AdErrorCode.other);
            }
        },

        _createClip: function (pod, pkg) {
            var mediaSelector = function () {
                var media = this._selectMedia(pkg.video);

                if (media && media.uri && media.uri !== "") {
                    return media.uri;
                } else {
                    this._reportError(pkg, MicrosoftNSJS.Advertising.AdScheduler.VastErrorCode.noSupportedMedia);
                    this._fireErrorOccurred(strings.noSupportedMedia, MicrosoftNSJS.Advertising.AdErrorCode.invalidServerResponse);
                    return null;
                }
            };

            var clip = new MicrosoftNSJS.Advertising.Clip({
                timeOffset: pod.time,
                type: "video",
                selectMedia: mediaSelector.bind(this),
                skipOffset: pkg.skipOffset,
                podId: pod.id,
                duration: MicrosoftNSJS.Advertising.AdUtilities.hhmmssToSeconds(pkg.duration),
                _package: pkg,
                _pod: pod,
            });
            return clip;
        },

        _allCompleteHandler: function (sender, args) {
            // bubble up the ClipScheduler's allComplete event
            this._fireAllCompleteEvent();
        },

        _breakCountdownHandler: function (sender, args) {
            this._fireCountdownEvent(args.remainingBreakTime, args.remainingAdTime);
        },

        _mediaBreakStartHandler: function (sender, args) {
            if (args && args._clips && args._clips[0] && args._clips[0]._pod) {
                this._firePodStartEvent(args, args._clips[0]._pod);
            }
        },

        _mediaBreakEndHandler: function (sender, args) {
            if (args && args._clips && args._clips[0] && args._clips[0]._pod) {
                this._firePodEndEvent(args, args._clips[0]._pod);
            }
        },

        _mediaErrorHandler: function (sender, args) {
            if (args && args.clip) {
                this._reportError(args.clip._package, MicrosoftNSJS.Advertising.AdScheduler.VastErrorCode.unableToPlayFile);
            }

            this._fireErrorOccurred(args.message, args.errorCode);
        },

        _mediaProgressHandler: function (sender, args) {
            this._fireProgressEvent(args.clip, args.progress);
        },

        // This is used to create the error arguments for both the errorOccurred event and the
        // error result from a promise.
        _createErrorArgs: function (msg, code) {
            return { errorMessage: msg, errorCode: code };
        },

        _fireCountdownEvent: function (remainingBreakTime, remainingAdTime) {
            var eventData = { remainingPodTime: remainingBreakTime, remainingAdTime: remainingAdTime };
            if (typeof (this._onPodCountdown) === "function") {
                this._onPodCountdown(this, eventData);
            }
            if (this._eventManager) {
                this._eventManager.broadcastEvent(MicrosoftNSJS.Advertising.AdScheduler.EventType.podCountdown, this, eventData);
            }
        },

        _fireErrorOccurred: function (msg, errorCode) {
            var errorArgs = this._createErrorArgs(msg, errorCode);
            if (typeof (this._onErrorOccurred) === "function") {
                this._onErrorOccurred(this, errorArgs);
            }
            if (this._eventManager) {
                this._eventManager.broadcastEvent(MicrosoftNSJS.Advertising.AdScheduler.EventType.errorOccurred, this, errorArgs);
            }
        },

        _fireProgressEvent: function (clip, progressEnum) {
            var pkg = clip._package;
            if (pkg && pkg.reportAsync) {
                if (progressEnum === MicrosoftNSJS.Advertising.MediaProgress.start) {
                    // When the video starts, also fire the impression and creativeView events.
                    this._reportImpression(pkg);
                    pkg.reportAsync("creativeView");
                }
                if (progressEnum === MicrosoftNSJS.Advertising.MediaProgress.complete) {
                    pkg.reportAsync(progressEnum);
                } else {
                    pkg.reportAsync(progressEnum);
                }
            }
            var eventData = { progress: progressEnum, adPackage: pkg, clip: clip };
            if (typeof (this._onAdProgress) === "function") {
                this._onAdProgress(this, eventData);
            }
            if (this._eventManager) {
                this._eventManager.broadcastEvent(MicrosoftNSJS.Advertising.AdScheduler.EventType.adProgress, this, eventData);
            }

        },

        _firePodStartEvent: function (brk, pod) {
            if (pod.reportAsync) {
                pod.reportAsync(MicrosoftNSJS.Advertising.AdScheduler.AdBreakEvent.breakStart);
            }
            var eventData = { startTime: brk.startTime, pod: pod };
            if (typeof (this._onPodStart) === "function") {
                this._onPodStart(this, eventData);
            }
            if (this._eventManager) {
                this._eventManager.broadcastEvent(MicrosoftNSJS.Advertising.AdScheduler.EventType.podStart, this, eventData);
            }
        },

        _firePodEndEvent: function (brk, pod) {
            if (pod.reportAsync) {
                pod.reportAsync(MicrosoftNSJS.Advertising.AdScheduler.AdBreakEvent.breakEnd);
            }
            var eventData = { startTime: brk.startTime, pod: pod };
            if (typeof (this._onPodEnd) === "function") {
                this._onPodEnd(this, eventData);
            }
            if (this._eventManager) {
                this._eventManager.broadcastEvent(MicrosoftNSJS.Advertising.AdScheduler.EventType.podEnd, this, eventData);
            }
        },

        _fireAllCompleteEvent: function () {
            if (typeof (this._onAllComplete) === "function") {
                this._onAllComplete(this);
            }
            if (this._eventManager) {
                this._eventManager.broadcastEvent(MicrosoftNSJS.Advertising.AdScheduler.EventType.allComplete, this);
            }
        },

        _fireAllCompleteIfNoContent: function () {
            // If there is no main content into which we're scheduling ads, fire the all complete event.
            // This is called when an error occurs retrieving a schedule.
            if (!this._mediaPlayer ||
                !this._mediaPlayer.mediaElementAdapter ||
                !this._mediaPlayer.mediaElementAdapter.mediaElement ||
                !this._mediaPlayer.mediaElementAdapter.mediaElement.src) {
                this._fireAllCompleteEvent();
            }
        },

        _reportError: function (pkg, vastErrorCode) {
            if (pkg) {
                var params = new Windows.Foundation.Collections.PropertySet();
                if (vastErrorCode) {
                    params.insert("[ERRORCODE]", vastErrorCode);
                    params.insert("%5BERRORCODE%5D", vastErrorCode);
                }

                pkg.reportAsync("error", params.getView());
            }
        },

        _reportImpression: function (pkg) {
            // This is put into a separate function so that the UAC app can overwrite it to prevent impression reporting, 
            // since the shell handles the impression reporting.
            pkg.reportAsync("impression");
        },

        _setOptions: function (control, options) {
            if (typeof options === "object") {
                var keys = Object.keys(options);
                for (var i = 0, len = keys.length; i < len; i++) {
                    var key = keys[i];
                    var value = options[key];
                    if (key.length > 2) {
                        var ch1 = key[0];
                        var ch2 = key[1];
                        if ((ch1 === 'o' || ch1 === 'O') && (ch2 === 'n' || ch2 === 'N')) {
                            if (typeof value === "function") {
                                if (control.addEventListener) {
                                    control.addEventListener(key.substr(2), value);
                                    continue;
                                }
                            }
                        }
                    }
                    control[key] = value;
                }
            }
        }
    };

    // static

    /// <field type="Object">
    //    These are AdScheduler’s supported event types when using addEventListener 
    //    or removeEventListener.
    /// </field>
    MicrosoftNSJS.Advertising.AdScheduler.EventType = {
        adProgress: "adProgress",
        errorOccurred: "errorOccurred",
        podStart: "podStart",
        podEnd: "podEnd",
        podCountdown: "podCountdown",
        allComplete: "allComplete"
    };

    MicrosoftNSJS.Advertising.AdScheduler._maxAdBitrate = 2000;

    MicrosoftNSJS.Advertising.AdScheduler._defaultRequestTimeoutMs = 30000; // 30 seconds
    MicrosoftNSJS.Advertising.AdScheduler._defaultMediaTimeoutMs = 30000; // 30 seconds
    MicrosoftNSJS.Advertising.AdScheduler._videoElement = null;

    MicrosoftNSJS.Advertising.AdScheduler.AdBreakEvent = {
        breakStart: "breakStart",
        breakEnd: "breakEnd",
        error: "error"
    };

    MicrosoftNSJS.Advertising.AdScheduler.VastErrorCode = {
        generalLinearError: "400",
        fileNotFound: "401",
        mediaTimeout: "402",
        noSupportedMedia: "403",
        unableToPlayFile: "405"
    };

    MicrosoftNSJS.Advertising.AdScheduler.canPlayType = function (mimeType) {
        // The video element's canPlayType returns "probably", "maybe", or "" if it cannot be played. We will trust 
        // than any non-"" result is playable.
        if (!MicrosoftNSJS.Advertising.AdScheduler._videoElement) {
            MicrosoftNSJS.Advertising.AdScheduler._videoElement = document.createElement("video");
        }
        return (MicrosoftNSJS.Advertising.AdScheduler._videoElement.canPlayType(mimeType) !== "");
    };

    MicrosoftNSJS.Advertising.AdScheduler._endsWith = function (str, value) {
        if (str && typeof (str) === "string" &&
            value && typeof (value) === "string" &&
            str.length >= value.length &&
            str.toLowerCase().substr(str.length - value.length, value.length) === value) {
            return true;
        }
        return false;
    };

    MicrosoftNSJS.Advertising.AdScheduler._getStreamPreferenceRank = function (stream) {
        //Ranks for each media type
        //DASH = 3, HLS = 2, Progressive = 1, NULL or invalid = 0
        if (!stream) {
            return 0;
        }

        var mimeType = typeof (stream.type) === "string" ? stream.type.toLowerCase() : "";
        var uri = typeof (stream.uri) === "string" ? stream.uri.toLowerCase() : "";

        // MPEG DASH
        if (mimeType === "application/dash+xml") {
            return 3;
        }
        // HLS
        if (mimeType === "application/vnd.apple.mpegurl" ||
            mimeType === "application/x-mpegurl" ||
            MicrosoftNSJS.Advertising.AdScheduler._endsWith(uri, ".m3u8") ||
            uri.indexOf(".m3u8?") !== -1
        ) {
            return 2;
        }
        // other playable
        if (MicrosoftNSJS.Advertising.AdScheduler.canPlayType(mimeType) && stream.bitrate <= MicrosoftNSJS.Advertising.AdScheduler._maxAdBitrate) {
            return 1;
        }
        // not playable
        return 0;
    };

    MicrosoftNSJS.Advertising.AdScheduler._isPreferredStream = function (newFile, currentBest) {
        var newRank = MicrosoftNSJS.Advertising.AdScheduler._getStreamPreferenceRank(newFile);
        if (newRank === 0) {
            return false;
        }
        var currentRank = MicrosoftNSJS.Advertising.AdScheduler._getStreamPreferenceRank(currentBest);
        if (currentRank !== newRank) {
            return newRank > currentRank;
        }
        else {
            return newFile.width > currentBest.width;
        }
    };

    var strings = {
        get mediaPlayerMustHaveTvControl() { return "mediaPlayer HTML element must have a MediaPlayer control"; },
        get applicationIdAndAdUnitIdRequired() { return "applicationId and adUnitId are required"; },
        get adServiceUrlIsRequired() { return "adServiceUrl is required"; },
        get adServiceUrlNotValid() { return "adServiceUrl is not a valid url"; },
        get requestAlreadyInProgress() { return "request is already in progress"; },
        get noSupportedMedia() { return "an ad package did not contain any supported media"; },
    };

}());

// SIG // Begin signature block
// SIG // MIIdogYJKoZIhvcNAQcCoIIdkzCCHY8CAQExCzAJBgUr
// SIG // DgMCGgUAMGcGCisGAQQBgjcCAQSgWTBXMDIGCisGAQQB
// SIG // gjcCAR4wJAIBAQQQEODJBs441BGiowAQS9NQkAIBAAIB
// SIG // AAIBAAIBAAIBADAhMAkGBSsOAwIaBQAEFNvUWKC0Y7Bm
// SIG // VyD9dMcJQ5MExXBXoIIYZDCCBMMwggOroAMCAQICEzMA
// SIG // AACu7D+ttou5LdIAAAAAAK4wDQYJKoZIhvcNAQEFBQAw
// SIG // dzELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0
// SIG // b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1p
// SIG // Y3Jvc29mdCBDb3Jwb3JhdGlvbjEhMB8GA1UEAxMYTWlj
// SIG // cm9zb2Z0IFRpbWUtU3RhbXAgUENBMB4XDTE2MDUwMzE3
// SIG // MTMyNVoXDTE3MDgwMzE3MTMyNVowgbMxCzAJBgNVBAYT
// SIG // AlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQH
// SIG // EwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29y
// SIG // cG9yYXRpb24xDTALBgNVBAsTBE1PUFIxJzAlBgNVBAsT
// SIG // Hm5DaXBoZXIgRFNFIEVTTjpCOEVDLTMwQTQtNzE0NDEl
// SIG // MCMGA1UEAxMcTWljcm9zb2Z0IFRpbWUtU3RhbXAgU2Vy
// SIG // dmljZTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoC
// SIG // ggEBAMU1NKkcd7KmYPBjeGAq6gNQs5mD6fPlPzBu2sR1
// SIG // z2RnTLzW0Uj5dvW2vPIwmEbrf+qeTo7whjXESDD1ihTK
// SIG // ilXmPM1KEDaeOo2LF3p5eL0wICdnFAnmhsvb8S2Exrl7
// SIG // WgoZ/oyKT7kesVEOtGOODNo8qbG3EGWHOjrpMOHKPgiM
// SIG // PHyqsT3A43ZtXP4Ms1Z4UmE17L/EtDQcJYroTQjROA/G
// SIG // 9CzY+xMY+c31WBrz+mfibRmOy0/u3GlAk9LiLSpRNA/4
// SIG // g75WOcy625blG+Fi1AaYJTMO21NAUgHL3DcdF8le/gHX
// SIG // JoYhUBreKWY21czrF7Nzzlh06uPyl0ZrRhyn7zMCAwEA
// SIG // AaOCAQkwggEFMB0GA1UdDgQWBBQcOHLSWpK6QYm6QfUy
// SIG // ZGCbCYBgVDAfBgNVHSMEGDAWgBQjNPjZUkZwCu1A+3b7
// SIG // syuwwzWzDzBUBgNVHR8ETTBLMEmgR6BFhkNodHRwOi8v
// SIG // Y3JsLm1pY3Jvc29mdC5jb20vcGtpL2NybC9wcm9kdWN0
// SIG // cy9NaWNyb3NvZnRUaW1lU3RhbXBQQ0EuY3JsMFgGCCsG
// SIG // AQUFBwEBBEwwSjBIBggrBgEFBQcwAoY8aHR0cDovL3d3
// SIG // dy5taWNyb3NvZnQuY29tL3BraS9jZXJ0cy9NaWNyb3Nv
// SIG // ZnRUaW1lU3RhbXBQQ0EuY3J0MBMGA1UdJQQMMAoGCCsG
// SIG // AQUFBwMIMA0GCSqGSIb3DQEBBQUAA4IBAQAAGbFWyVTR
// SIG // WVDTHF0cSnnpXNQ4IkBywutEopGvfDsAxV6JmGpJOsrx
// SIG // PnydwyApw1CvZJn/N7GEzkOWO4d0M8B3D3coSzx0gQZr
// SIG // j5JY+o3FhrrxyqVLj/T048igcNAj2dT0ztSXOUY7EGL8
// SIG // artNfhuVL2aJZzOlsO0KZgaAxMs3uSfnYBsK1jISCg8y
// SIG // i1fXaOkeaLmULy71e24x+dAF9rStp986WWLwJfy2sixx
// SIG // TSDuwNg0NVc1mt59ssmL2pnml9TZEiwN9j6owF8pJpA3
// SIG // x0OgxVbg1eJ6qzSPrNeBCYDEMvA81PV+/iiJAsyxTav2
// SIG // 3Nlg6NearEIgAj1UimNSDhoiMIIGBzCCA++gAwIBAgIK
// SIG // YRZoNAAAAAAAHDANBgkqhkiG9w0BAQUFADBfMRMwEQYK
// SIG // CZImiZPyLGQBGRYDY29tMRkwFwYKCZImiZPyLGQBGRYJ
// SIG // bWljcm9zb2Z0MS0wKwYDVQQDEyRNaWNyb3NvZnQgUm9v
// SIG // dCBDZXJ0aWZpY2F0ZSBBdXRob3JpdHkwHhcNMDcwNDAz
// SIG // MTI1MzA5WhcNMjEwNDAzMTMwMzA5WjB3MQswCQYDVQQG
// SIG // EwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UE
// SIG // BxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENv
// SIG // cnBvcmF0aW9uMSEwHwYDVQQDExhNaWNyb3NvZnQgVGlt
// SIG // ZS1TdGFtcCBQQ0EwggEiMA0GCSqGSIb3DQEBAQUAA4IB
// SIG // DwAwggEKAoIBAQCfoWyx39tIkip8ay4Z4b3i48WZUSNQ
// SIG // rc7dGE4kD+7Rp9FMrXQwIBHrB9VUlRVJlBtCkq6YXDAm
// SIG // 2gBr6Hu97IkHD/cOBJjwicwfyzMkh53y9GccLPx754gd
// SIG // 6udOo6HBI1PKjfpFzwnQXq/QsEIEovmmbJNn1yjcRlOw
// SIG // htDlKEYuJ6yGT1VSDOQDLPtqkJAwbofzWTCd+n7Wl7Po
// SIG // IZd++NIT8wi3U21StEWQn0gASkdmEScpZqiX5NMGgUqi
// SIG // +YSnEUcUCYKfhO1VeP4Bmh1QCIUAEDBG7bfeI0a7xC1U
// SIG // n68eeEExd8yb3zuDk6FhArUdDbH895uyAc4iS1T/+QXD
// SIG // wiALAgMBAAGjggGrMIIBpzAPBgNVHRMBAf8EBTADAQH/
// SIG // MB0GA1UdDgQWBBQjNPjZUkZwCu1A+3b7syuwwzWzDzAL
// SIG // BgNVHQ8EBAMCAYYwEAYJKwYBBAGCNxUBBAMCAQAwgZgG
// SIG // A1UdIwSBkDCBjYAUDqyCYEBWJ5flJRP8KuEKU5VZ5KSh
// SIG // Y6RhMF8xEzARBgoJkiaJk/IsZAEZFgNjb20xGTAXBgoJ
// SIG // kiaJk/IsZAEZFgltaWNyb3NvZnQxLTArBgNVBAMTJE1p
// SIG // Y3Jvc29mdCBSb290IENlcnRpZmljYXRlIEF1dGhvcml0
// SIG // eYIQea0WoUqgpa1Mc1j0BxMuZTBQBgNVHR8ESTBHMEWg
// SIG // Q6BBhj9odHRwOi8vY3JsLm1pY3Jvc29mdC5jb20vcGtp
// SIG // L2NybC9wcm9kdWN0cy9taWNyb3NvZnRyb290Y2VydC5j
// SIG // cmwwVAYIKwYBBQUHAQEESDBGMEQGCCsGAQUFBzAChjho
// SIG // dHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRz
// SIG // L01pY3Jvc29mdFJvb3RDZXJ0LmNydDATBgNVHSUEDDAK
// SIG // BggrBgEFBQcDCDANBgkqhkiG9w0BAQUFAAOCAgEAEJeK
// SIG // w1wDRDbd6bStd9vOeVFNAbEudHFbbQwTq86+e4+4LtQS
// SIG // ooxtYrhXAstOIBNQmd16QOJXu69YmhzhHQGGrLt48ovQ
// SIG // 7DsB7uK+jwoFyI1I4vBTFd1Pq5Lk541q1YDB5pTyBi+F
// SIG // A+mRKiQicPv2/OR4mS4N9wficLwYTp2OawpylbihOZxn
// SIG // LcVRDupiXD8WmIsgP+IHGjL5zDFKdjE9K3ILyOpwPf+F
// SIG // ChPfwgphjvDXuBfrTot/xTUrXqO/67x9C0J71FNyIe4w
// SIG // yrt4ZVxbARcKFA7S2hSY9Ty5ZlizLS/n+YWGzFFW6J1w
// SIG // lGysOUzU9nm/qhh6YinvopspNAZ3GmLJPR5tH4LwC8cs
// SIG // u89Ds+X57H2146SodDW4TsVxIxImdgs8UoxxWkZDFLyz
// SIG // s7BNZ8ifQv+AeSGAnhUwZuhCEl4ayJ4iIdBD6Svpu/RI
// SIG // zCzU2DKATCYqSCRfWupW76bemZ3KOm+9gSd0BhHudiG/
// SIG // m4LBJ1S2sWo9iaF2YbRuoROmv6pH8BJv/YoybLL+31HI
// SIG // jCPJZr2dHYcSZAI9La9Zj7jkIeW1sMpjtHhUBdRBLlCs
// SIG // lLCleKuzoJZ1GtmShxN1Ii8yqAhuoFuMJb+g74TKIdbr
// SIG // Hk/Jmu5J4PcBZW+JC33Iacjmbuqnl84xKf8OxVtc2E0b
// SIG // odj6L54/LlUWa8kTo/0wggYQMIID+KADAgECAhMzAAAA
// SIG // ZEeElIbbQRk4AAAAAABkMA0GCSqGSIb3DQEBCwUAMH4x
// SIG // CzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9u
// SIG // MRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNy
// SIG // b3NvZnQgQ29ycG9yYXRpb24xKDAmBgNVBAMTH01pY3Jv
// SIG // c29mdCBDb2RlIFNpZ25pbmcgUENBIDIwMTEwHhcNMTUx
// SIG // MDI4MjAzMTQ2WhcNMTcwMTI4MjAzMTQ2WjCBgzELMAkG
// SIG // A1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAO
// SIG // BgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29m
// SIG // dCBDb3Jwb3JhdGlvbjENMAsGA1UECxMETU9QUjEeMBwG
// SIG // A1UEAxMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMIIBIjAN
// SIG // BgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAky7a2OY+
// SIG // mNkbD2RfTahYTRQ793qE/DwRMTrvicJKLUGlSF3dEp7v
// SIG // q2YoNNV9KlV7TE2K8sDxstNSFYu2swi4i1AL3X/7agmg
// SIG // 3GcExPHfvHUYIEC+eCyZVt3u9S7dPkL5Wh8wrgEUirCC
// SIG // tVGg4m1l/vcYCo0wbU06p8XzNi3uXyygkgCxHEziy/f/
// SIG // JCV/14/A3ZduzrIXtsccRKckyn6B5uYxuRbZXT7RaO6+
// SIG // zUjQhiyu3A4hwcCKw+4bk1kT9sY7gHIYiFP7q78wPqB3
// SIG // vVKIv3rY6LCTraEbjNR+phBQEL7hyBxk+ocu+8RHZhbA
// SIG // hHs2r1+6hURsAg8t4LAOG6I+JQIDAQABo4IBfzCCAXsw
// SIG // HwYDVR0lBBgwFgYIKwYBBQUHAwMGCisGAQQBgjdMCAEw
// SIG // HQYDVR0OBBYEFFhWcQTwvbsz9YNozOeARvdXr9IiMFEG
// SIG // A1UdEQRKMEikRjBEMQ0wCwYDVQQLEwRNT1BSMTMwMQYD
// SIG // VQQFEyozMTY0Mis0OWU4YzNmMy0yMzU5LTQ3ZjYtYTNi
// SIG // ZS02YzhjNDc1MWM0YjYwHwYDVR0jBBgwFoAUSG5k5VAF
// SIG // 04KqFzc3IrVtqMp1ApUwVAYDVR0fBE0wSzBJoEegRYZD
// SIG // aHR0cDovL3d3dy5taWNyb3NvZnQuY29tL3BraW9wcy9j
// SIG // cmwvTWljQ29kU2lnUENBMjAxMV8yMDExLTA3LTA4LmNy
// SIG // bDBhBggrBgEFBQcBAQRVMFMwUQYIKwYBBQUHMAKGRWh0
// SIG // dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lvcHMvY2Vy
// SIG // dHMvTWljQ29kU2lnUENBMjAxMV8yMDExLTA3LTA4LmNy
// SIG // dDAMBgNVHRMBAf8EAjAAMA0GCSqGSIb3DQEBCwUAA4IC
// SIG // AQCI4gxkQx3dXK6MO4UktZ1A1r1mrFtXNdn06DrARZkQ
// SIG // Tdu0kOTLdlGBCfCzk0309RLkvUgnFKpvLddrg9TGp3n8
// SIG // 0yUbRsp2AogyrlBU+gP5ggHFi7NjGEpj5bH+FDsMw9Py
// SIG // gLg8JelgsvBVudw1SgUt625nY7w1vrwk+cDd58TvAyJQ
// SIG // FAW1zJ+0ySgB9lu2vwg0NKetOyL7dxe3KoRLaztUcqXo
// SIG // YW5CkI+Mv3m8HOeqlhyfFTYxPB5YXyQJPKQJYh8zC9b9
// SIG // 0JXLT7raM7mQ94ygDuFmlaiZ+QSUR3XVupdEngrmZgUB
// SIG // 5jX13M+Pl2Vv7PPFU3xlo3Uhj1wtupNC81epoxGhJ0tR
// SIG // uLdEajD/dCZ0xIniesRXCKSC4HCL3BMnSwVXtIoj/QFy
// SIG // mFYwD5+sAZuvRSgkKyD1rDA7MPcEI2i/Bh5OMAo9App4
// SIG // sR0Gp049oSkXNhvRi/au7QG6NJBTSBbNBGJG8Qp+5QTh
// SIG // KoQUk8mj0ugr4yWRsA9JTbmqVw7u9suB5OKYBMUN4hL/
// SIG // yI+aFVsE/KJInvnxSzXJ1YHka45ADYMKAMl+fLdIqm3n
// SIG // x6rIN0RkoDAbvTAAXGehUCsIod049A1T3IJyUJXt3OsT
// SIG // d3WabhIBXICYfxMg10naaWcyUePgW3+VwP0XLKu4O1+8
// SIG // ZeGyaDSi33GnzmmyYacX3BTqMDCCB3owggVioAMCAQIC
// SIG // CmEOkNIAAAAAAAMwDQYJKoZIhvcNAQELBQAwgYgxCzAJ
// SIG // BgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAw
// SIG // DgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3Nv
// SIG // ZnQgQ29ycG9yYXRpb24xMjAwBgNVBAMTKU1pY3Jvc29m
// SIG // dCBSb290IENlcnRpZmljYXRlIEF1dGhvcml0eSAyMDEx
// SIG // MB4XDTExMDcwODIwNTkwOVoXDTI2MDcwODIxMDkwOVow
// SIG // fjELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0
// SIG // b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1p
// SIG // Y3Jvc29mdCBDb3Jwb3JhdGlvbjEoMCYGA1UEAxMfTWlj
// SIG // cm9zb2Z0IENvZGUgU2lnbmluZyBQQ0EgMjAxMTCCAiIw
// SIG // DQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAKvw+nIQ
// SIG // HC6t2G6qghBNNLrytlghn0IbKmvpWlCquAY4GgRJun/D
// SIG // DB7dN2vGEtgL8DjCmQawyDnVARQxQtOJDXlkh36UYCRs
// SIG // r55JnOloXtLfm1OyCizDr9mpK656Ca/XllnKYBoF6WZ2
// SIG // 6DJSJhIv56sIUM+zRLdd2MQuA3WraPPLbfM6XKEW9Ea6
// SIG // 4DhkrG5kNXimoGMPLdNAk/jj3gcN1Vx5pUkp5w2+oBN3
// SIG // vpQ97/vjK1oQH01WKKJ6cuASOrdJXtjt7UORg9l7snuG
// SIG // G9k+sYxd6IlPhBryoS9Z5JA7La4zWMW3Pv4y07MDPbGy
// SIG // r5I4ftKdgCz1TlaRITUlwzluZH9TupwPrRkjhMv0ugOG
// SIG // jfdf8NBSv4yUh7zAIXQlXxgotswnKDglmDlKNs98sZKu
// SIG // HCOnqWbsYR9q4ShJnV+I4iVd0yFLPlLEtVc/JAPw0Xpb
// SIG // L9Uj43BdD1FGd7P4AOG8rAKCX9vAFbO9G9RVS+c5oQ/p
// SIG // I0m8GLhEfEXkwcNyeuBy5yTfv0aZxe/CHFfbg43sTUkw
// SIG // p6uO3+xbn6/83bBm4sGXgXvt1u1L50kppxMopqd9Z4Dm
// SIG // imJ4X7IvhNdXnFy/dygo8e1twyiPLI9AN0/B4YVEicQJ
// SIG // TMXUpUMvdJX3bvh4IFgsE11glZo+TzOE2rCIF96eTvSW
// SIG // sLxGoGyY0uDWiIwLAgMBAAGjggHtMIIB6TAQBgkrBgEE
// SIG // AYI3FQEEAwIBADAdBgNVHQ4EFgQUSG5k5VAF04KqFzc3
// SIG // IrVtqMp1ApUwGQYJKwYBBAGCNxQCBAweCgBTAHUAYgBD
// SIG // AEEwCwYDVR0PBAQDAgGGMA8GA1UdEwEB/wQFMAMBAf8w
// SIG // HwYDVR0jBBgwFoAUci06AjGQQ7kUBU7h6qfHMdEjiTQw
// SIG // WgYDVR0fBFMwUTBPoE2gS4ZJaHR0cDovL2NybC5taWNy
// SIG // b3NvZnQuY29tL3BraS9jcmwvcHJvZHVjdHMvTWljUm9v
// SIG // Q2VyQXV0MjAxMV8yMDExXzAzXzIyLmNybDBeBggrBgEF
// SIG // BQcBAQRSMFAwTgYIKwYBBQUHMAKGQmh0dHA6Ly93d3cu
// SIG // bWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljUm9vQ2Vy
// SIG // QXV0MjAxMV8yMDExXzAzXzIyLmNydDCBnwYDVR0gBIGX
// SIG // MIGUMIGRBgkrBgEEAYI3LgMwgYMwPwYIKwYBBQUHAgEW
// SIG // M2h0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lvcHMv
// SIG // ZG9jcy9wcmltYXJ5Y3BzLmh0bTBABggrBgEFBQcCAjA0
// SIG // HjIgHQBMAGUAZwBhAGwAXwBwAG8AbABpAGMAeQBfAHMA
// SIG // dABhAHQAZQBtAGUAbgB0AC4gHTANBgkqhkiG9w0BAQsF
// SIG // AAOCAgEAZ/KGpZjgVHkaLtPYdGcimwuWEeFjkplCln3S
// SIG // eQyQwWVfLiw++MNy0W2D/r4/6ArKO79HqaPzadtjvyI1
// SIG // pZddZYSQfYtGUFXYDJJ80hpLHPM8QotS0LD9a+M+By4p
// SIG // m+Y9G6XUtR13lDni6WTJRD14eiPzE32mkHSDjfTLJgJG
// SIG // KsKKELukqQUMm+1o+mgulaAqPyprWEljHwlpblqYluSD
// SIG // 9MCP80Yr3vw70L01724lruWvJ+3Q3fMOr5kol5hNDj0L
// SIG // 8giJ1h/DMhji8MUtzluetEk5CsYKwsatruWy2dsViFFF
// SIG // WDgycScaf7H0J/jeLDogaZiyWYlobm+nt3TDQAUGpgEq
// SIG // KD6CPxNNZgvAs0314Y9/HG8VfUWnduVAKmWjw11SYobD
// SIG // HWM2l4bf2vP48hahmifhzaWX0O5dY0HjWwechz4GdwbR
// SIG // BrF1HxS+YWG18NzGGwS+30HHDiju3mUv7Jf2oVyW2ADW
// SIG // oUa9WfOXpQlLSBCZgB/QACnFsZulP0V3HjXG0qKin3p6
// SIG // IvpIlR+r+0cjgPWe+L9rt0uX4ut1eBrs6jeZeRhL/9az
// SIG // I2h15q/6/IvrC4DqaTuv/DDtBEyO3991bWORPdGdVk5P
// SIG // v4BXIqF4ETIheu9BCrE/+6jMpF3BoYibV3FWTkhFwELJ
// SIG // m3ZbCoBIa/15n8G9bW1qyVJzEw16UM0xggSqMIIEpgIB
// SIG // ATCBlTB+MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2Fz
// SIG // aGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UE
// SIG // ChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSgwJgYDVQQD
// SIG // Ex9NaWNyb3NvZnQgQ29kZSBTaWduaW5nIFBDQSAyMDEx
// SIG // AhMzAAAAZEeElIbbQRk4AAAAAABkMAkGBSsOAwIaBQCg
// SIG // gb4wGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQwHAYK
// SIG // KwYBBAGCNwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZI
// SIG // hvcNAQkEMRYEFHjXoMgGe0ZFwQ/KSDEnRDYMLJ76MF4G
// SIG // CisGAQQBgjcCAQwxUDBOoAyACgBBAGQAUwBEAEuhPoA8
// SIG // aHR0cDovL2Vkd2ViL3NpdGVzL0lTU0VuZ2luZWVyaW5n
// SIG // L0VuZ0Z1bi9TaXRlUGFnZXMvSG9tZS5hc3B4MA0GCSqG
// SIG // SIb3DQEBAQUABIIBAIOSg8nAG7qtKeeJgkHEye+m+YwN
// SIG // fQRxvgFBiixA3X1CbdbQODhDbvwE7Pl+trOFirNR75fU
// SIG // pQR61mjK9nr+ZRFlOhMZS8PH4f2XJwghWU3AEOc+RDCk
// SIG // oBtbLdVUrR0p47wptisBTOA+CDGnJSut9/ShUkujVQUi
// SIG // ofbT9sXkt/H0XPxLYGCElFB116T+GyW6NJpZkIAlr2SR
// SIG // PzKyAj3t6TlWNK+LUz4PMklsAM/gI3PJuMs00B7KBUh2
// SIG // YoNY7AcYfXsM868ge4WIhLYQ4OeLgdH4vyg3WGPedcgt
// SIG // r7J36Vwf3B0LI6bK3x9o5J92ecYihGceVNuRCLtXvdGt
// SIG // zZFjHY6hggIoMIICJAYJKoZIhvcNAQkGMYICFTCCAhEC
// SIG // AQEwgY4wdzELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldh
// SIG // c2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNV
// SIG // BAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEhMB8GA1UE
// SIG // AxMYTWljcm9zb2Z0IFRpbWUtU3RhbXAgUENBAhMzAAAA
// SIG // ruw/rbaLuS3SAAAAAACuMAkGBSsOAwIaBQCgXTAYBgkq
// SIG // hkiG9w0BCQMxCwYJKoZIhvcNAQcBMBwGCSqGSIb3DQEJ
// SIG // BTEPFw0xNjA3MjkxNzU4MzRaMCMGCSqGSIb3DQEJBDEW
// SIG // BBS7ZbjU9lIOjfzrezPFvhlqnDjXPDANBgkqhkiG9w0B
// SIG // AQUFAASCAQCn/C5spdeTbeyzHT0D2J1BxB0B87Qv2oHU
// SIG // MaUzUmDbky2CSY5drjnHJdOusECGdoCxEhP18F7Qa6Oo
// SIG // +pF3mdfwQyk6q6AHHpwiZFJwZOFOz1Tz0xZBULxTOBOE
// SIG // elAQkTvfGGPnODMMNHE3kiyXuqVh/tqYVSSe6f7cCEkq
// SIG // 7dZYKv+s06hZl4TbRABeulM5VF7SOkIRZThhCz0E7+xu
// SIG // wbC3ZjAbPM+0DFrEdK8vUyoXsWNP26/6yticWtCRb4ML
// SIG // c+0WRFsvCsIX2Cm1rjsJ67k9Pbj79QcayiiIjEnv5ldq
// SIG // RP62tS0Tw/ZvtHXSJiUjcAtEhSjxcIa8yex4vSX5VfGX
// SIG // End signature block
