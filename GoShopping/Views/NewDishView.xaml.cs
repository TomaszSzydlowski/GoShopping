using GoShopping.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        static Dictionary<string, bool> validDictionary = new Dictionary<string, bool>();

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
            CheckValid(sender);
        }

        private static void SetUnit(object sender)
        {
            var senderName = ((ComboBox)sender).Name;
            var number = short.Parse(Regex.Match(senderName, @"\d+").Value);
            if (NewDishViewModel.IngredientUnits.Count < number) { NewDishViewModel.IngredientUnits.Add(String.Empty); }
            NewDishViewModel.IngredientUnits[number - 1] = ((ComboBox)sender).Text;
        }

        private void CheckValid(object sender)
        {
            if (sender is ComboBox comboBox)
            {
                if (validDictionary.ContainsKey(comboBox.Name))
                {
                    validDictionary[comboBox.Name] = !string.IsNullOrWhiteSpace(comboBox.Text);
                }
                else
                {
                    validDictionary.Add(comboBox.Name, !string.IsNullOrWhiteSpace(comboBox.Text));
                }
            }
            if (sender is TextBox textBox)
            {
                if (textBox.Name.Contains("DishName"))
                {
                    if (validDictionary.ContainsKey(textBox.Name))
                    {
                        validDictionary[textBox.Name] = !string.IsNullOrWhiteSpace(textBox.Text) && !ndvm.DishNameExistingInDB.Any(x => x.ToLower().Equals(((TextBox)sender).Text.ToLower()));
                    }
                    else
                    {
                        validDictionary.Add(textBox.Name, !string.IsNullOrWhiteSpace(textBox.Text) && !ndvm.DishNameExistingInDB.Any(x => x.ToLower().Equals(((TextBox)sender).Text.ToLower())));
                    }

                    var lastLineOfElements = GetLastLineOfElements();
                    SaveNewDishBtn.IsEnabled = isButtonAddEnable(lastLineOfElements) && !validDictionary.Values.Contains(false);
                    ShowDishNameErrorMessage(sender);
                    return;
                }
                if (textBox.Name.Contains("IngredientQuantity"))
                {
                    if (validDictionary.ContainsKey(textBox.Name))
                    {
                        validDictionary[textBox.Name] = textBox.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(textBox.Text);
                    }
                    else
                    {
                        validDictionary.Add(textBox.Name, textBox.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(textBox.Text));
                    }
                }
                if (textBox.Name.Contains("IngredientName"))
                {
                    if (validDictionary.ContainsKey(textBox.Name))
                    {
                        validDictionary[textBox.Name] = !string.IsNullOrWhiteSpace(textBox.Text);
                    }
                    else
                    {
                        validDictionary.Add(textBox.Name, !string.IsNullOrWhiteSpace(textBox.Text));
                    }
                }

                ShowTextBoxError(textBox);
            }

            var currentLineOfElements = GetCurrentLineOfElements(sender);
            var currentButtonAddInLineOfElements = GetCurrentButtonAddInLineOfElements(currentLineOfElements);
            if (!(currentButtonAddInLineOfElements is null))
            {
                currentButtonAddInLineOfElements.IsEnabled = isButtonAddEnable(currentLineOfElements);
            }
            SaveNewDishBtn.IsEnabled = isButtonAddEnable(currentLineOfElements) && validDictionary["DishName"];
        }

        private void ShowDishNameErrorMessage(object sender)
        {
            if (ndvm.DishNameExistingInDB.Any(x => x.ToLower().Equals(((TextBox)sender).Text.ToLower())))
            {
                ShowDishNameError(true, "Name existing!");
            }
            else if (string.IsNullOrWhiteSpace(DishName.Text))
            {
                ShowDishNameError(true, "Invalid name!");
            }
            else
            {
                ShowDishNameError(false);
            }
        }

        private int GetLastLineOfElements()
        {
            var arrayWithOutDishName = validDictionary.Keys.Where(s => !s.Contains("DishName")).ToArray();
            if (arrayWithOutDishName.Length == 0) return 1;
            short[] arrayOfRowsNumbers = arrayWithOutDishName.Select(s => short.Parse(Regex.Match(s, @"\d+").Value)).ToArray();
            return arrayOfRowsNumbers.Max();

        }

        private Button GetCurrentButtonAddInLineOfElements(int number)
        {
            return listButtons.FirstOrDefault(x => x.Name.Equals($"AddBtn{number}"));
        }

        private short GetCurrentLineOfElements(object sender)
        {
            var name = ((FrameworkElement)sender).Name;
            return short.Parse(Regex.Match(name, @"\d+").Value);
        }


        private bool isButtonAddEnable(object number)
        {
            var isValidIngredientName = validDictionary.ContainsKey($"IngredientName{number}") && validDictionary[$"IngredientName{number}"];
            var isValidIngredientQuantity = validDictionary.ContainsKey($"IngredientQuantity{number}") && validDictionary[$"IngredientQuantity{number}"];
            var isValidUnit = validDictionary.ContainsKey($"Unit{number}") && validDictionary[$"Unit{number}"];

            return isValidIngredientName && isValidIngredientQuantity && isValidUnit;
        }

        private static void ShowTextBoxError(TextBox textBox)
        {
            try
            {
                if (validDictionary[textBox.Name])
                {
                    textBox.Foreground = new SolidColorBrush(Colors.Black);
                    textBox.Background = new SolidColorBrush(Colors.White);
                }
                else
                {
                    textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x72, 0x1c, 0x24));
                    textBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF8, 0xD7, 0xDA));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void IngredientName_OnLostFocus(object sender, RoutedEventArgs e)
        {
            SetIngredientName(sender);
        }

        private static void SetIngredientName(object sender)
        {
            var name = ((TextBox)sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            if (NewDishViewModel.IngredientNames.Count < number) { NewDishViewModel.IngredientNames.Add(String.Empty); }
            NewDishViewModel.IngredientNames[number - 1] = ((TextBox)sender).Text.ToLower();
        }

        private void IngredientQuantity_OnLostFocus(object sender, RoutedEventArgs e)
        {
            SetIngredientQuantity(sender);
        }

        private static void SetIngredientQuantity(object sender)
        {
            var name = ((TextBox)sender).Name;
            var number = short.Parse(Regex.Match(name, @"\d+").Value);
            if (NewDishViewModel.IngredientQuantities.Count < number) { NewDishViewModel.IngredientQuantities.Add(0); }
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
            if (number < listButtons.Count(x => x.Name.Contains("AddBtn")))
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

        private void DishName_TextChanged(object sender, TextChangedEventArgs e)                //TODO
        {
            CheckValid(sender);
        }

        private void ShowDishNameError(bool isError, string message = null)
        {
            if (isError)
            {
                DishNameWarning.Text = message;
                DishNameWarning.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF8, 0xD7, 0xDA));
                DishNameWarning.OpacityMask = new SolidColorBrush(Color.FromArgb(0xFF, 0xF5, 0xC6, 0xCB));
            }
            else
            {
                DishNameWarning.Text = String.Empty;
                DishNameWarning.Background = new SolidColorBrush(Colors.White);
                DishNameWarning.OpacityMask = new SolidColorBrush(Colors.White);
            }


        }

        private void IngredientQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValid(sender);
        }

        private void SaveNewDish_Click(object sender, RoutedEventArgs e)
        {
            NewDishViewModel.Save();
        }

        private void IngredientName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValid(sender);
        }
    }
}
