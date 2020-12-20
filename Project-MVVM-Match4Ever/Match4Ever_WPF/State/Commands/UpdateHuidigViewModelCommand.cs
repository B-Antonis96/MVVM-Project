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
    public class UpdateHuidigViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;

        public UpdateHuidigViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                switch (viewType)
                {
                    //LOGIN & REGISTRATIE COMMANDS
                    case ViewType.Login:
                        _navigator.HuidigViewModel = new LoginViewModel(_navigator);
                        break;
                    case ViewType.Registreer:
                        _navigator.HuidigViewModel = new RegistreerViewModel(_navigator);
                        break;
                    case ViewType.Wachtwoord:
                        _navigator.HuidigViewModel = new WachtwoordViewModel(_navigator);
                        break;
                }
            }
        }
    }
}
