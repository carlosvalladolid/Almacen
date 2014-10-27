using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Mantenimiento
{
    public class TipoAsistenciaEntidad : Base
    {

        private Int16 _TipoAsistenciaId;           // Identificador del tipo de asistencia
        private string _Nombre;                     // Nombre del tipo de asistencia

        public TipoAsistenciaEntidad()
        {
            _TipoAsistenciaId = 0;
            _Nombre = string.Empty;
        }

        public Int16 TipoAsistenciaId
        {
            get { return _TipoAsistenciaId; }
            set { _TipoAsistenciaId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

    }
}
