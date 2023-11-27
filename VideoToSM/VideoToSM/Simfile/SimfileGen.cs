using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Chart;
using VideoToSM.Notes;

namespace VideoToSM.Simfile
{
    public class SimfileGen
    {
        public Simfile Simfile { get; set; } = new();

        public void Generate()
        {
            Chart.Chart chart = G.ChartBuilder.Chart;

            // TEMP
            chart.Columns[0].AddNote(new ShortNote(), chart.LastB64thOrderNumber + 3, chart.Columns[0].LastAddedFrameNum.Value + 9);
            // END TEMP

            NoteBars noteBars = new();

            int notesLoopCount = chart.LastB64thOrderNumber + (G.NOTE_TIME_ACCURACY - chart.LastB64thOrderNumber % G.NOTE_TIME_ACCURACY);
            for (int orderNumber = 1; orderNumber <= notesLoopCount; orderNumber++)
            {
                noteBars.GenerateRow(chart, orderNumber);
            }

            noteBars.Compress();
            Simfile.Notes = noteBars.ToString();

            G.TextBoxHelper.Write(Simfile.Notes);
        }
    }
}
