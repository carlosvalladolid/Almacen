using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Mantenimiento
{
    public class TemporalMantenimientoEmpleadoEntidad : Base
    {

        private string _SesionId;               // Almacena una cadena aleatoria de la sesion
        private Int16 _EmpleadoId;              // Identificador del empleado
        private Int16 _UsuarioIdInserto;        // Identificador del usuario que realiza la transaccion

        //Otros campos
        private int _MantenimientoId;           // Identificador del mantenimiento

        public TemporalMantenimientoEmpleadoEntidad()
        {
            _SesionId = string.Empty;
            _EmpleadoId = 0;
            _UsuarioIdInserto = 0;
            _MantenimientoId = 0;
            
        }

        public string SesionId
        {
            get { return _SesionId; }
            set { _SesionId = value; }
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

        public int MantenimientoId
        {
            get { return _MantenimientoId; }
            set { _MantenimientoId = value; }
        }

    }
}
