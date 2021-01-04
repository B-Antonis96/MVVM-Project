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


        //Voorkeur navigatie
        public int Navigeer(bool switcher, int teller, int lengte)
        {
            //Kiezen volgende of vorige
            if (switcher)
            {
                //Volgende voorkeur
                if (teller < lengte)
                {
                    teller += 1;
                }
                else
                {
                    teller = 1;
                }
            }
            else
            {
                //Vorige voorkeur
                if (teller > 1)
                {
                    teller -= 1;
                }
                else
                {
                    teller = lengte;
                }
            }
            return teller;
        }
    }
}
