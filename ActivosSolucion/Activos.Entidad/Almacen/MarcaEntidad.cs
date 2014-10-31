using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
   public class MarcaEntidad:Base
    {
        private Int16 _MarcaId;                     // Identificador de marca
        private Int16 _DependenciaId;               // Identificador de dependencia
        private string _Nombre;                     // Nombre de la marca
        private Int16 _EstatusId;                   // Identificador de estatus

        //****************************************+
        private string _BusquedaRapida;             // Texto de búsqueda en el catálogo de edificios
        private string _CadenaMarcaId;              // Cadena con Ids de marcas seleccionados
        private string _BuscarNombre;               // Campo para buscar marcas por nombre exacto


        public MarcaEntidad()
        {
            _MarcaId = 0;
            _DependenciaId = 0;
            _EstatusId = 0;
            _Nombre = string.Empty;
            _BusquedaRapida = string.Empty;
            _CadenaMarcaId = string.Empty;
            _BuscarNombre = string.Empty;
        }

        public Int16 MarcaId
        {
            get { return _MarcaId; }
            set { _MarcaId = value; }
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
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string BusquedaRapida
        {
            get { return _BusquedaRapida; }
            set { _BusquedaRapida = value; }
        }

        public string CadenaMarcaId
        {
            get { return _CadenaMarcaId; }
            set { _CadenaMarcaId = value; }
        }

        public string BuscarNombre
        {
            get { return _BuscarNombre; }
            set { _BuscarNombre = value; }
        }
    }
}

