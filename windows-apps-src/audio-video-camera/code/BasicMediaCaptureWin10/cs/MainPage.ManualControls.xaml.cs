using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System;
using Windows.Media.Devices;
using System.Linq;
using Windows.UI.Xaml.Input;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.MediaProperties;
using Windows.Graphics.Display;
using Windows.Media.Capture;

namespace BasicCameraWin10
{
    public sealed partial class MainPage : Page
    {
        bool _settingUpUi;

        #region EV
        private void UpdateEvControlCapabilities()
        {
            //<SnippetEvControl>
            var exposureCompensationControl = _mediaCapture.VideoDeviceController.ExposureCompensationControl;

            if (exposureCompensationControl.Supported)
            {
                EvSlider.Visibility = Visibility.Visible;
                EvSlider.Minimum = exposureCompensationControl.Min;
                EvSlider.Maximum = exposureCompensationControl.Max;
                EvSlider.StepFrequency = exposureCompensationControl.Step;

                EvSlider.ValueChanged -= EvSlider_ValueChanged;
                EvSlider.Value = exposureCompensationControl.Value;
                EvSlider.ValueChanged += EvSlider_ValueChanged;
            }
            else
            {
                EvSlider.Visibility = Visibility.Collapsed;
            }
            //</SnippetEvControl>
        }
        //<SnippetEvValueChanged>
        private async void EvSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            var value = (sender as Slider).Value;
            await _mediaCapture.VideoDeviceController.ExposureCompensationControl.SetValueAsync((float)value);
        }
        //</SnippetEvValueChanged>

        /// <summary>
        /// Converts the number of steps into a (mixed) fraction based on the step size, when the value used is steps * stepSize
        /// </summary>
        /// <param name="steps">Number of steps</param>
        /// <param name="stepSize">The size of one step</param>
        /// <returns></returns>
        //<SnippetEvStepCountToString>
        private static string EvStepCountToString(int steps, float stepSize)
        {
            if (stepSize <= 0) throw new ArgumentOutOfRangeException(nameof(stepSize), "Parameter must be a positive value");

            int stepsPerUnit = (int)Math.Round(1 / stepSize);
            if (steps == 0) return "0";

            string sign = steps < 0 ? "-" : "+";
            int whole = (int)Math.Abs(steps * stepSize);
            int remainder = (stepsPerUnit >= 1) ? Math.Abs(steps) % stepsPerUnit : 0;

            if (remainder == 0) return sign + whole.ToString();

            string fraction = remainder.ToString() + "/" + stepsPerUnit.ToString();

            if (whole == 0) return sign + fraction;
            return sign + whole.ToString() + " " + fraction;
        }
        //</SnippetEvStepCountToString>

        #endregion

        #region flash
        
