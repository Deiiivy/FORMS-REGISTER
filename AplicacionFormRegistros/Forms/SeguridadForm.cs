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
    public partial class SeguridadForm : Form
    {
        public SeguridadForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private string usuarioActual;

        public SeguridadForm(string usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            txtUsuarioActual.Text = usuario; 
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string nuevoUsuario = txtNuevoUsuario.Text.Trim();
            string nuevaClave = txtNuevaClave.Text.Trim();

            if (string.IsNullOrEmpty(nuevoUsuario) || string.IsNullOrEmpty(nuevaClave))
            {
                MessageBox.Show("Ingrese un nuevo usuario y contraseña");
                return;
            }

            string query = "UPDATE Usuarios SET Usuario = @NuevoUsuario, Clave = @NuevaClave WHERE Usuario = @UsuarioActual";

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@NuevoUsuario", nuevoUsuario);
                    cmd.Parameters.AddWithValue("@NuevaClave", nuevaClave);
                    cmd.Parameters.AddWithValue("@UsuarioActual", usuarioActual);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Credenciales actualizadas correctamente.");
                        usuarioActual = nuevoUsuario;
                        txtUsuarioActual.Text = nuevoUsuario;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        
        }
    }
}
