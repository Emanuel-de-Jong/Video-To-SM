using FFmpeg.AutoGen.Abstractions;
using FFmpeg.AutoGen.Bindings.DynamicallyLoaded;
using SkiaSharp;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VideoToSM.Chart;

namespace VideoToSM.VideoDecoder
{
    public class VideoReader
    {
        public VideoReader()
        {
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

                FindNotesInFrame(bitmap, frameNum);
            }
        }

        private void FindNotesInFrame(SKBitmap bitmap, int frameNum)
        {
            bool shouldWrite = false;

            string[] arrowSymbols = { "◄", "▼", "◆", "▲", "►" };
            int[] xCoords = { 190, 258, 320, 375, 450 };

            if (shouldWrite) G.TextBoxHelper.Write(frameNum);

            for (int i = 0; i < xCoords.Length; i++)
            {
                int x = xCoords[i];
                int yBase = 370;

                var pixelUp = bitmap.GetPixel(x, yBase - 10);
                var pixelMid = bitmap.GetPixel(x, yBase);
                var pixelDown = bitmap.GetPixel(x, yBase + 10);

                string arrowSymbol = arrowSymbols[i];
                if (shouldWrite) G.TextBoxHelper.Write(" ");
                if (shouldWrite) G.TextBoxHelper.Write(arrowSymbol, pixelUp);
                if (shouldWrite) G.TextBoxHelper.Write(arrowSymbol, pixelMid);
                if (shouldWrite) G.TextBoxHelper.Write(arrowSymbol, pixelDown);

                G.ChartBuilder.ColorsToNote((new SKColor[] { pixelUp, pixelMid, pixelDown }).ToList(), i, frameNum);
            }

            if (shouldWrite) G.TextBoxHelper.WriteLine();
        }
    }
}
