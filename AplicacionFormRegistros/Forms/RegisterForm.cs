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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nuevoUsuario = txtNuevoUsuario.Text.Trim();
            string nuevaClave = txtNuevaClave.Text.Trim();

            if (string.IsNullOrEmpty(nuevoUsuario) || string.IsNullOrEmpty(nuevaClave))
            {
                MessageBox.Show("Ingrese usuario y contraseña");
                return;
            }

            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                try
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                    checkCmd.Parameters.AddWithValue("@Usuario", nuevoUsuario);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        MessageBox.Show("Ese usuario ya existe. Elige otro.");
                        return;
                    }

                    string insertQuery = "INSERT INTO Usuarios (Usuario, Clave) VALUES (@Usuario, @Clave)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@Usuario", nuevoUsuario);
                    insertCmd.Parameters.AddWithValue("@Clave", nuevaClave);

                    int rows = insertCmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Usuario registrado correctamente.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar el usuario.");
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

