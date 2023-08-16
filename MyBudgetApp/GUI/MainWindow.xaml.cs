using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Charts;
using MyBudgetApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MyBudgetApp;

public record BeforeAnimation(double imageHeight, double imageWidth, GridLength colomnWidth, GridLength rowHeight);
/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const double ANIMATION_DURATION = 0.5;
    
    private readonly ApplicationContext _context = new();
    private CollectionViewSource spendingsViewSource;
    private Spending? SelectedSpending;
    private bool isShowZeroSpending,
                 isImageEnlarged = false;
    
    private BeforeAnimation stackedBar, donut;
    private Duration animationDuration = new Duration(TimeSpan.FromSeconds(ANIMATION_DURATION));


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
        var result = MessageBox.Show("Are you sure you want to clear base?", "Clear base", 
            MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
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
        var result = MessageBox.Show($"Are you sure you want to delete \"{((Spending)OutputGrid.SelectedItem).Name}\"?",
            "Delete spending?", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            _context.Spendings.Remove((Spending)OutputGrid.SelectedItem);
            RefreshData();
            _context.SaveChanges();
        }
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
        DataGridBox.ItemsSource = _context.Categories.ToList();


        List<double> numbers, categoriesLimit;
        List<string> labels;
        isShowZeroSpending = ShowZeroSpending.IsChecked == true;
        List<CategorySammary> CatList = ChartsCalculations.DonutGraphCalcs(DateFrom.SelectedDate,
                                                                           DateTo.SelectedDate,   
                                                                           isShowZeroSpending);

        
        DonutGraph.Source = ChartsDrawing.DonutPlot(CatList);
        StackedBarGraph.Source = ChartsDrawing.StackedBarPlot(CatList);

        spendingsViewSource = (CollectionViewSource)FindResource(nameof(spendingsViewSource));
    }

    private void StackedBarGraph_MouseLeftButtonUp(object sender, MouseEventArgs e)
    {

        if (!isImageEnlarged)
        {
            stackedBar = new BeforeAnimation(
                        StackedBarGraph.ActualHeight,
                        StackedBarGraph.ActualWidth,
                        FirstColomn.Width,
                        SecondRow.Height);

            FirstColomn.Width = new GridLength(0);
            SecondRow.Height = new GridLength(0);
            DoubleAnimation heightAnimation = new DoubleAnimation(StackedBarGraph.ActualHeight, StartupWindow.ActualHeight, animationDuration, FillBehavior.HoldEnd);
            DoubleAnimation widthAnimation = new DoubleAnimation(StackedBarGraph.ActualWidth, StartupWindow.ActualWidth, animationDuration, FillBehavior.HoldEnd);
            StackedBarGraph.BeginAnimation(HeightProperty, heightAnimation);
            StackedBarGraph.BeginAnimation(WidthProperty, widthAnimation);
            isImageEnlarged = true;
        }
        else
        {
            DoubleAnimation heightAnimation = new DoubleAnimation(StackedBarGraph.ActualHeight, stackedBar.imageHeight, animationDuration, FillBehavior.HoldEnd);
            DoubleAnimation widthAnimation = new DoubleAnimation(StackedBarGraph.ActualWidth, stackedBar.imageWidth, animationDuration, FillBehavior.HoldEnd);

            heightAnimation.Completed += (s, e) =>
            {
                isImageEnlarged = false;
                FirstColomn.Width = new GridLength(40, GridUnitType.Star);
                SecondRow.Height = new GridLength(50, GridUnitType.Star);
                StackedBarGraph.BeginAnimation(HeightProperty, null);
                StackedBarGraph.BeginAnimation(WidthProperty, null);
            };
            StackedBarGraph.BeginAnimation(HeightProperty, heightAnimation);
            StackedBarGraph.BeginAnimation(WidthProperty, widthAnimation);



        }
    }

    private void DonutGraph_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (!isImageEnlarged)
        {
            donut = new BeforeAnimation(
                DonutGraph.ActualHeight,
                DonutGraph.ActualWidth,
                FirstColomn.Width,
                SecondRow.Height);
            FirstColomn.Width = new GridLength(StartupWindow.ActualWidth);
            SecondRow.Height = new GridLength(0);
            DoubleAnimation heightAnimation = new DoubleAnimation(DonutGraph.ActualHeight, StartupWindow.ActualHeight*0.8, animationDuration, FillBehavior.HoldEnd);
            DoubleAnimation widthAnimation = new DoubleAnimation(DonutGraph.ActualWidth, StartupWindow.ActualWidth, animationDuration, FillBehavior.HoldEnd);
            DonutGraph.BeginAnimation(HeightProperty, heightAnimation);
            DonutGraph.BeginAnimation(WidthProperty, widthAnimation);
            isImageEnlarged = true;
        }
        else
        {
            DoubleAnimation heightAnimation = new DoubleAnimation(DonutGraph.ActualHeight, donut.imageHeight, animationDuration, FillBehavior.HoldEnd);
            DoubleAnimation widthAnimation = new DoubleAnimation(DonutGraph.ActualWidth, donut.imageWidth, animationDuration, FillBehavior.HoldEnd);
            heightAnimation.Completed += (s, e) =>
            {
                isImageEnlarged = false;
                FirstColomn.Width = new GridLength(40, GridUnitType.Star);
                SecondRow.Height = new GridLength(50, GridUnitType.Star);
                DonutGraph.BeginAnimation(HeightProperty, null);
                DonutGraph.BeginAnimation(WidthProperty, null);
            };
            DonutGraph.BeginAnimation(HeightProperty, heightAnimation);
            DonutGraph.BeginAnimation(WidthProperty, widthAnimation);
        }
    }


}
