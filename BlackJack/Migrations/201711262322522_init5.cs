namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "inDeck", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cards", "Discarded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "Discarded");
            DropColumn("dbo.Cards", "inDeck");
        }
    }
}
