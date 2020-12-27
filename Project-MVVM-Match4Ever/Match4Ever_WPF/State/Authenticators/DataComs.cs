using Match4Ever_DAL.DALServices.AuthenticationServices;
using Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts;
using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Models;
using Match4Ever_WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.State.Authenticators
{
    public class DataComs
    {
        //BENODIGDHEDEN\\
        private readonly DataService DataService = new DataService();
        private readonly LoginService InlogService = new LoginService();
        private readonly RegistrationService RegistratieService = new RegistrationService();
        private readonly WachtwoordService WachtwoordService = new WachtwoordService();

        //SCHERM CONTROLE\\ => verwijderen account = teruggaan naar login
        public ICommand SwitchViewModel => Navigator.StaticNavigator.SwitchViewModel;


        //AUTHENTICATOR METHODES

        //Registreren
        public string Registreren(string email, string gebruikersnaam, string wachtwoord,
            string bevestigwachtwoord, string geaardheid, string geslacht, DateTime geboortedatum, string stad)
        {
            //Gebruiker registreren, anders fouten terug geven
            AccountSetter(RegistratieService.RegistreerGebruiker(email, gebruikersnaam, wachtwoord,
            bevestigwachtwoord, geaardheid, geslacht, geboortedatum, stad));
            return RegistratieService.ResultaatString;
        }

        //Inloggen
        public string LogIn(string gebruikersnaam, string wachtwoord)
        {
            //Gebruiker ophalen, anders fouten terug geven
            AccountSetter(InlogService.Login(gebruikersnaam, wachtwoord));
            return InlogService.ResultaatString;
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
            }

            SwitchViewModel.Execute(ViewType.Login);
            Navigator.StaticNavigator.HuidigMenuViewModel = null;
        }

        //Aanpassen gebruiker
        public string HuidigAccountAanpassen()
        {
            InlogService.AccountUpdatenOfVerwijderen(Authenticator.HuidigAccount, true);
            return InlogService.ResultaatString;
        }

        //Verwijderen gebruiker
        public string HuidigAccountVerwijderen()
        {
            InlogService.AccountUpdatenOfVerwijderen(Authenticator.HuidigAccount, false);
            return InlogService.ResultaatString;
        }

        //Wijzigen wachtwoord
        public string WachtwoordWijzigen(string niewWw, string oudWw, string bevestigWw)
        {
            string resultaat = "Huidig wachtwoord is niet correct!";
            if (WachtwoordService.HashCheck(oudWw, Authenticator.HuidigAccount.Wachtwoord))
            {
                WachtwoordService.VeranderWachtwoord(Authenticator.HuidigAccount, niewWw, bevestigWw);
                resultaat = WachtwoordService.ResultaatString;
            }
            return resultaat;
        }

        //Hulp methodes
        private void AccountSetter(Account account)
        {
            if (account != null)
            {
                Authenticator.HuidigAccount = account;
                Authenticator.IsIngelogd = true;
                Authenticator.IsAdmin = (bool)account.IsAdmin;
                if (!Authenticator.IsAdmin)
                {
                    int locatieID = (int)Authenticator.HuidigAccount.LocatieID;
                    if (locatieID > 0)
                    {
                        Authenticator.HuidigeLocatie = LocatieOpIDOphalen(locatieID);
                    }
                    Authenticator.AccountVoorkeuren = AccountVoorkeurenOphalenOpAccountID(account.AccountID);
                }
            }
        }


        //LOCATIES

        //Locaties ophalen
        public List<string> LocatiesOphalen() => DataService.LocatiesOphalen().ToList();

        //Locatie op ID ophalen
        public Locatie LocatieOpIDOphalen(int id) => DataService.LocatieOphalenOpID(id);

        //LocatieID op stad ophalen
        public int LocatieIDOpStadOphalen(string stad) => DataService.LocatieIDOphalen(stad);


        //VOORKEUREN

        //Voorkeuren ophalen
        public List<string> VoorkeurVragenOphalen() => DataService.VoorkeurenOphalen().ToList();

        //VoorkeurID Ophalen
        public int VoorkeurIDOphalen(string vraag) => DataService.VoorkeurIDOphalen(vraag);

        //VoorkeurAntwoordID ophalen op vraag
        public int VoorkeurAntwoordIDOphalen(string antwoord) => DataService.VoorkeurAntwoordIDOphalen(antwoord);

        //VoorkeurAntwoorden ophalen
        public List<string> VoorkeurAntwoordenOpIDOphalen(int id) => DataService.VoorkeurAntwoordenOphalenOpVoorkeurID(id).ToList();

        //Voorkeur controle
        public int VoorkeurControle() => VoorkeurVragenOphalen().Count;

        //AccountVoorkeuren ophalen op accountID
        public List<AccountVoorkeur> AccountVoorkeurenOphalenOpAccountID(int id)
        {
            List<AccountVoorkeur> accountVoorkeuren = DataService.AccountVoorkeurenOphalenOpAccountID(id).ToList();
            if (accountVoorkeuren.Count < 0)
            {
                return accountVoorkeuren;
            }
            return null;
        }

        //AccountVoorkeuren toevoegen
        //public string AccountVoorkeurenToevoegenOfAanpassen(string vraag, string antwoord, int voorkeurID)
        //{



            
        //}
    }
}
