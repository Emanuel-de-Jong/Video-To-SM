using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoToSM.Simfile
{
    public class NoteBar : List<NoteRow>
    {
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
