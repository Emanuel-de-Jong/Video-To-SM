using System.Collections.Generic;
using System.Text;

namespace VideoToSM.Simfile
{
    public class NoteBar : List<NoteRow>
    {
        public void Compress()
        {
            int[] noteTimes = { 64, 32, 16, 8 };

            List<NoteRow> noteRowsToRemove = new();
            foreach (int noteTime in noteTimes)
            {
                bool areAllEmpty = true;

                int firstTimingStepSize = G.NOTE_TIME_ACCURACY / noteTime;
                int timingStepSize = firstTimingStepSize * 2;
                for (int i = 1; i <= (noteTime / 2); i++)
                {
                    int step = firstTimingStepSize + ((i - 1) * timingStepSize);
                    if (!this[step].IsEmpty)
                    {
                        areAllEmpty = false;
                        break;
                    }
                }

                if (areAllEmpty)
                {
                    for (int i = 1; i <= (noteTime / 2); i++)
                    {
                        int step = firstTimingStepSize + ((i - 1) * timingStepSize);
                        noteRowsToRemove.Add(this[step]);
                    }
                }
                else
                {
                    break;
                }
            }

            foreach (NoteRow noteToRemove in noteRowsToRemove)
            {
                Remove(noteToRemove);
            }
        }

        public override string ToString()
        {
            StringBuilder output = new();
            foreach (NoteRow noteRow in this)
            {
                output.AppendLine(noteRow.ToString());
            }

            return output.ToString();
        }
    }
}
