using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Personal_Budgeting
{
    public interface IDatabase
    {
        public void saveUser(string username, string email, string password);

        public User loadUser(int id);

        public void editUser(User altUser);
    }

    public class Database : IDatabase
    {
        private string connectionstring;

        Database(string cS)
        {
            connectionstring = cS;
        }

        public void saveUser(string username, string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring)) 
                {
                    connection.Open();

                    string sql = "INSERT INTO [USER]" +
                                 "(Username, [Password], Email) VALUES" +
                                 "(@username, @email, @password");
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("username", username);
                        command.Parameters.AddWithValue("email", email);
                        command.Parameters.AddWithValue("password", password);

                        command.ExecuteNonQuery();
                    }

                }
            }

            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public User loadUser(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    string sqlUser = "SELECT * FROM [User] WHERE ID = @id";

                    string password = "";
                    string username = "";
                    string email = "";

                    using (SqlCommand command = new SqlCommand(sqlUser, connection))
                    {
                        command.Parameters.AddWithValue("id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    username = reader.GetString(1);
                                    password = reader.GetString(2);
                                    email = reader.GetString(3);

                                }
                            }

                            else
                            {
                                throw new Exception("User ID doesn't exist");
                            }
                        }
                    }

                    string sqlBudget = "SELECT * FROM Budget WHERE userID = @id";

                    BudgetManager budgetManager;
                    using (SqlCommand command = new SqlCommand(sqlBudget, connection))
                    {
                        command.Parameters.AddWithValue("id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            List<Budget> budgets = new List<Budget>;
                            while (reader.Read())
                            {
                                int budgetID = reader.GetInt32(0);
                                double limit = reader.GetDouble(2);
                                double amountSpent = reader.GetDouble(3);
                                string category = reader.GetString(4);

                                budgets.Add(new Budget(budgetID, limit, amountSpent, category));
                            }

                            budgetManager = new BudgetManager(budgets);
                        }


                    }

                    string sqlIncome = "SELECT * FROM Income WHERE userID = @id";

                    IncomeManager incomeManager;
                    using (SqlCommand command = new SqlCommand(sqlIncome, connection))
                    {
                        command.Parameters.AddWithValue("id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            List<Income> incomes = new List<Income>;
                            while (reader.Read())
                            {
                                int incomeID = reader.GetInt32(0);
                                double amount = reader.GetDouble(2);
                                DateTime recieveDate = reader.GetDateTime(3);
                                bool isNonRecurring = reader.GetBoolean(4);

                                incomes.Add(new Income(incomeID, amount, recieveDate, isNonRecurring));
                            }

                            incomeManager = new IncomeManager(incomes);
                        }


                    }


                    string sqlExpense = "SELECT * FROM Expense WHERE userID = @id";

                    ExpenseManager expenseManager;
                    using (SqlCommand command = new SqlCommand(sqlExpense, connection))
                    {
                        command.Parameters.AddWithValue("id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            List<Expense> expenses = new List<Expense>;
                            while (reader.Read())
                            {
                                int expenseID = reader.GetInt32(0);
                                string category = reader.GetString(2);
                                double amount = reader.GetDouble(3);
                                bool isRecurring = reader.GetBoolean(4);

                                expenses.Add(new Expense(expenseID, category, amount, isRecurring));
                            }

                            expenseManager = new ExpenseManager(expenses);
                        }


                    }

                    return new User(id, username, email, expenseManager, incomeManager, budgetManager, password);

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

}
