using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private void Unit_OnLostFocus(object sender, EventArgs e)
        {
            SetUnit(sender);
        }

        private static void SetUnit(object sender)
        {
            var name = ((ComboBox) sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            NewDishViewModel.IngredientUnits[number - 1] = ((ComboBox) sender).Text;
        }

        private void IngredientName_OnLostFocus(object sender, RoutedEventArgs e)
        {
            SetIngredientName(sender);
        }

        private static void SetIngredientName(object sender)
        {
            var name = ((TextBox) sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            NewDishViewModel.IngredientNames[number - 1] = ((TextBox) sender).Text;
        }

        private void IngredientQuantity_OnLostFocus(object sender, RoutedEventArgs e)
        {
            SetIngredientQuantity(sender);
        }

        private static void SetIngredientQuantity(object sender)
        {
            var name = ((TextBox) sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            if (Int32.TryParse(((TextBox) sender).Text, out var quantity))
            {
                NewDishViewModel.IngredientQuantities[number - 1] = quantity;
            }
        }

        private void DishName_OnLostFocus(object sender, RoutedEventArgs e)
        {
            SetDishName(sender);
        }

        private static void SetDishName(object sender)
        {
            NewDishViewModel.DishName = ((TextBox) sender).Text;
        }

        private void Unit_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SetUnit(sender);
        }

        private void DishName_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SetDishName(sender);
        }

        private void IngredientName_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SetIngredientName(sender);
        }

        private void IngredientQuantity_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SetIngredientQuantity(sender);
        }
    }
}
