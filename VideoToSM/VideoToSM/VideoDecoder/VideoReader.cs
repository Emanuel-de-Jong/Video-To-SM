using FFmpeg.AutoGen.Abstractions;
using FFmpeg.AutoGen.Bindings.DynamicallyLoaded;
using SkiaSharp;
using System;
using System.Windows;
using System.Windows.Controls;
using VideoToSM.Chart;

namespace VideoToSM.VideoDecoder
{
    public class VideoReader
    {
        public TextBoxHelper TextBoxHelper { get; set; }
        public ChartBuilder ChartBuilder { get; set; } = new();

        public VideoReader(TextBoxHelper textBoxHelper)
        {
            TextBoxHelper = textBoxHelper;

            DynamicallyLoadedBindings.LibrariesPath = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length - 24) + "ffmpeg";
            DynamicallyLoadedBindings.Initialize();
        }

        public unsafe void Read(string filePath)
        {
            using var vsd = new VideoStreamDecoder(filePath);

            var sourceSize = vsd.FrameSize;
            var sourcePixelFormat = vsd.PixelFormat;
            var destinationSize = sourceSize;
            var destinationPixelFormat = AVPixelFormat.@AV_PIX_FMT_BGRA;
            using var vfc = new VideoFrameConverter(sourceSize, sourcePixelFormat, destinationSize, destinationPixelFormat);

            int frameNum = 0;
            while (vsd.TryDecodeNextFrame(out var frame))
            {
                frameNum++;

                var convertedFrame = vfc.Convert(frame);
                var imageInfo = new SKImageInfo(convertedFrame.width, convertedFrame.height, SKColorType.Bgra8888, SKAlphaType.Opaque);
                using var bitmap = new SKBitmap();
                bitmap.InstallPixels(imageInfo, (IntPtr)convertedFrame.data[0]);

                var pixel = bitmap.GetPixel(266, 214);

                TextBoxHelper.WriteLine("Frame " + frameNum, pixel);

                if (frameNum == 74)
                {
                    var test = 0;
                }

                ChartBuilder.ColorToNote(pixel, 0);
            }
        }
    }
}
