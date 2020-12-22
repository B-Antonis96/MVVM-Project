using Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts;
using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Data;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts.DataEnums;

namespace Match4Ever_DAL.DALServices.AuthenticationServices
{
    public class RegistrationService
    {
        //BENODIGDHEDEN\\
        private readonly WachtwoordHasher Hasher = new WachtwoordHasher();
        private readonly DataService DataService = new DataService();
        private DataTools Tools = new DataTools();
        public AuthentcatieResultaat ResultaatEnum { get; private set; }

        //REGISTRATIE FUNCTIES\\

        //Admin registreren
        //public void RegistreerAdmin(string email, string gebruikersnaam, string wachtwoord, string bevestigwachtwoord, Locatie locatie)
        //{
        //    //Account check uitvoeren
        //    if (CheckAccounts(gebruikersnaam, email, wachtwoord, bevestigwachtwoord) == AuthentcatieResultaat.Gelukt)
        //    {
        //        //Admin aanmaken
        //        Account admin = new Account()
        //        {
        //            Emailadres = email,
        //            Gebruikersnaam = gebruikersnaam,
        //            Wachtwoord = Hasher.HashWachtwoord(wachtwoord),
        //            Locatie = locatie,
        //            IsAdmin = true
        //        };

        //        //Admin toevoegen aan database
        //        DataService.ToevoegenAccount(admin);
        //    }
        //}

        //Gebruiker registreren
        public void RegistreerGebruiker(string email, string gebruikersnaam, string wachtwoord, string bevestigwachtwoord, 
            string naam, string geslacht/*, DateTime geboortedatum*/, string land, string stad)
        {
            //Acount check uitvoeren
            if (CheckAccounts(gebruikersnaam, email, wachtwoord, bevestigwachtwoord) == AuthentcatieResultaat.Gelukt)
            {
                //Gebruiker aanmaken
                Account gebruiker = new Account()
                {
                    Emailadres = email,
                    Gebruikersnaam = gebruikersnaam,
                    Wachtwoord = Hasher.HashWachtwoord(wachtwoord),
                    Naam = naam,
                    Geslacht = geslacht,
                    //Geboortedatum = geboortedatum,
                    LocatieID = DataService.LocatieIDOphalen(land, stad),
                    IsAdmin = false
                };

                //Gebruiker toevoegen aan database
                DataService.ToevoegenAccount(gebruiker);
            }
        }

        //Resultaat registreren
        public string Resultaat()
        {
            //Controleren of gebruikersnaam of email al bestaat
            if (ResultaatEnum == AuthentcatieResultaat.EmailBestaatAl ||
                ResultaatEnum == AuthentcatieResultaat.GebruikersnaamBestaatAl)
            {
                return "Gebruiker bestaat al!";
            }

            if (ResultaatEnum == AuthentcatieResultaat.WachtwoordenNietHetZelfde)
            {
                return "Wachtwoorden komen niet overeen!";
            }

            return "Registreren gelukt!";
        }


        //REGISTRATIE HELPER FUNCTIES\\

        //Check naar bestaande accounts en wachtwoord overeenkomst
        private protected AuthentcatieResultaat CheckAccounts(string gebruikersnaam, string email, string wachtwoord, string bevestigwachtwoord)
        {
            //Controleren of gebruikersnaam gelinkt is aan een AccountID
            int id = DataService.AccountIDOphalenOpNaam(gebruikersnaam);

            //Als account op gebruikersnaam opgehaald kon worden....
            if (id > 0)
            {
                ResultaatEnum = AuthentcatieResultaat.GebruikersnaamBestaatAl;
            }
            else
            {
                //Controleren of email gelinkt is aan een AccountID
                id = DataService.AccountIDOphalenOpEmail(email);

                //Controleren of email al bestaat
                if (id > 0)
                {
                    ResultaatEnum = AuthentcatieResultaat.EmailBestaatAl;
                }
                else
                {
                    //Wachtwoord controleren
                    if (Tools.ParameterCheck(wachtwoord, bevestigwachtwoord))
                    {
                        ResultaatEnum = AuthentcatieResultaat.Gelukt;
                    }
                    else
                    {
                        ResultaatEnum = AuthentcatieResultaat.WachtwoordenNietHetZelfde;
                    }
                }
            }
            return ResultaatEnum;
        }
    }
}
