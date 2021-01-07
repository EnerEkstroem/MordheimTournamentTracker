namespace MordheimTournamentTrackerModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyInArmyTypes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArmyTypes", "Game_Id", "dbo.Games");
            DropIndex("dbo.ArmyTypes", new[] { "Game_Id" });
            AddColumn("dbo.ArmyTypes", "Game_Id1", c => c.Int());
            AlterColumn("dbo.ArmyTypes", "Game_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ArmyTypes", "Game_Id1");
            AddForeignKey("dbo.ArmyTypes", "Game_Id1", "dbo.Games", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArmyTypes", "Game_Id1", "dbo.Games");
            DropIndex("dbo.ArmyTypes", new[] { "Game_Id1" });
            AlterColumn("dbo.ArmyTypes", "Game_Id", c => c.Int());
            DropColumn("dbo.ArmyTypes", "Game_Id1");
            CreateIndex("dbo.ArmyTypes", "Game_Id");
            AddForeignKey("dbo.ArmyTypes", "Game_Id", "dbo.Games", "Id");
        }
    }
}
