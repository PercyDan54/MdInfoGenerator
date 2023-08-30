using System.Windows.Controls;

namespace MdInfoGenerator
{
    /// <summary>
    /// Interaction logic for MapInfoEditor.xaml
    /// </summary>
    public partial class MapInfoEditor : UserControl
    {
        public string Title { get; set; }

        public MapInfoEditor()
        {
            InitializeComponent();
        }

        public MapInfo GetMapInfo()
        {
            return new MapInfo
            {
                SongTitle = SongTitleTextBox.TextBox.Text,
                Author = AuthorTextBox.TextBox.Text,
                ChartDesign = string.IsNullOrEmpty(ChartDesignTextBox.TextBox.Text) ? "?" : ChartDesignTextBox.TextBox.Text,
                Scene = SceneTextBox.TextBox.Text,
                Bpm = BpmTextBox.TextBox.Text,
                Dfficulty = string.IsNullOrEmpty(DifficultyTextBox.TextBox.Text) ? "?" : DifficultyTextBox.TextBox.Text,
                Level = string.IsNullOrEmpty(LevelTextBox.TextBox.Text) ? "?" : LevelTextBox.TextBox.Text,
            };
        }
    }
}
