using NavigationViewModule.ViewModels;
using System.Windows.Controls;

namespace NavigationViewModule.Views
{
    /// <summary>
    /// Interaction logic for NavigaitonView.xaml
    /// </summary>
    public partial class NavigaitonView : UserControl
    {
        public NavigaitonView(NavigationViewModel viewModel)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                DataContext = viewModel;
            };
        }
    }
}
