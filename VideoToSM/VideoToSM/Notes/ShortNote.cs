using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Enums;

namespace VideoToSM.Notes
{
    public class ShortNote : Note
    {
        public ShortNote()
        {
            Id = 1;

            FrameOffsetByColNum.Add(G.BaseOnFPS(2));
            FrameOffsetByColNum.Add(G.BaseOnFPS(2));
            FrameOffsetByColNum.Add(G.BaseOnFPS(1));
            FrameOffsetByColNum.Add(G.BaseOnFPS(2));
            FrameOffsetByColNum.Add(G.BaseOnFPS(2));
        }
    }
}
