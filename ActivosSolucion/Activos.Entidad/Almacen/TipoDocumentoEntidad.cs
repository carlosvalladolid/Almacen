using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
  public class TipoDocumentoEntidad:Base
    {


        private Int16 _TipoDocumentoId;
        private string _Nombre;
        // Otros campos
        private Int16 _EstadoId;
        private string _BusquedaRapida;
        private string _CadenaTipoDocumentoId;
        private string _BuscarNombre;

        public TipoDocumentoEntidad()
        {
            _TipoDocumentoId = 0;
            _Nombre = string.Empty;
         }



        /// <summary>
        ///     Identificador de TipoDocumento
        /// </summary>
        public Int16 TipoDocumentoId
        {
            get { return _TipoDocumentoId; }
            set { _TipoDocumentoId = value; }
        }


        /// <summary>
        ///     Identificador de TipoDocumento
        /// </summary>
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

        /// <summary>
        ///     Cadena con Ids de TipoDocumento seleccionados
        /// </summary>
        public string CadenaTipoDocumentoId
        {
            get { return _CadenaTipoDocumentoId; }
            set { _CadenaTipoDocumentoId = value; }
        }

        /// <summary>
        ///     Campo para buscar TipoDocumento por nombre exacto
        /// </summary>
        public string BuscarNombre
        {
            get { return _BuscarNombre; }
            set { _BuscarNombre = value; }
        }



    }
}
