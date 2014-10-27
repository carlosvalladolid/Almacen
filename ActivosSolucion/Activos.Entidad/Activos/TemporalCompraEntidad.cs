using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class TemporalCompraEntidad : Base
    {

        private int _TemporalCompraId;          // Identificador de la compra temporal
        private Int16 _UsuarioId;               // Identificador del usuario que inserta el registroc

        public TemporalCompraEntidad()
        {
            _TemporalCompraId = 0;
            _UsuarioId = 0;
        }

        public int TemporalCompraId
        {
            get { return _TemporalCompraId; }
            set { _TemporalCompraId = value; }
        }

        public Int16 UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

    }
}
