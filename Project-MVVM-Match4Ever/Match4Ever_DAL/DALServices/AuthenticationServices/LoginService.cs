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
        private readonly WachtwoordHasher Hasher = new WachtwoordHasher();
        private readonly DataService DataService = new DataService();
        private readonly DataTools Tools = new DataTools();
        public AuthentcatieResultaat Resultaat { get; private set; }

        //Account login
        public Account Login(string gebruikersnaam, string wachtwoord)
        {
            Resultaat = AuthentcatieResultaat.GebruikerBestaatNiet;

            int id = DataService.AccountIDOphalenOpNaam(gebruikersnaam);

            if (id > 0)
            {
                Account account = DataService.AccountOphalenOpID(id);
                Resultaat = AuthentcatieResultaat.WachtwoordenNietHetZelfde;

                if (Tools.ParameterCheck(Hasher.HashWachtwoord(wachtwoord), account.Wachtwoord))
                {
                    Resultaat = AuthentcatieResultaat.Gelukt;
                    return account;
                }
            }

            return null; //Indien account niet gevonden wordt NULL terug gegeven
        }
    }
}
