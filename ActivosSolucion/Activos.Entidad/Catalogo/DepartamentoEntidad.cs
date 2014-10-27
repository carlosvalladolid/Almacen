using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo
{
    public class DepartamentoEntidad : Base
    {
        private Int16 _DepartamentoId;                  // Identificador de departamento
        private Int16 _DireccionId;                     // Identificador de direccion
        private Int16 _EstatusId;                       // Identificador de estatus
        private Int16 _UsuarioIdInserto;                // Identificador del usuario que creó el registro
        private Int16 _UsuarioIdModifico;               // Identificador del usuario que modificó el registro por última vez
        private string _Nombre;                         // Nombre del usuario
        private string _FechaInserto;                   // Fecha en formato texto en que se creó el registro
        private string _FechaUltimaModificacion;        // Fecha en formato texto de la última vez que se modificó el registro

        // Otros campos
        private string _BusquedaRapida;                 // Texto de búsqueda en el catálogo de departamentos
        private string _CadenaDepartamentoId;           // Cadena con Ids de departamentos seleccionados
        private string _BuscarNombre;                   // Campo para buscar empleados por nombre exacto

        public DepartamentoEntidad()
        {
            _DepartamentoId = 0;
            _DireccionId = 0;
            _EstatusId = 0;
            _UsuarioIdInserto = 0;
            _UsuarioIdModifico = 0;
            _Nombre = string.Empty;
            _FechaInserto = string.Empty;
            _FechaUltimaModificacion = string.Empty;
            _BusquedaRapida = string.Empty;
        }

        public Int16 DepartamentoId
        {
            get { return _DepartamentoId; }
            set { _DepartamentoId = value; }
        }

        public Int16 DireccionId
        {
            get { return _DireccionId; }
            set { _DireccionId = value; }
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

        public string CadenaDepartamentoId
        {
            get { return _CadenaDepartamentoId; }
            set { _CadenaDepartamentoId = value; }
        }

        public string BuscarNombre
        {
            get { return _BuscarNombre; }
            set { _BuscarNombre = value; }
        }
    }
}
