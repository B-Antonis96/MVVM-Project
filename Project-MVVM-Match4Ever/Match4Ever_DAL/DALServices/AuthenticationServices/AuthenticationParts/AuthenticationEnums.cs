using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts
{
    public class AuthenticationEnums
    {
        public enum AuthentcatieResultaat
        {
            Gelukt,
            WachtwoordenNietHetZelfde,
            GebruikerBestaatNiet,
            EmailBestaatAl,
            GebruikersnaamBestaatAl
        }
    }
}
