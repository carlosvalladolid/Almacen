using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo
{
    public class CiudadEntidad : Base
    {
        private Int16 _CiudadId;                       // Identificador de ciudad
        private Int16 _EstadoId;                       // Identificador de estado
        private string _Nombre;                        // Nombre de la entidad

        public CiudadEntidad()
        {
            _CiudadId = 0;
            _EstadoId = 0;
            _Nombre = string.Empty;
        }

        public Int16 CiudadId
        {
            get { return _CiudadId; }
            set { _CiudadId = value; }
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
