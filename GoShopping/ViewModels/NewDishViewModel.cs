using System.Collections.Generic;
using System.Linq;
using GoShopping.Models;

namespace GoShopping.ViewModels
{
    class NewDishViewModel
    {
        public static string DishName { get; set; }

        public static string[] IngredientNames { get; set; }
        public static int[] IngredientQuantities { get; set; }
        public static string[] IngredientUnits { get; set; }

        private short _size = 7; //Number of Ingredients on page

        private static List<Ingredient> Ingredients { get; set; }

        private readonly GoShoppingDbContext _dbContext = new GoShoppingDbContext();

        public List<string> Units { get; set; }

        public NewDishViewModel()
        {
            Units = GetUnits();
            CreateArrays();
        }

        private void CreateArrays()
        {
            IngredientNames = new string[_size];
            IngredientQuantities = new int[_size];
            IngredientUnits = new string[_size];
        }

        public List<string> GetUnits()
        {
            return _dbContext.Units.Select(x => x.Name).ToList();
        }

        public static void Save()
        {
            
        }
    }
}
