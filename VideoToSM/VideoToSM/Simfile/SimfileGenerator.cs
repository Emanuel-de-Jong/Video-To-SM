namespace VideoToSM.Simfile
{
    public class SimfileGenerator
    {
        public Simfile? Simfile { get; set; }

        public void Generate()
        {
            if (Simfile == null)
                Simfile = new();

            Chart.Chart chart = G.ChartBuilder.Chart;

            NoteBars noteBars = new();

            int notesLoopCount = chart.LastB64thOrderNumber + (G.NOTE_TIME_ACCURACY - chart.LastB64thOrderNumber % G.NOTE_TIME_ACCURACY);
            for (int orderNumber = 1; orderNumber <= notesLoopCount; orderNumber++)
            {
                noteBars.GenerateRow(chart, orderNumber);
            }

            noteBars.Compress();

            SimfileDifficulty difficulty = new(noteBars.ToString());
            Simfile.difficulties.Add(difficulty);

            G.SCCTextBoxHelper.Clear();
            G.SCCTextBoxHelper.Write(Simfile.ToString());

            G.MessageTextBoxHelper.WriteLine("Video converted");
        }
    }
}
