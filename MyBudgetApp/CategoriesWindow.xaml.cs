using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Charts;
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
    /// Логика взаимодействия для CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        private CollectionViewSource categoriesViewSource;
        private readonly ApplicationContext _context = new();
        public CategoriesWindow()
        {
            InitializeComponent();

            categoriesViewSource = (CollectionViewSource)FindResource(nameof(categoriesViewSource));
        }

        public void RefreshData()
        {
            _context.Database.EnsureCreated();
            _context.Categories.Load();
            _context.Spendings.Load();
            categoriesViewSource.Source = _context.Categories.Local.ToObservableCollection();
            //DataGridBox.ItemsSource = _context.Categories.Local.ToObservableCollection();

            double[] numbers;
            string[] labels;

            categoriesViewSource = (CollectionViewSource)FindResource(nameof(categoriesViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AddCategory addCategory = new AddCategory(this);
            addCategory.Owner = this;
            addCategory.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addCategory.ShowDialog();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (CategoriesGrid.SelectedItem is Category category)
            {
                _context.Remove(category);
                RefreshData();
                _context.SaveChanges();
            }
            else MessageBox.Show("Select a category to remove");
            
        }
    }
}
