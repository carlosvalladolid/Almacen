using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class CondicionEntidad : Base
    {

        private Int16 _CondicionId;           // Identificador de la condición
        private string _Nombre;               // Nombre del tipo de activo

        public CondicionEntidad()
        {
            _CondicionId = 0;
            _Nombre = string.Empty;
        }

        public Int16 CondicionId
        {
            get { return _CondicionId; }
            set { _CondicionId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

    }
}
