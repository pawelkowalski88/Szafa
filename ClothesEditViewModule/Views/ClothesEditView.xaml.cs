using System.Windows.Controls;
using SzafaInterfaces;

namespace ClothesEditViewModule.Views
{
    /// <summary>
    /// Interaction logic for ClothesEditView.xaml
    /// </summary>
    public partial class ClothesEditView : UserControl
    {
        public ClothesEditView(IClothesEditViewModel viewModel)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };
        }
    }
}
