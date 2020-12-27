namespace Match4Ever_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraMatchUpdateMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("Match4Ever.AccountVoorkeuren", "VoorkeurAntwoordID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Match4Ever.AccountVoorkeuren", "VoorkeurAntwoordID");
        }
    }
}
