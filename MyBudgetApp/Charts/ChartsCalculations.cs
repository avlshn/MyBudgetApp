using DB;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Charts;

static internal class ChartsCalculations
{
    internal static (double[] spends, string[] catNames) DonutGraphCalcs()
    {
        List<Double> spends = new List<Double>();
        List<string> names = new List<string>();
        using (ApplicationContext _context = new ApplicationContext())
        {
            foreach (Category cat in _context.Categories)
            {
                decimal catSpending = 0;
                names.Add(cat.Name);
                foreach (Spending spend in cat.Spendings)
                {
                    catSpending += spend.MoneyValue;
                }
                spends.Add(Convert.ToDouble(catSpending));
            }
        }
        
        return (spends.ToArray(), names.ToArray());
    }
}
