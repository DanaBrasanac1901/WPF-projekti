using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Project1.Model;

namespace Project1.ViewModel
{
    public class UserRepository
    { 
        public ObservableCollection<User> Users { get; set; }
        public UserRepository() { 
        
            LoadDb();
        }
        public void LoadDb()
        {
            Users = new ObservableCollection<User>();
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ADMIN_1;Integrated Security=True");
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                String query = "SELECT * FROM Users";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            String username = reader["UserName"].ToString();
                            String password = reader["UserPass"].ToString();
                            bool isAdmin = Convert.ToBoolean(reader["isAdmin"]);
                            User user = new User(id, username, password, isAdmin);
                            Users.Add(user);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { sqlConnection.Close(); }

        }

        public void AddUser(User user)
        {

            SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ADMIN_1;Integrated Security=True");
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                String query = "INSERT INTO Users VALUES (@ID,@UserName,@UserPass,@IsAdmin)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", GenerateId());
                sqlCommand.Parameters.AddWithValue("@UserName", user.UserName);
                sqlCommand.Parameters.AddWithValue("@UserPass", user.UserPass);
                sqlCommand.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { sqlConnection.Close(); }
        }

        public int GenerateId()
        {
            int i = 300;
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ADMIN_1;Integrated Security=True");
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                String query = "Select Count(*) from Users";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                i = Convert.ToInt32(sqlCommand.ExecuteScalar());
                i++;
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { sqlConnection.Close();
               
            }
            return i;
        }

        public void UpdateUser(User user)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ADMIN_1;Integrated Security=True");
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                String query = "Update Users set UserName=@UserName, UserPass=@UserPass, IsAdmin=@IsAdmin where ID=@ID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", user.ID);
                sqlCommand.Parameters.AddWithValue("@UserName", user.UserName);
                sqlCommand.Parameters.AddWithValue("@UserPass", user.UserPass);
                sqlCommand.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { sqlConnection.Close(); }
        }


    }
}
