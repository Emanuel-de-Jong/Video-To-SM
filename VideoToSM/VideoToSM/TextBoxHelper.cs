using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace VideoToSM
{
    public class TextBoxHelper
    {
        public RichTextBox RichTextBox { get; set; }
        public FlowDocument FlowDocument { get; set; }
        public List<Paragraph> Paragraphs { get; set; } = new();

        public TextBoxHelper(RichTextBox richTextBox)
        {
            RichTextBox = richTextBox;

            FlowDocument = new();
            RichTextBox.Document = FlowDocument;

            Paragraphs.Add(new());
            FlowDocument.Blocks.Add(Paragraphs[0]);
        }

        public void WriteLine(string text, SKColor? color = null)
        {
            Run run = new(text + "\n\r");

            if (color != null)
            {
                SolidColorBrush brush = new((Color)ColorConverter.ConvertFromString(color.ToString()));
                run.Foreground = brush;
            }

            Paragraphs[0].Inlines.Add(run);
        }
    }
}
