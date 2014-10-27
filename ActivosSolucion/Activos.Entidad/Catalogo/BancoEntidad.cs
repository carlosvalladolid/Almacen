using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo
{
    public class BancoEntidad : Base
    {
        private Int16 _BancoId;                       // Identificador de Banco
        private string _Nombre;                        // Nombre de la entidad

        public BancoEntidad()
        {
            _BancoId = 0;
            _Nombre = string.Empty;
        }

        public Int16 BancoId
        {
            get { return _BancoId; }
            set { _BancoId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
    }
}
