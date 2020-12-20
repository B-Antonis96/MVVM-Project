using Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts;
using Match4Ever_DAL.Data;
using Match4Ever_DAL.Data.UnitOfWork;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts.AuthenticationEnums;

namespace Match4Ever_DAL.DALServices.AuthenticationServices
{
    public class RegistrationService
    {
        //BENODIGDHEDEN\\
        private readonly IUnitOfWork WorkUnit = new UnitOfWork(new Match4EverEntities());
        private readonly WachtwoordHasher Hasher = new WachtwoordHasher();
        private ObservableCollection<Account> Accounts { get; set; }

        public RegistrationService()
        {

        }

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
        //        ToevoegenAccount(admin);
        //    }
        //}

        //Gebruiker registreren
        public void RegistreerGebruiker(string email, string gebruikersnaam, string wachtwoord, string bevestigwachtwoord, 
            string naam, string geslacht, string geaardheid, DateTime geboortedatum, Locatie locatie)
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
                    Geaardheid = geaardheid,
                    Geboortedatum = geboortedatum,
                    Locatie = locatie,
                    IsAdmin = false
                };

                //Gebruiker toevoegen aan database
                ToevoegenAccount(gebruiker);
            }
        }


        //GEDEELDE FUNCTIES\

        //Parameter checker
        private bool ParameterCheck(string parameter, string parameterCheck) => parameter == parameterCheck;

        //Toevoegen account in database
        private void ToevoegenAccount(Account account) => WorkUnit.AccountRepo.EntityToevoegen(account);


        //REGISTRATIE FUNCTIE\\

        //Check naar bestaande accounts
        private protected AuthentcatieResultaat CheckAccounts(string gebruikersnaam, string email, string wachtwoord, string bevestigwachtwoord)
        {
            //Accounts ophalen uit database
            Accounts = new ObservableCollection<Account>(WorkUnit.AccountRepo.AllesOphalen());

            //Acounts controleren via iteratie
            foreach (Account account in Accounts)
            {
                //Controleren of gebruikersnaam al bestaat
                if (ParameterCheck(gebruikersnaam, account.Gebruikersnaam))
                {
                    return AuthentcatieResultaat.GebruikersnaamBestaatAl;
                }

                //Controleren of email al bestaat
                if (ParameterCheck(email, account.Emailadres))
                {
                    return AuthentcatieResultaat.EmailBestaatAl;
                }
            }

            //Wachtwoord controleren
            if (!ParameterCheck(wachtwoord, bevestigwachtwoord))
            {
                return AuthentcatieResultaat.WachtwoordenNietHetZelfde;
            }

            return AuthentcatieResultaat.Gelukt;
        }
    }
}
