namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeckId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "Discarded_Id", c => c.Int());
            AddColumn("dbo.Games", "MyDeck_Id", c => c.Int());
            CreateIndex("dbo.Games", "Discarded_Id");
            CreateIndex("dbo.Games", "MyDeck_Id");
            AddForeignKey("dbo.Games", "Discarded_Id", "dbo.Decks", "Id");
            AddForeignKey("dbo.Games", "MyDeck_Id", "dbo.Decks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "MyDeck_Id", "dbo.Decks");
            DropForeignKey("dbo.Games", "Discarded_Id", "dbo.Decks");
            DropIndex("dbo.Games", new[] { "MyDeck_Id" });
            DropIndex("dbo.Games", new[] { "Discarded_Id" });
            DropColumn("dbo.Games", "MyDeck_Id");
            DropColumn("dbo.Games", "Discarded_Id");
            DropTable("dbo.Decks");
        }
    }
}
