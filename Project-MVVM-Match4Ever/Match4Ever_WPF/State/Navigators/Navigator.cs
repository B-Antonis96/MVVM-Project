using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Match4Ever_WPF.State.Navigators
{
    public class Navigator : INavigator, INotifyPropertyChanged //Geïmplementeerd uit het voorbeeld van YouTuber SingletonSean! => een ware held!
    {
        //ATTRIBUTEN\\

        //Knop zichtbaarheid
        private Visibility _knopzichtbaarheid = Visibility.Visible;
        public Visibility KnopZichtbaarheid
        {
            get { return _knopzichtbaarheid; }
            set
            {
                _knopzichtbaarheid = value;
                NotifyViewModelChanged(nameof(KnopZichtbaarheid));
            }
        }

        //Huidig viewmodel bepalen
        private BasisViewModel _huidigviewmodel;
        public BasisViewModel HuidigViewModel
        {
            get { return _huidigviewmodel; }
            set
            {
                _huidigviewmodel = value;
                NotifyViewModelChanged(nameof(HuidigViewModel));

                //StartKnop op zichtbaarheid controleren
                if (KnopZichtbaarheid == Visibility.Visible)
                {
                    //StartKnop onzichtbaar maken!
                    KnopZichtbaarheid = Visibility.Collapsed;
                }
            }
        }

        //Huidig viewmodel update command
        public ICommand UpdateHuidigViewModelCommand => new UpdateHuidigViewModelCommand(this);

        //Controleren of ViewModel veranderd
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyViewModelChanged(string viewmodel)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(viewmodel));
        }
    }
}
