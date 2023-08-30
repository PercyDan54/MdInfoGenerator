using System.Windows.Controls;

namespace MdInfoGenerator
{
    /// <summary>
    /// Interaction logic for LabelledTextBox.xaml
    /// </summary>
    public partial class LabelledTextBox : UserControl
    {
        public string LabelText { get; set; }
        public double TextBoxMinWidth { get; set; } = 150;
        public double TextBoxMaxWidth { get; set; } = 200;

        public LabelledTextBox()
        {
            InitializeComponent();
        }
    }
}
