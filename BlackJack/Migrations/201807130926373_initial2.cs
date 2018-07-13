namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "GameId", "dbo.Games");
            DropIndex("dbo.Cards", new[] { "GameId" });
            AddColumn("dbo.Cards", "DeckId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cards", "DeckId");
            AddForeignKey("dbo.Cards", "DeckId", "dbo.Decks", "DeckId", cascadeDelete: true);
            DropColumn("dbo.Cards", "GameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "GameId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cards", "DeckId", "dbo.Decks");
            DropIndex("dbo.Cards", new[] { "DeckId" });
            DropColumn("dbo.Cards", "DeckId");
            CreateIndex("dbo.Cards", "GameId");
            AddForeignKey("dbo.Cards", "GameId", "dbo.Games", "GameId", cascadeDelete: true);
        }
    }
}
