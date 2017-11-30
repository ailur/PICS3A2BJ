namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialb : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cards", "inDeck");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "inDeck", c => c.String());
        }
    }
}
