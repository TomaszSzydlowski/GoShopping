using GoShopping.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace GoShopping.ViewModels
{
    public class DishesListViewModel
    {
        private GoShoppingDbContext _dbContext = new GoShoppingDbContext();

        public static IList SelectedDishes { get; set; }
        public static string SelectedItem { get; set; }

        public List<string> Dishes { get; set; }
        public static Dictionary<string, int> HowManyPortion { get; set; }

        public DishesListViewModel()
        {
            Dishes = GetDishList();
            HowManyPortion = InitHowManyPortion();
        }

        private Dictionary<string, int> InitHowManyPortion()
        {
            var result = new Dictionary<string, int>();
            Dishes.ForEach(d => result.Add(d, 0));

            return result;
        }

        private int GetDishIdFromDB()
        {
            return _dbContext.Dishes.FirstOrDefault(x => x.Name.Equals(SelectedItem)).DishId;
        }

        public void DeleteDish()
        {
            int dishId = GetDishIdFromDB();

            _dbContext.Dishes.Remove(_dbContext.Dishes.FirstOrDefault(x => x.DishId == dishId));
            _dbContext.Ingredients.RemoveRange(_dbContext.Ingredients.Where(x => x.Dish.DishId == dishId));
            _dbContext.SaveChanges();

            Dishes = GetDishList();
        }

        public List<string> GetDishList()
        {
            return _dbContext.Dishes.Select(x => x.Name).ToList();
        }

        public void UpdateHowManyPortion(TextBox textBox = null,int numberOfRow = -1, int valueOfRow = 0)
        {
            if (textBox!=null)
            {
                numberOfRow = short.Parse(Regex.Match(textBox.Name, @"\d+").Value);
                valueOfRow = short.Parse(Regex.Match(textBox.Text, @"\d+").Value);
            }

            if (numberOfRow==-1){return;}

            var dishName = Dishes[numberOfRow];
            HowManyPortion[dishName] = valueOfRow;
        }
    }
}
