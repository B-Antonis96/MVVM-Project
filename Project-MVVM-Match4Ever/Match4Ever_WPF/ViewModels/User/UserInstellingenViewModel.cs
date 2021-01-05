using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.Styles.TestImages;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static Match4Ever_WPF.WPFTools.WPFEnums;

namespace Match4Ever_WPF.ViewModels.User
{
    public class UserInstellingenViewModel : BasisViewModel
    {
        //BENODIGDHEDEN\\
        private readonly DataComs DataCom = new DataComs();
        private readonly AuthenticatorInstellingen Instellingen = new AuthenticatorInstellingen();
        private readonly FotoLinks FotoLinksClass = new FotoLinks();
        private string[] AccountChecks { get; set; }

        //ONDERDELEN\\
        public string HuidigWachtwoord { get; set; }
        public string NieuwWachtwoord { get; set; }
        public string NieuwWachtwoordBevestiging { get; set; }
        public string AccountInfo { get; set; }
        public List<string> GeaardheidLijst { get; set; }
        public string Geaardheid { get; set; }
        public List<string> FotoLinks { get; set; }
        public string FotoLink { get; set; }
        public ImageBrush Foto { get; set; }
        public List<string> StadLijst { get; set; }
        public string Stad { get; set; }

        //UPDATEVIEWMODEL\\
        public void UpdateUserInstellingenViewModel()
        {
            //Inladen van waardes uit de Authenticator en database
            string[] acountChecks = { Authenticator.HuidigAccount.Geaardheid,
                Authenticator.HuidigAccount.ProfielfotoLink,
                Authenticator.HuidigeLocatie.Stad };
            AccountChecks = acountChecks;
            AccountInfo = $"    " +
                         $"{Authenticator.HuidigAccount.Gebruikersnaam}\n    " +
                         $"{Authenticator.HuidigAccount.Emailadres}\n    " +
                         $"{Authenticator.HuidigAccount.Geslacht}\n    ";

            FotoLinks = FotoLinksClass.FotoLinksMethode().ToList();
            FotoLink = Authenticator.HuidigAccount.ProfielfotoLink;
            Foto = Instellingen.ImageBuilder(FotoLink);
            StadLijst = DataCom.LocatiesNamenOphalen();
            GeaardheidLijst = Enum.GetNames(typeof(Geaardheden)).ToList();
            Geaardheid = Authenticator.HuidigAccount.Geaardheid;
            Stad = Authenticator.HuidigeLocatie.Stad;
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
                        UpdateUserInstellingenViewModel();
                        break;
                }
            }
        }


        //COMMANDS\\

        //Opslaan
        public void Opslaan()
        {
            string resultaat = "Er moet minstens 1 veld aangepast worden!";
            if (Instellingen.GegevensWijzigen(Geaardheid, FotoLink, Stad, AccountChecks))
            {
                resultaat = Instellingen.HuidigAccountAanpassen(FotoLink, Geaardheid, Stad, AccountChecks[2]);
            }
            if (Instellingen.WachtwoordVelden(HuidigWachtwoord, NieuwWachtwoord, NieuwWachtwoordBevestiging))
            {
                resultaat = Instellingen.WachtwoordWijzigen(HuidigWachtwoord, NieuwWachtwoord, NieuwWachtwoordBevestiging);
            }
            MessageBox.Show(resultaat);
            UpdateUserInstellingenViewModel();
        }
    }
}
