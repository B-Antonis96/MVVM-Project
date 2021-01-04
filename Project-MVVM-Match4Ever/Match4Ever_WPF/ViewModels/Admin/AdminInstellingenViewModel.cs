using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Match4Ever_WPF.ViewModels.Admin
{
    public class AdminInstellingenViewModel : BasisViewModel
    {
        //BENODIGDHEDEN\\
        private readonly AuthenticatorInstellingen Instellingen = new AuthenticatorInstellingen();

        //ONDERDELEN\\
        public string HuidigWachtwoord { get; set; }
        public string NieuwWachtwoord { get; set; }
        public string NieuwWachtwoordBevestiging { get; set; }
        public string AccountInfo { get; set; }

        //UPDATEVIEWMODEL\\
        public void UpdateAdminInstellingenViewModel()
        {
            //Inladen van waardes uit de Authenticator
            AccountInfo = $"    " +
             $"{Authenticator.HuidigAccount.Gebruikersnaam}\n    " +
             $"{Authenticator.HuidigAccount.Emailadres}\n    ";

            //Bij update wachtwoordvelden leegmaken
            HuidigWachtwoord = null;
            NieuwWachtwoord = null;
            NieuwWachtwoordBevestiging = null;
        }


        //Testen van commands
        public override bool CanExecute(object parameter)
        {
            if (parameter is Commands command)
            {
                switch (command)
                {
                    case Commands.Opslaan:
                        return true;
                    case Commands.Verwijder:
                        return true;
                    case Commands.Afmelden:
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
                    case Commands.Opslaan:
                        Opslaan();
                        break;
                    case Commands.Verwijder:
                        Instellingen.VerwijderAccount();
                        break;
                    case Commands.Afmelden:
                        Instellingen.Afmelden();
                        break;
                    case Commands.Update:
                        UpdateAdminInstellingenViewModel();
                        break;
                }
            }
        }


        //COMMANDS\\

        //Opslaan
        public void Opslaan()
        {
            MessageBox.Show(Instellingen.WachtwoordWijzigen(HuidigWachtwoord, NieuwWachtwoord, NieuwWachtwoordBevestiging));
        }
    }
}
