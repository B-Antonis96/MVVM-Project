using Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts;
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
using System.Windows.Input;

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class WachtwoordViewModel : BasisViewModel //Niets in deze classe is serieus te nemen! Dit zou nooit zo in een applicatie gestopt moeten worden!!
    {
        //SCHERM CONTROLE\\
        public ICommand SwitchViewModel => Navigator.StaticNavigator.SwitchViewModel;


        //BENODIGDHEDEN\\
        private readonly AuthenticatorInstellingen Instellingen = new AuthenticatorInstellingen();
        private readonly Tools Tools = new Tools();

        //Tegen mijn systeem in! Maar toch om tijd te besparen op iets dat toch met mailserver helemaal anders zou zijn!
        private readonly WachtwoordService WachtwoordService = new WachtwoordService();


        //ONDERDELEN\\
        public string Email { get; set; }
        public string Code { get; set; }
        public string Wachtwoord { get; set; }
        public string WachtwoordBevestiging { get; set; }
        public Visibility Zichtbaarheid1 { get; set; } = Visibility.Visible;
        public Visibility Zichtbaarheid2 { get; set; } = Visibility.Hidden;
        public Visibility Zichtbaarheid3 { get; set; } = Visibility.Hidden;


        //UPDATEVIEWMODEL\\
        public void UpdateWachtwoordViewModel()
        {
            Zichtbaarheid1 = Visibility.Visible;
            Zichtbaarheid2 = Visibility.Hidden;
            Zichtbaarheid3 = Visibility.Hidden;
        }


        //Testen van commands
        public override bool CanExecute(object parameter)
        {
            if (parameter is Commands command)
            {
                switch (command)
                {
                    case Commands.Registreer:
                        return true;
                    case Commands.Aanmelden:
                        return true;
                    case Commands.Opslaan:
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
                        VerstuurCode();
                        break;
                    case Commands.Aanmelden:
                        CheckCode();
                        break;
                    case Commands.Opslaan:
                        AanpassenWachtwoord();
                        break;
                }
            }
        }

        //COMMANDS\\

        //Code versturen
        public void VerstuurCode()
        {
            string resultaat = Instellingen.EmailChecker(Email);
            if (resultaat == null)
            {
                if (WachtwoordService.CodeGenerator(Email))
                {
                    Code = WachtwoordService.Code;
                    Zichtbaarheid1 = Visibility.Hidden;
                    Zichtbaarheid2 = Visibility.Visible;
                    MessageBox.Show("U heeft mail! Uw code is gesynchroniseerd!");
                }
                else
                {
                    resultaat = WachtwoordService.ResultaatString;
                }
            }
            if (Tools.VeldVol(resultaat))
            {
                MessageBox.Show(resultaat);
            }
            Email = null;
        }

        //Code checken!
        public void CheckCode()
        {
            Zichtbaarheid2 = Visibility.Hidden;
            Zichtbaarheid3 = Visibility.Visible;
            Code = null;
        }

        //Veranderen vergeten wachtwoord
        public void AanpassenWachtwoord()
        {
            string resultaat = "Wachtwoordvelden mogen niet leeg zijn!";
            if (Tools.VeldVol(Wachtwoord) && Tools.VeldVol(WachtwoordBevestiging))
            {
                WachtwoordService.WachtwoordVergeten(WachtwoordService.AccountID, Wachtwoord, WachtwoordBevestiging, WachtwoordService.Code);
                resultaat = WachtwoordService.ResultaatString;
                UpdateWachtwoordViewModel();
            }
            MessageBox.Show(resultaat);
        }
    }

}
