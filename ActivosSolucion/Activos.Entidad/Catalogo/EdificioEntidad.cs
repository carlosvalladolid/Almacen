using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo
{
    public class EdificioEntidad : Base
    {
        private Int16 _EdificioId;                  // Identificador de edificio
        private Int16 _CiudadId;                    // Identificador de ciudad
        private Int16 _EstatusId;                   // Identificador de estatus
        private Int16 _UsuarioIdInserto;            // Identificador del usuario que creó el registro
        private Int16 _UsuarioIdModifico;           // Identificador del usuario que modificó el registro por última vez
        private string _Nombre;                     // Nombre del edificio
        private string _Calle;                      // Nombre de la calle
        private string _Numero;                     // Numero de la calle
        private string _Colonia;                    // Nombre de la colonia
        private string _NumeroInt;                  // Numero Interior
        private string _CodigoPostal;               // Numero del codigo postal
        private string _NombreArrendado;            // Nombre del arrendado
        private string _TelefonoArrendado;          // Telefono del arrendado
        private string _EmailArrendado;             // Email del arrendado
        private string _FechaInserto;               // Fecha en formato texto en que se creó el registro
        private string _FechaUltimaModificacion;    // Fecha en formato texto de la última vez que se modificó el registro

        // Otros campos
        private string _BusquedaRapida;             // Texto de búsqueda en el catálogo de edificios
        private Int16 _EstadoId;                    // Identificador del estado
        private string _CadenaEdificioId;           // Cadena con Ids de edificios seleccionados
        private string _BuscarNombre;               // Campo para buscar edificios por nombre exacto


        public EdificioEntidad()
        {
            _EdificioId = 0;
            _CiudadId = 0;
            _EstatusId = 0;
            _UsuarioIdInserto = 0;
            _UsuarioIdModifico = 0;
            _Nombre = string.Empty;
            _Calle = string.Empty;
            _Numero = string.Empty;
            _Colonia = string.Empty;
            _NumeroInt = string.Empty;
            _CodigoPostal = string.Empty;
            _NombreArrendado = string.Empty;
            _TelefonoArrendado = string.Empty;
            _EmailArrendado = string.Empty;
            _FechaInserto = string.Empty;
            _FechaUltimaModificacion = string.Empty;
            _BusquedaRapida = string.Empty;
            _EstadoId = 0;
            _CadenaEdificioId = string.Empty;
            _BuscarNombre = string.Empty;
        }

        public Int16 EdificioId
        {
            get { return _EdificioId; }
            set { _EdificioId = value; }
        }

        public Int16 CiudadId
        {
            get { return _CiudadId; }
            set { _CiudadId = value; }
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
        public string NumeroInt
        {
            get { return _NumeroInt; }
            set { _NumeroInt = value; }
        }
        public string CodigoPostal
        {
            get { return _CodigoPostal; }
            set { _CodigoPostal = value; }
        }
        public string NombreArrendado
        {
            get { return _NombreArrendado; }
            set { _NombreArrendado = value; }
        }
        public string TelefonoArrendado
        {
            get { return _TelefonoArrendado; }
            set { _TelefonoArrendado = value; }
        }
        public string EmailArrendado
        {
            get { return _EmailArrendado; }
            set { _EmailArrendado = value; }
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

        public string CadenaEdificioId
        {
            get { return _CadenaEdificioId; }
            set { _CadenaEdificioId = value; }
        }

        public string BuscarNombre
        {
            get { return _BuscarNombre; }
            set { _BuscarNombre = value; }
        }

    }
}
