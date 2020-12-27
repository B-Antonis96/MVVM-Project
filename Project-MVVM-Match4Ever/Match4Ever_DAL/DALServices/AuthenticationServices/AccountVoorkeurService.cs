using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.AuthenticationServices
{
    public class AccountVoorkeurService
    {
        //BENODIGDHEDEN\\
        private readonly DataService DataService = new DataService();
        private DataTools Tools = new DataTools();
        public string ResultaatString { get; private set; }


        //ACCOUNTVOORKEUR FUNCTIES\\

        //AccountVoorkeur toevoegen
        public AccountVoorkeur AccountVoorkeurToevoegen(string vraag, string antwoord, int voorkeurID, int accountID)
        {
            int voorkeurAntwoordID = DataService.VoorkeurAntwoordIDOphalen(antwoord);

            if (voorkeurID > 0 && voorkeurAntwoordID > 0)
            {
                AccountVoorkeur AccountVoorkeur = new AccountVoorkeur()
                {
                    AccountID = accountID,
                    VoorkeurID = voorkeurID,
                    VoorkeurAntwoordID = voorkeurAntwoordID,
                };

                if (DataService.ToevoegenAccountVoorkeur(AccountVoorkeur))
                {
                    ResultaatString = "Voorkeur is toegevoegd en opgeslagen in de database";
                    return AccountVoorkeur;
                }
                else
                {
                    ResultaatString = "Er is iets fout gegaan!";
                }
            }
            ResultaatString = "Voorkeur kan niet opgeslagen worden!";
            return null;
        }

        //AccountVoorkeur aanpassen
        public AccountVoorkeur AccountVoorkeurAanpassen(string vraag, string antwoord, int accountVoorkeurID)
        {
            int voorkeurAntwoordID = DataService.VoorkeurAntwoordIDOphalen(antwoord);
            AccountVoorkeur accountVoorkeur = DataService.AccountVoorkeurOphalenOpAccountVoorkeurID(accountVoorkeurID);


            return null;
        }
    }
}
