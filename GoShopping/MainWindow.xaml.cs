using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }
    }

    public class Dish
    {
        public int DishId { get; set; }
        public String Name { get; set; }
        public virtual IList<Ingredient> Ingredients { get; set; }
    }

    public class Ingredient
    {
        public int IngredientId { get; set; }
        public String Name { get; set; }
        public bool IsSpice { get; set; }
        public int Quantity { get; set; }

    }
}
