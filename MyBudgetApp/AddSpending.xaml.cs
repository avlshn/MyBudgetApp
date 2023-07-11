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
using System.Windows.Shapes;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MyBudgetApp;

/// <summary>
/// Логика взаимодействия для AddSpending.xaml
/// </summary>
public partial class AddSpending : Window
{
    Category selectedCategory = new Category();
    private Window Parrent;
    private readonly Category ADD_CATEGORY = new Category() {Name = "Add Category"};
    public AddSpending(MainWindow parrent)
    {
        InitializeComponent();

        UpdateComboBox(null);
        Parrent = parrent;

        DateBox.SelectedDate = DateTime.Now;
    }

    private void OnClickOk(object sender, RoutedEventArgs e)
    {
        using (ApplicationContext db = new())
        {
            string sName = NameTextBox.Text;
            decimal sAmount;
            if (!Decimal.TryParse(AmountTextBox.Text, out sAmount))
            {
                sAmount = 0;
            }
            db.Attach(selectedCategory);
            var tmp = new Spending { Name = sName, MoneyValue = sAmount, spendingCategory = selectedCategory, EventDate = DateOnly.FromDateTime(DateBox.SelectedDate ?? DateTime.Now)  };
            db.Spendings.Add(tmp);
            db.SaveChanges();
        }
        ((MainWindow)(this.Parrent)).DataGridUpdate();
        Close();
        
    }

    public void UpdateComboBox(string? newValue)
    {
        using (ApplicationContext db = new())
        {
            var categoriesList = db.Categories.ToList();
            categoriesList.Add(ADD_CATEGORY);
            CategoryBox.ItemsSource = categoriesList;
            if (newValue != null)
            {
                foreach (Category cat in categoriesList)
                {
                    if (cat.Name == newValue)
                    {
                        CategoryBox.SelectedItem = cat;
                        selectedCategory = cat;
                        break;
                    }
                    
                }
            }
            else CategoryBox.SelectedItem = null;
        }
        
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        this.Parrent.Show();
    }

    private void CategoryBox_DropDownClosed(object sender, EventArgs e)
    {
        if (CategoryBox.SelectedItem is Category category)
        {
            if (category == ADD_CATEGORY)
            {
                AddCategory addCategory = new(this);
                addCategory.Show();
                Hide();
            }
            else
            {
                selectedCategory = category;
            }
        }
        else
        {

        }
    }
}
