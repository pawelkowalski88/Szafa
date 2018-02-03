using NavigationViewModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
