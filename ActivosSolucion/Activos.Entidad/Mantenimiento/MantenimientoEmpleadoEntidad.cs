using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Mantenimiento
{
    public class MantenimientoEmpleadoEntidad : Base
    {

        private int _MantenimientoId;          // Identificador del movimiento
        private Int16 _EmpleadoId;              // Identificador del empleado
        private Int16 _UsuarioIdInserto;        // Identificador del usuario que realiza la transaccion

        //Otros campos
        private string _SesionId;               // Almacena una cadena aleatoria de la sesion

        public MantenimientoEmpleadoEntidad()
        {
            _MantenimientoId = 0;
            _EmpleadoId = 0;
            _UsuarioIdInserto = 0;
            _SesionId = string.Empty;
            
        }

        public int MantenimientoId
        {
            get { return _MantenimientoId; }
            set { _MantenimientoId = value; }
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

        public string SesionId
        {
            get { return _SesionId; }
            set { _SesionId = value; }
        }

    }
}
