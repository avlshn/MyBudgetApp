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
    internal static List<CategorySammary> DonutGraphCalcs(DateTime? dateFrom, DateTime? dateTo, bool isShowZeroSpending)
    {
        List<CategorySammary> CategoriesInfo = new List<CategorySammary>();

        List<Category> categories = new List<Category>();

        if (dateFrom == null) dateFrom = DateTime.MinValue;
        if (dateTo == null) dateTo = DateTime.MaxValue;

        using (ApplicationContext _context = new ApplicationContext())
        {
            _context.Spendings.Load();
            categories = _context.Categories.ToList();

            foreach (Category cat in categories)
            {
                decimal catSpending = 0;
                foreach (Spending spend in cat.Spendings)
                {
                    if (spend.EventDate>=dateFrom && spend.EventDate<=dateTo)
                        catSpending += spend.MoneyValue;
                }
                if (isShowZeroSpending || catSpending > 0) 
                    CategoriesInfo.Add(new CategorySammary(
                        cat.CategoryId,
                        cat.Name, 
                        Convert.ToDouble(cat.CategoryLimit), 
                        Convert.ToDouble(catSpending)
                        ));
            }

            return CategoriesInfo;
        }
    }
}
