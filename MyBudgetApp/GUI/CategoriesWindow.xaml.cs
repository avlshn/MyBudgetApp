using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Charts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class CategoriesWindow : Window, INotifyPropertyChanged
    {
        private CollectionViewSource categoriesViewSource;

        public CollectionViewSource CategoriesViewSource
        {
            get { return categoriesViewSource; }
            set 
            { 
                categoriesViewSource = value;
                OnPropertyChanged();
            }
        }


        private readonly ApplicationContext _context = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CategoriesWindow()
        {
            InitializeComponent();

            CategoriesViewSource = (CollectionViewSource)FindResource(nameof(categoriesViewSource));
        }

        public void RefreshData()
        {
            _context.Database.EnsureCreated();
            _context.Categories.Load();
            _context.Spendings.Load();
            CategoriesViewSource.Source = _context.Categories.Local.ToObservableCollection();

            double[] numbers;
            string[] labels;

            CategoriesViewSource = (CollectionViewSource)FindResource(nameof(categoriesViewSource));
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
            }
            else MessageBox.Show("Select a category to remove");
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _context.SaveChanges();
            ((MainWindow)Owner).RefreshData();
            
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
