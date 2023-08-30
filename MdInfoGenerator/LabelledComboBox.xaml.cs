using System.Windows.Controls;

namespace MdInfoGenerator
{
    /// <summary>
    /// Interaction logic for LabelledComboBox.xaml
    /// </summary>
    public partial class LabelledComboBox : UserControl
    {
        public string LabelText { get; set; }
        public double ComboBoxMinWidth { get; set; } = 200;
        public double ComboBoxMaxWidth { get; set; } = 250;

        public LabelledComboBox()
        {
            InitializeComponent();
        }
    }
}
