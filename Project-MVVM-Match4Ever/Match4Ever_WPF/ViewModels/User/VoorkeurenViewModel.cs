using Match4Ever_DAL.Models;
using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Props;
using Match4Ever_WPF.WPFTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Match4Ever_WPF.ViewModels.User
{
    public class VoorkeurenViewModel : BasisViewModel
    {
        //BENODIGDHEDEN\\
        private readonly DataComs DataCom = new DataComs();
        private readonly AuthenticatorInstellingen Instellingen = new AuthenticatorInstellingen();
        private readonly Tools Tools = new Tools();
        private int VoorkeurTeller { get; set; } = 1;
        private int VoorkeurID { get; set; }
        public int AntwoordID { get; set; }


        //ONDERDELEN\\
        public List<Voorkeur> VoorkeurLijst { get; set; }
        public string Inhoud { get; set; }
        public List<VoorkeurAntwoord> VoorkeurAntwoordLijst { get; set; }
        public List<string> AntwoordLijst { get; set; }
        public string Antwoord { get; set; }


        //UPDATEVIEWMODEL\\
        public void UpdateVoorkeurenViewModel()
        {
            VoorkeurLijst = DataCom.VoorkeurVragenOphalen();
            VoorkeurAntwoordLijst = DataCom.VoorkeurAntwoordenOpIDOphalen(VoorkeurLijst[VoorkeurTeller - 1].VoorkeurID);
            Inhoud = VoorkeurLijst[VoorkeurTeller - 1].Vraag;
            AntwoordLijst = VoorkeurAntwoordLijst.Select(x => x.Antwoord).ToList();
            Antwoord = AntwoordLijst.FirstOrDefault();
            AntwoordIDSetter();
            VoorkeurID = VoorkeurLijst.Where(x => x.Vraag == Inhoud).Select(x => x.VoorkeurID).FirstOrDefault();
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
                    case Commands.Opslaan:
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
                    case Commands.Opslaan:
                        AntwoordIDSetter();
                        Instellingen.AccountVoorkeurenToevoegen(AntwoordID, VoorkeurID);
                        break;
                }
                UpdateVoorkeurenViewModel();
            }
        }

        //METHODES

        //AntwoordID setter
        private void AntwoordIDSetter()
        {
            AntwoordID = VoorkeurAntwoordLijst.Where(x => x.Antwoord == Antwoord).Select(x => x.VoorkeurAntwoordID).FirstOrDefault();
        }
    }
}
