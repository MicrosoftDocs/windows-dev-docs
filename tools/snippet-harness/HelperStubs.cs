// HelperStubs.cs — minimal placeholder definitions for custom/third-party
// helper types that individual doc snippets *reference* but don't (or can't)
// fully define within the same, isolated snippet compilation unit.
//
// Each ```csharp fenced block in a doc is compiled in its own namespace
// (see scaffold() in validate-csharp-snippets.py), so a class defined in one
// snippet is not visible to a different snippet later in the same article,
// even though they're both in the same markdown file. These stubs exist
// purely so the harness can resolve symbols for compilation; they are not
// part of the documented API surface and mirror the existing synthetic
// "base_members" placeholder fields (button, textBox, etc.) that the harness
// already injects for the same reason.

namespace OpenCVBridge
{
    // Real definition: authored as a C++/WinRT component (OpenCVHelper.idl +
    // C++ implementation) in process-software-bitmaps-with-opencv.md. It's
    // never expressed as C# source in the doc, so it can't be picked up from
    // another snippet — it must be stubbed here.
    public sealed class OpenCVHelper
    {
        public void ProcessBitmap(SoftwareBitmap input, SoftwareBitmap output) { }

        public void ApplyCannyEdges(
            SoftwareBitmap input,
            SoftwareBitmap output,
            double threshold1,
            double threshold2)
        { }
    }
}

// Real definition: fully authored as its own snippet in
// handle-device-orientation-with-mediacapture.md, then referenced by type
// name only from a separate, later snippet in the same article. Stubbed
// here (global namespace) so that later snippet can resolve the type.
public sealed class CameraRotationHelper
{
    public CameraRotationHelper(EnclosureLocation cameraEnclosureLocation) { }

    public event EventHandler<bool>? OrientationChanged;

    public VideoRotation GetCameraPreviewOrientation() => VideoRotation.None;

    public PhotoOrientation GetCapturePhotoOrientation() => PhotoOrientation.Normal;

    public int GetCaptureVideoOrientation() => 0;

    public void Dispose() { }

    public static bool IsEnclosureLocationExternal(EnclosureLocation? enclosureLocation) => true;
}

// Real definition: this COM interop interface is authored as its own
// ```csharp snippet independently in custom-audio-effects.md,
// custom-video-effects.md, and use-opencv-with-mediaframereader.md. Each
// doc's copy is scaffolded in its own isolated synthetic namespace (see
// scaffold()'s _LEVEL_TYPE handling), so it isn't visible to the later
// snippets in the same article that cast to it. Stubbed here (global
// namespace) purely so those later snippets resolve the type; the
// per-doc snippets remain the authoritative, reader-facing definition.
[System.Runtime.InteropServices.ComImport]
[System.Runtime.InteropServices.Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
[System.Runtime.InteropServices.InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)]
public unsafe interface IMemoryBufferByteAccess
{
    void GetBuffer(out byte* buffer, out uint capacity);
}
