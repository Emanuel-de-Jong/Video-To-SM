using SkiaSharp;

namespace VideoToSM.VideoDecoder
{
    public class NoteColorGroup
    {
        public SKColor CenterTop { get; set; }
        public SKColor CenterCenter { get; set; }
        public SKColor CenterBottom { get; set; }

        public SKColor LNLeftTop { get; set; }
        public SKColor LNLeftCenter { get; set; }
        public SKColor LNLeftBottom { get; set; }

        public SKColor LNRightTop { get; set; }
        public SKColor LNRightCenter { get; set; }
        public SKColor LNRightBottom { get; set; }

        public NoteColorGroup(SKColor centerTop, SKColor centerCenter, SKColor centerBottom,
            SKColor lnLeftTop, SKColor lnLeftCenter, SKColor lnLeftBottom,
            SKColor lnRightTop, SKColor lnRightCenter, SKColor lnRightBottom)
        {
            CenterTop = centerTop;
            CenterCenter = centerCenter;
            CenterBottom = centerBottom;
            LNLeftTop = lnLeftTop;
            LNLeftCenter = lnLeftCenter;
            LNLeftBottom = lnLeftBottom;
            LNRightTop = lnRightTop;
            LNRightCenter = lnRightCenter;
            LNRightBottom = lnRightBottom;
        }
    }
}
