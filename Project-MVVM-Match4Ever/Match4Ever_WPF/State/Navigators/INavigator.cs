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

    public enum ViewType
    {
        Login,
        Registreer,
        Wachtwoord
    }
    public interface INavigator
    {
        BasisViewModel HuidigViewModel { get; set; }
        ICommand UpdateHuidigViewModelCommand { get; }
    }
}
