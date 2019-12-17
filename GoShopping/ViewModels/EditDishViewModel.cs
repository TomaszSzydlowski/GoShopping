using GoShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoShopping.ViewModels
{
    class EditDishViewModel
    {
        public static string DishName { get; set; }

        public static List<string> IngredientNames { get; set; }
        public static List<int> IngredientQuantities { get; set; }
        public static List<string> IngredientUnits { get; set; }

        private static readonly GoShoppingDbContext _dbContext = new GoShoppingDbContext();

        public static List<string> Units { get; set; }
        public List<string> DishesNameExistingInDB { get; set; }
        public static int DishIdExistingInDB { get; set; }

        public EditDishViewModel()
        {
            DishName = DishesListViewModel.SelectedItem;
            Units = GetUnits();
            DishesNameExistingInDB = GetExistingDishesNameFromDB();
            CreateArrays();
            GetDishWithIngredientsFromDB();
            DishIdExistingInDB = GetDishIdExistingInDB();

        }

        private static int GetDishIdExistingInDB() => _dbContext.Dishes.FirstOrDefault(x => x.Name.Equals(DishesListViewModel.SelectedItem)).DishId;

        private void GetDishWithIngredientsFromDB()
        {
            var query = (from d in _dbContext.Dishes
                         join i in _dbContext.Ingredients on d.DishId equals i.Dish.DishId
                         join u in _dbContext.Units on i.Unit.UnitId equals u.UnitId
                         where d.Name == DishesListViewModel.SelectedItem
                         select new
                         {
                             IngredientName = i.Name,
                             IngredientQuantity = i.Quantity,
                             IngredientUnit = u.Name,
                         }).ToList();

            IngredientNames = query.Select(q => q.IngredientName).ToList();
            IngredientQuantities = query.Select(q => q.IngredientQuantity).ToList();
            IngredientUnits = query.Select(q => q.IngredientUnit).ToList();
        }

        private List<string> GetExistingDishesNameFromDB()
        {
            return _dbContext.Dishes.Where(x => !x.Name.Contains(DishesListViewModel.SelectedItem)).Select(x => x.Name.ToLower()).ToList();
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

        public static void Update()
        {
            try
            {

                _dbContext.Dishes.Remove(_dbContext.Dishes.First(x => x.DishId == DishIdExistingInDB));
                _dbContext.Ingredients.RemoveRange(_dbContext.Ingredients.Where(x => x.Dish.DishId == DishIdExistingInDB));
                _dbContext.SaveChanges();

                var newDish = new Dish {Name = DishName};
                var minListCount =
                    new[] {IngredientNames.Count, IngredientQuantities.Count, IngredientUnits.Count}.Min();

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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
