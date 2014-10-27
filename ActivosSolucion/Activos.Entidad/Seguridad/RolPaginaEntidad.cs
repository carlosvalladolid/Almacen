using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Seguridad
{
    public class RolPaginaEntidad : Base
    {
        private Int16 _PaginaId;                    // Identificador de la pagina
        private Int16 _RolId;                       // Identificador del rol

        public RolPaginaEntidad()
        {
             _RolId = 0;
             _PaginaId = 0;
            
        }

        public Int16 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        public Int16 PaginaId
        {
            get { return _PaginaId; }
            set { _PaginaId = value; }
        }
    }
}
