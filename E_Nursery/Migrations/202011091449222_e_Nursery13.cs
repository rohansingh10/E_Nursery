namespace E_Nursery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e_Nursery13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NurseryAccounts", "Password", c => c.String());
            AlterColumn("dbo.UserAccounts", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccounts", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.NurseryAccounts", "Password", c => c.String(nullable: false));
        }
    }
}
