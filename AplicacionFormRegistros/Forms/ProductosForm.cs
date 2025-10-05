using AplicacionFormRegistros.BLL;
using AplicacionFormRegistros.BLL;
using AplicacionFormRegistros.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace AplicacionFormRegistros.Forms
{
    public partial class ProductosForm : Form
    {
        private ProductBLL productBLL = new ProductBLL();

        public ProductosForm()
        {
            InitializeComponent();

            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;

            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                var productos = productBLL.GetAll();
                dgvProductos.DataSource = productos;

                if (dgvProductos.Rows.Count > 0)
                {
                    dgvProductos.Rows[0].Selected = true;
                    dgvProductos_SelectionChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del producto.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Debe ingresar un precio válido.");
                return;
            }

            Product product = new Product
            {
                ProductName = txtProductName.Text.Trim(),
                Price = price
            };

            try
            {
                productBLL.Insert(product);
                MessageBox.Show("Producto insertado correctamente.");
                btnCargar_Click(null, null);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar producto: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Debe seleccionar un producto para actualizar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del producto.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Debe ingresar un precio válido.");
                return;
            }

            Product product = new Product
            {
                ProductID = Convert.ToInt32(txtProductID.Text),
                ProductName = txtProductName.Text.Trim(),
                Price = price
            };

            try
            {
                productBLL.Update(product);
                MessageBox.Show("Producto actualizado correctamente.");
                btnCargar_Click(null, null);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar producto: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Debe seleccionar un producto para eliminar.");
                return;
            }

            int id = Convert.ToInt32(txtProductID.Text);

            try
            {
                productBLL.Delete(id);
                MessageBox.Show("Producto eliminado correctamente.");
                btnCargar_Click(null, null);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar producto: " + ex.Message);
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProductos.SelectedRows[0];

                txtProductID.Text = row.Cells["ProductID"].Value.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
            }
        }

        private void LimpiarCampos()
        {
            txtProductID.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
        }
    }
}
