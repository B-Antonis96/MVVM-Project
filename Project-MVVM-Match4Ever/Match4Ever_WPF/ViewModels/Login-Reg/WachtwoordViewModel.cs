using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class WachtwoordViewModel : BasisViewModel
    {
        //SCHERM CONTROLE\\
        public ICommand SwitchViewModel => Navigator.StaticNavigator.SwitchViewModel;

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
