using GoShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoShopping.ViewModels
{
    class NewDishViewModel
    {
        public static string DishName { get; set; }

        public static List<string> IngredientNames { get; set; }
        public static List<int> IngredientQuantities { get; set; }
        public static List<string> IngredientUnits { get; set; }

        private static readonly GoShoppingDbContext _dbContext = new GoShoppingDbContext();

        public static List<string> Units { get; set; }
        public List<string> DishNameExistingInDB { get; set; }

        public NewDishViewModel()
        {
            Units = GetUnits();
            DishNameExistingInDB = GetExistingDishNameFromDB();
            CreateArrays();
        }

        private List<string> GetExistingDishNameFromDB()
        {
            return _dbContext.Dishes.Select(x => x.Name.ToLower()).ToList();
        }

        private void CreateArrays()
        {
            IngredientNames = new List<string>();
            IngredientQuantities = new List<int>();
            IngredientUnits = new List<string>();
        }

        public List<string> GetUnits()
        {
            return _dbContext.Units.Select(x => x.Name).ToList();
        }

        public static void Save()
        {
            var newDish = new Dish { Name = DishName };
            var minListCount = new[] { IngredientNames.Count, IngredientQuantities.Count, IngredientUnits.Count }.Min();

            for (var i = 0; i < minListCount; i++)
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
