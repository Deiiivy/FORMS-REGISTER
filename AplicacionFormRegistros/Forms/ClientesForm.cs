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

            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;

            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
        }

        private bool ValidarCliente(bool incluirID = false)
        {
            if (incluirID && string.IsNullOrWhiteSpace(txtPersonID.Text))
            {
                MessageBox.Show("Debe seleccionar un cliente de la lista.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("El apellido no puede estar vacío.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("El nombre no puede estar vacío.");
                return false;
            }

            return true;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Persons";
            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable clientesTable = new DataTable();
                    adapter.Fill(clientesTable);
                    dgvClientes.DataSource = clientesTable;

                    if (dgvClientes.Rows.Count > 0)
                    {
                        dgvClientes.Rows[0].Selected = true;
                        dgvClientes_SelectionChanged(null, null);
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
            if (!ValidarCliente()) return;

            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();

            string query = "INSERT INTO Persons(LastName, FirstName) VALUES(@LastName, @FirstName)";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente insertado correctamente.");

                    btnCargar_Click(null, null);
                    LimpiarCampos();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al insertar datos: " + ex.Message);
                }
            }
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarCliente(incluirID: true)) return;

            int personID = Convert.ToInt32(txtPersonID.Text);
            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();

            string query = "UPDATE Persons SET LastName = @LastName, FirstName = @FirstName WHERE PersonID = @PersonID";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Datos actualizados correctamente.");
                    else
                        MessageBox.Show("No se encontró el cliente para actualizar.");

                    btnCargar_Click(null, null);
                    LimpiarCampos();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al actualizar datos: " + ex.Message);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!ValidarCliente(incluirID: true)) return;

            int personID = Convert.ToInt32(txtPersonID.Text);

            string query = "DELETE FROM Persons WHERE PersonID = @PersonID";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PersonID", personID);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Cliente eliminado correctamente.");
                    else
                        MessageBox.Show("No se encontró el cliente para eliminar.");

                    btnCargar_Click(null, null);
                    LimpiarCampos();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al eliminar datos: " + ex.Message);
                }
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvClientes.SelectedRows[0];

                txtPersonID.Text = row.Cells["PersonID"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
            }
        }

        private void LimpiarCampos()
        {
            txtPersonID.Clear();
            txtLastName.Clear();
            txtFirstName.Clear();
        }
    }
}
