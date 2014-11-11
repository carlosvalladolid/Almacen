using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
    public class ProveedorEntidad : Base
    {
        private Int16 _ProveedorId;
        private Int16 _DependenciaId;
        private Int16 _CiudadId;
        private Int16 _BancoId;
        private Int16 _UsuarioIdInserto;
        private Int16 _UsuarioIdModifico;
        private string _Nombre;
        private string _RFC;
        private string _Calle;
        private string _Numero;
        private string _Colonia;
        private string _CodigoPostal;
        private string _Telefono;
        private string _NombreContacto;
        private string _Email;
        private string _Cuenta;
        private string _Clabe;
        private string _CiudadOtro;
        private string _FechaInserto;
        private string _FechaUltimaModificacion;

        // Otros campos
        private Int16 _EstadoId;
        private string _BusquedaRapida;
        private string _CadenaProveedorId;
        private string _BuscarNombre;

        public ProveedorEntidad()
        {
            _ProveedorId = 0;
            _DependenciaId = 0;
            _CiudadId = 0;
            _BancoId = 0;
            _UsuarioIdInserto = 0;
            _UsuarioIdModifico = 0;
            _Nombre = string.Empty;
            _RFC = string.Empty;
            _Calle = string.Empty;
            _Numero = string.Empty;
            _Colonia = string.Empty;
            _CodigoPostal = string.Empty;
            _Telefono = string.Empty;
            _NombreContacto = string.Empty;
            _Email = string.Empty;
            _Cuenta = string.Empty;
            _Clabe = string.Empty;
            _CiudadOtro = string.Empty;
            _FechaInserto = string.Empty;
            _FechaUltimaModificacion = string.Empty;

            _EstadoId = 0;
            _BusquedaRapida = string.Empty;
            _CadenaProveedorId = string.Empty;
            _BuscarNombre = string.Empty;
        }

        /// <summary>
        ///     Identificador de proveedor
        /// </summary>
        public Int16 ProveedorId
        {
            get { return _ProveedorId; }
            set { _ProveedorId = value; }
        }

        /// <summary>
        ///     Identificador de dependencia
        /// </summary>
        public Int16 DependenciaId
        {
            get { return _DependenciaId; }
            set { _DependenciaId = value; }
        }

        /// <summary>
        ///     Identificador de ciudad
        /// </summary>
        public Int16 CiudadId
        {
            get { return _CiudadId; }
            set { _CiudadId = value; }
        }

        /// <summary>
        ///     Identificador de banco
        /// </summary>
        public Int16 BancoId
        {
            get { return _BancoId; }
            set { _BancoId = value; }
        }

        /// <summary>
        ///     Identificador del usuario que creó el registro
        /// </summary>
        public Int16 UsuarioIdInserto
        {
            get { return _UsuarioIdInserto; }
            set { _UsuarioIdInserto = value; }
        }

        /// <summary>
        ///     Identificador del usuario que modificó el registro por última vez
        /// </summary>
        public Int16 UsuarioIdModifico
        {
            get { return _UsuarioIdModifico; }
            set { _UsuarioIdModifico = value; }
        }

        /// <summary>
        ///     Nombre del proveedor
        /// </summary>
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        /// <summary>
        ///     RFC del proveedor
        /// </summary>
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }

        /// <summary>
        ///     Nombre de la calle
        /// </summary>
        public string Calle
        {
            get { return _Calle; }
            set { _Calle = value; }
        }

        /// <summary>
        ///     Numero de la calle
        /// </summary>
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        /// <summary>
        ///     Nombre de la colonia
        /// </summary>
        public string Colonia
        {
            get { return _Colonia; }
            set { _Colonia = value; }
        }

        /// <summary>
        ///     Numero del codigo postal
        /// </summary>
        public string CodigoPostal
        {
            get { return _CodigoPostal; }
            set { _CodigoPostal = value; }
        }

        /// <summary>
        ///     Telefono del proveedor
        /// </summary>
        public string NombreContacto
        {
            get { return _NombreContacto; }
            set { _NombreContacto = value; }
        }

        /// <summary>
        ///     Nombre del contacto
        /// </summary>
        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        /// <summary>
        ///     Email del proveedor
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        /// <summary>
        ///     Numero de cuenta del proveedor
        /// </summary>
        public string Cuenta
        {
            get { return _Cuenta; }
            set { _Cuenta = value; }
        }

        /// <summary>
        ///     Clabe del proveedor
        /// </summary>
        public string Clabe
        {
            get { return _Clabe; }
            set { _Clabe = value; }
        }

        /// <summary>
        ///     Otra ciudad
        /// </summary>
        public string CiudadOtro
        {
            get { return _CiudadOtro; }
            set { _CiudadOtro = value; }
        }

        /// <summary>
        ///     Fecha en formato texto en que se creó el registro
        /// </summary>
        public string FechaInserto
        {
            get { return _FechaInserto; }
            set { _FechaInserto = value; }
        }

        /// <summary>
        ///     Fecha en formato texto de la última vez que se modificó el registro
        /// </summary>
        public string FechaUltimaModificacion
        {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = value; }
        }

        /// <summary>
        ///     Identificador del estado
        /// </summary>
        public Int16 EstadoId
        {
            get { return _EstadoId; }
            set { _EstadoId = value; }
        }

        /// <summary>
        ///     Texto de búsqueda en el catálogo de edificios
        /// </summary>
        public string BusquedaRapida
        {
            get { return _BusquedaRapida; }
            set { _BusquedaRapida = value; }
        }

        /// <summary>
        ///     Cadena con Ids de proveedores seleccionados
        /// </summary>
        public string CadenaProveedorId
        {
            get { return _CadenaProveedorId; }
            set { _CadenaProveedorId = value; }
        }

        /// <summary>
        ///     Campo para buscar proveedores por nombre exacto
        /// </summary>
        public string BuscarNombre
        {
            get { return _BuscarNombre; }
            set { _BuscarNombre = value; }
        }
    }
}
