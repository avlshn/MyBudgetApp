using DB;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataGridUpdate();
        }

        private void OnClickAdd(object sender, RoutedEventArgs e)
        {
            AddSpending addSpending = new(this);
            addSpending.Show();
            Hide();
        }

        public void DataGridUpdate()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var SpendingsList = from s in db.Spendings
                                    join c in db.Categories
                                    on s.CategoryId equals c.CategoryId
                                    select new
                                    {
                                        Spending = s.Name,
                                        Value = s.MoneyValue,
                                        Category = c.Name
                                    };
                OupputGrid.ItemsSource = SpendingsList.ToList();
            }
        }

        private void OupputGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            
        }
    }
}
