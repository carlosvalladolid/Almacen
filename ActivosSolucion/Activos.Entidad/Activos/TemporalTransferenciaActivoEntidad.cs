using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class TemporalTransferenciaActivoEntidad : Base
    {
        private string _SesionId;            // SesionId del usuario
        private int _ActivoId;               // Identificador del Activo
        private Int16 _CondicionId;          // Identificador de la condición del activo
        private Int16 _UbicacionActivoId;    // Identificador de la ubicacion del activo

        //Otros campos
        private int _CompraId;             // Identificador de la compra

        public TemporalTransferenciaActivoEntidad()
        {
            _SesionId = string.Empty;
            _ActivoId = 0;
            _CondicionId = 0;
            _UbicacionActivoId = 0;
            _CompraId = 0;
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

        public Int16 CondicionId
        {
            get { return _CondicionId; }
            set { _CondicionId = value; }
        }

        public Int16 UbicacionActivoId
        {
            get { return _UbicacionActivoId; }
            set { _UbicacionActivoId = value; }
        }

        public int CompraId
        {
            get { return _CompraId; }
            set { _CompraId = value; }
        }

    }
}
