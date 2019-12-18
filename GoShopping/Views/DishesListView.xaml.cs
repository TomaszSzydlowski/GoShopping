using System.Collections.Generic;
using GoShopping.Models;
using GoShopping.ViewModels;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GoShopping.Views
{
    /// <summary>
    /// Interaction logic for DishesListView.xaml
    /// </summary>
    public partial class DishesListView : UserControl
    {
        GoShoppingDbContext _dbContext = new GoShoppingDbContext();
        List<string> _temporaryListOfSelectedDishes=new List<string>();

        public DishesListView()
        {
            InitializeComponent();
            DataContext = _dbContext.Dishes.Select(x => x.Name).ToList();
        }

        private void OnUncheckItem(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;

            DishesListViewModel.SelectedItem = _temporaryListOfSelectedDishes.Except(DishesListViewModel.SelectedDishes.Cast<string>().ToList()).FirstOrDefault();
            DishesListViewModel.SelectedDishes = listView.SelectedItems;

            _temporaryListOfSelectedDishes.Remove(DishesListViewModel.SelectedItem);
        }

        private void checkedListView_Checked(object sender, RoutedEventArgs e)
        {
            var listView = (ListView) sender;

            DishesListViewModel.SelectedItem = listView.SelectedItems.Cast<string>().ToList().LastOrDefault();
            DishesListViewModel.SelectedDishes = listView.SelectedItems;

            _temporaryListOfSelectedDishes.Add(DishesListViewModel.SelectedItem);
        }

        private void EditMenu_Click(object sender, RoutedEventArgs e)
        {
            GoToEditDishView();
        }

        private static void GoToEditDishView()
        {
            Window parentWindow = Application.Current.MainWindow;
            if (parentWindow.GetType() == typeof(MainWindow))
            {
                (parentWindow as MainWindow).DataContext = new EditDishViewModel();
                (parentWindow as MainWindow).Go.Visibility = Visibility.Collapsed;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int dishId = GetDishIdFromDB();

            _dbContext.Dishes.Remove(_dbContext.Dishes.FirstOrDefault(x => x.DishId == dishId));
            _dbContext.Ingredients.RemoveRange(_dbContext.Ingredients.Where(x => x.Dish.DishId == dishId));
            _dbContext.SaveChanges();

            DataContext = _dbContext.Dishes.Select(x => x.Name).ToList();
        }

        private int GetDishIdFromDB()
        {
            return _dbContext.Dishes.FirstOrDefault(x => x.Name.Equals(DishesListViewModel.SelectedItem)).DishId;
        }

        private void CheckedListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GoToEditDishView();
        }
    }
}
