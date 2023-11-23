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
                LastB64thOrderNumber = CalcB64thOrderNumber(note, frameNum);
            }

            Columns[colNum].Notes[LastB64thOrderNumber] = note;
        }

        public int CalcB64thOrderNumber(Note note, int frameNum)
        {
            double msPerFrame = 1000 / G.FPS;
            double ms = (frameNum - FirstNoteFrame.Value) * msPerFrame;
            double b64thBPM = G.BPM * 2 * 2 * 2 * 2;
            double b64thBeatsPerMS = b64thBPM / 60 / 1000;
            int b64thBeat = (int)(ms * b64thBeatsPerMS);

            int timingStepSize = 64 / (int)note.NoteTiming;
            int beatRemainder = b64thBeat % 64;

            int closestStep = 0;
            int closestStepDiff = int.MaxValue;
            for (int i = note.NoteTiming == ENoteTiming.Red ? 0 : 1; i < (int)note.NoteTiming + 1; i++)
            {
                int step = i * timingStepSize;
                int stepDiff = Math.Abs(step - beatRemainder);
                if (stepDiff < closestStepDiff)
                {
                    closestStep = step;
                    closestStepDiff = stepDiff;
                }
            }

            int b64thOrderNumber = b64thBeat - beatRemainder + closestStep + 1;
            return b64thOrderNumber;
        }
    }
}
