using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Enums;

namespace VideoToSM.Notes
{
    public class Note
    {
        public ENoteTiming noteTiming { get; set; }
        public decimal Time { get; set; }

        public Note(ENoteTiming noteTiming)
        {
            this.noteTiming = noteTiming;
        }
    }
}
