using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Seguridad
{
    public class SeccionEntidad : Base
    {
        private Int16 _SeccionId;                   // Identificador de seccion
        private string _Nombre;                     // Nombre de la aplicacion
        private string _Descripcion;                // Descripcion de la aplicacion

         public SeccionEntidad()
        {
             _SeccionId = 0;
            _Nombre = string.Empty;
            _Descripcion = string.Empty;
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
