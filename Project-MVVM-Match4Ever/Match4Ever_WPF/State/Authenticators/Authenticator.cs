using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match4Ever_DAL.DALServices.AuthenticationServices;
using System.Collections.ObjectModel;

namespace Match4Ever_WPF.State.Authenticators
{
    public static class Authenticator //Static class om ingelogde gebruiker op te slaan => ook een "beetje" op het voorbeeld van YouTuber SingletonSean!
    {
        //Het ingelogde account
        public static Account HuidigAccount { get; set; }

        //De gelinkt AccountVoorkeuren
        public static List<AccountVoorkeur> AccountVoorkeuren { get; set; } = new List<AccountVoorkeur>();

        //De gelinkt AccountVoorkeuren
        public static List<Match> Matches { get; set; } = new List<Match>();

        //De gelinkt Locatie
        public static Locatie HuidigeLocatie { get; set; }

        //Gebruiker ingelogd controle
        public static bool IsIngelogd { get; set; }

        //Check admin status
        public static bool IsAdmin { get; set; }
    }
}
