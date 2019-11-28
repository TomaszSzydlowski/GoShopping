using GoShopping.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GoShopping.Views
{
    /// <summary>
    /// Interaction logic for NewDishView.xaml
    /// </summary>
    public partial class NewDishView : UserControl
    {
        NewDishViewModel ndvm = new NewDishViewModel();

        List<Button> listButtons = new List<Button>();
        List<TextBox> listTextBoxes = new List<TextBox>();
        List<TextBlock> listTextBlockes = new List<TextBlock>();
        List<ComboBox> listComboBoxes = new List<ComboBox>();

        public NewDishView()
        {
            InitializeComponent();
            DataContext = ndvm;
            GetElementsFromView();
        }

        private void GetElementsFromView()
        {
            GetLogicalChildCollection(this, listButtons);
            GetLogicalChildCollection(this, listTextBoxes);
            GetLogicalChildCollection(this, listTextBlockes);
            GetLogicalChildCollection(this, listComboBoxes);
        }

        private void Unit_OnLostFocus(object sender, EventArgs e)
        {
            SetUnit(sender);
        }

        private static void SetUnit(object sender)
        {
            var name = ((ComboBox)sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            NewDishViewModel.IngredientUnits[number - 1] = ((ComboBox)sender).Text;
        }

        private void IngredientName_OnLostFocus(object sender, RoutedEventArgs e)
        {
            SetIngredientName(sender);
        }

        private static void SetIngredientName(object sender)
        {
            var name = ((TextBox)sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            NewDishViewModel.IngredientNames[number - 1] = ((TextBox)sender).Text;
        }

        private void IngredientQuantity_OnLostFocus(object sender, RoutedEventArgs e)
        {
            SetIngredientQuantity(sender);
        }

        private static void SetIngredientQuantity(object sender)
        {
            var name = ((TextBox)sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            if (Int32.TryParse(((TextBox)sender).Text, out var quantity))
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
            NewDishViewModel.DishName = ((TextBox)sender).Text;
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

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var name = ((Button)sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);

            List<FrameworkElement> elements = new List<FrameworkElement>();

            ((Button)sender).Visibility = Visibility.Hidden;
            if (number < listButtons.Count)
            {
                elements.Add(listButtons.First(x => x.Name.Equals($"AddBtn{number + 1}")));
            }

            elements.Add(listTextBlockes.First(x => x.Name.Equals($"TextBlockIngredientName{number + 1}")));
            elements.Add(listTextBoxes.First(x => x.Name.Equals($"IngredientName{number + 1}")));
            elements.Add(listTextBlockes.First(x => x.Name.Equals($"TextBlockIngredientQuantity{number + 1}")));
            elements.Add(listTextBoxes.First(x => x.Name.Equals($"IngredientQuantity{number + 1}")));
            elements.Add(listTextBlockes.First(x => x.Name.Equals($"TextBlockIngredientUnit{number + 1}")));
            elements.Add(listComboBoxes.First(x => x.Name.Equals($"Unit{number + 1}")));

            elements.ForEach(c => c.Visibility = Visibility.Visible);
        }

        private static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);
                    }
                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

        private void DishName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DishName.Text))
            {
                ChangeVisibility(Visibility.Visible);
            }
            else
            {
                ChangeVisibility(Visibility.Collapsed);
            }
        }

        private void ChangeVisibility(Visibility visibility)
        {
            TextBlockIngredientName1.Visibility = visibility;
            TextBlockIngredientQuantity1.Visibility = visibility;
            TextBlockIngredientUnit1.Visibility = visibility;
            IngredientName1.Visibility = visibility;
            IngredientQuantity1.Visibility = visibility;
            Unit1.Visibility = visibility;
            AddBtn1.Visibility = visibility;
        }
    }
}
