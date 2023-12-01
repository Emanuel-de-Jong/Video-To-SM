using System;
using VideoToSM.Chart;
using VideoToSM.Enums;
using VideoToSM.Simfile;
using VideoToSM.VideoDecoder;

namespace VideoToSM
{
    public static class G
    {
        public static int KEYS = 5;
        public static int NOTE_TIME_ACCURACY = 64; // 64th notes

        public static int BPM { get; set; } = 146;
        public static ESMXDifficultyType DifficultyType { get; set; } = ESMXDifficultyType.HardPlus;
        public static string SongTitle { get; set; }

        public static double FPS { get; set; }
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }

        public static VideoReader VideoReader { get; set; } = new();
        public static TextBoxHelper? MessageTextBoxHelper { get; set; }
        public static TextBoxHelper? SCCTextBoxHelper { get; set; }
        public static ChartBuilder ChartBuilder { get; set; } = new();
        public static SimfileGenerator SimfileGen { get; set; } = new();

        public static int BaseOnFPS(int num)
        {
            return (int)Math.Round(BaseOnFPS((double)num));
        }

        public static double BaseOnFPS(double num)
        {
            return num * (FPS / 60d);
        }

        public static int BaseOnScreenWidth(int num)
        {
            return (int)Math.Round(BaseOnScreenWidth((double)num));
        }

        public static double BaseOnScreenWidth(double num)
        {
            return num * (G.ScreenWidth / 1280d);
        }

        public static int BaseOnScreenHeight(int num)
        {
            return (int)Math.Round(BaseOnScreenHeight((double)num));
        }

        public static double BaseOnScreenHeight(double num)
        {
            return num * (G.ScreenWidth / 1280d);
        }
    }
}
