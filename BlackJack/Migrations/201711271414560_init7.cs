namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Decks", "IsDiscardedDeck", c => c.Boolean(nullable: false));
            DropColumn("dbo.Cards", "inDeck");
            DropColumn("dbo.Cards", "Discarded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "Discarded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cards", "inDeck", c => c.Boolean(nullable: false));
            DropColumn("dbo.Decks", "IsDiscardedDeck");
        }
    }
}
