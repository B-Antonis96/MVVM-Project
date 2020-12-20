using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.State.Authenticators
{
    public static class Authenticator //Static class om ingelogde gebruiker op te slaan
    {
        //het ingelogde account
        public static Account HuidigAccount { get; set; }

        //Gebruiker ingelogd controle
        public static bool IsIngelogd { get; set; }

        //Check admin status
        public static bool IsAdmin { get; set; }
    }
}
