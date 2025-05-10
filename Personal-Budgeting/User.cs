using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Budgeting
{
    public class User
    {
        public int ID { get; private set; }
        public string userName { get; private set; }
        public string password { get; private set; }
        public string Email { get; private set; }
        public ExpenseManager expenseManager { get; private set; }
        public IncomeManager incomeManager { get; private set; }
        public BudgetManager budgetManager { get; private set; }
        public ReminderManager reminderManager {get; private set;}

        public User(int iD, string userName, string email, ExpenseManager expenseManager, IncomeManager incomeManager, BudgetManager budgetManager, ReminderManager reminderManager, string password)
        {
            ID = iD;
            this.userName = userName;
            Email = email;
            this.expenseManager = expenseManager;
            this.incomeManager = incomeManager;
            this.budgetManager = budgetManager;
            this.reminderManager = reminderManager;
            this.password = password;
            expenseManager.owner = this;
            budgetManager.owner = this;
            incomeManager.owner = this;
            reminderManager.owner = this;
        }
    }
}
