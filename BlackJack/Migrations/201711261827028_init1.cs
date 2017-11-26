namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Croupier_PlayerId = c.Int(),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Players", t => t.Croupier_PlayerId)
                .Index(t => t.Croupier_PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsCroupier = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        CardScore = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        Suite = c.Int(nullable: false),
                        Player_PlayerId = c.Int(),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId)
                .Index(t => t.Player_PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Croupier_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Cards", "Player_PlayerId", "dbo.Players");
            DropIndex("dbo.Cards", new[] { "Player_PlayerId" });
            DropIndex("dbo.Games", new[] { "Croupier_PlayerId" });
            DropTable("dbo.Cards");
            DropTable("dbo.Players");
            DropTable("dbo.Games");
        }
    }
}
