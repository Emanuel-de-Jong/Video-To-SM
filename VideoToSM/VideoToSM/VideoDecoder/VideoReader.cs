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
            NoteCoordGroup[] noteCoordGroups = {
                new(185, 205, 221),
                new(264, 264, 264),
                new(316, 316, 316),
                new(366, 366, 366),
                new(428, 428, 428)
            };

            if (shouldWrite) G.TextBoxHelper.Write(frameNum);

            for (int i = 0; i < noteCoordGroups.Length; i++)
            {
                NoteCoordGroup noteCoordGroup = noteCoordGroups[i];
                int yBase = 690;

                int PIXEL_OFFSET = 10;
                NoteColorGroup noteColorGroup = new(
                    bitmap.GetPixel(noteCoordGroup.Center, yBase - PIXEL_OFFSET),
                    bitmap.GetPixel(noteCoordGroup.Center, yBase),
                    bitmap.GetPixel(noteCoordGroup.Center, yBase + PIXEL_OFFSET),

                    bitmap.GetPixel(noteCoordGroup.LNLeft, yBase - PIXEL_OFFSET),
                    bitmap.GetPixel(noteCoordGroup.LNLeft, yBase),
                    bitmap.GetPixel(noteCoordGroup.LNLeft, yBase + PIXEL_OFFSET),

                    bitmap.GetPixel(noteCoordGroup.LNRight, yBase - PIXEL_OFFSET),
                    bitmap.GetPixel(noteCoordGroup.LNRight, yBase),
                    bitmap.GetPixel(noteCoordGroup.LNRight, yBase + PIXEL_OFFSET)
                );

                string arrowSymbol = arrowSymbols[i];
                if (shouldWrite) G.TextBoxHelper.Write(" ");
                if (shouldWrite) G.TextBoxHelper.Write(arrowSymbol, noteColorGroup.CenterTop);
                if (shouldWrite) G.TextBoxHelper.Write(arrowSymbol, noteColorGroup.CenterCenter);
                if (shouldWrite) G.TextBoxHelper.Write(arrowSymbol, noteColorGroup.CenterBottom);

                G.ChartBuilder.ColorsToNote(noteColorGroup, i, frameNum);
            }

            if (shouldWrite) G.TextBoxHelper.WriteLine();
        }
    }
}
