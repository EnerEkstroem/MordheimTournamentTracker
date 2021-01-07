namespace MordheimTournamentTrackerModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingSuperfluousForeignKey : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ArmyTypes", new[] { "Game_Id1" });
            DropColumn("dbo.ArmyTypes", "Game_Id");
            RenameColumn(table: "dbo.ArmyTypes", name: "Game_Id1", newName: "Game_Id");
            AlterColumn("dbo.ArmyTypes", "Game_Id", c => c.Int());
            CreateIndex("dbo.ArmyTypes", "Game_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ArmyTypes", new[] { "Game_Id" });
            AlterColumn("dbo.ArmyTypes", "Game_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ArmyTypes", name: "Game_Id", newName: "Game_Id1");
            AddColumn("dbo.ArmyTypes", "Game_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ArmyTypes", "Game_Id1");
        }
    }
}
