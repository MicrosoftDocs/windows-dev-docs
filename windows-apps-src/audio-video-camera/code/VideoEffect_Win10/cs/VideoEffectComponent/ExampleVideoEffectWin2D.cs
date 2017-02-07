using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Windows.Graphics.DirectX.Direct3D11;
using Windows.Foundation.Collections;
using Windows.Media.MediaProperties;
using Windows.Media.Effects;

//<SnippetUsingWin2D>
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas;
//</SnippetUsingWin2D>

namespace VideoEffectComponent
{
    public sealed class ExampleVideoEffectWin2D : IBasicVideoEffect
    {

        //<SnippetSupportedMemoryTypesWin2D>
        public MediaMemoryTypes SupportedMemoryTypes { get { return MediaMemoryTypes.Gpu; } }
        //</SnippetSupportedMemoryTypesWin2D>

        //<SnippetSupportedEncodingPropertiesWin2D>
        public IReadOnlyList<VideoEncodingProperties> SupportedEncodingProperties {
            get
            {
                var encodingProperties = new VideoEncodingProperties();
                encodingProperties.Subtype = "ARGB32";
                return new List<VideoEncodingProperties>() { encodingProperties };
            }
        }
        //</SnippetSupportedEncodingPropertiesWin2D>

        //<SnippetSetEncodingPropertiesWin2D>
        private CanvasDevice canvasDevice;
        public void SetEncodingProperties(VideoEncodingProperties encodingProperties, IDirect3DDevice device)
        {
            canvasDevice = CanvasDevice.CreateFromDirect3D11Device(device);
        }
        //</SnippetSetEncodingPropertiesWin2D>

        

        //<SnippetSetPropertiesWin2D>
        private IPropertySet configuration;
        public void SetProperties(IPropertySet configuration)
        {
            this.configuration = configuration;
        }
        //</SnippetSetPropertiesWin2D>


        /// <summary> 
        /// Value used for BlurAmount property 
        /// </summary> 
        //<SnippetBlurAmountWin2D>
        public double BlurAmount
        {
            get
            {
                object val;
                if (configuration != null && configuration.TryGetValue("BlurAmount", out val))
                {
                    return (double)val;
                }
                return 3;
            }
        }
        //</SnippetBlurAmountWin2D>

        //<SnippetProcessFrameWin2D>
        public void ProcessFrame(ProcessVideoFrameContext context)
        {

            using (CanvasBitmap inputBitmap = CanvasBitmap.CreateFromDirect3D11Surface(canvasDevice, context.InputFrame.Direct3DSurface))
            using (CanvasRenderTarget renderTarget = CanvasRenderTarget.CreateFromDirect3D11Surface(canvasDevice, context.OutputFrame.Direct3DSurface))
            using (CanvasDrawingSession ds = renderTarget.CreateDrawingSession())
            {


                var gaussianBlurEffect = new GaussianBlurEffect
                {
                    Source = inputBitmap,
                    BlurAmount = (float)BlurAmount,
                    Optimization = EffectOptimization.Speed
                };

                ds.DrawImage(gaussianBlurEffect);

            }
        }
        //</SnippetProcessFrameWin2D>

        public bool IsReadOnly { get { return false; } }


        

        


        


        public bool TimeIndependent { get { return true; } }


        public void Close(MediaEffectClosedReason reason)
        {
            if (canvasDevice != null) canvasDevice.Dispose();
        }


        public void DiscardQueuedFrames()
        {
        }

        

        
    }
}
