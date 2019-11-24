using System;
using System.Collections.Generic;

namespace GoShopping.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public String Name { get; set; }
        public virtual IList<Ingredient> Ingredients { get; set; }
        public int PreparationTime  { get; set; }
    }
}