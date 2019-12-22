using GoShopping.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GoShopping.Views
{
    /// <summary>
    /// Interaction logic for DishesListView.xaml
    /// </summary>
    public partial class DishesListView : UserControl
    {
        DishesListViewModel dlvm = new DishesListViewModel();
        List<string> _temporaryListOfSelectedDishes = new List<string>();
        private int currentClickedNumberOfRow;

        public DishesListView()
        {
            InitializeComponent();

            DataContext = dlvm.GetDishList();
            SetGrid();
        }

        private void SetGrid()
        {
            for (int i = 0; i < dlvm.Dishes.Count; i++)
            {
                NumericBoxGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(22.2) });
            }
            NumericBoxGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            NumericBoxGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(25) });
            NumericBoxGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            Grid.SetColumn(StackPanelListView, 1);
            Grid.SetRow(StackPanelListView, 0);
            Grid.SetRowSpan(StackPanelListView, dlvm.Dishes.Count + 1);
        }

        private void OnUncheckItem(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;

            DishesListViewModel.SelectedItem = _temporaryListOfSelectedDishes.Except(DishesListViewModel.SelectedDishes.Cast<string>().ToList()).FirstOrDefault();
            DishesListViewModel.SelectedDishes = listView.SelectedItems;

            _temporaryListOfSelectedDishes.Remove(DishesListViewModel.SelectedItem);

            currentClickedNumberOfRow = dlvm.Dishes.IndexOf(DishesListViewModel.SelectedItem);
            if (NumericBoxGrid.Children.OfType<TextBox>().Any(x => x.Name.Equals($"NumericBox{currentClickedNumberOfRow}"))){RemoveNumericBoxByRowNumber(currentClickedNumberOfRow);}
            dlvm.UpdateHowManyPortion(numberOfRow: currentClickedNumberOfRow);
        }

        private void RemoveNumericBoxByRowNumber(int numberOfRow)
        {
            NumericBoxGrid.Children.Remove(NumericBoxGrid.Children.OfType<TextBox>()
                .FirstOrDefault(x => x.Name.Equals($"NumericBox{numberOfRow}")));
        }

        private void checkedListView_Checked(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;

            DishesListViewModel.SelectedItem = listView.SelectedItems.Cast<string>().ToList().LastOrDefault();
            DishesListViewModel.SelectedDishes = listView.SelectedItems;

            _temporaryListOfSelectedDishes.Add(DishesListViewModel.SelectedItem);

            currentClickedNumberOfRow = dlvm.Dishes.IndexOf(DishesListViewModel.SelectedItem);
            var startValue = 1;
            if (!NumericBoxGrid.Children.OfType<TextBox>().Any(x => x.Name.Equals($"NumericBox{currentClickedNumberOfRow}"))) { CreateNumericBox(currentClickedNumberOfRow); }
            dlvm.UpdateHowManyPortion(numberOfRow: currentClickedNumberOfRow, valueOfRow: startValue);
        }

        private void CreateNumericBox(int numberOfRow)
        {
            TextBox txt = new TextBox();
            txt.Name = $"NumericBox{numberOfRow}";
            txt.Text = "1";
            txt.FontFamily = new FontFamily("Segoe UI Light");
            txt.FontSize = 16;
            txt.TextAlignment = TextAlignment.Center;
            txt.Margin = new Thickness(0, 1, 0, 1);
            txt.TextChanged += NumericBox_TextChanged;
            Grid.SetColumn(txt, 0);
            Grid.SetRow(txt, numberOfRow);
            NumericBoxGrid.Children.Add(txt);
        }

        private void NumericBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var valid = isNumberBoxValid(textBox);
            if (valid) { dlvm.UpdateHowManyPortion(textBox); }
            ShowTextBoxError(textBox, valid);
        }

        private bool isNumberBoxValid(TextBox textBox)
        {
            return textBox.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(textBox.Text);
        }

        private static void ShowTextBoxError(TextBox textBox, bool valid)
        {
            try
            {
                if (valid)
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

        private void EditMenu_Click(object sender, RoutedEventArgs e)
        {
            GoToEditDishView();
        }

        private static void GoToEditDishView()
        {
            Window parentWindow = Application.Current.MainWindow;
            if (parentWindow.GetType() == typeof(MainWindow))
            {
                (parentWindow as MainWindow).DataContext = new EditDishViewModel();
                (parentWindow as MainWindow).Go.Visibility = Visibility.Collapsed;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            dlvm.DeleteDish();
            RemoveNumericBoxByRowNumber(currentClickedNumberOfRow);
            NumericBoxGrid.RowDefinitions.RemoveAt(currentClickedNumberOfRow);
            DataContext = dlvm.GetDishList();
        }

        private void CheckedListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GoToEditDishView();
        }
    }
}
