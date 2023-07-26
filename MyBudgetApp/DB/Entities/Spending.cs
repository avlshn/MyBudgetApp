using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities;

public class Spending : BudgetChange
{
    public int SpendingId { get; set; }

    [ForeignKey(nameof(Category))]
    public int? CategoryId { get; set; }
    public virtual Category? spendingCategory { get; set; }

    //public override string ToString()
    //{
    //    return ("ID: " + SpendingId.ToString() + " Name: " + Name + " Value: " + MoneyValue.ToString() + " CategoryId: " + spendingCategory.CategoryId.ToString() + " CategoryName: " + spendingCategory.Name);
    //}

}
