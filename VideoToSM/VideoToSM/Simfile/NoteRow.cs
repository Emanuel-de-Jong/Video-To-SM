using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoToSM.Simfile
{
    public class NoteRow : List<string>
    {
        public bool IsEmpty => !this.Where(n => n != "0").Any();

        public NoteRow()
        {
            for (int note = 0; note < G.KEYS; note++)
            {
                Add("0");
            }
        }

        public override string ToString()
        {
            StringBuilder output = new();
            foreach (string note in this)
            {
                output.Append(note);
            }

            return output.ToString();
        }
    }
}
