using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





//<SnippetEffectUsing>
using Windows.Media.Effects;
using Windows.Media.MediaProperties;
using Windows.Foundation.Collections;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.Graphics.Imaging;
//</SnippetEffectUsing>

//<SnippetCOMUsing>
using System.Runtime.InteropServices;

//</SnippetCOMUsing>

namespace VideoEffectComponent
{

    // <SnippetCOMImport>
    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess
    {
        void GetBuffer(out byte* buffer, out uint capacity);
    }
    //</SnippetCOMImport>

    //<SnippetImplementIBasicVideoEffect>
    public sealed class ExampleVideoEffect : IBasicVideoEffect
    //</SnippetImplementIBasicVideoEffect>
    {

        //<SnippetClose>
        public void Close(MediaEffectClosedReason reason)
        {
            // Dispose of effect resources
        }
        //</SnippetClose>

        //<SnippetDiscardQueuedFrames>
        private int frameCount;
        public void DiscardQueuedFrames()
        {
            frameCount = 0;
        }
        //</SnippetDiscardQueuedFrames>

        //<SnippetIsReadOnly>
        public bool IsReadOnly { get { return false; } }
        //</SnippetIsReadOnly>

        //<SnippetSetEncodingProperties>
        private VideoEncodingProperties encodingProperties;
        public void SetEncodingProperties(VideoEncodingProperties encodingProperties, IDirect3DDevice device)
        {
            this.encodingProperties = encodingProperties;
        }
        //</SnippetSetEncodingProperties>

        //<SnippetSupportedEncodingProperties>
        public IReadOnlyList<VideoEncodingProperties> SupportedEncodingProperties
        {            
            get
            {
                var encodingProperties = new VideoEncodingProperties();
                encodingProperties.Subtype = "ARGB32";
                return new List<VideoEncodingProperties>() { encodingProperties };

                // If the list is empty, the encoding type will be ARGB32.
                // return new List<VideoEncodingProperties>();
            }
        }
        //</SnippetSupportedEncodingProperties>

        //<SnippetSupportedMemoryTypes>
        public MediaMemoryTypes SupportedMemoryTypes { get { return MediaMemoryTypes.Cpu; } }
        //</SnippetSupportedMemoryTypes>

        //<SnippetTimeIndependent>
        public bool TimeIndependent { get { return true; } }
        //</SnippetTimeIndependent>


        //<SnippetSetProperties>
        private IPropertySet configuration;
        public void SetProperties(IPropertySet configuration)
        {
            this.configuration = configuration;
        }
        //</SnippetSetProperties>

        //<SnippetFadeValue>
        public double FadeValue
        {
            get
            {
                object val;
                if (configuration != null && configuration.TryGetValue("FadeValue", out val))
                {
                    return (double)val;
                }
                return .5;
            }
        }
        //</SnippetFadeValue>

        //<SnippetProcessFrameSoftwareBitmap>
        public unsafe void ProcessFrame(ProcessVideoFrameContext context)
        {
            using (BitmapBuffer buffer = context.InputFrame.SoftwareBitmap.LockBuffer(BitmapBufferAccessMode.Read))
            using (BitmapBuffer targetBuffer = context.OutputFrame.SoftwareBitmap.LockBuffer(BitmapBufferAccessMode.Write))
            {
                using (var reference = buffer.CreateReference())
                using (var targetReference = targetBuffer.CreateReference())
                {
                    byte* dataInBytes;
                    uint capacity;
                    ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacity);

                    byte* targetDataInBytes;
                    uint targetCapacity;
                    ((IMemoryBufferByteAccess)targetReference).GetBuffer(out targetDataInBytes, out targetCapacity);

                    var fadeValue = FadeValue;

                    // Fill-in the BGRA plane
                    BitmapPlaneDescription bufferLayout = buffer.GetPlaneDescription(0);
                    for (int i = 0; i < bufferLayout.Height; i++)
                    {
                        for (int j = 0; j < bufferLayout.Width; j++)
                        {

                            byte value = (byte)((float)j / bufferLayout.Width * 255);

                            int bytesPerPixel = 4; 
                            if (encodingProperties.Subtype != "ARGB32")
                            {
                                // If you support other encodings, adjust index into the buffer accordingly
                            }
                            

                            int idx = bufferLayout.StartIndex + bufferLayout.Stride * i + bytesPerPixel * j;

                            targetDataInBytes[idx + 0] = (byte)(fadeValue * (float)dataInBytes[idx + 0]);
                            targetDataInBytes[idx + 1] = (byte)(fadeValue * (float)dataInBytes[idx + 1]);
                            targetDataInBytes[idx + 2] = (byte)(fadeValue * (float)dataInBytes[idx + 2]);
                            targetDataInBytes[idx + 3] = dataInBytes[idx + 3];
                        }
                    }
                }
            }
        }
        //</SnippetProcessFrameSoftwareBitmap>
    }
}
