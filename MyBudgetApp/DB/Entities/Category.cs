using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities;

public class Category
{
    public Category()
    {
        
    }
    public Category(string name)
    {
        Name = name;
    }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<Spending> Spendings { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
