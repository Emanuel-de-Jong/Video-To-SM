using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Chart;

namespace VideoToSM.Simfile
{
    public class SimfileGen
    {
        public Simfile Simfile { get; set; } = new();

        public void Generate()
        {
            Chart.Chart chart = G.ChartBuilder.Chart;

            NoteRows noteRows = new();

            int notesLoopCount = chart.LastB64thOrderNumber + (64 - chart.LastB64thOrderNumber % 64);
            for (int orderNumber = 1; orderNumber <= notesLoopCount; orderNumber++)
            {
                noteRows.GenerateRow(chart, orderNumber);
            }

            Simfile.Notes = noteRows.ToString();

            G.TextBoxHelper.Write(Simfile.Notes);
        }
    }
}
