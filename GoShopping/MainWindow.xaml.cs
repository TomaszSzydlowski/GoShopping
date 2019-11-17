using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;

namespace GoShopping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class Dish
    {
        public int DishId { get; set; }
        public String Name { get; set; }
        public virtual IList<Ingredient> Ingredients { get; set; }
    }

    public class Ingredient
    {
        public int IngredientId { get; set; }
        public String Name { get; set; }
        public bool IsSpice { get; set; }
        public int Quantity { get; set; }
    }

    public class GoShoppingDbContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }

}
