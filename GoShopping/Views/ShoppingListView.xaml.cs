using System.Windows.Controls;
using GoShopping.ViewModels;

namespace GoShopping.Views
{
    /// <summary>
    /// Interaction logic for ShoppingListView.xaml
    /// </summary>
    public partial class ShoppingListView : UserControl
    {
        ShoppingListViewModel slViewModel=new ShoppingListViewModel();

        public ShoppingListView()
        {
            InitializeComponent();
            DataContext = slViewModel;
        }
    }
}
