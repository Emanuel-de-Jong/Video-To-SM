using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using VideoToSM.Enums;
using VideoToSM.Notes;

namespace VideoToSM.Chart
{
    public class ChartBuilder
    {
        public Chart Chart { get; set; } = new();

        public void ColorsToNote(List<SKColor> colors, int colNum, int frameNum)
        {
            int framesInPause = (int)Math.Round(3 * (G.FPS / 30));

            ENoteTiming? noteTiming = null;
            foreach (var color in colors)
            {
                noteTiming = ColorToENoteTiming(color);
                if (noteTiming != null)
                    break;
            }

            if (noteTiming == null)
                return;

            ChartCol col = Chart.Columns[colNum];
            if (col.LastNoteTiming != null && noteTiming == col.LastNoteTiming &&
                col.LastNoteFrameNum != null && frameNum - col.LastNoteFrameNum <= framesInPause)
                return;

            col.LastNoteTiming = noteTiming;
            col.LastNoteFrameNum = frameNum;

            Note note = new();
            note.NoteTiming = noteTiming.Value;
            Chart.AddNote(note, colNum, frameNum);
        }

        private ENoteTiming? ColorToENoteTiming(SKColor color)
        {
            SKColor redMin = new(181, 26, 10); // 186, 31, 15
            SKColor redMax = new(195, 106, 88); // 190, 101, 83

            SKColor blueMin = new(4, 64, 159); // 9, 69, 164
            SKColor blueMax = new(74, 103, 151); // 69, 98, 146

            if (IsColorInRange(color, KnownColor.Red, redMin, redMax))
            {
                return ENoteTiming.Red;
            }
            //else if (IsColorInRange(color, KnownColor.Blue, blueMin, blueMax))
            //{
            //    return ENoteTiming.Blue;
            //}
            else
            {
                return null;
            }
        }

        private bool IsColorInRange(SKColor color, KnownColor mainColor, SKColor minColor, SKColor maxColor)
        {
            switch(mainColor)
            {
                case KnownColor.Red:
                    if (color.Red < minColor.Red)
                        return false;
                    break;
                case KnownColor.Blue:
                    if (color.Blue < minColor.Blue)
                        return false;
                    break;
            }

            int secondaryColorSum = (
                mainColor != KnownColor.Red ? color.Red : 0 +
                mainColor != KnownColor.Green ? color.Green : 0 +
                mainColor != KnownColor.Blue ? color.Blue : 0);
            int secondaryMinColorSum = (
                mainColor != KnownColor.Red ? minColor.Red : 0 +
                mainColor != KnownColor.Green ? minColor.Green : 0 +
                mainColor != KnownColor.Blue ? minColor.Blue : 0);
            int secondaryMaxColorSum = (
                mainColor != KnownColor.Red ? maxColor.Red : 0 +
                mainColor != KnownColor.Green ? maxColor.Green : 0 +
                mainColor != KnownColor.Blue ? maxColor.Blue : 0);

            if (secondaryColorSum >= secondaryMinColorSum && secondaryMinColorSum <= secondaryMaxColorSum)
                return true;

            return false;
        }
    }
}
