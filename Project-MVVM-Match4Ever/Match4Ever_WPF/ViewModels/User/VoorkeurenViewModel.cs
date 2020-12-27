using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Props;
using Match4Ever_WPF.WPFTools;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.ViewModels.User
{
    public class VoorkeurenViewModel : BasisViewModel
    {
        //BENODIGDHEDEN\\
        private readonly DataComs DataCom = new DataComs();
        private readonly Tools Tools = new Tools();
        private int VoorkeurTeller { get; set; } = 1;

        //CONSTRUCTOR\\
        public VoorkeurenViewModel()
        {
            VoorkeurLijst = DataCom.VoorkeurVragenOphalen();
        }

        //UPDATEVIEWMODEL\\
        public void UpdateVoorkeurenViewModel()
        {
            Inhoud = VoorkeurLijst[VoorkeurTeller - 1];
            AntwoordLijst = DataCom.VoorkeurAntwoordenOpIDOphalen(VoorkeurTeller);
        }

        //ONDERDELEN\\
        public List<string> VoorkeurLijst { get; set; }
        public string Inhoud { get; set; }
        public List<string> AntwoordLijst { get; set; }
        public string Antwoord { get; set; }


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
                        Navigeer(true);
                        break;
                    case Commands.Vorige:
                        Navigeer(false);
                        break;
                    case Commands.Update:
                        UpdateVoorkeurenViewModel();
                        break;
                    case Commands.Opslaan:
                        break;
                }
            }
        }


        //COMMANDS\\

        //Opslaan voorkeur antwoorden
        public void Opslaan()
        {
            string resultaat = "Er moet een antwoord geslecteerd zijn!";

            if (Tools.VeldVol(Antwoord))
            {

            }
        }

        //Voorkeur navigatie
        private void Navigeer(bool value)
        {
            int lengte = VoorkeurLijst.Count;
            if (value)
            {
                //Volgende voorkeur
                if (VoorkeurTeller < lengte)
                {
                    VoorkeurTeller += 1;
                }
                else
                {
                    VoorkeurTeller = 1;
                }
            }
            else
            {
                //Vorige voorkeur
                if (VoorkeurTeller > 1)
                {
                    VoorkeurTeller -= 1;
                }
                else
                {
                    VoorkeurTeller = lengte;
                }
            }

            //Update viewmodel
            UpdateVoorkeurenViewModel();
        }
    }
}
