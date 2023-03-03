using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Runtime.InteropServices;
using OpenCvSharp5;

namespace BlazorApp
{
    public class CanvasClient
    {
        private readonly IJSRuntime jsRuntime;
        private readonly ElementReference canvasElement;

        public CanvasClient(
            IJSRuntime jsRuntime, 
            ElementReference canvasElement)
        {
            this.jsRuntime = jsRuntime;
            this.canvasElement = canvasElement;
        }
        
        public async Task DrawPixelsAsync(byte[] pixels)
        {
            await jsRuntime.InvokeVoidAsync("drawPixels", canvasElement, pixels);
        }

        public async Task DrawMatAsync(Mat mat)
        {
            /*
            Mat? rgba = null;
            try
            {
                var type = mat.Type();
                if (type == MatType.CV_8UC1)
                    rgba = mat.CvtColor(ColorConversionCodes.GRAY2RGBA);
                else if (type == MatType.CV_8UC3)
                    rgba = mat.CvtColor(ColorConversionCodes.BGR2RGBA);
                else
                    throw new ArgumentException($"Invalid mat type ({mat.Type()})");

                if (!rgba.IsContinuous())
                    throw new InvalidOperationException("RGBA Mat should be continuous.");
                var length = (int)(rgba.DataEnd.ToInt64() - rgba.DataStart.ToInt64());
                var pixelBytes = new byte[length];
                Marshal.Copy(rgba.DataStart, pixelBytes, 0, length);

                await DrawPixelsAsync(pixelBytes);
            }
            finally
            {
                rgba?.Dispose();
            }*/
            Console.WriteLine("AAAAAAAAAA");

            await Task.CompletedTask;
        }
    }
}
