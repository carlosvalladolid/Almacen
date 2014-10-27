using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo 
{
    public class PuestoEntidad : Base
    {
        private Int16 _PuestoId;                    // Identificador de puesto
        private Int16 _DependenciaId;               // Identificador de dependencia
        private Int16 _EstatusId;                   // Identificador de estatus
        private Int16 _UsuarioIdInserto;            // Identificador del usuario que creó el registro
        private Int16 _UsuarioIdModifico;           // Identificador del usuario que modificó el registro por última vez
        private string _Nombre;                     // Nombre del proveedor
        private string _FechaInserto;               // Fecha en formato texto en que se creó el registro
        private string _FechaUltimaModificacion;    // Fecha en formato texto de la última vez que se modificó el registro

        // Otros campos
        private string _BusquedaRapida;             // Texto de búsqueda en el catálogo de departamentos
        private string _CadenaPuestoId;             // Cadena con Ids de puestos seleccionados
        private string _BuscarNombre;               // Campo para buscar puestos por nombre exacto

        public PuestoEntidad()
        {
            _PuestoId = 0;
            _DependenciaId = 0;
            _EstatusId = 0;
            _UsuarioIdInserto = 0;
            _UsuarioIdModifico = 0;
            _Nombre = string.Empty;
            _FechaInserto = string.Empty;
            _FechaUltimaModificacion = string.Empty;
            _BusquedaRapida = string.Empty;
            _CadenaPuestoId = string.Empty;
            _BuscarNombre = string.Empty;
        }

        public Int16 PuestoId
        {
            get { return _PuestoId; }
            set { _PuestoId = value; }
        }

        public Int16 DependenciaId
        {
            get { return _DependenciaId; }
            set { _DependenciaId = value; }
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

        public string CadenaPuestoId
        {
            get { return _CadenaPuestoId; }
            set { _CadenaPuestoId = value; }
        }

        public string BuscarNombre
        {
            get { return _BuscarNombre; }
            set { _BuscarNombre = value; }
        }
    }
}
