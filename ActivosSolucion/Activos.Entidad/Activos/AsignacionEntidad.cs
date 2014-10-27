using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class AsignacionEntidad : Base
    {

        private Int16 _ActivoId;                // Identificador del activo
        private Int16 _EmpleadoId;              // Identificador del Empleado
        private Int16 _UsuarioIdInserto;        // Identificador del usuario que creó el registro
        private Int16 _UsuarioIdTransferencia;  // Identificador del usuario que transfirio el activo

        // Otros Campos
        private int _TemporalAsignacionId;          // Identificador de la asignación temporal

        public AsignacionEntidad()
        {
            _ActivoId = 0;
            _EmpleadoId = 0;
            _UsuarioIdInserto = 0;
            _UsuarioIdTransferencia = 0;
            _TemporalAsignacionId = 0;
        }

        public Int16 ActivoId
        {
            get { return _ActivoId; }
            set { _ActivoId = value; }
        }

        public Int16 EmpleadoId
        {
            get { return _EmpleadoId; }
            set { _EmpleadoId = value; }
        }

        public Int16 UsuarioIdInserto
        {
            get { return _UsuarioIdInserto; }
            set { _UsuarioIdInserto = value; }
        }

        public Int16 UsuarioIdTransferencia
        {
            get { return _UsuarioIdTransferencia; }
            set { _UsuarioIdTransferencia = value; }
        }

        public int TemporalAsignacionId
        {
            get { return _TemporalAsignacionId; }
            set { _TemporalAsignacionId = value; }
        }

    }
}
