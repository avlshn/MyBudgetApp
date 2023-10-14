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

    //private bool isShowZeroSpending,
    //             isImageEnlarged = false,
    //             isPanelExtended = false;

    //private BeforeAnimation stackedBar, donut;

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

}
