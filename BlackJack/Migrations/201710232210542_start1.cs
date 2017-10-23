namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Hands", name: "HandId", newName: "IdHand");
            RenameColumn(table: "dbo.Cards", name: "Hand_HandId", newName: "Hand_IdHand");
            RenameIndex(table: "dbo.Hands", name: "IX_HandId", newName: "IX_IdHand");
            RenameIndex(table: "dbo.Cards", name: "IX_Hand_HandId", newName: "IX_Hand_IdHand");
            AddColumn("dbo.Players", "IdHand", c => c.Int(nullable: false));
            DropColumn("dbo.Players", "HandId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "HandId", c => c.Int(nullable: false));
            DropColumn("dbo.Players", "IdHand");
            RenameIndex(table: "dbo.Cards", name: "IX_Hand_IdHand", newName: "IX_Hand_HandId");
            RenameIndex(table: "dbo.Hands", name: "IX_IdHand", newName: "IX_HandId");
            RenameColumn(table: "dbo.Cards", name: "Hand_IdHand", newName: "Hand_HandId");
            RenameColumn(table: "dbo.Hands", name: "IdHand", newName: "HandId");
        }
    }
}
