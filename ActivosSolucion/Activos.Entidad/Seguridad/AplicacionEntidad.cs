using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Seguridad
{
    public class AplicacionEntidad : Base
    {
        private Int16 _AplicacionId;                // Identificador de la aplicacion
        private string _Nombre;                     // Nombre de la aplicacion
        private string _Descripcion;                // Descripcion de la aplicacion

         public AplicacionEntidad()
        {
             _AplicacionId = 0;
            _Nombre = string.Empty;
            _Descripcion = string.Empty;
        }

         public Int16 AplicacionId
         {
             get { return _AplicacionId; }
             set { _AplicacionId = value; }
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
