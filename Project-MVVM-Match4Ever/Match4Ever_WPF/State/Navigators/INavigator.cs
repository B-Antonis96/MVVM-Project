using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.State.Navigators
{
    //Geïmplementeerd uit het voorbeeld van YouTuber SingletonSean! => een ware held!

    //Enum voor verschillende viewmodel types
    public enum ViewType
    {
        //Gebruikers schermen
        Login,
        Registreer,
        Wachtwoord,
        Welkom,
        InstellingenUser,
        Matches,
        Meldingen,
        Voorkeuren,

        //Admin schermen
        InstellingenAdmin,
        LijstGebruikers,
        VoorkeurenWijzigen,

        //Menu's
        MenuAdmin,
        MenuUser
    }
    public interface INavigator
    {
        //Huidig viewmodel
        BasisViewModel HuidigViewModel { get; set; }

        //Huidig menu viewmodel
        BasisViewModel HuidigMenuViewModel { get; set; }

        //Switchviewmodel command
        ICommand SwitchViewModel { get; }
    }
}
