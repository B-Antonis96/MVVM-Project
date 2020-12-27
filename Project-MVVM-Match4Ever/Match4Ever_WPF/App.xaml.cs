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
using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.State.Authenticators;

namespace Match4Ever_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) //Geïmplementeerd uit het voorbeeld van de lessen (BindablePasswordBox)!
        {
            Window startWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };

            startWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //Gebruiker uitloggen wanneer venster sluit
            DataComs DataCom = new DataComs();
            DataCom.LogUit(true);
            base.OnExit(e);
        }
    }
}
