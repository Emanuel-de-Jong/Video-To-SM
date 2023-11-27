using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Chart;

namespace VideoToSM.Simfile
{
    public class NoteRows : List<NoteRow>
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

            Add(noteRow);
        }

        public override string ToString()
        {
            StringBuilder output = new();
            for (int i = 0; i < Count; i++)
            {
                output.AppendLine(this[i].ToString());

                if ((i + 1) % 64 == 0 && (i + 1) != Count)
                {
                    output.AppendLine(",");
                }
            }

            output.Append(';');

            return output.ToString();
        }
    }
}
