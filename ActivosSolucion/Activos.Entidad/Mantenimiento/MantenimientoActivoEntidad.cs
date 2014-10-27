using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Mantenimiento
{
    public class MantenimientoActivoEntidad : Base
    {
        private int _MantenimientoActivoId;     // Identificador unico del registro
        private int _MantenimientoId;           // Identificador del mantenimiento
        private int _ActivoId;                  // Identificador del activo
        private Int16 _TipoAsistenciaId;        // Identificador del tipo se asistencia
        private Int16 _TipoMantenimientoId;     // Identificador del tipo de mantenimiento
        private Int16 _EstatusId;               // Identificador del estatus
        private Int16 _UsuarioIdInserto;        // Identificador del usuario que realiza la transaccion
        private string _Descripcion;            // Descripción de la asistencia

        // Otros campos
        private string _SesionId;               // Almacena una cadena aleatoria de la sesion
        private string _StrFechaInicio;         // Fecha de inicio de rango
        private string _StrFechaFin;            // Fecha de fin de rango
        private Int16 _TipoReporte;             // Identificador del tipo de reporte
        private Int16 _TipoMovimientoId;        // Identificador del tipo de movimiento
        private Int16 _EmpleadoId;              // Identificador del empleado

        public MantenimientoActivoEntidad()
        {
            _MantenimientoActivoId = 0;
            _MantenimientoId = 0;
            _ActivoId = 0;
            _TipoMantenimientoId = 0;
            _TipoAsistenciaId = 0;
            _EstatusId = 0;
            _UsuarioIdInserto = 0;
            _Descripcion = string.Empty;
            _SesionId = string.Empty;
            _StrFechaInicio = string.Empty;
            _StrFechaFin = string.Empty;
            _TipoReporte = 0;
            _TipoMovimientoId = 0;
            _EmpleadoId = 0;
        }

        public int MantenimientoActivoId
        {
            get { return _MantenimientoActivoId; }
            set { _MantenimientoActivoId = value; }
        }

        public int MantenimientoId
        {
            get { return _MantenimientoId; }
            set { _MantenimientoId = value; }
        }

        public int ActivoId
        {
            get { return _ActivoId; }
            set { _ActivoId = value; }
        }

        public Int16 TipoAsistenciaId
        {
            get { return _TipoAsistenciaId; }
            set { _TipoAsistenciaId = value; }
        }

        public Int16 TipoMantenimientoId
        {
            get { return _TipoMantenimientoId; }
            set { _TipoMantenimientoId = value; }
        }

        public Int16 EstatusId
        {
            get { return _EstatusId; }
            set { _EstatusId = value; }
        }

        public Int16 UsuarioIdInserto
        {
            get { return _UsuarioIdInserto; }
            set { _UsuarioIdInserto = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string SesionId
        {
            get { return _SesionId; }
            set { _SesionId = value; }
        }

        public string StrFechaInicio
        {
            get { return _StrFechaInicio; }
            set { _StrFechaInicio = value; }
        }

        public string StrFechaFin
        {
            get { return _StrFechaFin; }
            set { _StrFechaFin = value; }
        }

        public Int16 TipoReporte
        {
            get { return _TipoReporte; }
            set { _TipoReporte = value; }
        }

        public Int16 TipoMovimientoId
        {
            get { return _TipoMovimientoId; }
            set { _TipoMovimientoId = value; }
        }

        public Int16 EmpleadoId
        {
            get { return _EmpleadoId; }
            set { _EmpleadoId = value; }
        }

    }
}
