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
            string query;

            if (!string.IsNullOrWhiteSpace(txtProductID.Text)) 
            {
                query = "SELECT * FROM Products WHERE ProductID=@id";
            }
            else
            {
                query = "SELECT * FROM Products"; 
            }

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);

                    if (!string.IsNullOrWhiteSpace(txtProductID.Text))
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductID.Text));

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable productosTable = new DataTable();
                    adapter.Fill(productosTable);

                    dgvProductos.DataSource = productosTable;

                    if (productosTable.Rows.Count == 1)
                    {
                        DataRow row = productosTable.Rows[0];
                        txtProductID.Text = row["ProductID"].ToString();
                        txtProductName.Text = row["ProductName"].ToString();
                        txtPrice.Text = row["Price"].ToString();
                    }
                    else if (productosTable.Rows.Count == 0 && !string.IsNullOrWhiteSpace(txtProductID.Text))
                    {
                        MessageBox.Show("No se encontró el producto con ese ID.");
                        txtProductName.Clear();
                        txtPrice.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar productos: " + ex.Message);
                }
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Debe ingresar nombre y precio.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("El precio debe ser un número válido y mayor o igual a 0.");
                return;
            }

            string query = "INSERT INTO Products(ProductName, Price) VALUES(@name, @price)";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", txtProductName.Text);
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
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Debe ingresar el ID del producto a actualizar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Debe ingresar nombre y precio.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("El precio debe ser un número válido y mayor o igual a 0.");
                return;
            }

            string query = "UPDATE Products SET ProductName=@name, Price=@price WHERE ProductID=@id";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductID.Text));
                    cmd.Parameters.AddWithValue("@name", txtProductName.Text);
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
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Debe ingresar el ID del producto a eliminar.");
                return;
            }

            string query = "DELETE FROM Products WHERE ProductID=@id";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductID.Text));

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
