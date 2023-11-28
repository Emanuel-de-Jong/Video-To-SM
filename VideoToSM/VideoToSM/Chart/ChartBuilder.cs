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
using VideoToSM.VideoDecoder;

namespace VideoToSM.Chart
{
    public class ChartBuilder
    {
        public Chart Chart { get; set; } = new();

        public void ColorsToNote(NoteColorGroup noteColorGroup, int colNum, int frameNum)
        {
            int framesInPause = G.BaseOnFPS(6);

            ChartCol col = Chart.Columns[colNum];

            Note? note = FindNote(noteColorGroup);
            if (note != null)
            {
                if (col.LastAddedNote != null && note.NoteTiming == col.LastAddedNote.NoteTiming &&
                    col.LastAddedFrameNum != null && frameNum - col.LastAddedFrameNum <= framesInPause)
                    return;

                Chart.AddNote(note, colNum, frameNum);
                return;
            }

            bool isLNOngoing = col.FirstLNFrameNum != null;

            bool isLN = FindLN(noteColorGroup);
            if (isLN)
            {
                if (!isLNOngoing)
                    col.FirstLNFrameNum = frameNum;

                col.LastLNFrameNum = frameNum;
                col.LNDetectionCount++;
            }
            else if (isLNOngoing && frameNum - col.LastLNFrameNum > G.BaseOnFPS(1)) // LN ended
            {
                if (frameNum - col.FirstLNFrameNum >= G.BaseOnFPS(8) && col.LNDetectionCount >= G.BaseOnFPS(4))
                {
                    LongNoteStart lnStart = new();
                    lnStart.NoteTiming = col.LastAddedNote.NoteTiming;

                    col.Notes[col.LastAddedKey] = lnStart;

                    LongNoteEnd lnEnd = new();
                    lnEnd.NoteTiming = lnStart.NoteTiming;
                    Chart.AddNote(lnEnd, colNum, frameNum);
                }

                col.ClearLNStats();
            }
        }

        private Note? FindNote(NoteColorGroup noteColorGroup)
        {
            ENoteTiming? noteTiming = null;
            foreach (var color in new SKColor[] { noteColorGroup.CenterTop, noteColorGroup.CenterCenter, noteColorGroup.CenterBottom })
            {
                noteTiming = FindNoteTiming(color);
                if (noteTiming != null)
                    break;
            }

            if (noteTiming == null)
                return null;

            ShortNote note = new();
            note.NoteTiming = noteTiming.Value;
            return note;
        }

        private ENoteTiming? FindNoteTiming(SKColor color)
        {
            SKColor redMinColor = new(181, 26, 10); // 186, 31, 15
            SKColor redMaxColor = new(195, 106, 88); // 190, 101, 83

            SKColor blueMinColor = new(4, 64, 159); // 9, 69, 164
            SKColor blueMaxColor = new(74, 103, 151); // 69, 98, 146

            SKColor yellowMinColor = new(226, 230, 64); // 231, 235, 69
            SKColor yellowMaxColor = new(254, 255, 138); // 249, 253, 133


            if (IsColorInRange(color, KnownColor.Yellow, yellowMinColor, yellowMaxColor))
            {
                return ENoteTiming.Yellow;
            }
            else if (IsColorInRange(color, KnownColor.Red, redMinColor, redMaxColor))
            {
                return ENoteTiming.Red;
            }
            else if (IsColorInRange(color, KnownColor.Blue, blueMinColor, blueMaxColor))
            {
                return ENoteTiming.Blue;
            }
            else
            {
                return null;
            }
        }

        private bool IsColorInRange(SKColor color, KnownColor mainColor, SKColor minColor, SKColor maxColor)
        {
            switch (mainColor)
            {
                case KnownColor.Red:
                    if (color.Red < minColor.Red)
                        return false;
                    break;
                case KnownColor.Blue:
                    if (color.Blue < minColor.Blue)
                        return false;
                    break;
                case KnownColor.Yellow:
                    if (color.Red < minColor.Red ||
                        color.Green <  minColor.Green)
                        return false;
                    break;
            }

            int secondaryColorSum = 0;
            int secondaryMinColorSum = 0;
            int secondaryMaxColorSum = 0;
            switch (mainColor)
            {
                case KnownColor.Red:
                    secondaryColorSum = color.Green + color.Blue;
                    secondaryMinColorSum = minColor.Green + minColor.Blue;
                    secondaryMaxColorSum = maxColor.Green + maxColor.Blue;
                    break;
                case KnownColor.Blue:
                    secondaryColorSum = color.Red + color.Green;
                    secondaryMinColorSum = minColor.Red + minColor.Green;
                    secondaryMaxColorSum = maxColor.Red + maxColor.Green;
                    break;
                case KnownColor.Yellow:
                    secondaryColorSum = color.Blue;
                    secondaryMinColorSum = minColor.Blue;
                    secondaryMaxColorSum = maxColor.Blue;
                    break;
            }

            if (secondaryColorSum >= secondaryMinColorSum && secondaryMinColorSum <= secondaryMaxColorSum)
                return true;

            return false;
        }

        private bool FindLN(NoteColorGroup noteColorGroup)
        {
            return
                FindLNRow(noteColorGroup.LNLeftTop, noteColorGroup.CenterTop, noteColorGroup.LNRightTop) ||
                FindLNRow(noteColorGroup.LNLeftCenter, noteColorGroup.CenterCenter, noteColorGroup.LNRightCenter) ||
                FindLNRow(noteColorGroup.LNLeftBottom, noteColorGroup.CenterBottom, noteColorGroup.LNRightBottom);
        }

        private bool FindLNRow(SKColor leftColor, SKColor centerColor, SKColor rightColor)
        {
            SKColor centerMinColor = new(104, 95, 97); // 109, 100, 102
            SKColor centerMaxColor = new(136, 131, 136); // 131, 126, 131

            SKColor sideMinColor = new(50, 40, 47); // 55, 45, 52
            SKColor sideMaxColor = new(74, 64, 71); // 69, 59, 66

            return
                IsLNInRange(leftColor, sideMinColor, sideMaxColor) &&
                IsLNInRange(centerColor, centerMinColor, centerMaxColor) &&
                IsLNInRange(rightColor, sideMinColor, sideMaxColor);
        }

        private bool IsLNInRange(SKColor color, SKColor minColor, SKColor maxColor)
        {
            if (color.Red < minColor.Red || color.Red > maxColor.Red ||
                color.Green < minColor.Green || color.Green > maxColor.Green ||
                color.Blue < minColor.Blue || color.Blue > maxColor.Blue)
                return false;

            int DIFF_TRESHOLD = 10;
            if (Math.Abs(color.Red - color.Green) > DIFF_TRESHOLD ||
                Math.Abs(color.Red - color.Blue) > DIFF_TRESHOLD ||
                Math.Abs(color.Green - color.Blue) > DIFF_TRESHOLD)
                return false;

            return true;
        }
    }
}
