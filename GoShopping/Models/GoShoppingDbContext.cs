using System.Data.Entity;

namespace GoShopping
{
    public class GoShoppingDbContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}