namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "GameId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cards", "GameId");
            AddForeignKey("dbo.Cards", "GameId", "dbo.Games", "GameId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "GameId", "dbo.Games");
            DropIndex("dbo.Cards", new[] { "GameId" });
            DropColumn("dbo.Cards", "GameId");
        }
    }
}
