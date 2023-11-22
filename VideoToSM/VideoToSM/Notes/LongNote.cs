using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Enums;

namespace VideoToSM.Notes
{
    public class LongNote : Note
    {
        public decimal Length { get; set; }

        public LongNote(ENoteTiming noteTiming) : base(noteTiming)
        {

        }
    }
}
