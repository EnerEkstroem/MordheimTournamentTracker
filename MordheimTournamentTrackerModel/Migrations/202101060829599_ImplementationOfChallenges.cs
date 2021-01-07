namespace MordheimTournamentTrackerModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImplementationOfChallenges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Challenges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShowDown = c.DateTime(nullable: false),
                        Challengee_Id = c.Int(),
                        Challenger_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Armies", t => t.Challengee_Id)
                .ForeignKey("dbo.Armies", t => t.Challenger_Id)
                .Index(t => t.Challengee_Id)
                .Index(t => t.Challenger_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Challenges", "Challenger_Id", "dbo.Armies");
            DropForeignKey("dbo.Challenges", "Challengee_Id", "dbo.Armies");
            DropIndex("dbo.Challenges", new[] { "Challenger_Id" });
            DropIndex("dbo.Challenges", new[] { "Challengee_Id" });
            DropTable("dbo.Challenges");
        }
    }
}
