using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Personal_Budgeting
{
    public interface IManager
    {
        public User owner { get; set; }
        public void deleteRecord(int id);
    }

    public class BudgetManager : IManager
    {
        private readonly Database db;
        public User owner { get; set; }
        public List<Budget> budgets { get; private set; }

        public BudgetManager(List<Budget> budgets)
        {
            db = new Database("Data Source=localhost;Initial Catalog=PersonalBudget;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            this.budgets = budgets;
        }

        public void deleteRecord(int id)
        {
            foreach (Budget budget in budgets)
            {
                if (budget.id == id)
                {
                    budgets.Remove(budget);
                }
            }

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                string sql = "DELETE FROM Budget" +
                             "WHERE budgetID = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }

            }
        }

        public void addRecord(double limit, double amountSpent, double category)
        {
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                string sql = "INSERT INTO Budget(userID, Limit, amountSpent, Category)" +
                             "VALUES (@userid, @limit, @amountspent, @category)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("category", category);
                    command.Parameters.AddWithValue("userid", owner.ID);
                    command.Parameters.AddWithValue("amountspent", amountSpent);
                    command.Parameters.AddWithValue("limit", limit);

                    command.ExecuteNonQuery();
                }

                budgets.Clear();
                string sqlBudget = "SELECT * FROM Budget WHERE userID = @id";
                using (SqlCommand command = new SqlCommand(sqlBudget, connection))
                {
                    command.Parameters.AddWithValue("id", owner.ID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int budgetID = reader.GetInt32(0);
                            double newlimit = reader.GetDouble(2);
                            double newamountSpent = reader.GetDouble(3);
                            string newcategory = reader.GetString(4);

                            budgets.Add(new Budget(budgetID, newlimit, newamountSpent, newcategory));
                        }
                    }


                }

            }
        }

        public void editRecord(int budgetid, double limit, double amountspent, string category)
        {
            foreach (Budget budget in budgets)
            {
                if (budget.id == budgetid)
                {
                    budget.amountSpent = amountspent;
                    budget.limit = limit;
                    budget.category = category;
                }
            }

            using (SqlConnection connection = db.GetConnection())
            { 
                connection.Open();

                string sql = "UPDATE Budget" +
                             "SET Limit = @limit, amountSpent = @amountspent, Category = @category" +
                             "WHERE budgetID = @budgetid";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("limit", limit);
                    command.Parameters.AddWithValue("amountspent", amountspent);
                    command.Parameters.AddWithValue("category", category);
                    command.Parameters.AddWithValue("budgetid", budgetid);

                    command.ExecuteNonQuery();

                }

            }
        }
    }

    public class IncomeManager
    {
        private readonly Database db;
        public User owner { get; set; }
        public List<Income> incomes { get; private set; }

        public IncomeManager(List<Income> incomes)
        {
            db = new Database("Data Source=localhost;Initial Catalog=PersonalBudget;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            this.incomes = incomes;
        }

        public void deleteRecord(int id)
        {
            foreach (Income income in incomes)
            {
                if (income.id == id)
                {
                    incomes.Remove(income);
                }
            }

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                string sql = "DELETE FROM Income" +
                             "WHERE incomeID = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }

            }
        }

        public void addRecord(double amount, DateTime recieveDate, bool isNonRecurring)
        {
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                string sql = "INSERT INTO Income(userID, Amount, isRecurring)" +
                             "VALUES (@userid, @amount, @recievedate, @isnonrecurring)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("amount", amount);
                    command.Parameters.AddWithValue("userid", owner.ID);
                    command.Parameters.AddWithValue("recievedate", recieveDate);
                    command.Parameters.AddWithValue("isnonrecurring", isNonRecurring);

                    command.ExecuteNonQuery();
                }

                incomes.Clear();
                string sqlBudget = "SELECT * FROM Income WHERE userID = @id";
                using (SqlCommand command = new SqlCommand(sqlBudget, connection))
                {
                    command.Parameters.AddWithValue("id", owner.ID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int incomeID = reader.GetInt32(0);
                            double newamount = reader.GetDouble(2);
                            DateTime newreciveDate = reader.GetDateTime(3);
                            bool newisnonrecurring = reader.GetBoolean(4);

                            incomes.Add(new Budget(incomeID, newamount, newreciveDate, newisnonrecurring));
                        }
                    }


                }

            }
        }

        public void editRecord(int incomeid, double amount, DateTime recievedate, bool isnonrecurring)
        {
            foreach (Income income in incomes)
            {
                if (income.id == incomeid)
                {
                    income.amount = amount;
                    income.recieveDate = recievedate;
                    income.isNonRecurring = isnonrecurring;
                }
            }

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                string sql = "UPDATE Income" +
                             "SET Amount = @amount, recieveDate = @recievedate, isNonRecurring = @isnonrecurring" +
                             "WHERE incomeID = @incomeid";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("amount", amount);
                    command.Parameters.AddWithValue("userid", owner.ID);
                    command.Parameters.AddWithValue("recievedate", recievedate);
                    command.Parameters.AddWithValue("isnonrecurring", isnonrecurring);

                    command.ExecuteNonQuery();

                    command.ExecuteNonQuery();

                }

            }
        }
    }
}
