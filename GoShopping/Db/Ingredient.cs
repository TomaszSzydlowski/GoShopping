using System;

namespace GoShopping
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public String Name { get; set; }
        public bool IsSpice { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
    }
}