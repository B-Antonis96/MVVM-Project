using Match4Ever_WPF.State.Commands;
using Match4Ever_WPF.State.Navigators;
using Match4Ever_WPF.ViewModels.Login_Reg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Match4Ever_WPF.ViewModels.Props
{
    public class MainWindowViewModel : BasisViewModel
    {
        //SCHERM CONTROLE\\
        public INavigator StaticNavigator => Navigator.StaticNavigator; //Verwijzing naar statische Navigator

        //CONTRUCTOR\\
        public MainWindowViewModel()
        {
            ViewModelBuilder.ViewModelsAanmaken();
            StaticNavigator.SwitchViewModel.Execute(ViewType.Login);
        }

        //Aleen maar overschrijven van basisviewmodel, moeten overerven want anders werkt navigatie hier niet! 
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter) { }
    }
}
