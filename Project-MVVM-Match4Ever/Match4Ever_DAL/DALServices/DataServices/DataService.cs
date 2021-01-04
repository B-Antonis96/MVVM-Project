using Match4Ever_DAL.Data;
using Match4Ever_DAL.Data.Repositories;
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
    public sealed class DataService
    {
        //BENODIGDHEDEN\\
        private IUnitOfWork WorkUnit = new UnitOfWork(new Match4EverEntities());
        private Account Account { get; set; }
        private Locatie Locatie { get; set; }


        //DATA SERVICES\\

        //ADMIN

        //Alle accounts ophalen => voor admin doeleinden!
        public List<Account> AccountsOphalen() => new List<Account>(WorkUnit.AccountRepo.AllesOphalen());


        //ACCOUNT DATA

        //Account op id ophalen
        public Account AccountOphalenOpID(int id) => WorkUnit.AccountRepo.Ophalen(x => x.AccountID == id).SingleOrDefault();

        //Wachtwoord ophalen op AccountID
        public string AccountWachtwoordOphalenOpID(int id) => WorkUnit.AccountRepo.Ophalen(x => x.AccountID == id).Select(x => x.Wachtwoord).SingleOrDefault();

        //AccountID op gebruikersnaam ophalen
        public int AccountIDOphalenOpNaam(string gebruikersnaam)
        {
            Account = WorkUnit.AccountRepo.Ophalen(x => x.Gebruikersnaam == gebruikersnaam).SingleOrDefault();
            if (Account != null)
            {
                return Account.AccountID;
            }
            return 0;
        }

        //AccountID op email ophalen
        public int AccountIDOphalenOpEmail(string email)
        {
            Account = WorkUnit.AccountRepo.Ophalen(x => x.Emailadres == email).SingleOrDefault();
            if (Account != null)
            {
                return Account.AccountID;
            }
            return 0;
        }

        //Account toevoegen aan database
        public void ToevoegenAccount(Account account)
        {
            WorkUnit.AccountRepo.EntityToevoegen(account);
            WorkUnit.Save();
        }

        //Account aanpassen in database
        public void AanpassenAccount(Account account)
        {
            WorkUnit.AccountRepo.EntityAanpassen(account);
            WorkUnit.Save();
        }

        //Account verwijderen uit database + alle gelinkte onderdelen
        public void VerwijderenAccount(Account account)
        {
            WorkUnit.AccountRepo.Verwijderen(account.AccountID);
            WorkUnit.Save();
        }


        //LOCATIE DATA

        //LocatieID op stad ophalen => land kan bij meerdere steden horen!
        public int LocatieIDOphalen(string stad)
        {
            Locatie = WorkUnit.LocatieRepo.Ophalen(x => x.Stad == stad).SingleOrDefault();
            if (Locatie != null)
            {
                return Locatie.LocatieID;
            }
            return 0;
        }

        //Lijst van locaties ophalen
        public List<Locatie> LocatiesOphalen() => new List<Locatie>(WorkUnit.LocatieRepo.AllesOphalen());

        //Locatie op ID ophalen
        public Locatie LocatieOphalenOpID(int id) => WorkUnit.LocatieRepo.Ophalen(x => x.LocatieID == id).SingleOrDefault();


        //VOORKEUR DATA

        //Functie alle voorkeuren ophalen
        public List<Voorkeur> VoorkeurenOphalen() => new List<Voorkeur>(WorkUnit.VoorkeurRepo.AllesOphalen());

        //Voorkeur toevoegen aan database
        public void ToevoegenVoorkeur(Voorkeur voorkeur)
        {
            WorkUnit.VoorkeurRepo.EntityToevoegen(voorkeur);
            WorkUnit.Save();
        }

        //Voorkeur aanpassen en opslaan in database
        public void AanpassenVoorkeur(Voorkeur Voorkeur)
        {
            WorkUnit.VoorkeurRepo.EntityAanpassen(Voorkeur);
            WorkUnit.Save();
        }

        //Voorkeur + gelinkte antwoorden en gekozen accountvoorkeuren verwijderen uit database
        public void VerwijderenVoorkeur(Voorkeur voorkeur)
        {
            WorkUnit.VoorkeurRepo.Verwijderen(voorkeur.VoorkeurID);
            WorkUnit.Save();
        }


        //VOORKEURANTWOORD DATA

        //Alle antwoorden op VoorkeurID ophalen
        public List<VoorkeurAntwoord> VoorkeurAntwoordenOphalenOpVoorkeurID(int id) => new List<VoorkeurAntwoord>(WorkUnit.VoorkeurAntwoordRepo.Ophalen(x => x.VoorkeurID == id));

        //Voorkeur toevoegen aan database
        public void ToevoegenAntwoord(VoorkeurAntwoord antwoord)
        {
            WorkUnit.VoorkeurAntwoordRepo.EntityToevoegen(antwoord);
            WorkUnit.Save();
        }

        //Voorkeur aanpassen en opslaan in database
        public void AanpassenAntwoord(VoorkeurAntwoord antwoord)
        {
            WorkUnit.VoorkeurAntwoordRepo.EntityAanpassen(antwoord);
            WorkUnit.Save();
        }

        //Voorkeur + gelinkte antwoorden en gekozen accountvoorkeuren verwijderen uit database
        public void VerwijderenAntwoord(VoorkeurAntwoord antwoord)
        {
            WorkUnit.VoorkeurAntwoordRepo.Verwijderen(antwoord.VoorkeurAntwoordID);
            WorkUnit.Save();
        }


        //ACCOUNTVOORKEUREN DATA

        //Alle AccountVoorkeuren opvragen met AccountID
        public List<AccountVoorkeur> AccountVoorkeurenOphalenOpAccountID(int id) => new List<AccountVoorkeur>(WorkUnit.AccountVoorkeurRepo.Ophalen(x => x.AccountID == id));

        //AccountVoorkeur toevoegen aan database
        public void ToevoegenAccountVoorkeur(AccountVoorkeur accountVoorkeur)
        {
            WorkUnit.AccountVoorkeurRepo.EntityToevoegen(accountVoorkeur);
            WorkUnit.Save();
        }

        //AccountVoorkeur aanpassen en opslaan in database
        public void AanpassenAccountVoorkeur(AccountVoorkeur accountVoorkeur)
        {
            WorkUnit.AccountVoorkeurRepo.EntityAanpassen(accountVoorkeur);
            WorkUnit.Save();
        }
    }
}
