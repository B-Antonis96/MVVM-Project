using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match4Ever_WPF.ViewModels.Props;
using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Authenticators;
using Match4Ever_WPF.State.Navigators;

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class LoginViewModel : BasisViewModel
    {
        #region WindowControls
        public INavigator Navigator = UpdateHuidigViewModelCommand.Navigator;
        public UpdateHuidigViewModelCommand UpdateHuidigViewModelCommand { get; set; }

        public LoginViewModel()
        {
            this.UpdateHuidigViewModelCommand = new UpdateHuidigViewModelCommand(Navigator);
        }

        #endregion

        //ATTRIBUTEN\\
        public string AccountLogin { get; set; }

        public string Wachtwoord { get; set; }


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
