using System.Windows.Controls;
using ClothesListModule.ViewModels;

namespace ClothesListModule.Views
{
    /// <summary>
    /// Interaction logic for ClothesListView.xaml
    /// </summary>
    public partial class ClothesListView : UserControl
    {
        public ClothesListView(ClothesListViewModel viewModel)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };
        }
    }
}
