namespace GoShopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDishAndIngredient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        DishId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DishId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsSpice = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Dish_DishId = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientId)
                .ForeignKey("dbo.Dishes", t => t.Dish_DishId)
                .Index(t => t.Dish_DishId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "Dish_DishId", "dbo.Dishes");
            DropIndex("dbo.Ingredients", new[] { "Dish_DishId" });
            DropTable("dbo.Ingredients");
            DropTable("dbo.Dishes");
        }
    }
}
