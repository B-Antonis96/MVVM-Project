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
    public class LoginService
    {
        //BENODIGDHEDEN
        private readonly IUnitOfWork WorkUnit = new UnitOfWork(new Match4EverEntities());
        private readonly WachtwoordHasher Hasher = new WachtwoordHasher();
        private ObservableCollection<Account> Accounts { get; set; }
        public AuthentcatieResultaat Resultaat { get; set; }

        //Account login
        public Account Login(string gebruikersnaam, string wachtwoord)
        {
            //Acounts uit database ophalen
            Accounts = new ObservableCollection<Account>(WorkUnit.AccountRepo.AllesOphalen());

            Resultaat = AuthentcatieResultaat.GebruikerBestaatNiet;

            //Acounts controleren via iteratie
            foreach (Account account in Accounts)
            {
                //Gebruikersnaam controleren
                if (gebruikersnaam == account.Gebruikersnaam)
                {
                    Resultaat = AuthentcatieResultaat.WachtwoordenNietHetZelfde;

                    //Wachtwoord controleren
                    if (Hasher.HashCheck(wachtwoord, account))
                    {
                        Resultaat = AuthentcatieResultaat.Gelukt;
                        return account; //Account teruggeven indien correct
                    }
                }
            }

            return null; //Indien account niet gevonden wordt NULL teruggeven
        }
    }
}
