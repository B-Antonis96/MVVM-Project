namespace Match4Ever_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChangesNaamMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("Match4Ever.Accounts", "Naam");
        }
        
        public override void Down()
        {
            AddColumn("Match4Ever.Accounts", "Naam", c => c.String(maxLength: 26));
        }
    }
}
