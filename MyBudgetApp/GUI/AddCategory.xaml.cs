using DB.Entities;
using DB;
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
using System.Xml.Linq;

namespace MyBudgetApp;

/// <summary>
/// Логика взаимодействия для AddCategory.xaml
/// </summary>
public partial class AddCategory : Window
{
    //private Window Parrent;
    //private bool isCategoryAdded = false;
    public AddCategory()
    {
        InitializeComponent();
        //((AddSpending)Parrent).CategoryBox.SelectedItem = null;
    }

    //private void OnClickOk(object sender, RoutedEventArgs e)
    //{
    //    using (ApplicationContext db = new())
    //    {
    //        string sName = CatNameTextBox.Text;
    //        decimal sLimit = decimal.Parse(CatLimitTextBox.Text);
    //        var tmp = new Category { Name = sName, CategoryLimit = sLimit };
    //        db.Categories.Add(tmp);

    //        db.SaveChanges();
    //    }
    //    isCategoryAdded = true;
    //    Close();


    //}

    //private void Window_Closed(object sender, EventArgs e)
    //{
    //    if (Parrent is AddSpending addSpending)
    //    {
    //        //if (isCategoryAdded)
    //        //    addSpending.UpdateComboBox(CatNameTextBox.Text);
    //        //else
    //        //    addSpending.UpdateComboBox(null);
    //    }
    //    else if (Parrent is CategoriesWindow categoriesWindow)
    //    {
    //        //categoriesWindow.RefreshData();
    //    }
    //}

    //private void Button_Click_Cancel(object sender, RoutedEventArgs e)
    //{
    //    this.Close();
    //}
}
