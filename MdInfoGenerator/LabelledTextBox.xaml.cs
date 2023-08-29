using System.Windows.Controls;

namespace MdInfoGenerator
{
    /// <summary>
    /// Interaction logic for LabelledTextBox.xaml
    /// </summary>
    public partial class LabelledTextBox : UserControl
    {
        public string LabelText { get; set; }

        public LabelledTextBox()
        {
            InitializeComponent();
        }
    }
}
