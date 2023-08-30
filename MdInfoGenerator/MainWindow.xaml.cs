using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace MdInfoGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string selectedPath;
        private readonly MapInfoEditor[] editors;

        public MainWindow()
        {
            InitializeComponent();
            directoryLabelledTextBox.TextBox.IsReadOnly = true;
            hideBmsModeComboBox.ComboBox.ItemsSource = new List<string> { "猛戳难度进入里谱面", "在选歌界面长按封面进入里谱面", "在曲目的三个难度之间切换进入里谱面" };
            hideBmsDifficultyComboBox.ComboBox.ItemsSource = new List<string> { "进入里谱时，只保留里谱面，其余正常谱面全部消失", "进入里谱时，高手难度或大触难度谱面替换为里谱", "进入里谱时，萌新难度替换为里谱面", "进入里谱时，高手难度替换为里谱面", "进入里谱时，大触难度替换为里谱面" };
            editors = new MapInfoEditor[] { map1Editor, map2Editor, map3Editor, map4Editor };
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
            {
                Multiselect = false,
            };

            if (dialog.ShowDialog() == true)
            {
                bool any = false;

                foreach (var editor in editors)
                {
                    string file = Path.Combine(dialog.SelectedPath, editor.Title);
                    if (File.Exists(file))
                    {
                        any |= true;
                        try
                        {
                            processBms(File.ReadAllLines(file), editor);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "ERROR reading " + editor.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

                if (any)
                {
                    exportInfoJsonButton.IsEnabled = true;
                    selectedPath = dialog.SelectedPath;
                    directoryLabelledTextBox.TextBox.Text = selectedPath;
                }
                else
                {
                    MessageBox.Show($"{dialog.SelectedPath} 不包含任何bms文件", "无效的目录", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void processBms(string[] lines, MapInfoEditor editor)
        {
            foreach (string line in lines)
            {
                parseInfo(line, "#TITLE", editor.SongTitleTextBox);
                parseInfo(line, "#ARTIST", editor.AuthorTextBox);
                parseInfo(line, "#LEVELDESIGN", editor.ChartDesignTextBox);
                parseInfo(line, "#GENRE", editor.SceneTextBox);
                parseInfo(line, "#BPM", editor.BpmTextBox);
                parseInfo(line, "#RANK", editor.DifficultyTextBox);
                parseInfo(line, "#PLAYLEVEL", editor.LevelTextBox);
            }
        }

        private void parseInfo(string line, string keyword, LabelledTextBox textBox)
        {
            if (line.StartsWith(keyword))
            {
                textBox.TextBox.Text = line.Substring(keyword.Length).Trim();
            }
        }

        private void exportInfoJsonButton_Click(object sender, RoutedEventArgs e)
        {
            var infoJson = new InfoJson();
            var mapInfos = editors.Select(f => f.GetMapInfo()).ToArray();

            for (int i = 0; i < editors.Length; i++)
            {
                var mapInfo = mapInfos[i];
                setIfPresent(ref infoJson.Name, mapInfo.SongTitle);
                setIfPresent(ref infoJson.Author, mapInfo.Author);
                setIfPresent(ref infoJson.Bpm, mapInfo.Bpm);
                setIfPresent(ref infoJson.Scene, mapInfo.Scene);
                setIfPresent(ref infoJson.LevelDesigner, mapInfo.ChartDesign);
                typeof(InfoJson).GetField("LevelDesigner" + (i + 1)).SetValue(infoJson, mapInfo.ChartDesign);
                typeof(InfoJson).GetField("Difficulty" + (i + 1)).SetValue(infoJson, mapInfo.Dfficulty);
            }

            infoJson.HideBmsMode = ((HideBmsMode)Math.Max(hideBmsModeComboBox.ComboBox.SelectedIndex, 0)).ToString();
            infoJson.HideBmsDifficulty = Math.Max(hideBmsDifficultyComboBox.ComboBox.SelectedIndex - 1, -1);
            infoJson.HideBmsMessage = hideBmsMessageTextBox.TextBox.Text;

            string file = Path.Combine(selectedPath, "info.json");
            try
            {
                File.WriteAllText(file, JsonConvert.SerializeObject(infoJson, Formatting.Indented));
                MessageBox.Show("导出完成", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            void setIfPresent(ref string key, string value)
            {
                if (key == null && !string.IsNullOrEmpty(value)) 
                {
                    key = value;
                }
            }
        }
    }

    enum HideBmsMode
    {
        CLICK,
        PRESS,
        TOGGLE,
    }
}
