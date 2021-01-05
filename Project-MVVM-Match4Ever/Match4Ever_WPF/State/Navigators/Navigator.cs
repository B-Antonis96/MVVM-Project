using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.ViewModels.Admin;
using Match4Ever_WPF.ViewModels.Login_Reg;
using Match4Ever_WPF.ViewModels.Menu;
using Match4Ever_WPF.ViewModels.Props;
using Match4Ever_WPF.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Match4Ever_WPF.State.Navigators
{
    public class Navigator : INavigator, INotifyPropertyChanged //Geïmplementeerd uit het voorbeeld van YouTuber SingletonSean! => een ware held!
    {
        //BENODIGDHEDEN\\

        //Controleren of ViewModel veranderd
        public event PropertyChangedEventHandler PropertyChanged;

        //Statische View Navigator die overal opgeroepen kan worden
        public static INavigator StaticNavigator { get; private set; } = new Navigator();

        //Huidig viewmodel update command
        public ICommand SwitchViewModel => new UpdateHuidigViewModelCommand();


        //VIEWMODEL SELECTORS\\

        //Huidig scherm viewmodel
        public BasisViewModel HuidigViewModel { get; set; }

        //Huidig menu viewmodel
        public BasisViewModel HuidigMenuViewModel { get; set; }
    }
}
