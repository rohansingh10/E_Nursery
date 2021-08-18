namespace E_Nursery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e_nursery2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NurseryInventories",
                c => new
                    {
                        InventoryID = c.Int(nullable: false, identity: true),
                        PlantName = c.String(),
                        Description = c.String(),
                        variety = c.String(),
                        origin = c.String(),
                        season = c.String(),
                        NurseryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryID)
                .ForeignKey("dbo.NurseryAccounts", t => t.NurseryID, cascadeDelete: true)
                .Index(t => t.NurseryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NurseryInventories", "NurseryID", "dbo.NurseryAccounts");
            DropIndex("dbo.NurseryInventories", new[] { "NurseryID" });
            DropTable("dbo.NurseryInventories");
        }
    }
}
