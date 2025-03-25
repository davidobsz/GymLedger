namespace GymLedger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v_11_FailedLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FailedLoginAttempts", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "LockedOutDate", c => c.DateTime());
            AddColumn("dbo.Users", "IsLockedOut", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsLockedOut");
            DropColumn("dbo.Users", "LockedOutDate");
            DropColumn("dbo.Users", "FailedLoginAttempts");
        }
    }
}
