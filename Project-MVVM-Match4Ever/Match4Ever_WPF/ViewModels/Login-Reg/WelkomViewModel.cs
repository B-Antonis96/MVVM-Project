using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.ViewModels.Login_Reg
{
    public class WelkomViewModel : BasisViewModel
    {
        //Aleen maar overschrijven van basisviewmodel, moeten overerven want anders werkt navigatie hier niet! 
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter) { }
    }
}
