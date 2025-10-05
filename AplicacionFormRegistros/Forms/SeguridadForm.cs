using AplicacionFormRegistros.BLL;
using AplicacionFormRegistros.ENTIDADES;
using System;
using System.Windows.Forms;

namespace AplicacionFormRegistros.Forms
{
    public partial class SeguridadForm : Form
    {
        private readonly UsuarioBLL _usuarioBLL;
        private string usuarioActual;

        public SeguridadForm(string usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            txtUsuarioActual.Text = usuario;
            _usuarioBLL = new UsuarioBLL();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string nuevoUsuario = txtNuevoUsuario.Text.Trim();
            string nuevaClave = txtNuevaClave.Text.Trim();

            if (string.IsNullOrEmpty(nuevoUsuario) || string.IsNullOrEmpty(nuevaClave))
            {
                MessageBox.Show("Debe ingresar un nuevo usuario y contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Verificar si el usuario ya existe
                if (_usuarioBLL.ExisteUsuario(nuevoUsuario) && !nuevoUsuario.Equals(usuarioActual, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("El nombre de usuario ya existe. Elija otro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Intentar actualizar las credenciales
                bool actualizado = _usuarioBLL.ActualizarCredenciales(usuarioActual, nuevoUsuario, nuevaClave);

                if (actualizado)
                {
                    MessageBox.Show("Credenciales actualizadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    usuarioActual = nuevoUsuario;
                    txtUsuarioActual.Text = nuevoUsuario;
                    txtNuevoUsuario.Clear();
                    txtNuevaClave.Clear();
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
