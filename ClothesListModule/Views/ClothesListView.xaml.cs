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
