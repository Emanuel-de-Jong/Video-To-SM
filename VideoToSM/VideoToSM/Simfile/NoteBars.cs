using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Chart;

namespace VideoToSM.Simfile
{
    public class NoteBars : List<NoteBar>
    {
        public void GenerateRow(Chart.Chart chart, int orderNumber)
        {
            NoteRow noteRow = new();

            for (int i = 0; i < chart.Columns.Count; i++)
            {
                ChartCol chartCol = chart.Columns[i];
                if (chartCol.Notes.ContainsKey(orderNumber))
                {
                    noteRow[i] = chartCol.Notes[orderNumber].Id;
                }
            }

            int index = (orderNumber - 1) / G.NOTE_TIME_ACCURACY;
            if (Count < (index + 1))
            {
                Add(new NoteBar());
            }

            this[index].Add(noteRow);
        }

        public void Compress()
        {
            foreach (NoteBar noteBar in this)
            {
                noteBar.Compress();
            }
        }

        public override string ToString()
        {
            StringBuilder output = new();
            foreach (NoteBar noteBar in this)
            {
                output.Append(noteBar.ToString());

                if (noteBar != this.Last())
                {
                    output.AppendLine(",");
                }
            }

            output.Append(';');

            return output.ToString();
        }
    }
}
