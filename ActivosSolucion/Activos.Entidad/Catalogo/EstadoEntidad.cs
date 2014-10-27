using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo
{
    public class EstadoEntidad : Base
    {
        private Int16 _EstadoId;                       // Identificador de estado
        private string _Nombre;                        // Nombre de la entidad

        public EstadoEntidad()
        {
            _EstadoId = 0;
            _Nombre = string.Empty;
        }

        public Int16 EstadoId
        {
            get { return _EstadoId; }
            set { _EstadoId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
    }
}
