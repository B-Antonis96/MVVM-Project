using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Match4Ever_DAL.Data;
using Match4Ever_DAL.Data.UnitOfWork;
using Match4Ever_WPF.ViewModels.Props;
using System.Windows.Input;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Navigators;

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class LoginViewModel : BasisViewModel
    {
        #region WindowControls
        public INavigator Navigator { get; set; }
        public UpdateHuidigViewModelCommand UpdateHuidigViewModelCommand { get; set; }

        public LoginViewModel(INavigator navigator)
        {
            this.Navigator = navigator;
            this.UpdateHuidigViewModelCommand = new UpdateHuidigViewModelCommand(Navigator);
        }

        #endregion

        //Attributen
        public string AccountLogin { get; set; }

        public string Wachtwoord { get; set; }

        public ObservableCollection<Account> Accounts { get; set; }


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


    }
}
