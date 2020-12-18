using Match4Ever_WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.ViewModels.Props
{
    public class MainWindowViewModel : BasisViewModel
    {
        #region Overriding
        public override string this[string columnName]
        {
            get { return ""; }
        }

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {

            };

            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {

            };
        }
        #endregion

        #region WindowControls

        private string _knopzichtbaarheid = "Visible";
        public string KnopZichtbaarheid
        {
            get { return _knopzichtbaarheid; }
            set
            {
                _knopzichtbaarheid = value;
                NotifyPropertyChanged();
            }
        }

        private string _menuzichtbaarheid;
        public string MenuZichtbaarheid
        {
            get { return _menuzichtbaarheid; }
            set
            {
                _menuzichtbaarheid = value;
                NotifyPropertyChanged();
            }
        }

        private BasisViewModel _selectedmainviewmodel;
        public BasisViewModel SelectedMainViewModel
        {
            get { return _selectedmainviewmodel; }
            set
            {
                _selectedmainviewmodel = value;
                NotifyPropertyChanged(nameof(SelectedMainViewModel));
                NotifyPropertyChanged(KnopZichtbaarheid = "Hidden");
            }
        }

        private BasisViewModel _selectedmenuviewmodel;
        public BasisViewModel SelectedMenuViewModel
        {
            get { return _selectedmenuviewmodel; }
            set
            {
                _selectedmenuviewmodel = value;
                NotifyPropertyChanged(nameof(SelectedMainViewModel));
            }
        }

        public ICommand UpdateMainViewCommand { get; set; }
        public ICommand UpdateMenuViewCommand { get; set; }

        public MainWindowViewModel()
        {
            UpdateMainViewCommand = new UpdateMainViewCommand(this);
            UpdateMenuViewCommand = new UpdateMenuViewCommand(this);
        }

        #endregion
    }
}
