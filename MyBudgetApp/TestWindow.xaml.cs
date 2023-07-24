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

namespace MyBudgetApp;

/// <summary>
/// Логика взаимодействия для TestWindow.xaml
/// </summary>
public partial class TestWindow : Window
{
    private readonly ApplicationContext _context = new();
    private CollectionViewSource spendingsViewSource;
    private Spending? SelectedSpending;


    public TestWindow(Window parrent)
    {
        InitializeComponent();


        List<string> Categories = _context.Categories.Select(p => p.Name).ToList();

        double[] numbers = new double[] { 150, 130, 250};
        string[] labels = Categories.ToArray();

        DonutGraph.Source = ChartsDrawing.DonutPlot(numbers, labels);

        spendingsViewSource = (CollectionViewSource)FindResource(nameof(spendingsViewSource));
    }

    private void OnClickAdd(object sender, RoutedEventArgs e)
    {

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

    }

    private void StartupWindow_Loaded(object sender, RoutedEventArgs e)
    {
        RefreshData();
    }

    private void Button_Click_Delete(object sender, RoutedEventArgs e)
    {
        if ((Spending)OutputGrid.SelectedItem != null)
            _context.Spendings.Remove((Spending)OutputGrid.SelectedItem);
        else MessageBox.Show("No spending is selected");
        StartupWindow_Loaded(null, null);
        _context.SaveChanges();
    }

    private void DatePicker_DateChanged(object sender, RoutedEventArgs e)
    {
        var tempSpending = (Spending)OutputGrid.SelectedItem;
        tempSpending = _context.Spendings.FirstOrDefault(p => p.SpendingId == tempSpending.SpendingId);
        tempSpending.EventDate = ((DatePicker)sender).SelectedDate ?? DateTime.Now;
    }

    private void StartupWindow_Closed(object sender, EventArgs e)
    {
        //OutputGrid.CommitEdit();
        //OutputGrid.CommitEdit();
        _context.SaveChanges();
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
    }
}
