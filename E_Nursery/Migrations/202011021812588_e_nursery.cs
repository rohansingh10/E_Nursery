namespace E_Nursery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e_nursery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NurseryInventories", "stock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NurseryInventories", "stock");
        }
    }
}
