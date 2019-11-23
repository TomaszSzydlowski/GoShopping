namespace GoShopping.ViewModels
{
    class ShoppingListViewModel
    {
        public string text { get; set; }

        public ShoppingListViewModel()
        {
            foreach (var dish in DishesListViewModel.SelectedDishes)
            {
                text += dish.ToString();
            }
        }
    }
}
