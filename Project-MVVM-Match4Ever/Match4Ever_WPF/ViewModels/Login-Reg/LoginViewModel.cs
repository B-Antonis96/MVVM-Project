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
        private readonly Tools Tools = new Tools();
        private readonly DataComs DataCom = new DataComs();

        //ONDERDELEN\\
        public string AccountLogin { get; set; }

        public string Wachtwoord { get; set; }

        //Testen van commands
        public override bool CanExecute(object parameter)
        {
            if (parameter is Commands command)
            {
                switch (command)
                {
                    case Commands.Aanmelden:
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
                        Aanmelden();
                        break;
                }
            }
        }

        //Aanmelden
        public void Aanmelden()
        {
            string resultaat = "Gebruikersnaam en wachtwoord moeten ingevuld zijn!";

            //Controleren op lege velden
            if (Tools.VeldVol(AccountLogin) &&
                Tools.VeldVol(Wachtwoord))
            {
                //Gebruiker proberen inloggen, anders fouten teruggeven
                resultaat = DataCom.LogIn(AccountLogin, Wachtwoord);
            }

            //Controleren of gebruiker is ingelogd
            if (Authenticator.IsIngelogd)
            {
                //Benodigde ViewModels aanmaken
                ViewModelBuilder.ViewModelsAanmaken();

                if (!Authenticator.IsAdmin)
                {
                    //Naar gebruiker gedeelte
                    SwitchViewModel.Execute(ViewType.MenuUser);
                    SwitchViewModel.Execute(ViewType.Welkom);
                }
                else
                {
                    //Naar admin gedeelte
                    SwitchViewModel.Execute(ViewType.MenuAdmin);
                    SwitchViewModel.Execute(ViewType.VoorkeurenWijzigen);

                }
            }
            else
            {
                //Fouten tonen
                MessageBox.Show(resultaat);
            }
        }
    }
}
