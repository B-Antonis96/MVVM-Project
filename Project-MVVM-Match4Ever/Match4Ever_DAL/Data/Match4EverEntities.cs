using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Match4Ever_DAL.Models;

namespace Match4Ever_DAL.Data
{
    public class Match4EverEntities : DbContext
    {
        public Match4EverEntities(): base ("Match4Ever")
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountVoorkeur> AccountVoorkeuren { get; set; }
        public DbSet<Locatie> Locaties { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Melding> Meldingen { get; set; }
        public DbSet<Voorkeur> Voorkeuren { get; set; }
        public DbSet<VoorkeurAntwoord> VoorkeurAntwoorden { get; set; }
    }
}
