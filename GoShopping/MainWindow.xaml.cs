using GoShopping.ViewModels;
using System.Windows;

namespace GoShopping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DishesListViewModel();
            SetWhichMainButtonShow();

        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ShoppingListViewModel();
            SetWhichMainButtonShow();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new DishesListViewModel();
            SetWhichMainButtonShow();
        }

        private void NewDish_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new NewDishViewModel();
            SetWhichMainButtonShow();
        }

        private void SaveNewDish_Click(object sender, RoutedEventArgs e)
        {
            NewDishViewModel.Save();
            DataContext = new DishesListViewModel();
            SetWhichMainButtonShow();
        }

        private void SetWhichMainButtonShow()
        {
            if (DataContext is DishesListViewModel)
            {
                SaveNewDish.Visibility = Visibility.Hidden;
                Go.Visibility = Visibility.Visible;
            }
            else if (DataContext is NewDishViewModel)
            {
                SaveNewDish.Visibility = Visibility.Visible;
                Go.Visibility = Visibility.Hidden;
            }
            else
            {
                SaveNewDish.Visibility = Visibility.Hidden;
                Go.Visibility = Visibility.Hidden;
            }
        }
    }
}
