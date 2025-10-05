using AplicacionFormRegistros.DAL;
using AplicacionFormRegistros.DALL.DAL;
using AplicacionFormRegistros.ENTIDADES;
using System;

namespace AplicacionFormRegistros.BLL
{
    public class UsuarioBLL
    {
        private readonly UsuarioDAL _usuarioDAL;

        public UsuarioBLL()
        {
            _usuarioDAL = new UsuarioDAL();
        }

        public bool Registrar(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.UsuarioNombre) || string.IsNullOrWhiteSpace(usuario.Clave))
                throw new Exception("Usuario y clave son obligatorios.");

            if (_usuarioDAL.ExisteUsuario(usuario.UsuarioNombre))
                throw new Exception("El usuario ya existe.");

            return _usuarioDAL.RegistrarUsuario(usuario);
        }

        public bool Login(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                throw new Exception("Debes ingresar usuario y contraseña.");

            return _usuarioDAL.ValidarLogin(usuario, clave);
        }

        public bool ActualizarCredenciales(string usuarioActual, string nuevoUsuario, string nuevaClave)
        {
            if (string.IsNullOrWhiteSpace(nuevoUsuario) || string.IsNullOrWhiteSpace(nuevaClave))
                throw new Exception("El nuevo usuario y la nueva clave son obligatorios.");

            return _usuarioDAL.ActualizarCredenciales(usuarioActual, nuevoUsuario, nuevaClave);
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("Debe especificar el nombre de usuario.");

            return _usuarioDAL.ExisteUsuario(nombreUsuario);
        }
    }
}
