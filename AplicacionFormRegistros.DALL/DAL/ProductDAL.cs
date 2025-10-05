using AplicacionFormRegistros.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionFormRegistros.DAL.DAL
{
    public class ProductDAL
    {
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT * FROM Products";

            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
                    });
                }
            }
            return products;
        }

        public void Insert(Product product)
        {
            string query = "INSERT INTO Products (ProductName, Price) VALUES (@ProductName, @Price)";
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product product)
        {
            string query = "UPDATE Products SET ProductName = @ProductName, Price = @Price WHERE ProductID = @ProductID";
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Products WHERE ProductID = @ProductID";
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
