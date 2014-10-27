using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Seguridad
{
    public class RolEntidad : Base
    {
        private Int16 _RolId;                       // Identificador del rol
        private string _Nombre;                     // Nombre del usuario
        private string _Descripcion;                // Descripcion del usuario

        //Otros campos
        private Int16 _PaginaId;                    // Identificador de la pagina
        
        public RolEntidad()
        {
            _RolId = 0;
            _Nombre = string.Empty;
            _Descripcion = string.Empty;
            _PaginaId = 0;
        }

        public Int16 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
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

        public Int16 PaginaId
        {
            get { return _PaginaId; }
            set { _PaginaId = value; }
        }
    }
}
