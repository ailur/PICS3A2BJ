namespace BlackJack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        IdPlayer = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HandId = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.IdPlayer);
            
            CreateTable(
                "dbo.Hands",
                c => new
                    {
                        HandId = c.Int(nullable: false),
                        IdPlayer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HandId)
                .ForeignKey("dbo.Players", t => t.HandId)
                .Index(t => t.HandId);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hand_HandId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hands", t => t.Hand_HandId)
                .Index(t => t.Hand_HandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hands", "HandId", "dbo.Players");
            DropForeignKey("dbo.Cards", "Hand_HandId", "dbo.Hands");
            DropIndex("dbo.Cards", new[] { "Hand_HandId" });
            DropIndex("dbo.Hands", new[] { "HandId" });
            DropTable("dbo.Cards");
            DropTable("dbo.Hands");
            DropTable("dbo.Players");
        }
    }
}
