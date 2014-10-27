using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class TemporalAccesorioEntidad : Base
    {

        private int _TemporalAccesorioId;       // Identificador del accesorio temporal
        private int _TemporalActivoId;          // Identificador del activo temporal al que pertenece el accesorio temporal
        private Int16 _TipoAccesorioId;         // Identificador del tipo de accesorio
        private int _ActivoAccesorioId;       // Identificador del activo accesorio
        private string _Descripcion;            // Descripcion del accesorio temporal
        private Int16 _Estatus;                 // Estatus del accesorio temporal

        // Otros campos
        private string _GrupoEstatus;           // Grupo de estatus de accesorio temporal separados por coma
        private int _TemporalCompraId;          // Identificador de la compra temporal

        public TemporalAccesorioEntidad()
        {
            _TemporalAccesorioId = 0;
            _TemporalActivoId = 0;
            _TipoAccesorioId = 0;
            _ActivoAccesorioId = 0;
            _Descripcion = string.Empty;
            _Estatus = 0;
            _GrupoEstatus = string.Empty;
            _TemporalCompraId = 0;
        }

        public int TemporalAccesorioId
        {
            get { return _TemporalAccesorioId; }
            set { _TemporalAccesorioId = value; }
        }

        public int TemporalActivoId
        {
            get { return _TemporalActivoId; }
            set { _TemporalActivoId = value; }
        }

        public Int16 TipoAccesorioId
        {
            get { return _TipoAccesorioId; }
            set { _TipoAccesorioId = value; }
        }

        public int ActivoAccesorioId
        {
            get { return _ActivoAccesorioId; }
            set { _ActivoAccesorioId = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public Int16 Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        public string GrupoEstatus
        {
            get { return _GrupoEstatus; }
            set { _GrupoEstatus = value; }
        }

        public int TemporalCompraId
        {
            get { return _TemporalCompraId; }
            set { _TemporalCompraId = value; }
        }

    }
}
