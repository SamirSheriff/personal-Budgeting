using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Budgeting
{
    public class Expense
    {
        public int id { get; private set; }
        public string category {  get; set; }
        public double amount { get; set; }
        public bool isRecurring {  get; set; }

        public Expense(int id, string category, double amount, bool isRecurring)
        {
            this.id = id;
            this.category = category;
            this.amount = amount;
            this.isRecurring = isRecurring;
        }
    }

    
    public class Income
    {
        public int id { get; private set; }
        public double amount { get; set; }
        public DateTime recieveDate { get; set; }
        public bool isNonRecurring { get; set; }

        public Income(int id, double amount, DateTime recieveDate, bool isNonRecurring)
        {
            this.id = id;
            this.amount = amount;
            this.recieveDate = recieveDate;
            this.isNonRecurring = isNonRecurring;
        }
    }

    public class Budget
    {
        public int id { get; private set; }
        public double limit { get; set; }
        public double amountSpent { get; set; }
        public string category { get; set; }

        public Budget(int id, double limit, double amountSpent, string category)
        {
            this.id = id;
            this.limit = limit;
            this.category = category;
            this.amountSpent = amountSpent;
        }
    }
}
