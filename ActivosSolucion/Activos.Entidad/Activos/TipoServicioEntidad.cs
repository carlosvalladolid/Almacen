using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class TipoServicioEntidad : Base
    {
        private Int16 _TipoServicioId;           // Identificador del tipo de servicio
        private string _Nombre;                 // Nombre del tipo de servicio

        public TipoServicioEntidad()
        {
            _TipoServicioId = 0;
            _Nombre = string.Empty;
        }

        public Int16 TipoServicioId
        {
            get { return _TipoServicioId; }
            set { _TipoServicioId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
    }
}
