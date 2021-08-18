namespace E_Nursery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e_nursery3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NurseryAccounts", "WorkingHour", c => c.String(nullable: false));
            AlterColumn("dbo.NurseryAccounts", "Pincode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NurseryAccounts", "Pincode", c => c.String());
            AlterColumn("dbo.NurseryAccounts", "WorkingHour", c => c.String());
        }
    }
}
