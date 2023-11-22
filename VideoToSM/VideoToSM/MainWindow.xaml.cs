using System.Windows;
using VideoToSM.VideoDecoder;

namespace VideoToSM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public VideoReader VideoReader { get; set; }
        public TextBoxHelper TextBoxHelper { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            TextBoxHelper = new(MainRichTextBox);
            VideoReader = new(TextBoxHelper);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                VideoReader.Read(files[0]);
            }
        }
    }
}
