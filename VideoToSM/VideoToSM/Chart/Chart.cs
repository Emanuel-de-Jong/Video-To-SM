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
            for (int i = 0; i < G.KEYS; i++)
            {
                Columns.Add(new ChartCol());
            }
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

            Columns[colNum].AddNote(note, LastB64thOrderNumber, frameNum);
        }

        public int CalcB64thOrderNumber(Note note, int frameNum)
        {
            double msPerFrame = 1000 / G.FPS;
            double ms = (frameNum - FirstNoteFrame.Value) * msPerFrame;
            double b64thBPM = G.BPM * 2 * 2 * 2 * 2;
            double b64thBeatsPerMS = b64thBPM / 60 / 1000;
            int b64thBeat = (int)(ms * b64thBeatsPerMS);
            int beatRemainder = b64thBeat % G.NOTE_TIME_ACCURACY;

            int firstTimingStepSize = G.NOTE_TIME_ACCURACY / (int)note.NoteTiming;
            int timingStepSize = firstTimingStepSize * 2;
            int timingInstances = (int)note.NoteTiming / 2;

            if (note.NoteTiming == ENoteTiming.Red)
            {
                firstTimingStepSize = 0;
                timingStepSize = (G.NOTE_TIME_ACCURACY / (int)ENoteTiming.Blue) * 2;
                timingInstances = (int)ENoteTiming.Blue / 2;
            }

            int closestStep = 0;
            int closestStepDiff = int.MaxValue;
            for (int i = 1; i <= timingInstances + 1; i++)
            {
                int step = firstTimingStepSize + ((i - 1) * timingStepSize);

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
