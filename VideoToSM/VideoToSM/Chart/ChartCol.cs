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
        public Note? LastAddedNote { get; set; }
        public int LastAddedKey { get; set; }
        public int? LastAddedFrameNum { get; set; }

        public int? FirstLNFrameNum { get; set; }
        public int? LastLNFrameNum { get; set; }
        public int LNDetectionCount { get; set; } = 0;

        public void AddNote(Note note, int b64thOrderNumber, int frameNum)
        {
            if (!Notes.ContainsKey(b64thOrderNumber))
            {
                Notes.Add(b64thOrderNumber, note);
            }
            else
            {
                G.MessageTextBoxHelper.WriteLine("!!!DOUBLE NOTE " + b64thOrderNumber + "!!!");
            }

            LastAddedNote = note;
            LastAddedKey = b64thOrderNumber;
            LastAddedFrameNum = frameNum;
        }

        public void ClearLNStats()
        {
            FirstLNFrameNum = null;
            LastLNFrameNum = null;
            LNDetectionCount = 0;
        }
    }
}
