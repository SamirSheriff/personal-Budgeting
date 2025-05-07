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
        private List<Budget> budgets;

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

                string sql = "INSERT INTO Budget(userID, Category, Amount, isRecurring)" +
                             "VALUES (@userid, @limit, @amountspent, @category)";

                using (SqlCommand command = new SqlCommand(sql, connection)) 
                { 
                    command.Parameters.AddWithValue ("category", category);
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
}
