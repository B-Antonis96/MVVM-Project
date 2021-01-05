using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.AuthenticationServices
{
    public sealed class AntwoordVoorkeurService
    {
        //BENODIGDHEDEN\\
        private readonly DataService DataService = new DataService();
        private readonly DataTools Tools = new DataTools();
        public string ResultaatString { get; private set; }
        private List<Voorkeur> Voorkeuren;
        private Voorkeur Voorkeur;
        private List<VoorkeurAntwoord> Antwoorden;
        private VoorkeurAntwoord Antwoord;


        //VOORKEUR FUNCTIES\\

        //Voorkeur toevoegen
        public void VoorkeurenToevoegen(string vraag)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Voorkeur bestaat al!", "Voorkeur toegevoegd" };
            ResultaatString = zinnen[0];

            UpdateVoorkeuren();

            //Controleren of vraag al bestaat in voorkeuren
            if (!Voorkeuren.Select(x => x.Vraag).Contains(vraag))
            {
                //Nieuwe voorkeur aanmaken
                Voorkeur = new Voorkeur()
                {
                    Vraag = vraag
                };

                ResultaatString = zinnen[1];

                //Toevoegen voorkeur
                DataService.ToevoegenVoorkeur(Voorkeur);
            }
        }

        //Voorkeur aanpassen
        public void VoorkeurenAanpassen(string vraag, int voorkeurID)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Voorkeurlijst is leeg!", "Vraag is reeds hetzelfde!", "Voorkeur aangepast!" };
            ResultaatString = zinnen[0];

            UpdateVoorkeuren();

            //Controleren of voorkeuren meer zijn als 0
            if (Tools.SizeChecker(Voorkeuren.Count, 0))
            {
                ResultaatString = zinnen[1];

                //Voorkeur zoeken op voorkeurID
                Voorkeur = Voorkeuren.Find(x => x.VoorkeurID == voorkeurID);
                if (!Tools.ParameterCheck(Voorkeur.Vraag, vraag))
                {
                    //Gevonden voorkeur wijzigen
                    Voorkeur.Vraag = vraag;
                    ResultaatString = zinnen[2];
                    DataService.AanpassenVoorkeur(Voorkeur);
                }
            }
        }

        //Voorkeur verwijderen
        public void VoorkeurenVerwijderen(int voorkeurID)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Voorkeurlijst is leeg!", "Voorkeur verwijderd!" };
            ResultaatString = zinnen[0];

            UpdateVoorkeuren();

            //Controleren of voorkeuren meer zijn als 0
            if (Tools.SizeChecker(Voorkeuren.Count, 0))
            {
                ResultaatString = zinnen[1];

                //Voorkeur zoeken en verwijderen
                Voorkeur = Voorkeuren.Find(x => x.VoorkeurID == voorkeurID);
                DataService.VerwijderenVoorkeur(Voorkeur);
            }
        }

        //ANTWOORD FUNCTIES\\

        //Antwoord toevoegen
        public void AntwoordenToevoegen(string antwoord, int voorkeurID)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Antwoord bestaat al!", "Antwoord toegevoegd" };
            ResultaatString = zinnen[0];

            UpdateAntwoorden(voorkeurID);

            //Controleren of antwoord al bestaat in voorkeuren
            if (!Antwoorden.Select(x => x.Antwoord).Contains(antwoord))
            {
                //Nieuw antwoord aanmaken
                Antwoord = new VoorkeurAntwoord()
                {
                    Antwoord = antwoord,
                    VoorkeurID = voorkeurID
                };

                ResultaatString = zinnen[1];

                //antwoord toevoegen
                DataService.ToevoegenAntwoord(Antwoord);
            }
        }

        //Antwoord aanpassen
        public void AntwoordenAanpassen(string antwoord, int antwoordID, int voorkeurID)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Antwoordlijst is leeg!", "Antwoord is reeds hetzelfde!","Antwoord aangepast!" };
            ResultaatString = zinnen[0];

            UpdateAntwoorden(voorkeurID);

            //Controleren of antwoorden meer zijn als 0
            if (Tools.SizeChecker(Antwoorden.Count, 0))
            {
                ResultaatString = zinnen[1];

                //Antwoord zoeken op antwoordID
                Antwoord = Antwoorden.Find(x => x.VoorkeurAntwoordID == antwoordID);
                if (!Tools.ParameterCheck(Antwoord.Antwoord, antwoord))
                {
                    ResultaatString = zinnen[2];

                    //Gevonden antwoord aanpassen
                    Antwoord.Antwoord = antwoord;
                    DataService.AanpassenAntwoord(Antwoord);
                }
            }
        }

        //Antwoord verwijderen
        public void AntwoordenVerwijderen(int antwoordID, int voorkeurID)
        {
            //Resultaten aanmaken + standaard resultaat
            string[] zinnen = { "Antwoordlijst is leeg!", "Antwoord verwijderd!" };
            ResultaatString = zinnen[0];

            UpdateAntwoorden(voorkeurID);

            //Controleren of antwoorden meer zijn als 0
            if (Tools.SizeChecker(Antwoorden.Count, 0))
            {
                ResultaatString = zinnen[1];

                //Antwoord zoeken en verwijderen
                Antwoord = Antwoorden.Find(x => x.VoorkeurAntwoordID == antwoordID);
                DataService.VerwijderenAntwoord(Antwoord);
            }
        }



        //HELPER FUNCTIES\\

        //Update voorkeuren
        private void UpdateVoorkeuren()
        {
            Voorkeuren = DataService.VoorkeurenOphalen();
        }

        //Update antwoorden
        private void UpdateAntwoorden(int voorkeurID)
        {
            Antwoorden = DataService.VoorkeurAntwoordenOphalenOpVoorkeurID(voorkeurID);
        }

    }
}
