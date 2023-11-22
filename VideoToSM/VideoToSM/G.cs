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
        public static int BPM { get; set; } = 146;
        public static double FPS { get; set; }
        public static VideoReader VideoReader { get; set; } = new();
        public static TextBoxHelper? TextBoxHelper { get; set; }
        public static ChartBuilder ChartBuilder { get; set; } = new();
        public static SimfileGen SimfileGen { get; set; } = new();
    }
}
