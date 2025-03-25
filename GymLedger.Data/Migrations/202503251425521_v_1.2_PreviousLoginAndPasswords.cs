namespace GymLedger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v_12_PreviousLoginAndPasswords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreviousLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueId = c.String(),
                        LoginDate = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PreviousPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        UserId = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PreviousPasswords", "UserId", "dbo.Users");
            DropForeignKey("dbo.PreviousLogins", "UserId", "dbo.Users");
            DropIndex("dbo.PreviousPasswords", new[] { "UserId" });
            DropIndex("dbo.PreviousLogins", new[] { "UserId" });
            DropTable("dbo.PreviousPasswords");
            DropTable("dbo.PreviousLogins");
        }
    }
}
