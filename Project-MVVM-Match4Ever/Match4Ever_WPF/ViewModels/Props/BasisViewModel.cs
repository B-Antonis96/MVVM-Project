using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Match4Ever_WPF.ViewModels.Props
{
    public abstract class BasisViewModel : ICommand, INotifyPropertyChanged  //Geïmplementeerd uit het voorbeeld van de lessen!
    {
        //EXECUTION PROPERTIES
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Kan het uitvoeren?
        public abstract bool CanExecute(object parameter);

        //Uivoeren!
        public abstract void Execute(object parameter);


        //PropertyChanged => thats it! Thanks to fody!
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
