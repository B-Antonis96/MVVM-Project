using Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts;
using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Data;
using Match4Ever_DAL.Models;
using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Match4Ever_DAL.DALServices.AuthenticationServices
{
    public sealed class RegistrationService
    {
        //BENODIGDHEDEN\\
        private readonly WachtwoordService Hasher = new WachtwoordService();
        private readonly DataService DataService = new DataService();
        private readonly DataTools Tools = new DataTools();
        public List<string> ResultaatString { get; private set; } = new List<string>();


        //REGISTRATIE FUNCTIES\\

        //Gebruiker registreren
        public Account Registreer(string email, string gebruikersnaam, string wachtwoord, string bevestigwachtwoord, 
            string geaardheid, string geslacht, DateTime geboortedatum, string stad, bool admin)
        {
            //Acount check uitvoeren
            if (!AccountChecks(gebruikersnaam, email, wachtwoord, bevestigwachtwoord, geboortedatum).Contains(false))
            {
                //Gebruiker aanmaken
                Account gebruiker = new Account()
                {
                    Emailadres = email,
                    Gebruikersnaam = gebruikersnaam,
                    Wachtwoord = Hasher.HashWachtwoord(wachtwoord),
                    Geaardheid = geaardheid,
                    Geslacht = geslacht,
                    Geboortedatum = geboortedatum,
                    LocatieID = DataService.LocatieIDOphalen(stad),
                    IsAdmin = admin
                };

                //Anders fout op admin toe te voegen!
                if (gebruiker.LocatieID == 0)
                {
                    gebruiker.LocatieID = null;
                }

                if ((bool)!gebruiker.IsAdmin)
                {
                    gebruiker.AccountVoorkeuren = new List<AccountVoorkeur>();
                }

                //Gebruiker toevoegen aan database
                DataService.ToevoegenAccount(gebruiker);
                return gebruiker;
            }

            return null; //Indien registratie niet gelukt is NULL teruggeven
        }


        //REGISTRATIE HELPER FUNCTIES\\

        //Leeftijd checken
        private int LeeftijdChecker(DateTime geboortedatum)
        {
            var vandaag = DateTime.Today;
            var leeftijd = vandaag.Year - geboortedatum.Year;
            if (geboortedatum > vandaag.AddYears(-leeftijd))
            {
                leeftijd--;
            }
            return leeftijd;
        }

        //Account checker
        private List<bool> AccountChecks(string gebruikersnaam, string email, string wachtwoord, string bevestigwachtwoord, DateTime geboortedatum)
        {
            //Leegmaken resultaat string
            ResultaatString.Clear();

            //List van controle bools
            List<bool> bools = new List<bool>();

            //Array van meldingen
            string[] zinnen = { "Gebruiker bestaat al!\n",
                "Email is al gekoppeld aan gebruiker!\n", "Een wachtwoord moet minstens 8 tekens bevatten!\n",
                "Wachtwoorden komen niet overeen!\n", "Je moet 18 jaar of ouder zijn om deze app te gebruiken!\n",
                "Registreren gelukt!"};

            //Controleren of gebruikersnaam of email gelinkt is aan een AccountID
            int[] id = { DataService.AccountIDOphalenOpNaam(gebruikersnaam), DataService.AccountIDOphalenOpEmail(email) };

            //Als account op gebruikersnaam opgehaald kon worden....
            bools.Add(IngaveChecker(Tools.SizeChecker(id[0], 0), zinnen[0]));

            //Controleren of email al bestaat
            bools.Add(IngaveChecker(Tools.SizeChecker(id[1], 0), zinnen[1]));

            ////Controleren of wachtwoord meer dan 8 tekens bevat
            bools.Add(IngaveChecker(Tools.SizeChecker(8, wachtwoord.Length), zinnen[2]));

            //Wachtwoorden controleren
            bools.Add(IngaveChecker(!Tools.ParameterCheck(wachtwoord, bevestigwachtwoord), zinnen[3]));

            //Geboortedatum checken op huidige leeftijd! Ouder dan 18 jaar
            bools.Add(IngaveChecker(Tools.SizeChecker(18, LeeftijdChecker(geboortedatum)), zinnen[4]));

            //Nergens problemen, dan 'Gelukt' teruggeven aan ResultaatString
            IngaveChecker(!bools.Contains(false), zinnen[5]);

            return bools;
        }

        //Bool controleren
        private bool IngaveChecker(bool ingave, string zin)
        {
            bool test = true;
            if (ingave)
            {
                test = false;
                ResultaatString.Add(zin);
            }
            return test;
        }
    }
}
