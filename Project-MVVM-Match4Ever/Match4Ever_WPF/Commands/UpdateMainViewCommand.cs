using Match4Ever_WPF.ViewModels;
using Match4Ever_WPF.ViewModels.Login_Reg;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.Commands
{
    public class UpdateMainViewCommand : ICommand
    {
        private MainWindowViewModel mainViewModel;

        public UpdateMainViewCommand(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Start":
                    mainViewModel.SelectedMainViewModel = new LoginViewModel();
                    break;

            }
        }
    }
}
