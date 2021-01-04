using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.AuthenticationServices
{
    public sealed class AdminService
    {
        //BENODIGDHEDEN\\
        private readonly DataService DataService = new DataService();
        private readonly DataTools Tools = new DataTools();
        public string ResultaatString { get; private set; }


        //ADMIN FUNCTIES\\

        //Alle gebruikers op naam ophalen
        public List<string> GebruikersOphalen() => DataService.AccountsOphalen().Select(x => x.Gebruikersnaam).ToList();

        //Verwijder gebruiker op naam
        public void VerwijderGebruikerOpNaam(string gebruikersnaam)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Gebruiker kon niet gevonden worden in database\n", "Gebruiker is succesvol verwijderd!"  };
            ResultaatString = zinnen[0];

            //AccountID ophalen op naam
            int accountID = DataService.AccountIDOphalenOpNaam(gebruikersnaam);
            if (Tools.SizeChecker(accountID, 0))
            {
                //Account ophalen op accountID en verwijderen
                Account account = DataService.AccountOphalenOpID(accountID);
                DataService.VerwijderenAccount(account);
                ResultaatString = zinnen[1];
            }
        }
    }
}
