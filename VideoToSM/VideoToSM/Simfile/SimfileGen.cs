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

            StringBuilder notesSB = new();

            int notesLoopCount = chart.LastB64thOrderNumber + (64 - chart.LastB64thOrderNumber % 64);
            for (int i = 1; i <= notesLoopCount; i++)
            {
                foreach (ChartCol chartCol in chart.Columns)
                {
                    notesSB.Append(chartCol.Notes.ContainsKey(i) ? chartCol.Notes[i].Id : 0);
                }
                notesSB.AppendLine();

                if (i % 64 == 0 && i != notesLoopCount)
                {
                    notesSB.AppendLine(",");
                }
            }

            notesSB.Append(";");

            Simfile.Notes = notesSB.ToString();

            G.TextBoxHelper.Write(Simfile.Notes);
        }
    }
}
