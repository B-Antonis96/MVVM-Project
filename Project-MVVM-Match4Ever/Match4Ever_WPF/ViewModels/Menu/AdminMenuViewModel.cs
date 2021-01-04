using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.ViewModels.Menu
{
    public class AdminMenuViewModel : BasisViewModel
    {
        //SCHERM CONTROLE\\
        public ICommand SwitchViewModel => Navigator.StaticNavigator.SwitchViewModel;
        public bool VoorkeurButtonChecked { get; set; }

        //Aleen maar overschrijven van basisviewmodel, moeten overerven want anders werkt navigatie hier niet! 
        public AdminMenuViewModel()
        {
            VoorkeurButtonChecked = true;
        }

        public override bool CanExecute(object parameter)
        { 
            return true;
        }

        public override void Execute(object parameter) { }
    }
}
