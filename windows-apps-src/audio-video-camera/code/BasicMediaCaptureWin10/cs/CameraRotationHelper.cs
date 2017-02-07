using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using Windows.Storage.FileProperties;

namespace BasicCameraWin10
{
    //<SnippetCameraRotationHelperFull>
    class CameraRotationHelper
    {
        private EnclosureLocation _cameraEnclosureLocation;
        private DisplayInformation _displayInformation = DisplayInformation.GetForCurrentView();
        private SimpleOrientationSensor _orientationSensor = SimpleOrientationSensor.GetDefault();
        public event EventHandler<bool> OrientationChanged;

        public CameraRotationHelper(EnclosureLocation cameraEnclosureLocation)
        {
            _cameraEnclosureLocation = cameraEnclosureLocation;
            if (!IsEnclosureLocationExternal(_cameraEnclosureLocation))
            {
                _orientationSensor.OrientationChanged += SimpleOrientationSensor_OrientationChanged;
            }
            _displayInformation.OrientationChanged += DisplayInformation_OrientationChanged;
        }

        private void SimpleOrientationSensor_OrientationChanged(SimpleOrientationSensor sender, SimpleOrientationSensorOrientationChangedEventArgs args)
        {
            if (args.Orientation != SimpleOrientation.Faceup && args.Orientation != SimpleOrientation.Facedown)
            {
                HandleOrientationChanged(false);
            }
        }

        private void DisplayInformation_OrientationChanged(DisplayInformation sender, object args)
        {
            HandleOrientationChanged(true);
        }

        private void HandleOrientationChanged(bool updatePreviewStreamRequired)
        {
            var handler = OrientationChanged;
            if (handler != null)
            {
                handler(this, updatePreviewStreamRequired);
            }
        }

        public static bool IsEnclosureLocationExternal(EnclosureLocation enclosureLocation)
        {
            return (enclosureLocation == null || enclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Unknown);
        }

        private bool IsCameraMirrored()
        {
            // Front panel cameras are mirrored by default
            return (_cameraEnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front);
        }

        private SimpleOrientation GetCameraOrientationRelativeToNativeOrientation()
        {
            // Account for the fact that, on portrait-first devices, the built in camera sensor is read at a 90 degree offset to the native orientation
            if (_displayInformation.NativeOrientation == DisplayOrientations.Portrait && !IsEnclosureLocationExternal(_cameraEnclosureLocation))
            {
                return SimpleOrientation.Rotated90DegreesCounterclockwise;
            }
            else
            {
                return SimpleOrientation.NotRotated;
            }
        }

        // Gets the rotation to rotate ui elements
        public SimpleOrientation GetUIOrientation()
        {
            if (IsEnclosureLocationExternal(_cameraEnclosureLocation))
            {
                // Cameras that are not attached to the device do not rotate along with it, so apply no rotation
                return SimpleOrientation.NotRotated;
            }

            // Return the difference between the orientation of the device and the orientation of the app display
            var deviceOrientation = _orientationSensor.GetCurrentOrientation();
            var displayOrientation = ConvertDisplayOrientationToSimpleOrientation(_displayInformation.CurrentOrientation);
            return SubOrientations(displayOrientation, deviceOrientation);
        }

        // Gets the rotation of the camera to rotate pictures/videos when saving to file
        public SimpleOrientation GetCameraCaptureOrientation()
        {
            if (IsEnclosureLocationExternal(_cameraEnclosureLocation))
            {
                // Cameras that are not attached to the device do not rotate along with it, so apply no rotation
                return SimpleOrientation.NotRotated;
            }

            // Get the device orienation offset by the camera hardware offset
            var deviceOrientation = _orientationSensor.GetCurrentOrientation();
            var result = SubOrientations(deviceOrientation, GetCameraOrientationRelativeToNativeOrientation());

            // If the preview is being mirrored for a front-facing camera, then the rotation should be inverted
            if (IsCameraMirrored())
            {
                result = MirrorOrientation(result);
            }
            return result;
        }

