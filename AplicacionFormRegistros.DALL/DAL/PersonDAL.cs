
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionFormRegistros.DAL;
using AplicacionFormRegistros.ENTIDADES;

namespace AplicacionFormRegistros.DALL.DAL
{
    public class PersonDAL
    {
        public static List<Person> ObtenerTodos()
        {
            List<Person> lista = new List<Person>();

            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                string query = "SELECT * FROM Persons";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Person
                    {
                        PersonID = Convert.ToInt32(reader["PersonID"]),
                        LastName = reader["LastName"].ToString(),
                        FirstName = reader["FirstName"].ToString()
                    });
                }
            }
            return lista;
        }

        public static void Insertar(Person p)
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                string query = "INSERT INTO Persons(LastName, FirstName) VALUES(@LastName, @FirstName)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LastName", p.LastName);
                cmd.Parameters.AddWithValue("@FirstName", p.FirstName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Actualizar(Person p)
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                string query = "UPDATE Persons SET LastName = @LastName, FirstName = @FirstName WHERE PersonID = @PersonID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LastName", p.LastName);
                cmd.Parameters.AddWithValue("@FirstName", p.FirstName);
                cmd.Parameters.AddWithValue("@PersonID", p.PersonID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Eliminar(int id)
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                string query = "DELETE FROM Persons WHERE PersonID = @PersonID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PersonID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
