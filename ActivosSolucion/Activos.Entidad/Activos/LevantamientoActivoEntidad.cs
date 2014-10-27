using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class LevantamientoActivoEntidad : Base
    {
        private int _LevantamientoID;            //Identificador del levantamiento
        private Int16 _ActivoId;                // Identificador del activo
        private string _CodigoBarrasParticular; // Código de barras particulas del activo
        private int _EmpleadoId;              //Numero de Empleado
        private Int16 _EstatusId;
        private Int16 _UsuarioIdInserto;        // Identificador del usuario que inserto el registro

        // Otros campos
        private string _CadenaActivosXML;    // Cadena usado como XML para guardar los activos en levantamiento

        public LevantamientoActivoEntidad()
        {
            _LevantamientoID = 0;
            _ActivoId = 0;
            _CodigoBarrasParticular = string.Empty;
            _EmpleadoId = 0;
            _EstatusId = 0;
            _UsuarioIdInserto = 0;
            _CadenaActivosXML = string.Empty;
        }

        public int LevantamientoID
        {
            get { return _LevantamientoID; }
            set { _LevantamientoID = value; }
        }

        public Int16 ActivoId
        {
            get { return _ActivoId; }
            set { _ActivoId = value; }
        }

        public string CodigoBarrasParticular
        {
            get { return _CodigoBarrasParticular; }
            set { _CodigoBarrasParticular = value; }
        }

        public int EmpleadoId
        {
            get { return _EmpleadoId; }
            set { _EmpleadoId = value; }
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

        public string CadenaActivosXML
        {
            get { return _CadenaActivosXML; }
            set { _CadenaActivosXML = value; }
        }
    }
}
