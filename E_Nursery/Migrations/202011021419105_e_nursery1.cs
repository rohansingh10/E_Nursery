namespace E_Nursery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e_nursery1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminAccounts",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        LoginID = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.NurseryAccounts",
                c => new
                    {
                        NurseryID = c.Int(nullable: false, identity: true),
                        NurseryName = c.String(),
                        MapLocation = c.String(nullable: false),
                        WorkingHour = c.String(),
                        LoginID = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        PasswordRecoveryQuestion = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Pincode = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.NurseryID);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        PasswordRecoveryQuestion = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Pincode = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAccounts");
            DropTable("dbo.NurseryAccounts");
            DropTable("dbo.AdminAccounts");
        }
    }
}
