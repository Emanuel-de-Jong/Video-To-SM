using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using VideoToSM.Enums;

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

            foreach (ESMXDifficultyType difficultyType in Enum.GetValues(typeof(ESMXDifficultyType)))
            {
                ComboBoxItem item = new();
                item.Content = difficultyType;
                DifficultyTypeComboBox.Items.Add(item);
            }

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
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            G.ChartBuilder.Chart = new();
            G.VideoReader.Read(files[0], ReadLeftSideCheckBox.IsChecked.Value);
            G.SimfileGen.Generate();
        }

        private void DifficultyTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)DifficultyTypeComboBox.SelectedItem;
            G.DifficultyType = (ESMXDifficultyType)item.Content;
        }

        private void BrowseVideoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDlg = new();

            bool? result = openFileDlg.ShowDialog();
            if (result == null || result == false)
                return;

            VideoPathTextBox.Text = openFileDlg.FileName;
        }

        private void BrowseVideoButton_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            VideoPathTextBox.Text = files[0];
        }

        private void BrowseAudioButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDlg = new();

            bool? result = openFileDlg.ShowDialog();
            if (result == null || result == false)
                return;

            AudioPathTextBox.Text = openFileDlg.FileName;
        }

        private void BrowseAudioButton_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            AudioPathTextBox.Text = files[0];
        }

        private void CutVideoButton_Click(object sender, RoutedEventArgs e)
        {
            VideoCutter videoCutter = new();
            videoCutter.Cut(
                VideoPathTextBox.Text,
                AudioPathTextBox.Text,
                DateTimeOffset.Parse("00:" + VideoStartTimeTextBox.Text),
                DateTimeOffset.Parse("00:" + VideoEndTimeTextBox.Text),
                DateTimeOffset.Parse("00:" + AudioStartTimeTextBox.Text),
                DateTimeOffset.Parse("00:" + AudioEndTimeTextBox.Text));
        }
    }
}
