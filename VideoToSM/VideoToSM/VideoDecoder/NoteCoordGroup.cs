using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoToSM.VideoDecoder
{
    public class NoteCoordGroup
    {
        public int LNLeft { get; set; }
        public int Center { get; set; }
        public int LNRight { get; set; }

        public NoteCoordGroup(int lNLeft, int center, int lNRight)
        {
            LNLeft = lNLeft;
            Center = center;
            LNRight = lNRight;
        }
    }
}
