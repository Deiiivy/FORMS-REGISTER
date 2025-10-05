using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionFormRegistros.ENTIDADES;
using AplicacionFormRegistros.DALL.DAL;

namespace AplicacionFormRegistros.BLL
{
    public class PersonBLL
    {
        public static List<Person> ObtenerTodos()
        {
            return PersonDAL.ObtenerTodos();
        }

        public static void Insertar(Person p)
        {
            if (string.IsNullOrWhiteSpace(p.LastName))
                throw new Exception("El apellido no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(p.FirstName))
                throw new Exception("El nombre no puede estar vacío.");

            PersonDAL.Insertar(p);
        }

        public static void Actualizar(Person p)
        {
            if (p.PersonID <= 0)
                throw new Exception("Debe seleccionar un cliente válido.");

            if (string.IsNullOrWhiteSpace(p.LastName))
                throw new Exception("El apellido no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(p.FirstName))
                throw new Exception("El nombre no puede estar vacío.");

            PersonDAL.Actualizar(p);
        }

        public static void Eliminar(int id)
        {
            if (id <= 0)
                throw new Exception("Debe seleccionar un cliente válido.");

            PersonDAL.Eliminar(id);
        }
    }
}
