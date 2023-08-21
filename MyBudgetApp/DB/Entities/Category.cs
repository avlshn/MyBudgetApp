using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities;

public class Category
{
    public Category()
    {
        Name = "Choose category name";
    }
    public Category(string name)
    {
        Name = name;
    }
    public int CategoryId { get; set; }
    public string Name { get; set; }

    public decimal? CategoryLimit { get; set; }

    public decimal? CategoryValue { get; set; }
    public virtual ICollection<Spending> Spendings { get; set; } = new ObservableCollection<Spending>();

    public override string ToString()
    {
        return Name;
    }
}
