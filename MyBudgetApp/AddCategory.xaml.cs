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

namespace MyBudgetApp
{
    /// <summary>
    /// Логика взаимодействия для AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        private Window Parrent;
        public AddCategory(Window parrent)
        {
            InitializeComponent();
            Parrent = parrent;
        }

        private void OnClickOk(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new())
            {
                string sName = CatNameTextBox.Text;
                var tmp = new Category { Name = sName};
                db.Categories.Add(tmp);
                //MessageBox.Show(tmp.ToString());

                db.SaveChanges();
            }
            this.Parrent.Show();
            ((AddSpending)(this.Parrent)).UpdateComboBox();
            Close();

        }
    }
}
