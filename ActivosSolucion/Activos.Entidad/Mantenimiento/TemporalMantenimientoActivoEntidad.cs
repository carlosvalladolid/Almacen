using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Mantenimiento
{
    public class TemporalMantenimientoActivoEntidad : Base
    {

        private int _TemporalMantenimientoActivoId;     // Identificador unico del registro
        private string _SesionId;                       // Almacena una cadena aleatoria de la sesion
        private int _ActivoId;                          // Identificador del activo
        private Int16 _TipoAsistenciaId;                // Identificador del tipo se asistencia
        private Int16 _TipoMantenimientoId;             // Identificador del tipo de mantenimiento
        private Int16 _EstatusId;                       // Identificador del estatus
        private string _Descripcion;                    // Descripción de la asistencia

        //Otros campos
        private string _ActivosXML;                     // Cadena usado como XML para guardar los activos

        public TemporalMantenimientoActivoEntidad()
        {
            _TemporalMantenimientoActivoId = 0;
            _SesionId = string.Empty;
            _ActivoId = 0;
            _TipoAsistenciaId = 0;
            _TipoMantenimientoId = 0;
            _EstatusId = 0;
            _Descripcion = string.Empty;
            _ActivosXML = string.Empty;
        }

        public int TemporalMantenimientoActivoId
        {
            get { return _TemporalMantenimientoActivoId; }
            set { _TemporalMantenimientoActivoId = value; }
        }

        public string SesionId
        {
            get { return _SesionId; }
            set { _SesionId = value; }
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

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string ActivosXML
        {
            get { return _ActivosXML; }
            set { _ActivosXML = value; }
        }

    }
}
