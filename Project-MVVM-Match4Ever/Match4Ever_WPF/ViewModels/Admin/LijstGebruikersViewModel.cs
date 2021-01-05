using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Match4Ever_WPF.ViewModels.Admin
{
    public class LijstGebruikersViewModel : BasisViewModel
    {
        //BENODIGDHEDEN\\
        private readonly Timer Timer = new Timer();
        private readonly AdminInstellingen Instellingen = new AdminInstellingen();


        //ONDERDELEN\\
        public string Invoer { get; set; }
        public List<string> GebruikersLijst { get; set; }
        public string Gebruiker { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string WachtwoordBevestiging { get; set; }



        //UPDATEVIEWMODEL\\
        public void UpdateLijstGebruikersViewModel()
        {
            TimerSetter(true);//Elke 30 seconden worden gebruikers geupdated!
            UpdateGebruikers();
            Invoer = null;
            Gebruikersnaam = null;
            Email = null;
            Gebruiker = null;
        }


        //Testen van commands
        public override bool CanExecute(object parameter)
        {
            if (parameter is Commands command)
            {
                switch (command)
                {
                    case Commands.Verwijder:
                        return true;
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
                    case Commands.Verwijder:
                        Instellingen.VerwijderGebruiker(Gebruiker);
                        break;
                    case Commands.Registreer:
                        Instellingen.RegistrerenAdmin(Gebruikersnaam, Email, Wachtwoord, WachtwoordBevestiging);
                        break;
                    case Commands.Update:
                        break;
                }
                UpdateLijstGebruikersViewModel();
            }
        }

        //HULP METHODES\\

        //Timer
        public void TimerSetter(bool timerstand)
        {
            if (timerstand)
            {
                Timer.Interval = 30000;
                Timer.Elapsed += TimerInit;
                Timer.AutoReset = true;
                Timer.Enabled = true;
                Timer.Start();
            }
            else
            {
                Timer.Stop();
                Timer.Enabled = false;
            }
        }

        //Timer uitvoeren
        private void TimerInit(object source, ElapsedEventArgs e)
        {
            UpdateGebruikers();
        }

        private void UpdateGebruikers()
        {
            GebruikersLijst = Instellingen.GebruikerNamenOphalen();
        }
    }
}
