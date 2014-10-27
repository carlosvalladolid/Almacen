using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class TipoAccesorioEntidad : Base
    {

        private Int16 _TipoAccesorioId;         // Identificador del tipo de accesorio
        private string _Nombre;                 // Nombre del tipo de accesorio

        public TipoAccesorioEntidad()
        {
            _TipoAccesorioId = 0;
            _Nombre = string.Empty;
        }

        public Int16 TipoAccesorioId
        {
            get { return _TipoAccesorioId; }
            set { _TipoAccesorioId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

    }
}
