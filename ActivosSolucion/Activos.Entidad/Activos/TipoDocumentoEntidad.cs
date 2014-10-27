using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class TipoDocumentoEntidad : Base
    {

        private Int16 _TipoDocumentoId;         // Identificador del tipo de documento
        private string _Nombre;                 // Nombre del tipo de documento

        public TipoDocumentoEntidad()
        {
            _TipoDocumentoId = 0;
            _Nombre = string.Empty;
        }

        public Int16 TipoDocumentoId
        {
            get { return _TipoDocumentoId; }
            set { _TipoDocumentoId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

    }
}
