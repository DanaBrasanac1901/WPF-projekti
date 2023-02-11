using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Project2.Model;

namespace Project2.ViewModel
{
    public class Repository
    {
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public Repository() {
            Users = new ObservableCollection<User>();
            Products = new ObservableCollection<Product>();
            LoadProducts();
            LoadUsers();
            UpdateDb();
        }

        public void LoadUsers()
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

        public void LoadProducts()
        {
            Users = new ObservableCollection<User>();
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ADMIN_1;Integrated Security=True");
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                String query = "SELECT * FROM Products";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            int price = Convert.ToInt32(reader["Price"]);
                            String name = reader["Name"].ToString();
                            String bidder = reader["Bidder"].ToString();
                            bool isClosed = Convert.ToBoolean(reader["isClosed"]);
                            DateTime exp = DateTime.Parse(reader["BidExpiration"].ToString());
                            Product product = new Product(id, price, name, bidder, isClosed, exp);
                            if (DateTime.Compare(exp, DateTime.Now) <= 0) product.IsClosed = true;
                            Products.Add(product);
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

        public void UpdateDb()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ADMIN_1;Integrated Security=True");
            try
            {
                foreach(Product product in Products)
                {
                    if (DateTime.Compare(product.BidExpiration, DateTime.Now) <= 0) product.IsClosed = true;
                    if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                    String query = "Update Products set IsClosed=@IsClosed where ID=@ID";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", product.ID);
                    sqlCommand.Parameters.AddWithValue("@IsClosed", product.IsClosed);
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.ExecuteNonQuery();

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { sqlConnection.Close(); }

        }

        public void AddProduct(Product product)
        {

            SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ADMIN_1;Integrated Security=True");
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                String query = "INSERT INTO Products VALUES (@ID, @Price, @Name, @Bidder, @IsClosed, @BidExpiration)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", GenerateId());
                sqlCommand.Parameters.AddWithValue("@Name", product.Name);
                sqlCommand.Parameters.AddWithValue("@Bidder", product.Bidder);
                sqlCommand.Parameters.AddWithValue("@Price", product.Price);
                sqlCommand.Parameters.AddWithValue("@IsClosed", product.IsClosed);
                sqlCommand.Parameters.AddWithValue("@BidExpiration", product.BidExpiration);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.ExecuteNonQuery();
                LoadProducts();
                UpdateDb();

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
                String query = "Select Count(*) from Products";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                i = Convert.ToInt32(sqlCommand.ExecuteScalar());
                i++;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                sqlConnection.Close();

            }
            return i;
        }

        public void UpdateProduct(Product product)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ADMIN_1;Integrated Security=True");
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                String query = "Update Products set Name=@Name, Price=@Price, IsClosed=@IsClosed, Bidder=@Bidder, BidExpiration=@BidExpiration where ID=@ID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", product.ID);
                sqlCommand.Parameters.AddWithValue("@Name", product.Name);
                sqlCommand.Parameters.AddWithValue("@Bidder", product.Bidder);
                sqlCommand.Parameters.AddWithValue("@Price", product.Price);
                sqlCommand.Parameters.AddWithValue("@IsClosed", product.IsClosed);
                sqlCommand.Parameters.AddWithValue("@BidExpiration", product.BidExpiration);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.ExecuteNonQuery();
                LoadProducts();
                UpdateDb();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { sqlConnection.Close(); }
        }

        public void DeleteProduct(Product product) {

            SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ADMIN_1;Integrated Security=True");
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                String query = "Delete From Products where ID=@ID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", product.ID);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.ExecuteNonQuery();
                UpdateDb();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { sqlConnection.Close(); }
        }


    }

}
