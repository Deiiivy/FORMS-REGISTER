using AplicacionFormRegistros.BLL;
using AplicacionFormRegistros.ENTIDADES;
using System;
using System.Windows.Forms;

namespace AplicacionFormRegistros.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UsuarioBLL usuarioBLL;

        public LoginForm()
        {
            InitializeComponent();
            usuarioBLL = new UsuarioBLL();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            try
            {
                // Validar credenciales usando la capa BLL
                bool loginExitoso = usuarioBLL.Login(usuario, password);

                if (loginExitoso)
                {
                    MessageBox.Show("Inicio de sesión exitoso.", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    MainForm main = new MainForm(usuario);
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.ShowDialog();
        }
    }
}
