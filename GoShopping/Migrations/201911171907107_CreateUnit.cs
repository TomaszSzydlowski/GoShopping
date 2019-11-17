namespace GoShopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUnit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UnitId);
            
            AddColumn("dbo.Ingredients", "Unit_UnitId", c => c.Int());
            CreateIndex("dbo.Ingredients", "Unit_UnitId");
            AddForeignKey("dbo.Ingredients", "Unit_UnitId", "dbo.Units", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.Ingredients", new[] { "Unit_UnitId" });
            DropColumn("dbo.Ingredients", "Unit_UnitId");
            DropTable("dbo.Units");
        }
    }
}
