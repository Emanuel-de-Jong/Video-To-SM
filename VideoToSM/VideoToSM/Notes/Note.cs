using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Enums;

namespace VideoToSM.Notes
{
    public class Note
    {
        public ENoteTiming NoteTiming { get; set; }
        public int B64thOrderNumber { get; set; }

        public void CalcB64thOrderNumber(int frameNum)
        {
            double msPerFrame = 1000 / G.FPS;
            double ms = frameNum * msPerFrame;
            double b64thBPM = G.BPM * 2 * 2 * 2 * 2;
            double b64thBeatsPerMS = b64thBPM / 60 / 1000;
            B64thOrderNumber = (int) (ms * b64thBeatsPerMS);
        }
    }
}
