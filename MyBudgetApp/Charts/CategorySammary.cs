using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Charts
{
    internal class CategorySammary
    {
        public CategorySammary(int id, string label, double limit, double value, double position = 0)
        {
            ID = id;
            Label = label;
            Limit = limit;
            Value = value;
            Position = position;
        }
        internal int ID { get; set; }
        internal string Label { get; set; }
        internal double Limit { get; set; }
        internal double Value { get; set; }
        internal double Position { get; set; }
        internal bool isOverspended
        {
            get 
            {
                if (Limit < Value) return true; 
                else return false;
            }
        }
    }
}
