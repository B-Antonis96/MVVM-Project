﻿using System;
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
using Match4Ever_WPF.ViewModels.Props;

namespace Match4Ever_WPF.Components
{
    /// <summary>
    /// Interaction logic for StartKnop.xaml
    /// </summary>
    public partial class StartKnop : UserControl
    {
        public StartKnop()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.Visibility = Visibility.Collapsed; //Niet zo zeer MVVM vriendelijk, maar is de beste manier om deze bepaalde knop te verbergen!
        }
    }
}