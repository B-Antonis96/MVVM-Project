using Match4Ever_DAL.DALServices.AuthenticationServices;
using Match4Ever_DAL.Data.UnitOfWork;
using Match4Ever_DAL.Models;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class RegistreerViewModel : BasisViewModel
    {
        #region WindowControls
        public INavigator Navigator = UpdateHuidigViewModelCommand.Navigator;
        public UpdateHuidigViewModelCommand UpdateHuidigViewModelCommand { get; set; }
        #endregion

        //CONSTRUCTOR\\
        public RegistreerViewModel()
        {
            this.UpdateHuidigViewModelCommand = new UpdateHuidigViewModelCommand(Navigator);
        }

        //BENODIGDHEEDEN\\
        private RegistrationService Registreren = new RegistrationService();

        //GETTERS & SETTERS\\
        public string Gebruikersnaam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string WachtwoordBevestiging { get; set; }
        public string Naam { get; set; }
        public List<string> GeslachtLijst { get; set; }
        public string Geslacht { get; set; }
        public List<string> LandLijst { get; set; }
        public string Land { get; set; }
        public List<string> StadLijst { get; set; }
        public string Stad { get; set; }


        public override string this[string columnName]
        {
            get { return ""; }
        }

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Registreer":
                    return true;
            };

            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Registreer":
                    Registreer();
                    break;
            };
        }

        //COMMAND PARAMETERS\\
        public void Registreer()
        {
            string resultaat = "Alle velden moeten ingevuld zijn!";

            if (VeldChecker())
            {
                Registreren.RegistreerGebruiker(Email, Gebruikersnaam, Wachtwoord, WachtwoordBevestiging, Naam, Geslacht, Land, Stad);
                resultaat = Registreren.Resultaat();
            }

            MessageBox.Show(resultaat);

        }


        //HULP METHODES\\

        //Checken op lege velden
        private bool VeldChecker()
        {
            if (VeldVol(Gebruikersnaam) &&
                VeldVol(Email) &&
                VeldVol(Wachtwoord) &&
                VeldVol(WachtwoordBevestiging) &&
                VeldVol(Naam) &&
                VeldVol(Geslacht) &&
                VeldVol(Land) &&
                VeldVol(Stad))
            {
                return true;
            }
            return false;
        }

        //Kijken of veld "vol" is => kwestie van niet vaak !string.IsNullOrWhiteSpace() te moeten typen!
        private bool VeldVol(string veld)
        {
            if (!string.IsNullOrWhiteSpace(veld))
            {
                return true;
            }
            return false;
        }
    }
}
