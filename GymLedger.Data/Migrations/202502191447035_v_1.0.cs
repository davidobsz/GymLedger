namespace GymLedger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v_10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueId = c.String(),
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        DateAdded = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        UserRole = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        UniqueId = c.String(),
                        LastLogin = c.DateTime(),
                        DateAdded = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                        UniqueId = c.String(),
                        Date = c.DateTime(),
                        DateAdded = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Sets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueId = c.String(),
                        SetNumber = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Reps = c.Int(nullable: false),
                        SessionId = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sessions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sets", "SessionId", "dbo.Sessions");
            DropIndex("dbo.Sets", new[] { "SessionId" });
            DropIndex("dbo.Sessions", new[] { "UserId" });
            DropIndex("dbo.Exercises", new[] { "UserId" });
            DropTable("dbo.Sets");
            DropTable("dbo.Sessions");
            DropTable("dbo.Users");
            DropTable("dbo.Exercises");
        }
    }
}
