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
using System.Diagnostics;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Threading.Tasks;

// <SnippetUsing>
using Windows.Media.Core;
using Windows.Media.Playback;

// </SnippetUsing>

using Windows.Media.Streaming.Adaptive;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaSource_RS1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        #region SpeechCues

        private void SpeechCueButton_Click(object sender, RoutedEventArgs e)
        {
            InitSpeechCueScenario();
        }
        // <SnippetSpeechInputText>
        string inputText = "In the lake heading for the mountain, the flea swims";
        // </SnippetSpeechInputText>

        public async void InitSpeechCueScenario()
        {
            // <SnippetSynthesizeSpeech>
            var synthesizer = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Enable word marker generation (false by default). 
            synthesizer.Options.IncludeWordBoundaryMetadata = true;
            synthesizer.Options.IncludeSentenceBoundaryMetadata = true;

            var stream = await synthesizer.SynthesizeTextToStreamAsync(inputText);
            var mediaSource = MediaSource.CreateFromStream(stream, "");
            var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
            // </SnippetSynthesizeSpeech>

            // <SnippetSpeechTracksChanged>
            // Since the tracks are added later we will  
            // monitor the tracks being added and subscribe to the ones of interest 
            mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
            {
                if (args.CollectionChange == CollectionChange.ItemInserted)
                {
                    RegisterMetadataHandlerForSpeech(sender, (int)args.Index);
                }
                else if (args.CollectionChange == CollectionChange.Reset)
                {
                    for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
                    {
                        RegisterMetadataHandlerForSpeech(sender, index);
                    }
                }
            };

            // If tracks were available at source resolution time, itterate through and register: 
            for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
            {
                RegisterMetadataHandlerForSpeech(mediaPlaybackItem, index);
            }
            // </SnippetSpeechTracksChanged>


            // Set the source of the MediaElement or MediaPlayerElement to the MediaPlaybackItem 
            // and start playing the synthesized audio stream. 
            // <SnippetSpeechPlay>
            _mediaPlayer = new MediaPlayer();
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            _mediaPlayer.Source = mediaPlaybackItem;
            _mediaPlayer.Play();
            // </SnippetSpeechPlay>
        }



        // <SnippetRegisterMetadataHandlerForWords>
        private void RegisterMetadataHandlerForSpeech(MediaPlaybackItem item, int index)
        {
            var timedTrack = item.TimedMetadataTracks[index];
            timedTrack.CueEntered += metadata_SpeechCueEntered;
            timedTrack.CueExited += metadata_SpeechCueExited;
            item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);

        }
        // </SnippetRegisterMetadataHandlerForWords>

        // <SnippetSpeechWordCueEntered>
        private void metadata_SpeechCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
            // Check in case there are different tracks and the handler was used for more tracks 
            if (timedMetadataTrack.TimedMetadataKind == TimedMetadataKind.Speech)
            {
                var cue = args.Cue as SpeechCue;
                if (cue != null)
                {
                    if (timedMetadataTrack.Label == "SpeechWord")
                    {
                        // Do something with the cue 
                        System.Diagnostics.Debug.WriteLine($"{cue.StartPositionInInput} - {cue.EndPositionInInput}: {inputText.Substring((int)cue.StartPositionInInput, ((int)cue.EndPositionInInput - (int)cue.StartPositionInInput) + 1)}");
                    }
                }
            }
        }
        // </SnippetSpeechWordCueEntered>
        private void metadata_SpeechCueExited(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
        }

        #endregion

        #region image-based subtitles

        public void InitImageBasedSubtitleScenario()
        {
            // <SnippetImageSubtitleLoadContent>
            var contentUri = new Uri("http://contoso.com/content.mp4");
            var mediaSource = MediaSource.CreateFromUri(contentUri);

            var subUri = new Uri("http://contoso.com/content.sub");
            var idxUri = new Uri("http://contoso.com/content.idx");
            var timedTextSource = TimedTextSource.CreateFromUriWithIndex(subUri, idxUri);
            mediaSource.ExternalTimedTextSources.Add(timedTextSource);

            var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
            // </SnippetImageSubtitleLoadContent>

            // <SnippetImageSubtitleTracksChanged>
            mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
            {
                if (args.CollectionChange == CollectionChange.ItemInserted)
                {
                    RegisterMetadataHandlerForImageSubtitles(sender, (int)args.Index);
                }
                else if (args.CollectionChange == CollectionChange.Reset)
                {
                    for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
                    {
                        if (sender.TimedMetadataTracks[index].TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
                            RegisterMetadataHandlerForImageSubtitles(sender, index);
                    }
                }
            };

            for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
            {
                RegisterMetadataHandlerForImageSubtitles(mediaPlaybackItem, index);
            }
            // </SnippetImageSubtitleTracksChanged>

            // <SnippetImageSubtitlePlay>
            _mediaPlayer = new MediaPlayer();
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            _mediaPlayer.Source = mediaPlaybackItem;
            _mediaPlayer.Play();
            // </SnippetImageSubtitlePlay>
        }

        // <SnippetRegisterMetadataHandlerForImageSubtitles>
        private void RegisterMetadataHandlerForImageSubtitles(MediaPlaybackItem item, int index)
        {
            var timedTrack = item.TimedMetadataTracks[index];
            timedTrack.CueEntered += metadata_ImageSubtitleCueEntered;
            timedTrack.CueExited += metadata_ImageSubtitleCueExited;
            item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);

        }
        // </SnippetRegisterMetadataHandlerForImageSubtitles>

        // <SnippetImageSubtitleCueEntered>
        private async void metadata_ImageSubtitleCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
            // Check in case there are different tracks and the handler was used for more tracks 
            if (timedMetadataTrack.TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
            {
                var cue = args.Cue as ImageCue;
                if (cue != null)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        var source = new SoftwareBitmapSource();
                        await source.SetBitmapAsync(cue.SoftwareBitmap);
                        SubtitleImage.Source = source;
                        SubtitleImage.Width = cue.Extent.Width;
                        SubtitleImage.Height = cue.Extent.Height;
                        SubtitleImage.SetValue(Canvas.LeftProperty, cue.Position.X);
                        SubtitleImage.SetValue(Canvas.TopProperty, cue.Position.Y);
                    });
                }
            }
        }
        // </SnippetImageSubtitleCueEntered>
        private void metadata_ImageSubtitleCueExited(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
        }
        #endregion

        #region chapter cues

        public void InitChapterCueScenario()
        {
            // <SnippetChapterCueLoadContent>
            var contentUri = new Uri("http://contoso.com/content.mp4");
            var mediaSource = MediaSource.CreateFromUri(contentUri);
            var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
            // </SnippetChapterCueLoadContent>

            // <SnippetChapterCueTracksChanged>
            mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
            {
                if (args.CollectionChange == CollectionChange.ItemInserted)
                {
                    RegisterMetadataHandlerForChapterCues(sender, (int)args.Index);
                }
                else if (args.CollectionChange == CollectionChange.Reset)
                {
                    for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
                    {
                        if (sender.TimedMetadataTracks[index].TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
                            RegisterMetadataHandlerForChapterCues(sender, index);
                    }
                }
            };

            for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
            {
                RegisterMetadataHandlerForChapterCues(mediaPlaybackItem, index);
            }
            // </SnippetChapterCueTracksChanged>

            // <SnippetChapterCuePlay>
            _mediaPlayer = new MediaPlayer();
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            _mediaPlayer.Source = mediaPlaybackItem;
            _mediaPlayer.Play();
            // </SnippetChapterCuePlay>
        }

        // <SnippetRegisterMetadataHandlerForChapterCues>
        private void RegisterMetadataHandlerForChapterCues(MediaPlaybackItem item, int index)
        {
            var timedTrack = item.TimedMetadataTracks[index];
            timedTrack.CueEntered += metadata_ChapterCueEntered;
            timedTrack.CueExited += metadata_ChapterCueExited;
            item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);
        }
        // </SnippetRegisterMetadataHandlerForChapterCues>

        // <SnippetChapterCueEntered>
        private async void metadata_ChapterCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
            // Check in case there are different tracks and the handler was used for more tracks 
            if (timedMetadataTrack.TimedMetadataKind == TimedMetadataKind.Chapter)
            {
                var cue = args.Cue as ChapterCue;
                if (cue != null)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        ChapterTitleTextBlock.Text = cue.Title;
                    });
                }
            }
        }
        // </SnippetChapterCueEntered>
        private void metadata_ChapterCueExited(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
        }
        // <SnippetGoToNextChapter>
        private void GoToNextChapter(MediaPlayer player, MediaPlaybackItem item)
        {
            // Find the chapters track if one exists
            TimedMetadataTrack chapterTrack = item.TimedMetadataTracks.FirstOrDefault(track => track.TimedMetadataKind == TimedMetadataKind.Chapter);
            if (chapterTrack == null)
            {
                return;
            }

            // Find the first chapter that starts after current playback position
            TimeSpan currentPosition = player.PlaybackSession.Position;
            foreach (ChapterCue cue in chapterTrack.Cues)
            {
                if (cue.StartTime > currentPosition)
                {
                    // Change player position to chapter start time
                    player.PlaybackSession.Position = cue.StartTime;

                    // Display chapter name
                    ChapterTitleTextBlock.Text = cue.Title;
                    break;
                }
            }
        }
        // </SnippetGoToNextChapter>
        private void DisplayChapter(String title)
        {
            // Display chapter name
        }

        #endregion




        #region Extended M3U manifest comments

        public async void InitEXTM3UCueScenario()
        {
            // <SnippetEXTM3ULoadContent>
            AdaptiveMediaSourceCreationResult result =
                await AdaptiveMediaSource.CreateFromUriAsync(new Uri("http://contoso.com/playlist.m3u"));

            if (result.Status != AdaptiveMediaSourceCreationStatus.Success)
            {
                // TODO: Handle adaptive media source creation errors.
                return;
            }
            var mediaSource = MediaSource.CreateFromAdaptiveMediaSource(result.MediaSource);
            var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
            // </SnippetEXTM3ULoadContent>

            // <SnippetEXTM3UCueTracksChanged>
            mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
            {
                if (args.CollectionChange == CollectionChange.ItemInserted)
                {
                    RegisterMetadataHandlerForEXTM3UCues(sender, (int)args.Index);
                }
                else if (args.CollectionChange == CollectionChange.Reset)
                {
                    for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
                    {
                        if (sender.TimedMetadataTracks[index].TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
                            RegisterMetadataHandlerForEXTM3UCues(sender, index);
                    }
                }
            };

            for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
            {
                RegisterMetadataHandlerForEXTM3UCues(mediaPlaybackItem, index);
            }
            // </SnippetEXTM3UCueTracksChanged>

            // <SnippetEXTM3UCuePlay>
            _mediaPlayer = new MediaPlayer();
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            _mediaPlayer.Source = mediaPlaybackItem;
            _mediaPlayer.Play();
            // </SnippetEXTM3UCuePlay>
        }

        // <SnippetRegisterMetadataHandlerForEXTM3UCues>
        private void RegisterMetadataHandlerForEXTM3UCues(MediaPlaybackItem item, int index)
        {
            var timedTrack = item.TimedMetadataTracks[index];
            var dispatchType = timedTrack.DispatchType;

            if (String.Equals(dispatchType, "EXTM3U", StringComparison.OrdinalIgnoreCase))
            {
                timedTrack.Label = "EXTM3U comments";
                timedTrack.CueEntered += metadata_EXTM3UCueEntered;
                timedTrack.CueExited += metadata_EXTM3UCueExited;
                item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);
            }
        }
        // </SnippetRegisterMetadataHandlerForEXTM3UCues>

        // <SnippetEXTM3UCueEntered>
        private void metadata_EXTM3UCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
            var dataCue = args.Cue as DataCue;
            if (dataCue != null && dataCue.Data != null)
            {
                // The payload is a UTF-16 Little Endian null-terminated string.
                // It is any comment line in a manifest that is not part of the HLS spec.
                var dr = Windows.Storage.Streams.DataReader.FromBuffer(dataCue.Data);
                dr.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE;
                var m3uComment = dr.ReadString(dataCue.Data.Length / 2 - 1);
                System.Diagnostics.Debug.WriteLine(m3uComment);
            }
        }
        // </SnippetEXTM3UCueEntered>
        private void metadata_EXTM3UCueExited(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
        }

        #endregion

        #region ID3 Tags

        public async void InitID3CueScenario()
        {
            // <SnippetID3LoadContent>
            AdaptiveMediaSourceCreationResult result =
                await AdaptiveMediaSource.CreateFromUriAsync(new Uri("http://contoso.com/playlist.m3u"));

            if (result.Status != AdaptiveMediaSourceCreationStatus.Success)
            {
                // TODO: Handle adaptive media source creation errors.
                return;
            }
            var mediaSource = MediaSource.CreateFromAdaptiveMediaSource(result.MediaSource);
            var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
            // </SnippetID3LoadContent>

            // <SnippetID3CueTracksChanged>
            mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
            {
                if (args.CollectionChange == CollectionChange.ItemInserted)
                {
                    RegisterMetadataHandlerForID3Cues(sender, (int)args.Index);
                }
                else if (args.CollectionChange == CollectionChange.Reset)
                {
                    for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
                    {
                        if (sender.TimedMetadataTracks[index].TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
                            RegisterMetadataHandlerForID3Cues(sender, index);
                    }
                }
            };

            for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
            {
                RegisterMetadataHandlerForID3Cues(mediaPlaybackItem, index);
            }
            // </SnippetID3CueTracksChanged>

            // <SnippetID3CuePlay>
            _mediaPlayer = new MediaPlayer();
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            _mediaPlayer.Source = mediaPlaybackItem;
            _mediaPlayer.Play();
            // </SnippetID3CuePlay>
        }

        // <SnippetRegisterMetadataHandlerForID3Cues>
        private void RegisterMetadataHandlerForID3Cues(MediaPlaybackItem item, int index)
        {
            var timedTrack = item.TimedMetadataTracks[index];
            var dispatchType = timedTrack.DispatchType;

            if (String.Equals(dispatchType, "15260DFFFF49443320FF49443320000F", StringComparison.OrdinalIgnoreCase))
            {
                timedTrack.Label = "ID3 tags";
                timedTrack.CueEntered += metadata_ID3CueEntered;
                timedTrack.CueExited += metadata_ID3CueExited;
                item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);
            }
        }
        // </SnippetRegisterMetadataHandlerForID3Cues>

        // <SnippetID3CueEntered>
        private void metadata_ID3CueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
            var dataCue = args.Cue as DataCue;
            if (dataCue != null && dataCue.Data != null)
            {
                // The payload is the raw ID3 bytes found in a TS stream
                // Ref: http://id3.org/id3v2.4.0-structure
                var dr = Windows.Storage.Streams.DataReader.FromBuffer(dataCue.Data);
                var header_ID3 = dr.ReadString(3);
                var header_version_major = dr.ReadByte();
                var header_version_minor = dr.ReadByte();
                var header_flags = dr.ReadByte();
                var header_tagSize = dr.ReadUInt32();

                System.Diagnostics.Debug.WriteLine($"ID3 tag data: major {header_version_major}, minor: {header_version_minor}");
            }
        }
        // </SnippetID3CueEntered>
        private void metadata_ID3CueExited(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
        }

        #endregion

        #region Emsg Tags

        public async void InitEmsgCueScenario()
        {
            // <SnippetEmsgLoadContent>
            AdaptiveMediaSourceCreationResult result =
                await AdaptiveMediaSource.CreateFromUriAsync(new Uri("http://contoso.com/playlist.m3u"));

            if (result.Status != AdaptiveMediaSourceCreationStatus.Success)
            {
                // TODO: Handle adaptive media source creation errors.
                return;
            }
            var mediaSource = MediaSource.CreateFromAdaptiveMediaSource(result.MediaSource);
            var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
            // </SnippetEmsgLoadContent>

            // <SnippetEmsgCueTracksChanged>
            mediaPlaybackItem.TimedMetadataTracksChanged += (MediaPlaybackItem sender, IVectorChangedEventArgs args) =>
            {
                if (args.CollectionChange == CollectionChange.ItemInserted)
                {
                    RegisterMetadataHandlerForEmsgCues(sender, (int)args.Index);
                }
                else if (args.CollectionChange == CollectionChange.Reset)
                {
                    for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
                    {
                        if (sender.TimedMetadataTracks[index].TimedMetadataKind == TimedMetadataKind.ImageSubtitle)
                            RegisterMetadataHandlerForEmsgCues(sender, index);
                    }
                }
            };

            for (int index = 0; index < mediaPlaybackItem.TimedMetadataTracks.Count; index++)
            {
                RegisterMetadataHandlerForEmsgCues(mediaPlaybackItem, index);
            }
            // </SnippetEmsgCueTracksChanged>

            // <SnippetEmsgCuePlay>
            _mediaPlayer = new MediaPlayer();
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            _mediaPlayer.Source = mediaPlaybackItem;
            _mediaPlayer.Play();
            // </SnippetEmsgCuePlay>
        }

        // <SnippetRegisterMetadataHandlerForEmsgCues>
        private void RegisterMetadataHandlerForEmsgCues(MediaPlaybackItem item, int index)
        {
            var timedTrack = item.TimedMetadataTracks[index];
            var dispatchType = timedTrack.DispatchType;

            if (String.Equals(dispatchType, "emsg:mp4", StringComparison.OrdinalIgnoreCase))
            {
                timedTrack.Label = "mp4 Emsg boxes";
                timedTrack.CueEntered += metadata_EmsgCueEntered;
                timedTrack.CueExited += metadata_EmsgCueExited;
                item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);
            }
        }
        // </SnippetRegisterMetadataHandlerForEmsgCues>

        // <SnippetLastProcessedAdId>
        List<uint> processedAdIds = new List<uint>();
        // <SnippetLastProcessedAdId>

        // <SnippetEmsgCueEntered>
        private void metadata_EmsgCueEntered(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
            var dataCue = args.Cue as DataCue;
            if (dataCue != null)
            {
                string scheme_id_uri = string.Empty;
                string value = string.Empty;
                UInt32 timescale = (UInt32)TimeSpan.TicksPerSecond;
                UInt32 presentation_time_delta = (UInt32)dataCue.StartTime.Ticks;
                UInt32 event_duration = (UInt32)dataCue.Duration.Ticks;
                UInt32 id = 0;
                Byte[] message_data = null;

                const string scheme_id_uri_key = "emsg:scheme_id_uri";
                object propValue = null;
                dataCue.Properties.TryGetValue(scheme_id_uri_key, out propValue);
                scheme_id_uri = propValue != null ? (string)propValue : "";

                const string value_key = "emsg:value";
                propValue = null;
                dataCue.Properties.TryGetValue(value_key, out propValue);
                value = propValue != null ? (string)propValue : "";

                const string timescale_key = "emsg:timescale";
                propValue = null;
                dataCue.Properties.TryGetValue(timescale_key, out propValue);
                timescale = propValue != null ? (UInt32)propValue : timescale;

                const string presentation_time_delta_key = "emsg:presentation_time_delta";
                propValue = null;
                dataCue.Properties.TryGetValue(presentation_time_delta_key, out propValue);
                presentation_time_delta = propValue != null ? (UInt32)propValue : presentation_time_delta;

                const string event_duration_key = "emsg:event_duration";
                propValue = null;
                dataCue.Properties.TryGetValue(event_duration_key, out propValue);
                event_duration = propValue != null ? (UInt32)propValue : event_duration;

                const string id_key = "emsg:id";
                propValue = null;
                dataCue.Properties.TryGetValue(id_key, out propValue);
                id = propValue != null ? (UInt32)propValue : 0;

                System.Diagnostics.Debug.WriteLine($"Label: {timedMetadataTrack.Label}, Id: {dataCue.Id}, StartTime: {dataCue.StartTime}, Duration: {dataCue.Duration}");
                System.Diagnostics.Debug.WriteLine($"scheme_id_uri: {scheme_id_uri}, value: {value}, timescale: {timescale}, presentation_time_delta: {presentation_time_delta}, event_duration: {event_duration}, id: {id}");

                if (dataCue.Data != null)
                {
                    var dr = Windows.Storage.Streams.DataReader.FromBuffer(dataCue.Data);

                    // Check if this is a SCTE ad message:
                    // Ref:  http://dashif.org/identifiers/event-schemes/
                    if (scheme_id_uri.ToLower() == "urn:scte:scte35:2013:xml")
                    {
                        // SCTE recommends publishing emsg more than once, so we avoid reprocessing the same message id:
                        if (!processedAdIds.Contains(id))
                        {
                            processedAdIds.Add(id);
                            dr.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                            var scte35payload = dr.ReadString(dataCue.Data.Length);
                            System.Diagnostics.Debug.WriteLine($", message_data: {scte35payload}");
                            // TODO: ScheduleAdFromScte35Payload(timedMetadataTrack, presentation_time_delta, timescale, event_duration, scte35payload);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"This emsg.Id, {id}, has already been processed.");
                        }
                    }
                    else
                    {
                        message_data = new byte[dataCue.Data.Length];
                        dr.ReadBytes(message_data);
                        // TODO: Use the 'emsg' bytes for something useful. 
                        System.Diagnostics.Debug.WriteLine($", message_data.Length: {message_data.Length}");
                    }
                }
            }
        }
        // </SnippetEmsgCueEntered>
        private void metadata_EmsgCueExited(TimedMetadataTrack timedMetadataTrack, MediaCueEventArgs args)
        {
        }

        private void ScheduleAdFromScte35Payload(TimedMetadataTrack timedMetadataTrack, uint presentation_time_delta, uint timescale, uint event_duration, string scte35payload)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
