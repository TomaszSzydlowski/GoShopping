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
            if (Equals(Go.Content, "Go!"))
            {
                DataContext = new ShoppingListViewModel();
            }
            else if (Equals(Go.Content, "Save"))
            {
                DataContext=new DishesListViewModel();
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new DishesListViewModel();
            Go.Content = "Go!";
        }

        private void NewDish_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new NewDishViewModel();
            Go.Content = "Save";
        }
    }
}
