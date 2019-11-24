using GoShopping.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoShopping.ViewModels
{
    class ShoppingListViewModel
    {
        private readonly GoShoppingDbContext _dbContext = new GoShoppingDbContext();

        public string Text { get; set; }

        public ShoppingListViewModel()
        {
            var ingredientToBuyList = CreateIngredientToBuyList(GetIngredientsOfSelectedDishes());
            Text = CreateTextToShow(ingredientToBuyList);
        }

        private string CreateTextToShow(List<IngredientToBuy> ingredientToBuyList)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var ingredientToBuy in ingredientToBuyList)
            {
                sb.Append(ingredientToBuy.Name);
                sb.Append("\t");
                if (ingredientToBuy.Name.Length <= 7) sb.Append("\t");
                sb.Append(ingredientToBuy.Quantity);
                sb.Append(ingredientToBuy.Unit);
                sb.Append("\n");
            }

            return sb.ToString();
        }

        private List<IngredientToBuy> CreateIngredientToBuyList(List<Recipe> ingredientsOfSelectedDishes)
        {
            var result = new List<IngredientToBuy>();

            foreach (var recipe in ingredientsOfSelectedDishes)
            {
                var ingredientToBuy = new IngredientToBuy
                {
                    Name = recipe.IngredientName,
                    Quantity = recipe.Quantity,
                    Unit = recipe.UnitName
                };

                if (!result.Any(x => x.Name.Contains(ingredientToBuy.Name) && x.Unit.Contains(ingredientToBuy.Unit)))
                {
                    result.Add(ingredientToBuy);
                }
                else
                {
                    result.Find(x => x.Name.Contains(ingredientToBuy.Name) && x.Unit == ingredientToBuy.Unit).Quantity
                        += ingredientToBuy.Quantity;
                }
            }

            return result;
        }

        private List<Recipe> GetIngredientsOfSelectedDishes()
        {
            var selectedDishes = (from object selectedDish in DishesListViewModel.SelectedDishes select selectedDish.ToString()).ToList();

            var query = (from i in _dbContext.Ingredients
                         join d in _dbContext.Dishes on i.Dish.DishId equals d.DishId
                         join u in _dbContext.Units on i.Unit.UnitId equals u.UnitId
                         where selectedDishes.Contains(d.Name)
                         select new Recipe
                         {
                             DishName = d.Name,
                             IngredientName = i.Name,
                             Quantity = i.Quantity,
                             UnitName = u.Name
                         }
                ).ToList();

            return query;
        }
    }
}
