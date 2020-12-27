using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.WPFTools
{
    public class Tools
    {
        //Kijken of veld "vol" is => kwestie van niet vaak !string.IsNullOrWhiteSpace() te moeten typen!
        public bool VeldVol(string veld) => (!string.IsNullOrWhiteSpace(veld));
    }
}
