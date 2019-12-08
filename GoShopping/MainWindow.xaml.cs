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
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ShoppingListViewModel();
            Go.Visibility = Visibility.Collapsed;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new DishesListViewModel();
            Go.Visibility = Visibility.Visible;
        }

        private void NewDish_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new NewDishViewModel();
            Go.Visibility = Visibility.Collapsed;
        }
    }
}
