using System.Windows;
using VideoToSM.VideoDecoder;

namespace VideoToSM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            G.MessageTextBoxHelper = new(MessageRichTextBox);
            G.SCCTextBoxHelper = new(SCCRichTextBox);
        }

        private void BPMTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (int.TryParse(BPMTextBox.Text, out int newBPM))
            {
                G.BPM = newBPM;
            }
        }

        private void Label_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                G.VideoReader.Read(files[0]);
                G.SimfileGen.Generate();
            }
        }
    }
}
