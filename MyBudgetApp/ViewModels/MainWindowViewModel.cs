using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Commands;
using MyBudgetApp.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        #region Properties Add spending window

        public bool IsCopySpendingMode { get; set; }

        public string EditWindowTitle
        {
            get 
            {
                if (IsCopySpendingMode) return "Edit spending";
                else return "Add spending";
            }
        }


        private string? _spendingName;

        public string? SpendingName
        {
            get { return _spendingName; }
            set { Set(ref _spendingName, value); }
        }

        private decimal? _spendingValue;

        public decimal? SpendingValue
        {
            get { return _spendingValue; }
            set { Set(ref _spendingValue, value); }
        }

        private Category? _spendingCategory;

        public Category? SpendingCategory
        {
            get { return _spendingCategory; }
            set { Set(ref _spendingCategory, value); }
        }

        private DateTime? _spendingDate;

        public DateTime? SpendingDate
        {
            get { return _spendingDate; }
            set { Set(ref _spendingDate, value); }
        }



        #endregion

        #region Properties Add category window

        private string? _categoryName;

        public string? CategoryName
        {
            get { return _categoryName; }
            set { Set(ref _categoryName, value); }
        }

        private decimal? _categorySpendingLimit;

        public decimal? CategorySpendingLimit
        {
            get { return _categorySpendingLimit; }
            set { Set(ref _categorySpendingLimit, value); }
        }

        #endregion

        #region Properties Global

        private readonly ApplicationContext _context = new ApplicationContext();
        
        
        private ObservableCollection<Spending> _spendings;
        public ObservableCollection<Spending> Spendings
        {
            get { return _spendings; }
            set { Set(ref _spendings, value); }
        }

        
        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { Set(ref _categories, value); }
        }

        public string Title { get; set; } = "test";
        

        private Spending _selectedSpending;

        public Spending SelectedSpending
        {
            get { return _selectedSpending; }
            set { Set(ref _selectedSpending, value); }
        }

        private Category? _selectedCategory;

        public Category? SelectedCategory
        {
            get { return _selectedCategory; }
            set { Set(ref _selectedCategory, value); }
        }


        #endregion

        #region Commands

        #region AddCategoryWindowOk

        public ICommand AddCategoryWindowOkCommand { get; }

        private bool CanAddCategoryWindowOkCommand(object o)
        {
            if ((CategoryName != null && CategoryName.Length>0) && 
                (CategorySpendingLimit != null && CategorySpendingLimit >= 0)) return true;
            else return false;
        }

        private void OnAddCategoryWindowOkCommand(object o)
        {
            Categories.Add(new Category(CategoryName, CategorySpendingLimit));
            _context.SaveChanges();
            ((Window)o).Close();
            CategoryName = null;
            CategorySpendingLimit = null;
        }

        #endregion

        #region AddCategoryWindowCancel

        public ICommand AddCategoryWindowCancelCommand { get; }

        private bool CanAddCategoryWindowCancelCommand(object o) => true;

        private void OnAddCategoryWindowCancelCommand(object o)
        {
            ((Window)o).Close();
            CategoryName = null;
            CategorySpendingLimit = null;
        }
        #endregion

        #region DeleteCategory

        public ICommand DeleteCategoryCommand { get; }
        private bool CanDeleteCategoryCommand(object o)
        {
            if (SelectedCategory != null) return true;
            else return false;
        }

        private void OnDeleteCategoryCommand(object o)
        {
            var result = MessageBox.Show($"Are you sure you want to delete \"{SelectedCategory.Name}\"?",
            "Delete category?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Categories.Remove(SelectedCategory);
                _context.SaveChanges();
                SelectedCategory = null;
            }
        }
        #endregion

        #region AddCategoryWindow
        public ICommand AddCategoryWindowCommand { get; }
        private bool CanAddCategoryWindowCommand(object o) => true;
        private void OnAddCategoryWindowCommand(object o)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.Owner = (Window)o;
            addCategory.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addCategory.ShowDialog();
        }
        #endregion

        #region CloseWindow
        public ICommand CloseWindowCommand { get;}
        private bool CanCloseWindowCommand(object o) => true;
        private void OnCloseWindowCommand(object o) => ((Window)o).Close();
        #endregion

        #region SaveDbCommand
        public ICommand SaveDbCommand { get; }
        private bool CanSaveDbCommand(object o) => true;
        private void OnSaveDbCommand(object o) => _context.SaveChanges();

        #endregion

        #region OpenCategoriewWindow
        public ICommand OpenCategoriewWindowCommand { get; }
        private bool CanOpenCategoriewWindowCommand(object o) => true;
        private void OnOpenCategoriewWindowCommand(object o)
        {
            CategoriesWindow categoriesWindow = new CategoriesWindow();
            categoriesWindow.Owner = (Window)o;
            categoriesWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            categoriesWindow.ShowDialog();
        }
        #endregion

        #region AddSpendingWindowCancel
        public ICommand AddSpendingWindowCancelCommand { get; }
        private bool CanAddSpendingWindowCancelCommand(object o) => true;
        private void OnAddSpendingWindowCancelCommand(object o)
        {
            ((Window)o).Close();

            SpendingName = null;
            SpendingValue = null;
            SpendingCategory = null;
            SpendingDate = null;
        }
        #endregion

        #region CopySpendingCommand

        #endregion

        #region AddSpendingWindowOk
        public ICommand AddSpendingWindowOkCommand { get; }
        private bool CanAddSpendingWindowOkCommand(object o)
        {
            if (SpendingName == null || SpendingValue == null || SpendingCategory == null || SpendingDate == null) return false;
            else return true;
        }
        private void OnAddSpendingWindowOkCommand(object o)
        {
            Spendings.Add(new Spending
            {
                Name = SpendingName,
                MoneyValue = SpendingValue ?? 0 ,
                spendingCategory = SpendingCategory,
                EventDate = SpendingDate ?? DateTime.Now
            });
            _context.SaveChanges();

            ((Window)o).Close();

            SpendingName = null;
            SpendingValue = null;
            SpendingCategory = null;
            SpendingDate = null;

        }

        #endregion

        #region DeleteSpendingCommand

        public ICommand DeleteSpendingCommand { get; }
        private bool CanDeleteSpendingCommand(object o)
        {
            if (SelectedSpending != null) return true;
            else return false;
        }

        private void OnDeleteSpendingCommand(object o)
        {
            var result = MessageBox.Show($"Are you sure you want to delete \"{SelectedSpending.Name}\"?",
            "Delete spending?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Spendings.Remove(SelectedSpending);
                _context.SaveChanges();
                SelectedSpending = null;
            }
        }
        #endregion

        #region AddSpendingCommand
        public ICommand AddSpendingCommand { get; }
        private bool CanAddSpendingCommand(object o) => true;
        private void OnAddSpendingCommand(object o)
        {
            IsCopySpendingMode = false;
            AddSpending addSpending = new AddSpending();
            addSpending.Owner = (Window)o;
            addSpending.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addSpending.ShowDialog();
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            _context.Categories.Load();
            _context.Spendings.Load();
            Spendings = _context.Spendings.Local.ToObservableCollection();
            Categories = _context.Categories.Local.ToObservableCollection();

            #region Commands ctor
            AddCategoryWindowCommand = new RelayCommand(OnAddCategoryWindowCommand, CanAddCategoryWindowCommand);
            CloseWindowCommand = new RelayCommand(OnCloseWindowCommand, CanCloseWindowCommand);
            AddSpendingWindowOkCommand = new RelayCommand(OnAddSpendingWindowOkCommand, CanAddSpendingWindowOkCommand);
            SaveDbCommand = new RelayCommand(OnSaveDbCommand, CanSaveDbCommand);
            DeleteSpendingCommand = new RelayCommand(OnDeleteSpendingCommand, CanDeleteSpendingCommand);
            AddSpendingCommand = new RelayCommand(OnAddSpendingCommand, CanAddSpendingCommand);
            AddSpendingWindowCancelCommand = new RelayCommand(OnAddSpendingWindowCancelCommand, CanAddSpendingWindowCancelCommand);
            OpenCategoriewWindowCommand = new RelayCommand(OnOpenCategoriewWindowCommand, CanOpenCategoriewWindowCommand);
            AddCategoryWindowOkCommand = new RelayCommand(OnAddCategoryWindowOkCommand, CanAddCategoryWindowOkCommand);
            AddCategoryWindowCancelCommand = new RelayCommand(OnAddCategoryWindowCancelCommand, CanAddCategoryWindowCancelCommand);
            DeleteCategoryCommand = new RelayCommand(OnDeleteCategoryCommand, CanDeleteCategoryCommand);

            #endregion
        }



    }
}
