using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.AuthenticationServices
{
    public sealed class AccountVoorkeurService
    {
        //BENODIGDHEDEN\\
        private readonly DataService DataService = new DataService();
        public string ResultaatString { get; private set; }


        //ACCOUNTVOORKEUR FUNCTIES\\

        //AccountVoorkeur toevoegen
        public List<AccountVoorkeur> AccountVoorkeurToevoegenOfAanpassen(int antwoordID, int voorkeurID, int accountID)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Voorkeur kan niet opgeslagen worden!", "Voorkeur is toegevoegd en opgeslagen in de database", "Er is iets fout gegaan in de database!", 
                "Voorkeur is aangepast en opgeslagen in de database", "Er moeten veranderingen aangebracht worden aan de voorkeur!"};
            ResultaatString = zinnen[0];

            //AccountVoorkeuren ophalen uit de database ter controle
            List<AccountVoorkeur> accountVoorkeuren = DataService.AccountVoorkeurenOphalenOpAccountID(accountID).ToList();

            //Backup lijst uit database
            List<AccountVoorkeur> accountVoorkeurenBackup = accountVoorkeuren;

            //AccountVoorkeur
            AccountVoorkeur accountVoorkeur;

            //Indien AccountVoorkeur nog niet bestaat nieuwe toevoegen
            if (!accountVoorkeuren.Select(x => x.VoorkeurID).Contains(voorkeurID))
            {
                //Nieuwe accountvoorkeur aanmaken
                accountVoorkeur = new AccountVoorkeur()
                {
                    AccountID = accountID,
                    VoorkeurID = voorkeurID,
                    VoorkeurAntwoordID = antwoordID
                };

                ResultaatString = zinnen[2];

                //Toevoegen
                DataService.ToevoegenAccountVoorkeur(accountVoorkeur);
                ResultaatString = zinnen[1];
                accountVoorkeuren.Add(accountVoorkeur);
                return accountVoorkeuren;
            }

            //Bestaande AccountVoorkeur aanpassen
            else
            {
                //AccountVoorkeur zoeken in lijst en verwijderen
                accountVoorkeur = accountVoorkeuren.Find(x => x.VoorkeurID == voorkeurID);
                accountVoorkeuren.Remove(accountVoorkeur);

                ResultaatString = zinnen[4];

                //Controleren of antwoord niet hetzelfde is als voordien
                if (accountVoorkeur.VoorkeurAntwoordID != antwoordID)
                {
                    //AccountVoorkeur instellen met nieuwe waardes
                    accountVoorkeur.VoorkeurID = voorkeurID;
                    accountVoorkeur.VoorkeurAntwoordID = antwoordID;

                    ResultaatString = zinnen[2];

                    //Aanpassing controleren + toevoegen
                    DataService.AanpassenAccountVoorkeur(accountVoorkeur);
                    ResultaatString = zinnen[3];
                    accountVoorkeuren.Add(accountVoorkeur);
                    return accountVoorkeuren;
                }
            }
            return accountVoorkeurenBackup; //Indien iets fot loopt backup meegeven
        }
    }
}
