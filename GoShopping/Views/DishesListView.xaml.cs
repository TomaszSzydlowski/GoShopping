using GoShopping.Models;
using GoShopping.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GoShopping.Views
{
    /// <summary>
    /// Interaction logic for DishesListView.xaml
    /// </summary>
    public partial class DishesListView : UserControl
    {
        GoShoppingDbContext context = new GoShoppingDbContext();

        public DishesListView()
        {
            InitializeComponent();
            DataContext = context.Dishes.Select(x => x.Name).ToList();
        }

        private void OnUncheckItem(object sender, RoutedEventArgs e)
        {
            DishesListViewModel.SelectedDishes = CheckedListView.SelectedItems;
        }

        private void checkedListView_Checked(object sender, RoutedEventArgs e)
        {
            DishesListViewModel.SelectedDishes = CheckedListView.SelectedItems;
        }
    }
}
