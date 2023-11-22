using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Enums;
using VideoToSM.Notes;

namespace VideoToSM.Chart
{
    public class ChartCol
    {
        public Dictionary<int, Note> Notes { get; set; } = new();
        public ENoteTiming? LastNoteTiming { get; set; }
        public int? LastNoteFrameNum { get; set; }
    }
}
