using ClothesEditViewModule.ViewModels;
using System.Windows.Controls;

namespace ClothesEditViewModule.Views
{
    /// <summary>
    /// Interaction logic for ClothesEditView.xaml
    /// </summary>
    public partial class ClothesEditView : UserControl
    {
        public ClothesEditView(ClothesEditViewModel viewModel)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };
        }
    }
}
