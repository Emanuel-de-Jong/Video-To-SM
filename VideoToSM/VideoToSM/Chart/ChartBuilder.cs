using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoToSM.Enums;
using VideoToSM.Notes;

namespace VideoToSM.Chart
{
    public class ChartBuilder
    {
        public Chart Chart { get; set; } = new();

        public void ColorToNote(SKColor color, int colNum)
        {
            ENoteTiming? noteTiming = ColorToENoteTiming(color);
            if (noteTiming != null)
            {
                Chart.AddNote(new Note(noteTiming.Value), colNum);
            }
        }

        private ENoteTiming? ColorToENoteTiming(SKColor color)
        {
            SKColor redMin = new(0, 0, 0);
            SKColor redMax = new(255, 255, 255);
            if (IsColorInRange(color, redMin, redMax))
            {
                return ENoteTiming.Red;
            } 
            else
            {
                return null;
            }
        }

        private bool IsColorInRange(SKColor color, SKColor minColor, SKColor maxColor)
        {
            if (color.Red >= minColor.Red && color.Red <= maxColor.Red &&
                color.Green >= minColor.Green && color.Green <= maxColor.Green &&
                color.Blue >= minColor.Blue && color.Blue <= maxColor.Blue)
                return true;

            return false;
        }
    }
}
