namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        CardScore = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        Suite = c.Int(nullable: false),
                        Player_PlayerId = c.Int(),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId)
                .Index(t => t.GameId)
                .Index(t => t.Player_PlayerId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        DateStarted = c.DateTime(nullable: false),
                        Discarded_DeckId = c.Int(),
                        MyDeck_DeckId = c.Int(),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Decks", t => t.Discarded_DeckId)
                .ForeignKey("dbo.Decks", t => t.MyDeck_DeckId)
                .Index(t => t.Discarded_DeckId)
                .Index(t => t.MyDeck_DeckId);
            
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        DeckId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.DeckId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GameId = c.Int(nullable: false),
                        IsCroupier = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "Player_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Players", "GameId", "dbo.Games");
            DropForeignKey("dbo.Cards", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "MyDeck_DeckId", "dbo.Decks");
            DropForeignKey("dbo.Games", "Discarded_DeckId", "dbo.Decks");
            DropIndex("dbo.Players", new[] { "GameId" });
            DropIndex("dbo.Games", new[] { "MyDeck_DeckId" });
            DropIndex("dbo.Games", new[] { "Discarded_DeckId" });
            DropIndex("dbo.Cards", new[] { "Player_PlayerId" });
            DropIndex("dbo.Cards", new[] { "GameId" });
            DropTable("dbo.Players");
            DropTable("dbo.Decks");
            DropTable("dbo.Games");
            DropTable("dbo.Cards");
        }
    }
}
