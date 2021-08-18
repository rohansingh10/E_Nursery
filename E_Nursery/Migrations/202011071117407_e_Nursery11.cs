namespace E_Nursery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e_Nursery11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NurseryAccounts", "MapLocation", c => c.String());
            AlterColumn("dbo.NurseryAccounts", "Pincode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NurseryAccounts", "Pincode", c => c.String(nullable: false));
            AlterColumn("dbo.NurseryAccounts", "MapLocation", c => c.String(nullable: false));
        }
    }
}
