using System.Collections.Generic;
using VideoToSM.Enums;

namespace VideoToSM.Notes
{
    public class Note
    {
        public int Id { get; set; }
        public ENoteTiming NoteTiming { get; set; }
        public List<int> FrameOffsetByColNum { get; set; } = new();
    }
}
