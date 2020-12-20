using Match4Ever_WPF.State.Navigators;
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
        #region Overervingen
        public override string this[string columnName]
        {
            get { return ""; }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter) { }
        #endregion

        #region WindowControls

        public INavigator Navigator { get; set; } = new Navigator();

        #endregion
    }
}
