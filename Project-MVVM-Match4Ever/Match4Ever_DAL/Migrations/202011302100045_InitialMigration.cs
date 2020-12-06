namespace Match4Ever_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Match4Ever.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        LocatieID = c.Int(nullable: false),
                        Gebruikersnaam = c.String(nullable: false, maxLength: 26),
                        Emailadres = c.String(nullable: false, maxLength: 255),
                        Wachtwoord = c.String(nullable: false, maxLength: 255),
                        ProfielfotoLink = c.String(maxLength: 255),
                        Voornaam = c.String(maxLength: 255),
                        Achternaam = c.String(maxLength: 255),
                        Geslacht = c.String(maxLength: 25),
                        Geaardheid = c.String(maxLength: 25),
                        Geboortedatum = c.DateTime(nullable: false),
                        LaatsteLogin = c.DateTime(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("Match4Ever.Locaties", t => t.LocatieID, cascadeDelete: true)
                .Index(t => t.LocatieID)
                .Index(t => t.Gebruikersnaam, unique: true)
                .Index(t => t.Emailadres, unique: true);
            
            CreateTable(
                "Match4Ever.AccountVoorkeuren",
                c => new
                    {
                        AccountVoorkeurID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        VoorkeurID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountVoorkeurID)
                .ForeignKey("Match4Ever.Accounts", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("Match4Ever.Voorkeuren", t => t.VoorkeurID, cascadeDelete: true)
                .Index(t => t.AccountID)
                .Index(t => t.VoorkeurID);
            
            CreateTable(
                "Match4Ever.Voorkeuren",
                c => new
                    {
                        VoorkeurID = c.Int(nullable: false, identity: true),
                        Vraag = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.VoorkeurID)
                .Index(t => t.Vraag, unique: true);
            
            CreateTable(
                "Match4Ever.VoorkeurAntwoorden",
                c => new
                    {
                        VoorkeurAntwoordID = c.Int(nullable: false, identity: true),
                        VoorkeurID = c.Int(nullable: false),
                        Antwoord = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VoorkeurAntwoordID)
                .ForeignKey("Match4Ever.Voorkeuren", t => t.VoorkeurID, cascadeDelete: true)
                .Index(t => t.VoorkeurID);
            
            CreateTable(
                "Match4Ever.Locaties",
                c => new
                    {
                        LocatieID = c.Int(nullable: false, identity: true),
                        Stad = c.String(nullable: false, maxLength: 15),
                        Postcode = c.String(nullable: false, maxLength: 15),
                        Land = c.String(nullable: false, maxLength: 26),
                    })
                .PrimaryKey(t => t.LocatieID)
                .Index(t => t.Postcode, unique: true)
                .Index(t => t.Land, unique: true);
            
            CreateTable(
                "Match4Ever.Matchen",
                c => new
                    {
                        MatchID = c.Int(nullable: false, identity: true),
                        Account1ID = c.Int(nullable: false),
                        Account2ID = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        MatchDatum = c.DateTime(nullable: false),
                        Type = c.Boolean(nullable: false),
                        Account1_AccountID = c.Int(),
                        Account2_AccountID = c.Int(),
                        Account_AccountID = c.Int(),
                    })
                .PrimaryKey(t => t.MatchID)
                .ForeignKey("Match4Ever.Accounts", t => t.Account1_AccountID)
                .ForeignKey("Match4Ever.Accounts", t => t.Account2_AccountID)
                .ForeignKey("Match4Ever.Accounts", t => t.Account_AccountID)
                .Index(t => t.Account1ID, unique: true)
                .Index(t => t.Account2ID, unique: true)
                .Index(t => t.Account1_AccountID)
                .Index(t => t.Account2_AccountID)
                .Index(t => t.Account_AccountID);
            
            CreateTable(
                "Match4Ever.Meldingen",
                c => new
                    {
                        MeldingID = c.Int(nullable: false, identity: true),
                        MatchID = c.Int(nullable: false),
                        Zin = c.String(maxLength: 255),
                        DatumMelding = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MeldingID)
                .ForeignKey("Match4Ever.Matchen", t => t.MatchID, cascadeDelete: true)
                .Index(t => t.MatchID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Match4Ever.Matchen", "Account_AccountID", "Match4Ever.Accounts");
            DropForeignKey("Match4Ever.Meldingen", "MatchID", "Match4Ever.Matchen");
            DropForeignKey("Match4Ever.Matchen", "Account2_AccountID", "Match4Ever.Accounts");
            DropForeignKey("Match4Ever.Matchen", "Account1_AccountID", "Match4Ever.Accounts");
            DropForeignKey("Match4Ever.Accounts", "LocatieID", "Match4Ever.Locaties");
            DropForeignKey("Match4Ever.AccountVoorkeuren", "VoorkeurID", "Match4Ever.Voorkeuren");
            DropForeignKey("Match4Ever.VoorkeurAntwoorden", "VoorkeurID", "Match4Ever.Voorkeuren");
            DropForeignKey("Match4Ever.AccountVoorkeuren", "AccountID", "Match4Ever.Accounts");
            DropIndex("Match4Ever.Meldingen", new[] { "MatchID" });
            DropIndex("Match4Ever.Matchen", new[] { "Account_AccountID" });
            DropIndex("Match4Ever.Matchen", new[] { "Account2_AccountID" });
            DropIndex("Match4Ever.Matchen", new[] { "Account1_AccountID" });
            DropIndex("Match4Ever.Matchen", new[] { "Account2ID" });
            DropIndex("Match4Ever.Matchen", new[] { "Account1ID" });
            DropIndex("Match4Ever.Locaties", new[] { "Land" });
            DropIndex("Match4Ever.Locaties", new[] { "Postcode" });
            DropIndex("Match4Ever.VoorkeurAntwoorden", new[] { "VoorkeurID" });
            DropIndex("Match4Ever.Voorkeuren", new[] { "Vraag" });
            DropIndex("Match4Ever.AccountVoorkeuren", new[] { "VoorkeurID" });
            DropIndex("Match4Ever.AccountVoorkeuren", new[] { "AccountID" });
            DropIndex("Match4Ever.Accounts", new[] { "Emailadres" });
            DropIndex("Match4Ever.Accounts", new[] { "Gebruikersnaam" });
            DropIndex("Match4Ever.Accounts", new[] { "LocatieID" });
            DropTable("Match4Ever.Meldingen");
            DropTable("Match4Ever.Matchen");
            DropTable("Match4Ever.Locaties");
            DropTable("Match4Ever.VoorkeurAntwoorden");
            DropTable("Match4Ever.Voorkeuren");
            DropTable("Match4Ever.AccountVoorkeuren");
            DropTable("Match4Ever.Accounts");
        }
    }
}
