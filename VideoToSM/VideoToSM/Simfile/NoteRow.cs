using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace VideoToSM.Simfile
{
    public class NoteRow : List<int>
    {
        public bool IsEmpty => this.Select(n => n != 0).Any();

        public NoteRow()
        {
            for (int note = 0; note < G.KEYS; note++)
            {
                this.Add(0);
            }
        }

        public override string ToString()
        {
            string output = "";
            foreach (int note in this)
            {
                output += note;
            }

            return output;
        }
    }
}
