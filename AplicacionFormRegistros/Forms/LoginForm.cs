using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using AplicacionFormRegistros.Database;


namespace AplicacionFormRegistros.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingresa usuario y contraseña", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = ConexionBD.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT COUNT(*) 
                        FROM Usuarios 
                        WHERE LTRIM(RTRIM(LOWER(Usuario))) = LOWER(@Usuario)
                          AND LTRIM(RTRIM(LOWER(Clave))) = LOWER(@Clave)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Clave", password);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            this.Hide();
                            MainForm main = new MainForm(usuario);
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.ShowDialog();
        }
    }
}
