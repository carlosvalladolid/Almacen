using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class TemporalAsignacionEntidad : Base
    {
        private int _TemporalAsignacionId;          // Identificador de la asignación temporal
        private int _TemporalAsignacionDetalleId;   // Identificador del detalle de la asignación temporal
        private Int16 _ActivoId;                    // Identificador del activo
        private Int16 _CondicionId;                 // Identificador de la condición del activo
        private Int16 _UsuarioId;                   // Identificador del usuario que realiza la transacción
        private Int16 _UbicacionActivoId;           // Identificador de la ubicación del activo

        public TemporalAsignacionEntidad()
        {
            _TemporalAsignacionId = 0;
            _TemporalAsignacionDetalleId = 0;
            _ActivoId = 0;
            _CondicionId = 0;
            _UsuarioId = 0;
            _UbicacionActivoId = 0;
        }

        public int TemporalAsignacionId
        {
            get { return _TemporalAsignacionId; }
            set { _TemporalAsignacionId = value; }
        }

        public int TemporalAsignacionDetalleId
        {
            get { return _TemporalAsignacionDetalleId; }
            set { _TemporalAsignacionDetalleId = value; }
        }

        public Int16 ActivoId
        {
            get { return _ActivoId; }
            set { _ActivoId = value; }
        }

        public Int16 CondicionId
        {
            get { return _CondicionId; }
            set { _CondicionId = value; }
        }

        public Int16 UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        public Int16 UbicacionActivoId
        {
            get { return _UbicacionActivoId; }
            set { _UbicacionActivoId = value; }
        }

    }
}
