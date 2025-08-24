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
using AplicacionFormRegistros.Database;


namespace AplicacionFormRegistros.Forms
{
    public partial class ClientesForm : Form
    {
        public ClientesForm()
        {
            InitializeComponent();
        }




        private void btnCargar_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Persons";
            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable clientesTable = new DataTable();
                        adapter.Fill(clientesTable);
                        dgvClientes.DataSource = clientesTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos: " + ex.Message);
                }
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(txtPersonID.Text);
            string LastName = txtLastName.Text;
            string FirstName = txtFirstName.Text;

            string query = "INSERT INTO Persons(PersonID, LastName, FirstName) VALUES(@PersonID, @LastName, @FirstName)";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PersonID", PersonID);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente insertado correctamente");

                    btnCargar_Click(null, null);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al insertar datos: " + ex.Message);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(txtPersonID.Text);
            string LastName = txtLastName.Text;
            string FirstName = txtFirstName.Text;

            string query = "UPDATE Persons SET LastName = @LastName, FirstName = @FirstName WHERE PersonID = @PersonID";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PersonID", PersonID);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Datos actualizados correctamente.");
                    else
                        MessageBox.Show("No se encontró el registro para actualizar.");

                    btnCargar_Click(null, null);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al actualizar datos: " + ex.Message);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(txtPersonID.Text);

            string query = "DELETE FROM Persons WHERE PersonID = @PersonID";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PersonID", PersonID);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Cliente eliminado correctamente.");
                    else
                        MessageBox.Show("No se encontró el registro para eliminar.");

                    btnCargar_Click(null, null);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al eliminar datos: " + ex.Message);
                }
            }
        }
    }
}
