﻿using Match4Ever_WPF.ViewModels.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.ViewModels.Admin
{
    public class AdminInstellingenViewModel : BasisViewModel
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter) { }
    }
}
