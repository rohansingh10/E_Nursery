namespace E_Nursery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e_nursery8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ANotifications",
                c => new
                    {
                        NotiID = c.Int(nullable: false, identity: true),
                        details = c.String(),
                    })
                .PrimaryKey(t => t.NotiID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ANotifications");
        }
    }
}
