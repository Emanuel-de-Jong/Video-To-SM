using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Enums;
using VideoToSM.Notes;

namespace VideoToSM.Chart
{
    public class Chart
    {
        public List<ChartCol> Columns { get; set; } = new();
        public int LastB64thOrderNumber { get; set; }
        public int? FirstNoteFrame { get; set; }

        public Chart()
        {
            Columns.Add(new ChartCol());
            Columns.Add(new ChartCol());
            Columns.Add(new ChartCol());
            Columns.Add(new ChartCol());
            Columns.Add(new ChartCol());
        }

        public void AddNote(Note note, int colNum, int frameNum)
        {
            if (FirstNoteFrame == null)
            {
                FirstNoteFrame = frameNum;
                LastB64thOrderNumber = 1;
            } else
            {
                LastB64thOrderNumber = CalcB64thOrderNumber(frameNum);
            }

            Columns[colNum].Notes[LastB64thOrderNumber] = note;
        }

        public int CalcB64thOrderNumber(int frameNum)
        {
            double msPerFrame = 1000 / G.FPS;
            double ms = (frameNum - FirstNoteFrame.Value) * msPerFrame;
            double b64thBPM = G.BPM * 2 * 2 * 2 * 2;
            double b64thBeatsPerMS = b64thBPM / 60 / 1000;
            return (int)(ms * b64thBeatsPerMS);
        }
    }
}
