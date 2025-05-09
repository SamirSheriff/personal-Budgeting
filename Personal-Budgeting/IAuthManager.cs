using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Personal_Budgeting
{
    public interface IAuthManager
    {
        public User login(string username, string password);
        public void signup(string username, string password, string email);
    }

    public class AuthManager : IAuthManager
    {
        private readonly Database database;

        public AuthManager()
        {
            database = new Database("Data Source = localhost; Initial Catalog = PersonalBudget; Integrated Security = True; Encrypt=True;Trust Server Certificate=True");
        }

        public User login(string username, string password)
        {
            string realPassword;
            int id;
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();

                string sql = "SELECT ID, Password FROM [User] WHERE Username = @username";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("username", username);

                    using (SqlDataReader reader = command.ExecuteReader()) 
                    {
                        if (reader.Read())
                        {
                            id = reader.GetInt32(0);
                            realPassword = reader.GetString(1);
                        }
                        else
                        {
                            throw new Exception("User not found");
                        }
                    }
                }
            }

            if (password == realPassword)
            {
                return database.loadUser(id);
            }

            else
            {
                throw new Exception("Invalid Password");
            }
        }

        public void signup(string username, string password, string email)
        {
            database.saveUser(username, email, password);
        }
    }
}
