using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Match4Ever_WPF.ViewModels
{
    public abstract class BasisViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public abstract string this[string columnName] { get; }

        public bool IsGeldig()
        {
            return string.IsNullOrWhiteSpace(Error);
        }

        public string Error
        {
            get
            {
                string foutmeldingen = "";

                foreach (var item in this.GetType().GetProperties())
                {
                    if (item.CanRead)
                    {
                        string fout = this[item.Name];

                        if (!string.IsNullOrWhiteSpace(fout))
                        {
                            foutmeldingen += fout + Environment.NewLine;
                        }
                    }
                }
                return foutmeldingen;
            }
        }
    }
}
