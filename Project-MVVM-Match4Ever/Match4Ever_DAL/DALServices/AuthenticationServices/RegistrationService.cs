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
using static Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts.DataEnums;
using System.Text.RegularExpressions;

namespace Match4Ever_DAL.DALServices.AuthenticationServices
{
    public class RegistrationService
    {
        //BENODIGDHEDEN\\
        private readonly WachtwoordService Hasher = new WachtwoordService();
        private readonly DataService DataService = new DataService();
        private DataTools Tools = new DataTools();
        public string ResultaatString { get; private set; }

        //Regex voor email
        private string RegexPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        
        //REGISTRATIE FUNCTIES\\

        //Gebruiker registreren
        public Account RegistreerGebruiker(string email, string gebruikersnaam, string wachtwoord, string bevestigwachtwoord, 
            string geaardheid, string geslacht, DateTime geboortedatum, string stad)
        {
            //Acount check uitvoeren
            if (AccountChecks(gebruikersnaam, email, wachtwoord, bevestigwachtwoord, geboortedatum) == AuthentcatieResultaat.Gelukt)
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
                    IsAdmin = false
                };

                //Gebruiker toevoegen aan database
                if (DataService.ToevoegenAccount(gebruiker))
                {
                    return gebruiker;
                }
                else
                {
                    ResultaatString = "Er is iets fout gegaan!";
                }
            }

            return null; //Indien registratie niet gelukt is NULL teruggeven
        }


        //REGISTRATIE HELPER FUNCTIES\\

        //Leeftijd checken op +18 jaar
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
        private protected AuthentcatieResultaat AccountChecks(string gebruikersnaam, string email, string wachtwoord, string bevestigwachtwoord, DateTime geboortedatum)
        {
            string[] zinnen = { "Gebruiker bestaat al!", "Geen correct emailadres ingegeven!",
                "Email is al gekoppeld aan gebruiker!", "Wachtwoord te kort!\nEen wachtwoord moet minstens 8 tekens bevatten!",
                "Wachtwoorden komen niet overeen!", "Je bent te jong!\nJe moet 18 jaar of ouder zijn om deze app te gebruiken!",
                "Registreren gelukt!"};

            //Controleren of gebruikersnaam of email gelinkt is aan een AccountID
            int[] id = { DataService.AccountIDOphalenOpNaam(gebruikersnaam), DataService.AccountIDOphalenOpEmail(email) };

            //Als account op gebruikersnaam opgehaald kon worden....
            if (Tools.SizeChecker(id[0], 0)) 
            { 
                ResultaatString = zinnen[0]; 
                return AuthentcatieResultaat.NietGelukt;
            }

            //Controleren of email juist gevalideerd is
            if (!Regex.IsMatch(email, RegexPattern)) 
            {
                ResultaatString = zinnen[1]; 
                return AuthentcatieResultaat.NietGelukt;
            }

            //Controleren of email al bestaat
            if (Tools.SizeChecker(id[1], 0)) 
            { 
                ResultaatString = zinnen[2]; 
                return AuthentcatieResultaat.NietGelukt; 
            }

            //Controleren of wachtwoord meer dan 8 tekens bevat
            if (Tools.SizeChecker(8, wachtwoord.Length)) 
            { 
                ResultaatString = zinnen[3]; 
                return AuthentcatieResultaat.NietGelukt; 
            }

            //Wachtwoorden controleren
            if (!Tools.ParameterCheck(wachtwoord, bevestigwachtwoord)) 
            {
                ResultaatString = zinnen[4]; 
                return AuthentcatieResultaat.NietGelukt;
            }

            //Geboortedatum checken op huidige leeftijd!
            if (Tools.SizeChecker(18, LeeftijdChecker(geboortedatum))) 
            {
                ResultaatString = zinnen[5];
                return AuthentcatieResultaat.NietGelukt; 
            }

            //Nergens problemen, dan 'Gelukt' teruggeven
            ResultaatString = zinnen[6];
            return AuthentcatieResultaat.Gelukt;
        }
    }
}
