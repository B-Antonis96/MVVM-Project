using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class WachtwoordViewModel : BasisViewModel
    {
        #region WindowControls
        public INavigator Navigator { get; set; }
        public UpdateHuidigViewModelCommand UpdateHuidigViewModelCommand { get; set; }

        public WachtwoordViewModel(INavigator navigator)
        {
            this.Navigator = navigator;
            this.UpdateHuidigViewModelCommand = new UpdateHuidigViewModelCommand(Navigator);
        }

        #endregion


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
