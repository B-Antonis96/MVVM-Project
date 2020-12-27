﻿using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Props;
using Match4Ever_WPF.WPFTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Match4Ever_WPF.State.Authenticators;
using static Match4Ever_WPF.WPFTools.WPFEnums;

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class RegistreerViewModel : BasisViewModel
    {   
        //SCHERM CONTROLE\\
        public ICommand SwitchViewModel => Navigator.StaticNavigator.SwitchViewModel;

        //BENODIGDHEDEN\\
        private readonly DataComs DataCom = new DataComs();
        private readonly DateTime TijdMin18Jaar;
        private readonly Tools Tools = new Tools();

        //CONSTRUCTOR\\
        public RegistreerViewModel()
        {
            GeslachtLijst = Enum.GetNames(typeof(Geslachten)).ToList();
            GeaardheidLijst = Enum.GetNames(typeof(Geaardheden)).ToList();
            TijdMin18Jaar = DateTime.Now.AddDays(-1).AddYears(-18);
        }

        //UPDATEVIEWMODEL\\
        public void UpdateRegistreerViewModel()
        {
            //Locaties opniew opvullen
            StadLijst = DataCom.LocatiesOphalen();

            //Reset
            Gebruikersnaam = null;
            Email = null;
            Geaardheid = null;
            Geslacht = null;
            Stad = null;
            Geboortedatum = TijdMin18Jaar;
        }

        //ONDERDELEN\\
        public string Gebruikersnaam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string WachtwoordBevestiging { get; set; }
        public List<string> GeaardheidLijst { get; set; }
        public string Geaardheid { get; set; }
        public List<string> GeslachtLijst { get; set; }
        public string Geslacht { get; set; }
        public DateTime Geboortedatum { get; set; }
        public List<string> StadLijst { get; set; }
        public string Stad { get; set; }

        //Testen van commands
        public override bool CanExecute(object parameter)
        {
            if (parameter is Commands command)
            {
                switch (command)
                {
                    case Commands.Registreer:
                        return true;
                    case Commands.Update:
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
                    case Commands.Registreer:
                        Registreer();
                        break;
                    case Commands.Update:
                        UpdateRegistreerViewModel();
                        break;
                        
                }
            }
        }


        //COMMANDS\\

        //Registreer
        public void Registreer()
        {
            string resultaat = "Alle velden moeten ingevuld zijn!";

            //Controleren op lege velden doormiddel van VeldChecker methode
            if (VeldChecker())
            {
                //Gebruiker registreren doormidddel van Registratie service + resultaat opvragen
                resultaat = DataCom.Registreren(Email.ToLower(), Gebruikersnaam, Wachtwoord,
                    WachtwoordBevestiging, Geaardheid, Geslacht, Geboortedatum, Stad);
            }

            //Meldingen tonen
            MessageBox.Show(resultaat);

            //Indien registratie succesvol was terug naar login scherm
            if (Authenticator.IsIngelogd)
            {
                //Benodigde ViewModels aanmaken
                ViewModelBuilder.ViewModelsAanmaken();
                SwitchViewModel.Execute(ViewType.MenuUser);
                SwitchViewModel.Execute(ViewType.Welkom);
            }
        }

        //HULP METHODES\\

        //Checken op lege velden
        private bool VeldChecker()
        {
            if (Tools.VeldVol(Gebruikersnaam) &&
                Tools.VeldVol(Email) &&
                Tools.VeldVol(Wachtwoord) &&
                Tools.VeldVol(WachtwoordBevestiging) &&
                Tools.VeldVol(Geaardheid) &&
                Tools.VeldVol(Geslacht) &&
                Geboortedatum != null &&
                Tools.VeldVol(Stad))
            {
                return true;
            }
            return false;
        }
    }
}
