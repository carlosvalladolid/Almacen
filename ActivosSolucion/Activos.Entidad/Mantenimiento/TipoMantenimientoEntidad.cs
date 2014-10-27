using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Mantenimiento
{
    public class TipoMantenimientoEntidad : Base
    {

        private Int16 _TipoMantenimientoId;           // Identificador del tipo de mantenimiento
        private string _Nombre;                     // Nombre del tipo de mantenimiento

        public TipoMantenimientoEntidad()
        {
            _TipoMantenimientoId = 0;
            _Nombre = string.Empty;
        }

        public Int16 TipoMantenimientoId
        {
            get { return _TipoMantenimientoId; }
            set { _TipoMantenimientoId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

    }
}
