using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Seguridad
{
    public class EstatusEntidad : Base
    {
        private Int16 _EstatusId;                   // Identificador del estatus
        private Int16 _SeccionId;                   // Identificador de seccion
        private string _Nombre;                     // Nombre del usuario
        private string _Descripcion;                // Descripcion del usuario

        public EstatusEntidad()
        {
             _EstatusId = 0;
             _SeccionId = 0;
            _Nombre = string.Empty;
            _Descripcion = string.Empty;
        }

        public Int16 EstatusId
        {
            get { return _EstatusId; }
            set { _EstatusId = value; }
        }

        public Int16 SeccionId
        {
            get { return _SeccionId; }
            set { _SeccionId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
    }
}
