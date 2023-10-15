using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Charts;
using MyBudgetApp.Commands;
using MyBudgetApp.GUI;
using MyBudgetApp.Other;
using MyBudgetApp.Properties;
using MyBudgetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static MyBudgetApp.Other.Constants;

namespace MyBudgetApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        #region Properties

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

        #region Properties Spendings Filter

        private string _spendingNameFilter;

        public string SpendingNameFilter
        {
            get { return _spendingNameFilter; }
            set
            {
                Set(ref _spendingNameFilter, value);
                SpendingsCollection.Filter = CollectionViewSource_Filter;
            }
        }

        private ICollectionView _spendingsCollection;

        public ICollectionView SpendingsCollection
        {
            get { return _spendingsCollection; }
            set { Set(ref _spendingsCollection, value); }
        }

        private DateTime? _filterDateFrom;

        public DateTime? FilterDateFrom
        {
            get { return _filterDateFrom; }
            set
            {
                Set(ref _filterDateFrom, value);
                SpendingsCollection.Filter = CollectionViewSource_Filter;
            }
        }

        private DateTime? _filterDateTo;

        public DateTime? FilterDateTo
        {
            get { return _filterDateTo; }
            set
            {
                Set(ref _filterDateTo, value);
                SpendingsCollection.Filter = CollectionViewSource_Filter;
            }
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

        #region Properties plots

        private BitmapImage _donutGraph;

        public BitmapImage DonutGraph
        {
            get { return _donutGraph; }
            set { Set(ref _donutGraph, value); }
        }

        private BitmapImage _stackedBarGraph;

        public BitmapImage StackedBarGraph
        {
            get { return _stackedBarGraph; }
            set { Set(ref _stackedBarGraph, value); }
        }

        public bool isShowZeroSpending
        {
            get => Settings.Default.ShowZeroSpendings;
            set
            {
                Settings.Default.ShowZeroSpendings = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _plotsDateFrom;

        public DateTime? PlotsDateFrom
        {
            get { return _plotsDateFrom; }
            set { Set(ref _plotsDateFrom, value); }
        }

        private DateTime? _plotsDateTo;

        public DateTime? PlotsDateTo
        {
            get { return _plotsDateTo; }
            set { Set(ref _plotsDateTo, value); }
        }



        #endregion

        #region Properties animation

        public bool isImageEnlarged { get; set; } = false;
        private BeforeAnimation donut;
        private BeforeAnimation stackedBar;
        private Duration animationDuration = new Duration(TimeSpan.FromSeconds(ANIMATION_DURATION));

        private GridLength _firstColomn;

        public GridLength FirstColomn
        {
            get { return _firstColomn; }
            set { Set(ref _firstColomn, value); }
        }

        private GridLength _secondColomn;

        public GridLength SecondColomn
        {
            get { return _secondColomn; }
            set { Set(ref _secondColomn, value); }
        }

        private GridLength _secondRow;

        public GridLength SecondRow
        {
            get { return _secondRow; }
            set { Set(ref _secondRow, value); }
        }

        private double _windowHeight;

        public double WindowHeight
        {
            get { return _windowHeight; }
            set { Set(ref _windowHeight, value); }
        }

        private double _windowWidth;

        public double WindowWidth
        {
            get { return _windowWidth; }
            set { Set(ref _windowWidth, value); }
        }


        #endregion

        #endregion

        #region Commands
        #region StackedBarClick

        public ICommand StackedBarClickCommand { get; }
        private bool CanStackedBarClickCommand(object o) => true;
        private void OnStackedBarClickCommand(object o)
        {
            Image StackedBar = o as Image;
            if (!isImageEnlarged)
            {
                stackedBar = new BeforeAnimation(
                            StackedBar.ActualHeight,
                            StackedBar.ActualWidth,
                            FirstColomn,
                            SecondRow);

                FirstColomn = new GridLength(0);
                SecondRow = new GridLength(0);
                DoubleAnimation heightAnimation = new DoubleAnimation(StackedBar.ActualHeight, WindowHeight, animationDuration, FillBehavior.HoldEnd);
                DoubleAnimation widthAnimation = new DoubleAnimation(StackedBar.ActualWidth, WindowWidth, animationDuration, FillBehavior.HoldEnd);
                StackedBar.BeginAnimation(FrameworkElement.HeightProperty, heightAnimation);
                StackedBar.BeginAnimation(FrameworkElement.WidthProperty, widthAnimation);
                isImageEnlarged = true;
            }
            else
            {
                DoubleAnimation heightAnimation = new DoubleAnimation(StackedBar.ActualHeight, stackedBar.imageHeight, animationDuration, FillBehavior.HoldEnd);
                DoubleAnimation widthAnimation = new DoubleAnimation(StackedBar.ActualWidth, stackedBar.imageWidth, animationDuration, FillBehavior.HoldEnd);

                heightAnimation.Completed += (s, e) =>
                                {
                                    isImageEnlarged = false;
                                    FirstColomn = new GridLength(FIRST_COLOMN_WIDTH_PERCENTAGE, GridUnitType.Star);
                                    SecondRow = new GridLength(SECOND_ROW_HEIGHT_PERCANTAGE, GridUnitType.Star);
                                    StackedBar.BeginAnimation(FrameworkElement.HeightProperty, null);
                                    StackedBar.BeginAnimation(FrameworkElement.WidthProperty, null);
                                };
                StackedBar.BeginAnimation(FrameworkElement.HeightProperty, heightAnimation);
                StackedBar.BeginAnimation(FrameworkElement.WidthProperty, widthAnimation);



            }
        }
        #endregion

        #region DonutGraphClick

        public ICommand DonutGraphClickCommand { get; }
        private bool CanDonutGraphClickCommand(object o) => true;
        private void OnDonutGraphClickCommand(object o)
        {
            Image Donut = o as Image;

            if (!isImageEnlarged)
            {
                donut = new BeforeAnimation(
                    Donut.ActualHeight,
                    Donut.ActualWidth,
                    FirstColomn,
                    SecondRow);
                SecondColomn = new GridLength(0);
                SecondRow = new GridLength(0);
                DoubleAnimation heightAnimation = new DoubleAnimation(Donut.ActualHeight, WindowHeight * 0.8, animationDuration, FillBehavior.HoldEnd);
                DoubleAnimation widthAnimation = new DoubleAnimation(Donut.ActualWidth, WindowWidth, animationDuration, FillBehavior.HoldEnd);
                Donut.BeginAnimation(FrameworkElement.HeightProperty, heightAnimation);
                Donut.BeginAnimation(FrameworkElement.WidthProperty, widthAnimation);
                isImageEnlarged = true;
            }
            else
            {
                DoubleAnimation heightAnimation = new DoubleAnimation(Donut.ActualHeight, donut.imageHeight, animationDuration, FillBehavior.HoldEnd);
                DoubleAnimation widthAnimation = new DoubleAnimation(Donut.ActualWidth, donut.imageWidth, animationDuration, FillBehavior.HoldEnd);
                heightAnimation.Completed += (s, e) =>
                {
                    isImageEnlarged = false;
                    SecondColomn = new GridLength(60, GridUnitType.Star);
                    SecondRow = new GridLength(SECOND_ROW_HEIGHT_PERCANTAGE, GridUnitType.Star);
                    Donut.BeginAnimation(FrameworkElement.HeightProperty, null);
                    Donut.BeginAnimation(FrameworkElement.WidthProperty, null);
                };
                Donut.BeginAnimation(FrameworkElement.HeightProperty, heightAnimation);
                Donut.BeginAnimation(FrameworkElement.WidthProperty, widthAnimation);
            }
        }

        #endregion

        #region AddCategoryWindowOk

        public ICommand AddCategoryWindowOkCommand { get; }

        private bool CanAddCategoryWindowOkCommand(object o)
        {
            if ((CategoryName != null && CategoryName.Length > 0) &&
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
            var result = MessageBox.Show($"Are you sure you want to delete \"{SelectedCategory?.Name}\"?",
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
        public ICommand CloseWindowCommand { get; }
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

        #region CopySpending

        public ICommand CopySpendingCommand { get; }
        private bool CanCopySpendingCommand(object o)
        {
            if (SelectedSpending != null) return true;
            return false;
        }

        private void OnCopySpendingCommand(object o)
        {
            IsCopySpendingMode = true;

            SpendingName = SelectedSpending.Name;
            SpendingValue = SelectedSpending.MoneyValue;
            SpendingCategory = SelectedSpending.spendingCategory;
            SpendingDate = SelectedSpending.EventDate;

            AddSpending addSpending = new AddSpending();
            addSpending.Owner = (Window)o;
            addSpending.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addSpending.ShowDialog();
        }

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
                    MoneyValue = SpendingValue ?? 0,
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

        #region OpenConnectionSettings
        public ICommand OpenConnectionSettingsCommand { get; }
        private bool CanOpenConnectionSettingsCommand(object o) => true;
        private void OnOpenConnectionSettingsCommand(object o)
        {
            ConnectionSettings connectionSettings = new ConnectionSettings();
            connectionSettings.Owner = (Window)o;
            connectionSettings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            connectionSettings.ShowDialog();
        }
        #endregion    

        #endregion

        public MainWindowViewModel()
        {
            _context.Categories.Load();
            _context.Spendings.Load();
            Spendings = _context.Spendings.Local.ToObservableCollection();
            Categories = _context.Categories.Local.ToObservableCollection();
            SpendingsCollection = CollectionViewSource.GetDefaultView(Spendings);

            SpendingsCollection.Filter = CollectionViewSource_Filter;

            PropertyChanged += ChartsDataRefresh;
            ChartsDataRefresh(null, null);

            #region Commands ctor
            AddCategoryWindowCommand = new RelayCommand(OnAddCategoryWindowCommand, CanAddCategoryWindowCommand);
            CloseWindowCommand = new RelayCommand(OnCloseWindowCommand, CanCloseWindowCommand);
            AddSpendingWindowOkCommand = new RelayCommand(OnAddSpendingWindowOkCommand, CanAddSpendingWindowOkCommand);
            CopySpendingCommand = new RelayCommand(OnCopySpendingCommand, CanCopySpendingCommand);
            SaveDbCommand = new RelayCommand(OnSaveDbCommand, CanSaveDbCommand);
            DeleteSpendingCommand = new RelayCommand(OnDeleteSpendingCommand, CanDeleteSpendingCommand);
            AddSpendingCommand = new RelayCommand(OnAddSpendingCommand, CanAddSpendingCommand);
            AddSpendingWindowCancelCommand = new RelayCommand(OnAddSpendingWindowCancelCommand, CanAddSpendingWindowCancelCommand);
            OpenCategoriewWindowCommand = new RelayCommand(OnOpenCategoriewWindowCommand, CanOpenCategoriewWindowCommand);
            AddCategoryWindowOkCommand = new RelayCommand(OnAddCategoryWindowOkCommand, CanAddCategoryWindowOkCommand);
            AddCategoryWindowCancelCommand = new RelayCommand(OnAddCategoryWindowCancelCommand, CanAddCategoryWindowCancelCommand);
            DeleteCategoryCommand = new RelayCommand(OnDeleteCategoryCommand, CanDeleteCategoryCommand);
            DonutGraphClickCommand = new RelayCommand(OnDonutGraphClickCommand, CanDonutGraphClickCommand);
            StackedBarClickCommand = new RelayCommand(OnStackedBarClickCommand, CanStackedBarClickCommand);
            OpenConnectionSettingsCommand = new RelayCommand(OnOpenConnectionSettingsCommand, CanOpenConnectionSettingsCommand);
            #endregion

            #region Colomns and rows

            FirstColomn = new(FIRST_COLOMN_WIDTH_PERCENTAGE, GridUnitType.Star);
            SecondColomn = new(60, GridUnitType.Star);
            SecondRow = new(SECOND_ROW_HEIGHT_PERCANTAGE, GridUnitType.Star);

            #endregion
        }

        #region Functions



        private bool CollectionViewSource_Filter(object obj)
        {
            DateTime? dateFrom = FilterDateFrom,
                dateTo = FilterDateTo;

            if (dateFrom == null) dateFrom = DateTime.MinValue;
            if (dateTo == null) dateTo = DateTime.MaxValue;

            Spending? s = obj as Spending;
            if (s != null)
            {
                if ((s.EventDate >= dateFrom) && (s.EventDate <= dateTo))
                {
                    if (SpendingNameFilter == null || s.Name.ToLower().Contains(SpendingNameFilter.ToLower()))
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            return false;
        }

        private void ChartsDataRefresh(object? sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName == "DonutGraph") return;
            if (e?.PropertyName == "StackedBarGraph") return;
            List<CategorySammary> CatList = ChartsCalculations.DonutGraphCalcs(PlotsDateFrom,
                                             PlotsDateTo,
                                             isShowZeroSpending);

            DonutGraph = ChartsDrawing.DonutPlot(CatList);
            StackedBarGraph = ChartsDrawing.StackedBarPlot(CatList);


        }
    }
    #endregion
}
