using System.Windows;

namespace VideoToSM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public VideoReader VideoReader { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            VideoReader.MainLabel = MainLabel;
            VideoReader = new();
        }

        private void MainGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                MainLabel.Content = files[0];

                VideoReader.Read(files[0]);
            }
        }
    }
}
