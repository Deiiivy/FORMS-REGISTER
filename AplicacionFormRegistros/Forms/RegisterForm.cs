using AplicacionFormRegistros.BLL;
using AplicacionFormRegistros.ENTIDADES;
using System;
using System.Windows.Forms;

namespace AplicacionFormRegistros.Forms
{
    public partial class RegisterForm : Form
    {
        private readonly UsuarioBLL usuarioBLL;

        public RegisterForm()
        {
            InitializeComponent();
            usuarioBLL = new UsuarioBLL();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nuevoUsuario = txtNuevoUsuario.Text.Trim();
            string nuevaClave = txtNuevaClave.Text.Trim();

            if (string.IsNullOrEmpty(nuevoUsuario) || string.IsNullOrEmpty(nuevaClave))
            {
                MessageBox.Show("Debe ingresar un usuario y una contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Usuario usuario = new Usuario
                {
                    UsuarioNombre = nuevoUsuario,
                    Clave = nuevaClave
                };

                bool registrado = usuarioBLL.Registrar(usuario);

                if (registrado)
                {
                    MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El usuario ya existe. Elige otro nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
