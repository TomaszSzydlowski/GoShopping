using System.Windows;
using GoShopping.ViewModels;

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
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new DishesListViewModel();
        }
    }
}
