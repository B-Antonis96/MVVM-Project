using Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts;
using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts.DataEnums;

namespace Match4Ever_DAL.DALServices.AuthenticationServices
{
    public class LoginService
    {
        //BENODIGDHEDEN
        private readonly WachtwoordService Hasher = new WachtwoordService();
        private readonly DataService DataService = new DataService();
        private readonly DataTools Tools = new DataTools();
        public string ResultaatString { get; private set; }
        private Account Account { get; set; }


        //ACCOUNT FUNCTIES\\

        //Account login
        public Account Login(string gebruikersnaam, string wachtwoord)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Gebruiker bestaat niet!", "Wachtwoord niet correct!", "Gelukt"};
            ResultaatString = zinnen[0];

            //AccountID ophalen op gebruikersnaam
            int id = DataService.AccountIDOphalenOpNaam(gebruikersnaam);

            if (Tools.SizeChecker(id, 0))
            {
                //Wachtwoord ophalen op AccountID
                string accountWachtwoord = DataService.AccountWachtwoordOphalenOpID(id);
                ResultaatString = zinnen[1];

                //Contoleren of wachtwoorden overeenkomen
                if (Hasher.HashCheck(wachtwoord, accountWachtwoord))
                {
                    //Account ophalen op AccountID
                    Account = DataService.AccountOphalenOpID(id);
                    ResultaatString = zinnen[2];
                    return Account;
                }
            }

            return null; //Indien account niet gevonden wordt of wachtwoord niet klopt NULL teruggeven
        }

        //Account updaten of verwijderen
        public Account AccountUpdatenOfVerwijderen(Account account, bool switcher)
        {
            ResultaatString = "Wijzigingen konden niet worden aangebracht!";
            if (account != null)
            {
                if (switcher)
                {
                    if (DataService.AanpassenAccount(account))
                    {
                        ResultaatString = "Account aangepast!";
                        return account;
                    }
                }
                else
                {
                    if (DataService.VerwijderenAccount(account))
                    {
                        ResultaatString = "Gebruiker verwijderd!";
                        return null;
                    }
                }
            }
            return null;
        }

    }
}
