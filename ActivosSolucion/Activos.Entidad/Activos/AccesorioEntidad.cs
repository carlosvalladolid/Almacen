using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class AccesorioEntidad : Base
    {

        private int _AccesorioId;             // Identificador del accesorio
        private int _ActivoId;                // Identificador del activo al que pertenece el accesorio
        private int _ActivoAccesorioId;       // Identificador del activo que es accesorio
        private Int16 _TipoAccesorioId;         // Identificador del tipo de accesorio
        private Int16 _UsuarioIdInserto;        // Identificador del usuario que creó el registro
        private string _Descripcion;            // Descripción del accesorio

        // Otros campos
        private int _TemporalActivoId;          // Identificador del activo temporal de donde se seleccionarán los accesorios temporales
        private string _GrupoActivoAccesorioId; // Grupo de identificadores  de activos accesorios separados por coma
        private int _CompraId;                  // Identificador de la compra

        public AccesorioEntidad()
        {
            _AccesorioId = 0;
            _ActivoId = 0;
            _ActivoAccesorioId = 0;
            _TipoAccesorioId = 0;
            _UsuarioIdInserto = 0;
            _Descripcion = string.Empty;
            _TemporalActivoId = 0;
            _GrupoActivoAccesorioId = string.Empty;
            _CompraId = 0;
        }

        public int AccesorioId
        {
            get { return _AccesorioId; }
            set { _AccesorioId = value; }
        }

        public int ActivoId
        {
            get { return _ActivoId; }
            set { _ActivoId = value; }
        }

        public int ActivoAccesorioId
        {
            get { return _ActivoAccesorioId; }
            set { _ActivoAccesorioId = value; }
        }

        public Int16 TipoAccesorioId
        {
            get { return _TipoAccesorioId; }
            set { _TipoAccesorioId = value; }
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

        public int TemporalActivoId
        {
            get { return _TemporalActivoId; }
            set { _TemporalActivoId = value; }
        }

        public string GrupoActivoAccesorioId
        {
            get { return _GrupoActivoAccesorioId; }
            set { _GrupoActivoAccesorioId = value; }
        }

        public int CompraId
        {
            get { return _CompraId; }
            set { _CompraId = value; }
        }

    }
}
