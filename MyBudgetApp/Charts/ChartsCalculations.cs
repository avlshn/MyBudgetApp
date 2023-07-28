using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Charts;

static internal class ChartsCalculations
{
    internal static (List<double> spends, List<string> catNames, List<double> categoriesLimit) DonutGraphCalcs()
    {
        List<Double> spends = new List<Double>(),
            categoriesLimit = new List<double>();
        List<string> names = new List<string>();
        List<Category> categories = new List<Category>();

        using (ApplicationContext _context = new ApplicationContext())
        {
            _context.Spendings.LoadAsync();
            categories = _context.Categories.ToList();
            foreach (Category cat in categories)
            {
                decimal catSpending = 0;
                names.Add(cat.Name);
                categoriesLimit.Add(Convert.ToDouble(cat.CategoryLimit));
                foreach (Spending spend in cat.Spendings)
                {
                    catSpending += spend.MoneyValue;
                }
                spends.Add(Convert.ToDouble(catSpending));
            }

            return (spends, names, categoriesLimit);
        }
    }
}
