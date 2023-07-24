using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities;

public class BudgetChange
{
    public string Name { get; set; }
    public decimal MoneyValue { get; set; }
    public DateTime EventDate { get; set; }

    [NotMapped]
    public String EventDateToString
    {
        get
        {
            return this.EventDate.ToString("d") ?? DateTime.Now.ToString("d");
        }

        set
        {
            DateTime tmp;
            DateTime.TryParse(value, out tmp);
            EventDate = tmp;
        }
    }
}
