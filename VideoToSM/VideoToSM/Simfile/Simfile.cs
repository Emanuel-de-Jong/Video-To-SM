using System.Collections.Generic;
using System.Text;

namespace VideoToSM.Simfile
{
    public class Simfile
    {
        public Dictionary<string, string> Metadata { get; set; } = new();
        public List<SimfileDifficulty> difficulties = new();

        public Simfile()
        {
            Metadata.Add("VERSION", "0.83");
            Metadata.Add("TITLE", "Broken");
            Metadata.Add("ARTIST", "Matduke");
            Metadata.Add("TITLETRANSLIT", "");
            Metadata.Add("SUBTITLETRANSLIT", "");
            Metadata.Add("ARTISTTRANSLIT", "");
            Metadata.Add("GENRE", "House");
            Metadata.Add("ORIGIN", "");
            Metadata.Add("TAGS", "");
            Metadata.Add("CREDIT", "");
            Metadata.Add("BANNER", "broken-bn.jpg");
            Metadata.Add("BACKGROUND", "broken-bg.jpg");
            Metadata.Add("PREVIEWVID", "");
            Metadata.Add("JACKET", "broken-jk.jpg");
            Metadata.Add("CDIMAGE", "");
            Metadata.Add("DISCIMAGE", "");
            Metadata.Add("LYRICSPATH", "");
            Metadata.Add("CDTITLE", "");
            Metadata.Add("MUSIC", "broken.opus");
            Metadata.Add("SAMPLESTART", "72.503998");
            Metadata.Add("SAMPLELENGTH", "15.000000");
            Metadata.Add("SELECTABLE", "YES");
        }

        public override string ToString()
        {
            StringBuilder output = new();

            foreach (var kv in Metadata)
            {
                output.Append('#');
                output.Append(kv.Key);
                output.Append(':');
                output.Append(kv.Value);
                output.AppendLine(";");
            }

            foreach (SimfileDifficulty difficulty in difficulties)
            {
                output.AppendLine();
                output.AppendLine(difficulty.ToString());
            }

            return output.ToString();
        }
    }
}
