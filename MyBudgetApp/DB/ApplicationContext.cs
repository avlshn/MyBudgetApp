using DB.Entities;
using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB;

public class ApplicationContext : DbContext
{
    public DbSet<Spending> Spendings => Set<Spending>();
    public DbSet<Income> Incomes => Set<Income>();
    public DbSet<Category> Categories => Set<Category>();

    public ApplicationContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlite("Data Source=helloapp.db").UseLazyLoadingProxies();
        ServerTypeSettings.Connections[MyBudgetApp.Properties.Settings.Default.ConnectionServerType].CreateDB(optionsBuilder);
    }
}
