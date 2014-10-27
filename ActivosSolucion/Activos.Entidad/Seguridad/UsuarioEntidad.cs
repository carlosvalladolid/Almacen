using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Seguridad
{
    public class UsuarioEntidad : Base
    {
        private Int16 _UsuarioId;                   // Identificador del usuario
        private Int16 _AplicacionId;                // Identificador de la aplicación a la que pertenece el usuario
        private Int16 _RolId;                       // Identificador del rol
        private Int16 _EstatusId;                   // Identificador del estatus
        private Int16 _UsuarioIdInserto;            // Identificador del usuario que creó el registro
        private Int16 _UsuarioIdModifico;           // Identificador del usuario que modificó el registro por última vez
        private string _Nombre;                     // Nombre del usuario
        private string _ApellidoPaterno;            // Apellido paterno del usuario
        private string _ApellidoMaterno;            // Apellido materno del usuario
        private string _CuentaUsuario;              // Cuenta de usuario
        private string _Contrasenia;                // Contraseña de la cuenta de usuario
        private string _FechaInserto;               // Fecha en formato texto en que se creó el registro
        private string _FechaUltimaModificacion;    // Fecha en formato texto de la última vez que se modificó el registro
        private string _SesionId;                   // Variable que almacena una cadena aleatoria de la sesión

        // Otros campos
        private bool _RecordarContrasenia;          // Indica si el usuario quiere que el sistema recuerde su contraseña
        private string _BusquedaRapida;             // Texto de búsqueda en el catálogo de socios
        private string _ContraseniaAnterior;        // Contraseña anterior
        private string _NuevaContrasenia;           // Contraseña nueva
        private string _Confirmacion;               // Confirmación de la contraseña
        private string _CadenaUsuarioId;            // Cadena con Ids de usuarios seleccionados

        public UsuarioEntidad()
        {
            _UsuarioId = 0;
            _AplicacionId = 0;
            _RolId = 0;
            _EstatusId = 0;
            _UsuarioIdInserto = 0;
            _UsuarioIdModifico = 0;
            _Nombre = string.Empty;
            _ApellidoPaterno = string.Empty;
            _ApellidoMaterno = string.Empty;
            _CuentaUsuario = string.Empty;
            _Contrasenia = string.Empty;
            _FechaInserto = string.Empty;
            _FechaUltimaModificacion = string.Empty;
            _RecordarContrasenia = false;
            _BusquedaRapida = string.Empty;
            _ContraseniaAnterior = string.Empty;
            _NuevaContrasenia = string.Empty;
            _Confirmacion = string.Empty;
            _CadenaUsuarioId = string.Empty;
            _SesionId = Guid.NewGuid().ToString();
        }

        public Int16 UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        public Int16 AplicacionId
        {
            get { return _AplicacionId; }
            set { _AplicacionId = value; }
        }

        public Int16 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        public Int16 EstatusId
        {
            get { return _EstatusId; }
            set { _EstatusId = value; }
        }

        public Int16 UsuarioIdInserto
        {
            get { return _UsuarioIdInserto; }
            set { _UsuarioIdInserto = value; }
        }

        public Int16 UsuarioIdModifico
        {
            get { return _UsuarioIdModifico; }
            set { _UsuarioIdModifico = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string ApellidoPaterno
        {
            get { return _ApellidoPaterno; }
            set { _ApellidoPaterno = value; }
        }

        public string ApellidoMaterno
        {
            get { return _ApellidoMaterno; }
            set { _ApellidoMaterno = value; }
        }

        public string CuentaUsuario
        {
            get { return _CuentaUsuario; }
            set { _CuentaUsuario = value; }
        }

        public string Contrasenia
        {
            get { return _Contrasenia; }
            set { _Contrasenia = value; }
        }

        public string FechaInserto
        {
            get { return _FechaInserto; }
            set { _FechaInserto = value; }
        }

        public string FechaUltimaModificacion
        {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = value; }
        }

        public bool RecordarContrasenia
        {
            get { return _RecordarContrasenia; }
            set { _RecordarContrasenia = value; }
        }

        public string BusquedaRapida
        {
            get { return _BusquedaRapida; }
            set { _BusquedaRapida = value; }
        }

        public string ContraseniaAnterior
        {
            get { return _ContraseniaAnterior; }
            set { _ContraseniaAnterior = value; }
        }

        public string NuevaContrasenia
        {
            get { return _NuevaContrasenia; }
            set { _NuevaContrasenia = value; }
        }

        public string Confirmacion
        {
            get { return _Confirmacion; }
            set { _Confirmacion = value; }
        }

        public string CadenaUsuarioId
        {
            get { return _CadenaUsuarioId; }
            set { _CadenaUsuarioId = value; }
        }

        public string SesionId
        {
            get { return _SesionId; }
        }

    }
}
