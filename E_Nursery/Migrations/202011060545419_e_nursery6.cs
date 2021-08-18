namespace E_Nursery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e_nursery6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Feedbacks", "Article");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "Article", c => c.String());
        }
    }
}
