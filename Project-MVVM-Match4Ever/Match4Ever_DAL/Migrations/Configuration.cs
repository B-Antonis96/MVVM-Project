namespace Match4Ever_DAL.Migrations
{
    using Match4Ever_DAL.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Match4EverEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Match4EverEntities context)
        {
            //Niets aan te passen hier!
        }
    }
}
