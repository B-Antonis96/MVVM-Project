using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Login_Reg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.State.Commands
{
    public class UpdateHuidigViewModelCommand : ICommand //Geïmplementeerd uit het voorbeeld van YouTuber SingletonSean! => een ware held!
    {
        public event EventHandler CanExecuteChanged;

        public static INavigator Navigator { get; set; } = new Navigator();

        public UpdateHuidigViewModelCommand(INavigator navigator)
        {
            Navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                switch (viewType)
                {
                    //LOGIN & REGISTRATIE COMMANDS
                    case ViewType.Login:
                        Navigator.HuidigViewModel = new LoginViewModel();
                        break;
                    case ViewType.Registreer:
                        Navigator.HuidigViewModel = new RegistreerViewModel();
                        break;
                    case ViewType.Wachtwoord:
                        Navigator.HuidigViewModel = new WachtwoordViewModel();
                        break;
                }
            }
        }
    }
}
