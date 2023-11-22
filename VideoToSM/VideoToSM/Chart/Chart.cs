using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
