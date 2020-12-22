using Match4Ever_DAL.Data;
using Match4Ever_DAL.Data.UnitOfWork;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.DataServices
{
    public class DataService
    {
        //BENODIGDHEDEN\\
        private IUnitOfWork WorkUnit = new UnitOfWork(new Match4EverEntities());


        //DATA SERVICES\\

        //ADMIN
        //Functie alle accounts ophalen => voor admin doeleinden!
        public ObservableCollection<Account> AccountsOphalen()
        {
            return new ObservableCollection<Account>(WorkUnit.AccountRepo.AllesOphalen());
        }

        //Functie locaties ophalen => voor admin doeleinden!
        public ObservableCollection<Locatie> LocatiesOphalen()
        {
            return new ObservableCollection<Locatie>(WorkUnit.LocatieRepo.AllesOphalen());
        }


        //ACCOUNT DATA

        //Functie account op id ophalen
        public Account AccountOphalenOpID(int id)
        {
            return (Account)WorkUnit.AccountRepo.Ophalen(x => x.AccountID == id);
        }

        //Functie accountID op gebruikersnaam ophalen => registratie
        public int AccountIDOphalenOpNaam(string gebruikersnaam)
        {
            Account account = (Account)WorkUnit.AccountRepo.Ophalen(x => x.Gebruikersnaam == gebruikersnaam);
            return account.AccountID;
        }

        //Functie accountID op email ophalen => registratie
        public int AccountIDOphalenOpEmail(string email)
        {
            Account account = (Account)WorkUnit.AccountRepo.Ophalen(x => x.Emailadres == email);
            return account.AccountID;
        }

        //Functie Account toevoegen aan database
        public void ToevoegenAccount(Account account)
        {
            WorkUnit.AccountRepo.EntityToevoegen(account);
            WorkUnit.Save();
        }


        //LOCATIE DATA

        //Functie locatieID op land en stad ophalen
        public int LocatieIDOphalen(string land, string stad)
        {
            Locatie locatie = (Locatie)WorkUnit.LocatieRepo.Ophalen(x => x.Land == land, y => y.Stad == stad);
            return locatie.LocatieID;
        }
    }
}
