namespace Match4Ever_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelsUpdateMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("Match4Ever.Accounts", new[] { "Emailadres" });
            DropIndex("Match4Ever.Locaties", new[] { "Postcode" });
            AddColumn("Match4Ever.Accounts", "Naam", c => c.String(maxLength: 26));
            AlterColumn("Match4Ever.Accounts", "Emailadres", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("Match4Ever.Accounts", "ProfielfotoLink", c => c.String(maxLength: 50));
            AlterColumn("Match4Ever.Accounts", "IsAdmin", c => c.Boolean());
            CreateIndex("Match4Ever.Accounts", "Emailadres", unique: true);
            DropColumn("Match4Ever.Accounts", "Voornaam");
            DropColumn("Match4Ever.Accounts", "Achternaam");
            DropColumn("Match4Ever.Accounts", "LaatsteLogin");
            DropColumn("Match4Ever.Locaties", "Postcode");
        }
        
        public override void Down()
        {
            AddColumn("Match4Ever.Locaties", "Postcode", c => c.String(nullable: false, maxLength: 15));
            AddColumn("Match4Ever.Accounts", "LaatsteLogin", c => c.DateTime(nullable: false));
            AddColumn("Match4Ever.Accounts", "Achternaam", c => c.String(maxLength: 255));
            AddColumn("Match4Ever.Accounts", "Voornaam", c => c.String(maxLength: 255));
            DropIndex("Match4Ever.Accounts", new[] { "Emailadres" });
            AlterColumn("Match4Ever.Accounts", "IsAdmin", c => c.Boolean(nullable: false));
            AlterColumn("Match4Ever.Accounts", "ProfielfotoLink", c => c.String(maxLength: 255));
            AlterColumn("Match4Ever.Accounts", "Emailadres", c => c.String(nullable: false, maxLength: 255));
            DropColumn("Match4Ever.Accounts", "Naam");
            CreateIndex("Match4Ever.Locaties", "Postcode", unique: true);
            CreateIndex("Match4Ever.Accounts", "Emailadres", unique: true);
        }
    }
}
