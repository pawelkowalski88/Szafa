using ClothesDetailedViewModule.ViewModels;
using System.Windows.Controls;

namespace ClothesDetailedViewModule.Views
{
    /// <summary>
    /// Interaction logic for ClothesDetailedView.xaml
    /// </summary>
    public partial class ClothesDetailedView : UserControl
    {
        public ClothesDetailedView(ClothesDetailedViewModel viewModel)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };
        }
    }
}
