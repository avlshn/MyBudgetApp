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

        UpdateComboBox();
        Parrent = parrent;
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
            var tmp = new Spending { Name = sName, MoneyValue = sAmount, spendingCategory = selectedCategory };
            db.Spendings.Add(tmp);
            //MessageBox.Show(tmp.ToString());

            db.SaveChanges();
        }
        ((MainWindow)(this.Parrent)).DataGridUpdate();
        //this.Parrent.Show();
        Close();
        
    }

    private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                //MessageBox.Show(selectedCategory.ToString() + selectedCategory.CategoryId);
            }
        }
        else 
        {
            
        }
    }

    public void UpdateComboBox()
    {
        using (ApplicationContext db = new())
        {
            var categoriesList = db.Categories.ToList();
            categoriesList.Add(ADD_CATEGORY);
            CategoryBox.ItemsSource = categoriesList;
        }
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        this.Parrent.Show();
    }
}
