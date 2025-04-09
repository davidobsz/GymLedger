namespace GymLedger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v_13_OneRepMaxes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OneRepMaxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                        UniqueId = c.String(),
                        Date = c.DateTime(),
                        Weight = c.Single(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OneRepMaxes", "UserId", "dbo.Users");
            DropIndex("dbo.OneRepMaxes", new[] { "UserId" });
            DropTable("dbo.OneRepMaxes");
        }
    }
}
