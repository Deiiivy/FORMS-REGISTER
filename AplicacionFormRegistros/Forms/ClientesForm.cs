using System;
using System;
using System;
using System.Windows.Forms;
using AplicacionFormRegistros.BLL;
using AplicacionFormRegistros.ENTIDADES;

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

        private void btnCargar_Click(object sender, EventArgs e)
        {
            dgvClientes.DataSource = PersonBLL.ObtenerTodos();

            if (dgvClientes.Rows.Count > 0)
            {
                dgvClientes.Rows[0].Selected = true;
                dgvClientes_SelectionChanged(null, null);
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                var person = new Person
                {
                    LastName = txtLastName.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim()
                };

                PersonBLL.Insertar(person);
                MessageBox.Show("Cliente insertado correctamente.");
                btnCargar_Click(null, null);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var person = new Person
                {
                    PersonID = int.Parse(txtPersonID.Text),
                    LastName = txtLastName.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim()
                };

                PersonBLL.Actualizar(person);
                MessageBox.Show("Cliente actualizado correctamente.");
                btnCargar_Click(null, null);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtPersonID.Text);
                PersonBLL.Eliminar(id);
                MessageBox.Show("Cliente eliminado correctamente.");
                btnCargar_Click(null, null);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var row = dgvClientes.SelectedRows[0];
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

