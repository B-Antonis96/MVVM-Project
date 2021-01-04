using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.State.Authenticators
{
    public sealed class DataComs
    {
        //BENODIGDHEDEN\\
        private readonly DataService DataService = new DataService();


        //LOCATIES

        //Locaties ophalen
        public List<string> LocatiesNamenOphalen() => DataService.LocatiesOphalen().Select(x => x.Stad).ToList();

        public List<Locatie> LocatiesOphalen() => DataService.LocatiesOphalen();

        //Locatie op ID ophalen
        public Locatie LocatieOpIDOphalen(int id) => DataService.LocatieOphalenOpID(id);



        //VOORKEUREN

        //Voorkeuren ophalen
        public List<Voorkeur> VoorkeurVragenOphalen() => DataService.VoorkeurenOphalen();

        //VoorkeurAntwoorden ophalen
        public List<VoorkeurAntwoord> VoorkeurAntwoordenOpIDOphalen(int id) => DataService.VoorkeurAntwoordenOphalenOpVoorkeurID(id);

        //AccountVoorkeuren ophalen op accountID
        public List<AccountVoorkeur> AccountVoorkeurenOphalen()
        {
            List<AccountVoorkeur> accountVoorkeuren = DataService.AccountVoorkeurenOphalenOpAccountID(Authenticator.HuidigAccount.AccountID);
            if (accountVoorkeuren.Count < 0)
            {
                return accountVoorkeuren;
            }
            return null;
        }
    }
}
