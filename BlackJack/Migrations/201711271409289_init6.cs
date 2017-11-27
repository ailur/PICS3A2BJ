namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "Discarded_Id", "dbo.Decks");
            DropForeignKey("dbo.Games", "MyDeck_Id", "dbo.Decks");
            RenameColumn(table: "dbo.Games", name: "Discarded_Id", newName: "Discarded_DeckId");
            RenameColumn(table: "dbo.Games", name: "MyDeck_Id", newName: "MyDeck_DeckId");
            RenameIndex(table: "dbo.Games", name: "IX_Discarded_Id", newName: "IX_Discarded_DeckId");
            RenameIndex(table: "dbo.Games", name: "IX_MyDeck_Id", newName: "IX_MyDeck_DeckId");
            DropPrimaryKey("dbo.Decks");
            AlterColumn("dbo.Decks", "DeckId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Decks", "DeckId");
            AddForeignKey("dbo.Games", "Discarded_DeckId", "dbo.Decks", "DeckId");
            AddForeignKey("dbo.Games", "MyDeck_DeckId", "dbo.Decks", "DeckId");
            DropColumn("dbo.Decks", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Decks", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Games", "MyDeck_DeckId", "dbo.Decks");
            DropForeignKey("dbo.Games", "Discarded_DeckId", "dbo.Decks");
            DropPrimaryKey("dbo.Decks");
            AlterColumn("dbo.Decks", "DeckId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Decks", "Id");
            RenameIndex(table: "dbo.Games", name: "IX_MyDeck_DeckId", newName: "IX_MyDeck_Id");
            RenameIndex(table: "dbo.Games", name: "IX_Discarded_DeckId", newName: "IX_Discarded_Id");
            RenameColumn(table: "dbo.Games", name: "MyDeck_DeckId", newName: "MyDeck_Id");
            RenameColumn(table: "dbo.Games", name: "Discarded_DeckId", newName: "Discarded_Id");
            AddForeignKey("dbo.Games", "MyDeck_Id", "dbo.Decks", "Id");
            AddForeignKey("dbo.Games", "Discarded_Id", "dbo.Decks", "Id");
        }
    }
}
