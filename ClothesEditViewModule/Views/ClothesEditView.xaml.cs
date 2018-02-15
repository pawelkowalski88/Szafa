﻿using ClothesEditViewModule.ViewModels;
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