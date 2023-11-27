using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoToSM.Simfile
{
    public class NoteBar : List<NoteRow>
    {
        public void Compress()
        {
            int[] noteTimes = { 64, 32, 16, 8 };

            foreach (int noteTime in noteTimes)
            {
                bool areAllEmpty = true;

                for (int i = 1; i < noteTime; i++)
                {
                    if (Count >= i && !this[i].IsEmpty)
                    {
                        areAllEmpty = false;
                        break;
                    }
                }

                if (areAllEmpty)
                {
                    List<NoteRow> cachedNoteRows = new(this);

                    for (int i = 1; i < noteTime; i++)
                    {
                        if (cachedNoteRows.Count >= i)
                        {
                            Remove(cachedNoteRows[i]);
                        }
                    }
                }
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
