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
            DishesListViewModel.SelectedItem = CheckedListView.SelectedItem.ToString();
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
            throw new System.NotImplementedException();
        }

        private void CheckedListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GoToEditDishView();
        }
    }
}
