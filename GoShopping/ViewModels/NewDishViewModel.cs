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

        private static short _size = 7; //Number of Ingredients on page

        private static List<Ingredient> Ingredients { get; set; }

        private static readonly GoShoppingDbContext _dbContext = new GoShoppingDbContext();

        public static List<string> Units { get; set; }

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
            var newDish = new Dish { Name = DishName };

            for (var i = 0; i < _size; i++)
            {
                if (!string.IsNullOrWhiteSpace(IngredientNames[i]) && IngredientQuantities[i] > 0 &&
                    !string.IsNullOrWhiteSpace(IngredientUnits[i]))
                {
                    var unitName = IngredientUnits[i];
                    var unit = _dbContext.Units.First(u => u.Name.Contains(unitName));

                    var ingredients = new Ingredient
                    {
                        Dish = newDish,
                        Name = IngredientNames[i],
                        Quantity = IngredientQuantities[i],
                        Unit = unit
                    };

                    _dbContext.Ingredients.Add(ingredients);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
