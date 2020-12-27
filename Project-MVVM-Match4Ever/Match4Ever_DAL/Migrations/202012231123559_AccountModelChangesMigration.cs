namespace Match4Ever_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountModelChangesMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Match4Ever.Accounts", "LocatieID", "Match4Ever.Locaties");
            DropIndex("Match4Ever.Accounts", new[] { "LocatieID" });
            AlterColumn("Match4Ever.Accounts", "LocatieID", c => c.Int());
            CreateIndex("Match4Ever.Accounts", "LocatieID");
            AddForeignKey("Match4Ever.Accounts", "LocatieID", "Match4Ever.Locaties", "LocatieID");
        }
        
        public override void Down()
        {
            DropForeignKey("Match4Ever.Accounts", "LocatieID", "Match4Ever.Locaties");
            DropIndex("Match4Ever.Accounts", new[] { "LocatieID" });
            AlterColumn("Match4Ever.Accounts", "LocatieID", c => c.Int(nullable: false));
            CreateIndex("Match4Ever.Accounts", "LocatieID");
            AddForeignKey("Match4Ever.Accounts", "LocatieID", "Match4Ever.Locaties", "LocatieID", cascadeDelete: true);
        }
    }
}
