using System.Collections.Generic;
using VideoToSM.Enums;

namespace VideoToSM.Notes
{
    public class Note
    {
        public string Id { get; set; } = "0";
        public ENoteTiming NoteTiming { get; set; }
        public List<int> FrameOffsetByColNum { get; set; } = new();
    }
}
