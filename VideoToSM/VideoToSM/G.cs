using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.VideoDecoder;

namespace VideoToSM
{
    public static class G
    {
        public static int BPM { get; set; }
        public static VideoReader VideoReader { get; set; } = new();
        public static TextBoxHelper? TextBoxHelper { get; set; }
    }
}
