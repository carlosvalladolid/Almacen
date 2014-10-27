using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class TipoActivoEntidad : Base
    {

         private Int16 _TipoActivoId;           // Identificador del tipo de activo
        private string _Nombre;                 // Nombre del tipo de activo

        public TipoActivoEntidad()
        {
            _TipoActivoId = 0;
            _Nombre = string.Empty;
        }

        public Int16 TipoActivoId
        {
            get { return _TipoActivoId; }
            set { _TipoActivoId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

    }
}
