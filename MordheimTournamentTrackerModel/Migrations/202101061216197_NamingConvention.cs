namespace MordheimTournamentTrackerModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NamingConvention : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArmyTypes", "Name", c => c.String());
            AddColumn("dbo.Challenges", "Name", c => c.String());
            DropColumn("dbo.ArmyTypes", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArmyTypes", "Type", c => c.String());
            DropColumn("dbo.Challenges", "Name");
            DropColumn("dbo.ArmyTypes", "Name");
        }
    }
}
