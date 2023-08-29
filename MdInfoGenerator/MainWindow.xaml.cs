using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MdInfoGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string selectedPath;
        private readonly KeyValuePair<string, MapInfoEditor>[] editors;

        public MainWindow()
        {
            InitializeComponent();
            directoryLabelledTextBox.TextBox.MinWidth = 600;
            directoryLabelledTextBox.TextBox.IsReadOnly = true;
            editors = new KeyValuePair<string, MapInfoEditor>[]
            {
                new KeyValuePair<string, MapInfoEditor>("map1.bms", map1Editor),
                new KeyValuePair<string, MapInfoEditor>("map2.bms", map2Editor),
                new KeyValuePair<string, MapInfoEditor>("map3.bms", map3Editor),
                new KeyValuePair<string, MapInfoEditor>("map4.bms", map4Editor),
            };
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
            {
                Multiselect = false,
            };

            if (dialog.ShowDialog() == true)
            {
                selectedPath = dialog.SelectedPath;
                directoryLabelledTextBox.TextBox.Text = selectedPath;
                foreach (var item in editors)
                {
                    string file = Path.Combine(selectedPath, item.Key);
                    if (File.Exists(file))
                    {
                        processBms(File.ReadAllLines(file), item.Value);
                    }
                }
            }
        }

        private void processBms(string[] lines, MapInfoEditor editor)
        {
            foreach (string line in lines)
            {
                stripInfo(line, "#TITLE", editor.SongTitleTextBox);
                stripInfo(line, "#ARTIST", editor.AuthorTextBox);
                stripInfo(line, "#LEVELDESIGN", editor.ChartDesignTextBox);
                stripInfo(line, "#GENRE", editor.SceneTextBox);
                stripInfo(line, "#BPM", editor.BpmTextBox);
                stripInfo(line, "#RANK", editor.DifficultyTextBox);
                stripInfo(line, "#PLAYLEVEL", editor.LevelTextBox);
            }
        }

        private void stripInfo(string line, string keyword, LabelledTextBox textBox)
        {
            if (line.StartsWith(keyword))
            {
                textBox.TextBox.Text = line.Substring(keyword.Length);
            }
        }
    }
}
