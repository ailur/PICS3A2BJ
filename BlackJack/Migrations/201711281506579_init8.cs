namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init8 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Players", "GameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "GameId", c => c.Int(nullable: false));
        }
    }
}
