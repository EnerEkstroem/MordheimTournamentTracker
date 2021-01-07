namespace MordheimTournamentTrackerModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Armies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Raiting = c.Int(nullable: false),
                        Game_Id = c.Int(),
                        Owner_Id = c.Int(),
                        Tournament_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .ForeignKey("dbo.ArmyTypes", t => t.Type_Id)
                .Index(t => t.Game_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Tournament_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArmyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Game_Id = c.Int(),
                        User_Id = c.Int(),
                        User_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .Index(t => t.Game_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Resolved = c.Boolean(nullable: false),
                        ArmyOne_Id = c.Int(),
                        ArmyTwo_Id = c.Int(),
                        Winner_Id = c.Int(),
                        Tournament_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Armies", t => t.ArmyOne_Id)
                .ForeignKey("dbo.Armies", t => t.ArmyTwo_Id)
                .ForeignKey("dbo.Armies", t => t.Winner_Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .Index(t => t.ArmyOne_Id)
                .Index(t => t.ArmyTwo_Id)
                .Index(t => t.Winner_Id)
                .Index(t => t.Tournament_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Armies", "Type_Id", "dbo.ArmyTypes");
            DropForeignKey("dbo.Tournaments", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Tournaments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Matches", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Matches", "Winner_Id", "dbo.Armies");
            DropForeignKey("dbo.Matches", "ArmyTwo_Id", "dbo.Armies");
            DropForeignKey("dbo.Matches", "ArmyOne_Id", "dbo.Armies");
            DropForeignKey("dbo.Tournaments", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Armies", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Armies", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Armies", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.ArmyTypes", "Game_Id", "dbo.Games");
            DropIndex("dbo.Matches", new[] { "Tournament_Id" });
            DropIndex("dbo.Matches", new[] { "Winner_Id" });
            DropIndex("dbo.Matches", new[] { "ArmyTwo_Id" });
            DropIndex("dbo.Matches", new[] { "ArmyOne_Id" });
            DropIndex("dbo.Tournaments", new[] { "User_Id1" });
            DropIndex("dbo.Tournaments", new[] { "User_Id" });
            DropIndex("dbo.Tournaments", new[] { "Game_Id" });
            DropIndex("dbo.ArmyTypes", new[] { "Game_Id" });
            DropIndex("dbo.Armies", new[] { "Type_Id" });
            DropIndex("dbo.Armies", new[] { "Tournament_Id" });
            DropIndex("dbo.Armies", new[] { "Owner_Id" });
            DropIndex("dbo.Armies", new[] { "Game_Id" });
            DropTable("dbo.Matches");
            DropTable("dbo.Tournaments");
            DropTable("dbo.Users");
            DropTable("dbo.ArmyTypes");
            DropTable("dbo.Games");
            DropTable("dbo.Armies");
        }
    }
}
