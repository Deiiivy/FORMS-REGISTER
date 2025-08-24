using AplicacionFormRegistros.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionFormRegistros.Forms
{
    public partial class ProductosForm : Form
    {
        public ProductosForm()
        {
            InitializeComponent();
        }


        private void btnCargar_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Products";
            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable productosTable = new DataTable();
                    adapter.Fill(productosTable);
                    dgvProductos.DataSource = productosTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar productos: " + ex.Message);
                }
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text;
            decimal price = Convert.ToDecimal(txtPrice.Text);

            string query = "INSERT INTO Products(ProductName, Price) VALUES(@name, @price)";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@price", price);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producto insertado correctamente");

                    btnCargar_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al insertar producto: " + ex.Message);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(txtProductID.Text);
            string name = txtProductName.Text;
            decimal price = Convert.ToDecimal(txtPrice.Text);

            string query = "UPDATE Products SET ProductName=@name, Price=@price WHERE ProductID=@id";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@price", price);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Producto actualizado correctamente.");
                    else
                        MessageBox.Show("No se encontró el producto.");

                    btnCargar_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar producto: " + ex.Message);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtProductID.Text);

            string query = "DELETE FROM Products WHERE ProductID=@id";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Producto eliminado correctamente.");
                    else
                        MessageBox.Show("No se encontró el producto.");

                    btnCargar_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar producto: " + ex.Message);
                }
            }
        }
    }
}
