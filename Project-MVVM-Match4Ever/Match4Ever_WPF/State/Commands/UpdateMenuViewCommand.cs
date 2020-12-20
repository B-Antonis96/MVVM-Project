using Match4Ever_WPF.ViewModels;
using Match4Ever_WPF.ViewModels.Menu;
using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.Commands
{
    public class UpdateMenuViewCommand : ICommand
    {
        private MainWindowViewModel menuViewModel;

        public UpdateMenuViewCommand(MainWindowViewModel viewModel)
        {
            this.menuViewModel = viewModel;
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
                case "User":
                    menuViewModel.SelectedMenuViewModel = new UserMenuViewModel();
                    break;

                case "Admin":
                    menuViewModel.SelectedMenuViewModel = new AdminMenuViewModel();
                    break;
            }
        }
    }
}
