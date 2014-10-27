using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo
{
    public class DependenciaEntidad : Base
    {
        private Int16 _DependenciaId;                       // Identificador de dependencia
        private string _Nombre;                             // Nombre del usuario

        public DependenciaEntidad()
        {
            _DependenciaId = 0;
            _Nombre = string.Empty;
        }

        public Int16 DependenciaId
        {
            get { return _DependenciaId; }
            set { _DependenciaId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
    }
}

