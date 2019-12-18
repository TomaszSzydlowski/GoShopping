using GoShopping.Models;
using GoShopping.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GoShopping.Views
{
    /// <summary>
    /// Interaction logic for DishesListView.xaml
    /// </summary>
    public partial class DishesListView : UserControl
    {
        GoShoppingDbContext _dbContext = new GoShoppingDbContext();
        List<string> dishes = new List<string>();
        List<string> _temporaryListOfSelectedDishes = new List<string>();

        public DishesListView()
        {
            InitializeComponent();
            dishes = _dbContext.Dishes.Select(x => x.Name).ToList();
            DataContext = dishes;
            SetGrid();
        }

        private void SetGrid()
        {
            for (int i = 0; i < dishes.Count; i++)
            {
                NumericBoxGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(22.2) });
            }
            NumericBoxGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            NumericBoxGrid.ColumnDefinitions.Add(new ColumnDefinition {  Width = new GridLength(25)});
            NumericBoxGrid.ColumnDefinitions.Add(new ColumnDefinition {  Width = GridLength.Auto });
            Grid.SetColumn(StackPanelListView, 1);
            Grid.SetRow(StackPanelListView, 0);
            Grid.SetRowSpan(StackPanelListView, dishes.Count+1);
        }

        private void OnUncheckItem(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;

            DishesListViewModel.SelectedItem = _temporaryListOfSelectedDishes.Except(DishesListViewModel.SelectedDishes.Cast<string>().ToList()).FirstOrDefault();
            DishesListViewModel.SelectedDishes = listView.SelectedItems;

            _temporaryListOfSelectedDishes.Remove(DishesListViewModel.SelectedItem);

            var numberOfRow = dishes.IndexOf(DishesListViewModel.SelectedItem);
            if (NumericBoxGrid.Children.OfType<TextBox>().Any(x => x.Name.Equals($"NumericBox{numberOfRow + 1}"))) { NumericBoxGrid.Children.Remove(NumericBoxGrid.Children.OfType<TextBox>().FirstOrDefault(x => x.Name.Equals($"NumericBox{numberOfRow + 1}"))); }
        }

        private void checkedListView_Checked(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;

            DishesListViewModel.SelectedItem = listView.SelectedItems.Cast<string>().ToList().LastOrDefault();
            DishesListViewModel.SelectedDishes = listView.SelectedItems;

            _temporaryListOfSelectedDishes.Add(DishesListViewModel.SelectedItem);

            var numberOfRow = dishes.IndexOf(DishesListViewModel.SelectedItem);
            if (!NumericBoxGrid.Children.OfType<TextBox>().Any(x => x.Name.Equals($"NumericBox{numberOfRow + 1}"))) { CreateNumericBox(numberOfRow); }
        }

        private void CreateNumericBox(int numberOfRow)
        {
            TextBox txt = new TextBox();
            txt.Name = $"NumericBox{numberOfRow + 1}";
            txt.Text = "1";
            txt.FontFamily = new FontFamily("Segoe UI Light");
            txt.FontSize = 16;
            txt.TextAlignment = TextAlignment.Center;
            txt.Margin = new Thickness(0, 1, 0, 1);
            Grid.SetColumn(txt, 0);
            Grid.SetRow(txt, numberOfRow);
            NumericBoxGrid.Children.Add(txt);
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
