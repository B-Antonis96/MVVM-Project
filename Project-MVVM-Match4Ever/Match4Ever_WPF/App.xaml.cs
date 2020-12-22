using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Match4Ever_WPF.Views;
using Match4Ever_WPF.ViewModels;
using System.Windows.Input;
using Match4Ever_WPF.ViewModels.Props;
using Match4Ever_WPF.ViewModels.Login_Reg;

namespace Match4Ever_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application //Geïmplementeerd uit het voorbeeld van Kilian (BindablePasswordBox)!
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Window startWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };

            startWindow.Show();

            base.OnStartup(e);
        }
    }
}
