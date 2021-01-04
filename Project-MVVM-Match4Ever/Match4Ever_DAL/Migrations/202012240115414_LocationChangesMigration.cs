namespace Match4Ever_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationChangesMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("Match4Ever.Locaties", new[] { "Land" });
            DropColumn("Match4Ever.Locaties", "Land");
        }
        
        public override void Down()
        {
            AddColumn("Match4Ever.Locaties", "Land", c => c.String(nullable: false, maxLength: 26));
            CreateIndex("Match4Ever.Locaties", "Land", unique: true);
        }
    }
}
