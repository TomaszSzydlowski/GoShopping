﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GoShopping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GoShoppingDbContext context = new GoShoppingDbContext();

        public MainWindow()
        {
            InitializeComponent();
            dynamicCheckBox();
        }

        private void dynamicCheckBox()
        {
            GoShoppingDbContext context = new GoShoppingDbContext();
            var itemList = context.Dishes.ToList();

            foreach (var item in itemList)
            {
                CheckBox chk=new CheckBox();
                chk.Content = item.Name.ToString();
                st1.Children.Add(chk);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //// Create demo: Create a User instance and save it to the database
            //Dish newDish = new Dish { Name = "Spagetti" };
            //context.Dishes.Add(newDish);
            //context.SaveChanges();

            //// Create demo: Create a Task instance and save it to the database
            //Ingredient newIngredient = new Ingredient()
            //{
            //    Name = "Makaron",
            //    Quantity = 200,
            //};
            //context.Ingredients.Add(newIngredient);
            //context.SaveChanges();

            //Unit newUnit = new Unit
            //{
            //    Name = "g"
            //};

            //context.Units.Add(newUnit);
            //context.SaveChanges();

            //// Association demo: Assign task to user
            //newIngredient.Dish = newDish;
            //newIngredient.Unit = newUnit;
            //context.SaveChanges();

            //// Read demo: find incomplete tasks assigned to user 'Anna'
            //var query = from d in context.Dishes
            //            where d.Name == "Spagetti"
            //            select d;


            //dataGrid1.ItemsSource = query.ToList();
        }
    }
}
