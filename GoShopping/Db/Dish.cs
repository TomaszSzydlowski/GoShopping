using System;
using System.Collections.Generic;

namespace GoShopping
{
    public class Dish
    {
        public int DishId { get; set; }
        public String Name { get; set; }
        public virtual IList<Ingredient> Ingredients { get; set; }
    }
}