        private void UpdateFlashControlCapabilities()
        {
            //<SnippetFlashControl>
            var flashControl = _mediaCapture.VideoDeviceController.FlashControl;

            if (flashControl.Supported)
            {
                FlashAutoRadioButton.Visibility = Visibility.Visible;
                FlashOnRadioButton.Visibility = Visibility.Visible;
                FlashOffRadioButton.Visibility = Visibility.Visible;

                FlashAutoRadioButton.IsChecked = true;

                if (flashControl.RedEyeReductionSupported)
                {
                    RedEyeFlashCheckBox.Visibility = Visibility.Visible;
                }

                // Video light is not strictly part of flash, but users might expect to find it there
                if (_mediaCapture.VideoDeviceController.TorchControl.Supported)
                {
                    TorchCheckBox.Visibility = Visibility.Visible;
                }
            }
            else
            {
                FlashAutoRadioButton.Visibility = Visibility.Collapsed;
                FlashOnRadioButton.Visibility = Visibility.Collapsed;
                FlashOffRadioButton.Visibility = Visibility.Collapsed;
            }

            //</SnippetFlashControl>
        }
        //<SnippetFlashRadioButtons>
        private void FlashOnRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _mediaCapture.VideoDeviceController.FlashControl.Enabled = true;
            _mediaCapture.VideoDeviceController.FlashControl.Auto = false;
        }

        private void FlashAutoRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _mediaCapture.VideoDeviceController.FlashControl.Enabled = true;
            _mediaCapture.VideoDeviceController.FlashControl.Auto = true;
        }
        
        private void FlashOffRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _mediaCapture.VideoDeviceController.FlashControl.Enabled = false;
        }
        //</SnippetFlashRadioButtons>

        //<SnippetRedEye>
        private void RedEyeFlashCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            _mediaCapture.VideoDeviceController.FlashControl.RedEyeReduction = (RedEyeFlashCheckBox.IsChecked == true);
        }
        //</SnippetRedEye>

        //<SnippetTorch>
        private void TorchCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            _mediaCapture.VideoDeviceController.TorchControl.Enabled = (TorchCheckBox.IsChecked == true);

            if(! (_isPreviewing && _isRecording))
            {
                System.Diagnostics.Debug.WriteLine("Torch may not emit light if preview and video capture are not running.");
            }
        }
        //</SnippetTorch>
        #endregion

        #region ISO
        //<SnippetIsoControl>
        private void UpdateIsoControlCapabilities()
        {
            var isoSpeedControl = _mediaCapture.VideoDeviceController.IsoSpeedControl;

            if (isoSpeedControl.Supported)
            {
                IsoAutoCheckBox.Visibility = Visibility.Visible;
                IsoSlider.Visibility = Visibility.Visible;

                IsoAutoCheckBox.IsChecked = isoSpeedControl.Auto;
                
                IsoSlider.Minimum = isoSpeedControl.Min;
                IsoSlider.Maximum = isoSpeedControl.Max;
                IsoSlider.StepFrequency = isoSpeedControl.Step;

                IsoSlider.ValueChanged -= IsoSlider_ValueChanged;
                IsoSlider.Value = isoSpeedControl.Value;
                IsoSlider.ValueChanged += IsoSlider_ValueChanged;
            }
            else
            {
                IsoAutoCheckBox.Visibility = Visibility.Collapsed;
                IsoSlider.Visibility = Visibility.Collapsed;
            }
        }
        //</SnippetIsoControl>
        //<SnippetIsoSlider>
        private async void IsoSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            var value = (sender as Slider).Value;
            await _mediaCapture.VideoDeviceController.IsoSpeedControl.SetValueAsync((uint)value);
        }
        //</SnippetIsoSlider>
        //<SnippetIsoCheckBox>
        private async void IsoAutoCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            var autoIso = (sender as CheckBox).IsChecked == true;

            if (autoIso)
            {
                await _mediaCapture.VideoDeviceController.IsoSpeedControl.SetAutoAsync();
            }
            else
            {
                await _mediaCapture.VideoDeviceController.IsoSpeedControl.SetValueAsync((uint)IsoSlider.Value);
            }
        }
        //</SnippetIsoCheckBox>
        #endregion

        #region Exposure
        private void UpdateExposureControlCapabilities()
        {
            //<SnippetExposureControl>
            var exposureControl = _mediaCapture.VideoDeviceController.ExposureControl;

            if (exposureControl.Supported)
            {
                ExposureAutoCheckBox.Visibility = Visibility.Visible;
                ExposureSlider.Visibility = Visibility.Visible;

                ExposureAutoCheckBox.IsChecked = exposureControl.Auto;

                ExposureSlider.Minimum = exposureControl.Min.Ticks;
                ExposureSlider.Maximum = exposureControl.Max.Ticks;
                ExposureSlider.StepFrequency = exposureControl.Step.Ticks;

                ExposureSlider.ValueChanged -= ExposureSlider_ValueChanged;
                var value = exposureControl.Value;
                ExposureSlider.Value = value.Ticks;
                ExposureSlider.ValueChanged += ExposureSlider_ValueChanged;
            }
            else
            {
                ExposureAutoCheckBox.Visibility = Visibility.Collapsed;
                ExposureSlider.Visibility = Visibility.Collapsed;
            }
            //</SnippetExposureControl>
        }
        //<SnippetExposureSlider>
        private async void ExposureSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            var value = TimeSpan.FromTicks((long)(sender as Slider).Value);
            await _mediaCapture.VideoDeviceController.ExposureControl.SetValueAsync(value);
        }
        //</SnippetExposureSlider>

        //<SnippetExposureCheckBox>
        private async void ExposureCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if(! _isPreviewing)
            {
                // Auto exposure only supported while preview stream is running.
                return;
            }

            var autoExposure = ((sender as CheckBox).IsChecked == true);
            await _mediaCapture.VideoDeviceController.ExposureControl.SetAutoAsync(autoExposure);
        }
        //</SnippetExposureCheckBox>
        #endregion

        #region whitebalance

        private void UpdateWbControlCapabilities()
        {
            //<SnippetWhiteBalance>
            var whiteBalanceControl = _mediaCapture.VideoDeviceController.WhiteBalanceControl;

            if (whiteBalanceControl.Supported)
            {
                WbSlider.Visibility = Visibility.Visible;
                WbComboBox.Visibility = Visibility.Visible;

                if (WbComboBox.ItemsSource == null)
                {
                    WbComboBox.ItemsSource = Enum.GetValues(typeof(ColorTemperaturePreset)).Cast<ColorTemperaturePreset>();
                }

                WbComboBox.SelectedItem = whiteBalanceControl.Preset;

                if (whiteBalanceControl.Max - whiteBalanceControl.Min > whiteBalanceControl.Step)
                {
 
                    WbSlider.Minimum = whiteBalanceControl.Min;
                    WbSlider.Maximum = whiteBalanceControl.Max;
                    WbSlider.StepFrequency = whiteBalanceControl.Step;

                    WbSlider.ValueChanged -= WbSlider_ValueChanged;
                    WbSlider.Value = whiteBalanceControl.Value;
                    WbSlider.ValueChanged += WbSlider_ValueChanged;
                }
                else
                {
                    WbSlider.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                WbSlider.Visibility = Visibility.Collapsed;
                WbComboBox.Visibility = Visibility.Collapsed;
            }
            //</SnippetWhiteBalance>
        }
        //<SnippetWhiteBalanceComboBox>
        private async void WbComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!_isPreviewing)
            {
                // Do not set white balance values unless the preview stream is running.
                return;
            }

            var selected = (ColorTemperaturePreset)WbComboBox.SelectedItem;
            WbSlider.IsEnabled = (selected == ColorTemperaturePreset.Manual);
            await _mediaCapture.VideoDeviceController.WhiteBalanceControl.SetPresetAsync(selected);

        }
        //</SnippetWhiteBalanceComboBox>

        //<SnippetWhiteBalanceSlider>
        private async void WbSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (!_isPreviewing)
            {
                // Do not set white balance values unless the preview stream is running.
                return;
            }

            var value = (sender as Slider).Value;
            await _mediaCapture.VideoDeviceController.WhiteBalanceControl.SetValueAsync((uint)value);
        }
        //</SnippetWhiteBalanceSlider>
        #endregion

        #region zoom

       
        private void UpdateZoomControlCapabilities()
        {
            //<SnippetZoomControl>
            var zoomControl = _mediaCapture.VideoDeviceController.ZoomControl;

            if (zoomControl.Supported)
            {
                ZoomSlider.Visibility = Visibility.Visible;

                ZoomSlider.Minimum = zoomControl.Min;
                ZoomSlider.Maximum = zoomControl.Max;
                ZoomSlider.StepFrequency = zoomControl.Step;
                
                ZoomSlider.ValueChanged -= ZoomSlider_ValueChanged;
                ZoomSlider.Value = zoomControl.Value;
                ZoomSlider.ValueChanged += ZoomSlider_ValueChanged;
            }
            else
            {
                ZoomSlider.Visibility = Visibility.Collapsed;
            }
            //</SnippetZoomControl>
        }
        //<SnippetZoomSlider>
        private void ZoomSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            var level = (float)ZoomSlider.Value;
            var settings = new ZoomSettings { Value = level };

            var zoomControl = _mediaCapture.VideoDeviceController.ZoomControl;
            if (zoomControl.SupportedModes.Contains(ZoomTransitionMode.Smooth))
            {
                settings.Mode = ZoomTransitionMode.Smooth;
            }
            else
            {
                settings.Mode = zoomControl.SupportedModes.First();
            }

            zoomControl.Configure(settings);
        }
        //</SnippetZoomSlider>

        #endregion

        #region Focus

        private void CAF()
        {
            //<SnippetCAF>
            var focusControl = _mediaCapture.VideoDeviceController.FocusControl;

            if (focusControl.Supported)
            {
                CafFocusRadioButton.Visibility = focusControl.SupportedFocusModes.Contains(FocusMode.Continuous) 
                    ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                CafFocusRadioButton.Visibility = Visibility.Collapsed;
            }
            //</SnippetCAF>
        }

        //<snippetCafFocusRadioButton>
        private async void CafFocusRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(! _isPreviewing)
            {
                // Autofocus only supported while preview stream is running.
                return;
            }

            var focusControl = _mediaCapture.VideoDeviceController.FocusControl;
            await focusControl.UnlockAsync();
            var settings = new FocusSettings { Mode = FocusMode.Continuous, AutoFocusRange = AutoFocusRange.FullRange };
            focusControl.Configure(settings);
            await focusControl.FocusAsync();
        }
        //</snippetCafFocusRadioButton>


        private void TapFocus()
        {
            //<SnippetTapFocus>
            var focusControl = _mediaCapture.VideoDeviceController.FocusControl;

            if (focusControl.Supported)
            {
                TapFocusRadioButton.Visibility = (_mediaCapture.VideoDeviceController.RegionsOfInterestControl.AutoFocusSupported &&
                                                  _mediaCapture.VideoDeviceController.RegionsOfInterestControl.MaxRegions > 0) 
                                                  ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                TapFocusRadioButton.Visibility = Visibility.Collapsed;
            }
            //</SnippetTapFocus>
        }

        //<snippetTapFocusRadioButton>
        private async void TapFocusRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Lock focus in case Continuous Autofocus was active when switching to Tap-to-focus
            var focusControl = _mediaCapture.VideoDeviceController.FocusControl;
            await focusControl.LockAsync();
            // Wait for user tap
        }
        //</snippetTapFocusRadioButton>


        //<SnippetIsFocused>
        bool _isFocused = false;
        //</SnippetIsFocused>

        //<SnippetTapFocusPreviewControl>
        private async void PreviewControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!_isPreviewing || (TapFocusRadioButton.IsChecked != true)) return;

            if (!_isFocused && _mediaCapture.VideoDeviceController.FocusControl.FocusState != MediaCaptureFocusState.Searching)
            {
                var smallEdge = Math.Min(Window.Current.Bounds.Width, Window.Current.Bounds.Height);

                // Choose to make the focus rectangle 1/4th the length of the shortest edge of the window
                var size = new Size(smallEdge / 4, smallEdge / 4);
                var position = e.GetPosition(sender as UIElement);

                // Note that at this point, a rect at "position" with size "size" could extend beyond the preview area. The following method will reposition the rect if that is the case
                await TapToFocus(position, size);
            }
            else
            {
                await TapUnfocus();
            }
        }
        //</SnippetTapFocusPreviewControl>


        /// <summary>
        /// Focus the camera on the given rectangle of the preview, defined by the position and size parameters, in UI coordinates within the CaptureElement
        /// </summary>
        /// <param name="position">The position of the tap, to become the center of the focus rectangle</param>
        /// <param name="size">the size of the rectangle around the tap</param>
        /// <returns></returns>
        /// 
        //<snippetTapToFocus>
        public async Task TapToFocus(Point position, Size size)
        {
            _isFocused = true;

            var previewRect = GetPreviewStreamRectInControl();
            var focusPreview = ConvertUiTapToPreviewRect(position, size, previewRect);

            // Note that this Region Of Interest could be configured to also calculate exposure 
            // and white balance within the region
            var regionOfInterest = new RegionOfInterest
            {
                AutoFocusEnabled = true,
                BoundsNormalized = true,
                Bounds = focusPreview,
                Type = RegionOfInterestType.Unknown,
                Weight = 100,
            };


            var focusControl = _mediaCapture.VideoDeviceController.FocusControl;
            var focusRange = focusControl.SupportedFocusRanges.Contains(AutoFocusRange.FullRange) ? AutoFocusRange.FullRange : focusControl.SupportedFocusRanges.FirstOrDefault();
            var focusMode = focusControl.SupportedFocusModes.Contains(FocusMode.Single) ? FocusMode.Single : focusControl.SupportedFocusModes.FirstOrDefault();
            var settings = new FocusSettings { Mode = focusMode, AutoFocusRange = focusRange };
            focusControl.Configure(settings);

            var roiControl = _mediaCapture.VideoDeviceController.RegionsOfInterestControl;
            await roiControl.SetRegionsAsync(new[] { regionOfInterest }, true);

            await focusControl.FocusAsync();
        }
        //</snippetTapToFocus>

        //<snippetTapUnfocus>
        private async Task TapUnfocus()
        {
            _isFocused = false;

            var roiControl = _mediaCapture.VideoDeviceController.RegionsOfInterestControl;
            await roiControl.ClearRegionsAsync();

            var focusControl = _mediaCapture.VideoDeviceController.FocusControl;
            await focusControl.FocusAsync();
        }
        //</snippetTapUnfocus>

        /// <summary>
        /// Calculates the size and location of the rectangle that contains the preview stream within the preview control, when the scaling mode is Uniform
        /// </summary>
        /// <param name="previewResolution">The resolution at which the preview is running</param>
        /// <param name="previewControl">The control that is displaying the preview using Uniform as the scaling mode</param>
        /// <param name="displayOrientation">The orientation of the display, to account for device rotation and changing of the CaptureElement display ratio compared to the camera stream</param>
        /// <returns></returns>
        /// 
        //<SnippetGetPreviewStreamRectInControl>
        public Rect GetPreviewStreamRectInControl()
        {
            var result = new Rect();

            var previewResolution = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview) as VideoEncodingProperties;

            // In case this function is called before everything is initialized correctly, return an empty result
            if (PreviewControl == null || PreviewControl.ActualHeight < 1 || PreviewControl.ActualWidth < 1 ||
                previewResolution == null || previewResolution.Height == 0 || previewResolution.Width == 0)
            {
                return result;
            }

            var streamWidth = previewResolution.Width;
            var streamHeight = previewResolution.Height;

            // For portrait orientations, the width and height need to be swapped
            if (_displayOrientation == DisplayOrientations.Portrait || _displayOrientation == DisplayOrientations.PortraitFlipped)
            {
                streamWidth = previewResolution.Height;
                streamHeight = previewResolution.Width;
            }

            // Start by assuming the preview display area in the control spans the entire width and height both (this is corrected in the next if for the necessary dimension)
            result.Width = PreviewControl.ActualWidth;
            result.Height = PreviewControl.ActualHeight;

            // If UI is "wider" than preview, letterboxing will be on the sides
            if ((PreviewControl.ActualWidth / PreviewControl.ActualHeight > streamWidth / (double)streamHeight))
            {
                var scale = PreviewControl.ActualHeight / streamHeight;
                var scaledWidth = streamWidth * scale;

                result.X = (PreviewControl.ActualWidth - scaledWidth) / 2.0;
                result.Width = scaledWidth;
            }
            else // Preview stream is "wider" than UI, so letterboxing will be on the top+bottom
            {
                var scale = PreviewControl.ActualWidth / streamWidth;
                var scaledHeight = streamHeight * scale;

                result.Y = (PreviewControl.ActualHeight - scaledHeight) / 2.0;
                result.Height = scaledHeight;
            }

            return result;
        }
        //</SnippetGetPreviewStreamRectInControl>

        /// <summary>
        /// Applies the necessary rotation to a tap on a CaptureElement (with Stretch mode set to Uniform) to account for device orientation
        /// </summary>
        /// <param name="tap">The location, in UI coordinates, of the user tap</param>
        /// <param name="size">The size, in UI coordinates, of the desired focus rectangle</param>
        /// <param name="previewRect">The area within the CaptureElement that is actively showing the preview, and is not part of the letterboxed area</param>
        /// <returns>A Rect that can be passed to the MediaCapture Focus and RegionsOfInterest APIs, with normalized bounds in the orientation of the native stream</returns>
        //<snippetConvertUiTapToPreviewRect>
        private Rect ConvertUiTapToPreviewRect(Point tap, Size size, Rect previewRect)
        {
            // Adjust for the resulting focus rectangle to be centered around the position
            double left = tap.X - size.Width / 2, top = tap.Y - size.Height / 2;

            // Get the information about the active preview area within the CaptureElement (in case it's letterboxed)
            double previewWidth = previewRect.Width, previewHeight = previewRect.Height;
            double previewLeft = previewRect.Left, previewTop = previewRect.Top;

            // Transform the left and top of the tap to account for rotation
            switch (_displayOrientation)
            {
                case DisplayOrientations.Portrait:
                    var tempLeft = left;

                    left = top;
                    top = previewRect.Width - tempLeft;
                    break;
                case DisplayOrientations.LandscapeFlipped:
                    left = previewRect.Width - left;
                    top = previewRect.Height - top;
                    break;
                case DisplayOrientations.PortraitFlipped:
                    var tempTop = top;

                    top = left;
                    left = previewRect.Width - tempTop;
                    break;
            }

            // For portrait orientations, the information about the active preview area needs to be rotated
            if (_displayOrientation == DisplayOrientations.Portrait || _displayOrientation == DisplayOrientations.PortraitFlipped)
            {
                previewWidth = previewRect.Height;
                previewHeight = previewRect.Width;
                previewLeft = previewRect.Top;
                previewTop = previewRect.Left;
            }

            // Normalize width and height of the focus rectangle
            var width = size.Width / previewWidth;
            var height = size.Height / previewHeight;

            // Shift rect left and top to be relative to just the active preview area
            left -= previewLeft;
            top -= previewTop;

            // Normalize left and top
            left /= previewWidth;
            top /= previewHeight;

            // Ensure rectangle is fully contained within the active preview area horizontally
            left = Math.Max(left, 0);
            left = Math.Min(1 - width, left);

            // Ensure rectangle is fully contained within the active preview area vertically
            top = Math.Max(top, 0);
            top = Math.Min(1 - height, top);

            // Create and return resulting rectangle
            return new Rect(left, top, width, height);
        }
        //</snippetConvertUiTapToPreviewRect>

        private void UpdateFocusControlCapabilities()
        {
            //<SnippetFocus>
            var focusControl = _mediaCapture.VideoDeviceController.FocusControl;

            if (focusControl.Supported)
            {
                FocusSlider.Visibility = Visibility.Visible;
                ManualFocusRadioButton.Visibility = Visibility.Visible;

                FocusSlider.Minimum = focusControl.Min;
                FocusSlider.Maximum = focusControl.Max;
                FocusSlider.StepFrequency = focusControl.Step;
                

                FocusSlider.ValueChanged -= FocusSlider_ValueChanged;
                FocusSlider.Value = focusControl.Value;
                FocusSlider.ValueChanged += FocusSlider_ValueChanged;
            }
            else
            {
                FocusSlider.Visibility = Visibility.Collapsed;
                ManualFocusRadioButton.Visibility = Visibility.Collapsed;
            }
            //</SnippetFocus>
        }


        //<SnippetManualFocusChecked>
        private async void ManualFocusRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var focusControl = _mediaCapture.VideoDeviceController.FocusControl;
            await focusControl.LockAsync();
        }
        //</SnippetManualFocusChecked>

        //<snippetFocusSlider>
        private async void FocusSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            var value = (sender as Slider).Value;
            await _mediaCapture.VideoDeviceController.FocusControl.SetValueAsync((uint)value);
        }
        //</snippetFocusSlider>


        private void InitFocusLight()
        {
            //<SnippetFocusLight>
            var focusControl = _mediaCapture.VideoDeviceController.FocusControl;

            if (focusControl.Supported)
            {

                FocusLightCheckBox.Visibility = (_mediaCapture.VideoDeviceController.FlashControl.Supported &&
                                                 _mediaCapture.VideoDeviceController.FlashControl.AssistantLightSupported) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                FocusLightCheckBox.Visibility = Visibility.Collapsed;
            }
            //</SnippetFocusLight>
        }

        //<snippetFocusLightCheckBox>
        private void FocusLightCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            var flashControl = _mediaCapture.VideoDeviceController.FlashControl;

            flashControl.AssistantLightEnabled = (FocusLightCheckBox.IsChecked == true);
        }
        //</snippetFocusLightCheckBox>






        #endregion

        #region PowerlineFrequency
        private void SetPowerlineFrequency()
        {
            //<SnippetPowerlineFrequency>
            PowerlineFrequency getFrequency;

            if (! _mediaCapture.VideoDeviceController.TryGetPowerlineFrequency(out getFrequency))
            {
                // Powerline frequency is not supported on this device.
                return;
            }

            if (! _mediaCapture.VideoDeviceController.TrySetPowerlineFrequency(PowerlineFrequency.Auto))
            {
                // Set the frequency manually
                PowerlineFrequency setFrequency = MyCustomFrequencyLookup();
                if (_mediaCapture.VideoDeviceController.TrySetPowerlineFrequency(setFrequency))
                {
                    System.Diagnostics.Debug.WriteLine(String.Format("Powerline frequency manually set to {0}.", setFrequency));
                }
            }
           
            //</SnippetPowerlineFrequency>
        }
        

        private PowerlineFrequency MyCustomFrequencyLookup()
        {
            return PowerlineFrequency.FiftyHertz;
        }
        #endregion
    }
}