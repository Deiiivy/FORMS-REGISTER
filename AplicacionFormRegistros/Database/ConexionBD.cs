using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionFormRegistros.Database
{
    public class ConexionBD
    {
        private static string connectionString =
            "Server=PC-DEIVY\\SQLEXPRESS;Database=miBaseDeDatos;Trusted_Connection=True;MultipleActiveResultSets=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
