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
        private Account Account { get; set; }
        private Locatie Locatie { get; set; }
        private Voorkeur Voorkeur { get; set; }
        private VoorkeurAntwoord VoorkeurAntwoord {get; set;}
        private AccountVoorkeur AccountVoorkeur { get; set; }


        //DATA SERVICES\\

        //ADMIN

        //Alle accounts ophalen => voor admin doeleinden!
        public ObservableCollection<Account> AccountsOphalen() => new ObservableCollection<Account>(WorkUnit.AccountRepo.AllesOphalen());


        //ACCOUNT DATA

        //Account op id ophalen
        public Account AccountOphalenOpID(int id) => WorkUnit.AccountRepo.Ophalen(x => x.AccountID == id).SingleOrDefault();

        //Wachtwoord ophalen op AccountID
        public string AccountWachtwoordOphalenOpID(int id) => WorkUnit.AccountRepo.Ophalen(x => x.AccountID == id).Select(x => x.Wachtwoord).SingleOrDefault();

        //AccountID op gebruikersnaam ophalen => registratie
        public int AccountIDOphalenOpNaam(string gebruikersnaam)
        {
            Account = WorkUnit.AccountRepo.Ophalen(x => x.Gebruikersnaam == gebruikersnaam).SingleOrDefault();
            if (Account != null)
            {
                return Account.AccountID;
            }
            return 0;
        }

        //AccountID op email ophalen => registratie
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
        public bool ToevoegenAccount(Account account)
        {
            WorkUnit.AccountRepo.EntityToevoegen(account);
            if (WorkUnit.Save() == 1)
            {
                return true;
            }

            return false;
        }

        //Account aanpassen in database
        public bool AanpassenAccount(Account account)
        {
            WorkUnit.AccountRepo.EntityAanpassen(account);
            if (WorkUnit.Save() == 1)
            {
                return true;
            }

            return false;
        }

        //Account verwijderen uit database + alle gelinkte onderdelen
        public bool VerwijderenAccount(Account account)
        {
            //Controleren of er AccountVoorkeuren zijn om te verwijderen
            ObservableCollection<AccountVoorkeur> accountVoorkeuren = AccountVoorkeurenOphalenOpAccountID(account.AccountID);
            if (accountVoorkeuren != null)
            {
                WorkUnit.AccountVoorkeurRepo.Verwijderen(accountVoorkeuren);
            }

            //Controleren of er Matchen zijn om te verwijderen
            //ObservableCollection<AccountVoorkeur> Matchen = (account.AccountID);
            //if (accountVoorkeuren != null)
            //{
            //    WorkUnit.AccountVoorkeurRepo.Verwijderen(accountVoorkeuren);
            //}

            WorkUnit.AccountRepo.Verwijderen(account.AccountID);
            if (WorkUnit.Save() == 1)
            {
                return true;
            }

            return false;
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

        //Collectie van locaties ophalen
        public ObservableCollection<string> LocatiesOphalen() => new ObservableCollection<string>(WorkUnit.LocatieRepo.AllesOphalen().Select(x => x.Stad));

        //Locatie op ID ophalen
        public Locatie LocatieOphalenOpID(int id) => WorkUnit.LocatieRepo.Ophalen(x => x.LocatieID == id).SingleOrDefault();


        //VOORKEUR DATA

        //Functie alle voorkeuren met vragen ophalen
        public ObservableCollection<string> VoorkeurenOphalen() => new ObservableCollection<string>(WorkUnit.VoorkeurRepo.AllesOphalen().Select(x => x.Vraag));

        //VoorkeurID ophalen op vraag
        public int VoorkeurIDOphalen(string vraag)
        {
            Voorkeur = WorkUnit.VoorkeurRepo.Ophalen(x => x.Vraag == vraag).SingleOrDefault();
            if (Voorkeur != null)
            {
                return Voorkeur.VoorkeurID;
            }
            return 0;
        }

        //Alle antwoorden op VoorkeurID ophalen
        public ObservableCollection<string> VoorkeurAntwoordenOphalenOpVoorkeurID(int id) => new ObservableCollection<string>(WorkUnit.VoorkeurAntwoordRepo.Ophalen(x => x.VoorkeurID == id).Select(x => x.Antwoord));

        //VoorkeurAntwoordID ophalen op antwoord
        public int VoorkeurAntwoordIDOphalen(string antwoord)
        {
            VoorkeurAntwoord = WorkUnit.VoorkeurAntwoordRepo.Ophalen(x => x.Antwoord == antwoord).SingleOrDefault();
            if (VoorkeurAntwoord != null)
            {
                return VoorkeurAntwoord.VoorkeurAntwoordID;
            }
            return 0;
        }


        //ACCOUNTVOORKEUREN DATA

        //Alle AccountVoorkeuren opvragen met AccountID
        public ObservableCollection<AccountVoorkeur> AccountVoorkeurenOphalenOpAccountID(int id) => new ObservableCollection<AccountVoorkeur>(WorkUnit.AccountVoorkeurRepo.Ophalen(x => x.AccountID == id));

        //AccountVoorkeur opvragen met AccountVoorkeurID
        public AccountVoorkeur AccountVoorkeurOphalenOpAccountVoorkeurID(int id) => WorkUnit.AccountVoorkeurRepo.Ophalen(x => x.AccountVoorkeurID == id).SingleOrDefault();

        //AccountVoorkeur toevoegen aan database
        public bool ToevoegenAccountVoorkeur(AccountVoorkeur accountVoorkeur)
        {
            WorkUnit.AccountVoorkeurRepo.EntityToevoegen(accountVoorkeur);
            if (WorkUnit.Save() == 1)
            {
                return true;
            }

            return false;
        }


        //MATCHEN EN MELDINGEN DATA

        //Alle AccountVoorkeuren opvragen met AccountID
        public ObservableCollection<Match> MatchenOphalenOpAccountID(int id) => new ObservableCollection<Match>(WorkUnit.MatchRepo.Ophalen(x => x.Account1ID == id));
    }
}
