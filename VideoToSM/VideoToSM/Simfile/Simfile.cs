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
            Metadata.Add("VERSION", "1.0");
            Metadata.Add("TITLE", "");
            Metadata.Add("ARTIST", "");
            //Metadata.Add("TITLETRANSLIT", "");
            //Metadata.Add("SUBTITLETRANSLIT", "");
            //Metadata.Add("ARTISTTRANSLIT", "");
            Metadata.Add("GENRE", "");
            Metadata.Add("ORIGIN", "StepManiaX");
            Metadata.Add("TAGS", "StepManiaX");
            Metadata.Add("CREDIT", "StepManiaX");
            Metadata.Add("BANNER", "bn.jpg");
            Metadata.Add("BACKGROUND", "bg.jpg");
            //Metadata.Add("PREVIEWVID", "");
            Metadata.Add("JACKET", "jk.jpg");
            //Metadata.Add("CDIMAGE", "");
            //Metadata.Add("DISCIMAGE", "");
            //Metadata.Add("LYRICSPATH", "");
            //Metadata.Add("CDTITLE", "");
            Metadata.Add("MUSIC", "music.opus");
            //Metadata.Add("SAMPLESTART", "72.503998");
            //Metadata.Add("SAMPLELENGTH", "15.000000");
            //Metadata.Add("SELECTABLE", "YES");
            Metadata.Add("BPMS", "0=" + G.BPM);
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
