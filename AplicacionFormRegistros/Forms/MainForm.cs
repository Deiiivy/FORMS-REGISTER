using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionFormRegistros.Forms
{
    public partial class MainForm : Form
    {
        private string usuarioLogueado;
        public MainForm(string usuario)
        {
            InitializeComponent();
            usuarioLogueado = usuario;
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientesForm frm = new ClientesForm();
            frm.ShowDialog();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductosForm frm = new ProductosForm();
            frm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void seguridadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeguridadForm seguridad = new SeguridadForm(usuarioLogueado);
            seguridad.ShowDialog();
        }
    }
}
