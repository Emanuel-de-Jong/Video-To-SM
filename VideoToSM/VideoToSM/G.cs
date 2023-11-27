using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Chart;
using VideoToSM.Simfile;
using VideoToSM.VideoDecoder;

namespace VideoToSM
{
    public static class G
    {
        public static int KEYS = 5;
        public static int NOTE_TIME_ACCURACY = 64; // 64th notes

        public static int BPM { get; set; } = 146;
        public static double FPS { get; set; }
        public static VideoReader VideoReader { get; set; } = new();
        public static TextBoxHelper? TextBoxHelper { get; set; }
        public static ChartBuilder ChartBuilder { get; set; } = new();
        public static SimfileGen SimfileGen { get; set; } = new();

        public static int BaseOnFPS(int num)
        {
            return num * (int)Math.Round(FPS / 30);
        }

        public static double BaseOnFPS(double num)
        {
            return num * (30 / FPS);
        }
    }
}
