using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Match4Ever_WPF.Views;
using Match4Ever_WPF.ViewModels;

namespace Match4Ever_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LoginView loginView = new LoginView();
            LoginViewModel loginViewModel = new LoginViewModel();
            loginView.DataContext = loginViewModel;
            loginView.Show();
        }
    }
}
