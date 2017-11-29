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
                        inDeck = c.String(),
                        CardScore = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        Suite = c.Int(nullable: false),
                        Player_PlayerId = c.Int(),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId)
                .Index(t => t.Player_PlayerId);
            
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        DeckId = c.Int(nullable: false, identity: true),
                        IsDiscardedDeck = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeckId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        DateStarted = c.DateTime(nullable: false),
                        Croupier_PlayerId = c.Int(),
                        Discarded_DeckId = c.Int(),
                        MyDeck_DeckId = c.Int(),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Players", t => t.Croupier_PlayerId)
                .ForeignKey("dbo.Decks", t => t.Discarded_DeckId)
                .ForeignKey("dbo.Decks", t => t.MyDeck_DeckId)
                .Index(t => t.Croupier_PlayerId)
                .Index(t => t.Discarded_DeckId)
                .Index(t => t.MyDeck_DeckId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsCroupier = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "MyDeck_DeckId", "dbo.Decks");
            DropForeignKey("dbo.Games", "Discarded_DeckId", "dbo.Decks");
            DropForeignKey("dbo.Games", "Croupier_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Cards", "Player_PlayerId", "dbo.Players");
            DropIndex("dbo.Games", new[] { "MyDeck_DeckId" });
            DropIndex("dbo.Games", new[] { "Discarded_DeckId" });
            DropIndex("dbo.Games", new[] { "Croupier_PlayerId" });
            DropIndex("dbo.Cards", new[] { "Player_PlayerId" });
            DropTable("dbo.Players");
            DropTable("dbo.Games");
            DropTable("dbo.Decks");
            DropTable("dbo.Cards");
        }
    }
}
