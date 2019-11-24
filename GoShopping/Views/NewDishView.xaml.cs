using System.Windows.Controls;
using GoShopping.ViewModels;

namespace GoShopping.Views
{
    /// <summary>
    /// Interaction logic for NewDishView.xaml
    /// </summary>
    public partial class NewDishView : UserControl
    {
        public NewDishView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new DishesListViewModel();
        }
    }
}
