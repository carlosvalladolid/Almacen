using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo
{
    public class ProveedorEntidad : Base
    {
        private Int16 _ProveedorId;                 // Identificador de proveedor
        private Int16 _DependenciaId;               // Identificador de dependencia
        private Int16 _CiudadId;                    // Identificador de ciudad
        private Int16 _BancoId;                     // Identificador de banco
        private Int16 _UsuarioIdInserto;            // Identificador del usuario que creó el registro
        private Int16 _UsuarioIdModifico;           // Identificador del usuario que modificó el registro por última vez
        private string _Nombre;                     // Nombre del proveedor
        private string _RFC;                        // RFC del proveedor
        private string _Calle;                      // Nombre de la calle
        private string _Numero;                     // Numero de la calle
        private string _Colonia;                    // Nombre de la colonia 
        private string _CodigoPostal;               // Numero del codigo postal
        private string _Telefono;                    // Telefono del proveedor
        private string _NombreContacto;             // Nombre del contacto
        private string _Email;                      // Email del proveedor
        private string _Cuenta;                      // Numero de cuenta del proveedor
        private string _Clabe;                       // Clabe del proveedor
        private string _CiudadOtro;                 // Otra ciudad
        private string _FechaInserto;               // Fecha en formato texto en que se creó el registro
        private string _FechaUltimaModificacion;    // Fecha en formato texto de la última vez que se modificó el registro

        // Otros campos
        private string _BusquedaRapida;             // Texto de búsqueda en el catálogo de edificios
        private Int16 _EstadoId;                    // Identificador del estado
        private string _CadenaProveedorId;          // Cadena con Ids de proveedores seleccionados
        private string _BuscarNombre;               // Campo para buscar proveedores por nombre exacto

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
            _BusquedaRapida = string.Empty;
            _EstadoId = 0;
            _CadenaProveedorId = string.Empty;
            _BuscarNombre = string.Empty;
        }

        public Int16 ProveedorId
        {
            get { return _ProveedorId; }
            set { _ProveedorId = value; }
        }

        public Int16 DependenciaId
        {
            get { return _DependenciaId; }
            set { _DependenciaId = value; }
        }

        public Int16 CiudadId
        {
            get { return _CiudadId; }
            set { _CiudadId = value; }
        }

        public Int16 BancoId
        {
            get { return _BancoId; }
            set { _BancoId = value; }
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

        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }

        public string Calle
        {
            get { return _Calle; }
            set { _Calle = value; }
        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public string Colonia
        {
            get { return _Colonia; }
            set { _Colonia = value; }
        }
        
        public string CodigoPostal
        {
            get { return _CodigoPostal; }
            set { _CodigoPostal = value; }
        }

        public string NombreContacto
        {
            get { return _NombreContacto; }
            set { _NombreContacto = value; }
        }

        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Cuenta
        {
            get { return _Cuenta; }
            set { _Cuenta = value; }
        }

        public string Clabe
        {
            get { return _Clabe; }
            set { _Clabe = value; }
        }

        public string CiudadOtro
        {
            get { return _CiudadOtro; }
            set { _CiudadOtro = value; }
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

        public string BusquedaRapida
        {
            get { return _BusquedaRapida; }
            set { _BusquedaRapida = value; }
        }

        public Int16 EstadoId
        {
            get { return _EstadoId; }
            set { _EstadoId = value; }
        }

        public string CadenaProveedorId
        {
            get { return _CadenaProveedorId; }
            set { _CadenaProveedorId = value; }
        }

        public string BuscarNombre
        {
            get { return _BuscarNombre; }
            set { _BuscarNombre = value; }
        }

    }
}
