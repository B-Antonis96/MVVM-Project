using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Login_Reg;
using Match4Ever_WPF.ViewModels.User;
using Match4Ever_WPF.ViewModels.Admin;
using Match4Ever_WPF.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Match4Ever_WPF.ViewModels.Props;

namespace Match4Ever_WPF.State.Commands
{
    public class UpdateHuidigViewModelCommand : ICommand //Geïmplementeerd uit het voorbeeld van YouTuber SingletonSean! => een ware held!
    {
        public event EventHandler CanExecuteChanged; //Benodigd voor ICommand

        //Navigator ophalen
        public INavigator StaticNavigator => Navigator.StaticNavigator;
        

        //Testen van commands
        public bool CanExecute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                switch (viewType)
                {
                    //MENU\\
                    case ViewType.MenuAdmin:
                        return true;
                    case ViewType.MenuUser:
                        return true;

                    //LOGIN & REGISTRATIE COMMANDS
                    case ViewType.Login:
                        return true;
                    case ViewType.Registreer:
                        return true;
                    case ViewType.Wachtwoord:
                        return true;
                    case ViewType.Welkom:
                        return true;

                    //USER COMMANDS\\
                    case ViewType.Matches:
                        return true;
                    case ViewType.Meldingen:
                        return true;
                    case ViewType.InstellingenUser:
                        return true;
                    case ViewType.Voorkeuren:
                        return true;

                    //ADMIN COMMANDS\\
                    case ViewType.InstellingenAdmin:
                        return true;
                    case ViewType.LijstGebruikers:
                        return true;
                    case ViewType.VoorkeurenWijzigen:
                        return true;
                }
            }
            return false;
        }

        //Uitvoeren van commands
        public void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                //Statisch viewmodel aanmaken op basis van command en zo nodig updaten
                switch (viewType)
                {
                    //MENU\\
                    case ViewType.MenuAdmin:
                        StaticNavigator.HuidigMenuViewModel = ViewModelBuilder.AdminMenuVM;
                        break;
                    case ViewType.MenuUser:
                        StaticNavigator.HuidigMenuViewModel = ViewModelBuilder.UserMenuVM;
                        break;

                    //LOGIN & REGISTRATIE COMMANDS
                    case ViewType.Login:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.LoginVM;
                        ViewModelBuilder.LoginVM.Execute(Commands.Update);
                        break;
                    case ViewType.Registreer:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.RegistreerVM;
                        ViewModelBuilder.RegistreerVM.Execute(Commands.Update);
                        break;
                    case ViewType.Wachtwoord:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.WachtwoordVM;
                        ViewModelBuilder.WachtwoordVM.Execute(Commands.Update);
                        break;
                    case ViewType.Welkom:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.WelkomVM;
                        break;

                    //USER COMMANDS\\
                    case ViewType.Matches:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.MatchesVM;
                        break;
                    case ViewType.Meldingen:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.MeldingenVM;
                        break;
                    case ViewType.InstellingenUser:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.UserInstellingenVM;
                        ViewModelBuilder.UserInstellingenVM.Execute(Commands.Update);
                        break;
                    case ViewType.Voorkeuren:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.VoorkeurenVM;
                        ViewModelBuilder.VoorkeurenVM.Execute(Commands.Update);
                        break;

                    //ADMIN COMMANDS\\
                    case ViewType.InstellingenAdmin:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.AdminInstellingenVM;
                        ViewModelBuilder.AdminInstellingenVM.Execute(Commands.Update);
                        break;
                    case ViewType.LijstGebruikers:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.LijstGebruikersVM;
                        ViewModelBuilder.LijstGebruikersVM.Execute(Commands.Update);
                        break;
                    case ViewType.VoorkeurenWijzigen:
                        StaticNavigator.HuidigViewModel = ViewModelBuilder.VoorkeurenWijzigenVM;
                        ViewModelBuilder.VoorkeurenWijzigenVM.Execute(Commands.Update);
                        break;
                }
            }
        }
    }
}
