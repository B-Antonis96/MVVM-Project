namespace Match4Ever_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchUpdateMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Match4Ever.Matchen", "Account_AccountID", "Match4Ever.Accounts");
            DropIndex("Match4Ever.Matchen", new[] { "Account1ID" });
            DropIndex("Match4Ever.Matchen", new[] { "Account2ID" });
            DropIndex("Match4Ever.Matchen", new[] { "Account_AccountID" });
            DropColumn("Match4Ever.Matchen", "Account_AccountID");
        }
        
        public override void Down()
        {
            AddColumn("Match4Ever.Matchen", "Account_AccountID", c => c.Int());
            CreateIndex("Match4Ever.Matchen", "Account_AccountID");
            CreateIndex("Match4Ever.Matchen", "Account2ID", unique: true);
            CreateIndex("Match4Ever.Matchen", "Account1ID", unique: true);
            AddForeignKey("Match4Ever.Matchen", "Account_AccountID", "Match4Ever.Accounts", "AccountID");
        }
    }
}
