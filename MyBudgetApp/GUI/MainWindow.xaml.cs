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


/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    //private readonly ApplicationContext _context = new();
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







    //private void Button_Click_Clear_Date_Filter(object sender, RoutedEventArgs e)
    //{
    //    GridDateFrom.SelectedDate = null;
    //    GridDateTo.SelectedDate = null;
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
    //}


}
