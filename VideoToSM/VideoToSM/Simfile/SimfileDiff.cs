using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoToSM.Simfile
{
    public class SimfileDiff
    {
        public Dictionary<string, string> Metadata { get; set; } = new();
        public string Notes { get; set; }

        public SimfileDiff(string notes)
        {
            Notes = notes;

            Metadata.Add("NOTEDATA", "");
            Metadata.Add("CHARTNAME", "");
            Metadata.Add("CHARTHASH", "1364798683");
            Metadata.Add("STEPSTYPE", "smx-single");
            Metadata.Add("BANNER", "");
            Metadata.Add("DESCRIPTION", "");
            Metadata.Add("CHARTSTYLE", "");
            Metadata.Add("DIFFICULTY", "Beginner");
            Metadata.Add("METER", "5");
            Metadata.Add("METERF", "0.000000");
            Metadata.Add("LASTSECONDHINT", "13.528890");
            Metadata.Add("RADARVALUES", "0.000000");
            Metadata.Add("CREDIT", "");
            Metadata.Add("OFFSET", "-0.3");
            Metadata.Add("BPMS", "0.000000=" + G.BPM);
            Metadata.Add("STOPS", "");
            Metadata.Add("DELAYS", "");
            Metadata.Add("WARPS", "");
            Metadata.Add("TIMESIGNATURES", "0.000000=4=4");
            Metadata.Add("TICKCOUNTS", "0.000000=4");
            Metadata.Add("COMBOS", "0.000000=1");
            Metadata.Add("SPEEDS", "0.000000=1.000000=0.000000=0");
            Metadata.Add("SCROLLS", "0.000000=1.000000");
            Metadata.Add("FAKES", "");
            Metadata.Add("LABELS", "0.000000=Song Start");
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

            output.AppendLine("#NOTES:");
            output.Append(Notes);
            output.AppendLine(";");

            return output.ToString();
        }
    }
}
