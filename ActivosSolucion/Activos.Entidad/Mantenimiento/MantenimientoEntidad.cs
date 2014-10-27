using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Mantenimiento
{
    public class MantenimientoEntidad : Base
    {

        private int _MantenimientoId;           // Identificador del movimiento
        private Int16 _EmpleadoId;              // Identificador del empleado
        private Int16 _DepartamentoId;          // Identificador del departamento
        private Int16 _UsuarioIdInserto;        // Identificador del usuario que realiza la transaccion

        //Otros campos
        private string _SesionId;                       // Almacena una cadena aleatoria de la sesion
        private Int16 _EstatusId;                       // Identificador del estatus

        public MantenimientoEntidad()
        {
            _MantenimientoId = 0;
            _EmpleadoId = 0;
            _DepartamentoId = 0;
            _UsuarioIdInserto = 0;
            _SesionId = string.Empty;
            _EstatusId = 0;
            
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

        public Int16 DepartamentoId
        {
            get { return _DepartamentoId; }
            set { _DepartamentoId = value; }
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

        public Int16 EstatusId
        {
            get { return _EstatusId; }
            set { _EstatusId = value; }
        }

    }
}
