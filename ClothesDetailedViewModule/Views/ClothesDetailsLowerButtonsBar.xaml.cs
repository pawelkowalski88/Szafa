
using ClothesDetailedViewModule.ViewModels;
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

namespace ClothesDetailedViewModule.Views
{
    /// <summary>
    /// Interaction logic for ClothesDetailsLowerButtonsBar.xaml
    /// </summary>
    public partial class ClothesDetailsLowerButtonsBar : UserControl
    {
        public ClothesDetailsLowerButtonsBar(ClothesDetailsLowerButtonsBarViewModel viewModel)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };
        }
    }
}