        // Gets the rotation of the camera to display the camera preview
        public SimpleOrientation GetCameraPreviewOrientation()
        {
            if (IsEnclosureLocationExternal(_cameraEnclosureLocation))
            {
                // Cameras that are not attached to the device do not rotate along with it, so apply no rotation
                return SimpleOrientation.NotRotated;
            }

            // Get the app display rotation offset by the camera hardware offset
            var result = ConvertDisplayOrientationToSimpleOrientation(_displayInformation.CurrentOrientation);
            result = SubOrientations(result, GetCameraOrientationRelativeToNativeOrientation());

            // If the preview is being mirrored for a front-facing camera, then the rotation should be inverted
            if (IsCameraMirrored())
            {
                result = MirrorOrientation(result);
            }
            return result;
        }

        public static PhotoOrientation ConvertSimpleOrientationToPhotoOrientation(SimpleOrientation orientation)
        {
            switch (orientation)
            {
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    return PhotoOrientation.Rotate90;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    return PhotoOrientation.Rotate180;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    return PhotoOrientation.Rotate270;
                case SimpleOrientation.NotRotated:
                default:
                    return PhotoOrientation.Normal;
            }
        }

        public static int ConvertSimpleOrientationToClockwiseDegrees(SimpleOrientation orientation)
        {
            switch (orientation)
            {
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    return 270;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    return 180;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    return 90;
                case SimpleOrientation.NotRotated:
                default:
                    return 0;
            }
        }

        private SimpleOrientation ConvertDisplayOrientationToSimpleOrientation(DisplayOrientations orientation)
        {
            SimpleOrientation result;
            switch (orientation)
            {
                case DisplayOrientations.Landscape:
                    result = SimpleOrientation.NotRotated;
                    break;
                case DisplayOrientations.PortraitFlipped:
                    result = SimpleOrientation.Rotated90DegreesCounterclockwise;
                    break;
                case DisplayOrientations.LandscapeFlipped:
                    result = SimpleOrientation.Rotated180DegreesCounterclockwise;
                    break;
                case DisplayOrientations.Portrait:
                default:
                    result = SimpleOrientation.Rotated270DegreesCounterclockwise;
                    break;
            }

            // Above assumes landscape; offset is needed if native orientation is portrait
            if (_displayInformation.NativeOrientation == DisplayOrientations.Portrait)
            {
                result = AddOrientations(result, SimpleOrientation.Rotated90DegreesCounterclockwise);
            }

            return result;
        }

        private static SimpleOrientation MirrorOrientation(SimpleOrientation orientation)
        {
            // This only affects the 90 and 270 degree cases, because rotating 0 and 180 degrees is the same clockwise and counter-clockwise
            switch (orientation)
            {
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    return SimpleOrientation.Rotated270DegreesCounterclockwise;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    return SimpleOrientation.Rotated90DegreesCounterclockwise;
            }
            return orientation;
        }

        private static SimpleOrientation AddOrientations(SimpleOrientation a, SimpleOrientation b)
        {
            var aRot = ConvertSimpleOrientationToClockwiseDegrees(a);
            var bRot = ConvertSimpleOrientationToClockwiseDegrees(b);
            var result = (aRot + bRot) % 360;
            return ConvertClockwiseDegreesToSimpleOrientation(result);
        }

        private static SimpleOrientation SubOrientations(SimpleOrientation a, SimpleOrientation b)
        {
            var aRot = ConvertSimpleOrientationToClockwiseDegrees(a);
            var bRot = ConvertSimpleOrientationToClockwiseDegrees(b);
            //add 360 to ensure the modulus operator does not operate on a negative
            var result = (360 + (aRot - bRot)) % 360;
            return ConvertClockwiseDegreesToSimpleOrientation(result);
        }

        private static VideoRotation ConvertSimpleOrientationToVideoRotation(SimpleOrientation orientation)
        {
            switch (orientation)
            {
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    return VideoRotation.Clockwise270Degrees;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    return VideoRotation.Clockwise180Degrees;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    return VideoRotation.Clockwise90Degrees;
                case SimpleOrientation.NotRotated:
                default:
                    return VideoRotation.None;
            }
        }

        private static SimpleOrientation ConvertClockwiseDegreesToSimpleOrientation(int orientation)
        {
            switch (orientation)
            {
                case 270:
                    return SimpleOrientation.Rotated90DegreesCounterclockwise;
                case 180:
                    return SimpleOrientation.Rotated180DegreesCounterclockwise;
                case 90:
                    return SimpleOrientation.Rotated270DegreesCounterclockwise;
                case 0:
                default:
                    return SimpleOrientation.NotRotated;
            }
        }
    }
    //</SnippetCameraRotationHelperFull>
}
