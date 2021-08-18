namespace E_Nursery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e_nursery9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NurseryInventories", "discount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NurseryInventories", "discount");
        }
    }
}
