using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Seguridad
{
    public class PaginaEntidad : Base
    {
        private Int16 _PaginaId;                    // Identificador de la pagina
        private string _Nombre;                     // Nombre de la pagina
        private string _Descripcion;                // Descripcion de la pagina
        private string _URL;                        // URL de la pagina
        private string _RutaIcono;                  // RutaIcono de la pagina

        public PaginaEntidad()
        {
             _PaginaId = 0;
            _Nombre = string.Empty;
            _Descripcion = string.Empty;
            _URL = string.Empty;
            _RutaIcono = string.Empty;
        }

        public Int16 PaginaId
        {
            get { return _PaginaId; }
            set { _PaginaId = value; }
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

        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }

        public string RutaIcono
        {
            get { return _RutaIcono; }
            set { _RutaIcono = value; }
        }
    }
}
