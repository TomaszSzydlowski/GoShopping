namespace GoShopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnPreparationTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dishes", "PreparationTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dishes", "PreparationTime");
        }
    }
}
