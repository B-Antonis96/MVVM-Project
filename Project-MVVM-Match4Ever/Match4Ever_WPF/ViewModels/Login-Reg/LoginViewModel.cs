using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match4Ever_WPF.ViewModels.Props;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Navigators;
using System.Windows.Input;
using Match4Ever_WPF.WPFTools;
using System.Windows;

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class LoginViewModel : BasisViewModel
    {
        //SCHERM CONTROLE\\
        public ICommand SwitchViewModel => Navigator.StaticNavigator.SwitchViewModel;

        //BENODIGDHEDEN\\
        private readonly AuthenticatorInstellingen Instellingen = new AuthenticatorInstellingen();

        //ONDERDELEN\\
        public string AccountLogin { get; set; }
        public string Wachtwoord { get; set; }

        //UPDATEVIEWMODEL\\
        public void UpdateLoginViewModel()
        {
            AccountLogin = null;
        }

        //Testen van commands
        public override bool CanExecute(object parameter)
        {
            if (parameter is Commands command)
            {
                switch (command)
                {
                    case Commands.Aanmelden:
                        return true;
                    case Commands.Update:
                        return true;
                }
            }
            return false;
        }

        //Uitvoeren van commands
        public override void Execute(object parameter)
        {
            if (parameter is Commands command)
            {
                switch (command)
                {
                    case Commands.Aanmelden:
                        Instellingen.LogIn(AccountLogin, Wachtwoord);
                        break;
                    case Commands.Update:
                        UpdateLoginViewModel();
                        break;
                }
            }
        }
    }
}
