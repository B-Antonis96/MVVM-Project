using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.Styles.TestImages;
using Match4Ever_WPF.ViewModels.Props;
using Match4Ever_WPF.WPFTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Match4Ever_WPF.WPFTools.WPFEnums;

namespace Match4Ever_WPF.ViewModels.User
{
    public class UserInstellingenViewModel : BasisViewModel
    {
        //BENODIGDHEDEN\\
        private readonly DataComs DataCom = new DataComs();
        private readonly Tools Tools = new Tools();
        private readonly FotoLinks FotoLinksClass = new FotoLinks();
        private string[] AccountChecks { get; set; }

        //CONSTRUCTOR\\
        public UserInstellingenViewModel()
        {
            UpdateUserInstellingenViewModel();
        }

        //UPDATEVIEWMODEL\\
        public void UpdateUserInstellingenViewModel()
        {
            string[] acountChecks = { Authenticator.HuidigAccount.Geaardheid,
                Authenticator.HuidigAccount.ProfielfotoLink,
                Authenticator.HuidigeLocatie.Stad };
            AccountChecks = acountChecks;
            AccountInfo = $"    " +
                         $"{Authenticator.HuidigAccount.Gebruikersnaam}\n    " +
                         $"{Authenticator.HuidigAccount.Emailadres}\n    " +
                         $"{Authenticator.HuidigAccount.Geslacht}\n    ";

            FotoLinks = FotoLinksClass.FotoLinksMethode();
            FotoLink = Authenticator.HuidigAccount.ProfielfotoLink;
            Foto = ImageBuilder(FotoLink);
            StadLijst = DataCom.LocatiesOphalen();
            GeaardheidLijst = Enum.GetNames(typeof(Geaardheden)).ToList();
            Geaardheid = Authenticator.HuidigAccount.Geaardheid;
            Stad = Authenticator.HuidigeLocatie.Stad;
        }

        //ONDERDELEN\\
        public string HuidigWachtwoord { get; set; }
        public string NiewWachtwoord { get; set; }
        public string NiewWachtwoordBevestiging { get; set; }
        public string AccountInfo { get; set; }
        public List<string> GeaardheidLijst { get; set; }
        public string Geaardheid { get; set; }
        public List<string> FotoLinks { get; set; }
        public string FotoLink { get; set; }
        public ImageBrush Foto { get; set; }
        public List<string> StadLijst { get; set; }
        public string Stad { get; set; }

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
                        VerwijderAccount();
                        break;
                    case Commands.Afmelden:
                        Afmelden();
                        break;
                }
            }
        }


        //COMMANDS\\

        //Opslaan
        public void Opslaan()
        {
            string resultaat = "Er moet minstens 1 veld aangepast worden!";
            if (GegevensWijzigen())
            {
                Authenticator.HuidigAccount.ProfielfotoLink = FotoLink;
                Authenticator.HuidigAccount.Geaardheid = Geaardheid;
                if (AccountChecks[2] != Stad)
                {
                    Authenticator.HuidigAccount.LocatieID = DataCom.LocatieIDOpStadOphalen(Stad);
                    Authenticator.HuidigeLocatie = DataCom.LocatieOpIDOphalen((int)Authenticator.HuidigAccount.LocatieID);
                }

                resultaat = DataCom.HuidigAccountAanpassen();
            }
            if (WachtwoordVelden())
            {
                resultaat = WachtwoordWijzigen();
            }
            UpdateUserInstellingenViewModel();
            MessageBox.Show(resultaat);
        }

        //Account verwijderen
        public void VerwijderAccount()
        {
            var resultaat = MessageBox.Show("Ben je zeker dat je het account VOORGOED wil verwijderen?!", 
                "ACCOUNT VERWIJDEREN !", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Warning);

            if (resultaat == MessageBoxResult.Yes)
            {
                resultaat = MessageBox.Show("Je account echt verwijderen? Dit kan niet ongedaan gemaakt worden!",
                "HEEL ZEKER ?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
                if (resultaat == MessageBoxResult.Yes)
                {
                    DataCom.HuidigAccountVerwijderen();
                    DataCom.LogUit(false);
                }
            }
        }

        //Afmelden
        public void Afmelden()
        {
            var resultaat = MessageBox.Show("Zeker dat je wil afmelden?",
                "UITLOGGEN ?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (resultaat == MessageBoxResult.Yes)
            {
                DataCom.LogUit(false);
            }
        }


        //HULPMETHODES\\

        //Imagebuilder
        private ImageBrush ImageBuilder(string imageLink)
        {
            //ImageBrush aanmaken
            ImageBrush imageBrush = new ImageBrush();
            if (!Tools.VeldVol(imageLink))
            {
                //Als gebruiker profielfoto link leeg is standaard toepassen
                imageLink = FotoLinksClass.FotoLinksMethode().First();
                FotoLink = imageLink;
            }
            string internalImageLink = $"pack://application:,,,/Styles/TestImages/{imageLink}";
            imageBrush.ImageSource = new BitmapImage(new Uri(internalImageLink)); ;
            return imageBrush;
        }

        //Gegevens wijzigen
        private bool GegevensWijzigen()
        {
            if (AccountChecks[0] != Geaardheid || AccountChecks[1] != FotoLink || AccountChecks[2] != Stad)
            {
                return true;
            }
            return false;
        }

        //Wachtwoordvelden controleren
        private bool WachtwoordVelden()
        {
            if (Tools.VeldVol(HuidigWachtwoord) || Tools.VeldVol(NiewWachtwoord) || Tools.VeldVol(NiewWachtwoordBevestiging))
            {
                return true;
            }
            return false;
        }

        //Wachtwoord wijzigen
        private string WachtwoordWijzigen()
        {
            if (Tools.VeldVol(HuidigWachtwoord) && Tools.VeldVol(NiewWachtwoord) && Tools.VeldVol(NiewWachtwoordBevestiging))
            {
                return DataCom.WachtwoordWijzigen(NiewWachtwoord, HuidigWachtwoord, NiewWachtwoordBevestiging);
            }
            return "Alle velden voor wachtwoord moeten ingevuld zijn!";
        }
    }
}
