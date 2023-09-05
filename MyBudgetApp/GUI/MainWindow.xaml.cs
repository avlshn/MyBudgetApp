using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Charts;
using MyBudgetApp.GUI;
using MyBudgetApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using static MyBudgetApp.Other.Constants;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MyBudgetApp.GUI;

//public record BeforeAnimation(double imageHeight, double imageWidth, GridLength colomnWidth, GridLength rowHeight);
/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    //private readonly ApplicationContext _context = new();
    //public CollectionViewSource spendingsViewSource;
    //private Spending? SelectedSpending;
    //private bool isShowZeroSpending,
    //             isImageEnlarged = false,
    //             isPanelExtended = false;

    //private BeforeAnimation stackedBar, donut;
    //private Duration animationDuration = new Duration(TimeSpan.FromSeconds(ANIMATION_DURATION));
    //double initialGridWidth;



    public MainWindow()
    {
        InitializeComponent();
        
        //spendingsViewSource=(CollectionViewSource)FindResource(nameof(spendingsViewSource));

    }

    //private void OnClickAdd(object sender, RoutedEventArgs e)
    //{
    //    AddSpending addSpending = new AddSpending();
    //    addSpending.Owner = this;
    //    addSpending.WindowStartupLocation = WindowStartupLocation.CenterOwner;
    //    addSpending.ShowDialog();

    //}


    //private void Button_Click_Clear(object sender, RoutedEventArgs e)
    //{
    //    var result = MessageBox.Show("Are you sure you want to clear base?", "Clear base", 
    //        MessageBoxButton.YesNo, MessageBoxImage.Question);

    //    if (result == MessageBoxResult.Yes)
    //    {
    //        using (ApplicationContext db = new ApplicationContext())
    //        {
    //            db.Database.EnsureDeleted();
    //            db.Database.EnsureCreated();
    //        }
    //    }
    //}

    //private void StartupWindow_Loaded(object sender, RoutedEventArgs e)
    //{

    //    //test
    //    _context.Database.EnsureCreated();
    //    _context.Categories.Load();
    //    _context.Spendings.Load();
    //    spendingsViewSource.Source = _context.Spendings.Local.ToObservableCollection();

    //    RefreshData();
    //    if (Settings.Default.GroupSpendings)
    //    {
    //        ICollectionView cvTasks = CollectionViewSource.GetDefaultView(OutputGrid.ItemsSource);
    //        if (cvTasks != null && cvTasks.CanGroup == true)
    //            spendingsViewSource.GroupDescriptions.Add(new PropertyGroupDescription("spendingCategory"));
    //    }
    //}

    //private void Button_Click_Delete(object sender, RoutedEventArgs e)
    //{
    //    var result = MessageBox.Show($"Are you sure you want to delete \"{((Spending)OutputGrid.SelectedItem).Name}\"?",
    //        "Delete spending?", MessageBoxButton.YesNo, MessageBoxImage.Question);

    //    if (result == MessageBoxResult.Yes)
    //    {
    //        _context.Spendings.Remove((Spending)OutputGrid.SelectedItem);
    //        RefreshData();
    //        _context.SaveChanges();
    //    }
    //}




    //public void RefreshData()
    //{
    //    _context.Categories.Load();
    //    _context.Spendings.Load();

    //    DataGridBox.ItemsSource = _context.Categories.ToList();

    //    List<double> numbers, categoriesLimit;
    //    List<string> labels;



    //}

    //private void CheckBox_GroupByCategory_Checked(object sender, RoutedEventArgs e)
    //{
    //    if (OutputGrid != null)
    //    {
    //        ICollectionView cvTasks = CollectionViewSource.GetDefaultView(OutputGrid.ItemsSource);
    //        if (cvTasks != null && cvTasks.CanGroup == true)
    //            spendingsViewSource.GroupDescriptions.Add(new PropertyGroupDescription("spendingCategory"));
    //    }
    //}

    //private void CheckBox_GroupByCategory_Unchecked(object sender, RoutedEventArgs e)
    //{
    //    ICollectionView cvTasks = CollectionViewSource.GetDefaultView(OutputGrid.ItemsSource);
    //    if (cvTasks != null) spendingsViewSource.GroupDescriptions.Clear();
    //}





    //private void SidePanel_MouseEnter(object sender, MouseEventArgs e)
    //{
    //    if (!isPanelExtended)
    //    {
    //        initialGridWidth = OutputGrid.ActualWidth;
    //        DoubleAnimation panelExtend = new(), dataGridExtend = new();

    //        panelExtend.To = SIDE_PANEL_WIDTH;
    //        panelExtend.Duration = animationDuration;
    //        dataGridExtend.From = OutputGrid.ActualWidth;
    //        dataGridExtend.To = OutputGrid.ActualWidth - SIDE_PANEL_WIDTH;
    //        dataGridExtend.Duration = animationDuration;

    //        SidePanel.BeginAnimation(WidthProperty, panelExtend);
    //        OutputGrid.BeginAnimation(WidthProperty, dataGridExtend);

    //        isPanelExtended = true;
    //    }   
    //}

    //private void FilterChanged(object sender, TextChangedEventArgs e)
    //{
    //    CollectionViewSource.GetDefaultView(OutputGrid.ItemsSource).Refresh();
    //}

    //private void FilterChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    //Calculating all spendings for selected period
    //    List<CategorySammary> CatList = ChartsCalculations.DonutGraphCalcs(GridDateFrom.SelectedDate, GridDateTo.SelectedDate, true);
    //    foreach (var c in CatList)
    //    {
    //        Category? cat = _context.Categories.SingleOrDefault(p => p.CategoryId == c.ID);
    //        if (cat != null)
    //        {
    //            cat.CategoryValue = (Decimal)c.Value;
    //        }
    //    }

    //    _context.SaveChanges();

    //    CollectionViewSource.GetDefaultView(OutputGrid.ItemsSource).Refresh();
    //}

    //private void Button_Click_Clear_Date_Filter(object sender, RoutedEventArgs e)
    //{
    //    GridDateFrom.SelectedDate = null;
    //    GridDateTo.SelectedDate = null;
    //}

    //private void SidePanel_MouseLeave(object sender, MouseEventArgs e)
    //{
    //    if (isPanelExtended)
    //    {
    //        Duration reverseDuration = new Duration(TimeSpan.FromSeconds(ANIMATION_DURATION*(initialGridWidth- OutputGrid.ActualWidth)/ SIDE_PANEL_WIDTH));
    //        DoubleAnimation panelExtend = new DoubleAnimation(SidePanel.ActualWidth, SIDE_PANEL_MINIMIZED_WIDTH, reverseDuration);
    //        DoubleAnimation dataGridExtend = new DoubleAnimation(OutputGrid.ActualWidth, initialGridWidth, reverseDuration);
    //        dataGridExtend.Completed += (s, e) =>
    //        {
    //            SidePanel.BeginAnimation(WidthProperty, null);
    //            OutputGrid.BeginAnimation(WidthProperty, null);
    //            //OutputGrid.Width = Double.NaN;
    //            isPanelExtended = false;
    //        };
    //        SidePanel.BeginAnimation(WidthProperty, panelExtend);
    //        OutputGrid.BeginAnimation(WidthProperty, dataGridExtend);

    //    }
    //}

    //private void StackedBarGraph_MouseLeftButtonUp(object sender, MouseEventArgs e)
    //{

    //    if (!isImageEnlarged)
    //    {
    //        stackedBar = new BeforeAnimation(
    //                    StackedBarGraph.ActualHeight,
    //                    StackedBarGraph.ActualWidth,
    //                    FirstColomn.Width,
    //                    SecondRow.Height);

    //        FirstColomn.Width = new GridLength(0);
    //        SecondRow.Height = new GridLength(0);
    //        DoubleAnimation heightAnimation = new DoubleAnimation(StackedBarGraph.ActualHeight, StartupWindow.ActualHeight, animationDuration, FillBehavior.HoldEnd);
    //        DoubleAnimation widthAnimation = new DoubleAnimation(StackedBarGraph.ActualWidth, StartupWindow.ActualWidth, animationDuration, FillBehavior.HoldEnd);
    //        StackedBarGraph.BeginAnimation(HeightProperty, heightAnimation);
    //        StackedBarGraph.BeginAnimation(WidthProperty, widthAnimation);
    //        isImageEnlarged = true;
    //    }
    //    else
    //    {
    //        DoubleAnimation heightAnimation = new DoubleAnimation(StackedBarGraph.ActualHeight, stackedBar.imageHeight, animationDuration, FillBehavior.HoldEnd);
    //        DoubleAnimation widthAnimation = new DoubleAnimation(StackedBarGraph.ActualWidth, stackedBar.imageWidth, animationDuration, FillBehavior.HoldEnd);

    //        heightAnimation.Completed += (s, e) =>
    //        {
    //            isImageEnlarged = false;
    //            FirstColomn.Width = new GridLength(FIRST_COLOMN_WIDTH_PERCENTAGE, GridUnitType.Star);
    //            SecondRow.Height = new GridLength(SECOND_ROW_HEIGHT_PERCANTAGE, GridUnitType.Star);
    //            StackedBarGraph.BeginAnimation(HeightProperty, null);
    //            StackedBarGraph.BeginAnimation(WidthProperty, null);
    //        };
    //        StackedBarGraph.BeginAnimation(HeightProperty, heightAnimation);
    //        StackedBarGraph.BeginAnimation(WidthProperty, widthAnimation);



    //    }
    //}

    //private void DonutGraph_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    //{
    //    if (!isImageEnlarged)
    //    {
    //        donut = new BeforeAnimation(
    //            DonutGraph.ActualHeight,
    //            DonutGraph.ActualWidth,
    //            FirstColomn.Width,
    //            SecondRow.Height);
    //        FirstColomn.Width = new GridLength(StartupWindow.ActualWidth);
    //        SecondRow.Height = new GridLength(0);
    //        DoubleAnimation heightAnimation = new DoubleAnimation(DonutGraph.ActualHeight, StartupWindow.ActualHeight * 0.8, animationDuration, FillBehavior.HoldEnd);
    //        DoubleAnimation widthAnimation = new DoubleAnimation(DonutGraph.ActualWidth, StartupWindow.ActualWidth, animationDuration, FillBehavior.HoldEnd);
    //        DonutGraph.BeginAnimation(HeightProperty, heightAnimation);
    //        DonutGraph.BeginAnimation(WidthProperty, widthAnimation);
    //        isImageEnlarged = true;
    //    }
    //    else
    //    {
    //        DoubleAnimation heightAnimation = new DoubleAnimation(DonutGraph.ActualHeight, donut.imageHeight, animationDuration, FillBehavior.HoldEnd);
    //        DoubleAnimation widthAnimation = new DoubleAnimation(DonutGraph.ActualWidth, donut.imageWidth, animationDuration, FillBehavior.HoldEnd);
    //        heightAnimation.Completed += (s, e) =>
    //        {
    //            isImageEnlarged = false;
    //            FirstColomn.Width = new GridLength(FIRST_COLOMN_WIDTH_PERCENTAGE, GridUnitType.Star);
    //            SecondRow.Height = new GridLength(SECOND_ROW_HEIGHT_PERCANTAGE, GridUnitType.Star);
    //            DonutGraph.BeginAnimation(HeightProperty, null);
    //            DonutGraph.BeginAnimation(WidthProperty, null);
    //        };
    //        DonutGraph.BeginAnimation(HeightProperty, heightAnimation);
    //        DonutGraph.BeginAnimation(WidthProperty, widthAnimation);
    //    }
    //    }


}
