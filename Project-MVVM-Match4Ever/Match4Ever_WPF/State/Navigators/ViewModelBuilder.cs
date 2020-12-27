using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.ViewModels.Admin;
using Match4Ever_WPF.ViewModels.Login_Reg;
using Match4Ever_WPF.ViewModels.Menu;
using Match4Ever_WPF.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.State.Navigators
{
    public class ViewModelBuilder
    {
        //STATISCHE VIEWMODELS\\

        //Statische Menu ViewModels
        public static AdminMenuViewModel AdminMenuVM;
        public static UserMenuViewModel UserMenuVM;

        //Statische Login - Reg ViewModels
        public static LoginViewModel LoginVM;
        public static RegistreerViewModel RegistreerVM;
        public static WachtwoordViewModel WachtwoordVM;
        public static WelkomViewModel WelkomVM;

        //Statische User ViewModels
        public static MatchesViewModel MatchesVM;
        public static MeldingenViewModel MeldingenVM;
        public static UserInstellingenViewModel UserInstellingenVM;
        public static VoorkeurenViewModel VoorkeurenVM;

        //Statische Admin ViewModels
        public static AdminInstellingenViewModel AdminInstellingenVM;
        public static LijstGebruikersViewModel LijstGebruikersVM;
        public static VoorkeurenWijzigenViewModel VoorkeurenWijzigenVM;



        //ViewModels aanmaken => Alleen de benodigde aanmaken, veiligheid en prestatie gericht!
        public static void ViewModelsAanmaken()
        {
            if (!Authenticator.IsIngelogd)
            {
                //Menu ViewModels aanmaken
                LoginVM = new LoginViewModel();
                RegistreerVM = new RegistreerViewModel();
                WachtwoordVM = new WachtwoordViewModel();
            }
            else
            {
                if (!Authenticator.IsAdmin)
                {
                    //User ViewModels aanmaken
                    UserMenuVM = new UserMenuViewModel();
                    WelkomVM = new WelkomViewModel();
                    MatchesVM = new MatchesViewModel();
                    MeldingenVM = new MeldingenViewModel();
                    UserInstellingenVM = new UserInstellingenViewModel();
                    VoorkeurenVM = new VoorkeurenViewModel();
                    LoginRegViewModelsNullen();
                }
                else
                {
                    //Admin ViewModels aanmaken
                    AdminMenuVM = new AdminMenuViewModel();
                    AdminInstellingenVM = new AdminInstellingenViewModel();
                    LijstGebruikersVM = new LijstGebruikersViewModel();
                    VoorkeurenWijzigenVM = new VoorkeurenWijzigenViewModel();
                    LoginRegViewModelsNullen();
                }
            }
        }

        //ViewModels op NULL zetten
        public static void ViewModelsNullen()
        {
            if (!Authenticator.IsAdmin)
            {
                //User ViewModels op null zetten
                UserMenuVM = null;
                WelkomVM = null;
                MatchesVM = null;
                MeldingenVM = null;
                UserInstellingenVM = null;
                VoorkeurenVM = null;
            }
            else
            {
                //Admin ViewModels op null zetten
                AdminMenuVM = null;
                AdminInstellingenVM = null;
                LijstGebruikersVM = null;
                VoorkeurenWijzigenVM = null;
            }
        }

        //Login en registratie ViewModel op null zetten
        public static void LoginRegViewModelsNullen()
        {
            //Menu ViewModels op null zetten
            LoginVM = null;
            RegistreerVM = null;
            WachtwoordVM = null;
        }
    }
}
