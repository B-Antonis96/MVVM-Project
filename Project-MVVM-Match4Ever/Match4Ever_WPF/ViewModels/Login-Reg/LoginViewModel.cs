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

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class LoginViewModel : MainWindowViewModel
    {
        //Uniy Of Work
        //IUnitOfWork unitOfWork = new UnitOfWork(new Match4EverEntities());

        //Initiaties
        private string _accountLogin;
        public string AccountLogin
        {
            get { return _accountLogin; }
            set { _accountLogin = value; NotifyPropertyChanged(nameof(AccountLogin)); }
        }

        private string _wachtwoord;
        public string Wachtwoord
        {
            get { return _wachtwoord; }
            set { _wachtwoord = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<Account> _accounts;
        public ObservableCollection<Account> Accounts
        {
            get { return _accounts; }
            set { _accounts = value; NotifyPropertyChanged(nameof(Wachtwoord)); }
        }


        public LoginViewModel()
        {
            //Accounts = new ObservableCollection<Account>(unitOfWork.AccountRepo.Ophalen());
        }

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



    }
}
