using AplicacionFormRegistros.DAL;
using AplicacionFormRegistros.ENTIDADES;
using System;
using System.Data.SqlClient;

namespace AplicacionFormRegistros.DAL
{
    public class UsuarioDAL
    {
        public bool RegistrarUsuario(Usuario usuario)
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                string query = "INSERT INTO Usuarios (Usuario, Clave) VALUES (@Usuario, @Clave)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Usuario", usuario.UsuarioNombre);
                cmd.Parameters.AddWithValue("@Clave", usuario.Clave);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE LTRIM(RTRIM(LOWER(Usuario))) = LOWER(@Usuario)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Usuario", nombreUsuario);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public bool ValidarLogin(string usuario, string clave)
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Usuarios 
                    WHERE LTRIM(RTRIM(LOWER(Usuario))) = LOWER(@Usuario)
                      AND LTRIM(RTRIM(LOWER(Clave))) = LOWER(@Clave)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Clave", clave);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public bool ActualizarCredenciales(string usuarioActual, string nuevoUsuario, string nuevaClave)
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                string query = "UPDATE Usuarios SET Usuario = @NuevoUsuario, Clave = @NuevaClave WHERE Usuario = @UsuarioActual";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NuevoUsuario", nuevoUsuario);
                cmd.Parameters.AddWithValue("@NuevaClave", nuevaClave);
                cmd.Parameters.AddWithValue("@UsuarioActual", usuarioActual);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
