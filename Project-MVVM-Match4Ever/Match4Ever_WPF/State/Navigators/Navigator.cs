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
    public class Navigator : INavigator, INotifyPropertyChanged
    {
        private BasisViewModel _huidigviewmodel;
        public BasisViewModel HuidigViewModel
        {
            get { return _huidigviewmodel; }
            set
            {
                _huidigviewmodel = value;
                NotifyPropertyChanged(nameof(HuidigViewModel));
            }
        }

        public ICommand UpdateHuidigViewModelCommand => new UpdateHuidigViewModelCommand(this);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
