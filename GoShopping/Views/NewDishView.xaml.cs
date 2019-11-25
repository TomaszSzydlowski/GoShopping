using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using GoShopping.ViewModels;

namespace GoShopping.Views
{
    /// <summary>
    /// Interaction logic for NewDishView.xaml
    /// </summary>
    public partial class NewDishView : UserControl
    {
        NewDishViewModel ndvm = new NewDishViewModel();
        public NewDishView()
        {
            InitializeComponent();
            DataContext = ndvm;
        }

        private void Unit_OnDropDownClosed(object sender, EventArgs e)
        {
            var name = ((ComboBox)sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            NewDishViewModel.IngredientUnits[number - 1] = ((ComboBox) sender).Text;
        }

        private void IngredientName_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var name = ((TextBox)sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            NewDishViewModel.IngredientNames[number - 1] = ((TextBox)sender).Text;
        }

        private void IngredientQuantity_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var name = ((TextBox)sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            if (Int32.TryParse(((TextBox) sender).Text, out var quantity))
            {
                NewDishViewModel.IngredientQuantities[number - 1] = quantity;
            }
        }

        private void DishName_OnLostFocus(object sender, RoutedEventArgs e)
        {
            NewDishViewModel.DishName = ((TextBox) sender).Text;
        }
    }
}
