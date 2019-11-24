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
            var ingredientToBuyList = CreateIngredientToBuyDistinctList(GetIngredientsOfSelectedDishes());
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

        private List<IngredientToBuy> CreateIngredientToBuyDistinctList(List<IngredientToBuy> ingredientsOfSelectedDishes)
        {
            var result = new List<IngredientToBuy>();

            foreach (var ingredient in ingredientsOfSelectedDishes)
            {
                if (!result.Any(x => x.Name.Contains(ingredient.Name) && x.Unit.Contains(ingredient.Unit)))
                {
                    result.Add(ingredient);
                }
                else
                {
                    result.Find(x => x.Name.Contains(ingredient.Name) && x.Unit == ingredient.Unit).Quantity
                        += ingredient.Quantity;
                }
            }

            return result;
        }

        private List<IngredientToBuy> GetIngredientsOfSelectedDishes()
        {
            var selectedDishes = (from object selectedDish in DishesListViewModel.SelectedDishes select selectedDish.ToString()).ToList();

            var query = (from i in _dbContext.Ingredients
                         join d in _dbContext.Dishes on i.Dish.DishId equals d.DishId
                         join u in _dbContext.Units on i.Unit.UnitId equals u.UnitId
                         where selectedDishes.Contains(d.Name)
                         select new IngredientToBuy
                         {
                             DishName = d.Name,
                             Name = i.Name,
                             Quantity = i.Quantity,
                             Unit = u.Name
                         }
                ).ToList();

            return query;
        }
    }
}
