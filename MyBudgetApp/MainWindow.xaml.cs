using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Charts;
using MyBudgetApp.Properties;
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

namespace MyBudgetApp;

//record class onScreen(int Id, string spending, decimal MoneyValue, string category, DateTime? date);
/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ApplicationContext _context = new();
    private CollectionViewSource spendingsViewSource;
    private Spending? SelectedSpending;
    private bool isShowZeroSpending;
    


    public MainWindow()
    {
        InitializeComponent();
        
        spendingsViewSource=(CollectionViewSource)FindResource(nameof(spendingsViewSource));

    }

    private void OnClickAdd(object sender, RoutedEventArgs e)
    {
        AddSpending addSpending = new AddSpending(this);
        addSpending.Owner = this;
        addSpending.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        addSpending.ShowDialog();
        
    }
    #region DataGridUpdate
    //public void DataGridUpdate()
    //{
    //    using (ApplicationContext db = new ApplicationContext())
    //    {
    //        var SpendingsList = from s in db.Spendings
    //                            join c in db.Categories
    //                            on s.CategoryId equals c.CategoryId
    //                            orderby s.EventDate
    //                            select new
    //                            {
    //                                Id = s.SpendingId,
    //                                Spending = s.Name,
    //                                Value = s.MoneyValue,
    //                                Category = c.Name,
    //                                Date = s.EventDate
    //                            };
    //        var outList = new List<onScreen>();
    //        foreach (var s in SpendingsList)
    //        {
    //            outList.Add(new onScreen(s.Id, s.Spending, s.Value, s.Category, s.Date ?? DateOnly.FromDateTime(DateTime.Now)));
    //        }
    //        OutputGrid.ItemsSource = outList;
    //    }
    //}
    #endregion

    private void Button_Click_Clear(object sender, RoutedEventArgs e)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //DataGridUpdate();
        }
    }

    private void Open_Test_Window(object sender, RoutedEventArgs e)
    {
        CategoriesWindow categoriesWindow = new CategoriesWindow();
        categoriesWindow.Owner = this;
        categoriesWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        categoriesWindow.ShowDialog();
    }

    private void StartupWindow_Loaded(object sender, RoutedEventArgs e)
    {
        RefreshData();
    }

    private void Button_Click_Delete(object sender, RoutedEventArgs e)
    {
        _context.Spendings.Remove((Spending)OutputGrid.SelectedItem);
        RefreshData();
        _context.SaveChanges();
    }

    private void DatePicker_DateChanged(object sender, RoutedEventArgs e)
    {
        //TODO: Fix null reference exception in LINQ when adding first spending in empty DB
        var tempSpending= (Spending)OutputGrid.SelectedItem;
        tempSpending = _context.Spendings.FirstOrDefault(p => p.SpendingId == tempSpending.SpendingId);
        tempSpending.EventDate = ((DatePicker)sender).SelectedDate ?? DateTime.Now;
    }

    private void StartupWindow_Closed(object sender, EventArgs e)
    {
        _context.SaveChanges();
        Settings.Default.Save();
    }

    private void Button_Click_Refresh(object sender, RoutedEventArgs e)
    {
        OutputGrid.CommitEdit();
        _context.SaveChanges();
        RefreshData();
    }

    public void RefreshData()
    {
        _context.Database.EnsureCreated();
        _context.Categories.Load();
        _context.Spendings.Load();
        spendingsViewSource.Source = _context.Spendings.Local.ToObservableCollection();
        DataGridBox.ItemsSource = _context.Categories.Local.ToObservableCollection();

        List<double> numbers, categoriesLimit;
        List<string> labels;
        isShowZeroSpending = ShowZeroSpending.IsChecked == true;
        List<CategorySammary> CatList = ChartsCalculations.DonutGraphCalcs(DateFrom.SelectedDate, DateTo.SelectedDate, isShowZeroSpending);

        
        DonutGraph.Source = ChartsDrawing.DonutPlot(CatList);
        StackedBarGraph.Source = ChartsDrawing.StackedBarPlot(CatList);

        spendingsViewSource = (CollectionViewSource)FindResource(nameof(spendingsViewSource));
    }

 
}
