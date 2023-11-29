using FFmpeg.AutoGen.Abstractions;
using FFmpeg.AutoGen.Bindings.DynamicallyLoaded;
using SkiaSharp;
using System;

namespace VideoToSM.VideoDecoder
{
    public class VideoReader
    {
        public VideoReader()
        {
            DynamicallyLoadedBindings.LibrariesPath = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length - 24) + "ffmpeg";
            DynamicallyLoadedBindings.Initialize();
        }

        public unsafe void Read(string filePath, bool isReadLeftSide)
        {
            using VideoStreamDecoder vsd = new(filePath);

            System.Drawing.Size sourceSize = vsd.FrameSize;
            AVPixelFormat sourcePixelFormat = vsd.PixelFormat;
            System.Drawing.Size destinationSize = sourceSize;
            AVPixelFormat destinationPixelFormat = AVPixelFormat.@AV_PIX_FMT_BGRA;
            using VideoFrameConverter vfc = new(sourceSize, sourcePixelFormat, destinationSize, destinationPixelFormat);

            // Start at 10 so it can't become negative after frame offset
            int frameNum = 10;
            while (vsd.TryDecodeNextFrame(out AVFrame frame))
            {
                frameNum++;

                AVFrame convertedFrame = vfc.Convert(frame);
                SKImageInfo imageInfo = new(convertedFrame.width, convertedFrame.height, SKColorType.Bgra8888, SKAlphaType.Opaque);
                using SKBitmap bitmap = new();
                bitmap.InstallPixels(imageInfo, (IntPtr)convertedFrame.data[0]);

                FindNotesInFrame(bitmap, frameNum, isReadLeftSide);
            }
        }

        private void FindNotesInFrame(SKBitmap bitmap, int frameNum, bool isReadLeftSide)
        {
            bool shouldWrite = false;

            string[] arrowSymbols = { "◄", "▼", "◆", "▲", "►" };

            NoteCoordGroup[] noteCoordGroups = {
                new(G.BaseOnScreenWidth(189), G.BaseOnScreenWidth(202), G.BaseOnScreenWidth(219)),
                new(G.BaseOnScreenWidth(264), G.BaseOnScreenWidth(264), G.BaseOnScreenWidth(264)),
                new(G.BaseOnScreenWidth(299), G.BaseOnScreenWidth(316), G.BaseOnScreenWidth(329)),
                new(G.BaseOnScreenWidth(366), G.BaseOnScreenWidth(366), G.BaseOnScreenWidth(366)),
                new(G.BaseOnScreenWidth(410), G.BaseOnScreenWidth(428), G.BaseOnScreenWidth(441))
            };

            if (shouldWrite) G.MessageTextBoxHelper.Write(frameNum);

            int xOffset = isReadLeftSide ? 0 : 653;

            for (int i = 0; i < noteCoordGroups.Length; i++)
            {
                NoteCoordGroup noteCoordGroup = noteCoordGroups[i];
                int yBase = G.BaseOnScreenHeight(690);

                int pixelOffset = G.BaseOnScreenHeight(10);
                NoteColorGroup noteColorGroup = new(
                    bitmap.GetPixel(xOffset + noteCoordGroup.Center, yBase - pixelOffset),
                    bitmap.GetPixel(xOffset + noteCoordGroup.Center, yBase),
                    bitmap.GetPixel(xOffset + noteCoordGroup.Center, yBase + pixelOffset),

                    bitmap.GetPixel(xOffset + noteCoordGroup.LNLeft, yBase - pixelOffset),
                    bitmap.GetPixel(xOffset + noteCoordGroup.LNLeft, yBase),
                    bitmap.GetPixel(xOffset + noteCoordGroup.LNLeft, yBase + pixelOffset),

                    bitmap.GetPixel(xOffset + noteCoordGroup.LNRight, yBase - pixelOffset),
                    bitmap.GetPixel(xOffset + noteCoordGroup.LNRight, yBase),
                    bitmap.GetPixel(xOffset + noteCoordGroup.LNRight, yBase + pixelOffset)
                );

                string arrowSymbol = arrowSymbols[i];
                if (shouldWrite) G.MessageTextBoxHelper.Write(" ");
                if (shouldWrite) G.MessageTextBoxHelper.Write(arrowSymbol, noteColorGroup.CenterTop);
                if (shouldWrite) G.MessageTextBoxHelper.Write(arrowSymbol, noteColorGroup.CenterCenter);
                if (shouldWrite) G.MessageTextBoxHelper.Write(arrowSymbol, noteColorGroup.CenterBottom);

                G.ChartBuilder.ColorsToNote(noteColorGroup, i, frameNum);
            }

            if (shouldWrite) G.MessageTextBoxHelper.WriteLine();
        }
    }
}
