using Match4Ever_DAL.DALServices.AuthenticationServices;
using Match4Ever_WPF.WPFTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Match4Ever_WPF.State.Authenticators
{
    public sealed class AdminInstellingen
    {
        //BENODIGDHEDEN\\
        private readonly Tools Tools = new Tools();
        private readonly AdminService AdminService = new AdminService();
        private readonly AntwoordVoorkeurService VoorkeurService = new AntwoordVoorkeurService();
        private readonly RegistrationService RegistratieService = new RegistrationService();
        private readonly AuthenticatorInstellingen AuthInstellingen = new AuthenticatorInstellingen();


        //VOORKEUREN METHODES

        //Voorkeuren toevoegen
        public void VoorkeurenToevoegen(string vraag)
        {
            //Standaard resultaat
            string resultaat = "Vraag aanpassen kan niet leeg zijn als u wilt toevoegen!";

            if (Tools.VeldVol(vraag))
            {
                //Als veld vol is voorkeur toevoegen
                VoorkeurService.VoorkeurenToevoegen(vraag);
                resultaat = VoorkeurService.ResultaatString;
            }
            MessageBox.Show(resultaat);
        }

        //Voorkeuren aanpassen
        public void VoorkeurenAanpassen(string vraag, int voorkeurID)
        {
            //Standaard resultaat
            string resultaat = "Vraag aanpassen kan niet leeg zijn als u wilt aanpassen!";

            if (Tools.VeldVol(vraag))
            {
                //Als veld vol is voorkeur aanpassen
                VoorkeurService.VoorkeurenAanpassen(vraag, voorkeurID);
                resultaat = VoorkeurService.ResultaatString;
            }
            MessageBox.Show(resultaat);
        }

        //Voorkeuren verwijderen
        public void VoorkeurenVerwijderen(int voorkeurID)
        {
            VoorkeurService.VoorkeurenVerwijderen(voorkeurID);
            MessageBox.Show(VoorkeurService.ResultaatString);
        }


        //ANTWOORDEN METHODES

        //Antwoorden toevoegen
        public void AntwoordenToevoegen(string antwoord, int voorkeurID)
        {
            //Standaard resultaat
            string resultaat = "Antwoord aanpassen kan niet leeg zijn als u wilt toevoegen!";

            if (Tools.VeldVol(antwoord))
            {
                //Als veld vol is antwoord toevoegen
                VoorkeurService.AntwoordenToevoegen(antwoord, voorkeurID);
                resultaat = VoorkeurService.ResultaatString;
            }
            MessageBox.Show(resultaat);
        }

        //Antwoorden aanpassen
        public void AntwoordenAanpassen(string antwoord, int antwoordID, int voorkeurID)
        {
            //Standaard resultaat
            string resultaat = "Antwoord aanpassen kan niet leeg zijn als u wilt aanpassen!";

            if (Tools.VeldVol(antwoord))
            {
                //Als veld vol is antwoord wijzigen
                VoorkeurService.AntwoordenAanpassen(antwoord, antwoordID, voorkeurID);
                resultaat = VoorkeurService.ResultaatString;
            }
            MessageBox.Show(resultaat);
        }

        //Antwoorden verwijderen
        public void AntwoordenVerwijderen(int antwoordID, int voorkeurID)
        {
            VoorkeurService.AntwoordenVerwijderen(antwoordID, voorkeurID);
            MessageBox.Show(VoorkeurService.ResultaatString);
        }



        //LIJSTGERBUIKERS METHODES

        //Ophalen gebruikersnamen weglaten ingelogd account
        public List<string> GebruikerNamenOphalen()
        {
            List<string> gebruikers = AdminService.GebruikersOphalen();
            if (Authenticator.HuidigAccount.Gebruikersnaam != null)
            {
                gebruikers.Remove(Authenticator.HuidigAccount.Gebruikersnaam);
            }
            return gebruikers;
        }

        //Admin registreren
        public void RegistrerenAdmin(string gebruikersnaam, string email, string wachtwoord, string bevestigWw)
        {
            List<string> resultaten = new List<string>();
            
            //Velden controleren
            if (Tools.VeldVol(gebruikersnaam) && Tools.VeldVol(email) && Tools.VeldVol(bevestigWw))
            {
                //Email testen
                string tester = AuthInstellingen.EmailChecker(email);
                if (tester == null)
                {
                    //Indien test correct admin toevoegen
                    RegistratieService.Registreer(email, gebruikersnaam, wachtwoord,
                    bevestigWw, null, null, DateTime.Now.AddYears(-20), null, Authenticator.IsAdmin);
                    resultaten.AddRange(RegistratieService.ResultaatString);
                }
                else
                {
                    //Anders fout teruggeven
                    resultaten.Clear();
                    resultaten.Add(tester);
                }
            }
            else
            {
                resultaten.Add("Alle velden moeten ingevuld zijn!");
            }

            //Meldingen tonen
            var resultaat = string.Join(Environment.NewLine, resultaten);
            MessageBox.Show(resultaat);
        }

        //Verwijderen gebruiker
        public void VerwijderGebruiker(string gebruikersnaam)
        {
            //Warning voor account te verwijderen
            var resultaat = MessageBox.Show("Dit account echt verwijderen? Dit kan niet ongedaan gemaakt worden!",
            "HEEL ZEKER ?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
            if (resultaat == MessageBoxResult.Yes)
            {
                //Account verwijderen
                AdminService.VerwijderGebruikerOpNaam(gebruikersnaam);
                MessageBox.Show(AdminService.ResultaatString);
            }
        }
    }
}
