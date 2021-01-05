namespace Match4Ever_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraveranderingenMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Match4Ever.Accounts", "Geboortedatum", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("Match4Ever.Accounts", "Geboortedatum", c => c.DateTime(nullable: false));
        }
    }
}
