using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Notes;

namespace VideoToSM.Chart
{
    public class Chart
    {
        public List<ChartCol> Columns { get; set; } = new();

        public Chart()
        {
            Columns.Add(new ChartCol());
            Columns.Add(new ChartCol());
            Columns.Add(new ChartCol());
            Columns.Add(new ChartCol());
            Columns.Add(new ChartCol());
        }

        public void AddNote(Note note, int colNum)
        {
            Columns[colNum].Notes.Add(note);
        }
    }
}
