using Match4Ever_DAL.Models;
using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.ViewModels.Props;
using Match4Ever_WPF.WPFTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.ViewModels.Admin
{
    public class VoorkeurenWijzigenViewModel : BasisViewModel
    {
        //BENODIGDHEDEN\\
        private DataComs DataCom;
        private readonly Tools Tools = new Tools();
        private readonly AdminInstellingen Instellingen = new AdminInstellingen();
        private int VoorkeurTeller { get; set; } = 1;
        private int VoorkeurID { get; set; }
        private int AntwoordID { get; set; }

        //ONDERDELEN\\
        public List<Voorkeur> VoorkeurLijst { get; set; }
        public string Inhoud { get; set; }
        public string VoorkeurVraag { get; set; }
        public List<VoorkeurAntwoord> VoorkeurAntwoordLijst { get; set; }
        public List<string> AntwoordLijst { get; set; }
        public string VoorkeurAntwoord { get; set; }
        public string Antwoord { get; set; }


        //UPDATEVIEWMODEL\\
        public void UpdateVoorkeurenWijzigenViewModel()
        {
            //Alles ophalen en instellen
            DataCom = new DataComs(); //Anders updated inhoud niet!?
            VoorkeurLijst = DataCom.VoorkeurVragenOphalen();
            VoorkeurAntwoordLijst = DataCom.VoorkeurAntwoordenOpIDOphalen(VoorkeurLijst[VoorkeurTeller - 1].VoorkeurID);
            Inhoud = VoorkeurLijst[VoorkeurTeller - 1].Vraag;
            AntwoordLijst = VoorkeurAntwoordLijst.Select(x => x.Antwoord).ToList();
            VoorkeurAntwoord = AntwoordLijst.FirstOrDefault();
            VoorkeurID = VoorkeurLijst.Where(x => x.Vraag == Inhoud).Select(x => x.VoorkeurID).FirstOrDefault();
            AntwoordID = VoorkeurAntwoordLijst.Where(x => x.Antwoord == VoorkeurAntwoord).Select(x => x.VoorkeurAntwoordID).FirstOrDefault();

            //Leegmaken velden
            VoorkeurVraag = null;
            Antwoord = null;
        }


        //Testen van commands
        public override bool CanExecute(object parameter)
        {
            if (parameter is Commands command)
            {
                switch (command)
                {
                    case Commands.Volgende:
                        return true;
                    case Commands.Vorige:
                        return true;
                    case Commands.Update:
                        return true;
                    case Commands.ToevoegenVoorkeur:
                        return true;
                    case Commands.AanpassenVoorkeur:
                        return true;
                    case Commands.VerwijderenVoorkeur:
                        return true;
                    case Commands.ToevoegenAntwoord:
                        return true;
                    case Commands.AanpassenAntwoord:
                        return true;
                    case Commands.VerwijderenAntwoord:
                        return true;
                }
            }
            return false;
        }

        //Uitvoeren van commands
        public override void Execute(object parameter)
        {
            if (parameter is Commands command)
            {
                switch (command)
                {
                    case Commands.Volgende:
                        VoorkeurTeller = Tools.Navigeer(true, VoorkeurTeller, VoorkeurLijst.Count);
                        break;
                    case Commands.Vorige:
                        VoorkeurTeller = Tools.Navigeer(false, VoorkeurTeller, VoorkeurLijst.Count);
                        break;
                    case Commands.Update:
                        //Niet meer nodig wordt beneden opgeroepen => maar command is nog wel nodig!
                        break;
                    case Commands.ToevoegenVoorkeur:
                        Instellingen.VoorkeurenToevoegen(VoorkeurVraag);
                        break;
                    case Commands.AanpassenVoorkeur:
                        Instellingen.VoorkeurenAanpassen(VoorkeurVraag, VoorkeurID);
                        break;
                    case Commands.VerwijderenVoorkeur:
                        Instellingen.VoorkeurenVerwijderen(VoorkeurID);
                        break;
                    case Commands.ToevoegenAntwoord:
                        Instellingen.AntwoordenToevoegen(Antwoord, VoorkeurID);
                        break;
                    case Commands.AanpassenAntwoord:
                        Instellingen.AntwoordenAanpassen(Antwoord, AntwoordID, VoorkeurID);
                        break;
                    case Commands.VerwijderenAntwoord:
                        Instellingen.AntwoordenVerwijderen(AntwoordID, VoorkeurID);
                        break;
                }
                UpdateVoorkeurenWijzigenViewModel();
            }
        }
    }
}
