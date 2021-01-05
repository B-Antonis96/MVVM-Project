using Match4Ever_DAL.DALServices.AuthenticationServices;
using Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts;
using Match4Ever_DAL.Models;
using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.Styles.TestImages;
using Match4Ever_WPF.WPFTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Match4Ever_WPF.State.Authenticators
{
    public sealed class AuthenticatorInstellingen
    {
        //BENODIGDHEDEN\\
        private readonly DataComs DataCom = new DataComs();
        private readonly Tools Tools = new Tools();
        private readonly LoginService InlogService = new LoginService();
        private readonly RegistrationService RegistratieService = new RegistrationService();
        private readonly WachtwoordService WachtwoordService = new WachtwoordService();
        private readonly AccountVoorkeurService AccountVoorkeurService = new AccountVoorkeurService();
        private readonly FotoLinks FotoLinksClass = new FotoLinks();

        //Regex voor email
        private string RegexPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        //SCHERM CONTROLE\\
        public ICommand SwitchViewModel => Navigator.StaticNavigator.SwitchViewModel;


        //AUTHENTICATOR METHODES

        //Registreren
        public void Registreren(string email, string gebruikersnaam, string wachtwoord, 
            string BevestigWw, string geaardheid, string geslacht, DateTime geboortedatum, string stad)
        {
            List<string> resultaten = new List<string>();

            //Controleren op lege velden
            if (Tools.VeldVol(email) &&
                Tools.VeldVol(gebruikersnaam) &&
                Tools.VeldVol(wachtwoord) &&
                Tools.VeldVol(BevestigWw) &&
                Tools.VeldVol(geaardheid) &&
                Tools.VeldVol(geslacht) &&
                geboortedatum != null &&
                Tools.VeldVol(stad))
            {
                string tester = EmailChecker(email);
                if (tester == null)
                {
                    //Gebruiker registreren, anders fouten terug geven
                    AccountSetter(RegistratieService.Registreer(email, gebruikersnaam, wachtwoord,
                    BevestigWw, geaardheid, geslacht, geboortedatum, stad, Authenticator.IsAdmin));
                    resultaten.AddRange(RegistratieService.ResultaatString);
                }
                else
                {
                    resultaten.Add(tester);
                }
            }
            else
            {
                resultaten.Add("Alle velden moeten ingevuld zijn!");
            }

            //Meldingen tonen
            var resultaat = string.Join(Environment.NewLine, resultaten);
            MessageBox.Show(resultaat);

            //Indien registratie succesvol was doorgaan naar applicatie
            if (Authenticator.IsIngelogd)
            {
                //Benodigde ViewModels aanmaken
                ViewModelBuilder.ViewModelsAanmaken();
                SwitchViewModel.Execute(ViewType.MenuUser);
                SwitchViewModel.Execute(ViewType.Welkom);
            }
        }

        //Inloggen
        public void LogIn(string gebruikersnaam, string wachtwoord)
        {
            string resultaat = "Gebruikersnaam en wachtwoord moeten ingevuld zijn!";

            //Controleren op lege velden
            if (Tools.VeldVol(gebruikersnaam) &&
                Tools.VeldVol(wachtwoord))
            {
                //Gebruiker proberen inloggen, anders fouten teruggeven
                AccountSetter(InlogService.Login(gebruikersnaam, wachtwoord));
                resultaat = InlogService.ResultaatString;
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

        //Uitloggen
        public void LogUit(bool switcher)
        {
            Authenticator.IsIngelogd = false;
            Authenticator.HuidigAccount = null;
            Authenticator.HuidigeLocatie = null;
            Authenticator.AccountVoorkeuren = null;
            ViewModelBuilder.ViewModelsNullen();
            Authenticator.IsAdmin = false;

            //scherm afsluiten of enkel uitloggen?
            if (!switcher)
            {
                ViewModelBuilder.ViewModelsAanmaken();
                SwitchViewModel.Execute(ViewType.Login);
            }
            Navigator.StaticNavigator.HuidigMenuViewModel = null;
        }

        //Aanpassen gebruiker
        public string HuidigAccountAanpassen(string fotoLink, string geaardheid, string stad, string accountCheck)
        {
            Authenticator.HuidigAccount.ProfielfotoLink = fotoLink;
            Authenticator.HuidigAccount.Geaardheid = geaardheid;

            //Contoleren of stad al overeenkomt met accountcheck
            if (accountCheck != stad)
            {
                Locatie locatie = DataCom.LocatiesOphalen().Find(x => x.Stad == stad);
                Authenticator.HuidigAccount.LocatieID = locatie.LocatieID;
                Authenticator.HuidigeLocatie = locatie;
            }

            Authenticator.HuidigAccount = InlogService.AccountUpdatenOfVerwijderen(Authenticator.HuidigAccount, true);
            return InlogService.ResultaatString;
        }

        //Wachtwoord wijzigen
        public string WachtwoordWijzigen(string huidigWw, string nieuwWw, string nieuwBevestigWw)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Alle velden voor wachtwoord moeten ingevuld zijn!", "Huidig wachtwoord is niet correct!",
                "Nieuw wachtwoord en huidig wachtwoord zijn reeds hetzelfde!" };
            string resultaat = zinnen[0];

            //Controleren velden
            if (Tools.VeldVol(huidigWw) && Tools.VeldVol(nieuwWw) && Tools.VeldVol(nieuwBevestigWw))
            {
                resultaat = zinnen[1];

                //Hashchecker uitvoeren
                if (WachtwoordService.HashCheck(huidigWw, Authenticator.HuidigAccount.Wachtwoord))
                {
                    resultaat = zinnen[2];

                    //Controleren of wachtwoord uniek is
                    if (nieuwWw != huidigWw)
                    {
                        Account gebruiker = Authenticator.HuidigAccount;
                        WachtwoordService.VeranderWachtwoord(gebruiker, nieuwWw, nieuwBevestigWw);
                        resultaat = WachtwoordService.ResultaatString;
                    }
                }
            }
            return resultaat;
        }

        //Statische authenticator instellen
        private void AccountSetter(Account account)
        {
            if (account != null)
            {
                //Waardes koppelen
                Authenticator.HuidigAccount = account;

                //Account mag niet NULL zijn om volgende instellingen toe te passen!
                if (Authenticator.HuidigAccount != null)
                {
                    Authenticator.IsIngelogd = true;
                    Authenticator.IsAdmin = (bool)account.IsAdmin;

                    //Alleen als gebruiker geen admin is!
                    if (!Authenticator.IsAdmin)
                    {
                        int locatieID = (int)Authenticator.HuidigAccount.LocatieID;
                        if (locatieID > 0)
                        {
                            Authenticator.HuidigeLocatie = DataCom.LocatieOpIDOphalen(locatieID);
                        }

                        Authenticator.AccountVoorkeuren = DataCom.AccountVoorkeurenOphalen();
                    }
                }
            }
        }

        //Email checker
        public string EmailChecker(string email)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Veld mag niet leeg zijn!", "Geen geldig emailadres ingegeven!" };

            if (!Tools.VeldVol(email))
            {
                return zinnen[0];
            }
            if (!Regex.IsMatch(email, RegexPattern))
            {
                return zinnen[1];
            }
            return null;
        }


        //INSTELLINGEN METHODES

        //Account verwijderen warnings + uitvoeren
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
                    InlogService.AccountUpdatenOfVerwijderen(Authenticator.HuidigAccount, false);
                    LogUit(false);
                    MessageBox.Show(InlogService.ResultaatString);
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
                LogUit(false);
            }
        }


        //HULPMETHODES INSTELLINGEN\\

        //Gegevens wijzigen checks
        public bool GegevensWijzigen(string geaardheid, string fotoLink, string stad, string[] accountChecks)
        {
            if (accountChecks[0] != geaardheid || accountChecks[1] != fotoLink || accountChecks[2] != stad)
            {
                return true;
            }
            return false;
        }

        //Wachtwoordvelden controleren
        public bool WachtwoordVelden(string huidigWw, string nieuwWw, string nieuwBevestigWw)
        {
            if (Tools.VeldVol(huidigWw) || Tools.VeldVol(nieuwWw) || Tools.VeldVol(nieuwBevestigWw))
            {
                return true;
            }
            return false;
        }

        //Imagebuilder
        public ImageBrush ImageBuilder(string imageLink)
        {
            //ImageBrush aanmaken
            ImageBrush imageBrush = new ImageBrush();
            if (!Tools.VeldVol(imageLink))
            {
                //Als gebruiker profielfoto link leeg is standaard toepassen
                imageLink = FotoLinksClass.FotoLinksMethode().First();
            }
            string internalImageLink = $"pack://application:,,,/Styles/TestImages/{imageLink}";
            imageBrush.ImageSource = new BitmapImage(new Uri(internalImageLink)); ;
            return imageBrush;
        }


        //ACCOUNTVOORKEUREN METHODES

        //AccountVoorkeuren toevoegen of aanpassen
        public void AccountVoorkeurenToevoegen(int antwoordID, int voorkeurID)
        {
            //Standaard resultaat
            string resultaat = "Er moet een antwoord geslecteerd zijn!";

            if (antwoordID > 0)
            {
                Authenticator.AccountVoorkeuren = AccountVoorkeurService.AccountVoorkeurToevoegenOfAanpassen(antwoordID, voorkeurID, Authenticator.HuidigAccount.AccountID);
                Authenticator.HuidigAccount.AccountVoorkeuren = Authenticator.AccountVoorkeuren;
                resultaat = AccountVoorkeurService.ResultaatString;
            }

            MessageBox.Show(resultaat);
        }
    }
}
